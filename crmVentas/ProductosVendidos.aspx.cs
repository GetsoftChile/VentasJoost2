using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;

namespace crm_valvulas_industriales
{
    public partial class ProductosVendidos : System.Web.UI.Page
    {
        Datos dal = new Datos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
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

        void buscar()
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarProductoCantdadVendida(ddlEmpresa.SelectedValue, txtFechaDesde.Text, txtFechaHasta.Text).Tables[0];
            grvProductos.DataSource = dt;
            grvProductos.DataBind();
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
    }
}