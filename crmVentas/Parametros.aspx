<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Parametros.aspx.cs" Inherits="crm_fadonel.Parametros" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ol class="breadcrumb">
        <li><a href="Default.aspx">Inicio</a></li>
        <li><a href="Default.aspx">Administración</a></li>
        <li class="active">Parámetros</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div class="panel panel-primary">
        <div class="panel-heading">
            <strong>Parámetros</strong>
        </div>

        <table class="table table-condensed small">
        <tr class="active">
            <td>
                <strong>Nombre Sistema</strong>
                <asp:TextBox ID="txtNombreSistema" Width="30%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
            </td>
        </tr>
        <tr class="active">
            <td>
                <strong>Descuento Pago Contado</strong>
                <asp:TextBox ID="txtDescuento" Width="10%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
            </td>
        </tr>
        <tr class="active">
            <td>
                <strong>Vigencia Cot Dias</strong>
                <asp:TextBox ID="txtVigenciaDiasCot" Width="10%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
            </td>
        </tr>
        <tr class="active">
            <td>
                <strong>Logo</strong>
                <asp:FileUpload ID="fuImagen" Width="30%" CssClass="form-control input-sm" runat="server" />
                <asp:Image ID="imgImagen" Width="20%" runat="server" />
            </td>
        </tr>
        <tr class="active">
            <td>
                <asp:Button ID="btnGrabar" CssClass="btn btn-danger btn-sm" runat="server" Text="Grabar" onclick="btnGrabar_Click" />
            </td>
        </tr>
        </table>

    </div>




    

        
    <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
    <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
        <%--MODALPOPUP CON BOOTSTRAP--%>
        <div id="panelInformacion" runat="server" class="panel panel-red">
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
    </asp:Panel>


    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
