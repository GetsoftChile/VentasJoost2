<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="GrafPanelGestionVentas.aspx.cs" Inherits="crm_valvulas_industriales.GrafPanelGestionVentas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script  type="text/javascript" language="javascript" src="assets/JSClass/FusionCharts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Paneles</a></li>
          <li class="active">Gestión Ventas</li>
    </ol>

    <div class="panel panel-primary">
    <div class="panel-heading">
        <strong>Modulo Paneles - Gestión Ventas</strong>
    </div>

    <table class="table table-condensed small">
    <tr class="active">
        <td width="200px">
            <strong>Ejecutivo</strong>
            <asp:DropDownList ID="ddlVendedorIndicador" CssClass="form-control input-sm" runat="server" OnDataBound="ddlVendedorIndicador_DataBound">
            </asp:DropDownList>
        </td>
        <td>
            <strong>Actividad Comercial</strong>
            <asp:DropDownList ID="ddlActividadComercial" Width="150px" CssClass="form-control input-sm" runat="server" OnDataBound="ddlActividadComercial_DataBound">
            </asp:DropDownList>
        </td>
        <td width="200px">
            <strong>Fecha Desde</strong>
            <asp:TextBox ID="txtFechaDesde" Width="105px" runat="server" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
            <asp:CalendarExtender ID="ce_txtFecha" runat="server"
            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
            TargetControlID="txtFechaDesde" ></asp:CalendarExtender>
        </td>
        <td width="200px">
            <strong>Fecha Hasta</strong>
            <asp:TextBox ID="txtFechaHasta" Width="105px" runat="server" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server"
            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
            TargetControlID="txtFechaHasta"></asp:CalendarExtender>
        </td>
        
        <td>
            <br />
            <asp:LinkButton ID="lbtnBuscar" CssClass="btn btn-danger btn-sm" runat="server" onclick="lbtnBuscar_Click" ><i aria-hidden="true" class="glyphicon glyphicon-search"></i> Buscar</asp:LinkButton>
        </td>
    </tr>
    </table>
    </div>


    <div class="row">
        <div class="col-sm-6">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            
                        </div>
                        <div class="panel-body">
                            <asp:Literal ID="litCantidad" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                
                <div class="col-sm-6">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            
                        </div>
                        <div class="panel-body">
                            <asp:Literal ID="litPesos" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
    </div>


    <div class="row">
    <div class="col-sm-12">
        <div class="panel panel-success">
            <div class="panel-heading">
                <strong>Cotizaciones</strong>
                <asp:ImageButton ID="ibtnExportar" OnClick="ibtnExportar_Click" ImageUrl="~/assets/img/export_excel.png" runat="server" />
            </div>
            
                <asp:GridView ID="grvGestionVenas" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnRowDataBound="paginacion_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="ID Cotización">
                        <ItemTemplate>
                            <asp:Label ID="lblIdCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblRutaPdf" runat="server" Visible="false" Text='<%# Bind("RUTA_COTIZACION") %>'></asp:Label>
                            <asp:LinkButton ID="lbtnIdCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnIdCotizacion_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NV">
                        <ItemTemplate>
                            <asp:Label ID="lblIdNotaVenta" runat="server" Visible="false" Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                            <asp:LinkButton ID="lbtnIdNotaVenta" runat="server" Text='<%# Bind("ID_NOTA_VENTA") %>' OnClick="lbtnIdNotaVenta_Click"></asp:LinkButton>
                            <asp:Label ID="lblRutaPdfNV" runat="server" Visible="false"  Text='<%# Bind("RUTA_NOTA_VENTA") %>'></asp:Label>
                            <%--<asp:ImageButton ID="imgPdfNotaVenta" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdfNotaVenta_Click" runat="server" />--%>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rut Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lblRut" runat="server" Visible="false" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                            <asp:LinkButton ID="lbtnRut" runat="server" Text='<%# Bind("RUT_CLIENTE") %>' OnClick="lbtnRut_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Razon Social">
                        <ItemTemplate>
                            <asp:Label ID="lblRazonSocial" runat="server" Visible="true" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaIngreso" runat="server"  Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoNeto" runat="server" Visible="true" Text='<%# Bind("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Desc" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoDescuento" runat="server" Visible="true" Text='<%# Bind("MONTO_DESCUENTO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Iva" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoIva" runat="server" Visible="true" Text='<%# Bind("MONTO_IVA", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Total" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoTotal" runat="server" Visible="true" Text='<%# Bind("MONTO_TOTAL", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" ToolTip='<%# Bind("MOTIVO_RECHAZO") %>' Text='<%# Bind("NOM_ESTADO_COTIZACION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usuario Asig">
                        <ItemTemplate>
                            <asp:Label ID="lblUsuarioAsig" runat="server" Visible="true" Text='<%# Bind("USUARIO") %>'></asp:Label>
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
    </div>
    </div>











    

    <div class="row">
    <div class="col-sm-12">
        <div class="panel panel-success">
            <div class="panel-heading">
                <strong>Facturas</strong>
                <asp:ImageButton ID="lbtnExportarFactura" OnClick="lbtnExportarFactura_Click" ImageUrl="~/assets/img/export_excel.png" runat="server" />
            </div>
            
                <asp:GridView ID="grvFacturas" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnRowDataBound="paginacionFacturas_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Nro Factura">
                        <ItemTemplate>
                            <asp:Label ID="lblNroFactura" runat="server" Text='<%# Bind("ID_FACTURA") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NV">
                        <ItemTemplate>
                            <asp:Label ID="lblIdNotaVenta" runat="server" Visible="true" Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                            
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rut Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lblRut" runat="server" Visible="true" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Facturación">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaFacturacion" runat="server"  Text='<%# Bind("FECHA_FACTURACION", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoNeto" runat="server" Visible="true" Text='<%# Bind("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Forma Pago">
                        <ItemTemplate>
                            <asp:Label ID="lblFormaPago" runat="server" Text='<%# Bind("NOM_FORMA_PAGO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>

                <PagerTemplate>
                    <div>
                        <div style="float:left">
                            <asp:ImageButton ID="imgFirst" runat="server"  
                                ImageUrl="~/assets/img/grid/first.gif" onclick="imgFirstFactura_Click" 
                                style="height: 15px" title="Navegación: Ir a la Primera Pagina" Width="26px" />
                            <asp:ImageButton ID="imgPrev" runat="server" 
                                ImageUrl="~/assets/img/grid/prev.gif" onclick="imgPrevFactura_Click" 
                                title="Navegación: Ir a la Pagina Anterior" Width="26px" />
                            <asp:ImageButton ID="imgNext" runat="server"
                                ImageUrl="~/assets/img/grid/next.gif" onclick="imgNextFactura_Click" 
                                title="Navegación: Ir a la Siguiente Pagina" Width="26px" />
                            <asp:ImageButton ID="imgLast" runat="server" 
                                ImageUrl="~/assets/img/grid/last.gif" onclick="imgLastFactura_Click" 
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
    </div>
    </div>

    



















    

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


</asp:Content>
