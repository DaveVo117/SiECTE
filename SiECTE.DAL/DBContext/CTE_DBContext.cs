using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SiECTE.Entity;

namespace SiECTE.DAL.DBContext
{
    public partial class CTE_DBContext : DbContext
    {
        public CTE_DBContext()
        {
        }

        public CTE_DBContext(DbContextOptions<CTE_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Configuracion> Configuracion { get; set; } = null!;
        public virtual DbSet<CteCatArea> CteCatArea { get; set; } = null!;
        public virtual DbSet<CteCatDocumentoIngreso> CteCatDocumentoIngreso { get; set; } = null!;
        public virtual DbSet<CteCatEstatus> CteCatEstatuse { get; set; } = null!;
        public virtual DbSet<CteCatIdentificacionResidente> CteCatIdentificacionResidente { get; set; } = null!;
        public virtual DbSet<CteCatPension> CteCatPension { get; set; } = null!;
        public virtual DbSet<CteCatRol> CteCatRols { get; set; } = null!;
        public virtual DbSet<CteCatServicioSalud> CteCatServicioSalud { get; set; } = null!;
        public virtual DbSet<CteCatTipoNota> CteCatTipoNota { get; set; } = null!;
        public virtual DbSet<CteCatUsuario> CteCatUsuario { get; set; } = null!;
        public virtual DbSet<CteCtrlDocumentoIngresoResidente> CteCtrlDocumentoIngresoResidente { get; set; } = null!;
        public virtual DbSet<CteCtrlNotaResidente> CteCtrlNotaResidente { get; set; } = null!;
        public virtual DbSet<CteCtrlPensionResidente> CteCtrlPensionResidente { get; set; } = null!;
        public virtual DbSet<CteCtrlResponsableResidente> CteCtrlResponsableResidente { get; set; } = null!;
        public virtual DbSet<CteFichaIdentificacionResidente> CteFichaIdentificacionResidente { get; set; } = null!;
        public virtual DbSet<CteHistorialIngresoResidente> CteHistorialIngresoResidente { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Organismo> Organismos { get; set; } = null!;
        public virtual DbSet<RolMenu> RolMenus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local); Database=CTE_DB; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Configuracion");

                entity.Property(e => e.TxtPropiedad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Propiedad");

                entity.Property(e => e.TxtRecurso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Recurso");

                entity.Property(e => e.TxtValor)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Valor");
            });

            modelBuilder.Entity<CteCatArea>(entity =>
            {
                entity.HasKey(e => e.IdArea);

                entity.ToTable("CTE_Cat_Area");

                entity.Property(e => e.IdArea).HasColumnName("ID_Area");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.IdAreaDependenciaDirecta).HasColumnName("ID_Area_Dependencia_Directa");

                entity.Property(e => e.TxtArea)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Area");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCatAreas)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Cat_Area_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCatDocumentoIngreso>(entity =>
            {
                entity.HasKey(e => e.IdDocumentoIngreso);

                entity.ToTable("CTE_Cat_DocumentoIngreso");

                entity.Property(e => e.IdDocumentoIngreso).HasColumnName("ID_DocumentoIngreso");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");

                entity.Property(e => e.TxtDocumentoIngreso)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_DocumentoIngreso");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCatDocumentoIngresos)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Cat_DocumentoIngreso_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCatEstatus>(entity =>
            {
                entity.HasKey(e => e.IdEstatus)
                    .HasName("PK_CTE_Cat_Estatus_Residente");

                entity.ToTable("CTE_Cat_Estatus");

                entity.Property(e => e.IdEstatus)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Estatus");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.TxtEstatus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Estatus");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCatEstatuses)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Cat_Estatus_CTE_Cat_Usuario1");
            });

