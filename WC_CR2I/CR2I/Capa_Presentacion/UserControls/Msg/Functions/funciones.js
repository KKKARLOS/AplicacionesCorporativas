/*********************************************
Funciones para mostrar la capa de mmoff
**********************************************/
try{
    if (window.dialogWidth == "undefined" || window.dialogWidth == null) {
        $I("tblMsg").style.top = ((document.documentElement.offsetHeight / 2) - 20)+"px";
        $I("tblMsg").style.left = ((document.documentElement.offsetWidth / 2) - 115)+"px"; 
    }else{//modales
        $I("tblMsg").style.top = ((window.dialogHeight.substring(0, window.dialogHeight.length - 2) / 2) - 20)+"px";
        $I("tblMsg").style.left = ((window.dialogWidth.substring(0, window.dialogWidth.length - 2) / 2) - 115)+"px"; 	             
    }
} catch (e) { }

//mostrar mensaje off
function mmoff(sTipo, sTexto, nWidth, nTiempo, nHeight, nLeft, nTop) {
    try {
        if (sTipo.toLowerCase() == "hide") {
            hide_mmoff();
            return;
        }
        $I("tblMsg").setAttribute("tipo", sTipo);
        setEstiloMMOff(sTipo);
        var nOcultarMiliseg = (nTiempo) ? nTiempo : 2000;

        if (document.body.clientWidth != null) {
            $I("tblMsg").style.left = (nLeft) ? nLeft + "px" : ((document.documentElement.clientWidth / 2) - nWidth / 2) + "px";
            $I("tblMsg").style.top = (nTop) ? nTop + "px" : ((document.documentElement.clientHeight / 2) - ((nHeight) ? nHeight : 36)) + "px";
        } else {
            $I("tblMsg").style.left = (nLeft) ? nLeft + "px" : ((window.innerWidth / 2) - nWidth / 2)+"px";
            $I("tblMsg").style.top = (nTop) ? nTop + "px" : ((window.innerHeight / 2) - ((nHeight) ? nHeight + 6 : 36)) + "px";
        }

        $I("tblMsg").style.width = nWidth+"px";
        $I("tdMsg5").style.width = (nWidth - 40) + "px";
        $I("tdMsg5").style.textAlign = "left";
        
        var reg = /\n/g;
        $I("tdMsg5").innerHTML = sTexto.replace(reg, "</br>");

        fadeEffect.init("tblMsg", 1);
        if (sTipo.toLowerCase() != "err"
            && sTipo.toLowerCase() != "infper"
            && sTipo.toLowerCase() != "warper"
            && sTipo.toLowerCase() != "sucper"
            ) {
            setTimeout("fadeEffect.init('tblMsg', 0,'" + sTipo + "');", nOcultarMiliseg);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el mensaje informativo", e.message);
    }
}

var fadeEffect = function() {
    return {
        init: function(id, flag, sTipo, target) {
            this.elem = document.getElementById(id);
            clearInterval(this.elem.si);
            this.target = target ? target : flag ? 100 : 0;
            this.flag = flag || -1;
            if (this.flag == 1) {
                if (nName == "ie") {
                    $I("imgPopUpClose").style.visibility = "hidden";
                    $I("imgPopUpCloseIE").style.position = "absolute";
                    $I("imgPopUpCloseIE").style.top = ($I("tblMsg").style.pixelTop - 9)+ "px";
                    $I("imgPopUpCloseIE").style.left = ($I("tblMsg").style.pixelLeft + $I("tblMsg").style.pixelWidth - 14);
                    $I("imgPopUpCloseIE").style.display = "block";
                } else {
                    $I("imgPopUpClose").style.position = "absolute";
                    $I("imgPopUpClose").style.top = "-9px"; // $I("tblMsg").style.top;
                    $I("imgPopUpClose").style.right = "-10px"; // $I("tblMsg").style.left;
                }
                this.elem.style.opacity = 1;
                this.elem.style.filter = 'alpha(opacity=' + 100 + ')';
                this.elem.style.display = "block";
            } else {
                this.alpha = this.elem.style.opacity ? parseFloat(this.elem.style.opacity) * 100 : 0;
                this.elem.si = setInterval(function() { fadeEffect.tween() }, 20);
            }
        },
        tween: function() {
            if (this.alpha == this.target) {
                clearInterval(this.elem.si);
            } else {
                var value = Math.round(this.alpha + ((this.target - this.alpha) * .05)) + (1 * this.flag);
                var sTipoAux = $I("tblMsg").getAttribute("tipo");
                if (sTipoAux.toLowerCase() == "err"
                    || sTipoAux.toLowerCase() == "infper"
                    || sTipoAux.toLowerCase() == "warper"
                    || sTipoAux.toLowerCase() == "sucper"
                    ) value = 100;
                this.elem.style.opacity = value / 100;
                this.elem.style.filter = 'alpha(opacity=' + value + ')';
                this.alpha = value;
                if (nName == "ie") {
                    $I("imgPopUpCloseIE").style.display = "none";
                }
                if (value == 0) {
                    hide_mmoff();
                }
            }
        }
    }
} ();

function hide_mmoff() {
    $I("imgPopUpCloseIE").style.display = "none";
    $I("tblMsg").style.opacity = 0;
    $I("tblMsg").style.filter = 'alpha(opacity=0)';
    $I("tblMsg").style.display = "none";
}