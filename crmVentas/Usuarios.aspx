<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="crm_fadonel.Usuarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<div class="container-fluid">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlUsuarios" runat="server" DefaultButton="btnBuscar">
        
        <ol class="breadcrumb">
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Usuarios</li>
        </ol>
        
        <div class="well">
        <h4>Usuarios</h4>
                <div class="form-inline">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-success btn-sm" Text="Buscar" 
                        onclick="btnBuscar_Click"/>
                        <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" 
                        CssClass="btn btn-primary btn-sm" onclick="btnNuevoUsuario_Click"/>
                </div>
        </div>
        <asp:GridView ID="grvUsuarios" runat="server" CssClass="table table-bordered table-condensed small" HeaderStyle-BackColor="Control" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowDataBound="paginacion_RowDataBound" OnSorting="gvEmployee_Sorting" AllowSorting="true" >
                    <Columns>
                        <asp:TemplateField HeaderText="Usuario" SortExpression="usuario">
                            <ItemTemplate>
                                <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Bind("ID_USUARIO") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descuento" SortExpression="DESCUENTO_AUTORIZADO">
                            <ItemTemplate>
                                <asp:Label ID="lblDescuento" runat="server" Text='<%# Bind("DESCUENTO_AUTORIZADO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activo" SortExpression="activo">
                            <ItemTemplate>
                                <asp:Label ID="lblActivo" runat="server" Visible="false" Text='<%# Bind("ACTIVO") %>'></asp:Label>
                                <asp:Label ID="lblActivo2" runat="server" Text='<%# Bind("ACTIVO2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Perfil" SortExpression="perfil">
                            <ItemTemplate>
                                <asp:Label ID="lblPerfil" runat="server" Text='<%# Bind("NOMBRE_PERFIL") %>'></asp:Label>
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
        </asp:Panel>

        <asp:Button ID="btnAgregarUsuarioMDL" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlAgregarUsuario" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregar" TargetControlID="btnAgregarUsuarioMDL" BehaviorID="_mdlAgregarObra">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregar" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
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
                            <td>
                                Nombre y Apellido</td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr class="active">
                            <td>
                                Usuario</td>
                            <td>
                                <asp:TextBox ID="txtUsuario" runat="server" Width="200px" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr class="active">
                            <td>
                                Contraseña</td>
                            <td>
                                <asp:TextBox ID="txtContrasena" runat="server" Width="150px"   TextMode="Password" ViewStateMode="Disabled" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr class="active">
                            <td>
                                Confirmar Contraseña</td>
                            <td>
                                <asp:TextBox ID="txtConfirmarContrasena" runat="server"  Width="150px"  TextMode="Password" ViewStateMode="Disabled" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        
                        <tr class="active">
                            <td>
                                Descuento</td>
                            <td>
                                <asp:TextBox ID="txtDescuento" runat="server" Width="200px" MaxLength="4" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr class="active">
                            <td>
                                Activo</td>
                            <td>
                                <asp:DropDownList ID="ddlActivo"  Width="100px"  runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        
                        <tr class="active">
                            <td>Perfil</td>
                            <td>
                                <asp:DropDownList ID="ddlPerfil" Width="100px" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>   
                    
                    
                    <div class="well well-sm">
                        <strong>Empresas</strong><br /><br />
                                <asp:CheckBoxList ID="chkEmpresas" runat="server" Width="100%" 
                                   RepeatColumns="6"
                                   RepeatDirection="Horizontal"
                                   TextAlign="Right">
                                </asp:CheckBoxList>
                        </div>                 
                    </div>
                <div class="panel-footer">
                    <asp:Button ID="btnModificar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm" onclick="btnModificar_Click" />
                    <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm" onclick="btnAgregar_Click" />
                </div> 
                </div>
                
                <%--MODALPOPUP CON BOOTSTRAP--%>
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

</div>
    </ContentTemplate>
 </asp:UpdatePanel>

</asp:Content>
