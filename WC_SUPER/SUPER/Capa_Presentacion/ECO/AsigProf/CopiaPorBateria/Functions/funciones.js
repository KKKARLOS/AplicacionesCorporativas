function init(){
    try{
	    //activarGrabar();
	    AccionBotonera("grabar", "H");
	    
	    if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }else 
        {
            sValorNodo = $I("cboCR").value;   	    
            $I("lblNodo").className = "texto";
        }
        $I("divProyAsig").children[0].innerHTML = "<table id='tblDatos2' class='texto MM' style='WIDTH: 440px;'><colgroup><col style='width:20px' /><col style='width:20px' /><col style='width:20px' /><col style='width:50x;' /><col style='width:310px;' /><col style='width:20px' /></colgroup></table>"; 
	    $I("txtApe1").focus();
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
            //$I("divCatalogo2").children[0].innerHTML = "<table id='tblOpciones3' class='texto MAM' style='WIDTH: 440px;' cellSpacing='0' border='0'><colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup></table>";
            BorrarFilasDe("tblOpciones2");
            $I("ambAp").style.display = "none";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambPE").style.display = "none";
            $I("ambOF").style.display = "none";

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
                case "O":
                    $I("ambOF").style.display = "block";
                    break;
            }
            sAmb = sOp;
            buscarProfesionales();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
	    $I("divTareas").children[0].innerHTML ="<table id='tblTareas'></table>";
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
	    if (strInicial == "@#@@#@") return;

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function obtenerCR(){
    try{
	    var aOpciones;

	    var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=T";
	    mostrarProcesando();
	    //var ret = window.showModalDialog(strEnlace, self, sSize(450, 450));
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
	        .then(function(ret) {
                if (ret != null){
                    aOpciones = ret.split("@#@");
                    $I("hdnCRBusq").value = aOpciones[0];
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
	    var strEnlace = "";
        //Si es administrador o super administrador
	    if (es_administrador == "A" || es_administrador == "SA") strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=";
	    else strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnCRActual").value;

        mostrarProcesando();
	    //var ret = window.showModalDialog(strEnlace, self, sSize(450, 450));
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
	        .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                $I("hdnGFBusq").value = aOpciones[0];
	                $I("txtGF").value = aOpciones[1];
	                mostrarRelacionTecnicos("G", aOpciones[0], "", "");
	            } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function obtenerPE(){
    try{
	    var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge";
	    //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null){
	                var aDatos = ret.split("///");
                    $I("txtCodPE").value = aDatos[3].ToString("N", 9, 0);
                    $I("txtPE").value = aDatos[4];
                    $I("t305_idproyectosubnodo").value = aDatos[0];
                    mostrarRelacionTecnicos("P", aDatos[0],"","");
                }
	        }); 	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el proyecto económico", e.message);
    }
}
function obtenerOF() {
    try {
        var aOpciones;
        var strEnlace = strServer + "Capa_Presentacion/ECO/getOficina.aspx?";

        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(620, 450))
	        .then(function (ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                $I("txtOficina").value = aOpciones[1];
	                $I("hdnIdOficina").value = aOpciones[0];
	                mostrarRelacionTecnicos("O", aOpciones[0], "", "");
	            } else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las oficinas", e.message);
    }
}


function mostrarRelacionTecnicos(sOpcion, sValor1, sValor2, sValor3){
    var sCodUne="",sNumPE="";
    try{
        //if (sOpcion=="G") sCodUne=$I("hdnCRActual").value;
        //if (sOpcion=="C") sCodUne=sValor1;
        if (sOpcion == "P") sNumPE = sValor1;
        if (sOpcion == "N") {
            sValor1= Utilidades.escape($I("txtApe1").value);
            sValor2= Utilidades.escape($I("txtApe2").value);
            sValor3= Utilidades.escape($I("txtNom").value);
            if (sValor1=="" && sValor2=="" && sValor3==""){
                mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
                return;
            }
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion + "@#@" + sValor1 + "@#@" + sValor2 + "@#@" + sValor3 + "@#@" + sNumPE + "@#@";
        //Añado los tipos de profesional
        if ($I("chkTipoProf_0").checked)
            js_args += "I,";//Internos
        if ($I("chkTipoProf_1").checked)
            js_args += "E,T,";//Externos y ETT
        if ($I("chkTipoProf_2").checked)
            js_args += "F";//Foráneos
        js_args += "@#@";
        if ($I("chkSoloActivos").checked)
            js_args += "S";
        else
            js_args += "N";

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function buscarProfesionales() {
    try {
        var sOp = getRadioButtonSelectedValue("rdbAmbito", false);

        switch (sOp) {
            case "A":
                if ($I("txtApe1").value != "" || $I("txtApe2").value != "" || $I("txtNom").value != "")
                    mostrarRelacionTecnicos('N', '', '', '');
                break;
            case "C":
                if ($I("txtCR").value != "" && $I("hdnCRBusq").value != "")
                    mostrarRelacionTecnicos("C", $I("hdnCRBusq").value, "", "");
                break;
            case "G":
                if ($I("txtGF").value != "" && $I("hdnGFBusq").value!= "")
                    mostrarRelacionTecnicos("G", $I("hdnGFBusq").value, "", "");
                break;
            case "P":
                if ($I("t305_idproyectosubnodo").value != "")
                    mostrarRelacionTecnicos("P", $I("t305_idproyectosubnodo").value, "", "");
                break;
            case "O":
                if ($I("hdnIdOficina").value != "")
                    mostrarRelacionTecnicos("O", $I("hdnIdOficina").value, "", "");
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscarProfesionales", e.message);
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
        oCloneNode.setAttribute("bd", "I");
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
        if ($I("tblOpciones3").rows.length==0){
            mmoff("War", "No hay usuarios para asignar", 220);
            return false;
        }
        if ($I("tblDatos2").rows.length == 0) {
            mmoff("War", "No hay proyectos para asignar", 230);
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
/* lista de recursos seleccionados @#@ lista de proyectos
*/
    var sRes="",sw=0, sDeriva;
    try{
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
        var aProys = FilasDe("tblDatos2");
        for (var i=0;i<aProys.length;i++){
            sw = 1;
            if (aProys[i].cells[5].children[0].checked) sDeriva="T";
            else sDeriva="F";
            sRes += aProys[i].id + "," + aProys[i].getAttribute("nodo") + "," + aProys[i].getAttribute("cierre") + "," + sDeriva + "##";
        }
        if (sw == 1) sRes = sRes.substring(0, sRes.length-2);

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
        //desActivarGrabar();
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}
function getItems(idRecurso){
    try{
        var js_args = "getItems@#@"+ idRecurso;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener proyectos del usuario", e.message);
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
//            case "buscar":
//                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
//                $I("divCatalogo").children[0].innerHTML = aResul[2];
//                scrollTablaProf();
//		        actualizarLupas("tblTitulo", "tblOpciones");
//                
//        	    $I("txtApellido1").value = "";
//        	    $I("txtApellido2").value = "";
//        	    $I("txtNombre").value = "";
//                break;
            case "tecnicos":
                $I("divRelacion").children[0].innerHTML = aResul[2];
                scrollTablaProf2();
		        actualizarLupas("tblTitRec", "tblOpciones2");
        	    //$I("txtApe1").value = "";
        	    //$I("txtApe2").value = "";
        	    //$I("txtNom").value = "";
                break;
            case "grabar":
                //actualizarLupas("tblTitulo2", "tblOpciones2");
                //desActivarGrabar();
                bCambios=false;
                //popupWinespopup_winLoad();
                mmoff("Suc", "Grabación correcta", 160); 
                
                break;
			case "PROYECTOS":
				$I("divCatalogo").children[0].innerHTML = aResul[2];
				//$I("divProyAsig").children[0].innerHTML = "<TABLE id='tblDatos2' style='WIDTH: 450px;' class='texto' cellSpacing='0' border='0'><colgroup><col style='width:20px' /><col style='width:20px' /><col style='width:20px' /><col style='width:50x; text-align:right; padding-right:10px;' /><col style='width:340px;cursor:pointer;' /></colgroup></TABLE>";                          
				actualizarLupas("tblCatIni", "tblDatos");	
				if (aResul[3]=="T")		
                    scrollTablaProy();
                else
                    mmoff("War", "No se han encontrado proyectos a los que se pueda asignar profesionales",450);
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
                if (oFila.getAttribute("baja")=="1") 
                    oFila.cells[1].style.color = "red";
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

                //oFila.onclick = function (){mmse(this);};
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
//function marcar(nOpcion){
//    try{
//        for (var i=0; i<tblTareas.rows.length; i++){
//            tblTareas.rows[i].cells[1].children[0].checked = (nOpcion==1)?true:false;
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
//	}
//}
////////////////////////////////////////////////////////////////////////////////
//  FUNCIONES PARA EL MANEJO DE LOS CONTROLES DE LA SELECCION DE PROYECTOS /////
////////////////////////////////////////////////////////////////////////////////
function marcar2(nOpcion){
    try{
        for (var i=0; i<$I("tblDatos2").rows.length; i++){
            $I("tblDatos2").rows[i].cells[5].children[0].checked = (nOpcion==1)?true:false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}
function insertarItem(oFila){
    
    try{
        var idItem = oFila.id;
        var bExiste = false;

        for (var i=0; i < $I("tblDatos2").rows.length; i++){
            if ($I("tblDatos2").rows[i].id == idItem){
                bExiste = true;
                break;
            }
        }
        if (bExiste){
            //alert("El item indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;
        var sNuevo = oFila.cells[4].innerText;
        
        for (var iFilaNueva=0; iFilaNueva < $I("tblDatos2").rows.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual=$I("tblDatos2").rows[iFilaNueva].cells[4].innerText;
            if (sActual>sNuevo)break;
        }
        // Se inserta la fila
        var NewRow;
        NewRow = $I("tblDatos2").insertRow(iFilaNueva);
        
        var oCloneNode	= oFila.cloneNode(true);
//        oCloneNode.onmousedown = function(){DD(this)};
//        oCloneNode.onclick = function() { mmse(this) };
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        
        //oCloneNode.className = "";
        oCloneNode.className = "MM";
        NewRow.swapNode(oCloneNode);
        
        //oCloneNode.cells[4].children[0].className = "MM";
        oCloneNode.cells[4].style.width="310px";
        oCloneNode.insertCell(5);
        //oCloneNode.cells[5].appendChild(document.createElement("<input type='checkbox' style='width:15px; height:15px;' class='checkTabla' checked='true'>"));
        var oCtrl2 = document.createElement("input");
        oCtrl2.setAttribute("type", "checkbox");
        oCtrl2.setAttribute("className", "check");
        oCtrl2.setAttribute("checked", "true");
        //oCtrl2.style.width = "15px";
        //oCtrl2.style.height = "15px";
        oCtrl2.setAttribute("style", "width:13px; height:13px; padding-top:0px; padding-bottom:0px;");
        oCloneNode.cells[5].appendChild(oCtrl2);
        
        actualizarLupas("tblAsignados", "tblDatos2");
       
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}
function getCliente(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0", self, sSize(600, 480));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0", self, sSize(600, 480))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                } else ocultarProcesando();
	        }); 

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function borrarCliente(){
    try{
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        borrarCatalogo();
	    if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar(){
    try{
        var js_args = "PROYECTOS@#@";
        js_args += sValorNodo +"@#@";
        js_args += $I("cboEstado").value +"@#@";
        js_args += $I("cboCategoria").value +"@#@";
        js_args += $I("hdnIdCliente").value +"@#@";
        js_args += $I("hdnIdResponsable").value +"@#@";
        js_args += dfn($I("txtNumPE").value) +"@#@";
        js_args += Utilidades.escape($I("txtDesPE").value) +"@#@";
        js_args += getRadioButtonSelectedValue("rdbTipoBusqueda", true) +"@#@";
        js_args += $I("cboCualidad").value +"@#@";
        js_args += $I("txtIDContrato").value.replace(".","") +"@#@";
        js_args += $I("hdnIdHorizontal").value + "@#@";
        js_args += $I("hdnNaturaleza").value + "@#@";

        //alert(js_args);     
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

function getNodo(){
    try{
        if ($I("lblNodo").className == "texto") return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx";
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 500));
        modalDialog.Show(strEnlace, self, sSize(500, 470))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            sValorNodo = aDatos[0];
		            $I("hdnIdNodo").value = aDatos[0];
		            $I("txtDesNodo").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
	            }else ocultarProcesando();
	        }); 	    //alert(ret);
	}catch(e){
		mostrarErrorAplicacion("Error en la función getNodo ", e.message);
    }
}

function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
        }else{
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        
        $I("divCatalogo").children[0].innerHTML = "";
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el nodo", e.message);
    }
}

function getResponsable(){
    try{
        mostrarProcesando();
	    //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto", self, sSize(550, 540));
	    var strEnlace = strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto";
	    modalDialog.Show(strEnlace, self, sSize(550, 540))
	        .then(function(ret) {
                if (ret != null){
	                var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }else ocultarProcesando();
	        }); 

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function borrarResponsable(){
    try{
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        borrarCatalogo();
	    if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}

function setNumPE(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
        }else{
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        $I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("cboCualidad").value = "";
        $I("txtDesPE").value = "";
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function setDesPE(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
        }else{
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        $I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("cboCualidad").value = "";
        $I("txtNumPE").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir la denominación de proyecto", e.message);
    }
}

function getHorizontal(){
    try{
        mostrarProcesando();
	    //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getHorizontal.aspx", self, sSize(400, 480));
	    var strEnlace = strServer + "Capa_Presentacion/ECO/getHorizontal.aspx";
	    modalDialog.Show(strEnlace, self, sSize(400, 480))
	        .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("hdnIdHorizontal").value = aDatos[0];
	                $I("txtDesHorizontal").value = aDatos[1];
	                borrarCatalogo();
	                if ($I("chkActuAuto").checked) buscar();
	                else ocultarProcesando();
	            } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener Horizontal", e.message);
    }
}

function borrarHorizontal(){
    try{
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
        if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el horizontal", e.message);
    }
}

function getContrato(){
    try{

//        if (sValorNodo == ""){
//            alert("Para poder obtener el contrato, antes debe seleccionar el "+ strEstructuraNodo);
//            return;
//        }
        
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getContrato.aspx?nNodo=" + sValorNodo + "&origen=busqueda", self, sSize(1020, 550));
        var strEnlace = strServer + "Capa_Presentacion/ECO/getContrato.aspx?nNodo=" + sValorNodo + "&origen=busqueda";
        modalDialog.Show(strEnlace, self, sSize(1020, 550))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("///");
		            $I("txtIDContrato").value = aDatos[0];
		            $I("txtDesContrato").value = Utilidades.unescape(aDatos[1]);
                    $I("hdnIdCliente").value = aDatos[2];
                    $I("txtDesCliente").value = Utilidades.unescape(aDatos[3]);
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }else ocultarProcesando();
	        }); 
	    //alert(ret);

	}catch(e){
		mostrarErrorAplicacion("Error al obtener el contrato", e.message);
    }
}

function borrarContrato(){
    try{
        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el contrato", e.message);
    }
}

function getNaturaleza() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNaturalezaSimple.aspx";
        modalDialog.Show(strEnlace, self, sSize(420, 460))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("hdnNaturaleza").value = aDatos[0];
	                $I("txtDesNaturaleza").value = aDatos[1];
	                borrarCatalogo();
	                if ($I("chkActuAuto").checked) buscar();
	                else ocultarProcesando();
	            } else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener Naturaleza", e.message);
    }
}

function borrarNaturaleza() {
    try {
        $I("hdnNaturaleza").value = "";
        $I("txtDesNaturaleza").value = "";
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la naturaleza", e.message);
    }
}

//function setActuAuto(){
//    try{
//        if ($I("chkActuAuto").checked){
//            setOp($I("btnObtener"), 30);
//            buscar();
//        }else{
//            setOp($I("btnObtener"), 100);
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
//	}
//}

function setCombo(){
    try{
        borrarCatalogo();
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function mdpsn(oNOBR){
    try{
        insertarItem(oNOBR.parentNode.parentNode);
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar proyecto", e.message);
	}
}
var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProy){
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", "1");
                
                oFila.ondblclick = function(){insertarItem(this)};
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("categoria") == "P") 
                    oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else 
                    oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                
                switch (oFila.getAttribute("cualidad")){
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")){
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}
function desasignarProy(){
    try{
        var aFilas = FilasDe("tblDatos2");
        if (aFilas.length == 0) return;
        for (var i=aFilas.length-1; i>=0; i--){
            if (aFilas[i].className.toUpperCase() == "FS"){
                $I("tblDatos2").deleteRow(aFilas[i].rowIndex);
            }
        }
        actualizarLupas("tblAsignados", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al desasignar un proyecto", e.message);
	}
}
function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{
	    switch (oElement.tagName) {
	        case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
	    }
	    oTable = oTarget.getElementsByTagName("TABLE")[0];
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
		            if (oRow.getAttribute("bd") == "I") {
	                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
	                }    
	                else mfa(oRow, "D");
			        break;
		        case "divCatalogo2":
		        case "ctl00_CPHC_divCatalogo2":
		            if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i=0;i<oTable.rows.length;i++){
	                    if (oTable.rows[i].id == oRow.id){
		                    sw = 1;
		                    break;
	                    }
                    }
                    if (sw == 0){
                        var NewRow;
                        if (nIndiceInsert == null){
                            nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length) 
                                nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;
                        var oCloneNode	= oRow.cloneNode(true);
                        oCloneNode.className = "";
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        oCloneNode.style.cursor = strCurMM;
                        NewRow.swapNode(oCloneNode);
                	    
                        mfa(oCloneNode, "I");
                    }
			        break;
		        case "imgPapelera2":
		        case "ctl00_CPHC_imgPapelera2":                
		            if (nOpcionDD == 3){
		                if (oRow.getAttribute("bd") == "I") {
		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                }    
		                else mfa(oRow, "D");
		            }else if (nOpcionDD == 4){
		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		            }
			        break;	                
		        case "divProyAsig":
		        case "ctl00_CPHC_divProyAsig":
		            if (FromTable == null || ToTable == null) continue;
		            //var oTable = oTarget.getElementsByTagName("TABLE")[0];
		            var sw = 0;
		            //Controlar que el elemento a insertar no existe en la tabla
		            for (var i = 0; i < oTable.rows.length; i++) {
		                if (oTable.rows[i].id == oRow.id) {
		                    sw = 1;
		                    break;
		                }
		            }
		            if (sw == 0) {
		                var NewRow;
		                if (nIndiceInsert == null) {
		                    nIndiceInsert = oTable.rows.length;
		                    NewRow = oTable.insertRow(nIndiceInsert);
		                }
		                else {
		                    if (nIndiceInsert > oTable.rows.length)
		                        nIndiceInsert = oTable.rows.length;
		                    NewRow = oTable.insertRow(nIndiceInsert);
		                }
		                nIndiceInsert++;
		                
		                var oCloneNode = oRow.cloneNode(true);
		                oCloneNode.attachEvent('onclick', mm);
		                oCloneNode.attachEvent('onmousedown', DD);
		                oCloneNode.style.cursor = strCurMM;
		                oCloneNode.className = "";
		                NewRow.swapNode(oCloneNode);
		                //oCloneNode.className = "MM";
		                //oCloneNode.cells[4].children[0].className = "MM";
		                oCloneNode.cells[4].style.width = "310px";
		                oCloneNode.insertCell(5);
		                //oCloneNode.cells[5].appendChild(document.createElement("<input type='checkbox' style='width:15' class='checkTabla' checked='true'>"));
		                var oCtrl2 = document.createElement("input");
		                oCtrl2.setAttribute("type", "checkbox");
		                oCtrl2.setAttribute("className", "check");
		                oCtrl2.setAttribute("checked", "true");
		                oCtrl2.setAttribute("style", "width:13px; height:13px; padding-top:0px; padding-bottom:0px;");
		                oCloneNode.cells[5].appendChild(oCtrl2);

		            }
		            break;			        			        
			}
		}
		actualizarLupas("tblTitRecAsig", "tblOpciones3");		
		actualizarLupas("tblAsignados", "tblDatos2");		
		activarGrabar();
	}
	oTable = null;
	killTimer();
	CancelDrag();
	
	obj.style.display	= "none";
	oEl					= null;
	aEl.length = 0;
	oTarget				= null;
	beginDrag			= false;
	TimerID				= 0;
	oRow                = null;
    FromTable           = null;
    ToTable             = null;
}
