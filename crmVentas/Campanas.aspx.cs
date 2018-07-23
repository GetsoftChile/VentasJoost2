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
    public partial class Campanas : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
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

        protected void btnNuevoCampana_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Visible = true;
                btnModificar.Visible = false;

                limpiar(this.Controls);

                lblAgregarUsuario.Text = "Nuevo Campana";
                mdlAgregarCliente.Show();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEditar_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = true;

                lblAgregarUsuario.Text = "Modificar Campaña";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdCampana = (Label)grvCampanas.Rows[row.RowIndex].FindControl("lblIdCampana");
                Label _lblNombreCampana = (Label)grvCampanas.Rows[row.RowIndex].FindControl("lblNombreCampana");
                Label _lblFechaInicio = (Label)grvCampanas.Rows[row.RowIndex].FindControl("lblFechaInicio");
                Label _lblFechaTermino = (Label)grvCampanas.Rows[row.RowIndex].FindControl("lblFechaTermino");
                Label _lblActivo = (Label)grvCampanas.Rows[row.RowIndex].FindControl("lblActivo");
                Label _lblIdActivo = (Label)grvCampanas.Rows[row.RowIndex].FindControl("lblIdActivo");

                limpiar(this.Controls);
                hfIdUsuario.Value = _lblIdCampana.Text;
                
                txtNombreCampana.Text = _lblNombreCampana.Text;
                txtFechaInicio.Text = _lblFechaInicio.Text;
                txtFechaTermino.Text = _lblFechaTermino.Text;

                if (_lblIdActivo.Text != string.Empty)
                {
                    ddlActiva.SelectedValue = _lblIdActivo.Text;
                }

                mdlAgregarCliente.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        public void limpiar(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
                else if (control is DropDownList)
                    ((DropDownList)control).ClearSelection();
                else if (control is RadioButtonList)
                    ((RadioButtonList)control).ClearSelection();
                else if (control is CheckBoxList)
                    ((CheckBoxList)control).ClearSelection();
                else if (control is RadioButton)
                    ((RadioButton)control).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)control).Checked = false;
                else if (control.HasControls())
                    //Esta linea detécta un Control que contenga otros Controles
                    //Así ningún control se quedará sin ser limpiado.
                    limpiar(control.Controls);
            }
        }

        protected void imgEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdCampana = (Label)grvCampanas.Rows[row.RowIndex].FindControl("lblIdCampana");

                DataTable dt = new DataTable();
                dt = dal.getBuscarClientePorIdCampana(_lblIdCampana.Text).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dal.setEliminarCampana(_lblIdCampana.Text);
                }
                else
                {
                    lblInformacion.Text = "No puede eliminar la campaña porque existe algún rut asociado de ella";
                    mdlInformacion.Show();
                    return;
                }
                
                
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
            grvCampanas.DataSource = dal.getBuscarCampana(txtBuscar.Text,null);
            grvCampanas.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvCampanas.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvCampanas.PageCount);
            }
        }



        protected void imgFirst_Click(object sender, EventArgs e)
        {
            buscar();

            grvCampanas.PageIndex = 0;
            grvCampanas.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvCampanas.PageIndex != 0)
                grvCampanas.PageIndex--;
            grvCampanas.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvCampanas.PageIndex != (grvCampanas.PageCount - 1))
                grvCampanas.PageIndex++;
            grvCampanas.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            buscar();
            grvCampanas.PageIndex = grvCampanas.PageCount - 1;
            grvCampanas.DataBind();
        }



        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                dal.setEditarCampana(hfIdUsuario.Value, txtNombreCampana.Text, txtFechaInicio.Text, txtFechaTermino.Text, ddlActiva.SelectedValue);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtMontoCredito.Text == string.Empty)
                //{
                //    txtMontoCredito.Text = null;
                //}

                //if (txtRut.Text == string.Empty)
                //{
                //    lblInformacion.Text = "El campo Rut es obligatorio.";
                //    mdlInformacion.Show();
                //    return;
                //}

                //string idUsuario = Session["variableIdUsuario"].ToString();
                //dal.setIngresarCliente(txtRut.Text, txtRazonSocial.Text, txtNombreCorto.Text,
                //    txtTelefono.Text, txtDireccion.Text, txtComuna.Text, txtCiudad.Text,
                //    txtEmail.Text, txtInfo.Text, txtMontoCredito.Text, txtClasificacion.Text,
                //    txtZona.Text, ddlEstado.SelectedValue, idUsuario, txtGiro.Text,
                //    ddlCondicionDeVenta.SelectedValue, txtUrl.Text, ddlActividadComercial.SelectedValue,
                //    txtObservacion.Text, ddlCampana.SelectedValue);
                //In.setIngresarCliente(txtRazonSocial.Text, txtNombreCorto.Text, txtRut.Text, txtDireccion.Text, ddlComuna.SelectedValue, txtCiudad.Text, txtFono1.Text, txtFono2.Text, txtRepLegal.Text, txtRepLegalRut.Text, txtNombreContacto.Text, txtEmailContacto.Text, ddlActivo.SelectedValue, "", txtCaducidad.Text, txtPlazoBarrido.Text);

                dal.setIngresarCampana(txtNombreCampana.Text, txtFechaInicio.Text, txtFechaTermino.Text, ddlActiva.SelectedValue);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

    }
}