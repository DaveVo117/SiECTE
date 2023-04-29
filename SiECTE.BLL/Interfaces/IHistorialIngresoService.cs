using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SiECTE.Entity;

namespace SiECTE.BLL.Interfaces
{
    public interface IHistorialIngresoService
    {

        Task<List<CteFichaIdentificacionResidente>> ObteberResidentes(string busqueda);
        Task<CteHistorialIngresoResidente> Registrar(CteHistorialIngresoResidente entidad);
        Task<List<CteHistorialIngresoResidente>> Historial(string numeroFicha, string fechaIni, string fechaFin);
        Task<CteHistorialIngresoResidente> Detalle(string numeroFicha);
        Task<List<CteFichaIdentificacionResidente>> Reporte(string fechaIni, string fechaFin);

    }
}
