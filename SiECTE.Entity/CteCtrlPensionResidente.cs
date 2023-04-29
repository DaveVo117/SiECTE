using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCtrlPensionResidente
    {
        public int IdCtrlPension { get; set; }
        public int IdResidente { get; set; }
        public int? IdPension { get; set; }
        public decimal? MontoPension { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public bool? SnActivo { get; set; }

        public virtual CteCatPension? IdPensionNavigation { get; set; }
        public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
    }
}
