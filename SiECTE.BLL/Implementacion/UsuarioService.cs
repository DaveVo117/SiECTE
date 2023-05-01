using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using SiECTE.BLL.Interfaces;
using SiECTE.DAL.Interfaces;
using SiECTE.Entity;

namespace SiECTE.BLL.Implementacion
{
    public class UsuarioService : IUsuarioService
    {

#region ATRIBUTOS

        private readonly IGenericRepository<CteCatUsuario> _repositorio;
        private readonly IFireBaseService _fireBaseService;
        private readonly IUtilidadesService _utilidadesService;
        private readonly ICorreoService _correoService;

        //constructor
        public UsuarioService(IGenericRepository<CteCatUsuario> repositorio, IFireBaseService fireBaseService, IUtilidadesService utilidadesService, ICorreoService correoService)
        {
            _repositorio = repositorio;
            _fireBaseService = fireBaseService;
            _utilidadesService = utilidadesService;
            _correoService = correoService;
        }

        #endregion

#region METODOS
        
        public async Task<List<CteCatUsuario>> Lista()//consultar
        {
            IQueryable<CteCatUsuario> query = await _repositorio.Consultar();
            return query.Include(rol => rol.IdRolNavigation).ToList();
        }



        public async Task<CteCatUsuario> Crear(CteCatUsuario entidad, Stream foto = null, string nombreFoto = "", string urlPlantillaCorreo = "")
        {
            CteCatUsuario usuario_existe = await _repositorio.Obtener(u => u.TxtCorreo == entidad.TxtCorreo);
            if (usuario_existe != null)
                throw new TaskCanceledException("El usuario ya existe");

            try
            {
                string clave_generada = _utilidadesService.GenerarClave();
                entidad.PassUsuario = _utilidadesService.ConvertirSha256(clave_generada);
                entidad.TxtNombreFoto = nombreFoto;

                if (foto!= null)
                {
                    string urlFoto = await _fireBaseService.SubirStorage(foto, "carpeta_usuario", nombreFoto);
                    entidad.TxtUrlFoto= urlFoto;
                }

                CteCatUsuario usuario_creado = await _repositorio.Crear(entidad);

                if (usuario_creado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                if (urlPlantillaCorreo != "")//envio de correo
                { 
                    urlPlantillaCorreo = urlPlantillaCorreo.Replace("[correo]", usuario_creado.TxtCorreo).Replace("[clave]", clave_generada);

                    string htmlCorreo = "";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                          StreamReader readerStream = null;

                             if (response.CharacterSet == null)
                              readerStream= new StreamReader(dataStream);
                             else
                              readerStream= new StreamReader(dataStream,Encoding.GetEncoding(response.CharacterSet));

                             htmlCorreo= readerStream.ReadToEnd();
                             response.Close();
                              readerStream.Close();
                        }
                    }

                if (htmlCorreo != "")//validación y envío de correo
                  await _correoService.EnviarCorreo(usuario_creado.TxtCorreo, "Cuenta Creada", htmlCorreo);
                }

            IQueryable<CteCatUsuario> query = await _repositorio.Consultar(u=>u.IdUsuario == usuario_creado.IdUsuario);
            usuario_creado= query.Include(r=>r.IdRolNavigation).First();

        return usuario_creado;

            }
            catch (Exception ex)
            {

                throw;
            }
 
        }



        public async Task<CteCatUsuario> Editar(CteCatUsuario entidad, Stream foto = null, string nombreFoto = "")
        {
            CteCatUsuario usuario_existe = await _repositorio.Obtener(u => u.TxtCorreo == entidad.TxtCorreo && u.IdUsuario != entidad.IdUsuario);
          if (usuario_existe != null)
            throw new TaskCanceledException("El usuario ya existe");

          try
          {
            IQueryable<CteCatUsuario> queryUsuario = await _repositorio.Consultar(u=>u.IdUsuario==entidad.IdUsuario);

                CteCatUsuario usuario_editar = queryUsuario.First();

            usuario_editar.Nombre= entidad.Nombre;
            usuario_editar.TxtCorreo= entidad.TxtCorreo;
            usuario_editar.TxtTelefono= entidad.TxtTelefono;
            usuario_editar.IdRol= entidad.IdRol;
            usuario_editar.IdOrganismo = entidad.IdOrganismo;
            usuario_editar.SnActivo= entidad.SnActivo;

            if (usuario_editar.TxtNombreFoto == "")
              usuario_editar.TxtNombreFoto = nombreFoto;

            if (foto != null)
            {
              string urlFoto = await _fireBaseService.SubirStorage(foto, "carpeta_usuario", usuario_editar.TxtNombreFoto);
              usuario_editar.TxtUrlFoto= urlFoto;
            }

            bool respuesta= await _repositorio.Editar(usuario_editar);

            if(!respuesta) //si la respuesta es falsa
              throw new TaskCanceledException("No se pudo modificar el usuario");

                CteCatUsuario usuario_editado = queryUsuario.Include(r=>r.IdRolNavigation).First();

        return usuario_editado;
          }
          catch 
          {
         
            throw;
          }

        }



