<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="DetalleResumen.aspx.cs" Inherits="crm_valvulas_industriales.DetalleResumen" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:HiddenField ID="hfVendedor" runat="server" />
        <asp:HiddenField ID="hfFechaDesde" runat="server" />
        <asp:HiddenField ID="hfFechaHasta" runat="server" />


        
    <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="form-inline">
                    <strong>Detalle cotizaciones</strong>
                    
                   <%-- <div align="right">--%>

                        <asp:ImageButton ID="ibtnExportarExcel" ImageAlign="Right" 
                        ImageUrl="~/assets/img/file_extension_xls.png" runat="server" 
                        onclick="ibtnExportarExcel_Click" />
                        <asp:Button ID="btnVolver"  runat="server" Text="Volver"  
                        CssClass="btn btn-default btn-xs text-right" onclick="btnVolver_Click" />
                        </div>

                   <%-- </div>--%>
                </div>
        <asp:GridView ID="grvSeguimiento" HeaderStyle-CssClass="active" runat="server" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnRowDataBound="paginacion_RowDataBound" EmptyDataText="No hay registros para esta consulta">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Cot" SortExpression="ID_COTIZACION">
                        <ItemTemplate>
                            <asp:Label ID="lblIdCotizacion" runat="server" Visible="true" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NV" SortExpression="ID_NOTA_VENTA">
                        <ItemTemplate>
                            <asp:Label ID="lblIdNotaVenta" runat="server"  Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rut Cliente" SortExpression="RUT_CLIENTE">
                        <ItemTemplate>
                            <asp:Label ID="lblRutCliente" runat="server" Visible="true" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Razon Social" SortExpression="RAZON_SOCIAL">
                        <ItemTemplate>
                            <asp:Label ID="lblRazonSocial" runat="server" Visible="true" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaCotizacion" runat="server"  Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_NETO">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoNeto" runat="server" Visible="false"  Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                            <asp:Label ID="lblMontoNeto2" runat="server"  Text='<%# Eval("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dcto Pago Contado" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_DESCUENTO">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoDescuento" runat="server" Visible="false" Text='<%# Bind("MONTO_DESCUENTO") %>'></asp:Label>
                            <asp:Label ID="lblMontoDescuento2" runat="server"  Text='<%# Eval("MONTO_DESCUENTO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Porc Desc" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPorcentajeDescuentaCotizacion" runat="server" Visible="false" Text='<%# Bind("PORC_DESCUENTO") %>'></asp:Label>
                            <asp:Label ID="lblPorcentajeDescuentaCotizacion2" runat="server"  Text='<%# Eval("PORC_DESCUENTO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Monto Iva" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_IVA">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoIva" runat="server" Visible="false" Text='<%# Bind("MONTO_IVA") %>'></asp:Label>
                            <asp:Label ID="lblMontoOIva2" runat="server"  Text='<%# Eval("MONTO_IVA", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Total" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_TOTAL">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoTotal" runat="server" Visible="false" Text='<%# Bind("MONTO_TOTAL") %>'></asp:Label>
                            <asp:Label ID="lblMontoTotal2" runat="server"  Text='<%# Eval("MONTO_TOTAL", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fec Validez" SortExpression="FECHA_VALIDEZ">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaValidez" runat="server"  Text='<%# Bind("FECHA_VALIDEZ", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado Cot" SortExpression="ESTADO_COTIZACION">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoCotizacion" runat="server" ToolTip='<%# Bind("MOTIVO_RECHAZO") %>'  Text='<%# Bind("NOM_ESTADO_COTIZACION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Usuario Asig" SortExpression="USUARIO">
                        <ItemTemplate>
                            <asp:Label ID="lblUsuarioAsig" runat="server"  Text='<%# Bind("USUARIO") %>'></asp:Label>
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
            
            <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" DefaultButton="imgAceptar" style="display:none; background:white; width:auto; height:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                  <div class="modal-header">
                        <button class="close" data-dismiss="modal">×</button>
                        <h3>Información</h3>
                  </div>
                  <div class="modal-body">
                      <asp:Label ID="lblInformacion" runat="server" Text="" CssClass="alert"></asp:Label>
                  </div>
                  <div class="modal-footer">
                    <asp:ImageButton ID="imgAceptar" runat="server"  ImageUrl="~/assets/img/accept.png" />
                  </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
            </asp:Panel>


    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
