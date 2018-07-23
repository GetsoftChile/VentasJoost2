<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="DetalleGestiones.aspx.cs" Inherits="crm_valvulas_industriales.DetalleGestiones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Control</a></li>
          <li><a href="Default.aspx">Gestiones</a></li>
          <li class="active">Detalle Gestiones</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="grvCantidadDeGestiones" EmptyDataText="Selección realizada no obtuvo registros" runat="server" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed table small panel" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Id Gestión">
                    <ItemTemplate>
                        <asp:Label ID="lblIdGestion" runat="server" Text='<%# Bind("ID_GESTION") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Id Lead">
                    <ItemTemplate>
                        <asp:Label ID="lblLead" runat="server" Text='<%# Bind("ID_LEAD") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Categoria">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoria" runat="server" Text='<%# Bind("ESTATUS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SubCategoria">
                    <ItemTemplate>
                        <asp:Label ID="lblSubCategoria" runat="server" Text='<%# Bind("SUB_ESTATUS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gestión">
                    <ItemTemplate>
                        <asp:Label ID="lblGestion" runat="server" Text='<%# Bind("ESTATUS_SEGUIMIENTO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Usuario">
                    <ItemTemplate>
                        <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obs">
                    <ItemTemplate>
                        <asp:Label ID="lblObservacion" runat="server" Text='<%# Bind("OBSERVACION") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fec. Ingreso">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaIngreso" runat="server" Text='<%# Bind("FECHA_INGRESO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fec Agend">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaAgendamiento" runat="server" Text='<%# Bind("FECHA_AGENDAMIENTO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hora Agend">
                    <ItemTemplate>
                        <asp:Label ID="lblHoraaAgendamiento" runat="server" Text='<%# Bind("HORA_AGENDAMIENTO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>


        </asp:GridView>


    
        
        <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" style="display:none; background:white; width:40%; height:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                  <div class="panel-header">
                        <button class="close" data-dismiss="modal">×</button>
                        <h3>Información</h3>
                  </div>
                  <div class="panel-body">
                    <div class="alert alert-error">
                        <asp:Label ID="lblInformacion" runat="server" Text=""></asp:Label>
                    </div>
                  </div>
                  <div class="panel-footer">
                    <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/assets/img/accept.png" />
                  </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
            </asp:Panel>


    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
