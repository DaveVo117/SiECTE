using SiECTE.AplicacionWeb.Models.ViewModels;
using SiECTE.Entity;
using System.Globalization;
using AutoMapper;


namespace SiECTE.AplicacionWeb.Utilidades.Automapper
{
    public class AutoMapperProfile:Profile //<Referencia de AutoMapper
    {
        /*En esta clase se hacen als conversiones de cada dato que no coincide entre el Model y el ViewModel*/
        public AutoMapperProfile()
        {
            #region ROL

            CreateMap<CteCatRol, VMRol>().ReverseMap(); //Convierte una clase en otra y viceversa

            #endregion

            #region USUARIO

            CreateMap<CteCatUsuario, VMUsuario>()
                .ForMember(destino =>
                destino.SnActivo,
                opt => opt.MapFrom(origen => origen.SnActivo == true ? 1 : 0)  //esta instruccion convierte el booleano a entero 1,0
                )

                .ForMember(destino =>
                destino.TxtOrganismo,
                opt => opt.MapFrom(origen => origen.IdOrganismoNavigation.TxtNombre)  //Convierte la descripcion del Organismo en el nombre de Organismo de destino
                )

                .ForMember(destino =>
                destino.TxtRol,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.TxtRol)  //Convierte la descripcion del Rol en el nombre de rol de destino
                );

            //Mapeo Inverso
            CreateMap<VMUsuario, CteCatUsuario>()
                .ForMember(destino =>
                destino.SnActivo,
                opt => opt.MapFrom(origen => origen.SnActivo == 1 ? true : false)
                )

                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore()
                );


            #endregion

            #region Organismo

            //CreateMap<Organismo,VMOrganismo>().ReverseMap();

            CreateMap<Organismo, VMOrganismo>()
                .ForMember(destino =>
                destino.SnActivo,
              opt => opt.MapFrom(origen => origen.SnActivo == true ? 1 : 0)  //esta instruccion convierte el booleano a entero 1,0
              );


            //mapeoInverso
            CreateMap<VMOrganismo, Organismo>()
            .ForMember(destino =>
            destino.SnActivo,
                opt => opt.MapFrom(origen => origen.SnActivo == 1 ? true : false)
                );

            #endregion

            #region CATEGORIA

            CreateMap<CteCatDocumentoIngreso, VMDocumento>()
           .ForMember(destino =>
           destino.SnActivo,
           opt => opt.MapFrom(origen => origen.SnActivo == true ? 1:0)
           );

            //mapeoInverso
            CreateMap<VMDocumento, CteCatDocumentoIngreso>()
           .ForMember(destino =>
           destino.SnActivo,
           opt => opt.MapFrom(origen => origen.SnActivo == 1 ? true : false)
           );

            #endregion

            #region PRODUCTO

            CreateMap<CteFichaIdentificacionResidente, VMResidente>()
                .ForMember(destino =>
                destino.IdEstatus,
                opt => opt.MapFrom(origen => origen.IdEstatus == 1 ? 1 : 0)//MODIFICAR
                )

                //.ForMember(destino=>
                //destino.TxtNombreCategoria,
                //opt=>opt.MapFrom(origen=> origen.IdEstatusNavigation.TxtEstatus)
                //)

                //.ForMember(destino =>
                //destino.Precio,
                //opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value,new CultureInfo("es-MX")))
                //)
                ;

            //Mapeo Inverso
            CreateMap<VMResidente, CteFichaIdentificacionResidente>()
                .ForMember(destino =>
                destino.IdEstatus,
                opt => opt.MapFrom(origen => origen.IdEstatus == 1 ? 1 : 0)
                )

                .ForMember(destino =>
                destino.IdEstatusNavigation,
                opt => opt.Ignore()
                )

                //.ForMember(destino =>
                //destino.Precio,
                //opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-MX")))
                //)
                ;

            #endregion

            #region TIPODOCUMENTOVENTA

