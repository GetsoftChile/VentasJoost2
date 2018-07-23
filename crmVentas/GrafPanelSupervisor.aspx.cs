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
    public partial class GrafPanelSupervisor : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {

                    buscarVendedorIndicadores();

                    if (Session["fechaDesdePanelSupervisor"] == null)
                    {
                        //string fechaDesde = Session["fechaDesdePanelSupervisor"].ToString();
                        //string fechaHasta = Session["fechaHastaPanelSupervisor"].ToString();

                        DateTime hoy = DateTime.Now;
                        txtFechaDesdeIndicador.Text = (hoy.AddDays(-30).ToString("dd-MM-yyyy"));
                        txtFechaHastaIndicador.Text = hoy.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        string fechaDesde = Session["fechaDesdePanelSupervisor"].ToString();
                        string fechaHasta = Session["fechaHastaPanelSupervisor"].ToString();

                        txtFechaDesdeIndicador.Text = fechaDesde;
                        txtFechaHastaIndicador.Text = fechaHasta;
                        ddlPesoOQ.SelectedValue = Session["tipoPanelSupervisor"].ToString();

                        if (Session["vendedorPanelSupervisor"].ToString() == string.Empty)
                        {
                            ddlVendedorIndicador.SelectedValue = "0";
                        }
                        else
                        {

                            ddlVendedorIndicador.SelectedValue = Session["vendedorPanelSupervisor"].ToString();
                        }
                       
                    }

                    string idUsuario = Session["variableIdUsuario"].ToString();
                    string idPerfil = Session["variablePerfil"].ToString();
                    if (idPerfil == "3")
                    {
                        ddlVendedorIndicador.SelectedValue = idUsuario;
                        ddlVendedorIndicador.Enabled = false;
                    }


                    buscar();
                }
                
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnBuscarIndicador_Click(object sender, EventArgs e)
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

        protected void ddlVendedorIndicador_DataBound(object sender, EventArgs e)
        {
            ddlVendedorIndicador.Items.Insert(0, new ListItem("Todos", "0"));
        }


        void buscarVendedorIndicadores()
        {
            ddlVendedorIndicador.DataSource = dal.getBuscarUsuario(null, null);
            ddlVendedorIndicador.DataValueField = "ID_USUARIO";
            ddlVendedorIndicador.DataTextField = "USUARIO";
            ddlVendedorIndicador.DataBind();
        }



        protected void lbtnMisCotizaciones_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = ddlPesoOQ.SelectedValue;
                string fechaDesde = txtFechaDesdeIndicador.Text;
                string fechaHasta = txtFechaHastaIndicador.Text;
                string vendedor = ddlVendedorIndicador.SelectedValue;

                //string _url = string.Format("IdentificacionTipoExamen.aspx?id={0}", _lblidUsuario.Text);
                Session["variableFechaDesde"] = fechaDesde;
                Session["variableFechaHasta"] = fechaHasta;
                Session["variableVendedor"] = vendedor;
                Session["variableTipoFiltro"] = "TotalCotizacion";

                Response.Redirect("DetalleResumen.aspx");

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnMisVentasGanadas_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = ddlPesoOQ.SelectedValue;
                string fechaDesde = txtFechaDesdeIndicador.Text;
                string fechaHasta = txtFechaHastaIndicador.Text;
                string vendedor = ddlVendedorIndicador.SelectedValue;

                //string _url = string.Format("IdentificacionTipoExamen.aspx?id={0}", _lblidUsuario.Text);
                Session["variableFechaDesde"] = fechaDesde;
                Session["variableFechaHasta"] = fechaHasta;
                Session["variableVendedor"] = vendedor;
                Session["variableTipoFiltro"] = "VentasGanadas";

                Response.Redirect("DetalleResumen.aspx");

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }



        protected void lbtnMisVentasFacturadas_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = ddlPesoOQ.SelectedValue;
                string fechaDesde = txtFechaDesdeIndicador.Text;
                string fechaHasta = txtFechaHastaIndicador.Text;
                string vendedor = ddlVendedorIndicador.SelectedValue;

                //string _url = string.Format("IdentificacionTipoExamen.aspx?id={0}", _lblidUsuario.Text);
                Session["variableFechaDesde"] = fechaDesde;
                Session["variableFechaHasta"] = fechaHasta;
                Session["variableVendedor"] = vendedor;
                Session["variableTipoFiltro"] = "VentasFacturadas";

                Response.Redirect("DetalleVentasFacturadas.aspx");

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnMisSeguimientos_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = ddlPesoOQ.SelectedValue;
                string fechaDesde = txtFechaDesdeIndicador.Text;
                string fechaHasta = txtFechaHastaIndicador.Text;
                string vendedor = ddlVendedorIndicador.SelectedValue;

                //string _url = string.Format("IdentificacionTipoExamen.aspx?id={0}", _lblidUsuario.Text);
                Session["variableFechaDesde"] = fechaDesde;
                Session["variableFechaHasta"] = fechaHasta;
                Session["variableVendedor"] = vendedor;
                Session["variableTipoFiltro"] = "MisSeguimientos";

                Response.Redirect("DetalleResumen.aspx");

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnMisNegociosPerdidos_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = ddlPesoOQ.SelectedValue;
                string fechaDesde = txtFechaDesdeIndicador.Text;
                string fechaHasta = txtFechaHastaIndicador.Text;
                string vendedor = ddlVendedorIndicador.SelectedValue;

                //string _url = string.Format("IdentificacionTipoExamen.aspx?id={0}", _lblidUsuario.Text);
                Session["variableFechaDesde"] = fechaDesde;
                Session["variableFechaHasta"] = fechaHasta;
                Session["variableVendedor"] = vendedor;
                Session["variableTipoFiltro"] = "NegociosPerdidos";

                Response.Redirect("DetalleResumen.aspx");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }


        protected void lbtnSinSeguimiento_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = ddlPesoOQ.SelectedValue;
                string fechaDesde = txtFechaDesdeIndicador.Text;
                string fechaHasta = txtFechaHastaIndicador.Text;
                string vendedor = ddlVendedorIndicador.SelectedValue;

                //string _url = string.Format("IdentificacionTipoExamen.aspx?id={0}", _lblidUsuario.Text);
                Session["variableFechaDesde"] = fechaDesde;
                Session["variableFechaHasta"] = fechaHasta;
                Session["variableVendedor"] = vendedor;
                Session["variableTipoFiltro"] = "SinSeguimientos";

                Response.Redirect("DetalleResumen.aspx");

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscar() 
        {
            string vendedor = ddlVendedorIndicador.SelectedValue;
            string tipo = ddlPesoOQ.SelectedValue;
            Session["tipoPanelSupervisor"] = vendedor;
            Session["vendedorPanelSupervisor"] = tipo;
            Session["fechaDesdePanelSupervisor"] = txtFechaDesdeIndicador.Text;
            Session["fechaHastaPanelSupervisor"] = txtFechaHastaIndicador.Text;

            string fechaDesde = Session["fechaDesdePanelSupervisor"].ToString();
            string fechaHasta = Session["fechaHastaPanelSupervisor"].ToString();

            string conSeguimiento = "";
            //string sinSeguimiento = "";
            string total = "";
            conSeguimiento = dal.getValTotalSeguimientoMarcados(ddlVendedorIndicador.Text, fechaDesde, fechaHasta, "C", tipo);
            //sinSeguimiento = dal.getValTotalSeguimientoMarcados(null, txtFechaDesdeIndicador.Text, txtFechaHastaIndicador.Text, "S", tipo);
            total = dal.getValTotalSeguimientoMarcados(null, fechaDesde, fechaHasta, "T", tipo);

            if (conSeguimiento == string.Empty)
            {
                conSeguimiento = "0";
            }
            //Int64 resto = Convert.ToInt64(Convert.ToDecimal(total)) - Convert.ToInt64(Convert.ToDecimal(conSeguimiento));
            DataTable dtSinSeg = dal.getBuscarContarSeguimiento(ddlVendedorIndicador.SelectedValue, txtFechaDesdeIndicador.Text, txtFechaHastaIndicador.Text,"S").Tables[0];
            string sinSeg = "0";
            foreach (DataRow item in dtSinSeg.Rows)
            {
                sinSeg = item["CANTIDAD"].ToString();
            }
            lbtnSinSeguimiento.Text = Convert.ToInt64(sinSeg).ToString("n0");

            DataTable dtGrafCotizaciones = new DataTable();
            dtGrafCotizaciones = dal.getBuscarGrafCotizaciones(vendedor, fechaDesde, fechaHasta, tipo).Tables[0];

            if (tipo == "Q")
            {
                foreach (DataRow item in dtGrafCotizaciones.Rows)
                {
                    if (item["CANT_COTIZ"].ToString() == string.Empty)
                    {
                        lbtnMisCotizaciones.Text = "";
                    }
                    else
                    {
                        lbtnMisCotizaciones.Text = Convert.ToInt64(item["CANT_COTIZ"]).ToString("n0");
                    }
                }
            }
            else
            {
                foreach (DataRow item in dtGrafCotizaciones.Rows)
                {
                    if (item["MONTO_COTIZ"].ToString() == string.Empty)
                    {
                        lbtnMisCotizaciones.Text = "";
                    }
                    else
                    {
                        Int64 c = Convert.ToInt64(Convert.ToDecimal(item["MONTO_COTIZ"]));
                        lbtnMisCotizaciones.Text = c.ToString("n0");
                    }
                }
            }

            DataTable dtVentasGanadas = new DataTable();
            dtVentasGanadas = dal.getBuscarVentasGanadas(vendedor, fechaDesde, fechaHasta, tipo).Tables[0];

            foreach (DataRow item in dtVentasGanadas.Rows)
            {
                if (item["cont_cotiz"].ToString() == string.Empty)
                {
                    lbtnMisVentasGanadas.Text = "0";
                }
                else
                {
                    lbtnMisVentasGanadas.Text = Convert.ToInt64(Convert.ToDecimal(item["cont_cotiz"])).ToString("n0");
                }
            }

            DataTable dtNegociosPerdidos = new DataTable();
            dtNegociosPerdidos = dal.getBuscarNegociosPerdidos(vendedor, fechaDesde, fechaHasta, tipo).Tables[0];
            foreach (DataRow item in dtNegociosPerdidos.Rows)
            {
                if (item["cont_cotiz"].ToString() == string.Empty)
                {
                    lbtnMisNegociosPerdidos.Text = "0";
                }
                else
                {
                    lbtnMisNegociosPerdidos.Text = Convert.ToInt64(Convert.ToDecimal(item["cont_cotiz"])).ToString("n0");
                }
            }

            DataTable dtConSeguimiento = new DataTable();
            dtConSeguimiento = dal.getBuscarContarSeguimiento(vendedor, fechaDesde, fechaHasta, "C").Tables[0];
            foreach (DataRow item in dtConSeguimiento.Rows)
            {
                if (tipo == "Q")
                {
                    string v = item["CANTIDAD"].ToString();
                    if (v == string.Empty)
                    {
                        lbtnMisSeguimientos.Text = "0";
                    }
                    else
                    {
                        lbtnMisSeguimientos.Text = Convert.ToInt64(Convert.ToDecimal(v)).ToString("n0");
                    }
                }
                else
                {
                    string v = item["PRECIO"].ToString();
                    if (v == string.Empty)
                    {
                        lbtnMisSeguimientos.Text = "0";
                    }
                    else
                    {
                        lbtnMisSeguimientos.Text = Convert.ToInt64(Convert.ToDecimal(v)).ToString("n0");
                    }
                }
            }

            DataTable dtSinSeguimiento = new DataTable();
            dtSinSeguimiento = dal.getBuscarContarSeguimiento(vendedor, fechaDesde, fechaHasta, "S").Tables[0];
            string sinSeguimiento = "";
            foreach (DataRow item in dtSinSeguimiento.Rows)
            {
                if (tipo == "Q")
                {
                    sinSeguimiento = item["CANTIDAD"].ToString();
                }
                else
                {
                    sinSeguimiento = item["PRECIO"].ToString();
                }
            }

            lbtnMiEfectividad.Text = ((Convert.ToDouble(lbtnMisVentasGanadas.Text) / Convert.ToDouble(lbtnMisCotizaciones.Text)) * 100).ToString("0.00") + " %"; 

        }


        
    }
}