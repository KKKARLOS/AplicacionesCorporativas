<%@ Control Language="c#" Inherits="GEMO.Capa_Presentacion.UserControls.Novedades" CodeFile="Novedades.ascx.cs" %>
<STYLE type=text/css>
.msn {
    FONT-WEIGHT: normal; FONT-SIZE: 11px; PADDING-TOP: 10px; FONT-FAMILY: Arial
}
A.msn:link {
    FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none
}
A.msn:visited {
    FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none
}
A.msn:active {
    FONT-WEIGHT: normal; COLOR: #003366; TEXT-DECORATION: none
}
</STYLE>
	
<SCRIPT type=text/JavaScript>
<!--
	var x = 1
	var y = 1

	function startClock(){
		x = x-y
		setTimeout("startClock()", 1000)
		if(x==0) {
			popupWinespopup_winLoad();
		}
	}
	
	var popupWinoldonloadHndlr=window.onload, popupWinpopupHgt, popupWinactualHgt, popupWintmrId=-1, popupWinresetTimer;
	var popupWintitHgt, popupWincntDelta, popupWintmrHide=-1, popupWinhideAfter=-1, popupWinhideAlpha, popupWinhasFilters=true;
	var popupWinnWin, popupWinshowBy=null, popupWindxTimer=-1, popupWinpopupBottom;

    var popupWinnText,popupWinnMsg,popupWinnTitle,popupWinbChangeTexts=false;

    window.onload=startClock;

	function popupWinespopup_ShowPopup(show)
	{
		if (popupWindxTimer!=-1) { el.filters.blendTrans.stop(); }
		if ((popupWintmrHide!=-1) && ((show!=null) && (show==popupWinshowBy)))
		{
		clearInterval(popupWintmrHide);
		popupWintmrHide=setInterval(popupWinespopup_tmrHideTimer,popupWinhideAfter);
		return;
		}

		if (popupWintmrId!=-1) return;

		popupWinshowBy=show;

		elCnt=document.getElementById('popupWin_content')
		elTit=document.getElementById('popupWin_header');
		el=document.getElementById('popupWinNov');
		el.style.left='';
		el.style.top='';
		el.style.filter='';

		if (popupWintmrHide!=-1) clearInterval(popupWintmrHide); popupWintmrHide=-1;

		document.getElementById('popupWin_header').style.display='none';
		document.getElementById('popupWin_content').style.display='none';

		if (navigator.userAgent.indexOf('Opera')!=-1)
			el.style.bottom=(document.body.scrollHeight*1-document.body.scrollTop*1	- document.body.offsetHeight*1+1*popupWinpopupBottom)+'px';

		if (popupWinbChangeTexts)
		{
			popupWinbChangeTexts=false;
			document.getElementById('popupWinaCnt').innerHTML=popupWinnMsg;
			document.getElementById('popupWintitleEl').innerHTML=popupWinnTitle;
		}


		popupWinactualHgt=0; el.style.height=popupWinactualHgt+'px';
		el.style.visibility='';
		if (!popupWinresetTimer) el.style.display='';
		popupWintmrId=setInterval(popupWinespopup_tmrTimer,(popupWinresetTimer?1000:20));
	}

	function popupWinespopup_winLoad()
	{
		if (popupWinoldonloadHndlr!=null) popupWinoldonloadHndlr();

		elCnt=document.getElementById('popupWin_content')
		elTit=document.getElementById('popupWin_header');
		el=document.getElementById('popupWinNov');
		popupWinpopupBottom=el.style.bottom.substr(0,el.style.bottom.length-2);

		popupWintitHgt=elTit.style.height.substr(0,elTit.style.height.length-2);
		popupWinpopupHgt=el.style.height;
		popupWinpopupHgt=popupWinpopupHgt.substr(0,popupWinpopupHgt.length-2); popupWinactualHgt=0;
		popupWincntDelta=popupWinpopupHgt-(elCnt.style.height.substr(0,elCnt.style.height.length-2));

		if (true)
		{
			popupWinresetTimer=true;
			popupWinespopup_ShowPopup(null);
		}

	}

	function popupWinespopup_tmrTimer()
	{
		el=document.getElementById('popupWinNov');

		if (popupWinresetTimer)
		{
			el.style.display='';
			clearInterval(popupWintmrId); popupWinresetTimer=false;
			popupWintmrId=setInterval(popupWinespopup_tmrTimer,20);
		}

		popupWinactualHgt+=5;

		if (popupWinactualHgt>=popupWinpopupHgt)
		{
			popupWinactualHgt=popupWinpopupHgt; clearInterval(popupWintmrId); popupWintmrId=-1;
			document.getElementById('popupWin_content').style.display='';
			if (popupWinhideAfter!=-1) popupWintmrHide=setInterval(popupWinespopup_tmrHideTimer,popupWinhideAfter);
		}

		if (popupWintitHgt<popupWinactualHgt-6)
		document.getElementById('popupWin_header').style.display='';

		if ((popupWinactualHgt-popupWincntDelta)>0)
		{
			elCnt=document.getElementById('popupWin_content')
			elCnt.style.display='';
			elCnt.style.height=(popupWinactualHgt-popupWincntDelta)+'px';
		}

		el.style.height=popupWinactualHgt+'px';
	}

	function popupWinespopup_tmrHideTimer()
	{
		clearInterval(popupWintmrHide); popupWintmrHide=-1;
		el=document.getElementById('popupWinNov');

		if (popupWinhasFilters)
		{
			backCnt=document.getElementById('popupWin_content').innerHTML;
			backTit=document.getElementById('popupWin_header').innerHTML;
			document.getElementById('popupWin_content').innerHTML='';
			document.getElementById('popupWin_header').innerHTML='';
			el.style.filter='blendTrans(duration=1)';
			el.filters.blendTrans.apply();
			el.style.visibility='hidden';
			el.filters.blendTrans.play();
			document.getElementById('popupWin_content').innerHTML=backCnt;
			document.getElementById('popupWin_header').innerHTML=backTit;
			popupWindxTimer=setInterval(popupWinespopup_dxTimer,1000);
		}

		else el.style.visibility='hidden';
	}

	function popupWinespopup_dxTimer()
	{
		clearInterval(popupWindxTimer); popupWindxTimer=-1;
	}

	function popupWinespopup_Close()
	{
		if (popupWintmrId==-1)
		{
			el=document.getElementById('popupWinNov');
			el.style.filter='';
			el.style.display='none';
			if (popupWintmrHide!=-1) clearInterval(popupWintmrHide); popupWintmrHide=-1;
		}
	}


	var popupWinmousemoveBack,popupWinmouseupBack;
	var popupWinofsX,popupWinofsY;

	function popupWinespopup_DragDrop(e)
	{
		popupWinmousemoveBack=document.body.onmousemove;
		popupWinmouseupBack=document.body.onmouseup;
		ox=(e.offsetX==null)?e.layerX:e.offsetX;
		oy=(e.offsetY==null)?e.layerY:e.offsetY;
		popupWinofsX=ox; popupWinofsY=oy;

		document.body.onmousemove=popupWinespopup_DragDropMove;
		document.body.onmouseup=popupWinespopup_DragDropStop;
		if (popupWintmrHide!=-1) clearInterval(popupWintmrHide);
	}

	function popupWinespopup_DragDropMove(e)
	{
		el=document.getElementById('popupWinNov');

		if (e==null&&event!=null)
		{
			el.style.left=(event.clientX*1+document.body.scrollLeft-popupWinofsX)+'px';
			el.style.top=(event.clientY*1+document.body.scrollTop-popupWinofsY)+'px';
			event.cancelBubble=true;
		}
		else
		{
			el.style.left=(e.pageX*1-popupWinofsX)+'px';
			el.style.top=(e.pageY*1-popupWinofsY)+'px';
			e.cancelBubble=true;
		}

		if ((event.button&1)==0) popupWinespopup_DragDropStop();
	}



	function popupWinespopup_DragDropStop()
	{
		document.body.onmousemove=popupWinmousemoveBack;
		document.body.onmouseup=popupWinmouseupBack;
	}

