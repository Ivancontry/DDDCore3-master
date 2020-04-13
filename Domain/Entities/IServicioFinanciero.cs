using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface IServicioFinanciero
    {

        string Nombre { get; set; }
        string Numero { get; set; }
        decimal Saldo { get; }

        string Consignar(decimal valor, string ciudad);
        string Retirar(decimal valor, string ciudad);
        string Trasladar(IServicioFinanciero servicioFinanciero, decimal valor, string ciudad);

    }
}
