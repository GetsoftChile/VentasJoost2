using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using InfoSoftGlobal;
using DAL;
using DataTableToExcel;

namespace crm_valvulas_industriales
{
    public partial class GrafClientesIngresados : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.lbtnBuscar);
                scriptManager.RegisterPostBackControl(this.ibtnExportar);
                
                if (!this.Page.IsPostBack)
                {
                    DateTime hoy = DateTime.Now;
                    txtFechaDesde.Text = (hoy.AddDays(-7).ToString("dd-MM-yyyy"));
                    txtFechaHasta.Text = hoy.ToString("dd-MM-yyyy");
                    
                    buscar();
                    buscarGrilla();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                buscar();
                buscarGrilla();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscar() 
        {
            DataTable dt = new DataTable();
            StringBuilder xmlGrafico = new StringBuilder();

            dt = dal.getGraficoCliente(txtFechaDesde.Text, txtFechaHasta.Text).Tables[0];

            xmlGrafico.Append("<chart caption='Clientes' xAxisName='Fecha' yAxisName='Q'>");
            xmlGrafico.Append("<categories>");
            foreach (DataRow item in dt.Rows)
            {
                xmlGrafico.Append("<category label='" + item["FECHA"].ToString() + "'/>");
            }
            xmlGrafico.Append("</categories>");

            xmlGrafico.Append("<dataset seriesName='Cliente'>");
            foreach (DataRow item in dt.Rows)
            {
                xmlGrafico.Append("<set value='" + item["CLIENTES"].ToString() + "'/>");
            }
            xmlGrafico.Append("</dataset>");

            xmlGrafico.Append("<dataset seriesName='Cotizaciones'>");
            foreach (DataRow item in dt.Rows)
            {
                xmlGrafico.Append("<set value='" + item["COTIZACIONES"].ToString() + "'/>");
            }
            xmlGrafico.Append("</dataset>");

            xmlGrafico.Append("</chart>");
            litClientesNuevos.Text = FusionCharts.RenderChart("assets/FusionCharts/ScrollColumn2D.swf", "", xmlGrafico.ToString(), "generalGanadasPerdidasPorVendedor", "1000", "400", false, true);

        }

        void buscarGrilla() 
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarClientesNuevos(txtFechaDesde.Text, txtFechaHasta.Text).Tables[0];
            Session["grillaClientesNuevos"] = dt;
            grvClientesNuevos.DataSource = dt;
            grvClientesNuevos.DataBind();
        }

        //paginamiento grilla seguimiento

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaClientesNuevos"] != null)
            {
                grvClientesNuevos.DataSource = Session["grillaClientesNuevos"];
                grvClientesNuevos.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            grvClientesNuevos.PageIndex = 0;
            grvClientesNuevos.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaClientesNuevos"] != null)
            {
                grvClientesNuevos.DataSource = Session["grillaClientesNuevos"];
                grvClientesNuevos.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvClientesNuevos.PageIndex != 0)
                grvClientesNuevos.PageIndex--;
            grvClientesNuevos.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaClientesNuevos"] != null)
            {
                grvClientesNuevos.DataSource = Session["grillaClientesNuevos"];
                grvClientesNuevos.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvClientesNuevos.PageIndex != (grvClientesNuevos.PageCount - 1))
                grvClientesNuevos.PageIndex++;
            grvClientesNuevos.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {

            if (Session["grillaClientesNuevos"] != null)
            {
                grvClientesNuevos.DataSource = Session["grillaClientesNuevos"];
                grvClientesNuevos.DataBind();
            }
            else
            {
                buscarGrilla();
            }
            grvClientesNuevos.PageIndex = grvClientesNuevos.PageCount - 1;
            grvClientesNuevos.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvClientesNuevos.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvClientesNuevos.PageCount);
            }
        }



        protected void lbtnRut_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRut = (Label)grvClientesNuevos.Rows[row.RowIndex].FindControl("lbtnIdCliente");
                Response.Redirect("Default.aspx?c=" + _lblRut.Text);
                //buscarClientePorRut(_lblRut.Text.Trim());
                //TabContainer1.ActiveTab = tpCliente;
                //buscarDetalleCotizacion(_lblCotizacion.Text.Trim());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }




        protected void ibtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Session["grillaClientesNuevos"] as DataTable;
                Utilidad.ExportDataTableToExcel(dt, "Exporte_Clientes_Nuevos.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        
    }
}