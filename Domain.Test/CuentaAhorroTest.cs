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
            var respuesta = cuentaAhorro.CanConsignar(-10000, "Valledupar");
            string obtenido = "";
            string esperado = "El valor a consignar es incorrecto";
            if (respuesta.Contains(esperado))  obtenido = esperado;
           
            Assert.AreEqual("El valor a consignar es incorrecto", obtenido);
        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "00001";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;                     
            var respuesta = cuentaAhorro.Consignar(50000,"Valledupar");           
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
            string obtenido = "";
            string esperado = "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos";
            var respuesta = cuentaAhorro.CanConsignar(40000, "Valledupar");
            if (respuesta.Contains(esperado)) obtenido = esperado;
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos", obtenido);
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;
            cuentaAhorro.Consignar(50000, "Valledupar");
            cuentaAhorro.Retirar(20000, "Valledupar");
            var respuesta = cuentaAhorro.Consignar(49950, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 79950 + " m/c", respuesta);
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrectaCiudad()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;
            cuentaAhorro.Consignar(50000, "Valledupar");
            cuentaAhorro.Retirar(20000, "Valledupar");
            var respuesta = cuentaAhorro.Consignar(49950, "Bogota");  
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
            string obtenido = "";
            string esperado = "El valor a consignar es incorrecto";
            var respuesta = cuentaAhorro.CanRetirar(-10000);
            if (respuesta.Contains(esperado)) { obtenido = esperado; }
            Assert.AreEqual("El valor a consignar es incorrecto", obtenido);
        }
        [Test]
        public void ValorMinimoDeCuentaParaPoderRetirar()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 10000;
            string obtenido = "";
            string esperado = "No es posible realizar el Retiro, Su cuenta tiene menos de 20.000 mil pesos";
            var respuesta = cuentaAhorro.CanRetirar(10000);
            if (respuesta.Contains(esperado)) { obtenido = esperado; }
            Assert.AreEqual("No es posible realizar el Retiro, Su cuenta tiene menos de 20.000 mil pesos", obtenido);
        
        }
        [Test]
        public void ValorCorrectoRetirar()
        {
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 300000;
            var respuesta = cuentaAhorro.EjecutarRetiro(100000, "Valledupar");
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
            cuentaAhorro.EjecutarRetiro(10000, "Valledupar");
            cuentaAhorro.EjecutarRetiro(10000, "Valledupar");
            cuentaAhorro.EjecutarRetiro(10000, "Valledupar");
            cuentaAhorro.EjecutarRetiro(10000, "Valledupar");
            var respuesta = cuentaAhorro.EjecutarRetiro(10000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 245000 + " m/c", respuesta);
        }


    }
}
