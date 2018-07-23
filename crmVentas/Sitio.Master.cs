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
    public partial class Sitio : System.Web.UI.MasterPage
    {
        Datos dal = new Datos();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["variableUsuario"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {

                    }

                    //string perfil = Session["variablePerfil"].ToString();
                    //if (perfil != "Admin" && perfil != "SupVentas")
                    //{
                    //    liAdmin.Visible = false;
                    //}

                    //if (perfil != "Admin")
                    //{
                    //    liPanelGerencialSeguimiento.Visible = false;
                    //    liPanelGerencialGestionVenta.Visible = false;
                    //}
                    string rutaLogo = "";
                    DataTable dt = new DataTable();
                    dt = dal.getBuscarParametros(Session["idEmpresa"].ToString()).Tables[0];
                    foreach (DataRow item in dt.Rows)
                    {
                        rutaLogo = item["LOGO_MENU"].ToString();
                    }
                    imgLogo.ImageUrl = rutaLogo;

                    if (Session["variableUsuario"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        if (Session["idEmpresa"] == null)
                        {
                            buscarEmpresa(Session["variableIdUsuario"].ToString());
                            string idEmpresa = ddlEmpresaPorUsuario.SelectedValue;
                            Session["idEmpresa"] = idEmpresa;
                        }
                        else
                        {
                            buscarEmpresa(Session["variableIdUsuario"].ToString());
                            ddlEmpresaPorUsuario.SelectedValue = Session["idEmpresa"].ToString();
                        }
                    }

                    //1	Admin
                    //2	Gerencial
                    //3	Ejecutivo Ventas
                    //4	Control de Gestión
                    buscarPerfiles();
                    


                    lbtnFechaSession.Text = "<b>Sesion: </b>" + Session["variableFechaSession"].ToString();
                    lbtnUsuario.Text = "<b>Usuario: </b> " + Session["variableNombreEjecutivo"].ToString();
                }
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
                //Response.Redirect(ex.Message);
            }
        }

        void buscarPerfiles() 
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarUsuario(null, Session["variableIdUsuario"].ToString()).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                string perfil = item["ID_PERFIL"].ToString();

                if (perfil == "3")
                {
                    liAdmin.Visible = false;
                    liProcesos.Visible = false;
                    //liTareas.Visible = false;
                }
            }
            
        }


        protected void lbtnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Session["variableUsuario"] = null;
                Session["rutCliente"] = null;
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
                //Response.Redirect(ex.Message);
            }
        }

        protected void ddlEmpresaPorUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["idEmpresa"] = ddlEmpresaPorUsuario.SelectedValue;
                Response.Redirect("Default.aspx");
                //buscarImagen();
            }
            catch (Exception ex)
            {
                string ee = ex.Message;
            }
        }

        void buscarEmpresa(string idUsuario)
        {
            ddlEmpresaPorUsuario.DataSource = dal.getBuscarEmpresaPorUsuario(idUsuario);
            ddlEmpresaPorUsuario.DataValueField = "ID_EMPRESA";
            ddlEmpresaPorUsuario.DataTextField = "NOMBRE_EMPRESA";
            ddlEmpresaPorUsuario.DataBind();
        }
    }
}