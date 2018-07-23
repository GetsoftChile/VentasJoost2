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
    public partial class IngresoGestionLead : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    buscarEstatus("0");
                    buscarSubEstatus(ddlEstatus.SelectedValue);
                    buscarEstatusSeguimiento(ddlEstatus.SelectedValue, ddlSubEstatus.SelectedValue);

                    comuna();

                    string _idLead = Convert.ToString(Request.QueryString["id"]);
                    
                    if (_idLead != null)
                    {
                        hfIdLead.Value = _idLead;
                        buscarGestionesCRM(_idLead);

                        DataTable dt = new DataTable();
                        dt = dal.getBuscarLead(Convert.ToInt32(_idLead), null,null).Tables[0];

                        foreach (DataRow item in dt.Rows)
                        {
                            txtNombre.Text = item["NOMBRE"].ToString();
                            txtCargo.Text = item["CARGO"].ToString();
                            txtComentario.Text = item["Comentario"].ToString();
                            txtDireccion.Text = item["DIRECCION"].ToString();
                            txtEmail.Text = item["EMAIL"].ToString();
                            txtEmpresa.Text = item["EMPRESA"].ToString();
                            txtTelefono1.Text = item["TELEFONO_1"].ToString();
                            txtTelefono2.Text = item["TELEFONO_2"].ToString();

                            if (item["COMUNA"].ToString() != string.Empty)
                            {
                                ddlComuna.SelectedValue = item["COMUNA"].ToString();
                            }

                            if (item["TIENE_SW_ERP"].ToString() != string.Empty)
                            {
                                ddlTieneSwERP.SelectedValue = item["TIENE_SW_ERP"].ToString();
                            }

                            if (item["TIENE_SW_COBROS"].ToString() != string.Empty)
                            {
                                ddlTieneSwCobros.SelectedValue = item["TIENE_SW_COBROS"].ToString();
                            }
                            if (item["TIENE_SW_TICKET"].ToString() != string.Empty)
                            {
                                ddlTieneSwTicket.SelectedValue = item["TIENE_SW_TICKET"].ToString();
                            }
                            if (item["TIENE_SW_VENTAS"].ToString() != string.Empty)
                            {
                                ddlTieneSwVentas.SelectedValue = item["TIENE_SW_VENTAS"].ToString();
                            }

                            txtSwCobros.Text = item["SW_COBROS"].ToString();
                            txtSwErp.Text = item["SW_ERP"].ToString();
                            txtSwTicket.Text= item["SW_TICKET"].ToString();
                            txtSwVentas.Text= item["SW_VENTAS"].ToString();

                            string idUsuarioAsig= item["ID_USUARIO_ASIG"].ToString();
                            hfIdUsuarioAsignado.Value = idUsuarioAsig;
                        }

                        //txtNombre.Enabled = false;
                        //txtCargo.Enabled = false;
                        //txtComentario.Enabled = false;
                        //txtDireccion.Enabled = false;
                        //txtEmail.Enabled = false;
                        //txtEmpresa.Enabled = false;
                        //txtTelefono1.Enabled = false;
                        //txtTelefono2.Enabled = false;
                        //ddlComuna.Enabled = false;
                        //ddlTieneSwERP.Enabled = false;
                        //ddlTieneSwCobros.Enabled = false;
                        //ddlTieneSwTicket.Enabled = false;
                        //ddlTieneSwVentas.Enabled = false;

                        //ddlActivo.Enabled = false;
                    }
                 }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        void comuna()
        {
            ddlComuna.DataSource = dal.getBuscarComuna();
            ddlComuna.DataValueField = "NOM_COMUNA";
            ddlComuna.DataTextField = "NOM_COMUNA";
            ddlComuna.DataBind();
        }
        void buscarEstatus(string flagCartera)
        {
            string idEmpresa = Session["idEmpresa"].ToString();

            ddlEstatus.DataSource = dal.getBuscarEstatus(null, null,"L");
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

        protected void ddlComuna_DataBound(object sender, EventArgs e)
        {
            ddlComuna.Items.Insert(0, new ListItem("Seleccionar", "0"));
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
                if (ddlEstatus.SelectedValue == "0" || ddlSubEstatus.SelectedValue == "0" || ddlEstatusSeguimiento.SelectedValue == "0")
                {
                    lblInformacion.Text = "Debe seleccionar algun estatus";
                    mdlInformacion.Show();
                    return;
                }

                string codGestion = ddlEstatus.SelectedValue + "-" + ddlSubEstatus.SelectedValue + "-" + ddlEstatusSeguimiento.SelectedValue;

                string nombre = Session["variableUsuario"].ToString();
                string idUsuario = Session["variableIdUsuario"].ToString();

                string hora = txtHora.Text;
                string fechaAgend = txtFecAgendamiento.Text;

                string idEmpresa = Session["idEmpresa"].ToString();
                DataTable dt = new DataTable();
                dt = dal.getBuscarAgendamiento(ddlEstatus.SelectedValue, ddlSubEstatus.SelectedValue, ddlEstatusSeguimiento.SelectedValue, idEmpresa).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    if (item["CAMBIA_ESTADO_LEAD"].ToString() != string.Empty)
                    {
                        dal.setEditarLeadEstado(Convert.ToInt32(item["CAMBIA_ESTADO_LEAD"].ToString()), Convert.ToInt32(hfIdLead.Value));
                    }
                }

                DateTime dateFechaAgend = new DateTime();
                if (fechaAgend != string.Empty)
                {
                    dateFechaAgend = Convert.ToDateTime(fechaAgend + " " + hora);
                    DateTime Hoy = DateTime.Now;
                    if (dateFechaAgend < Hoy)
                    {
                        lblInformacion.Text = "Favor seleccionar una fecha superior a la de hoy";
                        mdlInformacion.Show();
                        return;
                    }

                    if (hora == string.Empty)
                    {
                        lblInformacion.Text = "Favor ingresar la hora de agendamiento, ej:14:00";
                        mdlInformacion.Show();
                        return;
                    }
                }

                string idGestion = dal.setIngresarGestionLead(hfIdLead.Value,ddlEstatus.SelectedValue,
                    ddlSubEstatus.SelectedValue, idUsuario, txtFecAgendamiento.Text, hora, txtObservacion.Text, "", 
                    ddlEstatusSeguimiento.SelectedValue, txtFechaVisita.Text, null);

                string idGestionLead = ddlEstatus.SelectedValue + ddlSubEstatus.SelectedValue + ddlEstatusSeguimiento.SelectedValue;
                dal.setEditarLeadPorId(Convert.ToInt32(hfIdLead.Value), idGestionLead);

                ddlEstatus.ClearSelection();
                ddlSubEstatus.ClearSelection();
                ddlEstatusSeguimiento.ClearSelection();
                ddlMotivoNoCompra.ClearSelection();
                txtFecAgendamiento.Text = string.Empty;
                txtHora.Text = string.Empty;
                txtFechaVisita.Text = string.Empty;
                txtObservacion.Text = string.Empty;
                hfVieneSeguimiento.Value = string.Empty;

                buscarGestionesCRM(hfIdLead.Value);

                lblInformacion.Text = "<strong>Correcto!</strong> La gestión se ingresó correctamente.";
                mdlInformacion.Show();
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

        void buscarGestionesCRM(string idLead)
        {
            DataTable dt = dal.getBuscarGestionesLead(idLead).Tables[0];

            grvHistorialGestiones.DataSource = dt;
            grvHistorialGestiones.DataBind();
        }

        protected void btnModificarLead_Click(object sender, EventArgs e)
        {
            try
            {
                string idUsuario = Session["variableIdUsuario"].ToString();
                string idUsuarioAsig = hfIdUsuarioAsignado.Value;
                dal.setEditarLead(Convert.ToInt32(hfIdLead.Value), txtNombre.Text, txtEmpresa.Text, txtCargo.Text,
                    txtDireccion.Text, ddlComuna.SelectedValue, txtEmail.Text, txtTelefono1.Text, txtTelefono2.Text,
                    Convert.ToInt32(ddlTieneSwERP.SelectedValue), Convert.ToInt32(ddlTieneSwCobros.SelectedValue),
                    Convert.ToInt32(ddlTieneSwTicket.SelectedValue), Convert.ToInt32(ddlTieneSwVentas.SelectedValue),
                    txtComentario.Text, Convert.ToInt32(ddlActivo.SelectedValue), Convert.ToInt32(idUsuarioAsig),
                    txtSwErp.Text,txtSwCobros.Text,txtSwVentas.Text,txtSwTicket.Text);

                string _idLead = Convert.ToString(Request.QueryString["id"]);
                Response.Redirect("IngresoGestionLead.aspx?id="+ _idLead);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}