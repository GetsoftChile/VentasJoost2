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

        double totalGridview = 0;
        double totalGridviewMontoIva = 0;
        double totalGridviewMontoTotal = 0;
        double totalGridviewMontoSaldo = 0;
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

                double rightBonus = Convert.ToDouble(((Label)e.Row.FindControl("lblMontoNeto")).Text);
                totalGridview += rightBonus;

                double _lblMontoIva = Convert.ToDouble(((Label)e.Row.FindControl("lblMontoIva")).Text);
                totalGridviewMontoIva += _lblMontoIva;

                double _lblMontoTotal = Convert.ToDouble(((Label)e.Row.FindControl("lblMontoTotal")).Text);
                totalGridviewMontoTotal += _lblMontoTotal;

                double _lblSaldo = Convert.ToDouble(((Label)e.Row.FindControl("lblSaldo")).Text);
                totalGridviewMontoSaldo += _lblSaldo;
                
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvNotaVenta.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvNotaVenta.PageCount);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label _lblTotalMontoNeto = (Label)e.Row.FindControl("lblTotalMontoNeto");
                _lblTotalMontoNeto.Text = totalGridview.ToString("n0");

                Label _lblTotalMontoIVA = (Label)e.Row.FindControl("lblTotalMontoIVA");
                _lblTotalMontoIVA.Text = totalGridviewMontoIva.ToString("n0");

                Label _lblTotalMontoTOTAL = (Label)e.Row.FindControl("lblTotalMontoTOTAL");
                _lblTotalMontoTOTAL.Text = totalGridviewMontoTotal.ToString("n0");

                Label _lblTotalMontoSaldo = (Label)e.Row.FindControl("lblTotalMontoSaldo");
                _lblTotalMontoSaldo.Text = totalGridviewMontoTotal.ToString("n0");
                
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

        protected void ibtnAgregarFactura_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdNotaVenta = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdNotaVenta");
                Label _lblMontoNeto = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblMontoNeto2");
                Label _lblRutCliente = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblRutCliente");
                txtRutCliente.Text = _lblRutCliente.Text;
                //formaPago();
                ddlFormaPago.ClearSelection();
                txtIdFactura.Text = string.Empty;
                txtIdFactura.Enabled = true;
                txtFechaFacturacion.Text = string.Empty;
                txtMontoNeto.Text = string.Empty;

                hfIdNotaVenta.Value = _lblIdNotaVenta.Text;
                txtMontoNeto.Text = _lblMontoNeto.Text.Replace(".", "");
                mdlAgregarFactura.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                string idUsuarioCreacion = this.Session["variableIdUsuario"].ToString();
                string text = this.txtRutCliente.Text;
                int int32 = Convert.ToInt32(this.Session["IdCliente"]);
                this.dal.setIngresarFactura(this.txtIdFactura.Text, text, this.hfIdNotaVenta.Value, this.txtFechaFacturacion.Text, this.txtMontoNeto.Text, "1", idUsuarioCreacion, this.ddlFormaPago.SelectedValue, int32);
                //this.factura();
                this.mdlAgregarFactura.Hide();

                buscar();

                lblInformacion.Text = "Listo, factura Agregada!";
                mdlInformacion.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlFormaPago_DataBound(object sender, EventArgs e)
        {
            ddlFormaPago.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void ibtnVerCliente_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblRut.Text = string.Empty;
                lblRazonSocial.Text = string.Empty;
                lblCondiciondeVenta.Text = string.Empty;
                lblClasificacion.Text = string.Empty;
                lblGiro.Text = string.Empty;
                lblEstado.Text = string.Empty;
                lblUsuarioAsig.Text = string.Empty;
                lblMontoCredito.Text = string.Empty;
                lblComuna.Text = string.Empty;
                lblCiudad.Text = string.Empty;
                lblMontoVenta12Meses.Text = string.Empty;
                lblTotalCotizado.Text = string.Empty;

                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdCliente = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdCliente");

                DataTable dt = new DataTable();
                dt = dal.getBuscarClientePorId(_lblIdCliente.Text).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    lblRut.Text = item["RUT_CLIENTE"].ToString();
                    lblRazonSocial.Text = item["RAZON_SOCIAL"].ToString();
                    lblCondiciondeVenta.Text = item["CONDICION_VENTA"].ToString();
                    lblClasificacion.Text = item["CLASIFICACION"].ToString();
                    lblGiro.Text = item["GIRO"].ToString();
                    lblEstado.Text = item["NOM_ESTADO_CLIENTE"].ToString();
                    lblUsuarioAsig.Text = item["USUARIO"].ToString();
                    lblMontoCredito.Text = item["MONTO_CREDITO"].ToString();
                    lblComuna.Text = item["COMUNA"].ToString();
                    lblCiudad.Text = item["CIUDAD"].ToString();
                    lblMontoVenta12Meses.Text = item["MONTO_VENTA_ULTIMO_12"].ToString();
                    lblTotalCotizado.Text = item["MONTO_COTIZADO"].ToString();
                }

                mdlVerCliente.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}