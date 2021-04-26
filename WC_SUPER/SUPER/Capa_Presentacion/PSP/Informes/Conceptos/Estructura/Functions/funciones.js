function init(){
    try{
        if (!mostrarErrores()) return;
        $I("cboNivelEstru").focus();
  
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function CargarNivelEstructura(){
	try{   
	    $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' style='width: 350px;' class='texto MA'></table>";                     
	    actualizarLupas("tblCatIni", "tblDatos");
	    var js_args="";    
		if ($I("cboNivelEstru").value==1)
		    js_args="SUBNODO";   
		else if ($I("cboNivelEstru").value==2)
		    js_args="NODO";  
		else if ($I("cboNivelEstru").value==3)
		    js_args="SNN1";  
		else if ($I("cboNivelEstru").value==4)
		    js_args="SNN2";  
		else if ($I("cboNivelEstru").value==5)
		    js_args="SNN3";  
		else if ($I("cboNivelEstru").value==6)
		    js_args="SNN4";  
		else return;
		js_args += "@#@";
		js_args += ($I("chkMostrarInactivos").checked) ? "1@#@" : "0@#@";		            
		//alert(js_args);
		mostrarProcesando();
		RealizarCallBack(js_args, "");
		return;                      

	}catch(e){
		mostrarErrorAplicacion("Error en la función CargarNivelEstructura", e.message);
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
			case "SUBNODO":
			case "NODO":
			case "SNN1":
			case "SNN2":
			case "SNN3":
			case "SNN4":
				$I("divCatalogo").children[0].innerHTML = aResul[2];
				$I("divCatalogo2").children[0].innerHTML = "<table id='tblDatos2' style='width: 350px;' class='texto MM'></colgroup></table>";                
				actualizarLupas("tblCatIni", "tblDatos");
				$I("tblCatIni").focus();  
				break;				
        }
        if (aResul[0]=="NODO")
            ot('tblDatos', 0, 0, '', '');
        ocultarProcesando();
    }
}

function aceptarAux(){
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar(){
    try{
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        sb.Append( $I("cboNivelEstru").value + "|||");
        var tblDatos2 = $I("tblDatos2");
        for (var i=0; i<tblDatos2.rows.length;i++){
            sb.Append(tblDatos2.rows[i].id + "@#@");
            sb.Append(tblDatos2.rows[i].cells[0].innerHTML);
            sb.Append("///");
            sw = 1;
        }
        
        if (sw == 0)
        {
            ocultarProcesando();
            mmoff("Inf", "Debes seleccionar algún item", 210);
            return;   
		}
        var returnValue =  sb.ToString().substring(0,sb.ToString().length-3);
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;
        
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function insertarItem(oFila){
    
    try{
        var idItem = oFila.id;
        var bExiste = false;
        
        var tblDatos2 = $I("tblDatos2");
        for (var i=0; i < tblDatos2.rows.length; i++){
            if (tblDatos2.rows[i].id == idItem){
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


        var oTable = $I("tblDatos2");
        var sNuevo = oFila.innerText;
        for (var iFilaNueva=0; iFilaNueva < oTable.rows.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual=oTable.rows[iFilaNueva].innerText;
            if (sActual>sNuevo)break;
        }

        // Se inserta la fila
        
        var NewRow;

        NewRow = $I("tblDatos2").insertRow(iFilaNueva);

        var oCloneNode	= oFila.cloneNode(true);
        oCloneNode.style.cursor = strCurMM;
        oCloneNode.className = "";
        //oCloneNode.children[0].className = "MM";
        NewRow.swapNode(oCloneNode);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        oCloneNode.style.height = "16px";              
 		actualizarLupas("tblAsignados", "tblDatos2");
       
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
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
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
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
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
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
                        NewRow.swapNode(oCloneNode);
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        oCloneNode.style.height = "16px";
                        //oCloneNode.cells[0].children[0].style.verticalAlign = "middle";
                        oCloneNode.style.cursor = strCurMM;
                        oCloneNode.className = "";
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        //ot('tblDatos2', 0, 0, '', '');
    }
    oTable = null;
    killTimer();
    CancelDrag();

    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;
}