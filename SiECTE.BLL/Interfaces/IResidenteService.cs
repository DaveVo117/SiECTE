using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SiECTE.Entity;

namespace SiECTE.BLL.Interfaces
{
    public interface IResidenteService
    {
        Task<List<CteFichaIdentificacionResidente>> Lista();
        Task<CteFichaIdentificacionResidente> Crear(CteFichaIdentificacionResidente entidad, Stream imagen = null, string nombreImagen ="");
        Task<CteFichaIdentificacionResidente> Editar(CteFichaIdentificacionResidente entidad, Stream imagen = null, string nombreImagen = "");
        Task<bool> Eliminar(int idResidente);

    }
}
