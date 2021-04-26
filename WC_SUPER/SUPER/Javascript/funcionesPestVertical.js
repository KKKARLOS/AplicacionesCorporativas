/* Valores necesarios para la pestaña retractil */
var nVision=0;
var nIntervaloPX = 20;
var nAlturaPestana = 200;
var nTopPestana = 0;
var oPestRetr;
var oImgPestana;

//document.onreadystatechange = function ()
//                              {
//                                try{ coc(); }catch(e){}
//                                if (document.readyState == "complete" || document.readyState == "loaded"){
//                                    alert('carga');
//                                    oPestRetr = $I("divPestRetr");
//                                    oImgPestana = $I("imgPestHorizontalAux");
//                                }
//                              }
//                              
                              
           
                    
var bCargado = false 

function Cargado() {   
    if (bCargado) return 
    bCargado = true 
    try{ coc(); }catch(e){}
    oPestRetr = $I("divPestRetr");
    oImgPestana = $I("imgPestHorizontalAux");    
}  


if (document.addEventListener ) 
{ // native event  
     document.addEventListener( "DOMContentLoaded", Cargado, false )  
} else if ( document.attachEvent ) {  // IE  
    // IE, the document is inside a frame  
     document.attachEvent("onreadystatechange", function(){  
         if ( document.readyState == "complete" ) {  
             Cargado()  
         }  
     })  
}  

                              
/* Fin de Valores necesarios para la pestaña retractil */


var bPestRetrMostrada = false;
function mostrarOcultarPestVertical(){
    if (!bPestRetrMostrada) mostrarCriterios();
    else ocultarCriterios();
    bPestRetrMostrada = !bPestRetrMostrada;
}

function mostrarCriterios(){
    if (document.readyState != "complete") return;
	nVision = nVision + nIntervaloPX;
	if (oImgPestana != null) oImgPestana.style.top = nVision + nTopPestana + "px";
	oPestRetr.style.clip = "rect(auto auto "+nVision+"px auto)";
	if (nVision < nAlturaPestana) setTimeout("mostrarCriterios()", 1);
}
function ocultarCriterios(){
    if (nVision <= 0) return;
	nVision = nVision - nIntervaloPX;
	if (oImgPestana != null) oImgPestana.style.top = nVision + nTopPestana + "px";
    oPestRetr.style.clip = "rect(auto auto "+nVision+"px auto)";
	if (nVision > 0) setTimeout("ocultarCriterios()", 1);
}
