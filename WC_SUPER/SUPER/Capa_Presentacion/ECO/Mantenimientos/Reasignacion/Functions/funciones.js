function init(){
    try{
        actualizarLupas("tblTituloNodo", "tblNodos");
        actualizarLupas("tblTitulo", "tblDatos");
        actualizarLupas("tblTituloAsignados", "tblDatos2");        
        comprobarProyectosManiobra();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "proyectos":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        actualizarLupas("tblTitulo", "tblDatos");
		        $I("hdnIdSubnodoOrigen").value = "";
		        $I("txtSubnodoOrigen").value = "";
		        window.focus();
		        scrollTablaProy();
		        AccionBotonera("procesar", "H");
                break;
           case "procesar":
                $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
                actualizarLupas("tblTitulo", "tblDatos");             
                setTimeout("recuperar();", 20);
                break;
            case "recuperar":
		        $I("divCatalogo2").children[0].innerHTML = aResul[2];
		        $I("divCatalogo2").scrollTop = 0;
		        nTopScrollProyDest = -1;
                scrollTablaProyDest();
                actualizarLupas("tblTituloAsignados", "tblDatos2");
                setTimeout("getNodos()", 20);
                break;
            case "getNodos":
		        $I("divCatalogoNodo").children[0].innerHTML = aResul[2];
		        $I("divCatalogoNodo").scrollTop = 0;
		        comprobarProyectosManiobra();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function setNodoOrigen(oFila){
    try{
        borrarCatalogo();
        borrarCatalogo2();
        $I("hdnIdNodo").value=oFila.id;
        $I("hdnDesNodo").value=oFila.cells[0].innerText;           
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nodo origen.", e.message);
    }
}    
function setNodo(oFila){
    try{
        var sw=0;
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].getAttribute("procesado") == ""){
                sw=1;
                break;
            }
        }        
        if (sw == 1) {
            jqConfirm("", "¡ Atención !<br><br>Hay proyectos pendientes de procesar. Al seleccionar otro " + strEstructuraNodoLarga + " no se reasignarán.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
                if (!answer) return false;
                LLamadaSetNodo(oFila);
            })

        } else LLamadaSetNodo(oFila);

    }catch(e){
        mostrarErrorAplicacion("Error al establecer el nodo.", e.message);
    }
}
function LLamadaSetNodo(oFila) {
    try {
        borrarCatalogo();
        borrarCatalogo2();
        $I("hdnIdNodo").value=oFila.id;
        $I("hdnDesNodo").value=oFila.cells[0].innerText;
        
        mostrarRelacionProyectos("M", "");
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nodo.", e.message);
    }
}

function recuperar(){
    try{
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("recuperar@#@");
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar la situación destino.", e.message);
    }
}

function getNodos(){
    try{
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("getNodos@#@");
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar la situación de los nodos.", e.message);
    }
}

function mostrarRelacionProyectos(sOpcion, sValor){
    try{
        //alert("Proyectos("+ sOpcion +","+ sValor +")");
        if ($I("hdnIdNodo").value == ""){
            mmoff("Inf", "Para obtener los proyectos es necesario seleccionar un "+ strEstructuraNodoLarga,400);
            return;
        }
 /*       
        if (sOpcion == "P"){
            if (sValor=="") return;
            if (isNaN(dfn(sValor))){
                alert("El valor introducido no es numérico");
                $I("txtNumero").value = "";
                $I("txtNumero").focus();
                return;
            }else $I("txtNumero").value = sValor.ToString("N", 9, 0);
        }
 */
        var js_args = "proyectos@#@";
        js_args += sOpcion +"@#@";
        js_args += $I("hdnIdNodo").value +"@#@";
        if (sOpcion == "L") js_args += sValor;
        else js_args += dfn(sValor);
//alert(js_args); ocultarProcesando(); return;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la relación de proyectos", e.message);
    }
}

