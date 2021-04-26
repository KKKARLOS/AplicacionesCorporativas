<%@ Control Language="c#" Inherits="CR2I.Capa_Presentacion.UserControls.MensajeOff" CodeFile="mensajeOff.ascx.cs" %>
<style type="text/css">
.css-grd {
    /* default background colour, for all layout engines that don't implement gradients */
    background: #FFFFFF;
    /* gecko based browsers */
    background: -moz-linear-gradient(top, #E1EAF8, #FFFFFF);
    /* webkit based browsers */
    background: -webkit-gradient(linear, left top, left bottom, from(#E1EAF8), to(#FFFFFF));
    }
</style>
	<div id="popupWin" style="BORDER-RIGHT: #455690 1px solid; BORDER-TOP: #b9c9ef 1px solid; DISPLAY: none; Z-INDEX: 9999; BACKGROUND: #e0e9f8; LEFT: 400px; BORDER-LEFT: #b9c9ef 1px solid; WIDTH: 230px; BORDER-BOTTOM: #455690 1px solid; POSITION: absolute; TOP: 300px; HEIGHT: 36px">
		<div id="popupWin_content" class="css-grd" style="BORDER-RIGHT: #b9c9ef 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #728eb8 1px solid; DISPLAY: none; PADDING-LEFT: 5px; LEFT: 2px; PADDING-BOTTOM: 5px; FONT: 12px arial,sans-serif; OVERFLOW: hidden; BORDER-LEFT: #728eb8 1px solid; WIDTH: 224px; COLOR: #1f336b; PADDING-TOP: 7px; BORDER-BOTTOM: #b9c9ef 1px solid; POSITION: absolute; TOP: 2px; HEIGHT: 30px; TEXT-ALIGN: center; TEXT-DECORATION: none; filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr= '#E1EAF8' , EndColorStr= '#FFFFFF' );">Grabación 
			correcta
		</div>
	</div>
	<div id="fade" class="texto" style="visibility:hidden; TEXT-ALIGN: center; TEXT-DECORATION: none; Z-INDEX: 9999; LEFT:400px;WIDTH: 230px; POSITION: absolute; TOP: 300px; HEIGHT: 25px; opacity:0; moz-opacity:0.0; filter:alpha(opacity='0') border-radius:3px; -moz-border-radius:3px; margin-bottom:10px; padding:9px 10px 0; border:1px solid #548954; background:#e0e9f8; color:#1f336b;">Grabación correctAA</div> 
	<script type="text/javascript">
	
	<!--
	    /*********************************************
	    Funciones para mostrar la capa de Grabación correcta
	    **********************************************/
	    try {
	        if (window.dialogWidth == "undefined") {
	            if (document.body.clientWidth != null) {
	                $("popupWin").style.top = (document.body.clientHeight / 2) - 20;
	                $("popupWin").style.left = (document.body.clientWidth / 2) - 115;
	                $("fade").style.top = (document.body.clientHeight / 2) - 20;
	                $("fade").style.left = (document.body.clientWidth / 2) - 115;
	            }
	            else {
	                $("popupWin").style.top = (window.innerHeight / 2) - 20;
	                $("popupWin").style.left = (window.innerWidth / 2) - 115;
	                $("fade").style.top = (window.innerHeight / 2) - 20;
	                $("fade").style.left = (window.innerWidth / 2) - 115;
	            }
	        } 
	        else {//modales
	            if (document.body.clientWidth != null) {
	                $("popupWin").style.top = (window.dialogHeight.substring(0, window.dialogHeight.length - 2) / 2) - 20;
	                $("popupWin").style.left = (window.dialogWidth.substring(0, window.dialogWidth.length - 2) / 2) - 115;
	                $("fade").style.top = (window.dialogHeight.substring(0, window.dialogHeight.length - 2) / 2) - 20;
	                $("fade").style.left = (window.dialogWidth.substring(0, window.dialogWidth.length - 2) / 2) - 115;
	            }
	            else {
	                $("popupWin").style.top = (window.dialogHeight.substring(0, window.innerHeight - 2) / 2) - 20;
	                $("popupWin").style.left = (window.dialogWidth.substring(0, window.innerWidth - 2) / 2) - 115;
	                $("fade").style.top = (window.dialogHeight.substring(0, window.innerHeight - 2) / 2) - 20;
	                $("fade").style.left = (window.dialogWidth.substring(0, window.innerWidth - 2) / 2) - 115;
	            }
	        }
	    } 
	    catch (e) { }

	    var popupWinpopupHgt, popupWinactualHgt, popupWintmrId = -1, popupWinresetTimer;
	    var popupWintitHgt, popupWincntDelta, popupWintmrHide = -1, popupWinhideAfter = 1500, popupWinhasFilters = true;
	    var popupWinshowBy = null, popupWindxTimer = -1, popupWinpopupBottom, popupWinoldLeft;

	    //mostrar mensaje off
	    function mmoff(sTexto, nWidth, nTiempo, nHeight, nLeft, nTop) {
	        try {
	            popupWinhideAfter = (nTiempo) ? nTiempo : 1500;

	            if (document.body.clientWidth != null) {
	                $("popupWin_content").parentNode.style.left = (nLeft) ? nLeft : (document.body.clientWidth / 2) - nWidth / 2;
	                $("popupWin_content").parentNode.style.top = (nTop) ? nTop : (document.body.clientHeight / 2) - ((nHeight) ? nHeight + 6 : 36);
	                $("fade").style.left = $("popupWin_content").parentNode.style.left;
	                $("fade").style.top = $("popupWin_content").parentNode.style.top;
	            }
	            else {
	                $("popupWin_content").parentNode.style.left = (nLeft) ? nLeft : (window.innerWidth / 2) - nWidth / 2;
	                $("popupWin_content").parentNode.style.top = (nTop) ? nTop : (window.innerHeight / 2) - ((nHeight) ? nHeight + 6 : 36);
	                $("fade").style.left = $("popupWin_content").parentNode.style.left;
	                $("fade").style.top = $("popupWin_content").parentNode.style.top;
	            }

	            $("popupWin_content").parentNode.style.width = nWidth + 6;
	            $("popupWin_content").parentNode.style.height = (nHeight) ? nHeight + 6 : 36;
	            $("popupWin_content").style.width = nWidth;

	            $("fade").style.width = nWidth;

	            if (document.all) {
	                $("popupWin_content").innerText = sTexto;
	                $("fade").innerText = sTexto;
	            } else {
	                $("popupWin_content").textContent = sTexto;
	                $("fade").textContent = sTexto;
	            }

	            $("popupWin_content").style.height = (nHeight) ? nHeight : 30;
	            $("fade").style.height = (nHeight) ? nHeight : 25;

	            if (document.all) popupWinespopup_winLoad();
	            else {
	                document.getElementById('fade').style.visibility = 'visible';
	                fade("fade", 2); // 1 fade-in  2 fade-out
	            }
	        }
	        catch (e) {
	            //mostrarErrorAplicacion("Error al mostrar el mensaje informativo", e.message);
	        }
	    }

	    function popupWinespopup_ShowPopup(show) {
	        if (popupWindxTimer != -1) { el.filters.blendTrans.stop(); }

	        if ((popupWintmrHide != -1) && ((show != null) && (show == popupWinshowBy))) {
	            clearInterval(popupWintmrHide);
	            popupWintmrHide = setInterval(popupWinespopup_tmrHideTimer, popupWinhideAfter);
	            return;
	        }
	        if (popupWintmrId != -1) return;
	        popupWinshowBy = show;

	        elCnt = document.getElementById('popupWin_content')
	        el = document.getElementById('popupWin');
	        el.style.left = popupWinoldLeft;
	        el.style.filter = '';
	        //            el.style.filter = "alpha(opacity=0)";
	        //            el.style.opacity = '0';
	        //            
	        if (popupWintmrHide != -1) {
	            clearInterval(popupWintmrHide);
	            popupWintmrHide = -1;
	        }

	        document.getElementById('popupWin_content').style.display = 'none';

	        popupWinactualHgt = 0;
	        el.style.height = popupWinactualHgt + 'px';
	        el.style.visibility = '';
	        if (!popupWinresetTimer) el.style.display = '';
	        popupWintmrId = setInterval(popupWinespopup_tmrTimer, (popupWinresetTimer ? 200 : 20));
	    }

	    function popupWinespopup_winLoad() {
	        if (popupWintmrHide != -1) {
	            clearInterval(popupWintmrHide);
	            popupWintmrHide = -1;
	        }
	        elCnt = document.getElementById('popupWin_content')
	        el = document.getElementById('popupWin');
	        popupWinoldLeft = el.style.left;
	        popupWinpopupBottom = el.style.bottom.substr(0, el.style.bottom.length - 2);

	        popupWinpopupHgt = el.style.height;
	        popupWinpopupHgt = popupWinpopupHgt.substr(0, popupWinpopupHgt.length - 2); popupWinactualHgt = 0;
	        popupWincntDelta = popupWinpopupHgt - (elCnt.style.height.substr(0, elCnt.style.height.length - 2));

	        if (true) {
	            popupWinresetTimer = true;
	            popupWinespopup_ShowPopup(null);
	        }
	    }

	    function popupWinespopup_tmrTimer() {
	        el = document.getElementById('popupWin');
	        if (popupWinresetTimer) {
	            el.style.display = '';
	            clearInterval(popupWintmrId);
	            popupWinresetTimer = false;
	            popupWintmrId = setInterval(popupWinespopup_tmrTimer, 20);
	        }
	        popupWinactualHgt += 5;
	        if (popupWinactualHgt >= popupWinpopupHgt) {
	            popupWinactualHgt = popupWinpopupHgt; clearInterval(popupWintmrId); popupWintmrId = -1;
	            document.getElementById('popupWin_content').style.display = '';
	            if (popupWinhideAfter != -1) popupWintmrHide = setInterval(popupWinespopup_tmrHideTimer, popupWinhideAfter);
	        }
	        if ((popupWinactualHgt - popupWincntDelta) > 0) {
	            elCnt = document.getElementById('popupWin_content')
	            elCnt.style.display = '';
	            elCnt.style.height = (popupWinactualHgt - popupWincntDelta) + 'px';
	        }
	        el.style.height = popupWinactualHgt + 'px';
	    }

	    function popupWinespopup_tmrHideTimer() {
	        clearInterval(popupWintmrHide);
	        popupWintmrHide = -1;
	        el = document.getElementById('popupWin');
	        if (popupWinhasFilters) {
	            backCnt = document.getElementById('popupWin_content').innerHTML;
	            document.getElementById('popupWin_content').innerHTML = '';

	            if (document.all) {
	                el.style.filter = 'blendTrans(duration=1)';
	                el.filters.blendTrans.apply();
	                el.style.visibility = 'hidden';
	                el.filters.blendTrans.play();
	            }

	            document.getElementById('popupWin_content').innerHTML = backCnt;

	            popupWindxTimer = setInterval(popupWinespopup_dxTimer, 1000);
	        }
	        else el.style.visibility = 'hidden';
	    }

	    function popupWinespopup_dxTimer() {
	        clearInterval(popupWindxTimer);
	        popupWindxTimer = -1;
	    }

	    function popupWinespopup_Close() {
	        if (popupWintmrId == -1) {
	            el = document.getElementById('popupWin');
	            el.style.filter = '';
	            el.style.opacity = '0';
	            el.style.display = 'none';
	            if (popupWintmrHide != -1) {
	                clearInterval(popupWintmrHide);
	                popupWintmrHide = -1;
	            }
	        }
	    }

	    var TimeToFade = 2500.0;
	    var element;
	    var curTick;
	    var elapsedTicks;

	    function fade(eid, in_out) {
	        element = document.getElementById(eid);
	        if (element == null) return;

	        //if(element.FadeState == null) element.FadeState = -2;

	        // in_out = 2 fade_out
	        // in_out = 1 fade_in

	        element.FadeState = in_out;

	        if (element.FadeState == 1 || element.FadeState == -1) {
	            element.FadeState = (element.FadeState == 1) ? -1 : 1;
	            element.FadeTimeLeft = (TimeToFade - element.FadeTimeLeft);
	        }
	        else {
	            element.FadeState = (element.FadeState == 2) ? -1 : 1;
	            element.FadeTimeLeft = TimeToFade;
	            setTimeout("animateFade(" + new Date().getTime() + ",'" + eid + "')", 33);
	        }
	    }

	    function animateFade(lastTick, eid) {
	        curTick = new Date().getTime();
	        elapsedTicks = (curTick - lastTick);
	        element = document.getElementById(eid);

	        if (element.FadeTimeLeft <= elapsedTicks) {
	            element.style.opacity = (element.FadeState == 1) ? '1' : '0';
	            element.style.filter = 'alpha(opacity = ' + (element.FadeState == 1) ? '100' : '0' + ')';
	            element.FadeState = (element.FadeState == 1) ? 2 : -2;
	            document.getElementById('fade').style.visibility = 'hidden';
	            return;
	        }

	        element.FadeTimeLeft -= elapsedTicks;
	        var newOpVal = element.FadeTimeLeft / TimeToFade;
	        if (element.FadeState == 1) newOpVal = (1 - newOpVal);

	        element.style.opacity = newOpVal;
	        element.style.filter = 'alpha(opacity = ' + (newOpVal * 100) + ')';

	        setTimeout("animateFade(" + curTick + ",'" + eid + "')", 33);
	    }
	-->
    </script>
