using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using DataTableToExcel;

namespace crm_valvulas_industriales
{
    public partial class NotasDeVentas : System.Web.UI.Page
    {
        Datos dal = new Datos();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.ibtnExportarExcel);

                if (!this.Page.IsPostBack)
                {
                    buscarVendedor();
                    buscarEmpresa();
                    buscar();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscar() {
            string idVendedor = ddlVendedor.SelectedValue;
            string fechaDesde = txtFechaDesde.Text.Trim();
            string fechaHasta = txtFechaHasta.Text.Trim();
            string idEmpresa = ddlEmpresa.SelectedValue;
            int conFact = Convert.ToInt32(ddlFacturado.SelectedValue);
            ds = dal.getBuscarNotaVentaParametros(idVendedor, fechaDesde, fechaHasta, idEmpresa, conFact);
            grvNotaVenta.DataSource = ds;
            grvNotaVenta.DataBind();

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
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

        protected void ddlVendedor_DataBound(object sender, EventArgs e)
        {
            ddlVendedor.Items.Insert(0, new ListItem("Todos", "0"));
        }

        void buscarVendedor()
        {
            ddlVendedor.DataSource = dal.getBuscarUsuario(null, null);
            ddlVendedor.DataValueField = "ID_USUARIO";
            ddlVendedor.DataTextField = "USUARIO";
            ddlVendedor.DataBind();
        }

        void buscarEmpresa()
        {
            string idUsuario = Session["variableIdUsuario"].ToString();
            ddlEmpresa.DataSource = dal.getBuscarEmpresa(null, null);
            ddlEmpresa.DataTextField = "NOMBRE_EMPRESA";
            ddlEmpresa.DataValueField = "ID_EMPRESA";
            ddlEmpresa.DataBind();
        }

        protected void ddlEmpresa_DataBound(object sender, EventArgs e)
        {
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        //paginamiento grilla seguimiento

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewNotaDeVentas"] != null)
            {

                grvNotaVenta.DataSource = Session["SortedViewNotaDeVentas"];
                grvNotaVenta.DataBind();
            }
            else
            {
                buscar();
            }

            grvNotaVenta.PageIndex = 0;
            grvNotaVenta.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewNotaDeVentas"] != null)
            {
                grvNotaVenta.DataSource = Session["SortedViewNotaDeVentas"];
                grvNotaVenta.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvNotaVenta.PageIndex != 0)
                grvNotaVenta.PageIndex--;
            grvNotaVenta.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewNotaDeVentas"] != null)
            {
                grvNotaVenta.DataSource = Session["SortedViewNotaDeVentas"];
                grvNotaVenta.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvNotaVenta.PageIndex != (grvNotaVenta.PageCount - 1))
                grvNotaVenta.PageIndex++;
            grvNotaVenta.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {

            if (Session["SortedViewNotaDeVentas"] != null)
            {
                grvNotaVenta.DataSource = Session["SortedViewNotaDeVentas"];
                grvNotaVenta.DataBind();
            }
            else
            {
                buscar();
            }
            grvNotaVenta.PageIndex = grvNotaVenta.PageCount - 1;
            grvNotaVenta.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string idPerfil = Session["variablePerfil"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label _lblRutaOrdenDeCompra = (Label)e.Row.FindControl("lblRutaOrdenDeCompra");
                ImageButton _ibtnRutaOrdenCompra = (ImageButton)e.Row.FindControl("ibtnRutaOrdenCompra");
                //ImageButton _imgEliminar = (ImageButton)e.Row.FindControl("imgEliminar");
                if (_lblRutaOrdenDeCompra.Text == string.Empty)
                {
                    _ibtnRutaOrdenCompra.Visible = false;
                }

                //if (idPerfil == "1")
                //{
                //    _imgEliminar.Visible = true;
                //}
                //else
                //{
                //    _imgEliminar.Visible = false;
                //}
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvNotaVenta.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvNotaVenta.PageCount);
            }
            
        }
        
        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }

        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            buscar();

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            {
                string SortDir = string.Empty;
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                Session["SortedViewNotaDeVentas"] = sortedView;

                grvNotaVenta.DataSource = sortedView;
                grvNotaVenta.DataBind();

                //hidTAB.Value = "#seguimiento";
                //hfSeguimiento.Value = "1";
            }
        }



        protected void lbtnDetalleCotizacionCotizacionesCRMNv_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdCotizacion = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdCotizacion");

                buscarDetalleCotizacion(_lblIdCotizacion.Text.Trim());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarDetalleCotizacion(string cotizacion)
        {
            lblNumeroCotizacionDetalle.Text = "Cotización Nro. " + cotizacion;
            grvDetalleCotizacion.DataSource = dal.getBuscarDetalleCotizacion(cotizacion);
            grvDetalleCotizacion.DataBind();
            mdlDetalleCotizacion.Show();
        }

        protected void ibtnRutaOrdenCompra_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaOrdenDeCompra = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblRutaOrdenDeCompra");
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaOrdenDeCompra.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void imgPdfNotaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaPdf = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblRutaPdf");
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdf.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string idVendedor = ddlVendedor.SelectedValue;
                string fechaDesde = txtFechaDesde.Text.Trim();
                string fechaHasta = txtFechaHasta.Text.Trim();
                string idEmpresa = ddlEmpresa.SelectedValue;
                int conFact = Convert.ToInt32(ddlFacturado.SelectedValue);
                ds = dal.getBuscarNotaVentaParametros(idVendedor, fechaDesde, fechaHasta, idEmpresa, conFact);

                Utilidad.ExportDataTableToExcel(ds.Tables[0], "Exporte_NV.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
            
        }
    }
}