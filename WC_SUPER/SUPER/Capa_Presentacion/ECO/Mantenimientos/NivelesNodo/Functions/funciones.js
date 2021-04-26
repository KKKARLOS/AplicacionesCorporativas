var tbody;
var aFila;
var bTarifaNodo = false;
var sValorNodo = "";

function init(){
    try{
        $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Mantenimientos &gt; Niveles por " + $I("lblNodo").innerText.toLowerCase();

        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }else 
        {
            sValorNodo = $I("cboNodo").value;   	    
            $I("lblNodo").className = "texto";
        }
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
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar el nivel '" + aResul[3] + "',\n ya que existen elementos con los que está relacionado.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                var sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                        continue;
                    }else if (aFila[i].getAttribute("bd") == "I"){
                        aFila[i].id = aEI[nIndiceEI]; 
                        nIndiceEI++;
                    }
                    mfa(aFila[i],"N");
                }
                for (var i=0;i<aFila.length;i++){
                    aFila[i].setAttribute("orden", i);
                }
                
                nFilaDesde = -1;
                nFilaHasta = -1;
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160); 
                //popupWinespopup_winLoad();
                
                if (bTarifaNodo){
                    bTarifaNodo = false;
                    setTimeout("getNodo();", 50);
                }
                break;
            case "getTarifas":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                tbody = document.getElementById('tbodyDatos'); 
                tbody.onmousedown = startDragIMG; 
                break;
        }
        ocultarProcesando();
    }
}


function nuevo() {
    try {
        if ($I("hdnIdNodo").value == "") return;
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        //oNF --> objeto nueva fila
        oNF = $I("tblDatos").insertRow(-1);
        oNF.id = oNF.rowIndex;
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.setAttribute("orden", oNF.rowIndex);
        //oNF.onclick = function(){mmse(this);};
        oNF.attachEvent('onclick', mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true), null);
        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' >"));
        oNF.insertCell(-1).appendChild(oImgMR.cloneNode(true), null);

        //        oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtL' style='width:260px' value='' maxlength='30' onKeyUp='fm(event)'>"));
        oNC2 = oNF.insertCell(-1);
        var oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.style.width = "260px";
        //oCtrl1.style.padding = "0px 0px 0px 0px";
        oCtrl1.className = "txtL";
        oCtrl1.maxLength = "30";
        //oCtrl1.onkeyup = function() { fm(event) };
        oCtrl1.attachEvent('onkeyup', fm);
        oNC2.appendChild(oCtrl1);
        //	    oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtNumL' style='width:95px' value='' onKeyUp='fm(event)' onfocus='fn(this)'>"));
        oNC3 = oNF.insertCell(-1);
        var oCtrl2 = document.createElement("input");
        oCtrl2.type = "text";
        oCtrl2.style.width = "90px";
        //oCtrl2.style.padding = "0px 0px 0px 0px";
        oCtrl2.className = "txtNumL";
        //oCtrl2.maxLength = "30";
        //oCtrl2.onkeyup = function() { fm(event) };
        oCtrl2.onfocus = function() { fn(this) };
        oCtrl2.attachEvent('onkeyup', fm);
        oNC3.appendChild(oCtrl2);
        //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtNumL' style='width:95px' value='' onKeyUp='fm(event)' onfocus='fn(this)'>"));
        oNC4 = oNF.insertCell(-1);
        var oCtrl3 = document.createElement("input");
        oCtrl3.type = "text";
        oCtrl3.style.width = "90px";
        //oCtrl3.style.padding = "0px 3px 0px 0px";
        oCtrl3.className = "txtNumL";
        //oCtrl3.maxLength = "30";
        //oCtrl3.onkeyup = function() { fm(event) };
        oCtrl3.attachEvent('onkeyup', fm);
        oCtrl3.onfocus = function() { fn(this) };
        oNC4.appendChild(oCtrl3);

        //oNF.cells[2].setAttribute("style", "margin-left:5px;");
        oNF.cells[3].setAttribute("style", "text-align:right; margin-right:5px;");
        oNF.cells[4].setAttribute("style", "text-align:right; margin-right:5px;");

        ms(oNF);

        oNF.cells[2].children[0].focus();
        activarGrabar();
    } catch (e) {
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}

