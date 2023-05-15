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
using SiECTE.AplicacionWeb.Models.ViewModels;

using System.Security.Claims;
using NuGet.Protocol;
using SiECTE.AplicacionWeb.Utilidades.Response;

namespace SiECTE.AplicacionWeb.Controllers
{

    public class SIEBController : Controller
    {
        //private readonly IApiBeneficiariosService _apiService;
        //    public SIEBController(IApiBeneficiariosService apiService)
        //    {
        //        _apiService= apiService;
        //    }

        public curpModel? objetoCurp = new curpModel();
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            public int? Id { get; set; } = 0;
            public int? IdDomicilio { get; set; } = 0;
            [StringLength(18, ErrorMessage = "La CURP debe contener 18 caracteres", MinimumLength = 18)]
            [Display(Name = "CURP")]
            public string? curp { get; set; } = string.Empty;
            [Required(ErrorMessage = "El campo nombre es requerido")]
            [Display(Name = "*Nombre(s)")]
            public string nombre { get; set; } = string.Empty;
            [Required(ErrorMessage = "El campo primer apellido es requerido")]
            [Display(Name = "*Primer apellido")]
            public string apellido1 { get; set; } = string.Empty;
            [Display(Name = "Segundo apellido")]
            public string? apellido2 { get; set; } = string.Empty;
            [Required(ErrorMessage = "El campo fecha de nacimiento es requerido"), DataType(DataType.Date)]
            [Display(Name = "*Fecha de nacimiento")]
            public DateTime? fechaNac { get; set; } = DateTime.MinValue;
            [Required(ErrorMessage = "El campo sexo es requerido")]
            [Display(Name = "*Sexo")]
            public int sexo { get; set; } = 0;
            [Required(ErrorMessage = "El campo estado de nacimiento es requerido")]
            [Display(Name = "*Estado de nacimiento")]
            public int edoNac { get; set; } = 0;
            [Display(Name = "Estado civil")]
            public int? edoCiv { get; set; } = 0;
            [Display(Name = "Tipo de identificación")]
            public int? tipoIdentificacion { get; set; } = 0;
            [StringLength(13, ErrorMessage = "El RFC debe contener entre 12 y 13 caracteres", MinimumLength = 12)]
            [Display(Name = "RFC")]
            public string? rfc { get; set; } = "";
            [Display(Name = "Teléfono")]
            [DataType(DataType.PhoneNumber)]
            public string? telefono { get; set; } = String.Empty;
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Celular")]
            public string? celular { get; set; } = String.Empty;
            [Display(Name = "Representa a una institución")]
            public bool? representaInst { get; set; } = false;
            [Display(Name = "Nombre de la institucion")]
            public string? nombreInstitucion { get; set; } = String.Empty;
            [Required(ErrorMessage = "El campo estado es requerido")]
            [Display(Name = "*Estado")]
            public int? edoAct { get; set; } = 0;
            [Required(ErrorMessage = "El campo municipio es requerido")]
            [Display(Name = "*Municipio")]
            public int? mun { get; set; } = 0;
            [Required(ErrorMessage = "El campo localidad es requerido")]
            [Display(Name = "*Localidad")]
            public int? loc { get; set; } = 1;
            [Required(ErrorMessage = "El campo nombre del asentamiento es requerido")]
            [Display(Name = "*Nombre del asentamiento")]
            public string nomAsen { get; set; } = String.Empty;
            [Display(Name = "Código postal")]
            public int? CP { get; set; }
            [Required(ErrorMessage = "El campo nombre de la vialidad es requerido")]
            [Display(Name = "*Nombre de la vialidad")]
            public string nomVia { get; set; } = string.Empty;
            [Display(Name = "Numero exterior")]
            public string? noExt { get; set; } = String.Empty;
            [Display(Name = "Numero interior")]
            public string? noInt { get; set; } = String.Empty;
            [Display(Name = "Entre calle")]
            public string? entreCalle1 { get; set; } = String.Empty;
            [Display(Name = "y calle")]
            public string? entreCalle2 { get; set; } = String.Empty;
            [Display(Name = "Descripción del domicilio")]
            public string? descDomicilio { get; set; } = String.Empty;
            [Display(Name = "Ocupación")]
            public string? ocupacion { get; set; } = String.Empty;
            [Display(Name = "Tipo de asentamiento")]
            public int? tipoAsentamiento { get; set; } = 8;
            [Display(Name = "Tipo de vialidad")]
            public int? tipoVialidad { get; set; } = 6;
            public string? vialidadAct { get; set; } = String.Empty;
            public string? asentamientoAct { get; set; } = String.Empty;
            public bool rdbMismo { get; set; }
            public int? validaCurp { get; set; } = 0;
            public int? idEstado { get; set; } = 0;
            public int? idMun { get; set; } = 0;
            public int? idLoc { get; set; } = 0;
            public int? idTipoVia { get; set; } = 0;
            public string? hdNombreVia { get; set; } = string.Empty;
            public int? idTipoAsen { get; set; } = 0;
            public string? hdnombreAsen { get; set; } = string.Empty;
            public string hdNumInt { get; set; } = string.Empty;
            public string hdNumExt { get; set; } = string.Empty;
            public int? hdCp { get; set; } = 0;
            public string? hdEntreCalle1 { get; set; } = String.Empty;
            public string? hdEntreCalle2 { get; set; } = String.Empty;
            public string? hdDescDomicilio { get; set; } = String.Empty;

        }


