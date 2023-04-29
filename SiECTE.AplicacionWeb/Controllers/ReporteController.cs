using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.BLL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.AspNetCore.Authorization;

namespace SiECTE.AplicacionWeb.Controllers
{
    //Seguridad de Inicio de Sesion
    [Authorize]

    public class ReporteController : Controller
    {
/*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly IHistorialIngresoService _historialService;

        public ReporteController(IMapper mapper, IHistorialIngresoService historialService)//Constructor
        {
            _mapper = mapper;
            _historialService = historialService;
        }




        /*METODOS*/
        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public  async Task<IActionResult> ReporteIngreso(string fechaIni, string fechaFin)
        {
            List<VMReporteResidente> vmLista = _mapper.Map<List<VMReporteResidente>>( await _historialService.Reporte(fechaIni, fechaFin));

            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });
        }









    }
}
