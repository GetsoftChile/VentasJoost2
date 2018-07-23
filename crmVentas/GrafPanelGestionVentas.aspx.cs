using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Text;
using InfoSoftGlobal;
using DataTableToExcel;

namespace crm_valvulas_industriales
{
    public partial class GrafPanelGestionVentas : System.Web.UI.Page
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
                    //txtFechaDesde.Text = (hoy.AddDays(-7).ToString("dd-MM-yyyy"));
                    txtFechaDesde.Text = hoy.ToString("dd-MM-yyyy");
                    txtFechaHasta.Text = hoy.ToString("dd-MM-yyyy");


                    buscarVendedorIndicadores();
                    actividadComercial();

                    string idUsuario = Session["variableIdUsuario"].ToString();
                    string idPerfil = Session["variablePerfil"].ToString();
                    if (idPerfil == "3")
                    {
                        ddlVendedorIndicador.SelectedValue = idUsuario;
                        ddlVendedorIndicador.Enabled = false;
                    }

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

        protected void ddlVendedorIndicador_DataBound(object sender, EventArgs e)
        {
            ddlVendedorIndicador.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ddlActividadComercial_DataBound(object sender, EventArgs e)
        {
            ddlActividadComercial.Items.Insert(0, new ListItem("Todos", "0"));
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

            dt = dal.getGraficoGestionVentas(txtFechaDesde.Text, txtFechaHasta.Text, ddlVendedorIndicador.SelectedValue, ddlActividadComercial.SelectedValue).Tables[0];

            xmlGrafico.Append("<chart caption='Cantidad'>");
            foreach (DataRow item in dt.Rows)
            {
                xmlGrafico.Append("<set value='" + item["CANT"].ToString() + "' label='" + item["TIPO"].ToString() + "'/>");
            }
            xmlGrafico.Append("</chart>");

            litCantidad.Text = FusionCharts.RenderChart("assets/FusionCharts/Column3D.swf", "", xmlGrafico.ToString(), "Cantidad", "540", "250", false, true);

            StringBuilder xmlGrafico2 = new StringBuilder();
            xmlGrafico2.Append("<chart caption='Pesos'>");
            foreach (DataRow item in dt.Rows)
            {
                string monto = "";
                if (item["MONTO"].ToString() != string.Empty)
                {
                    monto = Convert.ToInt64(Convert.ToDecimal(item["MONTO"].ToString())).ToString();
                }
                
                string tipo = item["TIPO"].ToString();
                xmlGrafico2.Append("<set value='" + monto + "' label='" + tipo + "'/>");
            }
            xmlGrafico2.Append("</chart>");
            litPesos.Text = FusionCharts.RenderChart("assets/FusionCharts/Column3D.swf", "", xmlGrafico2.ToString(), "Pesos", "540", "250", false, true);
            //litClientesNuevos.Text = FusionCharts.RenderChart("assets/FusionCharts/ScrollColumn2D.swf", "", xmlGrafico.ToString(), "generalGanadasPerdidasPorVendedor", "1000", "400", false, true);
            buscarGrilla();
        }


        protected void ibtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Session["grillaGestionesVentas"] as DataTable;
                Utilidad.ExportDataTableToExcel(dt, "Exporte_Gestiones.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarGrilla() 
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarGestionDeVentaDetalle(txtFechaDesde.Text, txtFechaHasta.Text, ddlVendedorIndicador.SelectedValue,ddlActividadComercial.SelectedValue).Tables[0];
            Session["grillaGestionesVentas"] = dt;
            grvGestionVenas.DataSource = dt;
            grvGestionVenas.DataBind();

            DataTable dtFactura = new DataTable();
            dtFactura = dal.getBuscarFactura(txtFechaDesde.Text, txtFechaHasta.Text, ddlVendedorIndicador.SelectedValue, ddlActividadComercial.SelectedValue).Tables[0];
            Session["grillaFacturas"] = dtFactura;
            grvFacturas.DataSource = dtFactura;
            grvFacturas.DataBind();

        }

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaGestionesVentas"] != null)
            {
                grvGestionVenas.DataSource = Session["grillaGestionesVentas"];
                grvGestionVenas.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            grvGestionVenas.PageIndex = 0;
            grvGestionVenas.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaGestionesVentas"] != null)
            {
                grvGestionVenas.DataSource = Session["grillaGestionesVentas"];
                grvGestionVenas.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvGestionVenas.PageIndex != 0)
                grvGestionVenas.PageIndex--;
            grvGestionVenas.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaGestionesVentas"] != null)
            {
                grvGestionVenas.DataSource = Session["grillaGestionesVentas"];
                grvGestionVenas.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvGestionVenas.PageIndex != (grvGestionVenas.PageCount - 1))
                grvGestionVenas.PageIndex++;
            grvGestionVenas.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {

            if (Session["grillaGestionesVentas"] != null)
            {
                grvGestionVenas.DataSource = Session["grillaGestionesVentas"];
                grvGestionVenas.DataBind();
            }
            else
            {
                buscarGrilla();
            }
            grvGestionVenas.PageIndex = grvGestionVenas.PageCount - 1;
            grvGestionVenas.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvGestionVenas.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvGestionVenas.PageCount);
            }
        }

