<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="NotasDeVentas.aspx.cs" Inherits="crm_valvulas_industriales.NotasDeVentas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


       <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Notas de Ventas</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="panel panel-primary">
                    <div class="panel-heading">
                        <strong>Notas de ventas</strong>
                    </div>
                <table class="table table-condensed small">
                    <tr class="success">
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
                            <strong>Facturado</strong>
                            <asp:DropDownList ID="ddlFacturado" runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Con facturación"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Sin facturación"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm" Text="Buscar" OnClick="btnBuscar_Click" />
                        </td>
                        <td><br />
                            <asp:ImageButton ID="ibtnExportarExcel" runat="server" 
                        ImageUrl="~/assets/img/export_excel.png" onclick="ibtnExportarExcel_Click" />
                        </td>
                    </tr>
                </table>
            </div>


            
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <strong>Nota de Venta</strong>
                        </div>

                        <asp:GridView ID="grvNotaVenta" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small"
                            AutoGenerateColumns="false"  AllowPaging="True" PageSize="50" OnRowDataBound="paginacion_RowDataBound" OnSorting="gvEmployee_Sorting" AllowSorting="true">
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Nota Venta" SortExpression="ID_NOTA_VENTA"  HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdNotaVenta" runat="server" Visible="true" Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                                        <%--<asp:Label ID="lblIdPago" runat="server" Visible="false" Text='<%# Bind("ID_PAGO") %>'></asp:Label>--%>
                                        <%--<asp:LinkButton ID="lbtnIdNotaVenta" runat="server" Text='<%# Bind("ID_NOTA_VENTA") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRM_Click"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rut Cliente" SortExpression="ID_NOTA_VENTA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRutCliente" runat="server" Visible="true" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaNotaVenta" runat="server" Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_NETO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoNeto" runat="server" Visible="false" Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                                        <asp:Label ID="lblMontoNeto2" runat="server" Text='<%# Eval("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dcto Pago Contado" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_DESCUENTO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoDescuento" runat="server" Visible="false" Text='<%# Bind("MONTO_DESCUENTO") %>'></asp:Label>
                                        <asp:Label ID="lblMontoDescuento2" runat="server" Text='<%# Eval("MONTO_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Porc Desc" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPorcentajeDescuentaCotizacion" runat="server" Visible="false" Text='<%# Bind("PORC_DESCUENTO") %>'></asp:Label>
                                        <asp:Label ID="lblPorcentajeDescuentaCotizacion2" runat="server" Text='<%# Eval("PORC_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monto Iva" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_IVA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoIva" runat="server" Visible="false" Text='<%# Bind("MONTO_IVA") %>'></asp:Label>
                                        <asp:Label ID="lblMontoOIva2" runat="server" Text='<%# Eval("MONTO_IVA", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monto Total" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_TOTAL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoTotal" runat="server" Visible="false" Text='<%# Bind("MONTO_TOTAL") %>'></asp:Label>
                                        <asp:Label ID="lblMontoTotal2" runat="server" Text='<%# Eval("MONTO_TOTAL", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo" ItemStyle-HorizontalAlign="Right"  SortExpression="SALDO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldo" runat="server" Visible="false" Text='<%# Bind("SALDO") %>'></asp:Label>
                                        <asp:Label ID="lblSaldo2" runat="server" Text='<%# Eval("SALDO", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado Not Venta" SortExpression="NOM_ESTADO_NOTA_VENTA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoNotaVenta" runat="server"  Text='<%# Bind("NOM_ESTADO_NOTA_VENTA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Usuario Asig" SortExpression="USUARIO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsuarioAsig" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="O.C." SortExpression="ORDEN_DE_COMPRA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrdenCompra" runat="server" Text='<%# Bind("ORDEN_DE_COMPRA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cot" SortExpression="ID_COTIZACION">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                        <asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRMNv_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fac" SortExpression="Id_factura">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdFactura" runat="server" Visible="true" Text='<%# Bind("ID_FACTURA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjunto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRutaOrdenDeCompra" runat="server" Visible="false" Text='<%# Bind("RUTA_ORDEN_DE_COMPRA") %>'></asp:Label>
                                        <asp:ImageButton ID="ibtnRutaOrdenCompra" ImageUrl="~/assets/img/file_manager.png" runat="server" OnClick="ibtnRutaOrdenCompra_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PDF">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRutaPdf" runat="server" Visible="false" Text='<%# Bind("RUTA_NOTA_VENTA") %>'></asp:Label>
                                        <asp:ImageButton ID="imgPdfNotaVenta" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdfNotaVenta_Click" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            
                <PagerTemplate>
                    <div>
                        <div style="float:left">
                            <asp:ImageButton ID="imgFirst" runat="server"  
                                ImageUrl="~/assets/img/grid/first.gif" onclick="imgFirst_Click" 
                                style="height: 15px" title="Navegación: Ir a la Primera Pagina" Width="26px" />
                            <asp:ImageButton ID="imgPrev" runat="server" 
                                ImageUrl="~/assets/img/grid/prev.gif" onclick="imgPrev_Click" 
                                title="Navegación: Ir a la Pagina Anterior" Width="26px" />
                            <asp:ImageButton ID="imgNext" runat="server"
                                ImageUrl="~/assets/img/grid/next.gif" onclick="imgNext_Click" 
                                title="Navegación: Ir a la Siguiente Pagina" Width="26px" />
                            <asp:ImageButton ID="imgLast" runat="server" 
                                ImageUrl="~/assets/img/grid/last.gif" onclick="imgLast_Click" 
                                title="Navegación: Ir a la Ultima Pagina" Width="26px" />
                        </div>
                        <div style="float:right">
                            Página
                            <asp:Label ID="lblPagina" runat="server"></asp:Label>
                            de
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </div>
                    </div>
                </PagerTemplate>
                        </asp:GridView>


                    </div>



            
            
    <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnDetalleCotizacionMDL" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlDetalleCotizacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlDetalleCotizacion" TargetControlID="btnDetalleCotizacionMDL">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlDetalleCotizacion" runat="server" CssClass="panel panel-primary" style="background:white; width:95%; max-height:90%; overflow:auto; display:none">
                <div class="panel panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>Detalle Cotización 
                        <asp:Label ID="lblNumeroCotizacionDetalle" runat="server" CssClass="label label-danger"></asp:Label></strong>
                </div>
                
                    <asp:GridView ID="grvDetalleCotizacion" runat="server" CssClass="table table-bordered table-hover table-condensed small" HeaderStyle-CssClass="active" AutoGenerateColumns="false" EmptyDataText="No se encontraron registros asociados a esa cotización">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Correlativo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCorrelativo" runat="server"  Text='<%# Bind("CORRELATIVO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Código">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblCodigo" runat="server"  Text='<%# Bind("CODIGO") %>'></asp:Label>
                                                <asp:Label ID="lblIdProducto" runat="server" Visible="false" Text='<%# Bind("ID_PRODUCTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Producto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNombreProducto" runat="server"  Text='<%# Bind("NOM_PRODUCTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoNeto" runat="server"  Text='<%# Eval("MONTO_NETO", "{0:n0}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCantidad" runat="server"  Text='<%# Eval("CANTIDAD", "{0:n0}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Monto Total" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoTotal" runat="server"  Text='<%# Eval("MONTO_TOTAL", "{0:n0}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>
                
            </asp:Panel>

            
<%--MODALPOPUP CON BOOTSTRAP--%>
            



            
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
