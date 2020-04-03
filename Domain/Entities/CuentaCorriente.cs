using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaCorriente : ServicioFinanciero
    {
      
        public const double VALORPRIMERACONSIGNACCION = 100000;      
        public override string Consignar(double valor, string ciudad)
        {
            if (CanConsignar(valor,ciudad).Count != 0) { throw new InvalidOperationException(); }
            return base.Consignar(valor, ciudad);
        }
        public override IList<string> CanConsignar(double valor, string ciudad)
        {
            var errors = new List<string>();
            if (valor < 0)
            {
                errors.Add("El valor a consignar es incorrecto");
            }
            else {             
                if (this.Movimientos.Count > 0)
                {
                    if (valor < VALORPRIMERACONSIGNACCION)
                    {
                        errors.Add("El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos");
                    }
                }
            }
            return errors;
        }

        public override string EjecutarRetiro(double valor, string ciudad)
        {
            if (CanRetirar(valor).Count != 0) { throw new InvalidOperationException(); }
            valor = ObternerValor4xMil(valor);
            return this.Retirar(valor, ciudad);
        }

        private double ObternerValor4xMil(double valor)
        {
            double nuevoValor = Math.Truncate(valor / 1000) * valor;
            double nuevoSaldo = Saldo - nuevoValor;
            return nuevoSaldo;
        }

        public override IList<string> CanRetirar(double valor)
        {
            var errors = new List<string>();
            if (valor < 0)
            {
                errors.Add("El valor a consignar es incorrecto");
            }
            else { 
            
                if (this.Movimientos.Count > 0)
                {
                    if (valor < VALORPRIMERACONSIGNACCION) { errors.Add("El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos"); }
                }
                double nuevoSaldo = ObternerValor4xMil(valor);
                if (nuevoSaldo < 0 && nuevoSaldo < TopeGiro)
                {
                    errors.Add("No es posible realizar el Retiro, supera el valor del saldo de la cuenta y del tope de giro permitido");
                }
            }
            return errors;
        }

        
    }   
}
