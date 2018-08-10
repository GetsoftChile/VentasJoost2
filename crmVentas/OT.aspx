<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="OT.aspx.cs" Inherits="crm_valvulas_industriales.OT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <ol class="breadcrumb">
        <li><a href="Default.aspx">Inicio</a></li>
        <li><a href="Default.aspx">Administración</a></li>
        <li class="active">OT</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <strong>OT</strong>
                </div>
                <table class="table table-condensed small">
                    <tr class="warning">
                        <td>
                            <strong>Empresas</strong>
                            <asp:DropDownList ID="ddlEmpresa" runat="server" OnDataBound="ddlEmpresa_DataBound" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <strong>Vendedor</strong>
                            <asp:DropDownList ID="ddlVendedor" runat="server" OnDataBound="ddlVendedor_DataBound" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <strong>Fecha Entrega Desde</strong>
                            <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="ce_txtFechaDesdeAgendamiento" runat="server"
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                TargetControlID="txtFechaDesde">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <strong>Fecha Entrega Hasta</strong>
                            <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                TargetControlID="txtFechaHasta">
                            </asp:CalendarExtender>
                        </td>

                        <td>
                            <br />
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm" Text="Buscar" OnClick="btnBuscar_Click" />
                        </td>
                        <td>
                            <br />
                            <asp:ImageButton ID="ibtnExportarExcel" runat="server"
                                ImageUrl="~/assets/img/export_excel.png" OnClick="ibtnExportarExcel_Click" />
                        </td>
                    </tr>
                </table>
            </div>


            <div class="panel panel-warning">
                <div class="panel-heading">
                    <strong>OT</strong>
                </div>

                <asp:GridView ID="grvOT" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small"
                    AutoGenerateColumns="false" AllowPaging="True" PageSize="50" OnRowDataBound="paginacion_RowDataBound" OnSorting="gvEmployee_Sorting" AllowSorting="true">
                    <Columns>
                        <asp:TemplateField HeaderText="OT" SortExpression="ID_ORDEN_TRABAJO">
                            <ItemTemplate>
                                <asp:Label ID="lblIdOrdenDeTrabajo" runat="server" Visible="true" Text='<%# Bind("ID_ORDEN_TRABAJO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empresa" SortExpression="NOMBRE_EMPRESA">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpresa" runat="server" Visible="true" Text='<%# Bind("NOMBRE_EMPRESA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Rut Cliente" SortExpression="RUT_CLIENTE">
                            <ItemTemplate>
                                <asp:Label ID="lblRutCliente" runat="server" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha creacion" SortExpression="FECHA">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaOT" runat="server" Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha entrega" SortExpression="FECHA_ENTREGA">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaEntrega" runat="server" Text='<%# Bind("FECHA_ENTREGA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado OT">
                            <ItemTemplate>
                                <asp:Label ID="lblEstadoOT" runat="server" Text='<%# Bind("NOMBRE_ESTADO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Usuario Asig" SortExpression="USUARIO">
                            <ItemTemplate>
                                <asp:Label ID="lblUsuarioAsig" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cot" SortExpression="ID_COTIZACION">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                <asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRMNv_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PDF" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblRutaPdf" runat="server" Visible="false" Text='<%# Bind("RUTA_ORDEN_TRABAJO") %>'></asp:Label>
                                <asp:ImageButton ID="imgPdfOT" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdfOT_Click" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminarOT" runat="server" ToolTip="Eliminar OT" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminarOT_Click" OnClientClick="return confirm('¿Desea eliminar el registro?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerTemplate>
                        <div>
                            <div style="float: left">
                                <asp:ImageButton ID="imgFirst" runat="server"
                                    ImageUrl="~/assets/img/grid/first.gif" OnClick="imgFirst_Click"
                                    Style="height: 15px" title="Navegación: Ir a la Primera Pagina" Width="26px" />
                                <asp:ImageButton ID="imgPrev" runat="server"
                                    ImageUrl="~/assets/img/grid/prev.gif" OnClick="imgPrev_Click"
                                    title="Navegación: Ir a la Pagina Anterior" Width="26px" />
                                <asp:ImageButton ID="imgNext" runat="server"
                                    ImageUrl="~/assets/img/grid/next.gif" OnClick="imgNext_Click"
                                    title="Navegación: Ir a la Siguiente Pagina" Width="26px" />
                                <asp:ImageButton ID="imgLast" runat="server"
                                    ImageUrl="~/assets/img/grid/last.gif" OnClick="imgLast_Click"
                                    title="Navegación: Ir a la Ultima Pagina" Width="26px" />
                            </div>
                            <div style="float: right">
                                Página
                            <asp:Label ID="lblPagina" runat="server"></asp:Label>
                                de
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </div>
                        </div>
                    </PagerTemplate>
                </asp:GridView>
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
