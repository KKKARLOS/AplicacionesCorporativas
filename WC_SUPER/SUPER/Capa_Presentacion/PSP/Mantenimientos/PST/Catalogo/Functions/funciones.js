var sValorNodo="";
var aFila;
var idNuevo=1;
var bActivas = false;
var bCboNodo = false;
var oFecharef;
var idNuevaLinea = -1;

function init(){
    try
    {
        if (btnCal == "I") 
        {
            oFecharef = document.createElement("input");
            oFecharef.type = "text";
            oFecharef.readOnly = true;
            oFecharef.className = "txtL";
            oFecharef.setAttribute("class", "txtL");
            oFecharef.setAttribute("style", "width:60px");
            oFecharef.setAttribute("Calendar", "oCal");
            oFecharef.onchange = function() { fm_mn(this) };
            oFecharef.onclick = function() { mc(this) };

            //oFecharef = document.createElement("<input type='text' value='' class='txtL' style='width:60px;' readonly Calendar='oCal' onclick='mc(this);fm(this);'>");
        }
        else 
        {
            oFecharef = document.createElement("input");
            oFecharef.type = "text";
            oFecharef.className = "txtL";
            oFecharef.setAttribute("class", "txtL");
            oFecharef.setAttribute("style", "width:60px");
            oFecharef.setAttribute("Calendar", "oCal");
            oFecharef.onchange = function() { fm_mn(this) };
            oFecharef.attachEvent("onmousedown", mc1);
            oFecharef.attachEvent("onfocus", focoFecha);
            
            //oFecharef = document.createElement("<input type='text' value='' class='txtL' style='width:60px;' Calendar='oCal' onclick='fm(this);' onfocus='focoFecha(this)' onmousedown='mc1(this)' >");
        }
        if ($I("hdnMensajeError").value!="")
        {
		    var reg = /\\n/g;
		    var strMsg = $I("hdnMensajeError").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mmoff("Inf", strMsg, 400);
            $I("hdnMensajeError").value="";
        }
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }
        else{
            sValorNodo = $I("cboCR").value;   	    
            $I("lblNodo").className = "texto";
        }
        actualizarLupas("tblTitulo", "tblDatos");
        //scrollTablaAE();        
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
		    mostrarError("No se puede eliminar la OTC '" + aResul[3] + "',\n ya que existen proyectos técnicos o tareas que la tienen asignada.\n\nSi deseas que una OTC no se pueda asignar a mas tareas debe desactivarla.");
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
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].id = aEI[nIndiceEI]; 
                        nIndiceEI++;
                    }
                    mfa(aFila[i],"N");
                }
                actualizarLupas("tblTitulo", "tblDatos");
                desActivarGrabar();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();
                if (bActivas){
                    bActivas = false;
                    setTimeout("buscar2()", 50);
                }
                else {
                    if (bCboNodo) {
                        bCboNodo = false;
                        bActivas = false;
                        sValorNodo = $I("cboCR").value;
                        setTimeout("buscar2()", 50);
                    }
                }
                break;
//            case "eliminar":
//                var aFila = FilasDe("tblDatos")
//                for (var i = aFila.length-1; i>=0; i--){
//                    if (aFila[i].className == "FS") $I("tblDatos").deleteRow(i);
//                }
//                break;
            case "pst":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                //alert(tblDatos.rows.length);
                actualizarLupas("tblTitulo", "tblDatos");
                scrollTablaAE();
                ocultarProcesando();
                break;
        }
    }
}
//function eliminar(){
//    try{
//        var aFila = FilasDe("tblDatos");
//        if (aFila.length == 0) return;
//        var sw = 0;
//        var strID = "";
//        for(var i=0;i<aFila.length;i++){
//            if (aFila[i].className == "FS"){
//                strID += aFila[i].id + "\\" + Utilidades.escape(aFila[i].cells[1].innerText) + "##";
//                sw = 1;
//            }
//        }
//        if (sw == 0){
//            alert("Seleccione la fila a eliminar");
//            return;
//        }
//        strID = strID.substring(0, strID.length-2);
//        var js_args = "eliminar@#@";
//        js_args += strID; 
//        
//        mostrarProcesando();
//        RealizarCallBack(js_args, "");
//        return;
//	}catch(e){
//		mostrarErrorAplicacion("Error al eliminar la OTC", e.message);
//    }
//}
function eliminar(){
    try{
        var sw=0;
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw=1;
                if (aFila[i].getAttribute("bd") == "I"){
                    $I("tblDatos").deleteRow(i);
                }else{
                    mfa(aFila[i], "D");
                }
            }
        }
        if (sw==0)
            mmoff("Inf", "Debes seleccionar alguna fila a borrar", 250);
        else 
            activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}

