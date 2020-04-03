using Domain.Contracts;
using Domain.Entities;
using Domain.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CrearCuentaBancariaService
    {
        readonly IUnitOfWork _unitOfWork;
        
        public CrearCuentaBancariaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CrearCuentaBancariaResponse Ejecutar(CrearCuentaBancariaRequest request)
        {
            ServicioFinanciero cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero==request.Numero);
            if (cuenta == null)
            {
                ServicioFinanciero cuentaNueva = new CuentaBancariaFactory().Create(request.TipoCuenta);//Debe ir un factory que determine que tipo de cuenta se va a crear
                cuentaNueva.Nombre = request.Nombre;
                cuentaNueva.Numero = request.Numero;
                cuentaNueva.CiudadDeCreacion = request.Ciudad;
                
                _unitOfWork.CuentaBancariaRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                return new CrearCuentaBancariaResponse() { Mensaje = $"Se creó con exito la cuenta {cuentaNueva.Numero}." };
            }
            else
            {
                return new CrearCuentaBancariaResponse() { Mensaje = $"El número de cuenta ya exite" };
            }
        }



    }
    public class CrearCuentaBancariaRequest
    {
        public string Nombre { get; set; }
        public string TipoCuenta { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public double TopeGiro { get; set; }
    }

    

    public class CrearCuentaBancariaResponse
    {
        public string Mensaje { get; set; }
    }
}
