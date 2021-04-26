var tbody, tbodyVAE;
var aFila, aFilaVAE;
var bNuevo = false;
var idAE, idAEnew=-1, idVAEnew=-1;

function init(){
    try{
        idAE="";
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
        tbody = document.getElementById('tbodyDatos');
        if (tbody != null)
            tbody.onmousedown = startDragIMG; 
        if ($I("hdnVAE").value != ""){
            eval($I("hdnVAE").value);
            $I("hdnVAE").value="";
        }
        if (sValorNodo != "") {
            $I("lblCliente").className = "enlace";
            buscar();
        }
        scrollTablaAE();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var idNuevoAE;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    if (sAmbito=="T")
		        mostrarError("No se puede eliminar el criterio estadístico ya que existen\nproyectos técnicos o tareas que lo tienen asignado.");
		    else
		        mostrarError("No se puede eliminar el criterio estadístico ya que existen\nproyectos económicos que lo tienen asignado.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                //$I("divCatalogo").innerHTML = aResul[2];
                idAE="";
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                refrescarVAEs("*");
                eval(aResul[3]);
                tbody = document.getElementById('tbodyDatos');
                if (tbody != null)
                    tbody.onmousedown = startDragIMG; 
                scrollTablaAE();
                break;
            case "preEliminarAE":
                if (aResul[2] != "0"){
                    mmoff("Inf","El criterio estadístico está siendo utilizado en Proyectos técnicos y/o tareas",500);
//                    if (confirm("El criterio estadístico está siendo utilizado en Proyectos técnicos y/o tareas.\n¿Deseas continuar?")){
//                        setTimeout("eliminarAE();",500);
//                    }
//                }
//                else{
//                    setTimeout("eliminarAE();",500);
                }
                break;
            case "eliminar":
                idAE="";
                aFila = FilasDe("tblDatos");
                for(var i=aFila.length-1;i>=0;i--)
                    if (aFila[i].className == "FS"){
                        borrarAEDeArray(aFila[i].id);
                        $I("tblDatos").deleteRow(i);
                        refrescarVAEs("*");
                    }
                break;
            case "grabar":
                idAEnew = -1, idVAEnew = -1;
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos").deleteRow(i);
                    } else {
                    if (aFila[i].getAttribute("bd") == "I") {//Para los AEs insertados actualizo su id con el código creado 
                            idNuevoAE = getIdAE(aFila[i].id, aResul[2]);
                            if (idNuevoAE != "") {
                                if (aFila[i].className == "FS") idAE = idNuevoAE;
                                //aFila[i].onclick = function (){mmse(this);refrescarVAEs(this.id)};
                                //Actualizo en el array de valores de los criterios estadísticos el ID del AE
                                for (var j = aVAES.length - 1; j >= 0; j--) {
                                    if (aVAES[j].idAE < 0 && aVAES[j].idAE == aFila[i].id) {
                                        aVAES[j].idAE = idNuevoAE;
                                    }
                                }
                                aFila[i].id = idNuevoAE;
                            }
                        }
                        mfa(aFila[i], "N");
                    }
                }
                for (var i = 0; i < aFila.length; i++) {
                    aFila[i].setAttribute("orden", i) ;
                    aFila[i].className = "";
                }
                refrescarVAEs("*");
                eval(aResul[3]);
                nFilaDesde = -1;
                nFilaHasta = -1;
                desActivarGrabar();
                mmoff("Suc","Grabación correcta", 160); 
                if (bCambioNodo) {
                    bCambioNodo = false;
                    setTimeout("getNodo();", 50);
                }
                if (bCambioNodoCbo) {
                    bCambioNodoCbo = false;
                    setTimeout("setCombo();", 50);
                }
                if (bBorrarNodo) {
                    bBorrarNodo = false;
                    setTimeout("borrarNodo();", 50);
                }
                if (bCambioCliente) {
                    bCambioCliente = false;
                    setTimeout("getCliente();", 50);
                }
                if (bBorrarCliente) {
                    bBorrarCliente = false;
                    setTimeout("borrarCli();", 50);
                }
                break;
        }
        ocultarProcesando();
    }
}
//function getVAEs(){
//    try{
//        var js_args = "getVAEs@#@" + sValorNodo + "@#@" + sAmbito ;
//        mostrarProcesando();
//        RealizarCallBack(js_args, "");
//	}catch(e){
//		mostrarErrorAplicacion("Error al obtener los datos de los valores", e.message);
//    }
//}
function preEliminarAE(){
    try{
        var aFila = FilasDe("tblDatos");
        if (aFila == null){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }
        if (aFila.length == 0){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }

        var sw = 0;
        var strFilas = "";
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw = 1;
                if (aFila[i].getAttribute("bd") == "I") $I("tblDatos").deleteRow(i);
                else{
                    mfa(aFila[i], "D");
                    strFilas += aFila[i].id +"##"
                }
            }
        }
        if (sw == 0){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }
        else{
            if (strFilas != ""){
                strFilas = strFilas.substring(0, strFilas.length-2);
                var js_args = "preEliminarAE@#@";
                js_args += strFilas; //IDs del AEs a eliminar
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
        }
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al intentar eliminar criterio estadístico", e.message);
    }
}
function eliminarAE(){
    try{
        var aFila = FilasDe("tblDatos");
        if (aFila == null){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }
        if (aFila.length == 0){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }
        var sw = 0;
        var strFilas = "";
        for(var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                sw = 1;
                strFilas += aFila[i].id +"##"
            }
        }
        if (sw == 0){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }else strFilas = strFilas.substring(0, strFilas.length-2);
        
        var js_args = "eliminar@#@";
        js_args += strFilas; //IDs del AEs a eliminar
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar criterio estadístico", e.message);
    }
}
function nuevoAE(){
    try{
        if (sValorNodo == ""){
            mmoff("War", "Debes indicar un " + $I("lblNodo").innerText, 300);
            return;
        }
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
        bNuevo = true;
        oNF = $I("tblDatos").insertRow(-1);
        var iFila=oNF.rowIndex;

        oNF.id = idAEnew--;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("orden", oNF.rowIndex);
        oNF.setAttribute("cli", $I("hdnIdCliente").value);

        oNF.attachEvent('onclick', mm);
        oNF.onclick = function (){refrescarVAEs(this.id)};

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        oNF.insertCell(-1).appendChild(oImgMR.cloneNode(true));
        oNF.cells[1].children[0].ondragstart = 'return false;'

        var oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.id = "txtDesc";
        oCtrl1.className = "txtL";
        oCtrl1.setAttribute("class", "txtL");
        oCtrl1.setAttribute("style", "width:290px");
        oCtrl1.setAttribute("maxLength", "30");
        oCtrl1.onkeyup = function() { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl1);            
        
        var oCtrl2 = document.createElement("input");
        oCtrl2.type = "checkbox";
        oCtrl2.checked = true;
        oCtrl2.setAttribute("checked", "true");
        oCtrl2.className = "checkTabla";
        oCtrl2.setAttribute("style", "width:15px");
        oCtrl2.onclick = function() { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl2);
        oNF.cells[3].style.textAlign = "center";
        
        var oCtrl3 = document.createElement("input");
        oCtrl3.type = "checkbox";
        oCtrl3.className = "checkTabla";
        oCtrl3.setAttribute("style", "width:15px");
        oCtrl3.onclick = function() { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl3);
        oNF.cells[4].style.textAlign = "center";
        
        //oNF.insertCell().appendChild(document.createElement("<input type='checkbox' style='width:15' class='checkTabla' onclick='fm(this)'>"));

        var oCtrl4 = document.createElement("span");
        oCtrl4.className = "NBR W340";
        oNF.insertCell(-1).appendChild(oCtrl4);  
        
        //oNF.insertCell().appendChild(document.createElement("<nobr class='NBR W345'></nobr>"));

        if (oNF.getAttribute("cli") == "")
        {
            //oNF.cells[5].style.cursor = "url('../../../../../images/imgManoAzul2.cur')";
            oNF.cells[5].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
            oNF.cells[5].style.backgroundRepeat = "no-repeat";
        }
        else{
            oNF.cells[5].children[0].innerText=$I("txtDesCliente").value;
        }
        oNF.cells[5].className = "MA";

        oNF.cells[5].ondblclick = function(){getClienteAE(this.parentNode);};
        
        scrollTablaAE();

        ms(oNF);
        //seleccionar(oNF);
        refrescarVAEs(oNF.id)
	    oNF.cells[2].children[0].focus();
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo registro", e.message);
    }
}
//function mostrarDetalle(strID){
//    try{
//        location.href= "../Detalle/Default.aspx?nIDAE="+ strID+"&nCR="+$I("cboCR").value + "&A=" + sAmbito;
//	}catch(e){
//		mostrarErrorAplicacion("Error al mostrar el detalle", e.message);
//    }
//}
function buscar(){
    try{
        if (sValorNodo != ""){
            var js_args = "buscar@#@"+ sValorNodo + "@#@" + sAmbito + "@#@" + $I("hdnIdCliente").value;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener criterios estadísticos del nodo", e.message);
    }
}

function comprobarDatos(){
    try{
        var nOrden=0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].className == "FS") idAE=aFila[i].id;
            if (aFila[i].cells[2].children[0].value == ""){
                mmoff("War","Debes indicar la descripción del criterio estadístico",330);
                return false;
            }

            if (aFila[i].getAttribute("orden") != nOrden) {
                if (aFila[i].getAttribute("bd") != "I") aFila[i].setAttribute("bd", "U");
                aFila[i].setAttribute("orden", nOrden);
            }
            nOrden++;
        }
        //Control de valores de atributos estadísticos//