function nuevo(){
    try{
        if (sValorNodo=="" || sValorNodo=="-1"){
            mmoff("Inf", "Debes seleccionar un " + $I("lblNodo").innerText, 330);
            return;
        }
        var oNF;
        oNF = $I("tblDatos").insertRow(-1);
        idNuevo++;
        oNF.id = idNuevaLinea--;
        oNF.style.height = "20px";      
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", "1");
        oNF.setAttribute("cli", "");
        oNF.setAttribute("idOText", "");
        oNF.setAttribute("idOriExt", "");
        oNF.attachEvent('onclick', mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
//      oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../../images/imgFI.gif'>"));

        var oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.id = "txtFun" + iFila;
        oCtrl1.className = "txtL";
        oCtrl1.setAttribute("class", "txtL");
        oCtrl1.setAttribute("style", "width:120px");
        oCtrl1.setAttribute("maxLength", "25");
        oCtrl1.onkeyup = function() { fm_mn(this) };
        oCtrl1.onkeypress = function() { activarGrabar() };

        oNC1 = oNF.insertCell(-1);
        oNC1.setAttribute("style", "padding-left:5px");
        oNC1.appendChild(oCtrl1);            
	    
//	    oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtL' style='width:195px;' value='' onkeypress='activarGrabar();fm(this);' maxlength='15' />"));

        var oCtrl2 = document.createElement("input");
        oCtrl2.type = "text";
        oCtrl2.id = "txtFun" + iFila;
        oCtrl2.className = "txtL";
        oCtrl2.setAttribute("class", "txtL");
        oCtrl2.setAttribute("style", "width:255px");
        oCtrl2.setAttribute("maxLength", "40");
        oCtrl2.onkeyup = function() { fm_mn(this) };
        oCtrl2.onkeypress = function() { activarGrabar() };
        oNF.insertCell(-1).appendChild(oCtrl2); 

//	    oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtL' style='width:335px;' value='' onkeypress='activarGrabar();fm(this);' maxlength='50' />"));


        var oCtrlH = document.createElement("input");
        oCtrlH.type = "text";
        oCtrlH.id = "txtHoras" + iFila;
        oCtrlH.className = "txtL";
        oCtrlH.setAttribute("class", "txtL");
        oCtrlH.setAttribute("style", "width:47px; text-align:right");
        oCtrlH.setAttribute("SkinID", "numero");
        oCtrlH.setAttribute("maxLength", "10");
        oCtrlH.onfocus = function () { fn(this, 7, 2); };
        oCtrlH.onkeypress = function () { activarGrabar() };
        oNF.insertCell(-1).appendChild(oCtrlH);


        var oCtrlP = document.createElement("input");
        oCtrlP.type = "text";
        oCtrlP.id = "txtPresupuesto" + iFila;
        oCtrlP.className = "txtL";
        oCtrlP.setAttribute("class", "txtL");
        oCtrlP.setAttribute("style", "width:80px; text-align:right");
        oCtrlP.setAttribute("SkinID", "numero");
        oCtrlP.setAttribute("maxLength", "10");
        oCtrlP.onfocus = function () { fn(this, 7, 2) };
        oCtrlP.onkeypress = function () { activarGrabar() };
        oNF.insertCell(-1).appendChild(oCtrlP);

        var oCtrlM = document.createElement("label");
        oCtrlM.id = "lblMoneda" + iFila;
        oCtrlM.innerHTML = "";
        oCtrlM.value = "EUR";
        oCtrlM.className = "enlace";
        oCtrlM.setAttribute("style", "width:50px; text-align:center;");
        oCtrlM.setAttribute("maxLength", "10"); 
        oCtrlM.onclick = function () { getMonedaImportes(null, oCtrlM.id); };
        oNF.insertCell(-1).appendChild(oCtrlM);
        //$(oCtrlM.id).html("Euro");


        var oFecha;
        if (btnCal == "I") 
        {
            oFecha = document.createElement("input");
            oFecha.type = "text";
            oFecha.id = "fn" + idNuevo;
            oFecha.readOnly = true;
            oFecha.className = "txtL";
            oFecha.setAttribute("class", "txtL");
            oFecha.setAttribute("style", "width:60px;cursor:pointer");
            oFecha.setAttribute("Calendar", "oCal");
            oFecha.setAttribute("goma", "1");
            oFecha.onchange = function() { fm_mn(this); activarGrabar() };
            oFecha.onclick = function() { mc(this) };
            oNF.insertCell(-1).appendChild(oFecha);
            
            //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='fn" + idNuevo + "' class='txtL' style='width:60px; cursor:pointer' value='' Calendar='oCal' onclick='mc(this);' onchange='activarGrabar();fm(this);' goma='1' readonly />"));
        }
        else 
        {
            oFecha = document.createElement("input");
            oFecha.type = "text";
            oFecha.id = "fn" + idNuevo;
            oFecha.className = "txtL";
            oFecha.setAttribute("class", "txtL");
            oFecha.setAttribute("style", "width:60px;cursor:pointer");
            oFecha.setAttribute("Calendar", "oCal");
            oFecha.onchange = function() { fm_mn(this); activarGrabar() };
            oFecha.attachEvent("onmousedown", mc1);
            oFecha.attachEvent("onfocus", focoFecha);
            oNF.insertCell(-1).appendChild(oFecha);
            
            //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='fn" + idNuevo + "' class='txtL' style='width:60px; cursor:pointer' value='' Calendar='oCal' onchange='activarGrabar();fm(this);' goma='1' onmousedown='mc1(this)' onfocus='focoFecha(this)'/>"));
        }

        var oCtrl3 = document.createElement("input");
        oCtrl3.type = "checkbox";
        oCtrl3.checked = true;
        oCtrl3.setAttribute("checked", "true");
        oCtrl3.className = "check";
        oCtrl3.setAttribute("style", "width:15px");
        oCtrl3.onclick = function() { fm_mn(this) };

        oNC3 = oNF.insertCell(-1);        
        oNC3.setAttribute("style", "text-align:center");

        oNC3.appendChild(oCtrl3);  
        
	    //oNF.insertCell(-1).appendChild(document.createElement("<input type='checkbox' class='check' onclick='fm(this)' checked>"));

        var oCtrl4 = document.createElement("span");
        oCtrl4.className = "NBR W260";
        oNF.insertCell(-1).appendChild(oCtrl4);  
        
        //oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W265'></nobr>"));
        
        oNF.cells[8].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
        oNF.cells[8].style.backgroundRepeat = "no-repeat";
        oNF.cells[8].style.cursor = strCurMA;
        oNF.cells[8].ondblclick = function(){getClienteAE(this.parentNode);};

        ms(oNF);
        oNF.cells[1].children[0].focus();
        mfa(oNF, "I");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir una OTC", e.message);
    }
}
function mostrarActivas(){
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bActivas = true;
                    grabar();
                }
                else {
                    ordenarTabla('3', '0');
                    bCambios = false;
                }
            });
        }
        else ordenarTabla('3', '0');
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar OTCs", e.message);
    }
}
function ordenarTabla(nOrden,nAscDesc){
    if (sValorNodo=="" || sValorNodo=="-1") return;
    if ($I("chkAct").checked)
        buscar(nOrden,nAscDesc,"T");//PST activas e inactivashttp://localhost:64606/Capa_Presentacion/UserControls/Msg/Images/popup_close.png
    else
        buscar(nOrden,nAscDesc,"A");//solo PST activas
}
function buscar2(){
    ordenarTabla('3','0');
}
function buscar(nOrden,nAscDesc,sTipo){
    try{
        if (sValorNodo=="" || sValorNodo=="-1") return;
        var js_args = "pst@#@"+sTipo+"@#@"+sValorNodo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    }catch(e){
	    mostrarErrorAplicacion("Error al ordenar el catálogo de OTCs", e.message);
    }
}
function setCombo() {
    try {
        sValorNodo = $I("cboCR").value;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCboNodo = true;
                    grabar();
                }
                else {
                    LLamarSetCombo();
                    bCambios = false;
                }
            });
        } else LLamarSetCombo();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function LLamarSetCombo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
        if ($I("chkAct").checked)
            buscar(3, 0, "T");
        else
            buscar(3, 0, "A");
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}

