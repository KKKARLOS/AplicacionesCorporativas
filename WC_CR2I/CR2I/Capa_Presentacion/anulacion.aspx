<%@ Page language="c#" Inherits="CR2I.Capa_Presentacion.Anulacion" CodeFile="Anulacion.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Motivo de la anulacion</title>
	<meta http-equiv="X-UA-Compatible" content="IE=8"> 
	<meta name="vs_defaultClientScript" content="JavaScript" />
    <link rel="stylesheet" href="../App_Themes/Corporativo/Corporativo.css" type="text/css" />	
	<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
<!--
    function init() {
//        if (nName == "chrome") {
//            window.resizeTo(370, 200);
//        }
        switch (nName) {
            case "chrome":
                window.resizeTo(370, 200);
                break;
            case "ie":
                $I("cldCancelar").style.paddingTop = "1px";
                break;
        }
        try {
            $I("txtMotivo").focus();
        } catch (e) { }        
    }
    function aceptar() {
	    //alert("aceptar="+window.returnValue);
//        window.returnValue = "A@#@" + $I("txtMotivo").value;
//        window.close();
        var returnValue = "A@#@" + $I("txtMotivo").value;
        modalDialog.Close(window, returnValue);        
	}
	
	function cancelar(){
	    //alert("aceptar=" + window.returnValue);
//	    window.returnValue = "C@#@";
//		window.close();
	    var returnValue = "C@#@";
	    modalDialog.Close(window, returnValue);     
	}
-->
	</script>
</head>
<body onload="init();">
	<form id="Form1" runat="server">
        <script type="text/javascript">
        <!--
            var strServer = "<%=Session["strServer"]%>";
        -->
        </script>
		<table style="width:100%" border="0">
			<tr>
				<td colspan="2">
					<asp:TextBox id="txtMotivo" style="width:95%; height:60px; margin-top:15px; margin-left:10px;" SkinID="Multi" TextMode="MultiLine" runat="server"></asp:TextBox>
				    <br /><br />
				</td>
			</tr>
		</table>
		<center>
		<table style="width:65%;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="padding-top:0px;" align="center"><button id="btnAceptar" type="button" onclick="aceptar()" style="width:85px;" hidefocus=hidefocus>
                        <span><img src="../images/imgAceptar.gif" />Aceptar</span>
                    </button></td>
				<td style="padding-top:0px;" id="cldCancelar" align="center"><button id="btnCancelar" type="button" onclick="cancelar()" style="width:85px;" hidefocus=hidefocus>
                        <span><img src="../images/Botones/imgCancelar.gif" />&nbsp;Cancelar</span>
                    </button></td>
			</tr>
		</table>
		</center>
	</form>
</body>
</html>