        public async Task<bool> Eliminar(int idUsuario)
        {
            try
            {
                CteCatUsuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == idUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                string nombreFoto = usuario_encontrado.TxtNombreFoto;
                bool respuesta = await _repositorio.Eliminar(usuario_encontrado);

                if (respuesta)
                    await _fireBaseService.EliminarStorage("carpeta_usuario", nombreFoto);

                return true;
            }
            catch 
            {

              throw;
            }
        }



        public async Task<CteCatUsuario> ObtenerPorCredenciales(string? correo, string? clave)
        {
            string clave_encriptada = _utilidadesService.ConvertirSha256(clave);

            CteCatUsuario usuario_encontrado = await _repositorio.ObtenerUsuario(u=>u.TxtCorreo.Equals(correo) && u.PassUsuario.Equals(clave_encriptada));

            return usuario_encontrado;
        }



        public async Task<CteCatUsuario> ObtenerPorId(int idUsuario)
        {
            IQueryable<CteCatUsuario> query = await _repositorio.Consultar(u => u.IdUsuario == idUsuario);

            CteCatUsuario resultado = query.Include(rol => rol.IdRolNavigation).FirstOrDefault();

            return resultado;
        }



        public async Task<bool> GuardarPerfil(CteCatUsuario entidad)
        {
            try
            {

                CteCatUsuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == entidad.IdUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                usuario_encontrado.TxtCorreo = entidad.TxtCorreo;
                usuario_encontrado.TxtTelefono= entidad.TxtTelefono;

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;
            }
            catch
            {

                throw;
            }
        }



        public async Task<bool> CambiarClave(int idUsuario, string claveActual, string claveNueva)
        {
            try
            {
                CteCatUsuario usuario_encontrado = await _repositorio.Obtener(u=>u.IdUsuario == idUsuario);

                if (usuario_encontrado == null)
                    throw new TaskCanceledException("El usuario no esiste");

                if(usuario_encontrado.PassUsuario != _utilidadesService.ConvertirSha256(claveActual))
                    throw new TaskCanceledException("La contraseña ingresada como actual no es correcta");

                usuario_encontrado.PassUsuario = _utilidadesService.ConvertirSha256(claveNueva);

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        


        public async Task<bool> RestablecerClave(string correo, string urlPlantillaCorreo)
        {

            try
            {

                CteCatUsuario usuario_encontardo = await _repositorio.Obtener(u=>u.TxtCorreo == correo);

                if (usuario_encontardo == null)
                    throw new TaskCanceledException("No se encuentra ningún usuario asociado al correo");

                string clave_generada = _utilidadesService.GenerarClave();
                usuario_encontardo.PassUsuario = _utilidadesService.ConvertirSha256(clave_generada);

                //lógica de correo

                urlPlantillaCorreo = urlPlantillaCorreo.Replace("[clave]", clave_generada);

                string htmlCorreo = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPlantillaCorreo);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader readerStream = null;

                        if (response.CharacterSet == null)
                            readerStream = new StreamReader(dataStream);
                        else
                            readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                        htmlCorreo = readerStream.ReadToEnd();
                        response.Close();
                        readerStream.Close();
                    }
                }

                bool correo_enviado = false;

                if (htmlCorreo != "")//validación y envío de correo
                   correo_enviado =  await _correoService.EnviarCorreo(correo, "Contraseña Restablecida", htmlCorreo);

                if (!correo_enviado)
                    throw new TaskCanceledException("Tenemos problemas, por favor inténtalo mas tarde");

                bool respuesta = await _repositorio.Editar(usuario_encontardo);

                return respuesta;

            }
            catch 
            {
                throw;
            }

        }


#endregion
    }
}
