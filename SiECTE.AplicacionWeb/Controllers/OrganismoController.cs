using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Newtonsoft.Json;
using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.AplicacionWeb.Utilidades.Response;
using SiECTE.BLL.Interfaces;
using SiECTE.Entity;
using Microsoft.AspNetCore.Authorization;
using SiECTE.BLL.Implementacion;

namespace SiECTE.AplicacionWeb.Controllers
{
    //Seguridad de Inicio de Sesion
    [Authorize]

    public class OrganismoController : Controller
    {
    /*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly IOrganismoService _OrganismoService;

        /*CONSTRUCTOR*/
        public OrganismoController(IMapper mapper,IOrganismoService OrganismoService)
        {
            _mapper= mapper;
            _OrganismoService= OrganismoService;
        }



    /*MÉDTODOS*/
        public IActionResult Index()
        {
            return View();
        }






        [HttpGet]
        public async Task<IActionResult>Obtener()
        {
            GenericResponse<VMOrganismo> genericResponse = new GenericResponse<VMOrganismo>();

            try
            {
                VMOrganismo vmOrganismo = _mapper.Map<VMOrganismo>(await _OrganismoService.Obtener());//Convierte la información del objeto Organismo en VMOrganismo par adesplegarla en la vista

                genericResponse.Estado = true;
                genericResponse.Objeto = vmOrganismo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = true;
                genericResponse.Mensaje = ex.Message;
                throw;
            }

            return StatusCode(StatusCodes.Status200OK,genericResponse);
        }




        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            //GenericResponse se utiliza en este método como un formato estándar par alas respuestas, se leerá con JavaScript
            GenericResponse<VMOrganismo> genericResponse = new GenericResponse<VMOrganismo>();
            try
            {

                VMOrganismo vmOrganismo = JsonConvert.DeserializeObject<VMOrganismo>(modelo);

                string nombreFoto = "";
                Stream fotoStream = null;

                if (foto != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto = string.Concat(nombre_en_codigo, extension);
                    fotoStream = foto.OpenReadStream();
                }

                //string urlPlantillaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/EnviarClave?correo=[correo]&clave=[clave]"; //en servicioUsuario método Crear() reemplaza las secciones entre corchetes

                List<Organismo> organismo_creado = await _OrganismoService.Crear(_mapper.Map<Organismo>(vmOrganismo), fotoStream, nombreFoto);//el primer parámetro lo convertimos el tipo vmUsuario al tipo Usuario

                vmOrganismo = _mapper.Map<VMOrganismo>(organismo_creado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmOrganismo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }





        [HttpPost]
        public async Task<IActionResult> GuardarCambios([FromForm]IFormFile logo, [FromForm]string modelo)
        {
            GenericResponse<VMOrganismo> genericResponse = new GenericResponse<VMOrganismo>();

            try
            {
                VMOrganismo vmOrganismo = JsonConvert.DeserializeObject<VMOrganismo>(modelo);//Convierte la información del objeto Organismo en VMOrganismo par adesplegarla en la vista

                string nombreLogo = "";
                Stream logoStream= null;

                if(logo!= null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");//"N" para obtener números y letras
                    string extension = Path.GetExtension(logo.FileName);
                    nombreLogo = string.Concat(nombre_en_codigo, extension);
                    logoStream = logo.OpenReadStream();
                }

                //GuardaCambios
                Organismo Organismo_editado = await _OrganismoService.GuardarCambios(_mapper.Map<Organismo>(vmOrganismo)
                    , logoStream, nombreLogo);

                //Regresa cambios guardados para mostra en la vista
                vmOrganismo = _mapper.Map<VMOrganismo>(Organismo_editado);


                genericResponse.Estado = true;
                genericResponse.Objeto = vmOrganismo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = true;
                genericResponse.Mensaje = ex.Message;
                throw;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }





        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            //GenericResponse se utiliza en este método como un formato estándar par alas respuestas, se leerá con JavaScript
            GenericResponse<VMOrganismo> genericResponse = new GenericResponse<VMOrganismo>();
            try
            {

                VMOrganismo vmOrganismo = JsonConvert.DeserializeObject<VMOrganismo>(modelo);

                string nombreFoto = "";
                Stream fotoStream = null;

                if (foto != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(foto.FileName);
                    nombreFoto = string.Concat(nombre_en_codigo, extension);
                    fotoStream = foto.OpenReadStream();
                }

                Organismo organismo_editado = await _OrganismoService.GuardarCambios(_mapper.Map<Organismo>(vmOrganismo), fotoStream, nombreFoto);//el primer parámetro lo convertimos el tipo vmUsuario al tipo Usuario

                vmOrganismo = _mapper.Map<VMOrganismo>(organismo_editado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmOrganismo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }





        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdOrganismo)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();
            try
            {
                genericResponse.Estado = await _OrganismoService.Eliminar(IdOrganismo);
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
