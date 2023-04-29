using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteHistorialIngresoResidente
    {
        public int IdHistorialIngreso { get; set; }
        public int IdResidente { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? ObservacionesSalida { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }

        public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
    }
}
