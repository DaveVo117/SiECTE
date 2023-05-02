using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatDocumentoIngreso
    {
        public CteCatDocumentoIngreso()
        {
            CteCtrlDocumentoIngresoResidentes = new HashSet<CteCtrlDocumentoIngresoResidente>();
        }

        public int IdDocumentoIngreso { get; set; }
        public int? IdOrganismo { get; set; }
        public string? TxtDocumentoIngreso { get; set; }
        public string? TxtDescripcion { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public bool? SnActivo { get; set; }

        public virtual Organismo? IdOrganismoNavigation { get; set; }
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        public virtual ICollection<CteCtrlDocumentoIngresoResidente> CteCtrlDocumentoIngresoResidentes { get; set; }
    }
}
