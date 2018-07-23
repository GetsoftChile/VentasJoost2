using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Datos
    {
        Database db = DatabaseFactory.CreateDatabase();

        public string getValUsuario(string usuario, string contrasena)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_validaUsuario");
            db.AddInParameter(cmd, "@usuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@contrasena", DbType.String, contrasena);
            try
            {
                string val = db.ExecuteScalar(cmd).ToString();
                return val;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el usuario, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarUsuario(string nombre, string idUsuario)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarUsuario");
            db.AddInParameter(cmd, "@nomUsuario", DbType.String, nombre);

            if (idUsuario == string.Empty)
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el usuario, " + ex.Message, ex);
            }

        }

        public DataSet getBuscarGrupo(string idGrupo, string nomGrupo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarGrupo");
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            if (nomGrupo == string.Empty)
            {
                db.AddInParameter(cmd, "@nomGrupo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nomGrupo", DbType.String, "%" + nomGrupo + "%");
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el grupo, " + ex.Message, ex);
            }
        }

        public void setEliminarUsuario(string idUsuario)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_eliminarUsuario");
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el usuario, " + ex.Message, ex);
            }
        }

        public void setEliminarGrupo(string idGrupo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarGrupo");
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el grupo, " + ex.Message, ex);
            }
        }

        public void setEliminarCliente(string rut)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarCliente");
            db.AddInParameter(cmd, "@rut", DbType.String, rut);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el cliente, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarPerfil(string nombre)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarPerfil");
            db.AddInParameter(cmd, "@nombre", DbType.String, "%" + nombre + "%");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el perfil, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el perfil, " + ex.Message, ex);
            }
        }

        //Empresa
        public DataSet getBuscarEmpresa(string nombre, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarEmpresa");
            db.AddInParameter(cmd, "@nombre", DbType.String, "%" + nombre + "%");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la empresa, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la empresa, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarEmpresaPorUsuario(string idUsuario)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarEmpresaPorUsuario");
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el o las empresas por usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el o las empresas por usuario, " + ex.Message, ex);
            }
        }

        public void setEditarEmpresa(string idEmpresa, string nombreEmpresa, 
            string descripcion, string email, string activo, string imagen,
            string rut, string giro, string telefono)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarEmpresa");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@nombreEmpresa", DbType.String, nombreEmpresa);
            db.AddInParameter(cmd, "@descripcion", DbType.String, descripcion);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            db.AddInParameter(cmd, "@imagen", DbType.String, imagen);

            db.AddInParameter(cmd, "@rut", DbType.String, rut);
            db.AddInParameter(cmd, "@giro", DbType.String, giro);
            db.AddInParameter(cmd, "@telefono", DbType.String, telefono);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede modificar la empresa, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar la empresa, " + ex.Message, ex);
            }
        }


        public string setIngresarEmpresa(string nombreEmpresa, string descripcion, string email, string activo, string imagen)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarEmpresa");

            db.AddInParameter(cmd, "@nombreEmpresa", DbType.String, nombreEmpresa);
            db.AddInParameter(cmd, "@descripcion", DbType.String, descripcion);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            db.AddInParameter(cmd, "@imagen", DbType.String, imagen);
            
            try
            {
                string valor = db.ExecuteScalar(cmd).ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar la empresa, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar la empresa, " + ex.Message, ex);
            }
        }

        public void setEliminarEmpresa(string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarEmpresa");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar la empresa, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar la empresa, " + ex.Message, ex);
            }
        }


        //fin empresa
        public DataSet getBuscarCliente(string rut, string razonSocial, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCliente");

            string r = razonSocial;
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rut);
            db.AddInParameter(cmd, "@razonSocial", DbType.String, "%" + razonSocial + "%");

            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }

        
        public DataSet getBuscarClienteExporte(string rut, string razonSocial)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClienteExporte");


            string r = razonSocial;
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rut);
            db.AddInParameter(cmd, "@razonSocial", DbType.String, "%" + razonSocial + "%");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarClientePorRut(string rut)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientePorRut");
            db.AddInParameter(cmd, "@idCliente", DbType.String, rut);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarClientePorId(string id)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientePorRut");
            db.AddInParameter(cmd, "@idCliente", DbType.String, id);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarClientesAsociados(string rut)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientesAsociados");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rut);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }
        


        public DataSet getBuscarEstadoCliente(string idEstadoCliente, string nomEstadoCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarEstado");

            db.AddInParameter(cmd, "@idEstadoCliente", DbType.String, idEstadoCliente);
            db.AddInParameter(cmd, "@nomEstadoCliente", DbType.String, "%" + nomEstadoCliente + "%");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }


        public void setEditarUsuario(string idUsuario, string usuario, string contrasena, string perfil, string nombre, string activo, string descuento)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_modificarUsuario");
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            db.AddInParameter(cmd, "@usuario", DbType.String, usuario);
            //db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@contrasena", DbType.String, contrasena);
            db.AddInParameter(cmd, "@perfil", DbType.String, perfil);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            db.AddInParameter(cmd, "@descuento", DbType.String, descuento);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede modificar el usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el usuario, " + ex.Message, ex);
            }
        }

        public void setEditarCliente(string idCliente,
            string rut, string razonSocial, string nombreCorto, string telefono, string direccion,
            string comuna, string ciudad, string email, string info, string montoCredito,
            string clasificacion, string zona, string idEstadoCliente, string idUsuarioIngreso,
            string giro, string condicionVenta, string url, string idActividadComercial, string observacion,
            string idCampana, string activo, string rutPadre)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarCliente");
            db.AddInParameter(cmd, "@idCliente", DbType.String, idCliente);
            db.AddInParameter(cmd, "@rut", DbType.String, rut);
            db.AddInParameter(cmd, "@razonSocial", DbType.String, razonSocial);
            db.AddInParameter(cmd, "@nombreCorto", DbType.String, nombreCorto);
            db.AddInParameter(cmd, "@telefono", DbType.String, telefono);
            db.AddInParameter(cmd, "@direccion", DbType.String, direccion);
            db.AddInParameter(cmd, "@comuna", DbType.String, comuna);
            db.AddInParameter(cmd, "@ciudad", DbType.String, ciudad);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@info", DbType.String, info);
            db.AddInParameter(cmd, "@montoCredito", DbType.String, montoCredito);
            db.AddInParameter(cmd, "@clasificacion", DbType.String, clasificacion);
            db.AddInParameter(cmd, "@zona", DbType.String, zona);
            db.AddInParameter(cmd, "@idEstadoCliente", DbType.String, idEstadoCliente);
            db.AddInParameter(cmd, "@idUsuarioIngreso", DbType.String, idUsuarioIngreso);
            db.AddInParameter(cmd, "@giro", DbType.String, giro);
            db.AddInParameter(cmd, "@condicionVenta", DbType.String, condicionVenta);

            db.AddInParameter(cmd, "@url", DbType.String, url);
            db.AddInParameter(cmd, "@idActividadComercial", DbType.String, idActividadComercial);
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            db.AddInParameter(cmd, "@rutClientePadre", DbType.String, rutPadre);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede modificar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar el cliente, " + ex.Message, ex);
            }
        }

        public void setEliminarEmpresaPorUsuario(string idUsuario)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarEmpresaPorUsuario");
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la empresa por usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar la empresa por usuario, " + ex.Message, ex);
            }
        }

        public string getIngresarUsuario(string usuario, string contrasena, string perfil, string nombre, string activo, string descuento)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ingresarUsuario");

            db.AddInParameter(cmd, "@usuario", DbType.String, usuario);
            //db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@contrasena", DbType.String, contrasena);
            db.AddInParameter(cmd, "@perfil", DbType.String, perfil);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            if (string.IsNullOrEmpty(descuento))
            {
                db.AddInParameter(cmd, "@descuento", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@descuento", DbType.String, descuento);
            }
            
            try
            {
                string valor = db.ExecuteScalar(cmd).ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar el usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar el usuario, " + ex.Message, ex);
            }
        }


        public void setIngresarEmpresaPorUsuario(string idUsuario, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarEmpresaPorUsuario");
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar la empresa por usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar la empresa por usuario, " + ex.Message, ex);
            }
        }

        public string setIngresarCliente(string rut, string razonSocial, string nombreCorto, string telefono, string direccion,
            string comuna, string ciudad, string email, string info, string montoCredito,
            string clasificacion, string zona, string idEstadoCliente, string idUsuarioIngreso,
            string giro, string condicionVenta, string url, string idActividadComercial, string observacion, string idCampana, 
            string activo, string rutPadre, string idLead)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCliente");
            db.AddInParameter(cmd, "@rut", DbType.String, rut);
            db.AddInParameter(cmd, "@razonSocial", DbType.String, razonSocial);
            db.AddInParameter(cmd, "@nombreCorto", DbType.String, nombreCorto);
            db.AddInParameter(cmd, "@telefono", DbType.String, telefono);
            db.AddInParameter(cmd, "@direccion", DbType.String, direccion);
            db.AddInParameter(cmd, "@comuna", DbType.String, comuna);
            db.AddInParameter(cmd, "@ciudad", DbType.String, ciudad);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@info", DbType.String, info);
            db.AddInParameter(cmd, "@montoCredito", DbType.String, montoCredito);
            db.AddInParameter(cmd, "@clasificacion", DbType.String, clasificacion);
            db.AddInParameter(cmd, "@zona", DbType.String, zona);
            db.AddInParameter(cmd, "@idEstadoCliente", DbType.String, idEstadoCliente);
            db.AddInParameter(cmd, "@idUsuarioIngreso", DbType.String, idUsuarioIngreso);
            db.AddInParameter(cmd, "@giro", DbType.String, giro);
            db.AddInParameter(cmd, "@condicionVenta", DbType.String, condicionVenta);

            db.AddInParameter(cmd, "@url", DbType.String, url);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            db.AddInParameter(cmd, "@idLead", DbType.String, idLead);
            if (idActividadComercial == "0")
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, idActividadComercial);
            }

            if (idCampana == "0")
            {
                db.AddInParameter(cmd, "@idCampana", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);
            }
            
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            db.AddInParameter(cmd, "@rutClientePadre", DbType.String, rutPadre);

            try
            {
                string valor = db.ExecuteScalar(cmd).ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el cliente, " + ex.Message, ex);
            }
        }




        public void setIngresarClienteMasivo(string rut, string razonSocial, string telefono, string direccion,
            string comuna, string ciudad, string email, string info, string montoCredito,
            string clasificacion, string zona, string giro, 
            string condicionVenta, string idActividadComercial, string idTipoCliente, string rutClientePadre)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarClienteMasivo");
            db.AddInParameter(cmd, "@rut", DbType.String, rut);
            db.AddInParameter(cmd, "@razonSocial", DbType.String, razonSocial);
            db.AddInParameter(cmd, "@telefono", DbType.String, telefono);
            db.AddInParameter(cmd, "@direccion", DbType.String, direccion);
            db.AddInParameter(cmd, "@comuna", DbType.String, comuna);
            db.AddInParameter(cmd, "@ciudad", DbType.String, ciudad);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@info", DbType.String, info);
            db.AddInParameter(cmd, "@montoCredito", DbType.String, montoCredito);
            db.AddInParameter(cmd, "@clasificacion", DbType.String, clasificacion);
            db.AddInParameter(cmd, "@zona", DbType.String, zona);
            

            if (condicionVenta == string.Empty)
            {
                db.AddInParameter(cmd, "@condicionVenta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@condicionVenta", DbType.String, condicionVenta);
            }

            db.AddInParameter(cmd, "@giro", DbType.String, giro);


            if (idTipoCliente == string.Empty)
            {
                db.AddInParameter(cmd, "@idTipoCliente", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idTipoCliente", DbType.String, idTipoCliente);
            }

            if (idActividadComercial == string.Empty)
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, idActividadComercial);
            }

            db.AddInParameter(cmd, "@rutClientePadre", DbType.String, rutClientePadre);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el cliente, " + ex.Message, ex);
            }
        }





        public void setIngresarContacto(string nomContacto, string rutCliente, string email1,
            string email2, string celular, string telefono1, string telefono2, string cargo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarContacto");
            db.AddInParameter(cmd, "@nomContacto", DbType.String, nomContacto);
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@email1", DbType.String, email1);
            db.AddInParameter(cmd, "@email2", DbType.String, email2);
            db.AddInParameter(cmd, "@celular", DbType.String, celular);
            db.AddInParameter(cmd, "@telefono1", DbType.String, telefono1);
            db.AddInParameter(cmd, "@telefono2", DbType.String, telefono2);

            if (cargo == string.Empty)
            {
                db.AddInParameter(cmd, "@cargo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@cargo", DbType.String, cargo);
            }

            
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el contacto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el contacto, " + ex.Message, ex);
            }
        }


        public void setIngresarGrupo(string idGrupo, string nomGrupo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarGrupo");
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@nombreGrupo", DbType.String, nomGrupo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el grupo, " + ex.Message, ex);
            }
        }


        

        public void setEditarContacto(string idContacto, string nomContacto, string rutCliente, string email1,
            string email2, string celular, string telefono1, string telefono2, string cargo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarContacto");
            db.AddInParameter(cmd, "@idContacto", DbType.String, idContacto);
            db.AddInParameter(cmd, "@nomContacto", DbType.String, nomContacto);
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@email1", DbType.String, email1);
            db.AddInParameter(cmd, "@email2", DbType.String, email2);
            db.AddInParameter(cmd, "@celular", DbType.String, celular);
            db.AddInParameter(cmd, "@telefono1", DbType.String, telefono1);
            db.AddInParameter(cmd, "@telefono2", DbType.String, telefono2);
            db.AddInParameter(cmd, "@cargo", DbType.String, cargo);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el contacto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el contacto, " + ex.Message, ex);
            }
        }



        public void setEditarGrupo(string idGrupo, string nomGrupo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarGrupo");
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@nombreGrupo", DbType.String, nomGrupo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el grupo, " + ex.Message, ex);
            }
        }

        public void setEliminarContacto(string idContacto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarContacto");
            db.AddInParameter(cmd, "@idContacto", DbType.String, idContacto);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el contacto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el contacto, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarContacto(string nombreContacto, string idContacto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarContacto");

            db.AddInParameter(cmd, "@idContacto", DbType.String, idContacto);
            db.AddInParameter(cmd, "@nomContacto", DbType.String, "%" + nombreContacto + "%");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarContactoPorId(string idContacto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarContactoPorId");

            db.AddInParameter(cmd, "@idContacto", DbType.String, idContacto);


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }


        

        public DataSet getBuscarContactoPorRutCliente(string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarContactoPorRutCliente");

            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }



        public DataSet getBuscarArchivos(string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarArchivos");

            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar cliente, " + ex.Message, ex);
            }
        }


        public void setEliminarArchivo(string idArchivo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarArchivo");
            db.AddInParameter(cmd, "@idArchivo", DbType.String, idArchivo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el archivo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el archivo, " + ex.Message, ex);
            }
        }

        public void setIngresarArchivo(string rutCliente, string archivo, string nombre, string ruta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarArchivo");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@archivo", DbType.String, archivo);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@ruta", DbType.String, ruta);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el archivo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el archivo, " + ex.Message, ex);
            }
        }


        //SUB-GRUPO

        public void setIngresarSubGrupo(string idGrupo, string idSubGrupo, string nombreSubGrupo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarSubGrupo");
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@idSubGrupo", DbType.String, idSubGrupo);
            db.AddInParameter(cmd, "@nombreSubGrupo", DbType.String, nombreSubGrupo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el sub grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el sub grupo, " + ex.Message, ex);
            }
        }


        public void setEliminarSubGrupo(string idSubGrupo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarSubGrupo");
            db.AddInParameter(cmd, "@idSubGrupo", DbType.String, idSubGrupo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el sub grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el sub grupo, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarSubGrupo(string idSubGrupo, string nombreSubGrupo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BucarSubGrupo");

            db.AddInParameter(cmd, "@idSubGrupo", DbType.String, idSubGrupo);
            db.AddInParameter(cmd, "@nombreSubGrupo", DbType.String, "%" + nombreSubGrupo + "%");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el sub grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el sub grupo, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarSubGrupoPorIdGrupo(string idGrupo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarSubGrupoPorIdGrupo");

            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el sub grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el sub grupo, " + ex.Message, ex);
            }
        }


        public void setEditarSubGrupo(string idGrupo, string idSubGrupo, string nombreSubGrupo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarSubGrupo");
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@idSubGrupo", DbType.String, idSubGrupo);
            db.AddInParameter(cmd, "@nombreSubGrupo", DbType.String, nombreSubGrupo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el sub grupo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el sub grupo, " + ex.Message, ex);
            }
        }


        //PRODUCTO
        public void setEliminarProducto(string idProducto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarProducto");
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el producto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el producto, " + ex.Message, ex);
            }
        }

        public void setEditarProducto(string idProducto, string nombreProducto,
            string codigo, string bodega, string idGrupo, string idSubGrupo,
            string uMedida, string costoUnitario, string valorVenta, int stock,
            string rutaImagen, string rutaPdf, string observacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarProducto");
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            db.AddInParameter(cmd, "@nombreProducto", DbType.String, nombreProducto);
            db.AddInParameter(cmd, "@codigo", DbType.String, codigo);
            db.AddInParameter(cmd, "@bodega", DbType.String, bodega);
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@idSubGrupo", DbType.String, idSubGrupo);
            db.AddInParameter(cmd, "@uMedida", DbType.String, uMedida);
            db.AddInParameter(cmd, "@costoUnitario", DbType.String, costoUnitario);
            db.AddInParameter(cmd, "@valorVenta", DbType.String, valorVenta);
            db.AddInParameter(cmd, "@stock", DbType.String, stock);
            db.AddInParameter(cmd, "@rutaImagen", DbType.String, rutaImagen);
            db.AddInParameter(cmd, "@rutaPdf", DbType.String, rutaPdf);
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el producto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el producto, " + ex.Message, ex);
            }
        }
        

        public void setEditarProductoDescontarStock(int idProducto, int stock)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarProductoDescontarStock");

            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            db.AddInParameter(cmd, "@stock", DbType.String, stock);
            

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el stock del producto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el stock del producto, " + ex.Message, ex);
            }
        }


        public void setIngresarProducto(string nombreProducto,
            string codigo, string bodega, string idGrupo, string idSubGrupo,
            string uMedida, string costoUnitario, string valorVenta, int stock, string observacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarProducto");

            db.AddInParameter(cmd, "@nombreProducto", DbType.String, nombreProducto);
            db.AddInParameter(cmd, "@codigo", DbType.String, codigo);
            db.AddInParameter(cmd, "@bodega", DbType.String, bodega);
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            if (idGrupo==string.Empty || idGrupo=="0")
            {
                db.AddInParameter(cmd, "@idGrupo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            }

            if (idSubGrupo == string.Empty || idSubGrupo == "0")
            {
                db.AddInParameter(cmd, "@idSubGrupo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idSubGrupo", DbType.String, idSubGrupo);
            }
            
            db.AddInParameter(cmd, "@uMedida", DbType.String, uMedida);
            db.AddInParameter(cmd, "@costoUnitario", DbType.String, costoUnitario);
            db.AddInParameter(cmd, "@valorVenta", DbType.String, valorVenta);
           

            db.AddInParameter(cmd, "@stock", DbType.String, stock);
            
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el producto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el producto, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarProducto(string idProducto, string nombreProducto, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarProducto");

            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            if (string.IsNullOrEmpty(nombreProducto))
            {
                db.AddInParameter(cmd, "@nombreProducto", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nombreProducto", DbType.String, "%" + nombreProducto + "%");
            }
            
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el producto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el producto, " + ex.Message, ex);
            }
        }


        //canal
        public DataSet getBuscarCanal(string idCanal, string nombreCanal)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCanal");

            db.AddInParameter(cmd, "@idCanal", DbType.String, idCanal);
            db.AddInParameter(cmd, "@nombreCanal", DbType.String, "%" + nombreCanal + "%");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el canal, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el canal, " + ex.Message, ex);
            }
        }


        public void setIngresarCanal(string nombreCanal)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCanal");
            db.AddInParameter(cmd, "@nombreCanal", DbType.String, nombreCanal);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el canal, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el canal, " + ex.Message, ex);
            }
        }

        public void setEditarCanal(string idCanal, string nombreCanal)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarCanal");

            db.AddInParameter(cmd, "@idCanal", DbType.String, idCanal);
            db.AddInParameter(cmd, "@nombreCanal", DbType.String, nombreCanal);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el canal, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el canal, " + ex.Message, ex);
            }
        }

        public void setEliminarCanal(string idCanal)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarCanal");
            db.AddInParameter(cmd, "@idCanal", DbType.String, idCanal);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el canal, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el canal, " + ex.Message, ex);
            }
        }

        //Cotizacion

        public string setIngresarCotizacion(string idEmpresa, string rutCliente, string montoNeto,
            string montoDescuento, string montoIva, string montoTotal, string idEstadoCotizacion,
            string idUsuarioCreacion, string idUsuarioAsig, string idCanal, string idContacto,
            string observacion, string porcentajeDescuento, string porAprobar, int idRazonSocial,int idCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCotizacion");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@montoNeto", DbType.String, montoNeto);
            db.AddInParameter(cmd, "@montoDescuento", DbType.String, montoDescuento);
            db.AddInParameter(cmd, "@montoIva", DbType.String, montoIva);
            db.AddInParameter(cmd, "@montoTotal", DbType.String, montoTotal);
            db.AddInParameter(cmd, "@idEstadoCotizacion", DbType.String, idEstadoCotizacion);
            db.AddInParameter(cmd, "@idUsuarioCreacion", DbType.String, idUsuarioCreacion);
            db.AddInParameter(cmd, "@idUsuarioAsig", DbType.String, idUsuarioAsig);
            db.AddInParameter(cmd, "@idCanal", DbType.String, idCanal);

            if (idContacto==string.Empty)
            {
                db.AddInParameter(cmd, "@idContacto", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idContacto", DbType.String, idContacto);
            }
            
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            db.AddInParameter(cmd, "@porcentajeDescuento", DbType.String, porcentajeDescuento);
            db.AddInParameter(cmd, "@porAprobar", DbType.String, porAprobar);
            db.AddInParameter(cmd, "@idRazonSocial", DbType.String, idRazonSocial);

            db.AddInParameter(cmd, "@idCliente", DbType.String, idCliente);
            try
            {
                string valor = db.ExecuteScalar(cmd).ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar la cotización, " + ex.Message, ex);
            }
        }

        public void setIngresarCotizacionDetalle(string idCotizacion, string correlativo, string idProducto,
            string montoNeto, string cantidad, string montoTotal, 
            string descuento, string porcDescuento, string porAprobar)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCotizacionDetalle");
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);
            db.AddInParameter(cmd, "@correlativo", DbType.String, correlativo);
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            db.AddInParameter(cmd, "@montoNeto", DbType.String, montoNeto);
            db.AddInParameter(cmd, "@cantidad", DbType.String, cantidad);
            db.AddInParameter(cmd, "@montoTotal", DbType.String, montoTotal);
            db.AddInParameter(cmd, "@descuento", DbType.String, descuento);
            db.AddInParameter(cmd, "@porcDescuento", DbType.String, porcDescuento);
            db.AddInParameter(cmd, "@porAprobar", DbType.String, porAprobar);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar el detalle de la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar el detalle de la cotización, " + ex.Message, ex);
            }
        }

        public void setEditarCotizacionRutaPdf(string idCotizacion, string rutaPdf)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarCotizacionRutaPdf");
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);
            db.AddInParameter(cmd, "@rutaCotizacion", DbType.String, rutaPdf);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar la ruta cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar la ruta cotización, " + ex.Message, ex);
            }
        }
        
        public DataSet getBuscarCotizacion(string idCotizacion, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCotizacion");

            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarCotizacionDetalle(string idCotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCotizacionDetalle");
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarCotizacionGestion(string idGestion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCotizacionPorGestion");
            db.AddInParameter(cmd, "@idGestion", DbType.String, idGestion);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
        }



        public DataSet getBuscarEmpresaPorNombreRut(string razonSocial, string rut)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarClientePrincipal");

            if (razonSocial == "")
            {
                db.AddInParameter(cmd, "@razonSocial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@razonSocial", DbType.String, "%" + razonSocial + "%");
            }

            if (rut == "")
            {
                db.AddInParameter(cmd, "@rut", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@rut", DbType.String,  rut);
            }


            //if (cotizacion == "")
            //{
            //    db.AddInParameter(cmd, "@cotizacion", DbType.String, null);
            //}
            //else
            //{
            //    db.AddInParameter(cmd, "@cotizacion", DbType.String, "%" + cotizacion + "%");
            //}

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarEmpresaPorEmail(string email)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientePorEmail");

            if (email == "")
            {
                db.AddInParameter(cmd, "@email", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@email", DbType.String, "%" + email + "%");
            }
            

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarEmpresaPorTelefono(string telefono)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarClientePrincipalPorTelefono");

            if (telefono == "")
            {
                db.AddInParameter(cmd, "@telefono", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@telefono", DbType.String, "%" + telefono + "%");
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el teléfono, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el teléfono, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarRazonSocial(int? idRazonSocial)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarRazonSocial");
            db.AddInParameter(cmd, "@idRazonSocial", DbType.String, idRazonSocial);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la razon socual, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la razon social, " + ex.Message, ex);
            }
        }
        
        public DataSet getBuscarEmpresaPorNombreRutSinLike(string razonSocial, string rut)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarClientePrincipal");

            if (razonSocial == "")
            {
                db.AddInParameter(cmd, "@razonSocial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@razonSocial", DbType.String, "%" + razonSocial + "%");
            }

            if (rut == "")
            {
                db.AddInParameter(cmd, "@rut", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@rut", DbType.String, rut);
            }


            //if (cotizacion == "")
            //{
            //    db.AddInParameter(cmd, "@cotizacion", DbType.String, null);
            //}
            //else
            //{
            //    db.AddInParameter(cmd, "@cotizacion", DbType.String, "%" + cotizacion + "%");
            //}

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarClientePorCotizacion(string cotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarClientePorCotizacion");

            if (cotizacion == "")
            {
                db.AddInParameter(cmd, "@cotizacion", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@cotizacion", DbType.String,  cotizacion);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
        }



        public DataSet getBuscarGestiones(string rutCliente, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarGestiones");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarGestionesLead(string idLead)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarGestionesLead");
            db.AddInParameter(cmd, "@idLead", DbType.String, idLead);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        }
        

        public DataSet getBuscarCotizacionesEnSeguimientoPorCliente(string rutCliente, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCotizacionesEnSeguimientoPorCliente");
            db.AddInParameter(cmd, "@rut", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarCotizacionesParaGestionar(string rutCliente, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCotizacionesParaGestionar");
            db.AddInParameter(cmd, "@rut", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarCotizacionResumen(string idEmpresa, string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCotizacionResumen");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            
            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        }


        

        public DataSet getBuscarContactoCargo(string idContactoCarga, string contactoCargo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarContactoCargo");
            db.AddInParameter(cmd, "@idContactoCarga", DbType.String, idContactoCarga);
            db.AddInParameter(cmd, "@contactoCargo", DbType.String, "%"+contactoCargo+"%");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los contactos, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los contactos, " + ex.Message, ex);
            }
        }
        
        public DataSet getBuscarDetalleCotizacion(string cotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarDetalleCotizacion");
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, cotizacion);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
        }



        public DataSet getBuscarSeguimiento(string vendedor, string montoCotizacionDesde, string montoCotizacionHasta, 
            string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarSeguimiento");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, vendedor);
            }

            if (montoCotizacionDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCotizacionDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCotizacionDesde", DbType.String, montoCotizacionDesde);
            }

            if (montoCotizacionHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCotizacionHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCotizacionHasta", DbType.String, montoCotizacionHasta);
            }

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }

            


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el seguimiento, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarCotizacionesPorAprobar(string vendedor,string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCotizacionesPorAprobar");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, vendedor);
            }

            

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones, " + ex.Message, ex);
            }
        }

        
        public DataSet getBuscarSeguimientoExporte(string vendedor, string montoCotizacionDesde, string montoCotizacionHasta, 
            string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarSeguimientoExporte");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, vendedor);
            }

            if (montoCotizacionDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCotizacionDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCotizacionDesde", DbType.String, montoCotizacionDesde);
            }

            if (montoCotizacionHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCotizacionHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCotizacionHasta", DbType.String, montoCotizacionHasta);
            }

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }

            


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el seguimiento, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarEstatus(string idEmpresa, string flagCampana, string destino)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarEstatus2");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@flagCampana", DbType.String, flagCampana);
            db.AddInParameter(cmd, "@destino", DbType.String, destino);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el estatus, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el estatus, " + ex.Message, ex);
            }

        }


        public DataSet getBuscarSubEstatus(string estatus, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarSubEstatus");
            db.AddInParameter(cmd, "@estatus", DbType.String, estatus);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el sub estatus, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el sub estatus, " + ex.Message, ex);
            }

        }

        public DataSet getBuscarEstatusSeguimiento(string idEstatus, string idSubEstatus, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarEstatusSeguimiento");
            db.AddInParameter(cmd, "@idEstatus", DbType.String, idEstatus);
            db.AddInParameter(cmd, "@idSubEstatus", DbType.String, idSubEstatus);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar estatus seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar estatus seguimiento, " + ex.Message, ex);
            }

        }

        public DataSet getBuscarAgendamiento(string idEstatus, string idSubEstatus
            , string idEstatusSeguimiento, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarAgendamiento");
            db.AddInParameter(cmd, "@idEstatus", DbType.String, idEstatus);
            db.AddInParameter(cmd, "@idSubEstatus", DbType.String, idSubEstatus);
            db.AddInParameter(cmd, "@idEstatusSeguimiento", DbType.String, idEstatusSeguimiento);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar estatus seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar estatus seguimiento, " + ex.Message, ex);
            }

        }



        public string setIngresarGestion(string idCliente, string idEmpresa, string idEstatus, string idSubEstatus,
                                        string idUsuario, string fechaAgendamiento, string hora,
                                        string observacion, string telefonoAsociado,
                                        string idEstatusSeguimiento, string idMotivoNoCompra, string fechaVisita,
                                        string idCampana)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ingresarGestion");

            db.AddInParameter(cmd, "@rutCliente", DbType.String, idCliente);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@idEstatus", DbType.String, idEstatus);
            db.AddInParameter(cmd, "@idSubEstatus", DbType.String, idSubEstatus);
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            db.AddInParameter(cmd, "@fechaAgendamiento", DbType.String, fechaAgendamiento);
            db.AddInParameter(cmd, "@hora", DbType.String, hora);
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            db.AddInParameter(cmd, "@telefonoAsociado", DbType.String, telefonoAsociado);
            db.AddInParameter(cmd, "@idEstatusSeguimiento", DbType.String, idEstatusSeguimiento);
            db.AddInParameter(cmd, "@idMotivoNoCompra", DbType.String, idMotivoNoCompra);

            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);

            if (fechaVisita == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaVisita", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaVisita", DbType.String, fechaVisita);
            }

            try
            {
                string valor = db.ExecuteScalar(cmd).ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar la gestion, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar la gestion, " + ex.Message, ex);
            }
        }


        public string setIngresarGestionLead(string idLead, string idEstatus, string idSubEstatus,
                                        string idUsuario, string fechaAgendamiento, string hora,
                                        string observacion, string telefonoAsociado,
                                        string idEstatusSeguimiento, string fechaVisita,
                                        string idCampana)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ingresarGestionLead");

            db.AddInParameter(cmd, "@idLead", DbType.String, idLead);
            db.AddInParameter(cmd, "@idEstatus", DbType.String, idEstatus);
            db.AddInParameter(cmd, "@idSubEstatus", DbType.String, idSubEstatus);
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);

            if (fechaAgendamiento == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaAgendamiento", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaAgendamiento", DbType.String, fechaAgendamiento);
            }
            
            db.AddInParameter(cmd, "@hora", DbType.String, hora);
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            db.AddInParameter(cmd, "@telefonoAsociado", DbType.String, telefonoAsociado);
            db.AddInParameter(cmd, "@idEstatusSeguimiento", DbType.String, idEstatusSeguimiento);

            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);

            if (fechaVisita == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaVisita", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaVisita", DbType.String, fechaVisita);
            }

            try
            {
                string valor = db.ExecuteScalar(cmd).ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar la gestion, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar la gestion, " + ex.Message, ex);
            }
        }


        public void setIngresarGestionCotizacion(string idGestion, string idCotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ingresarGestionCotizacion");

            db.AddInParameter(cmd, "@idGestion", DbType.String, idGestion);
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar la gestion cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar la gestion cotización, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarGestionesConAgendamiento(string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarGestionesConAgendamiento");
            db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar agendamiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar agendamiento, " + ex.Message, ex);
            }

        }


        public DataSet getBuscarGestionesConAgendamientoLead(string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarGestionesConAgendamientoLead");
            db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar agendamiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar agendamiento, " + ex.Message, ex);
            }

        }
        //graficos
        public DataSet getGraficoCliente(string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_GrafNuevosCliente");

            db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }

        }

        

        public DataSet getBuscarClientesNuevos(string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientesNuevos");

            db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }
        }

        public DataSet getGraficoGestionVentas(string fechaDesde, string fechaHasta, string ejecutivo, string idActividadComercial)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_GrafGestionVentas");

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            if (ejecutivo == "0")
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, ejecutivo);
            }

            if (idActividadComercial == "0")
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, idActividadComercial);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }

        }


        public DataSet getBuscarGestionDeVentaDetalle(string fechaDesde, string fechaHasta, string ejecutivo, string idActividadComercial)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarGestionDeVentaDetalle");

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            if (ejecutivo == "0")
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, ejecutivo);
            }

            if (idActividadComercial == "0")
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, idActividadComercial);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los clientes, " + ex.Message, ex);
            }

        }


        public DataSet getBuscarFactura(string fechaDesde, string fechaHasta, string ejecutivo, string idActividadComercial)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarFactura");

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            if (ejecutivo == "0")
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, ejecutivo);
            }

            if (idActividadComercial == "0")
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idActividadComercial", DbType.String, idActividadComercial);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las facturas, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las facturas, " + ex.Message, ex);
            }

        }

        

        public DataSet getBuscarGrafCotizaciones(string vendedor, string fechaDesde, string fechaHasta, string tipo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_Graf_Cotizaciones");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            if (tipo == string.Empty)
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, tipo);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el graf cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el graf cotización, " + ex.Message, ex);
            }

        }

        public DataSet getGrafSeguimientoCotizacionesCliente(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_GrafSegCotizaciones");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@intEjecutivo", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el graf cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el graf cotización, " + ex.Message, ex);
            }

        }

        public DataSet getBuscarVentasGanadas(string vendedor, string fechaDesde, string fechaHasta, string tipo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_VentasGanadas");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            if (tipo == string.Empty)
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, tipo);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar ventas ganadas, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar ventas ganadas, " + ex.Message, ex);
            }

        }

        

        public DataSet getBuscarVentasGanadasDetalle(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_VentasGanadasDetalle");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar ventas ganadas, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar ventas ganadas, " + ex.Message, ex);
            }

        }

        public DataSet getBuscarNegociosPerdidos(string vendedor, string fechaDesde, string fechaHasta, string tipo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_NegociosPerdidos");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            if (tipo == string.Empty)
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, tipo);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los negocios perdidos, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los negocios perdidos, " + ex.Message, ex);
            }

        }

        public DataSet getBuscarNegociosPerdidosDetalle(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_NegociosPerdidosDetalle");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los negocios perdidos, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los negocios perdidos, " + ex.Message, ex);
            }

        }


        public DataSet getBuscarContarSeguimiento(string vendedor, string fechaDesde, string fechaHasta, string tipo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarContarCotizacionesSeguimiento");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            if (tipo == string.Empty)
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strTipo", DbType.String, tipo);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }

        }




        public DataSet getBuscarCotizacionesSeguimientoDetalle(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCotizacionesSeguimientoDetalle");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
          
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }

        }


        public DataSet getBuscarCotizacionesSinSeguimientoDetalle(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCotizacionesSinSeguimientoDetalle");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@strVendedor", DbType.String, vendedor);
            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }

        }

        public string getValTotalSeguimientoMarcados(string vendedor, string fechaDesde, string fechaHasta, string conSeguimiento, string tipo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ContarSeguimientoMarcados");

            if (vendedor == "0")
            {
                db.AddInParameter(cmd, "@vendedor", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@vendedor", DbType.String, vendedor);
            }




            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }

            db.AddInParameter(cmd, "@conSeguimiento", DbType.String, conSeguimiento);
            db.AddInParameter(cmd, "@tipo", DbType.String, tipo);

            try
            {
                string val = db.ExecuteScalar(cmd).ToString();
                return val;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el usuario, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el usuario, " + ex.Message, ex);
            }
        }


        //Condicion Comercial

        public DataSet getBuscarCondicionComercial(string idCondicionComercial, string @nombre)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCondicionComercial");
            if (idCondicionComercial == string.Empty)
            {
                db.AddInParameter(cmd, "@idCondicionComercial", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idCondicionComercial", DbType.String, idCondicionComercial);
            }

            if (nombre == string.Empty)
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, "%" + nombre + "%");
            }
            

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }

        }

        //Condicion Venta
        public DataSet getBuscarCondicionVentaMant(string idCondicionVenta, string nombre)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCondicionVentaMant");
            if (idCondicionVenta == string.Empty)
            {
                db.AddInParameter(cmd, "@idCondicionVenta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idCondicionVenta", DbType.String, idCondicionVenta);
            }

            if (nombre == string.Empty)
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, "%" + nombre + "%");
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones seguimiento, " + ex.Message, ex);
            }
        }


        public void setIngresarCondicionComercial(string nombre, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCondicionComercial");

            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar la condición comercial, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar la condición comercial, " + ex.Message, ex);
            }
        }


        public void setIngresarCondicionVenta(string nombre, string glosa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCondicionVenta");

            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@glosa", DbType.String, glosa);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar la condición comercial, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar la condición comercial, " + ex.Message, ex);
            }
        }


        public void setEliminarCondicionComercial(string idCondicionComercial)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarCondicionComercial");

            db.AddInParameter(cmd, "@idCondicionComercial", DbType.String, idCondicionComercial);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar la condición comercial, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar la condición comercial, " + ex.Message, ex);
            }
        }


        public void setEliminarCondicionVenta(string idCondicionVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarCondicionVenta");
            db.AddInParameter(cmd, "@idCondicionVenta", DbType.String, idCondicionVenta);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar la condición venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar la condición venta, " + ex.Message, ex);
            }
        }

        public void setEditarCondicionComercial(string idCondicionComercial, string nombre, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarCondicionComercial");

            db.AddInParameter(cmd, "@idCondicionComercial", DbType.String, idCondicionComercial);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar la condición comercial, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar la condición comercial, " + ex.Message, ex);
            }
        }

        public void setEditarCondicionVenta(string idCondicionVenta, string nombre, string glosa, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarCondicionVenta");

            db.AddInParameter(cmd, "@id", DbType.String, idCondicionVenta);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@glosa", DbType.String, glosa);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar la condición comercial, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar la condición comercial, " + ex.Message, ex);
            }
        }

        


        //Parametros
        public DataSet getBuscarParametros(string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarParametros");
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el parámetro, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el parámetro, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarImagenesProductoPorCotizacion(string idCotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarImagenesProductoPorCotizacion");
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la imagen, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la imagen, " + ex.Message, ex);
            }
        }



        public DataSet getBuscarClienteDireccion(string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClienteDireccion");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la direccion, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la direccion, " + ex.Message, ex);
            }
        }
                
        public DataSet getBuscarNotaVentaPorCliente(string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarNotaVentaPorCliente");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarNotaVentaParametros(string idEjecutivo, string fechaDesde, string fechaHasta, string idEmpresa, int ConFactura)
        {
            //todos 0
            //con fact 1
            //sin fact 2
            //(@idEjecutivo varchar(20), @fechaDesde varchar(10), @fechaHasta varchar(10), @idEmpresa varchar(10))
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarNotaVentaParamentros");
            if (idEjecutivo == "0")
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idEjecutivo", DbType.String, idEjecutivo);
            }

            if (idEmpresa == "0")
            {
                db.AddInParameter(cmd, "@idEmpresa", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            }

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            db.AddInParameter(cmd, "@ConFactura", DbType.String, ConFactura);
            
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarNotaVenta(string idNotaVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarNotaVenta");
            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
        }
        
        public DataSet getBuscarDetalleNotaVenta(string idNotaVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarDetalleNotaVenta");
            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la nota venta, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarDireccionPorCliente(string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarDireccionPorCliente");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la dirección, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la dirección, " + ex.Message, ex);
            }
        }
        
        


        public void setEditarParametro(string idEmpresa, string nombreSistema, string logoMenu, string vigenciaCotizacionDias, string descuentoPagoContato)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarParametros");

            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@nombreSistema", DbType.String, nombreSistema);
            db.AddInParameter(cmd, "@logoMenu", DbType.String, logoMenu);
            db.AddInParameter(cmd, "@vigenciaCotizacionDias", DbType.String, vigenciaCotizacionDias);
            db.AddInParameter(cmd, "@descuentoPagoContato", DbType.String, descuentoPagoContato);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar el parámetro, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar el parámetro, " + ex.Message, ex);
            }
        }

        


        public void setEditarRutaPdfNotaVenta(string idNotaVenta, string rutaPdf)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_editarRutaPdfNotaVenta");

            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta);
            db.AddInParameter(cmd, "@rutaPdfNotaVenta", DbType.String, rutaPdf);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar la nota de venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar la nota de venta, " + ex.Message, ex);
            }
        }
        

        public void setIngresarParametro(string idEmpresa, string nombreSistema, string logoMenu, string vigenciaCotizacionDias, string descuentoPagoContato)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarParametros");

            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@nombreSistema", DbType.String, nombreSistema);
            db.AddInParameter(cmd, "@logoMenu", DbType.String, logoMenu);
            db.AddInParameter(cmd, "@vigenciaCotizacionDias", DbType.String, vigenciaCotizacionDias);
            db.AddInParameter(cmd, "@descuentoPagoContato", DbType.String, descuentoPagoContato);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar el parámetro, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar el parámetro, " + ex.Message, ex);
            }
        }

        public void setEditarContactoCargo(string idContactoCargo, string contactoCargo, string idEmpresa, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarContactoCargo");

            db.AddInParameter(cmd, "@idContactoCargo", DbType.String, idContactoCargo);
            db.AddInParameter(cmd, "@contactoCargo", DbType.String, contactoCargo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar el cargo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar el cargo, " + ex.Message, ex);
            }
        }

        public void setIngresarContactoCargo(string contactoCargo, string idEmpresa)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarContactoCargo");

            db.AddInParameter(cmd, "@contactoCargo", DbType.String, contactoCargo);
            db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar el contacto cargo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar el contacto cargo, " + ex.Message, ex);
            }
        }

        public void setEliminarContactoCargo(string idContactoCargo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarContactoCargo");
            db.AddInParameter(cmd, "@idContactoCargo", DbType.String, idContactoCargo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar el cargo, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar el cargo, " + ex.Message, ex);
            }
        }


        public void setEditarClienteCargaMasiva(string rut, string idEstadoCliente, string montoAprobacion, string condicionDeVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarClienteCargaMasiva");
            db.AddInParameter(cmd, "@rut", DbType.String, rut);
            db.AddInParameter(cmd, "@idEstadoCliente", DbType.String, idEstadoCliente);
            db.AddInParameter(cmd, "@montoAprobacion", DbType.String, montoAprobacion);
            db.AddInParameter(cmd, "@condicionDeVenta", DbType.String, condicionDeVenta);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar el cliente, " + ex.Message, ex);
            }
        }

        public void setEditarProductoCargaMasiva(string codigo, string precio)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarProductoCargaMasiva");
            db.AddInParameter(cmd, "@codigo", DbType.String, codigo);
            db.AddInParameter(cmd, "@precio", DbType.String, precio);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar el producto, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar el producto, " + ex.Message, ex);
            }
        }


        public string setIngresarNotaVenta(string idCotizacion, string direccionDespacho, string direccionFacturacion,
            string referencia, string ordenDeCompra, string rutaOrdenDeCompra, string rutaOrdenDeCompra2)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarNotaVenta");
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);
            db.AddInParameter(cmd, "@idDireccionDespacho", DbType.String, direccionDespacho);
            db.AddInParameter(cmd, "@idDireccionFacturacion", DbType.String, direccionFacturacion);
            db.AddInParameter(cmd, "@referencia", DbType.String, referencia);
            db.AddInParameter(cmd, "@ordenDeCompra", DbType.String, ordenDeCompra);
            db.AddInParameter(cmd, "@rutaOrdenDeCompra", DbType.String, rutaOrdenDeCompra);
            db.AddInParameter(cmd, "@rutaOrdenDeCompra2", DbType.String, rutaOrdenDeCompra2);
            try
            {
                //db.ExecuteNonQuery(cmd);
                string val = db.ExecuteScalar(cmd).ToString();
                return val;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresa la nota venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresa la nota venta, " + ex.Message, ex);
            }
        }

        public void setIngresarDireccion(string rutCliente, string calle, string numero, string resto, string comuna)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarDireccion");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@calle", DbType.String, calle);
            db.AddInParameter(cmd, "@numero", DbType.String, numero);
            db.AddInParameter(cmd, "@resto", DbType.String, resto);
            db.AddInParameter(cmd, "@comuna", DbType.String, comuna);
            try
            {
                db.ExecuteNonQuery(cmd);
                //string val = db.ExecuteScalar(cmd).ToString();
                //return val;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresa la dirección, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresa la dirección, " + ex.Message, ex);
            }
        }

        public void setEditarDireccionCliente(string idDireccion, string calle, string numero, string resto, string comuna)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarDireccionCliente");
            db.AddInParameter(cmd, "@idDireccion", DbType.String, idDireccion);
            db.AddInParameter(cmd, "@calle", DbType.String, calle);
            db.AddInParameter(cmd, "@numero", DbType.String, numero);
            db.AddInParameter(cmd, "@resto", DbType.String, resto);
            db.AddInParameter(cmd, "@comuna", DbType.String, comuna);
            try
            {
                db.ExecuteNonQuery(cmd);
                //string val = db.ExecuteScalar(cmd).ToString();
                //return val;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar la dirección, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar la dirección, " + ex.Message, ex);
            }
        }



        public void setEditarClienteAsignacionUsuario(string idUsuario, string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarClienteAsignacionUsuario");

            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el cliente, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarCarteraAsignada(string idUsuario, string idCampana, string rut, string nombre)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCarteraAsignada2");

            if (idUsuario == "0")
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            }

            if (idCampana == "0")
            {
                db.AddInParameter(cmd, "@idCampana", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);
            }

            if (rut == string.Empty)
            {
                db.AddInParameter(cmd, "@rut", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@rut", DbType.String, rut);
            }

            if (nombre == "0")
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            }
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la cartera asignada, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la cartera asignada, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarCarteraAsignadaExporte(string idUsuario, string idCampana)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCarteraAsignadaExporte");

            if (idUsuario == "0")
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            }

            if (idCampana == "0")
            {
                db.AddInParameter(cmd, "@idCampana", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);
            }
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la cartera asignada, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la cartera asignada, " + ex.Message, ex);
            }
        }


        
        public DataSet getBuscarCondicionVenta(string idCondicionVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCondicionVenta");

            if (idCondicionVenta == string.Empty)
            {
                db.AddInParameter(cmd, "@idCondicionVenta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idCondicionVenta", DbType.String, idCondicionVenta);
            }
            
 
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la condición venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la condición venta, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarComuna()
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarComunas");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la dirección, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la dirección, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarClientePorIdCampana(string idCampana)
        {
            DbCommand cmd = db.GetStoredProcCommand("buscarClientePorCampana");
            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
        }

        
        
        public void setEliminarNotaVenta(string idNotaVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarNotaVenta");

            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la nota de venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar la nota de venta, " + ex.Message, ex);
            }
        }



        public void setEditarProductoMasivo(string nombreProducto, string codigo, 
            string bodega, string idGrupo, string idSubGrupo, string medida,
            string stock, string costoUnitario, string valorVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarProductosMasivo");

            db.AddInParameter(cmd, "@nombreProducto", DbType.String, nombreProducto);
            db.AddInParameter(cmd, "@codigo", DbType.String, codigo);
            db.AddInParameter(cmd, "@bodega", DbType.String, bodega);
            db.AddInParameter(cmd, "@idGrupo", DbType.String, idGrupo);
            db.AddInParameter(cmd, "@idSubGrupo", DbType.String, idSubGrupo);
            db.AddInParameter(cmd, "@medida", DbType.String, medida);
            db.AddInParameter(cmd, "@stock", DbType.String, stock);
            db.AddInParameter(cmd, "@costoUnitario", DbType.String, costoUnitario);
            db.AddInParameter(cmd, "@valorVenta", DbType.String, valorVenta);


            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la nota de venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar la nota de venta, " + ex.Message, ex);
            }
        }

        public void setEditarEstadoCotizacion(string idCotizacion, string idEstadoCotizacion, string observacion, string idUsuario)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarEstadoCotizacion");

            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);
            db.AddInParameter(cmd, "@idEstadoCotizacion", DbType.String, idEstadoCotizacion);
            db.AddInParameter(cmd, "@observacion", DbType.String, observacion);
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar la cotización, " + ex.Message, ex);
            }
        }


        //Campaña
        public void setEditarCampana(string idCampana, string nomCampana, string fechaInicio, string fechaTermino, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarCampana");

            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);
            db.AddInParameter(cmd, "@nomCampana", DbType.String, nomCampana);
            db.AddInParameter(cmd, "@fechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@fechaTermino", DbType.String, fechaTermino);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar la campaña, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar la cotización, " + ex.Message, ex);
            }
        }


        public void setEliminarCampana(string idCampana)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarCampana");

            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la campaña, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar la campaña, " + ex.Message, ex);
            }
        }


        public void setIngresarCampana(string nomCampana, string fechaInicio, string fechaTermino, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCampana");

            db.AddInParameter(cmd, "@nomCampana", DbType.String, nomCampana);
            db.AddInParameter(cmd, "@fechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@fechaTermino", DbType.String, fechaTermino);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar la campaña, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar la campaña, " + ex.Message, ex);
            }
        }
        

        public DataSet getBuscarCampana(string nomCampana, string activo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCampana");

            if (nomCampana == string.Empty)
            {
                db.AddInParameter(cmd, "@nomCampana", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nomCampana", DbType.String, "%" + nomCampana + "%");
            }

            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la campaña, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la campaña, " + ex.Message, ex);
            }
        }

        
        public DataSet getBuscarTipoCliente(string nombreTipoCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarTipoCliente");

            if (nombreTipoCliente == string.Empty)
            {
                db.AddInParameter(cmd, "@nombreTipoCliente", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nombreTipoCliente", DbType.String, "%" + nombreTipoCliente + "%");
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el tipo cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el tipo cliente, " + ex.Message, ex);
            }
        }

        


        public DataSet getBuscarCotizacionPorId(string id)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarCotizacionPorId");

            if (id == string.Empty)
            {
                db.AddInParameter(cmd, "@id", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@id", DbType.String, id);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la cotización, " + ex.Message, ex);
            }
        }

        
        public DataSet getBuscarActividadComercial()
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarActividadComercial");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la actividad comercial, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la actividad comercial, " + ex.Message, ex);
            }
        }

        public void setEditarClienteCampana(string rutCliente, string idCampana)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarClienteCampana");

            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente.Trim());
            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana.Trim());

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar el cliente, " + ex.Message, ex);
            }
        }

        public void setIngresarCampanaCliente(string idCampana, string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCampanaCliente");

            db.AddInParameter(cmd, "@idCampana", DbType.String, idCampana.Trim());
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente.Trim());
            
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar la campaña, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar la campaña, " + ex.Message, ex);
            }
        }


        public void setIngresarFactura(string idFactura,string rutCliente,string idNotaVenta,string fechaFacturacion,
            string montoNeto,string idEstadoFactura,string idUsuarioCreacion,string idFormaPago, int idCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarFactura");

            db.AddInParameter(cmd, "@idFactura", DbType.String, idFactura.Trim());
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente.Trim());
            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta.Trim());
            db.AddInParameter(cmd, "@fechaFacturacion", DbType.String, fechaFacturacion.Trim());
            db.AddInParameter(cmd, "@montoNeto", DbType.String, montoNeto.Trim());
            db.AddInParameter(cmd, "@idEstadoFactura", DbType.String, idEstadoFactura.Trim());
            db.AddInParameter(cmd, "@idUsuarioCreacion", DbType.String, idUsuarioCreacion.Trim());
            db.AddInParameter(cmd, "@idFormaPago", DbType.String, idFormaPago.Trim());
            db.AddInParameter(cmd, "@idCliente", DbType.String, idCliente);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo ingresar la factura, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo ingresar la factura, " + ex.Message, ex);
            }
        }
        

        
        public DataSet getBuscarFormaPago(string nombre)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarFormaPago");
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar la forma de pago, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la forma de pago, " + ex.Message, ex);
            }
        }


        
        public DataSet getBuscarFacturaPorRutCliente(string rutCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarFacturaPorRutCliente");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las factura, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las factura, " + ex.Message, ex);
            }
        }

        
        public DataSet getBuscarBanco()
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarBanco");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el banco, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el banco, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarClientePorIdFactura(string idFactura)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientePorIdFactura");
            db.AddInParameter(cmd, "@idFactura", DbType.String, idFactura);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarClientePorIdNotaVenta(int idNotaVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientePorIdNotaVenta");
            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el cliente, " + ex.Message, ex);
            }
        }
        
        public void setEditarEstadoNotaVenta(int idNotaVenta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarEstadoNotaVenta");
            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo editar la nota de venta, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo editar la nota de venta, " + ex.Message, ex);
            }
        }

        public void setEliminarFactura(string idFactura)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarFactura");
            db.AddInParameter(cmd, "@idFactura", DbType.String, idFactura.Trim());

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar la factura, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar la factura, " + ex.Message, ex);
            }
        }

        public void setEliminarPagoPorIdPago(string idPago)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarPagoIdFactura");
            db.AddInParameter(cmd, "@idPago", DbType.String, idPago.Trim());

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo eliminar el pago, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el pago, " + ex.Message, ex);
            }
        }

        

        public string setIngresarCajaEnc(int idNotaVenta, string rutCliente, string comprobanteIngreso,
            string observaciones, string idUsuario, string montoNeto, string montoTotal)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCajaEnc");

            db.AddInParameter(cmd, "@idNotaVenta", DbType.String, idNotaVenta);
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@comprobanteIngreso", DbType.String, comprobanteIngreso);
            db.AddInParameter(cmd, "@observaciones", DbType.String, observaciones);
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            db.AddInParameter(cmd, "@montoNeto", DbType.String, montoNeto);
            db.AddInParameter(cmd, "@montoPagado", DbType.String, montoTotal);
            try
            {
                string valor = db.ExecuteScalar(cmd).ToString();
                return valor;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar caja enc, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar caja enc, " + ex.Message, ex);
            }
        }


        public void setIngresarCajaDet(string idPago, string correlativo, string nroDoc,
            string montoNeto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCajaDet");

            db.AddInParameter(cmd, "@idPago", DbType.String, idPago);
            db.AddInParameter(cmd, "@correlativo", DbType.String, correlativo);
            db.AddInParameter(cmd, "@nroDoc", DbType.String, nroDoc);
            db.AddInParameter(cmd, "@montoNeto", DbType.String, montoNeto);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar caja enc, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar caja enc, " + ex.Message, ex);
            }
        }



        public void setIngresarCajaDocPago(string idPago, string correlativo, string monto,
            string fechaVencimiento, string idBanco, string nroCheque, string nroCuentaCorriente,
            string idFormaPago, string rutCheque)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarCajaDocPago");

            db.AddInParameter(cmd, "@idPago", DbType.String, idPago);
            db.AddInParameter(cmd, "@correlativo", DbType.String, correlativo);
            db.AddInParameter(cmd, "@monto", DbType.String, monto);

            if (fechaVencimiento == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaVencimiento", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaVencimiento", DbType.String, fechaVencimiento);
            }
            
            db.AddInParameter(cmd, "@idBanco", DbType.String, idBanco);
            db.AddInParameter(cmd, "@nroCheque", DbType.String, nroCheque);
            db.AddInParameter(cmd, "@nroCuentaCorriente", DbType.String, nroCuentaCorriente);
            db.AddInParameter(cmd, "@idFormaPago", DbType.String, idFormaPago);
            db.AddInParameter(cmd, "@rutCheque", DbType.String, rutCheque);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar caja doc pago, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar caja doc pago, " + ex.Message, ex);
            }
        }




        public DataSet getBuscarPagoPorIdFactura(string idFactura)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarPagoPorIdFactura");
            db.AddInParameter(cmd, "@idFactura", DbType.String, idFactura);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el pago, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el pago, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarPagoDetallePorIdFactura(string idFactura)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarPagoDetallePorIdFactura");
            db.AddInParameter(cmd, "@idFactura", DbType.String, idFactura);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar el pago, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar el pago, " + ex.Message, ex);
            }
        }

        public DataSet getExporteGestionesMotivoNoCompra()
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ExporteGestionesMotivoNoCompra");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        } 

        public DataSet getExporteCotizacionesRechazadas()
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ExporteCotizacionesRechazadas");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones, " + ex.Message, ex);
            }
        } 

        

        public DataSet getExporteGestionesCotizacionPorCampana()
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ExporteGestionesCotizacionPorCampana");

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarProductosVendidosPorRutCliente(string rutCliente, string fechaDesde, string fechaHasta, int? idCliente)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarProductosVendidosPorRutCliente");
            db.AddInParameter(cmd, "@rutCliente", DbType.String, rutCliente);
            db.AddInParameter(cmd, "@idCliente", DbType.String, idCliente);
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar los productos vendidos, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar los productos vendidos, " + ex.Message, ex);
            }
        }
        

        public DataSet getBuscarLead(int? id,string nombre, int? idUsuarioAsignado)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarLead");
            db.AddInParameter(cmd, "@id", DbType.String, id);

            if (nombre==string.Empty)
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            }

            if (idUsuarioAsignado==0)
            {
                db.AddInParameter(cmd, "@idUsuarioAsig", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idUsuarioAsig", DbType.String, idUsuarioAsignado);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al buscar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarLeadSeguimientoExporte(int? id, string nombre, int idUsuarioAsignado)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarLeadSeguimientoExporte");
            db.AddInParameter(cmd, "@id", DbType.String, id);

            if (nombre == string.Empty)
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            }

            if (idUsuarioAsignado == 0)
            {
                db.AddInParameter(cmd, "@idUsuarioAsig", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idUsuarioAsig", DbType.String, idUsuarioAsignado);
            }

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al buscar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar, " + ex.Message, ex);
            }
        }
        

        public void setIngresarLead(string nombre, string empresa, string cargo,
            string direccion, string comuna, string email, string telefono1,
            string telefono2, int tieneSwErp, int tieneSwCobro,int tieneSwTicket, int tieneSwVenta, string comentario,
            int activo, int idUsuarioCreacion, int idUsuarioAsignado)
            //@nombre varchar(150),@empresa varchar(30),@cargo varchar(30),@direccion varchar(150),
