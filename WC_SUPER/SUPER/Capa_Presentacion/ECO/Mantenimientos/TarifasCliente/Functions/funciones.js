var tbody;
var aFila;
var bTarifaCliente = false;
var bTarifas = false;
var sElementosInsertados = "";
var bCambios = false;
function init(){
    try{
//        tbody = document.getElementById('tbodyDatos'); 
//        tbody.onmousedown = startDrag; 
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
		    mostrarError(("No se puede eliminar el perfil '" + aResul[3] + "',\n ya que existen elementos con los que está relacionada.").replace(reg, "\n"));
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
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
                if (bTarifas) {
                    bTarifas = false;
                    setTimeout("getTarifas();", 50);
                }

                if (bTarifaCliente){
                    bTarifaCliente = false;
                    setTimeout("getCliente();", 50);
                }

                break;
            case "getTarifas":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                tbody = document.getElementById('tbodyDatos'); 
                tbody.onmousedown = startDragIMG; 
                iFila = -1;
                break;
        }
        ocultarProcesando();
    }
}
function aG()
{
    activarGrabar();    
}

function nuevo(){
    try{
        if ($I("txtIDCliente").value == ""){
            mmoff("Inf", "Debes seleccionar el cliente",210);
            return;
        }
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

        var oCtrl4 = document.createElement("input");
        oCtrl4.type = "checkbox";
        oCtrl4.checked = true;
        oCtrl4.setAttribute("checked", "true");
        oCtrl4.className = "checkTabla";
        oCtrl4.setAttribute("style", "width:20px;margin-left:23px;");
        oCtrl4.attachEvent('onclick', fm);
        //oCtrl4.onclick = function () { fm_mn(this); };
        oNF.insertCell(-1).appendChild(oCtrl4);
        //oNF.cells[5].style.textAlign = "center";




        if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
        oNF.cells[2].children[0].focus();
        aG();
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
        aG();
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
        sb.Append($I("txtIDCliente").value +"@#@");
        
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") +"##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Tarifa
                sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) +"##"); //Descripcion
                sb.Append((aFila[i].cells[3].children[0].value!="")? aFila[i].cells[3].children[0].value +"##" : "0##"); //hora
                sb.Append((aFila[i].cells[4].children[0].value != "") ? aFila[i].cells[4].children[0].value + "##" : "0##"); //jornada
                sb.Append(aFila[i].getAttribute("orden") + "##"); //Orden
                sb.Append((aFila[i].cells[5].children[0].checked == true) ? "1///" : "0///"); //Estado

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
                mmoff("War", "Debes indicar la denominación del Perfil", 270);
                aFila[i].cells[2].children[0].focus();
                return false;
            }

            if (js_denominaciones.isInArray(aFila[i].cells[2].children[0].value) == null) {
                js_denominaciones[js_denominaciones.length] = aFila[i].cells[2].children[0].value;
            } else {
                ms(aFila[i]);
                mmoff("War", "No se permiten denominaciones de Perfil repetidas.", 340);
                aFila[i].cells[2].children[0].focus();
                return false;
            }

            if (aFila[i].cells[3].children[0].value == "") aFila[i].cells[3].children[0].value = "0,00";
            if (aFila[i].cells[4].children[0].value=="") aFila[i].cells[4].children[0].value="0,00";

            if (getFloat(aFila[i].cells[3].children[0].value) > getFloat(aFila[i].cells[4].children[0].value)) {
                ms(aFila[i]);
                mmoff("War", "El importe por hora no puede ser mayor que el importe por jornada", 410);
                aFila[i].cells[3].children[0].focus();
                return false;
            }
            if (aFila[i].getAttribute("orden") != nOrden){
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

function getCliente(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bTarifaCliente = true;
                    grabar();
                    return;
                }
                else bCambios = false;
                LLamadaGetCliente();
            });
        }
        else LLamadaGetCliente();
    }catch(e){
        mostrarErrorAplicacion("Error al mostrar los clientes-1", e.message);
    }
}
function LLamadaGetCliente() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getCliente.aspx";
	    modalDialog.Show(strEnlace, self, sSize(600, 480))
	        .then(function(ret) {
	            if (ret != null){
		            var aOpciones = ret.split("@#@");
                    $I("txtIDCliente").value = aOpciones[0];
                    $I("txtDesCliente").value = aOpciones[1];
                    
                    getTarifas();
                } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los clientes-2", e.message);
    }
}
function getTarifas() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bTarifas = true;
                    grabar();
                    return;
                }
                else {
                    bCambios = false;
                    llamadaGetTarifas();
                }
            });
        }
        else llamadaGetTarifas();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los clientes-1", e.message);
    }
}
function llamadaGetTarifas(){
    try{
        var js_args = "getTarifas@#@";
        js_args += $I("txtIDCliente").value+"@#@";
        js_args += ($I("chkAct").checked) ? "A" : "T";
        js_args += "@#@";
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las tarifas", e.message);
    }
}