        private string cve_estado_curp(int id_est)
        {

            switch (id_est)
            {
                case 2:
                    //AGUASCALIENTES
                    return "AS";
                    break;
                case 3:
                    //BAJA CALIFORNIA
                    return "BC";
                    break;
                case 4:
                    //BAJA CALIFORNIA SUR
                    return "BS";
                    break;
                case 5:
                    //CAMPECHE
                    return "CC";
                    break;
                case 6:
                    //COAHUILA
                    return "CL";
                    break;
                case 7:
                    //COLIMA
                    return "CM";
                    break;
                case 8:
                    //CHIAPAS
                    return "CS";
                    break;
                case 9:
                    //CHIHUAHUA
                    return "CH";
                    break;
                case 10:
                    //DISTRITO FEDERAL
                    return "DF";
                    break;
                case 11:
                    //DURANGO
                    return "DG";
                    break;
                case 12:
                    //GUANAJUATO
                    return "GT";
                    break;
                case 13:
                    //GUERRERO
                    return "GR";
                    break;
                case 14:
                    //HIDALGO
                    return "HG";
                    break;
                case 15:
                    //JALISCO
                    return "JC";
                    break;
                case 16:
                    //MEXICO
                    return "MC";
                    break;
                case 17:
                    //MICHOACAN
                    return "MN";
                    break;
                case 18:
                    //MORELOS
                    return "MS";
                    break;
                case 19:
                    //NAYARIT
                    return "NT";
                    break;
                case 20:
                    //NUEVO LEON
                    return "NL";
                    break;
                case 21:
                    //OAXACA
                    return "OC";
                    break;
                case 22:
                    //PUEBLA
                    return "PL";
                    break;
                case 23:
                    //QUERETARO
                    return "QT";
                    break;
                case 24:
                    //QUINTANA ROO
                    return "QR";
                    break;
                case 25:
                    //SAN LUIS POTOSI
                    return "SP";
                    break;
                case 26:
                    //SINALOA
                    return "SL";
                    break;
                case 27:
                    //SONORA
                    return "SR";
                    break;
                case 28:
                    //TABASCO
                    return "TC";
                    break;
                case 29:
                    //TAMAULIPAS
                    return "TS";
                    break;
                case 30:
                    //TLAXCALA
                    return "TL";
                    break;
                case 31:
                    //VERACRUZ
                    return "VZ";
                    break;
                case 32:
                    //YUCATAN
                    return "YN";
                    break;
                case 33:
                    //ZACATECAS
                    return "ZS";
                    break;
                case 34:
                    return "NE";
                    break;
                default:
                    return "";
                    break;
            }
        }
        private int cve_curp_estado(string cve_est)
        {
            int entidad = 0;
            switch (cve_est)
            {
                case "AS":
                    //AGUASCALIENTES
                    entidad = 2;
                    return entidad;
                    break;
                case "BC":
                    //BAJA CALIFORNIA
                    entidad = 3;
                    return entidad;
                    break;
                case "BS":
                    //BAJA CALIFORNIA SUR
                    entidad = 4;
                    return entidad;
                    break;
                case "CC":
                    //CAMPECHE
                    entidad = 5;
                    return entidad;
                    break;
                case "CL":
                    //COAHUILA
                    entidad = 6;
                    return entidad;
                    break;
                case "CM":
                    //COLIMA
                    entidad = 7;
                    return entidad;
                    break;
                case "CS":
                    //CHIAPAS
                    entidad = 8;
                    return entidad;
                    break;
                case "CH":
                    //CHIHUAHUA
                    entidad = 9;
                    return entidad;
                    break;
                case "DF":
                    //DISTRITO FEDERAL
                    entidad = 10;
                    return entidad;
                    break;
                case "DG":
                    //DURANGO
                    entidad = 11;
                    return entidad;
                    break;
                case "GT":
                    //GUANAJUATO
                    entidad = 12;
                    return entidad;
                    break;
                case "GR":
                    //GUERRERO
                    entidad = 13;
                    return entidad;
                    break;
                case "HG":
                    //HIDALGO
                    entidad = 14;
                    return entidad;
                    break;
                case "JC":
                    //JALISCO
                    entidad = 15;
                    return entidad;
                    break;
                case "MC":
                    //MEXICO
                    entidad = 16;
                    return entidad;
                    break;
                case "MN":
                    //MICHOACAN
                    entidad = 17;
                    return entidad;
                    break;
                case "MS":
                    //MORELOS
                    entidad = 18;
                    return entidad;
                    break;
                case "NT":
                    //NAYARIT
                    entidad = 19;
                    return entidad;
                    break;
                case "NL":
                    //NUEVO LEON
                    entidad = 20;
                    return entidad;
                    break;
                case "OC":
                    //OAXACA
                    entidad = 21;
                    return entidad;
                    break;
                case "PL":
                    //PUEBLA
                    entidad = 22;
                    return entidad;
                    break;
                case "QT":
                    //QUERETARO
                    entidad = 23;
                    return entidad;
                    break;
                case "QR":
                    //QUINTANA ROO
                    entidad = 24;
                    return entidad;
                    break;
                case "SP":
                    //SAN LUIS POTOSI
                    entidad = 25;
                    return entidad;
                    break;
                case "SL":
                    //SINALOA
                    entidad = 26;
                    return entidad;
                    break;
                case "SR":
                    //SONORA
                    entidad = 27;
                    return entidad;
                    break;
                case "TC":
                    //TABASCO
                    entidad = 28;
                    return entidad;
                    break;
                case "TS":
                    //TAMAULIPAS
                    entidad = 29;
                    return entidad;
                    break;
                case "TL":
                    //TLAXCALA
                    entidad = 30;
                    return entidad;
                    break;
                case "VZ":
                    //VERACRUZ
                    entidad = 31;
                    return entidad;
                    break;
                case "YN":
                    //YUCATAN
                    entidad = 32;
                    return entidad;
                    break;
                case "ZS":
                    //ZACATECAS
                    entidad = 33;
                    return entidad;
                    break;
                case "NE":
                    entidad = 34;
                    return entidad;
                    break;
                default:
                    return entidad;
                    break;
            }
        }

