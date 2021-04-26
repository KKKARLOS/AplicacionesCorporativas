<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Head.ascx.cs" Inherits="Capa_Presentacion_bsUserControls_Head" %>

<!-- Bootstrap -->
<link href="<% =Session["strServer"].ToString() %>css/bootstrap.min.css" rel="stylesheet" />

<!--Icon Fonts FontAwsome-->
<link href="<% =Session["strServer"].ToString() %>css/font-awesome.min.css" rel="stylesheet" />
<link href="<% =Session["strServer"].ToString() %>css/SUPER.css" rel="Stylesheet" />

<asp:Literal runat="server" id="ltrPreCss"></asp:Literal>

<!-- css propio de la página-->
<link href="css/StyleSheet.css?20170517_01" rel="stylesheet" />

<asp:Literal runat="server" id="ltrPostCss"></asp:Literal>

<!-- Etiquetas Meta -->
<meta charset="utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta http-equiv='Expires' content='0' />
<meta http-equiv='Pragma' content='no-cache' />
<meta http-equiv='Cache-Control' content='no-cache' />
<meta name="apple-mobile-web-app-capable" content="yes" />



<script src="<% =Session["strServer"].ToString() %>scripts/jquery-2.2.1.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/bootstrap.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/IB.js?20170517_01"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/dal.js?20171009_01"></script>

<script>
    IB.vars.strserver = "<% =Session["strServer"].ToString() %>";
    
    window.moveTo(0, 0);
    window.resizeTo(screen.availWidth, screen.availHeight);

    $(document).ready(function () {
        document.body.scroll = "yes";
        IB.sesion.init(<% =Session.Timeout %>); //Iniciar timeout de session
    })
</script>