using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using DataTableToExcel;

namespace crm_fadonel
{
    public partial class Productos : System.Web.UI.Page
    {
        Datos dal = new Datos();
        string rutaFichaTecnica = "productos/fichaTecnica/";
        string rutaImagen = "productos/imagen/";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnModificarProducto);
                scriptManager.RegisterPostBackControl(this.ibtnExportarExcel);
                
                if (!this.Page.IsPostBack)
                {
                    buscarMateriales();
                    buscar();
                }
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
            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataBind();
        }

        void subGrupo(string idGrupo) 
        {
            string idEmpresa = Session["idEmpresa"].ToString();
            ddlSubGrupo.DataSource = dal.getBuscarSubGrupoPorIdGrupo(ddlGrupo.SelectedValue, idEmpresa);
            ddlSubGrupo.DataValueField = "ID_SUB_GRUPO";
            ddlSubGrupo.DataTextField = "NOM_SUB_GRUPO";
            ddlSubGrupo.DataBind();
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
                btnGrabarProducto.Visible = true;
                btnModificarProducto.Visible = false;
                txtStock.Enabled = false;

                imgImagen.Visible = false;

                lblAgregarProducto.Text = "Nuevo Producto";
                grupo();
                subGrupo(ddlGrupo.SelectedValue);
                limpiar(this.Controls);
                mdlAgregarProducto.Show();
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

                if (txtCostoUnitario.Text.Trim() == string.Empty)
                {
                    txtCostoUnitario.Text = "0";
                }

                if (txtValorVenta.Text.Trim() == string.Empty)
                {
                    txtValorVenta.Text = "0";
                }
                
                if (txtStock.Text.Trim() == string.Empty)
                {
                    txtStock.Text = "0";
                }

                dal.setIngresarProducto(txtNombreProducto.Text, txtCodigo.Text, txtBodega.Text, ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue, txtUnidadMedida.Text, txtCostoUnitario.Text.Replace(",", "."), txtValorVenta.Text.Replace(",", "."), Convert.ToInt32(txtStock.Text),txtObservacion.Text);

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
                txtStock.Enabled = true;
                if (txtCostoUnitario.Text == string.Empty)
                {
                    txtCostoUnitario.Text = "0";
                }

                if (txtValorVenta.Text == string.Empty)
                {
                    txtValorVenta.Text = "0";
                }

                if (txtStock.Text.Trim() == string.Empty)
                {
                    txtStock.Text = "0";
                }

                string urlImagen = "";
                string urlPdf = "";
                //buscar();
                
                if (fuImagen.HasFile && fuFichaTecnica.HasFile)
                {
                    urlImagen = "~/" + rutaImagen + hfIdProducto.Value + "_" + fuImagen.FileName;
                    urlPdf = "~/" + rutaFichaTecnica + hfIdProducto.Value + "_" + fuFichaTecnica.FileName;

                    fuImagen.SaveAs(Server.MapPath(urlImagen));
                    fuFichaTecnica.SaveAs(Server.MapPath(urlPdf));

                    dal.setEditarProducto(hfIdProducto.Value, txtNombreProducto.Text, txtCodigo.Text, txtBodega.Text, 
                        ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue, txtUnidadMedida.Text, 
                        txtCostoUnitario.Text.Replace(",", "."), txtValorVenta.Text.Replace(",", "."), 
                        Convert.ToInt32(txtStock.Text), urlImagen,urlPdf,txtObservacion.Text);
                }

                if (fuImagen.HasFile && fuFichaTecnica.HasFile == false)
                {
                    urlImagen = "~/" + rutaImagen + hfIdProducto.Value + "_" + fuImagen.FileName;
                    //urlPdf = rutaFichaTecnica + hfIdProducto.Value + "_" + fuFichaTecnica.FileName;

                    fuImagen.SaveAs(Server.MapPath(urlImagen));

                    if (hfPDF.Value != string.Empty)
                    {
                        urlPdf = hfPDF.Value;
                    }

                    dal.setEditarProducto(hfIdProducto.Value, txtNombreProducto.Text, txtCodigo.Text, txtBodega.Text, 
                        ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue, txtUnidadMedida.Text, 
                        txtCostoUnitario.Text.Replace(",", "."), txtValorVenta.Text.Replace(",", "."), 
                        Convert.ToInt32(txtStock.Text), urlImagen, urlPdf,txtObservacion.Text);
                }

                if (fuImagen.HasFile == false && fuFichaTecnica.HasFile)
                {
                    //urlImagen = rutaImagen + hfIdProducto.Value + "_" + fuImagen.FileName;
                    urlPdf = "~/" + rutaFichaTecnica + hfIdProducto.Value + "_" + fuFichaTecnica.FileName;

                    fuFichaTecnica.SaveAs(Server.MapPath(urlPdf));

                    if (imgImagen.ImageUrl != string.Empty)
                    {
                        urlImagen = imgImagen.ImageUrl;
                    }

                    dal.setEditarProducto(hfIdProducto.Value, txtNombreProducto.Text, txtCodigo.Text, txtBodega.Text, 
                        ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue, txtUnidadMedida.Text, 
                        txtCostoUnitario.Text.Replace(",", "."), txtValorVenta.Text.Replace(",", "."), 
                        Convert.ToInt32(txtStock.Text), urlImagen, urlPdf,txtObservacion.Text);
                }

                if (fuImagen.HasFile == false && fuFichaTecnica.HasFile == false)
                {
                    if (hfPDF.Value != string.Empty)
                    {
                        urlPdf = hfPDF.Value;
                    }
                    if (imgImagen.ImageUrl != string.Empty)
                    {
                        urlImagen = imgImagen.ImageUrl;
                    }

                    dal.setEditarProducto(hfIdProducto.Value, txtNombreProducto.Text, txtCodigo.Text, txtBodega.Text, 
                        ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue, txtUnidadMedida.Text, 
                        txtCostoUnitario.Text.Replace(",", "."), txtValorVenta.Text.Replace(",", "."), 
                        Convert.ToInt32(txtStock.Text), urlImagen, urlPdf, txtObservacion.Text);
                }


                //foreach (GridViewRow item in grvMateriales.Rows)
                //{
                //    TextBox _txtCantidadSeleccionada = (TextBox)grvProductos.Rows[item.RowIndex].FindControl("txtCantidadSeleccionada");
                //    if (_txtCantidadSeleccionada.Text != string.Empty)
                //    {
                //        Label _lblIdMaterial = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblIdMaterial");
                //        Label _lblCantidadSeleccionada = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblCantidadSeleccionada");
                //        Label _lblCantidadDisponible = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblCantidadDisponible");
                //        int cantidadSeleccionadaText = Convert.ToInt32(_txtCantidadSeleccionada.Text);
                //        int cantidadSeleccionadaLabel = Convert.ToInt32(_lblCantidadSeleccionada.Text);
                //        int cantidadDisponible = Convert.ToInt32(_lblCantidadDisponible.Text);

                //        if (cantidadSeleccionadaText > cantidadDisponible)
                //        {
                //            lblInformacion.Text = "Advertencia: No hay materiales suficiente para el producto " + txtNombreProducto.Text;
                //            mdlInformacion.Show();
                //        }
                //        else
                //        {
                //            //int resto = cantidadSeleccionadaLabel - cantidadSeleccionadaText;
                //            //dal.setEditarMaterialResto(Convert.ToInt32(_lblIdMaterial.Text), resto);
                //        }
                //    }
                //}
                if (txtStock.Text!=string.Empty)
                {
                    int nuevoStock = Convert.ToInt32(txtStock.Text);
                    int antiguoStock = Convert.ToInt32(hfStockAntiguo.Value);
                    int resto = nuevoStock - antiguoStock;

                    DataTable dt = new DataTable();
                    dt = dal.getBuscarMaterialProducto(Convert.ToInt32(hfIdProducto.Value)).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        int idMaterial = Convert.ToInt32(item["Id_Material"]);
                        int cantidad = Convert.ToInt32(item["cantidad"]);
                        int total = cantidad * resto;
                        dal.setEditarMaterialResto(idMaterial, total);
                    }

                    
                }
                
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
                btnGrabarProducto.Visible = false;
                btnModificarProducto.Visible = true;

                lblAgregarProducto.Text = "Editar Producto";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdProducto = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblIdProducto");
                Label _lblNombreProducto = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblNombreProducto");
                Label _lblCodigo = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblCodigo");
                Label _lblBodega = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblBodega");
                Label _lblIdGrupo = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblIdGrupo");
                Label _lblGrupo = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblGrupo");
                Label _lblIdSubGrupo = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblIdSubGrupo");
                Label _lblUnidadMedida = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblUnidadMedida");
                Label _lblCostoUnitario = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblCostoUnitario");
                Label _lblValorVenta = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblValorVenta");
                Label _lblRutaFichaTecnica = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblRutaFichaTecnica");
                Label _lblRutaImagen = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblRutaImagen");
                Label _lblStock = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblStock");
                Label _lblObservacion = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblObservacion");

                limpiar(this.Controls);

                grupo();

                hfIdProducto.Value = _lblIdProducto.Text;
                txtObservacion.Text = _lblObservacion.Text;
                txtNombreProducto.Text = _lblNombreProducto.Text;
                txtCodigo.Text = _lblCodigo.Text;
                txtBodega.Text = _lblBodega.Text;
                ddlGrupo.SelectedValue = _lblIdGrupo.Text;
                subGrupo(ddlGrupo.SelectedValue);
                ddlSubGrupo.SelectedValue = _lblIdSubGrupo.Text;
                txtUnidadMedida.Text = _lblUnidadMedida.Text;
                txtCostoUnitario.Text = _lblCostoUnitario.Text;
                txtValorVenta.Text = _lblValorVenta.Text;
                txtStock.Text = _lblStock.Text;
                if (_lblStock.Text==string.Empty)
                {
                    txtStock.Text = "0";
                    hfStockAntiguo.Value = "0";
                }
                else
                {
                    hfStockAntiguo.Value = _lblStock.Text;
                }
                txtNombreProducto.Enabled = false;
                txtCodigo.Enabled = true;
                //txtBodega.Enabled = false;
                ddlGrupo.Enabled = false;
                ddlSubGrupo.Enabled = false;
                txtUnidadMedida.Enabled = false;
                //txtCostoUnitario.Enabled = false;
                //txtValorVenta.Enabled = false;

                if (_lblRutaFichaTecnica.Text != string.Empty)
                {
                    hfPDF.Value = _lblRutaFichaTecnica.Text;
                    lbtnVerPdf.Visible = true;
                }

                string idPerfil = Session["variablePerfil"].ToString();
                string idUsuario = Session["variableIdUsuario"].ToString();
                if (idPerfil != "1")
                {
                    txtStock.Enabled = false;
                }
                else
                {
                    txtStock.Enabled = true;
                }
                
                imgImagen.ImageUrl = _lblRutaImagen.Text;
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

                Label _lblIdProducto = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblIdProducto");
                dal.setEliminarProducto(_lblIdProducto.Text);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                subGrupo(ddlGrupo.SelectedValue);
                mdlAgregarProducto.Show();
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
            dt = dal.getBuscarProducto(null, txtBuscar.Text, idEmpresa).Tables[0];
            Session["DatosListadoProducto"] = dt;
            grvProductos.DataSource = dt;
            grvProductos.DataBind();
        }

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoProducto"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoProducto"];
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

            if (Session["DatosListadoProducto"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoProducto"];
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
            if (Session["DatosListadoProducto"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoProducto"];
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
            if (Session["DatosListadoProducto"] != null)
            {
                grvProductos.DataSource = Session["DatosListadoProducto"];
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
                dt = Session["DatosListadoProducto"] as DataTable;
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

            txtNombreProducto.Enabled = true;
            txtCodigo.Enabled = true;
            txtBodega.Enabled = true;
            ddlGrupo.Enabled = true;
            ddlSubGrupo.Enabled = true;
            txtUnidadMedida.Enabled = true;
            txtCostoUnitario.Enabled = true;
            txtValorVenta.Enabled = true;
        }


        protected void lbtnVerPdf_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = hfPDF.Value.Replace("~", "").TrimStart('/');

                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + ruta + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["DatosListadoProducto"] as DataTable;
                Utilidad.ExportDataTableToExcel(dt, "Exporte_Productos.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = "Error: " + ex.Message;
                mdlInformacion.Show();
            }
        }
        
        void buscarMateriales()
        {
            grvMateriales.DataSource = dal.getBuscarMaterial(null,null);
            grvMateriales.DataBind();

            
        }

        protected void imgMaterial_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdProducto = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblIdProducto");
                Response.Redirect("AsignacionMaterialProducto.aspx?idP="+_lblIdProducto.Text);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}