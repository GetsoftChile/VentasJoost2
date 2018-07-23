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
    public partial class CondicionesComerciales : System.Web.UI.Page
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

        protected void btnNuevoCondicionComercial_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabarProducto.Visible = true;
                btnModificarProducto.Visible = false;

                lblAgregarProducto.Text = "Nueva Condición Comercial";
                limpiar(this.Controls);
                mdlAgregarProducto.Show();
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
                btnGrabarProducto.Visible = false;
                btnModificarProducto.Visible = true;

                lblAgregarProducto.Text = "Editar Condición Comercial";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblId = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblId");
                Label _lblCondicionComercial = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblCondicionComercial");
                Label _lblActivo = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblActivo");
                

                limpiar(this.Controls);

                hfIdProducto.Value = _lblId.Text;
                txtNombreProducto.Text = _lblCondicionComercial.Text;
                ddlActivo.SelectedValue = _lblActivo.Text;

                mdlAgregarProducto.Show();
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

                Label _lblId = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblId");
                dal.setEliminarCondicionComercial(_lblId.Text);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void btnGrabarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                dal.setIngresarCondicionComercial(txtNombreProducto.Text, ddlActivo.SelectedValue);
                
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void btnModificarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                dal.setEditarCondicionComercial(hfIdProducto.Value, txtNombreProducto.Text, ddlActivo.SelectedValue);
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

            if (Session["DatosListadoCondicionComerciales"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoCondicionComerciales"];
                grvProductos.DataBind();
            }
            else
            {
                buscar();
            }

            grvProductos.PageIndex = 0;
            grvProductos.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscar();

            if (Session["DatosListadoCondicionComerciales"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoCondicionComerciales"];
                grvProductos.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvProductos.PageIndex != 0)
                grvProductos.PageIndex--;
            grvProductos.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoCondicionComerciales"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoCondicionComerciales"];
                grvProductos.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvProductos.PageIndex != (grvProductos.PageCount - 1))
                grvProductos.PageIndex++;
            grvProductos.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoCondicionComerciales"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoCondicionComerciales"];
                grvProductos.DataBind();
            }
            else
            {
                buscar();
            }

            grvProductos.PageIndex = grvProductos.PageCount - 1;
            grvProductos.DataBind();
        }



        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label _lblTotalRegistros = (Label)e.Row.FindControl("lblTotalRegistros");
                _lblPagina.Text = Convert.ToString(grvProductos.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvProductos.PageCount);

                DataTable dt = new DataTable();
                dt = Session["DatosListadoCondicionComerciales"] as DataTable;
                //_lblTotalRegistros.Text = dt.Rows.Count.ToString();

                //Session["DatosCarteraAsignada"]
                //_lblTotal.Text = 
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


        void buscar() 
        {
            string idEmpresa = Session["idEmpresa"].ToString();
            DataTable dt = new DataTable();
            dt = dal.getBuscarCondicionComercial(null, txtBuscar.Text).Tables[0];
            Session["DatosListadoCondicionComerciales"] = dt;
            grvProductos.DataSource = dt;
            grvProductos.DataBind();
        }
    }
}