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
    public partial class Canal : System.Web.UI.Page
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

        protected void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabar.Visible = true;
                btnModificar.Visible = false;

                limpiar(this.Controls);
                mdlAgregarGrupo.Show();
                lblAgregarContacto.Text = "Nuevo Canal";
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
                string idEmpresa = Session["idEmpresa"].ToString();
                dal.setIngresarCanal(txtNombre.Text);
                //dal.setIngresarGrupo(txtIdGrupo.Text, txtNombre.Text, idEmpresa);
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
                string idEmpresa = Session["idEmpresa"].ToString();
                dal.setEditarCanal(hfIdContacto.Value, txtNombre.Text);
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
                btnGrabar.Visible = false;
                btnModificar.Visible = true;

                lblAgregarContacto.Text = "Editar Canal";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdCanal = (Label)grvCanal.Rows[row.RowIndex].FindControl("lblIdCanal");
                Label _lblNombreCanal = (Label)grvCanal.Rows[row.RowIndex].FindControl("lblNombreCanal");
                limpiar(this.Controls);

                hfIdContacto.Value = _lblIdCanal.Text;
                txtNombre.Text = _lblNombreCanal.Text;

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

                Label _lblIdCanal = (Label)grvCanal.Rows[row.RowIndex].FindControl("lblIdCanal");
                dal.setEliminarCanal(_lblIdCanal.Text);
                buscar();
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

            if (Session["DatosListadoCanal"] != null)
            {
                
                grvCanal.DataSource = Session["DatosListadoCanal"];
                grvCanal.DataBind();
            }
            else
            {
                buscar();
            }

            grvCanal.PageIndex = 0;
            grvCanal.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscar();

            if (Session["DatosListadoCanal"] != null)
            {
                grvCanal.DataSource = Session["DatosListadoCanal"];
                grvCanal.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvCanal.PageIndex != 0)
                grvCanal.PageIndex--;
            grvCanal.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoCanal"] != null)
            {
                grvCanal.DataSource = Session["DatosListadoCanal"];
                grvCanal.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvCanal.PageIndex != (grvCanal.PageCount - 1))
                grvCanal.PageIndex++;
            grvCanal.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoCanal"] != null)
            {
                grvCanal.DataSource = Session["DatosListadoCanal"];
                grvCanal.DataBind();
            }
            else
            {
                buscar();
            }

            grvCanal.PageIndex = grvCanal.PageCount - 1;
            grvCanal.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label _lblTotalRegistros = (Label)e.Row.FindControl("lblTotalRegistros");
                _lblPagina.Text = Convert.ToString(grvCanal.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvCanal.PageCount);

                DataTable dt = new DataTable();
                dt = Session["DatosListadoCanal"] as DataTable;
                //_lblTotalRegistros.Text = dt.Rows.Count.ToString();

                //Session["DatosCarteraAsignada"]
                //_lblTotal.Text = 
            }
        }

        void buscar()
        {
            string idEmpresa = Session["idEmpresa"].ToString();
            DataTable dt = new DataTable();
            
            dt = dal.getBuscarCanal(null, txtBuscar.Text).Tables[0];
            Session["DatosListadoCanal"] = dt;

            grvCanal.DataSource = dt;
            grvCanal.DataBind();
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