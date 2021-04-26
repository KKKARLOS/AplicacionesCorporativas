function init(){
    try{
        if (!mostrarErrores()) return;
        cargarElementosTipo(nTipo);
        cargarCriteriosSeleccionados();
        actualizarLupas("tblTitulo", "tblDatos");
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
        
        var tblDatos2 = $I("tblDatos2");
        for (var i=0; i<tblDatos2.rows.length;i++){
            sb.Append(tblDatos2.rows[i].id + "@#@" + tblDatos2.rows[i].cells[0].innerText + "///");
        }
        var returnValue =  sb.ToString();
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

        var sNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < tblDatos2.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = tblDatos2.rows[iFilaNueva].innerText;
            if (sActual>sNuevo)break;
        }

        // Se inserta la fila

        var NewRow = tblDatos2.insertRow(iFilaNueva);

        var oCloneNode = oFila.cloneNode(true);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);        
        NewRow.swapNode(oCloneNode);
//        oCloneNode.onmousedown = function() { DD(event); };
//        oCloneNode.onclick = function() { mm(event); };
//        oCloneNode.style.cursor = strCurMM;
        //        oCloneNode.setAttribute("class", "");
        scrollTablaProyAsig();
        actualizarLupas("tblAsignados", "tblDatos2");
       
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
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
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = $I("DW");
//	var nIndiceInsert = null;
	var oTable, oNF;
	var js_ids;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{	
//	    switch (event.srcElement.tagName){
//	        case "TD": nIndiceInsert = event.srcElement.parentNode.rowIndex; break;
//	        case "INPUT": nIndiceInsert = event.srcElement.parentNode.parentNode.rowIndex; break;
//	    }
	    if (oTarget.id == "divCatalogo2"
	           || oTarget.id == "ctl00_CPHC_divCatalogo2"){
	        oTable = oTarget.getElementsByTagName("TABLE")[0];
	        js_ids = getIDsFromTable(oTable);
	    }
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":                
		            if (nOpcionDD == 3){
		                if (oRow.getAttribute("bd") == "I"){
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
		            if (js_ids.isInArray(oRow.id) == null) {
		                //oRow.setAttribute("class", "");
		                oNF = oTable.insertRow(-1);
		                var oCloneNode = oRow.cloneNode(true);
		                oCloneNode.attachEvent('onclick', mm);
		                oCloneNode.attachEvent('onmousedown', DD);     
		                oNF.swapNode(oCloneNode);
//		                oCloneNode.onmousedown = function() { DD(event); };
//		                oCloneNode.onclick = function() { mm(event); };
//		                oCloneNode.style.cursor = strCurMM;
//		                oCloneNode.setAttribute("class", "");
		            }
		            break;			        			        
			}
		}
		
		scrollTablaProyAsig();
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

var nTopScrollProyAsig = 0;
var nIDTimeProyAsig = 0;

function scrollTablaProyAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProyAsig){
            nTopScrollProyAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProyAsig);
            nIDTimeProyAsig = setTimeout("scrollTablaProyAsig()", 50);
            return;
        }
        clearTimeout(nIDTimeProyAsig);
        
        var tblDatos2 = $I("tblDatos2");
        var nFilaVisible = Math.floor(nTopScrollProyAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20 + 1, tblDatos2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos2.rows[i].getAttribute("sw")){
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);                
                oFila.className = "";
                oFila.style.cursor = strCurMM;
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos asignados.", e.message);
    }
}
function cargarElementosTipo(nTipo){
    try{
        var sb = new StringBuilder;
        var aux = nTipo;
        switch (aux) {
            case "11":
                aux = 5; 
                break;
            case "12":
                aux = 3; 
                break;
            case "13":
            case "14":
            case "15":
                aux = aux - 2; 
                break;
        } 

        for (var i = 0; i < fOpener().js_cri.length; i++) {
//ojo se pone para las pruebas de mejora de las preferencias        
            //            if (fOpener().js_cri[i].t > aux) break;

            if (fOpener().js_cri[i].t == aux) {
                var oNF = $I("tblDatos").insertRow(-1);
                oNF.id = fOpener().js_cri[i].c;
                oNF.style.height = "20px";

                if (nTipo != 16) oNF.attachEvent('onmouseover', TTip);
                
                oNF.attachEvent('onclick', mm);
                oNF.attachEvent('onmousedown', DD);
                oNF.ondblclick = function() { insertarItem(this); };


                var oNC = oNF.insertCell(-1);

                var oLabel = document.createElement("label");
                oLabel.className = "NBR W340";
                oLabel.style.backgroundColor = "Red";
                oLabel.setAttribute("style", "vertical-align:middle;");
                oLabel.innerText = Utilidades.unescape(fOpener().js_cri[i].d);
                oNC.appendChild(oLabel); 
            }
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
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
                var oNF = $I("tblDatos2").insertRow(-1);
                oNF.id = aDatos[0];
                oNF.style.height = "20px";
                
                oNF.attachEvent('onmouseover', TTip);
                oNF.attachEvent('onclick', mm);
                oNF.attachEvent('onmousedown', DD);                

                var oNC = oNF.insertCell(-1);

                var oLabel = document.createElement("label");
                oLabel.className="NBR W340";
                oLabel.setAttribute("style", "vertical-align:middle;");
                oLabel.innerText = Utilidades.unescape(aDatos[1]);
                oNC.appendChild(oLabel); 
            }
        }
        if (sw==1){
            //insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
            actualizarLupas("tblAsignados", "tblDatos2");
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}

