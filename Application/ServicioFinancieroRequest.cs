using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public  class ServicioFinancieroRequest
    {
        public string Nombre { get; set; }       
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
