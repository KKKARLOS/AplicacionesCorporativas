<%@ Control Language="c#" Inherits="GESTAR.Capa_Presentacion.UserControls.Session" CodeFile="Session.ascx.cs" %>
<style type=text/css>
.msn {FONT-WEIGHT: normal; FONT-SIZE: 11px; PADDING-top: 10px; FONT-FAMILY: Arial}
A.msn:link {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:visited {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:active {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
</style>
	
<script type=text/JavaScript>
<!--
    function MostrarMensajeSession() {
        var a = ocultarCombos();
        el = document.getElementById('popupWin_Session');
        el.style.display = 'block';
    }
    function CerrarMensajeSession() {
        var a = mostrarCombos();
        el = document.getElementById('popupWin_Session');
        el.style.display = 'none';
    }
    function mostrarCombos() {
        var aCombos = document.getElementsByTagName("SELECT");
        for (var i = 0; i < aCombos.length; i++) {
            aCombos[i].style.visibility = "visible";
        }
        return true;
    }
    function ocultarCombos() {
        var aCombos = document.getElementsByTagName("SELECT");
        for (var i = 0; i < aCombos.length; i++) {
            aCombos[i].style.visibility = "hidden";
        }
        return true;
    }
-->
</script>

<div id="popupWin_Session"
	 style="BORDER-RIGHT: #455690 1px solid; 
			PADDING-RIGHT: 2px; 
			BORDER-top: #b9c9ef 1px solid; 
			DISPLAY: none; 
			PADDING-left: 2px; 
			Z-INDEX: 9999; 
			BACKGROUND: #e0e9f8; 
			PADDING-BOTTOM: 2px; 
			BORDER-left: #b9c9ef 1px solid; 
			PADDING-top: 2px; 
			BORDER-BOTTOM: #455690 1px solid; 
			position: absolute; 
			width: 281px; 
			height: 180px">

 	<div id="popupWin_header_Session"
		style="DISPLAY: block; 
		    FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF'); 
		    left: 2px; 
		    FONT: 11px arial,sans-serif; 
		    width: 276px; 
		    cursor: default; 
		    COLOR: #1f336b; 
		    position: absolute; 
		    top: 2px; 
		    height: 16px; 
		    TEXT-ALIGN: left; 
		    TEXT-DECORATION: none">
		
		<span id="popupWintitleEl_Session">
			<span style="COLOR: #1f336b; FONT-FAMILY: Arial, Helvetica, sans-serif">&nbsp;Caducidad de sesión</span>
		</span>

		<span onmouseover="style.color='#455690';" 
			style="RIGHT: 2px; 
			FONT: bold 12px arial,sans-serif; 
			cursor: pointer; 
			COLOR: #728eb8; 
			position: absolute; 
			top: 1px" 
			onclick="CerrarMensajeSession();"
			onmouseout="style.color='#728EB8';">
			
			<img height="13px" title="Cerrar" src="<%=strUrl %>imgCerrarBanner.gif" width="13px" border="0"> 

		</span>
	</div>

	<div id="popupWin_content_Session"
		style="BORDER-RIGHT: #b9c9ef 1px solid; 
		PADDING-RIGHT: 2px; 
		BORDER-top: #728eb8 1px solid; 
		DISPLAY: block; 
		PADDING-left: 2px; 
		BACKGROUND: #e0e9f8; 
		FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF'); 
		left: 2px; 
		PADDING-BOTTOM: 2px; 
		overflow: hidden; 
		BORDER-left: #728eb8 1px solid; 
		PADDING-top: 2px; 
		BORDER-BOTTOM: #b9c9ef 1px solid; 
		position: absolute; 
		top: 20px; 
		width: 275px; 
		height: 154px; 
		TEXT-ALIGN: left">
		
		<span style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif">
			<p style="FONT-SIZE: 11px; PADDING-top: 0px"><br />
				<table border="0" cellpadding="1" cellspacing="0" width="100%">
					<tr><td width="4%"></td>
						<td width="20%"><img Border=0 src="<%=strUrl %>imgInfo.gif" width="48px" height="48px"></td>
						<td width="4%"></td>
						<td width="72%" valign="middle" class="msn">
						La sesión de GESTAR va a caducar en breve.<br /><br />
                        ¿Desea reiniciar el tiempo de la sesión?<br />
                        Si pulsa "No" podría perder los datos pendientes de grabar.<br /><br />
                            <table id="tblSesion" border="0" cellspacing="0" cellpadding="0" align="center" width="100%">
                                <tr>
                                    <td>
                                        <table id="tblSI" border="0" cellspacing="0" cellpadding="0" align="center">
                                            <tr onclick="ReiniciarSession();CerrarMensajeSession();" style="cursor:pointer"> 
                                                <td width="7px"><img src="<%=strUrl %>imgBtnIzda.gif" width="7px"></td>
								                <td align="center" width="20" background="<%=strUrl %>bckBoton.gif"><img src="<%=strUrl %>imgSI.gif" border="0"></td>
                                                <td width="15px" background="<%=strUrl %>bckBoton.gif" align="center" class="txtBot"><a href="#" hidefocus>&nbsp;Sí</a></td>
                                                <td width="7px"><img src="<%=strUrl %>imgBtnDer.gif" width="7px"></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table id="tblNO" border="0" cellspacing="0" cellpadding="0" align="center">
                                            <tr onclick="CerrarMensajeSession();" style="cursor:pointer"> 
                                                <td width="7px"><img src="<%=strUrl %>imgBtnIzda.gif" width="7px"></td>
                                                <td align="center" width="20" background="<%=strUrl %>bckBoton.gif"><img src="<%=strUrl %>imgNO.gif" border="0"></td>
                                                <td width="15px" background="<%=strUrl %>bckBoton.gif" align=center class="txtBot"><a href="#" hidefocus>&nbsp;No</a></td>
                                                <td width="7px"><img src="<%=strUrl %>imgBtnDer.gif" width="7px"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
					</tr>
				</table>
			</p>
		</span>
	</div>
</div>    