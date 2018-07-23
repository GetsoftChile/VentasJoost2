using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Drawing;
using System.Text;
using DataTableToExcel;

namespace crm_valvulas_industriales
{
    public partial class ReporteGestiones : System.Web.UI.Page
    {
        int totalGridview = 0;
        Datos dal = new Datos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.ibtnExporte);
                if (!this.Page.IsPostBack)
                {
                    buscarEjecutivos();
                    string idPerfil = Session["variablePerfil"].ToString();
                    string idUsuario = Session["variableIdUsuario"].ToString();
                    if (idPerfil != "1")
                    {
                        ddlEjecutivo.SelectedValue = idUsuario;
                        ddlEjecutivo.Enabled = false;
                    }
                    string _par = Convert.ToString(Request.QueryString["par"]);
                    if (_par != null)
                    {
                        buscar(_par);
                    }
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        void buscarEjecutivos()
        {
            ddlEjecutivo.DataSource = dal.getBuscarUsuario(null, null);
            ddlEjecutivo.DataValueField = "ID_USUARIO";
            ddlEjecutivo.DataTextField = "USUARIO";
            ddlEjecutivo.DataBind();
        }

        void buscar(string clienteOLead)
        {
            DataTable dt = new DataTable();
            
            dt = dal.getBuscarGestionPorCategoriaSubCategoriaGestion( ddlEjecutivo.SelectedValue, txtFechaDesde.Text, txtFechaHasta.Text, clienteOLead).Tables[0];
            grvCantidadDeGestiones.DataSource = dt;
            grvCantidadDeGestiones.DataBind();
        }

        protected void lbtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string _par = Convert.ToString(Request.QueryString["par"]);
                if (_par != null)
                {
                    buscar(_par);
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        
        protected void ddlEjecutivo_DataBound(object sender, EventArgs e)
        {
            ddlEjecutivo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));
        }
        
        protected void grvCantidadDeGestiones_DataBound(object sender, EventArgs e)
        {
            string tempColumnValue = "Categoría";
            foreach (GridViewRow row in grvCantidadDeGestiones.Rows)
            {
                Label lblMyControl = row.FindControl("lblCategoria") as Label;
                if (tempColumnValue == lblMyControl.Text)
                {
                    lblMyControl.Text = "";
                }
                else
                {
                    tempColumnValue = lblMyControl.Text;
                    row.BackColor = Color.AntiqueWhite;
                }
            }
        }

        protected void grvCantidadDeGestiones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rightBonus = ((LinkButton)e.Row.FindControl("lbtnCantidad")).Text;
                totalGridview += Convert.ToInt32(rightBonus);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                LinkButton _lbtnTotalCantidad = (LinkButton)e.Row.FindControl("lbtnTotalCantidad");
                _lbtnTotalCantidad.Text = totalGridview.ToString("n0");
            }
        }
        
        protected void lbtnCantidad_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnCantidad = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtnCantidad.NamingContainer;

                Label _lblCodCategoria = (Label)grvCantidadDeGestiones.Rows[row.RowIndex].FindControl("lblCodCategoria");
                Label _lblCodSubCategoria = (Label)grvCantidadDeGestiones.Rows[row.RowIndex].FindControl("lblCodSubCategoria");
                Label _lblCodGestion = (Label)grvCantidadDeGestiones.Rows[row.RowIndex].FindControl("lblCodGestion");

                Label _lblGestion = (Label)grvCantidadDeGestiones.Rows[row.RowIndex].FindControl("lblGestion");
                string _par = Convert.ToString(Request.QueryString["par"]);
                DataSet ds = new DataSet();
                ds = dal.getBuscarDetalleGestionPorCategoriaSubCategoriaGestion(ddlEjecutivo.SelectedValue, txtFechaDesde.Text,
                    txtFechaHasta.Text, _par, _lblCodCategoria.Text, _lblCodSubCategoria.Text, _lblCodGestion.Text);

                Session["detalleGestiones"] = ds;

                Response.Redirect("DetalleGestiones.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void lbtnTotalCantidad_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnCantidad = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtnCantidad.NamingContainer;

                string _par = Convert.ToString(Request.QueryString["par"]);
                DataSet ds = new DataSet();
                ds = dal.getBuscarDetalleGestionPorCategoriaSubCategoriaGestion(ddlEjecutivo.SelectedValue, txtFechaDesde.Text, txtFechaHasta.Text, _par, null, null, null);

                Session["detalleGestiones"] = ds;
                Response.Redirect("DetalleGestiones.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnExporte_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string _par = Convert.ToString(Request.QueryString["par"]);
                DataSet ds = new DataSet();
                ds = dal.getBuscarDetalleGestionPorCategoriaSubCategoriaGestion(ddlEjecutivo.SelectedValue, txtFechaDesde.Text,
                    txtFechaHasta.Text, _par, null, null, null);
                Response.ContentType = "Application/x-msexcel";
                Response.AddHeader("content-disposition", "attachment;filename=" + "EXPORTE_GESTIONES" + ".csv");
                Response.ContentEncoding = Encoding.Unicode;
                Response.Write(Utilidad.ExportToCSVFile(ds.Tables[0]));
                Response.End();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}