        //METODOS
        public IActionResult SiebBusquedaIdBeneficiario([Required] string id)
        {
            string? token = HttpContext.Session.GetString("token");


            ApiBeneficiariosService obtenerToken = new ApiBeneficiariosService();
            //string token? = obtenerToken.ApiBeneficiariosToken(HttpContext.Session.GetString("token"));
            HttpContext.Session.SetString("token", token);

            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respuesta = httpclient.GetAsync("http://172.16.1.42/apiBeneficiarios/api/Beneficiarios/busqueda/id/" + id).Result;
            ViewBag.beneficiario1 = respuesta.Content.ReadAsStringAsync().Result;
            return View("Index");
        }





        public IActionResult OnGetValidarCurp(string nombre, string ape1, string ape2, int sexo, string fechaNac, int edoNac, string curp)
        {


            if (curp != "")
            {
                var url = "http://172.16.1.42/apiBeneficiariosPruebas/";
                string? token;
                string respuesta;
                if (HttpContext.Session.GetString("token") == null)
                {
                    using (var cliente = new HttpClient())
                    {
                        HttpResponseMessage response = cliente.PostAsync(url + "Auth/Login", JsonContent.Create(new { usuario = "saul", contraseña = "e5xdx*JzRUlt" })).Result;
                        token = response.Content.ReadAsStringAsync().Result;
                        HttpContext.Session.SetString("token", token);
                    }
                }
                else
                {
                    token = HttpContext.Session.GetString("token");
                }
                using (var cliente = new HttpClient())
                {
                    url = url + "api/Renapo/CURP/" + curp;

                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = cliente.GetAsync(url).Result;
                    try
                    {
                        respuesta = response.Content.ReadAsStringAsync().Result;
                        objetoCurp = System.Text.Json.JsonSerializer.Deserialize<curpModel>(respuesta);
                        return new JsonResult(objetoCurp);
                    }
                    catch
                    {
                        return new JsonResult("No se pudieron validar la CURP");
                    }
                }
            }
            else if (nombre != "" && ape1 != "" && ape2 != "" && sexo != 0 && edoNac != 0 && fechaNac != "01/01/0001 0:00:00")
            {
                string sex = "";
                if (sexo == 3)
                {
                    sex = "M";
                }
                else if (sexo == 2)
                {
                    sex = "H";
                }
                string estadoNac = cve_estado_curp(edoNac);

                var url = "http://172.16.1.42/apiBeneficiariosPruebas/";
                string? token;
                string respuesta;
                if (HttpContext.Session.GetString("token") == null)
                {
                    using (var cliente = new HttpClient())
                    {
                        HttpResponseMessage response = cliente.PostAsync(url + "Auth/Login", JsonContent.Create(new { usuario = "saul", contraseña = "e5xdx*JzRUlt" })).Result;
                        token = response.Content.ReadAsStringAsync().Result;
                        HttpContext.Session.SetString("token", token);
                    }
                }
                else
                {
                    token = HttpContext.Session.GetString("token");
                }
                var urlcurp = url + "api/Renapo/datos/?nombre=" + nombre + "&apePat=" + ape1 + "&apeMat=" + ape2 + "&fechNac=" + Input.fechaNac.Value.ToString("dd/MM/yyyy") + "&entidadNac=" + estadoNac + "&sexo=" + sex;

                try
                {
                    using (var clienteDC = new HttpClient())
                    {

                        clienteDC.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage responseDC = clienteDC.GetAsync(urlcurp).Result;
                        respuesta = responseDC.Content.ReadAsStringAsync().Result;
                        objetoCurp = System.Text.Json.JsonSerializer.Deserialize<curpModel>(respuesta);
                        return new JsonResult(objetoCurp);
                    }
                }
                catch
                {
                    return new JsonResult("No se pudieron validar los datos");
                }
            }
            else
            {
                //return Page();
                //return StatusCode(StatusCodes.Status200OK, respuesta);
            }
        }






        public IActionResult Index()
        {
            return View();
        }
    }
}
