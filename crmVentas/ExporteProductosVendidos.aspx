<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="ExporteProductosVendidos.aspx.cs" Inherits="crm_valvulas_industriales.ExporteProductosVendidos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Paneles</a></li>
          <li class="active">Exporte Productos Vendidos</li>
    </ol>


    <div class="panel panel-primary">
        <div class="panel-heading">Productos Vendidos</div>
        <table class="table small">
            <tr>
                <td width="150px">
                    <strong>Rut Cliente</strong>
                    <asp:TextBox ID="txtRutCliente" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </td>
                <td width="150px">
                    <strong>Fecha Desde</strong>
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="ce_txtFechaDesde" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaDesde"></asp:CalendarExtender>
                </td>
                <td width="150px">
                    <strong>Fecha Hasta</strong>
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="ce_txtFechaHasta" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaHasta"></asp:CalendarExtender>
                </td>
                <td>
                    <br />
                    <asp:ImageButton ID="lbtnExporte" runat="server" 
                    ImageUrl="~/assets/img/export_excel.png" 
                    onclick="lbtnExporte_Click"/>
                </td>
            </tr>
        </table>
    </div>



    


        <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <div class="panel panel-red">
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

</asp:Content>
