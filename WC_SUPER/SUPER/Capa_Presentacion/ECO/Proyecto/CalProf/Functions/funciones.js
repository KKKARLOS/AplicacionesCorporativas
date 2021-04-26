function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        $I("hdnIdCal").value = $I("hdnIdCalIni").value;
        scrollTablaProf();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
            case "getUsuarios":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
                scrollTablaProf();
                break;
            case "grabar":
	            //var returnValue = 1;
                //modalDialog.Close(window, returnValue);	
                bCambios = false;
                if (bSalir) setTimeout("salir()", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
        }
    }
    ocultarProcesando();
}
function getUsuarios(){
    try{
        //alert("mostrarRelacionTecnicos("+ sOpcion +","+ sValor +")");
        var js_args = "getUsuarios@#@";
        js_args += $I("hdnPSN").value +"@#@";
        js_args += $I("hdnCualidad").value +"@#@";
        js_args += ($I("chkMostrarBajas").checked)? "1":"0";

        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function getCalendario(){
    try{
        mostrarProcesando();
        var sPant = strServer + "Capa_Presentacion/ECO/Proyecto/getCalendario.aspx?idficepi=" + sIdFicepiEmpleado + "&nodo=" + $I("hdnIdNodo").value;
        //var ret = window.showModalDialog(sPant, self, sSize(460, 455));
        modalDialog.Show(sPant, self, sSize(460, 455))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            $I("hdnIdCal").value = aDatos[0];
		            $I("txtCalDestino").value = aDatos[1];
        		    
                    var aFila = FilasDe("tblDatos2");
                    for (var i=0; i < aFila.length; i++){
                        if (aFila[i].className == "FS"){
                            aFila[i].className = "";
                            aFila[i].setAttribute("cal_destino", aDatos[0]);
                            aFila[i].cells[2].children[0].innerText = aDatos[1];
                            
                        }
                    }
	            }
	            ocultarProcesando();
	        });
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el calendario destino.", e.message);
    }
}
function salir() {
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                salirContinuar();
            }
        });
    } else salirContinuar();
}
function salirContinuar() {
    try {
        var returnValue = 1;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
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

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                oFila.cells[1].children[0].ondblclick = function(){insertarRecurso(this.parentNode.parentNode);}
                oFila.cells[2].children[0].ondblclick = function(){insertarRecurso(this.parentNode.parentNode);}
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1") {
                    setOp(oFila.cells[0].children[0], 20);
                    oFila.cells[1].style.color = "red";
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla 1.", e.message);
    }
}

var nTopScrollProfDest = -1;
var nIDTimeProfDest = 0;
function scrollTablaProfDest(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProfDest){
            nTopScrollProfDest = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfDest);
            nIDTimeProfDest = setTimeout("scrollTablaProfDest()", 50);
            return;
        }
        
        var tblDatos2 = $I("tblDatos2");
        var nFilaVisible = Math.floor(nTopScrollProfDest/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblDatos2.rows.length);
        var oFila;
        //for (var i = nFilaVisible; i < tblDatos2.rows.length; i++){
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos2.rows[i].getAttribute("sw")) {
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("sexo")=="V"){
                    oFila.cells[0].appendChild(oImgIV.cloneNode(true), null);
                }else{
                    oFila.cells[0].appendChild(oImgIM.cloneNode(true), null);
                }
                if (oFila.getAttribute("baja")=="1"){
                    setOp(oFila.cells[0].children[0], 20);
                    oFila.cells[1].style.color = "red";
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla 2.", e.message);
    }
}


function insertarRecurso(oFila){
    try{
        var idRecurso = oFila.id;
        var bExiste = false;
        //1º buscar si existe en el array de recursos y su "opcionBD"
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
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
        oNF.id = oFila.id;
        oNF.setAttribute("sw", oFila.getAttribute("sw"));
        oNF.setAttribute("baja", oFila.getAttribute("baja"));
        oNF.setAttribute("procesado", "");

        oNF.setAttribute("cal_destino", $I("hdnIdCal").value);
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);        
        
        var oNC1 = oNF.insertCell(-1);
        if (oFila.cells[0].children.length>0)
            oNC1.appendChild(oFila.cells[0].children[0].cloneNode(true));
        
        var oNC2 = oNF.insertCell(-1);
        oNC2.appendChild(oFila.cells[1].children[0].cloneNode(true));
        oNC2.children[0].className = "NBR W210";
        
        if (oNF.getAttribute("baja") == "1") {
            oNC2.style.color = "red";
        }
        
        var oNC3 = oNF.insertCell(-1);
        oNC3.appendChild(oFila.cells[2].children[0].cloneNode(true));
        oNC3.children[0].className = "NBR W200";
        oNC3.children[0].innerText = $I("txtCalDestino").value;
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al usuario.", e.message);
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
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

	
	var obj = $I("DW");
	var oTable = null;
	var oNF = null;
    var oNC1 = null;
    var oNC2 = null;
    var oNC3 = null;
	var aProfAsig = new Array();
	var nND = $I("hdnIdCal").value;
	var sND = $I("txtCalDestino").value;
	var nIndiceInsert = null;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{
	    switch (oElement.tagName) {
	        case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
	    }		

        switch(oTarget.id){
            case "divCatalogo2":
            case "ctl00_CPHC_divCatalogo2":	
                oTable = oTarget.getElementsByTagName("TABLE")[0];
                for (var i=0;i<oTable.rows.length;i++){
                    aProfAsig[aProfAsig.length] = oTable.rows[i].id;
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
	                    for (var i=0;i<aProfAsig.length;i++){
		                    if (aProfAsig[i] == oRow.id){
			                    sw = 1;
			                    break;
		                    }
	                    }
                        if (sw == 0){
	                        oNF = oTable.insertRow(-1);
                            oNF.style.height = "20px";
                            oNF.id = oRow.id;

                            oNF.setAttribute("sw", oRow.getAttribute("sw"));
                            oNF.setAttribute("baja", oRow.getAttribute("baja"));
                            oNF.setAttribute("procesado", "");
                            oNF.setAttribute("cal_destino", nND);
                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);        
                            
                            oNC1 = oNF.insertCell(-1);
                            if (oRow.cells[0].children.length>0)
                                oNC1.appendChild(oRow.cells[0].children[0].cloneNode(true));
                            
                            oNC2 = oNF.insertCell(-1);
                            oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true));
                            oNC2.children[0].style.width = "210px";

                            if (oNF.getAttribute("baja") == "1") {
                                oNC2.style.color = "red";
                            }
                            
                            oNC3 = oNF.insertCell(-1);
                            oNC3.appendChild(oRow.cells[2].children[0].cloneNode(true));
                            oNC3.children[0].style.width = "200px";
                            oNC3.children[0].innerText = sND;//$I("txtCalDestino").value;
                            bCambios = true;
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

function comprobar(){
    var bRes=true;
    try{
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].getAttribute("cal_destino") == ""){
                bRes = false;
                mmoff("War", "No tiene asignado calendario o es el mismo de origen.", 350);
                seleccionar(aFila[i]);
                break;
            }
        }
	    return bRes;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar datos.", e.message);
    }
}
function grabarSalir() {
    bSalir = true;
    grabar();
}
function grabar(){
    if ($I("tblDatos2").rows.length==0) return;
    if (!comprobar()) return;

    try{
        var js_args = "grabar@#@";
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            js_args += aFila[i].id + "##" + aFila[i].getAttribute("cal_destino") + "///";
        }
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    
	}catch(e){
		mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}