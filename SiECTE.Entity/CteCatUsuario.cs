using System;
using System.Collections.Generic;

namespace SiECTE.Entity
{
    public partial class CteCatUsuario
    {
        public CteCatUsuario()
        {
            CteCatAreas = new HashSet<CteCatArea>();
            CteCatDocumentoIngresos = new HashSet<CteCatDocumentoIngreso>();
            CteCatEstatuses = new HashSet<CteCatEstatus>();
            CteCatIdentificacionResidentes = new HashSet<CteCatIdentificacionResidente>();
            CteCatPensions = new HashSet<CteCatPension>();
            CteCatServicioSaluds = new HashSet<CteCatServicioSalud>();
            CteCtrlDocumentoIngresoResidentes = new HashSet<CteCtrlDocumentoIngresoResidente>();
            CteCtrlNotaResidentes = new HashSet<CteCtrlNotaResidente>();
            CteCtrlPensionResidentes = new HashSet<CteCtrlPensionResidente>();
            CteCtrlResponsableResidentes = new HashSet<CteCtrlResponsableResidente>();
            CteFichaIdentificacionResidentes = new HashSet<CteFichaIdentificacionResidente>();
            CteHistorialIngresoResidentes = new HashSet<CteHistorialIngresoResidente>();
            InverseUsuarioActualizaNavigation = new HashSet<CteCatUsuario>();
        }

        public int IdUsuario { get; set; }
        public string CveUsuario { get; set; } = null!;
        public string PassUsuario { get; set; } = null!;
        public string? NombreCompleto { get; set; }
        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string SegundoApellido { get; set; } = null!;
        public string? Cargo { get; set; }
        public int IdArea { get; set; }
        public int? IdOrganismo { get; set; }
        public int? NoEmpleado { get; set; }
        public string? TxtUrlFoto { get; set; }
        public string? TxtNombreFoto { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string? ObservacionBaja { get; set; }
        public int? IdRol { get; set; }
        public string? TxtCorreo { get; set; }
        public string? TxtTelefono { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public int? UsuarioActualiza { get; set; }
        public bool? SnActivo { get; set; }

        public virtual Organismo? IdOrganismoNavigation { get; set; }
        public virtual CteCatRol? IdRolNavigation { get; set; }
        public virtual CteCatUsuario? UsuarioActualizaNavigation { get; set; }
        public virtual ICollection<CteCatArea> CteCatAreas { get; set; }
        public virtual ICollection<CteCatDocumentoIngreso> CteCatDocumentoIngresos { get; set; }
        public virtual ICollection<CteCatEstatus> CteCatEstatuses { get; set; }
        public virtual ICollection<CteCatIdentificacionResidente> CteCatIdentificacionResidentes { get; set; }
        public virtual ICollection<CteCatPension> CteCatPensions { get; set; }
        public virtual ICollection<CteCatServicioSalud> CteCatServicioSaluds { get; set; }
        public virtual ICollection<CteCtrlDocumentoIngresoResidente> CteCtrlDocumentoIngresoResidentes { get; set; }
        public virtual ICollection<CteCtrlNotaResidente> CteCtrlNotaResidentes { get; set; }
        public virtual ICollection<CteCtrlPensionResidente> CteCtrlPensionResidentes { get; set; }
        public virtual ICollection<CteCtrlResponsableResidente> CteCtrlResponsableResidentes { get; set; }
        public virtual ICollection<CteFichaIdentificacionResidente> CteFichaIdentificacionResidentes { get; set; }
        public virtual ICollection<CteHistorialIngresoResidente> CteHistorialIngresoResidentes { get; set; }
        public virtual ICollection<CteCatUsuario> InverseUsuarioActualizaNavigation { get; set; }
    }
}
