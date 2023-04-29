using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatEstatus
    {
        public CteCatEstatus()
        {
            CteFichaIdentificacionResidentes = new HashSet<CteFichaIdentificacionResidente>();
        }

        public int IdEstatus { get; set; }
        public string? TxtEstatus { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }

        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        public virtual ICollection<CteFichaIdentificacionResidente> CteFichaIdentificacionResidentes { get; set; }
    }
}