function getNodo(){
    try{
        if ($I("lblNodo").className == "texto") return;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bActivas = true;
                    grabar();
                }
                else
                    LLamarGetNodo();
            });
        } else LLamarGetNodo();
    }catch(e){
        mostrarErrorAplicacion("Error al obtener el nodo-2", e.message);
    }
}
function LLamarGetNodo() {
    try {        
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 460))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    LLamarSetCombo();
                }
            });
        window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo ", e.message);
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
                sb.Append(aFila[i].id +"##"); //ID OTC
                sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"); //Codigo
                sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) + "##"); //Descripcion                
                sb.Append(aFila[i].getAttribute("cli") + "##"); //ID Cliente
                sb.Append(aFila[i].cells[6].children[0].value +"##"); //Fecha
                sb.Append((aFila[i].cells[7].children[0].checked==true)? "1##" : "0##"); //Activo
                sb.Append(sValorNodo + "##"); //Nodo
                sb.Append(aFila[i].getAttribute("idOText") + "##"); //idOTExterno
                sb.Append(aFila[i].getAttribute("idOriExt") + "##"); //idOrigenExterno
                sb.Append(aFila[i].cells[3].children[0].value + "##"); //Horas
                sb.Append(aFila[i].cells[4].children[0].value + "##"); //Presupuesto
                sb.Append(aFila[i].cells[5].children[0].value + "##"); //Moneda
                sb.Append("///"); 
                sw = 1;
            }
        }
        
        if (sw == 0){
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
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D" || aFila[i].getAttribute("bd") == "") continue;
            
            if (aFila[i].cells[1].children[0].value == ""){
                ms(aFila[i]);
                mmoff("War", "El código de la OTC es obligatorio",210);
                aFila[i].cells[2].children[0].focus();
                return false;
            }
            if (aFila[i].cells[2].children[0].value == ""){
                ms(aFila[i]);
                mmoff("War","La denominación de la OTC es obligatoria",320);
                aFila[i].cells[2].children[0].focus();
                return false;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
//var oFF = document.createElement("<input type='text' value='' class='txtL' style='width:60px;' readonly Calendar='oCal' onclick='mc(this);' onchange='mmse(this.parentNode.parentNode);controlarFecha(\"F\");'>");
//function ii(oFila){
//    try{
//        if (oFila.sw == 1) return;
//        else{
//            var sAux = oFila.cells[4].innerText;
//            oFila.cells[4].innerText = "";
//            oFila.cells[4].appendChild(oFF.cloneNode(true), null);
//            oFila.cells[4].children[0].value = sAux;
//            oFila.cells[4].children[0].valAnt = sAux;
//        }
//        oFila.sw = 1;
//	}catch(e){
//		mostrarErrorAplicacion("Error al añadir los inputs en la fila", e.message);
//    }
//}
function getClienteAE(oFila){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("cli", aDatos[0]);
                    oFila.cells[8].style.backgroundImage = "";
                    oFila.cells[8].children[0].innerText = aDatos[1];
                    //oFila.sw=0;
                    if (oFila.cells[8].children[1] != null) oFila.cells[8].removeChild(oFila.cells[8].children[1]);
                    //scrollTablaAE();

                    if (oFila.getAttribute("cli") == "") {
                        oFila.cells[8].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
                        oFila.cells[8].style.backgroundRepeat = "no-repeat";
                    } else {
                        var oGoma = oFila.cells[8].appendChild(oGomaPerfil.cloneNode(true), null);
                        oGoma.onclick = function() { borrarCliente(this.parentNode.parentNode); };
                        oGoma.style.cursor = "pointer";
                    }

                    mfa(oFila, "U");
                    activarGrabar();
                }
            });
        window.focus();

	    ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function borrarCliente(oFila){
    try{
        oFila.setAttribute("cli", "");
        oFila.cells[8].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
        oFila.cells[8].style.backgroundRepeat = "no-repeat";
	    oFila.cells[8].children[0].innerText = "";
	    if (oFila.cells[8].children[1] != null) oFila.cells[8].removeChild(oFila.cells[8].children[1]);
	    mfa(oFila, "U");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cliente al criterio estadístico", e.message);
	}
}

