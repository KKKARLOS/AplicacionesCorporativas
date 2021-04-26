function init(){
    nNE = nNEAux;
    colorearNE();
}

function colorearNE(){
    try{
        switch(nNE){
            case 1:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                break;
            case 4:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                break;
            case 5:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5on.gif";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}


function mdn(oFila){
    try{
        alert("Nivel: " + oFila.nivel +", ID completo: " + oFila.id +", ID nodo: " + oFila.id.split("-")[oFila.nivel-1]);
	}catch(e){
		mostrarErrorAplicacion("Error ", e.message);
    }
}

function mostrar(oImg){
    try{
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.nivel;
        //var nDesplegado = oFila.desplegado;
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);
        
        //alert("nIndexFila: "+ nIndexFila);
        for (var i=nIndexFila+1; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].nivel > nNivel){
                if (opcion == "O")
                {
                    tblDatos.rows[i].style.display = "none";
                    if (tblDatos.rows[i].nivel < 4)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }
                else if (tblDatos.rows[i].nivel-1 == nNivel) tblDatos.rows[i].style.display = "block";
            }else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../images/plus.gif";
        else oImg.src = "../../../images/minus.gif"; 

        if (bMostrar) MostrarTodo(); 
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }

        if (nMostrar == 0){//Contraer
            for (var i=0; i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].nivel > 1)
                {
                    if (tblDatos.rows[i].nivel < 5)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                    tblDatos.rows[i].style.display = "none";
                }
                else 
                {
                    tblDatos.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }                             
            }
            ocultarProcesando();
        }else{ //Expandir
            MostrarTodo();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar=false;
var nIndiceTodo = -1;
function MostrarTodo(){
    try
    {
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i=nIndiceAux; i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].nivel < nNE){ 
                if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                    bMostrar=true;
                    nIndiceTodo = i;
                    mostrar(tblDatos.rows[i].cells[0].children[0]);
                    return;
                }
            }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

/* Función para establecer el nivel de expansión */
var nNE = 1;
function setNE(nValor){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }
        nNE = nValor;
        mostrarProcesando();
        
        colorearNE();
        setTimeout("setNE2()", 100);

	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function setNE2(){
    try{
        MostrarOcultar(0);
        if (nNE > 1) MostrarOcultar(1);
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}