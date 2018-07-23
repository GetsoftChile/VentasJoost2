<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Cotizacion.aspx.cs" Inherits="crm_fadonel.Cotizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li class="active">Cotización</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfRutClientePost" runat="server" />
        <asp:HiddenField ID="hfrutaArchivoPdf" runat="server" />

    <div class="panel panel-primary">
    <div class="panel-heading">
        <strong>Cotización</strong>
    </div>
        <table class="table table-condensed small">
                    <tr class="active">
                        <td>
                            <strong>Id Cliente</strong>
                            <br />
                            <asp:Label ID="lblIdCliente" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Rut</strong>
                            <br />
                            <asp:Label ID="lblRutCotizacion" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Cliente</strong>
                            <br />
                            <asp:Label ID="lblCliente" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Teléfono</strong>
                            <br />
                            <asp:Label ID="lblTelefono" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Condición de Venta</strong>
                            <br />
                            <asp:Label ID="lblCondicionDeVentaCotizacion" runat="server" Visible="true"></asp:Label>
                            <asp:Label ID="lblDireccionCotizacion" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblComunaCotizacin" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblCiudad" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <strong>Monto Credito</strong>
                            <br />
                            <asp:Label ID="lblMontoCreditoCotizacion" runat="server" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <strong>Canal</strong>
                            <asp:DropDownList ID="ddlCanal" runat="server" OnDataBound="ddlCanal_DataBound" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="active">
                        <td>
                            <asp:ImageButton ID="imgAgregarContacto" ImageUrl="~/assets/img/add.png" OnClick="imgAgregarContacto_Click" runat="server" />
                            <strong>Contacto</strong>
                            <br />
                            <asp:Label ID="lblContacto" runat="server"></asp:Label>
                            <asp:Label ID="lblIdContacto" Visible="false" runat="server"></asp:Label>

                        </td>
                        <td>
                            <strong>Email</strong><br />
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Email 2</strong><br />
                            <asp:Label ID="lblEmail2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Celular</strong><br />
                            <asp:Label ID="lblCelular" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Telefono</strong><br />
                            <asp:Label ID="lblTelefonoContacto" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Telefono 2</strong><br />
                            <asp:Label ID="lblTelefonoContacto2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="active" id="trRutClientePadreCotizacion" runat="server" visible="false">
                        <td>
                            <strong>Rut Cliente Padre</strong>
                            <asp:Label ID="lblRutClientePadreCotizacion" runat="server"></asp:Label>
                        </td>
                        <td>
                            <strong>Razon Social Padre</strong>
                            <asp:Label ID="lblRazonSocialPadreCotizacion" runat="server"></asp:Label>
                        </td>
                        <td>
                            
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
    </div>


    
    <div class="panel panel-primary">
    <div class="panel-heading">
        <strong>Selección de Productos</strong>
    </div>

        <table class="table table-condensed small">
        <tr class="active">
            <td >
                <br />
                <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto" 
                CssClass="btn btn-success btn-sm" onclick="btnAgregarProducto_Click"/>
            </td>
            <td>
                <strong>Razon Social</strong>
                <asp:DropDownList ID="ddlRazonSocial" runat="server" OnDataBound="ddlRazonSocial_DataBound" CssClass="form-control input-sm">
                </asp:DropDownList>
            </td>
            <td align="right">
                <b>Descuento Pago Contado</b>
                <asp:TextBox ID="txtDescuentoPagoContado" runat="server"  MaxLength="8" Width="150px" CssClass="form-control input-sm" placeholder="Descuento" ontextchanged="txtDescuentoPagoContado_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                TargetControlID="txtDescuentoPagoContado" ValidChars="0987654321," />
            </td>
        </tr>
        </table>
        <asp:HiddenField ID="hfTieneDescuento" runat="server" />
            <asp:GridView ID="grvProducto" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" AutoGenerateColumns="false" OnRowDataBound="paginacion_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("CORRELATIVO") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID Producto" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdProducto" runat="server" Text='<%# Bind("ID_PRODUCTO") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo">
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CODIGO") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreProducto" runat="server" Text='<%# Bind("NOMBRE_PRODUCTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cant">
                            <ItemTemplate>
                                <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("CANTIDAD") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Monto Uni">
                            <ItemTemplate>
                                <asp:Label ID="lblMontoUni" runat="server" Text='<%# Bind("MONTO_UNI") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Monto">
                            <ItemTemplate>
                                <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="% Desc">
                            <ItemTemplate>
                                <asp:Label ID="lblDescuentoPorcentaje" runat="server" Text='<%# Bind("DESCUENTO_PORCENTAJE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Descuento">
                            <ItemTemplate>
                                <asp:Label ID="lblDescuento" runat="server" Text='<%# Bind("DESCUENTO_MONTO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Final">
                            <ItemTemplate>
                                <asp:Label ID="lblMontoTotal" runat="server" Text='<%# Bind("MONTO_TOTAL") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminar_Click" onclientclick="return confirm('¿Desea eliminar el registro?');" ToolTip="Eliminar"/>
                                <%--<asp:LinkButton ID="lbtnEliminarProducto" OnClick="lbtnEliminarProducto_Click" runat="server"><i aria-hidden="true" class="text-danger glyphicon glyphicon-remove"></i></asp:LinkButton>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>


                </asp:GridView>

        <table class="table table-condensed small">
            <tr class="active">
                <td>
                    <strong>Observación
                    </strong>
                    <asp:TextBox ID="txtObservacionCotizacion" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Height="60px" MaxLength="800"></asp:TextBox>
                </td>
            </tr>
        </table>

        <table id="Table1" class="table table-condensed small" runat="server" visible="false">
            <tr>
                <td width="80px">
                    <strong>Total Neto:</strong>
                </td>
                <td>
                    <asp:Label ID="lblTotalNeto" runat="server"></asp:Label>
                    <asp:Label ID="lblTotalDescuento" runat="server"></asp:Label>
                    <asp:Label ID="lblPocentaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="80px">
                    <strong>Iva:</strong>
                </td>
                <td>
                    <asp:Label ID="lblIva" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="80px">
                    <strong>Total:</strong>
                </td>
                <td>
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </td>
            </tr>
        </table>


    <div class="panel-footer">
        <asp:Button ID="btnGrabarCotizacion" runat="server" Text="Generar Cotización" CssClass="btn btn-danger btn-sm" onclick="btnGrabarCotizacion_Click"/>
    </div>

    </div>





        
        
        <asp:Button ID="Button2" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="mdlInformacionValidacionProducto" BackgroundCssClass="modalBackground" runat="server" PopupControlID="Panel1" TargetControlID="Button2" BehaviorID="_mdlInfo">
        </asp:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <div class="panel panel-danger">
            <div class="panel-heading">
                    <strong>Información</strong>
            </div>
                <div class="alert">
                    <asp:Label ID="lblInformacionValidacionProducto" runat="server" Text=""></asp:Label>
                </div>
            <div class="panel-footer">
                <asp:ImageButton ID="imgValidacion" runat="server" OnClick="imgValidacion_Click" ImageUrl="~/assets/img/accept.png"/>
                    
            </div>
            </div>
            <%--MODALPOPUP CON BOOTSTRAP--%>
        </asp:Panel>




        
    
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnContactosMDL" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlContactos" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlContactos" TargetControlID="btnContactosMDL" BehaviorID="_mdlContactos">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlContactos" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:auto;overflow:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="Label1" runat="server" Text="Contactos"></asp:Label>
                    <asp:Button ID="btnNuevoContacto" runat="server" Text="Nuevo Contacto" CssClass="btn btn-danger btn-xs" onclick="btnNuevoContacto_Click" />
                    </strong>
                  </div>
                    
                <asp:GridView ID="grvContactosCotizacion" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreContacto" runat="server" Text='<%# Bind("NOM_CONTACTO") %>'></asp:Label>
                                <asp:Label ID="lblIdContacto" runat="server" Visible="false" Text='<%# Bind("ID_CONTACTO") %>'></asp:Label>
                                <asp:Label ID="lblRutCliente" runat="server" Visible="false" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email 1">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail1" runat="server" Text='<%# Bind("EMAIL_1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Email 2">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail2" runat="server" Text='<%# Bind("EMAIL_2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Celular">
                            <ItemTemplate>
                                <asp:Label ID="lblCelular" runat="server" Text='<%# Bind("CELULAR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Teléfono 1">
                            <ItemTemplate>
                                <asp:Label ID="lblTelefono1" runat="server" Text='<%# Bind("TELEFONO1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Teléfono 2">
                            <ItemTemplate>
                                <asp:Label ID="lblTelefono2" runat="server" Text='<%# Bind("TELEFONO2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cargo">
                            <ItemTemplate>
                                <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("CONTACTO_CARGO") %>'></asp:Label>
                                <asp:Label ID="lblIdCargo" runat="server" Visible="false" Text='<%# Bind("ID_CARGO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditarContacto" OnClick="ibtnEditarContacto_Click" runat="server" ImageUrl="~/assets/img/edit_button.png" />
                                <asp:ImageButton ID="imgSeleccionarContacto" runat="server" ImageUrl="~/assets/img/add.png" OnClick="imgSeleccionarContacto_Click" ToolTip="Seleccionar"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>




            

            
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnMDLAgregarContacto" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlAgregarContacto" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarContacto" TargetControlID="btnMDLAgregarContacto" BehaviorID="_mdlAgregarObra">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarContacto" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:auto;overflow:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="lblAgregarContacto" runat="server"></asp:Label>
                    </strong>
                  </div>
                    <asp:HiddenField ID="hfIdContacto" runat="server" />
                    <asp:HiddenField ID="hfRutCliente" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Nombre Contacto</strong></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtNombreContacto" Width="27%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Email 1</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Email 2</strong></td>
                            <td><asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Celular</strong></td>
                            <td>
                                <asp:TextBox ID="txtCelular" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 1</strong></td>
                            <td>
                                <asp:TextBox ID="txtTelefono1" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Teléfono 2</strong></td>
                            <td>
                                <asp:TextBox ID="txtTelefono2" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Cargo</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCargo" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
                <div class="panel-footer">
                    <asp:Button ID="btnGrabarContacto" runat="server" Text="Guardar" CssClass="btn btn-primary" onclick="btnGrabarContacto_Click" />
                    <asp:Button ID="btnModificarContacto" runat="server" Text="Guardar" CssClass="btn btn-primary" onclick="btnModificarContacto_Click" />
                </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>


        


    
    
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button1" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlSeleccionarProductos" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlProductos" TargetControlID="Button1" BehaviorID="_mdlProductos">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlProductos" runat="server" CssClass="panel" style="display:none; background:white; width:85%; max-height:90%;overflow:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="Label2" runat="server" Text="Productos"></asp:Label>
                    </strong>
                  </div>
                    
                    <table class="table table-condensed small">
                    <tr class="active">
                        <td width="300px">
                            <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
                        </td>
                        <td width="60px">
                            <asp:Button ID="btnBuscarProducto" runat="server" CssClass="btn btn-success btn-sm" Text="Buscar" onclick="btnBuscarProducto_Click"/>
                        </td>
                        <td width="150px">
                            <asp:TextBox ID="txtDescuento" runat="server" MaxLength="8" Width="150px" CssClass="form-control input-sm" placeholder="Descuento"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftbe" runat="server"
                            TargetControlID="txtDescuento" ValidChars="0987654321," />
                        </td>
                        <td>
                            <asp:Button ID="btnGrabarYSeleccionar" runat="server" CssClass="btn btn-danger btn-sm" Text="Seleccionar" onclick="btnGrabarYSeleccionar_Click"/>
                        </td>
                    </tr>
                    </table>
                    

        <asp:GridView ID="grvProductos" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>

                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control input-sm" Width="80px" MaxLength="6" placeholder="Cantidad"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftbe" runat="server"
                                    TargetControlID="txtCantidad"         
                                    ValidChars="0987654321" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Monto Descuento">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMontoDescuento" runat="server" CssClass="form-control input-sm" Width="80px" MaxLength="7" placeholder="Monto Descuento" AutoPostBack="true" OnTextChanged="txtMontoDescuento_TextChanged"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftbeDescuento1" runat="server"
                                    TargetControlID="txtMontoDescuento"         
                                    ValidChars="0987654321,-"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Descuento">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control input-sm" Width="80px" MaxLength="7" placeholder="Descuento" AutoPostBack="true" OnTextChanged="txtDescuento_TextChanged1"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftbeDescuento" runat="server"
                                    TargetControlID="txtDescuento"         
                                    ValidChars="0987654321,-"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblIdProducto" runat="server" Text='<%# Bind("ID_PRODUCTO") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblNombreProducto" runat="server" Text='<%# Bind("NOM_PRODUCTO") %>'></asp:Label>
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
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Venta">
                            <ItemTemplate>
                                <asp:Label ID="lblValorVenta" runat="server" Text='<%# Bind("VALOR_VENTA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblStock" runat="server" Text='<%# Bind("STOCK") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>


        
        
        <asp:Button ID="Button5" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="mdlInformacionValidarCotizacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlValidarCotizacion" TargetControlID="Button5" BehaviorID="_mdlInfoSiNo">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlValidarCotizacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <div class="panel panel-danger">
            <div class="panel-heading">
                    <strong>Información</strong>
            </div>
                <div class="alert">
                    <asp:Label ID="Label3" runat="server" Text="¿Desea solicitar un descuento mayor?, esta cotización quedará en estado por aprobar hasta que Gerencia autorice el descuento."></asp:Label>
                    <asp:HiddenField ID="hfAutorizarSiNo" runat="server" />

                </div>
            <div class="panel-footer">
                <asp:Button ID="btnSiAutorizar" runat="server" Text="Si" OnClick="btnSiAutorizar_Click" />
                <asp:Button ID="btnNoAutorizar" runat="server" Text="No" OnClick="btnNoAutorizar_Click" />
            </div>
            </div>
            <%--MODALPOPUP CON BOOTSTRAP--%>
        </asp:Panel>





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