function eliminar() {
    try {
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        if (aFila == null) return;
        for (var i = aFila.length - 1; i >= 0; i--) {
            if (aFila[i].className == "FS") {
                if (aFila[i].getAttribute("bd") == "I") {
                    $I("tblDatos").deleteRow(i);
                } else {
                    mfa(aFila[i], "D");
                }
            }
        }
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}

function grabar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id + "##"); //ID Tarifa
                sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) +"##"); //Descripcion
                sb.Append((aFila[i].cells[3].children[0].value!="")? aFila[i].cells[3].children[0].value +"##" : "0##"); //hora
                sb.Append((aFila[i].cells[4].children[0].value!="")? aFila[i].cells[4].children[0].value +"##" : "0##"); //jornada
                sb.Append(aFila[i].getAttribute("orden") + "##"); //Orden
                sb.Append(sValorNodo + "///"); //Nodo
                sw = 1;
            }
        }
        if (sw == 0){
            desActivarGrabar();
            mmoff("War", "No se han modificado los datos.", 230);
            return;
        }
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos(){
    try{
        var nOrden=0;
        var js_denominaciones = new Array();

        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].cells[2].children[0].value == ""){
                ms(aFila[i]);
                mmoff("War", "Debes indicar la denominación del Nivel", 270);
                aFila[i].cells[2].children[0].focus();
                return false;
            }

            if (js_denominaciones.isInArray(aFila[i].cells[2].children[0].value) == null) {
                js_denominaciones[js_denominaciones.length] = aFila[i].cells[2].children[0].value;
            } else {
                ms(aFila[i]);
                mmoff("War", "No se permiten denominaciones de Nivel repetidas.",340);
                aFila[i].cells[2].children[0].focus();
                return false;
            }
            
            if (aFila[i].cells[3].children[0].value=="") aFila[i].cells[3].children[0].value="0,00";
            if (aFila[i].cells[4].children[0].value=="") aFila[i].cells[4].children[0].value="0,00";

            if (getFloat(aFila[i].cells[3].children[0].value) > getFloat(aFila[i].cells[4].children[0].value)) {
                ms(aFila[i]);
                mmoff("War", "El importe por hora no puede ser mayor que el importe por jornada",410);
                aFila[i].cells[3].children[0].focus();
                return false;
            }
            if (aFila[i].getAttribute("orden") != nOrden) {
                if (aFila[i].getAttribute("bd") != "I") aFila[i].setAttribute("bd", "U");
                aFila[i].setAttribute("orden", nOrden);
            }
            nOrden++;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function getNodo() {
    try {
        if ($I("lblNodo").className == "texto") return;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bTarifaNodo = true;
                    grabar();
                    return;
                }
                else bCambios = false;
                LLamadaGetNodo();
            });
        }
        else LLamadaGetNodo();
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función getNodo-1 ", e.message);
    }
}
function LLamadaGetNodo() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
	        .then(function (ret) {
	            //alert(ret);
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                sValorNodo = aDatos[0];
	                $I("hdnIdNodo").value = aDatos[0];
	                $I("txtDesNodo").value = aDatos[1];
	                $I("lblMoneda").innerText = aDatos[14];
	                getTarifas();
	            }
	            ocultarProcesando();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función getNodo-2 ", e.message);
    }
}
function setNodo(){
    try{
        var oNodo = $I("cboNodo");

        if (oNodo.value == "") {
            $I("lblMoneda").innerText = "";
            borrarCatalogo();
            return;
        }

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    grabar();
                    return;
                }
                else desActivarGrabar();
                LLamadaSetNodo(oNodo);
            });
        }
        else LLamadaSetNodo(oNodo);
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el nodo-1.", e.message);
    }
}
function LLamadaSetNodo(oNodo) {
    try {
        $I("hdnIdNodo").value = oNodo.value;
        sValorNodo = oNodo.value;
        $I("lblMoneda").innerText = oNodo.options[oNodo.selectedIndex].desmoneda;
        getTarifas();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el nodo-2.", e.message);
    }
}
function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
        }else{
            $I("cboNodo").value = "";
        }        
        sValorNodo = "";
        $I("lblMoneda").innerText = "";
        
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el nodo", e.message);
    }
}
function borrarCatalogo(){
    try{
        sValorNodo="";
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function getTarifas(){
    try{
        var js_args = "getTarifas@#@";
        js_args += sValorNodo +"@#@";
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las tarifas", e.message);
    }
}

