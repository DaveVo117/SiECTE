using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SiECTE.DAL.DBContext;
using SiECTE.DAL.Implementacion;
using SiECTE.DAL.Interfaces;
using SiECTE.BLL.Implementacion;
using SiECTE.BLL.Interfaces;

namespace SiECTE.IOC
{
    public static class Dependencia
    {

        public static void InyectarDependencia(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<CTE_DBContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
            });

            //contenedor genérico para cualquier entidad 
            Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //contenedor para HistorialRepository
            Services.AddScoped<IHistorialRepository ,HistorialRepository>();

            //Servicio de envío de correo
            Services.AddScoped<ICorreoService, CorreoService>();
            //Servicio de almacenamiento multimedia
            Services.AddScoped<IFireBaseService, FireBaseService>();
            //Servicio de Utilidades
            Services.AddScoped<IUtilidadesService, UtilidadesService>();
            //Servicio de Roles
            Services.AddScoped<IRolService, RolService>();
            //Servicio de registro y modificación de Usuario
            Services.AddScoped<IUsuarioService, UsuarioService>();
            //Servicio de registro y modificación de Informacion de Organismo
            Services.AddScoped<IOrganismoService, OrganismoService>();
            //Servicio de registro y modificación de Documentos de Ingreso
            Services.AddScoped<IDocumentoService, DocumentoService>();
            //Servicio de registro de Producto
            Services.AddScoped<IResidenteService, ResidenteService>();
            //Servicio de registro de TipoDocumento
            Services.AddScoped<ITipoDocumentoIngresoService, TipoDocumentoIngresoService>();
            //Servicio de registro de Ingresos
            Services.AddScoped<IHistorialIngresoService, HistorialIngresoService>();
            //Servicio de DashBoard
            Services.AddScoped<IDashBoardService, DashBoardService>();
            //Servicio de CatTipoNota
            Services.AddScoped<ITipoNotaService, TipoNotaService>();
        }


    }
}
