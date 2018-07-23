using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.IO;
using System.Collections.Specialized;
using DataTableToExcel;
using crm_valvulas_industriales;

namespace crm_fadonel
{
    public partial class Cliente : System.Web.UI.Page
    {
        Datos dal = new Datos();
        Comun comunes = new Comun();
        public string carpeta = "biblioteca/";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnGrabarArchivo);
                scriptManager.RegisterPostBackControl(this.ibtnExportarExcel);

                if (!Page.IsPostBack)
                {
                    estado();
                    buscar();
                    buscarCondicionVenta();
                    buscarCampana();
                    buscarActividadComercial();
                    

                    string _idLead = Convert.ToString(Request.QueryString["idLead"]);
                    if (_idLead != null)
                    {
                        limpiar(this.Controls);
                        DataTable dt = new DataTable();
                        dt = dal.getBuscarLead(Convert.ToInt32(_idLead), null,null).Tables[0];
                        foreach (DataRow item in dt.Rows)
                        {
                            txtRazonSocial.Text = item["EMPRESA"].ToString();
                            txtNombreCorto.Text= item["EMPRESA"].ToString();
                            txtDireccion.Text = item["DIRECCION"].ToString();
                            txtComuna.Text= item["COMUNA"].ToString();
                            txtTelefono.Text= item["TELEFONO_1"].ToString();
                            txtEmail.Text= item["EMAIL"].ToString();
                        }
                        hfIdLead.Value = _idLead;

                        btnAgregar.Visible = true;
                        btnModificar.Visible = false;
                        
                        lblAgregarUsuario.Text = "Nuevo Cliente";
                        mdlAgregarCliente.Show();
                        
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

        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Visible = true;
                btnModificar.Visible = false;

                limpiar(this.Controls);

                lblAgregarUsuario.Text = "Nuevo Cliente";
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

                lblAgregarUsuario.Text = "Modificar Cliente";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdCliente = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblIdCliente");
                Label _lblRut = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblRut");
                Label _lblRazonSocial = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblRazonSocial");
                Label _lblNombreCorto = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblNombreCorto");
                Label _lblTelefono = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblTelefono");
                Label _lblDireccion = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblDireccion");
                Label _lblEmail = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblEmail");
                Label _lblActivo = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblActivo");
                Label _lblComuna = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblComuna");
                Label _lblCiudad = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblCiudad");
                Label _lblInfo = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblInfo");
                Label _lblMontoCredito = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblMontoCredito");
                Label _lblClasificacion = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblClasificacion");
                Label _lblZona = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblZona");
                Label _lblIdEstadoCliente = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblIdEstadoCliente");
                Label _lblGiro = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblGiro");
                Label _lblIdCondicionVenta = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblIdCondicionVenta");

                Label _lblActivo1 = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblActivo1");
                
                Label _lblIdActividadComercial = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblIdActividadComercial");
                Label _lblUrl = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblUrl");
                Label _lblIdCampana = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblIdCampana");
                Label _lblObservacion = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblObservacion");

                Label _lblRutClientePadre = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblRutClientePadre");


                limpiar(this.Controls);
                hfIdUsuario.Value = _lblRut.Text;
                hfIdCliente.Value = _lblIdCliente.Text;
                txtRazonSocial.Text = _lblRazonSocial.Text;
                txtRut.Text = _lblRut.Text;
                txtNombreCorto.Text = _lblNombreCorto.Text;
                txtDireccion.Text = _lblDireccion.Text;
                txtComuna.Text = _lblComuna.Text;
                txtCiudad.Text = _lblCiudad.Text;
                txtTelefono.Text = _lblTelefono.Text;
                txtEmail.Text = _lblEmail.Text;
                txtInfo.Text = _lblInfo.Text;
                txtMontoCredito.Text = _lblMontoCredito.Text;
                txtClasificacion.Text = _lblClasificacion.Text;
                txtZona.Text = _lblZona.Text;
                txtGiro.Text = _lblGiro.Text;

                if (_lblIdCondicionVenta.Text != string.Empty)
                {
                    ddlCondicionDeVenta.SelectedValue = _lblIdCondicionVenta.Text;
                }

                if (_lblIdEstadoCliente.Text != string.Empty)
                {
                    ddlEstado.SelectedValue = _lblIdEstadoCliente.Text;
                }

                if (_lblIdActividadComercial.Text != string.Empty)
                {
                    ddlActividadComercial.SelectedValue = _lblIdActividadComercial.Text;
                }

                if (_lblIdCampana.Text != string.Empty)
                {
                    ddlCampana.SelectedValue = _lblIdCampana.Text;
                }

                txtUrl.Text = _lblUrl.Text;
                txtObservacion.Text = _lblObservacion.Text;

                if (_lblActivo1.Text != string.Empty)
                {
                    ddlActivo.SelectedValue = _lblActivo1.Text;
                }

                txtRutPadre.Text = _lblRutClientePadre.Text;
                
                //lblIdEstadoCliente

                //if (_lblCaducidad.Text == string.Empty)
                //{
                //    txtCaducidad.Text = "0";
                //}
                //else
                //{
                //    txtCaducidad.Text = _lblCaducidad.Text;
                //}


                //if (_lblIdComuna.Text != string.Empty)
                //{
                //    ddlComuna.SelectedValue = _lblIdComuna.Text;
                //}

                mdlAgregarCliente.Show();
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
                if (txtMontoCredito.Text == string.Empty)
                {
                    txtMontoCredito.Text = null;
                }
                string idUsuario = Session["variableIdUsuario"].ToString();
                dal.setEditarCliente(hfIdCliente.Value, txtRut.Text, txtRazonSocial.Text, txtNombreCorto.Text, 
                    txtTelefono.Text, txtDireccion.Text, txtComuna.Text, txtCiudad.Text, 
                    txtEmail.Text, txtInfo.Text, txtMontoCredito.Text, txtClasificacion.Text, 
                    txtZona.Text, ddlEstado.SelectedValue, idUsuario,txtGiro.Text,
                    ddlCondicionDeVenta.SelectedValue,txtUrl.Text,
                    ddlActividadComercial.SelectedValue,txtObservacion.Text,ddlCampana.SelectedValue,ddlActivo.SelectedValue,txtRutPadre.Text.Trim());
                
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
                if (txtMontoCredito.Text == string.Empty)
                {
                    txtMontoCredito.Text = null;
                }

                //if (txtRut.Text == string.Empty)
                //{
                //    lblInformacion.Text = "El campo Rut es obligatorio.";
                //    mdlInformacion.Show();
                //    return;
                //}

                if (txtRut.Text != string.Empty)
                {
                    if (comunes.validarRut(txtRut.Text) == false)
                    {
                        lblInformacion.Text = "El rut no es valido.";
                        mdlInformacion.Show();
                        return;
                    }
                }

                string idUsuario = Session["variableIdUsuario"].ToString();
                dal.setIngresarCliente(txtRut.Text, txtRazonSocial.Text, txtNombreCorto.Text, 
                    txtTelefono.Text, txtDireccion.Text, txtComuna.Text, txtCiudad.Text, 
                    txtEmail.Text, txtInfo.Text, txtMontoCredito.Text, txtClasificacion.Text, 
                    txtZona.Text, ddlEstado.SelectedValue, idUsuario,txtGiro.Text, 
                    ddlCondicionDeVenta.SelectedValue,txtUrl.Text,ddlActividadComercial.SelectedValue,
                    txtObservacion.Text,ddlCampana.SelectedValue,ddlActivo.SelectedValue,txtRutPadre.Text.Trim(),hfIdLead.Value);

                dal.setIngresarContacto(txtRazonSocial.Text, txtRut.Text, txtEmail.Text, txtEmail1.Text, txtCelular.Text, txtTelefono.Text, txtTelefono1.Text, ddlCargo.SelectedValue);
                //In.setIngresarCliente(txtRazonSocial.Text, txtNombreCorto.Text, txtRut.Text, txtDireccion.Text, ddlComuna.SelectedValue, txtCiudad.Text, txtFono1.Text, txtFono2.Text, txtRepLegal.Text, txtRepLegalRut.Text, txtNombreContacto.Text, txtEmailContacto.Text, ddlActivo.SelectedValue, "", txtCaducidad.Text, txtPlazoBarrido.Text);
                buscar();
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

                Label _lblRut = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblRut");
                dal.setEliminarCliente(_lblRut.Text);
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
            dt = dal.getBuscarCliente(txtBuscar.Text, txtBuscar.Text,null).Tables[0];
            Session["datosCliente"] = dt;

            grvClientes.DataSource = dt;
            grvClientes.DataBind();
        }

        void buscarCondicionVenta() 
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarCondicionVenta(null).Tables[0];
            ddlCondicionDeVenta.DataSource = dt;
            ddlCondicionDeVenta.DataTextField = "CONDICION_VENTA";
            ddlCondicionDeVenta.DataValueField = "ID_COND_VENTA";
            ddlCondicionDeVenta.DataBind();
        }


