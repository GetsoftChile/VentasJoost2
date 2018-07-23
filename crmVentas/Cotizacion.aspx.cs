using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections;

namespace crm_fadonel
{
    public partial class Cotizacion : System.Web.UI.Page
    {
        Datos dal = new Datos();
        //double montoNeto;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!this.Page.IsPostBack)
                //{
                //    DataTable dt = new DataTable();
                //    Session["dtDetalleProductos"] = dt;

                //    string _rutCliente = Convert.ToString(Request.QueryString["c"]);
                //    if (_rutCliente == null)
                //    {
                //        _rutCliente = Session["rutCliente"].ToString();
                //    }

                //    hfRutClientePost.Value = _rutCliente;
                //    hfRutCliente.Value = _rutCliente;

                //    dt.Clear();
                //    CreateDataTable();
                //    buscarContactoCargo();
                //    canal();
                //    ddlCanal.SelectedValue = "1";
                //    buscarCliente(_rutCliente);
                //    buscarRazonSocial();
                //}

                if (this.Page.IsPostBack)
                    return;
                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl((Control)this.btnGrabarCotizacion);
                this.btnGrabarCotizacion.Attributes.Add("OnClick", string.Format("this.disabled = true; {0};", (object)this.ClientScript.GetPostBackEventReference((Control)this.btnGrabarCotizacion, (string)null)));
                DataTable dataTable = new DataTable();
                this.Session["dtDetalleProductos"] = (object)dataTable;
                string idCliente = Convert.ToString(this.Request.QueryString["c"]) ?? this.Session["IdCliente"].ToString();
                this.hfRutClientePost.Value = idCliente;
                this.hfRutCliente.Value = idCliente;
                this.lblIdCliente.Text = idCliente;
                dataTable.Clear();
                this.CreateDataTable();
                this.buscarContactoCargo();
                this.canal();
                this.ddlCanal.SelectedValue = "1";
                this.buscarCliente(idCliente);
                this.buscarRazonSocial();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarRazonSocial()
        {
            ddlRazonSocial.DataSource = dal.getBuscarRazonSocial(null);
            ddlRazonSocial.DataTextField = "NOMBRE";
            ddlRazonSocial.DataValueField = "ID_RAZON_SOCIAL";
            ddlRazonSocial.DataBind();
        }

