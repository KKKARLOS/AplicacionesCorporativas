var sValorNodo="";
function init(){
    try{
        ToolTipBotonera("nuevo", "Añadir función a profesional");
        ToolTipBotonera("eliminar","Eliminar la función seleccionada del profesional seleccionado");
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }
        else{
            sValorNodo = $I("cboCR").value;   	    
            $I("lblNodo").className = "texto";
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").innerHTML = aResul[2];
                recargarTecnicos(aResul[3]);
                break;
            case "grabar":
                desActivarGrabar();
                for (var i=0;i<aTecnicos.length;i++) aTecnicos[i].opcionBD = "";
                mmoff("Suc", "Grabación correcta", 160);
                break;
        }
        ocultarProcesando();
    }
}
function grabar(){
    try{
        var js_args = "grabar@#@";

        var sw = 0;
        for (var i = 0; i < aTecnicos.length; i++){
            if (aTecnicos[i].opcionBD != ""){
                sw = 1;
                js_args += aTecnicos[i].opcionBD +"##"; 
                js_args += aTecnicos[i].idFuncion +"##"; 
                js_args += aTecnicos[i].idTecnico +"///"; 
            }
        }
        if (sw == 1) js_args = js_args.substring(0, js_args.length-3);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

function mostrarTecnicos(idFuncion){
    try{
        nIDFun = idFuncion;
        //1º borrar filas de las tablas de técnicos y funciones por técnico.
        var aFila = FilasDe("tblTec");
        for (var i=aFila.length-1;i>=0;i--) $I("tblTec").deleteRow(i);
        aFila = FilasDe("tblFunTec");
        for (var i = aFila.length - 1; i >= 0; i--) $I("tblFunTec").deleteRow(i);

        //2º insertar los ligados a la función seleccionada.
        for (var i = 0; i < aTecnicos.length; i++){
            if (aTecnicos[i].idFuncion == idFuncion && aTecnicos[i].opcionBD != "D"){
                insertarTecnico(aTecnicos[i].idTecnico, aTecnicos[i].nombre);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los profesionales", e.message);
    }
}

function insertarTecnico(idTecnico, nombre){
    try{
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

	    sNombreNuevo = nombre;
	    var aFilas = $I("tblTec").rows;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblTec").insertRow(iFilaNueva);
        oNF.style.height = "16px";
        oNF.title=idTecnico + " - " + nombre;
        
        var iFila=oNF.rowIndex;

        oNF.id = idTecnico;
        oNF.onclick = function() { ms(this); }
        oNF.onclick = function (){mostrarFunciones(this.id);};

        oNC1 = oNF.insertCell(-1);
        oNC1.innerText = nombre;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una fila en la tabla de profesionales", e.message);
    }
}

function mostrarFunciones(idTecnico){
    try{
        nIDTec = idTecnico;
        //1º borrar filas de la tabla de funciones por técnico.
        aFila = FilasDe("tblFunTec");
        for (var i=aFila.length-1;i>=0;i--) tblFunTec.deleteRow(i);

        //2º insertar los ligados a la función seleccionada.
        for (var i = 0; i < aTecnicos.length; i++){
            if (aTecnicos[i].idTecnico == idTecnico && aTecnicos[i].opcionBD != "D"){
                insertarFuncion(aTecnicos[i].idFuncion);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los profesionales", e.message);
    }
}

function insertarFuncion(idFuncion){
    try{
        oNF = $I("tblFunTec").insertRow(-1);
        var iFila=oNF.rowIndex;

        oNF.id = idFuncion;
        oNF.style.height = "16px";
        
        oNC1 = oNF.insertCell(-1);
        var aFila = FilasDe("tblFun");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].id == idFuncion){
                oNC1.innerText = aFila[i].innerText;
                break;
            }
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una fila en la tabla de profesionales", e.message);
    }
}

function eliminar(){
    try{
        var sw = 0;
        //1º indicar el borrado en el array de FuncionTecnico
        for (var i = 0; i < aTecnicos.length; i++){
            if (aTecnicos[i].idFuncion == nIDFun && aTecnicos[i].idTecnico == nIDTec){
                aTecnicos[i].opcionBD = "D";
                sw = 1;
                break;
            }
        }
        //2º eliminar la fila de la tabla funciones por técnico
        var aFila = FilasDe("tblFunTec");
        var nFilas = aFila.length;
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].id == nIDFun){
                tblFunTec.deleteRow(i);
                nFilas--;
            }
        }
        //3º si el técnico no tiene ninguna función, borrar la fila del técnico
        if (nFilas == 0){
            aFila = FilasDe("tblTec");
            for (var i=0;i<aFila.length;i++){
                if (aFila[i].id == nIDTec){
                    tblTec.deleteRow(i);
                }
            }
        }
        if (sw == 1)
            activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}

function nuevo(){
    try{
        if (nIDFun == 0){
            mmoff("Inf", "Para añadir nuevos profesionales, antes debes seleccionar una función", 425);
            return;
        }
        if (sValorNodo=="" || sValorNodo=="-1"){
            mmoff("Inf", "Debes seleccionar un " + $I("lblNodo").innerText, 330);
            return;
        }
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerProfesional.aspx";
        modalDialog.Show(strEnlace, self, sSize(450, 470))
            .then(function(ret) {
                if (ret != null && ret != "") {
                    var aOpciones = ret.split("///");
                    for (var i = 0; i < aOpciones.length; i++) {
                        var aDatos = aOpciones[i].split("@#@");
                        //1º Comprobar que el usuario no está en el array,
                        // si estuviera como "" o "I", no hacer nada, 
                        // si estuviera como "D" mostrarlo y ponerlo como "".
                        var sw1 = 0;
                        var sw2 = 0;
                        for (var x = 0; x < aTecnicos.length; x++) {
                            if (aTecnicos[x].idFuncion == nIDFun && aTecnicos[x].idTecnico == aDatos[0]) {
                                sw1 = 1;
                                if (aTecnicos[x].opcionBD == "D") {
                                    sw2 = 1;
                                    aTecnicos[x].opcionBD = "";
                                    insertarTecnico(aDatos[0], aDatos[2]);
                                }
                                break;
                            }
                        }
                        if (sw1 == 0) {
                            sw2 = 1;
                            insertarTecnico(aDatos[0], aDatos[2]);
                            insertarTecnicoEnArray("I", nIDFun, aDatos[0], aDatos[2]);
                        }
                    }
                    if (sw2 == 1)
                        activarGrabar();
                }
            });
        window.focus();
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevos profesionales", e.message);
    }
}

//function mostrarProfesional(){
//    var sAp1 = Utilidades.escape($I("txtApellido1").value);
//    var sAp2 = Utilidades.escape($I("txtApellido2").value);
//    var sNombre = Utilidades.escape($I("txtNombre").value);

//	if (sAp1 == "" && sAp2 == "" && sNombre == "") return;

//	$I("procesando").style.visibility = "visible";
//	setTimeout("mostrarProfesionalAux('"+sAp1+"','"+sAp2+"','"+sNombre+"')",30);
//}

//function mostrarProfesionalAux(sAp1, sAp2, sNombre){
//	strUrl = document.location.toString();

//	intPos = strUrl.indexOf("Default.aspx");
//	strUrlPag = strUrl.substring(0,intPos)+"../../obtenerDatos.aspx";
//	strUrlPag += "?intOpcion=2";
//	strUrlPag += "&sAp1="+ Utilidades.escape(sAp1);
//	strUrlPag += "&sAp2="+ Utilidades.escape(sAp2);
//	strUrlPag += "&sNombre="+ Utilidades.escape(sNombre);

//	var strTable = Utilidades.unescape(sendHttp(strUrlPag));
//	$I("divCatalogo").children[0].innerHTML = strTable;
//	$I("procesando").style.visibility = "hidden";
//}
function recargarTecnicos(sLista){
try{
    //aTecnicos="";
    aTecnicos.length=0;
    var aLista = sLista.split("///");
    for (var i=0;i<aLista.length;i++){
        if (aLista[i] != ""){
            var aTec = aLista[i].split("##");
            insertarTecnicoEnArray("N", aTec[0], aTec[1], aTec[2]);
        }
    }
	}catch(e){
		mostrarErrorAplicacion("Error al recargar profesionales", e.message);
    }
}
function buscar(){
    try{
        BorrarFilasDe("tblTec");
        BorrarFilasDe("tblFunTec");
        var js_args = "buscar@#@"+ sValorNodo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener funciones del nodo", e.message);
    }
}
function setCombo(){
    try{
        $I("divCatalogo").innerHTML = "";
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener grupos funcionales del nodo", e.message);
    }
}

function getNodo(){
    try{
        if ($I("lblNodo").className == "texto") return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 460))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    setCombo();
                }
            });
        window.focus();

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo ", e.message);
    }
}
