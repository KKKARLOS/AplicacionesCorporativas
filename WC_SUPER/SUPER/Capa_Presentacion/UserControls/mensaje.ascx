<%@ Control Language="c#" Inherits="SUPER.Capa_Presentacion.UserControls.Mensaje" CodeFile="Mensaje.ascx.cs" %>
	<DIV id="popupWin" 
		style= "BORDER-RIGHT: solid 1px #455690; 
				BORDER-TOP: solid 1px #b9c9ef; 
				BORDER-LEFT: solid 1px #b9c9ef; 
				BORDER-BOTTOM: solid 1px #455690;
				BACKGROUND: #e0e9f8;
				DISPLAY: none; 
				Z-INDEX: 9999; 
				POSITION: absolute; 
				TOP: 300px; 
				LEFT: 400px; 
				WIDTH: 228px; 
				HEIGHT: 34px">
		<DIV id="popupWin_content" 
			style=	"BORDER-RIGHT: solid 1px #b9c9ef; 
				BORDER-LEFT: solid 1px #728eb8; 
				BORDER-TOP:  solid 1px #728eb8;
				BORDER-BOTTOM: solid 1px #b9c9ef; 				
				DISPLAY: none; 
				filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr= '#E1EAF8' , EndColorStr= '#FFFFFF' ); 
				PADDING-TOP: 7px; 
				PADDING-BOTTOM: 5px; 
				PADDING-LEFT: 5px; 
				PADDING-RIGHT: 5px; 
				FONT: 12px arial,sans-serif; 
				OVERFLOW: hidden; 
				COLOR: #1f336b; 
				POSITION: relative; 
				TOP: 2px; 
				LEFT: 2px; 
				WIDTH: 212px; 
				HEIGHT: 18px; 
				TEXT-ALIGN: center; 
				TEXT-DECORATION: none">Grabación 
				correcta
		</DIV>
	</DIV>
	<script type="text/javascript">
	<!--
	    /*********************************************
        Funciones para mostrar la capa de Grabación correcta
        **********************************************/
        var popupWinpopupHgt, popupWinactualHgt, popupWintmrId=-1, popupWinresetTimer;
        var popupWintitHgt, popupWincntDelta, popupWintmrHide=-1, popupWinhideAfter=1500, popupWinhasFilters=true;
        var popupWinshowBy=null, popupWindxTimer=-1, popupWinpopupBottom, popupWinoldLeft;

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
            el=document.getElementById('popupWin');
            el.style.left=popupWinoldLeft;
            el.style.filter='';

            if (popupWintmrHide!=-1) clearInterval(popupWintmrHide); popupWintmrHide=-1;

            document.getElementById('popupWin_content').style.display='none';

            popupWinactualHgt=0; el.style.height=popupWinactualHgt+'px';
            el.style.visibility='';
            if (!popupWinresetTimer) el.style.display='';
            popupWintmrId=setInterval(popupWinespopup_tmrTimer,(popupWinresetTimer?200:20));
        }

        function popupWinespopup_winLoad()
        {
            elCnt=document.getElementById('popupWin_content')
            el=document.getElementById('popupWin');
			        popupWinoldLeft=el.style.left;
            popupWinpopupBottom=el.style.bottom.substr(0,el.style.bottom.length-2);

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
            el=document.getElementById('popupWin');
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
            el=document.getElementById('popupWin');
            if (popupWinhasFilters)
            {
            backCnt=document.getElementById('popupWin_content').innerHTML;
            document.getElementById('popupWin_content').innerHTML='';
            el.style.filter='blendTrans(duration=1)';
            el.filters.blendTrans.apply();
            el.style.visibility='hidden';
            el.filters.blendTrans.play();
            document.getElementById('popupWin_content').innerHTML=backCnt;
            
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
            el=document.getElementById('popupWin');
            el.style.filter='';
            el.style.display='none';
            if (popupWintmrHide!=-1) clearInterval(popupWintmrHide); popupWintmrHide=-1;
            
            }
        }
	
		popupWinespopup_winLoad();
	-->
	</script>

