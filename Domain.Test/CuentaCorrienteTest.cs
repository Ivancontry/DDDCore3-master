using Domain.Entities;
using NUnit.Framework;

namespace Domain.Test
{
    public class CuentaCorrienteTests
    {
        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        public void ConsignacionInicialNegativaTest()
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
            Assert.AreEqual("El valor a consignar es incorrecto", obtenido);
        }

        [Test]
        public void ConsignacionInicialCorrectaCorriente()
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

        public void ConsignacionInicialIncorrectaCorriente()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.CanConsignar(40000, "Valledupar");
            string obtenido = "";
            string esperado = "El valor mínimo de la primera consignación debe ser de $100.000 mil pesos. Su nuevo saldo es $0 pesos";
            if (respuesta.Contains(esperado)) obtenido = esperado;
            Assert.AreEqual(esperado, obtenido);
        }

        [Test]

        public void ConsignacionPosteriorInicialCorrectaCorriente()
        {
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Corriente Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 0;
            cuentaCorriente.TopeGiro = 5000000;
            cuentaCorriente.Consignar(100000, "Valledupar");
            
            var respuesta = cuentaCorriente.Consignar(49950, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 149950 + " m/c", respuesta);
        }

        [Test]
        public void ValorRetirarNegativoCuentaCorriente()
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
            Assert.AreEqual(esperado, obtenido);
        }

        [Test]
        public void ValorCorrectoRetirarCuentaCorriente()
        {
            decimal valor = 18800.000m;
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 320000;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.Retirar(300000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + valor + " m/c", respuesta);
        }
        [Test]
        public void ValorCorrectoRetirarUsandoSobregiroMenor()
        {
            decimal valor = -202000.000m;
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 300000;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.Retirar(500000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + valor + " m/c", respuesta);
        }

        [Test]
        public void ValorCorrectoRetirarUsandoSobregiroMayor()
        {
            decimal valor = -202000.000m;
            var cuentaCorriente = new CuentaCorriente();
            cuentaCorriente.Numero = "111";
            cuentaCorriente.Nombre = "Ahorro Ejemplo";
            cuentaCorriente.CiudadDeCreacion = "Valledupar";
            cuentaCorriente.Saldo = 300000;
            cuentaCorriente.TopeGiro = 5000000;
            var respuesta = cuentaCorriente.Retirar(500000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + valor + " m/c", respuesta);
        }
    }
}