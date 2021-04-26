var iFilaRespon=0;
function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTituloResponsables", "tblResponsables");
        actualizarLupas("tblTitulo", "tblDatos");
        ocultarProcesando();    
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "lineas":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        actualizarLupas("tblTitulo", "tblDatos");
		        window.focus();
                scrollTablaLineas();
                break;
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogoResponsables").children[0].innerHTML = aResul[2];
                //scrollTablaProf();
		        actualizarLupas("tblTituloResponsables", "tblResponsables");
                
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
        	    
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
        }
        ocultarProcesando();
    }
}

function setResponsable(oFila){
    try{
        borrarCatalogo();
        //$I("hdnResponsableOrigen").value=oFila.id;
        iFilaRespon=oFila.rowIndex;
        mostrarRelacionLineas(oFila.id);
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el responsable.", e.message);
    }
}

function mostrarRelacionLineas(iResponsable){
    try{
        var js_args = "lineas@#@";
        js_args += iResponsable +"@#@"
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la relación de líneas", e.message);
    }
}

var nTopScrollLineas = 0;
var nIDTimeLin = 0;
function scrollTablaLineas(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollLineas){
            nTopScrollLineas = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeLin);
            nIDTimeLin = setTimeout("scrollTablaLineas()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollLineas/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                oFila.onclick = function(){ms(this);};
                oFila.ondblclick = function(){aceptarClick(this.rowIndex)};
                
                //oFila.onmousedown = function(){DD(this);};

                switch (oFila.estado)
                {
                case "A": oFila.cells[0].appendChild(oImgEst1.cloneNode(), null); break;
                case "I": oFila.cells[0].appendChild(oImgEst2.cloneNode(), null); break;
                case "Y": oFila.cells[0].appendChild(oImgEst3.cloneNode(), null);break;
                case "X": oFila.cells[0].appendChild(oImgEst4.cloneNode(), null); break;
                case "B": oFila.cells[0].appendChild(oImgEst5.cloneNode(), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de líneas.", e.message);
    }
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var tblResponsables = $I("tblResponsables");
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblResponsables.rows.length);
        var oFila; 
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblResponsables.rows[i].getAttribute("sw")) {
                oFila = tblResponsables.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    oFila.cells[0].appendChild(oImgV.cloneNode(), null);
                }else{
                    oFila.cells[0].appendChild(oImgM.cloneNode(), null);
                }
//                if (oFila.baja=="1") 
//                    oFila.cells[1].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
        actualizarLupas("tblTitulo", "tblDatos");        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function borrarCatalogoR(){
    try{
        $I("divCatalogoResponsables").children[0].innerHTML = "<table id='tblResponsables'></table>";
        actualizarLupas("tblTituloResponsables", "tblResponsables");
                
	    $I("txtApellido1").value = "";
	    $I("txtApellido2").value = "";
	    $I("txtNombre").value = "";        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo de responsables", e.message);
    }
}
function limpiar(){
    try{
        borrarCatalogoR();
        borrarCatalogo();          
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar catalogos", e.message);
    }
}    
function mostrarProfesional(){
	var strInicial;
    try{
	    if ($I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value==""){
	        mmoff("Inf", "Debe indicar usuario.", 200);
	        return;
	    }
	    
	    strInicial=Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value)+ "@#@";
	    
	    if (strInicial == "@#@@#@@#@") return;

        strInicial += ($I("chkBajas").checked) ? "1":"0";

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;
        var returnValue = tblDatos.rows[indexFila].cells[1].innerText + "@#@" + tblDatos.rows[indexFila].cells[2].innerText + "@#@" + tblResponsables.rows[iFilaRespon].cells[0].innerText + "@#@" + tblDatos.rows[indexFila].cells[3].innerText + "@#@" + tblDatos.rows[indexFila].getAttribute("beneficiario");
        modalDialog.Close(window, returnValue);	 
//        window.returnValue = tblDatos.rows[indexFila].cells[1].innerText + "@#@" + tblDatos.rows[indexFila].cells[2].innerText + "@#@" + tblResponsables.rows[iFilaRespon].cells[0].innerText + "@#@" + tblDatos.rows[indexFila].cells[3].innerText + "@#@" + tblDatos.rows[indexFila].beneficiario;
//        window.close();
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;
        var returnValue = null;
        modalDialog.Close(window, returnValue);	       
//        window.returnValue = null;
//        window.close();
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}