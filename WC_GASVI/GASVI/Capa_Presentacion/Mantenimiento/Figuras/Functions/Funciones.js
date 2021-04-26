var oTipoTime = null;
var js_Grabar = new Array();
var js_Integrantes = new Array();
var js_Figuras = new Array();
var filaSeleccionada = 0;
var bCambioFiguras = false;
var nIndiceFilaIntegranteActivo = -1;
var bRegresar = false;

function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Figuras";
        desActivarGrabar();
	    ocultarProcesando();
	    bCambios = false;
	    $I("hdnFiguraAnt").value = ""; //Valor del estado anterior del combo.
	    js_Figuras[js_Figuras.length] = { tipo: "A", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "P", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "S", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "L", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "T", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "N", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "1", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "2", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "3", lectura: 0 };
	    js_Figuras[js_Figuras.length] = { tipo: "4", lectura: 0 };
	} catch (e) {
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
            case "integrantes":
                $I("divIntegrantes").children[0].innerHTML = aResul[2];
                //actualizarLupas("tblTitulo2", "tblIntegrantes");
                scrollTablaProfAsig();
                actualizarArray(1);
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
                break;
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                switch(aResul[3]){
                    case "1":
                        $I("divPersonas").children[0].innerHTML = aResul[2];
                        scrollTablaProf();
                        actualizarLupas("tblTitulo", "tblPersonas");                
        	            $I("txtApellido1").value = "";
        	            $I("txtApellido2").value = "";
        	            $I("txtNombre").value = "";
                        break;
                    case "2":
                        $I("divPersonas2").children[0].innerHTML = aResul[2];
                        scrollTablaProf2();
                        //actualizarLupas("tblTitulo3", "tblPersonas2");                
        	            $I("txtSubApellido1").value = "";
        	            $I("txtSubApellido2").value = "";
        	            $I("txtSubNombre").value = "";
                        break;
                }
                break;
            case "tramitados":
                $I("divTramNodo").children[0].innerHTML = aResul[2];
                $I("txtSubApellido1").value = "";
                $I("txtSubApellido2").value = "";
                $I("txtSubNombre").value = "";
                actualizarArray(2);
                if (tblIntegrantes.rows[nIndiceFilaIntegranteActivo].setAttribute("leido","1")) {
                    if (js_Grabar.length > 0) {
                        var aFilaIntegrantes2 = FilasDe("tblIntegrantes2");
                        if (aFilaIntegrantes2 != null) {
                            var filaPadre = buscarFilaSelecPadre();
                            for (var i = 0, nCountLoop = js_Grabar.length; i < nCountLoop; i++) {
                                if (js_Grabar[i].idFicepi == filaPadre[0]) {
                                    var datosElemento = js_Grabar[i].nombreElemento.split("#sCad#");
                                    switch (js_Grabar[i].accionElemento) {
                                        case "N":
                                        case "U":
                                        case "I":
                                            reConvocar(js_Grabar[i].idElemento, datosElemento[0], datosElemento[1], datosElemento[2], datosElemento[3], js_Grabar[i].accionElemento, 2);
                                            break;
                                        case "D":
                                            for (var j = 0, nCountLoop2 = aFilaIntegrantes2.length; j < nCountLoop2; j++) {
                                                if (aFilaIntegrantes2[j].id == js_Grabar[i].idElemento)
                                                    mfa(aFilaIntegrantes2[j], "D");
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                tblIntegrantes.rows[nIndiceFilaIntegranteActivo].setAttribute("leido" , "1");
                //actualizarLupas("tblTitulo4", "tblIntegrantes2");
                scrollTablaProfAsig2();
                break;
            case "integrantesNodos":
                $I("divTramNodo").children[0].innerHTML = aResul[2];
                if (tblIntegrantes.rows[nIndiceFilaIntegranteActivo].getAttribute("leido", "1")) {
                    if (js_Grabar.length > 0) {
                        var aFilaIntegrantes2 = FilasDe("tblIntegrantes2");
                        if (aFilaIntegrantes2 != null) {
                            var filaPadre = buscarFilaSelecPadre();
                            for (var i = 0, nCountLoop = js_Grabar.length; i < nCountLoop; i++) {
                                if (js_Grabar[i].idFicepi == filaPadre[0]) {
                                    var datosElemento = js_Grabar[i].nombreElemento.split("#sCad#");
                                    switch (js_Grabar[i].accionElemento) {
                                        case "N":
                                        case "U":
                                        case "I":
                                            if ($I("cboFiguras").value == "L") reConvocar(js_Grabar[i].idElemento, datosElemento[0], null, null, null, js_Grabar[i].accionElemento, 3);
                                            else reConvocar(js_Grabar[i].idElemento, datosElemento[0], null, datosElemento[1], null, js_Grabar[i].accionElemento, 3);
                                            break;
                                        case "D":
                                            for (var j = 0, nCountLoop2 = aFilaIntegrantes2.length; j < nCountLoop2; j++) {
                                                if (aFilaIntegrantes2[j].id == js_Grabar[i].idElemento)
                                                    mfa(aFilaIntegrantes2[j], "D");
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                tblIntegrantes.rows[nIndiceFilaIntegranteActivo].setAttribute("leido", "1");
                //actualizarLupas("tblTitulo4", "tblIntegrantes2");
                ot('tblIntegrantes2', 2, 0, '');
                actualizarArray(2);
                setTimeout("mostrarCatalogoNodoSubnodos(\"" + $I("cboFiguras").value + "\")", 50); //50
                break; 
            case "catalogoNodos":
                $I("divPersonas2").children[0].innerHTML = aResul[2];
                //actualizarLupas("tblTitulo3", "tblPersonas2");                
                break;

            case "grabar":
                var aFilaIntegrantes = FilasDe("tblIntegrantes");
                if (aFilaIntegrantes != null) {
                    for (var i = aFilaIntegrantes.length - 1; i >= 0; i--) {
                        if (aFilaIntegrantes[i].getAttribute("bd") == "D") tblIntegrantes.deleteRow(i);
                        else {
                            if (aFilaIntegrantes[i].className == "FS") filaSeleccionada = aFilaIntegrantes[i].id;
                            mfa(aFilaIntegrantes[i], "N");
                        }
                    }
                }
                scrollTablaProfAsig();
                ot('tblIntegrantes', 2, 0, '');
                if ($I("tblIntegrantes2") != null) {
                    var aFilaIntegrantes2 = $I("tblIntegrantes2").rows;
                    for (var i = aFilaIntegrantes2.length - 1; i >= 0; i--) {
                        if (aFilaIntegrantes2[i].getAttribute("bd") == "D") tblIntegrantes2.deleteRow(i);
                        else mfa(aFilaIntegrantes2[i], "N");
                    }
                    ot('tblIntegrantes2', 2, 0, '');
                }
                actualizarArray(3);
                desActivarGrabar();
                bCambios = false;
                actualizarLupas("tblTitulo", "tblPersonas");
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bRegresar = false;
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                }
                else {
                    if (bCambioFiguras) {
                        bCambioFiguras = false;
                        setTimeout("cargarFiguras2()", 20)
                    }
                    else {
                        aFilaIntegrantes = FilasDe("tblIntegrantes");
                        if (aFilaIntegrantes != null) {
                            for (var i = 0; i < aFilaIntegrantes.length; i++) {
                                if (aFilaIntegrantes[i].id == filaSeleccionada) {
                                    filaSeleccionada = i;
                                    setTimeout("simularClick(filaSeleccionada)", 20);
                                    break;
                                }
                                else vaciarTablas(1);
                            }
                        }
                    }
                }
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        ocultarProcesando();
    }
}

function simularClick(fila){
    if (ie) tblIntegrantes.rows[fila].click();
    else {
        var clickEvent = window.document.createEvent("MouseEvent");
        clickEvent.initEvent("click", false, true);
        tblIntegrantes.rows[fila].dispatchEvent(clickEvent);
    }
}

function actualizarArray(iTipo){
    if($I("tblIntegrantes") != null) var aFilasI1 = FilasDe("tblIntegrantes");
    else var aFilasI1 = null;
    if($I("tblIntegrantes2") != null) var aFilasI2 = FilasDe("tblIntegrantes2");
    else var aFilasI2 = null;
    var elemento = "";
    var newArray = new Array();
    var newArray2 = new Array();
    
    switch(iTipo){
        case 1://Tabla de integrantes (tblIntegrantes)
            if (aFilasI1 != null){
                for(var i=0, nCount=aFilasI1.length; i<nCount; i++){
                    elemento = aFilasI1[i].innerText + "#sCad#" + aFilasI1[i].getAttribute("sexo") + "#sCad#" + aFilasI1[i].getAttribute("tipo") + "#sCad#" + aFilasI1[i].getAttribute("baja");
                    js_Integrantes[js_Integrantes.length] = {tipo:$I("cboFiguras").value, accion:"N", idFicepi:aFilasI1[i].id, nombreIntegrante:elemento};
                }
            }
            break;
        case 2://Tabla de subNodo, etc. (tblIntegrantes2)
            if (aFilasI2 != null){
                var filaPadre = buscarFilaSelecPadre();                
                for(var i=0, nCount=aFilasI2.length; i<nCount; i++){
                    if($I("cboFiguras").value == "T") elemento = aFilasI2[i].innerText + "#sCad#" + aFilasI2[i].getAttribute("sexo") + "#sCad#" + aFilasI2[i].getAttribute("tipo") + "#sCad#" + aFilasI2[i].getAttribute("baja");
                    else if ($I("cboFiguras").value == "L") elemento = aFilasI2[i].innerText + "#sCad#" + aFilasI2[i].getAttribute("of");
	                     else elemento = aFilasI2[i].innerText;
                    js_Grabar[js_Grabar.length] = {tipo:$I("cboFiguras").value, accion:"N", idFicepi:filaPadre[0], nombreIntegrante:filaPadre[2], accionElemento:"N", idElemento:aFilasI2[i].id, nombreElemento:elemento};
                }
            }            
            break;
        case 3:
            if (js_Integrantes.length > 0) {
                for (var i = 0, nCountLoop = js_Integrantes.length; i < nCountLoop; i++) { //Actualiza el array js_Integrantes
                    if (js_Integrantes[i].accion == "D") {
                        js_Integrantes.splice(i, 1);
                        nCountLoop--;
                        i--;
                    }
                    else js_Integrantes[i].accion = "N";
                }
            }
            if (js_Grabar.length > 0) {
                for (var i = 0, nCountLoop = js_Grabar.length; i < nCountLoop; i++) { //Actualiza el array js_Grabar
                    if (js_Grabar[i].accionElemento == "D" || js_Grabar[i].accion == "D") {
                        js_Grabar.splice(i, 1);
                        nCountLoop--;
                        i--;
                    }
                    else {
                        js_Grabar[i].accion = "N";
                        js_Grabar[i].accionElemento = "N";
                    }
                }
            }
            break; 
    }
    for(var i=0, nCount=js_Figuras.length; i<nCount; i++){//Modifico js_Figuras para saber que elemento del combo ha sido leido de la bd.
        if (js_Figuras[i].tipo == $I("hdnFiguraAnt").value) {
            js_Figuras[i].lectura = 1;
            break;
        }
    }
}

function habilitarBuscador(sTipo){
    if(sTipo == 1){//Buscador de la capa superior
        $I("txtApellido1").disabled = false;
        $I("txtApellido2").disabled = false;
        $I("txtNombre").disabled = false;
    }
    else{//Buscador de la capa inferior
        $I("txtSubApellido1").disabled = false;
        $I("txtSubApellido2").disabled = false;
        $I("txtSubNombre").disabled = false;
    }      
}

function deshabilitarBuscador(sTipo){
    if(sTipo == 1){//Buscador de la capa superior
        $I("txtApellido1").disabled = true;
        $I("txtApellido2").disabled = true;
        $I("txtNombre").disabled = true;
    }
    else{//Buscador de la capa inferior
        $I("txtSubApellido1").disabled = true;
        $I("txtSubApellido2").disabled = true;
        $I("txtSubNombre").disabled = true;
    } 
}

function vaciarTablas(sTipo){
    if($I("tblIntegrantes2") != null) BorrarFilasDe("tblIntegrantes2");
    if($I("cboFiguras").value == "T"){
        if($I("tblPersonas2") != null) BorrarFilasDe("tblPersonas2");
    }
    switch(sTipo){
        case 1://Cambio en el combo de figuras
        case 2://Tramitados
            if($I("tblIntegrantes2") != null) BorrarFilasDe("tblIntegrantes2");
            if($I("tblPersonas2") != null) BorrarFilasDe("tblPersonas2");
            break;
        case 3://Nodos y Subnodos
            if($I("tblIntegrantes2") != null) BorrarFilasDe("tblIntegrantes2");
            break;
        case 4://Cuando el combo es vacio
            BorrarFilasDe("tblIntegrantes");
            if($I("tblPersonas") != null) BorrarFilasDe("tblPersonas");
            break;
        case 5://Vaciar la tabla de integrantes (tblIntegrantes)
            if($I("tblIntegrantes") != null) BorrarFilasDe("tblIntegrantes");
            break;
    }
}
function cargarFiguras() {//Acciones a realizar cuando se hace una elección en el combo de figuras
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    bCambioFiguras = true;
                    grabar();
                }
                else {
                    bCambioFiguras = false;
                    desActivarGrabar();
                    cargarFiguras2();
                }
            });
        }
        else
            cargarFiguras2();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cargar figuras", e.message);
    }
}
    function cargarFiguras2(){//Acciones a realizar cuando se hace una elección en el combo de figuras
    try{
        //if(bCambios){
        //    if(confirm("Datos modificados. ¿Desea grabarlos?")){
        //        bCambioFiguras = true;
        //        grabar();
        //        return;
        //    }
        //    else{
        //        desActivarGrabar();
        //        //Inicializamos el array de grabar//
        //        //js_Grabar.length = 0;
        //    }
        //}
        $I("hdnFiguraAnt").value = $I("cboFiguras").value
        habilitarBuscador(1);
        deshabilitarBuscador(2);//Desactiva el buscador de la capa inferior hasta que se seleccione una fila
        switch ($I("cboFiguras").value){
            case "L": //Aceptadores
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "visible";
                $I("divTituloInferior").style.visibility = "visible";
                $I("tdTituloIntegrantes").innerHTML = "Aceptadores";
                $I("divTituloInferior").innerHTML = "Selección de oficinas";
                $I("tdTitulo").innerHTML = "Oficinas integrantes";
                $I("tdTituloBuscador2").innerHTML = "Oficinas";
                $I("tdTitulo").style.paddingLeft = "6px";
                $I("tdTituloBuscador2").style.paddingLeft = "6px";
                break;
            case "T": //Tramitadores
                $I("divBuscadorTramitados").style.visibility = "visible";
                $I("divSubFiguras").style.visibility = "visible";
                $I("divTituloInferior").style.visibility = "visible";
                $I("tdTituloIntegrantes").innerHTML = "Tramitadores";
                $I("divTituloInferior").innerHTML = "Selección de beneficiarios";
                $I("tdTitulo").innerHTML = "Beneficiarios";
                $I("tdTituloBuscador2").innerHTML = "Profesionales";
                $I("tdTitulo").style.paddingLeft = "20px";
                $I("tdTituloBuscador2").style.paddingLeft = "20px";
                break;
            case "N":
                $I("tdTituloIntegrantes").innerHTML = "Profesionales";
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "visible";
                $I("divTituloInferior").style.visibility = "visible";
                $I("divTituloInferior").innerHTML = "Selección de " + strEstructuraNODO;
                $I("tdTitulo").innerHTML = "Integrantes de " + strEstructuraNODO;
                $I("tdTituloBuscador2").innerHTML = strEstructuraNODO;
                $I("tdTitulo").style.paddingLeft = "3px";
                $I("tdTituloBuscador2").style.paddingLeft = "3px";
                break;
            case "1":
                $I("tdTituloIntegrantes").innerHTML = "Profesionales";
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "visible";
                $I("divTituloInferior").style.visibility = "visible";
                $I("divTituloInferior").innerHTML = "Selección de " + strEstructuraSUPERNODO1;
                $I("tdTitulo").innerHTML = "Integrantes de " + strEstructuraSUPERNODO1;
                $I("tdTituloBuscador2").innerHTML = strEstructuraSUPERNODO1;
                $I("tdTitulo").style.paddingLeft = "3px";  
                $I("tdTituloBuscador2").style.paddingLeft = "3px";            
                break;
            case "2":
                $I("tdTituloIntegrantes").innerHTML = "Profesionales";
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "visible";
                $I("divTituloInferior").style.visibility = "visible";
                $I("divTituloInferior").innerHTML = "Selección de " + strEstructuraSUPERNODO2;
                $I("tdTitulo").innerHTML = "Integrantes de " + strEstructuraSUPERNODO2;
                $I("tdTituloBuscador2").innerHTML = strEstructuraSUPERNODO2;
                $I("tdTitulo").style.paddingLeft = "3px";
                $I("tdTituloBuscador2").style.paddingLeft = "3px";
                break;
            case "3":
                $I("tdTituloIntegrantes").innerHTML = "Profesionales";
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "visible";
                $I("divTituloInferior").style.visibility = "visible";
                $I("divTituloInferior").innerHTML = "Selección de " + strEstructuraSUPERNODO3;
                $I("tdTitulo").innerHTML = "Integrantes de " + strEstructuraSUPERNODO3;
                $I("tdTituloBuscador2").innerHTML = strEstructuraSUPERNODO3;
                $I("tdTitulo").style.paddingLeft = "3px";
                $I("tdTituloBuscador2").style.paddingLeft = "3px";
                break;
            case "4":
                $I("tdTituloIntegrantes").innerHTML = "Profesionales";
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "visible";
                $I("divTituloInferior").style.visibility = "visible";
                $I("divTituloInferior").innerHTML = "Selección de " + strEstructuraSUPERNODO4;
                $I("tdTitulo").innerHTML = "Integrantes de " + strEstructuraSUPERNODO4;
                $I("tdTituloBuscador2").innerHTML = strEstructuraSUPERNODO4;
                $I("tdTitulo").style.paddingLeft = "3px";
                $I("tdTituloBuscador2").style.paddingLeft = "3px";
                break;
            case "P":
            case "A":
            case "S":
                $I("tdTituloIntegrantes").innerHTML = "Profesionales";
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "hidden";
                $I("divTituloInferior").style.visibility = "hidden";                
                break;
            default:
                $I("tdTituloIntegrantes").innerHTML = "Profesionales";
                $I("divBuscadorTramitados").style.visibility = "hidden";
                $I("divSubFiguras").style.visibility = "hidden";
                $I("divTituloInferior").style.visibility = "hidden";
                deshabilitarBuscador(1);
                vaciarTablas(4);
                return;                
        }     
        vaciarTablas(1);
        
        $I("txtApellido1").focus();
        var existe = false;
        for(var i=0, nCount=js_Figuras.length; i<nCount; i++){
            if(js_Figuras[i].tipo == $I("cboFiguras").value && js_Figuras[i].lectura == 1){
                existe = true;
                break;
            }
        }
        if(!existe){
    	    var js_args = "integrantes@#@" + $I("cboFiguras").value;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        else{
            vaciarTablas(5);
            for(var i=0, nCount=js_Integrantes.length; i<nCount; i++){
                if(js_Integrantes[i].tipo == $I("cboFiguras").value && js_Integrantes[i].accion == "N"){
                    var aIntengrante =  (js_Integrantes[i].nombreIntegrante).split("#sCad#");
                    reConvocarIntegrantes(js_Integrantes[i].idFicepi, aIntengrante[0], aIntengrante[1], aIntengrante[2], aIntengrante[3], js_Integrantes[i].accion);
                }
            }         
        }
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los integrantes", e.message);
    }
}

function mostrarProfesional(iTipo){
	var strInicial;
    try{
        var aFilaIntegrantes = FilasDe("tblIntegrantes");
        switch(iTipo){
            case 1:
                strInicial = Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
                var sExcluidos = "";
                if (aFilaIntegrantes != null) {
                    for (var i = 0, nCountLoop = aFilaIntegrantes.length; i < nCountLoop; i++) {
                        sExcluidos += aFilaIntegrantes[i].id + ",";
                    }
                }
                sExcluidos = sExcluidos.substring(0, sExcluidos.length - 1);
                break;
            case 2:
                strInicial = Utilidades.escape($I("txtSubApellido1").value) + "@#@" + Utilidades.escape($I("txtSubApellido2").value) + "@#@" + Utilidades.escape($I("txtSubNombre").value);
                var aFilaIntegrantes2 = FilasDe("tblIntegrantes2");
                var sExcluidos = "";
                if (aFilaIntegrantes != null) {
                    for (var i = 0, nCountLoop = aFilaIntegrantes.length; i < nCountLoop; i++) {
                        if (aFilaIntegrantes[i].className == "FS") {
                            sExcluidos = aFilaIntegrantes[i].id + ",";
                            break;
                        }
                    }
                }
                if (aFilaIntegrantes2 != null) {
                    for (var i = 0, nCountLoop = aFilaIntegrantes2.length; i < nCountLoop; i++) {
                        sExcluidos += aFilaIntegrantes2[i].id + ",";
                    }
                }
                sExcluidos = sExcluidos.substring(0, sExcluidos.length - 1);
                break;        
        }
        strInicial += "@#@" + sExcluidos;
	    if (strInicial == "@#@@#@") return;
	    setTimeout("mostrarProfesionalAux('" + strInicial + "'," + iTipo + ")",30);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}

function mostrarProfesionalAux(strInicial, iTipo){
    try {
        switch(iTipo){
            case 1:
                if (fTrim($I("txtApellido1").value) == ""
                    && fTrim($I("txtApellido2").value) == ""
                    && fTrim($I("txtNombre").value) == "") {
                    ocultarProcesando();
                    mmoff("War", "Debe introducir algún criterio de búsqueda", 280);
                    $I("txtApellido1").focus();
                    return;
                }
                break;
            case 2:
                if (fTrim($I("txtSubApellido1").value) == ""
                    && fTrim($I("txtSubApellido2").value) == ""
                    && fTrim($I("txtSubNombre").value) == "") {
                    ocultarProcesando();
                    mmoff("War", "Debe introducir algún criterio de búsqueda", 280);
                    $I("txtSubApellido1").focus();
                    return;
                }
                break;
        }
        var js_args = "buscar@#@" + strInicial + "@#@" + iTipo;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesionales", e.message);
    }
}

function mostrarTramitados(oFila){
    try{
        var aElemento = null;
        nIndiceFilaIntegranteActivo = oFila.rowIndex;
        var idFicepi = oFila.id;
        vaciarTablas(2);
        habilitarBuscador(2);
        if(oFila.getAttribute("leido") == "0"){
    	    var js_args = "tramitados@#@" + idFicepi;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        else{//idUsuario, strUsuario, sexo, tipo, baja, bd, iTipo
            for(var i=0, nCount=js_Grabar.length; i<nCount; i++){
                if(js_Grabar[i].idFicepi == idFicepi){
                    aElemento = (js_Grabar[i].nombreElemento).split("#sCad#");
                    reConvocar(js_Grabar[i].idElemento, aElemento[0], aElemento[1], aElemento[2], aElemento[3], js_Grabar[i].accionElemento, 2);
                }                
            }
        }
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los integrantes del tramitador seleccionado.", e.message);
    }
}

function restaurarFila2() { //Función que se llama cuando se pincha con el botón derecho a "No Eliminar"
    try {
        if (oFilaARestaurar.parentNode.parentNode.id == "tblIntegrantes") {
            var aFilas = FilasDe("tblIntegrantes");
            if (aFilas.length > 0 && aFilas.length - 1 >= iFila) {
                if (aFilas[iFila].getAttribute("bd") == "U" && aFilas[iFila].className == "FS") {
                    aFilas[iFila].setAttribute("leido", "0");
                    switch ($I("cboFiguras").value) {
                        case "T":
                            aFilas[iFila].onclick = function() {
                                ms(this);
                                mostrarTramitados(this);
                            };
                            habilitarBuscador(2);
                            break;
                        case "L":
                            habilitarBuscador(2);
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "N":
                            aFilas[iFila].onclick = function() {
                                ms(this);
                                mostrarSubIntegrantes(this, $I("cboFiguras").value);
                                //mostrarCatalogoSubTime($I("cboFiguras").value);
                            };
                            break;
                    }
                    for (var i = 0, nCount = js_Grabar.length; i < nCount; i++) {
                        if (js_Grabar[i].idFicepi == aFilas[iFila].id && $I("cboFiguras").value == js_Grabar[i].tipo)
                            js_Grabar[i].accion = "U";
                    }
                    for (var i = 0, nCount = js_Integrantes.length; i < nCount; i++) {
                        if (js_Integrantes[i].idFicepi == aFilas[iFila].id && $I("cboFiguras").value == js_Integrantes[i].tipo && js_Integrantes[i].accion == "D")
                            js_Integrantes[i].accion = "U";
                    }
                    if (ie) aFilas[iFila].click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        aFilas[iFila].dispatchEvent(clickEvent);
                    }
                    return;
                }
                
            }
        }
        if (oFilaARestaurar.parentNode.parentNode.id == "tblIntegrantes2") {
            var aFilas = FilasDe("tblIntegrantes2");
            if (aFilas.length > 0 && aFilas.length - 1 >= iFila) {
                if (aFilas[iFila].getAttribute("bd") == "U" && aFilas[iFila].className == "FS") {
                    var filaPadre = buscarFilaSelecPadre();
                    for (var i = 0, nCount = js_Grabar.length; i < nCount; i++) {
                        if (js_Grabar[i].idFicepi == filaPadre[0] && js_Grabar[i].idElemento == aFilas[iFila].id)
                            js_Grabar[i].accionElemento = "U";
                    }
                    return;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al restaurar la fila.", e.message);
    }
}

function mostrarSubIntegrantes(oFila, sTipo){
    try{
        var idFicepi = oFila.id;
        var aElemento = null;
        nIndiceFilaIntegranteActivo = oFila.rowIndex;
        vaciarTablas(3);
        habilitarBuscador(2);
        if(oFila.getAttribute("leido") == "0"){
    	    var js_args = "integrantesNodos@#@" + idFicepi + "@#@" + sTipo;
            RealizarCallBack(js_args, "");
        }
        else{
            for(var i=0, nCount=js_Grabar.length; i<nCount; i++){
                if (js_Grabar[i].idFicepi == idFicepi && $I("cboFiguras").value == js_Grabar[i].tipo) {
                    aElemento = (js_Grabar[i].nombreElemento).split("#sCad#");
                    if (aElemento != "") {
                        if ($I("cboFiguras").value == "L") reConvocar(js_Grabar[i].idElemento, aElemento[0], null, aElemento[1], null, js_Grabar[i].accionElemento, 3);
                        else reConvocar(js_Grabar[i].idElemento, aElemento[0], null, null, null, js_Grabar[i].accionElemento, 3);
                    }
                }
            }
            ot('tblIntegrantes2', 2, 0, '');      
        }
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los nodos.", e.message);
    }
}

//function mostrarCatalogoSubTime(sTipo){
//    oTipoTime = sTipo;
//    setTimeout("mostrarCatalogoNodoSubnodos()", 50);//50
//}

function mostrarCatalogoNodoSubnodos(sTipo) {
    try{
        //var sTipo = oTipoTime;
    	var js_args = "catalogoNodos@#@" + sTipo;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los el catálogo.", e.message);
    }
}

function anadirConvocados(iTipo){
    try{
	    if (bLectura) return;
	    switch (iTipo){
	        case 1:
	            var aFilas = $I("tblPersonas").rows;
	            break;
	        case 2:
	        case 3:
	            var aFilas = $I("tblPersonas2").rows;
	            break;
	    }
	    if (aFilas.length > 0){
		    for (x=0; x<aFilas.length; x++){
		        if (aFilas[x].className == "FS"){
			        if (!estaEnLista(aFilas[x].id, iTipo)){
                        switch (iTipo){
	                        case 1:
	                        case 2:
	                            convocar(aFilas[x].id, aFilas[x].cells[1].innerText,aFilas[x].getAttribute("sexo"),aFilas[x].getAttribute("tipo"), aFilas[x].getAttribute("baja"), iTipo);
	                            break;
	                        case 3:
	                            convocar(aFilas[x].id, aFilas[x].cells[1].innerText, null, null, null, iTipo);
	                            break;
	                    }
			        }
			    }    
		    }
		}
		switch (iTipo){
            case 1:
                actualizarLupas("tblTitulo", "tblPersonas");
                break;
            case 2:
            case 3:
                //actualizarLupas("tblTitulo3", "tblPersonas2");
                break;
        }
	}catch(e){
	    switch (iTipo){
            case 1:
            case 2:
                mostrarErrorAplicacion("Error al añadir integrantes", e.message);
                break;
            case 3:
                mostrarErrorAplicacion("Error al añadir los elementos", e.message);
                break;
        }		
    }
}

function fnRelease(e)
{
    if (beginDrag == false) return;
    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;   				    
	if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnRelease);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }   	
	
	var obj = $I("DW");
	var nIndiceInsert = null;
	var oTable;
	if (oTarget != null && (FromTable == ToTable)) {
	    for (var x = 0; x <= aEl.length - 1; x++) {
	        oRow = aEl[x];
	        if (ie) oRow.click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                oRow.dispatchEvent(clickEvent);
            }
	    }
	}
	else if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{	
	    switch (oElement.tagName){
	        case "TD": nIndiceInsert = event.srcElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = event.srcElement.parentNode.parentNode.rowIndex; break;
	    }
	    oTable = oTarget.getElementsByTagName("TABLE")[0];
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
		            if (js_Grabar.length > 0) {
		                for (var j = 0, nCountLoop = js_Grabar.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar
		                    if (js_Grabar[j].idFicepi == oRow.id && $I("cboFiguras").value == js_Grabar[j].tipo) {
		                        js_Grabar.splice(j, 1);
		                        nCountLoop--;
		                        j--;
		                    }
		                }
		            }
		            if (js_Integrantes.length > 0) {
		                for (var j = 0, nCountLoop = js_Integrantes.length; j < nCountLoop; j++) { //Actualiza el array js_Integrantes
		                    if (js_Integrantes[j].idFicepi == oRow.id && $I("cboFiguras").value == js_Integrantes[j].tipo) {
		                        js_Integrantes[j].accion = "D";
		                    }
		                }
		            }
		            if (oRow.getAttribute("bd") == "I") {
		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                vaciarTablas(3);
		                if ($I("tblPersonas2") != null) BorrarFilasDe("tblPersonas2");
		            }
		            else {
		                mfa(oRow, "D");
		                switch ($I("cboFiguras").value) {
		                    case "T":
		                    case "L":
		                        deshabilitarBuscador(2);
		                    case "1":
		                    case "2":
		                    case "3":
		                    case "4":
		                    case "N":
		                        vaciarTablas(1);
		                        oRow.onclick = null;
		                        oRow.onclick = function() {
		                            ms(this);
		                            vaciarTablas(1);
		                        };
		                        break;
		                }
		                var elemento = oRow.innerText + "#sCad#" + oRow.getAttribute("sexo") + "#sCad#" + oRow.getAttribute("tipo") + "#sCad#" + oRow.getAttribute("baja");
		                js_Grabar[js_Grabar.length] = { tipo: $I("cboFiguras").value, accion: "D", idFicepi: oRow.id, nombreIntegrante: elemento, accionElemento: "", idElemento: "", nombreElemento: "" }
		            }
		            var existeGrabar = false;
		            if (js_Grabar.length > 0) {
		                for (var j = 0, nCountLoop = js_Grabar.length; j < nCountLoop; j++) { //Comprobación de los datos a grabar
		                    if (js_Grabar[j].idFiaccioncepi != "N") {
		                        activarGrabar();
		                        existeGrabar = true;
		                        break;
		                    }
		                }
		            }
		            if (!existeGrabar) desActivarGrabar();
		            break;
			    case "imgPapelera2":
		        case "ctl00_CPHC_imgPapelera2":
		            var filaPadre = buscarFilaSelecPadre();
	                if (oRow.getAttribute("bd") == "I"){
	                    if (js_Grabar.length > 0) {
	                        for (var j = 0, nCountLoop = js_Grabar.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar
	                            if (js_Grabar[j].idFicepi == filaPadre[0] && js_Grabar[j].idElemento == oRow.id && $I("cboFiguras").value == js_Grabar[j].tipo) {
	                                js_Grabar.splice(j, 1);
	                                nCountLoop--;
	                                j--;
	                            }
	                        }
	                    }   
    	                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);                
	                }
	                else{
	                    mfa(oRow, "D");
	                    var elemento = "";
	                    if($I("cboFiguras").value == "T") elemento = oRow.innerText + "#sCad#" + oRow.getAttribute("sexo") + "#sCad#" + oRow.getAttribute("tipo") + "#sCad#" + oRow.getAttribute("baja");
	                    else if ($I("cboFiguras").value == "L") elemento = oRow.innerText + "#sCad#" + oRow.of;
	                        else elemento = oRow.innerText;
	                    var existe = false;
	                    for(var i=0, nCount=js_Grabar.length; i<nCount; i++){
	                        if (js_Grabar[i].idFicepi == filaPadre[0] && js_Grabar[i].idElemento == oRow.id && $I("cboFiguras").value == js_Grabar[i].tipo) {
	                            js_Grabar[i].accionElemento = "D";
	                            existe = true;
	                            break;
	                        }
	                    }
	                    if(!existe) js_Grabar[js_Grabar.length] = {tipo:$I("cboFiguras").value, accion:filaPadre[1], idFicepi:filaPadre[0], nombreIntegrante:filaPadre[2], accionElemento:"D", idElemento:oRow.id, nombreElemento:elemento};
	                }	                
	                activarGrabar();
			        break;
		        case "divIntegrantes":
		        case "ctl00_CPHC_divIntegrantes":
		            if (FromTable == null || ToTable == null) continue;
		            if (oRow.parentNode.parentNode.id != "tblPersonas") continue;

		            //var oTable = oTarget.getElementsByTagName("table")[0];
		            var sw = 0;
		            //Controlar que el elemento a insertar no existe en la tabla
		            for (var i = 0; i < oTable.rows.length; i++) {
		                if (oTable.rows[i].id == oRow.id) {
		                    sw = 1;
		                    break;
		                }
		            }
		            if (sw == 0) {
		                var NewRow;
		                if (nIndiceInsert == null) {
		                    nIndiceInsert = oTable.rows.length;
		                    NewRow = oTable.insertRow(nIndiceInsert);
		                }
		                else {
		                    if (nIndiceInsert > oTable.rows.length)
		                        nIndiceInsert = oTable.rows.length;
		                    NewRow = oTable.insertRow(nIndiceInsert);
		                }
		                nIndiceInsert++;
		                var oCloneNode = oRow.cloneNode(true);
		                oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);		                
                        oCloneNode.setAttribute("leido", "0");
		                oCloneNode.setAttribute("bd", "I");
		                oCloneNode.style.cursor = "url(../../../images/imgManoMove.cur)";
		                oCloneNode.ondblclick = null;
		                switch ($I("cboFiguras").value) {
		                    case "P":
		                    case "A":
		                    case "S":
		                        oCloneNode.onclick = function() { ms(this); };
		                        break;
		                    case "T":
		                        oCloneNode.onclick = function() {
		                            ms(this);
		                            mostrarTramitados(this);
		                        };
		                        break;
		                    case "L":
		                    case "1":
		                    case "2":
		                    case "3":
		                    case "4":
		                    case "N":
		                        oCloneNode.onclick = function() {
		                            ms(this);
		                            mostrarSubIntegrantes(this, $I("cboFiguras").value);
		                            //mostrarCatalogoSubTime($I("cboFiguras").value);
		                        };
		                        break;
		                }
		                oCloneNode.insertCell(0);
		                oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));
		                var elemento = "";
		                elemento = oCloneNode.innerText + "#sCad#" + oCloneNode.getAttribute("sexo") + "#sCad#" + oCloneNode.getAttribute("tipo") + "#sCad#" + oCloneNode.getAttribute("baja");
		                //mfa(oCloneNode, "I");
		                switch ($I("cboFiguras").value) {
		                    case "P":
		                    case "A":
		                    case "S":
		                    case "L":
		                        js_Grabar[js_Grabar.length] = { tipo: $I("cboFiguras").value, accion: "I", idFicepi: oCloneNode.id, nombreIntegrante: elemento, accionElemento: "", idElemento: "", nombreElemento: "" };
		                        activarGrabar();
		                        //break;                            
		                    case "T":
		                    case "1":
		                    case "2":
		                    case "3":
		                    case "4":
		                    case "N":
		                        js_Integrantes[js_Integrantes.length] = { tipo: $I("cboFiguras").value, accion: "I", idFicepi: oCloneNode.id, nombreIntegrante: elemento };
		                        break;
		                }
		                actualizarLupas("tblTitulo", "tblPersonas");
		            }
		            break;
			    case "divTramNodo":
			    case "ctl00_CPHC_divTramNodo":
			        var filaPadre = buscarFilaSelecPadre();
			        if (FromTable == null || ToTable == null) continue;
			        if (oRow.parentNode.parentNode.id != "tblPersonas2") continue;
			        //var oTable = oTarget.getElementsByTagName("table")[0];
			        var sw = 0;
			        //Controlar que el elemento a insertar no existe en la tabla
			        for (var i = 0; i < oTable.rows.length; i++) {
			            if (oTable.rows[i].id == oRow.id) {
			                sw = 1;
			                break;
			            }
			        }
			        if (sw == 0) {
			            var NewRow;
			            if (nIndiceInsert == null) {
			                nIndiceInsert = oTable.rows.length;
			                NewRow = oTable.insertRow(nIndiceInsert);
			            }
			            else {
			                if (nIndiceInsert > oTable.rows.length)
			                    nIndiceInsert = oTable.rows.length;
			                NewRow = oTable.insertRow(nIndiceInsert);
			            }
			            nIndiceInsert++;
			            var oCloneNode = oRow.cloneNode(true);
			            oCloneNode.className = "";
			            oCloneNode.setAttribute("bd" ,"I");
                        NewRow.swapNode(oCloneNode);		                
			            oCloneNode.ondblclick = null;
			            if (oCloneNode.cells[0].children[0] != null) oCloneNode.insertCell(0);

			            oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));
			            if ($I("cboFiguras").value == "T") {
			                oCloneNode.cells[1].style.display = "block";
			                oCloneNode.cells[1].style.width = "20px";
			            }
			            else {
			                oCloneNode.insertCell(1);
			                //			                oCloneNode.cells[1].style.display = "block";
			                //			                oCloneNode.cells[1].style.width = "340px";
			                //typeof(oFilaARestaurar.onclick) != "undefined
			            }
			            //var filaPadre = buscarFilaSelecPadre();
			            var existe = false; //Variable creada para la comprobación de si existe un elemento en js_Grabar
			            var existeNuevo = false; //Variable creada para la comprobación de si existe un elemento en js_Integrantes
			            var elemento = "";
			            if ($I("cboFiguras").value == "T") elemento = oCloneNode.innerText + "#sCad#" + oCloneNode.getAttribute("sexo") + "#sCad#" + oCloneNode.getAttribute("tipo") + "#sCad#" + oCloneNode.getAttribute("baja");
			            else if ($I("cboFiguras").value == "L") elemento = oCloneNode.innerText + "#sCad#" + oCloneNode.getAttribute("of");
			            else elemento = oCloneNode.innerText;
			            ///////// Se realiza esta serie de comprobaciones para no grabar aquel integrante que no tenga subnodos.
			            for (var i = 0, nCountLoop = js_Integrantes.length; i < nCountLoop; i++) {
			                if (filaPadre[0] == js_Integrantes[i].idFicepi && js_Integrantes[i].accion == "I") {
			                    existeNuevo = true;
			                    break;
			                }
			            }
			            if (existeNuevo) {
			                for (var i = 0, nCountLoop = js_Grabar.length; i < nCountLoop; i++) {
			                    if (filaPadre[0] == js_Grabar[i].idFicepi) {
			                        existe = true;
			                        break;
			                    }
			                }
			                if (existe) {
			                    js_Grabar[js_Grabar.length] = { tipo: $I("cboFiguras").value, accion: "", idFicepi: filaPadre[0], nombreIntegrante: filaPadre[2], accionElemento: "I", idElemento: oCloneNode.id, nombreElemento: elemento };
			                }
			                else {
			                    js_Grabar[js_Grabar.length] = { tipo: $I("cboFiguras").value, accion: "I", idFicepi: filaPadre[0], nombreIntegrante: filaPadre[2], accionElemento: "I", idElemento: oCloneNode.id, nombreElemento: elemento };
			                }
			            }
			            else {
			                js_Grabar[js_Grabar.length] = { tipo: $I("cboFiguras").value, accion: "", idFicepi: filaPadre[0], nombreIntegrante: filaPadre[2], accionElemento: "I", idElemento: oCloneNode.id, nombreElemento: elemento };
			            }

			            //actualizarLupas("tblTitulo3", "tblPersonas2");
			            activarGrabar();
			        }
			        break;
			}
		}
        //bCambios = true;
	}
	oTable = null;
	killTimer();
	CancelDrag();
	
	obj.style.display	= "none";
	oEl					= null;
	aEl.length = 0;
	oTarget				= null;
	beginDrag			= false;
	TimerID				= 0;
	oRow                = null;
    FromTable           = null;
    ToTable             = null;
}

function buscarFilaSelecPadre(){
    var aFilas = $I("tblIntegrantes").rows;
    var aRetorno = new Array(); 
    for (var x=0, nCount=aFilas.length; x<nCount; x++){
        if (aFilas[x].className == "FS"){
            aRetorno[0] = aFilas[x].id;
            aRetorno[1] = aFilas[x].getAttribute("bd");
            aRetorno[2] = aFilas[x].innerText + "#sCad#" + aFilas[x].getAttribute("sexo") + "#sCad#" + aFilas[x].getAttribute("tipo") + "#sCad#" + aFilas[x].getAttribute("baja");
            return aRetorno;
        }    
    }
}

function reConvocarIntegrantes(idUsuario, strUsuario, sexo, tipo, baja, bd){
    try{
        var iFilaNueva = 0;
        var sNombreAct;
        
        var aFilas = FilasDe("tblIntegrantes");
        oNF = tblIntegrantes.insertRow(-1);
        oNF.style.height = "20px";
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd", bd);
	    oNF.style.cursor = "url(../../../images/imgManoMove.cur)";
	    oNF.setAttribute("sw", 1);
	    for (var i = 0, nCount = js_Grabar.length; i < nCount; i++) {
	        if (js_Grabar[i].idFicepi == idUsuario) {
	            oNF.setAttribute("leido", "1");
	            break;
	        }
	        oNF.setAttribute("leido","0");
	    }
	    switch ($I("cboFiguras").value) {
	        case "P":
            case "A":
            case "S":
                oNF.onclick = function (){ms(this);};
                break;
            case "T":
                oNF.onclick = function (){ms(this);
                                        mostrarTramitados(this);};
                break;
            case "L": 
            case "1":
            case "2":
            case "3":
            case "4":
            case "N":
                oNF.onclick = function (){ms(this);
                                         mostrarSubIntegrantes(this,$I("cboFiguras").value);
                                         //mostrarCatalogoSubTime($I("cboFiguras").value);
                                         };
                break;           
        }
	    oNF.onmousedown = function (e){DD(e);};
    	oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));    	
	    oNC2 = oNF.insertCell(-1);
        oNC2.style.width = "20px";
        if (sexo == "V"){
            switch (tipo){
                case "B": oNC2.appendChild(oImgNV.cloneNode(true), null); break;
                case "G": oNC2.appendChild(oImgGV.cloneNode(true), null); break;
                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                case "T": oNC2.appendChild(oImgTV.cloneNode(true), null); break;
            }
        }
        else{
            switch (tipo){
                case "B": oNC2.appendChild(oImgNM.cloneNode(true), null); break;
                case "G": oNC2.appendChild(oImgGM.cloneNode(true), null); break;
                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                case "T": oNC2.appendChild(oImgTM.cloneNode(true), null); break;
            }
        }
        oNC3 = oNF.insertCell(-1);
        oNC3.style.width = "330px";
        oNC3.innerText = strUsuario;
        mfa(oNF, bd);
        actualizarLupas("tblTitulo", "tblPersonas");
	}catch(e){		
        mostrarErrorAplicacion("Error al mostrar integrante", e.message);;
    }
}

