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
    public class TipoDocumentoIngresoService : ITipoDocumentoIngresoService
    {
        /*ATRIBUTOS*/
        private readonly IGenericRepository<CteCatDocumentoIngreso> _repositorio;

        public TipoDocumentoIngresoService(IGenericRepository<CteCatDocumentoIngreso> repositorio)//Constructor
        {
            _repositorio = repositorio;
        }




        /*METODOS*/
        public async Task<List<CteCatDocumentoIngreso>> Lista()
        {
            IQueryable<CteCatDocumentoIngreso> query = await _repositorio.Consultar();
            return query.ToList();
        }

    }
}
