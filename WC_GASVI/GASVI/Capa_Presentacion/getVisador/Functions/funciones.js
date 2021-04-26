function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("tblVisadores") != null) {
            if (tblVisadores.rows.length == 1) $I("lblProfesionales").innerText = "Profesional";
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(oControl){
    try{
        if (bProcesando()) return;
        
        var oFila;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }


//        window.returnValue = oFila.id + "@#@" + oFila.getAttribute("sexo") + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.cells[2].innerText;
//        window.close();
        var returnValue = oFila.id + "@#@" + oFila.getAttribute("sexo") + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.cells[2].innerText;
        modalDialog.Close(window, returnValue);            
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir(){
    //        window.returnValue = null;
    //        window.close();
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

function getBeneficiarioAux() {
    try {
        mostrarProcesando();
        if (fTrim($I("txtApellido1").value) == ""
            && fTrim($I("txtApellido2").value) == ""
            && fTrim($I("txtNombre").value) == ""){
            ocultarProcesando();
            mmoff("War", "Debe introducir algún criterio de búsqueda", 280);
            $I("txtApellido1").focus();
            return;
        }
        
        var sb = new StringBuilder;

        sb.Append("getBeneficiario@#@");
        sb.Append(Utilidades.escape($I("txtApellido1").value) + "@#@");
        sb.Append(Utilidades.escape($I("txtApellido2").value) + "@#@");
        sb.Append(Utilidades.escape($I("txtNombre").value) + "@#@");

        RealizarCallBack(sb.ToString(), "");
    
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el beneficiario", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getBeneficiario":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                scrollTablaProf();
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        ocultarProcesando();
    }
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;

function scrollTablaProf() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProf) {
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProf / 20);
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblVisadores").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblVisadores").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblVisadores.rows[i].getAttribute("sw")) {
                oFila = tblVisadores.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    oFila.cells[0].appendChild(oImgIV.cloneNode(true), null);
                }
                else {
                    oFila.cells[0].appendChild(oImgIM.cloneNode(true), null);
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}

function mostrarQEQ(sIdFicepi) {
    try {
//        mostrarProcesando();
        var strEnlace = "http://web.intranet.ibermatica/paginasamarillas/profesionales/datos_simple.asp?cod=" + sIdFicepi + "&origen=MAPACON";
//        var ret = window.showModalDialog(strEnlace, "QEQ", "dialogtop:" + eval(screen.height / 2 - 195) + "; dialogleft:" + eval(screen.width / 2 - 320) + "px; " + sSize(640,400) + "status:NO; help:NO;");
//        window.focus();
        window.open(strEnlace, "QEQ", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 365) + "px,left=" + eval(screen.availWidth / 2 - 510) + "px,width=640px,height=400px");    
    } catch (e) {
        if (e.code != 18)//abre una página de la intranet que genera un error (no está en crossbrowsing).
            mostrarErrorAplicacion("Error al obtener el visador.", e.message);
        else
            ocultarProcesando();
    }
}
