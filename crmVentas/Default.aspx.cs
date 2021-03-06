﻿using System;
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
using crm_valvulas_industriales;
using System.Collections;

namespace crm_fadonel
{
    public partial class Default : System.Web.UI.Page
    {
        Datos dal = new Datos();
        Comun comunes = new Comun();
        DataSet ds = new DataSet();
        DataSet dsCotizaciones = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnSeleccionarDireccionGenerarNotaVenta);


                if (lblRut.Text == string.Empty)
                {
                    ibtnEditarCliente.Visible = false;
                }
                else
                {
                    ibtnEditarCliente.Visible = true;
                }

                if (this.Session["IdCliente"] == null)
                    this.ibtnEditarCliente.Visible = false;
                else
                    this.ibtnEditarCliente.Visible = true;

               
                
                if (this.Page.IsPostBack)
                    return;
                this.Session["SortedView"] = (object)null;
                this.Session["SortedViewSeguimiento"] = (object)null;
                this.hfVieneSeguimiento.Value = "0";

                if (Session["idEmpresa"] == null)
                {
                    Session["idEmpresa"] = "1";
                }
                
                string idCliente = Convert.ToString(this.Request.QueryString["c"]);
                string str1 = Convert.ToString(this.Request.QueryString["t"]);
                string flagCartera = Convert.ToString(this.Request.QueryString["car"]);
                string str2 = Convert.ToString(this.Request.QueryString["agendamiento"]);
                string str3 = Convert.ToString(this.Request.QueryString["seguimiento"]);
                if (idCliente == null)
                {
                    this.buscarEstatus("0");
                    this.buscarSubEstatus(this.ddlEstatus.SelectedValue);
                    this.buscarEstatusSeguimiento(this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue);
                    if (this.Session["IdCliente"] != null)
                    {
                        idCliente = this.Session["IdCliente"].ToString();
                        this.buscarClientePorRut(idCliente.Trim());
                        this.buscarDireccionesPorCliente();
                    }
                    if (this.Session["IdCliente"] == null)
                        this.ibtnEditarCliente.Visible = false;
                    else
                        this.ibtnEditarCliente.Visible = true;
                }
                else
                {
                    this.buscarClientePorRut(idCliente.Trim());
                    this.buscarDireccionesPorCliente();
                    if (str1 == "1")
                    {
                        this.TabContainer1.ActiveTab = this.tpGestion;
                        if (flagCartera == "1")
                        {
                            this.buscarEstatus(flagCartera);
                            this.buscarSubEstatus(this.ddlEstatus.SelectedValue);
                            this.buscarEstatusSeguimiento(this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue);
                        }
                        else if (str2 == "1")
                        {
                            this.buscarEstatus((string)null);
                            this.buscarSubEstatus(this.ddlEstatus.SelectedValue);
                            this.buscarEstatusSeguimiento(this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue);
                        }
                        else
                        {
                            this.buscarEstatus("0");
                            this.buscarSubEstatus(this.ddlEstatus.SelectedValue);
                            this.buscarEstatusSeguimiento(this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue);
                        }
                        this.hfVieneSeguimiento.Value = !(str3 == "1") ? "0" : "1";
                    }
                    else
                    {
                        this.buscarEstatus("0");
                        this.buscarSubEstatus(this.ddlEstatus.SelectedValue);
                        this.buscarEstatusSeguimiento(this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue);
                    }
                    if (this.Session["IdCliente"] == null)
                        this.ibtnEditarCliente.Visible = false;
                    else
                        this.ibtnEditarCliente.Visible = true;
                }
                DataTable dataTable = new DataTable();
                this.Session["dtDetalleProductos"] = (object)dataTable;
                this.hfRutClientePost.Value = idCliente;
                this.hfRutCliente.Value = idCliente;
                this.Session["rutCliente"] = (object)idCliente;
                dataTable.Clear();
                this.CreateDataTable();
                this.buscarCliente(idCliente);
                this.txtFecAgendamiento_CalendarExtender.StartDate = new DateTime?(DateTime.Now);
                this.buscarCondicionVenta();
                if (this.Session["IdCliente"] == null)
                    this.ibtnEditarCliente.Visible = false;
                else
                    this.ibtnEditarCliente.Visible = true;
                this.buscarCampana();
                this.buscarActividadComercial();
                BuscarNotaVenta();
                BuscarOT();
                factura();

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
                if (this.txtRutoRazonSocial.Text.Length < 1)
                {
                    this.lblInformacion.Text = "Texto de busqueda debe tener al menos 3 caracteres";
                    this.mdlInformacion.Show();
                }
                else
                {
                    this.grvEmpresas.DataSource = (object)this.dal.getBuscarEmpresaPorNombreRut(this.txtRutoRazonSocial.Text.Trim(), this.txtRutoRazonSocial.Text.Trim()).Tables[0];
                    this.grvEmpresas.DataBind();
                    this.txtRutoRazonSocial.Text = string.Empty;
                    this.mdlEmpresas.Show();
                }

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        
        protected void imgSeleccionarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow namingContainer = (GridViewRow)((Control)sender).NamingContainer;
                Label control1 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblIdCliente");
                Label control2 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblEmailCliente");
                Label control3 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblRutCliente");
                Label control4 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblRazonSocial");
                Label control5 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblDireccion");
                Label control6 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblComuna");
                Label control7 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblMontoCredito");
                Label control8 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblClasificacion");
                Label control9 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblZona");
                Label control10 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblGiro");
                Label control11 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblEstado");
                Label control12 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblCondicionVenta");
                Label control13 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblNomCampana");
                Label control14 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblIdCampana");
                Label control15 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblMontoVenta12Meses");
                Label control16 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblTotalCotizado");
                Label control17 = (Label)this.grvEmpresas.Rows[namingContainer.RowIndex].FindControl("lblTotalCerrado");
                this.Session["IdCliente"] = (object)control1.Text;
                this.Session["rutCliente"] = (object)control3.Text;
                this.lblRut.Text = control3.Text;
                this.lblRut.Visible = true;
                this.lblRazonSocial.Text = control4.Text;
                this.lblRazonSocial.Visible = true;
                this.lblMontoCredito.Text = control7.Text;
                this.lblMontoCredito.Visible = true;
                this.lblClasificacion.Text = control8.Text;
                this.lblClasificacion.Visible = true;
                this.lblZona.Text = control9.Text;
                this.lblZona.Visible = true;
                this.lblGiro.Text = control10.Text;
                this.lblGiro.Visible = true;
                this.lblEstado.Text = control11.Text;
                this.lblEstado.Visible = true;
                this.lblCondiciondeVenta.Text = control12.Text;
                this.lblCondiciondeVenta.Visible = true;
                this.lblCampana.Text = control13.Text;
                this.lblCampana.Visible = true;
                this.lblIdCampana.Text = control14.Text;
                this.lblEmailCliente.Text = control2.Text;
                Decimal num1;
                if (control15.Text != string.Empty)
                {
                    Label montoVenta12Meses = this.lblMontoVenta12Meses;
                    string str1 = "$";
                    num1 = Convert.ToDecimal(control15.Text);
                    string str2 = num1.ToString("n0");
                    string str3 = str1 + str2;
                    montoVenta12Meses.Text = str3;
                }
                else
                    this.lblMontoVenta12Meses.Text = string.Empty;
                this.lblMontoVenta12Meses.Visible = true;
                Decimal num2 = new Decimal();
                Decimal num3 = new Decimal();
                Decimal num4;
                if (control16.Text != string.Empty)
                {
                    num4 = Convert.ToDecimal(control16.Text);
                    Label lblTotalCotizado = this.lblTotalCotizado;
                    string str1 = "$";
                    num1 = Convert.ToDecimal(control16.Text);
                    string str2 = num1.ToString("n0");
                    string str3 = str1 + str2;
                    lblTotalCotizado.Text = str3;
                }
                else
                {
                    num4 = new Decimal();
                    this.lblTotalCotizado.Text = string.Empty;
                }
                Decimal num5;
                if (control17.Text != string.Empty)
                {
                    num5 = Convert.ToDecimal(control17.Text);
                    Label lblTotalCerrado = this.lblTotalCerrado;
                    string str1 = "$";
                    num1 = Convert.ToDecimal(control17.Text);
                    string str2 = num1.ToString("n0");
                    string str3 = str1 + str2;
                    lblTotalCerrado.Text = str3;
                }
                else
                {
                    num5 = new Decimal();
                    this.lblTotalCerrado.Text = string.Empty;
                }
                if (num5 != Decimal.Zero)
                {
                    Label porcentajeCierre = this.lblPorcentajeCierre;
                    num1 = num5 / num4 * new Decimal(100);
                    string str = num1.ToString("n") + "%";
                    porcentajeCierre.Text = str;
                }
                else
                    this.lblPorcentajeCierre.Text = string.Empty;
                this.tablaCliente.Visible = true;
                this.buscarContactos(control1.Text);
                this.hfRutClientePost.Value = control3.Text;
                this.buscarCliente(control1.Text);
                this.buscarCotizaciones(control1.Text);
                
                this.buscarGestion(control1.Text);
                lblId.Text = control1.Text;
                this.lblRutGestiones.Text = this.lblRut.Text;
                this.lblRazonSocialGestiones.Text = this.lblRazonSocial.Text;
                this.buscarGestionesCRM();
                BuscarNotaVenta();
                BuscarOT();
                this.factura();
                this.buscarDireccionesPorCliente();
                if (control1.Text == string.Empty)
                    this.ibtnEditarCliente.Visible = false;
                else
                    this.ibtnEditarCliente.Visible = true;


            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarDireccionesPorCliente() 
        {
            this.grvDireccionCliente.DataSource = (object)this.dal.getBuscarDireccionPorCliente(this.Session["IdCliente"].ToString());
            this.grvDireccionCliente.DataBind();
        }

        void buscarGestionesCRM()
        {
            DataTable dt = dal.getBuscarGestiones(Session["IdCliente"].ToString(), Session["idEmpresa"].ToString()).Tables[0];

            grvGestionesCRM.DataSource = dt;
            grvGestionesCRM.DataBind();

            grvHistorialGestiones.DataSource = dt;
            grvHistorialGestiones.DataBind();
        }

        protected void imgFirstgrvCotizacionesCRM_Click(object sender, EventArgs e)
        {
      
            if (Session["SortedView"] != null)
            {
                grvCotizacionesCRM.DataSource = Session["SortedView"];

            }
            else
            {
                cotizacionesSession();
            }
 
            grvCotizacionesCRM.PageIndex = 0;
            grvCotizacionesCRM.DataBind();
        }

        protected void imgPrevgrvCotizacionesCRM_Click(object sender, EventArgs e)
        {
  
            if (Session["SortedView"] != null)
            {
                grvCotizacionesCRM.DataSource = Session["SortedView"];
            
            }
            else
            {
                cotizacionesSession();
            }
            if (grvCotizacionesCRM.PageIndex != 0)
                grvCotizacionesCRM.PageIndex--;
            grvCotizacionesCRM.DataBind();
        }

        protected void imgNextgrvCotizacionesCRM_Click(object sender, EventArgs e)
        {
           
            if (Session["SortedView"] != null)
            {
                grvCotizacionesCRM.DataSource = Session["SortedView"];
             
            }
            else
            {
                cotizacionesSession();
            }


            if (grvCotizacionesCRM.PageIndex != (grvCotizacionesCRM.PageCount - 1))
                grvCotizacionesCRM.PageIndex++;
            grvCotizacionesCRM.DataBind();
        }

        protected void imgLastgrvCotizacionesCRM_Click(object sender, EventArgs e)
        {
            
            if (Session["SortedView"] != null)
            {
                grvCotizacionesCRM.DataSource = Session["SortedView"];
             
            }
            else
            {
                cotizacionesSession();
            }
            grvCotizacionesCRM.PageIndex = grvCotizacionesCRM.PageCount - 1;
            grvCotizacionesCRM.DataBind();
        }


