using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatIdentificacionResidente
    {
        public CteCatIdentificacionResidente()
        {
            CteCtrlResponsableResidentes = new HashSet<CteCtrlResponsableResidente>();
        }

        public int IdIdentificacion { get; set; }
        public int? IdOrganismo { get; set; }
        public string? TxtIdentificacion { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }

        public virtual Organismo? IdOrganismoNavigation { get; set; }
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        public virtual ICollection<CteCtrlResponsableResidente> CteCtrlResponsableResidentes { get; set; }
    }
}
