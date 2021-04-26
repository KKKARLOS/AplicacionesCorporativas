var tbody;
var aFila;
var bTarifaNodo = false;
var sElementosInsertados = "";
var sTipoGrupo = "";

function init(){
    try{
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
            case "grabar":
                sElementosInsertados = aResul[2];
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
                
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160); 

                if (bGrupoEco){
                    bGrupoEco = false;
                    setTimeout("getSE("+ nGrupoEco +")", 50);
                }
                if (bSubgrupoEco){
                    bSubgrupoEco = false;
                    setTimeout("getCE("+ nSubgrupoEco +")", 50);
                }
                if (bConceptoEco){
                    bConceptoEco = false;
                    setTimeout("getClaseEco($I('cboCE'))",100);
                }
                break;
            case "getSE":
                var aDatos = aResul[2].split("///");
                var j=1;
                $I("cboSE").length = 1;
                $I("cboCE").length = 1;

                $I("divCatalogo").children[0].innerHTML = "";
                iFila = -1;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1],aValor[0]); 
                    $I("cboSE").options[j] = opcion;
			        j++;
                }
                break;
            case "getCE":
                var aDatos = aResul[2].split("///");
                var j=1;
                $I("cboCE").length = 1;
                $I("divCatalogo").children[0].innerHTML = "";
                iFila = -1;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1],aValor[0]); 
                    $I("cboCE").options[j] = opcion;
			        j++;
                }
                break;
            case "getClaseEco":
                //alert(aResul[2]);
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                iFila = -1;
                tbody = document.getElementById('tbodyDatos'); 
                tbody.onmousedown = startDragIMG; 
                break;
                
        }
        ocultarProcesando();
    }
}


