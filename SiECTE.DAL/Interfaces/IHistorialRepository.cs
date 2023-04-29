using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SiECTE.Entity;

namespace SiECTE.DAL.Interfaces
{
    public interface IHistorialRepository:IGenericRepository<CteHistorialIngresoResidente>
    {
        Task<CteHistorialIngresoResidente> Registrar(CteHistorialIngresoResidente entidad);
        //Task<List<DetalleIngreso>> Reporte(DateTime FechaIni,DateTime FechaFin);
        Task<List<CteHistorialIngresoResidente>> Reporte(DateTime FechaIni, DateTime FechaFin);

    }
}
