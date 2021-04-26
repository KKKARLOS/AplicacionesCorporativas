var aTipo;
var aFila;
var sElementosInsertados = "";
var bHayFactura=false;
var bGrabando = false;
var bBorrar = false;

function init(){
    try{
        $I("txtSerie").focus();
        setExcelImg("imgExcel", "divCatalogo");
        $I("imgExcel_exp").style.left = "975px";
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function buscar1(){
    try{
        if ($I("txtSerie").value == ""){
            $I("txtSerie").focus();
            return;
        }
        if ($I("txtNum").value == ""){
            $I("txtNum").focus();
            return;
        }
        buscar();
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener líneas de la factura", e.message);
    }
}
function buscar(){
    try{
        mostrarProcesando();
        var js_args = "getLineas@#@"; 
        if ($I("txtSerie").value != ""){
            if ($I("txtNum").value != ""){
                    js_args+= $I("txtSerie").value + "@#@" + $I("txtNum").value;
                    RealizarCallBack(js_args, "");
            }
            else{
                ocultarProcesando();
                mmoff("Inf","Debes indicar nº de factura",220);
            }
        }
        else {
            ocultarProcesando();
                mmoff("Inf", "Debes indicar serie de factura",230)
        }       
       	}catch(e){
		mostrarErrorAplicacion("Error al obtener líneas de la factura", e.message);
    }
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function borrarSyN(){
    try {
        if (bGrabando) return;

        if (bCambios && intSession > 1) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war",330).then(function (answer) {
                if (answer) {
                    bBorrar = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    borrarSyN_Continuar();
                }
            });
        }
        else borrarSyN_Continuar();

	}catch(e){
		mostrarErrorAplicacion("Error al limpiar", e.message);
	}
}
function borrarSyN_Continuar(){
    try {

        $I("txtSerie").readOnly = false;
        $I("txtNum").readOnly = false;
        $I("txtSerie").value = "";
        $I("txtNum").value = "";
        $I("divCatalogo").children[0].innerHTML = "";
        $I("tblTotal").rows[0].cells[1].innerText = "";
        $I("tblTotal").rows[0].cells[3].innerText = "";
        $I("tblTotIni").rows[0].cells[1].innerText = "";
        $I("tblTotIni").rows[0].cells[3].innerText = "";
        $I("txtSerie").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar", e.message);
    }
}
var nId = 100000;
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        bGrabando = false;
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getLineas":
                eval(aResul[8])
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                if (aResul[3]=="T"){
                    bHayFactura=true;
                    $I("txtSerie").readOnly=true;
                    $I("txtNum").readOnly=true;
                    $I("hdnAnoMes").value = aResul[6];
                    $I("hdnClaseEco").value = aResul[7];
                }
                else{
                    bHayFactura=false;
                    $I("hdnAnoMes").value = "";
                    $I("hdnClaseEco").value = "";
                    ocultarProcesando();
                    mmoff("Inf","Factura no encontrada", 200);
                }
                $I("tblTotal").rows[0].cells[1].innerText=aResul[4];
                $I("tblTotal").rows[0].cells[3].innerText=aResul[5];
                $I("tblTotIni").rows[0].cells[1].innerText=aResul[4];
                $I("tblTotIni").rows[0].cells[3].innerText=aResul[5];

                //desactivarGrabar();
                AccionBotonera("grabar", "D");
                bCambios = false;
                if (bGrabando){
                    bGrabando=false;
                    ocultarProcesando();
                    mmoff("Suc", "Grabación correcta", 160);
                }
                break;
            case "grabar":
