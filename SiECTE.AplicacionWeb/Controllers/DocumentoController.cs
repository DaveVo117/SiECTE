using Microsoft.AspNetCore.Mvc;

using AutoMapper;
//using Newtonsoft.Json;
using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.AplicacionWeb.Utilidades.Response;
using SiECTE.BLL.Interfaces;
using SiECTE.Entity;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

namespace SiECTE.AplicacionWeb.Controllers
{
    //Seguridad de Inicio de Sesion
    [Authorize]

    public class DocumentoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDocumentoService _DocumentoService;
        public DocumentoController(IMapper mapper, IDocumentoService DocumentoService)//Constructor
        {
            _mapper= mapper;
            _DocumentoService= DocumentoService;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult>Lista()
        {

            List<VMDocumento> vmDocumentoLista = _mapper.Map<List<VMDocumento>>(await _DocumentoService.Lista());//Convierte lista de DocumentoService en Lista de VMCategoría
            return StatusCode(StatusCodes.Status200OK, new { data = vmDocumentoLista.OrderBy(x=>x.IdCtrlDocumentoIngresoResidente)});  //la propiedad data se ocupa para llenar un datatable

        }




        [HttpPost]
        public async Task<IActionResult>Crear([FromBody]VMDocumento modelo)
        {

            GenericResponse<VMDocumento> genericResponse = new GenericResponse<VMDocumento>();

            try
            {
                CteCatDocumentoIngreso Documento_creada = await _DocumentoService.Crear(_mapper.Map<CteCatDocumentoIngreso>(modelo));
                modelo = _mapper.Map<VMDocumento>(Documento_creada);

                genericResponse.Estado = true;
                genericResponse.Objeto = modelo;
            }
            catch (Exception ex) 
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }




        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMDocumento modelo)
        {

            GenericResponse<VMDocumento> genericResponse = new GenericResponse<VMDocumento>();

            try
            {
                CteCatDocumentoIngreso Documento_editada = await _DocumentoService.Editar(_mapper.Map<CteCatDocumentoIngreso>(modelo));
                modelo = _mapper.Map<VMDocumento>(Documento_editada);

                genericResponse.Estado = true;
                genericResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }




        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idDocumento)
        {

            GenericResponse<string> genericResponse = new GenericResponse<string>();

            try
            {

                genericResponse.Estado = await _DocumentoService.Eliminar(idDocumento);

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
