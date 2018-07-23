<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="AsignacionMaterialProducto.aspx.cs" Inherits="crm_valvulas_industriales.AsignacionMaterialProducto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Asignacion de materiales</li>
    </ol>

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-primary">
                    <div class="panel-heading">
                        <strong>Producto</strong>
                    </div>
                <table class="table table-condensed">
                    <tr>
                        <td>
                            <strong>Nombre</strong>
                            <asp:Label ID="lblNombreProducto" runat="server"></asp:Label>
                            <asp:HiddenField ID="hfIdProducto" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="panel panel-danger">
                    <div class="panel-heading">
                        <strong>Materiales asignados</strong>
                    </div>
                <asp:GridView ID="grvMateriales" runat="server" EmptyDataText="No hay materiales asignados a este producto" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <asp:Label ID="lblIdMaterial" runat="server" Text='<%# Bind("ID_MATERIAL") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblNombreMaterial" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("TIPO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Medida">
                        <ItemTemplate>
                            <asp:Label ID="lblMedida" runat="server" Text='<%# Bind("MEDIDA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Color">
                        <ItemTemplate>
                            <asp:Label ID="lblColor" runat="server" Text='<%# Bind("COLOR") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Fecha Ingreso">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaIngreso" runat="server" Text='<%# Bind("FECHA_INGRESO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad por producto">
                        <ItemTemplate>
                            <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%# Bind("CANTIDAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            

            <div class="panel panel-danger">
                <div class="panel-heading">
                    <strong>Seleccionar Materiales</strong>
                </div>
                <asp:GridView ID="grvMaterialesSeleccionar" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSeleccionar" runat="server" OnCheckedChanged="chkSeleccionar_CheckedChanged" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <asp:Label ID="lblIdMaterial" runat="server" Text='<%# Bind("ID_MATERIAL") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblNombreMaterial" runat="server" Text='<%# Bind("NOMBRE_MATERIAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad disponible">
                        <ItemTemplate>
                            <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%# Bind("CANTIDAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad seleccionada">
                        <ItemTemplate>
                            <asp:Label ID="lblCantidadSeleccionada" runat="server" Visible="false" Text='<%# Bind("CANTIDAD_SELECCIONADA") %>'></asp:Label>
                            <asp:TextBox ID="txtCantidadSeleccionada" Width="80px" runat="server" Enabled="false" CssClass="form-control input-sm" OnTextChanged="txtCantidadSeleccionada_TextChanged"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtCantidadSeleccionada" ValidChars="1234567890-" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                <div class="panel-footer">
                    <asp:Button ID="btnGrabar" runat="server" Text="Asignar Materiales" CssClass="btn btn-danger" OnClick="btnGrabar_Click" />
                </div>
            </div>

            

            <asp:Button ID="btnActivarPopUp" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" Style="display: none; background: white; width: 40%; height: auto">
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
                        <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/assets/img/accept.png" />

                    </div>
                </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
