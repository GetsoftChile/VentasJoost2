<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="DetalleGr.aspx.cs" Inherits="crm_valvulas_industriales.DetalleGr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<ol class="breadcrumb">
    <li><a href="Default.aspx">Administración</a></li>
    <li class="active">Cargas Generales</li>
</ol>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:HiddenField ID="hfVendedor" runat="server" />
        <asp:HiddenField ID="hfFechaDesde" runat="server" />
        <asp:HiddenField ID="hfFechaHasta" runat="server" />

    <div class="panel panel-primary">
                <div class="panel-heading">

                    <strong>Detalle Llamadas</strong>
                    <asp:ImageButton ID="ibtnExportarExcel" ImageAlign="Right" 
                        ImageUrl="~/assets/img/file_extension_xls.png" runat="server" 
                        onclick="ibtnExportarExcel_Click" />

                </div>
        <asp:GridView ID="grvSeguimiento" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnRowDataBound="paginacion_RowDataBound" EmptyDataText="No hay registros para esta consulta">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblIdCallCenter" runat="server" Text='<%# Bind("ID_CALLCENTER") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaCotizacion" runat="server"  Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ejecutivo">
                        <ItemTemplate>
                            <asp:Label ID="lblEjecutivo" runat="server"  Text='<%# Bind("EJECUTIVO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Llamadas" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoCotizacion2" runat="server" Text='<%# Eval("LLAMADOS", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tiempo Ocupación" ItemStyle-HorizontalAlign="Right" >
                        <ItemTemplate>
                            <asp:Label ID="lblMontoVentan2" runat="server" Text='<%# Eval("TIEMPO_OCUPACION", "{0:n0}") %>'></asp:Label>
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



    </ContentTemplate>
</asp:UpdatePanel>



</asp:Content>
