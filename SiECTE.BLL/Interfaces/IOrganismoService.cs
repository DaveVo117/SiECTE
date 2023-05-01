using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiECTE.Entity;

namespace SiECTE.BLL.Interfaces
{
    public interface IOrganismoService
    {

        Task<Organismo> Obtener();

        Task<List<Organismo>> ListaOrganismos();


        Task<Organismo> Crear(Organismo entidad, Stream foto = null, string nombreFoto = "");

        Task<Organismo> GuardarCambios(Organismo entidad, Stream Logo = null ,string NombreLogo="");

        Task<bool> Eliminar(int idOrganismo);



    }
}
