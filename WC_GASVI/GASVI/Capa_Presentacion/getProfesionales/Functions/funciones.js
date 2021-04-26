function init(){
    try{
        if (!mostrarErrores()) return;
        if (FilasDe("tblCatProf") != null){
            actualizarLupas("tblTitulo", "tblCatProf");
        }
        $I("txtApellido1").focus();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(oControl){
    try{
        if (bProcesando()) return;
        var strRetorno = "";
        var oFila;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }             
        strRetorno = oFila.id;
        strRetorno += "@#@" + fTrim(oFila.innerText);
        strRetorno += "@#@" + oFila.getAttribute("sexo");
        strRetorno += "@#@" + oFila.getAttribute("tipo");
//        window.returnValue = strRetorno;
//        window.close();

        var returnValue = strRetorno;
        modalDialog.Close(window, returnValue);              
        
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;
        //        window.returnValue = null;
        //        window.close();
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblCatProf");
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}

function mostrarProfesionales(){
    try {
        strInicial = Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value) + "@#@";
	    if (strInicial == "@#@@#@@#@"){
	        mmoff("War", "Debe introducir algún criterio de búsqueda",280);
	        $I("txtApellido1").focus();
	        return;
	    }
	    var js_args = "buscar@#@" + strInicial + "@#@" + (($I("chkTodos").checked) ? "1" : "0");
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
    }
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;

function scrollTablaProf(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblCatProf").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblCatProf").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblCatProf.rows[i].getAttribute("sw")){
                oFila = tblCatProf.rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 30);
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

function clickChk(chk){
    if (ie) chk.click();
    else {
        var clickEvent = window.document.createEvent("MouseEvent");
        clickEvent.initEvent("click", false, true);
        chk.dispatchEvent(clickEvent);
    }

}