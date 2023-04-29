using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCtrlDocumentoIngresoResidente
    {
        public int IdCtrlDocumentoIngresoResidente { get; set; }
        public int IdResidente { get; set; }
        public int IdDocumento { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public bool? SnActivo { get; set; }

        public virtual CteCatDocumentoIngreso IdDocumentoNavigation { get; set; } = null!;
        public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
    }
}
