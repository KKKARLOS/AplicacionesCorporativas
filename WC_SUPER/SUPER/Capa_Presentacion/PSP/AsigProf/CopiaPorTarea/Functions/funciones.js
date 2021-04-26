function init(){
    try{
	    //activarGrabar();
	    //AccionBotonera("grabar", "H");
	    $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
var sAmb = "A";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue("rdbAmbito", false);
        if (sOp == sAmb) return;
        else{
            $I("divCatalogo2").children[0].innerHTML = "<table id='tblOpciones3' class='texto MAM' style='width: 440px;'><colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup></table>";
            $I("ambAp").style.display = "none";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambPE").style.display = "none";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
                case "P":
                    $I("ambPE").style.display = "block";
                    break;
            }
            sAmb = sOp;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function seleccionItem(){
    try{
        var sOp = getRadioButtonSelectedValue("rdbItem", true);
        switch (sOp){
            case "E":
                $I("lblItem").innerText = "Denominación de proyecto económico";
                break;
            case "P":
                $I("lblItem").innerText = "Denominación de proyecto técnico";
                break;
            case "F":
                $I("lblItem").innerText = "Denominación de fase";
                break;
            case "A":
                $I("lblItem").innerText = "Denominación de actividad";
                break;
            case "T":
                $I("lblItem").innerText = "Denominación de tarea";
                break;
        }
        $I("divTareas").children[0].innerHTML ="<table id='tblTareas'></table>";
        var aFila = FilasDe("tblOpciones");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].className == "FS"){
                getItems2(aFila[i].id, sOp);
                break;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar items", e.message);
    }
}
function borrarCatalogo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}  
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
	    $I("divTareas").children[0].innerHTML ="<table id='tblTareas'></table>";
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
	    if (strInicial == "@#@@#@") return;
	    var js_args = "buscar@#@" + strInicial + "@#@" + (($I("chkSoloActivos").checked) ? "1" : "0");

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function obtenerCR(){
    try{
	    var aOpciones;
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=T";
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                $I("txtCR").value = aOpciones[1];
	                mostrarRelacionTecnicos("C", aOpciones[0], "", "");
	            }
	        });
	    window.focus();	    
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}

