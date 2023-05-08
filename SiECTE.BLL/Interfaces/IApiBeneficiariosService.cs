using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiECTE.BLL.Interfaces
{
    public interface IApiBeneficiariosService
    {
        string ApiBeneficiariosToken(string? token);

        }
}
