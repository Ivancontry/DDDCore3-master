using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaCorriente : CuentaBancaria
    {
        public bool EsPrimeraConsignaccion { get; set; }
        public const double VALORPRIMERACONSIGNACCION = 100000;
        public double TopeGiro { get; set; }
        public double SobreGiro {
            get {
                return SobreGiro;
            }

            set {
                SobreGiro = value * -1;
            } 
        }

        public override string ValidarConsignaccion(double valor, string ciudad)
        {
            if (valor > 0)
            {
                if (EsPrimeraConsignaccion)
                {
                    if (valor >= VALORPRIMERACONSIGNACCION)
                    {
                        this.Consignar(valor, ciudad);
                        EsPrimeraConsignaccion = true;
                    }
                    else {
                        return "El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos";
                    }
                }
                else
                {
                    this.Consignar(valor, ciudad);
                }

            }
            else
            {
                return "El valor a consignar es incorrecto";
            }

            return "Su nuevo saldo es " + this.Saldo + " m/c";
        }

        public override string ValidarRetiro(double valor, string ciudad)
        {
            double nuevoValor = Math.Truncate(valor / 1000)*valor;
            double nuevoSaldo = Saldo - nuevoValor;

            if (nuevoSaldo >= 0)
            {
                this.Retirar(nuevoValor, ciudad);
            }
            else {
                double nuevoSobreGiro = (SobreGiro - nuevoSaldo)*-1;
                if (nuevoSobreGiro <= TopeGiro)
                {
                    this.Retirar(nuevoValor,ciudad);
                    SobreGiro = nuevoSobreGiro;
                }
                else
                {
                    return "No es posible realizar el Retiro, supera el valor del saldo de la cuenta y del tope de giro permitido";
                }
            }
            return "Su nuevo saldo es " + this.Saldo + " m/c" + "Su sobregiro actual es "+this.SobreGiro;
            
        }

        
    }   
}
