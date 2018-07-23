<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="ExporteCotizacionesRechazadas.aspx.cs" Inherits="crm_valvulas_industriales.ExporteCotizacionesRechazadas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<div class="well well-sm">
<h4>Exporte Cotizaciones Rechazada</h4>
    <asp:ImageButton ID="ibtnExporteMotivoNoCompra" runat="server" 
        ImageUrl="~/assets/img/export_excel.png" 
        onclick="ibtnExporteMotivoNoCompra_Click"/>
</div>

</asp:Content>