        protected void ddlCanal_DataBound(object sender, EventArgs e)
        {
            ddlCanal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void CreateDataTable()
        {
            try
            {
                DataTable dt = Session["dtDetalleProductos"] as DataTable;

                dt.Columns.Add("ID", typeof(Int32));
                dt.Columns.Add("ID_PRODUCTO", typeof(String));
                dt.Columns.Add("CODIGO", typeof(String));
                dt.Columns.Add("CORRELATIVO", typeof(String));
                dt.Columns.Add("NOMBRE_PRODUCTO", typeof(String));
                dt.Columns.Add("CANTIDAD", typeof(String));
                dt.Columns.Add("MONTO_UNI", typeof(String));
                dt.Columns.Add("MONTO_NETO", typeof(String));
                dt.Columns.Add("DESCUENTO_PORCENTAJE", typeof(String));
                dt.Columns.Add("DESCUENTO_MONTO", typeof(String));
                dt.Columns.Add("MONTO_TOTAL", typeof(String));
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                //mdlInformacion.Show();
            }
        }


        protected void txtDescuento_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                TextBox img = (TextBox)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                TextBox _txtCantidad = (TextBox)grvProductos.Rows[row.RowIndex].FindControl("txtCantidad");
                TextBox _txtMontoDescuento = (TextBox)grvProductos.Rows[row.RowIndex].FindControl("txtMontoDescuento");
                TextBox _txtDescuento = (TextBox)grvProductos.Rows[row.RowIndex].FindControl("txtDescuento");
                Label _lblValorVenta = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblValorVenta");

                double descuento = 0;
                if (_txtDescuento.Text == string.Empty)
                {
                    descuento = 0;
                }
                else
                {
                    descuento = Convert.ToDouble(_txtDescuento.Text);
                }

                double cantidad = 1;
                double valorVenta = 0;
                double valor = 0;
                if (_txtCantidad.Text != string.Empty)
                {
                    cantidad = Convert.ToDouble(_txtCantidad.Text);
                    valorVenta = Convert.ToDouble(_lblValorVenta.Text);
                    valor = valorVenta * cantidad;
                }
                else
                {
                    valorVenta = Convert.ToDouble(_lblValorVenta.Text);
                    valor = valorVenta * cantidad;
                }

                string totalDescuento = ((valor * descuento) / 100).ToString();
                _txtMontoDescuento.Text = totalDescuento;

                mdlSeleccionarProductos.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        protected void txtMontoDescuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox img = (TextBox)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                TextBox _txtCantidad = (TextBox)grvProductos.Rows[row.RowIndex].FindControl("txtCantidad");
                TextBox _txtMontoDescuento = (TextBox)grvProductos.Rows[row.RowIndex].FindControl("txtMontoDescuento");
                TextBox _txtDescuento = (TextBox)grvProductos.Rows[row.RowIndex].FindControl("txtDescuento");
                Label _lblValorVenta = (Label)grvProductos.Rows[row.RowIndex].FindControl("lblValorVenta");

                double descuento = 0;
                if (_txtMontoDescuento.Text == string.Empty)
                {
                    descuento = 0;
                }
                else
                {
                    descuento = Convert.ToDouble(_txtMontoDescuento.Text);
                }

                double cantidad = 1;
                double valorVenta = 0;
                double valor = 0;
                if (_txtCantidad.Text != string.Empty)
                {
                    cantidad = Convert.ToDouble(_txtCantidad.Text);
                    valorVenta = Convert.ToDouble(_lblValorVenta.Text);
                    valor = valorVenta * cantidad;
                }
                else
                {
                    valorVenta = Convert.ToDouble(_lblValorVenta.Text);
                    valor = valorVenta * cantidad;
                }


                string totalPorcentaje = Math.Round(((descuento / valor) * 100), 2).ToString();
                _txtDescuento.Text = totalPorcentaje;

                mdlSeleccionarProductos.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        //void buscarCliente(string rut)
        //{
        //    if (rut != null)
        //    {
        //        DataTable dtCliente = new DataTable();
        //        dtCliente = dal.getBuscarClientePorRut(rut).Tables[0];
        //        foreach (DataRow item in dtCliente.Rows)
        //        {
        //            lblRutCotizacion.Text = item["RUT_CLIENTE"].ToString();
        //            lblCliente.Text = item["RAZON_SOCIAL"].ToString();
        //            lblCiudad.Text = item["CIUDAD"].ToString();
        //            lblComunaCotizacin.Text = item["COMUNA"].ToString();
        //            lblDireccionCotizacion.Text = item["DIRECCION"].ToString();
        //            lblTelefono.Text = item["TELEFONO"].ToString();

        //            string rutPadre = item["RUT_PADRE"].ToString();
        //            if (rutPadre.Trim() != string.Empty)
        //            {
        //                trRutClientePadreCotizacion.Visible = true;
        //                lblRutClientePadreCotizacion.Text = rutPadre;
        //                lblRazonSocialPadreCotizacion.Text = item["RAZON_PADRE"].ToString();

        //            }
        //            else
        //            {
        //                trRutClientePadreCotizacion.Visible = false;

        //            }

        //            break;
        //        }
        //    }
        //}
        private void buscarCliente(string idCliente)
        {
            if (idCliente == null)
                return;
            DataTable dataTable = new DataTable();
            IEnumerator enumerator = this.dal.getBuscarClientePorId(idCliente).Tables[0].Rows.GetEnumerator();
            try
            {
                if (!enumerator.MoveNext())
                    return;
                DataRow current = (DataRow)enumerator.Current;
                this.lblRutCotizacion.Text = current["RUT_CLIENTE"].ToString();
                this.lblCliente.Text = current["RAZON_SOCIAL"].ToString();
                this.lblCiudad.Text = current["CIUDAD"].ToString();
                this.lblComunaCotizacin.Text = current["COMUNA"].ToString();
                this.lblDireccionCotizacion.Text = current["DIRECCION"].ToString();
                this.lblTelefono.Text = current["TELEFONO"].ToString();
                string str = current["RUT_PADRE"].ToString();
                if (str.Trim() != string.Empty)
                {
                    this.trRutClientePadreCotizacion.Visible = true;
                    this.lblRutClientePadreCotizacion.Text = str;
                    this.lblRazonSocialPadreCotizacion.Text = current["RAZON_PADRE"].ToString();
                }
                else
                    this.trRutClientePadreCotizacion.Visible = false;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                grvProductos.DataSource = dal.getBuscarProducto(null, null, idEmpresa).Tables[0];
                grvProductos.DataBind();
                mdlSeleccionarProductos.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        
        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                grvProductos.DataSource = dal.getBuscarProducto(null, txtBuscarProducto.Text, idEmpresa).Tables[0];
                grvProductos.DataBind();
                mdlSeleccionarProductos.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        protected void imgValidacion_Click(object sender, EventArgs e)
        {
            //mdlSeleccionarProductos.Show();
        }
        protected void btnNuevoContacto_Click(object sender, EventArgs e)
        {
            try
            {
                btnGrabarContacto.Visible = true;
                btnModificarContacto.Visible = false;

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
        

        protected void imgAgregarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                //buscarContactos(hfRutClientePost.Value);
                //mdlContactos.Show();

                //if (lblRutCotizacion.Text == string.Empty)
                //{
                //    lblInformacion.Text = "No hay un cliente seleccionado, favor seleccionar un cliente";
                //    mdlInformacion.Show();
                //    return;
                //}

                //buscarContactosCotizacion(hfRutClientePost.Value);
                //mdlContactos.Show();

                this.buscarContactos(this.hfRutClientePost.Value);
                this.mdlContactos.Show();
                if (this.lblIdCliente.Text == string.Empty)
                {
                    this.lblInformacion.Text = "No hay un cliente seleccionado, favor seleccionar un cliente";
                    this.mdlInformacion.Show();
                }
                else
                {
                    this.buscarContactosCotizacion(this.hfRutClientePost.Value);
                    this.mdlContactos.Show();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarContactosCotizacion(string rutCliente)
        {
            grvContactosCotizacion.DataSource = dal.getBuscarContactoPorRutCliente(rutCliente);
            grvContactosCotizacion.DataBind();
            mdlContactos.Show();
        }

        protected void imgEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblId = (Label)grvProducto.Rows[row.RowIndex].FindControl("lblIdProducto");
                DataTable dt = Session["dtDetalleProductos"] as DataTable;
                dt.AcceptChanges();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ID_PRODUCTO"].ToString() == _lblId.Text)
                        dr.Delete();
                }
                dt.AcceptChanges();

                grvProducto.DataSource = dt;
                grvProducto.DataBind();

                calcularTotal();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgSeleccionarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdContacto = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblIdContacto");
                Label _lblNombreContacto = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblNombreContacto");
                Label _lblEmail1 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblEmail1");
                Label _lblEmail2 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblEmail2");
                Label _lblCelular = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblCelular");
                Label _lblTelefono1 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblTelefono1");
                Label _lblTelefono2 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblTelefono2");

                lblContacto.Text = _lblNombreContacto.Text;
                lblIdContacto.Text = _lblIdContacto.Text;
                lblEmail.Text = _lblEmail1.Text;
                lblEmail2.Text = _lblEmail2.Text;
                lblCelular.Text = _lblCelular.Text;
                lblTelefonoContacto.Text = _lblTelefono1.Text;
                lblTelefonoContacto2.Text = _lblTelefono2.Text;

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
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

        protected void btnGrabarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                string rutCliente = lblRutCotizacion.Text;
                dal.setIngresarContacto(txtNombreContacto.Text, rutCliente, txtEmail1.Text, txtEmail2.Text, txtCelular.Text, txtTelefono1.Text, txtTelefono2.Text, ddlCargo.SelectedValue);
                buscarContactos(lblRutCotizacion.Text);
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
                dal.setEditarContacto(hfIdContacto.Value, txtNombreContacto.Text, lblRutCotizacion.Text, txtEmail1.Text, txtEmail2.Text, txtCelular.Text, txtTelefono1.Text, txtTelefono2.Text, ddlCargo.SelectedValue);
                buscarContactos(lblRutCotizacion.Text);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        
        void buscarContactos(string rutCliente)
        {
            grvContactosCotizacion.DataSource = dal.getBuscarContactoPorRutCliente(rutCliente);
            grvContactosCotizacion.DataBind();
            mdlContactos.Show();
        }
        

        protected void btnGrabarYSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                string idUsuario = Session["variableIdUsuario"].ToString();
                DataTable dt = Session["dtDetalleProductos"] as DataTable;

                double porcentajeDescuentoPermitido = 0;

                foreach (DataRow item in dal.getBuscarUsuario(null, idUsuario).Tables[0].Rows)
                {
                    if (item["DESCUENTO_AUTORIZADO"].ToString() != string.Empty)
                    {
                        porcentajeDescuentoPermitido = Convert.ToDouble(item["DESCUENTO_AUTORIZADO"].ToString().Replace(".", ","));
                    }
                }

                foreach (GridViewRow item in grvProductos.Rows)
                {
                    TextBox _txtCantidad = (TextBox)grvProductos.Rows[item.RowIndex].FindControl("txtCantidad");
                    TextBox _txtDescuento = (TextBox)grvProductos.Rows[item.RowIndex].FindControl("txtDescuento");
                    TextBox _txtMontoDescuento = (TextBox)grvProductos.Rows[item.RowIndex].FindControl("txtMontoDescuento");
                    
                    if (_txtCantidad.Text != string.Empty)
                    {
                        int cantidad = Convert.ToInt32(_txtCantidad.Text);
                        Label _lblIdProducto = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblIdProducto");
                        Label _lblNombreProducto = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblNombreProducto");
                        Label _lblCodigo = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblCodigo");
                        Label _lblBodega = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblBodega");
                        Label _lblCostoUnitario = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblCostoUnitario");
                        Label _lblValorVenta = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblValorVenta");
                        Label _lblStock = (Label)grvProductos.Rows[item.RowIndex].FindControl("lblStock");

                        int stock = Convert.ToInt32(_lblStock.Text);
                        if (cantidad > stock)
                        {
                            lblInformacionValidacionProducto.Text = "Advertencia: No hay stock suficiente para el producto " + _lblNombreProducto.Text;
                            mdlInformacionValidacionProducto.Show();
                            calcularTotal();
                        }

                        double descuento = 0;
                        if (_txtDescuento.Text == string.Empty)
                        {
                            descuento = 0;
                        }
                        else
                        {
                            descuento = Convert.ToDouble(_txtDescuento.Text);
                        }

                        double montoDescuento = 0;
                        if (_txtMontoDescuento.Text == string.Empty)
                        {
                            montoDescuento = 0;
                        }
                        else
                        {
                            montoDescuento = Convert.ToDouble(_txtMontoDescuento.Text);
                        }

                        //if (descuento > porcentajeDescuentoPermitido)
                        //{
                        //    lblInformacionValidacionProducto.Text = "El porcentaje de descuento ingresado es mayor al permitido por su usuario, favor comunicarse con el administrador.";
                        //    mdlInformacionValidacionProducto.Show();
                        //    return;
                        //}

                        double monto = Convert.ToDouble(_lblValorVenta.Text) * Convert.ToDouble(_txtCantidad.Text);
                        double montoUni = Convert.ToDouble(_lblValorVenta.Text);
                        //int montoDescuento = calcularPorcentaje(descuento, monto);

                        int montoTotal = Convert.ToInt32(monto) - Convert.ToInt32(montoDescuento);
                        if (dt.Rows.Count == 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = "1";
                            dr["CORRELATIVO"] = "1";
                            dr["ID_PRODUCTO"] = _lblIdProducto.Text;
                            dr["CODIGO"] = _lblCodigo.Text;
                            dr["NOMBRE_PRODUCTO"] = _lblNombreProducto.Text;
                            dr["CANTIDAD"] = _txtCantidad.Text;
                            dr["MONTO_UNI"] = montoUni;
                            dr["MONTO_NETO"] = monto;
                            dr["DESCUENTO_PORCENTAJE"] = descuento.ToString();
                            dr["DESCUENTO_MONTO"] = montoDescuento.ToString();
                            dr["MONTO_TOTAL"] = montoTotal.ToString();
                            dt.Rows.Add(dr);
                            
                        }
                        else
                        {

                            dt.AcceptChanges();
                            foreach (DataRow fila in dt.Rows)
                            {
                                if (fila["ID_PRODUCTO"].ToString() == _lblIdProducto.Text)
                                    fila.Delete();
                            }
                            dt.AcceptChanges();


                            int cont = dt.Rows.Count;
                            var max = dt.AsEnumerable().Max(x => (int)x["ID"]);

                            max++;

                            DataRow dr = dt.NewRow();
                            dr["ID"] = max;
                            dr["CORRELATIVO"] = max;
                            dr["ID_PRODUCTO"] = _lblIdProducto.Text;
                            dr["CODIGO"] = _lblCodigo.Text;
                            dr["NOMBRE_PRODUCTO"] = _lblNombreProducto.Text;
                            dr["CANTIDAD"] = _txtCantidad.Text;
                            dr["MONTO_UNI"] = montoUni;
                            dr["MONTO_NETO"] = monto;
                            dr["DESCUENTO_PORCENTAJE"] = descuento.ToString();
                            dr["DESCUENTO_MONTO"] = montoDescuento.ToString();
                            dr["MONTO_TOTAL"] = montoTotal;
                            dt.Rows.Add(dr);
                            
                        }
                    }
                }
                calcularTotal();
                mdlSeleccionarProductos.Hide();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        public int calcularPorcentaje(double porcentaje, double monto)
        {
            int montoTotal = 0;

            montoTotal = Convert.ToInt32((porcentaje / 100) * monto);

            return montoTotal;
        }

        public void calcularTotal()
        {

            //aqui hacer la suma
            double montoNeto = 0;
            double iva = 0;
            double total = 0;
            DataTable dt = Session["dtDetalleProductos"] as DataTable;


            dt.AcceptChanges();
            foreach (DataRow drEliminar in dt.Rows)
            {
                if (drEliminar["CORRELATIVO"].ToString() == "")
                    drEliminar.Delete();
            }
            dt.AcceptChanges();


            if (txtDescuento.Text == string.Empty)
            {
                txtDescuento.Text = "0";
            }
            double descuento = Convert.ToDouble(txtDescuento.Text);


            //descuento tabla parametros
            double descuentoParametro = 0;
            foreach (DataRow item in dal.getBuscarParametros(Session["idEmpresa"].ToString()).Tables[0].Rows)
            {
                if (item["DESCUENTO_PAGO_CONTADO"].ToString() != string.Empty)
                {
                    descuentoParametro = Convert.ToDouble(item["DESCUENTO_PAGO_CONTADO"].ToString().Replace(".", ","));
                }
            }

            if (descuento > descuentoParametro)
            {
                lblInformacion.Text = "El porcentaje de descuento es mayor al establecido en parámetros, favor comunicarse con el administrador.";
                mdlInformacion.Show();
                return;
            }

            int porcentaje = 0;
            double montoNetoConDescuento = 0;

            foreach (DataRow item in dt.Rows)
            {
                montoNeto += Convert.ToDouble(item["MONTO_TOTAL"]);

                porcentaje = calcularPorcentaje(Convert.ToDouble(txtDescuento.Text), montoNeto);
                lblTotalNeto.Text = Convert.ToInt32((montoNeto - porcentaje)).ToString("n0");
                montoNetoConDescuento = (montoNeto - porcentaje);
                lblIva.Text = Convert.ToInt32(calcularIva(Convert.ToDouble(montoNetoConDescuento))).ToString("n0");

                iva = calcularIva(Convert.ToDouble(montoNetoConDescuento));

                lblTotal.Text = Convert.ToInt32((montoNetoConDescuento + iva)).ToString("n0");
                total = (montoNetoConDescuento + iva);
            }
            double totalConDescuento = montoNeto - porcentaje;


            DataRow drBlanco = dt.NewRow();
            drBlanco["ID"] = "0";
            drBlanco["CORRELATIVO"] = "";
            drBlanco["CODIGO"] = "";
            drBlanco["ID_PRODUCTO"] = "";
            drBlanco["NOMBRE_PRODUCTO"] = "&nbsp";
            drBlanco["CANTIDAD"] = "";
            drBlanco["MONTO_UNI"] = "";
            drBlanco["MONTO_NETO"] = "";
            drBlanco["DESCUENTO_PORCENTAJE"] = "";
            drBlanco["DESCUENTO_MONTO"] = "";
            drBlanco["MONTO_TOTAL"] = "";

            dt.Rows.Add(drBlanco);


            DataRow dr = dt.NewRow();
            dr["ID"] = "0";
            dr["CORRELATIVO"] = "";
            dr["CODIGO"] = "";
            dr["ID_PRODUCTO"] = "";
            dr["NOMBRE_PRODUCTO"] = "<b>Total Neto</b>";
            dr["CANTIDAD"] = "";
            dr["MONTO_UNI"] = "";
            dr["MONTO_NETO"] = "";
            dr["DESCUENTO_PORCENTAJE"] = "";
            dr["DESCUENTO_MONTO"] = "";
            dr["MONTO_TOTAL"] = "<b>" + Convert.ToInt32(montoNeto).ToString("n0") + "</b>";

            dt.Rows.Add(dr);


            DataRow drDescuentoPagoContado = dt.NewRow();
            drDescuentoPagoContado["ID"] = "0";
            drDescuentoPagoContado["CORRELATIVO"] = "";
            drDescuentoPagoContado["CODIGO"] = "";
            drDescuentoPagoContado["ID_PRODUCTO"] = "";
            drDescuentoPagoContado["NOMBRE_PRODUCTO"] = "<b>Descuento Pago Contado</b>";
            drDescuentoPagoContado["CANTIDAD"] = "";
            drDescuentoPagoContado["MONTO_UNI"] = "";
            drDescuentoPagoContado["MONTO_NETO"] = "";
            drDescuentoPagoContado["DESCUENTO_PORCENTAJE"] = "";
            drDescuentoPagoContado["DESCUENTO_MONTO"] = "";
            drDescuentoPagoContado["MONTO_TOTAL"] = "<b>" + Convert.ToInt32(porcentaje).ToString("n0") + "</b>";

            dt.Rows.Add(drDescuentoPagoContado);

            lblTotalDescuento.Text = Convert.ToInt32(porcentaje).ToString();


            DataRow drDescuento = dt.NewRow();
            drDescuento["ID"] = "0";
            drDescuento["CORRELATIVO"] = "";
            drDescuento["CODIGO"] = "";
            drDescuento["ID_PRODUCTO"] = "";
            drDescuento["NOMBRE_PRODUCTO"] = "<b>Total con Descuento</b>";
            drDescuento["CANTIDAD"] = "";
            drDescuento["MONTO_UNI"] = "";
            drDescuento["MONTO_NETO"] = "";
            drDescuento["DESCUENTO_PORCENTAJE"] = "";
            drDescuento["DESCUENTO_MONTO"] = "";
            drDescuento["MONTO_TOTAL"] = "<b>" + Convert.ToInt32(totalConDescuento).ToString("n0") + "</b>";

            dt.Rows.Add(drDescuento);

            DataRow drIva = dt.NewRow();
            drIva["ID"] = "0";
            drIva["CORRELATIVO"] = "";
            drIva["CODIGO"] = "";
            drIva["ID_PRODUCTO"] = "";
            drIva["NOMBRE_PRODUCTO"] = "<b>Iva</b>";
            drIva["CANTIDAD"] = "";
            drIva["MONTO_UNI"] = "";
            drIva["MONTO_NETO"] = "";
            drIva["DESCUENTO_PORCENTAJE"] = "";
            drIva["DESCUENTO_MONTO"] = "";
            drIva["MONTO_TOTAL"] = "<b>" + Convert.ToInt32(iva).ToString("n0") + "</b>";

            dt.Rows.Add(drIva);

            DataRow drTotal = dt.NewRow();
            drTotal["ID"] = "0";
            drTotal["CORRELATIVO"] = "";
            drTotal["CODIGO"] = "";
            drTotal["ID_PRODUCTO"] = "";
            drTotal["NOMBRE_PRODUCTO"] = "<b>Total</b>";
            drTotal["CANTIDAD"] = "";
            drTotal["MONTO_UNI"] = "";
            drTotal["MONTO_NETO"] = "";
            drTotal["DESCUENTO_PORCENTAJE"] = "";
            drTotal["DESCUENTO_MONTO"] = "";
            drTotal["MONTO_TOTAL"] = "<b>" + Convert.ToInt32(total).ToString("n0") + "</b>";

            dt.Rows.Add(drTotal);

            grvProducto.DataSource = dt;
            grvProducto.DataBind();
        }

        public void calcularTotalPagoContado()
        {
            double monto = 0.0;
            double num1 = 0.0;
            double num2 = 0.0;
            DataTable dataTable = this.Session["dtDetalleProductos"] as DataTable;
            dataTable.AcceptChanges();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row["CORRELATIVO"].ToString() == "")
                    row.Delete();
            }
            dataTable.AcceptChanges();
            if (this.txtDescuentoPagoContado.Text == string.Empty)
                this.txtDescuentoPagoContado.Text = "0";
            double num3 = Convert.ToDouble(this.txtDescuentoPagoContado.Text);
            double num4 = 0.0;
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarParametros(this.Session["idEmpresa"].ToString()).Tables[0].Rows)
            {
                if (row["DESCUENTO_PAGO_CONTADO"].ToString() != string.Empty)
                    num4 = Convert.ToDouble(row["DESCUENTO_PAGO_CONTADO"].ToString().Replace(".", ","));
            }
            if (num3 > num4)
            {
                this.lblInformacion.Text = "El porcentaje de descuento es mayor al establecido en parámetros, favor comunicarse con el administrador.";
                this.mdlInformacion.Show();
            }
            else
            {
                int num5 = 0;
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    monto += Convert.ToDouble(row["MONTO_TOTAL"]);
                    num5 = this.calcularPorcentaje(Convert.ToDouble(this.txtDescuentoPagoContado.Text), monto);
                    Label lblTotalNeto = this.lblTotalNeto;
                    int int32 = Convert.ToInt32(monto - (double)num5);
                    string str1 = int32.ToString("n0");
                    lblTotalNeto.Text = str1;
                    double num6 = monto - (double)num5;
                    Label lblIva = this.lblIva;
                    int32 = Convert.ToInt32(this.calcularIva(Convert.ToDouble(num6)));
                    string str2 = int32.ToString("n0");
                    lblIva.Text = str2;
                    num1 = this.calcularIva(Convert.ToDouble(num6));
                    Label lblTotal = this.lblTotal;
                    int32 = Convert.ToInt32(num6 + num1);
                    string str3 = int32.ToString("n0");
                    lblTotal.Text = str3;
                    num2 = num6 + num1;
                }
                double num7 = monto - (double)num5;
                DataRow row1 = dataTable.NewRow();
                row1["ID"] = (object)"0";
                row1["CORRELATIVO"] = (object)"";
                row1["CODIGO"] = (object)"";
                row1["ID_PRODUCTO"] = (object)"";
                row1["NOMBRE_PRODUCTO"] = (object)"&nbsp";
                row1["CANTIDAD"] = (object)"";
                row1["MONTO_UNI"] = (object)"";
                row1["MONTO_NETO"] = (object)"";
                row1["DESCUENTO_PORCENTAJE"] = (object)"";
                row1["DESCUENTO_MONTO"] = (object)"";
                row1["MONTO_TOTAL"] = (object)"";
                dataTable.Rows.Add(row1);
                DataRow row2 = dataTable.NewRow();
                row2["ID"] = (object)"0";
                row2["CORRELATIVO"] = (object)"";
                row2["CODIGO"] = (object)"";
                row2["ID_PRODUCTO"] = (object)"";
                row2["NOMBRE_PRODUCTO"] = (object)"<b>Total Neto</b>";
                row2["CANTIDAD"] = (object)"";
                row2["MONTO_UNI"] = (object)"";
                row2["MONTO_NETO"] = (object)"";
                row2["DESCUENTO_PORCENTAJE"] = (object)"";
                row2["DESCUENTO_MONTO"] = (object)"";
                row2["MONTO_TOTAL"] = (object)("<b>" + Convert.ToInt32(monto).ToString("n0") + "</b>");
                dataTable.Rows.Add(row2);
                DataRow row3 = dataTable.NewRow();
                row3["ID"] = (object)"0";
                row3["CORRELATIVO"] = (object)"";
                row3["CODIGO"] = (object)"";
                row3["ID_PRODUCTO"] = (object)"";
                row3["NOMBRE_PRODUCTO"] = (object)"<b>Descuento Pago Contado</b>";
                row3["CANTIDAD"] = (object)"";
                row3["MONTO_UNI"] = (object)"";
                row3["MONTO_NETO"] = (object)"";
                row3["DESCUENTO_PORCENTAJE"] = (object)"";
                row3["DESCUENTO_MONTO"] = (object)"";
                row3["MONTO_TOTAL"] = (object)("<b>" + Convert.ToInt32(num5).ToString("n0") + "</b>");
                dataTable.Rows.Add(row3);
                this.lblTotalDescuento.Text = Convert.ToInt32(num5).ToString();
                DataRow row4 = dataTable.NewRow();
                row4["ID"] = (object)"0";
                row4["CORRELATIVO"] = (object)"";
                row4["CODIGO"] = (object)"";
                row4["ID_PRODUCTO"] = (object)"";
                row4["NOMBRE_PRODUCTO"] = (object)"<b>Total con Descuento</b>";
                row4["CANTIDAD"] = (object)"";
                row4["MONTO_UNI"] = (object)"";
                row4["MONTO_NETO"] = (object)"";
                row4["DESCUENTO_PORCENTAJE"] = (object)"";
                row4["DESCUENTO_MONTO"] = (object)"";
                row4["MONTO_TOTAL"] = (object)("<b>" + Convert.ToInt32(num7).ToString("n0") + "</b>");
                dataTable.Rows.Add(row4);
                DataRow row5 = dataTable.NewRow();
                row5["ID"] = (object)"0";
                row5["CORRELATIVO"] = (object)"";
                row5["CODIGO"] = (object)"";
                row5["ID_PRODUCTO"] = (object)"";
                row5["NOMBRE_PRODUCTO"] = (object)"<b>Iva</b>";
                row5["CANTIDAD"] = (object)"";
                row5["MONTO_UNI"] = (object)"";
                row5["MONTO_NETO"] = (object)"";
                row5["DESCUENTO_PORCENTAJE"] = (object)"";
                row5["DESCUENTO_MONTO"] = (object)"";
                row5["MONTO_TOTAL"] = (object)("<b>" + Convert.ToInt32(num1).ToString("n0") + "</b>");
                dataTable.Rows.Add(row5);
                DataRow row6 = dataTable.NewRow();
                row6["ID"] = (object)"0";
                row6["CORRELATIVO"] = (object)"";
                row6["CODIGO"] = (object)"";
                row6["ID_PRODUCTO"] = (object)"";
                row6["NOMBRE_PRODUCTO"] = (object)"<b>Total</b>";
                row6["CANTIDAD"] = (object)"";
                row6["MONTO_UNI"] = (object)"";
                row6["MONTO_NETO"] = (object)"";
                row6["DESCUENTO_PORCENTAJE"] = (object)"";
                row6["DESCUENTO_MONTO"] = (object)"";
                row6["MONTO_TOTAL"] = (object)("<b>" + Convert.ToInt32(num2).ToString("n0") + "</b>");
                dataTable.Rows.Add(row6);
                this.grvProducto.DataSource = (object)dataTable;
                this.grvProducto.DataBind();
            }
        }


        public double calcularIva(double monto)
        {
            double montoTotal = 0;
            double porcentaje = 19;

            montoTotal = ((porcentaje / 100) * monto);

            return montoTotal;
        }


        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label _lblId = (Label)e.Row.FindControl("lblId");
                Label _lblDescuentoPorcentaje = (Label)e.Row.FindControl("lblDescuentoPorcentaje");
                string idUsuario = Session["variableIdUsuario"].ToString();

                ImageButton _imgEliminar = (ImageButton)e.Row.FindControl("imgEliminar");

                if (_lblId.Text == string.Empty)
                {
                    _imgEliminar.Visible = false;
                }

                if (_lblDescuentoPorcentaje.Text != string.Empty)
                {
                    double porcentajeDescuentoPermitido = 0;
                    foreach (DataRow item in dal.getBuscarUsuario(null, idUsuario).Tables[0].Rows)
                    {
                        if (item["DESCUENTO_AUTORIZADO"].ToString() != string.Empty)
                        {
                            porcentajeDescuentoPermitido = Convert.ToDouble(item["DESCUENTO_AUTORIZADO"].ToString().Replace(".", ","));
                        }
                    }

                    double descuento = 0;
                    if (_lblDescuentoPorcentaje.Text == string.Empty)
                    {
                        descuento = 0;
                    }
                    else
                    {
                        descuento = Convert.ToDouble(_lblDescuentoPorcentaje.Text);
                    }

                    if (descuento > porcentajeDescuentoPermitido)
                    {
                        e.Row.CssClass = "danger";
                        hfTieneDescuento.Value = "1";
                    }
                }
            }
        }

