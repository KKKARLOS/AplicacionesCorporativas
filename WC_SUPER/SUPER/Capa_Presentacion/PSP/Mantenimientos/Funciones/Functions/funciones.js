var indiceFila = 0;
var aFila;
var sValorNodo="";
function init(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }
        else
        {
            sValorNodo = $I("cboCR").value;   	    
            $I("lblNodo").className = "texto";
        }
        aFila = FilasDe("tblDatos");
        indiceFila = aFila.length;
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
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
                break;
            case "grabar":
                var aFila = FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                indiceFila = aFila.length;
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;
        }
        ocultarProcesando();
    }
}
function comprobarDatos(){
    try{
        var aFila = FilasDe("tblDatos");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != "D") {
                if (aFila[i].cells[1].children[0].value == ""){
                    mmoff("War", "Debes indicar la descripción de la función", 270);
                    ms(aFila[i]);
                    aFila[i].cells[1].children[0].focus();
                    return false;
                }
            }
            var strDesc = aFila[i].cells[1].children[0].value;
            for (var x=0;x<aFila.length;x++){
                if (aFila[i].getAttribute("bd") != "D" && aFila[x].getAttribute("bd") != "D" && i != x) {
                    if (strDesc == aFila[x].cells[1].children[0].value){
                        mmoff("War", "No se permiten dos o más funciones con la misma descripción", 350);
                        ms(aFila[i]);
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
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        if (!comprobarDatos()) return;
        var js_args = "grabar@#@"+sValorNodo+"@#@";

        var sw = 0;
        var aFila = FilasDe("tblDatos");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != "") {
                sw = 1;
                js_args += aFila[i].getAttribute("bd") + "##"; //Opcion BD. "I", "U", "D"
                js_args += aFila[i].id +"##"; //ID Funcion
                js_args += Utilidades.escape(aFila[i].cells[1].children[0].value) +"///"; //Descripcion
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

function nuevo(){
    try{
        if (sValorNodo=="" || sValorNodo=="-1")
            mmoff("Inf", "Debes seleccionar un " + $I("lblNodo").innerText, 330);
        else{
            
            oNF = $I("tblDatos").insertRow(-1);
            var iFila=oNF.rowIndex;
            
            oNF.id = indiceFila;
            oNF.setAttribute("bd", "I");
            oNF.style.height = "20px";
            oNF.attachEvent('onclick', mm);
            oNF.onkeyup = function (){mfa(this,'U');};

            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
            
            var oCtrl1 = document.createElement("input");
            oCtrl1.setAttribute("id", "txtFun" + indiceFila);
            oCtrl1.setAttribute("style", "width:380px");
            oCtrl1.setAttribute("className", "txtL");
            oCtrl1.className="txtL";
            oCtrl1.setAttribute("maxLength", "40");
            oNF.insertCell(-1).appendChild(oCtrl1);            
//          oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='txtFun" + indiceFila + "' class='txtL' style='width:380px' value='' maxlength='40'>"));

            ms(oNF);
	        oNF.cells[1].children[0].focus();
            indiceFila++;
            activarGrabar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}

function eliminar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        var sw = 0;
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("bd") == "I") {
                    //Si es una fila nueva, se elimina
                    $I("tblDatos").deleteRow(i);
                }    
                else{
                    mfa(aFila[i],"D");
                }
                sw = 1;                
            }
        }
        if (sw == 1) activarGrabar();
        else mmoff("Inf", "Debes seleccionar la fila a eliminar", 250);
        iFila = -1;
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function buscar(){
    try{
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
