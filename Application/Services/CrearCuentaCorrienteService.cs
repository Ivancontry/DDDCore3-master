using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CrearCuentaCorrienteService
    {
        readonly IUnitOfWork _unitOfWork;
        public CrearCuentaCorrienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CrearCuentaCorrienteResponse Ejecutar(CrearCuentaCorrienteRequest request) {
            ServicioFinanciero cuenta = _unitOfWork.ServicioFinancieroRepository.FindFirstOrDefault(t => t.Numero == request.Numero);

            if (cuenta == null)
            {
                CuentaCorriente cuentaCorriente = new CuentaCorriente();
                cuentaCorriente.Numero = request.Numero;
                cuentaCorriente.Nombre = request.Nombre;
                cuentaCorriente.CiudadDeCreacion = request.Ciudad;
                cuentaCorriente.Saldo = request.Saldo;
                cuentaCorriente.TopeGiro = request.TopeGiro;
                cuentaCorriente.FechaCreacion = request.FechaCreacion;

                _unitOfWork.ServicioFinancieroRepository.Add(cuentaCorriente);
                _unitOfWork.Commit();
                return new CrearCuentaCorrienteResponse() { Mensaje = $"Se creó con exito la cuenta {cuentaCorriente.Numero}" };
            }
            else {
                return new CrearCuentaCorrienteResponse() { Mensaje = $"El número de cuenta ya exite" };
            }

        }

    }

    public class CrearCuentaCorrienteResponse
    {
        public string Mensaje { get; set; }
    }

    public class CrearCuentaCorrienteRequest:ServicioFinancieroRequest {
        public decimal TopeGiro
        {
            get;set;
            
        }
    }
}
