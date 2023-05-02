using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiECTE.Entity;

namespace SiECTE.BLL.Interfaces
{
    public interface IDocumentoService
    {

        Task<List<CteCtrlDocumentoIngresoResidente>> Lista();
        Task<List<CteCatDocumentoIngreso>> CatLista();
        Task<CteCatDocumentoIngreso> Crear(CteCatDocumentoIngreso entidad);
        Task<CteCatDocumentoIngreso> Editar(CteCatDocumentoIngreso entidad);
        Task<bool> Eliminar(int idDocumento);




    }
}
