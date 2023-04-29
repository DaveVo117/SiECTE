using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SiECTE.DAL.DBContext;
using SiECTE.DAL.Interfaces;
using SiECTE.Entity;


namespace SiECTE.DAL.Implementacion
{
    public class HistorialRepository : GenericRepository<CteHistorialIngresoResidente>, IHistorialRepository
    {
        private readonly CTE_DBContext _dbContext;

        public HistorialRepository(CTE_DBContext dbContext):base(dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<CteHistorialIngresoResidente> Registrar(CteHistorialIngresoResidente entidad)
        {
            try
            {
                CteHistorialIngresoResidente historialGenerado = new CteHistorialIngresoResidente();
                //Se utiliza una transacción para insertar los registros
                using(var transaction= _dbContext.Database.BeginTransaction())
                {


                    try
                    {
                        //foreach (CteFichaIdentificacionResidente dv in entidad.IdResidente)
                        //{

                        //    CteFichaIdentificacionResidente residente_encontrato = _dbContext.CteFichaIdentificacionResidentes.Where(p=>p.IdResidente==dv.IdResidente).First();

                        //    residente_encontrato.= residente_encontrato.Stock - dv.Cantidad;
                        //    _dbContext.Update(residente_encontrato);

                        //}
                        //await _dbContext.SaveChangesAsync();



                        //NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "CteHistorialIngresoResidente").First();

                        //correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                        //correlativo.FechaActualizacion = DateTime.Now;

                        //_dbContext.NumeroCorrelativos.Update(correlativo);
                        //await _dbContext.SaveChangesAsync();



                        //string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                        //string numeroHistorialResidente = ceros + correlativo.UltimoNumero.ToString();
                        //numeroHistorialResidente.Substring(numeroHistorialResidente.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                        entidad.IdHistorialIngreso = 1;
                            //numeroHistorialResidente;
                        
                        await _dbContext.AddAsync(entidad);
                        await _dbContext.SaveChangesAsync();



                        historialGenerado = entidad;

                        //Secompleta la transacción
                        transaction.Commit();   

                    }
                    catch (Exception ex)
                    {
                        //Se regresa la transacción
                        transaction.Rollback();
                        throw ex;
                    }


                }


                return historialGenerado;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CteHistorialIngresoResidente>> Reporte(DateTime FechaIni, DateTime FechaFin)
        {
            List<CteHistorialIngresoResidente> listaResumen = await _dbContext.CteHistorialIngresoResidente
                .Include(v => v.IdResidenteNavigation)
                .ThenInclude(u => u.IdBeneficiario)
                .Include(v => v.FechaIngreso)
                //.ThenInclude(tdv => tdv.IdTipoDocumentoCteHistorialIngresoResidenteNavigation)
                .Where(dv => dv.FechaIngreso.Value.Date >= FechaIni.Date &&
                        dv.FechaIngreso.Value.Date <= FechaFin).ToListAsync();

            return listaResumen;

        }
    }
}
