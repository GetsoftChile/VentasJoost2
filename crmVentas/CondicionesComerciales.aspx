<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="CondicionesComerciales.aspx.cs" Inherits="crm_valvulas_industriales.CondicionesComerciales" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Condiciones Comerciales</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlProductos" runat="server" DefaultButton="btnBuscar">
        <div class="well well-sm">
        <h4>Condición Comercial</h4>
                <div class="form-inline">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary btn-sm" 
                        Text="Buscar" onclick="btnBuscar_Click"
                        />
                        <asp:Button ID="btnNuevoCondicionComercial" runat="server" Text="Nueva Condición Comercial" 
                        CssClass="btn btn-success btn-sm" onclick="btnNuevoCondicionComercial_Click"/>
                </div>
            </div>
        </asp:Panel>


        
        <asp:GridView ID="grvProductos" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowDataBound="paginacion_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID_COND_COMERCIAL") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblCondicionComercial" runat="server" Text='<%# Bind("CONDICION_COMERCIAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <asp:Label ID="lblActivo2" runat="server" Text='<%# Bind("ACTIVO2") %>'></asp:Label>
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
            <asp:Panel ID="pnlAgregarGrupo" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:auto;overflow:auto">
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
                            <td ><strong>Nombre</strong>
                                <asp:TextBox ID="txtNombreProducto"  runat="server" Width="40%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr class="active">
                            <td>
                                <strong>Activo</strong>
                                <asp:DropDownList ID="ddlActivo"  Width="100px"  runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
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
            <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:auto; height:auto">
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
