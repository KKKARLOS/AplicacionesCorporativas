function init(){
    try{
        if (!mostrarErrores()) return;
        
//        cargarElementosTipo(nTipo);
//        actualizarLupas("tblTitulo", "tblDatos");
        window.focus();
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarAux(){
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 20);
}

function aceptar(){
    try{
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        
        for (var i=0; i< $I("tblDatos2").rows.length;i++){
            sb.Append("<tr id='" +  $I("tblDatos2").rows[i].id + "' style='height:16px;'>");
            sb.Append("<td>" +  $I("tblDatos2").rows[i].cells[0].innerText + "</td>");
            sb.Append("</tr>");
            sw = 1;
        }
        
        if (sw == 0)
        {
            ocultarProcesando();
            mmoff("War", "Debes seleccionar algún criterio de búsqueda.", 300);
            return;   
		}
		
		var nNivelEstructura = 0;
		switch($I("cboNivelEstru").value){
		    case "19": nNivelEstructura = 2; break;
		    case "18": nNivelEstructura = 3; break;
		    case "17": nNivelEstructura = 4; break;
		    case "16": nNivelEstructura = 5; break;
		    case "15": nNivelEstructura = 6; break;
		    case "1": nNivelEstructura = 10; break;
		}

        var returnValue = sb.ToString() + "@#@" + nNivelEstructura;
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

        for (var i=0; i <  $I("tblDatos2").rows.length; i++){
            if ( $I("tblDatos2").rows[i].id == idItem){
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
		            if (nOpcionDD == 3){
		                if (oRow.getAttribute("bd") == "I") {
		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                }    
		                else mfa(oRow, "D");
		            }else if (nOpcionDD == 4){
		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		            }
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
						oCloneNode.attachEvent('onclick', mm);
						oCloneNode.attachEvent('onmousedown', DD);
						oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);
                        oCloneNode.style.cursor = strCurMM;
						oCloneNode.className = "";
						//oCloneNode.cells[0].className = "MM";
                    }
			        break;			        			        
			}
		}
		
		actualizarLupas("tblAsignados", "tblDatos2");	
		ot('tblDatos2', 0, 0, '', '');
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

function cargarElementosTipo(nTipo){
    try{
        var sb = new StringBuilder;
        BorrarFilasDe("tblDatos");
        BorrarFilasDe("tblDatos2");
        if (nTipo=="") return;
        for (var i=0; i<opener.js_cri.length; i++)
        {
//          ojo se pone para las pruebas de mejora de las preferencias
//            if (opener.js_cri[i].t > nTipo) break;
            
            if (opener.js_cri[i].t == nTipo){
                sb.Append("<tr id='" + opener.js_cri[i].c + "' style='height:16px;'");
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'>");
                sb.Append("<td style='padding-left:3px;'>" + Utilidades.unescape(opener.js_cri[i].d) + "</td>");
                sb.Append("</tr>");
            }
        }
        
        insertarFilasEnTablaDOM("tblDatos", sb.ToString(), 0);
        window.focus();
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
