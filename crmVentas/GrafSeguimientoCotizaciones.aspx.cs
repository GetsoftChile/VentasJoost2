using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using InfoSoftGlobal;
using System.Data;
using System.Text;
using DataTableToExcel;

namespace crm_valvulas_industriales
{
    public partial class GrafSeguimientoCotizaciones : System.Web.UI.Page
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

                    buscarVendedorIndicadores();
                    buscar();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlVendedorIndicador_DataBound(object sender, EventArgs e)
        {
            ddlVendedorIndicador.Items.Insert(0, new ListItem("Todos", "0"));
        }

        void buscarVendedorIndicadores()
        {
            ddlVendedorIndicador.DataSource = dal.getBuscarUsuario(null, null);
            ddlVendedorIndicador.DataValueField = "ID_USUARIO";
            ddlVendedorIndicador.DataTextField = "USUARIO";
            ddlVendedorIndicador.DataBind();
        }


        protected void lbtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                buscar();
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

            dt = dal.getGrafSeguimientoCotizacionesCliente(ddlVendedorIndicador.SelectedValue, txtFechaDesde.Text, txtFechaHasta.Text).Tables[0];

            xmlGrafico.Append("<chart caption='Seguimiento' xAxisName='Fecha' yAxisName='Q'>");
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

            xmlGrafico.Append("<dataset seriesName='Cotizaciones con ges'>");
            foreach (DataRow item in dt.Rows)
            {
                xmlGrafico.Append("<set value='" + item["COTIZACIONES_CON_GESTION"].ToString() + "'/>");
            }
            xmlGrafico.Append("</dataset>");

            xmlGrafico.Append("<dataset seriesName='Clientes con ges'>");
            foreach (DataRow item in dt.Rows)
            {
                xmlGrafico.Append("<set value='" + item["CLIENTES_CON_GESTION"].ToString() + "'/>");
            }
            xmlGrafico.Append("</dataset>");

            xmlGrafico.Append("</chart>");

            litSeguimientoCotizaciones.Text = FusionCharts.RenderChart("assets/FusionCharts/ScrollColumn2D.swf", "", xmlGrafico.ToString(), "generalGanadasPerdidasPorVendedor", "1000", "400", false, true);

            buscarGrilla();
        }

        void buscarGrilla() 
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarSeguimiento(ddlVendedorIndicador.SelectedValue, null, null, txtFechaDesde.Text, txtFechaHasta.Text).Tables[0];
            Session["SortedViewGrvSeguimiento"] = dt;
            grvSeguimiento.DataSource = dt;
            grvSeguimiento.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvSeguimiento.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvSeguimiento.PageCount);
            }
        }


        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscarGrilla();
            if (Session["SortedViewGrvSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewGrvSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            grvSeguimiento.PageIndex = 0;
            grvSeguimiento.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscarGrilla();
            if (Session["SortedViewGrvSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewGrvSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvSeguimiento.PageIndex != 0)
                grvSeguimiento.PageIndex--;
            grvSeguimiento.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscarGrilla();
            if (Session["SortedViewGrvSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewGrvSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvSeguimiento.PageIndex != (grvSeguimiento.PageCount - 1))
                grvSeguimiento.PageIndex++;
            grvSeguimiento.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {

            if (Session["SortedViewGrvSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewGrvSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarGrilla();
            }
            grvSeguimiento.PageIndex = grvSeguimiento.PageCount - 1;
            grvSeguimiento.DataBind();
        }

        protected void ibtnIrAGestion_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRut = (Label)grvSeguimiento.Rows[row.RowIndex].FindControl("lblRut");

                Response.Redirect("Default.aspx?c=" + _lblRut.Text + "&t=1");
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


        protected void imgPdf_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaPdf = (Label)grvSeguimiento.Rows[row.RowIndex].FindControl("lblRutaPdf");
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdf.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnRut_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRut = (Label)grvSeguimiento.Rows[row.RowIndex].FindControl("lblRut");
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
                string vendedor = Session["variableIdUsuario"].ToString();
                string perfil = Session["variablePerfil"].ToString();
                DataTable dt = new DataTable();
                dt = dal.getBuscarSeguimientoExporte(ddlVendedorIndicador.SelectedValue, null, null, txtFechaDesde.Text, txtFechaHasta.Text).Tables[0];
                Utilidad.ExportDataTableToExcel(dt, "Exporte_Seguimiento.xls", "", "", "", "");

                //if (perfil == "3")
                //{
                //    lblInfoVendedor.Visible = false;
                //    ddlVendedor.Visible = false;

                //    string montoCotizacionDesde = txtMontoCotizacionDesde.Text;
                //    string montoCotizacionHasta = txtMontoCotizacionHasta.Text;
                //    string fechaDesde = txtFechaDesde.Text.Replace("-", "/");
                //    string fechaHasta = txtFechaHasta.Text.Replace("-", "/");

                //    ds = dal.getBuscarSeguimiento(vendedor, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);
                //    Utilidad.ExportDataTableToExcel(ds.Tables[0], "Exporte_Seguimiento.xls", "", "", "", "");
                //}
                //else
                //{
                //    vendedor = ddlVendedor.SelectedValue;

                //    if (vendedor == "0")
                //    {
                //        vendedor = null;
                //    }

                //    string montoCotizacionDesde = txtMontoCotizacionDesde.Text;
                //    string montoCotizacionHasta = txtMontoCotizacionHasta.Text;
                //    string fechaDesde = txtFechaDesde.Text.Replace("-", "/");
                //    string fechaHasta = txtFechaHasta.Text.Replace("-", "/");

                //    ds = dal.getBuscarSeguimiento(vendedor, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);
                //    Utilidad.ExportDataTableToExcel(ds.Tables[0], "Exporte_Seguimiento.xls", "", "", "", "");
                //}
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


    }
}