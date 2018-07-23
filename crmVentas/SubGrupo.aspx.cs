using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;

namespace crm_fadonel
{
    public partial class SubGrupo : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    grupo();
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

            if (Session["DatosListadoSubGrupo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoSubGrupo"];
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

            if (Session["DatosListadoSubGrupo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoSubGrupo"];
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
            if (Session["DatosListadoSubGrupo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoSubGrupo"];
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
            if (Session["DatosListadoSubGrupo"] != null)
            {
                grvGrupo.DataSource = Session["DatosListadoSubGrupo"];
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
                dt = Session["DatosListadoSubGrupo"] as DataTable;
                //_lblTotalRegistros.Text = dt.Rows.Count.ToString();

                //Session["DatosCarteraAsignada"]
                //_lblTotal.Text = 
            }
        }

        void buscar()
        {
            string idEmpresa = Session["idEmpresa"].ToString();
            DataTable dt = new DataTable();
            dt = dal.getBuscarSubGrupo(null, txtBuscar.Text, idEmpresa).Tables[0];
            Session["DatosListadoSubGrupo"] = dt;
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
                lblAgregarContacto.Text = "Nuevo Sub Grupo";

                int idGrupo = 0;
                foreach (GridViewRow item in grvGrupo.Rows)
                {
                    Label _lblIdSubGrupo = (Label)grvGrupo.Rows[item.RowIndex].FindControl("lblIdSubGrupo");
                    idGrupo = Convert.ToInt32(_lblIdSubGrupo.Text);
                }
                idGrupo = idGrupo + 1;
                txtIdSubGrupo.Text = idGrupo.ToString();




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
                if (txtIdSubGrupo.Text == string.Empty)
                {
                    lblInformacion.Text = "El id sub grupo es obligatorio.";
                    mdlInformacion.Show();
                    return;
                }

                if (txtNombre.Text == string.Empty)
                {
                    lblInformacion.Text = "El nombre del sub grupo es obligatorio.";
                    mdlInformacion.Show();
                    return;
                }

                string idEmpresa = Session["idEmpresa"].ToString();
                dal.setIngresarSubGrupo(ddlGrupo.SelectedValue, txtIdSubGrupo.Text, txtNombre.Text, idEmpresa);
                //dal.setIngresarGrupo(txtIdGrupo.Text, txtNombre.Text, idEmpresa);
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
                dal.setEditarSubGrupo(ddlGrupo.SelectedValue, txtIdSubGrupo.Text, txtNombre.Text, idEmpresa);
                //dal.setEditarGrupo(hfIdContacto.Value, txtNombre.Text);
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

                lblAgregarContacto.Text = "Editar Sub Grupo";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblIdGrupo");
                Label _lblIdSubGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblIdSubGrupo");
                Label _lblNombreGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblNombreGrupo");
                Label _lblNombreSubGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblNombreSubGrupo");

                limpiar(this.Controls);

                ddlGrupo.SelectedValue = _lblIdGrupo.Text;
                txtIdSubGrupo.Text = _lblIdSubGrupo.Text;
                txtNombre.Text = _lblNombreSubGrupo.Text;

                //hfIdContacto.Value = _lblIdGrupo.Text;
                //txtIdGrupo.Text = _lblIdGrupo.Text;
                //txtNombre.Text = _lblNombreGrupo.Text;

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
                string idEmpresa = Session["idEmpresa"].ToString();

                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblIdGrupo");
                Label _lblIdSubGrupo = (Label)grvGrupo.Rows[row.RowIndex].FindControl("lblIdSubGrupo");
                dal.setEliminarSubGrupo(_lblIdSubGrupo.Text, idEmpresa);
                //dal.setEliminarGrupo(_lblIdGrupo.Text);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void grupo() 
        {
            string idEmpresa = Session["idEmpresa"].ToString();
            ddlGrupo.DataSource = dal.getBuscarGrupo(null, "", idEmpresa);
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataBind();
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