        protected void lbtnRut_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRut = (Label)grvGestionVenas.Rows[row.RowIndex].FindControl("lblRut");
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


        protected void lbtnIdCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdCotizacion = (Label)grvGestionVenas.Rows[row.RowIndex].FindControl("lblIdCotizacion");
                Label _lblRutaPdf = (Label)grvGestionVenas.Rows[row.RowIndex].FindControl("lblRutaPdf");

                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdf.Text + "','_blank');", true);

                //Response.Redirect("Default.aspx?c=" + _lblRut.Text);
                ////buscarClientePorRut(_lblRut.Text.Trim());
                ////TabContainer1.ActiveTab = tpCliente;
                ////buscarDetalleCotizacion(_lblCotizacion.Text.Trim());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void lbtnIdNotaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdNotaVenta = (Label)grvGestionVenas.Rows[row.RowIndex].FindControl("lblIdNotaVenta");
                Label _lblRutaPdfNV = (Label)grvGestionVenas.Rows[row.RowIndex].FindControl("lblRutaPdfNV");

                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdfNV.Text + "','_blank');", true);

                //Response.Redirect("Default.aspx?c=" + _lblRut.Text);
                ////buscarClientePorRut(_lblRut.Text.Trim());
                ////TabContainer1.ActiveTab = tpCliente;
                ////buscarDetalleCotizacion(_lblCotizacion.Text.Trim());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void actividadComercial()
        {
            ddlActividadComercial.DataSource = dal.getBuscarActividadComercial();
            ddlActividadComercial.DataValueField = "ID_ACTIVIDAD_COMERCIAL";
            ddlActividadComercial.DataTextField = "NOM_ACTIVIDAD_COMERCIAL";
            ddlActividadComercial.DataBind();
        }

        protected void lbtnExportarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Session["grillaFacturas"] as DataTable;
                Utilidad.ExportDataTableToExcel(dt, "Exporte_Facturas.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgFirstFactura_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaFacturas"] != null)
            {
                grvFacturas.DataSource = Session["grillaFacturas"];
                grvFacturas.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            grvFacturas.PageIndex = 0;
            grvFacturas.DataBind();
        }

        protected void imgPrevFactura_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaFacturas"] != null)
            {
                grvFacturas.DataSource = Session["grillaFacturas"];
                grvFacturas.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvFacturas.PageIndex != 0)
                grvFacturas.PageIndex--;
            grvFacturas.DataBind();
        }

        protected void imgNextFactura_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["grillaFacturas"] != null)
            {
                grvFacturas.DataSource = Session["grillaFacturas"];
                grvFacturas.DataBind();
            }
            else
            {
                buscarGrilla();
            }

            if (grvFacturas.PageIndex != (grvFacturas.PageCount - 1))
                grvFacturas.PageIndex++;
            grvFacturas.DataBind();
        }

        protected void imgLastFactura_Click(object sender, EventArgs e)
        {

            if (Session["grillaFacturas"] != null)
            {
                grvFacturas.DataSource = Session["grillaFacturas"];
                grvFacturas.DataBind();
            }
            else
            {
                buscarGrilla();
            }
            grvFacturas.PageIndex = grvGestionVenas.PageCount - 1;
            grvFacturas.DataBind();
        }

        protected void paginacionFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvGestionVenas.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvGestionVenas.PageCount);
            }
        }

        

    }
}