function PonerMM(oFila) {
    oFila.style.cursor = "url(../../../images/imgManoMove.cur)";
}

function reConvocar(idUsuario, strUsuario, sexo, tipo, baja, bd, iTipo){
    try{
        var iFilaNueva = 0;
        var sNombreAct;

        var aFilas = FilasDe("tblIntegrantes2");
        if ($I("cboFiguras").value == "T"){
            $I("colHidden").style.display = "block";
            $I("colHidden").style.width = "20px";
        }
        else $I("colHidden").style.display = "none";
        for (var i=0, nCountLoop=aFilas.length ; i<nCountLoop; i++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFilas[i].cells[1].innerText;
            iFilaNueva = i;
            if (sNombreAct > strUsuario) break;
        }
        oNF = tblIntegrantes2.insertRow(iFilaNueva);
        oNF.style.height = "20px";
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd", bd);
	    oNF.setAttribute("sw", 1);
	    if ($I("cboFiguras").value == "L" && tipo == "1") oNF.style.backgroundColor = "#e5e5e5";
	    else {
	        oNF.style.cursor = "url(../../../images/imgManoMove.cur)";
	        oNF.onclick = function() {
	            ms(this);
	            PonerMM(this);
	        };
	        oNF.onmousedown = function(e) { DD(e); }
	    }
            	
    	oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
        if(iTipo == 1 || iTipo == 2){
    	    oNC2 = oNF.insertCell(-1);
	        oNC2.style.width = "20px";
    	    
	        if (sexo == "V"){
                switch (tipo){
                    case "B": oNC2.appendChild(oImgNV.cloneNode(true), null); break;
                    case "G": oNC2.appendChild(oImgGV.cloneNode(true), null); break;
                    case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                    case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                    case "T": oNC2.appendChild(oImgTV.cloneNode(true), null); break;
                }
            }
            else{
                switch (tipo){
                    case "B": oNC2.appendChild(oImgNM.cloneNode(true), null); break;
                    case "G": oNC2.appendChild(oImgGM.cloneNode(true), null); break;
                    case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                    case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                    case "T": oNC2.appendChild(oImgTM.cloneNode(true), null); break;
                }
            }
            oNC3 = oNF.insertCell(-1);
	        oNC3.style.width = "330px";
	        oNC3.innerText = strUsuario;
        }
        else{
            oNC2 = oNF.insertCell(-1);
            oNC2.setAttribute("style", "width:1px;display:none");   
            oNC3 = oNF.insertCell(-1);
            oNC3.style.width = "350px";
            oNC3.innerText = strUsuario;
        }
        mfa(oNF, bd);
        switch (iTipo){
            case 1:
                actualizarLupas("tblTitulo", "tblPersonas");
                break;
            case 2:
            case 3:
	            //actualizarLupas("tblTitulo3", "tblPersonas2");
                break;
        }
	}catch(e){		
		switch (iTipo){
            case 1:
            case 2:
                mostrarErrorAplicacion("Error al mostrar integrante", e.message);;
                break;
            case 3:
                if($I("cboFiguras").value != "L") mostrarErrorAplicacion("Error al agregar elementos", e.message);
                else mostrarErrorAplicacion("Error al agregar la oficina", e.message);	            
                break;
        }
    }
}


