using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CuentaCorrienteTestPrueba 
{
    public class CuentasCorrienteTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void ValorConsignacionNegativoCero()
        {

            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.CanRetirar(-10000);
            string obtenido = "";
            string esperado = "El valor a consignar es incorrecto";
            if (respuesta.Contains(esperado)) obtenido = esperado;
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
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.Consignar(100000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 100000 + " m/c", respuesta);

        }

        [Test]

        public void ConsignacionInicialIncorrecta()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;           
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.CanConsignar(40000, "Valledupar");
            string obtenido = "";
            string esperado = "El valor a consignar es incorrecto";
            if (respuesta.Contains(esperado)) obtenido = esperado;
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos", obtenido);
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
            var respuesta = cuentaCorriente.Consignar(49950, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 79950 + " m/c", respuesta);
        }
     
        [Test]
        public void ValorRetirarNegativoCorriente()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.CanConsignar(-10000, "Valledupar");
            string obtenido = "";
            string esperado = "El valor a consignar es incorrecto";
            if (respuesta.Contains(esperado)) obtenido = esperado;
            Assert.AreEqual("El valor a consignar es incorrecto", obtenido);
        }
       
        [Test]
        public void ValorCorrectoRetirarCorriente()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 300000;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.EjecutarRetiro(300000, "Valledupar");
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
            var respuesta = cuentaCorriente.EjecutarRetiro(500000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 0 + " m/c", respuesta);
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
            var respuesta = cuentaCorriente.EjecutarRetiro(500000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 0 + " m/c", respuesta);
        }

    }
}
