<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="crm_fadonel.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="shortcut icon" href="../../assets/ico/favicon.ico"/>


    <title>Bienvenido a CRM Ventas</title>

    <!-- Bootstrap core CSS -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- Custom styles for this template -->
    <link href="assets/css/signin.css" rel="stylesheet"/>

    <!-- Just for debugging purposes. Don't actually copy this line! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->


    <style type="text/css">
      body {
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;


       background: url(assets/img/RichardRae-Dakar2015-full-3.jpeg) no-repeat center center fixed;
      -webkit-background-size: cover;
      -moz-background-size: cover;
      -o-background-size: cover;
      background-size: 100% 100%;
 
      height: 100%;
      color: #fff;
      text-align: center;
      text-shadow: 0 1px 3px rgba(0,0,0,.5);

      }
      
      .modalBackground 
      {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
        z-index: 10000;
      }
    </style>
</head>
<body>
    <div class="container">
      <form id="Form1" class="form-signin " role="form" runat="server">
<%--    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <br /><br /><br /><br /><br />

    <h3 class="form-signin-heading">Publicidad Exterior SPA</h3>
        <asp:TextBox ID="txtRut" runat="server" class="form-control" placeholder="Usuario" required autofocus></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password" placeholder="Contraseña" required></asp:TextBox>
        
        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" 
            CssClass="btn btn-lg btn-primary btn-block" onclick="btnIngresar_Click" />
<%--
            <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" style="display:none; background:white; width:40%; height:auto">
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
            </asp:Panel>--%>

<%--    </ContentTemplate>
    </asp:UpdatePanel>
--%>
           
        
      </form>
      </div>
 <!-- /container -->

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->


</body>
</html>
