using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using DataTableToExcel;
using System.Text;

namespace crm_valvulas_industriales
{
    public partial class ExporteProductosVendidos : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.lbtnExporte);
                if (!this.Page.IsPostBack)
                {
                    
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void lbtnExporte_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //DataTable dt = new DataTable();
                //string rutCliente = null;
                //if (txtRutCliente.Text != string.Empty)
                //{
                //    rutCliente = txtRutCliente.Text;
                //}
                //dt = dal.getBuscarProductosVendidosPorRutCliente(rutCliente, txtFechaDesde.Text, txtFechaHasta.Text).Tables[0];

                //Response.ContentType = "Application/x-msexcel";
                //Response.AddHeader("content-disposition", "attachment;filename=" + "exporteProductosVendidos" + ".csv");
                //Response.ContentEncoding = Encoding.Unicode;
                //Response.Write(Utilidad.ExportToCSVFile(dt));
                //Response.End();

                DataTable dataTable = new DataTable();
                string rutCliente = (string)null;
                if (this.txtRutCliente.Text != string.Empty)
                    rutCliente = this.txtRutCliente.Text;
                DataTable table = this.dal.getBuscarProductosVendidosPorRutCliente(rutCliente, this.txtFechaDesde.Text, this.txtFechaHasta.Text, null).Tables[0];
                this.Response.ContentType = "Application/x-msexcel";
                this.Response.AddHeader("content-disposition", "attachment;filename=exporteProductosVendidos.csv");
                this.Response.ContentEncoding = Encoding.Unicode;
                this.Response.Write(Utilidad.ExportToCSVFile(table));
                this.Response.End();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}