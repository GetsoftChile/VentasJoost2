using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTableToExcel;
using System.Data;
using DAL;

namespace crm_valvulas_industriales
{
    public partial class ExporteGestionesPorCampana : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.ibtnExporteMotivoNoCompra);

                if (!this.Page.IsPostBack)
                {
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
            }
        }


        protected void ibtnExporteMotivoNoCompra_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dal.getExporteGestionesCotizacionPorCampana().Tables[0];
                Utilidad.ExportDataTableToExcel(dt, "ExporteGestionesPorCampana.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
            }
        }
    }
}