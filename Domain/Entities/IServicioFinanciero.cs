using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface IServicioFinanciero
    {

        string Nombre { get; set; }
        string Numero { get; set; }
        double Saldo { get; }

        string Consignar(double valor, string ciudad);
        string Retirar(double valor, string ciudad);
        string Trasladar(IServicioFinanciero servicioFinanciero, double valor, string ciudad);

    }
}
