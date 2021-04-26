var sNodo = "";
var bSalir = false;
function init(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }
        else{
            sValorNodo = $I("cboCR").value;   	    
            $I("lblNodo").className = "texto";
        }
        //Si solo es Responsable de GF no existen los botones de Añadir/eliminar grupo
        if ($I("hdnEsSoloRGF").value != "S") {
            ToolTipBotonera("nuevo", "Añade un Grupo Funcional");
            ToolTipBotonera("eliminar", "Elimina el Grupo Funcional seleccionado");
        }
        actualizarLupas("tblTitulo", "tblDatos");
        //iFila = aFila.length - 1;
        if ($I("hdnIdGrupo").value != ""){
            var aFila = FilasDe("tblDatos");
            for (var i=aFila.length-1; i>=0; i--){
                if (aFila[i].id == $I("hdnIdGrupo").value){
                    ms(aFila[i]);
                    mostrarIntegrantes($I("hdnIdGrupo").value);
                    break;
                }
            }
        }
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
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "integrantes":
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                scrollTablaProfAsig();
                break;
            case "buscar":
                $I("divCatalogo2").children[0].innerHTML = "";
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                break;                
            case "eliminar":
                var aFila = FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].className == "FS"){
                        $I("tblDatos").deleteRow(i);
                    }
                }
                $I("divCatalogo2").children[0].innerHTML = "";
                actualizarLupas("tblTitulo", "tblDatos");
                break;
            case "tecnicos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf2();
                actualizarLupas("tblTitulo", "tblDatos");
                $I("txtApe1").value = "";
                $I("txtApe2").value = "";
                $I("txtNom").value = "";
                break;
                                
        }
        ocultarProcesando();
    }
}
var gnGF = -1;
var gnCR = -1;
function mostrarDetalleAux(oFila) {
    try {
        //location.href = strServer + "Capa_Presentacion/PSP/Mantenimientos/GF/Detalle/Default.aspx?nIdGrupo=" + nIdGrupo + "&nCR=" + sValorNodo;
        gnGF = oFila.getAttribute("id");
        gnCR = oFila.getAttribute("cr");
        gnGrupoMostrado = -1;
        setTimeout("mostrarDet();", 100);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el Grupo Funcional (mostrarDetalleAux)", e.message);
    }
}
function mostrarDet() {
    try {
        //location.href = strServer + "Capa_Presentacion/PSP/Mantenimientos/GF/Detalle/Default.aspx?nIdGrupo=" + nIdGrupo + "&nCR=" + sValorNodo;
        var sEnlace = strServer + "Capa_Presentacion/PSP/Mantenimientos/GF/Detalle/Default.aspx";
        sEnlace += "?nIdGrupo=" + gnGF + "&nCR=" + gnCR + "&rg=" + $I("hdnEsSoloRGF").value;
        location.href = sEnlace;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el Grupo Funcional (mostrarDetalleAux)", e.message);
    }
}
function mostrarDetalle(nIdGrupo) {
    try {
        location.href = "../Detalle/Default.aspx?nIdGrupo=" + nIdGrupo + "&nCR=" + sValorNodo + "&rg=" + $I("hdnEsSoloRGF").value;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el Grupo Funcional", e.message);
    }
}
function nuevoGF() {
    //Accede a la pantalla donde están los datos del grupo funcional (en principio solo su descripción)
    try{
        if (sValorNodo=="" || sValorNodo=="-1")
            mmoff("Inf", "Debes seleccionar un " + $I("lblNodo").innerText, 330);
        else
            location.href = "../Detalle/Default.aspx?nIdGrupo=0&nCR="+ sValorNodo;//$I("cboCR").value;
	}
	catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo Grupo Funcional", e.message);
    }
}
function setCombo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        buscar(1,0);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener grupos funcionales del nodo", e.message);
    }
}

function getNodo(){
    try{
        if ($I("lblNodo").className == "texto") return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 460))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    $I("divCatalogo").children[0].innerHTML = "";
                    buscar(1, 0);
                }
            });
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo ", e.message);
    }
}

function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
        }
        else {
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        $I("divCatalogo").children[0].innerHTML = "";
        $I("divCatalogo2").children[0].innerHTML = "";
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el nodo", e.message);
    }
}
function ordenarTabla(nOrden,nAscDesc){
	buscar(nOrden,nAscDesc);
}
function buscar(nOrden,nAscDesc){
    try{
        var js_args = "buscar@#@" + nOrden + "@#@" + nAscDesc + "@#@" + sValorNodo + "@#@" + $I("hdnEsSoloRGF").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordernar el catálogo", e.message);
    }
}
function eliminarGF(){
    try{
        var sw = 0;
        var js_args = "eliminar@#@";
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                //if (aFila[i].bd == "I"){
                    //Si es una fila nueva, se elimina
                    //$I("tblDatos").deleteRow(i);
                //}    
//                else{
//                    mfa(aFila[i],"D");
                    sw = 1;
//                }
                js_args += aFila[i].id +"##"
            }
        }
        if (sw == 1){
            jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
                if (answer) {
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
            });
        }
        else mmoff("Inf", "Debes seleccionar algún grupo funcional", 300);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el Grupo Funcional", e.message);
    }
}
var gnGrupoMostrado = -1;
function mostrarIntegrantes(idGrupo){
    try{
        if (gnGrupoMostrado == idGrupo) return;
        gnGrupoMostrado = idGrupo;
        //var sw = 0;
        //var idGrupoNew = idGrupo;
        //var aFila = FilasDe("tblDatos");
        //for (var i=aFila.length-1; i>=0; i--){
        //    if (aFila[i].className == "FS"){
        //        sw++;
        //        idGrupoNew = aFila[i].id;
        //    }
        //}
        //if (sw==1){
            //var js_args = "integrantes@#@" + idGrupoNew + "@#@" + sValorNodo;
            var js_args = "integrantes@#@" + idGrupo + "@#@" + sValorNodo;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        //}
        //else{
        //    $I("divCatalogo2").children[0].innerHTML = "";
        //}
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar integrantes del grupo funcional", e.message);
    }
}

var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, $I("tblOpciones2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblOpciones2").rows[i].getAttribute("sw")) {
                oFila = $I("tblOpciones2").rows[i];
                //oFila.sw = "1";
                oFila.setAttribute("sw", "1");

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.baja=="1") 
                    oFila.cells[1].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}

