﻿using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CertificadoDepositoATermino : ServicioFinanciero
    {
        const decimal VALORCONSIGNACIONINICIAL = 1000000;       
        public decimal TasaEfectivaAnual { get; set; }
        
        public int TerminoDefinido {get;set;}

        public override string Consignar(decimal valor, string ciudad)
        {
            if (CanConsignar(valor, ciudad).Count > 0) { throw new InvalidOperationException(); }
            return base.Consignar(valor, ciudad);
        }
        public override IList<string> CanConsignar(decimal valor, string ciudad) {
            var errors = new List<string>();
            if (valor <= 0)
            {
                errors.Add("El valor a consignar es incorrecto");
            }
            else { 
            
                if (this.Movimientos.Count == 0)
                {
                    if (valor < VALORCONSIGNACIONINICIAL)
                    {
                        errors.Add("El valor minimo de consignar es de 1000000");
                    }
                }else
                {

                    errors.Add("No es posible realizar dos consignaciones");
                }
            }
            return errors;
        }
        public override string Retirar(decimal valor, string ciudad)
        {
            if (CanRetirar(valor).Count != 0) { throw new InvalidCastException(); }
            return base.Retirar(valor, ciudad);

        }
        public override IList<string> CanRetirar(decimal valor) {
            var errors = new List<string>();
            TimeSpan time = DateTime.Now - this.FechaCreacion;
            int diasTrascurridos = time.Days;

            if (valor <= 0)
            {
                errors.Add("El valor a consignar es incorrecto");
                if (diasTrascurridos < TerminoDefinido)
                {
                    errors.Add("No es posible realizar retiros a la fecha actual, termino de plazo no cumplido");
                    if (valor > this.Saldo) {
                        errors.Add("No es posible realizar el Retiro, supera el valor del saldo de la cuenta");
                    }
                }
            }
            return errors;

        }


    }
  
}
