using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class ServicioFinanciero : Entity<int>, IServicioFinanciero
    {
        public ServicioFinanciero()
        {
            Movimientos = new List<MovimientoFinanciero>();              
        }

        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public decimal Saldo { get; set; }
        public string CiudadDeCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
       
        public virtual string Consignar(decimal valor, string ciudad)
        {           
            MovimientoFinanciero movimiento = new MovimientoFinanciero();
            movimiento.ValorConsignacion = valor;
            movimiento.CiudadDeRealizacion = ciudad;
            movimiento.FechaMovimiento = DateTime.Now;
            Saldo += valor;
            Movimientos.Add(movimiento);   
            return "Su nuevo saldo es " + this.Saldo + " m/c";
        }

        public abstract IList<string> CanConsignar(decimal valor, string ciudad);
        //public abstract string EjecutarConsignacion(double valor, string ciudad);

        public virtual string Retirar(decimal valor, string ciudad) {
           
                MovimientoFinanciero retiro = new MovimientoFinanciero();
                retiro.ValorRetiro = valor;
                retiro.CiudadDeRealizacion = ciudad;
                retiro.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(retiro);          
            return "Su nuevo saldo es " + this.Saldo + " m/c";

        }
        public abstract IList<string> CanRetirar(decimal valor);
    

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        public void Trasladar(IServicioFinanciero servicioFinanciero, decimal valor, string ciudad)
        {
            Retirar(valor, ciudad);
            servicioFinanciero.Consignar(valor, ciudad);
        }

        public int NumeroDeRetiros() {
           DateTime fechaActual = DateTime.Now;            
           return Movimientos.FindAll(x => x.FechaMovimiento.Year == fechaActual.Year && x.FechaMovimiento.Month==fechaActual.Month && x.ValorRetiro!=0 ).Count;
            
        }

        string IServicioFinanciero.Trasladar(IServicioFinanciero servicioFinanciero, decimal valor, string ciudad)
        {
            this.Retirar(valor,ciudad);
            this.Consignar(valor,ciudad);
            return "";
        }
    }
}
