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
    public partial class DetalleResumen : System.Web.UI.Page
    {
        Datos dal = new Datos();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.ibtnExportarExcel);

                if (!Page.IsPostBack)
                {
                    hfFechaDesde.Value = Session["variableFechaDesde"].ToString();
                    hfFechaHasta.Value = Session["variableFechaHasta"].ToString();
                    hfVendedor.Value = Session["variableVendedor"].ToString();

                    buscar();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
            
        }

        //paginamiento grilla seguimiento

        protected void imgFirst_Click(object sender, EventArgs e)
        {
            buscar();

            grvSeguimiento.PageIndex = 0;
            grvSeguimiento.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvSeguimiento.PageIndex != 0)
                grvSeguimiento.PageIndex--;
            grvSeguimiento.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            buscar();
            if (grvSeguimiento.PageIndex != (grvSeguimiento.PageCount - 1))
                grvSeguimiento.PageIndex++;
            grvSeguimiento.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            buscar();
            grvSeguimiento.PageIndex = grvSeguimiento.PageCount - 1;
            grvSeguimiento.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                _lblPagina.Text = Convert.ToString(grvSeguimiento.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvSeguimiento.PageCount);
            }
        }

        void buscar()
        {
            if (Session["variableFechaDesde"] != null)
            {

                string fechaDesde = hfFechaDesde.Value;
                string fechaHasta = hfFechaHasta.Value;
                string vendedor = hfVendedor.Value;
                string idEmpresa = Session["idEmpresa"].ToString();

                if (Session["variableTipoFiltro"].ToString() == "TotalCotizacion")
                {
                    DataTable dt = new DataTable();
                    dt = dal.getBuscarCotizacionResumen(idEmpresa,vendedor, fechaDesde, fechaHasta).Tables[0];
                    grvSeguimiento.DataSource = dt;
                    Session["GridDataTable"] = dt;
                    grvSeguimiento.DataBind();
                }
                if (Session["variableTipoFiltro"].ToString() == "VentasGanadas")
                {
                    DataTable dt = new DataTable();
                    dt = dal.getBuscarVentasGanadasDetalle(vendedor, fechaDesde, fechaHasta).Tables[0];
                    grvSeguimiento.DataSource = dt;
                    Session["GridDataTable"] = dt;
                    grvSeguimiento.DataBind();
                }

                if (Session["variableTipoFiltro"].ToString() == "VentasFacturadas")
                {
                    //DataTable dt = new DataTable();
                    //dt = dal.getBuscarVentasGanadasParaDetalle(vendedor, fechaDesde, fechaHasta).Tables[0];
                    //grvSeguimiento.DataSource = dt;
                    //Session["GridDataTable"] = dt;
                    //grvSeguimiento.DataBind();
                }
                if (Session["variableTipoFiltro"].ToString() == "NegociosPerdidos")
                {
                    DataTable dt = new DataTable();
                    dt = dal.getBuscarNegociosPerdidosDetalle(vendedor, fechaDesde, fechaHasta).Tables[0];
                    grvSeguimiento.DataSource = dt;
                    Session["GridDataTable"] = dt;
                    grvSeguimiento.DataBind();
                }
                if (Session["variableTipoFiltro"].ToString() == "MisSeguimientos")
                {
                    DataTable dt = new DataTable();
                    dt = dal.getBuscarCotizacionesSeguimientoDetalle(vendedor, fechaDesde, fechaHasta).Tables[0];
                    grvSeguimiento.DataSource = dt;
                    Session["GridDataTable"] = dt;
                    grvSeguimiento.DataBind();
                }

                if (Session["variableTipoFiltro"].ToString() == "SinSeguimientos")
                {
                    DataTable dt = new DataTable();
                    dt = dal.getBuscarCotizacionesSinSeguimientoDetalle(vendedor, fechaDesde, fechaHasta).Tables[0];
                    grvSeguimiento.DataSource = dt;
                    Session["GridDataTable"] = dt;
                    grvSeguimiento.DataBind();
                }
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
                grvSeguimiento.DataSource = sortedView;
                grvSeguimiento.DataBind();

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


        protected void ibtnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["GridDataTable"];
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("ID_COTIZACION");
                dtFinal.Columns.Add("FECHA");
                dtFinal.Columns.Add("MONTO_NETO");
                dtFinal.Columns.Add("MONTO_DESCUENTO");
                dtFinal.Columns.Add("PORC_DESCUENTO");
                dtFinal.Columns.Add("MONTO_IVA");
                dtFinal.Columns.Add("MONTO_TOTAL");
                dtFinal.Columns.Add("FECHA_VALIDEZ");
                dtFinal.Columns.Add("NOM_ESTADO_COTIZACION");
                dtFinal.Columns.Add("USUARIO");

                foreach (DataRow item in dt.Rows)
                {
                    DataRow fila = dtFinal.NewRow();
                    fila["ID_COTIZACION"] = item["ID_COTIZACION"].ToString();
                    fila["FECHA"] = item["FECHA"].ToString();
                    fila["MONTO_NETO"] = item["MONTO_NETO"].ToString();
                    fila["MONTO_DESCUENTO"] = item["MONTO_DESCUENTO"].ToString();
                    fila["PORC_DESCUENTO"] = item["PORC_DESCUENTO"].ToString();
                    fila["MONTO_IVA"] = item["MONTO_IVA"].ToString();
                    fila["MONTO_TOTAL"] = item["MONTO_TOTAL"].ToString();
                    fila["FECHA_VALIDEZ"] = item["FECHA_VALIDEZ"].ToString();
                    fila["NOM_ESTADO_COTIZACION"] = item["NOM_ESTADO_COTIZACION"].ToString();
                    fila["USUARIO"] = item["USUARIO"].ToString();
                    dtFinal.Rows.Add(fila);
                }
                Utilidad.ExportDataTableToExcel(dtFinal, "exporte.xls", "", "", "", "");
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("GrafPanelSupervisor.aspx");
        }

    }
}