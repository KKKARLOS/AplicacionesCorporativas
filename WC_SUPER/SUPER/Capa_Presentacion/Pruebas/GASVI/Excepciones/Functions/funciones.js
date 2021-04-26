function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
	    //$I("txtApellido1").focus();
	    setOp($I("tblGrabarSalir"),30);
	    //actualizarLupas("tblTitulo2", "tblOpciones2");
	    scrollTablaProfAsig();
	    bCambios=false;
        ocultarProcesando();
	    $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var sParam;
    try{
	    if (bLectura) return;
	    sParam= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
	    if (sParam == "@#@@#@@#@") return;

    	var js_args = "buscar@#@"+sParam;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function convocar(idUsuario, strUsuario, bActualizar){
    try{
	    if (bLectura) return;
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

        if (iFila >= 0) modoControles(tblOpciones.rows[iFila], false);
	    sNombreNuevo = strUsuario;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd", "I");
	    oNF.attachEvent('onclick', mm);
	    oNF.attachEvent('onmousedown', DD);
	    
        oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../images/imgFI.gif'>"));
        oNF.insertCell(-1).appendChild($I("tblOpciones").rows[iFila].children[0].cloneNode(true));

	    oNC3 = oNF.insertCell(-1);
	    oNC3.innerText = strUsuario;
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
function flGetIntegrantes(){
/*Recorre la tabla de Integrantes para obtener una cadena que se pasará como parámetro
  al procedimiento de grabación
*/
var sRes="",sCodigo, sTipoOperacion;
    var bGrabar=false;
    try{
        aFila = tblOpciones2.getElementsByTagName("TR");
        for (i=0;i<aFila.length;i++){
            sCodigo = aFila[i].id;
            sTipoOperacion = aFila[i].getAttribute("bd");
            sRes += sTipoOperacion+ "," + sCodigo + "##";
        }//for
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function salir(){
    //var strRetorno;
    if (bCambios && intSession > 0){
        if (confirm("Datos modificados. ¿Deseas grabarlos?")){
            bEnviar = grabar();
        }
        else bCambios=false;
    }

    //setTimeout("window.close();", 250);//para que de tiempo a grabar y actualizar "bCambios";
    modalDialog.Close(window, null);
}
function grabarSalir(){
    if (getOp($I("tblGrabarSalir")) != 100) return;
    bSalir = true;
    grabar();
}
function grabar(){
    try{
        if (iFila >= 0) modoControles(tblOpciones.rows[iFila], false);
        if (!comprobarDatos()) return;

        js_args = "grabar@#@" + flGetIntegrantes();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
//        //desActivarGrabar();
//        setTimeout("window.close();", 250);//para que de tiempo a grabar y actualizar "bCambios";
//        return true;
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
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblOpciones");
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
        	    $I("txtApellido1").focus();
                break;
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D") {
                        tblOpciones2.deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                scrollTablaProfAsig();
                desActivarGrabar();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta.", 160);
                bCambios = false;

                if (bSalir)
                    modalDialog.Close(window, null);
                break;
        }
        ocultarProcesando();
    }
}
function activarGrabar(){
    try{
        //if ($I("hdnAcceso").value!="R"){
            setOp($I("tblGrabarSalir"),100);
            bCambios = true;
            //bHayCambios=true;
        //}
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function desActivarGrabar(){
    try{
        setOp($I("tblGrabarSalir"),30);
        bCambios = false;
        //bHayCambios=false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
	}
}
function fnRelease()
{
    if (beginDrag == false) return;
       				    
	window.document.detachEvent( "onmousemove" , fnMove );
	window.document.detachEvent( "onscroll" , fnMove );
	window.document.detachEvent( "onmousemove" , fnCheckState );
	window.document.detachEvent( "onmouseup" , fnRelease );
	window.document.detachEvent( "onselectstart", fnSelect );
	
	var obj = $I("DW");
	var nIndiceInsert = null;
	var oTable;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{	
	    switch (event.srcElement.tagName){
	        case "TD": nIndiceInsert = event.srcElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = event.srcElement.parentNode.parentNode.rowIndex; break;
	    }
	    oTable = oTarget.getElementsByTagName("TABLE")[0];
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
		            if (nOpcionDD == 3){
		                if (oRow.getAttribute("bd") == "I") {
		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                }    
		                else mfa(oRow, "D");
		            }
			        break;
		        case "divCatalogo2":
		        case "ctl00_CPHC_divCatalogo2":
		            if (FromTable == null || ToTable == null) continue;
		            if (nOpcionDD == 1){
	                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
	                    var sw = 0;
	                    //Controlar que el elemento a insertar no existe en la tabla
	                    for (var i=0;i<oTable.rows.length;i++){
		                    if (oTable.rows[i].id == oRow.id){
			                    //alert("Persona ya incluida");
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
	                        oCloneNode.cells[0].appendChild(document.createElement("<img src='../../../images/imgFI.gif'>"));
	                        mfa(oCloneNode, "I");
                        }
                    }
			    break;
			}
		}
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
var oImgFI = document.createElement("<img src='../../../../images/imgFI.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgFU = document.createElement("<img src='../../../../images/imgFU.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgFD = document.createElement("<img src='../../../../images/imgFD.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgFN = document.createElement("<img src='../../../../images/imgFN.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgEM = document.createElement("<img src='../../../../images/imgUsuEM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgIM = document.createElement("<img src='../../../../images/imgUsuIM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgEV = document.createElement("<img src='../../../../images/imgUsuEV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgIV = document.createElement("<img src='../../../../images/imgUsuIV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

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
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblOpciones.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones.rows.length; i++){
            if (!tblOpciones.rows[i].sw){
                oFila = tblOpciones.rows[i];
                oFila.sw = 1;
                
                if (oFila.sexo=="V"){
                    switch (oFila.tipo){
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(), null); break;
                    }
                }else{
                    switch (oFila.tipo){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1") {
                    setOp(oFila.cells[0].children[0], 20);
                    oFila.cells[0].children[0].title = "Profesional en estado de baja";
                }
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
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblOpciones2.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones2.rows.length; i++){
            if (!tblOpciones2.rows[i].sw){
                oFila = tblOpciones2.rows[i];
                oFila.sw = 1;
                
                oFila.onclick = function (){mmse(this);};
                if (oFila.cells[0].children[0]==null){
                    switch (oFila.bd){
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }
                if (oFila.sexo=="V"){
                    switch (oFila.tipo){
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIV.cloneNode(), null); break;
                    }
                }else{
                    switch (oFila.tipo){
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIM.cloneNode(), null); break;
                    }
                }
                if (oFila.baja=="1"){
                    setOp(oFila.cells[1].children[0], 20);
                    oFila.cells[1].children[0].title = "Profesional en estado de baja";
                }
            }
//            nContador++;
//            if (nContador > $I("divCatalogo2").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}

