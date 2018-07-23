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
    public partial class AgendamientoLead : System.Web.UI.Page
    {
        Datos dal = new Datos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    DateTime hoy = DateTime.Now;
                    txtFechaDesdeAgendamiento.Text = hoy.ToString("dd-MM-yyyy");
                    txtFechaHastaAgendamiento.Text = hoy.ToString("dd-MM-yyyy");
                    buscarAgendamientos();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void btnBuscarAgendamiento_Click(object sender, EventArgs e)
        {
            try
            {
                buscarAgendamientos();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        void buscarAgendamientos()
        {

            DataTable dt = dal.getBuscarGestionesConAgendamientoLead(txtFechaDesdeAgendamiento.Text, txtFechaHastaAgendamiento.Text).Tables[0];
            string cont = dt.Rows.Count.ToString();

            //tpAgendamientos.HeaderText = "Agendamientos (" + cont + ")";

            grvAgendamientos.DataSource = dt;
            grvAgendamientos.DataBind();
        }


        protected void lbtnRutClienteAgendamiento_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRut = (Label)grvAgendamientos.Rows[row.RowIndex].FindControl("lblRut");

                //buscarClientePorRut(_lblRut.Text.Trim());

                Response.Redirect("Default.aspx?c=" + _lblRut.Text + "&t=1" + "&agendamiento=1");
                //TabContainer1.ActiveTab = tpCliente;
                //buscarDetalleCotizacion(_lblCotizacion.Text.Trim());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }



        protected void imgGestionarAgendamiento_Click(object sender, EventArgs e)
        {
            try
            {
                //LinkButton lbtn = sender as LinkButton;
                //ImageButton img = sender as ImageButton;
                //GridViewRow row = (GridViewRow)img.NamingContainer;

                //Label _lblRut = (Label)grvAgendamientos.Rows[row.RowIndex].FindControl("lblRut");

                //buscarClientePorRut(_lblRut.Text.Trim());

                //tablaCliente.Visible = true;
                ////tablaComercial.Visible = true;

                //lblRutGestiones.Text = lblRut.Text;
                //lblRazonSocialGestiones.Text = lblRazonSocial.Text;


                //string montoCotizacionDesde = txtMontoCotizacionDesde.Text;
                //string montoCotizacionHasta = txtMontoCotizacionHasta.Text;
                //string fechaDesde = txtFechaDesde.Text.Replace("-", "/");
                //string fechaHasta = txtFechaHasta.Text.Replace("-", "/");

                //buscarGestion(_lblRut.Text, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);
                ////buscarGestion(_lblCodSapAgendamiento.Text, tasaCierreDesde, tasaCierreHasta, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);
                //TabContainer1.ActiveTab = tpGestion;
                //buscarGestionesCRM();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}