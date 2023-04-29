using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.AplicacionWeb.Utilidades.Response;
using SiECTE.BLL.Interfaces;
using SiECTE.Entity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace SiECTE.AplicacionWeb.Controllers
{
    //Seguridad de Inicio de Sesion
    [Authorize]

    public class HistorialController : Controller
    {
        /*ATRIBUTOS*/
        private readonly ITipoDocumentoIngresoService  _tipoDocumentoIngresoService;
        private readonly IHistorialIngresoService _historialService;
        private readonly IMapper _mapper;

        private readonly IConverter _converter;

        public HistorialController(IHistorialIngresoService tipoDocumentohistorialService, IHistorialIngresoService historialService, IMapper mapper, IConverter converter)//Constructor
        {
            ////_tipoDocumentohistorialService = tipoDocumentohistorialService;
            _historialService = historialService;
            _mapper = mapper;

            _converter = converter;
        }




        /*METODOS*/
        public IActionResult NuevoIngreso()
        {
            return View();
        }
        public IActionResult HistorialIngreso()
        {
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoIngreso()
        {
            List<VMDocumento> vmListaTipoDocumentos = _mapper.Map<List<VMDocumento>>(await _tipoDocumentoIngresoService.Lista());

            return StatusCode(StatusCodes.Status200OK,vmListaTipoDocumentos);
        }





        [HttpGet]
        public async Task<IActionResult> ObtenerResidentes(string busqueda)
        {
            List<VMResidente> vmListaResidentes = _mapper.Map<List<VMResidente>>(await _historialService.ObteberResidentes(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaResidentes);
        }





        [HttpPost]
        public async Task<IActionResult> RegistrarIngreso([FromBody] VMResidente modelo)
        {
            GenericResponse<VMResidente> gResponse = new GenericResponse<VMResidente>();

            try
            {
                //modelo.IdUsuario = 1; // Se debe pasar el ID_Usuario que se ha logeado al sistema

                CteHistorialIngresoResidente Ingreso_creada = await _historialService.Registrar(_mapper.Map<CteHistorialIngresoResidente>(modelo));
                modelo = _mapper.Map<VMResidente>(Ingreso_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex) 
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }







        [HttpGet]
        public async Task<IActionResult> Historial(string numeroIngreso,string fechaIni, string fechaFin)
        {
            List<VMResidente> vmHistorialIngreso = _mapper.Map<List<VMResidente>>(await _historialService.Historial(numeroIngreso,fechaIni,fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialIngreso);
        }





        public IActionResult MostrarPDFIngreso(string numeroIngreso)
        {
            string urlPLantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFIngreso?numeroIngreso={numeroIngreso}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page = urlPLantillaVista
                    }
                }
            };


            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");

        }

    }
}
