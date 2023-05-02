using SiECTE.BLL.Interfaces;
using SiECTE.DAL.Interfaces;
using SiECTE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiECTE.BLL.Implementacion
{
    public class TipoNotaService: ITipoNotaService
    {
        //Atributos
        private readonly IGenericRepository<CteCatTipoNota> _repositorio;

        public TipoNotaService(IGenericRepository<CteCatTipoNota> repositorio)
        {
            _repositorio = repositorio;
        }

        //METODOS
        public async Task<List<CteCatTipoNota>> Lista()
        {
            IQueryable<CteCatTipoNota> query = await _repositorio.Consultar();
            return query.ToList();
        }


    }
}
