using SiECTE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiECTE.BLL.Interfaces
{
    public interface IUsuarioService
    {

        Task<List<CteCatUsuario>> Lista();
        Task<CteCatUsuario> Crear(CteCatUsuario entidad,Stream foto = null,string nombreFoto = "", string urlPlantillaCorreo = "");
        Task<CteCatUsuario> Editar(CteCatUsuario entidad, Stream foto = null, string nombreFoto = "");
        Task<bool> Eliminar(int idUsuario);
        Task<CteCatUsuario> ObtenerPorCredenciales(string correo, string clave);
        Task<CteCatUsuario> ObtenerPorId(int idUsuario);
        Task<bool> GuardarPerfil(CteCatUsuario entidad);
        Task<bool> CambiarClave(int idUsuario,string claveActual, string claveNueva);
        Task<bool> RestablecerClave(string correo, string UrlPlantillaCorreo);

    }
}