//        aFilaVAE = FilasDe("tblDatosVAE");
//        var nOrden=0;
//        for (var i=0;i<aFilaVAE.length;i++){
//            if (aFilaVAE[i].bd != "D"){
//                if (aFilaVAE[i].cells[2].children[0].value == ""){
//                    alert("Debe indicar la descripción del valor del criterio estadístico");
//                    aFilaVAE[i].cells[2].children[0].focus();
//                    return false;
//                }
//                if (aFilaVAE[i].cells[3].children[0].value == "") aFilaVAE[i].cells[2].children[0].value ="0";
//                if (aFilaVAE[i].orden != nOrden){
//                    if (aFilaVAE[i].bd != "I") aFilaVAE[i].bd = "U";
//                    aFilaVAE[i].orden = nOrden;
//                }
//                nOrden++;
//                
//            }
//        }
        //Control de que los valores de los atributos estadísticos tienen descripción
        var nOrden=0;
        var sNomAE="";
        var idAEant="";
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].idAE != idAEant){
                idAEant = aVAES[nIndice].idAE;
                nOrden=0;
            }
            if (aVAES[nIndice].bd != "D"){
                if (aVAES[nIndice].nombre == ""){
                    for (var i=0; i<aFila.length; i++){
                        if (aFila[i].id == aVAES[nIndice].idAE){
                            sNomAE = aFila[i].cells[2].children[0].value;
                            break;
                        }
                    }
                    mmoff("War","Debes indicar la descripción del valor del criterio estadístico " + sNomAE,460);
                    return false;
                }
//                if (aVAES[nIndice].orden != nOrden){
//                    if (aVAES[nIndice].bd != "I") aVAES[nIndice].bd = "U";
//                    aVAES[nIndice].orden = nOrden;
//                }
            }
            nOrden++;               
        }
        //Control de que los AEs obligatorios tienen al menos un VAE activo
        var sw=0;
        var sError="";
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D") continue;
            idAE=aFila[i].id;
            sNomAE = aFila[i].cells[2].children[0].value;
            sw=0;
            if (aFila[i].cells[3].children[0].checked){
                for (var nIndice=0; nIndice < aVAES.length; nIndice++){
                    if (aVAES[nIndice].idAE == idAE){
                        if (aVAES[nIndice].estado == 1){
                            sw++;
                            break;
                        }
                    }
                }
                if (sw==0){
                    sError+="El criterio estadístico " + sNomAE + " debe tener valores activos.\n";
                }
            }
        }
        if (sError != ""){
            mmoff("War",sError,380);
            return false;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function grabar(){
    try{
        aFila = FilasDe("tblDatos");
        
        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }
        var sb = new StringBuilder; //sin paréntesis 

        //sb.Append("grabar@#@"+idAE+"@#@");
        sb.Append("grabar@#@");
        var sw = 0, nEstado = 0, nObli=0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); 
                //Denominación del AE
                sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) +"##"); 
                
                nEstado = 0;
                if (aFila[i].cells[3].children[0].checked) nEstado = 1; 
                sb.Append(nEstado +"##");

                nObli=0;
                if (aFila[i].cells[4].children[0].checked) nObli = 1; 
                sb.Append(nObli +"##");
                
                sb.Append(sValorNodo +"##");
                sb.Append(aFila[i].getAttribute("cli") + "##"); //cliente
                sb.Append(aFila[i].getAttribute("orden") +"##"); 
                sb.Append(sAmbito +"///"); 
                sw = 1;
            }
        }
