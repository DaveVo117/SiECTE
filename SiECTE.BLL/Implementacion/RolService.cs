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
  public class RolService : IRolService
  {
    private readonly IGenericRepository<CteCatRol> _repositorio;

    //Constructor de clase
    public RolService(IGenericRepository<CteCatRol> repositorio)
    {
      _repositorio = repositorio;
    }

    //Devuelve la lista de roles
    public async Task<List<CteCatRol>> Lista()
    {
      IQueryable<CteCatRol> query = await _repositorio.Consultar();
      return query.ToList();
    }
  }
}