function nuevo(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        if ($I("cboCE").value==""){
            mmoff("Inf","Debes seleccionar el concepto económico",280);
            return;
        }
        //oNF --> objeto nueva fila
        oNF = $I("tblDatos").insertRow(-1);
        oNF.id = oNF.rowIndex;
        oNF.setAttribute("bd","I");
        oNF.setAttribute("orden",oNF.rowIndex);
        oNF.style.heigth = "22px";

        oNF.attachEvent('onclick', mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        var oImgMR = document.createElement("img");
        oImgMR.setAttribute("src", "../../../../images/imgMoveRow.gif");
        oImgMR.setAttribute("style", "cursor:row-resize;");
        oImgMR.setAttribute("title", "Pinchar y arrastrar para ordenar");
        oImgMR.ondragstart = function() { return false; };
        oNF.insertCell(-1).appendChild(oImgMR);

        var oCtrl1 = document.createElement("input");

        oCtrl1.type = "text";
        oCtrl1.style.width = "350px";
        oCtrl1.className = "txtL";
        oCtrl1.maxLength = "50";
        oCtrl1.onkeyup = function() { fm_mn(this) };
        
        oNF.insertCell(-1).appendChild(oCtrl1);

        var oCtrl2 = document.createElement("input");
        oCtrl2.setAttribute("type", "checkbox");
        oCtrl2.className = "check";
        oCtrl2.checked = true;
        oCtrl2.onclick = function() { fm_mn(this) };

        oNF.insertCell(-1).appendChild(oCtrl2);

        var oCtrl3 = document.createElement("input");
        oCtrl3.setAttribute("type", "checkbox");
        oCtrl3.className = "check";
        oCtrl3.onclick = function() { fm_mn(this) };

        oNF.insertCell(-1).appendChild(oCtrl3);

        oNC5 = oNF.insertCell(-1);
        var oCtrl5 = document.createElement("select");
        oCtrl5.className = "combo";
        oCtrl5.setAttribute("style", "width:80px");
        oCtrl5.onchange = function() { fm_mn(this); setReplica(this.value); };        
        
	    var opcion1 = new Option("", ""); 
        oCtrl5.options[0] = opcion1;
	    var opcion2 = new Option(strEstructuraNodo, "N"); 
        oCtrl5.options[1] = opcion2;
	    var opcion3 = new Option("Proveedor", "P"); 
        oCtrl5.options[2] = opcion3;

	    if (sTipoGrupo != "C") oCtrl5.disabled = true;
	    oNC5.appendChild(oCtrl5);

	    oNC6 = oNF.insertCell(-1);

	    var oCtrl6 = document.createElement("input");
	    oCtrl6.setAttribute("type", "checkbox");
	    oCtrl6.className = "check";
	    oCtrl6.onclick = function() { fm_mn(this) };        
	    oNC6.appendChild(oCtrl6);

	    oNC7 = oNF.insertCell(-1);
	    var oCtrl7 = document.createElement("select");
	    oCtrl7.className = "combo";
	    oCtrl7.setAttribute("style", "width:70px");
	    oCtrl7.onchange = function() { fm_mn(this) };

	    var opcion71 = new Option("", ""); 
        oCtrl7.options[0] = opcion71;
	    var opcion72 = new Option("Facturación", "F"); 
        oCtrl7.options[1] = opcion72;
	    var opcion73 = new Option("Previsión", "P"); 
        oCtrl7.options[2] = opcion73;
	    if (sTipoGrupo != "I") oCtrl7.disabled = true;
	    oNC7.appendChild(oCtrl7);

	    oNC8 = oNF.insertCell(-1);
	    var oCtrl8 = document.createElement("input");
	    oCtrl8.setAttribute("type", "checkbox");
	    oCtrl8.className = "check";
	    oCtrl8.onclick = function() { fm_mn(this) };
	    //if (sTipoGrupo != "C") oCtrl8.disabled = true;
	    oNC8.appendChild(oCtrl8);

	    oNC9 = oNF.insertCell(-1);
	    var oCtrl9 = document.createElement("input");
	    oCtrl9.setAttribute("type", "checkbox");
	    oCtrl9.className = "check";
	    oCtrl9.onclick = function() { fm_mn(this) };        
	    oNC9.appendChild(oCtrl9);

	    oNC10 = oNF.insertCell(-1);
	    var oCtrl10 = document.createElement("input");
	    oCtrl10.setAttribute("type", "checkbox");
	    oCtrl10.className = "check";
	    oCtrl10.onclick = function() { fm_mn(this) };           
	    oNC10.appendChild(oCtrl10);

	    oNC11 = oNF.insertCell(-1);
	    var oCtrl11 = document.createElement("input");
	    oCtrl11.setAttribute("type", "checkbox");
	    oCtrl11.className = "check";
	    oCtrl11.onclick = function() { fm_mn(this) };          
	    oNC11.appendChild(oCtrl11);

	    oNC12 = oNF.insertCell(-1);
	    var oCtrl12 = document.createElement("input");
	    oCtrl12.setAttribute("type", "checkbox");
	    oCtrl12.className = "check";
	    oCtrl12.onclick = function() { fm_mn(this) };
        oNC12.appendChild(oCtrl12);

        ms(oNF);
	    
	    oNF.cells[2].children[0].focus();
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}

function eliminar(){
    try {
        if ($I("tblDatos") == null) return;
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("bd") == "I"){
                    $I("tblDatos").deleteRow(i);
                }else{
                    mfa(aFila[i], "D");
                }
            }
        }
        activarGrabar();
	}catch(e){
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
                sb.Append(aFila[i].getAttribute("bd") +"##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Clase
                //sb.Append($I("cboCE").value + "##"); //ID Concepto Económico
                sb.Append(idCboCE + "##"); //ID Concepto Económico
                sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) +"##"); //Descripcion
                sb.Append((aFila[i].cells[3].children[0].checked==true)? "1##" : "0##"); //Estado
                sb.Append((aFila[i].cells[4].children[0].checked==true)? "1##" : "0##"); //PresentableAdm
                sb.Append(aFila[i].cells[5].children[0].value +"##"); //Necesidad
                sb.Append((aFila[i].cells[6].children[0].checked==true)? "1##" : "0##"); //Dispara réplica
                sb.Append(aFila[i].cells[7].children[0].value +"##"); //Decalaje y borrado
                sb.Append((aFila[i].cells[8].children[0].checked==true)? "1##" : "0##"); //CGF
                sb.Append((aFila[i].cells[9].children[0].checked==true)? "1##" : "0##"); //VPC
                sb.Append((aFila[i].cells[10].children[0].checked==true)? "1##" : "0##"); //VPSG
                sb.Append((aFila[i].cells[11].children[0].checked==true)? "1##" : "0##"); //VPCG
                sb.Append(aFila[i].getAttribute("orden") +"##"); //Orden
                sb.Append(aFila[i].getAttribute("nb") + "##"); //No borrable.
                sb.Append((aFila[i].cells[12].children[0].checked==true)? "1##" : "0##"); //Clonable
                //sb.Append((aFila[i].cells[13].children[0].checked==true)? "1##" : "0##"); //Factura
                sb.Append("///");
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
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].cells[2].children[0].value == "") {
                ms(aFila[i]);          

                mmoff("War","La denominación de la clase es obligatoria",300);
                aFila[i].cells[2].children[0].focus();
                return false;
            }
            
            if (aFila[i].cells[5].children[0].value != "" && sTipoGrupo != "C"){
                ms(aFila[i]);
                mmoff("War", "Una clase sólo puede tener una \"Necesidad\", si pertenece al grupo Consumos",480);
                if (aFila[i].cells[5].children[0].disabled==false) aFila[i].cells[5].children[0].focus();
                return false;
            }
            
            if (aFila[i].cells[7].children[0].value != "" && sTipoGrupo != "I"){
                ms(aFila[i]);
                mmoff("War", "Una clase sólo puede intervenir en el decalaje y borrado, si pertenece al grupo Ingresos",530);
                if (aFila[i].cells[7].children[0].disabled==false) aFila[i].cells[7].children[0].focus();
                return false;
            }
            
            if (aFila[i].cells[6].children[0].checked && aFila[i].cells[5].children[0].value != "N"){
                ms(aFila[i]);
                mmoff("War", "Una clase sólo puede disparar réplica, si existe Nodo destino",380);
                if (aFila[i].cells[5].children[0].disabled==false) aFila[i].cells[5].children[0].focus();
                return false;
            }
           
            if (aFila[i].cells[6].children[0].checked && sTipoGrupo != "C"){
                ms(aFila[i]);
                mmoff("War", "Una clase sólo puede disparar réplica, si pertenece al grupo Consumos",400);
                if (aFila[i].cells[6].children[0].disabled==false) aFila[i].cells[6].children[0].focus();
                return false;
            }
            
            if (aFila[i].getAttribute("orden") != nOrden){
                if (aFila[i].getAttribute("bd") != "I") aFila[i].setAttribute("bd","U");
                aFila[i].setAttribute("orden",nOrden);
            }
            nOrden++;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

