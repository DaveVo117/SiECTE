using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SiECTE.BLL.Interfaces;
using SiECTE.DAL.Interfaces;
using SiECTE.Entity;
using SiECTE.Entity;

namespace SiECTE.BLL.Implementacion
{
    public class ResidenteService : IResidenteService
    {
        /*ATRIBUTOS*/
        private readonly IGenericRepository <CteFichaIdentificacionResidente> _repositorio;
        private readonly IFireBaseService _firebaseService;


        public ResidenteService(IGenericRepository<CteFichaIdentificacionResidente> repositorio, IFireBaseService firebaseService, IUtilidadesService utilidadesService)//CONSTRUCTOR
        {
            _repositorio = repositorio;
            _firebaseService = firebaseService;
        }



        /*METODOS*/
        public async Task<List<CteFichaIdentificacionResidente>> Lista()
        {
            IQueryable<CteFichaIdentificacionResidente> query = await _repositorio.Consultar();
            //return query.Include(c=>c.IdCategoriaNavigation).ToList();
            return query.ToList();
        }






        public async Task<CteFichaIdentificacionResidente> Crear(CteFichaIdentificacionResidente entidad, Stream imagen = null, string nombreImagen = "")
        {
            CteFichaIdentificacionResidente Residente_existe = await _repositorio.Obtener(p => p.IdResidente == entidad.IdResidente);

            if (Residente_existe != null)
                throw new TaskCanceledException("El Residente ya existe");

            try
            {
                entidad.TxtFotografia = nombreImagen;

                if (nombreImagen!=null)
                {
                    string urlImagen = await _firebaseService.SubirStorage(imagen, "carpeta_residente", nombreImagen);
                    entidad.TxtUrlFotografia= urlImagen;
                }

                CteFichaIdentificacionResidente residente_creado = await _repositorio.Crear(entidad);

                if (residente_creado.IdResidente==0)
                    throw new TaskCanceledException("No se pudo crear el producto");

                IQueryable<CteFichaIdentificacionResidente> query = await _repositorio.Consultar(p => p.IdResidente == residente_creado.IdResidente);

                //residente_creado = query.Include(c=>c.IdCategoriaNavigation).First();
                residente_creado = query.Include(c=>c.IdEstatus).First();

                return residente_creado;
            }
            catch (Exception ex)
            {

                throw;
            }
        }






        public async Task<CteFichaIdentificacionResidente> Editar(CteFichaIdentificacionResidente entidad, Stream imagen = null, string nombreImagen = "")
        {
            CteFichaIdentificacionResidente producto_existe = await _repositorio.Obtener(p=>p.IdResidente == entidad.IdResidente && p.IdBeneficiario != entidad.IdBeneficiario);

            if(producto_existe != null)
                throw new TaskCanceledException("El código de barra ya existe");

            try
            {
                IQueryable<CteFichaIdentificacionResidente> queryResidente = await _repositorio.Consultar(p => p.IdResidente == entidad.IdResidente);

                CteFichaIdentificacionResidente Residente_para_editar = queryResidente.First();

                //Residente_para_editar.TxtCodigoBarra = entidad.TxtCodigoBarra;
                //Residente_para_editar.TxtMarca = entidad.TxtMarca;
                //Residente_para_editar.TxtDescripcion = entidad.TxtDescripcion;
                //Residente_para_editar.IdCategoria= entidad.IdCategoria;
                //Residente_para_editar.Stock= entidad.Stock;
                //Residente_para_editar.Precio = entidad.Precio;
                //Residente_para_editar.SnActivo = entidad.SnActivo;

                if (Residente_para_editar.TxtFotografia == "")
                    Residente_para_editar.TxtFotografia = nombreImagen;

                if (imagen != null)
                {
                    string urlImagen = await _firebaseService.SubirStorage(imagen, "carpeta_residente", Residente_para_editar.TxtFotografia);
                    Residente_para_editar.TxtUrlFotografia= urlImagen;
                }

                bool respuesta = await _repositorio.Editar(Residente_para_editar);

                if(!respuesta)
                    throw new TaskCanceledException("No se pudo editar el Residente");

                CteFichaIdentificacionResidente Residente_editado = queryResidente.Include(c => c.IdEstatus).First();

                return Residente_editado;

            }
            catch 
            {
                throw;
            }
        }







        public async Task<bool> Eliminar(int IdResidente)
        {
            try
            {
                CteFichaIdentificacionResidente Residente_encontrado = await _repositorio.Obtener(p => p.IdResidente == IdResidente);

                if(Residente_encontrado==null)
                    throw new TaskCanceledException("El Residente no existe");

                string nombreImagen = Residente_encontrado.TxtFotografia;
                bool respuesta = await _repositorio.Eliminar(Residente_encontrado);

                if (respuesta)
                    await _firebaseService.EliminarStorage("carpeta_Residente", nombreImagen);

                return true;

            }
            catch 
            {
                return false;
                throw;
            }
        }


    }
}
