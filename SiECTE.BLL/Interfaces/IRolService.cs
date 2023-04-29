using SiECTE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiECTE.BLL.Interfaces
{
  public interface IRolService
  {

    Task<List<CteCatRol>> Lista();

  }
}
