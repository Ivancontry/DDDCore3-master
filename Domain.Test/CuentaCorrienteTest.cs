using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
    public class CuentaCorrienteTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValorConsignaciónNegativoCero()
        {

            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.ValidarConsignaccion(-10000,"Valledupar");
            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "00001";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.EsPrimeraConsignaccion = true;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.ValidarConsignaccion(100000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 100000 + " m/c" + "Su sobregiro actual es " + 0, respuesta);

        }

        [Test]

        public void ConsignacionInicialIncorrecta()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.EsPrimeraConsignaccion = true;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.ValidarConsignaccion(40000, "Valledupar");
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos", respuesta);
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrecta()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 30000;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.ValidarConsignaccion(49950, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 79950 + " m/c" + "Su sobregiro actual es " + 0, respuesta);
        }
        [Test]
        public void ValorRetirarNegativo()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.ValidarRetiro(-10000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 0 + " m/c" + "Su sobregiro actual es " + 0, respuesta);
        }
       
        [Test]
        public void ValorCorrectoRetirar()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 300000;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.ValidarRetiro(300000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 0 + " m/c" + "Su sobregiro actual es " + 0, respuesta);
        }
        [Test]
        public void ValorCorrectoRetirarUsandoSobregiroMenor()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 300000;
            cuentaCorriente.TopeGiro = 5000000;
            cuentaCorriente.SobreGiro = 0;
            var respuesta = cuentaCorriente.ValidarRetiro(500000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 0 + " m/c" + "Su sobregiro actual es " + (-200000) , respuesta);
        }

        [Test]
        public void ValorCorrectoRetirarUsandoSobregiroMayor()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 300000;
            cuentaCorriente.TopeGiro = 5000000;
            cuentaCorriente.SobreGiro = -500000;
            var respuesta = cuentaCorriente.ValidarRetiro(500000, "Valledupar");
            Assert.AreEqual("No es posible realizar el Retiro, supera el valor del saldo de la cuenta y del tope de giro permitido", respuesta);
        }

    }
}
