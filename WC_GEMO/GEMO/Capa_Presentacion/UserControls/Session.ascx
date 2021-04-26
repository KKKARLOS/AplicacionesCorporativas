<%@ Control Language="c#" Inherits="GEMO.Capa_Presentacion.UserControls.Session" CodeFile="Session.ascx.cs" %>
<STYLE type=text/css>
.msn {FONT-WEIGHT: normal; FONT-SIZE: 11px; PADDING-TOP: 10px; FONT-FAMILY: Arial}
A.msn:link {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:visited {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
A.msn:active {FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none}
</STYLE>
	
<SCRIPT type=text/JavaScript>
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

<DIV id=popupWin_Session 
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
			HEIGHT: 180px">

 	<DIV id=popupWin_header_Session 
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
		
		<SPAN id=popupWintitleEl_Session>
			<SPAN style="COLOR: #1f336b; FONT-FAMILY: Arial, Helvetica, sans-serif">&nbsp;Caducidad de sesión</SPAN>
		</SPAN>

		<SPAN onmouseover="style.color='#455690';" 
			style="RIGHT: 2px; 
			FONT: bold 12px arial,sans-serif; 
			CURSOR: pointer; 
			COLOR: #728eb8; 
			POSITION: absolute; 
			TOP: 1px" 
			onclick=CerrarMensajeSession() 
			onmouseout="style.color='#728EB8';">
			
			<IMG height=13 alt="Cerrar" title="Cerrar" src="<%=strUrl %>imgCerrarBanner.gif" width=13 border=0> 

		</SPAN>
	</DIV>

	<DIV id=popupWin_content_Session 
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
		HEIGHT: 154px; 
		TEXT-ALIGN: left">
		
		<SPAN style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif">
			<P style="FONT-SIZE: 11px; PADDING-TOP: 0px"><br />
				<table Border=0 CellPadding=1 CellSpacing=0 Width=100%>
					<TR><TD Width='4px'></TD>
						<TD Width='48px'><img Border=0 SRC="<%=strUrl %>imgInfo.gif" width=48 height=48></TD>
						<TD Width='8px'></TD>
						<TD VAlign="Middle" class=msn>
						La sesión de GEMO va a caducar en breve.<br /><br />
                        ¿Desea reiniciar el tiempo de la sesión?<br />
                        Si pulsa "No" podría perder los datos pendientes de grabar.<br /><br />
                            <table id="tblSesion" align="center" width="100%">
                                <tr>
                                    <td>
                                        <table id="tblSI" border="0" cellspacing="0" cellpadding="0" align="center">
                                            <tr onclick="ReiniciarSession();CerrarMensajeSession();" style="cursor:pointer"> 
                                                <td width="7px"><img src="<%=strUrl %>imgBtnIzda.gif" width="7px"></td>
								                <td align="center" width="20" background="<%=strUrl %>bckBoton.gif"><IMG src="<%=strUrl %>imgSI.gif" border="0"></td>
                                                <td width="15px" background="<%=strUrl %>bckBoton.gif" align="center" class="txtBot"><a href="#" hidefocus>&nbsp;Sí</a></td>
                                                <td width="7px"><img src="<%=strUrl %>imgBtnDer.gif" width="7px"></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table id="tblNO" border="0" cellspacing="0" cellpadding="0" align="center">
                                            <tr onclick="CerrarMensajeSession();" style="cursor:pointer"> 
                                                <td width="7px"><img src="<%=strUrl %>imgBtnIzda.gif" width="7px"></td>
                                                <td align="center" width="20" background="<%=strUrl %>bckBoton.gif"><IMG src="<%=strUrl %>imgNO.gif" border="0"></td>
                                                <td width="15px" background="<%=strUrl %>bckBoton.gif" align=center class="txtBot"><a href="#" hidefocus>&nbsp;No</a></td>
                                                <td width="7px"><img src="<%=strUrl %>imgBtnDer.gif" width="7px"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </TD>
					</TR>
				</TABLE>
			</P>
		</SPAN>
	</DIV>
</DIV>    