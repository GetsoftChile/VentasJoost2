using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class Busqueda
    {

        Database db = DatabaseFactory.CreateDatabase("bdCrm");

        public DataSet getBuscarEmpresaPorNombreRut(string razonSocial, string rut, string codSap, string cotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCliente");

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
                db.AddInParameter(cmd, "@rut", DbType.String, "%" + rut + "%");
            }

            if (codSap == "")
            {
                db.AddInParameter(cmd, "@codSap", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@codSap", DbType.String, "%" + codSap + "%");
            }

            if (cotizacion == "")
            {
                db.AddInParameter(cmd, "@cotizacion", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@cotizacion", DbType.String, "%" + cotizacion + "%");
            }

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

        public DataSet getBuscarEmpresaPorCodSap(string codSap)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarClientePorCodSap");

            if (codSap == "")
            {
                db.AddInParameter(cmd, "@codSap", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@codSap", DbType.String, codSap);
            }

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

        public DataSet getBuscarSeguimiento(string rut)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarSeguimiento");
            db.AddInParameter(cmd, "@rut", DbType.String, rut);
            
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

        public DataSet getBuscarDetalleCotizacion(string cotizacion)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarDetalleCotizacion");
            db.AddInParameter(cmd, "@cotizacion", DbType.String, cotizacion);

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

        public DataSet getBuscarGestiones(string rut, string montoCierreDesde, string montoCierreHasta,
            string montoCotizacionDesde, string montoCotizacionHasta, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarGestiones");
            db.AddInParameter(cmd, "@rut", DbType.String, rut);

            if (montoCierreDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCierreDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCierreDesde", DbType.String, montoCierreDesde);
            }

            if (montoCierreHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCierreHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCierreHasta", DbType.String, montoCierreHasta);
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
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las gestiones, " + ex.Message, ex);
            }
        }



        
        public DataSet getBuscarCotizacionesConSeguimiento(string montoCierreDesde, string montoCierreHasta,
            string montoCotizacionDesde, string montoCotizacionHasta, string fechaDesde, string fechaHasta, string codSap)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarCotizacionesConSeguimiento_PRUEBA");
            db.AddInParameter(cmd, "@rut", DbType.String, codSap);
            if (montoCierreDesde == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCierreDesde", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCierreDesde", DbType.String, montoCierreDesde);
            }

            if (montoCierreHasta == string.Empty)
            {
                db.AddInParameter(cmd, "@montoCierreHasta", DbType.String, null);
            }
            else
            {
                db.AddInParameter(cmd, "@montoCierreHasta", DbType.String, montoCierreHasta);
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
                throw new Exception("No se pudo buscar las cotizaciones, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar las cotizaciones, " + ex.Message, ex);
            }
        }

        public string getValTotalMisTiemposDeOcupacion(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ContarMisTiemposdeOcupacion");

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
                string val = db.ExecuteScalar(cmd).ToString();
                return val;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo contar mis tiempos de ocupación, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo contar mis tiempos de ocupación, " + ex.Message, ex);
            }
        }



        public string getValTotalMisLlamados(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_ContarMisLlamados");

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
                string val = db.ExecuteScalar(cmd).ToString();
                return val;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudo contar mis llamados, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo contar mis llamados, " + ex.Message, ex);
            }
        }

        public DataSet getBuscarMisLlamados(string vendedor, string fechaDesde, string fechaHasta)
        {
            DbCommand cmd = db.GetStoredProcCommand("stp_buscarMisLlamados");

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
                throw new Exception("No se pudo buscar mis llamados, " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar mis llamados, " + ex.Message, ex);
            }
        }
    }
}
