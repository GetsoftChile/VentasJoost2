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
    public partial class OT : System.Web.UI.Page
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


        void buscar()
        {
            string idVendedor = ddlVendedor.SelectedValue;
            string fechaDesde = txtFechaDesde.Text.Trim();
            string fechaHasta = txtFechaHasta.Text.Trim();
            string idEmpresa = ddlEmpresa.SelectedValue;
            ds = dal.getBuscarOTParametros(idVendedor, fechaDesde, fechaHasta, idEmpresa);
            grvOT.DataSource = ds;
            grvOT.DataBind();
        }

        protected void ibtnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string idVendedor = ddlVendedor.SelectedValue;
                string fechaDesde = txtFechaDesde.Text.Trim();
                string fechaHasta = txtFechaHasta.Text.Trim();
                string idEmpresa = ddlEmpresa.SelectedValue;
               
                ds = dal.getBuscarOTParametros(idVendedor, fechaDesde, fechaHasta, idEmpresa);

                Utilidad.ExportDataTableToExcel(ds.Tables[0], "Exporte_OT.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }

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


        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string idPerfil = Session["variablePerfil"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "warning";
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvOT.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvOT.PageCount);
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
                Session["SortedViewOT"] = sortedView;

                grvOT.DataSource = sortedView;
                grvOT.DataBind();

                //hidTAB.Value = "#seguimiento";
                //hfSeguimiento.Value = "1";
            }
        }


        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewOT"] != null)
            {

                grvOT.DataSource = Session["SortedViewOT"];
                grvOT.DataBind();
            }
            else
            {
                buscar();
            }

            grvOT.PageIndex = 0;
            grvOT.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewOT"] != null)
            {
                grvOT.DataSource = Session["SortedViewOT"];
                grvOT.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvOT.PageIndex != 0)
                grvOT.PageIndex--;
            grvOT.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscarSeguimiento();
            if (Session["SortedViewOT"] != null)
            {
                grvOT.DataSource = Session["SortedViewOT"];
                grvOT.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvOT.PageIndex != (grvOT.PageCount - 1))
                grvOT.PageIndex++;
            grvOT.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {

            if (Session["SortedViewOT"] != null)
            {
                grvOT.DataSource = Session["SortedViewOT"];
                grvOT.DataBind();
            }
            else
            {
                buscar();
            }
            grvOT.PageIndex = grvOT.PageCount - 1;
            grvOT.DataBind();
        }


        protected void lbtnDetalleCotizacionCotizacionesCRMNv_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdCotizacion = (Label)grvOT.Rows[row.RowIndex].FindControl("lblIdCotizacion");
                
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEliminarOT_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdOrdenDeTrabajo = (Label)grvOT.Rows[row.RowIndex].FindControl("lblIdOrdenDeTrabajo");
                dal.setEliminarOT(_lblIdOrdenDeTrabajo.Text);
                buscar();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        protected void imgPdfOT_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaPdf = (Label)grvOT.Rows[row.RowIndex].FindControl("lblRutaPdf");

                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdf.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

    }
}