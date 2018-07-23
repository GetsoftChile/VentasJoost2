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
    public partial class Seguimiento : System.Web.UI.Page
    {
        Datos dal = new Datos();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //scriptManager.RegisterPostBackControl(this.ibtnExportar);

                //if (!this.Page.IsPostBack)
                //{
                //    //Session["SortedView"] = null;
                //    Session["SortedViewSeguimiento"] = null;
                //    buscarVendedor();
                //    buscarSeguimiento();
                //}

                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl((Control)this.ibtnExportar);
                if (this.Page.IsPostBack)
                    return;
                this.Session["SortedViewSeguimiento"] = (object)null;
                this.buscarVendedor();
                string str = this.Session["variableIdUsuario"].ToString();
                if (this.Session["variablePerfil"].ToString() == "3")
                {
                    this.ddlVendedor.Enabled = false;
                    this.ddlVendedor.SelectedValue = str;
                }
                else
                {
                    this.ddlVendedor.Enabled = true;
                    this.ddlVendedor.SelectedValue = "0";
                }
                this.buscarSeguimiento();
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
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


        protected void btnBuscarSeguimiento_Click(object sender, EventArgs e)
        {
            try
            {
                buscarSeguimiento();
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

        void buscarSeguimiento()
        {
            //string vendedor = Session["variableIdUsuario"].ToString();
            //string perfil = Session["variablePerfil"].ToString();

            //if (perfil == "3")
            //{
            //    lblInfoVendedor.Visible = false;
            //    ddlVendedor.Visible = false;

            //    string montoCotizacionDesde = txtMontoCotizacionDesde.Text;
            //    string montoCotizacionHasta = txtMontoCotizacionHasta.Text;
            //    string fechaDesde = txtFechaDesde.Text.Replace("-", "/");
            //    string fechaHasta = txtFechaHasta.Text.Replace("-", "/");

            //    ds = dal.getBuscarSeguimiento(vendedor, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);
            //    grvSeguimiento.DataSource = ds;
            //    grvSeguimiento.DataBind();
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
            //    grvSeguimiento.DataSource = ds;
            //    grvSeguimiento.DataBind();
            //}

            string vendedor1 = this.Session["variableIdUsuario"].ToString();
            if (this.Session["variablePerfil"].ToString() == "3")
            {
                string text1 = this.txtMontoCotizacionDesde.Text;
                string text2 = this.txtMontoCotizacionHasta.Text;
                string fechaDesde = this.txtFechaDesde.Text.Replace("-", "/");
                string fechaHasta = this.txtFechaHasta.Text.Replace("-", "/");
                this.ds = this.dal.getBuscarSeguimiento(vendedor1, text1, text2, fechaDesde, fechaHasta);
                this.grvSeguimiento.DataSource = (object)this.ds;
                this.grvSeguimiento.DataBind();
            }
            else
            {
                string vendedor2 = this.ddlVendedor.SelectedValue;
                if (vendedor2 == "0")
                    vendedor2 = (string)null;
                string text1 = this.txtMontoCotizacionDesde.Text;
                string text2 = this.txtMontoCotizacionHasta.Text;
                string fechaDesde = this.txtFechaDesde.Text.Replace("-", "/");
                string fechaHasta = this.txtFechaHasta.Text.Replace("-", "/");
                this.ds = this.dal.getBuscarSeguimiento(vendedor2, text1, text2, fechaDesde, fechaHasta);
                this.grvSeguimiento.DataSource = (object)this.ds;
                this.grvSeguimiento.DataBind();
            }
        }

        protected void lbtnRut_Click(object sender, EventArgs e)
        {
            try
            {
                //LinkButton lbtn = sender as LinkButton;
                //GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                //Label _lblRut = (Label)grvSeguimiento.Rows[row.RowIndex].FindControl("lblRut");
                //Response.Redirect("Default.aspx?c=" + _lblRut.Text);
                ////buscarClientePorRut(_lblRut.Text.Trim());
                ////TabContainer1.ActiveTab = tpCliente;
                ////buscarDetalleCotizacion(_lblCotizacion.Text.Trim());

                GridViewRow namingContainer = (GridViewRow)(sender as LinkButton).NamingContainer;
                Label control = (Label)this.grvSeguimiento.Rows[namingContainer.RowIndex].FindControl("lblRut");
                this.Response.Redirect("Default.aspx?c=" + ((Label)this.grvSeguimiento.Rows[namingContainer.RowIndex].FindControl("lblIdCliente")).Text);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnDetalleCotizacionSeguimiento_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblCotizacion = (Label)grvSeguimiento.Rows[row.RowIndex].FindControl("lblCotizacion");

                buscarDetalleCotizacion(_lblCotizacion.Text.Trim());
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




        protected void imgGestionar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //ImageButton img = (ImageButton)sender;
            //    LinkButton lbtn = sender as LinkButton;
            //    ImageButton img = sender as ImageButton;
            //    GridViewRow row = (GridViewRow)img.NamingContainer;
            //    Label _lblRut = (Label)grvSeguimiento.Rows[row.RowIndex].FindControl("lblRut");
            //    //Label _lblCodSapAgendamiento = (Label)grvAgendamientos.Rows[row.RowIndex].FindControl("lblRut");

            //    buscarClientePorRut(_lblRut.Text.Trim());
            //    //DataTable dt = bus.getBuscarEmpresaPorNombreRut(_lblCodSap.Text.Trim(), _lblCodSap.Text.Trim(), _lblCodSap.Text.Trim(), _lblCodSap.Text.Trim()).Tables[0];
            //    //foreach (DataRow item in dt.Rows)
            //    //{
            //    //    lblCodSap.Text = item["CP_COD_SAP"].ToString();

            //    //    lblRut.Text = item["CP_RUT"].ToString();
            //    //    lblRazonSocial.Text = item["CP_RAZON"].ToString();
            //    //    lblDireccion.Text = item["CP_DIRECCION"].ToString();
            //    //    lblNombreContacto.Text = item["CP_NOMBRE_CONTACTO"].ToString();
            //    //    lblTelefono.Text = item["CP_CONTACTO_TELEFONO"].ToString();
            //    //    lblCorreo.Text = item["CP_CONTACTO_TELEFONO"].ToString();

            //    //    if (item["CP_CIERRE_Q"].ToString() == string.Empty)
            //    //    {
            //    //        lblTasaCierreQ.Text = "0%";
            //    //    }
            //    //    else
            //    //    {
            //    //        double tasaCierreQ = Convert.ToDouble(item["CP_CIERRE_Q"].ToString());
            //    //        lblTasaCierreQ.Text = Convert.ToString(tasaCierreQ * 100) + "%";
            //    //    }

            //    //    if (item["CP_CIERRE_PESO"].ToString() == string.Empty)
            //    //    {
            //    //        lblTasaCierrePeso.Text = "0%";
            //    //    }
            //    //    else
            //    //    {
            //    //        double tasaCierreP = Convert.ToDouble(item["CP_CIERRE_PESO"].ToString());
            //    //        lblTasaCierrePeso.Text = Convert.ToString(tasaCierreP * 100) + "%";
            //    //    }

            //    //    lblTasaCierrePeso.Text = item["CP_CIERRE_PESO"].ToString();
            //    //    lblEstadoCobranza.Text = item["CP_ESTADO_COBRA"].ToString();
            //    //    lblCredito.Text = item["CP_CREDITO"].ToString();
            //    //    lblCondicion.Text = item["CP_CONDICION"].ToString();
            //    //    lblTipoCliente.Text = item["cp_clasificacion"].ToString();

            //    //    lblRubro.Text = item["cp_rubro"].ToString();
            //    //    lblZona.Text = item["cp_zona"].ToString();
            //    //}


            //    //tablaCliente.Visible = true;
            //    //tablaComercial.Visible = true;

            //    //lblRutGestiones.Text = lblRut.Text;
            //    //lblRazonSocialGestiones.Text = lblRazonSocial.Text;

            //    //string tasaCierreDesde = txtTasaCierreDesde.Text;
            //    //string tasaCierreHasta = txtTasaCierreHasta.Text;
            //    string montoCotizacionDesde = txtMontoCotizacionDesde.Text;
            //    string montoCotizacionHasta = txtMontoCotizacionHasta.Text;
            //    string fechaDesde = txtFechaDesde.Text.Replace("-", "/");
            //    string fechaHasta = txtFechaHasta.Text.Replace("-", "/");

            //    //buscarGestion(_lblCodSap.Text, tasaCierreDesde, tasaCierreHasta, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);

            //    buscarGestion(_lblRut.Text, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);
            //    TabContainer1.ActiveTab = tpGestion;
            //    buscarGestionesCRM();

            //}
            //catch (Exception ex)
            //{
            //    lblInformacion.Text = ex.Message;
            //    mdlInformacion.Show();
            //}
        }


        //paginamiento grilla seguimiento

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarSeguimiento();
            }

            grvSeguimiento.PageIndex = 0;
            grvSeguimiento.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarSeguimiento();
            }

            if (grvSeguimiento.PageIndex != 0)
                grvSeguimiento.PageIndex--;
            grvSeguimiento.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarSeguimiento();
            }

            if (grvSeguimiento.PageIndex != (grvSeguimiento.PageCount - 1))
                grvSeguimiento.PageIndex++;
            grvSeguimiento.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {

            if (Session["SortedViewSeguimiento"] != null)
            {
                grvSeguimiento.DataSource = Session["SortedViewSeguimiento"];
                grvSeguimiento.DataBind();
            }
            else
            {
                buscarSeguimiento();
            }
            grvSeguimiento.PageIndex = grvSeguimiento.PageCount - 1;
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label _lblIdEstadoCotizacion = (Label)e.Row.FindControl("lblIdEstadoCotizacion");
                ImageButton _imgPdf = (ImageButton)e.Row.FindControl("imgPdf");
                if (_lblIdEstadoCotizacion.Text == "1" || _lblIdEstadoCotizacion.Text == "2" || _lblIdEstadoCotizacion.Text == "3" ) 
                {
                    _imgPdf.Visible = true;
                }
                else
                {
                    _imgPdf.Visible = false;
                }
            }
        }


        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            buscarSeguimiento();

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
                Session["SortedViewSeguimiento"] = sortedView;
                grvSeguimiento.DataSource = sortedView;
                grvSeguimiento.DataBind();

                //hidTAB.Value = "#seguimiento";
                //hfSeguimiento.Value = "1";
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


        protected void ibtnIrAGestion_Click(object sender, EventArgs e)
        {
            try
            {
                //ImageButton lbtn = sender as ImageButton;
                //GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                //Label _lblRut = (Label)grvSeguimiento.Rows[row.RowIndex].FindControl("lblRut");

                //Response.Redirect("Default.aspx?c=" + _lblRut.Text + "&t=1&seguimiento=1");
                ////buscarClientePorRut(_lblRut.Text.Trim());
                ////TabContainer1.ActiveTab = tpCliente;
                ////buscarDetalleCotizacion(_lblCotizacion.Text.Trim());

                GridViewRow namingContainer = (GridViewRow)(sender as ImageButton).NamingContainer;
                Label control = (Label)this.grvSeguimiento.Rows[namingContainer.RowIndex].FindControl("lblRut");
                this.Response.Redirect("Default.aspx?c=" + ((Label)this.grvSeguimiento.Rows[namingContainer.RowIndex].FindControl("lblIdCliente")).Text + "&t=1&seguimiento=1");
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
                //string vendedor = Session["variableIdUsuario"].ToString();
                //string perfil = Session["variablePerfil"].ToString();

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

                string vendedor1 = this.Session["variableIdUsuario"].ToString();
                if (this.Session["variablePerfil"].ToString() == "3")
                {
                    this.lblInfoVendedor.Visible = false;
                    string text1 = this.txtMontoCotizacionDesde.Text;
                    string text2 = this.txtMontoCotizacionHasta.Text;
                    string fechaDesde = this.txtFechaDesde.Text.Replace("-", "/");
                    string fechaHasta = this.txtFechaHasta.Text.Replace("-", "/");
                    this.ds = this.dal.getBuscarSeguimiento(vendedor1, text1, text2, fechaDesde, fechaHasta);
                    Utilidad.ExportDataTableToExcel(this.ds.Tables[0], "Exporte_Seguimiento.xls", "", "", "", "");
                }
                else
                {
                    string vendedor2 = this.ddlVendedor.SelectedValue;
                    if (vendedor2 == "0")
                        vendedor2 = (string)null;
                    string text1 = this.txtMontoCotizacionDesde.Text;
                    string text2 = this.txtMontoCotizacionHasta.Text;
                    string fechaDesde = this.txtFechaDesde.Text.Replace("-", "/");
                    string fechaHasta = this.txtFechaHasta.Text.Replace("-", "/");
                    this.ds = this.dal.getBuscarSeguimiento(vendedor2, text1, text2, fechaDesde, fechaHasta);
                    Utilidad.ExportDataTableToExcel(this.ds.Tables[0], "Exporte_Seguimiento.xls", "", "", "", "");
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnIdCliente_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Default.aspx?c=" + ((Label)this.grvSeguimiento.Rows[((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex].FindControl("lblIdCliente")).Text);
            }
            catch (Exception ex)
            {
                this.lblInformacion.Text = ex.Message;
                this.mdlInformacion.Show();
            }
        }


    }
}