<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ControlSesion.aspx.cs" Inherits="MasterPages_ControlSesion" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Location="None" VaryByParam="None" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Control de caducidad de sesión</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
    <script type="text/javascript">
    <!--
        function restaSession(){
            if (nSession == 2){
                nSeg = 60;
                bSeg = true;
                clearTimeout(nIDTimeMin);
                restaSegundos();
                return;
            }else{
                nSession--;
                if (document.all)
                    window.top.$I("ctl00_lblSession").innerText = "La sesión caducará en " + nSession + " min.";
                else
                    window.top.$I("ctl00_lblSession").textContent = "La sesión caducará en " + nSession + " min.";
                    
                if (nSession == 5){
                    window.top.focus();
                    window.top.MostrarMensajeSession();
                }
                nSeg = 60;
                bSeg = false;
                nIDTimeMin = setTimeout("restaSession();", 60000);
            }
        }

        var nSeg = 60;
        var bSeg = false;
        function restaSegundos(){
            if (nSeg == 0){
                clearTimeout(nIDTimeSeg);
			    window.top.bCambios = false;
                window.top.location.href = strServer + "SesionCaducada.aspx";
            }else{
                if (bSeg){
                    nSeg--;
                    if (document.all)
                        window.top.$I("ctl00_lblSession").innerText = "La sesión caducará en " + nSeg + " seg.";
                    else
                        window.top.$I("ctl00_lblSession").textContent = "La sesión caducará en " + nSeg + " seg.";
                        
                    nIDTimeSeg = setTimeout("restaSegundos();", 1000);
                }
            }
        }

        function setDiamante(){
            try{
                var nValor = 0;
                //alert(window.top.$I("ctl00_Cabecera1_imgDiamante").src);
                if (window.top.$I("ctl00_Cabecera1_imgDiamante").src.indexOf("Fijo") != -1){
                    window.top.$I("ctl00_Cabecera1_imgDiamante").src = strServer + "images/imgDiamante.gif";
                    nValor = 1;
                }else{
                    window.top.$I("ctl00_Cabecera1_imgDiamante").src = strServer + "images/imgDiamanteFijo.gif";
                }
                
                var js_args = "setDiamante@#@";
                js_args += nValor;
                
                RealizarCallBack(js_args, "");
	        }catch(e){
		        mostrarErrorAplicacion("Error al ir a grabar los datos.", e.message);
            }
        }
        
        function RespuestaCallBack(strResultado, context){
            window.top.actualizarSession();
            var aResul = strResultado.split("@#@");
            if (aResul[1] != "OK"){
                ocultarProcesando();
                var reg = /\\n/g;
		        mostrarError(aResul[2].replace(reg, "\n"));
            }else{
                switch (aResul[0]){
                    case "setDiamante":
                        break;
                    case "SalirSession":
                        break;
                }
                //ocultarProcesando();
            }
        }
        
        function SalirSession(){
            try{
                var js_args = "SalirSession@#@";
                RealizarCallBack(js_args, "");
	        }catch(e){
		        mostrarErrorAplicacion("Error al ir a abandonar la sesión JS.", e.message);
            }
        }

        function goInicio() {
            try {
                window.top.location.href = strServer + "Capa_Presentacion/Inicio/Default.aspx";
            } catch (e) {
                //mostrarErrorAplicacion("Error al ir a abandonar la sesión JS.", e.message);
            }
            return;
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
        var strServer = '<%=(Session["GVT_strServer"]!=null)? Session["GVT_strServer"].ToString(): "../"%>';
        var nSession = <%=Session.Timeout%>;  //Caducidad en min.
        var nIDTimeMin = null;
        var nIDTimeSeg = null;
        nIDTimeMin = setTimeout("restaSession();", 60000);
    -->
    </script>
    <!--<div id='hdnDIVTableAux'></div>-->
    </form>
</body>
</html>
