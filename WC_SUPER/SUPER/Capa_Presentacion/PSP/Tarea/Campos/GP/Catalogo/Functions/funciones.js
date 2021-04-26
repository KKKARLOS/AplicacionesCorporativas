var bHayCambios = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        ocultarProcesando();
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
            case "eliminar":
                bHayCambios = true;
                var aFila = FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].className == "FS"){
                        $I("tblDatos").deleteRow(i);
                    }
                }
                $I("divCatalogo2").children[0].innerHTML = "";
                actualizarLupas("tblTitulo", "tblDatos");
                break;
            case "getDatos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                if (aResul[3]) {
                    $I("divCatalogo2").children[0].innerHTML = aResul[3];
                }
                if (aResul[4]) {
                    marcarFila(aResul[4]);
                }
                break;
        }
        ocultarProcesando();
    }
}

function Detalle(oFila) {
    try {
        var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/GP/Detalle/Default.aspx?nIdGrupo=" + oFila.id;
        modalDialog.Show(strEnlace, self, sSize(950, 700))
	        .then(function (ret) {
	            if (ret != null)
	            {
	                bHayCambios = true;
	                ObtenerDatos(oFila.id);
	            }
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}

function nuevo() {
    //Accede a la pantalla donde están los datos del grupo funcional (en principio solo su descripción)
    try{
        var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/GP/Detalle/Default.aspx?nIdGrupo=";
        modalDialog.Show(strEnlace, self, sSize(950, 700))
	        .then(function (ret) {
	            if (ret != null) {
	                bHayCambios = true;
	                var aResul = ret.split("@#@");
	                ObtenerDatos(aResul[1]);
	            }
	        });
	}
	catch(e){
	    mostrarErrorAplicacion("Error al crear un nuevo equipo", e.message);
    }
}
function eliminar(){
    try{
        var sw = 0;
        var js_args = "eliminar@#@";
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                aFila[i].setAttribute("bd", "D");
                sw = 1;
                js_args += aFila[i].id +"##";
            }
        }
        if (sw == 1){
            jqConfirm("", "Se notificará a los integrantes del equipo del borrado de dicho equipo.<br><br>¿Deseas realizar el borrado?", "", "", "war", 450).then(function (answer) {
                if (answer)
                {
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
            });
        }
        else
            mmoff("Inf", "Debes seleccionar algún equipo de profesionales", 330);
	}catch(e){
	    mostrarErrorAplicacion("Error al eliminar el equipo de profesionales", e.message);
    }
}
var gnGrupoMostrado = -1;
function mostrarIntegrantes(idGrupo){
    try{
        if (gnGrupoMostrado == idGrupo) return;
        gnGrupoMostrado = idGrupo;

        var js_args = "integrantes@#@" + idGrupo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
	    mostrarErrorAplicacion("Error al mostrar integrantes del equipo.", e.message);
    }
}

var oImgTM = document.createElement("img");
oImgTM.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuTM.gif");
oImgTM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTV = document.createElement("img");
oImgTV.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuTV.gif");
oImgTV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgGM = document.createElement("img");
oImgGM.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuGM.gif");
oImgGM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgGV = document.createElement("img");
oImgGV.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuGV.gif");
oImgGV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

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
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

//                if (oFila.baja=="1") 
//                    oFila.cells[2].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}

function salir() {
    bCambios = false;
    var returnValue = null;
    if (bHayCambios) returnValue = "SI";

    modalDialog.Close(window, returnValue);
}
function ObtenerDatos(idEquipo) {
    try {
        mostrarProcesando();
        var js_args = "getDatos@#@" + idEquipo;
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function marcarFila(idFila) {
    try {
        aFila = FilasDe("tblDatos");
        for (var i = aFila.length - 1; i >= 0; i--) {
            if (aFila[i].id == idFila) {
                aFila[i].className = "FS";
                    break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar la fila", e.message);
    }
}
