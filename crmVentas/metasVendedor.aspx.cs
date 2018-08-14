using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using InfoSoftGlobal;

namespace crm_valvulas_industriales
{
    public partial class metasVendedor : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnBuscar);
                
                if (!Page.IsPostBack)
                {
                    string idUsuario = Session["variableIdUsuario"].ToString();
                    string idPerfil = Session["variablePerfil"].ToString();
                    if (idPerfil == "3")
                    {
                        ddlVendedor.SelectedValue = idUsuario;
                        ddlVendedor.Enabled = false;
                    }

                    DateTime hoy = DateTime.Now;
                    //txtFechaDesde.Text = (hoy.AddDays(-7).ToString("dd-MM-yyyy"));
                    int mes = 0;
                    int ano = 0;
                    mes = hoy.Month;
                    ano = hoy.Year;

                    ddlAno.SelectedValue = ano.ToString();
                    ddlMes.SelectedValue = mes.ToString();

                    buscarVendedor();

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

        protected void ddlVendedor_DataBound(object sender, EventArgs e)
        {
            ddlVendedor.Items.Insert(0, new ListItem("Todos", "0"));
        }

        void buscarVendedor()
        {
            ddlVendedor.DataSource = dal.getBuscarUsuario(null, null);
            ddlVendedor.DataValueField = "ID_USUARIO";
            ddlVendedor.DataTextField = "USUARIO";
            ddlVendedor.DataBind();
        }

        void buscar()
        {
            DataTable dt = new DataTable();
            StringBuilder xmlGrafico = new StringBuilder();

            dt = dal.getGraficoMetas(ddlVendedor.SelectedValue, ddlMes.SelectedValue, ddlAno.SelectedValue).Tables[0];

            xmlGrafico.Append("<chart caption='Metas'>");
            foreach (DataRow item in dt.Rows)
            {
                xmlGrafico.Append("<set value='" + item["META_VENDIDO"].ToString() + "' label='META'/>");
                xmlGrafico.Append("<set value='" + item["AVANCE"].ToString() + "' label='AVANCE'/>");
                xmlGrafico.Append("<set value='" + item["MONTO_VENDIDO"].ToString() + "' label='VENDIDO'/>");
            }
            xmlGrafico.Append("</chart>");
            litGrafico.Text = FusionCharts.RenderChart("assets/FusionCharts/Column3D.swf", "", xmlGrafico.ToString(), "Metas", "100%", "350", false, true);
        }

    }
}