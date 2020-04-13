using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
    public class CertificadoDeDepositoATerminoTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void ValorConsignaciónNegativoCero()
        {
            var certificadoDepositoATermino = new CertificadoDepositoATermino();
            certificadoDepositoATermino.Numero = "111";
            certificadoDepositoATermino.Nombre = "Ahorro Ejemplo";
            certificadoDepositoATermino.CiudadDeCreacion = "Valledupar";
            certificadoDepositoATermino.Saldo = 0;
            certificadoDepositoATermino.TasaEfectivaAnual = 0.05m;
            certificadoDepositoATermino.TerminoDefinido = 60;
            var respuesta = certificadoDepositoATermino.CanConsignar(-10000, "Valledupar");
            string obtenido = "";
            string esperado = "El valor a consignar es incorrecto";
            if (respuesta.Contains(esperado)) obtenido = esperado;

            Assert.AreEqual("El valor a consignar es incorrecto", obtenido);
        }

        [Test]
        public void ConsignacionInicialCorrecta()
        {
            var certificadoDepositoATermino = new CertificadoDepositoATermino();
            certificadoDepositoATermino.Numero = "00001";
            certificadoDepositoATermino.Nombre = "Ahorro Ejemplo";
            certificadoDepositoATermino.CiudadDeCreacion = "Valledupar";
            certificadoDepositoATermino.Saldo = 0;
            var respuesta = certificadoDepositoATermino.Consignar(1500000, "Valledupar");
            Assert.AreEqual("Su nuevo saldo es " + 1500000 + " m/c", respuesta);

        }

        [Test]

        public void ConsignacionInicialIncorrecta()
        {
            var certificadoDepositoATermino = new CertificadoDepositoATermino();
            certificadoDepositoATermino.Numero = "111";
            certificadoDepositoATermino.Nombre = "Ahorro Ejemplo";
            certificadoDepositoATermino.CiudadDeCreacion = "Valledupar";
            certificadoDepositoATermino.Saldo = 0;
            string obtenido = "";
            string esperado = "El valor minimo de consignar es de 1000000";
            var respuesta = certificadoDepositoATermino.CanConsignar(40000, "Valledupar");
            if (respuesta.Contains(esperado)) obtenido = esperado;
            Assert.AreEqual(esperado, obtenido);
        }
    }
}
