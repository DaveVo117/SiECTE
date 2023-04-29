using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SiECTE.BLL.Interfaces;
using SiECTE.DAL.Interfaces;
using SiECTE.Entity;

namespace SiECTE.BLL.Implementacion
{
    public class HistorialIngresoService : IHistorialIngresoService
    {

        /*ATRIBUTOS*/
        private readonly IGenericRepository<CteFichaIdentificacionResidente> _repositorioResidente;
        private readonly IHistorialRepository _repositorioHistorial;

        public HistorialIngresoService(IGenericRepository<CteFichaIdentificacionResidente> repositorioResidente, IHistorialRepository repositorioHistorial)//Constructor
        {
            _repositorioResidente = repositorioResidente;
            _repositorioHistorial = repositorioHistorial;
        }



        /*METODOS*/
        public async Task<List<CteFichaIdentificacionResidente>> ObteberResidentes(string busqueda)
        {
            IQueryable<CteFichaIdentificacionResidente> query = await _repositorioResidente.Consultar(p =>
                p.IdEstatus == 1 &&
                //p.Stock > 0 &&
                string.Concat(
                    p.IdBeneficiario,
                    p.IdResidente,
                    p.FechaIngreso
                    ).Contains(busqueda)
                );
            return query.Include(c => c.IdEstatusNavigation).ToList();
        }





        public async Task<CteHistorialIngresoResidente> Registrar(CteHistorialIngresoResidente entidad)
        {
            try
            {
                return await _repositorioHistorial.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }





        public async Task<List<CteHistorialIngresoResidente>> Historial(string IdResidente, string fechaIni, string fechaFin)
        {
            IQueryable<CteHistorialIngresoResidente> query = await _repositorioHistorial.Consultar();

            fechaIni = fechaIni is null ? "" : fechaIni;
            fechaFin = fechaFin is null ? "" : fechaFin;

            if (fechaIni != "" && fechaFin != "")
            {
                DateTime fecha_ini = DateTime.ParseExact(fechaIni, "dd/MM/yyyy", new CultureInfo("en-MX"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("en-MX"));

                return query.Where(v =>
                    v.FechaIngreso.Value.Date >= fecha_ini.Date &&
                    v.FechaIngreso.Value.Date <= fecha_fin.Date
                )
                    .Include(tdv => tdv.IdResidenteNavigation)
                    .Include(u => u.UsuarioActualizaNavigation)
            
                    //.ThenInclude(p=>p.IdResidenteNavigation)
                    //.Select(pd=> new { txtDescripcionResidente = pd.DetalleVenta.Select(pd=>pd.txt)})
                    .ToList();
            }
            else
            {
                return query.Where(v => v.IdResidente.ToString() == IdResidente.ToString())
                   .Include(tdv => tdv.IdResidenteNavigation)
                   .Include(u => u.UsuarioActualizaNavigation)
                   //.Include(dv => dv.DetalleVenta)
                   .ToList();
            }


        }






        public async Task<CteHistorialIngresoResidente> Detalle(string idResidente)
        {
            IQueryable<CteHistorialIngresoResidente> query = await _repositorioHistorial.Consultar(v => v.IdResidente.ToString() == idResidente);

            return query.Include(tdv => tdv.IdResidenteNavigation)
                .Include(u => u.UsuarioActualizaNavigation)
                //.Include(dv => dv.DetalleVenta)
                .First();
        }






        public async Task<List<CteFichaIdentificacionResidente>> Reporte(string fechaIni, string fechaFin)
        {
            DateTime fecha_ini = DateTime.ParseExact(fechaIni, "dd/MM/yyyy", new CultureInfo("en-MX"));
            DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("en-MX"));

            IQueryable<CteFichaIdentificacionResidente> query = await _repositorioResidente.Consultar (r=>r.FechaIngreso >= DateTime.Parse(fechaIni )&& r.FechaIngreso<= DateTime.Parse(fechaFin)  );
            return query.ToList();

        }

    }
}
