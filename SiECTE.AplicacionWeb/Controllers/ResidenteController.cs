using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Newtonsoft.Json;
using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.AplicacionWeb.Utilidades.Response;
using SiECTE.BLL.Interfaces;
using SiECTE.Entity;
using Microsoft.AspNetCore.Authorization;

namespace SiECTE.AplicacionWeb.Controllers
{
    //Seguridad de Inicio de Sesion
    [Authorize]

    public class ResidenteController : Controller
    {
        /*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly IResidenteService _ResidenteService;
        private readonly IDocumentoService _documentoService;
        private readonly ITipoNotaService _tipoNotaService;

        public ResidenteController(IMapper mapper, IResidenteService ResidenteService, IDocumentoService DocumentoService, ITipoNotaService TipoNotaService)//Constructor
        {
            _mapper = mapper;
            _ResidenteService = ResidenteService;
            _documentoService = DocumentoService;
            _tipoNotaService = TipoNotaService;
        }





        /*METODOS*/
        public IActionResult Index()
        {
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMResidente> vmResidenteLista = _mapper.Map<List<VMResidente>>(await _ResidenteService.Lista());

            return StatusCode(StatusCodes.Status200OK,new {data = vmResidenteLista});
        }

        [HttpGet]
        public async Task<IActionResult> ListaDocumentos()
        {
            List<VMDocumento> vmListaDocumentos = _mapper.Map<List<VMDocumento>>(await _documentoService.Lista());

            return StatusCode(StatusCodes.Status200OK, new { data = vmListaDocumentos });
        }



        [HttpGet]
        public async Task<IActionResult> ListaCatDocumentos()
        {
            List<VMCatDocumento> vmListaDocumentos = _mapper.Map<List<VMCatDocumento>>(await _documentoService.CatLista());

            return StatusCode(StatusCodes.Status200OK, vmListaDocumentos );
        }



        [HttpGet]
        public async Task<IActionResult> ListaTipoNotas()
        {
            List<VMTipoNota> vmListaNotas = _mapper.Map<List<VMTipoNota>>(await _tipoNotaService.Lista());

            return StatusCode(StatusCodes.Status200OK, vmListaNotas);
        }




        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse <VMResidente> genericResponse = new GenericResponse<VMResidente>();

            try
            {
                VMResidente vmResidente = JsonConvert.DeserializeObject<VMResidente>(modelo);

                string nombreImagen = "";
                Stream imagenStream = null;

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }

                CteFichaIdentificacionResidente Residente_creado = await _ResidenteService.Crear(_mapper.Map<CteFichaIdentificacionResidente>(vmResidente),imagenStream,nombreImagen);

                vmResidente = _mapper.Map<VMResidente>(Residente_creado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmResidente;

            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }







        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<VMResidente> genericResponse = new GenericResponse<VMResidente>();

            try
            {
                VMResidente vmResidente = JsonConvert.DeserializeObject<VMResidente>(modelo);

                string nombreImagen = "";
                Stream imagenStream = null;

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }

                CteFichaIdentificacionResidente Residente_editado= await _ResidenteService.Editar(_mapper.Map<CteFichaIdentificacionResidente>(vmResidente), imagenStream,  nombreImagen );

                vmResidente = _mapper.Map<VMResidente>(Residente_editado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmResidente;

            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }






        [HttpDelete]
        public async Task<IActionResult>Eliminar(int IdResidente)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();

            try
            {
                genericResponse.Estado = await _ResidenteService.Eliminar(IdResidente);

            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);


        }





    }
}
