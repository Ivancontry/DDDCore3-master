using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class CuentaBancaria : Entity<int>, IServicioFinanciero
    {
        public CuentaBancaria()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }

        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public double Saldo { get; set; }
        public string CiudadDeCreacion { get; set; }
        public virtual void Consignar(double valor, string ciudad)
        {
            MovimientoFinanciero movimiento = new MovimientoFinanciero();
            movimiento.ValorConsignacion = valor;
            movimiento.CiudadDeRealizacion = ciudad;
            movimiento.FechaMovimiento = DateTime.Now;
            Saldo += valor;
            Movimientos.Add(movimiento);
        }

        public abstract string ValidarConsignaccion(double valor, string ciudad);

        public virtual void Retirar(double valor, string ciudad) {
            MovimientoFinanciero retiro = new MovimientoFinanciero();
            retiro.ValorRetiro = valor;
            retiro.CiudadDeRealizacion = ciudad;
            retiro.FechaMovimiento = DateTime.Now;
            Saldo -= valor;
            this.Movimientos.Add(retiro);
        }
        public abstract string ValidarRetiro(double valor, string ciudad);

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        public void Trasladar(IServicioFinanciero servicioFinanciero, double valor, string ciudad)
        {
            Retirar(valor, ciudad);
            servicioFinanciero.Consignar(valor, ciudad);
        }

        public int NumeroDeRetiros() {
            DateTime fechaActual = DateTime.Now;            
           return Movimientos.FindAll(x => x.FechaMovimiento.Year == fechaActual.Year && x.FechaMovimiento.Month==fechaActual.Month ).Count;
        }

        
    }
}
