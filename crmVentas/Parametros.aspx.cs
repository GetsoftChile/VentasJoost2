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
    public partial class Parametros : System.Web.UI.Page
    {
        Datos dal = new Datos();
        public string rutaLogo = "assets/img/imagenesEmpresa/";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnGrabar);
                
                if (!Page.IsPostBack)
                {
                    string idEmpresa = Session["idEmpresa"].ToString();
                    buscar();
                }
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
                string idEmpresa = Session["idEmpresa"].ToString();

                if (fuImagen.HasFile)
                {
                    string ruta = rutaLogo + idEmpresa + "_" + fuImagen.FileName;
                    fuImagen.SaveAs(Server.MapPath(ruta));
                    dal.setEditarParametro(idEmpresa, txtNombreSistema.Text, ruta, txtVigenciaDiasCot.Text, txtDescuento.Text.Replace(",","."));
                }
                else
                {
                    dal.setEditarParametro(idEmpresa, txtNombreSistema.Text, null, txtVigenciaDiasCot.Text, txtDescuento.Text.Replace(",", "."));
                }

                buscar();

                lblInformacion.Text = "El parámetro se guardó correctamete.";
                mdlInformacion.Show();
                
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
            
            dt = dal.getBuscarParametros(idEmpresa).Tables[0];

            if (dt.Rows.Count == 0)
            {
                dal.setIngresarParametro(idEmpresa, null, null, null, null);
            }


            foreach (DataRow item in dt.Rows)
            {
                txtNombreSistema.Text = item["NOMBRE_SISTEMA"].ToString();
                txtVigenciaDiasCot.Text = item["VIGENCIA_COTIZACION_DIAS"].ToString();
                txtDescuento.Text = item["DESCUENTO_PAGO_CONTADO"].ToString();

                if (item["LOGO_MENU"].ToString() == string.Empty)
                {
                    imgImagen.ImageUrl = string.Empty;
                    imgImagen.Visible = false;
                }
                else
                {
                    imgImagen.ImageUrl = item["LOGO_MENU"].ToString();
                    imgImagen.Visible = true;
                }
            }
        }
    }
}