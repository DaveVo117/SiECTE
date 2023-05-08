using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Diagnostics;
using SiECTE.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Net.Http.Headers;
using SiECTE.BLL.Interfaces;
using SiECTE.BLL.Implementacion;

namespace SiECTE.AplicacionWeb.Controllers
{

    public class SIEBController : Controller
    {
        //private readonly IApiBeneficiariosService _apiService;
        //    public SIEBController(IApiBeneficiariosService apiService)
        //    {
        //        _apiService= apiService;
        //    }


        public IActionResult SiebBusquedaIdBeneficiario([Required] string id)
        {
            ApiBeneficiariosService obtenerToken = new ApiBeneficiariosService();
            string token = obtenerToken.ApiBeneficiariosToken(HttpContext.Session.GetString("token"));
            HttpContext.Session.SetString("token", token);

            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respuesta = httpclient.GetAsync("http://172.16.1.42/apiBeneficiarios/api/Beneficiarios/busqueda/id/" + id).Result;
            ViewBag.beneficiario1 = respuesta.Content.ReadAsStringAsync().Result;
            return View("Index");
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
