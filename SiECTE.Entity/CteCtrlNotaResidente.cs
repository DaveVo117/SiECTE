using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCtrlNotaResidente
    {
        public int IdNotaResidente { get; set; }
        public int IdResidente { get; set; }
        public int IdTipoNota { get; set; }
        public string? TxtNota { get; set; }
        public int? IdServicioSalud { get; set; }
        public string? TxtOtroServicioSalud { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public bool? SnActivo { get; set; }

        public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        public virtual CteCatServicioSalud? IdServicioSaludNavigation { get; set; }
        public virtual CteCatTipoNota IdTipoNotaNavigation { get; set; } = null!;
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
    }
}
