using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class RetirarService
    {
        readonly IUnitOfWork _unitOfWork;

        public RetirarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public RetirarResponse Ejecutar(RetirarRequest request)
        {
            var cuenta = _unitOfWork.ServicioFinancieroRepository.FindFirstOrDefault(t => t.Numero == request.NumeroCuenta);
            if (cuenta != null)
            {
                var errors = cuenta.CanConsignar(request.Valor, request.Ciudad);
                if (errors.Count == 0)
                { 
                    cuenta.Consignar(request.Valor, request.Ciudad);                
                }
                _unitOfWork.Commit();
                return new RetirarResponse() { Mensaje = $"Su Nuevo saldo es {cuenta.Saldo}." };
            }
            else
            {
                return new RetirarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class RetirarRequest
    {
        public string NumeroCuenta { get; set; }
        public decimal Valor { get; set; }
        public string Ciudad { get; set; }
    }
    public class RetirarResponse
    {
        public string Mensaje { get; set; }
    }
}
