<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="MaterialesUsados.aspx.cs" Inherits="crm_valvulas_industriales.MaterialesUsados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ol class="breadcrumb">
        <li><a href="Default.aspx">Inicio</a></li>
        <li><a href="Default.aspx">Administración</a></li>
        <li class="active">Materiales Usados</li>
    </ol>

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-primary">
                    <div class="panel-heading">
                        <strong>Materiales Usados</strong>
                    </div>
                <table class="table table-condensed small">
                    <tr class="success">
                        
                        <td width="150px">
                            <strong>Fecha Desde</strong>
                            <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="ce_txtFechaDesdeAgendamiento" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaDesde"></asp:CalendarExtender>
                        </td>
                        <td width="150px">
                            <strong>Fecha Hasta</strong>
                            <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaHasta"></asp:CalendarExtender>
                        </td>
                        
                        <td>
                            <br />
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm" Text="Buscar" OnClick="btnBuscar_Click"/>
                        </td>
                    </tr>
                </table>
            </div>

            
        <asp:GridView ID="grvMateriales" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" >
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblIdMaterial" runat="server" Text='<%# Bind("ID_MATERIAL") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
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
                        
                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblStock" runat="server" Text='<%# Bind("CANTIDAD") %>'></asp:Label>
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Cantidad Usada">
                            <ItemTemplate>
                                <asp:Label ID="lblCantidadUsada" runat="server" Text='<%# Bind("cantidadUsada") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            
        
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
