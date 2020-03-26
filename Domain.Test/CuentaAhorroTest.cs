using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
    public class CuentaAhorroTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValorConsignaciónNegativoCero()
        {

            var cuentaAhorro = new CuentaAhorro();         
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;        
            var respuesta = cuentaAhorro.ValidarConsignaccion(-10000, "Valledupar");
            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "00001";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;               
            cuentaAhorro.primeraConsignaccion = true;
            var respuesta = cuentaAhorro.ValidarConsignaccion(50000,"Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 50000 + " m/c", respuesta);

        }

        [Test]

        public void ConsignacionInicialIncorrecta()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;           
            cuentaAhorro.primeraConsignaccion=true; 
            var respuesta = cuentaAhorro.ValidarConsignaccion(40000, "Valledupar");
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos", respuesta);
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 30000; 
            var respuesta = cuentaAhorro.ValidarConsignaccion(49950, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 79950 + " m/c", respuesta);
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrectaCiudad()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 30000;
            var respuesta = cuentaAhorro.ValidarConsignaccion(49950, "Bogota");  
            Assert.AreEqual("Su nuevo saldo es " + 69950 + " m/c", respuesta);
        }

        [Test]

        public void ValorRetirarNegativo()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;
            var respuesta = cuentaAhorro.ValidarRetiro(-10000, "Valledupar");
            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }
        [Test]
        public void ValorMinimoDeCuentaParaPoderRetirar()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 10.000;
            var respuesta = cuentaAhorro.ValidarRetiro(10000, "Valledupar");
            Assert.AreEqual("No es posible realizar el Retiro, Su cuenta tiene menos de 20.000 mil pesos", respuesta);
        }
        [Test]
        public void ValorCorrectoRetirar()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 300000;
            var respuesta = cuentaAhorro.ValidarRetiro(100000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 200000 + " m/c", respuesta);
        }

        [Test]
        public void ValorDelCuartoRetiroDeUnMes()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 300000;
            cuentaAhorro.ValidarRetiro(10000, "Valledupar");
            cuentaAhorro.ValidarRetiro(10000, "Valledupar");
            cuentaAhorro.ValidarRetiro(10000, "Valledupar");
            cuentaAhorro.ValidarRetiro(10000, "Valledupar");
            var respuesta = cuentaAhorro.ValidarRetiro(10000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 245000 + " m/c", respuesta);
        }


    }
}
