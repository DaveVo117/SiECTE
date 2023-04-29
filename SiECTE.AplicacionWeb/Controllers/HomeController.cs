using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiECTE.AplicacionWeb.Models;
using System.Diagnostics;
using System.Security.Claims;

using AutoMapper;
using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.AplicacionWeb.Utilidades.Response;
using SiECTE.BLL.Interfaces;
using SiECTE.Entity;

namespace SiECTE.AplicacionWeb.Controllers
{

    //Seguridad de Inicio de Sesion
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public HomeController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerUsuario()
        {
            GenericResponse<VMUsuario> gResponse = new GenericResponse<VMUsuario>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string IdUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                VMUsuario usuario = _mapper.Map<VMUsuario>(await _usuarioService.ObtenerPorId(int.Parse(IdUsuario)));

                gResponse.Estado = true;
                gResponse.Objeto = usuario;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }



        [HttpPost]
        public async Task<IActionResult> GuardarPerfil([FromBody] VMUsuario modelo)
        {
            GenericResponse<VMUsuario> gResponse = new GenericResponse<VMUsuario>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string IdUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                CteCatUsuario usuario = _mapper.Map<CteCatUsuario>(modelo);

                usuario.IdUsuario = int.Parse(IdUsuario);

                bool resultado = await _usuarioService.GuardarPerfil(usuario);

                gResponse.Estado = resultado;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }





        [HttpPost]
        public async Task<IActionResult> CambiarClave([FromBody] VMCambiarClave modelo)
        {
            GenericResponse<bool> gResponse = new GenericResponse<bool>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string IdUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();


                bool resultado = await _usuarioService.CambiarClave(
                    int.Parse(IdUsuario),
                    modelo.claveActual,
                    modelo.claveNueva
                    );

                gResponse.Estado = resultado;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public async Task<IActionResult> Salir()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Acceso");
        }






    }


}