﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using DataTableToExcel;
using System.Text;

namespace crm_valvulas_industriales
{
    public partial class SeguimientoLead : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.ibtnExportar);

                if (!this.Page.IsPostBack)
                {
                    //comuna();
                    ejecutivoAsignado();
                    comuna();
                    string idPerfil = Session["variablePerfil"].ToString();
                    string idUsuario = Session["variableIdUsuario"].ToString();
                    if (idPerfil!="1")
                    {
                        ddlEjecutivoAsignado.SelectedValue = idUsuario;
                        ddlEjecutivoAsignado.Enabled = false;
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
        protected void ddlUsuarioAsignado_DataBound(object sender, EventArgs e)
        {
            ddlUsuarioAsignado.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        void ejecutivoAsignado() {
            ddlEjecutivoAsignado.DataSource = dal.getBuscarUsuario(null, null);
            ddlEjecutivoAsignado.DataTextField = "USUARIO";
            ddlEjecutivoAsignado.DataValueField = "ID_USUARIO";
            ddlEjecutivoAsignado.DataBind();

            ddlUsuarioAsignado.DataSource = dal.getBuscarUsuario(null, null);
            ddlUsuarioAsignado.DataValueField = "ID_USUARIO";
            ddlUsuarioAsignado.DataTextField = "USUARIO";
            ddlUsuarioAsignado.DataBind();
        }
        
        void buscar()
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarLead(null, null,Convert.ToInt32(ddlEjecutivoAsignado.SelectedValue)).Tables[0];
            Session["DatosListadoLeadGestiones"] = dt;
            grvLead.DataSource = dt;
            grvLead.DataBind();
        }
        
        protected void imgFirst_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoLeadGestiones"] != null)
            {
                grvLead.DataSource = Session["DatosListadoLeadGestiones"];
                grvLead.DataBind();
            }
            else
            {
                buscar();
            }

            grvLead.PageIndex = 0;
            grvLead.DataBind();
        }

        protected void imgPrev_Click(object sender, EventArgs e)
        {
            //buscar();

            if (Session["DatosListadoLeadGestiones"] != null)
            {
                grvLead.DataSource = Session["DatosListadoLeadGestiones"];
                grvLead.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvLead.PageIndex != 0)
                grvLead.PageIndex--;
            grvLead.DataBind();
        }

        protected void imgNext_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoLeadGestiones"] != null)
            {
                grvLead.DataSource = Session["DatosListadoLeadGestiones"];
                grvLead.DataBind();
            }
            else
            {
                buscar();
            }

            if (grvLead.PageIndex != (grvLead.PageCount - 1))
                grvLead.PageIndex++;
            grvLead.DataBind();
        }

        protected void imgLast_Click(object sender, EventArgs e)
        {
            //buscar();
            if (Session["DatosListadoLeadGestiones"] != null)
            {
                grvLead.DataSource = Session["DatosListadoLeadGestiones"];
                grvLead.DataBind();
            }
            else
            {
                buscar();
            }

            grvLead.PageIndex = grvLead.PageCount - 1;
            grvLead.DataBind();
        }

        protected void paginacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label _lblPagina = (Label)e.Row.FindControl("lblPagina");
                Label _lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label _lblTotalRegistros = (Label)e.Row.FindControl("lblTotalRegistros");
                _lblPagina.Text = Convert.ToString(grvLead.PageIndex + 1);
                _lblTotal.Text = Convert.ToString(grvLead.PageCount);

                DataTable dt = new DataTable();
                dt = Session["DatosListadoLeadGestiones"] as DataTable;
                //_lblTotalRegistros.Text = dt.Rows.Count.ToString();

                //Session["DatosCarteraAsignada"]
                //_lblTotal.Text = 
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

        protected void ddlComuna_DataBound(object sender, EventArgs e)
        {
            ddlComuna.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        void comuna()
        {
            ddlComuna.DataSource = dal.getBuscarComuna();
            ddlComuna.DataValueField = "NOM_COMUNA";
            ddlComuna.DataTextField = "NOM_COMUNA";
            ddlComuna.DataBind();
        }

        protected void imgNuevaGestion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //mdlIngresoGestion.Show();
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdLead = (Label)grvLead.Rows[row.RowIndex].FindControl("lblIdLead");
                Response.Redirect("IngresoGestionLead.aspx?id=" + _lblIdLead.Text);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ibtnExportar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dal.getBuscarLeadSeguimientoExporte(null, null, Convert.ToInt32(ddlEjecutivoAsignado.SelectedValue)).Tables[0];

                Response.ContentType = "Application/x-msexcel";
                Response.AddHeader("content-disposition", "attachment;filename=" + "export_seguimientoLead" + ".csv");
                Response.ContentEncoding = Encoding.Unicode;
                Response.Write(Utilidad.ExportToCSVFile(dt));
                Response.End();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void imgNuevoCliente_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdLead = (Label)grvLead.Rows[row.RowIndex].FindControl("lblIdLead");
                Response.Redirect("Cliente.aspx?idLead=" + _lblIdLead.Text);
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void ddlEjecutivoAsignado_DataBound(object sender, EventArgs e)
        {
            ddlEjecutivoAsignado.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void ddlEjecutivoAsignado_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                btnGrabarLead.Visible = false;
                btnModificarLead.Visible = true;

                lblAgregarLead.Text = "Editar Lead";
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                Label _lblIdLead = (Label)grvLead.Rows[row.RowIndex].FindControl("lblIdLead");
                Label _lblNombre = (Label)grvLead.Rows[row.RowIndex].FindControl("lblNombre");
                Label _lblEmpresa = (Label)grvLead.Rows[row.RowIndex].FindControl("lblEmpresa");
                Label _lblCargo = (Label)grvLead.Rows[row.RowIndex].FindControl("lblCargo");
                Label _lblDireccion = (Label)grvLead.Rows[row.RowIndex].FindControl("lblDireccion");
                Label _lblComuna = (Label)grvLead.Rows[row.RowIndex].FindControl("lblComuna");
                Label _lblEmail = (Label)grvLead.Rows[row.RowIndex].FindControl("lblEmail");
                Label _lblTelefono1 = (Label)grvLead.Rows[row.RowIndex].FindControl("lblTelefono1");
                Label _lblTelefono2 = (Label)grvLead.Rows[row.RowIndex].FindControl("lblTelefono2");
                Label _lblTieneSwERP = (Label)grvLead.Rows[row.RowIndex].FindControl("lblTieneSwERP");
                Label _lblTieneSwCobros = (Label)grvLead.Rows[row.RowIndex].FindControl("lblTieneSwCobros");
                Label _lblTieneSwTicket = (Label)grvLead.Rows[row.RowIndex].FindControl("lblTieneSwTicket");
                Label _lblTieneSwVentas = (Label)grvLead.Rows[row.RowIndex].FindControl("lblTieneSwVentas");

                Label _lblComentario = (Label)grvLead.Rows[row.RowIndex].FindControl("lblComentario");
                Label _lblIdUsuarioAsignado = (Label)grvLead.Rows[row.RowIndex].FindControl("lblIdUsuarioAsignado");

                limpiar(this.Controls);

                hfIdLead.Value = _lblIdLead.Text;
                txtNombre.Text = _lblNombre.Text;
                txtCargo.Text = _lblCargo.Text;
                txtComentario.Text = _lblComentario.Text;
                txtDireccion.Text = _lblDireccion.Text;
                txtEmail.Text = _lblEmail.Text;
                txtEmpresa.Text = _lblEmpresa.Text;
                txtTelefono1.Text = _lblTelefono1.Text;
                txtTelefono2.Text = _lblTelefono2.Text;

                if (_lblComuna.Text != string.Empty)
                {
                    ddlComuna.SelectedValue = _lblComuna.Text;
                }

                if (_lblTieneSwERP.Text != string.Empty)
                {
                    ddlTieneSwERP.SelectedValue = _lblTieneSwERP.Text;
                }

                if (_lblTieneSwCobros.Text != string.Empty)
                {
                    ddlTieneSwCobros.SelectedValue = _lblTieneSwCobros.Text;
                }
                if (_lblTieneSwTicket.Text != string.Empty)
                {
                    ddlTieneSwTicket.SelectedValue = _lblTieneSwTicket.Text;
                }
                if (_lblTieneSwVentas.Text != string.Empty)
                {
                    ddlTieneSwVentas.SelectedValue = _lblTieneSwVentas.Text;
                }

                if (_lblIdUsuarioAsignado.Text != string.Empty)
                {
                    ddlUsuarioAsignado.SelectedValue = _lblIdUsuarioAsignado.Text;
                }

                mdlAgregarLead.Show();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
        
        protected void btnModificarLead_Click(object sender, EventArgs e)
        {
            try
            {
                string idUsuario = Session["variableIdUsuario"].ToString();
                string idUsuarioAsig = ddlUsuarioAsignado.SelectedValue;
                //dal.setEditarLead(Convert.ToInt32(hfIdLead.Value), txtNombre.Text, txtEmpresa.Text, txtCargo.Text,
                //    txtDireccion.Text, ddlComuna.SelectedValue, txtEmail.Text, txtTelefono1.Text, txtTelefono2.Text,
                //    Convert.ToInt32(ddlTieneSwERP.SelectedValue), Convert.ToInt32(ddlTieneSwCobros.SelectedValue),
                //    Convert.ToInt32(ddlTieneSwTicket.SelectedValue), Convert.ToInt32(ddlTieneSwVentas.SelectedValue),
                //    txtComentario.Text, Convert.ToInt32(ddlActivo.SelectedValue), Convert.ToInt32(idUsuarioAsig));

                buscar();
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void btnBuscarPorLead_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombreLead.Text.Trim();

                DataTable dt = new DataTable();
                dt = dal.getBuscarLead(null, nombre, null).Tables[0];
                Session["DatosListadoLeadGestiones"] = dt;
                grvLead.DataSource = dt;
                grvLead.DataBind();

            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }
    }
}