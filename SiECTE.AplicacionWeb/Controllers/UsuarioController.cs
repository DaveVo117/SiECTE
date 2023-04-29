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
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRolService _rolServicio;
        private readonly IOrganismoService _organismoService;
        private readonly IMapper _mapper;
        
 /*CONSTRUCTOR*/
        public UsuarioController(IUsuarioService usuarioService, 
                                           IRolService rolServicio,
                                           IMapper mapper,
                                           IOrganismoService organismoService)
        {
            _usuarioService= usuarioService;
            _rolServicio= rolServicio;
            _mapper=mapper;
            _organismoService= organismoService;
        }
        public IActionResult Index()
        {
            return View();
        }

/*METODOS*/

        [HttpGet]
        public async Task<IActionResult> ListaRoles()
        {
            var lista = await _rolServicio.Lista();
            //Convertimos lista en el tipo VMRol (ViewModelRol) con mapper
            List<VMRol>vmListaRoles = _mapper.Map<List<VMRol>>(lista);
            return StatusCode(StatusCodes.Status200OK, vmListaRoles);
        }


        [HttpGet]
        public async Task<IActionResult> ListaOrganismos()
        {
            var lista = await _organismoService.ListaOrganismos();

            List<VMOrganismo> vmListaOrganismos = _mapper.Map<List<VMOrganismo>>(lista);
            return StatusCode(StatusCodes.Status200OK, vmListaOrganismos);
        }



        [HttpGet]
        public async Task<IActionResult> ListaOrganismosData()
        {
            var lista = await _organismoService.ListaOrganismos();

            List<VMOrganismo> vmListaOrganismos = _mapper.Map<List<VMOrganismo>>(lista);
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaOrganismos }); //se regresa el formato data ya que es necesario para trabajar con DataTable de Jquery
        }



        [HttpGet]
        public async Task<IActionResult> Lista() //lista de usuarios
        {
            var lista = await _usuarioService.Lista();
            List<VMUsuario> vmUsuarioLista = _mapper.Map<List<VMUsuario>>(lista);
            return StatusCode(StatusCodes.Status200OK, new {data = vmUsuarioLista});//se regresa el formato data ya que es necesario para trabajar con DataTable de Jquery
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            //GenericResponse se utiliza en este método como un formato estándar par alas respuestas, se leerá con JavaScript
            GenericResponse<VMUsuario> genericResponse = new GenericResponse<VMUsuario>();
            try
            {

                VMUsuario vmUsuario = JsonConvert.DeserializeObject<VMUsuario>(modelo);

                string nombreFoto = "";
                Stream fotoStream = null;

                if (foto!= null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto= string.Concat(nombre_en_codigo, extension);
                    fotoStream = foto.OpenReadStream();
                }

                string urlPlantillaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/EnviarClave?correo=[correo]&clave=[clave]"; //en servicioUsuario método Crear() reemplaza las secciones entre corchetes

                CteCatUsuario usuario_creado = await _usuarioService.Crear(_mapper.Map<CteCatUsuario>(vmUsuario), fotoStream, nombreFoto, urlPlantillaCorreo);//el primer parámetro lo convertimos el tipo vmUsuario al tipo Usuario

                vmUsuario = _mapper.Map<VMUsuario>(usuario_creado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmUsuario;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK,genericResponse);

        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            //GenericResponse se utiliza en este método como un formato estándar par alas respuestas, se leerá con JavaScript
            GenericResponse<VMUsuario> genericResponse = new GenericResponse<VMUsuario>();
            try
            {

                VMUsuario vmUsuario = JsonConvert.DeserializeObject<VMUsuario>(modelo);

                string nombreFoto = "";
                Stream fotoStream = null;

                if (foto != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto = string.Concat(nombre_en_codigo, extension);
                    fotoStream = foto.OpenReadStream();
                }
             
                CteCatUsuario usuario_editado = await _usuarioService.Editar(_mapper.Map<CteCatUsuario>(vmUsuario), fotoStream, nombreFoto);//el primer parámetro lo convertimos el tipo vmUsuario al tipo Usuario

                vmUsuario = _mapper.Map<VMUsuario>(usuario_editado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmUsuario;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }


        [HttpDelete]
        public async Task<IActionResult>Eliminar(int IdUsuario)
        {
             GenericResponse<string> genericResponse = new GenericResponse<string>();
            try
            {
                genericResponse.Estado =await _usuarioService.Eliminar(IdUsuario);
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje=ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }



    }
}