        void canal() 
        {
            ddlCanal.DataSource = dal.getBuscarCanal(null, null);
            ddlCanal.DataTextField = "NOM_CANAL";
            ddlCanal.DataValueField = "ID_CANAL";
            ddlCanal.DataBind();

        }

        protected void btnGrabarCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                //string idEmpresa = Session["idEmpresa"].ToString();
                //string idUsuario = Session["variableIdUsuario"].ToString();
                //string idPerfil = Session["variablePerfil"].ToString();

                //if (lblRutCotizacion.Text == string.Empty)
                //{
                //    lblInformacion.Text = "No hay un cliente seleccionado, favor seleccionar un cliente";
                //    mdlInformacion.Show();
                //    return;
                //}

                ////if (lblIdContacto.Text == string.Empty)
                ////{
                ////    lblInformacion.Text = "No hay un contacto seleccionado, favor seleccionar un contacto";
                ////    mdlInformacion.Show();
                ////    return;
                ////}

                //if (ddlRazonSocial.SelectedValue=="0")
                //{
                //    lblInformacion.Text = "Favor seleccionar la razon social";
                //    mdlInformacion.Show();
                //    return;
                //}

                //if (grvProducto.Rows.Count == 0)
                //{
                //    lblInformacion.Text = "No hay detalle de la cotización, favor seleccionar los productos a cotizar.";
                //    mdlInformacion.Show();
                //    return;
                //}

                //if (idPerfil == "3")
                //{
                //    foreach (GridViewRow row in grvProducto.Rows)
                //    {
                //        //CheckBox check = (CheckBox)row.FindControl("CheckBox1");

                //        Label _lblId = (Label)row.FindControl("lblId");
                //        Label _lblDescuentoPorcentaje = (Label)row.FindControl("lblDescuentoPorcentaje");

                //        if (_lblId.Text == string.Empty)
                //        {

                //        }

                //        if (_lblDescuentoPorcentaje.Text != string.Empty)
                //        {
                //            double porcentajeDescuentoPermitido = 0;
                //            foreach (DataRow item in dal.getBuscarUsuario(null, idUsuario).Tables[0].Rows)
                //            {
                //                if (item["DESCUENTO_AUTORIZADO"].ToString() != string.Empty)
                //                {
                //                    porcentajeDescuentoPermitido = Convert.ToDouble(item["DESCUENTO_AUTORIZADO"].ToString().Replace(".", ","));
                //                }
                //            }

                //            double descuento = 0;
                //            if (_lblDescuentoPorcentaje.Text == string.Empty)
                //            {
                //                descuento = 0;
                //            }
                //            else
                //            {
                //                descuento = Convert.ToDouble(_lblDescuentoPorcentaje.Text);
                //            }

                //            if (descuento > porcentajeDescuentoPermitido)
                //            {
                //                hfTieneDescuento.Value = "1";
                //            }
                //            else
                //            {
                //                hfTieneDescuento.Value = "0";
                //            }

                //            if (hfTieneDescuento.Value == "1")
                //            {
                //                mdlInformacionValidarCotizacion.Show();
                //                return;
                //            }
                //        }
                //    }
                //}






                //if (ddlCanal.SelectedValue == "0")
                //{
                //    lblInformacion.Text = "Favor de seleccionar un canal";
                //    mdlInformacion.Show();
                //    return;
                //}
                //grabaCotizacion("0");


