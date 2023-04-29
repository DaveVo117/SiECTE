using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatServicioSalud
    {
        public CteCatServicioSalud()
        {
            CteCtrlNotaResidentes = new HashSet<CteCtrlNotaResidente>();
        }

        public int IdServicioSalud { get; set; }
        public string? TxtServicioSalud { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }

        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        public virtual ICollection<CteCtrlNotaResidente> CteCtrlNotaResidentes { get; set; }
    }
}