function estaEnLista(idUsuario, iTipo){
    try{
        switch(iTipo){
            case 1:
                var aFilas = FilasDe("tblIntegrantes");
                break;
            case 2:
            case 3:
                var aFilas = FilasDe("tblIntegrantes2");
                break;        
        }
	    if (aFilas != null){
	        if (aFilas.length > 0) {
	            for (var i = 0; i < aFilas.length; i++) {
	                if (aFilas[i].id == idUsuario) {
	                    //if (iTipo == 3 && $I("cboFiguras").value == "L" && aFilas[i].of == "0")
	                    return true;
	                }
	            }
	        }
	    }
		return false;
	}catch(e){
	    switch(iTipo){
            case 1:
            case 2:
                mostrarErrorAplicacion("Error al comprobar si el integrante está en la lista", e.message);
                break;
            case 3:
                mostrarErrorAplicacion("Error al comprobar si el elemento está en la lista", e.message);
                break;        
        }
    }
}

function convocar(idUsuario, strUsuario, sexo, tipo, baja, iTipo){
    try{
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;
        
         switch (iTipo){
            case 1:
                var oTabla = tblPersonas;
                var oTablaDe = tblIntegrantes;
                break;
            case 2:
            case 3:
                var oTabla = tblPersonas2;
                var oTablaDe = tblIntegrantes2;
                break;
        }

        if (iFila >= 0) modoControles(oTabla.rows[iFila], false);
	    sNombreNuevo = strUsuario;
	    var aFilas = FilasDe(oTablaDe.id);
        for (iFilaNueva=0; iFilaNueva<aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFilas[iFilaNueva].cells[1].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = oTablaDe.insertRow(iFilaNueva);
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd", "I");
	    oNF.setAttribute("leido","0");
	    if(iTipo == 1 || iTipo == 2){
            oNF.setAttribute("sexo", sexo);
	        oNF.setAttribute("tipo", tipo);
	        oNF.setAttribute("baja", baja);
	    }
	    oNF.setAttribute("style","height:20px; cursor: url(../../../images/imgManoMove.cur),pointer");
	    oNF.setAttribute("sw", 1);
        switch ($I("cboFiguras").value) {
            case "P":
            case "A":
            case "S":
                oNF.onclick = function() { ms(this); };
                break;
            case "T":
                if (iTipo == 1) {
                    oNF.onclick = function() {
                        ms(this);
                        mostrarTramitados(this);
                    };
                }
                else {
                    oNF.onclick = function() {
                        ms(this);
                    };
                }
                break;
            case "L":
                oNF.setAttribute("of", "0");
            case "1":
            case "2":
            case "3":
            case "4":
            case "N":
                if (iTipo != 3) {
                    oNF.onclick = function() {
                        ms(this);
                        mostrarSubIntegrantes(this, $I("cboFiguras").value);
                        //mostrarCatalogoSubTime($I("cboFiguras").value);
                    };
                }
                else {
                    oNF.onclick = function() {
                        ms(this);
                    };
                }
                break;
        }
	    oNF.onmousedown = function (e){DD(e)};
    	
        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        if(iTipo == 1 || iTipo == 2){
    	    oNC2 = oNF.insertCell(-1);
	        oNC2.style.width = "20px";
    	    
	        if (sexo == "V"){
                switch (tipo){
                    case "B": oNC2.appendChild(oImgNV.cloneNode(true), null); break;
                    case "G": oNC2.appendChild(oImgGV.cloneNode(true), null); break;
                    case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                    case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                    case "T": oNC2.appendChild(oImgTV.cloneNode(true), null); break;
                }
            }
            else{
                switch (tipo){
                    case "B": oNC2.appendChild(oImgNM.cloneNode(true), null); break;
                    case "G": oNC2.appendChild(oImgGM.cloneNode(true), null); break;
                    case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                    case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                    case "T": oNC2.appendChild(oImgTM.cloneNode(true), null); break;
                }
            }
            oNC3 = oNF.insertCell(-1);
	        oNC3.style.width = "330px";
	        oNC3.innerText = strUsuario;
        }
        else {
            oNC2 = oNF.insertCell(-1);
	        oNC3 = oNF.insertCell(-1);
            oNC3.style.width = "350px";
            oNC3.innerText = strUsuario;
        }
        var elemento = "";
        switch (iTipo){
            case 1:
                actualizarLupas("tblTitulo", "tblPersonas");
                elemento = strUsuario + "#sCad#" + oNF.getAttribute("sexo") + "#sCad#" + oNF.getAttribute("tipo") + "#sCad#" + oNF.getAttribute("baja");
                switch ($I("cboFiguras").value) {
                    case "P":
                    case "A":
                    case "S":
                    case "L":
                        js_Grabar[js_Grabar.length] = {tipo:$I("cboFiguras").value, accion:"I", idFicepi:idUsuario, nombreIntegrante:elemento, accionElemento:"", idElemento:"", nombreElemento:""}
                        activarGrabar();
                        //break;                    
                    case "T":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "N":
                        js_Integrantes[js_Integrantes.length] = {tipo:$I("cboFiguras").value, accion:"I", idFicepi:idUsuario, nombreIntegrante:elemento}
                        //activarGrabar();
                        break;
                }
                break;
            case 2:
            case 3:
                var filaPadre = buscarFilaSelecPadre();
                var existe = false;//Variable creada para la comprobación de si existe un elemento en js_Grabar
                var existeNuevo = false;//Variable creada para la comprobación de si existe un elemento en js_Integrantes
                var elemento = "";
                if($I("cboFiguras").value == "T") elemento = oNF.innerText + "#sCad#" + oNF.getAttribute("sexo") + "#sCad#" + oNF.getAttribute("tipo") + "#sCad#" + oNF.getAttribute("baja");
                else if ($I("cboFiguras").value == "L") elemento = oNF.innerText + "#sCad#" + oNF.getAttribute("of");
                     else elemento = oNF.innerText;
                for(var i=0, nCountLoop=js_Integrantes.length; i<nCountLoop; i++){
                    if(filaPadre[0] == js_Integrantes[i].idFicepi && js_Integrantes[i].accion == "I"){
                        existeNuevo = true;
                        break;
                    }                        
                }
                if (existeNuevo){
                    for(var i=0, nCountLoop=js_Grabar.length; i<nCountLoop; i++){
                        if(filaPadre[0] == js_Grabar[i].idFicepi){
                            existe = true;
                            break;
                        }                        
                    } 
                    if(existe){
                        js_Grabar[js_Grabar.length] = {tipo:$I("cboFiguras").value, accion:"", idFicepi:filaPadre[0], nombreIntegrante:filaPadre[2], accionElemento:"I", idElemento:idUsuario, nombreElemento:elemento};
                    } 
                    else{
                        js_Grabar[js_Grabar.length] = {tipo:$I("cboFiguras").value, accion:"I", idFicepi:filaPadre[0], nombreIntegrante:filaPadre[2], accionElemento:"I", idElemento:idUsuario, nombreElemento:elemento};
                    }
                }
                else{
                    js_Grabar[js_Grabar.length] = {tipo:$I("cboFiguras").value, accion:"", idFicepi:filaPadre[0], nombreIntegrante:filaPadre[2], accionElemento:"I", idElemento:idUsuario, nombreElemento:elemento};
                }
	            //actualizarLupas("tblTitulo3", "tblPersonas2");
	            activarGrabar();
                break;
        }   

	}catch(e){		
		switch (iTipo){
            case 1:
            case 2:
                mostrarErrorAplicacion("Error al agregar integrante", e.message);
                break;
            case 3:
                if($I("cboFiguras").value != "L") mostrarErrorAplicacion("Error al agregar elementos", e.message);
                else mostrarErrorAplicacion("Error al agregar la oficina", e.message);	            
                break;
        }
    }
}

function comprobarDatos() {
    if (js_Grabar.length < 1) return false;
    if ($I("cboFiguras").value != "P" && $I("cboFiguras").value != "A" && $I("cboFiguras").value != "S" && $I("cboFiguras").value != "L") {
        var aFila = FilasDe("tblIntegrantes");
        var existe = false;
        var fin = false;
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                existe = false
                for (var j = 0, nCountJ = js_Grabar.length; j < nCountJ; j++) {
                    if (aFila[i].getAttribute("bd") == "I" && js_Grabar[j].idFicepi == aFila[i].id) {
                        existe = true;
                        break;
                    }
                }
                if (aFila[i].getAttribute("bd") == "I" && !existe) {
                    fin = true;
                    if (ie) aFila[i].click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        aFila[i].dispatchEvent(clickEvent);
                    }
                    break;
                }
            }
            if (fin) {
                switch ($I("cboFiguras").value) {
                    //                case "L": //Aceptadores  
                    //                    mmoff("Existen aceptadores sin oficina asignada.", 300);  
                    //                    break;  
                    case "T": //Tramitadores
                        mmoff("War", "Existen tramitadores sin benificiarios asignados.", 325);
                        break;
                    case "N":
                        mmoff("War", "Existen profesionales sin integrante de " + strEstructuraNODO, 325);
                        break;
                    case "1":
                        mmoff("War", "Existen profesionales sin integrante de " + strEstructuraSUPERNODO1, 350);
                        break;
                    case "2":
                        mmoff("War", "Existen profesionales sin integrante de " + strEstructuraSUPERNODO2, 350);
                        break;
                    case "3":
                        mmoff("War", "Existen profesionales sin integrante de " + strEstructuraSUPERNODO3, 350);
                        break;
                    case "4":
                        mmoff("War", "Existen profesionales sin integrante de " + strEstructuraSUPERNODO4, 350);
                        break;
                }
                return false;
            }
        }
        else return false;
    }
    return true;//mmoff("Existen integrantes ", 200);
}

