function init(){
    try{
	    //activarGrabar();
        AccionBotonera("procesar", "H");
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
            $I("divCatalogo2").children[0].innerHTML = "<table id='tblOpciones3' class='texto MAM' style='WIDTH: 440px;'><colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup></table>";
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
function borrarCatalogo() {
    try
    {
        $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
    }catch(e){
    mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}    
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
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
	    //var ret = window.showModalDialog(strEnlace, self, sSize(450, 450));
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
	        .then(function(ret) {
	            if (ret != null){
		            aOpciones = ret.split("@#@");
		            $I("txtCR").value = aOpciones[1];
                    mostrarRelacionTecnicos("C", aOpciones[0],"","");
                } else ocultarProcesando();
	        }); 
	}catch(e){	
		mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}

function obtenerGF(){
    try{
	    var aOpciones;
	    if ($I("hdnCRActual").value == "") return;
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR="+$I("hdnCRActual").value;
	    //var ret = window.showModalDialog(strEnlace, self, sSize(450, 450));
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
	        .then(function(ret) {
	            if (ret != null){
		            aOpciones = ret.split("@#@");
		            $I("txtGF").value = aOpciones[1];
                    mostrarRelacionTecnicos("G", aOpciones[0],"","");
                } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function obtenerPE(){
    try {
        mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge";
	    //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                $I("txtCodPE").value = aDatos[3].ToString("N", 9, 0);
	                $I("txtPE").value = aDatos[4];
	                $I("t305_idproyectosubnodo").value = aDatos[0];
	                mostrarRelacionTecnicos("P", aDatos[0], "", "");
	            } else ocultarProcesando();
	        }); 
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
        //AccionBotonera("procesar", "H");
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
    }
}
function comprobarDatos(){
    try{
        var sw=0;
        var aOrigen = FilasDe("tblOpciones");
        if (aOrigen != null){
            for (var i=0; i < aOrigen.length; i++){
                if (aOrigen[i].className == "FS"){
                    sw=1;
                    break;
                }
            }
        }
        if (sw != 1){
            mmoff("War", "Debes seleccionar un usuario de referencia", 320);
            return false;
        }
        if ($I("tblOpciones3").rows.length==0){
            mmoff("War", "No hay usuarios para asignar", 220);
            return false;
        }
        return true;
    }
    catch(e){
        mostrarErrorAplicacion("Error al comprobar los datos antes de procesar", e.message);
        return false;
    }
    return true;
}
function flGetIntegrantes(){
/*empleado origen @#@ tipo de item @#@ lista de recursos seleccionados @#@ lista de items + indicador de asignación a proyecto
*/
    var sRes="",sw=0,idRecurso="";
    try{
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
            sRes=idRecurso+"@#@";
            //Lista de recursos destino
            var aDestino = FilasDe("tblOpciones3");
            for (var i=0;i<aDestino.length;i++){
                    sw = 1;
                    sRes += aDestino[i].id +"##"; //ID empleado
            }
            if (sw == 1) sRes = sRes.substring(0, sRes.length-2);
        }
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function procesar(){
    try{
        if (!comprobarDatos()) return;

        js_args = "procesar@#@" + flGetIntegrantes();
        if (js_args != "procesar@#@") {
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
        }
        //desActivarGrabar();
        return true;
	}catch(e){
	    mostrarErrorAplicacion("Error al procesar los datos", e.message);
		return false;
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
            case "procesar":
                //actualizarLupas("tblTitulo2", "tblOpciones2");
                //desActivarGrabar();
                bCambios=false;
                mmoff("Suc", "Permisos asignados", 180); 
                //popupWinespopup_winLoad();
                break;
        }
        ocultarProcesando();
    }
}
function desasignar(){
    try{
        //var sw=0;
        var aFilas = FilasDe("tblOpciones3");
        if (aFilas.length == 0) return;
        for (var i=aFilas.length-1; i>=0; i--){
            if (aFilas[i].className.toUpperCase() == "FS"){
                eliminarRecurso(aFilas[i]);
                //sw=1;
            }
        }
        //if (sw==1) aG(0);
	}catch(e){
		mostrarErrorAplicacion("Error al desasignar a un usuario", e.message);
	}
}
function eliminarRecurso(oFila){
    try{
//        if (oFila.bd=="I")
            $I("tblOpciones3").deleteRow(oFila.rowIndex);
//        else
//            mfa(oFila, "D");
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
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, $I("tblOpciones").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones.rows.length; i++){
            if (!$I("tblOpciones").rows[i].getAttribute("sw")){
                oFila = $I("tblOpciones").rows[i];
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
                if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 20); 
            }
//            nContador++;
//            if (nContador > $I("divCatalogo").offsetHeight/20 +1) break;
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
        
        var nFilaVisible = Math.floor(nTopScrollProf2/20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacion").offsetHeight/20+1, $I("tblOpciones2").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones2.rows.length; i++){
            if (!$I("tblOpciones2").rows[i].getAttribute("sw")){
                oFila = $I("tblOpciones2").rows[i];
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
                if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 20); 
            }
//            nContador++;
//            if (nContador > $I("divRelacion").offsetHeight/20 +1) break;
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
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, $I("tblOpciones3").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones3.rows.length; i++){
            if (!$I("tblOpciones3").rows[i].getAttribute("sw")){
                oFila = $I("tblOpciones3").rows[i];
                oFila.setAttribute("sw", "1");
                oFila.attachEvent('onclick', mm);
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
                if (oFila.getAttribute("baja")=="1") 
                    oFila.cells[1].style.color = "red";
            }
//            nContador++;
//            if (nContador > $I("divCatalogo2").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}

