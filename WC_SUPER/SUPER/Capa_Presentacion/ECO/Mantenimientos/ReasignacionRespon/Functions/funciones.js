function init(){
    try {
        ToolTipBotonera("obtener", "Muestra los proyectos y sus responsables de las áreas de negocio seleccionadas");
        ToolTipBotonera("procesar", "Realiza la reasignación del nuevo responsable en los proyectos seleccionados");
        actualizarLupas("tblTituloSubnodos", "tblSubnodos");
        actualizarLupas("tblTitulo", "tblDatos");
        actualizarLupas("tblTituloAsignados", "tblDatos2");
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
		        window.focus();
                scrollTablaProy();
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
                break;
            
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function obtener() {
    try {
        var strIdNodos = "";
        var strIdSubNodos = "";
        var aFila = FilasDe("tblSubnodos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                strIdNodos += aFila[i].getAttribute("nodo") + ",";
                strIdSubNodos += aFila[i].id + ",";
            }                
        }
        strIdNodos = strIdNodos.substring(0, strIdNodos.length - 1);
        strIdSubNodos = strIdSubNodos.substring(0, strIdSubNodos.length - 1);

        if (strIdSubNodos == "") {
            mmoff("Inf", "Debes indicar algún subnodo.", 230);
            return;
        }

        $I("hdnIdNodo").value = strIdNodos;
        $I("hdnIdSubnodo").value = strIdSubNodos;
        
        mostrarRelacionProyectos();
    } catch (e) 
    {
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

function mostrarRelacionProyectos(){
    try {

        var js_args = "proyectos@#@";
        js_args += $I("hdnIdSubnodo").value +"@#@";
        js_args += $I("hdnResponsableOrigen").value +"@#@"
        //alert(js_args); ocultarProcesando(); return;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la relación de proyectos", e.message);
    }
}

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
                
                if (oFila.getAttribute("procesado")=="1") oFila.cells[5].appendChild(oImgTrasOK.cloneNode(true), null);
                else if (oFila.getAttribute("procesado")=="0") oFila.cells[5].appendChild(oImgTrasKO.cloneNode(true), null);
                if (typeof (oFila.getAttribute("excepcion")) != "undefined" && oFila.getAttribute("excepcion") != "") 
                    oFila.cells[5].children[0].setAttribute("title", Utilidades.unescape(oFila.getAttribute("excepcion")));
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

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        var sTitle = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

        oNC1 = oNF.insertCell(-1);
        if (oFila.cells[0].children.length > 0)
            oNC1.appendChild(oFila.cells[0].children[0].cloneNode(true));
        oNC2 = oNF.insertCell(-1);
        if (oFila.cells[1].children.length > 0)
            oNC2.appendChild(oFila.cells[1].children[0].cloneNode(true));
        oNC3 = oNF.insertCell(-1);
        if (oFila.cells[2].children.length > 0)
            oNC3.appendChild(oFila.cells[2].children[0].cloneNode(true));

        oNC4 = oNF.insertCell(-1);
        oNC4.appendChild(oFila.cells[3].children[0].cloneNode(true));
        oNC4.children[0].style.width = "165px";

        oNC5 = oNF.insertCell(-1);
        oNC5.appendChild(oFila.cells[4].children[0].cloneNode(true));
        oNC5.children[0].style.width = "175px";
        if ($I("txtResponsableDestino").value != "")
            oNC5.children[0].innerText = $I("txtResponsableDestino").value;
        sTitle = oNC5.children[0].title;
        if (sTitle != "") {
            oNC5.children[0].setAttribute("title", sTootTip); //span
        } else {
            oNC5.children[0].setAttribute("boBDY", sTootTip); //span
        }

        oNF.insertCell(-1);
        actualizarLupas("tblTituloAsignados", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el proyecto.", e.message);
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

	var nIndiceInsert = null;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{
	    switch (oElement.tagName) {
	        case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
	    }
	    
        var sTitleR = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
        var sTootTipR = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitleR + "] hideselects=[off]\"";

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
			    case "ctl00_CPHC_divCatalogo2":		        
		            if (FromTable == null || ToTable == null) continue;
		            if (nOpcionDD == 1){
	                    var sw = 0;
	                    for (var i=0;i<aProyAsig.length;i++){
	                        if (aProyAsig[i] == oRow.getAttribute("idPSN")) {
			                    sw = 1;
			                    break;
		                    }
	                    }
	                    if (sw == 0) {
	                        // Se inserta la fila
//	                        var oNF;
//	                        if (nIndiceInsert == null) {
//	                            nIndiceInsert = oTable.rows.length;
//	                            oNF = oTable.insertRow(nIndiceInsert);
//	                        }
//	                        else {
//	                            if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
//	                            oNF = oTable.insertRow(nIndiceInsert);
//	                        }
//	                        nIndiceInsert++;
                      
	                        oNF = oTable.insertRow(-1);
                            oNF.style.height = "20px";
                            oNF.setAttribute("idPSN", oRow.idPSN);
                            oNF.setAttribute("sw", oRow.getAttribute("sw"));
                            oNF.setAttribute("categoria", oRow.getAttribute("categoria"));
                            oNF.setAttribute("cualidad", oRow.getAttribute("cualidad"));
                            oNF.setAttribute("estado", oRow.getAttribute("estado"));
                            oNF.setAttribute("procesado", "");
                            oNF.setAttribute("responsable_origen", oRow.getAttribute("responsable_origen"));
                            if (nRD == "")
                                oNF.setAttribute("responsable_destino", oRow.getAttribute("responsable_origen"));
                            else
                                oNF.setAttribute("responsable_destino", nRD);

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
                            oNC4.children[0].style.width = "165px";

                            oNC5 = oNF.insertCell(-1);
                            oNC5.appendChild(oRow.cells[4].children[0].cloneNode(true));
                            oNC5.children[0].style.width = "175px";
                            if (sRD != "")
                                oNC5.children[0].innerText = sRD;
                            sTitle = oNC5.children[0].title;
                            if (sTitle != "") {
                                oNC5.children[0].setAttribute("title", sTootTipR); //span
                            } else {
                                oNC5.children[0].setAttribute("boBDY", sTootTipR); //span
                            }
                          
                            oNF.insertCell(-1);
                        }
                    }
			        break;
			}
		}
		
        switch(oTarget.id){
            case "divCatalogo2":
            case "ctl00_CPHC_divCatalogo2":	        
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
    try{
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
        actualizarLupas("tblTitulo", "tblDatos");        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function borrarCatalogo2(){
    try{
        $I("hdnResponsableDestino").value = "";
        $I("txtResponsableDestino").value = "";
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
        var bPreguntar = true;
        var bCorrecto = true;
        var sb = new StringBuilder;
        sb.Append("procesar@#@");
        
        var aFila = FilasDe("tblDatos2");
        if (aFila.length == 0){
            mmoff("War", "No se han seleccionado proyectos a procesar.", 400);
            ocultarProcesando();
            return;
        }
        for (var i=0; i < aFila.length; i++){

            if (aFila[i].getAttribute("responsable_destino") == ""){
                bCorrecto = false;
                ocultarProcesando();
                mmoff("War", "Todo proyecto a reasignar debe tener identificado responsable.", 500, 2500);
                return;
            }
            if (bPreguntar){
                if (aFila[i].getAttribute("responsable_destino") == ""){
                    //if (!confirm("¡ Atención !\n\nHay proyectos para los que no se ha seleccionado responsable.\n\n¿Deseas mantener el responsable y continuar?")){
                        bCorrecto = false;
                    //    ocultarProcesando();
                    //    return;
                    //}
                }
                bPreguntar=false;
            }
            if (aFila[i].getAttribute("responsable_destino") == "") 
                aFila[i].setAttribute("responsable_destino", aFila[i].getAttribute("responsable_origen"));

                sb.Append(aFila[i].getAttribute("idPSN") +"##"); //0
                sb.Append(aFila[i].getAttribute("responsable_origen") +"##"); //1
                sb.Append(aFila[i].getAttribute("responsable_destino") +"##"); //2
                
                if (aFila[i].cells[5].innerHTML=="") 
                    sb.Append(""); //4
                else{
                    if (aFila[i].cells[5].innerHTML.indexOf("imgTrasladoOK.gif") != -1) 
                        sb.Append("1"); //4
                    else 
                        sb.Append("0"); //4
                }
                sb.Append("///");

        }
        //alert(sb.ToString());return;
        if (!bCorrecto) {
            ocultarProcesando();
            jqConfirm("", "¡ Atención !<br><br>Hay proyectos para los que no se ha seleccionado responsable.<br><br>¿Deseas mantener el responsable y continuar?", "", "", "war", 450).then(function (answer) {
                if (!answer) return;
                else RealizarCallBack(sb.ToString(), "");
            })

        } else RealizarCallBack(sb.ToString(), "");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}
function borrarResponsableOrigen() {
    try{
        $I("hdnResponsableOrigen").value = "";
        $I("txtResponsableOrigen").value = "";
        
        if ($I("hdnIdSubnodo").value != "") mostrarRelacionProyectos();
        else borrarCatalogo();
        
    } catch (e) {
    mostrarErrorAplicacion("Error en la función borrarResponsableOrigen.", e.message);
    }        
}
function getResponsableOrigen(){
    try{
        var strIdNodos = "";
        var strIdSubNodos = "";
        var aFila = FilasDe("tblSubnodos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                strIdNodos += aFila[i].getAttribute("nodo") + ",";
                strIdSubNodos += aFila[i].id + ",";
            }
        }
        strIdNodos = strIdNodos.substring(0, strIdNodos.length - 1);
        strIdSubNodos = strIdSubNodos.substring(0, strIdSubNodos.length - 1);
        $I("hdnIdNodo").value = strIdNodos;
        $I("hdnIdSubnodo").value = strIdSubNodos;
          
        mostrarProcesando();
        //var ret = window.showModalDialog("../../getResponsableMultiNodo.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value, self, sSize(550, 540));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsableMultiNodo.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value, self, sSize(550, 540))
	        .then(function(ret) {
	            //alert(ret);
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            $I("hdnResponsableOrigen").value = aDatos[0];
		            $I("txtResponsableOrigen").value = aDatos[1];
                
                    borrarCatalogo();
                    mostrarRelacionProyectos();        		    
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el responsable origen.", e.message);
    }
}
function getResponsableDestino(){
    try{
       
        var sTitle = "";
        var sTootTip = "";
        
        var strIdNodos = "";
        var aFila = FilasDe("tblSubnodos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") strIdNodos += aFila[i].getAttribute("nodo") + ",";
        }
        strIdNodos = strIdNodos.substring(0, strIdNodos.length - 1);
        $I("hdnIdNodo").value = strIdNodos;
                
        mostrarProcesando();

        //var ret = window.showModalDialog("../../getResponsableMultiNodo.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value, self, sSize(550, 540));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsableMultiNodo.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value, self, sSize(550, 540))
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
                            aFila[i].setAttribute("responsable_destino", $I("hdnResponsableDestino").value);
                            aFila[i].cells[4].children[0].innerText = $I("txtResponsableDestino").value;
                            
                            sTitle = aFila[i].cells[4].children[0].getAttribute("title");
                            if (sTitle != ""){
                                aFila[i].cells[4].children[0].setAttribute("title", sTootTip); //span
                            }else{
                                aFila[i].cells[4].children[0].setAttribute("boBDY", sTootTip); //span
                            }
                            aFila[i].cells[5].innerHTML = "";
                        }
                    }
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el responsable destino.", e.message);
    }
}
function limpiar() {
    try {
        $I("hdnIdSubnodo").value = "";
        $I("hdnIdNodo").value = "";
        $I("hdnDesNodo").value = "";
        $I("hdnResponsableOrigen").value = "";
        $I("txtResponsableOrigen").value = "";
        var aFila = FilasDe("tblSubnodos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") aFila[i].className ="";
        }        
        borrarCatalogo();
        borrarCatalogo2();                     
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar los horarios del calendario", e.message);
    }
    ocultarProcesando();
}