//        if (sw == 0){
        //            mmoff("Inf","No se han modificado los datos.",200);
//            ocultarProcesando();
//            return;
//        }
        //Control de valores de atributos estadísticos//
        sb.Append("@#@");
        sw = 0;
//        aFilaVAE = FilasDe("tblDatosVAE");
//        for (var i=0;i<aFilaVAE.length;i++){
//            if (aFilaVAE[i].bd != ""){
//                sw = 1;
//                sb.Append(aFilaVAE[i].bd +"##"); 
//                sb.Append(idAE +"##"); //nº AE
//                sb.Append(aFilaVAE[i].id +"##"); //nº VAE
//                sb.Append(Utilidades.escape(aFilaVAE[i].cells[2].children[0].value) +"##"); //Valor
//                sb.Append(aFilaVAE[i].orden +"##"); //Orden
//                if (aFilaVAE[i].cells[3].children[0].checked) 
//                    sb.Append("1///"); //Activo
//                else 
//                    sb.Append("0///"); //Activo
//            }
//        }
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].bd != ""){
                sb.Append(aVAES[nIndice].bd +"##"); 
                sb.Append(aVAES[nIndice].idAE +"##"); //nº AE
                sb.Append(aVAES[nIndice].idVAE +"##"); //nº VAE
                //sb.Append(Utilidades.escape(aVAES[nIndice].nombre) +"##"); //Valor
                sb.Append(aVAES[nIndice].nombre +"##"); //Valor
                sb.Append(aVAES[nIndice].orden +"##"); //Orden
                sb.Append(aVAES[nIndice].estado +"///"); //Activo
            }
        }
        //if (sw == 1) js_args = js_args.substring(0, js_args.length-3);
        sb.Append("@#@"+ sValorNodo + "@#@" + sAmbito);
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
var bCambioNodoCbo = false;
function setCombo(){
    try{
        if (bCambios) {
            jqConfirm("", "Existen cambios pendientes de grabación.<br><br>¿Deseas guardarlos?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bCambioNodoCbo = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarSetCombo();
                }
            });
        } else LLamarSetCombo();
    }catch(e){
        mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function LLamarSetCombo() {
    try {
        sValorNodo = $I("cboCR").value;
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
        if (sValorNodo != "")
            $I("lblCliente").className = "enlace";
        else
            $I("lblCliente").className = "texto";
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
var bCambioNodo = false;
function getNodo(){
    try{
        if ($I("lblNodo").className == "texto") return;
        if (bCambios) {
            jqConfirm("", "Existen cambios pendientes de grabación.<br><br>¿Deseas guardarlos?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bCambioNodo = true;
                    grabar();
                }
                else
                    LLamarGetNodo();
            });
        }
        else LLamarGetNodo();
    }
    catch (e) {
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
                    $I("gomaNodo").style.visibility = "visible";
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
                    $I("lblCliente").className = "enlace";
                    buscar();
                } else {
                    $I("lblCliente").className = "texto";
                    ocultarProcesando();
                }
            });
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo-2", e.message);
    }
}
var bBorrarNodo = false;
function borrarNodo(){
    try{
        if (bCambios) {
            jqConfirm("", "Existen cambios pendientes de grabación.<br><br>¿Deseas guardarlos?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bBorrarNodo = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarBorrarNodo();
                }
            });
        } else LLamarBorrarNodo();
    }catch(e){
        mostrarErrorAplicacion("Error al obtener el nodo-2", e.message);
    }
}
function LLamarBorrarNodo() {
    try {
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
             $I("gomaNodo").style.visibility="hidden";
        }
        else {
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        idAE="";
        $I("lblCliente").className = "texto";
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
        $I("divValores").children[0].innerHTML = "<table id='tblDatosVAE'></table>";
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el nodo", e.message);
    }
}
var bCambioCliente = false;
function getCliente(){
    try{
        if (bCambios) {
            jqConfirm("", "Existen cambios pendientes de grabación.<br><br>¿Desea guardarlos?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bCambioCliente = true;
                    grabar();
                    return;
                }//else desActivarGrabar();
                LLamarGetCliente();
            });
        } else LLamarGetCliente();
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los clientes-1", e.message);
    }
}
function LLamarGetCliente() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    $I("gomaCli").style.visibility = "visible";
                    buscar();
                }
            });
        window.focus();	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes-2", e.message);
    }
}
var bBorrarCliente = false;
function borrarCli(){
    try{
        $I("gomaCli").style.visibility="hidden";
        if ($I("hdnIdCliente").value !=""){
            if (bCambios) {
                jqConfirm("", "Existen cambios pendientes de grabación.<br><br>¿Desea guardarlos?", "", "", "war", 350).then(function (answer) {
                    if (answer) {
                        bBorrarCliente = true;
                        grabar();
                        return;
                    } else desActivarGrabar();
                    LLamarBorrarCli();
                });
            } else LLamarBorrarCli();
        }
    }catch(e){
        mostrarErrorAplicacion("Error al borrar los clientes-1", e.message);
    }
}
function LLamarBorrarCli() {
    try {
            mostrarProcesando();
            $I("hdnIdCliente").value ="";
            $I("txtDesCliente").value = "";
            $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
            buscar();
            ocultarProcesando();
	}catch(e){
	    mostrarErrorAplicacion("Error al borrar los clientes-2", e.message);
    }
}
function getClienteAE(oFila){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.cli = aDatos[0];
                    oFila.cells[5].style.backgroundImage = "";
                    oFila.cells[5].children[0].innerText = aDatos[1];

                    var oGoma = oGomaPerfil.cloneNode(true);
                    oGoma.onclick = function() { borrarCliente(this.parentNode.parentNode); };
                    oGoma.style.cursor = "pointer";
                    oFila.cells[5].appendChild(oGoma, null);

                    oFila.setAttribute("sw", "0");
                    mfa(oFila, "U");
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
	    oFila.cells[5].style.backgroundImage = "url('../../../../../images/imgOpcional.gif')";
        oFila.cells[5].style.backgroundRepeat = "no-repeat";
	    oFila.cells[5].children[0].innerText = "";
	    if (oFila.cells[5].children[1] != null) oFila.cells[5].removeChild(oFila.cells[5].children[1]);
	    //oFila.cells[5].style.cursor = "url('../../../../../images/imgManoAzul2.cur')";
	    oFila.cells[5].className = "MA";
	    mfa(oFila, "U");
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cliente al criterio estadístico", e.message);
	}
}


