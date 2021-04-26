<%@ Control Language="c#" Inherits="GESTAR.Capa_Presentacion.UserControls.Reconexion" CodeFile="Reconexion.ascx.cs" %>
<STYLE type=text/css>
.msn {FONT-WEIGHT: normal; FONT-SIZE: 11px; PADDING-top: 10px; FONT-FAMILY: Arial}
A.msn:link {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:visited {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:active {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
</STYLE>
	
<script type=text/JavaScript>
<!--
	function MostrarMensajeReconexion()
	{
	    var a = ocultarCombos();
		el=document.getElementById('popupWin_Reconexion');
		el.style.top = (document.body.clientHeight/2) -65;
	    el.style.left = (document.body.clientWidth/2) -140; 
		el.style.display='block';
	}
	function CerrarMensajeReconexion()
	{
	    var a = mostrarCombos();
		el=document.getElementById('popupWin_Reconexion');
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

<div id=popupWin_Reconexion 
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
			height: 130px">

 	<div id=popupWin_header_Reconexion 
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
		
		<SPAN id=popupWintitleEl_Reconexion>
			<SPAN style="COLOR: #1f336b; FONT-FAMILY: Arial, Helvetica, sans-serif">&nbsp;Usuarios activos</SPAN>
		</SPAN>

		<SPAN onmouseover="style.color='#455690';" 
			style="RIGHT: 2px; 
			FONT: bold 12px arial,sans-serif; 
			cursor: pointer; 
			COLOR: #728eb8; 
			position: absolute; 
			top: 1px" 
			onclick="CerrarMensajeReconexion()"
			onmouseout="style.color='#728EB8';">
			
			<img height=13 title="Cerrar" src="<%=strUrl %>imgCerrarBanner.gif" width=13 border=0 onclick="CerrarMensajeReconexion()"> 

		</SPAN>
	</div>

	<div id=popupWin_content_Reconexion 
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
		height: 104px; 
		TEXT-ALIGN: left">
		
		<SPAN style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif">
			<P style="FONT-SIZE: 11px; PADDING-top: 0px">
				<table Border=0 CellPadding=1 CellSpacing=0 width=100%>
					<tr><td width=4></td>
						<td><img Border=0 src="<%=strUrl %>imgInfo.gif" width="48px" height="48px"></td>
						<td width=4></td>
						<td VAlign="Middle" class=msn>
						IAP ha detectado que tiene más de un usuario activo.<br /><br />Por favor, reporte las horas con el usuario adecuado pinchando sobre el enlace "Profesional".
						</td>
					</tr>
				</table>
			</P>
		</SPAN>
	</div>
</div>    