function grabar(){
    try {
        if (!comprobarDatos()) return;        
        js_args = "grabar@#@";
        for (var nIndice = 0; nIndice < js_Grabar.length; nIndice++) {
            if (js_Grabar[nIndice].accion == "I" || js_Grabar[nIndice].accion == "D"
                 || js_Grabar[nIndice].accionElemento == "I" || js_Grabar[nIndice].accionElemento == "D") {
                js_args += js_Grabar[nIndice].tipo + "#sCad#";
                js_args += js_Grabar[nIndice].accion + "#sCad#";
                js_args += js_Grabar[nIndice].idFicepi + "#sCad#";
                js_args += js_Grabar[nIndice].accionElemento + "#sCad#";
                js_args += js_Grabar[nIndice].idElemento + "#sFin#";
            }
        }
        if (js_args != "grabar@#@") js_args = js_args.substring(0, js_args.length - 6);
        else {
            var aFila = FilasDe("tblIntegrantes");
            if (aFila != null) {
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "U")
                        mfa(aFila[i], "N");
                }
            }
            aFila = null;
            aFila = FilasDe("tblIntegrantes2");
            if (aFila != null) {
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "U")
                        mfa(aFila[i], "N");
                }
            }
            desActivarGrabar();
            actualizarArray(3)
            bCambios = false;
            return;
        }
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;

