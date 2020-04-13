using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CrearCertificadoDepositoTerminoService
    {
        readonly IUnitOfWork _unitOfWork;
        public CrearCertificadoDepositoTerminoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CrearCertificadoDepositoTerminoServiceResponse Ejecutar(CrearCertificadoDepositoTerminoServiceRequest request) {
            ServicioFinanciero cuenta = _unitOfWork.ServicioFinancieroRepository.FindFirstOrDefault(t=> t.Numero==request.Numero);

            if (cuenta == null)
            {
                CertificadoDepositoATermino cdt = new CertificadoDepositoATermino();
                cdt.Numero = request.Numero;
                cdt.Nombre = request.Nombre;
                cdt.CiudadDeCreacion = request.Ciudad;
                cdt.Saldo = request.Saldo;
                cdt.TasaEfectivaAnual = request.TasaEfectivaAnual;
                cdt.TerminoDefinido = request.TerminoDefinido;
                cdt.FechaCreacion = request.FechaCreacion;

                _unitOfWork.ServicioFinancieroRepository.Add(cdt);
                _unitOfWork.Commit();
                return new CrearCertificadoDepositoTerminoServiceResponse() { Mensaje = $"Se creo con Exito el Servicio Finaciero {cdt.Numero}." };
                
            }
            else 
            {
                return new CrearCertificadoDepositoTerminoServiceResponse() { Mensaje = $"El Servicio Financiero {request.Numero} ya existe" };
            }
        }

    }

    public class CrearCertificadoDepositoTerminoServiceRequest: ServicioFinancieroRequest {
        public decimal TasaEfectivaAnual { get; set; }
        public int TerminoDefinido { get; set; }

    }
    public class CrearCertificadoDepositoTerminoServiceResponse { 
        public string Mensaje { get; set; }
    }
}