        protected void gvEmployeegrvCotizacionesCRM_Sorting(object sender, GridViewSortEventArgs e)
        {
            cotizacionesSession();

            DataTable dt = new DataTable();
            dt = dsCotizaciones.Tables[0];
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
                Session["SortedView"] = sortedView;

                grvCotizacionesCRM.DataSource = sortedView;
                grvCotizacionesCRM.DataBind();

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

        void buscarCotizaciones(string rut)
        {
          
            Session["DatosCotizaciones"] = dal.getBuscarCotizacionesEnSeguimientoPorCliente(rut, null);
            grvCotizacionesCRM.DataSource = Session["DatosCotizaciones"] as DataSet;
            grvCotizacionesCRM.DataBind();

        }

        protected void lbtnDetalleCotizacionCotizacionesCRM_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdCotizacion = (Label)grvCotizacionesCRM.Rows[row.RowIndex].FindControl("lblIdCotizacion");

                buscarDetalleCotizacion(_lblIdCotizacion.Text.Trim());

               
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void lbtnDetalleCotizacionCotizacionesCRMNv_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdCotizacion = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdCotizacion");
                
                //Label _lblIdCotizacion2 = (Label)grvOrdenDeTrabajo.Rows[row.RowIndex].FindControl("lblIdCotizacion");
                buscarDetalleCotizacion(_lblIdCotizacion.Text.Trim());

                //buscarDetalleCotizacion(_lblIdCotizacion2.Text.Trim());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void lbtnDetalleCotizacionCotizacionesCRMOT_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                
                Label _lblIdCotizacion = (Label)grvOrdenDeTrabajo.Rows[row.RowIndex].FindControl("lblIdCotizacion");
                buscarDetalleCotizacion(_lblIdCotizacion.Text.Trim());
                
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        void buscarDetalleCotizacion(string cotizacion)
        {
            lblNumeroCotizacionDetalle.Text = "Cotización Nro. " + cotizacion;
            grvDetalleCotizacion.DataSource = dal.getBuscarDetalleCotizacion(cotizacion);
            grvDetalleCotizacion.DataBind();
            mdlDetalleCotizacion.Show();
        }
        

        void buscarGestion(string rut)
        {
          

            string idPerfil = Session["variablePerfil"].ToString();
            string idEmpresa = this.Session["idEmpresa"].ToString();
            string rutCliente = this.Session["IdCliente"].ToString();

            if (idPerfil=="1")
            {
                this.grvGestion.DataSource = (object)this.dal.getBuscarCotizacionesParaGestionar(rutCliente, null);
            }
            else
            {
                this.grvGestion.DataSource = (object)this.dal.getBuscarCotizacionesParaGestionar(rutCliente, idEmpresa);
            }

            
            this.grvGestion.DataBind();
            this.Session["DatosCotizaciones"] = (object)this.dal.getBuscarCotizacionesEnSeguimientoPorCliente(rutCliente, (string)null);
            this.grvCotizacionesCRM.DataSource = (object)(this.Session["DatosCotizaciones"] as DataSet);
            this.grvCotizacionesCRM.DataBind();
        }
        
        void buscarClientePorRut(string idCliente)
        {
          
            DataTable table = this.dal.getBuscarClientePorId(idCliente).Tables[0];
            if (table.Rows.Count == 0)
            {
                this.lblInformacion.Text = "Cliente no existe en la base de datos. Puede que haya sido eliminado.";
                this.mdlInformacion.Show();
            }
            else
            {
                foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                {
                    this.hfIdCliente.Value = row["ID_CLIENTE"].ToString();
                    this.Session["IdCliente"] = (object)this.hfIdCliente.Value;
                    this.lblRut.Text = row["RUT_CLIENTE"].ToString();
                    this.Session["rutCliente"] = (object)row["RUT_CLIENTE"].ToString();
                    this.lblRazonSocial.Text = row["RAZON_SOCIAL"].ToString();
                    this.lblMontoCredito.Text = row["MONTO_CREDITO"].ToString();
                    this.lblMontoCredito.Visible = true;
                    this.lblClasificacion.Text = row["CLASIFICACION"].ToString();
                    this.lblClasificacion.Visible = true;
                    this.lblZona.Text = row["ZONA"].ToString();
                    this.lblZona.Visible = true;
                    this.lblGiro.Text = row["GIRO"].ToString();
                    this.lblGiro.Visible = true;
                    this.lblEstado.Text = row["NOM_ESTADO_CLIENTE"].ToString();
                    this.lblEstado.Visible = true;
                    lblId.Text = hfIdCliente.Value;
                    this.lblRutGestiones.Text = row["RUT_CLIENTE"].ToString();
                    this.lblRazonSocialGestiones.Text = row["RAZON_SOCIAL"].ToString();
                    this.lblUsuarioAsig.Text = row["USUARIO"].ToString();
                    this.lblCampana.Text = row["NOM_CAMPANA"].ToString();
                    this.lblIdCampana.Text = row["ID_CAMPANA"].ToString();
                    this.lblCondiciondeVenta.Text = row["CONDICION_VENTA"].ToString();
                    this.lblUsuarioAsig.Text = row["USUARIO"].ToString();
                    string str1 = row["RUT_PADRE"].ToString();
                    if (str1.Trim() != string.Empty)
                    {
                        this.trRutClientePadreCliente.Visible = true;
                        this.trRutClientePadreGestion.Visible = true;
                        this.lblRutClientePadreCliente.Text = str1;
                        this.lblRutClientePadreGestion.Text = str1;
                        this.lblRazonSocialPadreCliente.Text = row["RAZON_PADRE"].ToString();
                        this.lblRazonSocialPadreGestion.Text = row["RAZON_PADRE"].ToString();
                    }
                    else
                    {
                        this.trRutClientePadreCliente.Visible = false;
                        this.trRutClientePadreGestion.Visible = false;
                        DataTable dataTable = new DataTable();
                        if (this.dal.getBuscarClientesAsociados(this.hfIdCliente.Value).Tables[0].Rows.Count == 0)
                            this.btnVerClientesAsociados.Visible = false;
                        else
                            this.btnVerClientesAsociados.Visible = true;
                    }
                    Decimal num1;
                    if (row["MONTO_VENTA_ULTIMO_12"].ToString() != string.Empty)
                    {
                        Label montoVenta12Meses = this.lblMontoVenta12Meses;
                        string str2 = "$";
                        num1 = Convert.ToDecimal(row["MONTO_VENTA_ULTIMO_12"].ToString());
                        string str3 = num1.ToString("n0");
                        string str4 = str2 + str3;
                        montoVenta12Meses.Text = str4;
                    }
                    else
                        this.lblMontoVenta12Meses.Text = string.Empty;
                    Decimal num2 = new Decimal();
                    Decimal num3 = new Decimal();
                    Decimal num4;
                    if (row["MONTO_COTIZADO"].ToString() != string.Empty)
                    {
                        num4 = Convert.ToDecimal(row["MONTO_COTIZADO"].ToString());
                        Label lblTotalCotizado = this.lblTotalCotizado;
                        string str2 = "$";
                        num1 = Convert.ToDecimal(row["MONTO_COTIZADO"].ToString());
                        string str3 = num1.ToString("n0");
                        string str4 = str2 + str3;
                        lblTotalCotizado.Text = str4;
                    }
                    else
                    {
                        num4 = new Decimal();
                        this.lblTotalCotizado.Text = string.Empty;
                    }
                    Decimal num5;
                    if (row["MONTO_CERRADO"].ToString() != string.Empty)
                    {
                        num5 = Convert.ToDecimal(row["MONTO_CERRADO"].ToString());
                        Label lblTotalCerrado = this.lblTotalCerrado;
                        string str2 = "$";
                        num1 = Convert.ToDecimal(row["MONTO_CERRADO"].ToString());
                        string str3 = num1.ToString("n0");
                        string str4 = str2 + str3;
                        lblTotalCerrado.Text = str4;
                    }
                    else
                    {
                        num5 = new Decimal();
                        this.lblTotalCerrado.Text = string.Empty;
                    }
                    this.lblEmailCliente.Text = row["EMAIL"].ToString();
                    if (num5 != Decimal.Zero)
                    {
                        Label porcentajeCierre = this.lblPorcentajeCierre;
                        num1 = num5 / num4 * new Decimal(100);
                        string str2 = num1.ToString("n") + "%";
                        porcentajeCierre.Text = str2;
                    }
                    else
                        this.lblPorcentajeCierre.Text = string.Empty;
                    this.tablaCliente.Visible = true;
                    this.buscarContactos(this.hfIdCliente.Value);
                    this.buscarCotizaciones(this.hfIdCliente.Value);
                }
                this.tablaCliente.Visible = true;
                this.buscarGestion(this.hfIdCliente.Value);
                this.buscarGestionesCRM();
                BuscarNotaVenta();
                BuscarOT();
                this.factura();
                this.limpiarContacto();
            }

        }
        


        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
        
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
                Session["SortedViewSeguimiento"] = sortedView;
                
            }
        }

        protected void lbtnDetalleCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;

                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblCotizacion = (Label)grvGestion.Rows[row.RowIndex].FindControl("lblCotizacion");

                buscarDetalleCotizacion(_lblCotizacion.Text.Trim());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarEstatus(string flagCartera)
        {
            string idEmpresa = Session["idEmpresa"].ToString();

            ddlEstatus.DataSource = dal.getBuscarEstatus(null, flagCartera,"C");
            ddlEstatus.DataValueField = "ID_ESTATUS";
            ddlEstatus.DataTextField = "ESTATUS";
            ddlEstatus.DataBind();
        }

        void buscarSubEstatus(string estatus)
        {
            string idEmpresa = Session["idEmpresa"].ToString();

            ddlSubEstatus.DataSource = dal.getBuscarSubEstatus(estatus, idEmpresa);
            ddlSubEstatus.DataValueField = "ID_SUB_ESTATUS";
            ddlSubEstatus.DataTextField = "SUB_ESTATUS";
            ddlSubEstatus.DataBind();
        }

        void buscarEstatusSeguimiento(string estatus, string subEstatus)
        {
            string idEmpresa = Session["idEmpresa"].ToString();

            ddlEstatusSeguimiento.DataSource = dal.getBuscarEstatusSeguimiento(estatus, subEstatus, idEmpresa);
            ddlEstatusSeguimiento.DataValueField = "ID_ESTATUS_SEGUIMIENTO";
            ddlEstatusSeguimiento.DataTextField = "ESTATUS_SEGUIMIENTO";
            ddlEstatusSeguimiento.DataBind();
        }


        protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                buscarSubEstatus(ddlEstatus.SelectedValue);
                buscarEstatusSeguimiento(ddlEstatus.SelectedValue, ddlSubEstatus.SelectedValue);

                txtFecAgendamiento.Visible = false;
                lblFechaAgendamiento.Visible = false;

                lblHora.Visible = false;
                txtHora.Visible = false;

                lblFechaVisita.Visible = false;
                txtFechaVisita.Visible = false;
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlSubEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                buscarEstatusSeguimiento(ddlEstatus.SelectedValue, ddlSubEstatus.SelectedValue);

                txtFecAgendamiento.Visible = false;
                lblFechaAgendamiento.Visible = false;

                lblHora.Visible = false;
                txtHora.Visible = false;

                lblFechaVisita.Visible = false;
                txtFechaVisita.Visible = false;
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlEstatusSeguimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string idEmpresa = Session["idEmpresa"].ToString();
                
                DataTable dt = new DataTable();

                dt = dal.getBuscarAgendamiento(ddlEstatus.SelectedValue, ddlSubEstatus.SelectedValue, ddlEstatusSeguimiento.SelectedValue, idEmpresa).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    if (item["AGENDAMIENTO"].ToString() == "1")
                    {
                        txtFecAgendamiento.Visible = true;
                        lblFechaAgendamiento.Visible = true;

                        lblHora.Visible = true;
                        txtHora.Visible = true;
                    }
                    else
                    {
                        lblFechaAgendamiento.Visible = false;
                        txtFecAgendamiento.Visible = false;

                        lblHora.Visible = false;
                        txtHora.Visible = false;
                    }

                    if (item["VISITA"].ToString() == "1")
                    {
                        lblFechaVisita.Visible = true;
                        txtFechaVisita.Visible = true;
                    }
                    else
                    {
                        lblFechaVisita.Visible = false;
                        txtFechaVisita.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }




        protected void ddlEstatus_DataBound(object sender, EventArgs e)
        {
            ddlEstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void ddlSubEstatus_DataBound(object sender, EventArgs e)
        {
            ddlSubEstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void ddlEstatusSeguimiento_DataBound(object sender, EventArgs e)
        {
            ddlEstatusSeguimiento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }
        

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                

                string idEmpresa = this.Session["idEmpresa"].ToString();
                if (this.hfVieneSeguimiento.Value == "1")
                {
                    DataTable dataTable = new DataTable();
                    DataTable table = this.dal.getBuscarAgendamiento(this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue, this.ddlEstatusSeguimiento.SelectedValue, idEmpresa).Tables[0];
                    string str1 = "";
                    foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                        str1 = row["EXIGE_COT"].ToString();
                    if (str1 == "1")
                    {
                        if (this.grvGestion.Rows.Count == 0)
                        {
                            this.lblInformacion.Text = "No existen cotizaciones para gestionar";
                            this.mdlInformacion.Show();
                            return;
                        }
                        if (this.grvGestion.Rows.Count != 0)
                        {
                            string str2 = "";
                            foreach (GridViewRow row in this.grvGestion.Rows)
                            {
                                if (((CheckBox)this.grvGestion.Rows[row.RowIndex].FindControl("chkSeleccionar")).Checked)
                                    str2 = "1";
                            }
                            if (str2 == "")
                            {
                                this.lblInformacion.Text = "Debe seleccionar alguna cotización";
                                this.mdlInformacion.Show();
                                return;
                            }
                        }
                    }
                }
                if (this.ddlEstatus.SelectedValue == "0" || this.ddlSubEstatus.SelectedValue == "0" || this.ddlEstatusSeguimiento.SelectedValue == "0")
                {
                    this.lblInformacion.Text = "Debe seleccionar algun estatus";
                    this.mdlInformacion.Show();
                }
                else
                {
                    this.Session["variableUsuario"].ToString();
                    string idUsuario = this.Session["variableIdUsuario"].ToString();
                    string text1 = this.lblRutGestiones.Text;
                    string text2 = this.txtHora.Text;
                    string text3 = this.txtFecAgendamiento.Text;
                    if (text3 != string.Empty && Convert.ToDateTime(text3 + " " + text2) < DateTime.Now)
                    {
                        this.lblInformacion.Text = "Favor seleccionar una fecha superior a la de hoy";
                        this.mdlInformacion.Show();
                    }
                    else
                    {
                        string idGestion = this.dal.setIngresarGestion(this.Session["IdCliente"].ToString(), idEmpresa, this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue, idUsuario, this.txtFecAgendamiento.Text, text2, this.txtObservacion.Text, "", this.ddlEstatusSeguimiento.SelectedValue, this.ddlMotivoNoCompra.SelectedValue, this.txtFechaVisita.Text, this.lblIdCampana.Text);
                        if (this.grvGestion.Rows.Count != 0)
                        {
                            foreach (GridViewRow row1 in this.grvGestion.Rows)
                            {
                                if (((CheckBox)this.grvGestion.Rows[row1.RowIndex].FindControl("chkSeleccionar")).Checked)
                                {
                                    Label control = (Label)this.grvGestion.Rows[row1.RowIndex].FindControl("lblCotizacion");
                                    this.dal.setIngresarGestionCotizacion(idGestion, control.Text);
                                    DataTable dataTable = new DataTable();
                                    foreach (DataRow row2 in (InternalDataCollectionBase)this.dal.getBuscarAgendamiento(this.ddlEstatus.SelectedValue, this.ddlSubEstatus.SelectedValue, this.ddlEstatusSeguimiento.SelectedValue, idEmpresa).Tables[0].Rows)
                                    {
                                        if (row2["TERMINAL"].ToString() == "True")
                                            this.dal.setEditarEstadoCotizacion(control.Text, "6", (string)null, (string)null);
                                    }
                                }
                            }
                        }
                        this.buscarGestionesCRM();
                        this.limpiar();
                        this.TabContainer1.ActiveTab = this.tpCliente;
                        this.lblInformacion.Text = "<strong>Correcto!</strong> La gestión se ingresó correctamente.";
                        this.mdlInformacion.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void limpiar()
        {
            txtFecAgendamiento.Text = string.Empty;
            txtObservacion.Text = string.Empty;
            ddlEstatus.SelectedValue = "0";
            ddlEstatusSeguimiento.SelectedValue = "0";
            ddlSubEstatus.SelectedValue = "0";
            ddlMotivoNoCompra.Items.Clear();
            txtHora.Text = string.Empty;
        }


        protected void btnGrabarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                string rutCliente = this.Session["IdCliente"].ToString();
                this.dal.setIngresarContacto(this.txtNombreContacto.Text, rutCliente, this.txtEmail1.Text, this.txtEmail2.Text, this.txtCelular.Text, this.txtTelefono1.Text, this.txtTelefono2.Text, this.ddlCargo.SelectedValue);
                this.buscarContactos(rutCliente);
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

                string rutCliente = this.Session["IdCliente"].ToString();
                this.dal.setEditarContacto(this.hfIdContacto.Value, this.txtNombreContacto.Text, rutCliente, this.txtEmail1.Text, this.txtEmail2.Text, this.txtCelular.Text, this.txtTelefono1.Text, this.txtTelefono2.Text, this.ddlCargo.SelectedValue);
                this.buscarContactos(rutCliente);
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
       
        }


        protected void ibtnAgregarContacto_Click(object sender, EventArgs e)
        {
            try
            {

               

                if (this.Session["IdCliente"] == null)
                {
                    this.lblInformacion.Text = "No puede ingresar un contacto si no hay un cliente seleccionado";
                    this.mdlInformacion.Show();
                }
                else
                {
                    this.btnGrabarContacto.Visible = true;
                    this.btnModificarContacto.Visible = false;
                    this.limpiarContacto();
                    this.buscarContactoCargo();
                    this.lblAgregarContacto.Text = "Nuevo Contacto";
                    this.mdlAgregarContacto.Show();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void limpiarContacto() 
        {
           
            txtNombreContacto.Text = string.Empty;
            txtEmail1.Text = string.Empty;
            txtEmail2.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtTelefono1.Text = string.Empty;
            txtTelefono2.Text = string.Empty;
            ddlCargo.ClearSelection();
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
                 
                    limpiar(control.Controls);
            }
        }
        
        protected void btnBuscarCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                string buscar = txtBuscarCotizacion.Text;
                
                if (buscar.Length < 1)
                {
                    lblInformacion.Text = "Texto de busqueda debe tener al menos 3 caracteres";
                    mdlInformacion.Show();

                    return;
                }

                grvEmpresas.DataSource = dal.getBuscarClientePorCotizacion(txtBuscarCotizacion.Text.Trim()).Tables[0];
                grvEmpresas.DataBind();

                txtRutoRazonSocial.Text = string.Empty;
                mdlEmpresas.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Session["rutCliente"] != null)
                //{
                //    Session["rutCliente"] = null;
                //}
                //Response.Redirect("Default.aspx");
                if (this.Session["rutCliente"] != null)
                    this.Session["rutCliente"] = (object)null;
                if (this.Session["IdCliente"] != null)
                    this.Session["IdCliente"] = (object)null;
                this.Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnEditarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                //btnAgregar.Visible = false;
                //btnModificar.Visible = true;

                //limpiar(this.Controls);
                //estado();

                //string rut = lblRut.Text;
                ////string id = hfid
                //DataTable dt = new DataTable();
                //dt = dal.getBuscarClientePorRut(rut).Tables[0];
                ////dt = dal.getBuscarClientePorId(id).Tables[0];
                //foreach (DataRow item in dt.Rows)
                //{
                //    string idCliente = item["ID_CLIENTE"].ToString();

                //    string razonSocial = item["RAZON_SOCIAL"].ToString();
                //    string rutCli = item["RUT_CLIENTE"].ToString();
                //    txtRut.Enabled = true;
                //    string direccion = item["DIRECCION"].ToString();
                //    string ciudad = item["CIUDAD"].ToString();
                //    string email = item["EMAIL"].ToString();
                //    string montoCredito = item["MONTO_CREDITO"].ToString();
                //    string zona = item["ZONA"].ToString();
                //    string giro = item["GIRO"].ToString();
                //    string nombreCorto = item["NOMBRE_CORTO"].ToString();
                //    string comuna = item["COMUNA"].ToString();
                //    string telefono = item["TELEFONO"].ToString();
                //    string info = item["INFO"].ToString();
                //    string clasificacion = item["CLASIFICACION"].ToString();
                //    string condicionVenta = item["CONDICION_DE_VENTA"].ToString();
                //    string idEstado = item["ID_ESTADO_CLIENTE"].ToString();

                //    string url = item["URL"].ToString();
                //    string idCampana = item["ID_CAMPANA"].ToString();
                //    string observacion = item["OBSERVACION"].ToString();
                //    string idActividadComercial = item["ID_ACTIVIDAD_COMERCIAL"].ToString();
                //    //string ciudad = item["COMUNA"].ToString();

                //    hfIdCliente.Value = idCliente;
                //    txtRazonSocial.Text = razonSocial;
                //    txtRut.Text = rutCli;
                //    txtDireccion.Text = direccion;
                //    txtCiudad.Text = ciudad;
                //    txtEmail.Text = email;
                //    txtMontoCredito.Text = montoCredito;
                //    txtZona.Text = zona;
                //    txtGiro.Text = giro;
                //    txtNombreCorto.Text = nombreCorto;
                //    txtZona.Text = zona;
                //    txtGiro.Text = giro;
                //    txtNombreCorto.Text = nombreCorto;
                //    txtComuna.Text = comuna;
                //    txtTelefono.Text = telefono;
                //    txtInfo.Text = info;
                //    txtClasificacion.Text = clasificacion;

                //    if (condicionVenta != string.Empty)
                //    {
                //        ddlCondicionDeVenta.SelectedValue = condicionVenta;
                //    }

                //    if (idEstado != string.Empty)
                //    {
                //        ddlEstado.SelectedValue = idEstado;
                //    }

                //    txtUrl.Text = url;
                //    txtObservacionCliente.Text = observacion;

                //    if (idActividadComercial != string.Empty)
                //    {
                //        ddlActividadComercial.SelectedValue = idActividadComercial;
                //    }

                //    if (idCampana != string.Empty)
                //    {
                //        ddlCampana.SelectedValue = idCampana;
                //    }
                //}

                //lblAgregarUsuario.Text = "Editar Cliente";
                //mdlAgregarCliente.Show();

                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = true;
                this.limpiar(this.Controls);
                this.estado();
                string text = this.lblRut.Text;
                string idCliente = this.Session["IdCliente"].ToString();
                DataTable dataTable = new DataTable();
                foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarClientePorId(idCliente).Tables[0].Rows)
                {
                    string str1 = row["ID_CLIENTE"].ToString();
                    string str2 = row["RAZON_SOCIAL"].ToString();
                    string str3 = row["RUT_CLIENTE"].ToString();
                    this.txtRut.Enabled = true;
                    string str4 = row["DIRECCION"].ToString();
                    string str5 = row["CIUDAD"].ToString();
                    string str6 = row["EMAIL"].ToString();
                    string str7 = row["MONTO_CREDITO"].ToString();
                    string str8 = row["ZONA"].ToString();
                    string str9 = row["GIRO"].ToString();
                    string str10 = row["NOMBRE_CORTO"].ToString();
                    string str11 = row["COMUNA"].ToString();
                    string str12 = row["TELEFONO"].ToString();
                    string str13 = row["INFO"].ToString();
                    string str14 = row["CLASIFICACION"].ToString();
                    string str15 = row["CONDICION_DE_VENTA"].ToString();
                    string str16 = row["ID_ESTADO_CLIENTE"].ToString();
                    string str17 = row["URL"].ToString();
                    string str18 = row["ID_CAMPANA"].ToString();
                    string str19 = row["OBSERVACION"].ToString();
                    string str20 = row["ID_ACTIVIDAD_COMERCIAL"].ToString();
                    this.hfIdCliente.Value = str1;
                    this.txtRazonSocial.Text = str2;
                    this.txtRut.Text = str3;
                    this.txtDireccion.Text = str4;
                    this.txtCiudad.Text = str5;
                    this.txtEmail.Text = str6;
                    this.txtMontoCredito.Text = str7;
                    this.txtZona.Text = str8;
                    this.txtGiro.Text = str9;
                    this.txtNombreCorto.Text = str10;
                    this.txtZona.Text = str8;
                    this.txtGiro.Text = str9;
                    this.txtNombreCorto.Text = str10;
                    this.txtComuna.Text = str11;
                    this.txtTelefono.Text = str12;
                    this.txtInfo.Text = str13;
                    this.txtClasificacion.Text = str14;
                    if (str15 != string.Empty)
                        this.ddlCondicionDeVenta.SelectedValue = str15;
                    if (str16 != string.Empty)
                        this.ddlEstado.SelectedValue = str16;
                    this.txtUrl.Text = str17;
                    this.txtObservacionCliente.Text = str19;
                    if (str20 != string.Empty)
                        this.ddlActividadComercial.SelectedValue = str20;
                    if (str18 != string.Empty)
                        this.ddlCampana.SelectedValue = str18;
                }
                this.lblAgregarUsuario.Text = "Editar Cliente";
                this.mdlAgregarCliente.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                //btnAgregar.Visible = true;
                //btnModificar.Visible = false;

                //txtRut.Visible = true;

                //limpiar(this.Controls);
                //estado();
                //lblAgregarUsuario.Text = "Nuevo Cliente";
                //mdlAgregarCliente.Show();
                this.btnAgregar.Visible = true;
                this.btnModificar.Visible = false;
                this.txtRut.Visible = true;
                this.limpiar(this.Controls);
                this.estado();
                this.ddlEstado.SelectedValue = "2";
                this.lblAgregarUsuario.Text = "Nuevo Cliente";
                this.mdlAgregarCliente.Show();
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
                //if (txtMontoCredito.Text == string.Empty)
                //{
                //    txtMontoCredito.Text = null;
                //}
                //string idUsuario = Session["variableIdUsuario"].ToString();
                //string id=hfIdCliente.Value;
                //dal.setEditarCliente(id, txtRut.Text, txtRazonSocial.Text, txtNombreCorto.Text, 
                //    txtTelefono.Text, txtDireccion.Text, txtComuna.Text, txtCiudad.Text, 
                //    txtEmail.Text, txtInfo.Text, txtMontoCredito.Text, txtClasificacion.Text, 
                //    txtZona.Text, ddlEstado.SelectedValue, idUsuario,txtGiro.Text,
                //    ddlCondicionDeVenta.SelectedValue,txtUrl.Text,ddlActividadComercial.SelectedValue,
                //    txtObservacionCliente.Text,ddlCampana.SelectedValue,ddlActivo.SelectedValue,txtRutClientePadre.Text);
                ////dal.setEditarCliente(hfIdUsuario.Value,
                ////buscar();


                //buscarClientePorRut(txtRut.Text);



                if (this.txtMontoCredito.Text == string.Empty)
                    this.txtMontoCredito.Text = (string)null;
                string idUsuarioIngreso = this.Session["variableIdUsuario"].ToString();
                string idCliente = this.hfIdCliente.Value;
                this.dal.setEditarCliente(idCliente, this.txtRut.Text, this.txtRazonSocial.Text, this.txtNombreCorto.Text, this.txtTelefono.Text, this.txtDireccion.Text, this.txtComuna.Text, this.txtCiudad.Text, this.txtEmail.Text, this.txtInfo.Text, this.txtMontoCredito.Text, this.txtClasificacion.Text, this.txtZona.Text, this.ddlEstado.SelectedValue, idUsuarioIngreso, this.txtGiro.Text, this.ddlCondicionDeVenta.SelectedValue, this.txtUrl.Text, this.ddlActividadComercial.SelectedValue, this.txtObservacionCliente.Text, this.ddlCampana.SelectedValue, this.ddlActivo.SelectedValue, this.txtRutClientePadre.Text);
                this.buscarClientePorRut(idCliente);
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
                ////if (txtRut.Text == string.Empty)
                ////{
                ////    lblInformacion.Text = "El campo Rut es obligatorio.";
                ////    mdlInformacion.Show();
                ////    return;
                ////}

                //if (txtRut.Text != string.Empty)
                //{
                //    if (comunes.validarRut(txtRut.Text) == false)
                //    {
                //        lblInformacion.Text = "El rut no es valido.";
                //        mdlInformacion.Show();
                //        return;
                //    }
                //}

                //if (txtMontoCredito.Text == string.Empty)
                //{
                //   txtMontoCredito.Text = null;
                //}
                //string idUsuario = Session["variableIdUsuario"].ToString();
                //dal.setIngresarCliente(txtRut.Text, txtRazonSocial.Text, txtNombreCorto.Text, 
                //    txtTelefono.Text, txtDireccion.Text, txtComuna.Text, txtCiudad.Text, 
                //    txtEmail.Text, txtInfo.Text, txtMontoCredito.Text, txtClasificacion.Text, 
                //    txtZona.Text, ddlEstado.SelectedValue, idUsuario, txtGiro.Text, 
                //    ddlCondicionDeVenta.SelectedValue,txtUrl.Text,ddlActividadComercial.SelectedValue,
                //    txtObservacionCliente.Text,ddlCampana.SelectedValue,ddlActivo.SelectedValue,txtRutClientePadre.Text,null);
                //dal.setIngresarContacto(txtRazonSocial.Text, txtRut.Text, txtEmail.Text, txtEmail1.Text, txtCelular.Text, txtTelefono.Text, txtTelefono1.Text, ddlCargo.SelectedValue);

                ////In.setIngresarCliente(txtRazonSocial.Text, txtNombreCorto.Text, txtRut.Text, txtDireccion.Text, ddlComuna.SelectedValue, txtCiudad.Text, txtFono1.Text, txtFono2.Text, txtRepLegal.Text, txtRepLegalRut.Text, txtNombreContacto.Text, txtEmailContacto.Text, ddlActivo.SelectedValue, "", txtCaducidad.Text, txtPlazoBarrido.Text);
                ////buscar();

                //buscarClientePorRut(txtRut.Text);

                if (this.txtRut.Text != string.Empty && !this.comunes.validarRut(this.txtRut.Text))
                {
                    this.lblInformacion.Text = "El rut no es valido.";
                    this.mdlInformacion.Show();
                }
                else
                {
                    if (this.txtMontoCredito.Text == string.Empty)
                        this.txtMontoCredito.Text = (string)null;
                    string idUsuarioIngreso = this.Session["variableIdUsuario"].ToString();
                    if (this.txtEmail.Text == string.Empty)
                    {
                        this.lblInformacion.Text = "El email es obligatorio.";
                        this.mdlInformacion.Show();
                    }
                    else
                    {
                        DataTable dataTable = new DataTable();
                        if (this.dal.getBuscarEmpresaPorEmailSinLike(this.txtEmail.Text).Tables[0].Rows.Count == 0)
                        {
                            
                            string str = this.dal.setIngresarCliente(this.txtRut.Text, this.txtRazonSocial.Text, this.txtNombreCorto.Text, this.txtTelefono.Text, this.txtDireccion.Text, this.txtComuna.Text, this.txtCiudad.Text, this.txtEmail.Text, this.txtInfo.Text, this.txtMontoCredito.Text, this.txtClasificacion.Text, this.txtZona.Text, this.ddlEstado.SelectedValue, idUsuarioIngreso, this.txtGiro.Text, this.ddlCondicionDeVenta.SelectedValue, this.txtUrl.Text, this.ddlActividadComercial.SelectedValue, this.txtObservacionCliente.Text, this.ddlCampana.SelectedValue, this.ddlActivo.SelectedValue, this.txtRutClientePadre.Text, (string)null);
                            this.dal.setIngresarContacto(this.txtRazonSocial.Text, str, this.txtEmail.Text, this.txtEmail1.Text, this.txtCelular.Text, this.txtTelefono.Text, this.txtTelefono1.Text, this.ddlCargo.SelectedValue);
                            this.buscarClientePorRut(str);
                        }
                        else
                        {
                            this.lblInformacion.Text = "El email del cliente ya existe en la base de datos.";
                            this.mdlInformacion.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void estado()
        {
            ddlEstado.DataSource = dal.getBuscarEstadoCliente(null, "");
            ddlEstado.DataValueField = "ID_ESTADO_CLIENTE";
            ddlEstado.DataTextField = "NOM_ESTADO_CLIENTE";
            ddlEstado.DataBind();
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


        private void buscarCliente(string idCliente)
        {
            if (idCliente == null)
                return;
            DataTable dataTable1 = new DataTable();
            IEnumerator enumerator = this.dal.getBuscarClientePorId(idCliente).Tables[0].Rows.GetEnumerator();

            //dal.getBuscarClientePorId(idCliente)
            try
            {
                if (!enumerator.MoveNext())
                    return;
                DataRow current = (DataRow)enumerator.Current;
                this.lblUsuarioAsig.Text = current["USUARIO"].ToString();
                string str = current["RUT_PADRE"].ToString();
                if (str.Trim() != string.Empty)
                {
                    this.trRutClientePadreCliente.Visible = true;
                    this.trRutClientePadreGestion.Visible = true;
                    this.lblRutClientePadreCliente.Text = str;
                    this.lblRutClientePadreGestion.Text = str;
                    this.lblRazonSocialPadreCliente.Text = current["RAZON_PADRE"].ToString();
                    this.lblRazonSocialPadreGestion.Text = current["RAZON_PADRE"].ToString();
                }
                else
                {
                    this.trRutClientePadreCliente.Visible = false;
                    this.trRutClientePadreGestion.Visible = false;
                    DataTable dataTable2 = new DataTable();
                    if (this.dal.getBuscarClientesAsociados(this.lblRut.Text).Tables[0].Rows.Count == 0)
                        this.btnVerClientesAsociados.Visible = false;
                    else
                        this.btnVerClientesAsociados.Visible = true;
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        //void buscarCliente(string rut)
        //{
        //    if (rut != null)
        //    {
        //        DataTable dtCliente = new DataTable();
        //        //dtCliente = dal.getBuscarCliente(rut, rut,"1").Tables[0];
        //        dtCliente = dal.getBuscarClientePorRut(rut).Tables[0];
        //        foreach (DataRow item in dtCliente.Rows)
        //        {
        //            //lblRutCotizacion.Text = item["RUT_CLIENTE"].ToString();
        //            //lblCliente.Text = item["RAZON_SOCIAL"].ToString();
        //            //lblCiudad.Text = item["CIUDAD"].ToString();
        //            //lblComunaCotizacin.Text = item["COMUNA"].ToString();
        //            //lblDireccionCotizacion.Text = item["DIRECCION"].ToString();
        //            //lblTelefono.Text = item["TELEFONO"].ToString();
        //            lblUsuarioAsig.Text = item["USUARIO"].ToString();

        //            string rutPadre = item["RUT_PADRE"].ToString();
        //            if (rutPadre.Trim() != string.Empty)
        //            {
        //                trRutClientePadreCliente.Visible = true;
        //                //trRutClientePadreCotizacion.Visible = true;
        //                trRutClientePadreGestion.Visible = true;

        //                lblRutClientePadreCliente.Text = rutPadre;
        //                //lblRutClientePadreCotizacion.Text = rutPadre;
        //                lblRutClientePadreGestion.Text = rutPadre;

        //                lblRazonSocialPadreCliente.Text = item["RAZON_PADRE"].ToString();
        //                //lblRazonSocialPadreCotizacion.Text = item["RAZON_PADRE"].ToString();
        //                lblRazonSocialPadreGestion.Text = item["RAZON_PADRE"].ToString();
        //            }
        //            else
        //            {
        //                trRutClientePadreCliente.Visible = false;
        //                //trRutClientePadreCotizacion.Visible = false;
        //                trRutClientePadreGestion.Visible = false;

        //                DataTable dtClientesAsociados = new DataTable();
        //                dtClientesAsociados = dal.getBuscarClientesAsociados(lblRut.Text).Tables[0];
        //                if (dtClientesAsociados.Rows.Count == 0)
        //                {
        //                    btnVerClientesAsociados.Visible = false;
        //                }
        //                else
        //                {
        //                    btnVerClientesAsociados.Visible = true;
        //                }
        //            }

        //            break;
        //        }
        //    }
        //}

        protected void imgAgregarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                //if (lblRutCotizacion.Text == string.Empty)
                //{
                //    lblInformacion.Text = "No hay un cliente seleccionado, favor seleccionar un cliente";
                //    mdlInformacion.Show();
                //    return;
                //}

                buscarContactosCotizacion(hfRutClientePost.Value);
                mdlContactos.Show();
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
        
        
        

        public double calcularIva(double monto)
        {
            double montoTotal = 0;
            double porcentaje = 19;

            montoTotal = ((porcentaje / 100) * monto);

            return montoTotal;
        }

        public int calcularPorcentaje(double porcentaje, double monto)
        {
            int montoTotal = 0;

            montoTotal = Convert.ToInt32((porcentaje / 100) * monto);

            return montoTotal;
        }
        

        protected void btnNuevoContacto_Click(object sender, EventArgs e)
        {
            try
            {
                //btnGrabarContactoCotizacion.Visible = true;
                //btnModificarContactoCotizacion.Visible = false;
                
                lblAgregarContactoCotizacion.Text = "Nuevo Contacto";
                limpiar(this.Controls);
                buscarContactoCargo();
                mdlAgregarContactoCotizacion.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnGrabarContactoCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                string rutCliente = hfRutCliente.Value;

                dal.setIngresarContacto(txtNombreContactoCotizacion.Text, rutCliente, txtEmail1Cotizacion.Text, txtEmail2Cotizacion.Text, txtCelularCotizacion.Text, txtTelefono1Cotizacion.Text, txtTelefono2Cotizacion.Text,ddlCargoCotizacion.SelectedValue);
                buscarContactos(rutCliente);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnModificarContactoCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                
                dal.setEditarContacto(hfIdContacto.Value, txtNombreContactoCotizacion.Text, hfRutCliente.Value, txtEmail1Cotizacion.Text, txtEmail2Cotizacion.Text, txtCelularCotizacion.Text, txtTelefono1Cotizacion.Text, txtTelefono2Cotizacion.Text,ddlCargo.SelectedValue);
                buscarContactos(hfRutCliente.Value);
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
                
                //lblContacto.Text = _lblNombreContacto.Text;
                //lblIdContacto.Text = _lblIdContacto.Text;
                //lblEmail.Text = _lblEmail1.Text;
                //lblEmail2.Text = _lblEmail2.Text;
                //lblCelular.Text = _lblCelular.Text;
                //lblTelefonoContacto.Text = _lblTelefono1.Text;
                //lblTelefonoContacto2.Text = _lblTelefono2.Text;

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
        
        protected void imgPdf_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaPdf = (Label)grvCotizacionesCRM.Rows[row.RowIndex].FindControl("lblRutaPdf");

                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdf.Text + "','_blank');", true);
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

            ddlCargoCotizacion.DataSource = dal.getBuscarContactoCargo(null, null, idEmpresa);
            ddlCargoCotizacion.DataValueField = "ID_CONTACTO_CARGO";
            ddlCargoCotizacion.DataTextField = "CONTACTO_CARGO";
            ddlCargoCotizacion.DataBind();
        }

        protected void ibtnGenerarNotaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                //ImageButton lbtn = sender as ImageButton;
                //GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                //Label _lblIdCotizacion = (Label)grvCotizacionesCRM.Rows[row.RowIndex].FindControl("lblIdCotizacion");

                //ddlDireccionNotaVenta.ClearSelection();
                //ddlDireccionFacturacionNotaVenta.ClearSelection();

                //ddlDireccionNotaVenta.DataSource = dal.getBuscarClienteDireccion(lblRut.Text);
                //ddlDireccionNotaVenta.DataTextField = "DIRECCION";
                //ddlDireccionNotaVenta.DataValueField = "ID_CLIENTE_DIRECCION";
                //ddlDireccionNotaVenta.DataBind();

                //ddlDireccionFacturacionNotaVenta.DataSource = dal.getBuscarClienteDireccion(lblRut.Text);
                //ddlDireccionFacturacionNotaVenta.DataTextField = "DIRECCION";
                //ddlDireccionFacturacionNotaVenta.DataValueField = "ID_CLIENTE_DIRECCION";
                //ddlDireccionFacturacionNotaVenta.DataBind();

                //hfIdCotizacionSeleccionDireccion.Value = _lblIdCotizacion.Text;
                //mdlSeleccionarTipoDireccion.Show();

                if (string.IsNullOrEmpty(lblRut.Text.Trim()))
                {
                    lblInformacion.Text = "Para generar una nota de venta, se necesita tener los siguientes datos del cliente; RUT, RAZON SOCIAL, DIRECCION, CIUDAD, GIRO, COMUNA, ACT COMERCIAL.";
                    mdlInformacion.Show();
                    return;
                }

                if (string.IsNullOrEmpty(lblRazonSocial.Text.Trim()))
                {
                    lblInformacion.Text = "Para generar una nota de venta, se necesita tener los siguientes datos del cliente; RUT, RAZON SOCIAL, DIRECCION, CIUDAD, GIRO, COMUNA, ACT COMERCIAL.";
                    mdlInformacion.Show();
                    return;
                }

                //if (string.IsNullOrEmpty(lblDireccion.Text.Trim()))
                //{
                //    lblInformacion.Text = "Para generar una nota de venta, se necesita tener los siguientes datos del cliente; RUT, RAZON SOCIAL, DIRECCION, CIUDAD, GIRO, COMUNA, ACT COMERCIAL.";
                //    mdlInformacion.Show();
                //    return;
                //}

                if (string.IsNullOrEmpty(lblGiro.Text.Trim()))
                {
                    lblInformacion.Text = "Para generar una nota de venta, se necesita tener los siguientes datos del cliente; RUT, RAZON SOCIAL, DIRECCION, CIUDAD, GIRO, COMUNA, ACT COMERCIAL.";
                    mdlInformacion.Show();
                    return;
                }

                Label control = (Label)this.grvCotizacionesCRM.Rows[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].FindControl("lblIdCotizacion");
                string rutCliente = this.Session["IdCliente"].ToString();
                this.ddlDireccionNotaVenta.ClearSelection();
                this.ddlDireccionFacturacionNotaVenta.ClearSelection();
                this.ddlDireccionNotaVenta.DataSource = (object)this.dal.getBuscarClienteDireccion(rutCliente);
                this.ddlDireccionNotaVenta.DataTextField = "DIRECCION";
                this.ddlDireccionNotaVenta.DataValueField = "ID_CLIENTE_DIRECCION";
                this.ddlDireccionNotaVenta.DataBind();
                this.ddlDireccionFacturacionNotaVenta.DataSource = (object)this.dal.getBuscarClienteDireccion(rutCliente);
                this.ddlDireccionFacturacionNotaVenta.DataTextField = "DIRECCION";
                this.ddlDireccionFacturacionNotaVenta.DataValueField = "ID_CLIENTE_DIRECCION";
                this.ddlDireccionFacturacionNotaVenta.DataBind();
                this.hfIdCotizacionSeleccionDireccion.Value = control.Text;
                this.mdlSeleccionarTipoDireccion.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnSeleccionarDireccionGenerarNotaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                //string idUsuario = Session["variableIdUsuario"].ToString();

                //DataTable dt = new DataTable();
                //dt = dal.getBuscarUsuario(null, idUsuario).Tables[0];
                //string nombre = "";
                //foreach (DataRow item in dt.Rows)
                //{
                //    nombre = item["NOMBRE"].ToString();
                //}

                //string idCotizacion = hfIdCotizacionSeleccionDireccion.Value;
                //string direccionFacturacion = ddlDireccionFacturacionNotaVenta.SelectedValue;
                //string direccionDespacho = ddlDireccionNotaVenta.SelectedValue;
                //string referencia = txtReferencia.Text;
                //string numeroOrdenCompra = txtOrdenCompra.Text;
                //string rutaOC = string.Empty;
                //string rutaOC2 = string.Empty;

                //if (fuArchivoOC.HasFile)
                //{
                //    rutaOC = "ordenCompra/" + numeroOrdenCompra + "_" + fuArchivoOC.FileName;
                //    fuArchivoOC.SaveAs(Server.MapPath(rutaOC));
                //}
                //if (fuArchivoOC2.HasFile)
                //{
                //    rutaOC2 = "ordenCompra/" + fuArchivoOC2.FileName;
                //    fuArchivoOC2.SaveAs(Server.MapPath(rutaOC2));
                //}
                //string notaVenta = dal.setIngresarNotaVenta(idCotizacion, direccionDespacho, direccionFacturacion, referencia,txtOrdenCompra.Text,rutaOC, rutaOC2);


                //DataTable dtCotDetalle = new DataTable();
                //dtCotDetalle=dal.getBuscarCotizacionDetalle(idCotizacion).Tables[0];
                //foreach (DataRow item in dtCotDetalle.Rows)
                //{
                //    int idProducto = Convert.ToInt32(item["ID_PRODUCTO"]);
                //    int stock = Convert.ToInt32(item["CANTIDAD"]);
                //    dal.setEditarProductoDescontarStock(idProducto, stock);
                //}

                ////buscarNotaVenta();
                //string ruta = generarNotaVentaPdf(notaVenta, idCotizacion, nombre);
                //dal.setEditarRutaPdfNotaVenta(notaVenta, ruta);

                //dal.setEditarEstadoCotizacion(idCotizacion, "3", null,null);

                //buscarCotizaciones(lblRut.Text);
                //buscarNotaVenta();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + ruta + "','_blank');", true);

                string idUsuario = this.Session["variableIdUsuario"].ToString();
                string rut = this.Session["IdCliente"].ToString();
                DataTable dataTable1 = new DataTable();

                DataTable table = this.dal.getBuscarUsuario((string)null, idUsuario).Tables[0];

                string nombreEjecutivo = "";

                foreach (DataRow item in table.Rows)
                {
                    nombreEjecutivo = item["NOMBRE"].ToString();
                }
                
                string idCotizacion = this.hfIdCotizacionSeleccionDireccion.Value;
                string selectedValue1 = this.ddlDireccionFacturacionNotaVenta.SelectedValue;
                string selectedValue2 = this.ddlDireccionNotaVenta.SelectedValue;
                string text1 = this.txtReferencia.Text;
                string text2 = this.txtOrdenCompra.Text;

                string str1 = string.Empty;
                string str2 = string.Empty;
                if (fuArchivoOC.HasFile)
                {
                    str1 = "ordenCompra/" + text2 + "_" + fuArchivoOC.FileName;
                    this.fuArchivoOC.SaveAs(Server.MapPath(str1));
                }
                if (fuArchivoOC2.HasFile)
                {
                    str2 = "ordenCompra/" + fuArchivoOC2.FileName;
                    fuArchivoOC2.SaveAs(Server.MapPath(str2));
                }

                if (txtFechaEntrega.Text==string.Empty)
                {
                    
                }

                string idNotaVenta = this.dal.setIngresarNotaVenta(idCotizacion, selectedValue2, selectedValue1, text1, this.txtOrdenCompra.Text, str1, str2);
                //string idOrdenTrabajo = dal.setIngresarOrdenTrabajo(idNotaVenta, idCotizacion, selectedValue2, selectedValue1, text1, txtOrdenCompra.Text, str1, str2, txtFechaEntrega.Text);
                
                DataTable dataTable2 = new DataTable();
                foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarCotizacionDetalle(idCotizacion).Tables[0].Rows)
                {
                    dal.setEditarProductoDescontarStock(Convert.ToInt32(row["ID_PRODUCTO"]), Convert.ToInt32(row["CANTIDAD"]));
                }
     
                string rutaPdf = this.generarNotaVentaPdf(idNotaVenta, idCotizacion, nombreEjecutivo);
                //string rutaPdfOT = generarOrdenTrabajoPdf(idOrdenTrabajo, idNotaVenta, idCotizacion, nombreEjecutivo);

                this.dal.setEditarRutaPdfNotaVenta(idNotaVenta, rutaPdf);
                this.dal.setEditarEstadoCotizacion(idCotizacion, "3", null, null);
                buscarCotizaciones(rut);
                BuscarNotaVenta();
                BuscarOT();
                ScriptManager.RegisterStartupScript((Page)this, this.GetType(), this.UniqueID, "window.open('" + rutaPdf + "','_blank');", true);
                //ScriptManager.RegisterStartupScript((Page)this, this.GetType(), this.UniqueID, "window.open('" + rutaPdfOT + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgPdfNotaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaPdf = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblRutaPdf");
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdf.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnAgregarDireccionCliente_Click(object sender, EventArgs e)
        {
            try
            {

                //if (lblRut.Text == string.Empty)
                //{
                //    lblInformacion.Text = "No puede ingresar una direccion si no hay un cliente seleccionado";
                //    mdlInformacion.Show();
                //    return;
                //}

                //txtCalle.Text = string.Empty;
                //txtNumero.Text = string.Empty;
                //txtResto.Text = string.Empty;
                //txtComunaDireccion.Text = string.Empty;
                //ddlComunaDireccion.ClearSelection();

                //buscarComunaDireccion();

                //btnGrabarDireccion.Visible = true;
                //btnEditarDireccion.Visible = false;

                //lblAgregarDireccion.Text = "Agregar Dirección";

                //mdlAgregarDireccion.Show();

                this.txtCalle.Text = string.Empty;
                this.txtNumero.Text = string.Empty;
                this.txtResto.Text = string.Empty;
                this.txtComunaDireccion.Text = string.Empty;
                this.ddlComunaDireccion.ClearSelection();
                this.buscarComunaDireccion();
                this.btnGrabarDireccion.Visible = true;
                this.btnEditarDireccion.Visible = false;
                this.lblAgregarDireccion.Text = "Agregar Dirección";
                this.mdlAgregarDireccion.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        
        void BuscarNotaVenta() 
        {
            //if (lblRut.Text != string.Empty)
            //{
            //    grvNotaVenta.DataSource = dal.getBuscarNotaVentaPorCliente(lblRut.Text);
            //    grvNotaVenta.DataBind();
            //}
            if (this.Session["IdCliente"] == null)
                return;
            this.grvNotaVenta.DataSource = (object)this.dal.getBuscarNotaVentaPorCliente(this.Session["IdCliente"].ToString());
            this.grvNotaVenta.DataBind();
        }

        void BuscarOT()
        {
            if (Session["IdCliente"] != null)
            {
                string idCliente = Session["IdCliente"].ToString();
                grvOrdenDeTrabajo.DataSource = dal.getBuscarOrdenTrabajoPorCliente(idCliente);
                grvOrdenDeTrabajo.DataBind();
            }
        }


        public string generarNotaVentaPdf(string notaVenta, string idCotizacion, string nombreEjecutivo)
        {
            string str1 = DateTime.Today.ToString("dd-MM-yyyy");
            string str2 = "NotaVenta_" + notaVenta + ".pdf";
            BaseFont font1 = BaseFont.CreateFont("Helvetica", "Cp1252", false);
            iTextSharp.text.Font font2 = new iTextSharp.text.Font(font1, 7f, 0);
            iTextSharp.text.Font font3 = new iTextSharp.text.Font(font1, 9f, 1, BaseColor.RED);
            iTextSharp.text.Font font4 = new iTextSharp.text.Font(font1, 9f, 1);
            iTextSharp.text.Font font5 = new iTextSharp.text.Font(font1, 8f, 1);
            iTextSharp.text.Font font6 = new iTextSharp.text.Font(font1, 8f, 1);
            Document document = new Document(PageSize.A4, 25f, 25f, 30f, 30f);
            PdfWriter.GetInstance(document, (Stream)new FileStream(this.Server.MapPath("notaVenta/" + str2), FileMode.Create));
            document.Open();
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string path1 = "";
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarEmpresa((string)null, this.Session["idEmpresa"].ToString()).Tables[0].Rows)
            {
                str3 = row["NOMBRE_EMPRESA"].ToString();
                str4 = row["GIRO"].ToString();
                str5 = row["RUT"].ToString();
                str6 = row["TELEFONO"].ToString();
                path1 = row["IMAGEN"].ToString();
            }
            PdfPTable pdfPtable1 = new PdfPTable(3);
            PdfPTable table1 = new PdfPTable(1);
            PdfPCell cell1 = new PdfPCell((Phrase)new Paragraph(str3, font5));
            PdfPCell cell2 = new PdfPCell((Phrase)new Paragraph(str4, font5));
            PdfPCell cell3 = new PdfPCell((Phrase)new Paragraph(str5, font5));
            PdfPCell cell4 = new PdfPCell((Phrase)new Paragraph(str6, font5));
            table1.AddCell(cell1);
            table1.AddCell(cell2);
            table1.AddCell(cell3);
            table1.AddCell(cell4);
            foreach (Rectangle cell5 in table1.Rows[0].GetCells())
                cell5.Border = 0;
            foreach (Rectangle cell5 in table1.Rows[1].GetCells())
                cell5.Border = 0;
            foreach (Rectangle cell5 in table1.Rows[2].GetCells())
                cell5.Border = 0;
            foreach (Rectangle cell5 in table1.Rows[3].GetCells())
                cell5.Border = 0;
            pdfPtable1.AddCell(table1);
            PdfPTable table2 = new PdfPTable(1);
            if (path1 != string.Empty)
            {
                iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(this.Server.MapPath(path1));
                instance.ScaleToFit(150f, 150f);
                instance.Alignment = 0;
                table2.AddCell(instance);
            }
            else
            {
                iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(this.Server.MapPath("assets/img/logoEmpresa.jpg"));
                instance.ScaleToFit(120f, 120f);
                instance.Alignment = 0;
                table2.AddCell(instance);
            }
            foreach (Rectangle cell5 in table2.Rows[0].GetCells())
                cell5.Border = 0;
            pdfPtable1.AddCell(table2);
            PdfPTable table3 = new PdfPTable(2);
            PdfPCell cell6 = new PdfPCell((Phrase)new Paragraph("Nro. Nota Venta :", font5));
            PdfPCell cell7 = new PdfPCell((Phrase)new Paragraph("Estado :", font5));
            PdfPCell cell8 = new PdfPCell((Phrase)new Paragraph("Fecha :", font5));
            PdfPCell cell9 = new PdfPCell((Phrase)new Paragraph("Nº OC :", font5));
            PdfPCell cell10 = new PdfPCell((Phrase)new Paragraph("Nº Cot :", font5));
            table3.AddCell(cell6);
            table3.AddCell((Phrase)new Paragraph(notaVenta, font2));
            table3.AddCell(cell7);
            table3.AddCell((Phrase)new Paragraph("Vigente", font2));
            table3.AddCell(cell8);
            table3.AddCell((Phrase)new Paragraph(str1, font2));
            table3.AddCell(cell9);
            table3.AddCell((Phrase)new Paragraph(this.txtOrdenCompra.Text, font2));
            table3.AddCell(cell10);
            table3.AddCell((Phrase)new Paragraph(idCotizacion, font2));
            table3.DefaultCell.Border = 0;
            table3.HorizontalAlignment = 2;
            table3.WidthPercentage = 25f;
            foreach (Rectangle cell5 in table3.Rows[0].GetCells())
                cell5.Border = 0;
            foreach (Rectangle cell5 in table3.Rows[1].GetCells())
                cell5.Border = 0;
            foreach (Rectangle cell5 in table3.Rows[2].GetCells())
                cell5.Border = 0;
            foreach (Rectangle cell5 in table3.Rows[3].GetCells())
                cell5.Border = 0;
            foreach (Rectangle cell5 in table3.Rows[4].GetCells())
                cell5.Border = 0;
            pdfPtable1.AddCell(table3);
            pdfPtable1.DefaultCell.Border = 0;
            pdfPtable1.HorizontalAlignment = 1;
            pdfPtable1.WidthPercentage = 100f;
            foreach (Rectangle cell5 in pdfPtable1.Rows[0].GetCells())
                cell5.Border = 0;
            document.Add((IElement)pdfPtable1);
            Chunk chunk1 = new Chunk("Nota Venta", FontFactory.GetFont("ARIAL", 11f, 1));
            chunk1.SetUnderline(0.1f, -2f);
            document.Add((IElement)new Paragraph(chunk1)
            {
                Alignment = 1
            });
            document.Add((IElement)new Paragraph(" ", font2));
            string content1 = string.Empty;
            string content2 = string.Empty;
            DataTable dataTable1 = new DataTable();
            DataTable table4 = this.dal.getBuscarClientePorId(this.Session["IdCliente"].ToString()).Tables[0];
            foreach (DataRow row in (InternalDataCollectionBase)table4.Rows)
            {
                if (row["RUT_PADRE"].ToString() != string.Empty)
                {
                    content1 = "Datos Cliente Asociado";
                    content2 = "Datos Contacto Cliente Asociado";
                }
                else
                {
                    content1 = "Datos Cliente";
                    content2 = "Datos Contacto";
                }
            }
            Chunk chunk2 = new Chunk(content1, FontFactory.GetFont("ARIAL", 9f, 1));
            chunk2.SetUnderline(0.1f, -2f);
            document.Add((IElement)chunk2);
            PdfPTable pdfPtable2 = new PdfPTable(4);
            float[] relativeWidths1 = new float[4]
            {
        35f,
        95f,
        35f,
        55f
            };
            pdfPtable2.SetWidths(relativeWidths1);
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string empty1 = string.Empty;
            foreach (DataRow row in (InternalDataCollectionBase)table4.Rows)
            {
                str7 = row["COMUNA"].ToString();
                str8 = row["CIUDAD"].ToString();
                str9 = row["GIRO"].ToString();
                str10 = row["GLOSA"].ToString();
                empty1 = row["TELEFONO"].ToString();
            }
            pdfPtable2.AddCell((Phrase)new Paragraph("Nombre :", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(this.lblRazonSocial.Text, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Rut :", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(this.lblRut.Text, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Dirección :", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(this.lblDireccion.Text, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Vendedor:", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(nombreEjecutivo, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Comuna:", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(str7, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Ciudad:", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(str8, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Giro:", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(str9, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Cond de Venta:", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(str10, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Teléfono:", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(empty1, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph(" ", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable2.AddCell((Phrase)new Paragraph("Glosa:", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(this.txtGlosa.Text, font2));
            pdfPtable2.AddCell((Phrase)new Paragraph(" ", font5));
            pdfPtable2.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable2.HorizontalAlignment = 0;
            pdfPtable2.WidthPercentage = 100f;
            document.Add((IElement)pdfPtable2);
            document.Add((IElement)new Paragraph(" ", font2));
            Chunk chunk3 = new Chunk(content2, FontFactory.GetFont("ARIAL", 9f, 1));
            chunk3.SetUnderline(0.1f, -2f);
            document.Add((IElement)chunk3);
            PdfPTable pdfPtable3 = new PdfPTable(4);
            float[] relativeWidths2 = new float[4]
            {
                35f,
                95f,
                35f,
                95f
            };

            
            pdfPtable3.SetWidths(relativeWidths2);
            string idContacto = "";
            string str11 = "";
            string str12 = "";
            string str13 = "";
            string str14 = "";
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            IEnumerator enumerator = this.dal.getBuscarNotaVenta(notaVenta).Tables[0].Rows.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    DataRow current = (DataRow)enumerator.Current;
                    idContacto = current["ID_CONTACTO"].ToString();
                    empty2 = current["RUTA_ORDEN_DE_COMPRA"].ToString();
                    empty3 = current["RUTA_ORDEN_DE_COMPRA2"].ToString();
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarContactoPorId(idContacto).Tables[0].Rows)
            {
                str11 = row["NOM_CONTACTO"].ToString();
                str12 = row["TELEFONO1"].ToString();
                str13 = row["CELULAR"].ToString();
                str14 = row["EMAIL_1"].ToString();
            }
            pdfPtable3.AddCell((Phrase)new Paragraph("Contacto :", font5));
            pdfPtable3.AddCell((Phrase)new Paragraph(str11, font2));
            pdfPtable3.AddCell((Phrase)new Paragraph("Teléfono :", font5));
            pdfPtable3.AddCell((Phrase)new Paragraph(str12, font2));
            pdfPtable3.AddCell((Phrase)new Paragraph("Celular :", font5));
            pdfPtable3.AddCell((Phrase)new Paragraph(str13, font2));
            pdfPtable3.AddCell((Phrase)new Paragraph("Email :", font5));
            pdfPtable3.AddCell((Phrase)new Paragraph(str14, font2));
            pdfPtable3.HorizontalAlignment = 0;
            pdfPtable3.WidthPercentage = 100f;
            document.Add((IElement)pdfPtable3);
            document.Add((IElement)new Paragraph(" ", font2));
            PdfPTable pdfPtable4 = new PdfPTable(4);
            foreach (DataRow row in (InternalDataCollectionBase)table4.Rows)
            {
                string str15 = row["RUT_PADRE"].ToString();
                if (str15.Trim() != string.Empty)
                {
                    string str16 = row["RAZON_PADRE"].ToString();
                    row["DIRECCION_PADRE"].ToString();
                    string str17 = row["TELEFONO_PADRE"].ToString();
                    string str18 = row["GIRO_PADRE"].ToString();
                    document.Add((IElement)new Paragraph(" ", font2));
                    Chunk chunk4 = new Chunk("Datos Cliente Principal", FontFactory.GetFont("ARIAL", 9f, 1));
                    chunk4.SetUnderline(0.1f, -2f);
                    document.Add((IElement)chunk4);
                    float[] relativeWidths3 = new float[4]
                    {
            35f,
            95f,
            35f,
            55f
                    };
                    pdfPtable4.SetWidths(relativeWidths3);
                    pdfPtable4.AddCell((Phrase)new Paragraph("Rut :", font5));
                    pdfPtable4.AddCell((Phrase)new Paragraph(str15, font2));
                    pdfPtable4.AddCell((Phrase)new Paragraph("Nombre :", font5));
                    pdfPtable4.AddCell((Phrase)new Paragraph(str16, font2));
                    pdfPtable4.AddCell((Phrase)new Paragraph("Dirección :", font5));
                    pdfPtable4.AddCell((Phrase)new Paragraph(str16, font2));
                    pdfPtable4.AddCell((Phrase)new Paragraph("Teléfono:", font5));
                    pdfPtable4.AddCell((Phrase)new Paragraph(str17, font2));
                    pdfPtable4.AddCell((Phrase)new Paragraph("Giro :", font5));
                    pdfPtable4.AddCell((Phrase)new Paragraph(str18, font2));
                    pdfPtable4.AddCell((Phrase)new Paragraph(" ", font5));
                    pdfPtable4.AddCell((Phrase)new Paragraph(" ", font2));
                    pdfPtable4.HorizontalAlignment = 0;
                    pdfPtable4.WidthPercentage = 100f;
                }
            }
            document.Add((IElement)pdfPtable4);
            document.Add((IElement)new Paragraph(" ", font2));
            document.Add((IElement)new Paragraph(" ", font2));
            Chunk chunk5 = new Chunk("Detalle Venta", FontFactory.GetFont("ARIAL", 9f, 1));
            chunk5.SetUnderline(0.1f, -2f);
            document.Add((IElement)chunk5);
            document.Add((IElement)new Paragraph(" ", font2));
            PdfPTable pdfPtable5 = new PdfPTable(3);
            pdfPtable5.AddCell((Phrase)new Paragraph("Código", font5));
            pdfPtable5.AddCell((Phrase)new Paragraph("Producto", font5));
            pdfPtable5.AddCell((Phrase)new Paragraph("Cantidad", font5));
            float[] relativeWidths4 = new float[3]
            {
        35f,
        105f,
        25f
            };
            pdfPtable5.SetWidths(relativeWidths4);
            DataTable dataTable2 = new DataTable();
            foreach (DataRow row in (InternalDataCollectionBase)this.dal.getBuscarDetalleNotaVenta(notaVenta).Tables[0].Rows)
            {
                pdfPtable5.AddCell((Phrase)new Paragraph(row["CODIGO"].ToString(), font2));
                pdfPtable5.AddCell((Phrase)new Paragraph(row["NOM_PRODUCTO"].ToString().Replace("<b>", "").Replace("</b>", "").Replace("&nbsp", "."), font2));
                string str15 = row["CANTIDAD"].ToString().Replace("<b>", "").Replace("</b>", "").Replace("&nbsp", ".");
                (Convert.ToDouble(row["MONTO_NETO"]) / Convert.ToDouble(row["CANTIDAD"])).ToString("n0");
                string str16 = row["MONTO_NETO"].ToString().Replace("<b>", "").Replace("</b>", "").Trim();
                if (str16 != string.Empty)
                    Convert.ToInt32(Convert.ToDouble(str16)).ToString("n0");
                iTextSharp.text.Font font7 = font2;
                pdfPtable5.AddCell(new PdfPCell((Phrase)new Paragraph(str15, font7))
                {
                    HorizontalAlignment = 2
                });
            }
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            DataTable dataTable3 = new DataTable();
            DataTable table5 = this.dal.getBuscarNotaVenta(notaVenta).Tables[0];
            string str19 = "";
            foreach (DataRow row in (InternalDataCollectionBase)table5.Rows)
            {
                num1 = Convert.ToInt32(Convert.ToDecimal(row["MONTO_NETO"].ToString()));
                num2 = Convert.ToInt32(Convert.ToDecimal(row["MONTO_DESCUENTO"].ToString()));
                num3 = Convert.ToInt32(Convert.ToDecimal(row["MONTO_IVA"].ToString()));
                num4 = Convert.ToInt32(Convert.ToDecimal(row["MONTO_TOTAL"].ToString()));
                str19 = row["DIRECCION"].ToString();
            }
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            new PdfPCell((Phrase)new Paragraph(num1.ToString("n0"), font2)).HorizontalAlignment = 2;
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            new PdfPCell((Phrase)new Paragraph(num2.ToString("n0"), font2)).HorizontalAlignment = 2;
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            new PdfPCell((Phrase)new Paragraph(num3.ToString("n0"), font2)).HorizontalAlignment = 2;
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            pdfPtable5.AddCell((Phrase)new Paragraph(" ", font2));
            new PdfPCell((Phrase)new Paragraph(num4.ToString("n0"), font2)).HorizontalAlignment = 2;
            pdfPtable5.HorizontalAlignment = 0;
            pdfPtable5.WidthPercentage = 100f;
            foreach (PdfPCell cell5 in pdfPtable5.Rows[0].GetCells())
            {
                cell5.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell5.HorizontalAlignment = 1;
                cell5.Padding = 2f;
            }
            document.Add((IElement)pdfPtable5);
            document.Add((IElement)new Paragraph("Dirección de despacho: " + str19, font2));
            document.Add((IElement)new Paragraph(" ", font2));
            document.Add((IElement)new Paragraph(" ", font2));
            PdfPTable pdfPtable6 = new PdfPTable(1);
            pdfPtable6.WidthPercentage = 100f;
            string path2 = empty2;
            if (path2 == string.Empty)
            {
                pdfPtable6.AddCell((Phrase)new Paragraph("Sin Imagen", font2));
            }
            else
            {
                iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(this.Server.MapPath(path2));
                instance.ScaleToFit(120f, 120f);
                instance.Alignment = 0;
                pdfPtable6.AddCell(instance);
            }
            string path3 = empty3;
            if (path3 == string.Empty)
            {
                pdfPtable6.AddCell((Phrase)new Paragraph("Sin Imagen", font2));
            }
            else
            {
                iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(this.Server.MapPath(path3));
                instance.ScaleToFit(120f, 120f);
                instance.Alignment = 0;
                pdfPtable6.AddCell(instance);
            }
            document.Add((IElement)pdfPtable6);
            document.Close();
            string str20 = "notaVenta/" + str2;
            this.hfrutaArchivoPdf.Value = str20;
            return str20;
        }



        public string generarOrdenTrabajoPdf(string ordenTrabajo, string notaVenta, string idCotizacion, string nombreEjecutivo)
        {
            string fechaEntrega = string.Empty;
            string obs = string.Empty;
            foreach (DataRow item in dal.getBuscarOrdenTrabajo(ordenTrabajo).Tables[0].Rows)
            {
                fechaEntrega = item["FECHA_ENTREGA"].ToString();
                obs = item["OBSERVACION"].ToString();
                break;
            }

            DateTime Hoy = DateTime.Today;
            string fechaHoy = Hoy.ToString("dd-MM-yyyy");

            string nombreArchivoPdf = "OrdenTrabajo_" + ordenTrabajo + ".pdf";
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

            Font times = new Font(bfTimes, 7, Font.NORMAL);
            Font timesRojo = new Font(bfTimes, 9, Font.BOLD, BaseColor.RED);
            Font timesCorrelativo = new Font(bfTimes, 9, Font.BOLD);
            Font fontCabecera = new Font(bfTimes, 8, Font.BOLD);
            Font fontFirma = new Font(bfTimes, 8, Font.BOLD);
            Font fontResaltar = new Font(bfTimes, 10, Font.BOLD);

            Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writePdf = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("ordenTrabajo/" + nombreArchivoPdf), FileMode.Create));
            doc.Open();

            string nombreEmp = "";
            string giroEmp = "";
            string rutEmp = "";
            string telefonoEmp = "";
            string logoEmpresa = "";

            foreach (DataRow item in dal.getBuscarEmpresa(null, Session["idEmpresa"].ToString()).Tables[0].Rows)
            {
                nombreEmp = item["NOMBRE_EMPRESA"].ToString();
                giroEmp = item["GIRO"].ToString();
                rutEmp = item["RUT"].ToString();
                telefonoEmp = item["TELEFONO"].ToString();
                logoEmpresa = item["IMAGEN"].ToString();
            }

            PdfPTable tableEmpresa = new PdfPTable(1);

            
            PdfPTable tableNumeroCotizacion = new PdfPTable(2);
            PdfPCell celdaNumeroComprobante = new PdfPCell(new Paragraph("Nro. Orden de Trabajo :", fontCabecera));
            
            PdfPCell celdaNumeroNV = new PdfPCell(new Paragraph("Nº NV :", fontCabecera));
            PdfPCell celdaNumeroCot = new PdfPCell(new Paragraph("Nº Cot :", fontCabecera));
            PdfPCell celdaNumeroComprobanteFecha = new PdfPCell(new Paragraph("Fecha Creacion:", fontResaltar));
            PdfPCell celdaNumeroComprobanteFechaEntrega = new PdfPCell(new Paragraph("Fecha Entrega:", fontResaltar));
            PdfPCell celdaEjecutivo = new PdfPCell(new Paragraph("Ejecutivo :", fontResaltar));
            
            tableNumeroCotizacion.AddCell(celdaNumeroComprobante);
            tableNumeroCotizacion.AddCell(new Paragraph(ordenTrabajo, times));

            //tableNumeroCotizacion.AddCell(celdaEstadoNotaVenta);
            //tableNumeroCotizacion.AddCell(new Paragraph("PENDIENTE", times));

            tableNumeroCotizacion.AddCell(celdaNumeroNV);
            tableNumeroCotizacion.AddCell(new Paragraph(notaVenta, times));

            tableNumeroCotizacion.AddCell(celdaNumeroCot);
            tableNumeroCotizacion.AddCell(new Paragraph(idCotizacion, times));

            tableNumeroCotizacion.AddCell(celdaNumeroComprobanteFecha);
            tableNumeroCotizacion.AddCell(new Paragraph(fechaHoy, fontResaltar));

            tableNumeroCotizacion.AddCell(celdaNumeroComprobanteFechaEntrega);
            tableNumeroCotizacion.AddCell(new Paragraph(fechaEntrega, fontResaltar));

            tableNumeroCotizacion.AddCell(celdaEjecutivo);
            tableNumeroCotizacion.AddCell(new Paragraph(nombreEjecutivo, fontResaltar));

            tableNumeroCotizacion.DefaultCell.Border = Rectangle.NO_BORDER;

            tableNumeroCotizacion.HorizontalAlignment = Element.ALIGN_LEFT;
            tableNumeroCotizacion.WidthPercentage = 35.0f;

            foreach (PdfPCell celda in tableNumeroCotizacion.Rows[0].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
            }

            foreach (PdfPCell celda in tableNumeroCotizacion.Rows[1].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
                //celda.HorizontalAlignment = 2;
            }

            foreach (PdfPCell celda in tableNumeroCotizacion.Rows[2].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
            }

            foreach (PdfPCell celda in tableNumeroCotizacion.Rows[3].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
            }

            foreach (PdfPCell celda in tableNumeroCotizacion.Rows[4].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
            }

            foreach (PdfPCell celda in tableNumeroCotizacion.Rows[5].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
            }

            tableEmpresa.AddCell(tableNumeroCotizacion);
            tableEmpresa.DefaultCell.Border = Rectangle.NO_BORDER;

            tableEmpresa.HorizontalAlignment = Element.ALIGN_LEFT;
            tableEmpresa.WidthPercentage = 35.0f;

            foreach (PdfPCell celda in tableEmpresa.Rows[0].GetCells())
            {
                celda.Border = Rectangle.NO_BORDER;
            }

            doc.Add(tableEmpresa);

            //FIN CABECERA
            
            Chunk tituloTipoExamen = new Chunk("Orden de Trabajo", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD));
            tituloTipoExamen.SetUnderline(0.1f, -2f);

            Paragraph par = new Paragraph(tituloTipoExamen);
            par.Alignment = Element.ALIGN_CENTER;
            doc.Add(par);
            
            doc.Add(new Paragraph(" ", times));

            string tituloClientePricipalOAsociado = string.Empty;
            string tituloContactoPrincipalOAsociado = string.Empty;

            DataTable dtCliente2 = new DataTable();
            dtCliente2 = dal.getBuscarClientePorRut(hfIdCliente.Value).Tables[0];

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

            Chunk datosCliente = new Chunk(tituloClientePricipalOAsociado, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD));
            datosCliente.SetUnderline(0.1f, -2f);
            doc.Add(datosCliente);

            PdfPTable tableDatosCliente = new PdfPTable(4);

            float[] widthsDatosCliente = new float[] { 35f, 95f, 35f, 55f };
            tableDatosCliente.SetWidths(widthsDatosCliente);

            string comunaCliente = "";
            string ciudadCliente = "";
            string giroCliente = "";
            string condVentaCliente = "";
            string telefono = string.Empty;
            foreach (DataRow item in dtCliente2.Rows)
            {
                comunaCliente = item["COMUNA"].ToString();
                ciudadCliente = item["CIUDAD"].ToString();
                giroCliente = item["GIRO"].ToString();
                condVentaCliente = item["GLOSA"].ToString();
                telefono = item["TELEFONO"].ToString();
            }

            tableDatosCliente.AddCell(new Paragraph("Rut :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(lblRut.Text, times));
            tableDatosCliente.AddCell(new Paragraph("Nombre :", fontCabecera));
            tableDatosCliente.AddCell(new Paragraph(lblRazonSocial.Text, times));
            
            

            tableDatosCliente.HorizontalAlignment = Element.ALIGN_LEFT;
            tableDatosCliente.WidthPercentage = 100.0f;

            doc.Add(tableDatosCliente);
            doc.Add(new Paragraph(" ", times));
            
            
            PdfPTable tableDatosClienteAsociado = new PdfPTable(4);
            foreach (DataRow item in dtCliente2.Rows)
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
            
            Chunk datosObs = new Chunk("Observación ", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD));
            datosObs.SetUnderline(0.1f, -2f);
            doc.Add(datosObs);

            PdfPTable tableDatosObs= new PdfPTable(1);
            tableDatosObs.AddCell(new Paragraph(obs, fontCabecera));
            tableDatosObs.HorizontalAlignment = Element.ALIGN_LEFT;
            tableDatosObs.WidthPercentage = 100.0f;
            doc.Add(tableDatosObs);



            doc.Add(new Paragraph(" ", times));
            Chunk datosDetalleCotizacion = new Chunk("Detalle ", FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD));
            datosDetalleCotizacion.SetUnderline(0.1f, -2f);
            doc.Add(datosDetalleCotizacion);
            
            doc.Add(new Paragraph(" ", times));
            

            PdfPTable tableDetalle = new PdfPTable(3);
            tableDetalle.AddCell(new Paragraph("Código", fontCabecera));
            tableDetalle.AddCell(new Paragraph("Producto", fontCabecera));
            tableDetalle.AddCell(new Paragraph("Cantidad", fontCabecera));
            float[] widthsDatosDetalle = new float[] { 35f, 105f, 25f };
            tableDetalle.SetWidths(widthsDatosDetalle);

            DataTable dtDetalleCot = new DataTable();
            dtDetalleCot = dal.getBuscarDetalleNotaVenta(notaVenta).Tables[0];

            foreach (DataRow item in dtDetalleCot.Rows)
            {
                tableDetalle.AddCell(new Paragraph(item["CODIGO"].ToString(), times));
                tableDetalle.AddCell(new Paragraph(item["NOM_PRODUCTO"].ToString().Replace("<b>", "").Replace("</b>", "").Replace("&nbsp", "."), times));
                
                string cantidad = item["CANTIDAD"].ToString().Replace("<b>", "").Replace("</b>", "").Replace("&nbsp", ".");
                string montoUni = (Convert.ToDouble(item["MONTO_NETO"]) / Convert.ToDouble(item["CANTIDAD"])).ToString("n0");

                string monto = item["MONTO_NETO"].ToString().Replace("<b>", "").Replace("</b>", "").Trim();
                if (monto != string.Empty)
                {
                    monto = Convert.ToInt32(Convert.ToDouble(monto)).ToString("n0");
                }

                PdfPCell celdaCantidad = new PdfPCell(new Paragraph(cantidad, times));
                celdaCantidad.HorizontalAlignment = 2;
                tableDetalle.AddCell(celdaCantidad);
                
            }

            int totalNeto = 0;
            int totalConDescuento = 0;
            int iva = 0;
            int total = 0;

            DataTable dtNotaVenta = new DataTable();
            dtNotaVenta = dal.getBuscarNotaVenta(notaVenta).Tables[0];
            string direccionDespacho = "";
            foreach (DataRow item in dtNotaVenta.Rows)
            {
                totalNeto = Convert.ToInt32(Convert.ToDecimal(item["MONTO_NETO"].ToString()));
                totalConDescuento = Convert.ToInt32(Convert.ToDecimal(item["MONTO_DESCUENTO"].ToString()));
                iva = Convert.ToInt32(Convert.ToDecimal(item["MONTO_IVA"].ToString()));
                total = Convert.ToInt32(Convert.ToDecimal(item["MONTO_TOTAL"].ToString()));
                direccionDespacho = item["DIRECCION"].ToString();
            }

            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));

            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));

            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            PdfPCell celdaMontoNeto = new PdfPCell(new Paragraph(totalNeto.ToString("n0"), times));
            celdaMontoNeto.HorizontalAlignment = 2;

            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            PdfPCell celdaMontoDescuento = new PdfPCell(new Paragraph(totalConDescuento.ToString("n0"), times));
            celdaMontoDescuento.HorizontalAlignment = 2;

            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            PdfPCell celdaMontoIva = new PdfPCell(new Paragraph(iva.ToString("n0"), times));
            celdaMontoIva.HorizontalAlignment = 2;

            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            tableDetalle.AddCell(new Paragraph(" ", times));
            PdfPCell celdaMontoTotal = new PdfPCell(new Paragraph(total.ToString("n0"), times));
            celdaMontoTotal.HorizontalAlignment = 2;
            
            tableDetalle.HorizontalAlignment = Element.ALIGN_LEFT;
            tableDetalle.WidthPercentage = 100.0f;

            foreach (PdfPCell celda in tableDetalle.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 2;
            }

            doc.Add(tableDetalle);
            doc.Add(new Paragraph(" ", times));

            string rutOrdenCompra = string.Empty;
            string rutOrdenCompra2 = string.Empty;
            IEnumerator enumerator = this.dal.getBuscarNotaVenta(notaVenta).Tables[0].Rows.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    DataRow current = (DataRow)enumerator.Current;
                    rutOrdenCompra = current["RUTA_ORDEN_DE_COMPRA"].ToString();
                    rutOrdenCompra2 = current["RUTA_ORDEN_DE_COMPRA2"].ToString();
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }

            PdfPTable pdfPtable6 = new PdfPTable(1);
            pdfPtable6.WidthPercentage = 100f;
            string path2 = rutOrdenCompra;
            if (path2 == string.Empty)
            {
                pdfPtable6.AddCell((Phrase)new Paragraph("Sin Imagen", times));
            }
            else
            {
                iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(this.Server.MapPath(path2));
                instance.ScaleToFit(120f, 120f);
                instance.Alignment = 0;
                pdfPtable6.AddCell(instance);
            }
            string path3 = rutOrdenCompra2;
            if (path3 == string.Empty)
            {
                pdfPtable6.AddCell((Phrase)new Paragraph("Sin Imagen", times));
            }
            else
            {
                iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(this.Server.MapPath(path3));
                instance.ScaleToFit(120f, 120f);
                instance.Alignment = 0;
                pdfPtable6.AddCell(instance);
            }
            doc.Add((IElement)pdfPtable6);
            doc.Close();

            string ruta = "ordenTrabajo/" + nombreArchivoPdf;
            hfrutaArchivoPdf.Value = ruta;

            return ruta;
        }
        
        protected void btnGrabarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                dal.setIngresarDireccion(this.Session["IdCliente"].ToString(), this.txtCalle.Text, this.txtNumero.Text, this.txtResto.Text, this.ddlComunaDireccion.SelectedValue);
                buscarDireccionesPorCliente();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnEditarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                dal.setEditarDireccionCliente(hfIdDireccion.Value, txtCalle.Text, txtNumero.Text, txtResto.Text, ddlComunaDireccion.SelectedValue);
                buscarDireccionesPorCliente();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnEditarDireccionCliente_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblId = (Label)grvDireccionCliente.Rows[row.RowIndex].FindControl("lblId");
                Label _lblCalle = (Label)grvDireccionCliente.Rows[row.RowIndex].FindControl("lblCalle");
                Label _lblNumero = (Label)grvDireccionCliente.Rows[row.RowIndex].FindControl("lblNumero");
                Label _lblResto = (Label)grvDireccionCliente.Rows[row.RowIndex].FindControl("lblResto");
                Label _lblComuna = (Label)grvDireccionCliente.Rows[row.RowIndex].FindControl("lblComuna");

                txtCalle.Text = string.Empty;
                txtNumero.Text = string.Empty;
                txtResto.Text = string.Empty;
                txtComunaDireccion.Text = string.Empty;
                ddlComunaDireccion.ClearSelection();

                buscarComunaDireccion();

                hfIdDireccion.Value = _lblId.Text;
                txtCalle.Text = _lblCalle.Text;
                txtNumero.Text = _lblNumero.Text;
                txtResto.Text = _lblResto.Text;
                txtComunaDireccion.Text = _lblComuna.Text;
                
                if (this.ddlComunaDireccion.Items.FindByValue(_lblComuna.Text) != null)
                {
                    ddlComunaDireccion.SelectedValue = _lblComuna.Text;
                }

                btnGrabarDireccion.Visible = false;
                btnEditarDireccion.Visible = true;

                lblAgregarDireccion.Text = "Editar Direccion";

                mdlAgregarDireccion.Show();
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
                Label _lblIdContacto = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblIdContacto");
                Label _lblEmail1 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblEmail1");
                Label _lblEmail2 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblEmail2");
                Label _lblCelular = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblCelular");
                Label _lblTelefono1 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblTelefono1");
                Label _lblTelefono2 = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblTelefono2");
                Label _lblNombreContacto = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblNombreContacto");
                Label _lblCargo = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblCargo");
                Label _lblIdCargo = (Label)grvContactos.Rows[row.RowIndex].FindControl("lblIdCargo");

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

        protected void ibtnRutaOrdenCompra_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaOrdenDeCompra = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblRutaOrdenDeCompra");
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaOrdenDeCompra.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEliminarNotaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdNotaVenta = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdNotaVenta");

                dal.setEliminarNotaVenta(_lblIdNotaVenta.Text);

                BuscarNotaVenta();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void paginaciongrvNotaVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string idPerfil = Session["variablePerfil"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "danger";

                Label _lblRutaOrdenDeCompra = (Label)e.Row.FindControl("lblRutaOrdenDeCompra");
                Label _lblIdOrdenTrabajo = (Label)e.Row.FindControl("lblIdOrdenTrabajo");
                ImageButton _ibtnRutaOrdenCompra = (ImageButton)e.Row.FindControl("ibtnRutaOrdenCompra");
                ImageButton _imgEliminar = (ImageButton)e.Row.FindControl("imgEliminar");
                ImageButton _imgIngresarOT = (ImageButton)e.Row.FindControl("imgIngresarOT");
                
                if (_lblRutaOrdenDeCompra.Text == string.Empty)
                {
                    _ibtnRutaOrdenCompra.Visible = false;
                }

                if (_lblIdOrdenTrabajo.Text == string.Empty)
                {
                    _imgIngresarOT.Visible = true;
                }
                else
                {
                    _imgIngresarOT.Visible = false;
                }

                if (idPerfil == "1")
                {
                    _imgEliminar.Visible = true;
                }
                else
                {
                    _imgEliminar.Visible = false;
                }
            }
            
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvNotaVenta.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvNotaVenta.PageCount);
            }
        }

        void buscarComunaDireccion()
        {
            ddlComunaDireccion.DataSource = dal.getBuscarComuna();
            ddlComunaDireccion.DataValueField = "NOM_COMUNA";
            ddlComunaDireccion.DataTextField = "NOM_COMUNA";
            ddlComunaDireccion.DataBind();
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
            ddlCampana.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void ddlActividadComercial_DataBound(object sender, EventArgs e)
        {
            ddlActividadComercial.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void ddlFormaPago_DataBound(object sender, EventArgs e)
        {
            ddlFormaPago.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void ibtnAgregarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdNotaVenta = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdNotaVenta");
                Label _lblMontoNeto = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblMontoNeto2");


                formaPago();
                ddlFormaPago.ClearSelection();
                txtIdFactura.Text = string.Empty;
                txtIdFactura.Enabled = true;
                txtFechaFacturacion.Text = string.Empty;
                txtMontoNeto.Text = string.Empty;

                hfIdNotaVenta.Value = _lblIdNotaVenta.Text;
                txtMontoNeto.Text = _lblMontoNeto.Text.Replace(".", "");
                mdlAgregarFactura.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                string idUsuarioCreacion = this.Session["variableIdUsuario"].ToString();
                string text = this.lblRut.Text;
                int int32 = Convert.ToInt32(this.Session["IdCliente"]);
                dal.setIngresarFactura(this.txtIdFactura.Text, text, this.hfIdNotaVenta.Value, this.txtFechaFacturacion.Text, this.txtMontoNeto.Text, "1", idUsuarioCreacion, this.ddlFormaPago.SelectedValue, int32);
                factura();
                mdlAgregarFactura.Hide();
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


        protected void imgEditarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdFactura = (Label)grvFacturas.Rows[row.RowIndex].FindControl("lblIdFactura");
                Label _lblIdNotaVenta = (Label)grvFacturas.Rows[row.RowIndex].FindControl("lblIdNotaVenta");
                Label _lblFechaFacturacion = (Label)grvFacturas.Rows[row.RowIndex].FindControl("lblFechaFacturacion");
                Label _lblMontoNeto = (Label)grvFacturas.Rows[row.RowIndex].FindControl("lblMontoNeto");
                Label _lblIdFormaPago = (Label)grvFacturas.Rows[row.RowIndex].FindControl("lblIdFormaPago");

                string idUsuario = Session["variableIdUsuario"].ToString();
                string rutCliente = lblRut.Text;
                formaPago();

                txtIdFactura.Text = string.Empty;
                txtIdFactura.Enabled = false;

                hfIdNotaVenta.Value = string.Empty;
                txtFechaFacturacion.Text = string.Empty;
                txtMontoNeto.Text = string.Empty;
                ddlFormaPago.ClearSelection();

                txtIdFactura.Text = _lblIdFactura.Text;
                hfIdNotaVenta.Value = _lblIdNotaVenta.Text;
                txtFechaFacturacion.Text = _lblFechaFacturacion.Text;
                txtMontoNeto.Text = _lblMontoNeto.Text.Replace(".","");

                if (_lblIdFormaPago.Text != string.Empty)
                {
                    ddlFormaPago.SelectedValue = _lblIdFormaPago.Text;
                }
                mdlAgregarFactura.Show();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void factura() 
        {
            if (Session["IdCliente"] == null)
                return;
            grvFacturas.DataSource = dal.getBuscarFacturaPorRutCliente(Session["IdCliente"].ToString());
            grvFacturas.DataBind();
        }

        protected void imgEliminarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;

                Label _lblIdFactura = (Label)grvFacturas.Rows[row.RowIndex].FindControl("lblIdFactura");
                dal.setEliminarFactura(_lblIdFactura.Text);
                factura();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnPagarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdFactura = (Label)grvFacturas.Rows[row.RowIndex].FindControl("lblIdFactura");

                Session["idFactura"] = _lblIdFactura.Text;
                Response.Redirect("IngresoPago.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnVerPago_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdNotaVenta = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdNotaVenta");

                grvPago.DataSource = dal.getBuscarPagoDetallePorIdFactura(_lblIdNotaVenta.Text);
                grvPago.DataBind();
                mdlVerPago.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void ibtnEliminarPago_Click(object sender, EventArgs e)
        {
            try
            {
                this.dal.setEliminarPagoPorIdPago(((Label)this.grvPago.Rows[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].FindControl("lblId")).Text);
                this.Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void grvFacturasPaginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "danger";
            }
        }

        protected void btnBuscarTelefono_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dal.getBuscarEmpresaPorEmail(txtBuscarTelefono.Text).Tables[0];
                
                grvEmpresas.DataSource = dt;
                grvEmpresas.DataBind();

                txtRutoRazonSocial.Text = string.Empty;
                mdlEmpresas.Show();
                
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        
        protected void btnVerProductosVendidos_Click(object sender, EventArgs e)
        {
            try
            {
                grvProductosVendidosPorCliente.DataSource = dal.getBuscarProductosVendidosPorRutCliente(null, null, null, Convert.ToInt32(Session["IdCliente"]));
                grvProductosVendidosPorCliente.DataBind();
                mdlProductosVendidos.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnVerClientesAsociados_Click(object sender, EventArgs e)
        {
            try
            {
                grvClientesAsociados.DataSource = dal.getBuscarClientePorId(Session["IdCliente"].ToString());
                grvClientesAsociados.DataBind();
                mdlVerClientesAsociados.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnNuevaCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Cotizacion.aspx?c=" + Session["IdCliente"].ToString());
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnPagarNotaVenta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdNotaVenta = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdNotaVenta");

                Session["idNotaVenta"] = _lblIdNotaVenta.Text;
                Response.Redirect("IngresoPago.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void grvPago_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string str = this.Session["variablePerfil"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            } 
        }

        protected void ibtnEliminarCotizacion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                dal.setEliminarCotizacion(Convert.ToInt32(((Label)this.grvCotizacionesCRM.Rows[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].FindControl("lblIdCotizacion")).Text));
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgIngresarOT_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdNotaVenta = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdNotaVenta");
                Label _lblIdCotizacion = (Label)grvNotaVenta.Rows[row.RowIndex].FindControl("lblIdCotizacion");
                
                lblIdNotaDeVenta.Text = _lblIdNotaVenta.Text;
                lblIdCotizacion.Text = _lblIdCotizacion.Text;

                txtFechaEntregaOriginal.Text = string.Empty;
                mdlIngresarOT.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnGenerarOT_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFechaEntregaOriginal.Text))
                {
                    lblInformacion.Text = "Favor ingresar la fecha de entrega.";
                    mdlInformacion.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtObservacionOT.Text))
                {
                    lblInformacion.Text = "Favor ingresar la observación.";
                    mdlInformacion.Show();
                    return;
                }

                string obs = txtObservacionOT.Text;
                string idUsuario = this.Session["variableIdUsuario"].ToString();
                DataTable table = this.dal.getBuscarUsuario(null, idUsuario).Tables[0];
                string nombreEjecutivo = "";
                foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                    nombreEjecutivo = row["NOMBRE"].ToString();

                string OT = dal.setIngresarOrdenTrabajo(lblIdNotaDeVenta.Text, lblIdCotizacion.Text, "", "", "", "", "", "", txtFechaEntregaOriginal.Text, obs);
                string rutaPdfOT = generarOrdenTrabajoPdf(OT, lblIdNotaDeVenta.Text, lblIdCotizacion.Text, nombreEjecutivo);

                dal.setEditarRutaPdfOT(OT, rutaPdfOT);

                BuscarNotaVenta();
                BuscarOT();
                ScriptManager.RegisterStartupScript((Page)this, this.GetType(), this.UniqueID, "window.open('" + rutaPdfOT + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void paginaciongrvCotizacionesCRM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvCotizacionesCRM.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvCotizacionesCRM.PageCount);
            }

            string str1 = this.Session["variablePerfil"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            e.Row.CssClass = "success";
            Label _lblIdNotaVenta = (Label)e.Row.FindControl("lblIdNotaVenta");
            Label _lblIdEstadoCotizacion = (Label)e.Row.FindControl("lblIdEstadoCotizacion");
            Label _lblEstadoCotizacion = (Label)e.Row.FindControl("lblEstadoCotizacion");
            

            ImageButton _ibtnGenerarNotaVenta = (ImageButton)e.Row.FindControl("ibtnGenerarNotaVenta");
            ImageButton control6 = (ImageButton)e.Row.FindControl("imgPdf");
            ImageButton control7 = (ImageButton)e.Row.FindControl("ibtnEliminarCotizacion");

            if (_lblIdEstadoCotizacion.Text == "1")
            {
                _lblEstadoCotizacion.CssClass = "label label-warning";
            }


            if (_lblIdNotaVenta.Text == string.Empty)
                _ibtnGenerarNotaVenta.Visible = true;
            else
                _ibtnGenerarNotaVenta.Visible = false;
            if (_lblIdEstadoCotizacion.Text == "5")
                control6.Visible = false;
            if (_lblIdEstadoCotizacion.Text == "5" || _lblIdEstadoCotizacion.Text == "4" || (_lblIdEstadoCotizacion.Text == "3" || _lblIdEstadoCotizacion.Text == "2"))
                _lblEstadoCotizacion.CssClass = "label label-success";
            if (str1 == "1")
                control7.Visible = true;
            else
                control7.Visible = false;
        }

        protected void grvGestionesCRM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            Label control1 = (Label)e.Row.FindControl("lblIdGestion");
            Label control2 = (Label)e.Row.FindControl("lblCotizacion");
            DataTable dataTable = new DataTable();
            DataTable table = this.dal.getBuscarCotizacionGestion(control1.Text).Tables[0];
            string str1 = "";
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                str1 = str1 + row["ID_COTIZACION"].ToString() + ",";
            string str2 = str1.TrimEnd(',');
            control2.Text = str2;
        }

        void cotizacionesSession()
        {
            dsCotizaciones = Session["DatosCotizaciones"] as DataSet;
            grvCotizacionesCRM.DataSource = dsCotizaciones;
            grvCotizacionesCRM.DataBind();
        }

        protected void grvOrdenDeTrabajo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string idPerfil = this.Session["variablePerfil"].ToString();
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                e.Row.CssClass = "warning";

                ImageButton _imgEliminarOT = (ImageButton)e.Row.FindControl("imgEliminarOT");
                if (idPerfil=="1")
                {
                    _imgEliminarOT.Visible = true;
                }
                else
                {
                    _imgEliminarOT.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgEliminarOT_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblIdOrdenDeTrabajo = (Label)grvOrdenDeTrabajo.Rows[row.RowIndex].FindControl("lblIdOrdenDeTrabajo");
                dal.setEliminarOT(_lblIdOrdenDeTrabajo.Text);

                BuscarOT();
                BuscarNotaVenta();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgPdfOT_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                Label _lblRutaPdf = (Label)grvOrdenDeTrabajo.Rows[row.RowIndex].FindControl("lblRutaPdf");

                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "window.open('" + _lblRutaPdf.Text + "','_blank');", true);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}