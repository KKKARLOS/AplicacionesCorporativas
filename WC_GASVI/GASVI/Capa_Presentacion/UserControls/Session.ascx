<%@ Control Language="c#" Inherits="GASVI.Capa_Presentacion.UserControls.Session" CodeFile="Session.ascx.cs" %>
<style type="text/css">
.msn {FONT-WEIGHT: normal; FONT-SIZE: 11px; PADDING-TOP: 10px; FONT-FAMILY: Arial}
A.msn:link {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:visited {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:active {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
</style>
	
<script type="text/JavaScript" language="javascript">
<!--
	function MostrarMensajeSession()
	{
	    var a = ocultarCombos();
		el=document.getElementById('popupWin_Session');
		el.style.display='block';
	}
	function CerrarMensajeSession()
	{
	    var a = mostrarCombos();
		el=document.getElementById('popupWin_Session');
		el.style.display='none';
	}
    function mostrarCombos(){
        var aCombos = document.getElementsByTagName("SELECT");
        for (var i=0;i<aCombos.length;i++){
            aCombos[i].style.visibility = "visible";
        }
        return true;
    }
    function ocultarCombos(){
        var aCombos = document.getElementsByTagName("SELECT");
        for (var i=0;i<aCombos.length;i++){
            aCombos[i].style.visibility = "hidden";
        }
        return true;
    }
-->
</script>
<center>
<div id="popupWin_Session" 
	style="border-right: #455690 1px solid; 
			padding-right: 2px; 
			border-top: #b9c9ef 1px solid; 
			display: none; 
			padding-left: 2px; 
			z-index: 9999; 
			background: #e0e9f8; 
			padding-bottom: 2px; 
			border-left: #b9c9ef 1px solid; 
			padding-top: 2px; 
			border-bottom: #455690 1px solid; 
			position: absolute; 
			width: 281px; 
			height: 180px">

 	<div id="popupWin_header_Session"
		style="display: block; 
		filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF'); 
		left: 2px; 
		font: 11px arial,sans-serif; 
		width: 276px; 
		cursor: default; 
		color: #1f336b; 
		position: absolute; 
		top: 2px; 
		height: 16px; 
		text-align: left; 
		text-decoration: none">
		
		<span id="popupWintitleEl_Session">
			<span style="color: #1f336b; font-family: Arial, Helvetica, sans-serif">&nbsp;Caducidad de sesión</span>
		</span>

		<span onmouseover="style.color='#455690';" 
			style="right: 2px; 
			font: bold 12px arial,sans-serif; 
			cursor: pointer; 
			color: #728eb8; 
			position: absolute; 
			top: 1px" 
			onclick="CerrarMensajeSession()"
			onmouseout="style.color='#728EB8';">
			
			<img height="13px" alt="Cerrar" title="Cerrar" src="<%=strUrl %>imgCerrarBanner.gif" width="13px" border="0" /> 
		</span>
	</div>

	<div id="popupWin_content_Session"
		style="border-right: #b9c9ef 1px solid; 
		padding-right: 2px; 
		border-top: #728eb8 1px solid; 
		display: block; 
		padding-left: 2px; 
		background: #e0e9f8; 
		filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF'); 
		left: 2px; 
		padding-bottom: 2px; 
		overflow: hidden; 
		border-left: #728eb8 1px solid; 
		padding-top: 2px; 
		border-bottom: #b9c9ef 1px solid; 
		position: absolute; 
		top: 20px; 
		width: 275px; 
		height: 154px; 
		text-align: left">

		<span style="font-family: Verdana, Arial, Helvetica, sans-serif">
			<p style="font-size: 11px; padding-top: 0px"><br />
				<table cellpadding="1" style="width:100%;">
				    <colgroup>
				        <col style="width:55px; height:48px;" />
				        <col />
				    </colgroup>
					<tr>
						<td>
						    <img src="<%=strUrl %>imgInfo.gif" />
						</td>
						<td style="vertical-align: middle" class="msn">
						    La sesión de GASVI va a caducar en breve.<br /><br />
                            ¿Desea reiniciar el tiempo de la sesión?<br />
                            Si pulsa "No" podría perder los datos pendientes de grabar.<br /><br />
                            <table id="tblSesion" width="100%">
                                <tr>
                                    <td style="text-align:center">
                                        <button id="btnSI" type="button" onclick="ReiniciarSession();CerrarMensajeSession();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="<%=strUrl %>imgSI.gif" /><span title="Sí">Sí</span></button>
                                    </td>
                                    <td style="text-align:center">
                                        <button id="btnNO" type="button" onclick="CerrarMensajeSession();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="<%=strUrl %>imgNO.gif" /><span title="No">No</span></button>
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
</center>