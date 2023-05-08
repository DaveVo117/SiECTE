 using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SiECTE.AplicacionWeb.Utilidades.ViewComponents
{
    public class OrganismoViewComponent: ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync()//usar este nombre específico
        {

            ClaimsPrincipal claimUser = HttpContext.User;

            string siglas = "";
            string txtOrganismo = "";


            if (claimUser.Identity.IsAuthenticated)
            {
                siglas = ((ClaimsIdentity) claimUser.Identity).FindFirst("TxtSiglas").Value;
                txtOrganismo = ((ClaimsIdentity) claimUser.Identity).FindFirst("TxtOrganismo").Value;
            }

            ViewData ["txtSiglas"] = siglas;
            ViewData ["txtOrganismo"] = txtOrganismo;

            return View();

        }


    }
}