//var oImgProducto = document.createElement("<img src='../../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgServicio = document.createElement("<img src='../../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

//var oImgContratante = document.createElement("<img src='../../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgRepJor = document.createElement("<img src='../../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgRepPrecio = document.createElement("<img src='../../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

//var oImgAbierto = document.createElement("<img src='../../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgCerrado = document.createElement("<img src='../../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgHistorico = document.createElement("<img src='../../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgPresup = document.createElement("<img src='../../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

//var oImgTrasOK = document.createElement("<img src='../../../../images/imgTrasladoOK.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgTrasOK = document.createElement("img");
oImgTrasOK.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgTrasladoOK.gif");
oImgTrasOK.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");
//var oImgTrasKO = document.createElement("<img src='../../../../images/imgTrasladoKO.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgTrasKO = document.createElement("img");
oImgTrasKO.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgTrasladoKO.gif");
oImgTrasKO.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

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
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", "1");
                
                //oFila.onclick = function(){mmse(this);};
                oFila.ondblclick = function(){insertarProyecto(this);};
                //oFila.onmousedown = function(){DD(this);};
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
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

                $I("hdnIdNodo").value = oFila.getAttribute("idNodo");
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollProyDest = 0;
var nIDTimeProyDest = 0;
function scrollTablaProyDest(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProyDest){
            nTopScrollProyDest = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProyDest);
            nIDTimeProyDest = setTimeout("scrollTablaProyDest()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProyDest/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, $I("tblDatos2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos2").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos2").rows[i];
                oFila.setAttribute("sw", "1");
                
                //oFila.onclick = function(){mmse(this);};
                //oFila.onmousedown = function(){DD(this);};
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
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
                
                if (oFila.getAttribute("procesado")=="1") oFila.cells[6].appendChild(oImgTrasOK.cloneNode(true), null);
                else if (oFila.getAttribute("procesado")=="0") oFila.cells[6].appendChild(oImgTrasKO.cloneNode(true), null);
                if (typeof (oFila.getAttribute("excepcion")) != "undefined" && oFila.getAttribute("excepcion") != "")
                    oFila.cells[6].children[0].title = Utilidades.unescape(oFila.getAttribute("excepcion"));
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos asignados.", e.message);
    }
}

function insertarProyecto(oFila){
    try{
        //var idPSN = oFila.idPSN;
        var bExiste = false;
        //1º buscar si existe en el array de recursos y su "opcionBD"
        var aFila = FilasDe("tblDatos2");        
        
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].getAttribute("idPSN") == oFila.getAttribute("idPSN")){
                bExiste = true;
                break;
            }
        }
        if (bExiste){
            //alert("El profesional indicado ya se encuentra asignado a la tarea");
            return;
        }
        
        var oNF = $I("tblDatos2").insertRow(-1);
        oNF.style.height = "20px";
        oNF.setAttribute("idPSN", oFila.getAttribute("idPSN"));
        oNF.setAttribute("idNodo", oFila.getAttribute("idPSN"));
        oNF.setAttribute("sw", oFila.getAttribute("sw"));
        oNF.setAttribute("categoria", oFila.getAttribute("categoria"));
        oNF.setAttribute("cualidad", oFila.getAttribute("cualidad"));
        oNF.setAttribute("estado", oFila.getAttribute("estado"));
        oNF.setAttribute("procesado", "");
        oNF.setAttribute("responsable_origen", oFila.getAttribute("responsable_origen"));
        if ($I("hdnResponsableDestino").value == "")
            oNF.setAttribute("responsable_destino", oFila.getAttribute("responsable_origen"));
        else
            oNF.setAttribute("responsable_destino", $I("hdnResponsableDestino").value);
        oNF.setAttribute("subnodo_origen", oFila.getAttribute("subnodo_origen"));
        if ($I("hdnIdSubnodoDestino").value == "")
            oNF.setAttribute("subnodo_destino", oFila.getAttribute("subnodo_origen"));
        else
            oNF.setAttribute("subnodo_destino", $I("hdnIdSubnodoDestino").value);