function scrollTablaProf(){
    try{
        if ($I("divPersonas").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divPersonas").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        if ($I("divPersonas").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divPersonas").offsetHeight / 20 + 1, $I("tblPersonas").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divPersonas").innerHeight / 20 + 1, $I("tblPersonas").rows.length);

        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblPersonas.rows[i].getAttribute("sw")){
                oFila = tblPersonas.rows[i];
                oFila.setAttribute("sw",1);
                
                if (oFila.getAttribute("sexo") == "V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTM.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de integrantes.", e.message);
    }
}


var nTopScrollProf = -1;
var nIDTimeProf = 0;

function scrollTablaProfAsig(){
    try{
        if ($I("divIntegrantes").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divIntegrantes").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        if ($I("divIntegrantes").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divIntegrantes").offsetHeight / 20 + 1, $I("tblIntegrantes").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divIntegrantes").innerHeight / 20 + 1, $I("tblIntegrantes").rows.length);

        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblIntegrantes.rows[i].getAttribute("sw")){
                oFila = tblIntegrantes.rows[i];
                oFila.setAttribute("sw",1);
                
               if (oFila.getAttribute("sexo") == "V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTM.cloneNode(true), null); break;
                    }
                }  
                
                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[1].children[0], 30);
            }          
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de personas.", e.message);
    }
}

