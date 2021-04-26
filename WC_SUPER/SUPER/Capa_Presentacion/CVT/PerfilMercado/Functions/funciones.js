var FSAI = null; //fila seleccionada de la tabla de internos
var FSAE = null; //fila seleccionada de la tabla de colaboradores externos
function init() {
    try {
        mmoff("InfPer", "Obteniendo relación de profesionales...", 260);
        getProfesionales();
        setExcelImg("imgExcelI", "divCatalogoI", "excelI");
        $I("imgExcelI_exp").style.top = "150px";
        $I("imgExcelI_exp").style.left = "978px";
        setExcelImg("imgExcelE", "divCatalogoE", "excelE");
        $I("imgExcelE_exp").style.top = "440px";
        $I("imgExcelE_exp").style.left = "978px";
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

function getProfesionales(){
    try{
        var js_args = "getProfesionales@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
    
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getProfesionales":
                mmoff("hide");
                $I("divCatalogoI").children[0].innerHTML = aResul[2];
                $I("divCatalogoE").children[0].innerHTML = aResul[3];
                scrollTablaI();
                scrollTablaE();
                break;
            case "grabar":
                mmoff("Suc","Cambio realizado", 160, 500);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

function grabar(oFila, perfilmercado) {
    try {
        mostrarProcesando();
        oFila.setAttribute("perMer",perfilmercado);
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        sb.Append(oFila.id + "@#@" + perfilmercado); 
        //eliminamos opción de combo vacia
        if (oFila.children[3].children[0].children[0].innerText == "");
            oFila.children[3].children[0].removeChild(oFila.children[3].children[0].children[0]);
        RealizarCallBack(sb.ToString());
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}

var nTopScrollI = -1;
var nIDTimeI = 0;
function scrollTablaI() {
    try {
        if ($I("divCatalogoI").scrollTop != nTopScrollI) {
            nTopScrollI = $I("divCatalogoI").scrollTop;
            clearTimeout(nIDTimeI);
            nIDTimeI = setTimeout("scrollTablaI()", 50);
            return;
        }
        //var tblDatosI = $I("tblDatosI");
        if (tblDatosI == null) return;
        var nFilaVisible = Math.floor(nTopScrollI / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoI").offsetHeight / 22 + 1, tblDatosI.rows.length);

        var oFila, sAux;
        var iCont = 0;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatosI.rows[i].getAttribute("sw")) {
                oFila = tblDatosI.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); activarComboI(this); };

                if (oFila.getAttribute("sexo") == "V") 
                    oFila.cells[0].appendChild(oImgIV.cloneNode(), null);
                else 
                    oFila.cells[0].appendChild(oImgIM.cloneNode(), null); 
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales internos.", e.message);
    }
}


var nTopScrollE = -1;
var nIDTimeE = 0;
function scrollTablaE() {
    try {
        if ($I("divCatalogoE").scrollTop != nTopScrollE) {
            nTopScrollE = $I("divCatalogoE").scrollTop;
            clearTimeout(nIDTimeE);
            nIDTimeE = setTimeout("scrollTablaE()", 50);
            return;
        }
        //var tblDatosE = $I("tblDatosE");
        if (tblDatosE == null) return;
        var nFilaVisible = Math.floor(nTopScrollE / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoE").offsetHeight / 22 + 1, tblDatosE.rows.length);

        var oFila, sAux;
        var iCont = 0;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatosE.rows[i].getAttribute("sw")) {
                oFila = tblDatosE.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); activarComboE(this);};

                if (oFila.getAttribute("sexo") == "V") 
                    oFila.cells[0].appendChild(oImgEV.cloneNode(), null); 
                else
                    oFila.cells[0].appendChild(oImgEM.cloneNode(), null);                 
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de los colaboradores externos.", e.message);
    }
}

var oNBRPM = document.createElement("nobr");
oNBRPM.setAttribute("class", "NBR W300");
function activarComboI(oFila){
    try{
        if (FSAI == oFila.rowIndex) return;
        if (FSAI != null) {
            var sPM = "";
            if ($I("comboI").selectedIndex != -1)
                sPM = $I("comboI").children[$I("comboI").selectedIndex].innerText;
            //var sPM = $I("comboI").children[$I("comboI").selectedIndex].innerText;
            $I("tblDatosI").rows[FSAI].children[3].removeChild($I("tblDatosI").rows[FSAI].children[3].children[0]);
            $I("tblDatosI").rows[FSAI].children[3].appendChild(oNBRPM.cloneNode(false));
            $I("tblDatosI").rows[FSAI].children[3].children[0].innerText = sPM;
        }
        FSAI = oFila.rowIndex;    
        var idPM = oFila.getAttribute("perMer");
        var comboI = "<select class='combo' id='comboI' style='width:290px;' onChange='grabar(this.parentNode.parentNode, this.value)'>";
        if (idPM == "")
            comboI += "<option value=''></option>";
        comboI += $I("hdnCombo").value + "</select>";
        oFila.children[3].innerHTML = comboI;
        $I("comboI").value = idPM;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar el combo.", e.message);
    }
}

function activarComboE(oFila){
    try{
        if (FSAE == oFila.rowIndex) return;
        if (FSAE != null) {
            var sPM = "";
            if ($I("comboE").selectedIndex != -1)
                sPM = $I("comboE").children[$I("comboE").selectedIndex].innerText;
            //var sPM = $I("comboE").children[$I("comboE").selectedIndex].innerText;
            $I("tblDatosE").rows[FSAE].children[3].removeChild($I("tblDatosE").rows[FSAE].children[3].children[0]);
            $I("tblDatosE").rows[FSAE].children[3].appendChild(oNBRPM.cloneNode(false));
            $I("tblDatosE").rows[FSAE].children[3].children[0].innerText = sPM;
        }
        FSAE = oFila.rowIndex;
        var idPM = oFila.getAttribute("perMer");
        var comboE = "<select class='combo' id='comboE' style='width:290px;' onChange='grabar(this.parentNode.parentNode, this.value)'>";
        if (idPM == "")
            comboE += "<option value=''></option>";
        comboE += $I("hdnCombo").value + "</select>";
        oFila.children[3].innerHTML = comboE;
        $I("comboE").value = idPM;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar el combo.", e.message);
    }
}

function excelI() {
    try {
        excelPM(1);
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function excelE() {
    try {
        excelPM(2);
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
} 

function excelPM(nOpcion) {
    try {
        var oTabla = null;
        if (nOpcion == 1) oTabla = $I("tblDatosI");
        else oTabla = $I("tblDatosE");

        if (oTabla == null || oTabla.rows.length == 0) {
            ocultarProcesando();
            mmoff("War", "No hay información a exportar.", 250);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border='1'>");
        sb.Append("	<tr align='center' style='font-weight:bold;'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Profesional</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Rol</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Perfil de mercado</td>");
        sb.Append("	</tr>");

        var aFilas = oTabla.rows;;
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<tr>");
            for (var x = 1; x < 4; x++) {
                if (x == 3){
                    if (aFilas[i].cells[x].getElementsByTagName("select").length > 0){
                        if (aFilas[i].cells[x].getElementsByTagName("select")[0].selectedIndex != -1){
                            sb.Append("<td>" + aFilas[i].cells[x].children[0].children[aFilas[i].cells[x].children[0].selectedIndex].innerText + "</td>");
                        }else sb.Append("<td></td>");
                    }else sb.Append("<td>" + aFilas[i].cells[x].innerText + "</td>");
                }
                else sb.Append("<td>" + aFilas[i].cells[x].innerText + "</td>");
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