<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="crm_valvulas_industriales.Lead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



      <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Lead</li>
    </ol>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlLead" runat="server" DefaultButton="btnBuscar">
                <div class="well well-sm">
                    <h4>Lead</h4>
                    <div class="form-inline">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary btn-sm"
                            Text="Buscar" OnClick="btnBuscar_Click" />
                        <asp:Button ID="btnNuevoLead" runat="server" Text="Nuevo Lead"
                            CssClass="btn btn-success btn-sm" OnClick="btnNuevoLead_Click" />
                        <asp:ImageButton ID="ibtnExportarExcel" runat="server"
                            ImageUrl="~/assets/img/export_excel.png" OnClick="ibtnExportarExcel_Click" />
                    </div>
                </div>
            </asp:Panel>

        <asp:GridView ID="grvLead" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowDataBound="paginacion_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lblIdLead" runat="server" Text='<%# Bind("ID_LEAD") %>' Visible="true"></asp:Label>

                                <asp:Label ID="lblTieneSwERP" runat="server" Text='<%# Bind("TIENE_SW_ERP") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblTieneSwCobros" runat="server" Text='<%# Bind("TIENE_SW_COBROS") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblTieneSwTicket" runat="server" Text='<%# Bind("TIENE_SW_TICKET") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblTieneSwVentas" runat="server" Text='<%# Bind("TIENE_SW_VENTAS") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblComentario" runat="server" Text='<%# Bind("COMENTARIO") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblIdUsuarioAsignado" runat="server" Text='<%# Bind("ID_USUARIO_ASIG") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empresa">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("EMPRESA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cargo">
                            <ItemTemplate>
                                <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("CARGO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dirección">
                            <ItemTemplate>
                                <asp:Label ID="lblDireccion" runat="server" Text='<%# Bind("DIRECCION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Comuna">
                            <ItemTemplate>
                                <asp:Label ID="lblComuna" runat="server" Text='<%# Bind("COMUNA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Teléfono 1">
                            <ItemTemplate>
                                <asp:Label ID="lblTelefono1" runat="server" Text='<%# Bind("TELEFONO_1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Teléfono 2">
                            <ItemTemplate>
                                <asp:Label ID="lblTelefono2" runat="server" Text='<%# Bind("TELEFONO_2") %>'></asp:Label>
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
            <asp:ModalPopupExtender ID="mdlAgregarLead" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarGrupo" TargetControlID="btnMDLAgregarContacto" BehaviorID="_mdlAgregarProducto">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarGrupo" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:auto;overflow:auto">
                <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="lblAgregarLead" runat="server" Text=""></asp:Label>
                    </strong>
                  </div>
                    <asp:HiddenField ID="hfIdLead" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td ><strong>Nombre</strong>
                                <asp:TextBox ID="txtNombre"  runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Empresa</strong>
                                <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Cargo</strong>
                                <asp:TextBox ID="txtCargo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Dirección</strong>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Comuna</strong>
                                <asp:DropDownList ID="ddlComuna" runat="server" CssClass="form-control input-sm" OnDataBound="ddlComuna_DataBound"></asp:DropDownList>
                            </td>
                            <td><strong>Email</strong>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 1</strong>
                                <asp:TextBox ID="txtTelefono1" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 2</strong>
                                <asp:TextBox ID="txtTelefono2" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Usuario Asignado</strong>
                                <asp:DropDownList ID="ddlUsuarioAsignado" runat="server" CssClass="form-control input-sm" OnDataBound="ddlUsuarioAsignado_DataBound">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Tiene SW ERP</strong>
                                <asp:DropDownList ID="ddlTieneSwERP" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><strong>Tiene SW Cobros</strong>
                                <asp:DropDownList ID="ddlTieneSwCobros" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </td>
                            <td><strong>Tiene SW Ticket</strong>
                                <asp:DropDownList ID="ddlTieneSwTicket" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><strong>Tiene SW Ventas</strong>
                                <asp:DropDownList ID="ddlTieneSwVentas" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>SW ERP</strong>
                                <asp:TextBox ID="txtSwErp" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>SW Cobros</strong>
                                <asp:TextBox ID="txtSwCobros" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>SW Ticket</strong>
                                <asp:TextBox ID="txtSwTicket" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>SW Ventas</strong>
                                <asp:TextBox ID="txtSwVentas" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td colspan="3"><strong>Comentario</strong>
                                <asp:TextBox ID="txtComentario" runat="server" Height="100" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Activos</strong>
                                <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        </table>

                <div class="panel-footer">
                    <asp:Button ID="btnGrabarLead" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm" onclick="btnGrabarLead_Click" />
                    <asp:Button ID="btnModificarLead" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm" onclick="btnModificarLead_Click" />
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