//                sElementosInsertados = aResul[2];
//                var aEI = sElementosInsertados.split("//");
//                aEI.reverse();
//                var nIndiceEI = 0;
//                for (var i=aFila.length-1; i>=0; i--){
//                    if (aFila[i].getAttribute("bd") == "D"){
//                        $I("tblDatos").deleteRow(i);
//                        continue;
//                    }else if (aFila[i].getAttribute("bd") == "I"){
//                        aFila[i].id = aEI[nIndiceEI]; 
//                        nIndiceEI++;
//                    }
//                    mfa(aFila[i],"N");
//                }
//                desActivarGrabar();
                //mmoff("Grabación correcta", 200);
                bGrabando=true;
                if (bBorrar) {
                    bBorrar = false;
                    borrarSyN_Continuar();
                }
                //Para poder recargar el reparto de cobros voy al servidor
                setTimeout("buscar();", 50);
                break;
            case "recuperarPSN":
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto no existe o está fuera de su ámbito de visión.", 400);
                    break;
                }
                oNF = $I("tblDatos").insertRow(-1);
                //oNF.id = oNF.rowIndex;
                oNF.id = nId;
                nId++;
                
                oNF.style.height = "20px";
                oNF.setAttribute("iPSN", aResul[2]);
                oNF.setAttribute("mp", aResul[8]);
                oNF.setAttribute("bd", "I");
                oNF.setAttribute("orden", oNF.rowIndex);

                oNF.attachEvent("onclick", mm);

                oNC1 = oNF.insertCell(-1);

                oNC1.appendChild(oImgFI.cloneNode(true));

                oNC2 = oNF.insertCell(-1);
                switch (aResul[4]) {
                    case "P":
                        oNC2.appendChild(oImgProducto.cloneNode(true));
                        break;
                    default:
                        oNC2.appendChild(oImgServicio.cloneNode(true));
                        break;
                }

                switch (aResul[5]) {
                    case "C":
                        oNC2.appendChild(oImgContratante.cloneNode(true));
                        break;
                    case "J":
                        oNC2.appendChild(oImgRepJor.cloneNode(true));
                        break;
                    case "P":
                        oNC2.appendChild(oImgRepPrecio.cloneNode(true));
                        break;
                }

                switch (aResul[3]) {
                    case "A":
                        oNC2.appendChild(oImgAbierto.cloneNode(true));
                        break;
                    case "C":
                        oNC2.appendChild(oImgCerrado.cloneNode(true));
                        break;
                    case "H":
                        oNC2.appendChild(oImgHistorico.cloneNode(true));
                        break;
                    case "P":
                        oNC2.appendChild(oImgPresup);
                        break;
                }

                var oCtrl1 = document.createElement("div");
                oCtrl1.setAttribute("style", "margin-left:5px;");
                oCtrl1.className = "NBR W190";
                oCtrl1.appendChild(document.createTextNode(aResul[6]));
                oNC2.appendChild(oCtrl1);

                //oNF.cells[1].children[3].innerText = aResul[6];

                var oCtrl2 = document.createElement("input");

                oCtrl2.type = "text";
                oCtrl2.style.width = "80px";
                oCtrl2.className = "txtNumL";
                oCtrl2.value = "0,00";

                oCtrl2.attachEvent("onkeyup", vtn);
                oCtrl2.onkeyup = function() { fm_mn(this); recTotal(); setImpMoneda(this); };
                oCtrl2.onfocus = function() { fn(this) };

                oNF.insertCell(-1).appendChild(oCtrl2);

                //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtNumL' style='width:80px' value='0,00' onKeyUp='vtn();fm(event);recTotal();setImpMoneda(this);' onfocus='fn(this)'>"));
                oNF.insertCell(-1); //Importe en moneda proyecto
                oNF.cells[3].align = "right";
                oNF.cells[3].innerText = "0,00";
                oNF.insertCell(-1); //Cobro en euros
                oNF.cells[4].align = "right";
                oNF.cells[4].innerText = "0,00";
                oNF.insertCell(-1); //Cobro en moneda proyecto
                oNF.cells[5].align = "right";
                oNF.cells[5].innerText = "0,00";
                oNF.insertCell(-1); //Moneda proyecto
                oNF.cells[6].innerText = aResul[7];

                //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtL' style='width:40px'>"));
                oNF.insertCell(-1); //Año
                oNF.insertCell(-1); //Mes
                oNF.insertCell(-1); //Cliente proyecto
                oNF.insertCell(-1); //Cliente factura
                oNF.insertCell(-1); //Motivo

                ms(oNF);

                oNF.cells[2].children[0].focus();
                activarGrabar();
                break;
        }
        ocultarProcesando();
    }
}