            //CreateMap<TipoDocumentoVenta, VMTipoDocumentoVenta>().ReverseMap();

            #endregion

            #region VENTA

            //CreateMap<Venta, VMVenta>()
            //    .ForMember(destino =>
            //    destino.TxtTipoDocumentoVenta,
            //    opt => opt.MapFrom(origen => origen.IdTipoDocumentoVentaNavigation.TxtDescripcion )
            //    )

            //    .ForMember(destino =>
            //    destino.TxtUsuario,
            //    opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.TxtNombre)
            //    )

            //    .ForMember(destino =>
            //    destino.SubTotal,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.SubTotal.Value, new CultureInfo("es-MX")))
            //    )

            //    .ForMember(destino =>
            //    destino.ImpuestoTotal,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-MX")))
            //    )

            //    .ForMember(destino =>
            //    destino.Total,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-MX")))
            //    )

            //    .ForMember(destino =>
            //    destino.FechaRegistro,
            //    opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            //    );


            //Mapeo Inverso
            //CreateMap<VMVenta, Venta>()
            //    .ForMember(destino =>
            //    destino.SubTotal,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.SubTotal, new CultureInfo("es-MX")))
            //    )

            //    .ForMember(destino =>
            //    destino.ImpuestoTotal,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImpuestoTotal, new CultureInfo("es-MX")))
            //    )

            //     .ForMember(destino =>
            //    destino.Total,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-MX")))
            //    );
            #endregion

            #region DETALLEVENTA

            //CreateMap<DetalleVenta, VMDetalleVenta>()
            //    .ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-MX")))
            //    )


            //    .ForMember(destino=>
            //    destino.Total,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-MX")))
            //    );

            ////Mapeo Inverso
            //CreateMap<VMDetalleVenta, DetalleVenta>()
            //    .ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-MX")))
            //    )


            //    .ForMember(destino =>
            //    destino.Total,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-MX")))
            //    );
            #endregion

            #region REPORTEVENTA

            //CreateMap<DetalleVenta, VMReporteVenta>()
            //                  .ForMember(destino =>
            //  destino.FechaRegistro,
            //  opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            //  )

            //  .ForMember(destino =>
            //  destino.TxtNumeroVenta,
            //  opt => opt.MapFrom(origen => origen.IdVentaNavigation.TxtNumeroVenta)
            //  )

            //  .ForMember(destino =>
            //  destino.TxtTipoDocumento,
            //  opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdTipoDocumentoVentaNavigation.TxtDescripcion)
            //  )

            //  .ForMember(destino =>
            //  destino.TxtDocumentoCliente,
            //  opt => opt.MapFrom(origen => origen.IdVentaNavigation.TxtDocumentoCliente)
            //  )

            //  .ForMember(destino =>
            //  destino.TxtNombreCliente,
            //  opt => opt.MapFrom(origen => origen.IdVentaNavigation.TxtNombreCliente)
            //  )

            //  .ForMember(destino =>
            //  destino.SubTotalVenta,
            //  opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.SubTotal.Value, new CultureInfo("es-MX")))
            //  )

            //  .ForMember(destino =>
            //  destino.ImpuestoTotalVenta,
            //  opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.ImpuestoTotal.Value, new CultureInfo("es-MX")))
            //  )

            //  .ForMember(destino =>
            //  destino.TotalVenta,
            //  opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-MX")))
            //  )

            //  .ForMember(destino =>
            //  destino.Producto,
            //  opt => opt.MapFrom(origen => origen.TxtDescripcionProducto)
            //  )

            //  .ForMember(destino =>
            //  destino.Precio,
            //  opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-MX")))
            //  )

            //  .ForMember(destino =>
            //  destino.Total,
            //  opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-MX")))
            //  );

            #endregion

            #region MENU

            CreateMap<Menu, VMMenu>()
                              .ForMember(destino =>
              destino.SubMenus,
              opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation)
              );

            #endregion
        }
        
    }
}
