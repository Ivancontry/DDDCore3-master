using Domain.Contracts;
using Domain.Entities;
using Domain.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CrearCuentaAhorroService
    {
        readonly IUnitOfWork _unitOfWork;
        
        public CrearCuentaAhorroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CrearCuentaAhorroResponse Ejecutar(CrearCuentaAhorroRequest request)
        {
            ServicioFinanciero cuenta = _unitOfWork.ServicioFinancieroRepository.FindFirstOrDefault(t => t.Numero==request.Numero);
            if (cuenta == null)
            {
                ServicioFinanciero cuentaNueva = new CuentaAhorro(); ///CuentaBancariaFactory().Create(request.TipoCuenta);//Debe ir un factory que determine que tipo de cuenta se va a crear
                cuentaNueva.Nombre = request.Nombre;
                cuentaNueva.Numero = request.Numero;
                cuentaNueva.CiudadDeCreacion = request.Ciudad;
                cuentaNueva.Saldo = request.Saldo;
                cuentaNueva.FechaCreacion = request.FechaCreacion;
                
                _unitOfWork.ServicioFinancieroRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                return new CrearCuentaAhorroResponse() { Mensaje = $"Se creó con exito la cuenta {cuentaNueva.Numero}" };
            }
            else
            {
                return new CrearCuentaAhorroResponse() { Mensaje = $"El número de cuenta ya existe" };
            }
        }



    }
    public class CrearCuentaAhorroRequest:ServicioFinancieroRequest
    {
       
    }   

    public class CrearCuentaAhorroResponse
    {
        public string Mensaje { get; set; }
    }
}
