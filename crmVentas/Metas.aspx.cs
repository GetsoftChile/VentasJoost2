using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace crm_valvulas_industriales
{
    public partial class Metas : System.Web.UI.Page
    {
        Datos dal = new Datos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string idUsuario = Session["variableIdUsuario"].ToString();
                    string idPerfil = Session["variablePerfil"].ToString();
                    if (idPerfil == "3")
                    {
                        ddlVendedor.SelectedValue = idUsuario;
                        ddlVendedor.Enabled = false;
                    }

                    DateTime hoy = DateTime.Now;
                    //txtFechaDesde.Text = (hoy.AddDays(-7).ToString("dd-MM-yyyy"));
                    int mes = 0;
                    int ano = 0;
                    mes = hoy.Month;
                    ano = hoy.Year;

                    ddlAno.SelectedValue = ano.ToString();
                    ddlMes.SelectedValue = mes.ToString();

                    buscarVendedor();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

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
                Buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void Buscar()
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarMetas(ddlVendedor.SelectedValue, ddlMes.SelectedValue, ddlAno.SelectedValue).Tables[0];

            grvMetas.DataSource = dt;
            grvMetas.DataBind();
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

        protected void btnNuevaMeta_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                
                foreach (GridViewRow row in this.grvMetas.Rows)
                {
                    Label _lblIdVendedor = ((Label)this.grvMetas.Rows[row.RowIndex].FindControl("lblIdVendedor"));
                    TextBox _txtFechaCierre = ((TextBox)this.grvMetas.Rows[row.RowIndex].FindControl("txtFechaCierre"));
                    TextBox _txtMetaMensual = ((TextBox)this.grvMetas.Rows[row.RowIndex].FindControl("txtMetaMensual"));
                    if (string.IsNullOrEmpty(_txtMetaMensual.Text))
                    {
                        _txtMetaMensual.Text = "0";
                    }

                    if (string.IsNullOrEmpty(_txtFechaCierre.Text))
                    {
                        _txtFechaCierre.Text = null;
                    }
                    dal.setInUpMetas(Convert.ToInt32(_txtMetaMensual.Text), _txtFechaCierre.Text, ddlMes.SelectedValue, ddlAno.SelectedValue, _lblIdVendedor.Text);
                }


                lblInformacion.Text = "Metas Asignadas";
                mdlInformacion.Show();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}