var oCodpst = document.createElement("input");
oCodpst.type = "text";
oCodpst.className = "txtL";
oCodpst.setAttribute("class", "txtL");
oCodpst.setAttribute("style", "width:110px");
oCodpst.setAttribute("maxLength", "25");

oCodpst.onkeyup = function() { fm_mn(this) };
    
//var oCodpst = document.createElement("<input type='text' class='txtL' style='width:195px' value='' maxlength='25' onKeyUp='fm(this)'>");

var oDespst = document.createElement("input");
oDespst.type = "text";
oDespst.className = "txtL";
oDespst.setAttribute("class", "txtL");
oDespst.setAttribute("style", "width:245px");
oDespst.setAttribute("maxLength", "40");

//oDespst.onkeyup = function() { fm_mn(this) };

//var oDespst = document.createElement("<input type='text' class='txtL' style='width:330px' value='' maxlength='30' onKeyUp='fm(this)'>");

var oHoraspst = document.createElement("input");
oHoraspst.type = "text";
oHoraspst.className = "txtL";
oHoraspst.setAttribute("class", "txtL");
oHoraspst.setAttribute("style", "width:47px; text-align:right");
oHoraspst.setAttribute("SkinID", "numero");
oHoraspst.setAttribute("maxLength", "40");

var oPresuppst = document.createElement("input");
oPresuppst.type = "text";
oPresuppst.className = "txtL";
oPresuppst.setAttribute("class", "txtL");
oPresuppst.setAttribute("style", "width:70px; text-align:right");
oPresuppst.setAttribute("SkinID", "numero");
oPresuppst.setAttribute("maxLength", "12");

