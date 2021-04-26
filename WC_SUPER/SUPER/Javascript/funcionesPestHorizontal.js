var bPestRetrMostrada = false;
function mostrarOcultarPestHorizontal(){
    if (!bPestRetrMostrada) pull();
    else draw();
    bPestRetrMostrada = !bPestRetrMostrada;
}

function pull(){
    //alert(window.idOcultar);
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
	if ($I("divPestRetr").offsetLeft<nLimiteDer)
		$I("divPestRetr").style.left=($I("divPestRetr").offsetLeft+nIntervaloPX)+"px";
	else if (window.idMostrar){
		$I("divPestRetr").style.left=0+"px";
		clearInterval(idMostrar);
	}
}
function drawengine(){
	if ($I("divPestRetr").offsetLeft>nLimiteIzq)
		$I("divPestRetr").style.left=($I("divPestRetr").offsetLeft-nIntervaloPX)+"px";
	else if (window.idOcultar){
		$I("divPestRetr").style.left=nLimiteIzq+"px";
		clearInterval(idOcultar);
	}
}
