<%@ Page Title="" Language="C#"  MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="crm_fadonel.Productos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Productos</li>
    </ol>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlProductos" runat="server" DefaultButton="btnBuscar">
            <div class="well well-sm">
                <h4>Productos</h4>
                <div class="form-inline">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary"
                        Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnNuevoProducto" runat="server" Text="Nuevo Producto"
                        CssClass="btn btn-success" OnClick="btnNuevoProducto_Click" />
                    <asp:ImageButton ID="ibtnExportarExcel" runat="server"
                        ImageUrl="~/assets/img/export_excel.png" OnClick="ibtnExportarExcel_Click" />

                </div>
            </div>
        </asp:Panel>


        <asp:GridView ID="grvProductos" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowDataBound="paginacion_RowDataBound">
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
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgMaterial" runat="server" ImageUrl="~/assets/img/plus.png" OnClick="imgMaterial_Click" ToolTip="Agregar Materiales"  />
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/assets/img/edit_button.png" OnClick="imgEditar_Click" ToolTip="Editar"  />
                                <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminar_Click" onclientclick="return confirm('¿Desea eliminar el registro?');" ToolTip="Eliminar"/>
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

                            <div style="float:left">
                            Registros por página: 15
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

        

            <asp:Button ID="btnMDLAgregarContacto" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlAgregarProducto" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarGrupo" TargetControlID="btnMDLAgregarContacto" BehaviorID="_mdlAgregarProducto">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarGrupo" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:90%;overflow:auto">
                <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="lblAgregarProducto" runat="server" Text=""></asp:Label>
                    </strong>
                  </div>
                    <asp:HiddenField ID="hfIdProducto" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td ><strong>Producto</strong>
                                <asp:TextBox ID="txtNombreProducto"  runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Código</strong>
                                <asp:TextBox ID="txtCodigo" Width="130px" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Bodega</strong>
                                <asp:TextBox ID="txtBodega" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Unidad Medida</strong>
                                <asp:TextBox ID="txtUnidadMedida" Width="100px" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Grupo</strong>
                                <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td><strong>Sub Grupo</strong>
                                <asp:DropDownList ID="ddlSubGrupo" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                            <td><strong>Costo Unitario</strong>
                                <asp:TextBox ID="txtCostoUnitario" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                TargetControlID="txtCostoUnitario"         
                              
                                ValidChars="1234567890,." />
                            </td>
                            <td><strong>Valor Venta</strong>
                                <asp:TextBox ID="txtValorVenta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                TargetControlID="txtValorVenta"         
                                
                                ValidChars="1234567890,." />
                            </td>
                        </tr>
                        <tr class="active">
                            <td colspan="4">
                                <strong>Observación</strong>
                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Height="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Stock</strong>
                                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftbe" runat="server"
                                TargetControlID="txtStock"         
                                
                                ValidChars="1234567890-" />
                                <asp:HiddenField ID="hfStockAntiguo" runat="server" />
                            </td>
                            <td><strong>Ficha Técnica (PDF)</strong>
                                <asp:FileUpload ID="fuFichaTecnica" CssClass="form-control" runat="server" />
                                <asp:HiddenField ID="hfPDF" runat="server" />
                                <asp:LinkButton ID="lbtnVerPdf" runat="server" OnClick="lbtnVerPdf_Click" Visible="false">Ver Ficha Técnica</asp:LinkButton>
                            </td>
                            <td colspan="2"><strong>Imagen</strong>
                                <asp:FileUpload ID="fuImagen" CssClass="form-control" runat="server" />
                                <asp:Image ID="imgImagen" runat="server" CssClass="img-rounded" Height="40px" Width="70px" />
                            </td>
                        </tr>

                        </table>
                    <div class="well well-sm" runat="server" visible="false">
                        <strong>Materiales</strong><br />
                        <asp:GridView ID="grvMateriales" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                            <Columns>
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
                                        <asp:TextBox ID="txtCantidadSeleccionada" Width="80px" runat="server" CssClass="form-control input-sm" Text='<%# Bind("CANTIDAD_SELECCIONADA") %>'></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtCantidadSeleccionada" ValidChars="1234567890-" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>                 
                    </div>

                <div class="panel-footer">
                    <asp:Button ID="btnGrabarProducto" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm" onclick="btnGrabarProducto_Click" />
                    <asp:Button ID="btnModificarProducto" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm" onclick="btnModificarProducto_Click" />
                </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>



        
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
