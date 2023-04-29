using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatTipoNota
    {
        public CteCatTipoNota()
        {
            CteCtrlNotaResidentes = new HashSet<CteCtrlNotaResidente>();
        }

        public int IdTipoNota { get; set; }
        public string? TxtTipoNota { get; set; }
        public string? TxtDescripcion { get; set; }

        public virtual ICollection<CteCtrlNotaResidente> CteCtrlNotaResidentes { get; set; }
    }
}
