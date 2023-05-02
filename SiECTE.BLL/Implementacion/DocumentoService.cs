using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SiECTE.BLL.Interfaces;
using SiECTE.DAL.Interfaces;
using SiECTE.Entity;

namespace SiECTE.BLL.Implementacion
{
    public class DocumentoService : IDocumentoService
    {
        /*ATRIBUTOS*/
        private readonly IGenericRepository<CteCatDocumentoIngreso> _repositorio;

        public DocumentoService(IGenericRepository<CteCatDocumentoIngreso> repositorio) //Constructor
        {
            _repositorio= repositorio;
        }

        /*METODOS*/
    
        public async Task<List<CteCtrlDocumentoIngresoResidente>> Lista()
        {
            //IQueryable<CteCtrlDocumentoIngresoResidente> query = await _repositorio.Consultar();

            return null;
        }




        public async Task<List<CteCatDocumentoIngreso>> CatLista()
        {
            IQueryable<CteCatDocumentoIngreso> query = await _repositorio.Consultar();
            return query.ToList();
        }




        public async Task<CteCatDocumentoIngreso> Crear(CteCatDocumentoIngreso entidad)
        {
            try
            {
                CteCatDocumentoIngreso categoria_creada = await _repositorio.Crear(entidad);

                if(categoria_creada.IdDocumentoIngreso ==0)
                    throw new TaskCanceledException("No se pudo crear la categoría");

                return categoria_creada;
            }
            catch
            {

                throw;
            }
        }




        public async Task<CteCatDocumentoIngreso> Editar(CteCatDocumentoIngreso entidad)
        {
            try
            {
                CteCatDocumentoIngreso categoria_encontrada = await _repositorio.Obtener(c=>c.IdDocumentoIngreso == entidad.IdDocumentoIngreso);
                categoria_encontrada.TxtDescripcion = entidad.TxtDescripcion;
                categoria_encontrada.SnActivo = entidad.SnActivo;
                bool respuesta = await _repositorio.Editar(categoria_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo nodificar la categoría");

                return categoria_encontrada;
            }
            catch 
            {

                throw;
            }
        }

    
        public async Task<bool> Eliminar(int idCategoria)
        {
            try
            {
                CteCatDocumentoIngreso categoria_encontrada = await _repositorio.Obtener(c => c.IdDocumentoIngreso == idCategoria);

                if (categoria_encontrada == null)
                    throw new TaskCanceledException("El Documento no existe");
                
                bool respuesta = await _repositorio.Eliminar(categoria_encontrada);

                return respuesta;

            }
            catch
            {

                throw;
            }
        }




    }
}
