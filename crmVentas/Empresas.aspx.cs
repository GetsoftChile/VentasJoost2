using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;

namespace crm_valvulas_industriales
{
    public partial class Empresas : System.Web.UI.Page
    {
        Datos dal = new Datos();
        string ruta = "assets/img/imagenesEmpresa/";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnModificar);
                scriptManager.RegisterPostBackControl(this.btnAgregar);

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
            DataTable dt = new DataTable();
            dt = dal.getBuscarEmpresa(txtBuscar.Text, null).Tables[0];
            Session["datosEmpresa"] = dt;

            grvEmpresas.DataSource = dt;
            grvEmpresas.DataBind();
        }

        protected void btnNuevoEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Visible = true;
                btnModificar.Visible = false;

                limpiar(this.Controls);

                lblAgregarUsuario.Text = "Nuevo Empresa";
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



        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvEmpresas.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvEmpresas.PageCount);
            }
        }

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            buscar();

            grvEmpresas.PageIndex = 0;
            grvEmpresas.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvEmpresas.PageIndex != 0)
                grvEmpresas.PageIndex--;
            grvEmpresas.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvEmpresas.PageIndex != (grvEmpresas.PageCount - 1))
                grvEmpresas.PageIndex++;
            grvEmpresas.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            buscar();
            grvEmpresas.PageIndex = grvEmpresas.PageCount - 1;
            grvEmpresas.DataBind();
        }


        protected void imgEditar_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Visible = false;
                btnModificar.Visible = true;

                lblAgregarUsuario.Text = "Modificar Empresa";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdEmpresa = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblIdEmpresa");
                Label _lblNombreEmpresa = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblNombreEmpresa");
                Label _lblDescripcion = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblDescripcion");
                Label _lblEmail = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblEmail");
                Label _lblActivo = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblActivo");
                Image _imgImagenEmpresa = (Image)grvEmpresas.Rows[row.RowIndex].FindControl("imgImagenEmpresa");

                Label _lblRut = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblRut");
                Label _lblGiro = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblGiro");
                Label _lblTelefono = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblTelefono");
                
                limpiar(this.Controls);
                hfIdUsuario.Value = _lblIdEmpresa.Text;

                txtNombreEmpresa.Text = _lblNombreEmpresa.Text;
                txtDescripcion.Text = _lblDescripcion.Text;
                txtEmail.Text = _lblEmail.Text;
                imgEmpresa.ImageUrl = _imgImagenEmpresa.ImageUrl;
                ddlEstado.SelectedValue = _lblActivo.Text;
                txtRut.Text = _lblRut.Text;
                txtGiro.Text = _lblGiro.Text;
                txtTelefono.Text = _lblTelefono.Text;
                
                mdlAgregarCliente.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdEmpresa = (Label)grvEmpresas.Rows[row.RowIndex].FindControl("lblIdEmpresa");
                dal.setEliminarEmpresa(_lblIdEmpresa.Text);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }

        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";

                if (fuImagen.HasFile)
                {
                    url = ruta + hfIdUsuario.Value + "_" + fuImagen.FileName;
                    fuImagen.SaveAs(Server.MapPath(ruta + hfIdUsuario.Value + "_" + fuImagen.FileName));

                    dal.setEditarEmpresa(hfIdUsuario.Value, txtNombreEmpresa.Text, txtDescripcion.Text,
                        txtEmail.Text, ddlEstado.SelectedValue, url, txtRut.Text, txtGiro.Text, txtTelefono.Text);
                    //Ed.setEditarCliente(hfIdUsuario.Value, txtRazonSocial.Text, txtNombreCorto.Text, txtRut.Text, txtDireccion.Text, ddlComuna.SelectedValue, txtCiudad.Text, txtFono1.Text, txtFono2.Text, txtRepLegal.Text, txtRepLegalRut.Text, txtNombreContacto.Text, txtEmailContacto.Text, ddlActivo.SelectedValue, url, txtCaducidad.Text, txtPlazoBarrido.Text, txtPriorizacionMonto.Text.Replace(",", "."));

                    buscar();
                    return;
                }

                if (imgEmpresa.ImageUrl != string.Empty)
                {
                    dal.setEditarEmpresa(hfIdUsuario.Value, txtNombreEmpresa.Text, txtDescripcion.Text, 
                        txtEmail.Text, ddlEstado.SelectedValue, imgEmpresa.ImageUrl, txtRut.Text, txtGiro.Text, txtTelefono.Text);
                }
                else
                {
                    dal.setEditarEmpresa(hfIdUsuario.Value, txtNombreEmpresa.Text, txtDescripcion.Text,
                        txtEmail.Text, ddlEstado.SelectedValue, "", txtRut.Text, txtGiro.Text, txtTelefono.Text);
                }

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
                string idEmpresa = dal.setIngresarEmpresa(txtNombreEmpresa.Text, txtDescripcion.Text, 
                    txtEmail.Text, ddlEstado.SelectedValue, "");
                string url = "";
                if (fuImagen.HasFile)
                {
                    url = ruta + hfIdUsuario.Value + "_" + fuImagen.FileName;
                    fuImagen.SaveAs(Server.MapPath(ruta + hfIdUsuario.Value + "_" + fuImagen.FileName));

                    dal.setEditarEmpresa(idEmpresa, txtNombreEmpresa.Text, txtDescripcion.Text, txtEmail.Text, 
                        ddlEstado.SelectedValue, url, txtRut.Text, txtGiro.Text, txtTelefono.Text);
                }

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