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
    public partial class Usuarios : System.Web.UI.Page
    {
        Datos dal = new Datos();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //string perfil = Session["variablePerfil"].ToString();
                    //if (perfil != "Admin")
                    //{
                    //    Response.Redirect("Default.aspx");
                    //}
                    buscarPerfil();
                    buscarEmpresa();
                    buscar();
                }
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

                lblAgregarUsuario.Text = "Editar Usuario";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblidUsuario = (Label)grvUsuarios.Rows[row.RowIndex].FindControl("lblIdUsuario");

                limpiar();

                hfIdUsuario.Value = _lblidUsuario.Text;

                DataSet ds = dal.getBuscarUsuario(null, hfIdUsuario.Value);
                DataTable dt = ds.Tables[0];

                foreach (DataRow rowDs in dt.Rows)
                {
                    txtUsuario.Text = Convert.ToString(rowDs["USUARIO"]);
                    txtDescuento.Text = Convert.ToString(rowDs["DESCUENTO_AUTORIZADO"]);
                    txtContrasena.Attributes.Add("Value", Convert.ToString(rowDs["CONTRASENA"]));
                    txtConfirmarContrasena.Attributes.Add("Value", Convert.ToString(rowDs["CONTRASENA"]));
                    ddlPerfil.SelectedValue = Convert.ToString(rowDs["PERFIL"]);
                    txtNombre.Text = Convert.ToString(rowDs["NOMBRE"]);
                    ddlActivo.SelectedValue = Convert.ToString(rowDs["ACTIVO"]);
                }

                foreach (DataRow item in dal.getBuscarEmpresaPorUsuario(_lblidUsuario.Text).Tables[0].Rows)
                {
                    string idCliente = Convert.ToString(item["ID_EMPRESA"]);
                    foreach (ListItem fila in chkEmpresas.Items)
                    {
                        if (idCliente == fila.Value.ToString())
                        {
                            fila.Selected = true;
                        }
                    }
                }

                mdlAgregarUsuario.Show();
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

                Label _lblidUsuario = (Label)grvUsuarios.Rows[row.RowIndex].FindControl("lblIdUsuario");

                dal.setEliminarEmpresaPorUsuario(_lblidUsuario.Text);
                dal.setEliminarUsuario(_lblidUsuario.Text);
                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgAceptar_Click(object sender, EventArgs e)
        {
            try
            {


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
                if (txtUsuario.Text == "" || txtContrasena.Text == "" || txtConfirmarContrasena.Text == "")
                {
                    lblInformacion.Text = "Favor de completar los campos con el signo '*'";
                    mdlInformacion.Show();
                }
                else
                {
                    if (txtContrasena.Text == txtConfirmarContrasena.Text)
                    {

                        dal.setEliminarEmpresaPorUsuario(hfIdUsuario.Value);
                        foreach (ListItem item in chkEmpresas.Items)
                        {
                            if (item.Selected == true)
                            {
                                string valorCheckIdCliente = item.Value;
                                dal.setIngresarEmpresaPorUsuario(hfIdUsuario.Value, valorCheckIdCliente);
                            }
                        }
                        string descuento = txtDescuento.Text.Replace(",", ".");
                        dal.setEditarUsuario(hfIdUsuario.Value, txtUsuario.Text, txtContrasena.Text, ddlPerfil.SelectedValue, txtNombre.Text, ddlActivo.SelectedValue, descuento);
                        hfIdUsuario.Value = "";
                        buscar();
                    }
                    else
                    {
                        lblInformacion.Text = "Las contraseñas no coinciden";
                        mdlInformacion.Show();
                    }
                }
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
                if (txtUsuario.Text == "" || txtContrasena.Text == "" || txtConfirmarContrasena.Text == "")
                {
                    lblInformacion.Text = "Favor de completar los campos con el signo '*'";
                    mdlInformacion.Show();
                }
                else
                {
                    if (txtContrasena.Text == txtConfirmarContrasena.Text)
                    {
                        string idUsuario = dal.getIngresarUsuario(txtUsuario.Text, txtContrasena.Text, ddlPerfil.SelectedValue, txtNombre.Text, ddlActivo.SelectedValue,txtDescuento.Text.Replace(",","."));

                        foreach (ListItem item in chkEmpresas.Items)
                        {
                            if (item.Selected == true)
                            {
                                string valorCheckIdCliente = item.Value;
                                dal.setIngresarEmpresaPorUsuario(idUsuario, valorCheckIdCliente);
                            }
                        }
                        buscar();
                    }
                    else
                    {
                        lblInformacion.Text = "Las contraseñas no coinciden";
                        mdlInformacion.Show();
                    }
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
            buscar();
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            btnAgregar.Visible = true;
            btnModificar.Visible = false;

            limpiar();

            mdlAgregarUsuario.Show();
            lblAgregarUsuario.Text = "Nuevo Usuario";
        }

        private void buscar()
        {
            try
            {
                string nombre = "%" + txtBuscar.Text + "%";
                ds = dal.getBuscarUsuario(nombre, "");
                grvUsuarios.DataSource = ds;
                grvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        public void limpiar()
        {
            txtConfirmarContrasena.Attributes.Add("Value", "");
            txtContrasena.Attributes.Add("Value", "");
            txtNombre.Text = "";
            txtUsuario.Text = "";
            ddlActivo.ClearSelection();
            ddlPerfil.ClearSelection();
            chkEmpresas.ClearSelection();
            txtDescuento.Text = string.Empty;
        }

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            buscar();

            grvUsuarios.PageIndex = 0;
            grvUsuarios.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvUsuarios.PageIndex != 0)
                grvUsuarios.PageIndex--;
            grvUsuarios.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvUsuarios.PageIndex != (grvUsuarios.PageCount - 1))
                grvUsuarios.PageIndex++;
            grvUsuarios.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            buscar();
            grvUsuarios.PageIndex = grvUsuarios.PageCount - 1;
            grvUsuarios.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvUsuarios.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvUsuarios.PageCount);
            }
        }


        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }

        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            buscar();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            {
                string SortDir = string.Empty;
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                grvUsuarios.DataSource = sortedView;
                grvUsuarios.DataBind();
            }
        }

        void buscarPerfil() 
        {
            ddlPerfil.DataSource = dal.getBuscarPerfil("");
            ddlPerfil.DataValueField = "ID_PERFIL";
            ddlPerfil.DataTextField = "NOMBRE_PERFIL";
            ddlPerfil.DataBind();
        }

        void buscarEmpresa()
        {
            chkEmpresas.DataSource = dal.getBuscarEmpresa("", null);
            chkEmpresas.DataValueField = "ID_EMPRESA";
            chkEmpresas.DataTextField = "NOMBRE_EMPRESA";
            chkEmpresas.DataBind();
        }
    }
}