var nGrupoEco = 0;
var bGrupoEco = false;
function getSE(nGE){
    try{
        if (nGE==""){
            $I("cboSE").length = 1;
            $I("cboCE").length = 1;
            $I("divCatalogo").children[0].innerHTML = "";
            sTipoGrupo = "";
            return;
        }
        
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGrupoEco = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamadagetSE(nGE);
                }
            });
        }
        else LLamadagetSE(nGE);
    }catch(e){
        mostrarErrorAplicacion("Error al ir a obtener los grupos económicos-1 ", e.message);
    }
}
function LLamadagetSE(nGE) {
    try {
        mostrarProcesando();
        nGrupoEco = nGE;
        sTipoGrupo = $I("cboGE").options[$I("cboGE").selectedIndex].getAttribute("sTipo");
        var js_args = "getSE@#@";
        js_args += nGE;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los grupos económicos-2 ", e.message);
    }
}

var nSubgrupoEco = 0;
var bSubgrupoEco = false;
function getCE(nSE){
    try{
        if (nSE==""){
            $I("cboCE").length = 1;
            $I("divCatalogo").children[0].innerHTML = "";
            return;
        }
        if (bCambios){
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bSubgrupoEco = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamadagetCE(nSE);
                }
            });
        }
        else LLamadagetCE(nSE);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los subgrupos económicos-1", e.message);
    }
}
function LLamadagetCE(nSE) {
    try {
        mostrarProcesando();
        nSubgrupoEco = nSE;
        var js_args = "getCE@#@";
        js_args += nSE;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los subgrupos económicos-2", e.message);
    }
}
var nConceptoEco = 0;
var bConceptoEco = false;
var nConsPropio = 0;
var idCboCE = "";
function getClaseEco(oCE){
    try{
        if (oCE.value==""){
            $I("divCatalogo").children[0].innerHTML = "";
            return;
        }
        if (idCboCE == "") idCboCE = oCE.value;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bConceptoEco = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamadagetClaseEco(oCE);
                }
            });
        }
        else LLamadagetClaseEco(oCE);
    }catch(e){
        mostrarErrorAplicacion("Error al ir a obtener las clases económicas-1", e.message);
    }
}
function LLamadagetClaseEco(oCE) {
    try {
        idCboCE = oCE.value;
        mostrarProcesando();
        nConceptoEco = oCE.value;
        nConsPropio = oCE.options[oCE.selectedIndex].getAttribute("ConsPropio");
        var js_args = "getClaseEco@#@";
        js_args += oCE.value +"@#@";
        js_args += sTipoGrupo;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las clases económicas-2", e.message);
    }
}

function setReplica(sValor){
    try{
        if (sTipoGrupo == "C"){
            $I("tblDatos").rows[iFila].cells[5].children[0].disabled = false;
            if (sValor == "N"){
                $I("tblDatos").rows[iFila].cells[6].children[0].checked = true;
            }else{
                $I("tblDatos").rows[iFila].cells[6].children[0].checked = false;
            }
        }else{
            $I("tblDatos").rows[iFila].cells[5].children[0].disabled = true;
            $I("tblDatos").rows[iFila].cells[6].children[0].checked = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al controlar el dato de la réplica", e.message);
    }
}
