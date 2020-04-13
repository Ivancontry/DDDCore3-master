using Application;
using Application.Services;
using Domain.Contracts;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;
//https://localhost:44325/swagger/index.html
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioFinancieroController : ControllerBase
    {
        readonly BancoContext _context;
        readonly IUnitOfWork _unitOfWork;
        
        //Se Recomienda solo dejar la Unidad de Trabajo
        public ServicioFinancieroController(BancoContext context,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpPost("cuentaAhorro")]
        public ActionResult<CrearCuentaAhorroResponse> Post(CrearCuentaAhorroRequest request)
        {
            CrearCuentaAhorroService _service = new CrearCuentaAhorroService(_unitOfWork);
            CrearCuentaAhorroResponse response = _service.Ejecutar(request);
            return Ok(response);
        }
        [HttpPost("cuentaCorriente")]
        public ActionResult<CrearCuentaCorrienteResponse> Post(CrearCuentaCorrienteRequest request)
        {
            CrearCuentaCorrienteService _service = new CrearCuentaCorrienteService(_unitOfWork);
            CrearCuentaCorrienteResponse response = _service.Ejecutar(request);
            return Ok(response);
        }
        [HttpPost("cdt")]
        public ActionResult<CrearCertificadoDepositoTerminoServiceResponse> Post(CrearCertificadoDepositoTerminoServiceRequest request)
        {
            CrearCertificadoDepositoTerminoService _service = new CrearCertificadoDepositoTerminoService(_unitOfWork);
            CrearCertificadoDepositoTerminoServiceResponse response = _service.Ejecutar(request);
            return Ok(response);
        }

        [HttpPost("consignacion")]
        public ActionResult<ConsignarResponse> Post(ConsignarRequest request)
        {
            var _service = new ConsignarService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            return Ok(response);
        }
        [HttpPost("retirar")]
        public ActionResult<RetirarResponse> Post(RetirarRequest request)
        {
            var _service = new RetirarService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            return Ok(response);
        }

    }
}