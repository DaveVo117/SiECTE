using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SiECTE.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
  {
/*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly IOrganismoService _OrganismoService;
        private readonly IHistorialIngresoService _historialService;
    public PlantillaController(IMapper mapper, IOrganismoService OrganismoService, IHistorialIngresoService historialService)//Constructor
        {
            _mapper = mapper;
            _OrganismoService = OrganismoService;
            _historialService = historialService;
        }




/*METODOS*/
        public IActionResult EnviarClave(string correo, string clave)
    {
      //Comparte información con la vista
      ViewData["Correo"] = correo;
      ViewData["Clave"] = clave;
      ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";
      return View();
    }


    public IActionResult RestablecerClave(string clave)
    {
      ViewData["Clave"] = clave;
      return View();
    }



    public async Task<IActionResult> PDFIngreso(string numeroIngreso)
    {

            VMHistorialIngreso vmHistorial = _mapper.Map<VMHistorialIngreso>(await _historialService.Detalle(numeroIngreso));
            VMOrganismo vmOrganismo= _mapper.Map<VMOrganismo>(await _OrganismoService.Obtener());

            VMPDFHsitorialIngreso modelo = new VMPDFHsitorialIngreso();

            modelo.organismo = vmOrganismo;
            modelo.historial = vmHistorial;

            return View(modelo);
    }



  }
}