                this.Session["idEmpresa"].ToString();
                string idUsuario = this.Session["variableIdUsuario"].ToString();
                string str = this.Session["variablePerfil"].ToString();
                if (this.lblIdCliente.Text == string.Empty)
                {
                    this.lblInformacion.Text = "No hay un cliente seleccionado, favor seleccionar un cliente";
                    this.mdlInformacion.Show();
                    return;
                }
                else if (this.ddlRazonSocial.SelectedValue == "0")
                {
                    this.lblInformacion.Text = "Favor seleccionar la razon social";
                    this.mdlInformacion.Show();
                    return;
                }
                else if (this.grvProducto.Rows.Count == 0)
                {
                    this.lblInformacion.Text = "No hay detalle de la cotización, favor seleccionar los productos a cotizar.";
                    this.mdlInformacion.Show();
                    return;
                }
                else
                {
                    if (str == "3")
                    {
                        foreach (GridViewRow row1 in this.grvProducto.Rows)
                        {
                            Label control1 = (Label)row1.FindControl("lblId");
                            Label control2 = (Label)row1.FindControl("lblDescuentoPorcentaje");
                            int num1 = control1.Text == string.Empty ? 1 : 0;
                            if (control2.Text != string.Empty)
                            {
                                double num2 = 0.0;
                                foreach (DataRow row2 in (InternalDataCollectionBase)this.dal.getBuscarUsuario((string)null, idUsuario).Tables[0].Rows)
                                {
                                    if (row2["DESCUENTO_AUTORIZADO"].ToString() != string.Empty)
                                        num2 = Convert.ToDouble(row2["DESCUENTO_AUTORIZADO"].ToString().Replace(".", ","));
                                }
                                this.hfTieneDescuento.Value = (!(control2.Text == string.Empty) ? Convert.ToDouble(control2.Text) : 0.0) <= num2 ? "0" : "1";
                                if (this.hfTieneDescuento.Value == "1")
                                {
                                    this.mdlInformacionValidarCotizacion.Show();
                                    return;
                                }
                            }
                        }
                    }
                    if (this.ddlCanal.SelectedValue == "0")
                    {
                        this.lblInformacion.Text = "Favor de seleccionar un canal";
                        this.mdlInformacion.Show();
                        return;
                    }
                    else
                        this.grabaCotizacion("0");
                }

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }



        protected void btnSiAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                hfAutorizarSiNo.Value = "Si";

                string idEmpresa = Session["idEmpresa"].ToString();
                string idUsuario = Session["variableIdUsuario"].ToString();

                if (lblIdCliente.Text == string.Empty)
                {
                    lblInformacion.Text = "No hay un cliente seleccionado, favor seleccionar un cliente";
                    mdlInformacion.Show();
                    return;
                }

                //if (lblIdContacto.Text == string.Empty)
                //{
                //    lblInformacion.Text = "No hay un contacto seleccionado, favor seleccionar un contacto";
                //    mdlInformacion.Show();
                //    return;
                //}

                if (grvProducto.Rows.Count == 0)
                {
                    lblInformacion.Text = "No hay detalle de la cotización, favor seleccionar los productos a cotizar.";
                    mdlInformacion.Show();
                    return;
                }

                if (ddlCanal.SelectedValue == "0")
                {
                    lblInformacion.Text = "Favor de seleccionar un canal";
                    mdlInformacion.Show();
                    return;
                }
                
                if (ddlRazonSocial.SelectedValue == "0")
                {
                    lblInformacion.Text = "Favor de seleccionar una Razón Social";
                    mdlInformacion.Show();
                    return;
                }


                grabaCotizacion("1");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        //void grabaCotizacion(string porAprobar)
        //{

        //    string idEmpresa = Session["idEmpresa"].ToString();
        //    string idUsuario = Session["variableIdUsuario"].ToString();

        //    string totalNeto = lblTotalNeto.Text.Replace(".", "");
        //    string ivaTotal = lblIva.Text.Replace(".", "");
        //    int idRazonSocial = Convert.ToInt32(ddlRazonSocial.SelectedValue);

        //    string idCotizacion = "";
        //    if (porAprobar == "1")
        //    {
        //        idCotizacion = dal.setIngresarCotizacion(idEmpresa, lblRutCotizacion.Text, totalNeto, lblTotalDescuento.Text, ivaTotal, lblTotal.Text.Replace(".", ""), "5", idUsuario, idUsuario, ddlCanal.SelectedValue, lblIdContacto.Text, txtObservacionCotizacion.Text, txtDescuento.Text.Replace(",", "."), porAprobar, idRazonSocial);
        //    }
        //    else
        //    {
        //        idCotizacion = dal.setIngresarCotizacion(idEmpresa, lblRutCotizacion.Text, totalNeto, lblTotalDescuento.Text, ivaTotal, lblTotal.Text.Replace(".", ""), "1", idUsuario, idUsuario, ddlCanal.SelectedValue, lblIdContacto.Text, txtObservacionCotizacion.Text, txtDescuento.Text.Replace(",", "."), porAprobar, idRazonSocial);
        //    }

        //    double descuentoPorcent = 0;
        //    foreach (GridViewRow item in grvProducto.Rows)
        //    {
        //        Label _lblId = (Label)grvProducto.Rows[item.RowIndex].FindControl("lblId");

        //        if (_lblId.Text != "")
        //        {
        //            Label _lblIdProducto = (Label)grvProducto.Rows[item.RowIndex].FindControl("lblIdProducto");
        //            Label _lblNombreProducto = (Label)grvProducto.Rows[item.RowIndex].FindControl("lblNombreProducto");
        //            Label _lblCantidad = (Label)grvProducto.Rows[item.RowIndex].FindControl("lblCantidad");
        //            Label _lblMonto = (Label)grvProducto.Rows[item.RowIndex].FindControl("lblMontoTotal");
        //            Label _lblDescuentoPorcentaje = (Label)grvProducto.Rows[item.RowIndex].FindControl("lblDescuentoPorcentaje");
        //            Label _lblDescuento = (Label)grvProducto.Rows[item.RowIndex].FindControl("lblDescuento");

        //            double montoConIva = calcularIva(Convert.ToDouble(_lblMonto.Text));
        //            string monto = Convert.ToInt32(Convert.ToDouble(_lblMonto.Text)).ToString();
        //            string iva = (Convert.ToInt32(montoConIva) + Convert.ToInt32(monto)).ToString();

        //            double porcentajeDescuentoPermitido = 0;
        //            foreach (DataRow row in dal.getBuscarUsuario(null, idUsuario).Tables[0].Rows)
        //            {
        //                if (row["DESCUENTO_AUTORIZADO"].ToString() != string.Empty)
        //                {
        //                    porcentajeDescuentoPermitido = Convert.ToDouble(row["DESCUENTO_AUTORIZADO"].ToString().Replace(".", ","));
        //                }
        //            }
        //            double descuento = 0;
        //            if (_lblDescuentoPorcentaje.Text == string.Empty)
        //            {
        //                descuento = 0;
        //                descuentoPorcent = 0;
        //            }
        //            else
        //            {
        //                descuento = Convert.ToDouble(_lblDescuentoPorcentaje.Text);
        //                descuentoPorcent = descuento;
        //            }

        //            if (descuento > porcentajeDescuentoPermitido)
        //            {
        //                dal.setIngresarCotizacionDetalle(idCotizacion, _lblId.Text, _lblIdProducto.Text, monto, _lblCantidad.Text, iva, _lblDescuento.Text, _lblDescuentoPorcentaje.Text.Replace(",", "."), porAprobar);
        //            }
        //            else
        //            {
        //                dal.setIngresarCotizacionDetalle(idCotizacion, _lblId.Text, _lblIdProducto.Text, monto, _lblCantidad.Text, iva, _lblDescuento.Text, _lblDescuentoPorcentaje.Text.Replace(",", "."), "0");
        //            }
        //        }
        //    }

        //    string negativo = string.Empty;
        //    if (descuentoPorcent < 0)
        //    {
        //        negativo = "1";
        //    }
        //    else
        //    {
        //        negativo = "0";
        //    }

        //    string ruta = "";
        //    if (porAprobar == "1")
        //    {
        //        ruta = generaPdf2(idCotizacion, "", lblRutCotizacion.Text, idEmpresa);
        //        dal.setEditarCotizacionRutaPdf(idCotizacion, ruta);
        //    }
        //    else
        //    {
        //        ruta = generaPdf2(idCotizacion, "", lblRutCotizacion.Text, idEmpresa);
        //        dal.setEditarCotizacionRutaPdf(idCotizacion, ruta);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + hfrutaArchivoPdf.Value + "','_blank');", true);
        //    }

        //    limpiar(this.Controls);

        //    DataTable dt = Session["dtDetalleProductos"] as DataTable;
        //    dt.Clear();

        //    DataTable dtLimpio = new DataTable();
        //    grvProducto.DataSource = dtLimpio;
        //    grvProducto.DataBind();

        //    //buscarCotizaciones(lblRutCotizacion.Text);
        //}




        private void grabaCotizacion(string porAprobar)
        {
            string idEmpresa = this.Session["idEmpresa"].ToString();
            string str1 = this.Session["variableIdUsuario"].ToString();
            int int32_1 = Convert.ToInt32(this.Session["IdCliente"]);
            string montoNeto1 = this.lblTotalNeto.Text.Replace(".", "");
            string montoIva = this.lblIva.Text.Replace(".", "");
            int int32_2 = Convert.ToInt32(this.ddlRazonSocial.SelectedValue);
            string str2 = !(porAprobar == "1") ? this.dal.setIngresarCotizacion(idEmpresa, this.lblRutCotizacion.Text, 
                montoNeto1, this.lblTotalDescuento.Text, montoIva, this.lblTotal.Text.Replace(".", ""), "1", str1, str1, 
                this.ddlCanal.SelectedValue, this.lblIdContacto.Text, this.txtObservacionCotizacion.Text, 
                this.txtDescuento.Text.Replace(",", "."), porAprobar, int32_2, int32_1)
                : this.dal.setIngresarCotizacion(idEmpresa, this.lblRutCotizacion.Text, montoNeto1, 
                this.lblTotalDescuento.Text, montoIva, this.lblTotal.Text.Replace(".", ""), "5", str1, str1, 
                this.ddlCanal.SelectedValue, this.lblIdContacto.Text, this.txtObservacionCotizacion.Text, 
                this.txtDescuento.Text.Replace(",", "."), porAprobar, int32_2, int32_1);
            double num1 = 0.0;
            foreach (GridViewRow row1 in this.grvProducto.Rows)
            {
                Label control1 = (Label)this.grvProducto.Rows[row1.RowIndex].FindControl("lblId");
                if (control1.Text != "")
                {
                    Label control2 = (Label)this.grvProducto.Rows[row1.RowIndex].FindControl("lblIdProducto");
                    Label control3 = (Label)this.grvProducto.Rows[row1.RowIndex].FindControl("lblNombreProducto");
                    Label control4 = (Label)this.grvProducto.Rows[row1.RowIndex].FindControl("lblCantidad");
                    Label control5 = (Label)this.grvProducto.Rows[row1.RowIndex].FindControl("lblMontoTotal");
                    Label control6 = (Label)this.grvProducto.Rows[row1.RowIndex].FindControl("lblDescuentoPorcentaje");
                    Label control7 = (Label)this.grvProducto.Rows[row1.RowIndex].FindControl("lblDescuento");
                    double num2 = this.calcularIva(Convert.ToDouble(control5.Text));
                    int num3 = Convert.ToInt32(Convert.ToDouble(control5.Text));
                    string montoNeto2 = num3.ToString();
                    num3 = Convert.ToInt32(num2) + Convert.ToInt32(montoNeto2);
                    string montoTotal = num3.ToString();
                    double num4 = 0.0;
                    foreach (DataRow row2 in (InternalDataCollectionBase)this.dal.getBuscarUsuario((string)null, str1).Tables[0].Rows)
                    {
                        if (row2["DESCUENTO_AUTORIZADO"].ToString() != string.Empty)
                            num4 = Convert.ToDouble(row2["DESCUENTO_AUTORIZADO"].ToString().Replace(".", ","));
                    }
                    double num5;
                    if (control6.Text == string.Empty)
                    {
                        num5 = 0.0;
                        num1 = 0.0;
                    }
                    else
                    {
                        num5 = Convert.ToDouble(control6.Text);
                        num1 = num5;
                    }
                    if (num5 > num4)
                        this.dal.setIngresarCotizacionDetalle(str2, control1.Text, control2.Text, montoNeto2, control4.Text, montoTotal, control7.Text, control6.Text.Replace(",", "."), porAprobar);
                    else
                        this.dal.setIngresarCotizacionDetalle(str2, control1.Text, control2.Text, montoNeto2, control4.Text, montoTotal, control7.Text, control6.Text.Replace(",", "."), "0");
                }
            }
            string empty = string.Empty;
            if (porAprobar == "1")
            {
                string rutaPdf = this.generaPdf2(str2, "", this.lblRutCotizacion.Text, idEmpresa);
                this.dal.setEditarCotizacionRutaPdf(str2, rutaPdf);
            }
            else
            {
                string rutaPdf = this.generaPdf2(str2, "", this.lblRutCotizacion.Text, idEmpresa);
                this.dal.setEditarCotizacionRutaPdf(str2, rutaPdf);
                ScriptManager.RegisterStartupScript((Page)this, this.GetType(), this.UniqueID, "window.open('" + this.hfrutaArchivoPdf.Value + "','_blank');", true);
            }
            this.limpiar(this.Controls);
            (this.Session["dtDetalleProductos"] as DataTable).Clear();
            this.grvProducto.DataSource = (object)new DataTable();
            this.grvProducto.DataBind();
        }






        //void buscarCotizaciones(string rut)
        //{
        //    string idEmpresa = Session["idEmpresa"].ToString();
        //    //dal.getBuscarSeguimiento(null
        //    //grvGestion.DataSource = dal.getBuscarSeguimiento(null, montoCotizacionDesde, montoCotizacionHasta, fechaDesde, fechaHasta);
        //    //grvGestion.DataBind();

        //    //modificacion con dudas
        //    Session["DatosCotizaciones"] = dal.getBuscarCotizacionesEnSeguimientoPorCliente(rut, idEmpresa);
        //    grvCotizacionesCRM.DataSource = Session["DatosCotizaciones"] as DataSet;
        //    grvCotizacionesCRM.DataBind();
        //}

        protected void btnNoAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                hfAutorizarSiNo.Value = "No";
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
        
        protected void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calcularTotal();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        private string generaPdf3(string cotizacion, string ejecutivo, string rut)
        {
            //
            DateTime Hoy = DateTime.Today;
            string fechaHoy = Hoy.ToString("dd-MM-yyyy");

            //string nombreArchivoPdf = DateTime.Now.Ticks.ToString() + "_folio_" + comprobantePago + ".pdf";
            string nombreArchivoPdf = "Cotizacion_" + cotizacion + ".pdf";
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

            Font times = new Font(bfTimes, 7, Font.NORMAL);
            Font timesBold = new Font(bfTimes, 7, Font.BOLD);
            Font timesDeNueve = new Font(bfTimes, 8, Font.NORMAL);
            Font timesRojo = new Font(bfTimes, 9, Font.BOLD, BaseColor.RED);
            Font timesCorrelativo = new Font(bfTimes, 9, Font.BOLD);
            Font fontCabecera = new Font(bfTimes, 8, Font.BOLD);
            Font fontFirma = new Font(bfTimes, 8, Font.BOLD);
            Font fontRazonSocial = new Font(bfTimes, 10, Font.BOLD);
            Font fontNroCotizacion = new Font(bfTimes, 12, Font.BOLD);

            Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writePdf = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("cotizaciones/" + nombreArchivoPdf), FileMode.Create));
            doc.Open();

            string logoEmpresa = "";
            string emailEmpresa = string.Empty;
            foreach (DataRow item in dal.getBuscarEmpresa(null, Session["idEmpresa"].ToString()).Tables[0].Rows)
            {
                logoEmpresa = item["IMAGEN"].ToString();
                emailEmpresa = item["EMAIL"].ToString();
            }

            if (logoEmpresa == string.Empty)
            {
                logoEmpresa = "assets/img/imagenesEmpresa/1_1_logo fadonel con call center y productos.png";
            }
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Server.MapPath(logoEmpresa));
            jpg.ScaleToFit(150, 150);
            jpg.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            //doc.Add(jpg);
            
            PdfPTable tableCabeceraSuperior = new PdfPTable(3);


            string rutRazonSocial=string.Empty;
            string direccionRazonSocial = string.Empty;
            string telefonoRazonSocial = string.Empty;
            string emailRazonSocial = string.Empty;
            string bancoRazonSocial = string.Empty;
            string cuentaCorrienteRazonSocial = string.Empty;
            
            foreach (DataRow item in dal.getBuscarRazonSocial(Convert.ToInt32(ddlRazonSocial.SelectedValue)).Tables[0].Rows)
            {
                rutRazonSocial = item["RUT"].ToString();
                direccionRazonSocial=item["DIRECCION"].ToString();
                telefonoRazonSocial = item["FONO"].ToString();
                emailRazonSocial = item["EMAIL"].ToString();
                bancoRazonSocial = item["BANCO"].ToString();
                cuentaCorrienteRazonSocial = item["CUENTA_CTE"].ToString();
            }

            PdfPTable tableCabecera = new PdfPTable(2);

            PdfPCell celdaRut = new PdfPCell(new Paragraph(rutRazonSocial + "\n" + "COTIZACION \nNº" + cotizacion, fontNroCotizacion));
            celdaRut.HorizontalAlignment = 1;
            //celdaRut.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            //PdfPCell celdaCot = new PdfPCell(new Paragraph("COTIZACION Nº" + cotizacion, fontNroCotizacion));
            //celdaCot.HorizontalAlignment = 1;

            PdfPCell celdaRazonSocial = new PdfPCell(new Paragraph(ddlRazonSocial.SelectedItem.ToString(), fontRazonSocial));
            celdaRazonSocial.VerticalAlignment = Element.ALIGN_BOTTOM;
            tableCabecera.AddCell(celdaRazonSocial);

            PdfPCell celdaLogo = new PdfPCell(jpg);
            celdaLogo.HorizontalAlignment = 1;
            //celdaLogo.Rowspan = 3;
            tableCabecera.AddCell(celdaLogo);

            PdfPCell celdaImportancion = new PdfPCell(new Paragraph("Importacion, Publicidad, Impresión de Telas, Comercializacion de Toldos Araña, Banderas, Carpas Araña, Pendones Roller, Cortinas con Diseño, Manteles, Paneles Araña Textil, Lienzos, Pasacalle, Minipendones, Banderas Velas, Banderas Gotas, Banderas Corporativas, Impresión y Aplicación de Logos.", timesDeNueve));
            celdaImportancion.Border = Rectangle.NO_BORDER;
            tableCabecera.AddCell(celdaImportancion);
            tableCabecera.AddCell(celdaRut);
            
            //tableCabecera.AddCell(new Paragraph(" "));
            //tableCabecera.AddCell(celdaCot);

            tableCabecera.DefaultCell.Border = Rectangle.NO_BORDER;
            tableCabecera.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableCabecera.WidthPercentage = 100.0f;
            foreach (PdfPCell celda in tableCabecera.Rows[0].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
            }
            

            //foreach (PdfPCell celda in tableCabecera.Rows[1].GetCells())
            //{
            //    celda.Border = Rectangle.NO_BORDER;
            //}

            doc.Add(tableCabecera);
            //doc.Add(new Paragraph("Importacion, Publicidad, Impresión de Telas, Comercializacion de Toldos Araña, Banderas, Carpas Araña, Pendones Roller, Cortinas con Diseño, Manteles, Paneles Araña Textil, Lienzos, Pasacalle, Minipendones, Banderas Velas, Banderas Gotas, Banderas Corporativas, Impresión y Aplicación de Logos.", times));
            doc.Add(new Paragraph(direccionRazonSocial, fontFirma));
            doc.Add(new Paragraph("FONO: " + telefonoRazonSocial, fontFirma));
            doc.Add(new Paragraph(emailEmpresa, fontFirma));


            //PdfPTable tableNumeroCotizacion = new PdfPTable(2);
            //PdfPCell celdaNumeroComprobante = new PdfPCell(new Paragraph("Nro.Cot. :", fontCabecera));
            ////celdaNumeroComprobante.HorizontalAlignment = 2;

            //PdfPCell celdaNumeroComprobanteFecha = new PdfPCell(new Paragraph("Fecha :", fontCabecera));
            ////celdaNumeroComprobanteFecha.HorizontalAlignment = 2;

            //tableNumeroCotizacion.AddCell(celdaNumeroComprobante);
            //tableNumeroCotizacion.AddCell(new Paragraph(cotizacion, times));

            //tableNumeroCotizacion.AddCell(celdaNumeroComprobanteFecha);
            //tableNumeroCotizacion.AddCell(new Paragraph(fechaHoy, times));

            //tableNumeroCotizacion.DefaultCell.Border = Rectangle.NO_BORDER;

            //tableNumeroCotizacion.HorizontalAlignment = Element.ALIGN_RIGHT;
            //tableNumeroCotizacion.WidthPercentage = 25.0f;

            //foreach (PdfPCell celda in tableNumeroCotizacion.Rows[0].GetCells())
            //{
            //    celda.Border = Rectangle.NO_BORDER;
            //}

            //foreach (PdfPCell celda in tableNumeroCotizacion.Rows[1].GetCells())
            //{
            //    celda.Border = Rectangle.NO_BORDER;
            //}

            //doc.Add(tableNumeroCotizacion);

            //Chunk tituloTipoExamen = new Chunk("Cotización", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD));
            //tituloTipoExamen.SetUnderline(0.1f, -2f);

            string tituloClientePricipalOAsociado = string.Empty;
            string tituloContactoPrincipalOAsociado = string.Empty;

            DataTable dtCliente2 = new DataTable();
            //dtCliente2 = dal.getBuscarCliente(lblRut.Text, lblRut.Text, "1").Tables[0];
            dtCliente2 = dal.getBuscarClientePorRut(lblRutCotizacion.Text).Tables[0];

            foreach (DataRow item in dtCliente2.Rows)
            {
                string rutPadre = item["RUT_PADRE"].ToString();
                if (rutPadre != string.Empty)
                {
                    tituloClientePricipalOAsociado = "Datos Cliente Asociado";
                    tituloContactoPrincipalOAsociado = "Datos Contacto Cliente Asociado";
                }
                else
                {
                    tituloClientePricipalOAsociado = "Datos Cliente";
                    tituloContactoPrincipalOAsociado = "Datos Contacto";
                }
            }

            ////Paragraph par = new Paragraph(tituloTipoExamen);
            ////par.Alignment = Element.ALIGN_CENTER;
            ////doc.Add(par);

            doc.Add(new Paragraph(" ", times));

            Chunk datosCliente = new Chunk(tituloClientePricipalOAsociado, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD));
            datosCliente.SetUnderline(0.1f, -2f);
            doc.Add(datosCliente);

            PdfPTable tableDatosCliente = new PdfPTable(4);

            DataTable dtDatosCli = new DataTable();
            dtDatosCli = dal.getBuscarClientePorRut(rut).Tables[0];
            string giro = "";
            string idCondicionDeVenta = "";
            string nombreCli = string.Empty;
            string direccionCli = string.Empty;
            string telefonoCli = string.Empty;

            foreach (DataRow item in dtDatosCli.Rows)
            {
                giro = item["GIRO"].ToString();
                idCondicionDeVenta = item["CONDICION_DE_VENTA"].ToString();
                nombreCli = item["RAZON_SOCIAL"].ToString();
                direccionCli = item["DIRECCION"].ToString();
                telefonoCli = item["TELEFONO"].ToString();
            }

            string idContacto = string.Empty;
            DataTable dtIdContacto = new DataTable();
            dtIdContacto = dal.getBuscarCotizacionPorId(cotizacion).Tables[0];
            string descuentoPagoContado = string.Empty;
            foreach (DataRow item in dtIdContacto.Rows)
            {
                idContacto = item["ID_CONTACTO"].ToString();
                descuentoPagoContado = item["MONTO_DESCUENTO"].ToString();
            }

            DataTable dtContacto = new DataTable();
            dtContacto = dal.getBuscarContactoPorId(idContacto).Tables[0];
            string contacto = string.Empty;
            string telefonoContacto = string.Empty;
            string celularContacto = string.Empty;
            string emailContacto = string.Empty;

            foreach (DataRow item in dtContacto.Rows)
            {
                contacto = item["NOM_CONTACTO"].ToString();
                telefonoContacto = item["TELEFONO1"].ToString();
                celularContacto = item["CELULAR"].ToString();
                emailContacto = item["EMAIL_1"].ToString();
            }

            float[] widthsDatosCliente = new float[] { 35f, 95f, 35f, 55f };
            tableDatosCliente.SetWidths(widthsDatosCliente);
            tableDatosCliente.AddCell(new Paragraph("Rut :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(rut, times));
            tableDatosCliente.AddCell(new Paragraph("Nombre :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(nombreCli, times));

            tableDatosCliente.AddCell(new Paragraph("Dirección :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(direccionCli, times));
            tableDatosCliente.AddCell(new Paragraph("Teléfono:", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(telefonoCli, times));

            tableDatosCliente.AddCell(new Paragraph("Giro :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(giro, times));
            tableDatosCliente.AddCell(new Paragraph("Fecha :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(fechaHoy, times));

            tableDatosCliente.AddCell(new Paragraph("Nombre Contacto :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(contacto, times));

            tableDatosCliente.AddCell(new Paragraph(" ", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(" ", times));

            tableDatosCliente.HorizontalAlignment = Element.ALIGN_LEFT;
            tableDatosCliente.WidthPercentage = 100.0f;

            //int contTabla = tableDatosCliente.Rows.Count;
            //for (int i = 0; i < contTabla; i++)
            //{
            //    foreach (PdfPCell celda in tableDatosCliente.Rows[i].GetCells())
            //    {
            //        celda.Border = Rectangle.NO_BORDER;
            //    }
            //}
            
            doc.Add(tableDatosCliente);
            doc.Add(new Paragraph(" ", times));

            //Chunk datosContacto = new Chunk(tituloContactoPrincipalOAsociado, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD));
            //datosContacto.SetUnderline(0.1f, -2f);
            //doc.Add(datosContacto);
            

            //PdfPTable tableDatosContacto = new PdfPTable(4);
            //float[] widthsDatosContacto = new float[] { 35f, 95f, 35f, 55f };
            //tableDatosContacto.SetWidths(widthsDatosContacto);

            //tableDatosContacto.AddCell(new Paragraph("Contacto :", fontCabecera));
            //tableDatosContacto.AddCell(new Paragraph(contacto, times));
            //tableDatosContacto.AddCell(new Paragraph("Teléfono :", fontCabecera));
            //tableDatosContacto.AddCell(new Paragraph(telefonoContacto, times));
            //tableDatosContacto.AddCell(new Paragraph("Celular :", fontCabecera));
            //tableDatosContacto.AddCell(new Paragraph(celularContacto, times));
            //tableDatosContacto.AddCell(new Paragraph("Email :", fontCabecera));
            //tableDatosContacto.AddCell(new Paragraph(emailContacto, times));

            //tableDatosContacto.HorizontalAlignment = Element.ALIGN_LEFT;
            //tableDatosContacto.WidthPercentage = 100.0f;

            //doc.Add(tableDatosContacto);

            //tabla de datos cliente asociado
            DataTable dtCliente = new DataTable();
            //dtCliente = dal.getBuscarCliente(lblRut.Text, lblRut.Text, "1").Tables[0];
            dtCliente = dal.getBuscarClientePorRut(lblRutCotizacion.Text).Tables[0];
            PdfPTable tableDatosClienteAsociado = new PdfPTable(4);
            foreach (DataRow item in dtCliente.Rows)
            {
                string rutPadre = item["RUT_PADRE"].ToString();
                if (rutPadre.Trim() != string.Empty)
                {
                    string razonSocialPadre = item["RAZON_PADRE"].ToString();
                    string direccionPadre = item["DIRECCION_PADRE"].ToString();
                    string telefonoPadre = item["TELEFONO_PADRE"].ToString();
                    string giroPadre = item["GIRO_PADRE"].ToString();

                    doc.Add(new Paragraph(" ", times));
                    Chunk datosClienteAsociado = new Chunk("Datos Cliente Principal", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD));
                    datosClienteAsociado.SetUnderline(0.1f, -2f);
                    doc.Add(datosClienteAsociado);

                    float[] widthsDatosClienteAsociado = new float[] { 35f, 95f, 35f, 55f };
                    tableDatosClienteAsociado.SetWidths(widthsDatosClienteAsociado);
                    tableDatosClienteAsociado.AddCell(new Paragraph("Rut :", fontCabecera));
                    tableDatosClienteAsociado.AddCell(new Paragraph(rutPadre, times));
                    tableDatosClienteAsociado.AddCell(new Paragraph("Nombre :", fontCabecera));
                    tableDatosClienteAsociado.AddCell(new Paragraph(razonSocialPadre, times));

                    tableDatosClienteAsociado.AddCell(new Paragraph("Dirección :", fontCabecera));
                    tableDatosClienteAsociado.AddCell(new Paragraph(razonSocialPadre, times));
                    tableDatosClienteAsociado.AddCell(new Paragraph("Teléfono:", fontCabecera));
                    tableDatosClienteAsociado.AddCell(new Paragraph(telefonoPadre, times));

                    tableDatosClienteAsociado.AddCell(new Paragraph("Giro :", fontCabecera));
                    tableDatosClienteAsociado.AddCell(new Paragraph(giroPadre, times));
                    tableDatosClienteAsociado.AddCell(new Paragraph(" ", fontCabecera));
                    tableDatosClienteAsociado.AddCell(new Paragraph(" ", times));

                    tableDatosClienteAsociado.HorizontalAlignment = Element.ALIGN_LEFT;
                    tableDatosClienteAsociado.WidthPercentage = 100.0f;
                }

            }

            doc.Add(tableDatosClienteAsociado);
            doc.Add(new Paragraph(" ", times));

            //doc.Add(new Paragraph(" ", times));
            //doc.Add(new Paragraph(" En respuesta a su consulta, le entregamos la siguiente cotizacion.", times));
            //doc.Add(new Paragraph(" Confiamos que tanto nuestros precios como condiciones le sean favorables, y aprovechamos la oportunidad para saludarle y quedar a vuestra entera disposicion.", times));
            //doc.Add(new Paragraph(" ", times));
            Chunk datosDetalleCotizacion = new Chunk("Detalle Cotización", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD));
            datosDetalleCotizacion.SetUnderline(0.1f, -2f);
            doc.Add(datosDetalleCotizacion);

            doc.Add(new Paragraph(" ", times));

            PdfPTable tableDetalle = new PdfPTable(5);
            tableDetalle.AddCell(new Paragraph("Código", fontCabecera));
            tableDetalle.AddCell(new Paragraph("Producto", fontCabecera));
            tableDetalle.AddCell(new Paragraph("Cantidad", fontCabecera));
            tableDetalle.AddCell(new Paragraph("Monto Uni", fontCabecera));
            tableDetalle.AddCell(new Paragraph("Valor Final", fontCabecera));
            float[] widthsDatosDetalle = new float[] { 35f, 105f, 25f, 35f, 25f };
            tableDetalle.SetWidths(widthsDatosDetalle);

            DataTable dtDetalleCotizacion = new DataTable();
            dtDetalleCotizacion = dal.getBuscarCotizacionDetalle(cotizacion).Tables[0];
            string codigoProd = string.Empty;
            string nombreProducto = string.Empty;
            string cantidadProducto = string.Empty;
            string montoUni = string.Empty;
            string descuentoPorcentaje = string.Empty;
            string descuentoMonto = string.Empty;
            string valorFinal = string.Empty;
            int contador = 0;

            foreach (DataRow item in dtDetalleCotizacion.Rows)
            {
                codigoProd = item["CODIGO"].ToString();
                nombreProducto = item["NOM_PRODUCTO"].ToString();
                cantidadProducto = item["CANTIDAD"].ToString();
                descuentoMonto = item["DESCUENTO"].ToString();
                descuentoPorcentaje = item["PORC_DESCUENTO"].ToString();
                montoUni = (((Convert.ToDouble(item["MONTO_NETO"].ToString()))) / Convert.ToDouble(cantidadProducto)).ToString("n0");
                valorFinal = (Convert.ToDouble(montoUni) * Convert.ToDouble(cantidadProducto)).ToString("n0");

                if (descuentoPorcentaje == string.Empty)
                {
                    descuentoPorcentaje = "0";
                }

                if (Convert.ToDecimal(descuentoPorcentaje) <= 0)
                {

                }

                if (descuentoMonto == string.Empty)
                {
                    descuentoMonto = "0";
                }

                if (descuentoMonto != string.Empty)
                {
                    descuentoMonto = Convert.ToInt32(Convert.ToDouble(descuentoMonto)).ToString("n0");
                }

                PdfPCell celdaCodigoProd = new PdfPCell(new Paragraph(codigoProd, times));
                celdaCodigoProd.BorderColorBottom = CMYKColor.WHITE;
                celdaCodigoProd.Border = Rectangle.NO_BORDER;
                celdaCodigoProd.Border = Rectangle.RIGHT_BORDER;
                celdaCodigoProd.Border = Rectangle.LEFT_BORDER;
                tableDetalle.AddCell(celdaCodigoProd);

                PdfPCell celdaNombreProducto = new PdfPCell(new Paragraph(nombreProducto, times));
                celdaNombreProducto.BorderColorBottom = CMYKColor.WHITE;
                celdaNombreProducto.Border = Rectangle.NO_BORDER;
                celdaNombreProducto.Border = Rectangle.RIGHT_BORDER;
                celdaNombreProducto.Border = Rectangle.LEFT_BORDER;
                tableDetalle.AddCell(celdaNombreProducto);
                
                PdfPCell celdaCantidad = new PdfPCell(new Paragraph(cantidadProducto, times));
                celdaCantidad.HorizontalAlignment = 2;
                celdaCantidad.BorderColorBottom = CMYKColor.WHITE;
                celdaCantidad.Border = Rectangle.NO_BORDER;
                celdaCantidad.Border = Rectangle.RIGHT_BORDER;
                celdaCantidad.Border = Rectangle.LEFT_BORDER;
                //celdaCantidad.BorderWidthLeft = 1f;
                //celdaCantidad.BorderWidthTop = 1f;
                //celdaCantidad.BorderWidthRight = 1f;
                tableDetalle.AddCell(celdaCantidad);

                PdfPCell celdaMontoUni = new PdfPCell(new Paragraph(montoUni, times));
                celdaMontoUni.HorizontalAlignment = 2;
                celdaMontoUni.BorderColorBottom = CMYKColor.WHITE;
                celdaMontoUni.Border = Rectangle.NO_BORDER;
                celdaMontoUni.Border = Rectangle.RIGHT_BORDER;
                celdaMontoUni.Border = Rectangle.LEFT_BORDER;
                tableDetalle.AddCell(celdaMontoUni);


                PdfPCell celdaValorFinal = new PdfPCell(new Paragraph(valorFinal, times));
                celdaValorFinal.HorizontalAlignment = 2;
                //celdaValorFinal.Border = Rectangle.NO_BORDER;
                celdaValorFinal.Border = Rectangle.RIGHT_BORDER;
                celdaValorFinal.Border = Rectangle.LEFT_BORDER;
                tableDetalle.AddCell(celdaValorFinal);
      
                contador++;
                
            }

            PdfPCell vacio1 = new PdfPCell(new Paragraph(" ", times));
            vacio1.HorizontalAlignment = 2;
            vacio1.BorderColorBottom = CMYKColor.WHITE;
            vacio1.Border = Rectangle.NO_BORDER;
            vacio1.Border = Rectangle.RIGHT_BORDER;
            vacio1.Border = Rectangle.LEFT_BORDER;
            PdfPCell vacio2 = new PdfPCell(new Paragraph(" ", times));
            vacio2.HorizontalAlignment = 2;
            vacio2.BorderColorBottom = CMYKColor.WHITE;
            vacio2.Border = Rectangle.NO_BORDER;
            vacio2.Border = Rectangle.RIGHT_BORDER;
            vacio2.Border = Rectangle.LEFT_BORDER;
            PdfPCell vacio3 = new PdfPCell(new Paragraph(" ", times));
            vacio3.HorizontalAlignment = 2;
            vacio3.BorderColorBottom = CMYKColor.WHITE;
            vacio3.Border = Rectangle.NO_BORDER;
            vacio3.Border = Rectangle.RIGHT_BORDER;
            vacio3.Border = Rectangle.LEFT_BORDER;
            PdfPCell vacio4 = new PdfPCell(new Paragraph(" ", times));
            vacio4.HorizontalAlignment = 2;
            vacio4.BorderColorBottom = CMYKColor.WHITE;
            vacio4.Border = Rectangle.NO_BORDER;
            vacio4.Border = Rectangle.RIGHT_BORDER;
            vacio4.Border = Rectangle.LEFT_BORDER;
            PdfPCell vacio5 = new PdfPCell(new Paragraph(" ", times));
            vacio5.HorizontalAlignment = 2;
            vacio5.BorderColorBottom = CMYKColor.WHITE;
            vacio5.Border = Rectangle.NO_BORDER;
            vacio5.Border = Rectangle.RIGHT_BORDER;
            vacio5.Border = Rectangle.LEFT_BORDER;

            tableDetalle.AddCell(vacio1);
            tableDetalle.AddCell(vacio2);
            tableDetalle.AddCell(vacio3);
            tableDetalle.AddCell(vacio4);
            tableDetalle.AddCell(vacio5);

            DataTable dtCotizacion = new DataTable();
            dtCotizacion = dal.getBuscarCotizacionPorId(cotizacion).Tables[0];
            string observacion = string.Empty;
            foreach (DataRow item in dtCotizacion.Rows)
            {
                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" Total Neto", times));

                PdfPCell celdaTotalNeto = new PdfPCell(new Paragraph(Convert.ToInt32(item["MONTO_NETO"]).ToString("n0"), times));
                celdaTotalNeto.HorizontalAlignment = 2;
                tableDetalle.AddCell(celdaTotalNeto);

                if (Convert.ToDouble(descuentoPagoContado) > 0)
                {
                    tableDetalle.AddCell(new Paragraph(" ", times));
                    tableDetalle.AddCell(new Paragraph(" ", times));
                    tableDetalle.AddCell(new Paragraph(" ", times));
                    tableDetalle.AddCell(new Paragraph(" Descuento Pago Contado", times));
                    tableDetalle.AddCell(new Paragraph(descuentoPagoContado, times));
                }

                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" Iva", times));

                PdfPCell celdaValorIva = new PdfPCell(new Paragraph(Convert.ToInt32(item["MONTO_IVA"]).ToString("n0"), times));
                celdaValorIva.HorizontalAlignment = 2;
                tableDetalle.AddCell(celdaValorIva);

                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" ", times));
                tableDetalle.AddCell(new Paragraph(" Total", times));

                PdfPCell celdaValorTotal = new PdfPCell(new Paragraph(Convert.ToInt32(item["MONTO_TOTAL"]).ToString("n0"), times));
                celdaValorTotal.HorizontalAlignment = 2;
                tableDetalle.AddCell(celdaValorTotal);

                observacion = item["OBSERVACION"].ToString();
            }

            tableDetalle.HorizontalAlignment = Element.ALIGN_LEFT;
            tableDetalle.WidthPercentage = 100.0f;

            foreach (PdfPCell celda in tableDetalle.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 2;
            }

            int contTabla = tableDetalle.Rows.Count;
            for (int i = 1; i < contTabla; i++)
            {
                foreach (PdfPCell celda in tableDetalle.Rows[i].GetCells())
                {
                    
                    int dd = tableDetalle.Rows.Count;
                    celda.Border = Rectangle.BOX;
                    //celda.BorderWidth = 1;
                    celda.DisableBorderSide(Rectangle.TOP_BORDER);
                    celda.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                    
                    //celda.Border = Rectangle.RIGHT_BORDER;
                    //celda.Border = Rectangle.LEFT_BORDER;
                    //Rectangle.BOX;
                }
               
                

            }
            tableDetalle.DefaultCell.EnableBorderSide(Rectangle.BOTTOM_BORDER);
            tableDetalle.DefaultCell.BorderWidthBottom = 1;
            doc.Add(tableDetalle);
            PdfPTable tableCierre = new PdfPTable(1);
            tableCierre.WidthPercentage = 100.0f;
            tableCierre.AddCell(" ");
            doc.Add(tableCierre);
            doc.Add(new Paragraph(" ", times));

            PdfPTable tableObservacion = new PdfPTable(1);
            tableObservacion.AddCell(new Paragraph("Observación", fontCabecera));
            tableObservacion.WidthPercentage = 100.0f;
            foreach (PdfPCell celda in tableObservacion.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 2;
            }

            tableObservacion.AddCell(new Paragraph(observacion, times));
            doc.Add(tableObservacion);

            string dias = "";
            string idEmpresa = Session["idEmpresa"].ToString();
            DataTable dtParametro = dal.getBuscarParametros(idEmpresa).Tables[0];
            foreach (DataRow item in dtParametro.Rows)
            {
                dias = item["VIGENCIA_COTIZACION_DIAS"].ToString();
            }

            doc.Add(new Paragraph(" Le envia esta cotización " + Session["variableNombreEjecutivo"].ToString(), timesBold));
            doc.Add(new Paragraph(" FECHA DE ENTREGA: A CONVENIR.", times));
            doc.Add(new Paragraph(" VALIDEZ DE LA OFERTA: "+dias+" DIAS CORRIDOS O HASTA TERMINAR STOCK.", timesBold));

            doc.Add(new Paragraph(" ", times));
            PdfPTable tableCondicionComercial = new PdfPTable(1);
            tableCondicionComercial.AddCell(new Paragraph("Condición Venta", fontCabecera));
            tableCondicionComercial.WidthPercentage = 100.0f;
            float[] widthsCondicionComercial = new float[] { 35f };
            tableCondicionComercial.SetWidths(widthsCondicionComercial);
            foreach (PdfPCell celda in tableCondicionComercial.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 2;
            }

            DataTable dtCondicionComercial = new DataTable();
            dtCondicionComercial = dal.getBuscarCondicionVenta(idCondicionDeVenta).Tables[0];

            foreach (DataRow item in dtCondicionComercial.Rows)
            {
                tableCondicionComercial.AddCell(new Paragraph(item["GLOSA"].ToString(), times));
            }
            doc.Add(tableCondicionComercial);

            //rutRazonSocial = item["RUT"].ToString();
            //direccionRazonSocial = item["DIRECCION"].ToString();
            //telefonoRazonSocial = item["FONO"].ToString();
            //emailRazonSocial = item["EMAIL"].ToString();
            //bancoRazonSocial = item["BANCO"].ToString();
            //cuentaCorrienteRazonSocial = item["CUENTA_CTE"].ToString();
            doc.Add(new Paragraph(" ", times));
            doc.Add(new Paragraph(" DATOS TRANSFERENCIA: ", timesBold));
            doc.Add(new Paragraph(" BANCO: " + bancoRazonSocial, times));
            doc.Add(new Paragraph(" CUENTA CTE: " + cuentaCorrienteRazonSocial, times));
            doc.Add(new Paragraph(" CORREO CONFIRMACION: " + emailEmpresa, times));
            doc.Add(new Paragraph(" NOTA: EN ASUNTO DE LA TRANSFERENCIA DEBE INCLUIR EL NUMERO DE LA COTIZACION.", timesBold));

            //NUEVA PAGINA
            doc.NewPage();

            Chunk tituloSegundaPagina = new Chunk("Productos", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD));
            tituloSegundaPagina.SetUnderline(0.1f, -2f);

            Paragraph parSegPag = new Paragraph(tituloSegundaPagina);
            parSegPag.Alignment = Element.ALIGN_CENTER;
            doc.Add(parSegPag);

            doc.Add(new Paragraph(" ", times));

            PdfPTable tableProductosImagen = new PdfPTable(4);
            tableProductosImagen.AddCell(new Paragraph("Código", fontCabecera));
            tableProductosImagen.AddCell(new Paragraph("Producto", fontCabecera));
            tableProductosImagen.AddCell(new Paragraph("Observación", fontCabecera));
            tableProductosImagen.AddCell(new Paragraph("Imagen", fontCabecera));

            float[] widthsProductosImagen = new float[] { 35f,35f, 105f, 35f };
            tableProductosImagen.SetWidths(widthsProductosImagen);

            DataTable dtImagen = new DataTable();
            dtImagen = dal.getBuscarImagenesProductoPorCotizacion(cotizacion).Tables[0];
            foreach (DataRow item in dtImagen.Rows)
            {
                tableProductosImagen.AddCell(new Paragraph(item["CODIGO"].ToString(), times));
                tableProductosImagen.AddCell(new Paragraph(item["NOM_PRODUCTO"].ToString(), times));
                tableProductosImagen.AddCell(new Paragraph(item["OBSERVACION_PROD"].ToString(), times));

                string imagen = item["IMAGEN"].ToString();
                if (imagen == string.Empty)
                {
                    tableProductosImagen.AddCell(new Paragraph("Sin Imagen", times));
                }
                else
                {
                    iTextSharp.text.Image jpgImagen = iTextSharp.text.Image.GetInstance(Server.MapPath(imagen));
                    jpgImagen.ScaleToFit(120, 120);
                    jpgImagen.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                    tableProductosImagen.AddCell(jpgImagen);
                }
            }
            doc.Add(tableProductosImagen);

            //NUEVA PAGINA
            foreach (DataRow item in dtImagen.Rows)
            {
                string ficha = item["FICHA_TECNICA_PDF"].ToString();
                if (ficha == string.Empty)
                {

                }
                else
                {

                }

            }
            
            doc.Close();
            string ruta = "cotizaciones/" + nombreArchivoPdf;
            hfrutaArchivoPdf.Value = ruta;
            return ruta;
        }


        private string generaPdf2(string cotizacion, string ejecutivo, string rut, string idEmpresa)
        {
            string str1 = DateTime.Today.ToString("dd-MM-yyyy");
            string str2 = "Cotizacion_" + cotizacion + ".pdf";
            BaseFont font1 = BaseFont.CreateFont("Helvetica", "Cp1252", false);
            iTextSharp.text.Font font2 = new iTextSharp.text.Font(font1, 7f, 0);
            iTextSharp.text.Font font3 = new iTextSharp.text.Font(font1, 7f, 1);
            iTextSharp.text.Font font4 = new iTextSharp.text.Font(font1, 8f, 0);
            iTextSharp.text.Font font5 = new iTextSharp.text.Font(font1, 9f, 1, BaseColor.RED);
            iTextSharp.text.Font font6 = new iTextSharp.text.Font(font1, 9f, 1);
            iTextSharp.text.Font font7 = new iTextSharp.text.Font(font1, 8f, 1);
            iTextSharp.text.Font font8 = new iTextSharp.text.Font(font1, 8f, 1);
            iTextSharp.text.Font font9 = new iTextSharp.text.Font(font1, 10f, 1);
            iTextSharp.text.Font font10 = new iTextSharp.text.Font(font1, 12f, 1);
            Document document = new Document(PageSize.A4, 25f, 25f, 30f, 30f);
            PdfWriter.GetInstance(document, (Stream)new FileStream(this.Server.MapPath("cotizaciones/" + str2), FileMode.Create));
            document.Open();
            string path1 = "";
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarEmpresa((string)null, idEmpresa).Tables[0].Rows)
            {
                path1 = row["IMAGEN"].ToString();
                empty1 = row["EMAIL"].ToString();
                empty2 = row["TELEFONO"].ToString();
            }
            if (path1 == string.Empty)
                path1 = "assets/img/imagenesEmpresa/1_1_logo fadonel con call center y productos.png";
            iTextSharp.text.Image instance1 = iTextSharp.text.Image.GetInstance(this.Server.MapPath(path1));
            instance1.ScaleToFit(150f, 150f);
            instance1.Alignment = 0;
            document.Add((IElement)instance1);
            string empty3 = string.Empty;
            string empty4 = string.Empty;
            string empty5 = string.Empty;
            string empty6 = string.Empty;
            string empty7 = string.Empty;
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarRazonSocial(new int?(Convert.ToInt32(this.ddlRazonSocial.SelectedValue))).Tables[0].Rows)
            {
                empty3 = row["RUT"].ToString();
                empty4 = row["DIRECCION"].ToString();
                row["EMAIL"].ToString();
                empty6 = row["BANCO"].ToString();
                empty7 = row["CUENTA_CTE"].ToString();
            }
            PdfPTable pdfPtable1 = new PdfPTable(2);
            PdfPCell cell1 = new PdfPCell((Phrase)new Paragraph("\nCOTIZACIÓN \nNº " + cotizacion, font10));
            cell1.HorizontalAlignment = 1;
            cell1.VerticalAlignment = 4;
            new PdfPCell((Phrase)new Paragraph(this.ddlRazonSocial.SelectedItem.ToString(), font9)).VerticalAlignment = 6;
            PdfPCell cell2 = new PdfPCell((Phrase)new Paragraph("Importación, Publicidad, Impresión de Telas, Comercializacion de Toldos Araña, Banderas, Carpas Araña, Pendones Roller, Cortinas con Diseño, Manteles, Paneles Araña Textil, Lienzos, Pasacalle, Minipendones, Banderas Velas, Banderas Gotas, Banderas Corporativas, Impresión y Aplicación de Logos.", font4));
            cell2.Border = 0;
            pdfPtable1.AddCell(cell2);
            pdfPtable1.AddCell(cell1);
            pdfPtable1.DefaultCell.Border = 0;
            pdfPtable1.HorizontalAlignment = 2;
            pdfPtable1.WidthPercentage = 100f;
            foreach (Rectangle cell3 in pdfPtable1.Rows[0].GetCells())
                cell3.Border = 0;
            document.Add((IElement)pdfPtable1);
            document.Add((IElement)new Paragraph(empty4, font8));
            document.Add((IElement)new Paragraph("FONO: " + empty2, font8));
            document.Add((IElement)new Paragraph(empty1, font8));
            string content = string.Empty;
            string empty8 = string.Empty;
            string id = this.Session["IdCliente"].ToString();
            DataTable dataTable1 = new DataTable();
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarClientePorId(id).Tables[0].Rows)
                content = !(row["RUT_PADRE"].ToString() != string.Empty) ? "Datos Cliente" : "Datos Cliente Asociado";
            document.Add((IElement)new Paragraph(" ", font2));
            Chunk chunk1 = new Chunk(content, FontFactory.GetFont("ARIAL", 9f, 1));
            chunk1.SetUnderline(0.1f, -2f);
            document.Add((IElement)chunk1);
            PdfPTable pdfPtable2 = new PdfPTable(4);
            DataTable dataTable2 = new DataTable();
            DataTable table1 = this.dal.getBuscarClientePorId(id).Tables[0];
            string str3 = "";
            string idCondicionVenta = "";
            string empty9 = string.Empty;
            string empty10 = string.Empty;
            string empty11 = string.Empty;
            foreach (DataRow row in (InternalDataCollectionBase)table1.Rows)
            {
                str3 = row["GIRO"].ToString();
                idCondicionVenta = row["CONDICION_DE_VENTA"].ToString();
                empty9 = row["RAZON_SOCIAL"].ToString();
                empty10 = row["DIRECCION"].ToString();
                empty11 = row["TELEFONO"].ToString();
            }
            string empty12 = string.Empty;
            DataTable dataTable3 = new DataTable();
            DataTable table2 = this.dal.getBuscarCotizacionPorId(cotizacion).Tables[0];
            string empty13 = string.Empty;
            foreach (DataRow row in (InternalDataCollectionBase)table2.Rows)
            {
                empty12 = row["ID_CONTACTO"].ToString();
                empty13 = row["MONTO_DESCUENTO"].ToString();
            }
            DataTable dataTable4 = new DataTable();
            DataTable table3 = this.dal.getBuscarContactoPorId(empty12).Tables[0];
            string empty14 = string.Empty;
            string empty15 = string.Empty;
            string empty16 = string.Empty;
            string empty17 = string.Empty;
            foreach (DataRow row in (InternalDataCollectionBase)table3.Rows)
            {
                empty14 = row["NOM_CONTACTO"].ToString();
                row["TELEFONO1"].ToString();
                row["CELULAR"].ToString();
                row["EMAIL_1"].ToString();
            }
            float[] relativeWidths1 = new float[4]
            {
                35f,
                95f,
                35f,
                55f
            };
            pdfPtable2.SetWidths(relativeWidths1);
            pdfPtable2.AddCell((Phrase)new Paragraph("Rut :", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(rut, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Nombre :", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(empty9, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Dirección :", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(empty10, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Teléfono:", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(empty11, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Giro :", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(str3, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Fecha :", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(str1, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Nombre Contacto :", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(empty14, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph(" ", font7));
            pdfPtable2.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable2.HorizontalAlignment = 0;
            pdfPtable2.WidthPercentage = 100f;
            document.Add((IElement)pdfPtable2);
            document.Add((IElement)new Paragraph(" ", font2));
            DataTable dataTable5 = new DataTable();
            DataTable table4 = this.dal.getBuscarClientePorId(id).Tables[0];
            PdfPTable pdfPtable3 = new PdfPTable(4);
            foreach (DataRow row in (InternalDataCollectionBase)table4.Rows)
            {
                string str4 = row["RUT_PADRE"].ToString();
                if (str4.Trim() != string.Empty)
                {
                    string str5 = row["RAZON_PADRE"].ToString();
                    row["DIRECCION_PADRE"].ToString();
                    string str6 = row["TELEFONO_PADRE"].ToString();
                    string str7 = row["GIRO_PADRE"].ToString();
                    document.Add((IElement)new Paragraph(" ", font2));
                    Chunk chunk2 = new Chunk("Datos Cliente Principal", FontFactory.GetFont("ARIAL", 9f, 1));
                    chunk2.SetUnderline(0.1f, -2f);
                    document.Add((IElement)chunk2);
                    float[] relativeWidths2 = new float[4]
                    {
                        35f,
                        95f,
                        35f,
                        55f
                    };
                    pdfPtable3.SetWidths(relativeWidths2);
                    pdfPtable3.AddCell((Phrase)new Paragraph("Rut :", font7));
                    pdfPtable3.AddCell((Phrase)new Paragraph(str4, font2));
                    pdfPtable3.AddCell((Phrase)new Paragraph("Nombre :", font7));
                    pdfPtable3.AddCell((Phrase)new Paragraph(str5, font2));
                    pdfPtable3.AddCell((Phrase)new Paragraph("Dirección :", font7));
                    pdfPtable3.AddCell((Phrase)new Paragraph(str5, font2));
                    pdfPtable3.AddCell((Phrase)new Paragraph("Teléfono:", font7));
                    pdfPtable3.AddCell((Phrase)new Paragraph(str6, font2));
                    pdfPtable3.AddCell((Phrase)new Paragraph("Giro :", font7));
                    pdfPtable3.AddCell((Phrase)new Paragraph(str7, font2));
                    pdfPtable3.AddCell((Phrase)new Paragraph(" ", font7));
                    pdfPtable3.AddCell((Phrase)new Paragraph(" ", font2));
                    pdfPtable3.HorizontalAlignment = 0;
                    pdfPtable3.WidthPercentage = 100f;
                }
            }
            document.Add((IElement)pdfPtable3);
            document.Add((IElement)new Paragraph(" ", font2));
            Chunk chunk3 = new Chunk("Detalle Cotización", FontFactory.GetFont("ARIAL", 9f, 1));
            chunk3.SetUnderline(0.1f, -2f);
            document.Add((IElement)chunk3);
            document.Add((IElement)new Paragraph(" ", font2));
            PdfPTable pdfPtable4 = new PdfPTable(5);
            pdfPtable4.AddCell((Phrase)new Paragraph("Código", font7));
            pdfPtable4.AddCell((Phrase)new Paragraph("Producto", font7));
            pdfPtable4.AddCell((Phrase)new Paragraph("Cantidad", font7));
            pdfPtable4.AddCell((Phrase)new Paragraph("Monto Uni", font7));
            pdfPtable4.AddCell((Phrase)new Paragraph("Valor Final", font7));
            float[] relativeWidths3 = new float[5]
            {
                35f,
                105f,
                25f,
                35f,
                25f
            };
            pdfPtable4.SetWidths(relativeWidths3);
            DataTable dataTable6 = new DataTable();
            DataTable table5 = this.dal.getBuscarCotizacionDetalle(cotizacion).Tables[0];
            string empty18 = string.Empty;
            string empty19 = string.Empty;
            string empty20 = string.Empty;
            string empty21 = string.Empty;
            string empty22 = string.Empty;
            string empty23 = string.Empty;
            string empty24 = string.Empty;
            int num1 = 0;
            foreach (DataRow row in (InternalDataCollectionBase)table5.Rows)
            {
                string str4 = row["CODIGO"].ToString();
                string str5 = row["NOM_PRODUCTO"].ToString();
                string str6 = row["CANTIDAD"].ToString();
                string str7 = row["DESCUENTO"].ToString();
                string str8 = row["PORC_DESCUENTO"].ToString();
                double num2 = Convert.ToDouble(row["MONTO_NETO"].ToString()) / Convert.ToDouble(str6);
                string str9 = num2.ToString("n0");
                num2 = Convert.ToDouble(str9) * Convert.ToDouble(str6);
                string str10 = num2.ToString("n0");
                if (str8 == string.Empty)
                    str8 = "0";
                int num3 = Convert.ToDecimal(str8) <= Decimal.Zero ? 1 : 0;
                if (str7 == string.Empty)
                    str7 = "0";
                if (str7 != string.Empty)
                    empty23 = Convert.ToInt32(Convert.ToDouble(str7)).ToString("n0");
                PdfPCell cell3 = new PdfPCell((Phrase)new Paragraph(str4, font2));
                cell3.BorderColorBottom = BaseColor.WHITE;
                cell3.Border = 0;
                cell3.Border = 8;
                cell3.Border = 4;
                pdfPtable4.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell((Phrase)new Paragraph(str5, font2));
                cell4.BorderColorBottom = BaseColor.WHITE;
                cell4.Border = 0;
                cell4.Border = 8;
                cell4.Border = 4;
                pdfPtable4.AddCell(cell4);
                PdfPCell cell5 = new PdfPCell((Phrase)new Paragraph(str6, font2));
                cell5.HorizontalAlignment = 2;
                cell5.BorderColorBottom = BaseColor.WHITE;
                cell5.Border = 0;
                cell5.Border = 8;
                cell5.Border = 4;
                pdfPtable4.AddCell(cell5);
                PdfPCell cell6 = new PdfPCell((Phrase)new Paragraph(str9, font2));
                cell6.HorizontalAlignment = 2;
                cell6.BorderColorBottom = BaseColor.WHITE;
                cell6.Border = 0;
                cell6.Border = 8;
                cell6.Border = 4;
                pdfPtable4.AddCell(cell6);
                PdfPCell cell7 = new PdfPCell((Phrase)new Paragraph(str10, font2));
                cell7.HorizontalAlignment = 2;
                cell7.Border = 0;
                cell7.Border = 8;
                cell7.Border = 4;
                pdfPtable4.AddCell(cell7);
                ++num1;
            }
            PdfPCell cell8 = new PdfPCell((Phrase)new Paragraph(" ", font2));
            cell8.HorizontalAlignment = 2;
            cell8.BorderColorBottom = BaseColor.WHITE;
            cell8.Border = 0;
            cell8.Border = 1;
            PdfPCell cell9 = new PdfPCell((Phrase)new Paragraph(" ", font2));
            cell9.BorderColorBottom = BaseColor.WHITE;
            cell9.Border = 0;
            PdfPCell cell10 = new PdfPCell((Phrase)new Paragraph(" ", font2));
            cell10.HorizontalAlignment = 2;
            cell10.BorderColorBottom = BaseColor.WHITE;
            cell10.Border = 0;
            cell10.Border = 8;
            cell10.Border = 4;
            PdfPCell cell11 = new PdfPCell((Phrase)new Paragraph(" ", font2));
            cell11.HorizontalAlignment = 2;
            cell11.BorderColorBottom = BaseColor.WHITE;
            cell11.Border = 0;
            cell11.Border = 8;
            cell11.Border = 4;
            PdfPCell cell12 = new PdfPCell((Phrase)new Paragraph(" ", font2));
            cell12.HorizontalAlignment = 2;
            cell12.BorderColorBottom = BaseColor.WHITE;
            cell12.Border = 0;
            cell12.Border = 8;
            cell12.Border = 4;
            PdfPCell cell13 = new PdfPCell((Phrase)new Paragraph(" ", font2));
            cell13.HorizontalAlignment = 2;
            cell13.BorderColorBottom = BaseColor.WHITE;
            cell13.Border = 0;
            cell13.Border = 8;
            cell13.Border = 4;
            PdfPCell cell14 = new PdfPCell((Phrase)new Paragraph(" ", font2));
            cell14.HorizontalAlignment = 2;
            cell14.BorderColorBottom = BaseColor.WHITE;
            cell14.Border = 0;
            cell14.Border = 8;
            cell14.Border = 4;
            pdfPtable4.AddCell(cell10);
            pdfPtable4.AddCell(cell11);
            pdfPtable4.AddCell(cell12);
            pdfPtable4.AddCell(cell13);
            pdfPtable4.AddCell(cell14);
            string empty25 = string.Empty;
            string empty26 = string.Empty;
            string empty27 = string.Empty;
            DataTable dataTable7 = new DataTable();
            DataTable table6 = this.dal.getBuscarCotizacionPorId(cotizacion).Tables[0];
            string empty28 = string.Empty;
            pdfPtable4.HorizontalAlignment = 0;
            pdfPtable4.WidthPercentage = 100f;
            foreach (PdfPCell cell3 in pdfPtable4.Rows[0].GetCells())
            {
                cell3.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell3.HorizontalAlignment = 1;
                cell3.Padding = 2f;
            }
            int count1 = pdfPtable4.Rows.Count;
            for (int index = 1; index < count1; ++index)
            {
                foreach (PdfPCell cell3 in pdfPtable4.Rows[index].GetCells())
                {
                    int count2 = pdfPtable4.Rows.Count;
                    cell3.Border = 15;
                    cell3.DisableBorderSide(1);
                    cell3.DisableBorderSide(2);
                }
            }
            foreach (DataRow row in (InternalDataCollectionBase)table6.Rows)
            {
                row["MONTO_NETO"].ToString();
                row["MONTO_IVA"].ToString();
                row["MONTO_TOTAL"].ToString();
                pdfPtable4.AddCell(cell8);
                pdfPtable4.AddCell(cell8);
                pdfPtable4.AddCell(cell8);
                pdfPtable4.AddCell((Phrase)new Paragraph(" Neto", font2));
                pdfPtable4.AddCell(new PdfPCell((Phrase)new Paragraph("$" + Convert.ToInt32(row["MONTO_NETO"]).ToString("n0"), font2))
                {
                    HorizontalAlignment = 2
                });
                if (Convert.ToDouble(empty13) > 0.0)
                {
                    pdfPtable4.AddCell(cell9);
                    pdfPtable4.AddCell(cell9);
                    pdfPtable4.AddCell(cell9);
                    pdfPtable4.AddCell((Phrase)new Paragraph(" Descuento Pago Contado", font2));
                    pdfPtable4.AddCell((Phrase)new Paragraph("$" + empty13, font2));
                }
                pdfPtable4.AddCell(cell9);
                pdfPtable4.AddCell(cell9);
                pdfPtable4.AddCell(cell9);
                pdfPtable4.AddCell((Phrase)new Paragraph(" 19% I.V.A.", font2));
                pdfPtable4.AddCell(new PdfPCell((Phrase)new Paragraph("$" + Convert.ToInt32(row["MONTO_IVA"]).ToString("n0"), font2))
                {
                    HorizontalAlignment = 2
                });
                cell9.DisableBorderSide(2);
                pdfPtable4.AddCell(cell9);
                pdfPtable4.AddCell(cell9);
                pdfPtable4.AddCell(cell9);
                pdfPtable4.AddCell((Phrase)new Paragraph(" Total", font2));
                pdfPtable4.AddCell(new PdfPCell((Phrase)new Paragraph("$" + Convert.ToInt32(row["MONTO_TOTAL"]).ToString("n0"), font2))
                {
                    HorizontalAlignment = 2
                });
                empty28 = row["OBSERVACION"].ToString();
            }
            document.Add((IElement)pdfPtable4);
            document.Add((IElement)new Paragraph(" ", font2));
            PdfPTable pdfPtable5 = new PdfPTable(1);
            pdfPtable5.AddCell((Phrase)new Paragraph("Observación", font7));
            pdfPtable5.WidthPercentage = 100f;
            foreach (PdfPCell cell3 in pdfPtable5.Rows[0].GetCells())
            {
                cell3.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell3.HorizontalAlignment = 1;
                cell3.Padding = 2f;
            }
            pdfPtable5.AddCell((Phrase)new Paragraph(empty28, font2));
            document.Add((IElement)pdfPtable5);
            string str11 = "";
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarParametros(idEmpresa).Tables[0].Rows)
                str11 = row["VIGENCIA_COTIZACION_DIAS"].ToString();
            document.Add((IElement)new Paragraph(" Le envia esta cotización " + this.Session["variableNombreEjecutivo"].ToString(), font3));
            document.Add((IElement)new Paragraph(" FECHA DE ENTREGA: A CONVENIR.", font2));
            document.Add((IElement)new Paragraph(" VALIDEZ DE LA OFERTA: " + str11 + " DIAS CORRIDOS O HASTA TERMINAR STOCK.", font3));
            document.Add((IElement)new Paragraph(" ", font2));
            PdfPTable pdfPtable6 = new PdfPTable(1);
            pdfPtable6.AddCell((Phrase)new Paragraph("Condición Venta", font7));
            pdfPtable6.WidthPercentage = 100f;
            float[] relativeWidths4 = new float[1] { 35f };
            pdfPtable6.SetWidths(relativeWidths4);
            foreach (PdfPCell cell3 in pdfPtable6.Rows[0].GetCells())
            {
                cell3.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell3.HorizontalAlignment = 1;
                cell3.Padding = 2f;
            }
            DataTable dataTable8 = new DataTable();
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarCondicionVenta(idCondicionVenta).Tables[0].Rows)
                pdfPtable6.AddCell((Phrase)new Paragraph(row["GLOSA"].ToString(), font2));
            document.Add((IElement)pdfPtable6);
            document.Add((IElement)new Paragraph(" ", font2));
            document.Add((IElement)new Paragraph(" DATOS TRANSFERENCIA: ", font3));
            document.Add((IElement)new Paragraph(" RAZON SOCIAL: " + this.ddlRazonSocial.SelectedItem.ToString(), font2));
            document.Add((IElement)new Paragraph(" BANCO: " + empty6, font2));
            document.Add((IElement)new Paragraph(" RUT: " + empty3, font2));
            document.Add((IElement)new Paragraph(" CUENTA CTE: " + empty7, font2));
            document.Add((IElement)new Paragraph(" CORREO CONFIRMACION: " + empty1, font2));
            document.Add((IElement)new Paragraph(" NOTA: EN ASUNTO DE LA TRANSFERENCIA DEBE INCLUIR EL NUMERO DE LA COTIZACION.", font3));
            document.NewPage();
            Chunk chunk4 = new Chunk("Productos", FontFactory.GetFont("ARIAL", 11f, 1));
            chunk4.SetUnderline(0.1f, -2f);
            document.Add((IElement)new Paragraph(chunk4)
            {
                Alignment = 1
            });
            document.Add((IElement)new Paragraph(" ", font2));
            PdfPTable pdfPtable7 = new PdfPTable(4);
            pdfPtable7.AddCell((Phrase)new Paragraph("Código", font7));
            pdfPtable7.AddCell((Phrase)new Paragraph("Producto", font7));
            pdfPtable7.AddCell((Phrase)new Paragraph("Observación", font7));
            pdfPtable7.AddCell((Phrase)new Paragraph("Imagen", font7));
            float[] relativeWidths5 = new float[4]
            {
        35f,
        35f,
        105f,
        35f
            };
            pdfPtable7.SetWidths(relativeWidths5);
            DataTable dataTable9 = new DataTable();
            DataTable table7 = this.dal.getBuscarImagenesProductoPorCotizacion(cotizacion).Tables[0];
            foreach (DataRow row in (InternalDataCollectionBase)table7.Rows)
            {
                pdfPtable7.AddCell((Phrase)new Paragraph(row["CODIGO"].ToString(), font2));
                pdfPtable7.AddCell((Phrase)new Paragraph(row["NOM_PRODUCTO"].ToString(), font2));
                pdfPtable7.AddCell((Phrase)new Paragraph(row["OBSERVACION_PROD"].ToString(), font2));
                string path2 = row["IMAGEN"].ToString();
                if (path2 == string.Empty)
                {
                    pdfPtable7.AddCell((Phrase)new Paragraph("Sin Imagen", font2));
                }
                else
                {
                    iTextSharp.text.Image instance2 = iTextSharp.text.Image.GetInstance(this.Server.MapPath(path2));
                    instance2.ScaleToFit(120f, 120f);
                    instance2.Alignment = 0;
                    pdfPtable7.AddCell(instance2);
                }
            }
            document.Add((IElement)pdfPtable7);
            foreach (DataRow row in (InternalDataCollectionBase)table7.Rows)
            {
                int num2 = row["FICHA_TECNICA_PDF"].ToString() == string.Empty ? 1 : 0;
            }
            document.Close();
            string str12 = "cotizaciones/" + str2;
            this.hfrutaArchivoPdf.Value = str12;
            return str12;
        }

        protected void ddlRazonSocial_DataBound(object sender, EventArgs e)
        {
            try
            {
                ddlRazonSocial.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void ibtnEditarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdContacto = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblIdContacto");
                Label _lblEmail1 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblEmail1");
                Label _lblEmail2 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblEmail2");
                Label _lblCelular = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblCelular");
                Label _lblTelefono1 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblTelefono1");
                Label _lblTelefono2 = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblTelefono2");
                Label _lblNombreContacto = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblNombreContacto");
                Label _lblCargo = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblCargo");
                Label _lblIdCargo = (Label)grvContactosCotizacion.Rows[row.RowIndex].FindControl("lblIdCargo");

                buscarContactoCargo();
                limpiarContacto();

                lblAgregarContacto.Text = "Editar Contacto";
                hfIdContacto.Value = _lblIdContacto.Text;
                txtNombreContacto.Text = _lblNombreContacto.Text;
                txtEmail1.Text = _lblEmail1.Text;
                txtEmail2.Text = _lblEmail2.Text;
                txtCelular.Text = _lblCelular.Text;
                txtTelefono1.Text = _lblTelefono1.Text;
                txtTelefono2.Text = _lblTelefono2.Text;


                if (_lblIdCargo.Text != string.Empty)
                {
                    ddlCargo.SelectedValue = _lblIdCargo.Text;
                }

                btnGrabarContacto.Visible = false;
                btnModificarContacto.Visible = true;

                mdlAgregarContacto.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        void limpiarContacto()
        {
            //hfIdContacto.Value = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtEmail1.Text = string.Empty;
            txtEmail2.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtTelefono1.Text = string.Empty;
            txtTelefono2.Text = string.Empty;
            ddlCargo.ClearSelection();
        }

        protected void txtDescuentoPagoContado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calcularTotalPagoContado();
            }
            catch (Exception ex)
            {
                this.lblInformacion.Text = ex.Message;
                this.mdlInformacion.Show();
            }
        }

    }
}