function recuperarDatosPSN(idPSN){
    try{
        var js_args = "recuperarPSN@#@" + idPSN;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
function getAuditoriaAux() {
    try {
        if ($I("txtSerie").value == "" && $I("txtNum").value == "") return;
        getAuditoria(10, $I("txtSerie").value + "-" + $I("txtNum").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}
function nuevo(){
    try{
//        alert("Pdte de resolver dudas de implementación");
//        return;
        if ($I("txtSerie").value == ""){
            mmoff("War", "Debes indicar serie de factura",230);
            $I("txtSerie").focus();
            return false;
        }
        if ($I("txtNum").value == ""){
            mmoff("War", "Debes indicar número de factura",230);
            $I("txtNum").focus();
            return false;
        }
        if (!bHayFactura)return;
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        //Selecciono el proyecto al que quiero asignar la nueva fila
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge";
	    //window.focus();
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                recuperarDatosPSN(aDatos[0]);
	            }
	        });
	    
        ocultarProcesando();
        
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}

function eliminar(){
    try{
        if ($I("txtSerie").value == ""){
            mmoff("War", "Debes seleccionar la serie de factura a eliminar",330);
            $I("txtSerie").focus();
            return false;
        }
        if ($I("txtNum").value == ""){
            mmoff("War", "Debes seleccionar el número de factura a eliminar", 330);
            $I("txtNum").focus();
            return false;
        }
        if (!bHayFactura){
            mmoff("War", "Debes seleccionar la línea de factura a eliminar", 330);
            return;
        }
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        var sw=0;
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw=1;
                if (aFila[i].getAttribute("bd") == "I") {
                    $I("tblDatos").deleteRow(i);
                }else{
                    mfa(aFila[i], "D");
                }
            }
        }
        if (sw==1){
            recTotal();
            activarGrabar();
        }
        else{
            mmoff("War", "Debes seleccionar la línea de factura a eliminar", 330);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}

function grabar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        if (bGrabando) return;

        aFila = FilasDe("tblDatos");
        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }

        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@"+ $I("txtSerie").value + "@#@" + $I("txtNum").value + "@#@" + $I("tblTotIni").rows[0].cells[1].innerText + "@#@" + $I("hdnAnoMes").value + "@#@" + $I("hdnClaseEco").value + "@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") +"##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID datoEco
                sb.Append(aFila[i].getAttribute("iPSN") +"##"); //ID ProyectoSubnodo
                //sb.Append(Utilidades.escape(aFila[i].cells[1].children[3].value) +"##"); //Descripcion proyecto
                //sb.Append((aFila[i].cells[2].children[0].value != "") ? aFila[i].cells[2].children[0].value + "///" : "0///"); //importe linea factura
                //importe linea factura en moneda proyecto
                sb.Append((aFila[i].cells[2].children[0].value != "") ? aFila[i].cells[2].children[0].value + "///" : "0///"); 
                sw = 1;
            }
        }
        if (sw == 0){
            mmoff("War", "No se han modificado los datos.", 230);
            ocultarProcesando();
            return;
        }
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos(){
    try{
        if ($I("txtSerie").value == ""){
            mmoff("War", "Debes indicar serie de factura", 230);
            $I("txtSerie").focus();
            return false;
        }
        if ($I("txtNum").value == ""){
            mmoff("War", "Debes indicar número de factura", 230);
            $I("txtNum").focus();
            return false;
        }
        if ($I("tblTotal").rows[0].cells[1].innerText != $I("tblTotIni").rows[0].cells[1].innerText){
            mmoff("War", "El importe actual de la factura no coincide con el importe inicial",390);
            return false;
        }
        
//        for (var i=0; i<aFila.length; i++){
//            if (aFila[i].getAttribute("bd") == "D") continue;
//            if (aFila[i].cells[1].children[0].value == ""){
//                msse(aFila[i]);
//                alert("Debe indicar la denominación de la categoría");
//                aFila[i].cells[1].children[0].focus();
//                return false;
//            }
//            if (aFila[i].cells[2].children[0].value=="") aFila[i].cells[2].children[0].value="0,00";
//            if (aFila[i].cells[3].children[0].value=="") aFila[i].cells[3].children[0].value="0,00";
//        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function setImpMoneda(oImpEu){
    var oFila = oImpEu.parentNode.parentNode;
    //var TipoCambio = getTipoCambio(oFila.cells[6].innerHTML);
    var importeMP = parseFloat(dfn(oImpEu.value)) * parseFloat(dfn(getTipoCambio(oFila.cells[6].innerHTML)));
    oFila.cells[3].innerHTML = importeMP.ToString("N", 10, 2);
}
function getTipoCambio(sMoneda) {
    var sRes = "";
    for (var i = 0; i < aTipo.length; i++) {
        if (aTipo[i].idM == sMoneda) {
            sRes = aTipo[i].tc;
            break;
        }
    }
    if (sRes == "")
        mmoff("Inf", "Atención!\n\nNo hay tipo de cambio activo para la moneda " + sMoneda,220);
    return sRes;
}
function recTotal(){
    try{
        var sCad;
        var fAux=0, fTotal=0;
        //no tener en cuenta las filas marcadas para borrar
        aFila = FilasDe("tblDatos");
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D") continue;
            sCad=getCelda(aFila[i],2);
            if (sCad=="") fAux=0;
            else fAux=parseFloat(dfn(sCad));
            if (fAux != 0){
                fTotal += fAux;
            }
        }
        $I("tblTotal").rows[0].cells[1].innerText = fTotal.ToString("N");
    }
	catch(e){
		mostrarErrorAplicacion("Error al calcular totales", e.message);
        return false;
    }
}
function restaurarFila2(){
    try{
        recTotal();
        
    }catch(e){
	    mostrarErrorAplicacion("Error al restaurar la fila", e.message);
    }
}
function excel() {
    try {
        var sImg, sOcupacion, sCad;
        var iInd, iInd2;
        var bAcciones = false;

        if ($I("divCatalogo").innerHTML == "") {
            ocultarProcesando();
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }
        aFila = FilasDe("tblDatos");

        if ($I("tblDatos") == null || aFila == null || aFila.length == 0) {
            ocultarProcesando();
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        
        sb.Append("<tr style='font-family:Arial;font-size:11pt;FONT-WEIGHT: bold;'>");
        sb.Append("<td colspan=14>");
        if ($I("txtSerie").value != "")
            sb.Append(" Serie: " + $I("txtSerie").value);
        if ($I("txtNum").value != "")
            sb.Append("  Número: " + $I("txtNum").value);
        sb.Append("</td>");
        sb.Append("</tr>");

        sb.Append("<tr><td colspan=4 align='center' style='background-color: #E4EFF3;'>PROYECTO</TD>");
        sb.Append("<td colspan=2 align='center' style='background-color: #E4EFF3;'>IMPORTE</TD>");
        sb.Append("<td colspan=2 align='center' style='background-color: #E4EFF3;'>COBRO</TD>");
        sb.Append("<td colspan=6>&nbsp;</TD></TR>");
        
        sb.Append("	<tr align='center' style='background-color: #BCD4DF;'>");
        sb.Append("        <td>Categoría</TD>");
        sb.Append("        <td>Cualidad</TD>");
        sb.Append("        <td>Estado</TD>");
        sb.Append("        <td>Proyecto</TD>");
        sb.Append("        <td>Euros</TD>");
        sb.Append("        <td>Moneda proyecto</TD>");
        sb.Append("        <td>Euros</TD>");
        sb.Append("        <td>Moneda proyecto</TD>");
        sb.Append("        <td>Moneda proyecto</TD>");
        sb.Append("        <td>Año</TD>");
        sb.Append("        <td>Mes</TD>");
        sb.Append("        <td>Cliente proyecto</TD>");
        sb.Append("        <td>Cliente factura</TD>");
        sb.Append("        <td>Motivo</TD>");
        sb.Append("</tr>");
        for (var i = 0; i < aFila.length; i++) {
            sb.Append("<tr style='height:18px'>");
            switch (aFila[i].getAttribute("cat")) {
                case "P":
                    sb.Append("<td>Producto</td>");
                    break;
                default:
                    sb.Append("<td>Servicio</td>");
                    break;
            }
            switch (aFila[i].getAttribute("cua")) {
                case "C":
                    sb.Append("<td>Contratante</td>");
                    break;
                case "J":
                    sb.Append("<td>Replicado</td>");
                    break;
                case "P":
                    sb.Append("<td>Replicado con gestión propia</td>");
                    break;
                default:
                    sb.Append("<td>" + aFila[i].getAttribute("cua") + "</td>");
                    break;
            }
            switch (aFila[i].getAttribute("est")) {
                case "A":
                    sb.Append("<td>Abierto</td>");
                    break;
                case "C":
                    sb.Append("<td>Cerrado</td>");
                    break;
                case "H":
                    sb.Append("<td>Histórico</td>");
                    break;
                case "P":
                    sb.Append("<td>Presupuestado</td>");
                default:
                    sb.Append("<td>" + aFila[i].getAttribute("est") + "</td>");
                    break;
            }
            sb.Append("<td>" + aFila[i].cells[1].innerText + "</td>");
            sb.Append("<td>" + aFila[i].cells[2].children[0].value + "</td>");
            for (var j = 3; j < 6; j++) {
                sb.Append(aFila[i].cells[j].outerHTML);
            }
            //Moneda del proyecto
            sb.Append("<td>" + aFila[i].cells[6].innerText + " " + aFila[i].getAttribute("mp") + "</td>");
            for (var j = 7; j < 12; j++) {
                sb.Append(aFila[i].cells[j].outerHTML);
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
