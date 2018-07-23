<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Seguimiento.aspx.cs" Inherits="crm_valvulas_industriales.Seguimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>


            <div class="well well-sm">
            <table class="table">
                
                <tr>
                <td>
                    <strong><asp:Label ID="lblInfoVendedor" runat="server" Text="Vendedor"></asp:Label></strong>
                    <asp:DropDownList ID="ddlVendedor" runat="server" OnDataBound="ddlVendedor_DataBound" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </td>

                <td><strong>Fecha Desde</strong>
                <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                <asp:CalendarExtender ID="txtFechaDesde_CalendarExtender" runat="server" 
                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                    TargetControlID="txtFechaDesde"></asp:CalendarExtender>
                </td>
                <td>
                <strong>Fecha Hasta</strong>
                <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                <asp:CalendarExtender ID="txtFechaHasta_CalendarExtender" runat="server" 
                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                    TargetControlID="txtFechaHasta"></asp:CalendarExtender>
                </td>
                <td><strong>Monto Cot Desde</strong>
                <asp:TextBox ID="txtMontoCotizacionDesde" runat="server" CssClass="form-control input-sm"  MaxLength="10"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="ft_txtMontoCotizacionDesde" runat="server"
                    TargetControlID="txtMontoCotizacionDesde" FilterType="Numbers" />
                
                </td>
                <td>
                <strong>Monto Cot Hasta</strong>
                <asp:TextBox ID="txtMontoCotizacionHasta" runat="server" CssClass="form-control input-sm"  MaxLength="10"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="ft_txtMontoCotizacionHasta" runat="server"
                    TargetControlID="txtMontoCotizacionHasta" FilterType="Numbers" />
                </td>
                <td>
                    <br />
                    <asp:Button ID="btnBuscarSeguimiento" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm" OnClick="btnBuscarSeguimiento_Click" />
                </td>
                <td>
                    <br />
                    <asp:ImageButton ID="ibtnExportar" ImageUrl="~/assets/img/export_excel.png" OnClick="ibtnExportar_Click" runat="server" />
                </td>
                </tr>
                </table>

            </div>


            
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>Casos asignados para seguimiento</strong>
                </div>
                
                <asp:GridView ID="grvSeguimiento" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnRowDataBound="paginacion_RowDataBound" OnSorting="gvEmployee_Sorting" AllowSorting="true" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtnIrAGestion" ImageUrl="~/assets/img/note_go.png" runat="server" OnClick="ibtnIrAGestion_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="lblIdCliente" runat="server" Visible="false" Text='<%# Bind("ID_CLIENTE") %>'></asp:Label>
                            <asp:LinkButton ID="lbtnIdCliente" runat="server" Text='<%# Bind("ID_CLIENTE") %>' OnClick="lbtnIdCliente_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rut Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lblRut" runat="server" Visible="false" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                            <asp:LinkButton ID="lbtnRut" runat="server" Text='<%# Bind("RUT_CLIENTE") %>' OnClick="lbtnRut_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lblNombreCliente" runat="server" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cotización" SortExpression="ID_COTIZACION">
                        <ItemTemplate>
                            <asp:Label ID="lblCotizacion" runat="server" Visible="false"  Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                            <asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacionSeguimiento_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PDF">
                        <ItemTemplate>
                            <asp:Label ID="lblRutaPdf" runat="server" Visible="false"  Text='<%# Bind("RUTA_COTIZACION") %>'></asp:Label>
                            <asp:ImageButton ID="imgPdf" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdf_Click" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fec Cotización" SortExpression="FECHA">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaCotizacion" runat="server"  Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Cot" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_NETO">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoCotizacion" runat="server" Visible="false" Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                            <asp:Label ID="lblMontoCotizacion2" runat="server" Text='<%# Eval("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Validez" SortExpression="FECHA_VALIDEZ">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaValidez" runat="server"  Text='<%# Bind("FECHA_VALIDEZ", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado" SortExpression="NOM_ESTADO_COTIZACION">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoCotizacion" runat="server" ToolTip='<%# Bind("MOTIVO_RECHAZO") %>' Text='<%# Bind("NOM_ESTADO_COTIZACION") %>'></asp:Label>
                            <asp:Label ID="lblIdEstadoCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_ESTADO_COTIZACION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usuario asig" SortExpression="USUARIO">
                        <ItemTemplate>
                            <asp:Label ID="lblIdUsuarioAsig" runat="server" Visible="false"  Text='<%# Bind("ID_USUARIO_ASIG") %>'></asp:Label>
                            <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fec Gestión" SortExpression="FECHA_ULT_GESTION">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaGestion" runat="server"  Text='<%# Bind("FECHA_ULT_GESTION", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ult Gestión" SortExpression="ULTIMA_GESTION">
                        <ItemTemplate>
                            <asp:Label ID="lblUltimagestion" runat="server"  Text='<%# Bind("ULTIMA_GESTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ult Gestión Obs" SortExpression="ULTIMA_GESTION">
                        <ItemTemplate>
                            <asp:Label ID="lblUltimaGestionObs" runat="server"  Text='<%# Bind("ULTIMA_GESTION_OBS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgSeleccionar" CausesValidation="False" runat="server"  Visible="true" ImageUrl="~/assets/img/plus.png"  ToolTip="Gestionar" OnClick="imgGestionar_Click"/>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
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
                                        <asp:TemplateField HeaderText="Id Cotización" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Correlativo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCorrelativo" runat="server"  Text='<%# Bind("CORRELATIVO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id Producto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdProducto" runat="server"  Text='<%# Bind("ID_PRODUCTO") %>'></asp:Label>
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