//        oNF.onclick = function(){mmse(this);};
//        oNF.onmousedown = function(){DD(this);};
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[1].children[0].cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[2].children[0].cloneNode(true));
        
        var oNC4 = oNF.insertCell(-1);
        oNC4.appendChild(oFila.cells[3].children[0].cloneNode(true));
        oNC4.children[0].className = "NBR W150";
        
        var oNC5 = oNF.insertCell(-1);
        oNC5.appendChild(oFila.cells[4].children[0].cloneNode(true));
        oNC5.children[0].className = "NBR W150";
        if ($I("txtResponsableDestino").value != "")
            oNC5.children[0].innerText = $I("txtResponsableDestino").value;
        
        var sTitle = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        var sTitle = oNC5.children[0].title;
        if (sTitle != ""){
            oNC5.children[0].setAttribute("title", sTootTip); //span
        }else{
            oNC5.children[0].setAttribute("boBDY", sTootTip); //span
        }
        
        var oNC6 = oNF.insertCell(-1);
        oNC6.appendChild(oFila.cells[5].children[0].cloneNode(true));
        oNC6.children[0].className = "NBR W150";
        if ($I("txtSubnodoDestino").value != "")
            oNC6.children[0].innerText = $I("txtSubnodoDestino").value;
        
        var sTitle = "<label style='width:60px;'>"+ strEstructuraSubnodo +":</label>" + $I("txtSubnodoDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        var sTitle = oNC6.children[0].title;
        if (sTitle != ""){
            oNC6.children[0].setAttribute("title", sTootTip); //span
        }else{
            oNC6.children[0].setAttribute("boBDY", sTootTip); //span
        }
        
        oNF.insertCell(-1);
        actualizarLupas("tblTituloAsignados", "tblDatos2");

        $I("hdnIdNodo").value = oFila.getAttribute("idNodo");
        
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el proyecto.", e.message);
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
	var oTable = null;
	var oNF = null;
    var oNC1 = null;
    var oNC2 = null;
    var oNC3 = null;
    var oNC4 = null;
    var oNC5 = null;
    var oNC6 = null;
	var aProyAsig = new Array();

	var nRD = $I("hdnResponsableDestino").value;
	var sRD = $I("txtResponsableDestino").value;
	var nSD = $I("hdnIdSubnodoDestino").value;
	var sSD = $I("txtSubnodoDestino").value;

	var nIndiceInsert = null;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{
	    switch (oElement.tagName) {
	        case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
	    }
	    
        var sTitleR = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
        var sTootTipR = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitleR + "] hideselects=[off]\"";
        var sTitleS = "<label style='width:60px;'>" + strEstructuraSubnodo + ":</label>" + $I("txtSubnodoDestino").value;
        var sTootTipS = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitleS + "] hideselects=[off]\"";

        switch(oTarget.id){
	        case "divCatalogo2":
                oTable = oTarget.getElementsByTagName("TABLE")[0];
                for (var i=0;i<oTable.rows.length;i++){
                    aProyAsig[aProyAsig.length] = oTable.rows[i].getAttribute("idPSN");
                }
                break;
        }
        
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
		            if (nOpcionDD == 4){
		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		            }
			        break;
		        case "divCatalogo2":
		            if (FromTable == null || ToTable == null) continue;
		            if (nOpcionDD == 1){
	                    var sw = 0;
	                    for (var i=0;i<aProyAsig.length;i++){
		                    if (aProyAsig[i] == oRow.getAttribute("idPSN")){
			                    sw = 1;
			                    break;
		                    }
	                    }
                        if (sw == 0){
	                        oNF = oTable.insertRow(-1);
                            oNF.style.height = "20px";
                            oNF.setAttribute("idPSN", oRow.getAttribute("idPSN"));
                            oNF.setAttribute("sw", oRow.getAttribute("sw"));
                            oNF.setAttribute("categoria", oRow.getAttribute("categoria"));
                            oNF.setAttribute("cualidad", oRow.getAttribute("cualidad"));
                            oNF.setAttribute("estado", oRow.getAttribute("estado"));
                            oNF.setAttribute("procesado", "");
                            oNF.setAttribute("responsable_origen", oRow.getAttribute("responsable_origen"));
                            if (nRD=="")
                                oNF.setAttribute("responsable_destino", oRow.getAttribute("responsable_origen"));
                            else
                                oNF.setAttribute("responsable_destino", nRD);
                            oNF.setAttribute("subnodo_origen", oRow.getAttribute("subnodo_origen"));
                            if (nSD=="")
                                oNF.setAttribute("subnodo_destino", oRow.getAttribute("subnodo_origen"));
                            else
                                oNF.setAttribute("subnodo_destino", nSD);
//                            oNF.onclick = function(){mmse(this);};
//                            oNF.onmousedown = function(){DD(this);};
                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);
                            
                            oNC1 = oNF.insertCell(-1);
                            if (oRow.cells[0].children.length>0)
                                oNC1.appendChild(oRow.cells[0].children[0].cloneNode(true));
                            oNC2 = oNF.insertCell(-1);
                            if (oRow.cells[1].children.length>0)
                                oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true));
                            oNC3 = oNF.insertCell(-1);
                            if (oRow.cells[2].children.length>0)
                                oNC3.appendChild(oRow.cells[2].children[0].cloneNode(true));

                            oNC4 = oNF.insertCell(-1);
                            oNC4.appendChild(oRow.cells[3].children[0].cloneNode(true));
                            oNC4.children[0].style.width = "150px";

                            oNC5 = oNF.insertCell(-1);
                            oNC5.appendChild(oRow.cells[4].children[0].cloneNode(true));
                            oNC5.children[0].style.width = "150px";
                            if (sRD != "")
                                oNC5.children[0].innerText = sRD;
                            sTitle = oNC5.children[0].getAttribute("title");
                            if (sTitle != ""){
                                oNC5.children[0].setAttribute("title", sTootTipR); //span
                            }else{
                                oNC5.children[0].setAttribute("boBDY", sTootTipR); //span
                            }

                            oNC6 = oNF.insertCell(-1);
                            oNC6.appendChild(oRow.cells[5].children[0].cloneNode(true));
                            oNC6.children[0].style.width = "150px";
                            if (sSD != "")
                                oNC6.children[0].innerText = sSD;                            
                            sTitle = oNC6.children[0].getAttribute("title");
                            if (sTitle != ""){
                                oNC6.children[0].setAttribute("title", sTootTipS); //span
                            }else{
                                oNC6.children[0].setAttribute("boBDY", sTootTipS); //span
                            }

                            oNF.insertCell(-1);
                        }
                    }
			        break;
			}
		}
		
        switch(oTarget.id){
	        case "divCatalogo2":
                scrollTablaProyDest();
                break;
        }
	}
    actualizarLupas("tblTituloAsignados", "tblDatos2");
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