            modelBuilder.Entity<CteCatIdentificacionResidente>(entity =>
            {
                entity.HasKey(e => e.IdIdentificacion);

                entity.ToTable("CTE_Cat_Identificacion_Residente");

                entity.Property(e => e.IdIdentificacion).HasColumnName("ID_Identificacion");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.TxtIdentificacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Identificacion");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCatIdentificacionResidentes)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Cat_Identificacion_Residente_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCatPension>(entity =>
            {
                entity.HasKey(e => e.IdPension);

                entity.ToTable("CTE_Cat_Pension");

                entity.Property(e => e.IdPension).HasColumnName("ID_Pension");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.TxtPension)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Pension");

                entity.Property(e => e.UsuarioActuliza).HasColumnName("Usuario_Actuliza");

                entity.HasOne(d => d.UsuarioActulizaNavigation)
                    .WithMany(p => p.CteCatPensions)
                    .HasForeignKey(d => d.UsuarioActuliza)
                    .HasConstraintName("FK_CTE_Cat_Pension_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCatRol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__202AD2201FE6B74B");

                entity.ToTable("CTE_Cat_Rol");

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtRol)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Rol");
            });

            modelBuilder.Entity<CteCatServicioSalud>(entity =>
            {
                entity.HasKey(e => e.IdServicioSalud);

                entity.ToTable("CTE_Cat_ServicioSalud");

                entity.Property(e => e.IdServicioSalud).HasColumnName("ID_ServicioSalud");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.TxtServicioSalud)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_ServicioSalud");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCatServicioSaluds)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Cat_ServicioSalud_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCatTipoNota>(entity =>
            {
                entity.HasKey(e => e.IdTipoNota);

                entity.ToTable("CTE_Cat_TipoNota");

                entity.Property(e => e.IdTipoNota)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Tipo_Nota");

                entity.Property(e => e.TxtDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");

                entity.Property(e => e.TxtTipoNota)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_TipoNota");
            });

            modelBuilder.Entity<CteCatUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("CTE_Cat_Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CveUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Cve_Usuario");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Alta");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Baja");

                entity.Property(e => e.IdArea).HasColumnName("ID_Area");