//@comuna varchar(50),@email varchar(80),@telefono1 varchar(12),@telefono2 varchar(12),@tieneSwErp int,@tieneSwCobro int,
//@tieneSwTicket int,@tieneSwVenta int,@comentario varchar(500),@activo int
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarLead");

            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@empresa", DbType.String, empresa);
            db.AddInParameter(cmd, "@cargo", DbType.String, cargo);
            
            db.AddInParameter(cmd, "@direccion", DbType.String, direccion);
            db.AddInParameter(cmd, "@comuna", DbType.String, comuna);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@telefono1", DbType.String, telefono1);
            db.AddInParameter(cmd, "@telefono2", DbType.String, telefono2);

            db.AddInParameter(cmd, "@tieneSwErp", DbType.String, tieneSwErp);
            db.AddInParameter(cmd, "@tieneSwCobro", DbType.String, tieneSwCobro);
            db.AddInParameter(cmd, "@tieneSwTicket", DbType.String, tieneSwTicket);
            db.AddInParameter(cmd, "@tieneSwVenta", DbType.String, tieneSwVenta);
            db.AddInParameter(cmd, "@comentario", DbType.String, comentario);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);

            db.AddInParameter(cmd, "@idUsuarioCreacion", DbType.String, idUsuarioCreacion);
            db.AddInParameter(cmd, "@idUsuarioAsignado", DbType.String, idUsuarioAsignado);
            

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
        }


        public void setEditarLead(int id,string nombre, string empresa, string cargo,
            string direccion, string comuna, string email, string telefono1,
            string telefono2, int tieneSwErp, int tieneSwCobro, int tieneSwTicket, int tieneSwVenta, string comentario,
            int activo,  int idUsuarioAsignado, string swErp, string swCobros, string swVentas, string swTicket)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarLead");
            db.AddInParameter(cmd, "@id", DbType.String, id);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@empresa", DbType.String, empresa);
            db.AddInParameter(cmd, "@cargo", DbType.String, cargo);

            db.AddInParameter(cmd, "@direccion", DbType.String, direccion);
            db.AddInParameter(cmd, "@comuna", DbType.String, comuna);
            db.AddInParameter(cmd, "@email", DbType.String, email);
            db.AddInParameter(cmd, "@telefono1", DbType.String, telefono1);
            db.AddInParameter(cmd, "@telefono2", DbType.String, telefono2);

            db.AddInParameter(cmd, "@tieneSwErp", DbType.String, tieneSwErp);
            db.AddInParameter(cmd, "@tieneSwCobro", DbType.String, tieneSwCobro);
            db.AddInParameter(cmd, "@tieneSwTicket", DbType.String, tieneSwTicket);
            db.AddInParameter(cmd, "@tieneSwVenta", DbType.String, tieneSwVenta);
            db.AddInParameter(cmd, "@comentario", DbType.String, comentario);
            db.AddInParameter(cmd, "@activo", DbType.String, activo);
            
            db.AddInParameter(cmd, "@idUsuarioAsignado", DbType.String, idUsuarioAsignado);

            db.AddInParameter(cmd, "@swErp", DbType.String, tieneSwTicket);
            db.AddInParameter(cmd, "@swCobros", DbType.String, tieneSwVenta);
            db.AddInParameter(cmd, "@swVentas", DbType.String, comentario);
            db.AddInParameter(cmd, "@swTicket", DbType.String, activo);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
        }
        
        public void setEliminarLead(int id)
        //@nombre varchar(150),@empresa varchar(30),@cargo varchar(30),@direccion varchar(150),
        //@comuna varchar(50),@email varchar(80),@telefono1 varchar(12),@telefono2 varchar(12),@tieneSwErp int,@tieneSwCobro int,
        //@tieneSwTicket int,@tieneSwVenta int,@comentario varchar(500),@activo int
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarLead");
            db.AddInParameter(cmd, "@id", DbType.String, id);
            
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
        }



        public void setEditarAsignacionPorLead(int idUsuario, int idLead)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_AsignacionPorIdLead");
            db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            db.AddInParameter(cmd, "@idLead", DbType.String, idLead);
        
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
        }

        


        public void setEditarMaterialProducto(int idMaterial, int idProducto, int cantidad)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarMaterialProducto");
            db.AddInParameter(cmd, "@idMaterial", DbType.String, idMaterial);
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            db.AddInParameter(cmd, "@cantidad", DbType.String, cantidad);
            
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
        }

        public void setEditarLeadEstado(int idEstado, int idLead)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_upLeadEstado");
            db.AddInParameter(cmd, "@idEstado", DbType.String, idEstado);
            db.AddInParameter(cmd, "@idLead", DbType.String, idLead);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarGestionPorCategoriaSubCategoriaGestion(string idUsuario, string fechaDesde, string fechaHasta, string tipo)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ContarGestionesExtrajudiciales");
            db.AddInParameter(cmd, "@tipo", DbType.String, tipo);
            if (idUsuario == "0")
            {
                db.AddInParameter(cmd, "@ejecutivo", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@ejecutivo", DbType.String, idUsuario);
            }

            if (string.IsNullOrEmpty(fechaDesde))
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (string.IsNullOrEmpty(fechaHasta))
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo contar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo contar las gestiones, " + ex.Message, ex);
            }
        }



        public DataSet getBuscarDetalleGestionPorCategoriaSubCategoriaGestion(string idUsuario, string fechaDesde, string fechaHasta, string tipo,
            string idEstatus, string idSubEstatus, string idEstatusSeguimiento)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarGestionesDetalle");
            db.AddInParameter(cmd, "@tipo", DbType.String, tipo);



            if (idEstatus == "0")
            {
                db.AddInParameter(cmd, "@idEstatus", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idEstatus", DbType.String, idEstatus);
            }
            if (idSubEstatus == "0")
            {
                db.AddInParameter(cmd, "@idSubEstatus", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idSubEstatus", DbType.String, idSubEstatus);
            }

            if (idEstatusSeguimiento == "0")
            {
                db.AddInParameter(cmd, "@idEstatusSeguimiento", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idEstatusSeguimiento", DbType.String, idEstatusSeguimiento);
            }

            if (idUsuario == "0")
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);
            }

            if (string.IsNullOrEmpty(fechaDesde))
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }

            if (string.IsNullOrEmpty(fechaHasta))
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo contar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo contar las gestiones, " + ex.Message, ex);
            }
        }


        public void setEditarLeadPorId(int idLead, string idGestionLead)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarLeadPorId");
            db.AddInParameter(cmd, "@idLead", DbType.String, idLead);
            db.AddInParameter(cmd, "@idGestionLead", DbType.String, idGestionLead);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
        }
        
        public DataSet getBuscarMaterial(string nombre, int? idProducto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarMaterial");
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarMaterialProducto(int idProducto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarProductoMaterial");
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
        }


        public DataSet getBuscarProductoCantdadVendida(string idEmpresa, string fechaDesde, string fechaHasta)
        {
            //@@idEmpresa varchar(15),@fechaDesde varchar(10),@fechaHasta varchar(10)
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarProductoCantidadVendida");
            if (idEmpresa == "0")
            {
                db.AddInParameter(cmd, "@idEmpresa", DbType.String, null);

            }
            else
            {
                db.AddInParameter(cmd, "@idEmpresa", DbType.String, idEmpresa);

            }
            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }
            
            
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
        }


        


        public DataSet getBuscarMaterialUsado(string fechaDesde, string fechaHasta)
        {
            //@@idEmpresa varchar(15),@fechaDesde varchar(10),@fechaHasta varchar(10)
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarMaterialUsado");

            if (fechaDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaDesde", DbType.String, fechaDesde);
            }
            if (fechaHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@fechaHasta", DbType.String, fechaHasta);
            }


            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
        }

        public void setIngresarMaterial(string tipo, string nombre ,string medida ,string color ,int cantidad)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarMaterial");
            db.AddInParameter(cmd, "@tipo", DbType.String, tipo);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@medida", DbType.String, medida);
            db.AddInParameter(cmd, "@color", DbType.String, color);
            db.AddInParameter(cmd, "@cantidad", DbType.String, cantidad);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
        }

        public void setEditarMaterial(int idMaterial, string tipo, string nombre, string medida, string color, int cantidad)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarMaterial");
            db.AddInParameter(cmd, "@idMaterial", DbType.String, idMaterial);
            db.AddInParameter(cmd, "@tipo", DbType.String, tipo);
            db.AddInParameter(cmd, "@nombre", DbType.String, nombre);
            db.AddInParameter(cmd, "@medida", DbType.String, medida);
            db.AddInParameter(cmd, "@color", DbType.String, color);
            db.AddInParameter(cmd, "@cantidad", DbType.String, cantidad);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
        }

        
        public void setEditarMaterialResto(int idMaterial, int cantidad)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EditarMaterialResto");
            db.AddInParameter(cmd, "@idMaterial", DbType.String, idMaterial);
            db.AddInParameter(cmd, "@cantidad", DbType.String, cantidad);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede editar, " + ex.Message, ex);
            }
        }

        public void setEliminarMaterial(int idMaterial)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarMaterial");
            db.AddInParameter(cmd, "@idMaterial", DbType.String, idMaterial);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar, " + ex.Message, ex);
            }
        }


        public void setEliminarMaterialProducto(int idMaterial, int idProducto)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarMaterialProducto");
            db.AddInParameter(cmd, "@idMaterial", DbType.String, idMaterial);
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar, " + ex.Message, ex);
            }
        }


        public void setEliminarCotizacion(int idCotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_EliminarCotizacion");
            db.AddInParameter(cmd, "@idCotizacion", DbType.String, idCotizacion);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede eliminar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar, " + ex.Message, ex);
            }
        }
        


        public void setIngresarMaterialProducto(int idMaterial, int idProducto, int cantidad)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_IngresarMaterialProducto2");
            db.AddInParameter(cmd, "@idMaterial", DbType.String, idMaterial);
            db.AddInParameter(cmd, "@idProducto", DbType.String, idProducto);
            db.AddInParameter(cmd, "@cantidad", DbType.String, cantidad);

            try
            {
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede ingresar, " + ex.Message, ex);
            }
        }




        public DataSet getBuscarEmpresaPorEmailSinLike(string email)
        {
 
            DbCommand cmd = db.GetStoredProcCommand("stp_BuscarClientePorEmailSinLike");

            db.AddInParameter(cmd, "@email", DbType.String, "%"+email+"%");
            

            try
            {
                return db.ExecuteDataSet(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar, " + ex.Message, ex);
            }
        }

        
    }

}