//var oImgMoveRow = document.createElement("img");
//oImgMoveRow.setAttribute("src", "../../../../../images/imgMoveRow.gif");
//oImgMoveRow.setAttribute("style", "cursor:row-resize;margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;");
//oImgMoveRow.setAttribute("title","Pinchar y arrastrar para ordenar"); 
 
var oGomaPerfil = document.createElement("img");
oGomaPerfil.setAttribute("src", "../../../../../images/botones/imgBorrar.gif");
oGomaPerfil.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

//var oGomaPerfil = document.createElement("<img src='../../../../../images/botones/imgBorrar.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

var nTopScrollAE = -1;
var nIDTimeAE = 0;
function scrollTablaAE(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollAE){
            nTopScrollAE = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeAE);
            nIDTimeAE = setTimeout("scrollTablaAE()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        var nFilaVisible = Math.floor(nTopScrollAE/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                //oFila.onclick = function() { ms(this);}
                if (oFila.getAttribute("cli") == "") {
                    oFila.cells[5].style.backgroundImage = "url(../../../../../images/imgOpcional.gif)";
                    oFila.cells[5].style.backgroundRepeat = "no-repeat";
                }else{
                    var oGoma = oGomaPerfil.cloneNode(true);
                    oGoma.onclick = function(){borrarCliente(this.parentNode.parentNode);};
                    oGoma.style.cursor = "pointer";
                    oFila.cells[5].appendChild(oGoma, null);
                }
                //oFila.cells[5].style.cursor = "url('../../../../../images/imgManoAzul2.cur')";
                oFila.cells[5].className = "MA";
                oFila.cells[5].ondblclick = function(){getClienteAE(this.parentNode);};
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}


