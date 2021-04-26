//variable en la que guardo el perfil del usuario para poder saber si tiene permiso para
//borrar una plantilla en función del ámbito de la plantilla
//	Si la plantilla es Empresarial solo será borrable si el usuario conectado tiene perfil de Administrador
//	Si la plantilla es Departamental solo será borrable si el usuario conectado tiene perfil de Oficina Técnica o superior
//	Si la plantilla es Personal siempre es borrable (se supone que un usuario normal solo ve las plantillas personales que son suyas)
var sPerfilEmpleado;
var sValorNodo="";
function init(){
    try{
        if (gsTipo=="E")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PST &gt; Mantenimiento &gt; Plantillas &gt; De proyectos económicos";
        else if (gsTipo=="T")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PST &gt; Mantenimiento &gt; Plantillas &gt; De proyectos técnicos";
    
    
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo2").className = "enlace";
            $I("lblNodo2").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }
        else{
            sValorNodo = $I("cboCR").value;   	    
            $I("lblNodo2").className = "texto";
        }
        setCR();
        ToolTipBotonera("nuevo","Nueva plantilla");
        ToolTipBotonera("eliminar","Elimina las plantillas seleccionadas");
        sPerfilEmpleado=$I("txtPerfil").value;
        actualizarLupas("tblTitulo", "tblDatos");
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error al iniciar la página", e.message);
    }
}
function mostrarMaestro(oFila){
    try{
        var sUrl="../Maestro/Default.aspx?nIDPlant="+ oFila.id+"&sTipo="+gsTipo+"&sOr="+$I("txtOrigen").value+
                 "&nCR="+oFila.getAttribute("nCR")+"&rCR="+sValorNodo;
        sUrl += ($I("chkEmp").checked)? "&bE=1":"&bE=0";
        sUrl += ($I("chkDep").checked)? "&bD=1":"&bD=0";
        sUrl += ($I("chkPer").checked)? "&bP=1":"&bP=0";
        location.href = sUrl;
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el maestro de la plantilla", e.message);
    }
}
function nuevaPlantilla(){
    try{
        var sUrl="../Maestro/Default.aspx?nIDPlant=0&sTipo="+gsTipo+"&sOr="+$I("txtOrigen").value+
                 "&nCR="+sValorNodo+"&rCR="+sValorNodo;
        sUrl += ($I("chkEmp").checked)? "&bE=1":"&bE=0";
        sUrl += ($I("chkDep").checked)? "&bD=1":"&bD=0";
        sUrl += ($I("chkPer").checked)? "&bP=1":"&bP=0";
        location.href = sUrl;
	}catch(e){
		mostrarErrorAplicacion("Error al crear una nueva plantilla", e.message);
    }
}
function eliminarPlantillas(){
    var nFilas,iNumFilasSel=0,slPlants="",i,iNumFilasBorrar=0;
    try{
        var aFila = $I("tblDatos").getElementsByTagName("TR");
        nFilas=aFila.length;
	    for (i=0;i<nFilas;i++){	        
            if (aFila[i].className == "FS"){
                iNumFilasSel++;
                //Compruebo si el perfil del usuario le permite borrar la plantilla
                //if (flBorrable(sPerfilEmpleado,aFila[i].cells[0].children[0].nodeValue)){
                if (flBorrable(sPerfilEmpleado,aFila[i].getAttribute("amb"))){
                    iNumFilasBorrar++;
                    slPlants+=aFila[i].id + "##";
                }
                else{
                    mmoff("War","No dispone de permiso para borrar la plantilla\n"+aFila[i].cells[1].children[0].nodeValue,400);
                    return;
                }
            }
        }
        if (iNumFilasSel == 0){
            mmoff("Inf", "Debes seleccionar la fila a eliminar", 250);
            return;
        }
//        if (iNumFilasBorrar == 0){
//            alert("No dispone de permiso para borrar la plantilla.");
//            return;
//        }
        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
            if (answer) {
                var js_args = "eliminar@#@" + slPlants;
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
            return;
        });
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar la plantilla", e.message);
    }
}
function flBorrable(sPerfil,sAmbito){
//Determina si una plantilla es borrable en función del perfil del empleado y el ambito de la plantilla
//Perfiles: A-> Administrador, D -> Director de CR o miembro de Oficina Técnica, T -> el resto
//Ambitos: EMPRESARIAL, DEPARTAMENTAL, PERSONAL
    var bRes=false;
    try{
        if (sPerfil=="A") bRes=true;
        else{
            if (sPerfil=="D"){
                //if (sAmbito=="DEPARTAMENTAL" || sAmbito=="PERSONAL")bRes=true;
                if (sAmbito=="D" || sAmbito=="P")bRes=true;
            }
            else{
                //if (sAmbito=="PERSONAL")bRes=true;
                if (sAmbito=="P")bRes=true;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al determinar la accesibilidad de la plantilla", e.message);
    }
    return bRes;
}
function desglosePlantilla(){
    try{
        if (iFila == -1){
            mmoff("Inf", "Debes seleccionar alguna plantilla para poder consultar su estructura", 400);
            return;
        }
        //if (nfs > 1)return;
        aFila = $I("tblDatos").getElementsByTagName("TR");
        if (numFilasSel(aFila) > 1){
            mmoff("Inf", "Debes seleccionar una única plantilla para poder consultar su estructura", 450);
            return;
        }
        var nIDPlant = $I("tblDatos").rows[iFila].id;
        var sDesPlant = $I("tblDatos").rows[iFila].cells[1].innerText;        
        var nNodo = $I("tblDatos").rows[iFila].getAttribute("nCR");
        
        var sUrl = "../Detalle/Default.aspx?nIDPlant="+ nIDPlant+"&sDesPlant="+sDesPlant+"&sTipo="+gsTipo+"&nCR="+nNodo+"&rCR="+sValorNodo;
        sUrl += ($I("chkEmp").checked)? "&bE=1":"&bE=0";
        sUrl += ($I("chkDep").checked)? "&bD=1":"&bD=0";
        sUrl += ($I("chkPer").checked)? "&bP=1":"&bP=0";
        location.href = sUrl;
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el desglose de la plantilla", e.message);
    }
}
function numFilasSel(aFila){
    try{
        var nFilasSel=0;
	    for (i=aFila.length - 1; i>=0; i--){	        
            if (aFila[i].className == "FS"){
                nFilasSel++;
            }
        }
        return nFilasSel;
    }
    catch(e){
		mostrarErrorAplicacion("Error al comprobar filas seleccionadas", e.message);
    }
}
//function getNodo(){
//    try{
//        buscar(1,0);
//	}catch(e){
//		mostrarErrorAplicacion("Error al ir a obtener atributos estadísticos del nodo", e.message);
//    }
//}
function setCR(){
    try{
        if ($I("chkDep").checked){
            $I("fldCR").style.visibility = "visible";
            if (es_administrador == "A" || es_administrador == "SA") {
		        if ($I("txtDesNodo").value != "") $I("gomaNodo").style.visibility="visible";
		        else $I("gomaNodo").style.visibility="hidden";
		    }
        }
        else{
            $I("fldCR").style.visibility = "hidden";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer visibilidad del nodo", e.message);
    }
}
function setCombo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        buscar(1,0);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener plantillas", e.message);
    }
}

function getNodo(){
    try{
        if ($I("lblNodo2").className == "texto") return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    $I("gomaNodo").style.visibility = "visible";
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
            $I("gomaNodo").style.visibility="hidden";
            sValorNodo = "";
        }else{
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        setCombo();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el nodo", e.message);
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
            case "eliminar":
                eliminarFilaDeTabla();
                mmoff("Suc", "Plantilla eliminada.", 250);
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblDatos");
                window.focus();
                break;
            case "grabarcomo":
                //RECARGO LAS PLANTILLAS
                iFila=-1;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                break;
        }
        ocultarProcesando();
    }
}
function eliminarFilaDeTabla(){
    try{	
        var aFila = $I("tblDatos").getElementsByTagName("TR");
        var nFilas=aFila.length;
	    for (i=nFilas-1;i>=0;i--){	        
            if (aFila[i].className == "FS"){
                $I("tblDatos").deleteRow(i);
            }
        }
	    iFila == -1;
	    actualizarLupas("tblTitulo", "tblDatos");
	}
    catch(e){
		mostrarErrorAplicacion("Error al eliminar línea de desglose", e.message);
    }
}
function ordenarTabla(nOrden,nAscDesc){
	buscar(nOrden,nAscDesc);
}
function buscar(nOrden,nAscDesc){
    try{
        iFila=-1;
//        if (!$I("chkEmp").checked && !$I("chkDep").checked && !$I("chkPer").checked)
//            alert("Debe seleccionar algún tipo de plantilla a mostrar.");
        var js_args = "buscar@#@";
        js_args += nOrden +"@#@";
        js_args += nAscDesc +"@#@";
        js_args += gsTipo +"@#@";
        js_args += $I("txtOrigen").value +"@#@";
        js_args += sValorNodo+"@#@";
        js_args += ($I("chkEmp").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDep").checked)? "1@#@":"0@#@";
        js_args += ($I("chkPer").checked)? "1":"0";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el catálogo", e.message);
    }
}
function grabarComo(){
    var sAmbito="P";
    var js_args="";
    try{
        if (iFila == -1){
            mmoff("Inf", "Debes seleccionar alguna plantilla para poder duplicarla", 350);
            return;
        }
        aFila = $I("tblDatos").getElementsByTagName("TR");
        if (numFilasSel(aFila) > 1){
            mmoff("Inf","Debes seleccionar una única plantilla para poder duplicarla",450);
            return;
        }
        var nIDPlant = $I("tblDatos").rows[iFila].id;
        var sDesPlant = $I("tblDatos").rows[iFila].cells[1].innerText;
        if (sPerfilEmpleado=="A")sAmbito="";
        //js_args = "grabarcomo@#@ Copia de " + sDesPlant + "@#@"+ sAmbito + "@#@"+ $I("hdnIDPlantilla").value + "@#@";
        js_args = "grabarcomo@#@Copia de " + Utilidades.escape(sDesPlant) + "@#@"+ sAmbito + "@#@"+ nIDPlant + "@#@";
        js_args += $I("txtOrigen").value +"@#@";
        js_args += gsTipo +"@#@";
        js_args += sValorNodo+"@#@";
        js_args += ($I("chkEmp").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDep").checked)? "1@#@":"0@#@";
        js_args += ($I("chkPer").checked)? "1":"0";
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}
	catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}
