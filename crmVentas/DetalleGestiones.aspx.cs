using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crm_valvulas_industriales
{
    public partial class DetalleGestiones : System.Web.UI.Page
    {
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

        void buscar()
        {
            grvCantidadDeGestiones.DataSource = Session["detalleGestiones"];
            grvCantidadDeGestiones.DataBind();
        }
    }
}