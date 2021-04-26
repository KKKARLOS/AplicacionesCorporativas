var sValorNodo = "";
var bGetNodoAdm = false;
var bGetNodo = false;
var sNuevoValorNodo = "";

function init(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }else sValorNodo = $I("cboCR").value;
	    $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
        if (sValorNodo==""){
            mmoff("Inf", "Es obligatorio seleccionar un "+ strEstructuraNodo, 270);
            return;
        }
	    
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + 
	              "@#@" + Utilidades.escape($I("txtNombre").value) + "@#@" + sValorNodo;
	    //if (strInicial == "@#@@#@@#@") return;

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}

function convocar(idUsuario, strUsuario){
    try{
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (var i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    //alert("Persona ya incluida");
				    return;
			    }
		    }
	    }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

	    sNombreNuevo = strUsuario;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
        oNF.style.height = "20px";
	    oNF.id = idUsuario;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", "1");
	    oNF.attachEvent('onclick', mm);
	    oNF.attachEvent('onmousedown', DD);

	    oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1).appendChild($I("tblOpciones").rows[iFila].children[0].cloneNode(true));

        //oNC3.appendChild(document.createElement("<label class=texto id='lbl"+idUsuario+"' style='text-overflow:ellipsis;overflow:hidden' value='"+strUsuario+"'>"));
        oNC3 = oNF.insertCell(-1);
	    var oCtrl1 = document.createElement("label");
	    oCtrl1.id = "lbl" + idUsuario + "'";
	    oCtrl1.setAttribute("style", "text-overflow:ellipsis;overflow:hidden");
	    oCtrl1.className = "texto";
	    oCtrl1.value = strUsuario;

	    oNC3.style.width = "330px";
	    oNC3.innerText = strUsuario;
	    oNC3.appendChild(oCtrl1);

	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
function comprobarDatos(){
    try{
       
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function grabar(){
    try{
        if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        //sb.Append(sValorNodo +"@#@");

        aFila = $I("tblOpciones2").getElementsByTagName("TR");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].getAttribute("bd") != ""){
                sw = 1;
                sb.Append(aFila[i].getAttribute("bd") +"##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"///"); //ID usuario
            }
        }

        if (sw == 0){
            desActivarGrabar();
            mmoff("War", "No se han modificado los datos.", 230);
            return;
        }
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), ""); 
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}

function getNodo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetNodoAdm = true;
                    grabar();
                    return;
                }
                else desActivarGrabar();
                LLamadaGetNodo();
            });
        }
        else LLamadaGetNodo();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el " + strEstructuraNodo, e.message);
    }
}
function LLamadaGetNodo() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 500))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                sValorNodo = aDatos[0];
	                $I("hdnIdNodo").value = aDatos[0];
	                $I("txtDesNodo").value = aDatos[1];
	                setNodo();
	            } else ocultarProcesando();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error-2 al ir a obtener el " + strEstructuraNodo, e.message);
    }
}
function setNodo(){
    try{
	    if (bCambios) {
	        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
	            if (answer) {
	                sNuevoValorNodo = $I("cboCR").value;
	                $I("cboCR").value = sValorNodo;
	                bGetNodo = true;
	                grabar();
	                return;
	            }
	            else desActivarGrabar();
	            LLamadaSetNodo();
	        });
	    }
	    else LLamadaSetNodo();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los CRP-1", e.message);
    }
}
function LLamadaSetNodo() {
    try {
        if (es_administrador == "") {
            var oNodo = $I("cboCR");
            sValorNodo = oNodo.value;
        }

        if (sValorNodo == "") {
            $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
            $I("divCatalogo2").children[0].innerHTML = "";
            return;
        }

        var js_args = "getCRP@#@" + sValorNodo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los CRP-2", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getCRP":
                $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                scrollTablaProfAsig();
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
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblOpciones2").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                scrollTablaProfAsig();
                desActivarGrabar();
                iFila = aFila.length;
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bGetNodoAdm){
                    bGetNodoAdm = false;
                    setTimeout("getNodo();", 20);
                }
                if (bGetNodo){
                    bGetNodo = false;
                    $I("cboCR").value = sNuevoValorNodo;
                    setTimeout("setNodo();", 20);
                }
                break;
        }
        ocultarProcesando();
    }
}
function fnRelease(e)
{
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
	                if (oRow.getAttribute("bd") == "I"){
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
                	    
                        oCloneNode.insertCell(0).appendChild(oImgFI.cloneNode(true), null);
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
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, $I("tblOpciones").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblOpciones").rows[i].getAttribute("sw")){
                oFila = $I("tblOpciones").rows[i];
                oFila.setAttribute("sw", "1");
                
                if (oFila.getAttribute("sexo")=="V"){
                    oFila.cells[0].appendChild(oImgPV.cloneNode(true), null);
//                    switch (oFila.tipo){
//                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
//                        case "N":  break;
//                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
//                    }
                }else{
                    oFila.cells[0].appendChild(oImgPM.cloneNode(true), null);
//                    switch (oFila.tipo){
//                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
//                        case "N":  break;
//                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
//                    }
                }
//                if (oFila.baja=="1") 
//                    oFila.cells[1].style.color = "red";
            }
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
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, $I("tblOpciones2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblOpciones2").rows[i].getAttribute("sw")){
                oFila = $I("tblOpciones2").rows[i];
                oFila.setAttribute("sw", "1");

                //oFila.onclick = function (){mmse(this);};
                oFila.attachEvent('onclick', mm);
                if (oFila.cells[0].children[0]==null){
                    switch (oFila.getAttribute("bd")){
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(true), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(true), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("sexo")=="V"){
                    oFila.cells[1].appendChild(oImgPV.cloneNode(true), null);
//                    switch (oFila.tipo){
//                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
//                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
//                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
//                    }
                }else{
                    oFila.cells[1].appendChild(oImgPM.cloneNode(true), null);
//                    switch (oFila.tipo){
//                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
//                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
//                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
//                    }
                }
//                if (oFila.baja=="1") 
//                    oFila.cells[2].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales crp.", e.message);
    }
}

