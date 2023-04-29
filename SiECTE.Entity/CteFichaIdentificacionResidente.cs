using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteFichaIdentificacionResidente
    {
        public CteFichaIdentificacionResidente()
        {
            CteCtrlDocumentoIngresoResidentes = new HashSet<CteCtrlDocumentoIngresoResidente>();
            CteCtrlNotaResidentes = new HashSet<CteCtrlNotaResidente>();
            CteCtrlPensionResidentes = new HashSet<CteCtrlPensionResidente>();
            CteCtrlResponsableResidentes = new HashSet<CteCtrlResponsableResidente>();
            CteHistorialIngresoResidentes = new HashSet<CteHistorialIngresoResidente>();
        }

        public int IdResidente { get; set; }
        public int? IdOrganismo { get; set; }
        public int? IdBeneficiario { get; set; }
        public string? TxtFotografia { get; set; }
        public string? TxtUrlFotografia { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? DependenciaEmite { get; set; }
        public decimal? UltimaCuota { get; set; }
        public string? Religion { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public int? IdEstatus { get; set; }

        public virtual CteCatEstatus? IdEstatusNavigation { get; set; }
        public virtual Organismo? IdOrganismoNavigation { get; set; }
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        public virtual ICollection<CteCtrlDocumentoIngresoResidente> CteCtrlDocumentoIngresoResidentes { get; set; }
        public virtual ICollection<CteCtrlNotaResidente> CteCtrlNotaResidentes { get; set; }
        public virtual ICollection<CteCtrlPensionResidente> CteCtrlPensionResidentes { get; set; }
        public virtual ICollection<CteCtrlResponsableResidente> CteCtrlResponsableResidentes { get; set; }
        public virtual ICollection<CteHistorialIngresoResidente> CteHistorialIngresoResidentes { get; set; }
    }
}
