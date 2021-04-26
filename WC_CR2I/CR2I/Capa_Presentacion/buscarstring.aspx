<%@ Page Language="C#" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Buscar</title>
<meta http-equiv="X-UA-Compatible" content="IE=8"> 
<link rel="stylesheet" href="../App_Themes/Corporativo/Corporativo.css" type="text/css" />
<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body class="FondoBody" style="margin-left:17px; margin-top:10px;" onload="init();">
    <br /><br /><br />
    <center>
	    <input type="text" id="txtString" class="textareaTexto" name="txtString" style="width:150px; border: #315d6b 1px solid;PADDING: 0px 0px 0px 2px;FONT-SIZE: 11px;TOP: 0px;LEFT: 0px;MARGIN: 1px 1px 1px 4px; FONT-FAMILY: Arial, Helvetica, sans-serif;  BACKGROUND-IMAGE: url(../images/fondoTxt.gif); HEIGHT: 14px;" onKeyPress="javascript:if(event.keyCode==13){aceptar();event.keyCode=0;}" />
        <br /><br /><br /><br />
        <button id="btnCancelar" type="button" onclick="cerrarVentana()" style="width:85px; margin-left:10px;" hidefocus=hidefocus>
            <span><img src="../images/Botones/imgCancelar.gif" />Cancelar</span>
        </button>    
	</center>
	<script type="text/javascript">
<!--
function init() {
    if (nName == "chrome") {
        window.resizeTo(300, 160);
    }
    try {
        $I("txtString").focus();
    } catch (e) { }    
}
function aceptar() {
	var strString = "";
	strString = $I("txtString").value;
	if (strString == ""){
		alert("Introduce la cadena a buscar.");
		$I("txtString").focus();
		return;
	}
//	window.returnValue = strString;
//	window.close();
	var returnValue = strString;
	modalDialog.Close(window, returnValue);    
}

function cerrarVentana(){
//	window.returnValue = null;
//	window.close();
	var returnValue = null;
	modalDialog.Close(window, returnValue);    	
}
-->
	</script>
</body>
</html>
