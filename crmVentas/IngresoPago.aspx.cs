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
    public partial class IngresoPago : System.Web.UI.Page
    {
        Datos dal = new Datos();
        Comun comunes = new Comun();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    DataTable dt = new DataTable();
                    Session["dtPagos"] = dt;

                    dt.Clear();
                    CreateDataTable();

                    formaPago();
                    banco();
                    validarFormaPago();

                    if (Session["idNotaVenta"].ToString() != null)
                    {
                        if (Session["idNotaVenta"].ToString() == "0")
                        {

                        }
                        else
                        {
                            string idNotaVenta = Session["idNotaVenta"].ToString();
                            DataTable dtCliente = new DataTable();
                            dtCliente=dal.getBuscarClientePorIdNotaVenta(Convert.ToInt32(idNotaVenta)).Tables[0];

                            foreach (DataRow item in dtCliente.Rows)
                            {
                                lblIdNotaVenta.Text = idNotaVenta;
                                lblRutCliente.Text = item["RUT_CLIENTE"].ToString();
                                lblNombreCliente.Text = item["RAZON_SOCIAL"].ToString();
                                string strMontoNeto = item["MONTO_NETO"].ToString();

                                double montoNeto = Convert.ToDouble(strMontoNeto);
                                double iva = montoNeto / 100 * 19;
                                double montoTotal = montoNeto + iva;

                                lblMontoNeto.Text = montoNeto.ToString("n0");
                                lblIva.Text = iva.ToString("n0");
                                lblMontoTotal.Text = montoTotal.ToString("n0");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void formaPago()
        {
            ddlFormaPago.DataSource = dal.getBuscarFormaPago(null);
            ddlFormaPago.DataValueField = "ID_FORMA_PAGO";
            ddlFormaPago.DataTextField = "NOM_FORMA_PAGO";
            ddlFormaPago.DataBind();
        }

        void banco() 
        {
            ddlBanco.DataSource = dal.getBuscarBanco();
            ddlBanco.DataTextField = "NOM_BANCO";
            ddlBanco.DataValueField = "ID_BANCO";
            ddlBanco.DataBind();
        }


        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                validarFormaPago();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void lbtnGrabar_Click(object sender, EventArgs e)
        {
            try
            {

                double montoTotal = 0;
                foreach (GridViewRow item in grvPago.Rows)
                {
                    Label _lblMonto = (Label)grvPago.Rows[item.RowIndex].FindControl("lblMonto");
                    montoTotal += Convert.ToDouble(_lblMonto.Text.Trim().Replace(".","").Replace(",",""));
                }

                //if (montoTotal != Convert.ToDouble(lblMontoTotal.Text.Replace(".", "")))
                //{
                //    lblInformacion.Text = "El valor total a pagar no coincide con el valor total de los documentos.";
                //    mdlInformacion.Show();
                //    return;
                //}

                string idPago = "0";
                string idNotaVenta = Session["idNotaVenta"].ToString();
                string idUsuario = Session["variableIdUsuario"].ToString();

                idPago = dal.setIngresarCajaEnc(Convert.ToInt32(idNotaVenta), lblRutCliente.Text, "", "", idUsuario, lblMontoNeto.Text.Replace(".", ""), montoTotal.ToString());
                //idPago = dal.setIngresarCajaEnc(Convert.ToInt32(idNotaVenta),lblRutCliente.Text,"","",idUsuario,lblMontoNeto.Text.Replace(".",""));
                dal.setIngresarCajaDet(idPago, "1", "", lblMontoNeto.Text.Replace(".", ""));
           
                foreach (GridViewRow item in grvPago.Rows)
                {
                    Label _lblId = (Label)grvPago.Rows[item.RowIndex].FindControl("lblId");
                    Label _lblIdFormaPago = (Label)grvPago.Rows[item.RowIndex].FindControl("lblIdFormaPago");
                    Label _lblRutCheque = (Label)grvPago.Rows[item.RowIndex].FindControl("lblRutCheque");
                    Label _lblMonto = (Label)grvPago.Rows[item.RowIndex].FindControl("lblMonto");
                    Label _lblFechaVencimiento = (Label)grvPago.Rows[item.RowIndex].FindControl("lblFechaVencimiento");
                    Label _lblIdBanco = (Label)grvPago.Rows[item.RowIndex].FindControl("lblIdBanco");
                    Label _lblNumeroChequeDeposito = (Label)grvPago.Rows[item.RowIndex].FindControl("lblNumeroChequeDeposito");
                    Label _lblCuentaCorriente = (Label)grvPago.Rows[item.RowIndex].FindControl("lblCuentaCorriente");

                    dal.setIngresarCajaDocPago(idPago, _lblId.Text, _lblMonto.Text, _lblFechaVencimiento.Text, _lblIdBanco.Text, _lblNumeroChequeDeposito.Text, _lblCuentaCorriente.Text, _lblIdFormaPago.Text, _lblRutCheque.Text);
                }
                
                //cambiar estado nota de venta
                dal.setEditarEstadoNotaVenta(Convert.ToInt32(idNotaVenta));


                Response.Redirect("Default.aspx");
                //lblInformacionPago.Text = "El pago se ha realizado correctamente. <br> El número generado es el <strong>" + idPago + "</strong>";
                //mdlInformacionPago.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        

        protected void lbtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Session["dtPagos"] as DataTable;

                if (ddlFormaPago.SelectedValue == "1")
                {
                    if (txtMonto.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el monto";
                        mdlInformacion.Show();
                        return;
                    }
                }
                if (ddlFormaPago.SelectedValue == "2")
                {
                    if (txtRutCheque.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el rut";
                        mdlInformacion.Show();
                        return;
                    }

                    if (txtRutCheque.Text != string.Empty)
                    {
                        if (comunes.validarRut(txtRutCheque.Text) == false)
                        {
                            lblInformacion.Text = "El rut no es valido.";
                            mdlInformacion.Show();
                            return;
                        }

                    }


                    if (txtMonto.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el monto";
                        mdlInformacion.Show();
                        return;
                    }
                    if (txtFechaVencimiento.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar la fecha de vencimiento";
                        mdlInformacion.Show();
                        return;
                    }
                    if (ddlBanco.SelectedValue == "0")
                    {
                        lblInformacion.Text = "Favor seleccionar el banco";
                        mdlInformacion.Show();
                        return;
                    }
                    if (txtNumeroChequeODeposito.Text == "0")
                    {
                        lblInformacion.Text = "Favor ingresar el número de cheque";
                        mdlInformacion.Show();
                        return;
                    }
                    if (txtCuentaCorriente.Text == "0")
                    {
                        lblInformacion.Text = "Favor ingresar el número de cuenta corriente";
                        mdlInformacion.Show();
                        return;
                    }
                    
                }
                if (ddlFormaPago.SelectedValue == "3")
                {
                    if (txtRutCheque.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el rut";
                        mdlInformacion.Show();
                        return;
                    }

                    if (txtRutCheque.Text != string.Empty)
                    {
                        if (comunes.validarRut(txtRutCheque.Text) == false)
                        {
                            lblInformacion.Text = "El rut no es valido.";
                            mdlInformacion.Show();
                            return;
                        }
                    }

                    if (txtMonto.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el monto";
                        mdlInformacion.Show();
                        return;
                    }
                    if (txtFechaVencimiento.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar la fecha de vencimiento";
                        mdlInformacion.Show();
                        return;
                    }
                    if (ddlBanco.SelectedValue == "0")
                    {
                        lblInformacion.Text = "Favor seleccionar el banco";
                        mdlInformacion.Show();
                        return;
                    }
                    if (txtNumeroChequeODeposito.Text == "0")
                    {
                        lblInformacion.Text = "Favor ingresar el número de cheque";
                        mdlInformacion.Show();
                        return;
                    }
                    if (txtCuentaCorriente.Text == "0")
                    {
                        lblInformacion.Text = "Favor ingresar el número de cuenta corriente";
                        mdlInformacion.Show();
                        return;
                    }
                    
                }
                if (ddlFormaPago.SelectedValue == "4")
                {
                    if (txtMonto.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el monto";
                        mdlInformacion.Show();
                        return;
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = "1";
                    dr["ID_FORMA_PAGO"] = ddlFormaPago.SelectedValue;
                    dr["FORMA_PAGO"] = ddlFormaPago.SelectedItem.ToString();
                    dr["RUT_CHEQUE"] = txtRutCheque.Text;
                    if (txtMonto.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el monto";
                        mdlInformacion.Show();
                        return;
                    }
                    dr["MONTO"] = txtMonto.Text;
                    dr["FECHA_VENCIMIENTO"] = txtFechaVencimiento.Text;
                    dr["ID_BANCO"] = ddlBanco.SelectedValue;
                    dr["BANCO"] = ddlBanco.SelectedItem.ToString();
                    dr["NUMERO_CHEQUE"] = txtNumeroChequeODeposito.Text;
                    dr["CUENTA_CORRIENTE"] = txtCuentaCorriente.Text;

                    dt.Rows.Add(dr);
                    
                    grvPago.DataSource = dt;
                    grvPago.DataBind();
                }
                else
                {
                    int cont = dt.Rows.Count;
                    cont++;

                    DataRow dr = dt.NewRow();
                    dr["ID"] = cont;
                    dr["ID_FORMA_PAGO"] = ddlFormaPago.SelectedValue;
                    dr["FORMA_PAGO"] = ddlFormaPago.SelectedItem.ToString();
                    dr["RUT_CHEQUE"] = txtRutCheque.Text;

                    if (txtMonto.Text == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar el monto";
                        mdlInformacion.Show();
                        return;
                    }
                    dr["MONTO"] = txtMonto.Text;
                    dr["FECHA_VENCIMIENTO"] = txtFechaVencimiento.Text;
                    dr["ID_BANCO"] = ddlBanco.SelectedValue;
                    dr["BANCO"] = ddlBanco.SelectedItem.ToString();
                    dr["NUMERO_CHEQUE"] = txtNumeroChequeODeposito.Text;
                    dr["CUENTA_CORRIENTE"] = txtCuentaCorriente.Text;

                    dt.Rows.Add(dr);

                    grvPago.DataSource = dt;
                    grvPago.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }



        void validarFormaPago()
        {
            txtRutCheque.Text = string.Empty;
            //txtMonto.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;
            ddlBanco.ClearSelection();
            txtNumeroChequeODeposito.Text = string.Empty;
            txtCuentaCorriente.Text = string.Empty;

            if (ddlFormaPago.SelectedValue == "1")
            {
                txtRutCheque.Enabled = false;
                txtMonto.Enabled = true;
                txtFechaVencimiento.Enabled = false;
                ddlBanco.Enabled = false;
                txtNumeroChequeODeposito.Enabled = false;
                txtCuentaCorriente.Enabled = false;
            }
            if (ddlFormaPago.SelectedValue == "2")
            {
                txtRutCheque.Enabled = true;
                txtMonto.Enabled = true;
                txtFechaVencimiento.Enabled = true;
                ddlBanco.Enabled = true;
                txtNumeroChequeODeposito.Enabled = true;
                txtCuentaCorriente.Enabled = true;

                DateTime Hoy = DateTime.Today;
                txtFechaVencimiento.Text = Hoy.ToString("dd-MM-yyyy");
            }
            if (ddlFormaPago.SelectedValue == "3")
            {
                txtRutCheque.Enabled = true;
                txtMonto.Enabled = true;
                txtFechaVencimiento.Enabled = true;
                ddlBanco.Enabled = true;
                txtNumeroChequeODeposito.Enabled = true;
                txtCuentaCorriente.Enabled = true;

            }
            if (ddlFormaPago.SelectedValue == "4")
            {
                txtRutCheque.Enabled = true;
                txtMonto.Enabled = true;
                txtFechaVencimiento.Enabled = true;
                ddlBanco.Enabled = true;
                txtNumeroChequeODeposito.Enabled = true;
                txtCuentaCorriente.Enabled = true;

            }
        }


        protected void CreateDataTable()
        {
            try
            {
                DataTable dt = Session["dtPagos"] as DataTable;

                dt.Columns.Add("ID", typeof(Int32));
                dt.Columns.Add("ID_FORMA_PAGO", typeof(String));
                dt.Columns.Add("FORMA_PAGO", typeof(String));
                dt.Columns.Add("RUT_CHEQUE", typeof(String));
                dt.Columns.Add("MONTO", typeof(String));
                dt.Columns.Add("FECHA_VENCIMIENTO", typeof(String));
                dt.Columns.Add("ID_BANCO", typeof(String));
                dt.Columns.Add("BANCO", typeof(String));
                dt.Columns.Add("NUMERO_CHEQUE", typeof(String));
                dt.Columns.Add("CUENTA_CORRIENTE", typeof(String));
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                //mdlInformacion.Show();
            }

        }


        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
                
                Label _lblId = (Label)grvPago.Rows[row.RowIndex].FindControl("lblId");
                DataTable dt = Session["dtPagos"] as DataTable;
                dt.AcceptChanges();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ID"].ToString() == _lblId.Text)
                        dr.Delete();
                }
                dt.AcceptChanges();

                grvPago.DataSource = dt;
                grvPago.DataBind();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        int total = 0;

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label _lblMonto = (Label)e.Row.FindControl("lblMonto");
                int monto = Convert.ToInt32(_lblMonto.Text);
                total += monto;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label _lblTotalMonto = (Label)e.Row.FindControl("lblTotalMonto");
                _lblTotalMonto.Text = total.ToString("n0");
            }
        }


    }
}