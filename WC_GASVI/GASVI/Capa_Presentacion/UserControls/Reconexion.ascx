<%@ Control Language="c#" Inherits="GASVI.Capa_Presentacion.UserControls.Reconexion" CodeFile="Reconexion.ascx.cs" %>
<STYLE type=text/css>
.msn {FONT-WEIGHT: normal; FONT-SIZE: 11px; PADDING-TOP: 10px; FONT-FAMILY: Arial}
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

<DIV id=popupWin_Reconexion 
	style="BORDER-RIGHT: #455690 1px solid; 
			PADDING-RIGHT: 2px; 
			BORDER-TOP: #b9c9ef 1px solid; 
			DISPLAY: none; 
			PADDING-LEFT: 2px; 
			Z-INDEX: 9999; 
			BACKGROUND: #e0e9f8; 
			PADDING-BOTTOM: 2px; 
			BORDER-LEFT: #b9c9ef 1px solid; 
			PADDING-TOP: 2px; 
			BORDER-BOTTOM: #455690 1px solid; 
			POSITION: absolute; 
			WIDTH: 281px; 
			HEIGHT: 130px">

 	<DIV id=popupWin_header_Reconexion 
		style="DISPLAY: block; 
		FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF'); 
		LEFT: 2px; 
		FONT: 11px arial,sans-serif; 
		WIDTH: 276px; 
		CURSOR: default; 
		COLOR: #1f336b; 
		POSITION: absolute; 
		TOP: 2px; 
		HEIGHT: 16px; 
		TEXT-ALIGN: left; 
		TEXT-DECORATION: none">
		
		<SPAN id=popupWintitleEl_Reconexion>
			<SPAN style="COLOR: #1f336b; FONT-FAMILY: Arial, Helvetica, sans-serif">&nbsp;Usuarios activos</SPAN>
		</SPAN>

		<SPAN onmouseover="style.color='#455690';" 
			style="RIGHT: 2px; 
			FONT: bold 12px arial,sans-serif; 
			CURSOR: pointer; 
			COLOR: #728eb8; 
			POSITION: absolute; 
			TOP: 1px" 
			onclick="CerrarMensajeReconexion()"
			onmouseout="style.color='#728EB8';">
			
			<IMG height=13 alt="Cerrar" title="Cerrar" src="<%=strUrl %>imgCerrarBanner.gif" width=13 border=0 onclick="CerrarMensajeReconexion()"> 

		</SPAN>
	</DIV>

	<DIV id=popupWin_content_Reconexion 
		style="BORDER-RIGHT: #b9c9ef 1px solid; 
		PADDING-RIGHT: 2px; 
		BORDER-TOP: #728eb8 1px solid; 
		DISPLAY: block; 
		PADDING-LEFT: 2px; 
		BACKGROUND: #e0e9f8; 
		FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#FFE0E9F8', EndColorStr='#FFFFFFFF'); 
		LEFT: 2px; 
		PADDING-BOTTOM: 2px; 
		OVERFLOW: hidden; 
		BORDER-LEFT: #728eb8 1px solid; 
		PADDING-TOP: 2px; 
		BORDER-BOTTOM: #b9c9ef 1px solid; 
		POSITION: absolute; 
		TOP: 20px; 
		WIDTH: 275px; 
		HEIGHT: 104px; 
		TEXT-ALIGN: left">
		
		<SPAN style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif">
			<P style="FONT-SIZE: 11px; PADDING-TOP: 0px">
				<TABLE Border=0 CellPadding=1 CellSpacing=0 Width=100%>
					<TR><TD Width=4></TD>
						<TD><img Border=0 SRC="<%=strUrl %>imgInfo.gif" width=48 height=48></TD>
						<TD Width=4></TD>
						<TD style="vertical-align: middle" class=msn>
						IAP ha detectado que tiene más de un usuario activo.<br /><br />Por favor, reporte las horas con el usuario adecuado pincpointero sobre el enlace "Profesional".
						</TD>
					</TR>
				</TABLE>
			</P>
		</SPAN>
	</DIV>
</DIV>    