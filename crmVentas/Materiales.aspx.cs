using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using DataTableToExcel;

namespace crm_valvulas_industriales
{
    public partial class Materiales : System.Web.UI.Page
    {
        Datos dal = new Datos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnModificarMaterial);
                scriptManager.RegisterPostBackControl(this.ibtnExportarExcel);

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

        protected void btnNuevoMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabarMaterial.Visible = true;
                btnModificarMaterial.Visible = false;
                

                lblAgregarProducto.Text = "Nuevo Material";
      
                limpiar(this.Controls);
                mdlAgregarProducto.Show();
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

            //txtNombreMaterial.Enabled = true;
            //txtCodigo.Enabled = true;
            //txtBodega.Enabled = true;
            //ddlGrupo.Enabled = true;
            //ddlSubGrupo.Enabled = true;
            //txtUnidadMedida.Enabled = true;
            //txtCostoUnitario.Enabled = true;
            //txtValorVenta.Enabled = true;
        }
        protected void ibtnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["DatosListadoMaterial"] as DataTable;
                Utilidad.ExportDataTableToExcel(dt, "Exporte_Materiales.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = "Error: " + ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label _lblTotalRegistros = (Label)e.Row.FindControl("lblTotalRegistros");
                _lblPagina.Text = Convert.ToString(grvMateriales.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvMateriales.PageCount);

                DataTable dt = new DataTable();
                dt = Session["DatosListadoMaterial"] as DataTable;
                //_lblTotalRegistros.Text = dt.Rows.Count.ToString();

                //Session["DatosCarteraAsignada"]
                //_lblTotal.Text = 
            }
        }

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                btnGrabarMaterial.Visible = false;
                btnModificarMaterial.Visible = true;

                lblAgregarProducto.Text = "Editar Material";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
                
                Label _lblIdMaterial = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblIdMaterial");
                Label _lblNombreMaterial = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblNombreMaterial");
                Label _lblTipo = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblTipo");
                Label _lblMedida = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblMedida");
                Label _lblColor = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblColor");
                Label _lblStock = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblStock");

                limpiar(this.Controls);


                hfIdMaterial.Value = _lblIdMaterial.Text;
                txtNombreMaterial.Text = _lblNombreMaterial.Text;
                txtTipo.Text = _lblTipo.Text;
                txtMedida.Text = _lblMedida.Text;
                txtColor.Text = _lblColor.Text;
                txtCantidad.Text = _lblStock.Text;

                //txtNombreProducto.Enabled = false;
                //txtCodigo.Enabled = false;
                ////txtBodega.Enabled = false;
                //ddlGrupo.Enabled = false;
                //ddlSubGrupo.Enabled = false;
                //txtUnidadMedida.Enabled = false;
                ////txtCostoUnitario.Enabled = false;
                ////txtValorVenta.Enabled = false;

                //if (_lblRutaFichaTecnica.Text != string.Empty)
                //{
                //    hfPDF.Value = _lblRutaFichaTecnica.Text;
                //    lbtnVerPdf.Visible = true;
                //}

                string idPerfil = Session["variablePerfil"].ToString();
                string idUsuario = Session["variableIdUsuario"].ToString();
                if (idPerfil != "1")
                {
                    txtCantidad.Enabled = false;
                }
                else
                {
                    txtCantidad.Enabled = true;
                }
                
                
                mdlAgregarProducto.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
                
                Label _lblIdProducto = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblIdProducto");
                dal.setEliminarProducto(_lblIdProducto.Text);
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
            string idEmpresa = Session["idEmpresa"].ToString();
            DataTable dt = new DataTable();
            dt = dal.getBuscarMaterial(txtBuscar.Text,null).Tables[0];
            Session["DatosListadoMaterial"] = dt;
            grvMateriales.DataSource = dt;
            grvMateriales.DataBind();
        }

        protected void btnGrabarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();

                
                if (txtCantidad.Text.Trim() == string.Empty)
                {
                    txtCantidad.Text = "0";
                }

                dal.setIngresarMaterial(txtTipo.Text, txtNombreMaterial.Text,txtMedida.Text, txtColor.Text, Convert.ToInt32(txtCantidad.Text));
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnModificarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                if (txtCantidad.Text == string.Empty)
                {
                    txtCantidad.Text = "0";
                }

                dal.setEditarMaterial(Convert.ToInt32(hfIdMaterial.Value), txtTipo.Text,
                    txtNombreMaterial.Text, txtMedida.Text, txtColor.Text, Convert.ToInt32(txtCantidad.Text));

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
            if (Session["DatosListadoMaterial"] != null)
            {
                grvMateriales.DataSource = Session["DatosListadoMaterial"];
                grvMateriales.DataBind();
            }
            else
            {
                buscar();
            }

            grvMateriales.PageIndex = 0;
            grvMateriales.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscar();

            if (Session["DatosListadoMaterial"] != null)
            {
                grvMateriales.DataSource = Session["DatosListadoMaterial"];
                grvMateriales.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvMateriales.PageIndex != 0)
                grvMateriales.PageIndex--;
            grvMateriales.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoMaterial"] != null)
            {
                grvMateriales.DataSource = Session["DatosListadoMaterial"];
                grvMateriales.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvMateriales.PageIndex != (grvMateriales.PageCount - 1))
                grvMateriales.PageIndex++;
            grvMateriales.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoMaterial"] != null)
            {
                grvMateriales.DataSource = Session["DatosListadoMaterial"];
                grvMateriales.DataBind();
            }
            else
            {
                buscar();
            }

            grvMateriales.PageIndex = grvMateriales.PageCount - 1;
            grvMateriales.DataBind();
        }



    }
}