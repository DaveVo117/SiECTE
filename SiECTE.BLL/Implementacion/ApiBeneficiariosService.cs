using SiECTE.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SiECTE.BLL.Implementacion
{
    public class ApiBeneficiariosService : IApiBeneficiariosService
    {
        public string ApiBeneficiariosToken(string? token)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respuesta = httpclient.GetAsync("http://172.16.1.42/apiBeneficiarios/api/Renapo/CURP/TEST00001234567890").Result;

            if (respuesta.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return token;
            }

            var json = new StringContent(JsonSerializer.Serialize(new { usuario = "usuExpedienteCTE", contraseña = "e5xdx*JzRUlt" }), Encoding.UTF8, "application/json");
            //var httpclient = new HttpClient();
            respuesta = httpclient.PostAsync("http://172.16.1.42/apiBeneficiarios/Auth/Login", json).Result;
            string respuesta1 = respuesta.Content.ReadAsStringAsync().Result;
            return respuesta1;
        }

    }
}
