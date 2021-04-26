function init(){
    try{
        actualizarLupas("tblTituloResponsables", "tblResponsables");
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
            case "lineas":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        actualizarLupas("tblTitulo", "tblDatos");
		        window.focus();
                scrollTablaLineas();
                break;
           case "procesar":                
                $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
                actualizarLupas("tblTitulo", "tblDatos");
                setTimeout("recuperar();", 20);
                break;
            case "recuperar":
		        $I("divCatalogo2").children[0].innerHTML = aResul[2];
		        $I("divCatalogo2").scrollTop = 0;
		        nTopScrollLineasDest = -1;
                scrollTablaLineasDest();
                actualizarLupas("tblTituloAsignados", "tblDatos2");
                break;
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogoResponsables").children[0].innerHTML = aResul[2];
                //scrollTablaProf();
		        actualizarLupas("tblTituloResponsables", "tblResponsables");
                
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
        }
        ocultarProcesando();
    }
}

function setResponsable(oFila){
    try{
        borrarCatalogo();
        $I("hdnResponsableOrigen").value=oFila.id;
       
        mostrarRelacionLineas();
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el responsable.", e.message);
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
function mostrarRelacionLineas(){
    try{
        var js_args = "lineas@#@";
        js_args += $I("hdnResponsableOrigen").value +"@#@"
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la relación de líneas", e.message);
    }
}

//var oImgTrasOK = document.createElement("<img src='../../../../images/imgTrasladoOK.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgTrasKO = document.createElement("<img src='../../../../images/imgTrasladoKO.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

var oImgTrasOK = document.createElement("img");
oImgTrasOK.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgTrasladoOK.gif");
oImgTrasOK.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTrasKO = document.createElement("img");
oImgTrasKO.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgTrasladoKO.gif");
oImgTrasKO.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var nTopScrollLineas = 0;
var nIDTimeLin = 0;
function scrollTablaLineas(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollLineas){
            nTopScrollLineas = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeLin);
            nIDTimeLin = setTimeout("scrollTablaLineas()", 50);
            return;
        }
        
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollLineas/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
                oFila.ondblclick = function(){insertarLinea(this);};
                oFila.attachEvent('onmousedown', DD);

                switch (oFila.getAttribute("estado"))
                {
                case "A": oFila.cells[0].appendChild(oImgEst1.cloneNode(), null); break;
                case "I": oFila.cells[0].appendChild(oImgEst2.cloneNode(), null); break;
                case "Y": oFila.cells[0].appendChild(oImgEst3.cloneNode(), null);break;
                case "X": oFila.cells[0].appendChild(oImgEst4.cloneNode(), null); break;
                case "B": oFila.cells[0].appendChild(oImgEst5.cloneNode(), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de líneas.", e.message);
    }
}

var nTopScrollLineasDest = 0;
var nIDTimeLinDest = 0;
function scrollTablaLineasDest(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollLineasDest){
            nTopScrollLineasDest = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeLinDest);
            nIDTimeLinDest = setTimeout("scrollTablaLineasDest()", 50);
            return;
        }
        
        var tblDatos2 = $I("tblDatos2");
        var nFilaVisible = Math.floor(nTopScrollLineasDest/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblDatos2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos2.rows[i].getAttribute("sw")) {
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);


                switch (oFila.getAttribute("estado"))
                {
                case "A": oFila.cells[0].appendChild(oImgEst1.cloneNode(), null); break;
                case "I": oFila.cells[0].appendChild(oImgEst2.cloneNode(), null); break;
                case "Y": oFila.cells[0].appendChild(oImgEst3.cloneNode(), null);break;
                case "X": oFila.cells[0].appendChild(oImgEst4.cloneNode(), null); break;
                case "B": oFila.cells[0].appendChild(oImgEst5.cloneNode(), null); break;
                }
                
                oFila.cells[3].appendChild(oImgTrasOK.cloneNode(), null);
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de líneas asignados.", e.message);
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

        var tblResponsables = $I("tblResponsables");
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblResponsables.rows.length);
        var oFila; 
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblResponsables.rows[i].getAttribute("sw")) {
                oFila = tblResponsables.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    oFila.cells[0].appendChild(oImgV.cloneNode(), null);
                }else{
                    oFila.cells[0].appendChild(oImgM.cloneNode(), null);
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
function insertarLinea(oFila){
    try{

        var bExiste = false;
        //1º buscar si existe en el array de líneas y su "opcionBD"
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == oFila.id){
                bExiste = true;
                break;
            }
        }
        if (bExiste){
            //alert("El profesional indicado ya se encuentra asignado a la tarea");
            return;
        }
        
        var oNF = $I("tblDatos2").insertRow();
        oNF.style.height = "20px";
        oNF.id = oFila.id;
        oNF.setAttribute("sw", oFila.getAttribute("sw"));
        oNF.setAttribute("estado",oFila.getAttribute("estado"));
        oNF.setAttribute("procesado","");
        oNF.setAttribute("responsable_origen",oFila.getAttribute("responsable_origen"));
        if ($I("hdnResponsableDestino").value == "")
            oNF.setAttribute("responsable_destino",oFila.getAttribute("responsable_origen"));
        else
            oNF.setAttribute("responsable_destino",$I("hdnResponsableDestino").value);

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);	

        oNF.insertCell().appendChild(oFila.cells[0].children[0].cloneNode(true));
        
        var oNC4 = oNF.insertCell();
        oNC4.appendChild(oFila.cells[1].children[0].cloneNode(true));
        oNC4.children[0].className = "NBR W80";
        
        var oNC5 = oNF.insertCell();
        oNC5.appendChild(oFila.cells[2].children[0].cloneNode(true));
        oNC5.children[0].className = "NBR W310";
        if ($I("txtResponsableDestino").value != "")
            oNC5.children[0].innerText = $I("txtResponsableDestino").value;
        
        var sTitle = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        var sTitle = oNC5.children[0].title;
        if (sTitle != ""){
            oNC5.children[0].title = sTootTip; //span
        }else{
            oNC5.children[0].boBDY = sTootTip; //span
        }
       
        oNF.insertCell();
        actualizarLupas("tblTituloAsignados", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al insertar la línea.", e.message);
    }
}

