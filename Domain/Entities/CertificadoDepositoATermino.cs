using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CertificadoDepositoATermino : CuentaBancaria
    {
        const double VALORCONSIGNACIONINICIAL = 1000000;
        public bool PrimeraConsignacion { get; set; }
        public float TasaEfectivaAnual { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public int TerminoDefinido {get;set;}
               
        public override string ValidarConsignaccion(double valor, string ciudad) {
            if (PrimeraConsignacion)
            {
                if (valor >= 1000000)
                {
                    this.Consignar(valor, ciudad);
                }
                else {
                    return "El valor minimo de conssignar es de 1000000";
                }
            }
            else {
               return "No es posible realizar dos consignaciones";
            }

            return "Su nuevo saldo es " + this.Saldo + " m/c";

        }
        public override string ValidarRetiro(double valor, string ciudad) {

            TimeSpan time = DateTime.Now - FechaDeCreacion;
            int diasTrascurridos = time.Days;
            if (diasTrascurridos >= TerminoDefinido)
            {
                this.Retirar(valor, ciudad);
            }
            else
            {
                return "No es posible realizar retiros a la fecha actual, termino de plazo no cumplido";
            }
            return "Su nuevo saldo es " + this.Saldo + " m/c";

        }


    }

    [Serializable]
    public class CertificadoDepositosATerminoException : Exception
    {
        public CertificadoDepositosATerminoException() { }
        public CertificadoDepositosATerminoException(string message) : base(message) { }
        public CertificadoDepositosATerminoException(string message, Exception inner) : base(message, inner) { }
        protected CertificadoDepositosATerminoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