function borrarCatalogo(){
    try {
        $I("hdnIdNodo").value = "";
        $I("hdnIdSubnodoOrigen").value = "";
        $I("txtSubnodoOrigen").value = "";
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
        actualizarLupas("tblTitulo", "tblDatos");        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function borrarCatalogo2(){
    try {
        $I("hdnIdNodo").value = "";
        $I("hdnResponsableDestino").value = "";
        $I("txtResponsableDestino").value = "";
        $I("hdnIdSubnodoDestino").value = "";
        $I("txtSubnodoDestino").value = "";
        for (var i=$I("tblDatos2").rows.length-1; i >= 0; i--){
            $I("tblDatos2").deleteRow(i);
        }
        actualizarLupas("tblTituloAsignados", "tblDatos2");
        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo de proyectos a reasignar", e.message);
    }
}

var bProcesar = false;
function procesar(){
    try{
        mostrarProcesando();
        var bPreguntar=true;
        var sb = new StringBuilder;
        sb.Append("procesar@#@");
        
        var aFila = FilasDe("tblDatos2");
        if (aFila.length == 0){
            mmoff("War", "No se han seleccionado proyectos a procesar.", 400);
            ocultarProcesando();
            return;
        }
        var bCorrecto = true;
        for (var i=0; i < aFila.length; i++){
//            if (aFila[i].responsable_destino == ""){
//                bCorrecto = false;
//                ocultarProcesando();
//                mmoff("War", "Todo proyecto a reasignar debe tener identificado responsable y "+ strEstructuraSubnodoLarga +".", 500, 2500);
//                return;
//            }
//            if (aFila[i].subnodo_destino == ""){
//                bCorrecto = false;
//                ocultarProcesando();
//                mmoff("War", "Todo proyecto a reasignar debe tener identificado responsable y "+ strEstructuraSubnodoLarga +".", 500, 2500);
//                return;
//            }
            if (aFila[i].getAttribute("responsable_destino") == "" && aFila[i].getAttribute("subnodo_destino") == ""){
                bCorrecto = false;
                ocultarProcesando();
                mmoff("War", "Todo proyecto a reasignar debe tener identificado responsable y "+ strEstructuraSubnodoLarga +".", 500, 2500);
                return;
            }
            if (bPreguntar){
                if (aFila[i].getAttribute("responsable_destino") == "" || aFila[i].getAttribute("subnodo_destino") == ""){
                    //if (!confirm("¡ Atención !\n\nHay proyectos para los que no se ha seleccionado responsable o área destino.\n\n¿Desea mantener el responsable o área origen y continuar?")){
                        bCorrecto = false;
                    //    ocultarProcesando();
                    //    return;
                    //}
                }
                bPreguntar=false;
            }
            if (aFila[i].getAttribute("responsable_destino")=="")
                aFila[i].setAttribute("responsable_destino", aFila[i].getAttribute("responsable_origen"));
            if (aFila[i].getAttribute("subnodo_destino")=="")
                aFila[i].setAttribute("subnodo_destino", aFila[i].getAttribute("subnodo_origen"));
//            if (aFila[i].responsable_destino == aFila[i].responsable_origen && aFila[i].subnodo_destino == aFila[i].subnodo_origen){
//                //si el origen y el destino es el mismo, no lo envío a procesar
//            }
//            else{
                sb.Append(aFila[i].getAttribute("idPSN") +"##"); //0
                sb.Append(aFila[i].getAttribute("responsable_origen") +"##"); //1
                sb.Append(aFila[i].getAttribute("responsable_destino") +"##"); //2
                sb.Append(aFila[i].getAttribute("subnodo_origen") +"##"); //3
                sb.Append(aFila[i].getAttribute("subnodo_destino") +"##"); //4
                
                if (aFila[i].cells[6].innerHTML=="") 
                    sb.Append(""); //5
                else{
                    if (aFila[i].cells[6].innerHTML.indexOf("imgTrasladoOK.gif") != -1) 
                        sb.Append("1"); //5
                    else 
                        sb.Append("0"); //5
                }
                sb.Append("///");
//            }
        }
        //alert(sb.ToString());return;

        if (!bCorrecto) {
            ocultarProcesando();
            jqConfirm("", "¡ Atención !<br><br>Hay proyectos para los que no se ha seleccionado responsable o área destino.<br><br>¿Deseas mantener el responsable o área origen y continuar?", "", "", "war", 450).then(function (answer) {
                if (!answer) return;
                else RealizarCallBack(sb.ToString(), "");
            })

        } else RealizarCallBack(sb.ToString(), "");

	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}


function comprobarProyectosManiobra(){
    try{
        mostrarProcesando();
        var sw=0;
        var aFila = FilasDe("tblNodos");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].cells[1].innerText != ""){
                sw = 1;
                break;
            }
        }
        if (sw==0){
            $I("imgCaution").style.display = "none";
            $I("divMsg").style.display = "none";
        }else{
            $I("imgCaution").title = "Existen proyectos en "+ strEstructuraSubnodoLarga + " 'Proyectos a reasignar'";
            $I("imgCaution").style.display = "block";
            $I("divMsg").style.display = "block";
        }
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aparcar la situación destino.", e.message);
    }
}