//function fnRelease()
//{
//    if (beginDrag == false) return;
//       				    
//	window.document.detachEvent( "onmousemove" , fnMove );
//	window.document.detachEvent( "onscroll" , fnMove );
//	window.document.detachEvent( "onmousemove" , fnCheckState );
//	window.document.detachEvent( "onmouseup" , fnRelease );
//	window.document.detachEvent( "onselectstart", fnSelect );
//	
//	var obj = document.all("DW");
//	var oTable = null;
//	var oNF = null;
//    var oNC1 = null;
//    var oNC2 = null;
//    var oNC3 = null;
//    var oNC4 = null;
//    var oNC5 = null;
//    var oNC6 = null;
//	var aLinAsig = new Array();

//	var nRD = $I("hdnResponsableDestino").value;
//	var sRD = $I("txtResponsableDestino").value;

//	var nIndiceInsert = null;
//	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
//	{	
//	    switch (event.srcElement.tagName){
//	        case "TD": nIndiceInsert = event.srcElement.parentElement.rowIndex; break;
//	        case "INPUT": nIndiceInsert = event.srcElement.parentElement.parentElement.rowIndex; break;
//	    }
//	    
//        var sTitleR = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
//        var sTootTipR = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitleR + "] hideselects=[off]\"";

//        switch(oTarget.id){
//	        case "divCatalogo2":
//                oTable = oTarget.getElementsByTagName("table")[0];
//                for (var i=0;i<oTable.rows.length;i++){
//                    aLinAsig[aLinAsig.length] = oTable.rows[i].id;
//                }
//                break;
//        }
//        
//	    for (var x=0; x<=aEl.length-1;x++){
//	        oRow = aEl[x];
//	        switch(oTarget.id){
//		        case "imgPapelera":
//		        case "ctl00_CPHC_imgPapelera":
//		            if (nOpcionDD == 4){
//		                oRow.parentElement.parentElement.deleteRow(oRow.rowIndex);
//		            }
//			        break;
//		        case "divCatalogo2":
//		            if (FromTable == null || ToTable == null) continue;
//		            if (nOpcionDD == 1){
//	                    var sw = 0;
//	                    for (var i=0;i<aLinAsig.length;i++){
//		                    if (aLinAsig[i] == oRow.id){
//			                    sw = 1;
//			                    break;
//		                    }
//	                    }
//                        if (sw == 0){
//	                        oNF = oTable.insertRow();
//                            oNF.style.height = "20px";
//                            oNF.id = oRow.id;
//                            oNF.setAttribute("sw", oRow.getAttribute("sw"));
//                            oNF.setAttribute("estado",oRow.getAttribute("estado"));
//                            oNF.setAttribute("procesado","");
//                            oNF.setAttribute("responsable_origen",oRow.getAttribute("responsable_origen"));
//                            if (nRD=="")
//                                oNF.setAttribute("responsable_destino",oRow.getAttribute("responsable_origen"));
//                            else
//                                oNF.setAttribute("responsable_destino",nRD);

