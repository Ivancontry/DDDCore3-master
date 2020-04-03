using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaAhorro : ServicioFinanciero
    {
        public const double TOPERETIRO = 1000;
        public const double VALORDECUENTAPARARETIRAR = 20000;
        public const double VALORDELCUARTORETIROENADELANTE = 5000;
        public const double VALORCONSIGNACIONAOTRACIUDAD = 10000;
        public const double VALORCONSIGNACIONINICIAL = 50000;
        public const int NUMERODERETIROSCONDESCUENTO = 4;

       
        public override IList<string> CanConsignar(double valor, string ciudad)
        {
            var errors = new List<string>();
            if (valor < 0)
            {
                errors.Add("El valor a consignar es incorrecto");                
            }
            else {
                if (this.Movimientos.Count == 0)
                {
                    if (valor < VALORCONSIGNACIONINICIAL)
                    {
                        errors.Add("El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos");
                    }
                }
            }
            return errors;
        }
//en el padre, ok, no sabia,

        public override string Consignar(double valor, string ciudad) {
            if (CanConsignar(valor,ciudad).Count > 0) { throw new InvalidOperationException(); }

            if (!ciudad.Equals(this.CiudadDeCreacion)) { valor -= VALORCONSIGNACIONAOTRACIUDAD;}
            return base.Consignar(valor, ciudad);
        
        }

        public override string EjecutarRetiro(double valor, string ciudad)
        {
            if (this.NumeroDeRetiros() >= 4){valor += VALORDELCUARTORETIROENADELANTE;}
            
            return this.Retirar(valor, ciudad);
            
        }
        public override IList<string> CanRetirar(double valor)
        {
            var errors = new List<string>();
            if (valor < 0)
            {
                errors.Add("El valor a consignar es incorrecto");             

            }
            else{

                if (this.Saldo < VALORDECUENTAPARARETIRAR)
                {
                    errors.Add("No es posible realizar el Retiro, Su cuenta tiene menos de 20.000 mil pesos");
                    if (valor > this.Saldo)
                    {
                        errors.Add("No es posible realizar el Retiro, supera el valor del saldo de la cuenta");
                    }
                }
            }
            return errors;
        }
    }


    
}
