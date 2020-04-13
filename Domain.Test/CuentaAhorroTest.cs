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

        //[TestCaseSource("DataSourceCuentaAhorro")]
        //public void CaseTestCuentaAhorro(string numeroCuenta, string nombreCuenta, string ciudadCreacion, decimal saldo, DateTime FechaCreacion,decimal valor, string mensajeEsperado ) {
        //    var cuentaAhorro = new CuentaAhorro();
        //    cuentaAhorro.Numero = numeroCuenta;
        //    cuentaAhorro.Nombre = nombreCuenta;
        //    cuentaAhorro.CiudadDeCreacion = ciudadCreacion;
        //    cuentaAhorro.Saldo = saldo;
        //    cuentaAhorro.FechaCreacion = FechaCreacion;
        //    var errores = cuentaAhorro.CanConsignar(valor, ciudadCreacion);
//        string obtenido = "";
//            if (errores.Count == 0)
//            {
//                obtenido = cuentaAhorro.Consignar(valor, ciudadCreacion);
//            }
//            else {
//                if (errores.Contains(mensajeEsperado)) obtenido = mensajeEsperado;  
//            }
//Assert.AreEqual(mensajeEsperado, obtenido);
        //}

        //private static IEnumerable<TestCaseData> DataSourceCuentaAhorro() {
        //    yield return new TestCaseData("0001", "Cuenta Ahorro 1", "Valledupar", 0, DateTime.Now, 50000, "Su nuevo saldo es " + 50000 + " m/c").SetName("ConsignacionInicialCorrecta").;
        //    yield return new TestCaseData("0002","Cuenta Ahorro 2", "Valledupar", 0, DateTime.Now, -1000, "El valor a consignar es incorrecto").SetName("ValorConsignaciónNegativoCero");            
        //    yield return new TestCaseData("0003", "Cuenta Ahorro 3", "Valledupar", 0, DateTime.Now, 40000, "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos").SetName("ConsignacionInicialIncorrecta");
        //    yield return new TestCaseData("0004", "Cuenta Ahorro 4", "Valledupar", 0, DateTime.Now, 49950, "Su nuevo saldo es " + 79950 + " m/c").SetName("ConsignacionPosteriorInicialCorrecta");
        //    yield return new TestCaseData("0005", "Cuenta Ahorro 5", "Bogota", 0, DateTime.Now, 49950, "Su nuevo saldo es " + 69950 + " m/c").SetName("ConsignacionPosteriorInicialCorrectaCiudad");           
        //}
        #region TestSeparadosConsignar
        [Test]
        public void ValorConsignaciónNegativoCero()
        {
            const decimal VALOR = -10000;
            const string CIUDAD = "Valledupar";
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;
            var errores = cuentaAhorro.CanConsignar(VALOR, CIUDAD);
            string esperado = "El valor a consignar es incorrecto";
            string obtenido = "";
            if (errores.Count == 0)
            {
                obtenido = cuentaAhorro.Consignar(VALOR, CIUDAD);
            }
            else
            {
                if (errores.Contains(esperado)) obtenido = esperado;
            }          
            if (errores.Contains(esperado)) obtenido = esperado;

            Assert.AreEqual(esperado, obtenido);
        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            const decimal VALOR = 50000;
            const string CIUDAD = "Valledupar";
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "00001";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;
            var errores = cuentaAhorro.CanConsignar(VALOR, CIUDAD);
            string esperado = "Su nuevo saldo es " + 50000 + " m/c";
            string obtenido = "";
            if (errores.Count == 0)
            {
                obtenido = cuentaAhorro.Consignar(VALOR, CIUDAD);
            }
            else
            {
                if (errores.Contains(esperado)) obtenido = esperado;
            }        
            if (errores.Contains(esperado)) obtenido = esperado;

            Assert.AreEqual(esperado, obtenido);         

        }

        [Test]

        public void ConsignacionInicialIncorrecta()
        {
            const decimal VALOR = 40000;
            const string CIUDAD = "Valledupar";
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;
            var errores = cuentaAhorro.CanConsignar(VALOR, CIUDAD);
            string esperado = "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos";
            string obtenido = "";
            if (errores.Count == 0)
            {
                obtenido = cuentaAhorro.Consignar(VALOR, CIUDAD);
            }
            else
            {
                if (errores.Contains(esperado)) obtenido = esperado;
            }         
            if (errores.Contains(esperado)) obtenido = esperado;

            Assert.AreEqual(esperado, obtenido);          
          
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrecta()
        {
            const decimal VALOR = 49950;
            const string CIUDAD = "Valledupar";
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;

            cuentaAhorro.Consignar(50000, "Valledupar");
            cuentaAhorro.Retirar(20000, "Valledupar");

            var errores = cuentaAhorro.CanConsignar(VALOR, CIUDAD);
            string esperado = "Su nuevo saldo es " + 79950 + " m/c";
            string obtenido = "";
            if (errores.Count == 0)
            {
                obtenido = cuentaAhorro.Consignar(VALOR, CIUDAD);
            }
            else
            {
                if (errores.Contains(esperado)) obtenido = esperado;
            }
            if (errores.Contains(esperado)) obtenido = esperado;

            Assert.AreEqual(esperado, obtenido);
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrectaCiudad()
        {
            const decimal VALOR = 49950;
            const string CIUDAD = "Bogota";
            var cuentaAhorro = new CuentaAhorro();
            cuentaAhorro.Numero = "111";
            cuentaAhorro.Nombre = "Ahorro Ejemplo";
            cuentaAhorro.CiudadDeCreacion = "Valledupar";
            cuentaAhorro.Saldo = 0;

            cuentaAhorro.Consignar(50000, "Valledupar");
            cuentaAhorro.Retirar(20000, "Valledupar");

            var errores = cuentaAhorro.CanConsignar(VALOR, CIUDAD);
            string esperado = "Su nuevo saldo es " + 69950 + " m/c";
            string obtenido = "";
            if (errores.Count == 0)
            {
                obtenido = cuentaAhorro.Consignar(VALOR, CIUDAD);
            }
            else
            {
                if (errores.Contains(esperado)) obtenido = esperado;
            }
            if (errores.Contains(esperado)) obtenido = esperado;

            Assert.AreEqual(esperado, obtenido);
        }
        #endregion
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
            var respuesta = cuentaAhorro.Retirar(100000, "Valledupar");
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
            cuentaAhorro.Retirar(10000, "Valledupar");
            cuentaAhorro.Retirar(10000, "Valledupar");
            cuentaAhorro.Retirar(10000, "Valledupar");
            cuentaAhorro.Retirar(10000, "Valledupar");
            var respuesta = cuentaAhorro.Retirar(10000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 245000 + " m/c", respuesta);
        }


    }
}
