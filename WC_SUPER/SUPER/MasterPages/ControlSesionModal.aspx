<%@ Page Language="C#" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Control de caducidad de sesión en ventanas modales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script type="text/javascript">
    <!--
        function restaSessionModal() {
            if (nSession == 2) {
                nSeg = 60;
                bSeg = true;
                restaSegundosModal();
                return;
            } else {
                nSession--;
                opener.$I("ctl00_lblSession").innerText = "La sesión caducará en " + nSession + " min.";
                nSeg = 60;
                bSeg = false;
                nIDTimeMin = setTimeout("restaSessionModal();", 60000);
            }
        }

        var nSeg = 60;
        var bSeg = false;
        function restaSegundosModal(){
            //if (nSeg == 0){
            if (nSeg <= 1){
                clearTimeout(nIDTimeSeg);
                opener.bCambios = false;
                opener.returnValue = null
                opener.close();
                try {
                    opener.frames["iFrmSessionModal"].nSeg = 0;
                    opener.frames["iFrmSessionModal"].restaSegundosModal();
                } catch (e) { };
                try {
                    opener.frames["iFrmSession"].nSeg = 0;
                    opener.frames["iFrmSession"].restaSegundos();
                    } catch (e) { };   
            } else {
                if (bSeg) {
                    nSeg--;
                    opener.$I("ctl00_lblSession").innerText = "La sesión caducará en " + nSeg + " seg.";
                    nIDTimeSeg = setTimeout("restaSegundosModal();", 1000);
                }
            }
        }

        function init() {
            if (window.top.nName == "safari") {
                window.top.killCSSRule(".combo");
                window.top.document.styleSheets[0].addRule(".combo", "padding: 0px; margin: 0px; font-size: 11px; top: 0px; left: 0px; font-family: Arial, Helvetica, sans-serif;");
                var aCombos = window.top.document.getElementsByTagName("select");
                for (var i = 0; i < aCombos.length; i++) {
                    aCombos[i].className = "combo";
                }
            }
        }
    -->
    </script>
</head>
<body onload="init();">
    <form id="form1" runat="server">
    <script type="text/javascript">
    <!--
        var nSession = <%=Session.Timeout%>;  //Caducidad en min.
        var nIDTimeMin;
        var nIDTimeSeg;
        nIDTimeMin = setTimeout("restaSessionModal();", 60000);
        if (window.top!=null) window.top.actualizarSession();
    -->
    </script>
    <!--<div id='hdnDIVTableAux'></div>-->
    </form>
</body>
</html>
