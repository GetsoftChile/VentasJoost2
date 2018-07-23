<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="crm_valvulas_industriales.Empresas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <ol class="breadcrumb">
        <li><a href="Default.aspx">Inicio</a></li>
        <li><a href="Default.aspx">Administración</a></li>
        <li class="active">Empresas</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        
        <asp:Panel ID="pnlEmpresas" runat="server" DefaultButton="btnBuscar">
        <div class="well well-sm">
        
        <h4>Empresas</h4>
                <div class="form-inline">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary btn-sm" 
                    Text="Buscar" onclick="btnBuscar_Click"
                    />
                    <asp:Button ID="btnNuevoEmpresa" runat="server" Text="Nuevo Empresa" 
                    CssClass="btn btn-success btn-sm" onclick="btnNuevoEmpresa_Click"/>
                </div>
        </div>
        </asp:Panel>
        


        
        
    <asp:GridView ID="grvEmpresas" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowDataBound="paginacion_RowDataBound">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblIdEmpresa" runat="server" Text='<%# Bind("ID_EMPRESA") %>'></asp:Label>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rut">
                            <ItemTemplate>
                                <asp:Label ID="lblRut" runat="server" Text='<%# Bind("RUT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreEmpresa" runat="server"  Text='<%# Bind("NOMBRE_EMPRESA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Giro">
                            <ItemTemplate>
                                <asp:Label ID="lblGiro" runat="server"  Text='<%# Bind("GIRO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono">
                            <ItemTemplate>
                                <asp:Label ID="lblTelefono" runat="server"  Text='<%# Bind("TELEFONO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Imagen">
                            <ItemTemplate>
                                <asp:Image ID="imgImagenEmpresa" ImageUrl='<%# Bind("IMAGEN") %>' CssClass="img-rounded" Height="30px" Width="60px" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <asp:Label ID="lblActivo2" Visible="true" runat="server" Text='<%# Bind("ACTIVO2") %>'></asp:Label>
                                <asp:Label ID="lblActivo" Visible="false" runat="server" Text='<%# Bind("ACTIVO") %>'></asp:Label>
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
                    </strong>
                  </div>
                    <asp:HiddenField ID="hfIdUsuario" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Nombre</strong></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtNombreEmpresa" Width="27%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Rut</strong></td>
                            <td>
                                <asp:TextBox ID="txtRut" runat="server" MaxLength="20" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Giro</strong></td>
                            <td><asp:TextBox ID="txtGiro" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Descripción</strong></td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Email</strong></td>
                            <td><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Teléfono</strong></td>
                            <td><asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Estado</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlEstado" runat="server" Width="150px" CssClass="form-control input-sm">
                                <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Imagen</strong></td>
                            <td colspan="3">
                                <asp:FileUpload ID="fuImagen" runat="server" />
                                <asp:Image ID="imgEmpresa" runat="server" CssClass="img-rounded" Height="40px" Width="70px" />
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
