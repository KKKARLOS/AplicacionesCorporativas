function init(){
    try{
        if (!mostrarErrores()) return;
        cargarCriteriosSeleccionados();
        actualizarLupas("tblAsignados", "tblDatos2");
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
        var sb = new StringBuilder; //sin paréntesis
//        sb.Append(sGrupoEco + "///");
//        sb.Append($I("cboGrupoEco").options[$I("cboGrupoEco").selectedIndex].innerText + "///");
        for (var i=0; i<$I("tblDatos2").rows.length; i++){
            //sb.Append(tblDatos2.rows[i].id + "@#@" + tblDatos2.rows[i].cells[0].innerText + "///");
            sb.Append($I("tblDatos2").rows[i].id + "@#@" + $I("tblDatos2").rows[i].getAttribute("grupo") + "@#@");
            sb.Append($I("tblDatos2").rows[i].getAttribute("subgrupo") + "@#@" + $I("tblDatos2").rows[i].getAttribute("concepto") + "@#@");
            sb.Append($I("tblDatos2").rows[i].cells[0].innerText + "///");
        }
        var returnValue = sb.ToString();
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

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getEstructura":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function mostrar(oImg){
    try{
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        //var nDesplegado = oFila.desplegado;
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);
        
        for (var i=nIndexFila+1; i<$I("tblDatos").rows.length; i++){
            if (tblDatos.rows[i].getAttribute("nivel") > nNivel){
                if (opcion == "O")
                {
                    $I("tblDatos").rows[i].style.display = "none";
                    if ($I("tblDatos").rows[i].getAttribute("nivel") < 4)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
                else if ($I("tblDatos").rows[i].getAttribute("nivel") - 1 == nNivel) $I("tblDatos").rows[i].style.display = "table-row";
            }else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../../images/plus.gif";
        else oImg.src = "../../../../images/minus.gif"; 

        ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}    

function MostrarInactivos(){
    try{
        mostrarProcesando();
        //sGrupoEco=$I("cboGrupoEco").value;
        var js_args = "getEstructura@#@" + sGrupoEco +"@#@";
        js_args += ($I("chkMostrarInactivos").checked) ? "1":"0";
        RealizarCallBack(js_args, "");
    }catch(e){
	    mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
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
//						oCloneNode.onmousedown = function(){DD(event)};
//						oCloneNode.onclick = function(){mm(event)};
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);
                        oCloneNode.style.height = "16px";
                        oCloneNode.cells[0].removeChild(oCloneNode.cells[0].children[0]);
                        //oCloneNode.cells[0].children[0].style.verticalAlign = "middle";
						oCloneNode.style.cursor = strCurMM;
						oCloneNode.className = "";
						//oCloneNode.cells[0].className = "MM";
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


        // Se inserta la fila
        
        var NewRow = $I("tblDatos2").insertRow(-1);

        var oCloneNode	= oFila.cloneNode(true);
//		oCloneNode.onmousedown = function(){DD(event)};
//		oCloneNode.onclick = function(){mm(event)};
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        oCloneNode.className = "";
        NewRow.swapNode(oCloneNode);
        oCloneNode.style.height = "16px";
        oCloneNode.cells[0].removeChild(oCloneNode.cells[0].children[0]);
        //oCloneNode.cells[0].children[0].style.verticalAlign = "middle";
        oCloneNode.style.cursor = strCurMM;
		oCloneNode.className = "";
		//oCloneNode.cells[0].className = "MM";
 		
       
        return NewRow.rowIndex;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}
function MarcarClases(oTabla){
    try{
        if ($I(oTabla)==null) return;
        var aFilas = FilasDe(oTabla);
        for (var i=0;i<aFilas.length;i++){
            if (aFilas[i].getAttribute("cl")=="S")
                aFilas[i].className = "FS";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar todas las filas.", e.message);
    }
}
function DesmarcarClases(oTabla){
    try{
        if ($I(oTabla)==null) return;
        var aFilas = FilasDe(oTabla);
        for (var i=0;i<aFilas.length;i++){
            aFilas[i].className = "";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al quitar la selección de todas las filas.", e.message);
    }
}
function cargarCriteriosSeleccionados(){
    try{
        var sb = new StringBuilder;
        var aDatos;
        var sw=0;
        for (var i = 0; i < fOpener().js_Valores.length; i++) {
            aDatos = fOpener().js_Valores[i].split("##");
            if (aDatos[0] !=""){
                //sb.Append("<tr id='" + aDatos[0] + "' cl='S' style='DISPLAY: block; HEIGHT: 16px' onmouseover='TTip()' onclick='mmse(this)' onmousedown='DD(this)'>");
                sb.Append("<tr id='" + aDatos[0] + "' cl='S' style='DISPLAY: table-row; HEIGHT: 16px' onclick='mm(event)' onmousedown='DD(event)'>");
                //sb.Append("<td><nobr class='NBR W430'>" + Utilidades.unescape(aDatos[1]) + "</nobr></td></tr>");
                sb.Append("<td><label class='texto MAM' style='margin-left:3px;' ");//color:" + sColor 
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' ");
                sb.Append("style='vertical-align:middle' />  Información] body=[");
//                sb.Append("<label style='width:70px;'>Grupo:</label>" + aDatos[1].Replace((char)34, (char)39));
                sb.Append("<label style='width:70px;'>Grupo:</label>" + aDatos[1]);
                sb.Append("<br><label style='width:70px;'>Subgrupo:</label>" + aDatos[2]);
                sb.Append("<br><label style='width:70px;'>Concepto:</label>" + aDatos[3]);
                //sb.Append("<br><label style='width:70px;'>Clase:</label>" + Utilidades.unescape(aDatos[4]));
                sb.Append("] hideselects=[off]\" " );
                sb.Append(" >" + Utilidades.unescape(aDatos[4]) + "</label></td></tr>");
                sw=1;
            }
        }
        if (sw==1){
            insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
            actualizarLupas("tblAsignados", "tblDatos2");
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}

