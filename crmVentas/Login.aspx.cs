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
    public partial class Login : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //txtRut.Focus();
                }
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
                //lblInformacion.Text = ex.Message;
                //mdlInformacion.Show();
            }
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string valor = dal.getValUsuario(txtRut.Text, txtPassword.Text);
                if (valor == "1")
                {
                    DataTable dt = dal.getBuscarUsuario(txtRut.Text, null).Tables[0];
                    string idUsuario = string.Empty;
                    string usuario = string.Empty;
                    
                    foreach (DataRow item in dt.Rows)
                    {
                        idUsuario = item["ID_USUARIO"].ToString();
                        Session["variablePerfil"] = item["ID_PERFIL"].ToString();
                        usuario = item["NOMBRE"].ToString();
                    }
                    
                    //foreach (DataRow item in dal.getBuscarUsuario(null, idUsuario).Tables[0].Rows)
                    //{
                    //    Session["variablePerfil"] = item["ID_PERFIL"].ToString();
                    //    usuario = item["NOMBRE"].ToString();
                    //    break;
                    //}
                    Session["variableIdUsuario"] = idUsuario;
                    Session["variableUsuario"] = txtRut.Text;
                    Session["variableFechaSession"] = DateTime.Now.ToString("G");
                    Session["variableNombreEjecutivo"] = usuario;

                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
                //lblInformacion.Text = ex.Message;
                //mdlInformacion.Show();
            }
        }
        
    }
}