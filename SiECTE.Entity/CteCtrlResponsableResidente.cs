using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCtrlResponsableResidente
    {
        public int IdCtrlResponsable { get; set; }
        public int IdResidente { get; set; }
        public string? NombreCompleto { get; set; }
        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string SegundoApellido { get; set; } = null!;
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public int? IdIdentidicacion { get; set; }
        public string? NumeroIdentificacion { get; set; }
        public string? Parentesco { get; set; }
        public int OrdenImportancia { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public bool? SnActivo { get; set; }

        public virtual CteCatIdentificacionResidente? IdIdentidicacionNavigation { get; set; }
        public virtual CteFichaIdentificacionResidente IdResidenteNavigation { get; set; } = null!;
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
    }
}
