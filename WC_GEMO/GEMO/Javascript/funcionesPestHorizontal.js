<!--
var bPestRetrMostrada = false;
function mostrarOcultarPestHorizontal(){
    if (!bPestRetrMostrada) pull();
    else draw();
    bPestRetrMostrada = !bPestRetrMostrada;
}

function pull(){
	if (window.idOcultar)
	    clearInterval(idOcultar);
	idMostrar=setInterval("pullengine()",3);
}
function draw(){
	if (window.idMostrar)
	    clearInterval(idMostrar);
	idOcultar=setInterval("drawengine()",3);
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
-->