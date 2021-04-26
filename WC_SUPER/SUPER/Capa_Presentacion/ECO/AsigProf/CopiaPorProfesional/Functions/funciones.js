function init(){
    try{
	    AccionBotonera("grabar", "H");
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
            case "grabar":
                //desActivarGrabar();
                bCambios=false;
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160); 
                //popupWinespopup_winLoad();
                //if (bSalir) salir();
                break;
            case "buscar":
                $I("divCatalogo").innerHTML = aResul[2];
                //actualizarLupas("tblTitulo", "tblOpciones");
                ActivarGrabar();
                break;
            case "recursos":
                $I("divCatalogo3").children[0].innerHTML = aResul[2];
                //actualizarLupas("tblTitulo3", "tblOpciones3");
                scrollTablaProfAsig();
                break;
            case "recuperarPSN":
                if (aResul[3]==""){
                    if ($I("txtNumPE").value != "") mmoff("War", "El proyecto no existe o está fuera de su ámbito de visión.", 350);
                    break;
                }
                $I("txtNumPE").value = aResul[3].ToString("N",9, 0);
                $I("txtPE").value = aResul[4];
	            $I("hdnNodo").value = aResul[5];
                setTimeout("buscar1()", 50);
                break;
            case "recuperarPSN2":
                if (aResul[3]==""){
                    if ($I("txtNumPE").value != "") mmoff("War", "El proyecto no existe o está fuera de su ámbito de visión.", 350);
                    break;
                }
                $I("txtNumPE2").value = aResul[3].ToString("N",9, 0);
                $I("txtPE2").value = aResul[4];
                break;
            case "buscarPE":
                if (aResul[2]==""){
                    if ($I("txtNumPE").value != "") mmoff("War", "El proyecto no existe o está fuera de su ámbito de visión.", 350);
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##");
                        $I("hdnT305IdProy").value = aDatos[0];
                        setTimeout("recuperarDatosPSN(1);", 20);
                    }else{
                        setTimeout("getPEByNum1("+ $I("txtNumPE").value +");", 20);
                    }
                }
                break;
            case "buscarPE2":
                if (aResul[2]==""){
                    if ($I("txtNumPE").value != "") mmoff("War", "El proyecto no existe o está fuera de su ámbito de visión.", 350);
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##");
                        $I("hdnT305IdProy2").value = aDatos[0];
                        setTimeout("recuperarDatosPSN(2);", 20);
                    }else{
                        setTimeout("getPEByNum2("+ $I("txtNumPE2").value +");", 20);
                    }
                }
                break;
            default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function flGetIntegrantes(){
/*tarea origen @#@ lista de recursos seleccionados @#@ lista de tareas + indicador de notificación
*/
    var sRes="",sw=0,idTarea="";
    try{
        //Lista de recursos 
        var aItems = FilasDe("tblOpciones3");
        for (var i=0;i<aItems.length;i++){
            if (aItems[i].cells[2].children[0].checked){
                sw = 1;
                //sRes += aDestino[i].id +"##"; //ID empleado
                sRes += aItems[i].id + "," + aItems[i].getAttribute("nodo") + "," + aItems[i].getAttribute("cierre") + "," + 
                        aItems[i].getAttribute("deriva") + "##";
                
            }
        }
        if (sw == 1) sRes = sRes.substring(0, sRes.length-2);
        sRes+="@#@";
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function grabar(){
    try{
        if (!comprobarDatos()) return;

        js_args = "grabar@#@" + $I("hdnT305IdProy2").value + "@#@" + flGetIntegrantes();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        //desActivarGrabar();
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}
function comprobarDatos(){
    try{
        if ($I("tblOpciones3").rows.length==0){
            mmoff("War", "No hay usuarios para asignar", 220);
            return false;
        }
        sw=0;
        var aRecs = FilasDe("tblOpciones3");
        for (var i=0;i<aRecs.length;i++){
            if (aRecs[i].cells[2].children[0].checked){
                sw = 1;
                break; 
            }
        }
        if (sw == 0){
            mmoff("War","No hay usuarios seleccionadas para asignar",240);
            return false;
        }
        if ($I("hdnT305IdProy2").value==""){
            mmoff("War", "No hay proyecto destino para asignar", 220);
            return false;
        }
        if ($I("hdnT305IdProy").value==$I("hdnT305IdProy2").value){
            mmoff("War","El proyecto origen y el destino no pueden ser el mismo",340);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function obtenerProyectos(nOp){
    try{
	    var sAux;
	    //var strEnlace = "../../../getProyectos/Default.aspx?mod=pst&sSoloAbiertos=1";//Solo proyectos abiertos
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge";
	    //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                if (nOp == 1) $I("hdnT305IdProy").value = aDatos[0];
	                else $I("hdnT305IdProy2").value = aDatos[0];
	                recuperarDatosPSN(nOp);
	            } else ocultarProcesando();
	        }); 	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function recuperarDatosPSN(nOp){
    try{
        var js_args;
        if (nOp==1) js_args = "recuperarPSN@#@"+ $I("hdnT305IdProy").value;
        else js_args = "recuperarPSN2@#@" + $I("hdnT305IdProy2").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function limpiarEstructura(nOp){
    if (nOp==1){
        //$I("txtNumPE").value = "";
        $I("txtPE").value = "";
        $I("hdnNodo").value = "";
        $I("hdnT305IdProy").value = "";
        BorrarFilasDe("tblOpciones3");
    }
    else{
        //$I("txtNumPE2").value = "";
        $I("txtPE2").value = "";
        $I("hdnT305IdProy2").value = "";
    }
}
function buscar1(){
    try{
        if ($I("hdnT305IdProy").value=="") return;
        BorrarFilasDe("tblOpciones3");
        var js_args="recursos@#@"+$I("hdnT305IdProy").value+"@#@"+$I("hdnNodo").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
	}catch(e){
		mostrarErrorAplicacion("Error al buscar tareas", e.message);
    }
}
var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divCatalogo3").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divCatalogo3").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo3").offsetHeight/20+1, $I("tblOpciones3").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones3.rows.length; i++){
            if (!$I("tblOpciones3").rows[i].getAttribute("sw")){
                oFila = $I("tblOpciones3").rows[i];
                oFila.setAttribute("sw", "1");
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                
                //oFila.cells[1].children[0].style.cursor = "../../../../images/imgManoAzul2.cur";
                //oFila.cells[1].children[0].ondblclick = function(){setEstado(this);};
//                if (oFila.estado=="0"){
//                    setOp(oFila.cells[0].children[0], 20);
//                    oFila.cells[0].children[0].title = "Profesional inactivo en la tarea (no le figura en su IAP)";
//                }else{
//                    oFila.cells[0].children[0].title = "Profesional activo en la tarea (le figura en su IAP)";
//                }
//                if (oFila.baja=="1") 
//                    oFila.cells[1].style.color = "red";
//                else{
//                    if (oFila.baja=="2") 
//                        oFila.cells[1].style.color = "maroon";
//                }
            }
//            nContador++;
//            if (nContador > $I("divCatalogo3").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function marcar2(nOpcion){
    try{
        for (var i=0; i<$I("tblOpciones3").rows.length; i++){
            $I("tblOpciones3").rows[i].cells[2].children[0].checked = (nOpcion==1)?true:false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}
function buscarPE(nOp){
    try{
        limpiarEstructura(nOp);
        var js_args = "";
        if (nOp==1) js_args = "buscarPE@#@" + dfn($I("txtNumPE").value);
        else js_args = "buscarPE2@#@" + dfn($I("txtNumPE2").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
function getPEByNum1(nPE){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE="+dfn(nPE.ToString());
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    recuperarDatosPSN(1);
                } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function getPEByNum2(nPE){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE="+dfn(nPE.ToString());
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy2").value = aDatos[0];
                    recuperarDatosPSN(2);
                } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
//function vtn1(){
//    try{
//        var tecla = String.fromCharCode(event.keyCode);
//        if("1234567890".indexOf(tecla)>-1) return true;
//        else{
//            event.keyCode=0;
//            return false;
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al validar formato numérico", e.message);
//    }
//}
