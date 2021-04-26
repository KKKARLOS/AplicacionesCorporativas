function init(){
    mostrarOcultarPestVertical();
}


/* Valores necesarios para la pestaña retractil */
var oPestRetr_width = "920px";
var oPestRetr_visible = "20px";
var nIntervaloPX = 20;
var nLimiteDer = 0;
var nLimiteIzq=(parseInt(oPestRetr_width, 10)-parseInt(oPestRetr_visible, 10))*-1;
/* Fin de Valores necesarios para la pestaña retractil */

var bPestRetrMostrada = false;
function mostrarOcultarPestVertical(){
    if (!bPestRetrMostrada) pull();
    else draw();
    bPestRetrMostrada = !bPestRetrMostrada;
}

function pull(){
	if (window.idOcultar)
	    clearInterval(idOcultar);
	idMostrar=setInterval("pullengine()",5);
}
function draw(){
	clearInterval(idMostrar);
	idOcultar=setInterval("drawengine()",5);
}
function pullengine(){
	if (divPestRetr.offsetLeft<nLimiteDer)
		divPestRetr.style.left=divPestRetr.offsetLeft+nIntervaloPX+"px";
	else if (window.idMostrar){
		divPestRetr.style.left=0;
		clearInterval(idMostrar);
	}
}
function drawengine(){
	if (divPestRetr.offsetLeft>nLimiteIzq)
		divPestRetr.style.left=divPestRetr.offsetLeft-nIntervaloPX+"px";
	else if (window.idOcultar){
		divPestRetr.style.left=nLimiteIzq;
		clearInterval(idOcultar);
	}
}