function getResponsableDestino(){
    try{
        if ($I("hdnIdNodo").value == ""){
            mmoff("Inf", "Para poder indicar el responsable, antes debe seleccionar el " + strEstructuraNodoLarga,400);
            return;
        }
        var sTitle = "";
        var sTootTip = "";
        
        mostrarProcesando();
        //var ret = window.showModalDialog("../../getResponsable.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value + "&sNodo=" + Utilidades.escape($I("hdnDesNodo").value), self, sSize(550, 540));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value + "&sNodo=" + Utilidades.escape($I("hdnDesNodo").value), self, sSize(550, 540))
	        .then(function(ret) {
	            //alert(ret);
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            $I("hdnResponsableDestino").value = aDatos[0];
		            $I("txtResponsableDestino").value = aDatos[1];
        		    
                    sTitle = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
                    sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
                    
                    var aFila = FilasDe("tblDatos2");
                    for (var i=0; i < aFila.length; i++){
                        if (aFila[i].className == "FS"){
        //                    aFila[i].className = "";
                            aFila[i].setAttribute("responsable_destino", $I("hdnResponsableDestino").value);
                            aFila[i].cells[4].children[0].innerText = $I("txtResponsableDestino").value;
                            
                            sTitle = aFila[i].cells[4].children[0].getAttribute("title");
                            if (sTitle != ""){
                                aFila[i].cells[4].children[0].setAttribute("title", sTootTip); //span
                            }else{
                                aFila[i].cells[4].children[0].setAttribute("boBDY", sTootTip); //span
                            }
                            aFila[i].cells[6].innerHTML = "";
                        }
                    }
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el responsable destino.", e.message);
    }
}

function getSubnodoDestino(){
    try{
        if ($I("hdnIdNodo").value == ""){
            mmoff("Inf", "Para poder indicar el " + strEstructuraSubnodoLarga + ", antes debe seleccionar el " + strEstructuraNodoLarga,400);
            return;
        }
        var sTitle = "";
        var sTootTip = "";
        
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getSubnodo.aspx?sO=" + codpar("RD");
	    strEnlace += "&nN="+ codpar($I("hdnIdNodo").value);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(400, 480));
	    modalDialog.Show(strEnlace, self, sSize(400, 480))
	        .then(function(ret) {
	            //alert(ret);
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            $I("hdnIdSubnodoDestino").value = aDatos[0];
		            $I("txtSubnodoDestino").value = aDatos[1];
        		    
                    sTitle = "<label style='width:60px;'>" + strEstructuraSubnodo + ":</label>" + $I("txtSubnodoDestino").value;
                    sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
                    
                    var aFila = FilasDe("tblDatos2");
                    for (var i=0; i < aFila.length; i++){
                        if (aFila[i].className == "FS"){
        //                    aFila[i].className = "";
                            aFila[i].setAttribute("subnodo_destino", $I("hdnIdSubnodoDestino").value);
                            aFila[i].cells[5].children[0].innerText = $I("txtSubnodoDestino").value;
                            
                            sTitle = aFila[i].cells[5].children[0].getAttribute("title");
                            if (sTitle != ""){
                                aFila[i].cells[5].children[0].setAttribute("title", sTootTip); //span
                            }else{
                                aFila[i].cells[5].children[0].getAttribute("boBDY", sTootTip); //span
                            }
                            aFila[i].cells[6].innerHTML = "";
                        }
                    }
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el subnodo destino.", e.message);
    }
}
function getSubnodoOrigen(){
    try{
        if ($I("hdnIdNodo").value == ""){
            mmoff("Inf", "Para poder indicar el " + strEstructuraSubnodoLarga + ", antes debe seleccionar el " + strEstructuraNodoLarga, 400);
            return;
        }
        var sTitle = "";
        var sTootTip = "";
        
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getSubnodo.aspx?sO=" + codpar("RO");
	    strEnlace += "&nN="+ codpar($I("hdnIdNodo").value);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(400, 480));
	    modalDialog.Show(strEnlace, self, sSize(400, 480))
	        .then(function(ret) {
	            //alert(ret);
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            $I("hdnIdSubnodoOrigen").value = aDatos[0];
		            $I("txtSubnodoOrigen").value = aDatos[1];
        		    
                    var js_args = "proyectos@#@";
                    js_args += "S@#@";
                    js_args += $I("hdnIdNodo").value +"@#@";
                    js_args += dfn($I("hdnIdSubnodoOrigen").value);
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el subnodo origen.", e.message);
    }
}
function getProyectosMultiple(oFila){
    try{
        var sw=0;
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].getAttribute("procesado") == ""){
                sw=1;
                break;
            }
        }        
        if (sw == 1) {
            jqConfirm("", "¡ Atención !<br><br>Hay proyectos pendientes de procesar. Al seleccionar otro " + strEstructuraNodoLarga + " no se reasignarán.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
                if (!answer) return false;
                LLamadaGetProyectosMultiple(oFila);
            })

        } else LLamadaGetProyectosMultiple(oFila);

    }catch(e){
        mostrarErrorAplicacion("Error al establecer el nodo-1.", e.message);
    }
}   
function LLamadaGetProyectosMultiple(oFila) {
    try {
        mostrarProcesando();
        borrarCatalogo();
        borrarCatalogo2();
        $I("hdnIdNodo").value=oFila.id;
        $I("hdnDesNodo").value=oFila.cells[0].innerText;
        var strEnlace = strServer + "Capa_Presentacion/getProyectosMultiple/Default.aspx?mod=pge&nNodo=" + oFila.id + "&sNodo=" + Utilidades.escape(oFila.cells[0].innerText);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 700));
	    modalDialog.Show(strEnlace, self, sSize(1010, 700))
	        .then(function(ret) {
	            if (ret != null){
	                aDatos = ret.split("@#@");
	                //var sPE = aDatos[1];
        	        
                    if (aDatos[1].length > 8000){
                        ocultarProcesando();
                        mmoff("War", "La longitud máxima de la lista no debe sobrepasar los 7000 caracteres.", 450, 3000);
                    }else{
                        mostrarRelacionProyectos("L", aDatos[1]);
                    }
	            }else ocultarProcesando();
	            return true;
	        }); 
	}catch(e){
	    mostrarErrorAplicacion("Error al establecer el nodo-2.", e.message);
    }
}
function getPSNs() {
    try {

        mostrarProcesando();
        borrarCatalogo();
        borrarCatalogo2();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge";
        modalDialog.Show(strEnlace, self, sSize(1010, 700))
	        .then(function (ret) {
	            if (ret != null) {
	                aDatos = (ret.split("@#@"))[0].split("///");
	                if (aDatos[0].length > 8000) {
	                    ocultarProcesando();
	                    mmoff("War", "La longitud máxima de la lista no debe sobrepasar los 7000 caracteres.", 450, 3000);
	                } else {
	                    mostrarRelacionProyectos2(aDatos[0]);
	                }
	            } else ocultarProcesando();
	            return true;
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function mostrarRelacionProyectos2(sValor) {
    try {
        var js_args = "proyectos@#@L2@#@@#@" + sValor;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de proyectos (2)", e.message);
    }
}
