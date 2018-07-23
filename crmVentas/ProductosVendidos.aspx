<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="ProductosVendidos.aspx.cs" Inherits="crm_valvulas_industriales.ProductosVendidos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ol class="breadcrumb">
        <li><a href="Default.aspx">Inicio</a></li>
        <li><a href="Default.aspx">Administración</a></li>
        <li class="active">Productos Vendidos</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                    <div class="panel-heading">
                        <strong>Productos Vendidos</strong>
                    </div>
                <table class="table table-condensed small">
                    <tr class="success">
                        <td>
                            <strong>Empresas</strong>
                            <asp:DropDownList ID="ddlEmpresa" runat="server" OnDataBound="ddlEmpresa_DataBound" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </td>
                        
                        <td>
                            <strong>Fecha Desde</strong>
                            <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="ce_txtFechaDesdeAgendamiento" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaDesde"></asp:CalendarExtender>
                        </td>
                        <td>
                            <strong>Fecha Hasta</strong>
                            <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaHasta"></asp:CalendarExtender>
                        </td>
                        
                        <td>
                            <br />
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm" Text="Buscar" OnClick="btnBuscar_Click" />
                        </td>
                    </tr>
                </table>
            </div>


            
        <asp:GridView ID="grvProductos" runat="server" CssClass="table table-bordered table-hover table-condensed table small"
             HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblIdProducto" runat="server" Text='<%# Bind("ID_PRODUCTO") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblNombreProducto" runat="server" Text='<%# Bind("NOM_PRODUCTO") %>'></asp:Label>

                                <asp:Label ID="lblRutaFichaTecnica" runat="server" Text='<%# Bind("FICHA_TECNICA_PDF") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblRutaImagen" runat="server" Text='<%# Bind("IMAGEN") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CODIGO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bodega">
                            <ItemTemplate>
                                <asp:Label ID="lblBodega" runat="server" Text='<%# Bind("BODEGA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Grupo">
                            <ItemTemplate>
                                <asp:Label ID="lblGrupo" runat="server" Text='<%# Bind("NOM_GRUPO") %>'></asp:Label>
                                <asp:Label ID="lblIdGrupo" runat="server" Visible="false" Text='<%# Bind("ID_GRUPO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Sub Grupo">
                            <ItemTemplate>
                                <asp:Label ID="lblSubGrupo" runat="server" Text='<%# Bind("NOM_SUB_GRUPO") %>'></asp:Label>
                                <asp:Label ID="lblIdSubGrupo" runat="server" Visible="false" Text='<%# Bind("ID_SUB_GRUPO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Uni Medida">
                            <ItemTemplate>
                                <asp:Label ID="lblUnidadMedida" runat="server" Text='<%# Bind("U_MEDIDA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Costo Unitario">
                            <ItemTemplate>
                                <asp:Label ID="lblCostoUnitario" runat="server" Text='<%# Bind("COSTO_UNITARIO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Venta">
                            <ItemTemplate>
                                <asp:Label ID="lblValorVenta" runat="server" Text='<%# Bind("VALOR_VENTA") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblStock" runat="server" Text='<%# Bind("STOCK") %>'></asp:Label>
                                <asp:Label ID="lblObservacion" runat="server" Visible="false" Text='<%# Bind("OBSERVACION_PROD") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cantidad Vendida">
                            <ItemTemplate>
                                <asp:Label ID="lblCantidadVendida" runat="server" Text='<%# Bind("cantidad") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            
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