var oMonedapst = document.createElement("label");
//oMonedapst.type = "text";
oMonedapst.className = "enlace";
//oMonedapst.setAttribute("class", "txtL");
oMonedapst.setAttribute("style", "width:50px; text-align:center");
oMonedapst.setAttribute("maxLength", "40");

var oEstadoA = document.createElement("input");
oEstadoA.type = "checkbox";
oEstadoA.checked = true;
oEstadoA.setAttribute("checked", "true");
oEstadoA.className = "check";
oEstadoA.setAttribute("style", "width:15px");

//var oEstadoA = document.createElement("<input type='checkbox' class='check' checked onclick='fm(this)'>");

var oEstadoD = document.createElement("input");
oEstadoD.type = "checkbox";
oEstadoD.className = "check";
oEstadoD.setAttribute("style", "width:15px");


//var oEstadoD = document.createElement("<input type='checkbox' class='check' onclick='fm(this)'>");

var oCliente = document.createElement("span");
oCliente.className = "NBR W265";

//var oCliente = document.createElement("<nobr class='NBR' style='width:265px'></nobr>");

var oGomaPerfil = document.createElement("img");
oGomaPerfil.setAttribute("src", "../../../../../images/botones/imgBorrar.gif");
oGomaPerfil.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

//var oGomaPerfil = document.createElement("<img src='../../../../../images/botones/imgBorrar.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

