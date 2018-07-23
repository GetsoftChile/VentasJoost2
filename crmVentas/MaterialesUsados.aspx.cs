using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace crm_valvulas_industriales
{
    public partial class MaterialesUsados : System.Web.UI.Page
    {
        Datos dal = new Datos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
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
            grvMateriales.DataSource = dal.getBuscarMaterialUsado(txtFechaDesde.Text, txtFechaHasta.Text);
            grvMateriales.DataBind();
        }
    }
}