//-->
</script>

<DIV onmousedown="return popupWinespopup_DragDrop(event);" 
	id=popupWinNov 
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
			BOTTOM: 0px; 
			RIGHT: 0px; 
			WIDTH: 281px; 
			HEIGHT: 126px">

 	<DIV id=popupWin_header 
		style="DISPLAY: none; 
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
		
		<SPAN id=popupWintitleEl>
			<SPAN style="COLOR: #1f336b; FONT-FAMILY: Arial, Helvetica, sans-serif">&nbsp;Novedades</SPAN>
		</SPAN>

		<SPAN onmousedown=event.cancelBubble=true; 
			onmouseover="style.color='#455690';" 
			style="RIGHT: 2px; 
			FONT: bold 12px arial,sans-serif; 
			CURSOR: pointer; 
			COLOR: #728eb8; 
			POSITION: absolute; 
			TOP: 1px" 
			onclick=popupWinespopup_Close() 
			onmouseout="style.color='#728EB8';">
			
			<IMG height=13 alt="Cerrar" title="Cerrar" src="<%=strUrl%>imgCerrarBanner.gif" width=13 border=0> 

		</SPAN>
	</DIV>

	<DIV onmousedown=event.cancelBubble=true; 
		id=popupWin_content 
		style="BORDER-RIGHT: #b9c9ef 1px solid; 
		PADDING-RIGHT: 2px; 
		BORDER-TOP: #728eb8 1px solid; 
		DISPLAY: none; 
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
		HEIGHT: 102px; 
		TEXT-ALIGN: left">
		
		<SPAN style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif">
			<P style="FONT-SIZE: 11px; PADDING-TOP: 0px"><br />
				<table Border=0 CellPadding=1 CellSpacing=0 Width=100%>
					<TR><TD Width=4></TD>
						<TD><a href="<%=Session["strServer"].ToString() %>Capa_Presentacion/Ayuda/Novedades/Default.aspx"><img Border=0 SRC="<%=strUrl%>imgInfo.gif" width=48 height=48></a></TD>
						<TD Width=4></TD>
						<TD VAlign="Middle" class=msn>
						Se han producido novedades en la aplicación GEMO.<br /><br />Para verlas, pulse <A href="<%=Session["strServer"].ToString() %>Capa_Presentacion/Ayuda/Novedades/Default.aspx" style="color:Red">aquí</A>.
						</TD>
					</TR>
				</TABLE>
			</P>
		</SPAN>
	</DIV>

</DIV>