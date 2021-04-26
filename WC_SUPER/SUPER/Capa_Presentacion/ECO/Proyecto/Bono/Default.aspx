<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Asignación de bonos de transporte</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()" style="margin-top:10px;">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var sNodo = "<%=sNodo %>";
</script>
<center>
<table style="width: 916px; text-align:left;">
    <tr>
        <td>
	        <table id="tblTitulo" style="width: 900px; height: 17px; margin-top:5px;">
                <colgroup>
                 <col style='width:15px;' />
                 <col style='width:20px;' />
                 <col style='width:300px;' />
                 <col style='width:65px;' />
                 <col style='width:62px;' />
                 <col style='width:258px;' />
                 <col style='width:90px;' />
                 <col style='width:90px;' />
                </colgroup>
		        <tr class="TBLINI">
				    <td></td>
				    <td></td>
				    <td><img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" usemap="#imgCod" border="0"> 
				        <map name="imgCod">
					        <area onclick="ot('tblDatos', 2, 0, '', 'scrollTablaAE()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 2, 1, '', 'scrollTablaAE()')" shape="RECT" coords="0,6,6,11">
				        </map>					
				        Profesional&nbsp;
					    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa1')"
						    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				        <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa1',event)"
						    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				    </td>
				    <td title="Fecha de alta del profesional en el proyecto">FAPP</td>
				    <td title="Fecha de baja del profesional en el proyecto">FBPP</td>
				    <td><img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" usemap="#imgBono" border="0"> 
				        <map name="imgBono">
					        <area onclick="ot('tblDatos', 5, 0, '', 'scrollTablaAE()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 5, 1, '', 'scrollTablaAE()')" shape="RECT" coords="0,6,6,11">
				        </map>					
				        Bono&nbsp;
					    <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
						    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				        <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa3', event)"
						    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
			        </td>
				    <td title="Fecha de inicio de asignación del bono">FIAB</td>
				    <td title="Fecha de fin de asignación del bono">FFAB</td>
		        </tr>
	        </table>
	        <div id="divCatalogo" style="overflow-y:auto; overflow-x:hidden; width:916px; height:520px;" onscroll="scrollTablaAE();">
	            <div style='background-image:url(../../../../Images/imgFT20.gif); width:900px'>
	            <%=strTablaHtml %>
	            </div>
            </div>
	        <table style="width: 900px; height: 17px">
		        <tr class="TBLFIN"><td></td></tr>
	        </table>
        </td>
    </tr>
    <tr>
        <td style="padding-top:5px;">
            <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
        </td> 
    </tr>
</table>
</center>
<br />
<center>
<table style="width:390px; margin-top:15px">
    <colgroup><col style="width:130px;"/><col style="width:130px;"/><col style="width:130px;"/></colgroup>
	<tr> 
		<td>
			<button id="btnGrabar" type="button" onclick="grabarAux()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>			 
		  </td>
		<td> 
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">&nbsp;Grabar...</span>
			</button>			
		  </td>
		<td style="padding-left:5px;">
			<button id="btnSalir" type="button" onclick="salir()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="">&nbsp;&nbsp;Salir</span>   
			</button>  
		</td>
	  </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnPSN" id="hdnPSN" value="" />
<input type="hidden" runat="server" name="hdnEstadoPSN" id="hdnEstadoPSN" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
		function __doPostBack(eventTarget, eventArgument) {
		    var bEnviar = true;
		    if (eventTarget.split("$")[2] == "Botonera") {
		        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
//		        switch (strBoton) {
//		            case "grabar":
//		                {
//		                    bEnviar = false;
//		                    mostrarProcesando();
//		                    setTimeout("grabar();", 20);
//		                    break;
//		                }
//		        }
		    }

		    var theform = document.forms[0];
		    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		    theform.__EVENTARGUMENT.value = eventArgument;
		    if (bEnviar) theform.submit();
		}

        function WebForm_CallbackComplete() {
            for (var i = 0; i < __pendingCallbacks.length; i++) {
                callbackObject = __pendingCallbacks[i];
                if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                    WebForm_ExecuteCallback(callbackObject);
                    if (!__pendingCallbacks[i].async) {
                        __synchronousCallBackIndex = -1;
                    }
                    __pendingCallbacks[i] = null;
                    var callbackFrameID = "__CALLBACKFRAME" + i;
                    var xmlRequestFrame = document.getElementById(callbackFrameID);
                    if (xmlRequestFrame) {
                        xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                    }
                }
            }
        }
</script>
</body>
</html>

