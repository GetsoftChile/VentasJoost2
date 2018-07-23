using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Text.RegularExpressions;

namespace crm_valvulas_industriales
{
    public partial class Asignacion : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    buscarUsuario();
                    buscarCampana();

                    string tipo = ddlTipoProceso.SelectedValue;
                    if (tipo == "USUARIO")
                    {
                        colUsuario.Visible = true;
                        colCampana.Visible = false;
                    }
                    if (tipo == "CAMPANA")
                    {
                        colUsuario.Visible = false;
                        colCampana.Visible = true;
                    }

                    if (tipo == "0")
                    {
                        colUsuario.Visible = false;
                        colCampana.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        void buscarUsuario()
        {
            grvUsuarios.DataSource = dal.getBuscarUsuario(null, "");
            grvUsuarios.DataBind();
        }

        protected void lbtnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                string idLogin = Session["variableIdUsuario"].ToString();
                string idEmpresa = Session["idEmpresa"].ToString();

                string tipo = ddlTipoProceso.SelectedValue;

                if (tipo == "0")
                {
                    lblInformacion.Text = "Favor seleccionar un tipo de proceso";
                    mdlInformacion.Show();
                    return;
                }

                if (tipo == "USUARIO")
                {
                    String[] numeroSiniestro = Regex.Split(txtRutClientes.Text, "\n");
                    String[] codigoUsuarios = Regex.Split(txtCodigosUsuario.Text, "\n");

                    int cantidadDeSiniestros = numeroSiniestro.Count();
                    int cantidadDeUsuarios = codigoUsuarios.Count();

                    if (cantidadDeSiniestros != cantidadDeUsuarios)
                    {
                        lblInformacion.Text = "La cantidad de registros de las 2 cajas no coinciden.";
                        mdlInformacion.Show();
                        return;
                    }

                    DataTable dt = new DataTable();
                    DataColumn nroSiniestro = dt.Columns.Add("RUT_CLIENTE", typeof(string));
                    DataColumn codigoUsuario = dt.Columns.Add("ID_USUARIO", typeof(string));

                    string primera = "";
                    string tercera = "";

                    for (int i = 0; i < numeroSiniestro.Length; i++)
                    {
                        primera = numeroSiniestro[i].Trim();
                        for (int r = i; r < codigoUsuarios.Length; r++)
                        {
                            tercera = codigoUsuarios[r].Trim();
                            dt.Rows.Add(primera, tercera);
                            break;
                        }
                    }

                    foreach (DataRow item in dt.Rows)
                    {
                        string idUsuario = item["ID_USUARIO"].ToString();
                        string siniestro = item["RUT_CLIENTE"].ToString();

                        dal.setEditarClienteAsignacionUsuario(idUsuario, siniestro);
                    }
                }

                if (tipo == "CAMPANA")
                {
                    String[] numeroSiniestro = Regex.Split(txtRutClientes.Text, "\n");
                    string idCampana = ddlCampana.SelectedValue;

                    string rutCliente = "";
                    for (int i = 0; i < numeroSiniestro.Length; i++)
                    {
                        rutCliente = numeroSiniestro[i].Trim();
                        dal.setEditarClienteCampana(rutCliente, ddlCampana.SelectedValue);
                        dal.setIngresarCampanaCliente(ddlCampana.SelectedValue, rutCliente);
                    }
                }

                if (tipo == "LEAD")
                {
                    String[] numeroSiniestro = Regex.Split(txtIdLead.Text, "\n");
                    String[] codigoUsuarios = Regex.Split(txtCodigosUsuario.Text, "\n");
                    
                    int cantidadDeSiniestros = numeroSiniestro.Count();
                    int cantidadDeUsuarios = codigoUsuarios.Count();

                    if (cantidadDeSiniestros != cantidadDeUsuarios)
                    {
                        lblInformacion.Text = "La cantidad de registros de las 2 cajas no coinciden.";
                        mdlInformacion.Show();
                        return;
                    }

                    DataTable dt = new DataTable();
                    DataColumn nroSiniestro = dt.Columns.Add("ID_LEAD", typeof(string));
                    DataColumn codigoUsuario = dt.Columns.Add("ID_USUARIO", typeof(string));

                    string primera = "";
                    string tercera = "";

                    for (int i = 0; i < numeroSiniestro.Length; i++)
                    {
                        primera = numeroSiniestro[i].Trim();
                        for (int r = i; r < codigoUsuarios.Length; r++)
                        {
                            tercera = codigoUsuarios[r].Trim();
                            dt.Rows.Add(primera, tercera);
                            break;
                        }
                    }

                    foreach (DataRow item in dt.Rows)
                    {
                        string idUsuario = item["ID_USUARIO"].ToString();
                        string siniestro = item["ID_LEAD"].ToString();

                        dal.setEditarAsignacionPorLead(Convert.ToInt32(idUsuario), Convert.ToInt32(siniestro));
                    }
                } 

                txtCodigosUsuario.Text = string.Empty;
                txtRutClientes.Text = string.Empty;
                txtIdLead.Text = string.Empty;
                lblInformacion.Text = "Proceso realizado correctamente";
                mdlInformacion.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlTipoProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tipo = ddlTipoProceso.SelectedValue;
                if (tipo == "USUARIO")
                {
                    colUsuario.Visible = true;
                    colCampana.Visible = false;

                    tdLead.Visible = false;
                    tdRutCliente.Visible = true;
                }

                if (tipo == "CAMPANA")
                {
                    colUsuario.Visible = false;
                    colCampana.Visible = true;

                    tdLead.Visible = false;
                    tdRutCliente.Visible = true;
                }

                if (tipo == "LEAD")
                {
                    tdLead.Visible = true;
                    tdRutCliente.Visible = false;
                    colUsuario.Visible = true;
                    colCampana.Visible = false;
                }

                if (tipo == "0")
                {
                    colUsuario.Visible = false;
                    colCampana.Visible = false;

                    tdLead.Visible = false;
                    tdRutCliente.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlCampana_DataBound(object sender, EventArgs e)
        {
            ddlCampana.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        protected void ddlTipoProceso_DataBound(object sender, EventArgs e)
        {
            ddlTipoProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccionar", "0"));
        }

        void buscarCampana()
        {
            ddlCampana.DataSource = dal.getBuscarCampana(null, "1");
            ddlCampana.DataValueField = "ID_CAMPANA";
            ddlCampana.DataTextField = "NOM_CAMPANA";
            ddlCampana.DataBind();
        }
    }
}