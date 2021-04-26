function init(){
    try{
        scrollTablaProfAsig();
        actualizarLupas("tblTitulo2", "tblOpciones2");
        $I("txtDesGF").focus();   


        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function () { getNodo() };
            sValorNodo = $I("hdnIdNodo").value;
        }
        else {
            $I("lblNodo").className = "texto";
            sValorNodo = $I("hdnIdNodo").value;
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function getNodo() {
    try {
        if ($I("lblNodo").className == "texto") return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 460))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];                    
                    $I("txtDesNodo").value = aDatos[1];
                    $I("divCatalogo").children[0].innerHTML = "";
                    activarGrabar();
                    buscar(1, 0);
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el nodo ", e.message);
    }
}

var sAmb = "A";
function seleccionAmbito(strRblist) {
    try {
        var sOp = getRadioButtonSelectedValue("rdbAmbito", false);
        if (sOp == sAmb) return;
        else {
            $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones' class='texto MAM' style='WIDTH: 350px;'><colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup><tbody id='tbodyOrigen'></table>";
            $I("ambAp").style.display = "none";
            $I("ambCR").style.display = "none";
            $I("ambPE").style.display = "none";

            switch (sOp) {
                case "A":
                    $I("txtApe1").value = "";
                    $I("txtApe2").value = "";
                    $I("txtNom").value = "";                
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("txtCR").value = "";
                    $I("ambCR").style.display = "block";
                    break;
                case "P":
                    $I("txtCodPE").value = "";
                    $I("txtPE").value = "";
                    $I("ambPE").style.display = "block";
                    break;
            }
            sAmb = sOp;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
	    strInicial = Utilidades.escape($I("txtApe1").value) + "@#@" + Utilidades.escape($I("txtApe2").value) + "@#@" +
                     Utilidades.escape($I("txtNom").value);
	    if (strInicial == "@#@@#@") return;
	    strInicial += "@#@" + $I("hdnCR").value;
	    setTimeout("mostrarProfesionalAux('"+strInicial+"')",30);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function mostrarProfesionalAux(strInicial) {
    try {
        var js_args = "buscar@#@" + strInicial + "@#@";

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function mostrarRelacionTecnicos(sOpcion, sValor1, sValor2, sValor3) {
    var sCodUne = "", sNumPE = "", sCualidad = "";
    try {
        if (sOpcion == "C") sCodUne = sValor1;
        if (sOpcion == "P") sNumPE = sValor1;
        if (sOpcion == "N") {
            sValor1 = Utilidades.escape($I("txtApe1").value);
            sValor2 = Utilidades.escape($I("txtApe2").value);
            sValor3 = Utilidades.escape($I("txtNom").value);
            if (sValor1 == "" && sValor2 == "" && sValor3 == "") {
                mmoff("Inf", "Debes indicar algún criterio para la búsqueda por apellidos/nombre", 410);
                return;
            }
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion + "@#@" + sValor1 + "@#@" + sValor2 + "@#@" + sValor3 + "@#@" +
                    sCodUne + "@#@" + sNumPE + "@#@" + sCualidad + "@#@" + $I("hdnCR").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function obtenerCR() {
    try {
        var aOpciones;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx";
        if ($I("hdnEsSoloRGF").value == "S")
            strEnlace += "?t=R";
        else
            strEnlace += "?t=T";
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
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}
function obtenerPE() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtCodPE").value = aDatos[3].ToString("N", 9, 0);
                    $I("txtPE").value = aDatos[4];
                    // $I("t305_idproyectosubnodo").value = aDatos[0];
                    mostrarRelacionTecnicos("P", aDatos[0], "", "");
                }
            });
        window.focus();
        ocultarProcesando(); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el proyecto económico", e.message);
    }
}
function anadirConvocados(){
    var sMens, sMensTotal="",sAux, iNumGFs=0;
    var bMasDeUnGF;//bActivarG=false;
    try{
	    if (bLectura) return;
	    var sAux = $I("hdnMasDeUnGF").value;
	    if (sAux=="F")
	        bMasDeUnGF=false;
	    else
	        bMasDeUnGF=true;
	    var aFilas = $I("tblOpciones").rows;
	    if (aFilas.length > 0){
		    for (x=0;x<aFilas.length;x++){
			    sMens="";
			    if (!estaEnLista(aFilas[x].id)){
			        if (aFilas[x].className == "FS"){
				        if (!bMasDeUnGF){
				            iNumGFs=aFilas[x].getAttribute("nGF");
				            if (iNumGFs>0){
				                //sMens="No se puede asociar " + aFilas[x].innerText + " a más de un Grupo Funcional";
				                sMens=aFilas[x].cells[1].innerText + "\n";
				                sMensTotal+=sMens;
				            }
				        }
				        if (sMens==""){
				            sAux=convocar(aFilas[x].id, aFilas[x].cells[1].innerText);
				        }
			        }
			    }    
		    }
            if (sMensTotal != ""){
                sMensTotal="El CR en curso no permite asociar un profesional a más de un grupo funcional del mismo CR.\nLa relación de profesionales que se muestra incumplirían la restricción al pertenecer ya a otro grupo funcional,\npor lo que la asociación no se permite.\n\n" + sMensTotal;
                mmoff("InfPer",sMensTotal,400);
            }
		}
		actualizarLupas("tblTitulo2", "tblOpciones2");
	}catch(e){
		mostrarErrorAplicacion("Error al añadir integrantes", e.message);
    }
}

function estaEnLista(idUsuario){
    try{
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    //alert("Persona ya incluida");
				    return true;
			    }
		    }
	    }
		return false;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar si el integrante está en la lista", e.message);
    }
}
function convocar(idUsuario, strUsuario, bIndividual){
    try{
	    if (bLectura) return;
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

        if (iFila >= 0 && $I("tblOpciones") != null) modoControles($I("tblOpciones").rows[iFila], false);
	    sNombreNuevo = strUsuario;
	    var aFilas = $I("tblOpciones2").rows;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].cells[2].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
        oNF.style.height = "20px";
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd","I");
	    oNF.style.cursor = "pointer";
	    oNF.setAttribute("sw", "1");

	    oNF.attachEvent('onclick', mm);
	    oNF.attachEvent('onmousedown', DD);

	    oNF.insertCell(-1).appendChild(oImgFI.cloneNode());

	    oNF.insertCell(-1).appendChild($I("tblOpciones").rows[iFila].children[0].cloneNode(true));
    	
	    oNC3 = oNF.insertCell(-1);
	    oNC3.style.width = "330px";
	    oNC3.innerText = strUsuario;

	    var oCtrl1 = document.createElement("label");
	    oCtrl1.id = "lbl" + idUsuario;
	    oCtrl1.className="texto";
	    oCtrl1.setAttribute("style", "text-overflow:ellipsis;overflow:hidden");
	    oCtrl1.value = strUsuario;
	    //oCtrl1.onclick = function() { exclusivo(this, 2) };
	    oNC3.appendChild(oCtrl1);
	    
    	//oNC3.appendChild(document.createElement("<label class=texto id='lbl"+idUsuario+"' style='text-overflow:ellipsis;overflow:hidden' value='"+strUsuario+"'>"));

	    oNC4 = oNF.insertCell(-1);
	    oNC4.style.width = "15px";

	    var oCtrl2 = document.createElement("input");
	    oCtrl2.type="checkbox";
	    oCtrl2.id = "chk" + idUsuario;
	    oCtrl2.name = "chk" + idUsuario;
	    oCtrl2.className = "checkTabla";

	    oNC4.appendChild(oCtrl2);	    
	    
    	//oNC4.appendChild(document.createElement("<input type='checkbox' name='chk"+idUsuario+"' id='chk"+idUsuario+"' class='checkTabla' >"));

	    if (bIndividual) actualizarLupas("tblTitulo2", "tblOpciones2");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
function desconvocar(nFila){
    try{
	    if (bLectura) return;
        
	    //$I("tblOpciones2").deleteRow(nFila);
	    var aFila = $I("tblOpciones2").rows;
        if (aFila[nFila].getAttribute("bd") == "I"){
            //Si es una fila nueva, se elimina
            $I("tblOpciones2").deleteRow(nFila);
        }    
        else{
            mfa(aFila[nFila],"D");
        }
	    
	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al quitar integrante", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("txtDesGF").value == "") {
            mmoff("War", "Debes indicar el nombre del grupo funcional", 270);
            return false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function flGetIntegrantes(){
    /*Recorre la tabla de Integrantes para obtener una cadena que se pasará como parámetro
      al procedimiento de grabación
    */
    var sRes="",sCodigo,sResponsable, sTipoOperacion;
    var bGrabar=false,bActivo=false;
    try{
        aFila = $I("tblOpciones2").getElementsByTagName("TR");
        for (i=0;i<aFila.length;i++){
            sCodigo = aFila[i].id;
            sTipoOperacion = aFila[i].getAttribute("bd");
            bActivo= aFila[i].cells[3].children[0].checked;
            if (bActivo)sResponsable="1";
            else sResponsable="0";
            sRes += sTipoOperacion+"##"+ sCodigo + "##" + sResponsable + ",";
        }//for
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function grabar(){
    try{
        if (iFila >= 0 && $I("tblOpciones")!=null) modoControles($I("tblOpciones").rows[iFila], false);
        if (!comprobarDatos()) return;
        
        js_args = "grabar@#@" + $I("hdnCR").value + "@#@" + $I("hdnIdGf").value + "#" + Utilidades.escape($I("txtDesGF").value) + "@#@" + sValorNodo + "@#@";
        js_args += flGetIntegrantes();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios=false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
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
            case "tecnicos":                
        	//La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblOpciones");

                $I("txtApe1").value = "";
                $I("txtApe2").value = "";
        	    $I("txtNom").value = "";
        	    break;
        	           	                  
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblOpciones2").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                desActivarGrabar();
                AccionBotonera("grabar","D");
                $I("hdnIdGf").value = aResul[2];
                $I("divCatalogo2").children[0].innerHTML = aResul[3];
                scrollTablaProfAsig();
                actualizarLupas("tblTitulo2", "tblOpciones2");
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) AccionBotonera("regresar", "P");

                break;
        }
        ocultarProcesando();
    }
}
function fnRelease(e) {
    //alert('entra fnRelease');
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);

    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);

    }

    var obj = $I("DW");
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
                        NewRow.swapNode(oCloneNode);
                	    
                        oCloneNode.insertCell(0);
                        //oCloneNode.cells[0].appendChild(document.createElement("<img src='../../../../../images/imgFI.gif'>"));

                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true), null);
                        oCloneNode.style.cursor = "../../../../../images/imgManoMove.cur";
                        
                        
	                    oCloneNode.insertCell(3);

	                    var oCtrl1 = document.createElement("input");
	                    oCtrl1.type = "checkbox";
	                    oCtrl1.id = "chk" + x;
	                    oCtrl1.name = "chk" + x;
	                    oCtrl1.className = "checkTabla";

	                    oCloneNode.cells[3].appendChild(oCtrl1);		  	  
	                                      
	                    //oCloneNode.cells[3].appendChild(document.createElement("<input type='checkbox' name='chk"+x+"' id='chk"+x+"' class='checkTabla'>"));
                        
                        mfa(oCloneNode, "I");
                    }
			        break;
			}
		}
		actualizarLupas("tblTitulo2", "tblOpciones2");
		
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
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblOpciones.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones.rows[i].getAttribute("sw")) {
                oFila = tblOpciones.rows[i];
                oFila.setAttribute("sw",1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N":
                            oFila.cells[0].appendChild(oImgNV.cloneNode(true), null);
                            break;
                        case "P":
                            oFila.cells[0].appendChild(oImgPV.cloneNode(true), null);
                            break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N":
                            oFila.cells[0].appendChild(oImgNM.cloneNode(true), null);
                            break;
                        case "P":
                            oFila.cells[0].appendChild(oImgPV.cloneNode(true), null);
                            break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja")=="1") 
                    oFila.cells[1].style.color = "red";
            }
//            nContador++;
//            if (nContador > $I("divCatalogo").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
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
        var tblOpciones2 = $I("tblOpciones2");
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, tblOpciones2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones2.rows[i].getAttribute("sw")) {
                oFila = tblOpciones2.rows[i];
                oFila.setAttribute("sw",1);
               
                oFila.attachEvent('onclick', mm);
                
                if (oFila.cells[0].children[0]==null){
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(true), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(true), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(true), null); break;
                    }
                }
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1") 
                    oFila.cells[2].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
