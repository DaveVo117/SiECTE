using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SiECTE.Entity;

namespace SiECTE.BLL.Interfaces
{
    public interface ITipoDocumentoIngresoService
    {
        Task<List<CteCatDocumentoIngreso>> Lista();
    }
}
