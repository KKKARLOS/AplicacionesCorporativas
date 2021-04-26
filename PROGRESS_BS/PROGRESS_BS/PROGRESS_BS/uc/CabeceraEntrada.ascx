<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CabeceraEntrada.ascx.cs" Inherits="uc_CabeceraEntrada" %>


<!-- Bootstrap -->

<link href="<% =Session["strServer"].ToString() %>css/bootstrap.min.css" rel="stylesheet" />
<!--Icon Fonts FontAwsome-->
<link href="<% =Session["strServer"].ToString() %>css/font-awesome.min.css" rel="stylesheet" />
<link href="<% =Session["strServer"].ToString() %>css/main.css" rel="Stylesheet" />
<!-- css propio de la página-->
<link href="css/StyleSheet.css" rel="stylesheet" />

<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->



<script>
    var strServer = "<%=Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 

</script>

<iframe name="iFrmSession" id="iFrmSession" src="<%=Session["strServer"].ToString() %>uc/ControlSesion.aspx" style="visibility:hidden;width:1px;height:1px"></iframe>
<script>

    function actualizarSession() {
        try {
            if (window.frames["iFrmSession"].nIDTimeMin==undefined) return;
            if (window.frames["iFrmSession"].nIDTimeMin==null) return;
            //Método al que solo se accede desde la ventana principal
            window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeMin);
            window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeSeg);
            window.frames["iFrmSession"].nSession = intSession + 1;
            window.frames["iFrmSession"].restaSession();
            window.top.CerrarMensajeSession();
        } catch (e) {
            alertNew("danger", "Error al actualizar la caducidad de sesión");
        }
    }
    function ReiniciarSession() {
        try {
            document.getElementById("iFrmSession").src = strServer + "uc/ControlSesion.aspx";
            document.getElementById("CabeceraMenu_lblSession").innerText = "La sesión caducará en " + intSession + " min.";
        } catch (e) {
            alertNew("danger", "Error al reiniciar la sesión.");
        }
    }

</script>