        protected void imgFirst_Click(object sender, EventArgs e)
        {
            buscar();

            grvClientes.PageIndex = 0;
            grvClientes.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvClientes.PageIndex != 0)
                grvClientes.PageIndex--;
            grvClientes.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvClientes.PageIndex != (grvClientes.PageCount - 1))
                grvClientes.PageIndex++;
            grvClientes.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            buscar();
            grvClientes.PageIndex = grvClientes.PageCount - 1;
            grvClientes.DataBind();
        }


        protected void imgContactos_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblRut = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblRut");
                hfRutCliente.Value = _lblRut.Text;
                buscarContactos(_lblRut.Text);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarContactos(string rutCliente)
        {
            grvContactos.DataSource = dal.getBuscarContactoPorRutCliente(rutCliente);
            grvContactos.DataBind();
            mdlContactos.Show();
        }

        protected void btnNuevoContacto_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabarContacto.Visible = true;
                btnModificarContacto.Visible = false;
                buscarContactoCargo();
                limpiar(this.Controls);

                lblAgregarContacto.Text = "Nuevo Contacto";
                limpiar(this.Controls);
                mdlAgregarContacto.Show();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEditarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabarContacto.Visible = false;
                btnModificarContacto.Visible = true;

                buscarContactoCargo();

                lblAgregarContacto.Text = "Modificar Contacto";

                limpiar(this.Controls);

                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdContacto = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblIdContacto");
                Label _lblNombreContacto = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblNombreContacto");
                Label _lblRutCliente = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblRutCliente");
                Label _lblEmail1 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblEmail1");
                Label _lblEmail2 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblEmail2");
                Label _lblCelular = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblCelular");
                Label _lblTelefono1 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblTelefono1");
                Label _lblTelefono2 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblTelefono2");




                hfIdContacto.Value = _lblIdContacto.Text;
                hfRutCliente.Value = _lblRutCliente.Text;
                txtNombreContacto.Text = _lblNombreContacto.Text;
                txtEmail1.Text = _lblEmail1.Text;
                txtEmail2.Text = _lblEmail2.Text;
                txtCelular.Text = _lblCelular.Text;
                txtTelefono1.Text = _lblTelefono1.Text;
                txtTelefono2.Text = _lblTelefono2.Text;

                mdlAgregarContacto.Show();
                    
        
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEliminarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdContacto = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblIdContacto");
                Label _lblRutCliente = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblRutCliente");
                dal.setEliminarContacto(_lblIdContacto.Text);
                buscarContactos(_lblRutCliente.Text);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
       
        protected void btnGrabarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                string rutCliente = hfRutCliente.Value;
                dal.setIngresarContacto(txtNombreContacto.Text, rutCliente, txtEmail1.Text, txtEmail2.Text, txtCelular.Text, txtTelefono1.Text, txtTelefono2.Text,ddlCargo.SelectedValue);
                buscarContactos(rutCliente);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnModificarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                dal.setEditarContacto(hfIdContacto.Value, txtNombreContacto.Text, hfRutCliente.Value, txtEmail1.Text, txtEmail2.Text, txtCelular.Text, txtTelefono1.Text, txtTelefono2.Text,ddlCargo.SelectedValue);
                buscarContactos(hfRutCliente.Value);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
            
        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvClientes.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvClientes.PageCount);
            }
        }

        void estado()
        {
            ddlEstado.DataSource = dal.getBuscarEstadoCliente(null, "");
            ddlEstado.DataValueField = "ID_ESTADO_CLIENTE";
            ddlEstado.DataTextField = "NOM_ESTADO_CLIENTE";
            ddlEstado.DataBind();
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

        protected void imgBilioteca_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblRut = (Label)grvClientes.Rows[row.RowIndex].FindControl("lblRut");
                hfRutClienteArchivo.Value = _lblRut.Text;
                limpiar(this.Controls);
                buscarArchivos(_lblRut.Text);
                mdlAgregarArchivo.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarArchivos(string rutCliente) 
        {
            grvDetalleBiblioteca.DataSource = dal.getBuscarArchivos(rutCliente);
            grvDetalleBiblioteca.DataBind();
        }

        protected void btnGrabarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuArchivo.HasFile)
                {
                    string nombreArchivo = fuArchivo.FileName;
                    string ruta = carpeta + nombreArchivo;

                    if (System.IO.File.Exists(ruta))
                    {
                        lblInformacion.Text = "El archivo ya existe. Sube un archivo con diferente nombre";
                        mdlInformacion.Show();
                        return;
                    }

                    fuArchivo.SaveAs(Server.MapPath(ruta));
                    dal.setIngresarArchivo(hfRutClienteArchivo.Value, nombreArchivo, txtNombre.Text, ruta);
                    buscarArchivos(hfRutClienteArchivo.Value);
                    mdlAgregarArchivo.Show();
                }
                else
                {

                }
                
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void imgEliminarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblId = (Label)grvDetalleBiblioteca.Rows[row.RowIndex].FindControl("lblId");
                Label _lblNombreArchivo = (Label)grvDetalleBiblioteca.Rows[row.RowIndex].FindControl("lblNombreArchivo");
                dal.setEliminarArchivo(_lblId.Text);
                File.Delete(Server.MapPath(carpeta + _lblNombreArchivo.Text));
                buscarArchivos(hfRutClienteArchivo.Value);
                mdlAgregarArchivo.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }



        protected void grvDetalleBiblioteca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton _imgVisualizar = (ImageButton)e.Row.FindControl("imgVisualizar");
                Label _lblNombreArchivo = (Label)e.Row.FindControl("lblNombreArchivo");
                ImageButton _imgEliminar = (ImageButton)e.Row.FindControl("imgEliminar");
                //Label _lblUsuario = (Label)e.Row.FindControl("lblUsuario");

                ////PERFILES
                //DataTable dt = new DataTable();
                //dt = dal.getBuscarPerfil(Session["variableUsuario"].ToString()).Tables[0];
                //string usuario = Session["variableUsuario"].ToString();

                //foreach (DataRow item in dt.Rows)
                //{
                //    string perfil = item["ID_PERFIL"].ToString();

                //    if (perfil == "1")
                //    {
                //        break;
                //    }

                //    if (perfil == "2")
                //    {
                //        if (usuario == _lblUsuario.Text)
                //        {
                //            _imgEliminar.Visible = true;
                //        }
                //        else
                //        {
                //            _imgEliminar.Visible = false;
                //        }
                //        break;
                //    }

                //    if (perfil == "3")
                //    {
                //        _imgEliminar.Visible = false;
                //        break;
                //    }
                //}
                ////FIN PERFILES



                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

                string nombreArchivo = _lblNombreArchivo.Text;
                string tipoArchivo = nombreArchivo;
                tipoArchivo = tipoArchivo.Substring(tipoArchivo.LastIndexOf(".") + 1).ToLower();

                FileInfo fi = new FileInfo(Server.MapPath(carpeta + _lblNombreArchivo.Text));
                string extension = fi.Extension.ToString().ToLower();

                NameValueCollection imageExtensions = new NameValueCollection();
                imageExtensions.Add(".jpg", "image/jpeg");
                imageExtensions.Add(".gif", "image/gif");
                imageExtensions.Add(".png", "image/png");
                imageExtensions.Add(".tiff", "image/tiff");
                imageExtensions.Add(".bmp", "image/bmp");

                if (imageExtensions.AllKeys.Contains(extension))
                {
                    _imgVisualizar.ImageUrl = carpeta + _lblNombreArchivo.Text;
                    _imgVisualizar.Height = 32;
                }
                else
                {
                    scriptManager.RegisterPostBackControl(_imgVisualizar);
                }

                if (tipoArchivo == "xls")
                {
                    _imgVisualizar.ImageUrl = "assets/img/file_extension_xls.png";
                }
                if (tipoArchivo == "xlsx")
                {
                    _imgVisualizar.ImageUrl = "assets/img/file_extension_xls.png";
                }
                if (tipoArchivo == "doc")
                {
                    _imgVisualizar.ImageUrl = "assets/img/file_extension_doc.png";
                }
                if (tipoArchivo == "docx")
                {
                    _imgVisualizar.ImageUrl = "assets/img/file_extension_doc.png";
                }
                if (tipoArchivo == "rar")
                {
                    _imgVisualizar.ImageUrl = "assets/img/file_extension_rar.png";
                }
                if (tipoArchivo == "zip")
                {
                    _imgVisualizar.ImageUrl = "assets/img/file_extension_zip.png";
                }
                if (tipoArchivo == "pdf")
                {
                    _imgVisualizar.ImageUrl = "assets/img/file_extension_pdf.png";
                }

                //DataTable dt = new DataTable();
                //dt = Bus.getBuscarPerfilPorUsuario(Session["variableIdUsuario"].ToString()).Tables[0];

                //foreach (DataRow item in dt.Rows)
                //{
                //string perfil = item["ID_PERFIL"].ToString();
                //if (perfil == "1")
                //{
                //    break;
                //}

                //if (perfil == "4")
                //{
                //    string usuarioGrv = _lblUsuario.Text;
                //    string usuarioSession = Session["variableUsuario"].ToString();

                //    if (usuarioSession == usuarioGrv)
                //    {
                //        _imgEliminar.Visible = true;
                //    }
                //    else
                //    {
                //        _imgEliminar.Visible = false;
                //    }

                //    break;
                //}

                //if (perfil == "5")
                //{
                //    _imgEliminar.Visible = false;
                //    break;
                //}
                //}
            }
        }


        protected void imgVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblId = (Label)grvDetalleBiblioteca.Rows[row.RowIndex].FindControl("lblId");
                Label _lblNombreArchivo = (Label)grvDetalleBiblioteca.Rows[row.RowIndex].FindControl("lblNombreArchivo");

                string url = Server.MapPath(carpeta + _lblNombreArchivo.Text);

                FileInfo fi = new FileInfo(url);
                string extension = fi.Extension.ToString().ToLower();

                NameValueCollection imageExtensions = new NameValueCollection();
                imageExtensions.Add(".jpg", "image/jpeg");
                imageExtensions.Add(".gif", "image/gif");
                imageExtensions.Add(".png", "image/png");
                imageExtensions.Add(".tiff", "image/tiff");
                imageExtensions.Add(".bmp", "image/bmp");

                if (imageExtensions.AllKeys.Contains(extension))
                {
                    ViewState["mdlId"] = 1;
                    imgFoto.ImageUrl = carpeta + _lblNombreArchivo.Text;
                    mdlImagen.Show();
                }
                else
                {
                    Response.ContentType = extension;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                    Response.TransmitFile(fi.FullName);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ViewState["mdlId"] = 0;
                lblInformacion.Text = ex.Message;
                //mdlInformacion.Show();
            }
        }



        protected void imgCerrar_Click(object sender, EventArgs e)
        {
            switch (Convert.ToString(ViewState["mdlId"]))
            {
                case "0":
                    mdlInformacion.Hide();
                    break;
                case "1":
                    //mdlDetalleBiblioteca.Show();
                    break;
                case "2":
                    mdlImagen.Show();
                    break;
            }
        }

        void buscarContactoCargo()
        {
            string idEmpresa = Session["idEmpresa"].ToString();
            ddlCargo.DataSource = dal.getBuscarContactoCargo(null, null, idEmpresa);
            ddlCargo.DataValueField = "ID_CONTACTO_CARGO";
            ddlCargo.DataTextField = "CONTACTO_CARGO";
            ddlCargo.DataBind();
        }

        protected void ibtnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dal.getBuscarClienteExporte(txtBuscar.Text, txtBuscar.Text).Tables[0];
               
                Utilidad.ExportDataTableToExcel(dt, "Exporte_Clientes.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarCampana()
        {
            ddlCampana.DataSource = dal.getBuscarCampana(null,null);
            ddlCampana.DataValueField = "ID_CAMPANA";
            ddlCampana.DataTextField = "NOM_CAMPANA";
            ddlCampana.DataBind();
        }

        void buscarActividadComercial()
        {
            ddlActividadComercial.DataSource = dal.getBuscarActividadComercial();
            ddlActividadComercial.DataValueField = "ID_ACTIVIDAD_COMERCIAL";
            ddlActividadComercial.DataTextField = "NOM_ACTIVIDAD_COMERCIAL";
            ddlActividadComercial.DataBind();
        }

        protected void ddlCampana_DataBound(object sender, EventArgs e)
        {
            ddlCampana.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void ddlActividadComercial_DataBound(object sender, EventArgs e)
        {
            ddlActividadComercial.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        
        
    }
}