using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;


namespace crm_valvulas_industriales
{
    public partial class AsignacionMaterialProducto : System.Web.UI.Page
    {
        Datos dal = new Datos();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    string _idProducto = Convert.ToString(Request.QueryString["idP"]);
                    if (_idProducto != null)
                    {
                        DataTable dt = new DataTable();
                        dt = dal.getBuscarProducto(_idProducto, null, null).Tables[0];
                        foreach (DataRow item in dt.Rows)
                        {
                            lblNombreProducto.Text = item["NOM_PRODUCTO"].ToString();
                            hfIdProducto.Value= item["ID_PRODUCTO"].ToString();
                        }
                        buscar();
                    }
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
            grvMateriales.DataSource = dal.getBuscarMaterialProducto(Convert.ToInt32(hfIdProducto.Value));
            grvMateriales.DataBind();

            grvMaterialesSeleccionar.DataSource = dal.getBuscarMaterial(null, null);
            grvMaterialesSeleccionar.DataBind();
        }

        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox img = (CheckBox)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
                TextBox _txtCantidadSeleccionada = (TextBox)grvMaterialesSeleccionar.Rows[row.RowIndex].FindControl("txtCantidadSeleccionada");
                if (img.Checked==true)
                {
                    _txtCantidadSeleccionada.Enabled = true;
                }
                else
                {
                    _txtCantidadSeleccionada.Enabled = false;
                }
            }
            catch (Exception ex )
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void txtCantidadSeleccionada_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox img = (TextBox)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
                
                TextBox _txtCantidadSeleccionada = (TextBox)grvMaterialesSeleccionar.Rows[row.RowIndex].FindControl("txtCantidadSeleccionada");
                Label _lblCantidadDisponible = (Label)grvMaterialesSeleccionar.Rows[row.RowIndex].FindControl("lblCantidadDisponible");

                int stock = Convert.ToInt32(_lblCantidadDisponible.Text);
                int cantidad = Convert.ToInt32(_txtCantidadSeleccionada.Text);
                //if (stock < cantidad)
                //{
                //    lblInformacion.Text = "El stock es menor a lo solicitado";
                //    mdlInformacion.Show();
                //    return;
                //}
                //else
                //{

                //}

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
                
                foreach (GridViewRow item in grvMaterialesSeleccionar.Rows)
                {
                    CheckBox _chkSeleccionar = (CheckBox)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("chkSeleccionar");

                    if (_chkSeleccionar.Checked == true)
                    {
                        TextBox _txtCantidadSeleccionada = (TextBox)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("txtCantidadSeleccionada");
                        Label _lblIdMaterial = (Label)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("lblIdMaterial");
                        Label _lblCantidadSeleccionada = (Label)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("lblCantidadSeleccionada");
                        Label _lblCantidadDisponible = (Label)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("lblCantidadDisponible");
                        int cantidadSeleccionadaText = Convert.ToInt32(_txtCantidadSeleccionada.Text);
                        if (_lblCantidadSeleccionada.Text==string.Empty)
                        {
                            _lblCantidadSeleccionada.Text = "0";
                        }
                        if (_lblCantidadDisponible.Text==string.Empty)
                        {
                            _lblCantidadDisponible.Text = "0";
                        }
                        int cantidadSeleccionadaLabel = Convert.ToInt32(_lblCantidadSeleccionada.Text);
                        int cantidadDisponible = Convert.ToInt32(_lblCantidadDisponible.Text);

                        if (_txtCantidadSeleccionada.Text != string.Empty)
                        {
                            if (cantidadSeleccionadaText > cantidadDisponible)
                            {
                                lblInformacion.Text = "Advertencia: No hay materiales suficiente para el producto " + lblNombreProducto.Text;
                                mdlInformacion.Show();
                                return;
                            }
                            else
                            {
                                //int resto = cantidadSeleccionadaLabel - cantidadSeleccionadaText;
                                //dal.setEditarMaterialResto(Convert.ToInt32(_lblIdMaterial.Text), resto);
                            }
                        }
                    }
                   
                }

                foreach (GridViewRow item in grvMaterialesSeleccionar.Rows)
                {
                    CheckBox _chkSeleccionar = (CheckBox)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("chkSeleccionar");

                    if (_chkSeleccionar.Checked == true)
                    {
                        TextBox _txtCantidadSeleccionada = (TextBox)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("txtCantidadSeleccionada");
                        Label _lblIdMaterial = (Label)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("lblIdMaterial");
                        Label _lblCantidadSeleccionada = (Label)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("lblCantidadSeleccionada");
                        Label _lblCantidadDisponible = (Label)grvMaterialesSeleccionar.Rows[item.RowIndex].FindControl("lblCantidadDisponible");

                        if (_txtCantidadSeleccionada.Text != string.Empty)
                        {
                            int cantidadSeleccionadaText = Convert.ToInt32(_txtCantidadSeleccionada.Text);
                            int cantidadSeleccionadaLabel = Convert.ToInt32(_lblCantidadSeleccionada.Text);
                            int cantidadDisponible = Convert.ToInt32(_lblCantidadDisponible.Text);
                            if (cantidadSeleccionadaText > cantidadDisponible)
                            {
                                lblInformacion.Text = "Advertencia: No hay materiales suficiente para el producto " + lblNombreProducto.Text;
                                mdlInformacion.Show();
                                return;
                            }
                            else
                            {
                                int esta=0;
                                foreach (GridViewRow row in grvMateriales.Rows)
                                {
                                    Label _lblIdMaterial2 = (Label)grvMateriales.Rows[row.RowIndex].FindControl("lblIdMaterial");
                                    if (_lblIdMaterial.Text == _lblIdMaterial2.Text)
                                    {
                                        esta = 1;
                                    }
                                    else
                                    {
                                        esta = 0;
                                    }
                                }

                                if (esta==1)
                                {
                                    dal.setEditarMaterialProducto(Convert.ToInt32(_lblIdMaterial.Text), Convert.ToInt32(hfIdProducto.Value), cantidadSeleccionadaText);
                                }
                                else
                                {
                                    dal.setIngresarMaterialProducto(Convert.ToInt32(_lblIdMaterial.Text), Convert.ToInt32(hfIdProducto.Value), cantidadSeleccionadaText);
                                }
                                //int resto = cantidadSeleccionadaLabel - cantidadSeleccionadaText;
                                //dal.setEditarMaterialResto(Convert.ToInt32(_lblIdMaterial.Text), resto);

                            }
                        }
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
    }
}