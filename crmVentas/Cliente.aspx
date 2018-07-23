<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="crm_fadonel.Cliente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Clientes</li>
    </ol>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlClientes" runat="server" DefaultButton="btnBuscar">
        <div class="well well-sm">
            <asp:HiddenField ID="hfIdCliente" runat="server" />
        <h4>Clientes</h4>
                <div class="form-inline">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary btn-sm" 
                    Text="Buscar" onclick="btnBuscar_Click"
                    />
                    <asp:Button ID="btnNuevoCliente" runat="server" Text="Nuevo Cliente" 
                    CssClass="btn btn-success btn-sm" onclick="btnNuevoCliente_Click"/>

                    <asp:ImageButton ID="ibtnExportarExcel" OnClick="ibtnExportarExcel_Click" runat="server" ImageUrl="~/assets/img/export_excel.png" />

                </div>
        </div>
        </asp:Panel>


        
    <asp:GridView ID="grvClientes" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowDataBound="paginacion_RowDataBound">
                    <Columns>
                        
                        
                        <asp:TemplateField HeaderText="Rut">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCliente" runat="server" Visible="false" Text='<%# Bind("ID_CLIENTE") %>'></asp:Label>
                                <asp:Label ID="lblRut" runat="server" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                                <asp:Label ID="lblComuna" runat="server" Visible="false" Text='<%# Bind("COMUNA") %>'></asp:Label>
                                <asp:Label ID="lblCiudad" runat="server" Visible="false" Text='<%# Bind("CIUDAD") %>'></asp:Label>
                                <asp:Label ID="lblInfo" runat="server" Visible="false" Text='<%# Bind("INFO") %>'></asp:Label>
                                <asp:Label ID="lblMontoCredito" runat="server" Visible="false" Text='<%# Bind("MONTO_CREDITO") %>'></asp:Label>
                                <asp:Label ID="lblClasificacion" runat="server" Visible="false" Text='<%# Bind("CLASIFICACION") %>'></asp:Label>
                                <asp:Label ID="lblZona" runat="server" Visible="false" Text='<%# Bind("ZONA") %>'></asp:Label>
                                <asp:Label ID="lblIdEstadoCliente" runat="server" Visible="false" Text='<%# Bind("ID_ESTADO_CLIENTE") %>'></asp:Label>
                                <asp:Label ID="lblGiro" runat="server" Visible="false" Text='<%# Bind("GIRO") %>'></asp:Label>
                                <asp:Label ID="lblIdCondicionVenta" runat="server" Visible="false" Text='<%# Bind("CONDICION_DE_VENTA") %>'></asp:Label>

                                <asp:Label ID="lblIdActividadComercial" runat="server" Visible="false" Text='<%# Bind("ID_ACTIVIDAD_COMERCIAL") %>'></asp:Label>
                                <asp:Label ID="lblUrl" runat="server" Visible="false" Text='<%# Bind("URL") %>'></asp:Label>
                                <asp:Label ID="lblIdCampana" runat="server" Visible="false" Text='<%# Bind("ID_CAMPANA") %>'></asp:Label>
                                <asp:Label ID="lblObservacion" runat="server" Visible="false" Text='<%# Bind("OBSERVACION") %>'></asp:Label>
                                
                                <asp:Label ID="lblRutClientePadre" runat="server" Visible="false" Text='<%# Bind("RUT_CLIENTE_PADRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Razon Social">
                            <ItemTemplate>
                                <asp:Label ID="lblRazonSocial" runat="server" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Nombre corto">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreCorto" runat="server" Text='<%# Bind("NOMBRE_CORTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Teléfono">
                            <ItemTemplate>
                                <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("TELEFONO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Dirección">
                            <ItemTemplate>
                                <asp:Label ID="lblDireccion" runat="server" Text='<%# Bind("DIRECCION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblActivo" runat="server" Visible="true" Text='<%# Bind("NOM_ESTADO_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <asp:Label ID="lblActivo1"  runat="server" Visible="false" Text='<%# Bind("ACTIVO") %>'></asp:Label>
                                <asp:Label ID="lblActivo2"  runat="server" Visible="true" Text='<%# Bind("ACTIVO2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Archivos">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBilioteca" runat="server" ImageUrl="assets/img/book.png" OnClick="imgBilioteca_Click" ToolTip="Archivos"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Contactos">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgContactos" runat="server" ImageUrl="assets/img/reseller_programm.png" OnClick="imgContactos_Click" ToolTip="Contactos"  />
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
                            <div style="float:right">
                                Página
                                <asp:Label ID="lblPagina" runat="server"></asp:Label>
                                de
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </div>
                        </div>
                    </PagerTemplate>
                </asp:GridView>


                
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnAgregarClienteMDL" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlAgregarCliente" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregar" TargetControlID="btnAgregarClienteMDL" BehaviorID="_mdlAgregarCliente">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregar" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:auto;overflow:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="lblAgregarUsuario" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hfIdLead" runat="server" />
                    </strong>
                  </div>
                    <asp:HiddenField ID="hfIdUsuario" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Razon Social</strong></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtRazonSocial" Width="27%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Rut</strong></td>
                            <td>
                                <asp:TextBox ID="txtRut" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Nombre Corto</strong></td>
                            <td><asp:TextBox ID="txtNombreCorto" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Dirección</strong></td>
                            <td>
                                <asp:TextBox ID="txtDireccion" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Comuna</strong></td>
                            <td>
                                <asp:TextBox ID="txtComuna" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Ciudad</strong></td>
                            <td>
                                <asp:TextBox ID="txtCiudad" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Teléfono</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefono" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Email</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Info</strong></td>
                            <td>
                                <asp:TextBox ID="txtInfo" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Monto Credito</strong></td>
                            <td><asp:TextBox ID="txtMontoCredito" runat="server"  CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Clasificacion</strong></td>
                            <td><asp:TextBox ID="txtClasificacion" runat="server"  CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Zona</strong></td>
                            <td><asp:TextBox ID="txtZona" runat="server"  CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Condición de Venta</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlCondicionDeVenta" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Giro</strong></td>
                            <td><asp:TextBox ID="txtGiro" runat="server"  CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Estado</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlEstado" runat="server" Width="150px" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="active">
                            <td><strong>Url</strong></td>
                            <td><asp:TextBox ID="txtUrl" runat="server"  CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Act Comercial</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlActividadComercial" runat="server" Width="150px" CssClass="form-control input-sm" OnDataBound="ddlActividadComercial_DataBound">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="active">
                            <td><strong>Observación</strong></td>
                            <td><asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Height="50px"></asp:TextBox></td>
                            <td><strong>Campaña</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlCampana" runat="server" CssClass="form-control input-sm" OnDataBound="ddlCampana_DataBound">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Rut Padre</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRutPadre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Activo</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlActivo" runat="server" Width="150px" CssClass="form-control input-sm">
                                    <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </table>

                <div class="panel-footer">
                    <asp:Button ID="btnModificar" runat="server" Text="Guardar" CssClass="btn btn-primary" onclick="btnModificar_Click" />
                    <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary" onclick="btnAgregar_Click" />
                </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
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

                    
    <asp:GridView ID="grvContactos" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
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
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/assets/img/edit_button.png" OnClick="imgEditarContacto_Click" ToolTip="Editar"  />
                                <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminarContacto_Click" onclientclick="return confirm('¿Desea eliminar el registro?');" ToolTip="Eliminar"/>
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
                    <asp:Label ID="lblAgregarContacto" runat="server" Text=""></asp:Label>
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
                            <td>
                                <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Celular</strong></td>
                            <td>
                                <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 1</strong></td>
                            <td>
                                <asp:TextBox ID="txtTelefono1" runat="server" CssClass="form-control input-sm"></asp:TextBox>
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
            <asp:Button ID="btnMdlAgregarArchivo" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlAgregarArchivo" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarArchivo" TargetControlID="btnMdlAgregarArchivo" BehaviorID="_btnMdlAgregarArchivo">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarArchivo" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:auto;overflow:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="Label2" runat="server" Text="Agregar Archivo"></asp:Label>
                    </strong>
                  </div>
                    <asp:HiddenField ID="hfRutClienteArchivo" runat="server" />
                    <table class="table small">
                    <tr class="active">
                    <td>
                        <strong>Nombre</strong>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </td>
                    <td>
                        <strong>Archivo</strong>
                        <asp:FileUpload ID="fuArchivo" runat="server"  CssClass="form-control input-sm" />
                    </td>
                    <td>
                    <br />
                    <asp:Button ID="btnGrabarArchivo" runat="server" CssClass="btn btn-danger btn-sm" 
                        Text="Guardar" onclick="btnGrabarArchivo_Click"/></td>
                    </tr>
                    
                    </table>
                

                
    <asp:GridView ID="grvDetalleBiblioteca" HeaderStyle-CssClass="active" runat="server" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" onrowdatabound="grvDetalleBiblioteca_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="Archivo">
                <ItemTemplate>
                    <asp:ImageButton ID="imgVisualizar" CausesValidation="False" runat="server"  Visible="true" ImageUrl="~/assets/img/file_manager.png"  ToolTip="Seleccionar" OnClick="imgVisualizar_Click"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server"  Text='<%# Bind("NOMBRE") %>' Visible="true"></asp:Label>

                    <asp:Label ID="lblId" runat="server" Visible="false"  Text='<%# Bind("ID_CLIENTE_ARCHIVO") %>'></asp:Label>
                    <asp:Label ID="lblRuta" runat="server"  Text='<%# Bind("RUTA") %>' Visible="false" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Archivo">
                <ItemTemplate>
                    <asp:Label ID="lblNombreArchivo" runat="server"  Text='<%# Bind("ARCHIVO") %>' Visible="true"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Eliminar" HeaderStyle-Width="5%">
                <ItemTemplate>
                    <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminarArchivo_Click" onclientclick="return confirm('¿Desea eliminar el registro?');" ToolTip="Eliminar"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>


            
<%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button1" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlImagen" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlImagen" TargetControlID="Button1">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlImagen" runat="server" CssClass="panel" style="background:white; width:auto;  height:auto; overflow:auto; display:none">
                <div class="panel panel-default">
                  <div class="panel-heading">
<%--                    <button class="close" data-dismiss="modal">
                        ×
                    </button>--%>
                      <asp:Button ID="btnCerrarImagen" CssClass="close" runat="server" Text="x" OnClick="imgCerrar_Click"/>
                    <h4>Imagen</h4>
                  </div>
                  <div class="text-center">
                    <asp:Image ID="imgFoto" CssClass="img-rounded" Width="60%"  runat="server" />
                  </div>
                
                </div>
            </asp:Panel>




        <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <div class="panel panel-red">
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
