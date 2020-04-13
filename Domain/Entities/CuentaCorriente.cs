using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaCorriente : ServicioFinanciero
    {
      
        public const decimal VALORPRIMERACONSIGNACCION = 100000;
        public const decimal VALOR4X1000 = 0.004m;
        private decimal _topeGiro;
        public decimal TopeGiro
        {
            get
            {
                return _topeGiro;
            }

            set
            {
                _topeGiro = value * -1;
            }
        }
        public override string Consignar(decimal valor, string ciudad)
        {
            if (CanConsignar(valor,ciudad).Count != 0) { throw new InvalidOperationException(); }
            return base.Consignar(valor, ciudad);
        }
        public override IList<string> CanConsignar(decimal valor, string ciudad)
        {
            var errors = new List<string>();
            if (valor < 0)
            {
                errors.Add("El valor a consignar es incorrecto");
            }
            else {             
                if (this.Movimientos.Count == 0)
                {
                    if (valor < VALORPRIMERACONSIGNACCION)
                    {
                        errors.Add("El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos");
                    }
                }
            }
            return errors;
        }

        public override string Retirar(decimal valor, string ciudad)
        {
            if (CanRetirar(valor).Count != 0) { throw new InvalidOperationException(); }
            valor = ObternerValor4xMil(valor);
            return base.Retirar(valor, ciudad);
        }

        private decimal ObternerValor4xMil(decimal valor)
        {
            decimal valorRetiro =  VALOR4X1000 * valor;
            decimal nuevoValor = valor + valorRetiro;
            return nuevoValor;
        }

        public override IList<string> CanRetirar(decimal valor)
        {
            var errors = new List<string>();
            if (valor < 0)
            {
                errors.Add("El valor a consignar es incorrecto");
            }
            else { 
            
               
                decimal nuevoSaldo = ObternerValor4xMil(valor);
                if (nuevoSaldo < 0 && nuevoSaldo < TopeGiro)
                {
                    errors.Add("No es posible realizar el Retiro, supera el valor del saldo de la cuenta y del tope de giro permitido");
                }
            }
            return errors;
        }

        
    }   
}
