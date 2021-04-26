<%@ Control Language="c#" Inherits="GASVI.Capa_Presentacion.UserControls.MensajeOff" CodeFile="mensajeOff.ascx.cs" %>
	<DIV id="popupWin" style="BORDER-RIGHT: #455690 1px solid; BORDER-TOP: #b9c9ef 1px solid; DISPLAY: none; Z-INDEX: 9999; BACKGROUND: #e0e9f8; ; LEFT: 400px; BORDER-LEFT: #b9c9ef 1px solid; WIDTH: 230px; BORDER-BOTTOM: #455690 1px solid; POSITION: absolute; TOP: 300px; HEIGHT: 36px">
		<DIV id="popupWin_content" style="BORDER-RIGHT: #b9c9ef 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #728eb8 1px solid; DISPLAY: none; PADDING-LEFT: 5px; filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr= '#E1EAF8' , EndColorStr= '#FFFFFF' ); LEFT: 2px; PADDING-BOTTOM: 5px; FONT: 12px arial,sans-serif; OVERFLOW: hidden; BORDER-LEFT: #728eb8 1px solid; WIDTH: 224px; COLOR: #1f336b; PADDING-TOP: 7px; BORDER-BOTTOM: #b9c9ef 1px solid; POSITION: absolute; TOP: 2px; HEIGHT: 30px; TEXT-ALIGN: center; TEXT-DECORATION: none">Grabación 
			correcta
		</DIV>
	</DIV>
	<script language="javascript">
	<!--
	    /*********************************************
        Funciones para mostrar la capa de Grabación correcta
        **********************************************/
	    try{
	        if (window.dialogWidth == "undefined"){
	            $I("popupWin").style.top = (document.body.clientHeight/2) -20;
	            $I("popupWin").style.left = (document.body.clientWidth/2) -115; 
	        }else{//modales
	            $I("popupWin").style.top = (window.dialogHeight.substring(0,window.dialogHeight.length-2)/2) -20;
	            $I("popupWin").style.left = (window.dialogWidth.substring(0,window.dialogWidth.length-2)/2) -115; 
	        }
	    }catch(e){}

        var popupWinpopupHgt, popupWinactualHgt, popupWintmrId=-1, popupWinresetTimer;
        var popupWintitHgt, popupWincntDelta, popupWintmrHide=-1, popupWinhideAfter=1500, popupWinhasFilters=true;
        var popupWinshowBy=null, popupWindxTimer=-1, popupWinpopupBottom, popupWinoldLeft;

        //mostrar mensaje off
        function mmoff(sTexto, nWidth, nTiempo, nHeight, nLeft, nTop){ 
            try{
                popupWinhideAfter = (nTiempo) ? nTiempo : 1500;
                $I("popupWin_content").parentNode.style.left =  (nLeft) ? nLeft : (document.body.clientWidth/2) - nWidth/2;
                //$I("popupWin_content").parentNode.style.top = (nTop) ? nTop : (document.body.clientHeight/2) -80;
                $I("popupWin_content").parentNode.style.top = (nTop) ? nTop : (document.body.clientHeight/2) - ((nHeight) ? nHeight+6 : 36);
                $I("popupWin_content").parentNode.style.width = nWidth+6;
                $I("popupWin_content").parentNode.style.height = (nHeight) ? nHeight+6 : 36;
                $I("popupWin_content").style.width = nWidth;
                $I("popupWin_content").innerText = sTexto;
                $I("popupWin_content").style.height = (nHeight) ? nHeight : 30;
                popupWinespopup_winLoad();
            }
            catch(e){
                //mostrarErrorAplicacion("Error al mostrar el mensaje informativo", e.message);
            }
        }


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

            if (popupWintmrHide!=-1){
                clearInterval(popupWintmrHide);
                popupWintmrHide=-1;
            }
            document.getElementById('popupWin_content').style.display='none';

            popupWinactualHgt=0; el.style.height=popupWinactualHgt+'px';
            el.style.visibility='';
            if (!popupWinresetTimer) el.style.display='';
            popupWintmrId=setInterval(popupWinespopup_tmrTimer,(popupWinresetTimer?200:20));
        }

        function popupWinespopup_winLoad()
        {
            if (popupWintmrHide!=-1){
                clearInterval(popupWintmrHide);
                popupWintmrHide=-1;
            }
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
            clearInterval(popupWintmrId);
            popupWinresetTimer=false;
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
            clearInterval(popupWintmrHide);
            popupWintmrHide=-1;
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
            clearInterval(popupWindxTimer);
            popupWindxTimer=-1;
        }

        function popupWinespopup_Close()
        {
            if (popupWintmrId==-1)
            {
                el=document.getElementById('popupWin');
                el.style.filter='';
                el.style.display='none';
                if (popupWintmrHide!=-1){
                    clearInterval(popupWintmrHide);
                    popupWintmrHide=-1;
                }
            }
        }
	-->
    </script>
</DIV>