//                            oNF.attachEvent('onclick', mm);
//                            oNF.attachEvent('onmousedown', DD);	
//                            
//                            oNC1 = oNF.insertCell();
//                            if (oRow.cells[0].children.length>0)
//                                oNC1.appendChild(oRow.cells[0].children[0].cloneNode(true));

//                            oNC4 = oNF.insertCell();
//                            oNC4.appendChild(oRow.cells[1].children[0].cloneNode(true));
//                            //oNC4.children[0].style.width = "80px";

//                            oNC5 = oNF.insertCell();
//                            oNC5.appendChild(oRow.cells[2].children[0].cloneNode(true));
//                            //oNC5.children[0].style.width = "310px";
//                            if (sRD != "")
//                                oNC5.children[0].innerText = sRD;
//                            sTitle = oNC5.children[0].title;
//                            if (sTitle != ""){
//                                oNC5.children[0].title = sTootTipR; //span
//                            }else{
//                                oNC5.children[0].boBDY = sTootTipR; //span
//                            }
//                          
//                            oNF.insertCell();
//                        }
//                    }
//			        break;
//			}
//		}
//		
//        switch(oTarget.id){
//	        case "divCatalogo2":
//                scrollTablaLineasDest();
//                break;
//        }
//	}
//    actualizarLupas("tblTituloAsignados", "tblDatos2");
//	oTable = null;
//	killTimer();
//	CancelDrag();
//	obj.style.display	= "none";
//	oEl					= null;
//	aEl.length = 0;
//	oTarget				= null;
//	beginDrag			= false;
//	TimerID				= 0;
//	oRow                = null;
//    FromTable           = null;
//    ToTable             = null;
//}


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
        $I("divCatalogo2").children[0].innerHTML = "<table id='tblDatos2' style='WIDTH: 400px;' class='MM'><colgroup><col style='width: 20px;' /><col style='width:167px;padding-left:3px;' /><col style='width:220px;' /><col style='width: 30px;' /></colgroup></table>";
        actualizarLupas("tblTituloAsignados", "tblDatos2");
//        for (var i=tblDatos2.rows.length-1; i >= 0; i--){
//            tblDatos2.deleteRow(i);
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo de líneas a reasignar", e.message);
    }
}
function borrarCatalogoR(){
    try{
        $I("divCatalogoResponsables").children[0].innerHTML = "<table id='tblResponsables'></table>";
        actualizarLupas("tblTituloResponsables", "tblResponsables");
                
	    $I("txtApellido1").value = "";
	    $I("txtApellido2").value = "";
	    $I("txtNombre").value = "";        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo de responsables", e.message);
    }
}
var bProcesar = false;
function procesar(){
    try{
        mostrarProcesando();
        var bPreguntar = false;
        var sb = new StringBuilder;
        sb.Append("procesar@#@");
        
        var aFila = FilasDe("tblDatos2");
        if (aFila.length == 0){
            mmoff("Inf", "No se han seleccionado líneas a procesar.", 380);
            ocultarProcesando();
            return;
        }
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].getAttribute("procesado")=="1") continue;
            if (aFila[i].getAttribute("responsable_destino") == "") {
                bCorrecto = false;
                ocultarProcesando();
                mmoff("Inf", "Toda línea a reasignar debe tener identificado responsable.", 500, 2500);
                return;
            }
            if (!bPreguntar){
                if (aFila[i].getAttribute("responsable_destino") == "") {
                    bPreguntar = true;
                }
            }
            if (aFila[i].getAttribute("responsable_destino") == null || aFila[i].getAttribute("responsable_destino") == "") aFila[i].setAttribute("responsable_destino") = aFila[i].getAttribute("responsable_origen");

            sb.Append(aFila[i].id +"##"); //0
            sb.Append(aFila[i].getAttribute("responsable_origen") + "##"); //1
            sb.Append(aFila[i].getAttribute("responsable_destino") + "##"); //2
            
            if (aFila[i].cells[3].innerHTML=="") 
                sb.Append(""); //4
            else{
                if (aFila[i].cells[3].innerHTML.indexOf("imgTrasladoOK.gif") != -1) 
                    sb.Append("1"); //4
                else 
                    sb.Append("0"); //4
            }
            sb.Append("///");

        }
        //alert(sb.ToString());return;
        if (bPreguntar) {
            ocultarProcesando();
            jqConfirm("", "Hay líneas para los que no se ha seleccionado responsable.<br /><br />¿Deseas mantener el responsable y continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    mostrarProcesando();
                    RealizarCallBack(sb.ToString(), "");
                }
            });
        }
        else {
            mostrarProcesando();
            RealizarCallBack(sb.ToString(), "");
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}

