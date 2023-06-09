﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SiECTE.DAL.DBContext;
using SiECTE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SiECTE.DAL.Implementacion
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly CTE_DBContext _dbContext;
        //constructor
        public GenericRepository(CTE_DBContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch (Exception)
            {

                throw ;
            }


            
        }
        public async Task<TEntity> Crear(TEntity entidad)
        {
            try
            {
                _dbContext.Set<TEntity>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(TEntity entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(TEntity entidad)
        {
            try
            {
                _dbContext.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }




        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
          IQueryable<TEntity>queryEntidad = filtro ==null? _dbContext.Set<TEntity>():_dbContext.Set<TEntity>().Where(filtro);
            return queryEntidad;
        }





        public async Task<TEntity> ObtenerUsuario(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await _dbContext.Set<TEntity>()
                    .Include("IdOrganismoNavigation") // incluye la tabla relacionada IdOrganismoNavigation
                    .FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
