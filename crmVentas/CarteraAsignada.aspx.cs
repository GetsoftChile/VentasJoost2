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
    public partial class CarteraAsignada : System.Web.UI.Page
    {

        Datos dal = new Datos();
        DataSet dsClientes = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.ibtnExportarExcel);
                
                if (!this.Page.IsPostBack)
                {
                    Session["SortedView"] = null;
                    buscarVendedor();
                    validarPerfilParaComboEjecutivo();
                    buscarCampana();
                    buscar();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void validarPerfilParaComboEjecutivo()
        {
            string idUsuario = Session["variableIdUsuario"].ToString();

            foreach (DataRow item in dal.getBuscarUsuario(null,idUsuario).Tables[0].Rows)
            {
                if (item["ID_PERFIL"].ToString() != "3")
                {
                    ddlEjecutivo.Enabled = true;
                    break;
                }
                else
                {
                    ddlEjecutivo.SelectedValue = Session["variableIdUsuario"].ToString();
                    ddlEjecutivo.Enabled = false;
                }
            }
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

        void buscarCampana()
        {
            ddlCampana.DataSource = dal.getBuscarCampana(null,"1");
            ddlCampana.DataValueField = "ID_CAMPANA";
            ddlCampana.DataTextField = "NOM_CAMPANA";
            ddlCampana.DataBind();
        }

        protected void ibtnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dal.getBuscarCarteraAsignadaExporte(ddlEjecutivo.SelectedValue, ddlCampana.SelectedValue).Tables[0];
                Utilidad.ExportDataTableToExcel(dt, "Exporte_CarteraAsignada.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvClientes.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvClientes.PageCount);
            }
        }

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            if (Session["SortedView"] != null)
            {
                grvClientes.DataSource = Session["SortedView"];
                grvClientes.DataBind();
            }
            else
            {
                buscar();
            }

            grvClientes.PageIndex = 0;
            grvClientes.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            if (Session["SortedView"] != null)
            {
                grvClientes.DataSource = Session["SortedView"];
                grvClientes.DataBind();
            }
            else
            {
                buscar();
            }
            if (grvClientes.PageIndex != 0)
                grvClientes.PageIndex--;
            grvClientes.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            if (Session["SortedView"] != null)
            {
                grvClientes.DataSource = Session["SortedView"];
                grvClientes.DataBind();
            }
            else
            {
                buscar();
            }
            if (grvClientes.PageIndex != (grvClientes.PageCount - 1))
                grvClientes.PageIndex++;
            grvClientes.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            if (Session["SortedView"] != null)
            {
                grvClientes.DataSource = Session["SortedView"];
                grvClientes.DataBind();
            }
            else
            {
                buscar();
            }
            grvClientes.PageIndex = grvClientes.PageCount - 1;
            grvClientes.DataBind();
        }

        void buscar() 
        {
            dsClientes = dal.getBuscarCarteraAsignada(ddlEjecutivo.SelectedValue, ddlCampana.SelectedValue, txtRut.Text, txtNombre.Text);
            grvClientes.DataSource = dsClientes;
            grvClientes.DataBind();
        }

        void buscarVendedor()
        {
            ddlEjecutivo.DataSource = dal.getBuscarUsuario(null, null);
            ddlEjecutivo.DataValueField = "ID_USUARIO";
            ddlEjecutivo.DataTextField = "USUARIO";
            ddlEjecutivo.DataBind();
        }

        protected void ddlEjecutivo_DataBound(object sender, EventArgs e)
        {
            ddlEjecutivo.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ddlCampana_DataBound(object sender, EventArgs e)
        {
            ddlCampana.Items.Insert(0, new ListItem("Todas", "0"));
        }

        protected void lbtnRut_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                LinkButton _lbtnRut = (LinkButton)grvClientes.Rows[row.RowIndex].FindControl("lbtnIdCliente");

                Response.Redirect("Default.aspx?c=" + _lbtnRut.Text + "&t=1" + "&car=1");
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
                this.Response.Redirect("Default.aspx?c=" + ((LinkButton)this.grvClientes.Rows[((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex].FindControl("lbtnIdCliente")).Text + "&t=1&car=1");
            }
            catch (Exception ex)
            {
                this.lblInformacion.Text = ex.Message;
                this.mdlInformacion.Show();
            }
        }

        protected void gvEmployeegrvCotizacionesCRM_Sorting(object sender, GridViewSortEventArgs e)
        {
            //cotizacionesSession();
            buscar();

            DataTable dt = new DataTable();
            dt = dsClientes.Tables[0];
            {
                string SortDir = string.Empty;
                if (dirGrvCotizacionesCRM == SortDirection.Ascending)
                {
                    dirGrvCotizacionesCRM = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dirGrvCotizacionesCRM = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                Session["SortedView"] = sortedView;


                grvClientes.DataSource = sortedView;
                grvClientes.DataBind();

                //hidTAB.Value = "#seguimiento";
                //hfSeguimiento.Value = "1";
            }
        }

        public SortDirection dirGrvCotizacionesCRM
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
        
    }
}