function limpiar(){
    try{
        borrarCatalogoR();
        borrarCatalogo();
        borrarCatalogo2();            
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar catalogos", e.message);
    }
}    
function getResponsableDestino(){
    try{
        var sTitle = "";
        var sTootTip = "";       
        mostrarProcesando();
//        var ret = window.showModalDialog("../../../Profesional/Default.aspx?nT=1" , self, "dialogwidth:450px; dialogheight:500px; center:yes; status:NO; help:NO;");
	    modalDialog.Show("../../../Profesional/Default.aspx?nT=1", self, sSize(450, 500))
	        .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("hdnResponsableDestino").value = aDatos[0];
	                $I("txtResponsableDestino").value = aDatos[1];

	                sTitle = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
	                sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

	                var aFila = FilasDe("tblDatos2");
	                for (var i = 0; i < aFila.length; i++) {
	                    if (aFila[i].className == "FS") {
	                        aFila[i].setAttribute("responsable_destino") = $I("hdnResponsableDestino").value;
	                        aFila[i].cells[2].children[0].innerText = $I("txtResponsableDestino").value;

	                        sTitle = aFila[i].cells[2].children[0].title;
	                        if (sTitle != "") {
	                            aFila[i].cells[2].children[0].title = sTootTip; //span
	                        } else {
	                            aFila[i].cells[2].children[0].boBDY = sTootTip; //span
	                        }
	                        aFila[i].cells[3].innerHTML = "";
	                    }
	                }
	            }
	            ocultarProcesando();	            
	        });	     
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el responsable destino.", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if ($I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value==""){
	        mmoff("Inf", "Debe indicar usuario.", 200);
	        return;
	    }
	    
	    strInicial=Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
	    if (strInicial == "@#@@#@") return;

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
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
    
    var oNF = null;
    var oNC1 = null;
    var oNC2 = null;
    var oNC3 = null;
    var oNC4 = null;
    var oNC5 = null;
    var oNC6 = null;
    var aLinAsig = new Array();

    var nRD = $I("hdnResponsableDestino").value;
    var sRD = $I("txtResponsableDestino").value;
    
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
        
        var sTitleR = "<label style='width:60px;'>Responsable:</label>" + $I("txtResponsableDestino").value;
        var sTootTipR = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitleR + "] hideselects=[off]\"";

        switch (oTarget.id) 
        {
            case "divCatalogo2":
                for (var i = 0; i < oTable.rows.length; i++) 
                {
                    aLinAsig[aLinAsig.length] = oTable.rows[i].id;
                }
                break;
        }                
        
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
                    if (nOpcionDD == 1) {
                        var sw = 0;
                        for (var i = 0; i < aLinAsig.length; i++) {
                            if (aLinAsig[i] == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 0) {
                            oNF = oTable.insertRow(-1);
                            oNF.style.height = "20px";
                            oNF.id = oRow.id;
                            oNF.setAttribute("sw", oRow.getAttribute("sw"));
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
                            if (oRow.cells[0].children.length > 0)
                                oNC1.appendChild(oRow.cells[0].children[0].cloneNode(true));

                            oNC4 = oNF.insertCell(-1);
                            oNC4.appendChild(oRow.cells[1].children[0].cloneNode(true));
                            //oNC4.children[0].style.width = "80px";

                            oNC5 = oNF.insertCell(-1);
                            oNC5.appendChild(oRow.cells[2].children[0].cloneNode(true));
                            //oNC5.children[0].style.width = "310px";
                            if (sRD != "")
                                oNC5.children[0].innerText = sRD;
                            sTitle = oNC5.children[0].title;
                            if (sTitle != "") {
                                oNC5.children[0].title = sTootTipR; //span
                            } else {
                                oNC5.children[0].boBDY = sTootTipR; //span
                            }

                            oNF.insertCell(-1);
                        }
                    }
                    break;
            }
        }

        actualizarLupas("tblTituloAsignados", "tblDatos2");
        ot('tblDatos2', 0, 0, '', '');
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