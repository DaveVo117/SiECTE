using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatPension
    {
        public CteCatPension()
        {
            CteCtrlPensionResidentes = new HashSet<CteCtrlPensionResidente>();
        }

        public int IdPension { get; set; }
        public string? TxtPension { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActuliza { get; set; }

        public virtual CteCatUsuario? UsuarioActulizaNavigation { get; set; }
        public virtual ICollection<CteCtrlPensionResidente> CteCtrlPensionResidentes { get; set; }
    }
}