function obtenerGF(){
    try{
	    var aOpciones;
        if ($I("hdnCRActual").value=="")return;
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnCRActual").value;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtGF").value = aOpciones[1];
                    mostrarRelacionTecnicos("G", aOpciones[0], "", "");
                }
            });
        window.focus();	            
        ocultarProcesando(); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function obtenerPE(){
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtCodPE").value = aDatos[3].ToString("N", 9, 0);
                    $I("txtPE").value = aDatos[4];
                    $I("t305_idproyectosubnodo").value = aDatos[0];
                    mostrarRelacionTecnicos("P", aDatos[0], "", "");
                }
            });
        window.focus();	    	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el proyecto económico", e.message);
    }
}
function mostrarRelacionTecnicos(sOpcion, sValor1, sValor2, sValor3){
    var sCodUne="",sNumPE="", sCualidad="";
    try{
        if (sOpcion=="G") sCodUne=$I("hdnCRActual").value;
        if (sOpcion=="C") sCodUne=sValor1;
        if (sOpcion=="P") sNumPE=sValor1;
        if (sOpcion=="N"){
            sValor1= Utilidades.escape($I("txtApe1").value);
            sValor2= Utilidades.escape($I("txtApe2").value);
            sValor3= Utilidades.escape($I("txtNom").value);
            if (sValor1=="" && sValor2=="" && sValor3==""){
                mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
                return;
            }
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion +"@#@"+sValor1+"@#@"+sValor2+"@#@"+sValor3+"@#@"+sCodUne+"@#@"+sNumPE+"@#@"+sCualidad;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}

function insertarRecurso(oFila){
    try{
        var idRecurso = oFila.id;
        var bExiste = false;
        var aFila = FilasDe("tblOpciones3");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
                bExiste = true;
                break;
            }
        }
        if (bExiste){
        //    alert("El profesional indicado ya se encuentra asignado a la actividad");
            return;
        }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;
	    sNombreNuevo = oFila.innerText;
        for (var iFilaNueva=0; iFilaNueva < aFila.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFila[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        
        oNF = $I("tblOpciones3").insertRow(iFilaNueva);
        var oCloneNode	= oFila.cloneNode(true);
        oCloneNode.className = "";
        oNF.swapNode(oCloneNode);
        
        $I("divCatalogo2").scrollTop = $I("tblOpciones3").rows.length * 16;
        actualizarLupas("tblTitRecAsig", "tblOpciones3");
        AccionBotonera("grabar", "H");
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
    }
}
function comprobarDatos(){
    try{
        sw=0;
        var aRecs = FilasDe("tblTareas");
        for (var i=0;i<aRecs.length;i++){
            if (aRecs[i].cells[1].children[0].checked){
                sw = 1;
                break; 
            }
        }
        if (sw == 0){
            mmoff("War", "No hay tareas seleccionadas para asignar",230);
            return false;
        }
        if ($I("tblOpciones3").rows.length==0){
            mmoff("War", "No hay usuarios para asignar", 210);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function flGetIntegrantes(){
/*Solo tareas asignadas al empleado origen @#@ empleado origen @#@ tipo de item @#@ lista de recursos seleccionados 
    @#@ lista de items + indicador de asignación a proyecto
*/
    var sRes="",sw=0,idRecurso="";
    try {
        sRes = ($I("chkSoloAsignadas").checked) ? "1" : "0";
        sRes += "@#@";
        sRes += ($I("chkSoloActivos2").checked) ? "1" : "0";
        sRes += "@#@";
        var aOrigen = FilasDe("tblOpciones");
        if (aOrigen != null){
            for (var i=0; i < aOrigen.length; i++){
                if (aOrigen[i].className == "FS"){
                    idRecurso=aOrigen[i].id;
                    break;
                }
            }
        }
        if (idRecurso != ""){
            sRes+=idRecurso + "@#@";
            var sOp = getRadioButtonSelectedValue("rdbItem", true);
            sRes+=sOp+"@#@";//tipo de item seleccionados
            //Lista de recursos destino
            var aDestino = FilasDe("tblOpciones3");
            for (var i=0;i<aDestino.length;i++){
                    sw = 1;
                    sRes += aDestino[i].id +"##"; //ID empleado
            }
            if (sw == 1) sRes = sRes.substring(0, sRes.length-2);
            sRes+="@#@";
            //Lista de items
            sw=0;
            var aItems = FilasDe("tblTareas");
            for (var i=0;i<aItems.length;i++){
                if (aItems[i].cells[1].children[0].checked){
                    sw = 1;//ID item, asociar a proyecto, id nodo, anomes del ultimo cierre economico, id proyectosubnodo
                    sRes += aItems[i].id + "," + aItems[i].getAttribute("a") + "," + aItems[i].getAttribute("nodo") + "," + aItems[i].getAttribute("cierre") + "," + aItems[i].getAttribute("psn");
                    if (sOp=="T")
                        sRes += "," + aItems[i].getAttribute("notif") + "##"; 
                    else 
                        sRes += ",N##"; 
                }
            }
            if (sw == 1) sRes = sRes.substring(0, sRes.length-2);
        }
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function grabar(){
    try{
        if (!comprobarDatos()) return;

        js_args = "grabar@#@" + flGetIntegrantes();
        
        if (js_args != "grabar@#@"){
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}
function getItems(idRecurso){
    try{
        var sOp = getRadioButtonSelectedValue("rdbItem", true);
        getItems2(idRecurso, sOp)
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener items del usuario", e.message);
    }
}
function getItems2(idRecurso, sOp){
    try{
        var js_args = "getItems@#@" + idRecurso + "@#@" + sOp + "@#@" + (($I("chkSoloActivos2").checked) ? "1" : "0");         
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener items del usuario", e.message);
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
            case "getItems":
                $I("divTareas").children[0].innerHTML = aResul[2];
                break;
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
		        actualizarLupas("tblTitulo", "tblOpciones");
                
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
                break;
            case "tecnicos":
                $I("divRelacion").children[0].innerHTML = aResul[2];
                scrollTablaProf2();
		        actualizarLupas("tblTitulo", "tblOpciones2");
        	    $I("txtApe1").value = "";
        	    $I("txtApe2").value = "";
        	    $I("txtNom").value = "";
                break;
            case "grabar":
                bCambios=false;
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();
                break;
        }
        ocultarProcesando();
    }
}
function desasignar(){
    try{
        var aFilas = FilasDe("tblOpciones3");
        if (aFilas.length == 0) return;
        for (var i=aFilas.length-1; i>=0; i--){
            if (aFilas[i].className.toUpperCase() == "FS"){
                eliminarRecurso(aFilas[i]);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al desasignar a un usuario", e.message);
	}
}
function eliminarRecurso(oFila){
    try{
        $I("tblOpciones3").deleteRow(oFila.rowIndex);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar al usuario.", e.message);
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
        var tblOpciones = $I("tblOpciones");
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblOpciones.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones.rows[i].getAttribute("sw")) {
                oFila = tblOpciones.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent("onclick", mm);
                oFila.onclick = function() { getItems(this.id) };
                
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

                if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 20);          
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales de referencia.", e.message);
    }
}
var nTopScrollProf2 = -1;
var nIDTimeProf2 = 0;
function scrollTablaProf2(){
    try{
        if ($I("divRelacion").scrollTop != nTopScrollProf2){
            nTopScrollProf2 = $I("divRelacion").scrollTop;
            clearTimeout(nIDTimeProf2);
            nIDTimeProf2 = setTimeout("scrollTablaProf2()", 50);
            return;
        }
        var tblOpciones2 = $I("tblOpciones2");
        var nFilaVisible = Math.floor(nTopScrollProf2/20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacion").offsetHeight/20+1, tblOpciones2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones2.rows[i].getAttribute("sw")) {
                oFila = tblOpciones2.rows[i];
                oFila.setAttribute("sw", 1);

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
                if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 20); 
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales candidatos.", e.message);
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
        var tblOpciones3 = $I("tblOpciones3");
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblOpciones3.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones3.rows[i].getAttribute("sw")) {
                oFila = tblOpciones3.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent("onclick", mm);

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
                if (oFila.getAttribute("baja") == "1") 
                    oFila.cells[1].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
function marcar(nOpcion){
    try {
        var tblTareas = $I("tblTareas");
        for (var i=0; i<tblTareas.rows.length; i++){
            tblTareas.rows[i].cells[1].children[0].checked = (nOpcion==1)?true:false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}