function scrollTablaProf2(){
    try{
        if ($I("divPersonas2").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divPersonas2").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf2()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        
        if ($I("divPersonas2").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divPersonas2").offsetHeight / 20 + 1, $I("tblPersonas2").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divPersonas2").innerHeight / 20 + 1, $I("tblPersonas2").rows.length);
      
        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblPersonas2.rows[i].getAttribute("sw")){
                oFila = tblPersonas2.rows[i];
                oFila.setAttribute("sw",1);
                
                if (oFila.getAttribute("sexo") == "V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTM.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla buscador2.", e.message);
    }
}


var nTopScrollProf = -1;
var nIDTimeProf = 0;

function scrollTablaProfAsig2(){
    try{
        if ($I("divTramNodo").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divTramNodo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProfAsig2()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        if ($I("divTramNodo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divTramNodo").offsetHeight / 20 + 1, $I("tblIntegrantes2").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divTramNodo").innerHeight / 20 + 1, $I("tblIntegrantes2").rows.length);
      
        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblIntegrantes2.rows[i].getAttribute("sw")){
                oFila = tblIntegrantes2.rows[i];
                oFila.setAttribute("sw",1);
                
               if (oFila.getAttribute("sexo") == "V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTM.cloneNode(true), null); break;
                    }
                }  
                
                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[1].children[0], 30);
            }          
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de integrantes2.", e.message);
    }
}