var nTopScrollAE = -1;
var nIDTimeAE = 0;
function scrollTablaAE(){
    try{
        if (nTopScrollAE < 0 || $I("divCatalogo").scrollTop != nTopScrollAE) {
            nTopScrollAE = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeAE);
            nIDTimeAE = setTimeout("scrollTablaAE()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        var nFilaVisible = Math.floor(nTopScrollAE / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        nFilaVisible = 0;
        nUltFila = tblDatos.rows.length;
        var oFila;
        var sAux;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw") && tblDatos.rows[i].getAttribute("id") > 0) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                oFila.attachEvent('onclick', mm);

                oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);


                sAux = oFila.cells[1].innerText;
                oFila.cells[1].innerText = "";

                var oCod = oCodpst.cloneNode(true)
                oCod.onkeyup = function() { fm_mn(this) };                
                
                oFila.cells[1].appendChild(oCod, null);
                oFila.cells[1].children[0].value = sAux;
                // nota: se vuelven asignar los eventos puestos se pierden al clonar el objeto.
                oFila.cells[1].children[0].onkeyup = function () { fm_mn(this) };

                 sAux = oFila.cells[2].innerText;
                oFila.cells[2].innerText = "";
                
                var oDes = oDespst.cloneNode(true);
                oDes.onkeyup = function() { fm_mn(this) };
                
                oFila.cells[2].appendChild(oDes, null);
                oFila.cells[2].children[0].value = sAux;
                oFila.cells[2].children[0].onkeyup = function () { fm_mn(this) };

                //Horas
                sAux = oFila.cells[3].innerText;
                oFila.cells[3].innerText = "";

                var oHoras = oHoraspst.cloneNode(true);
                oHoras.onfocus = function () { fn(this, 7, 2); };

                oFila.cells[3].appendChild(oHoras, null);
                oFila.cells[3].children[0].value = sAux;
                oFila.cells[3].children[0].onchange = function () { fm_mn(this) };

                //Presupuesto
                sAux = oFila.cells[4].innerText;
                oFila.cells[4].innerText = "";

                var oPresup = oPresuppst.cloneNode(true);
                oPresup.onfocus= function () { fn(this, 7, 2); };

                oFila.cells[4].appendChild(oPresup, null);
                oFila.cells[4].children[0].value = sAux;
                oFila.cells[4].children[0].onchange = function () { fm_mn(this) };

                //Moneda
                sAux = oFila.cells[5].innerText;
                oFila.cells[5].innerText = "";

                var oMoneda = oMonedapst.cloneNode(true);
                oMoneda.onclick = function () { getMonedaImportes(this, this.id); };
                oFila.cells[5].appendChild(oMoneda, null);
                oFila.cells[5].children[0].value = sAux;
                oFila.cells[5].children[0].innerHTML = sAux;
                oFila.cells[5].children[0].id = "m" + oFila.id;

                //Fecha referencia
                sAux = oFila.cells[6].innerText;
                oFila.cells[6].innerText = "";
                oFila.cells[6].appendChild(oFecharef.cloneNode(true), null);
                oFila.cells[6].children[0].id = "f"+ oFila.id
                oFila.cells[6].children[0].value = sAux;

                if (btnCal == "I") {
                    oFila.cells[6].children[0].onchange = function() { fm_mn(this) };
                    oFila.cells[6].children[0].onclick = function() { mc(this) };
                }
                else 
                {
                    oFila.cells[6].children[0].onchange = function() { fm_mn(this) };
                    oFila.cells[6].children[0].attachEvent("onmousedown", mc1);
                    oFila.cells[6].children[0].attachEvent("onfocus", focoFecha);                                
                }

                var oEstado;
                if (oFila.cells[7].innerHTML == "") {
                    if (oFila.getAttribute("estado") == '1' ) oEstado = oEstadoA.cloneNode(true)
                    else oEstado = oEstadoD.cloneNode(true);
                    oEstado.onclick = function () { fm_mn(this) };
                    oFila.cells[7].appendChild(oEstado, null);
                    oFila.cells[7].children[0].onclick = function () { fm_mn(this) };
                }                
                              

                if (oFila.getAttribute("cli") == "") {
                    oFila.cells[8].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
                    oFila.cells[8].style.backgroundRepeat = "no-repeat";
                }else{
                    var oGoma = oFila.cells[8].appendChild(oGomaPerfil.cloneNode(true), null);
                    oGoma.onclick = function(){borrarCliente(this.parentNode.parentNode);};
                    oGoma.style.cursor = "pointer";
                }
                oFila.cells[8].style.cursor = strCurMA;
                oFila.cells[8].ondblclick = function(){getClienteAE(this.parentNode);};
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de OTCs.", e.message);
    }    
}

function getMonedaImportes(obj,labelId) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDC";
        modalDialog.Show(strEnlace, self, sSize(350, 300))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I(labelId).innerText = aDatos[0];
                    $I(labelId).value = aDatos[0];
                    $I(labelId).title = aDatos[1];
                    if(obj != null) fm_mn(obj);
                    //activarGrabar();                    
                }
            });
        window.focus();
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}
