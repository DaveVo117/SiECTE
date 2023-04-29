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






    }
}
