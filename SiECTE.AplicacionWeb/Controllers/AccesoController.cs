using Microsoft.AspNetCore.Mvc;

using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.BLL.Interfaces;
using SiECTE.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SiECTE.AplicacionWeb.Controllers
{
    public class AccesoController : Controller
    {
        /*ATRIBUTOS*/
        private readonly IUsuarioService _usuarioService;

        public AccesoController(IUsuarioService usuarioService)//Constructor
        {
            _usuarioService = usuarioService;
        }




        /*METODOS*/
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }




        public IActionResult RestablecerClave()
        {


            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Login(VMUsuarioLogin modelo)
        {

            if (modelo.TxtCorreo ==null || modelo.TxtClave == null)
            {
                ViewData ["Mensaje"] = "Debe introducir sus credenciales";
                return View();
            }

            CteCatUsuario usuario_encontrado = await _usuarioService.ObtenerPorCredenciales(modelo.TxtCorreo, modelo.TxtClave);

            if (usuario_encontrado == null)
            {
                ViewData ["Mensaje"] = "No se encontraron coincidencias";
            return View();
            }

            ViewData ["Mensaje"] = null;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,usuario_encontrado.Nombre + " " + usuario_encontrado.PrimerApellido),
                new Claim(ClaimTypes.NameIdentifier,usuario_encontrado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role,usuario_encontrado.IdRol.ToString()),
                new Claim("TxtUrlFoto",usuario_encontrado.TxtUrlFoto),
                new Claim("TxtSiglas", usuario_encontrado.IdOrganismoNavigation != null ? usuario_encontrado.IdOrganismoNavigation.TxtSiglas : "DIFH"),
                new Claim("TxtOrganismo", usuario_encontrado.IdOrganismoNavigation != null ? usuario_encontrado.IdOrganismoNavigation.TxtNombre : "")
            };

            ClaimsIdentity claimsIdentity= new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = modelo.MantenerSesion
            };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );


        return RedirectToAction("Index","Home");
        }



        [HttpPost]
        public async Task<IActionResult>RestablecerClave(VMUsuarioLogin modelo)
        {
            try
            {

                string urlPlantillaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/RestablecerClave?clave=[clave]";

                bool resultado = await _usuarioService.RestablecerClave(modelo.TxtCorreo, urlPlantillaCorreo);

                if (resultado)
                {
                    ViewData ["Mensaje"] = "Listo, su contraseña fue restablecida. Revise su correo.";
                    ViewData ["MensajeError"] = null;
                }
                else
                {
                    ViewData ["MensajeError"] = "Tenemos problemas.Por favor inténtelo de nuevo mñas tarde.";
                    ViewData ["Mensaje"] = null;
                }

            }
            catch (Exception ex)
            {
                ViewData ["MensajeError"] = ex.Message;
                ViewData ["Mensaje"] = null;
            }

            return View();
        }




    }
}
