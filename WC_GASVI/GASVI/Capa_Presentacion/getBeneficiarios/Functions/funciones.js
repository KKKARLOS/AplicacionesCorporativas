function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("tblBeneficiarios") != null) {
            scrollTablaProf();
        }
        $I("txtApellido1").focus();
        ocultarProcesando();
        //opener.ocultarProcesando();
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


//        window.returnValue = oFila.id + "@#@"
//                            + oFila.getAttribute("sexo") + "@#@"
//                            + oFila.cells[1].innerText + "@#@"
//                            + oFila.cells[2].innerText; 
//        window.close();
        var returnValue = oFila.id + "@#@"
                            + oFila.getAttribute("sexo") + "@#@"
                            + oFila.cells[1].innerText + "@#@"
                            + oFila.cells[2].innerText; 
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
        sb.Append((($I("chkBajas").checked) ? "1" : "0"));

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
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblBeneficiarios").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblBeneficiarios").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblBeneficiarios.rows[i].getAttribute("sw")) {
                oFila = tblBeneficiarios.rows[i];
                oFila.setAttribute("sw",1);

                if (oFila.getAttribute("sexo") == "V") {
                    oFila.cells[0].appendChild(oImgIV.cloneNode(true), null);
                }
                else {
                    oFila.cells[0].appendChild(oImgIM.cloneNode(true), null);
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 30);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de beneficiarios.", e.message);
    }
}