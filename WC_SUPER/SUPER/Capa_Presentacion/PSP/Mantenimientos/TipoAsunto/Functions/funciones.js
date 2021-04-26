var aFila;
function init(){
    try{
        aFila = FilasDe("tblDatos");
        iFila = aFila.length - 1;
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
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar el tipo de asunto ya que existen asuntos que lo tienen asignado.");
//            var aFila = FilasDe("tblDatos");
//            for (var i=aFila.length-1; i>=0; i--){
//                if (aFila[i].bd == "D" && aFila[i].id != "") {
//                    aFila[i].bd="U";
//                    aFila[i].style.display = "";
//                }
//            }
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getTipos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                break;
            case "grabar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                for (var j=0;i<aFila.length;i++){
                    aFila[j].setAttribute("orden",j);
                }
                iFila = aFila.length;
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();
                break;
        }
        ocultarProcesando();
    }
}
function comprobarDatos(){
    try{
        aFila = FilasDe("tblDatos");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != "D") {
                if (aFila[i].cells[1].children[0].value == ""){
                    mmoff("Inf","Debes indicar la denominación del tipo de asunto",330);
                    ms(aFila[i]);
                    aFila[i].cells[1].children[0].focus();
                    return false;
                }
            }
            var strDesc = aFila[i].cells[1].children[0].value;
            for (var x=0;x<aFila.length;x++){
                if (aFila[i].getAttribute("bd") != "D" && aFila[x].getAttribute("bd") != "D" && i != x) {
                    if (strDesc == aFila[x].cells[1].children[0].value){
                        mmoff("No se permiten dos o más tipos de asunto con la misma descripción. ("+strDesc+")",600);
                        aFila[x].cells[1].children[0].focus();
                        return false;
                    }
                }
            }
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function grabar(){
    try{
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@", sDeletes="", sOtros="";

        var sw = 0;
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].getAttribute("bd") != "") {
                sw = 1;
                if (aFila[i].getAttribute("bd") == "D"){
                    sDeletes += aFila[i].getAttribute("bd") + "##"; //Opcion BD. "I", "U", "D"
                    sDeletes += aFila[i].id +"##"; //ID tipo asunto
                    sDeletes += Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"; //Descripcion
                    sDeletes += aFila[i].getAttribute("orden") + "///"; //Orden
                }
                else{
                    sOtros += aFila[i].getAttribute("bd") +"##"; //Opcion BD. "I", "U", "D"
                    sOtros += aFila[i].id +"##"; //tipo asunto
                    sOtros += Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"; //Descripcion
                    sOtros += aFila[i].getAttribute("orden") + "///"; //Orden
                }
            }
        }
        //Primero hago los borrados por si coinciden las denominaciones de algún borrado con alguna insert o update
        js_args+=sDeletes+sOtros;
        if (sw == 1) js_args = js_args.substring(0, js_args.length-3);
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

function nuevo(){
    try{
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);

        oNF = $I("tblDatos").insertRow(-1);
        iFila=oNF.rowIndex;

        oNF.id = iFila;

        oNF.setAttribute("bd", "I");
        oNF.setAttribute("orden", iFila);
        oNF.setAttribute("style", "height:20px");
        oNF.style.height="20px";
        //(typeof document.detachEvent != 'undefined') ? oNF.attachEvent('onclick', mm) : oNF.addEventListener('click', mm, false);
        oNF.attachEvent('onclick', mm);

        oNC1 = oNF.insertCell(-1);
        oNC1.width = "15px";
        oNC1.appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);
        var oCtrl1 = document.createElement("input");
        oCtrl1.id="txtFun" + iFila;
        oCtrl1.style.width = "400px";
        oCtrl1.className="txtL";
        oCtrl1.setAttribute("maxLength", "40");
        //oCtrl1.appendChild(document.createTextNode(oFila.cells[0].innerText.Trim()));
        oNC2.appendChild(oCtrl1);        
        
	    oNF.cells[1].children[0].focus();
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}

function eliminar(){
    try{
        var sw = 0;
        var sw2 = 0;
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw2 = 1;
                if (aFila[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    $I("tblDatos").deleteRow(i);
                }    
                else{
                    mfa(aFila[i],"D");
                    sw = 1;
                }
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("Inf", "Debes seleccionar la fila a eliminar", 250);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}

function ordenarTabla(nOrden,nAscDesc){
	buscar(nOrden,nAscDesc);
}

function buscar(nOrden,nAscDesc){
    try{
        var js_args = "getTipos@#@"+nOrden+"@#@"+nAscDesc;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordenar el catálogo de tipos de asunto", e.message);
    }
}
