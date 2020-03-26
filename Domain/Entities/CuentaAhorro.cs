using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaAhorro : CuentaBancaria
    {
        public const double TOPERETIRO = 1000;
        public const double VALORDECUENTAPARARETIRAR = 20000;
        public const double VALORDELCUARTORETIROENADELANTE = 5000;
        public const double VALORCOSIGNACIONAOTRACIUDAD = 10000;
        public const double VALORCONSIGNACIONINICIAL = 50000;
        public const int NUMERODERETIROSCONDESCUENTO=4;
        public bool primeraConsignaccion { get; set; }

        public override string ValidarConsignaccion(double valor, string ciudad)
        {
            if (valor > 0)
            {
                if (primeraConsignaccion)
                {
                    if (valor >= VALORCONSIGNACIONINICIAL)
                    {
                        this.ValidadCiudadDestino(valor, ciudad);
                        primeraConsignaccion = true;
                    }
                    else
                    {
                        return "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos";
                    }
                }
                else
                {
                    ValidadCiudadDestino(valor,ciudad);
                }

            }
            else
            {
              return  "El valor a consignar es incorrecto";
            }
            return "Su nuevo saldo es " + this.Saldo + " m/c";
        }
        private void ValidadCiudadDestino(double valor,string ciudad)
        {
            if (!ciudad.Equals(this.CiudadDeCreacion))
            {
                valor -= VALORCOSIGNACIONAOTRACIUDAD;

            }           
            this.Consignar(valor, ciudad);
        }
        public override string ValidarRetiro(double valor, string ciudad)
        {
            if (valor > 0)
            {             
                if (this.Saldo >= VALORDECUENTAPARARETIRAR)
                {
                    if (this.NumeroDeRetiros()>=4) {
                        valor += VALORDELCUARTORETIROENADELANTE;
                    }
                    this.Retirar(valor, ciudad);
                    return "Su nuevo saldo es " + this.Saldo + " m/c";
                }
                else
                {
                    return "No es posible realizar el Retiro, Su cuenta tiene menos de 20.000 mil pesos";
                }
            }
            else
            {
                return "El valor a consignar es incorrecto";
            }

        }
    }


    
}
