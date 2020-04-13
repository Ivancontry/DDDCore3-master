using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Application
{
    public class ConsignarService 
    {
        readonly IUnitOfWork _unitOfWork;
        
        public ConsignarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ConsignarResponse Ejecutar(ConsignarRequest request)
        {
            var cuenta = _unitOfWork.ServicioFinancieroRepository.FindFirstOrDefault(t => t.Numero==request.NumeroCuenta);
            if (cuenta != null)
            {
                
                var errors = cuenta.CanConsignar(request.Valor,request.Ciudad);
                if (errors.Count == 0) {
                    cuenta.Consignar(request.Valor, request.Ciudad);
                }
                _unitOfWork.Commit();
                return new ConsignarResponse() { Mensaje = $"Su Nuevo saldo es {cuenta.Saldo}." };
            }
            else
            {
                return new ConsignarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class ConsignarRequest
    {
        public string NumeroCuenta { get; set; }
        public decimal Valor { get; set; }
        public string Ciudad { get; set; }
    }
    public class ConsignarResponse
    {
        public string Mensaje { get; set; }
    }
}
