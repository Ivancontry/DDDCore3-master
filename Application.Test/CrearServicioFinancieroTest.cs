using Application.Services;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Application.Test
{
    public class CrearServiciosFinancierosTest
    {
        BancoContext _context;

        [SetUp]
        public void Setup()
        {
            var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("server=localhost;Database=Banco;Trusted_Connection=True;")
             .Options;
            //var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;

            _context = new BancoContext(optionsSqlServer);
        }

        [Test]
        public void CrearCuentaAhorroTest()
        {
            var request = new CrearCuentaAhorroRequest { Numero = "000002", Nombre = "Cuenta Ahorro", Ciudad="Valledupar", FechaCreacion= DateTime.Now, Saldo=0};
            CrearCuentaAhorroService _service = new CrearCuentaAhorroService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con exito la cuenta 000002", response.Mensaje);
        }
        [Test]
        public void CrearCuentaCorrienteTest()
        {
            var request = new CrearCuentaCorrienteRequest { Numero = "000003", Nombre = "Cuenta Corriente", Ciudad = "Valledupar", FechaCreacion = DateTime.Now, Saldo = 0, TopeGiro=1000000 };
            CrearCuentaCorrienteService _service = new CrearCuentaCorrienteService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con exito la cuenta 000003", response.Mensaje);
        }
        [Test]
        public void CrearCerticadoDepositoTerminoTest()
        {
            var request = new CrearCertificadoDepositoTerminoServiceRequest { Numero = "000001", Nombre = "aaaaa", Ciudad = "Valledupar", FechaCreacion = DateTime.Now, Saldo = 0, TasaEfectivaAnual=0.05m,TerminoDefinido=90};
            CrearCertificadoDepositoTerminoService _service = new CrearCertificadoDepositoTerminoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creo con Exito el Servicio Finaciero 000001.", response.Mensaje);
        }
    }
}