<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="GrafPanelSupervisor.aspx.cs" Inherits="crm_valvulas_industriales.GrafPanelSupervisor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script  type="text/javascript" language="javascript" src="assets/JSClass/FusionCharts.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Paneles</a></li>
          <li class="active">Panel Supervisor</li>
    </ol>
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div class="panel panel-primary">
                <div class="panel-heading">
                    Panel Supervisor
                </div>
                <div class="panel-body">
                    <div class="form-inline">

                    <asp:DropDownList ID="ddlPesoOQ" runat="server" CssClass="form-control input-sm">
                    <asp:ListItem Text="Q" Value="Q"></asp:ListItem>
                    <asp:ListItem Text="$" Value="P"></asp:ListItem>
                    </asp:DropDownList>

                    <strong>Fecha Desde</strong>
                    <asp:TextBox ID="txtFechaDesdeIndicador" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="ce_txtFechaDesdeIndicador" runat="server" 
                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                    TargetControlID="txtFechaDesdeIndicador"></asp:CalendarExtender>
                    <strong>Fecha Hasta</strong>
                    <asp:TextBox ID="txtFechaHastaIndicador" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="ce_txtFechaHastaIndicador" runat="server" 
                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                    TargetControlID="txtFechaHastaIndicador"></asp:CalendarExtender>
                    <strong>Vendedor</strong>
                    <asp:DropDownList ID="ddlVendedorIndicador" CssClass="form-control input-sm" runat="server" OnDataBound="ddlVendedorIndicador_DataBound">
                    </asp:DropDownList>
                    <asp:Button ID="btnBuscarIndicador" runat="server" CssClass="btn btn-danger btn-sm" 
                        Text="Buscar" onclick="btnBuscarIndicador_Click" />
                    </div>
                </div>
            </div>


            <h2>Resumen</h2>
            <table class="table text-center">

            <tr>
                <td></td>
                <td align="center" colspan="2">
                    <div class="img-circle" style="background-image:url('assets/img/rectangulo-azul.png'); height:113px; width:150px;">
                    <br /><br />
                    <span style="color:White;">Mis Cotizaciones</span><br />
                        <asp:LinkButton ID="lbtnMisCotizaciones" runat="server" ForeColor="White" Font-Bold="true" OnClick="lbtnMisCotizaciones_Click" Text="50"></asp:LinkButton>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="center">
                    <div class="img-circle" runat="server" id="divMisVentasGanadas" visible="true" style="background-image:url('assets/img/rectangulo-verde.png'); height:113px; width:150px;">
                    <br /><br />
                    <span style="color:White;">Mis ventas ganadas</span><br />
                        <asp:LinkButton ID="lbtnMisVentasGanadas" runat="server" ForeColor="White" Font-Bold="true" OnClick="lbtnMisVentasGanadas_Click" Text="50"></asp:LinkButton>
                    </div>

                    
                </td>
                <td align="center">
                    <div class="img-circle" style="background-image:url('assets/img/rectangulo-verde.png'); height:113px; width:150px;">
                    <br /><br />
                    <span style="color:White;">Mis Seguimientos</span><br />
                        <asp:LinkButton ID="lbtnMisSeguimientos" runat="server" ForeColor="White" Font-Bold="true" Text="50" OnClick="lbtnMisSeguimientos_Click"></asp:LinkButton>
                    </div>
                </td>
                <td align="center">
                    <div class="img-circle" style="background-image:url('assets/img/rectangulo-verde.png'); height:113px; width:150px;">
                    <br />
                    <span style="color:White;">Mis negocios <br /> perdidos</span><br />
                        <asp:LinkButton ID="lbtnMisNegociosPerdidos" runat="server" ForeColor="White" Font-Bold="true" Text="50" OnClick="lbtnMisNegociosPerdidos_Click"></asp:LinkButton>
                    </div>
                </td>

                <td align="center">
                    <div class="img-circle" style="background-image:url('assets/img/rectangulo-verde.png'); height:113px; width:150px;">
                    <br /><br />
                    <span style="color:White;">Sin Seguimiento</span><br />
                        <asp:LinkButton ID="lbtnSinSeguimiento" runat="server" ForeColor="White" Font-Bold="true" Text="" OnClick="lbtnSinSeguimiento_Click"></asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td align="center" colspan="2">
                    <div class="img-circle" style="background-image:url('assets/img/rectangulo-amarillo.png'); height:113px; width:150px;">
                    <br /><br />
                    <span style="color:White;">Mi efectividad</span><br />
                        <asp:LinkButton ID="lbtnMiEfectividad" runat="server" ForeColor="White" Font-Bold="true" Text="50"></asp:LinkButton>
                    </div>
                </td>
                <td></td>
            </tr>
            </table>


            

        <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <div class="panel panel-danger">
            <div class="panel-heading">
                <button class="close" data-dismiss="modal">×</button>
                    <strong>Información</strong>
            </div>
                <div class="alert">
                    <asp:Label ID="lblInformacion" runat="server" Text=""></asp:Label>
                </div>
            <div class="panel-footer">
                <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/assets/img/accept.png"/>
                    
            </div>
            </div>
            <%--MODALPOPUP CON BOOTSTRAP--%>
        </asp:Panel>

    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
