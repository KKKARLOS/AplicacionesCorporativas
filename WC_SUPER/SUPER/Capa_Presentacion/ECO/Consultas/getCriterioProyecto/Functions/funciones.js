function init(){
    try{
        if (!mostrarErrores()) return;
        //alert("Criterio "+ nTipo + " ("+ opener.js_cri.length +")");
        cargarElementosTipo(nTipo);
        cargarProyectosSeleccionados();
        scrollTablaProy();
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
        for (var i = 0; i < $I("tblDatos2").rows.length; i++) {
            sb.Append(tblDatos2.rows[i].id + "@#@" + Utilidades.escape(tblDatos2.rows[i].cells[3].innerText + " - " + tblDatos2.rows[i].cells[4].innerText) + "@#@" + tblDatos2.rows[i].getAttribute("categoria") + "@#@" + tblDatos2.rows[i].getAttribute("cualidad") + "@#@" + tblDatos2.rows[i].getAttribute("estado") + "///");
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

function insertarItem(oFila){
    try{
        var idItem = oFila.id;
        var bExiste = false;
        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            if (tblDatos2.rows[i].id == idItem) {
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
        for (var iFilaNueva = 0; iFilaNueva < $I("tblDatos2").rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = tblDatos2.rows[iFilaNueva].innerText;
            if (sActual>sNuevo)break;
        }

        // Se inserta la fila

        var NewRow = tblDatos2.insertRow(iFilaNueva);

        var oCloneNode	= oFila.cloneNode(true);
        NewRow.swapNode(oCloneNode);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);				
        oCloneNode.removeChild(oCloneNode.cells[5]);
        
 		actualizarLupas("tblAsignados", "tblDatos2");
       
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}

function fnRelease()
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
	var oTable, oNF;
	var js_ids;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{	
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
                    if (js_ids.isInArray(oRow.id)==null){
						oRow.className = "";
                        oNF = oTable.insertRow(-1);
                        var oCloneNode	= oRow.cloneNode(true);
                        oNF.swapNode(oCloneNode);
						oCloneNode.attachEvent('onclick', mm);
						oCloneNode.attachEvent('onmousedown', DD);						
						oCloneNode.removeChild(oCloneNode.cells[5]);
                    }
			        break;			        			        
			}
		}
		
		scrollTablaProyAsig();
		actualizarLupas("tblAsignados", "tblDatos2");	
		ot('tblDatos2', 4, 0, '', '');
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

        for (var i=0; i<opener.js_cri.length; i++)
        {
            // ojo se pone para las pruebas de mejora de las preferencias
//            if (opener.js_cri[i].t > nTipo) break;

            if (opener.js_cri[i].t == nTipo) {
                var oNF = $I("tblDatos").insertRow(-1);
                oNF.id = opener.js_cri[i].c;
                oNF.style.height = "20px";
                oNF.ondblclick = function() { insertarItem(this); };
                //oNF.onclick = function() { mm(event); };
                //oNF.onmousedown = function() { DD(event); };

                //oNF.attachEvent('ondblclick', insertarItem);
                oNF.attachEvent('onclick', mm);
                oNF.attachEvent('onmousedown', DD);
                oNF.setAttribute("categoria", opener.js_cri[i].a);
                oNF.setAttribute("cualidad", opener.js_cri[i].u);
                oNF.setAttribute("estado", opener.js_cri[i].e);

                oNF.insertCell(-1);
                oNF.insertCell(-1);
                oNF.insertCell(-1);

                var oNC4 = oNF.insertCell(-1);
                oNC4.setAttribute("style", "text-align:right;");
                oNC4.innerText = opener.js_cri[i].p.ToString("N", 9, 0);

                var oNC5 = oNF.insertCell(-1);
                var oLabel5 = document.createElement("label");
                oLabel5.ondblclick = function() { insertarItem(this.parentNode.parentNode); };
                oLabel5.className = "NBR W220";
                oLabel5.setAttribute("style", "noWrap:true; padding-left:5px;");
                oLabel5.setAttribute("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + Utilidades.unescape(opener.js_cri[i].d) + "<br><label style='width:70px;'>Responsable:</label>" + Utilidades.unescape(opener.js_cri[i].r) + "<br><label style='width:70px;'>" + sNodo + ":</label>" + Utilidades.unescape(opener.js_cri[i].n) + "<br><label style='width:70px;'>Cliente:</label>" + Utilidades.unescape(opener.js_cri[i].l) + "] hideselects=[off]");
                oLabel5.innerText = Utilidades.unescape(opener.js_cri[i].d);
                oNC5.appendChild(oLabel5); 

                var oNC6 = oNF.insertCell(-1);
                var oLabel6 = document.createElement("label");
                oLabel6.ondblclick = function() { insertarItem(this.parentNode.parentNode); };
                oLabel6.className = "NBR W170";
                oLabel6.setAttribute("style", "noWrap:true;");
                oLabel6.setAttribute("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + Utilidades.unescape(opener.js_cri[i].d) + "<br><label style='width:70px;'>Responsable:</label>" + Utilidades.unescape(opener.js_cri[i].r) + "<br><label style='width:70px;'>" + sNodo + ":</label>" + Utilidades.unescape(opener.js_cri[i].n) + "<br><label style='width:70px;'>Cliente:</label>" + Utilidades.unescape(opener.js_cri[i].l) + "] hideselects=[off]");
                oLabel6.innerText = Utilidades.unescape(opener.js_cri[i].l);
                oNC6.appendChild(oLabel6); 
            }
        }
        
        insertarFilasEnTablaDOM("tblDatos", sb.ToString(), 0);
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
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
                oFila.setAttribute("sw","1");
                
                oFila.ondblclick = function(){insertarItem(this)};

                if (oFila.getAttribute("categoria")=="P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                
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
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20 + 1, $I("tblDatos2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos2").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos2").rows[i];
                oFila.setAttribute("sw", 1);

//                oFila.onclick = function() { mm(event) };
//                oFila.onmousedown = function() { DD(event) };
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.cells[0].children.length == 0){
                    if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                    else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                }
                if (oFila.cells[1].children.length == 0) {
                    switch (oFila.getAttribute("cualidad")) {
                        case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                        case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                    }
                }
                if (oFila.cells[2].children.length == 0) {
                    switch (oFila.getAttribute("estado")) {
                        case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                        case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                        case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                        case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                    }
                }
				oFila.className = "";
				//oFila.cells[0].className = "MM";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos asignados.", e.message);
    }
}
function cargarProyectosSeleccionados(){
    try{
        var sb = new StringBuilder;
        var aDatos;
        var sw=0;
        for (var i=0; i<opener.js_Valores.length; i++){
            aDatos = opener.js_Valores[i].split("##");
            if (aDatos[0] != "") {
                var oNF = $I("tblDatos2").insertRow(-1);
                oNF.id = aDatos[0];
                oNF.style.height = "20px";

//                oNF.attachEvent('onclick', mm);
//                oNF.attachEvent('onmousedown', DD);

                oNF.setAttribute("categoria", aDatos[1]);
                oNF.setAttribute("cualidad", aDatos[2]);
                oNF.setAttribute("estado", aDatos[3]);

                oNF.insertCell(-1);
                oNF.insertCell(-1);
                oNF.insertCell(-1);

                var oNC4 = oNF.insertCell(-1);
                oNC4.setAttribute("style", "text-align:right;");
                oNC4.innerText = aDatos[4];

                var oNC5 = oNF.insertCell(-1);
                var oLabel5 = document.createElement("label");
                //oLabel5.ondblclick = function() { insertarItem(this.parentNode.parentNode); };
                oLabel5.className = "NBR W220";
                oLabel5.setAttribute("style", "noWrap:true; padding-left:5px;");
                oLabel5.setAttribute("title", Utilidades.unescape(aDatos[5]));
                oLabel5.innerText = Utilidades.unescape(aDatos[5]);
                oNC5.appendChild(oLabel5);
                sw=1;
            }
        }
        if (sw==1){
//            insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
            actualizarLupas("tblAsignados", "tblDatos2");
            scrollTablaProyAsig();
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
