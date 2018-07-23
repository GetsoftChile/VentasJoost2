using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataTableToExcel;
using DAL;

namespace crm_valvulas_industriales
{
    public partial class ExporteCotizacionesRechazadas : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.ibtnExporteMotivoNoCompra);

            }
            catch (Exception ex)
            {
                Response.Write("Error" + ex.Message);
            }
        }

        protected void ibtnExporteMotivoNoCompra_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dal.getExporteCotizacionesRechazadas().Tables[0];
                Utilidad.ExportDataTableToExcel(dt, "ExporteCotizacionesRechazadas.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
            }
        }

    }
}