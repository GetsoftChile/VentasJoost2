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
    public partial class Cargos : System.Web.UI.Page
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


        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscar();

            if (Session["DatosListadoCargo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoCargo"];
                grvGrupo.DataBind();
            }
            else
            {
                buscar();
            }

            grvGrupo.PageIndex = 0;
            grvGrupo.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscar();

            if (Session["DatosListadoCargo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoCargo"];
                grvGrupo.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvGrupo.PageIndex != 0)
                grvGrupo.PageIndex--;
            grvGrupo.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoCargo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoCargo"];
                grvGrupo.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvGrupo.PageIndex != (grvGrupo.PageCount - 1))
                grvGrupo.PageIndex++;
            grvGrupo.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoCargo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoCargo"];
                grvGrupo.DataBind();
            }
            else
            {
                buscar();
            }

            grvGrupo.PageIndex = grvGrupo.PageCount - 1;
            grvGrupo.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label _lblTotalRegistros = (Label)e.Row.FindControl("lblTotalRegistros");
                _lblPagina.Text = Convert.ToString(grvGrupo.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvGrupo.PageCount);

                DataTable dt = new DataTable();
                dt = Session["DatosListadoCargo"] as DataTable;
                //_lblTotalRegistros.Text = dt.Rows.Count.ToString();

                //Session["DatosCarteraAsignada"]
                //_lblTotal.Text = 
            }
        }

        void buscar()
        {
            string idEmpresa = Session["idEmpresa"].ToString();
            DataTable dt = new DataTable();

            dt = dal.getBuscarContactoCargo(null, txtBuscar.Text, idEmpresa).Tables[0];
            Session["DatosListadoConvenio"] = dt;
            grvGrupo.DataSource = dt;
            grvGrupo.DataBind();
        }

        protected void btnNuevoGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabarGrupo.Visible = true;
                btnModificarGrupo.Visible = false;

                limpiar(this.Controls);
                mdlAgregarGrupo.Show();
                lblAgregarContacto.Text = "Nuevo Cargo";
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

        protected void btnGrabarGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                dal.setIngresarContactoCargo(txtNombre.Text, idEmpresa);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnModificarGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                //dal.setEditarGrupo(hfIdContacto.Value, txtNombre.Text);
                dal.setEditarContactoCargo(hfIdContacto.Value, txtNombre.Text, idEmpresa, ddlActivo.SelectedValue);
                buscar();
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
                btnGrabarGrupo.Visible = false;
                btnModificarGrupo.Visible = true;

                lblAgregarContacto.Text = "Editar Cargo";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblIdGrupo");
                Label _lblNombreGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblNombreGrupo");
                Label _lblActivo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblActivo");
                limpiar(this.Controls);

                hfIdContacto.Value = _lblIdGrupo.Text;
                txtNombre.Text = _lblNombreGrupo.Text;
                ddlActivo.SelectedValue = _lblActivo.Text;

                mdlAgregarGrupo.Show();
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

                Label _lblIdGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblIdGrupo");

                
                dal.setEliminarContactoCargo(_lblIdGrupo.Text);
                buscar();
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
    }
}