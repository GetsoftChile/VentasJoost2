<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Asignacion.aspx.cs" Inherits="crm_valvulas_industriales.Asignacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />

    <ol class="breadcrumb">
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Asignación</li>
    </ol>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div class="panel panel-info">
            <div class="panel-heading">
                <strong>Asignación de Clientes</strong>
            </div>
    
    <table class="table small">
        <tr class="active">
            <td>
                <strong>Tipo Proceso</strong>
                <asp:DropDownList ID="ddlTipoProceso" runat="server" Width="150px" 
                    CssClass="form-control input-sm" AutoPostBack="true" 
                    onselectedindexchanged="ddlTipoProceso_SelectedIndexChanged" OnDataBound="ddlTipoProceso_DataBound" >
                    <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Usuario - Cliente" Value="USUARIO"></asp:ListItem>
                    <asp:ListItem Text="Usuario - Lead" Value="LEAD"></asp:ListItem>
                    <asp:ListItem Text="Campañas" Value="CAMPANA"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr class="active">
            <td id="tdRutCliente" runat="server" visible="false">
                <strong>Rut Cliente</strong>
                <asp:TextBox ID="txtRutClientes" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="200px"></asp:TextBox>
            </td>
            <td id="tdLead" runat="server" visible="false">
                <strong>Id Lead</strong>
                <asp:TextBox ID="txtIdLead" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="200px"></asp:TextBox>
            </td>
            <td runat="server" id="colUsuario">
                <strong>Usuario(Código)</strong>
                <asp:TextBox ID="txtCodigosUsuario" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="200px"></asp:TextBox>
            </td>
            <td runat="server" id="colCampana">
                <strong>Campaña</strong>
                <asp:DropDownList ID="ddlCampana" runat="server" Width="350px" 
                    CssClass="form-control input-sm" AutoPostBack="true" 
                     >
                
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="active">
            <td colspan="3">
                <asp:LinkButton ID="lbtnGrabar" CssClass="btn btn-danger btn-sm" runat="server" 
            onclick="lbtnGrabar_Click">Procesar</asp:LinkButton>
            </td>
        </tr>
        </table>
        </div>


        
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>Usuarios</strong>
            </div>
                
            <asp:GridView ID="grvUsuarios" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Bind("ID_USUARIO") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Nombres">
                            <ItemTemplate>
                                <asp:Label ID="lblNombres" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Login">
                            <ItemTemplate>
                                <asp:Label ID="lblLogin" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
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