//////////////////////////////////////////////////////////////////
// FUNCIONES PARA LA TABLA DE VALORES DE CRITERIOS ESTADISTICOS //
//////////////////////////////////////////////////////////////////
function nuevoVAE(){
    try{
        if (idAE == ""){
            mmoff("War", "Para insertar valores, debe seleccionar el criterio estadístico", 400);
            return;
        }
        oNF = $I("tblDatosVAE").insertRow(-1);
        var iFila=oNF.rowIndex;

        oNF.id = idVAEnew;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("orden", oNF.rowIndex);       
        oNF.style.height = "20px";

        oNF.attachEvent('onclick', mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1).appendChild(oImgMR.cloneNode(true));
        oNF.cells[1].children[0].ondragstart = 'return false;'
        oNF.cells[1].children[0].setAttribute("style", "margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;");
        
        var oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.id = "txtVAE";
        oCtrl1.className = "txtL";
        oCtrl1.setAttribute("class", "txtL");
        oCtrl1.setAttribute("style", "width:230px");
        oCtrl1.setAttribute("maxLength", "25");

        oCtrl1.onkeyup = function() { actualizarDatos('U', 'nombre', this); };
        oCtrl1.onfocus = function() { this.className='txtM';this.select(); };
        oCtrl1.onblur = function() { this.className='txtL' };

        oNF.insertCell(-1).appendChild(oCtrl1);            
        
        //oNF.insertCell().appendChild(document.createElement("<input type='text' id='txtVAE' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:240px' value='' onKeyUp=\"actualizarDatos('U','nombre',this);\" MaxLength='25'>"));

        var oCtrl2 = document.createElement("input");
        oCtrl2.type = "checkbox";
        oCtrl2.checked = true;
        oCtrl2.setAttribute("checked", "true");
        oCtrl2.className = "checkTabla";
        oCtrl2.setAttribute("style", "width:15px");
        oCtrl2.onclick = function() { actualizarDatos('U', 'estado', this); };
        oNF.insertCell(-1).appendChild(oCtrl2);
        oNF.cells[3].style.textAlign = "center";
        
	    //oNF.insertCell().appendChild(document.createElement("<input type='checkbox' class='checkTabla' checked onClick=\"actualizarDatos('U','estado',this);\">"));

        ms(oNF);
	    oNF.cells[2].children[0].focus();
        //indiceFila++;
        insertarVAEEnArray("I", idAE, idVAEnew--, "", 1, iFila);
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function eliminarVAE(){
    try{
        var sw = 0;
        aFilaVAE = FilasDe("tblDatosVAE");
        if (aFilaVAE == null){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }
        if (aFilaVAE.length == 0){
            mmoff("War", "Selecciona la fila a eliminar", 200);
            return;
        }
        for (var i=aFilaVAE.length-1; i>=0; i--){
            if (aFilaVAE[i].className == "FS"){
                sw = 1;
                if (aFilaVAE[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    borrarVAEDeArray(aFilaVAE[i].id);
                    $I("tblDatosVAE").deleteRow(i);
                }    
                else{
                    mfa(aFilaVAE[i], "D");
                    oVAEActivo = buscarVAEEnArray(aFilaVAE[i].id);
                    if (oVAEActivo.bd != "I") 
                         oVAEActivo.bd="D";
                    else 
                        borrarVAEDeArray(aFilaVAE[i].id);
                }
            }
        }
        if (sw == 0) mmoff("War", "Selecciona la fila a eliminar", 200);
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function actualizarDatos(accion, clave, obj){
    try{
        var oFila = obj.parentNode.parentNode;
        //if (oFila.bd != "I") oFila.bd = "U";
        fm_mn(oFila);
        activarGrabar();
        oVAEActivo = buscarVAEEnArray(oFila.id);
        oVAEActualizar(accion, clave, obj)
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los datos", e.message);
    }
}
function refrescarVAEs(id){
    try{
        if (id == "*"){
            $I("divValores").children[0].innerHTML = "<table id='tblDatosVAE'></table>";
            return;
        }
        idAE=id;
        var sb = new StringBuilder;
        var aVAEord = new Array();
        var sImagen="imgFN.gif";
        sb.Append("<table id='tblDatosVAE' class='texto MANO' style='width:320px; text-align:left;' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:15px;' /><col style='width:18px;' /><col style='width:240px;' /><col style='width:47px;' /></colgroup>");
        sb.Append("<tbody id='tbodyDatosVAE'>");
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].idAE==id){
                var sb2 = new StringBuilder;
                sb2.Append("<tr id='" + aVAES[nIndice].idVAE + "' style='height:20px' bd='"+aVAES[nIndice].bd+"' orden='" + aVAES[nIndice].orden + "' onclick='ms(this)' >");
                switch(aVAES[nIndice].bd){
                    case "I":
                        sImagen="imgFI.gif";
                        break;
                    case "U":
                        sImagen="imgFU.gif";
                        break;
                    case "D":
                        sImagen="imgFD.gif";
                        break;
                    default:
                        sImagen="imgFN.gif";
                        break;
                }
                sb2.Append("<td><img src='../../../../../images/"+sImagen+"'></td>");
                sb2.Append("<td><img src='../../../../../images/imgMoveRow.gif' style='cursor:row-resize; margin-right:2px;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                sb2.Append("<td><input type='text' id='txtVAE' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:240px' value='" + Utilidades.unescape(aVAES[nIndice].nombre) + "' onKeyUp=\"actualizarDatos('U','nombre',this);\" MaxLength='25'></td>");
                if (aVAES[nIndice].estado=="1")
                    sb2.Append("<td style='text-align:center;'><input type='checkbox' class='checkTabla' id='chkEstado' checked onClick=\"actualizarDatos('U','estado',this);\"></td>");
                else
                    sb2.Append("<td style='text-align:center;'><input type='checkbox' class='checkTabla' id='chkEstado' onClick=\"actualizarDatos('U','estado',this);\"></td>");
                sb2.Append("</tr>");
                //Puede que haya registros con el mismo orden por lo que si no lo tratamos se machacarían valores en el array
                var iOrd=aVAES[nIndice].orden;
                for (var i=iOrd; i < 32000; i++){
                    if (aVAEord[i]==null){
                        aVAEord[i]=sb2.ToString();
                        break;
                    }
                }
            }
        }
        sb.Append(aVAEord.join(''));
        sb.Append("</tbody>");
        sb.Append("</table>");

        $I("divValores").children[0].innerHTML = sb.ToString();
        tbodyVAE = document.getElementById('tbodyDatosVAE'); 
        tbodyVAE.onmousedown = startDragIMGvae; 
	}catch(e){
		mostrarErrorAplicacion("Error al refrescar los valores", e.message);
    }
}
function restaurarFila2(){
    try{
        //Miro si estoy restaurando una fila de AE o de VAE
        var sAux = oFilaARestaurar.innerHTML;
        var intPos = sAux.indexOf("W345");
        if (intPos >= 0){return;}//estoy restaurando un AE luego no hago nada con el array
        else{
            oVAEActivo = buscarVAEEnArray(oFilaARestaurar.id);
            if (oVAEActivo != null)
                oVAEActivo.bd="U";
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al restaurar la fila", e.message);
    }
}
function getIdAE(idOld, sCadena){
    try{
        var sElem, sRes="";
        var aLista = sCadena.split("@@");
        for (var i=0;i<aLista.length;i++){
            sElem = aLista[i];
            if (sElem != ""){
                var aAE = sElem.split("##");
                if (aAE[0] == idOld){
                    sRes = aAE[1];
                    break;
                }
            }
        }
        return sRes;
    }catch(e){
	    mostrarErrorAplicacion("Error al buscar el código del AE insertado", e.message);
    }
}
