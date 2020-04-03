using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factory
{
    public class CuentaBancariaFactory
    {
        public ServicioFinanciero Create(string tipoCuenta) {
            ServicioFinanciero cuentaNueva;
            if (tipoCuenta == "Ahorro")
            {
                cuentaNueva = new CuentaAhorro();
            }
            else {
                cuentaNueva = new CuentaCorriente();
            }
            return cuentaNueva;
        }
    }
}