                entity.Property(e => e.IdOrganismo).HasColumnName("ID_Organismo");

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.NoEmpleado).HasColumnName("No_Empleado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Completo");

                entity.Property(e => e.ObservacionBaja)
                    .IsUnicode(false)
                    .HasColumnName("Observacion_Baja");

                entity.Property(e => e.PassUsuario)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Pass_Usuario");

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Primer_Apellido");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Segundo_Apellido");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtCorreo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Correo");

                entity.Property(e => e.TxtNombreFoto)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre_Foto");

                entity.Property(e => e.TxtTelefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Telefono");

                entity.Property(e => e.TxtUrlFoto)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Txt_UrlFoto");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.IdOrganismoNavigation)
                    .WithMany(p => p.CteCatUsuarios)
                    .HasForeignKey(d => d.IdOrganismo)
                    .HasConstraintName("FK_CTE_Cat_Usuario_Organismo");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.CteCatUsuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_CTE_Cat_Usuario_CTE_Cat_Rol");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.InverseUsuarioActualizaNavigation)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Cat_Usuario_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCtrlDocumentoIngresoResidente>(entity =>
            {
                entity.HasKey(e => e.IdCtrlDocumentoIngresoResidente);

                entity.ToTable("CTE_Ctrl_DocumentoIngreso_Residente");

                entity.Property(e => e.IdCtrlDocumentoIngresoResidente).HasColumnName("ID_Ctrl_DocumentoIngreso_Residente");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.IdDocumento).HasColumnName("ID_Documento");

                entity.Property(e => e.IdResidente).HasColumnName("ID_Residente");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.CteCtrlDocumentoIngresoResidentes)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTE_Ctrl_DocumentoIngreso_Residente_CTE_Cat_DocumentoIngreso");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.CteCtrlDocumentoIngresoResidentes)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTE_Ctrl_DocumentoIngreso_Residente_CTE_FichaIdentificacion_Residente");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCtrlDocumentoIngresoResidentes)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Ctrl_DocumentoIngreso_Residente_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCtrlNotaResidente>(entity =>
            {
                entity.HasKey(e => e.IdNotaResidente)
                    .HasName("PK_CTE_InformacionMedica_Residente");

                entity.ToTable("CTE_Ctrl_Nota_Residente");

                entity.Property(e => e.IdNotaResidente).HasColumnName("ID_Nota_Residente");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.IdResidente).HasColumnName("ID_Residente");

                entity.Property(e => e.IdServicioSalud).HasColumnName("ID_ServicioSalud");

                entity.Property(e => e.IdTipoNota).HasColumnName("ID_TipoNota");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtNota)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nota");

                entity.Property(e => e.TxtOtroServicioSalud)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Otro_ServicioSalud");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.CteCtrlNotaResidentes)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTE_InformacionMedica_Residente_CTE_Ficha_Identificacion_Residente");

                entity.HasOne(d => d.IdServicioSaludNavigation)
                    .WithMany(p => p.CteCtrlNotaResidentes)
                    .HasForeignKey(d => d.IdServicioSalud)
                    .HasConstraintName("FK_CTE_InformacionMedica_Residente_CTE_Cat_ServicioSalud");

                entity.HasOne(d => d.IdTipoNotaNavigation)
                    .WithMany(p => p.CteCtrlNotaResidentes)
                    .HasForeignKey(d => d.IdTipoNota)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTE_Ctrl_Nota_Residente_CTE_Cat_TipoNota");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCtrlNotaResidentes)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Ctrl_Nota_Residente_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCtrlPensionResidente>(entity =>
            {
                entity.HasKey(e => e.IdCtrlPension);

                entity.ToTable("CTE_Ctrl_Pension_Residente");

                entity.Property(e => e.IdCtrlPension).HasColumnName("ID_Ctrl_Pension");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.IdPension).HasColumnName("ID_Pension");

                entity.Property(e => e.IdResidente).HasColumnName("ID_Residente");

                entity.Property(e => e.MontoPension)
                    .HasColumnType("money")
                    .HasColumnName("Monto_Pension");

                entity.Property(e => e.Observaciones).IsUnicode(false);

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.IdPensionNavigation)
                    .WithMany(p => p.CteCtrlPensionResidentes)
                    .HasForeignKey(d => d.IdPension)
                    .HasConstraintName("FK_CTE_Ctrl_Pension_Residente_CTE_Cat_Pension");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.CteCtrlPensionResidentes)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTE_Ctrl_Pension_Residente_CTE_Ficha_Identificacion_Residente");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCtrlPensionResidentes)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Ctrl_Pension_Residente_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteCtrlResponsableResidente>(entity =>
            {
                entity.HasKey(e => e.IdCtrlResponsable);

                entity.ToTable("CTE_Ctrl_Responsable_Residente");

                entity.Property(e => e.IdCtrlResponsable).HasColumnName("ID_Ctrl_Responsable");

                entity.Property(e => e.Domicilio).IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.IdIdentidicacion).HasColumnName("ID_Identidicacion");

                entity.Property(e => e.IdResidente).HasColumnName("ID_Residente");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Completo");

                entity.Property(e => e.NumeroIdentificacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Numero_Identificacion");

                entity.Property(e => e.OrdenImportancia).HasColumnName("Orden_Importancia");

                entity.Property(e => e.Parentesco)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Primer_Apellido");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Segundo_Apellido");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.IdIdentidicacionNavigation)
                    .WithMany(p => p.CteCtrlResponsableResidentes)
                    .HasForeignKey(d => d.IdIdentidicacion)
                    .HasConstraintName("FK_CTE_Ctrl_Responsable_Residente_CTE_Cat_Identificacion_Residente");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.CteCtrlResponsableResidentes)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTE_Ctrl_Responsable_Residente_CTE_Ficha_Identificacion_Residente");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteCtrlResponsableResidentes)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Ctrl_Responsable_Residente_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteFichaIdentificacionResidente>(entity =>
            {
                entity.HasKey(e => e.IdResidente)
                    .HasName("PK_CTE_Ficha_Identificacion_Residente");

                entity.ToTable("CTE_FichaIdentificacion_Residente");

                entity.Property(e => e.IdResidente).HasColumnName("ID_Residente");

                entity.Property(e => e.DependenciaEmite)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Dependencia_Emite");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.IdBeneficiario).HasColumnName("ID_Beneficiario");

                entity.Property(e => e.IdEstatus).HasColumnName("ID_Estatus");

                entity.Property(e => e.IdOrganismo).HasColumnName("ID_Organismo");

                entity.Property(e => e.Religion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TxtFotografia)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Fotografia");

                entity.Property(e => e.TxtUrlFotografia)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Txt_UrlFotografia");

                entity.Property(e => e.UltimaCuota)
                    .HasColumnType("money")
                    .HasColumnName("Ultima_Cuota");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.IdEstatusNavigation)
                    .WithMany(p => p.CteFichaIdentificacionResidentes)
                    .HasForeignKey(d => d.IdEstatus)
                    .HasConstraintName("FK_CTE_Ficha_Identificacion_Residente_CTE_Cat_Estatus");

                entity.HasOne(d => d.IdOrganismoNavigation)
                    .WithMany(p => p.CteFichaIdentificacionResidentes)
                    .HasForeignKey(d => d.IdOrganismo)
                    .HasConstraintName("FK_CTE_FichaIdentificacion_Residente_Organismo");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteFichaIdentificacionResidentes)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_Ficha_Identificacion_Residente_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<CteHistorialIngresoResidente>(entity =>
            {
                entity.HasKey(e => e.IdHistorialIngreso);

                entity.ToTable("CTE_HistorialIngreso_Residente");

                entity.Property(e => e.IdHistorialIngreso).HasColumnName("ID_HistorialIngreso");

                entity.Property(e => e.FechaActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualiza");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Ingreso");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Salida");

                entity.Property(e => e.IdResidente).HasColumnName("ID_Residente");

                entity.Property(e => e.ObservacionesSalida)
                    .IsUnicode(false)
                    .HasColumnName("Observaciones_Salida");

                entity.Property(e => e.UsuarioActualiza).HasColumnName("Usuario_Actualiza");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.CteHistorialIngresoResidentes)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTE_HistorialIngreso_Residente_CTE_FichaIdentificacion_Residente");

                entity.HasOne(d => d.UsuarioActualizaNavigation)
                    .WithMany(p => p.CteHistorialIngresoResidentes)
                    .HasForeignKey(d => d.UsuarioActualiza)
                    .HasConstraintName("FK_CTE_HistorialIngreso_Residente_CTE_Cat_Usuario");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__7F28421ACF40E547");

                entity.ToTable("Menu");

                entity.Property(e => e.IdMenu).HasColumnName("ID_Menu");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro");

                entity.Property(e => e.IdMenuPadre).HasColumnName("ID_Menu_Padre");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtControlador)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Controlador");

                entity.Property(e => e.TxtDescripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");

                entity.Property(e => e.TxtIcono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Icono");

                entity.Property(e => e.TxtPaginaAccion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Pagina_Accion");

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("FK_Menu_Menu");
            });

            modelBuilder.Entity<Organismo>(entity =>
            {
                entity.HasKey(e => e.IdOrganismo)
                    .HasName("PK__Negocio__93197F13ECEDD59D");

                entity.ToTable("Organismo");

                entity.Property(e => e.IdOrganismo).HasColumnName("ID_Organismo");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtCargoTitular)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Cargo_Titular");

                entity.Property(e => e.TxtCorreo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Correo");

                entity.Property(e => e.TxtDireccion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Direccion");

                entity.Property(e => e.TxtNombre)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre");

                entity.Property(e => e.TxtNombreLogo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre_Logo");

                entity.Property(e => e.TxtSiglas)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Siglas");

                entity.Property(e => e.TxtTelefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Telefono");

                entity.Property(e => e.TxtTitular)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Titular");

                entity.Property(e => e.TxtUrlLogo)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Url_Logo");
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.IdRolMenu)
                    .HasName("PK__RolMenu__26D7077BDCB5D325");

                entity.ToTable("RolMenu");

                entity.Property(e => e.IdRolMenu).HasColumnName("ID_Rol_Menu");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro");

                entity.Property(e => e.IdMenu).HasColumnName("ID_Menu");

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK_RolMenu_Menu");

                entity.HasOne(d => d.IdMenu1)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK_RolMenu_CTE_Cat_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
