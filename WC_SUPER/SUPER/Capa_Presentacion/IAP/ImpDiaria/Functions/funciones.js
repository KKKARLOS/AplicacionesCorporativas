var MonthNames      = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
var aLiteralesDias	= new Array("Lunes","Martes","Miércoles","Jueves","Viernes","Sábado","Domingo");
var aLetra		    = new Array("L","M","X","J","V","S","D");
var aImputaciones	= new Array("0,00","0,00","0,00","0,00","0,00","0,00","0,00");
var oInput = null;
var oChkFinTar = null;
var sUltimoDiaTareaRecur = "";

function init(){
	try{
        if ($I("hdnAccGasvi").value == "S")
            $I("btnGasvi").style.visibility="visible";
        else
            $I("btnGasvi").style.visibility="hidden";
        
        if (bRes1024) setResolucion1024();

        controlSemAntSig();
        
		for (var intIndDia=intPrimerDiaSemana;intIndDia<intUltimoDiaSemana;intIndDia++){
			aImputaciones[intIndDia] = $I("txtTotal"+aLetra[intIndDia]).value;
		}
		ocultarProcesando();
		$I("divCatalogo").style.position = "relative";
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	}
}

var message="Función deshabilitada";
function click(e) 
{
    if (!e) e = event; 
    var elem = e.srcElement ? e.srcElement : e.target; 
    if (e.button == 2) 
    {
            //alert(event.srcElement.tagName.toUpperCase());
            if (elem.tagName.toUpperCase()=="INPUT"){
                var sID = elem.id;
                if (sID.indexOf("txtLU-") > -1
                    || sID.indexOf("txtMA-") > -1
                    || sID.indexOf("txtMI-") > -1
                    || sID.indexOf("txtJU-") > -1
                    || sID.indexOf("txtVI-") > -1
                    || sID.indexOf("txtSA-") > -1
                    || sID.indexOf("txtDO-") > -1){
                        seleccionar(elem);
                        elem.focus();
		                elem.select();
                        $I("hdnInputActivo").value = sID;
                        AccionBotonera("ComentarioBot","H"); 
                        mostrarComentario();
                }
            }
            return false;
    }
//    else{//Para saber en que celda estoy si pulso el botón de gastos de viaje
//        var sID = event.srcElement.id;
//        if (sID != "btnGasvi")
//            $I("hdnInputActivo").value = "";
//        if (event.srcElement.tagName.toUpperCase()=="INPUT"){
//            
//            if (sID.indexOf("txtLU-") > -1
//                || sID.indexOf("txtMA-") > -1
//                || sID.indexOf("txtMI-") > -1
//                || sID.indexOf("txtJU-") > -1
//                || sID.indexOf("txtVI-") > -1
//                || sID.indexOf("txtSA-") > -1
//                || sID.indexOf("txtDO-") > -1){
//                    $I("hdnInputActivo").value = sID;
//            }
//        }
//    }
}
document.onmousedown=click;

function FinTareaRecur(obj)
{
    oChkFinTar = obj;
    mostrarProcesando();
    var js_args = "FinTareaRecur@#@";    
    js_args += obj.parentNode.parentNode.getAttribute("T")+"@#@";  
    RealizarCallBack(js_args, "");           
}

function mostrarComentario(){
    try{
	    //if (!bBotonHabilitado("ComentarioBot")) return;
	    
	    mostrarProcesando();
	    var idObjeto = $I("hdnInputActivo").value;
	    
	    rd($I(idObjeto));
	    //Si la tarea está paralizada, no se permite modificar el comentario (modo lectura)
	    var oFila = $I(idObjeto).parentNode.parentNode;
	    oInput = $I(idObjeto);
/*
	    if (document.all) {
	        if (oFila.getAttribute("idnaturaleza") != 27 && oInput.className.indexOf("Vacaciones") > -1
                ) {ocultarProcesando(); return;}
                
	    }
	    else {
	        if (oFila.getAttribute("idnaturaleza") != 27 && oInput.getAttribute("class").indexOf("Vacaciones") > -1
                ) {ocultarProcesando(); return;}
	    }
 */       
    	
	    //var reg = /\+/g;
	    var strCom = $I(idObjeto).getAttribute("comentario");
	    //strCom = strCom.replace(reg,"%2B");
	    //reg = /\$/g;
	    //strCom = strCom.replace(reg,"%24");
         
	    intFila = $I($I("hdnInputActivo").value).parentNode.parentNode.rowIndex;
	    intCelda = $I($I("hdnInputActivo").value).parentNode.cellIndex;

	    if (aDiasSemana[intCelda-3].length == 1) strDia = "0"+ aDiasSemana[intCelda-3];
	    else strDia = aDiasSemana[intCelda-3];
    	
	    if (intMes.toString().length == 1) strMes = "0"+ intMes;
	    else strMes = intMes;

        //if (strCom!="@") strCom="";
	    strTitulo = strDia+"/"+strMes+"/"+intAnno  + "    " + $I("tblDatos").rows[intFila].cells[0].innerText;
    	var sPantalla = strServer + "Capa_Presentacion/IAP/ImpDiaria/Comentario.aspx?strComentario="+ codpar(strCom) +"&strTitulo="+ codpar(strTitulo) +"&estado="+oFila.getAttribute("estado") +"&imputacion=" + oFila.getAttribute("imp") +"&idtarea=" + oFila.id +"&fecha=" + strDia+"-"+strMes+"-"+intAnno +"&empleado=" + num_empleado;
        modalDialog.Show(sPantalla, self, sSize(450,230))
            .then(function(ret) {
	            if (ret != null){
	                //if (Utilidades.escape(ret) != $I(idObjeto).getAttribute("comentario")){
	                if (ret != $I(idObjeto).getAttribute("comentario")) {
	                    $I(idObjeto).setAttribute("comentario", ret);
	                    if (ret != "") {
		                    if (($I(idObjeto).className == "LabImp") || ($I(idObjeto).className == "LabImpCom"))	
		                        $I(idObjeto).className = "LabImpCom";
		                    else if (($I(idObjeto).className == "Vacaciones") || ($I(idObjeto).className == "VacacionesCom"))	 //Vacaciones
		                        $I(idObjeto).className = "VacacionesCom";
		                    else $I(idObjeto).className = "FesImpCom";

		                }else{
		                    if (($I(idObjeto).className == "LabImpCom") || ($I(idObjeto).className == "LabImp"))	
		                        $I(idObjeto).className = "LabImp";
		                    else if (($I(idObjeto).className == "Vacaciones") || ($I(idObjeto).className == "VacacionesCom"))	 //Vacaciones
		                        $I(idObjeto).className = "Vacaciones";
		                    else    $I(idObjeto).className = "FesImp";
		                }
		                $I(idObjeto).setAttribute("bModif","1");
		                mfa(oFila, "U");
		                activarGrabar();
		                $I(idObjeto).focus();
		            }
	            }
            });
        window.focus();

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el comentario", e.message);
	}
}

//Habilitar Jornada (botón)
function hj(e){
    try{
        if (!e) e = event; 
        var oControl = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
        $I("hdnInputActivo").value = oControl.id;
        //$I("hdnInputActivo").value = event.srcElement.id;

        setTimeout("AccionBotonera('ComentarioBot', 'H')",100);
        setTimeout("AccionBotonera('Jornada', 'H')",100);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el comentario", e.message);
	}
}
//Deshabilitar Jornada (botón)
function dj(){
    setTimeout("AccionBotonera('ComentarioBot', 'D')",20);
    setTimeout("AccionBotonera('Jornada', 'D')",20);
//    AccionBotonera("ComentarioBot", "D");
//    AccionBotonera("Jornada", "D");
}
//function hbf(oFila){
//    $I("btnBitacora").src = "../../../images/imgTrans9x9.gif";
//}
//Habilitar botones al marcar una tarea
function hbt(oFila){
	AccionBotonera("Semana", "H");
	AccionBotonera("TareaBot", "H");
//	AccionBotonera("Bitacora", "D");
//	AccionBotonera("BitacoraPT", "D");
    switch(oFila.getAttribute("sAccesoBitacoraT")){
        case "E": 
            $I("btnBitacora").src = "../../../images/imgBTTW.gif";
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo escritura a la bitácora de tarea.";
            break;
        case "L": 
            $I("btnBitacora").src = "../../../images/imgBTTR.gif"; 
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de tarea.";
            break;
        default: 
            $I("btnBitacora").src = "../../../images/imgBTTN.gif"; 
            $I("btnBitacora").style.cursor = "default";
            $I("btnBitacora").onclick = null;
            $I("btnBitacora").title = "Sin acceso a la bitácora de tarea.";
            break;
    }
}

//Habilitar bitácora de PE
function hbitPE(oFila){
    setTimeout("AccionBotonera('ComentarioBot', 'D')",100);
    setTimeout("AccionBotonera('TareaBot', 'D')",100);    
    setTimeout("AccionBotonera('Jornada', 'D')",100);
    setTimeout("AccionBotonera('Semana', 'D')",100); 
       
//    AccionBotonera("ComentarioBot", "D");
//	AccionBotonera("TareaBot", "D");
//    AccionBotonera("Jornada", "D");
//	AccionBotonera("Semana", "D");
////	AccionBotonera("BitacoraPT", "D");
//	AccionBotonera("BitacoraT", "D");
    switch(oFila.getAttribute("sAccesoBitacoraPE")){
        case "E": 
            $I("btnBitacora").src = "../../../images/imgBTPEW.gif";
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo esritura a la bitácora de proyecto económico.";
            break;
        case "L": 
            $I("btnBitacora").src = "../../../images/imgBTPER.gif"; 
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de proyecto económico.";
            break;
        default: 
            $I("btnBitacora").src = "../../../images/imgBTPEN.gif"; 
            $I("btnBitacora").style.cursor = "default";
            $I("btnBitacora").onclick = null;
            $I("btnBitacora").title = "Sin acceso a la bitácora de proyecto económico.";
            break;
    }
}
//Habilitar bitácora de PT
function hbitPT(oFila){
    setTimeout("AccionBotonera('ComentarioBot', 'D')",100);
    setTimeout("AccionBotonera('TareaBot', 'D')",100);    
    setTimeout("AccionBotonera('Jornada', 'D')",100);
    setTimeout("AccionBotonera('Semana', 'D')",100); 

//	AccionBotonera("Bitacora", "D");
//	AccionBotonera("BitacoraT", "D");
    switch(oFila.getAttribute("sAccesoBitacoraPT")){
        case "E": 
            $I("btnBitacora").src = "../../../images/imgBTPTW.gif";
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo esritura a la bitácora de proyecto técnico.";
            break;
        case "L": 
            $I("btnBitacora").src = "../../../images/imgBTPTR.gif"; 
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de proyecto técnico.";
            break;
        default: 
            $I("btnBitacora").src = "../../../images/imgBTPTN.gif"; 
            $I("btnBitacora").style.cursor = "default";
            $I("btnBitacora").onclick = null;
            $I("btnBitacora").title = "Sin acceso a la bitácora de proyecto técnico.";
            break;
    }
}
function imputarJornada(){
    try{
        //if (!bBotonHabilitado("Jornada")) return;
	    var idObjeto = $I("hdnInputActivo").value;

	    if (idObjeto != ""){
            var oFila = $I(idObjeto).parentNode.parentNode;
        if (oFila.getAttribute("estado") == 0 || oFila.getAttribute("estado") == 2      //paralizada o pendiente
            || (oFila.getAttribute("estado") == 3 && oFila.getAttribute("imp") == 0)    //finalizada sin posibilidad de imputar
            || (oFila.getAttribute("estado") == 4 && oFila.getAttribute("imp") == 0)) return; //cerrada sin posibilidad de imputar

		    var strLetra = idObjeto.substring(idObjeto.length-1,idObjeto.length);
		    for (var i=0; i<7; i++){
		        if (strLetra == aLetra[i]) {

		            //if (document.all) {
		            //    if (oFila.getAttribute("idnaturaleza") != 27 && oFila.cells[i+3].children[0].className.indexOf("Vacaciones") > -1
                    //        ) continue;
		            //}
		            //else {
		            //    if (oFila.getAttribute("idnaturaleza") != 27 && oFila.cells[i + 3].children[0].getAttribute("class").indexOf("Vacaciones") > -1
                    //        ) continue;
		            //}

		            if (document.all) {
		                if (oFila.cells[i + 3].children[0].className == "OutProy"
                            || oFila.cells[i + 3].children[0].className == "OutVig")
                             continue;
		            }
		            else {
		                if (oFila.cells[i + 3].children[0].getAttribute("class") == "OutProy"
                            || oFila.cells[i + 3].children[0].getAttribute("class") == "OutVig"
                            ) continue;
		            }
				    if (aSemLab[i] == "1" && aHorasJornada[i] == "0"){
					    mmoff("Inf","No hay datos sobre las horas correspondientes a la jornada del "+ aLiteralesDias[i] +".",450);
					    return;
					    break;
				    }
        		    if (aHorasJornada[i] != "0"){
                        $I(idObjeto).value = aHorasJornada[i];
				        rd($I(idObjeto));
				    }
    			    break;
			    }
		    }
		    mfa($I(idObjeto).parentNode.parentNode , "U");
            activarGrabar();
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al imputar la jornada", e.message);
	}
}

function imputarSemana(){
    try{
        if (!Botonera.bBotonHabilitado("Semana")) return;
        if (iFila == -1) return;
        if ($I("hdnInputActivo").value != "") $I($I("hdnInputActivo").value).blur();
        var oFila = $I("tblDatos").rows[iFila];
        
        if (oFila.getAttribute("estado") == 0 || oFila.getAttribute("estado") == 2      //paralizada o pendiente
            || (oFila.getAttribute("estado") == 3 && oFila.getAttribute("imp") == 0)    //finalizada sin posibilidad de imputar
            || (oFila.getAttribute("estado") == 4 && oFila.getAttribute("imp") == 0)) return; //cerrada sin posibilidad de imputar
            
        for (var i=0; i<7; i++){
            if (aFechas[i] == "") continue;
            if (document.all) 
            {
                if (oFila.cells[i+3].children[0].className == "OutProy" 
                    || oFila.cells[i+3].children[0].className == "OutVig"
		            || oFila.cells[i+3].children[0].className.indexOf("FesImp") > -1
                    //|| oFila.cells[i + 3].children[0].className.indexOf("Vacaciones") > -1
                    ) continue;
            }
            else
            {
                if (oFila.cells[i+3].children[0].getAttribute("class") == "OutProy" 
                    || oFila.cells[i+3].children[0].getAttribute("class") == "OutVig"
		            || oFila.cells[i + 3].children[0].getAttribute("class").indexOf("FesImp") > -1
                    //|| oFila.cells[i + 3].children[0].getAttribute("class").indexOf("Vacaciones") > -1
                    ) continue;
            }                
            
		    if (aSemLab[i] == "1" && aHorasJornada[i] == "0"){
			    mmoff("Inf","No hay datos sobre las horas correspondientes a la jornada del "+ aLiteralesDias[i] +".",450);
			    continue;
		    }
		    if (aHorasJornada[i] != "0"){
                oFila.cells[i+3].children[0].value = aHorasJornada[i];
		        var sw = rd(oFila.cells[i+3].children[0]);
		    }
        }
        mfa(oFila , "U");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al imputar la jornada", e.message);
	}
}


function activarGrabar(){
    try{
	    AccionBotonera("Grabar", "H");
	    AccionBotonera("GrabarReg", "H");
	    /* Controlar que no se impute más de dos meses a partir del último mes cerrado.*/
	    if (!bMesCerrado) AccionBotonera("GrabarSS", "H");

        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón 'Grabar'", e.message);
	}
}


var bCambios = false;

function comprobarGrabar(strOpcion) {
    if (bCambios) {
        jqConfirm("","Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabarAux(strOpcion);
                return false;
            } else return true;
        });
    }
    else return true;
}
function grabarSiguienteAux(){
    if (!Botonera.bBotonHabilitado("GrabarSS")) return;
    mostrarProcesando();
	setTimeout("grabar('grabarss')",50);
}

function grabarAux(strOpcion){
    if (!Botonera.bBotonHabilitado("Grabar")) return;
    mostrarProcesando();
	setTimeout("grabar('"+ strOpcion +"')",50);
}

var bDiaSinImputar = false;
var strAccion = "";

function grabar(strOpcion){
    try{
        if (!Botonera.bBotonHabilitado("Grabar")) return;

        if (strOpcion==null) strOpcion = 'grabar';
        
	    strAccion = strOpcion;
	    bDiaSinImputar = false;
	    var iMarcarFinalizada = 0;
	    var aFilas = FilasDe("tblDatos");
	    var strPE = "", strPT = "", sFFE = "";
	    var nETE = 0, nEAT = 0, nEP = 0;
	    for (var a=0; a < aFilas.length; a++){
	        if (aFilas[a].getAttribute("tipo") == "PSN") strPE = (document.all)? aFilas[a].cells[0].innerText : aFilas[a].cells[0].textContent;
	        else if (aFilas[a].getAttribute("tipo") == "PT") strPT = (document.all)? aFilas[a].cells[0].innerText : aFilas[a].cells[0].textContent;
	        if (aFilas[a].getAttribute("bd") != "U") continue;
		    if (aFilas[a].getAttribute("obligaest") == "1"){
	            nETE = parseFloat(dfn(getCelda(aFilas[a], 10)));
	            sFFE = getCelda(aFilas[a], 11);
	            nEAT = parseFloat(dfn(getCelda(aFilas[a], 13)));
	            nEP = parseFloat(dfn(getCelda(aFilas[a], 14)));

			    //alert ("nETE = "+ nETE +"\nsFFE = "+ sFFE +"\nnEAT = "+ nEAT +"\nnEP = "+ nEP);
			    if (nEAT > 0) { 
				    if (aFilas[a].cells[12].children[0].checked == true){
				        setCelda(aFilas[a], 10, getCelda(aFilas[a], 13));
				        setCelda(aFilas[a], 14, " ");
				    }
				    if ((nETE == 0) && (aFilas[a].cells[12].children[0].checked == false)){
					    ocultarProcesando();
					   mmoff("Inf", "Debes introducir el esfuerzo total estimado de la siguiente tarea:<br><br>Proyecto:   " + strPE + "<br>P.T.:   " + strPT + "<br>Tarea:  " + aFilas[a].cells[0].innerText, 380);
					    return false;
				    }
				    if ((sFFE == "") && (aFilas[a].cells[12].children[0].checked == false)){
					    ocultarProcesando();
					   mmoff("Inf", "Debes introducir la fecha de fin estimada de la siguiente tarea:<br><br>Proyecto:   " + strPE + "<br>P.T.:   " + strPT + "<br>Tarea:  " + aFilas[a].cells[0].innerText, 380);
					    return false;
				    }
				    if ((nEP < 0) && (aFilas[a].cells[12].children[0].checked == false)){
					    ocultarProcesando();
					    mmoff("WarPer","No puedes dejar un esfuerzo pendiente negativo en la siguiente tarea:<br><br>Proyecto: " + strPE + "<br>P.T.: " + strPT +"<br>Tarea:  " + aFilas[a].cells[0].innerText ,380);
					    return false;
				    }
				    if ((nEP == 0) && (aFilas[a].cells[12].children[0].checked == false)){
					    //if (confirm("La tarea: '"+ aFilas[a].cells[0].innerText +"' tiene un esfuerzo pendiente de 0 horas.\n\nPulse \"Aceptar\" si desea marcarla como finalizada.")){
					    //    aFilas[a].cells[12].children[0].checked=true;
					    //}
				        iMarcarFinalizada = a;
				        break;
				    }
			    }
		    }
	    }
    	
	    var aTotales = $I("tblResultado").getElementsByTagName("INPUT");
        // Control para no imputar más de 24h por jornada.
	    for (var i=0; i < aTotales.length-1; i++){
		    if (parseFloat(dfn(aTotales[i].value)) > 24){
			    ocultarProcesando();
			    mmoff("WarPer", "No se pueden imputar jornadas superiores a 24h y el " + aLiteralJornada[i] + " has imputado " + aTotales[i].value, 380);
			    return;
		    }
	    }
        
	    // Control para no dejar huecos.
	    
	    var bImputado = false;
	    
	    var bImputacionesSemanasPosteriores = false;
	    var strUltFechaSemana = "";  // Identificamos la fecha del último día de la semana
	    
        for (var i=aTotales.length-1; i >= 0; i--){	   	        
	        if (aTotales[i].value == "") continue;
	        strUltFechaSemana = new Date(aFechas[eval(i)].substring(6,10),aFechas[eval(i)].substring(3,5)-1,aFechas[eval(i)].substring(0,2));
	        break;
	    }
  
	    for (var i=aTotales.length-1; i >= 0; i--){	   	        
		    if (aTotales[i].value != ""){
			    strLetra = aTotales[i].id.substring(aTotales[i].id.length-1,aTotales[i].id.length);
			    
			    if (aTotales[i].value != "0,00"){
				    if (!bImputado){
					    bImputado = true;				    
					    var strUltimaImputacion = aFechas[eval(i+intPrimerDiaSemana)];
					    var strNuevaUltimaFecha = new Date(intAnno,eval(intMes-1),aDiasSemana[eval(i+intPrimerDiaSemana)]);
				    }
			    }else if (controlhuecos){   //control de huecos únicamente para empleados internos.
				    //1º buscar el indice de la letra en el array aLetra.
				    var intIndice = aLetra.join("").indexOf(strLetra);
				    
				    //2º buscar el día que corresponde a ese índice en el array aDiasSemana.
				    var intDiasSemana = aFechas[intIndice];
				    var bFestivo = false;
				    
				    //Si es no laborable o las jornadas por defecto son 0, 
				    //no se tiene en cuenta para el control de huecos.
				    if ((aSemLab[intIndice] == 0) || (aHorasJornada[intIndice]=="0")){
				        bFestivo = true;
				    }else{
				        //3º buscar si ese día está entre los festivos.
				        for (var j=0;j<aFestivos.length;j++){
					        if (aFestivos[j] == intDiasSemana){
						        bFestivo = true;
						        break;
					        }
				        }
				    }
				    if (!bFestivo){
					    var strLiteralDia = aLiteralesDias[i];
					    var intDia = aDiasSemana[i];
					    bDiaSinImputar = true;
				    }

				    var strFechaAux = new Date(intAnno,eval(intMes-1),aDiasSemana[i]);   
				                                                                               
                    if (strUltImputac>strUltFechaSemana&&!bFestivo) bImputacionesSemanasPosteriores = true;
                    
                    var u=0;
                    if (i==6) u = i;
                    else u = i + 1;
                                                        
            	    if (strUltImputac>strFechaAux && aTotales[u].value == "0,00" && !bFestivo) strUltImputac = strFechaAux;
                        			    
				    /*strMsg = "strLetra: "+ strLetra +"\n";
				    strMsg += "strUltImputac: "+ strUltImputac +"\n";
				    strMsg += "strFechaAux: "+ strFechaAux +"\n";
				    strMsg += "bFestivo: "+ bFestivo +"\n";
				    strMsg += "bImputado: "+ bImputado;
				    alert(strMsg);*/
				    if (strFechaAux != Date(2000,0,1)){
					    //control para que no tenga en cuenta los días anteriores a la fecha de alta del recurso.
					    if (strFechaAux.getTime() >= intFecAlta){ 
                             if (
                                 (!bFestivo) && (bImputado)
                                 || (!bFestivo && !bImputado && strUltImputac >= strFechaAux && aTotales[u].value == "0,00")
                                 || (!bFestivo && !bImputado && strOpcion == "grabarss")
                                 || (bImputacionesSemanasPosteriores)
                                 ){

							    if (aImputaciones[i] == "0,00"){
								    var strMensaje = "Denegado. Detectado hueco el "+ strLiteralDia.toLowerCase() +" "+ intDia; 
							    }else{
								    var strMensaje = "No se permite dejar días laborables sin imputaciones, cuando antes sí las tenían.<br><br>";
								    strMensaje +=    "Si quiere eliminar la imputación de esfuerzo a un proyecto/tarea, deberá borrar ";
								    strMensaje +=    "dicha imputación y asignar los esfuerzos a otro proyecto/tarea antes de grabar ";
								    strMensaje +=    "los cambios.";
							    }
							    ocultarProcesando();
							    mmoff("WarPer", strMensaje, 500, 20000);
							    return false;
						    }
					    }
				    }
			    }
		    }
	    }

	    if (strOpcion == "semanaSiguiente" && bDiaSinImputar && controlhuecos){
		    ocultarProcesando();
		    mmoff("WarPer","No se puede acceder a la siguiente semana<br>si hay días sin imputar en la actual",380);
		    return;
	    }

	    var strQuery = "";
        var sb = new StringBuilder;
        //sb.Append($I("hdnIdNodo").value +"##"); //0  	
    	var oFila;
    	var oControl;
    	var sAuxLog = "";
    	var bHayQueGrabarCelda = false;
    	var iImputarDiaVacaProyNoVaca = 0;
    	for (var i=0; i < $I("tblDatos").rows.length; i++){
    	    if ($I("tblDatos").rows[i].getAttribute("bd") != "U") continue;
    	    oFila = $I("tblDatos").rows[i];
    	    //

    	    //Mikel 18/02/2015
    	    //if (num_empleado == 7109) {
    	    //    if (sAuxLog=="")
    	    //        sAuxLog = "id=" + oFila.id + " tipo=" + oFila.getAttribute("tipo") + " bd=" + oFila.getAttribute("bd");
    	    //    else
    	    //        sAuxLog+= "///id=" + oFila.id + " tipo=" + oFila.getAttribute("tipo") + " bd=" + oFila.getAttribute("bd");
    	    //}
    	    //var iMostrarUnaVez=0;
    	    var bRespuesta;
            for (var x=0; x<aFechas.length; x++){
                if (aFechas[x] == "") continue;
                oControl = oFila.cells[x + 3].children[0]; //+3 de denominación, otc y otl

                if (oFila.getAttribute("tipo") == "T" && oFila.getAttribute("idnaturaleza") != 27  && 
                     ( (oFila.cells[x + 3].children[0].className == "Vacaciones" || oFila.cells[x + 3].children[0].className == "VacacionesCom") && oFila.cells[x + 3].children[0].value!="" && oFila.cells[x + 3].children[0].value!="0,00"))
                {
                    ocultarProcesando();
                    iImputarDiaVacaProyNoVaca = 1;
                }

                //Mikel 18/02/2015
                //if (x < 5) {
                //    if (num_empleado == 7109) {
                //        sAuxLog += " dia=" + x + " bModif=" + oControl.getAttribute("bModif");
                //        sAuxLog += " vOri=" + oControl.getAttribute("oValueOriginal") + " val=" + oControl.value;
                //    }
                //}
                //1º Grabar las imputaciones modificadas
                bHayQueGrabarCelda = false;
                if (oControl.getAttribute("bModif") == "1")
                    bHayQueGrabarCelda = true;
                else {//Mikel 23/02/2015 Compruebo si el valor de entrada y el de salida es diferente
                    if (oControl.value != "") {
                        if(oControl.getAttribute("oValueOriginal") != oControl.value)
                            bHayQueGrabarCelda = true;
                    }
                }
                if (bHayQueGrabarCelda) { //solo se graban las modificadas
			        /*
			        0. Indicador de si es una Tarea o una estimación
			        1. Indicador de la acción a realizar: insert, update... "I","D",...
			        2. Número de empleado
			        3. Número de tarea
			        4. Fecha del consumo
			        5. Horas imputadas
			        6. Comentario
			        7. Horas por jornada (estandares o de proyecto), opcional
			        8. Proyecto técnico, opcional
			        9. FFE anterior, opcional
			        10. ETE anterior, opcional
        			
			        Los datos van separados por el doble signo del punto y coma "{{}}"
			        Los objetos van separados por doble arroba "##"
			        */
                    sb.Append("T{{}}");//0
                    
                    if (parseFloat(dfn(oControl.getAttribute("oValueOriginal"))) != 0 && parseFloat(dfn(oControl.value)) == 0) sb.Append("D{{}}");
                    else if (parseFloat(dfn(oControl.getAttribute("oValueOriginal"))) == 0 && parseFloat(dfn(oControl.value)) != 0) sb.Append("I{{}}");
                    else sb.Append("U{{}}");//1
                    
                    sb.Append(num_empleado +"{{}}");//2
                    sb.Append(oFila.getAttribute("T") +"{{}}");//3
                    sb.Append(aFechas[x] +"{{}}");//4
                    sb.Append((oControl.value=="")? "0{{}}":oControl.value +"{{}}");//5
                    sb.Append(Utilidades.escape(oControl.getAttribute("comentario")) +"{{}}");//6
                    sb.Append(aHorasJornada[x] +"{{}}");//7
                    sb.Append(oFila.getAttribute("PT") +"#{@");//8
                }
            }
            //2º Grabar las estimaciones
            if (oFila.cells[10].children[0].getAttribute("bModif") == "1"
                || oFila.cells[11].children[0].getAttribute("bModif") == "1"
                || oFila.cells[12].children[0].getAttribute("bModif") == "1"){
                sb.Append("E{{}}");//0
                sb.Append("U{{}}");//1
                sb.Append(num_empleado +"{{}}");//2
                sb.Append(oFila.getAttribute("T") +"{{}}");//3
                sb.Append(getCelda(oFila, 11) +"{{}}");//4
                sb.Append(getCelda(oFila, 10) +"{{}}");//5
                sb.Append((oFila.cells[12].children[0].checked==true)? "1{{}}" : "0{{}}"); //6
                sb.Append(oFila.cells[12].children[0].getAttribute("sAntes") +"{{}}"); //7
                sb.Append(oFila.getAttribute("PT") +"{{}}");//8
                sb.Append(oFila.cells[11].children[0].getAttribute("oValueOriginal") +"{{}}"); //9
                sb.Append(oFila.cells[10].children[0].getAttribute("oValueOriginal") +"#{@"); //10
            }
    	}
    	
    	strQuery = sb.ToString();
	    if (strQuery == ""){
	        ocultarProcesando();
		    mmoff("InfPer","No se han modificado los datos.",200);
		    AccionBotonera("Grabar", "D");
		    AccionBotonera("GrabarReg", "D");
		    AccionBotonera("GrabarSS", "D");
		    bCambios = false;
		    return;
	    }

	    strQuery = strQuery.substring(0, strQuery.length - 2);
        //if (confirm("La tarea: '"+ aFilas[a].cells[0].innerText +"' tiene un esfuerzo pendiente de 0 horas.\n\nPulse \"Aceptar\" si desea marcarla como finalizada.")){
        //    aFilas[a].cells[12].children[0].checked=true;
        //}

	    if (iImputarDiaVacaProyNoVaca == 1 || iMarcarFinalizada !=0)
	    {
	        var sMensaje = "";
	        if (iImputarDiaVacaProyNoVaca == 1)
	            sMensaje = "Durante días en los que has elegido vacaciones, has imputado consumos  en proyectos que no son de vacaciones.<br />Si estás conforme con lo que has hecho, pulsa 'Aceptar' para continuar con la grabación.<br />En caso contrario, pulsa 'Cancelar' y modifica las imputaciones.";
	        else if (iMarcarFinalizada != 0)
	            sMensaje = "La tarea: '" + aFilas[iMarcarFinalizada].cells[0].innerText + "' tiene un esfuerzo pendiente de 0 horas.<br><br>Pulse \"Aceptar\" si desea marcarla como finalizada.";
	        ocultarProcesando();
	        jqConfirm("", sMensaje, "", "", "war",470).then(function (answer) {
	            if (answer) {
	                if (iImputarDiaVacaProyNoVaca == 1) {
	                    if (strAccion == "grabarss")
	                        var js_args = "grabarss@#@";
	                    else
	                        var js_args = strOpcion + "@#@";

	                    js_args += strQuery + "@#@" + sAuxLog;
	                    mostrarProcesando();
	                    RealizarCallBack(js_args, "");
	                }
	                else {
	                    seleccionar(aFilas[a].cells[12].children[0]);
	                }
	                return true;
	            } else return false;
	        });
	    }
	    else
	    {
	        if (strAccion == "grabarss")
	            var js_args = "grabarss@#@";
	        else
	            var js_args = strOpcion + "@#@";

	        js_args += strQuery + "@#@" + sAuxLog;
	        RealizarCallBack(js_args, "");
	        return true;
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

function modificadosACero(){
    try{
        var aControles;
    	for (var i=0; i < $I("tblDatos").rows.length; i++){
    	    if ($I("tblDatos").rows[i].getAttribute("bd") == "U"){
    	        $I("tblDatos").rows[i].setAttribute("bd","");
    	        aControles = $I("tblDatos").rows[i].getElementsByTagName("INPUT");
    	        for (var x=0; x<aControles.length; x++){
    	            aControles[x].setAttribute("bModif","0");
    	            aControles[x].setAttribute("oValueOriginal",aControles[x].value);
    	            if (aControles[x].value=="" && aControles[x].getAttribute("comentario")!="" && aControles[x].getAttribute("comentario")!=undefined){
    	                aControles[x].setAttribute("comentario","");
                        switch (aControles[x].id.substring(3, 5)){
                            case "LU":
                                { 
                                    //if (ie)
                                        aControles[x].className = (aSemLab[0]=="1")? "LabImp":"FesImp"; 
                                    /*else
                                        aControles[x].setAttribute("class",(aSemLab[0]=="1")? "LabImp":"FesImp");
                                    */ 
                                }
                                break;
                            case "MA":                                                                 
                                { 
                                    //if (ie)
                                        aControles[x].className = (aSemLab[1]=="1")? "LabImp":"FesImp"; 
                                    /*else
                                        aControles[x].setAttribute("class",(aSemLab[1]=="1")? "LabImp":"FesImp");
                                    */ 
                                }                                
                                break;
                            case "MI": 
                                { 
                                    //if (ie)
                                        aControles[x].className = (aSemLab[2]=="1")? "LabImp":"FesImp"; 
                                    /*else
                                        aControles[x].setAttribute("class",(aSemLab[2]=="1")? "LabImp":"FesImp"); 
                                    */
                                }                                  
                                break;
                            case "JU": 
                                { 
                                    //if (ie)
                                        aControles[x].className = (aSemLab[3]=="1")? "LabImp":"FesImp"; 
                                    /*else
                                        aControles[x].setAttribute("class",(aSemLab[3]=="1")? "LabImp":"FesImp");
                                    */ 
                                }                                   
                                break;
                            case "VI": 
                                { 
                                    //if (ie)
                                        aControles[x].className = (aSemLab[4]=="1")? "LabImp":"FesImp"; 
                                    /*else
                                        aControles[x].setAttribute("class",(aSemLab[4]=="1")? "LabImp":"FesImp");
                                    */ 
                                }                                  
                                break;
                            case "SA":
                                { 
                                    //if (ie)
                                        aControles[x].className = (aSemLab[5]=="1")? "LabImp":"FesImp"; 
                                    /*else
                                        aControles[x].setAttribute("class",(aSemLab[5]=="1")? "LabImp":"FesImp"); 
                                    */
                                }                               
                                break;
                            case "DO": 
                                { 
                                    //if (ie)
                                        aControles[x].className = (aSemLab[6]=="1")? "LabImp":"FesImp"; 
                                    /*else
                                        aControles[x].setAttribute("class",(aSemLab[6]=="1")? "LabImp":"FesImp"); 
                                    */
                                }   
                                break;
    	                }
    	            }
    	        }
    	    }
    	}
	}catch(e){
		mostrarErrorAplicacion("Error al poner los objetos modificados a cero", e.message);
    }
}

function semanaSiguienteAux(){
	if (getOp($I("tblSiguiente")) != 100) return;
	
	if (bCambios) {
	    jqConfirm("","Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
	        if (answer) {
	            bControlSig = true;
	            //grabarAux("grabar")   
	            if ($I("hdnInputActivo").value != "") $I($I("hdnInputActivo").value).blur();
	            setTimeout("grabarAux('grabar');", 500);
	        }
	        else {
	            bCambios = false;
	            llamarSemanaSiguiente();
	        }
	    });
	}
	else llamarSemanaSiguiente();
}
function llamarSemanaSiguiente()
{
    mostrarProcesando();
    var bSiguiente = comprobarGrabar("semanaSiguiente");
    if (bSiguiente) {
        semanaSiguiente();
    }
}
function semanaAnteriorAux() {
    if (getOp($I("tblAnterior")) != 100) return;

    if (bCambios) {
        jqConfirm("","Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bControlAnt = true;
                //grabarAux("grabar")   
                if ($I("hdnInputActivo").value != "") $I($I("hdnInputActivo").value).blur();
                setTimeout("grabarAux('grabar');", 500);
            }
            else {
                bCambios = false;
                llamarSemanaAnterior();
            }
        });
    }
    else llamarSemanaAnterior();
}
function llamarSemanaAnterior() {
    mostrarProcesando();
    var bAnterior = comprobarGrabar("semanaAnterior");
    if (bAnterior) {
        semanaAnterior();
    }
}
//Funciones para el over de las flechas.
function MM_swapImgRestore() { //v3.0
	var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
	var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
	var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
	if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.0
	var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
	d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
	if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
	for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
	if(!x && document.getElementById) x=document.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
	var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
	if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context)
{
    try{
        actualizarSession();
        var aResul = strResultado.split("@#@");

        if (aResul[1] != "OK"){
            ocultarProcesando();
            var reg = /\\n/g;
		    mostrarError(aResul[2].replace(reg, "\n"));
        }else{
            switch (aResul[0]){
                case "getPT":
                case "getPTDesglose":
                case "getFase":
                case "getActividad":
                case "getFaseActivTarea":
                    insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila+1);
                    $I("tblDatos").rows[iFila].cells[0].children[0].src = strServer +"images/minus.gif";
                    $I("tblDatos").rows[iFila].setAttribute("desplegado","1"); 
                    
                    if (bMostrar) {
                        clearTimeout(nIDTimeMT);
                        nIDTimeMT = setTimeout("MostrarTodo();", 20);
                    }else{
                        ocultarProcesando();
                        if ($I("tblDatos").rows[iFila].getAttribute("tipo")=="F" || $I("tblDatos").rows[iFila].getAttribute("tipo")=="A"){
                            for (var i=0; i<$I("tblDatos").rows.length; i++){
                                if ($I("tblDatos").rows[i].getAttribute("class")!="") $I("tblDatos").rows[i].setAttribute("class","");
                            }
                            iFila = -1;
                        }                        
                        $I("hdnInputActivo").value="";
                        setTimeout("AccionBotonera('ComentarioBot', 'D')",100);
                        setTimeout("AccionBotonera('TareaBot', 'D')",100);    
                        setTimeout("AccionBotonera('Jornada', 'D')",100);
                        setTimeout("AccionBotonera('Semana', 'D')",100); 
    
//                        AccionBotonera("ComentarioBot", "D");
//	                    AccionBotonera("TareaBot", "D");
//                        AccionBotonera("Jornada", "D");
//	                    AccionBotonera("Semana", "D");
//	                    
    //	                AccionBotonera("Bitacora", "D");
    //	                AccionBotonera("BitacoraPT", "D");
                    }
                    break;
                case "grabar":
                    AccionBotonera("Grabar", "D");
    		        AccionBotonera("GrabarSS", "D");
    		        AccionBotonera("GrabarReg", "D");
                    bCambios = false;
                    
	                switch (strAccion){
	                    case "semanaAnterior":
	                    {
	                        semanaAnterior();
    	                    break;
    	                }
	                    case "semanaSiguiente":
	                    {
	                        semanaSiguiente();
                            break;
                        }
    	                case "regresar":
    	                {
    	                    AccionBotonera("regresar", "P");
    	                    break;
    	                }
    	                case "salir":
    	                {
    	                    window.close();
    	                    break;
    	                }
	                }
                    
                    //Se actualiza la fecha de última imputación.
                    strAuxUltimoDia = aResul[2];
	                aUltimoDia = strAuxUltimoDia.split("/");
	                strUltImputac = new Date(aUltimoDia[2],eval(aUltimoDia[1]-1),aUltimoDia[0]);  
    	            
	                controlSemAntSig();
                    modificadosACero();
                    //popupWinespopup_winLoad();
                    mmoff("Suc", "Grabación correcta", 160);
                    if (bSalir) AccionBotonera("regresar", "P");
                    break;

                case "grabarss":
                    bCambios = false;
                    //popupWinespopup_winLoad();
                    mmoff("Suc","Grabación correcta", 160); 
                    semanaSiguiente();
                    break;
                case "grabarreg":
                    bCambios = false;
                    //popupWinespopup_winLoad();
                    mmoff("Suc","Grabación correcta", 160); 
                    AccionBotonera("regresar","P");
                    break;
                case "setResolucion":
                    location.reload(true);
                    break;
                case "getNotasBloqueantes":
                    if (aResul[2] == "1"){
                        mmoff("War","No se permite la creación de nuevas solicitudes de GASVI si existe alguna en estado \"No aprobada\" o \"No aceptada\".\n\nAccede a GASVI (desde la sección Aplicaciones de la Intranet, o desde la opción de menú \"Archivo - Acceder a GASVI\"), corríjelas y vuelve a tramitarlas, o anúlalas directamente, y a continuación crea la nueva solicitud.",470);
                    }else{
                        setTimeout("mostrarGasvi();", 20);
                    }
                    break;                    
                case "FinTareaRecur":     
                    if (aResul[2]!="")
                    {    
                        sUltimoDiaTareaRecur = aResul[2];
	                    aUltimoDiaTareaRecur = sUltimoDiaTareaRecur.split("/");
	                    sUltimoDiaTareaRecur = new Date(aUltimoDiaTareaRecur[2],eval(aUltimoDiaTareaRecur[1]-1),aUltimoDiaTareaRecur[0]);                                                
                    }                    
                    dj();
                    setFin(oChkFinTar);
                    rd(oChkFinTar);
                    break;                    
            }
            ocultarProcesando();

            if (bControlSig){
                semanaSiguiente();
                return;
            } 
            if (bControlAnt){
                semanaAnterior();
                return;
            }
            if (bDetalle){
                bDetalle=false;
                mostrarDetalle(NumTarea, NumPT, ObligaEst);
                return;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la respuesta callback", e.message);
    }        
}

function mostrar(oImg){
    try{
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var sTipo = oFila.getAttribute("tipo");
        var nDesplegado = oFila.getAttribute("desplegado");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);
        
        if (nDesplegado == 0){
		    if (nNivel == "1"){ //PTs
			    var js_args = "getPT@#@";
		        js_args += oFila.getAttribute("PSN") +"@#@";         
		    }else if (nNivel == "2"){ //PTs desglosado
		        var js_args = "getPTDesglose@#@";
		        js_args += oFila.getAttribute("PSN") +"@#@"; 
		        js_args += oFila.getAttribute("PT") +"@#@"; 
		        js_args += "0@#@"; 		
            }else if (sTipo == "F"){ //Fases            
                var js_args = "getFase@#@";
		        js_args += oFila.getAttribute("PSN") +"@#@"; 
		        js_args += oFila.getAttribute("PT") +"@#@"; 	            
            	js_args += oFila.getAttribute("F") +"@#@"; 
            }else if (sTipo == "A"){ //Actividades
                var js_args = "getActividad@#@";
		        js_args += oFila.getAttribute("PSN") +"@#@"; 
		        js_args += oFila.getAttribute("PT") +"@#@"; 	            
            	js_args += oFila.getAttribute("A") +"@#@"; 	 
            }           			        	    
//			}else{ //Fases + Actividades + Tareas 
//			    var js_args = "getFaseActivTarea@#@";
//		        js_args += oFila.PSN +"@#@"; 
//		        js_args += oFila.PT +"@#@"; 
//		    }
		    js_args += sDesde +"@#@";
		    js_args += sHasta +"@#@";
		    js_args += aFechas[0] +"@#@";
		    js_args += aFechas[1] +"@#@";
		    js_args += aFechas[2] +"@#@";
		    js_args += aFechas[3] +"@#@";
		    js_args += aFechas[4] +"@#@";
		    js_args += aFechas[5] +"@#@";
		    js_args += aFechas[6] +"@#@";
            if (bRes1024)
                js_args +="F";
            else
                js_args +="T";

            iFila=nIndexFila;
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
            return;
        }
        
        for (var i=nIndexFila+1; i<$I("tblDatos").rows.length; i++){
            if ($I("tblDatos").rows[i].getAttribute("nivel") > nNivel){
                if (opcion == "O")
                {
                    $I("tblDatos").rows[i].style.display = "none";
                    //if ($I("tblDatos").rows[i].exp < 3)
                        if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                            $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }
                else if ($I("tblDatos").rows[i].getAttribute("nivel")-1 == nNivel) $I("tblDatos").rows[i].style.display = "table-row";
            }else{
                break;
            }
        }
        if ($I("tblDatos").rows[nIndexFila].getAttribute("tipo")=="F" || $I("tblDatos").rows[nIndexFila].getAttribute("tipo")=="A"){
            $I("btnBitacora").src = "../../../images/imgTrans9x9.gif";
            $I("btnBitacora").style.cursor = "default";
            $I("btnBitacora").onclick = null;
            $I("btnBitacora").setAttribute("title","");
            for (var i=0; i<$I("tblDatos").rows.length; i++){
                //if (ie)
                //{
                    if ($I("tblDatos").rows[i].className!="") $I("tblDatos").rows[i].className="";
                /*}
                else
                {
                    if ($I("tblDatos").rows[i].getAttribute("class")!="") $I("tblDatos").rows[i].setAttribute("class","");
                }*/
            }
            iFila = -1;
        }
        if (opcion == "O") oImg.src = "../../../images/plus.gif";
        else oImg.src = "../../../images/minus.gif"; 

        if (bMostrar) MostrarTodo(); 
        else ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }

        if (nMostrar == 0){//Contraer
            for (var i=0; i<$I("tblDatos").rows.length;i++){
                if ($I("tblDatos").rows[i].getAttribute("id") == "-1") continue; //Otros consumos imputados
                if ($I("tblDatos").rows[i].getAttribute("nivel") > 1)
                {
                    if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                    $I("tblDatos").rows[i].style.display = "none";
                }
                else 
                {
                    if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }                             
            }
            ocultarProcesando();
        }else{ //Expandir
            MostrarTodo();
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar=false;
var nIndiceTodo = -1;
var nIDTimeMT;
function MostrarTodo(){
    try
    {
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i=nIndiceAux; i<$I("tblDatos").rows.length;i++){
            if ($I("tblDatos").rows[i].getAttribute("id") == "-1") continue; //Otros consumos imputados
            //if ($I("tblDatos").rows[i].nivel < nNE){ 
            if ($I("tblDatos").rows[i].getAttribute("exp") < nNE || ($I("tblDatos").rows[i].getAttribute("exp") >= 3 && nNE=="3")){ 
                if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                    bMostrar=true;
                    nIndiceTodo = i;
                    mostrar($I("tblDatos").rows[i].cells[0].children[0]);
                    return;
                }
            }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

/* Función para establecer el nivel de expansión */
var nNE = 1;
function setNE(nValor){
    try{
        iFila=-1;
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }

        nNE = nValor;
        mostrarProcesando();
        
        colorearNE();
        setTimeout("setNE2()", 100);

    }catch(e){
	    mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function setNE2(){
    try{
        MostrarOcultar(0);
        if (nNE > 1) MostrarOcultar(1);
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function colorearNE(){
    try{
        switch(nNE){
            case 1:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                break;
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}

var NumTarea = 0;
var NumPT = 0;
var ObligaEst = 0;
function mostrarDetalle(nTarea, nPT, nObligaest){
    try{
        if (bCambios) {
            jqConfirm("", "Para acceder al detalle de la tarea es necesario grabar los cambios realizados.<br><br>¿Deseas grabarlos?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bDetalle = true;
                    NumTarea = nTarea;
                    NumPT = nPT;
                    ObligaEst = nObligaest;
                    grabarAux("grabar");
                } 
            });
        }
        else 
        {
            mostrarProcesando();
	        var oFila = $I("tblDatos").rows[iFila];
            var sDesTarea = (document.all)? oFila.cells[0].innerText : oFila.cells[0].textContent;
        
	        var sPantalla = strServer + "Capa_Presentacion/IAP/Tarea/Default.aspx?t="+ codpar(nTarea) +"&nObligaest="+ codpar(nObligaest) +"&pt="+ codpar(nPT) 
	                                         + "&sDesTarea="+ codpar(sDesTarea)+"&estado="+codpar(oFila.getAttribute("estado")) 
	                                         + "&imputacion="+ codpar(oFila.getAttribute("imp"));
            modalDialog.Show(sPantalla, self, sSize(870,650))
                .then(function(ret) {
	                if (ret != null){
		                aOpciones = ret.split("@#@");
		                //var oFila = $I("$I("tblDatos")").rows[iFila];
		                oFila.cells[10].children[0].value = aOpciones[0];
		                oFila.cells[11].children[0].value = aOpciones[1];
		                if (aOpciones[2] == "1"){
		                    oFila.cells[12].children[0].checked = true;
		                    oFila.cells[12].children[0].setAttribute("sAntes","1");
		                }    
		                else{
		                    oFila.cells[12].children[0].checked = false;
		                    oFila.cells[12].children[0].setAttribute("sAntes","0");
		                }
		                if (aOpciones[3] == "S") 
		                {
		                    //(ie)? oFila.cells[10].children[0].className = "LabImpCom" : oFila.cells[10].children[0].setAttribute("class","LabImpCom");
		                    oFila.cells[10].children[0].className = "LabImpCom";
		                }
		                else 
		                {
		                    //(ie)? oFila.cells[10].children[0].className = "LabImp" : oFila.cells[10].children[0].setAttribute("class","LabImp");
		                    oFila.cells[10].children[0].className = "LabImp";
		                }
		                //if (ie) 
		                oFila.cells[14].innerText = aOpciones[4];
		                setTotales(oFila.getAttribute("PSN"),oFila.getAttribute("PT"),oFila.getAttribute("F"),oFila.getAttribute("A"));
		                //else oFila.cells[14].textContent = aOpciones[4];
	                }
                });
            window.focus();
	        ocultarProcesando();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle de la tarea", e.message);
    }
}
function mostrarPantGasvi(idPSN, idnaturaleza) {
    try {
        if (EsForaneo == 1) {
            alert("Acceso no autorizado");
            return;
        }
        if (bCambios) {
            jqConfirm("", "Para acceder a GASVI es necesario grabar los cambios realizados. <br><br>¿Deseas grabarlos?", "", "", "war", 470).then(function (answer) {
                if (answer) {
                    grabarAux("grabar");
                }
                else
                    mostrarPantGasviContinuar(idPSN, idnaturaleza);
            });
        }
        else mostrarPantGasviContinuar(idPSN, idnaturaleza);

    } catch (e) {
        mostrarErrorAplicacion("Error al acceder a GASVI", e.message);
    }
}
function mostrarPantGasviContinuar(idPSN, idnaturaleza) {
    try {
        mostrarProcesando();
        var sFecha = "";
        if ($I("hdnInputActivo").value != "") {
            var intFila = $I($I("hdnInputActivo").value).parentNode.parentNode.rowIndex;
            var intCelda = $I($I("hdnInputActivo").value).parentNode.cellIndex;

            if (aDiasSemana[intCelda - 3].length == 1) strDia = "0" + aDiasSemana[intCelda - 3];
            else strDia = aDiasSemana[intCelda - 3];

            if (intMes.toString().length == 1) strMes = "0" + intMes;
            else strMes = intMes;
            sFecha = strDia + "/" + strMes + "/" + intAnno;
        }
        var sPantalla = strServer + "Capa_Presentacion/Gasvi/NotaEstandar/Default.aspx?nPSN=" + codpar(idPSN) + "&sF=" + codpar(sFecha) + "&nN=" + codpar(idnaturaleza);
        modalDialog.Show(sPantalla, self, sSize(1020, 620));
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en mostrarPantGasviContinuar", e.message);
    }
}

function mostrarGasviAux(){
    try{
        if (EsForaneo==1){
            alert("Acceso no autorizado");
            return;
        }
        mostrarProcesando();
        var js_args = "getNotasBloqueantes@#@";
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al ir a comprobar las notas bloqueantes.", e.message);
    }
}

function mostrarGasvi(){
    try{
        if (EsForaneo==1){
            alert("Acceso no autorizado");
            return;
        }
	    var bHayFilaSel=false;
	    aFila = FilasDe("tblDatos");
	    for (var i=0;i < aFila.length; i++){
	        if (document.all)
	        {
		        if (aFila[i].className == "FS"){
		            mostrarPantGasvi((aFila[i].imputablegasvi == "1")?aFila[i].PSN:-1, aFila[i].idnaturaleza);
		            bHayFilaSel=true;
		            break;
		        }
		    }
		    else
		    {
		        if (aFila[i].getAttribute("class") == "FS"){
		            mostrarPantGasvi((aFila[i].getAttribute("imputablegasvi") == "1")? aFila[i].getAttribute("PSN"):-1, aFila[i].getAttribute("idnaturaleza"));
		            bHayFilaSel=true;
		            break;
		        }		    
		    }
	    }
	    if (!bHayFilaSel)
	        mostrarPantGasvi(-1, -1);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle de la tarea", e.message);
	}
}
function mostrarTarea(){
    try{
	    aFila = FilasDe("tblDatos");
	    for (var i=0;i < aFila.length; i++){
	        if (document.all)
	        {
		        if (aFila[i].className == "FS"){
		            mostrarDetalle(aFila[i].id, aFila[i].PT, aFila[i].obligaest);
		            break;
		        }
		    }
		    else
		    {
		        if (aFila[i].getAttribute("class") == "FS"){
		            mostrarDetalle(aFila[i].getAttribute("id"), aFila[i].getAttribute("PT"), aFila[i].getAttribute("obligaest"));
		            break;
		        }		        
		    }
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle de la tarea", e.message);
	}
}

function rd(o){
    try{
        //var o = objInput;
        var sOpcion = o.id.substring(3, 5);
//        var oFila = o.parentNode.parentNode;
        var oFila = o;
        while (oFila && oFila.nodeName != 'TR') oFila = oFila.parentNode; 
        if (!oFila) return false; 
                
        if (o.getAttribute("oValue") == undefined) o.setAttribute("oValue","");
        o.setAttribute("bModif","1");
        var bCalcularTotales = false;
        
        switch (sOpcion){
            case "LU":
            case "MA":
            case "MI":
            case "JU":
            case "VI":
            case "SA":
            case "DO":
                bCalcularTotales = true;
	            var intNuevoValor = o.value;
	            var fNuevoValor = parseFloat(dfn(intNuevoValor));
	            //alert("fNuevoValor: "+ fNuevoValor);
	            if (fNuevoValor < 0){
		            mmoff("War","No se pueden imputar inferiores a 0 horas",350);
		            o.value = o.getAttribute("oValue")
		            o.focus();
		            o.select();
		            //if (ie) event.keyCode = 0;
		            return false;
	            }

	            var intAnteriorValor = o.getAttribute("oValue");
	            var fAnteriorValor = parseFloat(dfn(intAnteriorValor));
	            //alert("fAnteriorValor: "+ fAnteriorValor);
	            if (fNuevoValor == fAnteriorValor) return;
	            var intAux;
	            
	            var strLetraDia = o.id.substring(o.id.length-1, o.id.length);
	            var intIndiceDia = aLetra.join("").indexOf(strLetraDia);
	            
	            //Eliminada la restricción de imputar en múltiplos de 0,25 horas
	            //según Victor-Luis 01/10/2008
            	
	            if ((fNuevoValor == 0) && (fAnteriorValor == 0)) return;
	            var fDif = fNuevoValor - fAnteriorValor;

	            //control para proyectos que hay que imputar obligatoriamente a jornada completa o no.
	            //1: sí se puede imputar jornada NO completa.
	            //0: no se puede imputar jornada NO completa (obligatorio a jornada completa)
	            var intControlJNC = oFila.getAttribute("regjnocomp");
	            if (intControlJNC == 0){
		            if (fNuevoValor != parseFloat(dfn(aHorasJornada[intIndiceDia])) && (fNuevoValor != 0)){
			            mmoff("WarPer","Es obligatorio imputar este proyecto a jornada completa",450);
	                    o.value = o.getAttribute("oValue");
	                    o.focus();
	                    o.select();
			            //if (ie) event.keyCode = 0;
			            return false;
		            }
	            }
	            
            	setFin(o.value);
	            var fTotalJornada = parseFloat(dfn($I("txtTotal"+ strLetraDia).value)) + parseFloat(fDif);
            	
	            if (fTotalJornada > 24){
                    o.value = o.getAttribute("oValue");
                    o.focus();
                    o.select();
		            mmoff("WarPer","La imputación realizada el "+ aLiteralesDias[intIndiceDia].toString().toLowerCase() + " superaría el máximo de 24h. Imputación no permitida.",400);
		            setFin(o.value);
		            //if (ie) event.keyCode = 0;
		            return false;
	            }
	            
	            o.setAttribute("oValue",o.value);
	            
	            //actualizar EAT  EP
	            var fEAT = parseFloat(dfn(getCelda(oFila, 13))) + fDif;
	            setCelda(oFila, 13, fEAT.ToString("N"));
	            
	            var fETE = parseFloat(dfn(getCelda(oFila, 10)));
	            setCelda(oFila, 14, (fETE > 0) ? (fETE - fEAT).ToString("N"): " ");
	            
                break;
            case "ET":
                bCalcularTotales = true;
	            //actualizar EAT  EP
	            var fETE = parseFloat(dfn(getCelda(oFila, 10)));
	            var fEAT = parseFloat(dfn(getCelda(oFila, 13)));
	            setCelda(oFila, 14, (fETE > 0) ? (fETE - fEAT).ToString("N"): " ");
	            oFila.cells[12].children[0].checked = false;
                break;
            case "FI":
            case "FF":
                bCalcularTotales = true;                
                break;
        }
    
        mfa(oFila, "U");
        activarGrabar();
        //Calcular Totales.
        //var oF1 = new Date();  
        if (bCalcularTotales)
            //var sw = setTotales(oFila.PSN);
            var sw = setTotales(oFila.getAttribute("PSN"),oFila.getAttribute("PT"),oFila.getAttribute("F"),oFila.getAttribute("A"));
            
        //var oF2 = new Date(); 
        //alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");

        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al recalcular los datos", e.message);
    }
}
function setTotales(nProySubNodo,nProyTec,nFase,nActi){
    try{
        var nPSN = 0, nPT = 0, nF = 0, nA = 0;
        var nTotLU_Tot = 0, nTotLU_PSN = 0, nTotLU_PT = 0, nTotLU_F = 0, nTotLU_A = 0;
        var nTotMA_Tot = 0, nTotMA_PSN = 0, nTotMA_PT = 0, nTotMA_F = 0, nTotMA_A = 0;
        var nTotMI_Tot = 0, nTotMI_PSN = 0, nTotMI_PT = 0, nTotMI_F = 0, nTotMI_A = 0;
        var nTotJU_Tot = 0, nTotJU_PSN = 0, nTotJU_PT = 0, nTotJU_F = 0, nTotJU_A = 0;
        var nTotVI_Tot = 0, nTotVI_PSN = 0, nTotVI_PT = 0, nTotVI_F = 0, nTotVI_A = 0;
        var nTotSA_Tot = 0, nTotSA_PSN = 0, nTotSA_PT = 0, nTotSA_F = 0, nTotSA_A = 0;
        var nTotDO_Tot = 0, nTotDO_PSN = 0, nTotDO_PT = 0, nTotDO_F = 0, nTotDO_A = 0;

        var nTotET_PSN = 0, nTotET_PT = 0, nTotET_F = 0, nTotET_A = 0;
        var nTotEA_PSN = 0, nTotEA_PT = 0, nTotEA_F = 0, nTotEA_A = 0;
        var dFF_PSN = "", dFF_PT = "", dFF_F = "", dFF_A = "";
        
        var nLU=0, nMA=0, nMI=0, nJU=0, nVI=0, nSA=0, nDO=0, nET=0, nEA=0;
        var dFF = "";
        var fETE = 0;
        var fEAT = 0;
		var oFila;
        
        for (var i=$I("tblDatos").rows.length-1; i>=0; i--){
            //Probar a Crear y pasar oFila en lugar de $I("tblDatos").rows[i], para comprobar rendimiento.
            //Efectivamente, es más rápido.
			oFila = $I("tblDatos").rows[i];

            if (isNaN(parseFloat(dfn(getCelda(oFila, 3))))) setCelda(oFila, 3, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 4))))) setCelda(oFila, 4, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 5))))) setCelda(oFila, 5, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 6))))) setCelda(oFila, 6, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 7))))) setCelda(oFila, 7, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 8))))) setCelda(oFila, 8, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 9))))) setCelda(oFila, 9, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 10))))) setCelda(oFila, 10, "");
            if (isNaN(parseFloat(dfn(getCelda(oFila, 13))))) setCelda(oFila, 13, "");

			if (oFila.getAttribute("id") == -1){//consumos "desasignados"
                nTotLU_Tot += parseFloat(dfn(getCelda(oFila, 3)));
                nTotMA_Tot += parseFloat(dfn(getCelda(oFila, 4)));
                nTotMI_Tot += parseFloat(dfn(getCelda(oFila, 5)));
                nTotJU_Tot += parseFloat(dfn(getCelda(oFila, 6)));
                nTotVI_Tot += parseFloat(dfn(getCelda(oFila, 7)));
                nTotSA_Tot += parseFloat(dfn(getCelda(oFila, 8)));
                nTotDO_Tot += parseFloat(dfn(getCelda(oFila, 9)));
			    continue;
			}
			
            if (oFila.getAttribute("tipo") == "PSN"){
				if (oFila.getAttribute("PSN") == nProySubNodo){
                    setCelda(oFila, 3, (nTotLU_PSN==0)?" ":nTotLU_PSN.ToString("N"));
                    setCelda(oFila, 4, (nTotMA_PSN==0)?" ":nTotMA_PSN.ToString("N"));
                    setCelda(oFila, 5, (nTotMI_PSN==0)?" ":nTotMI_PSN.ToString("N"));
                    setCelda(oFila, 6, (nTotJU_PSN==0)?" ":nTotJU_PSN.ToString("N"));
                    setCelda(oFila, 7, (nTotVI_PSN==0)?" ":nTotVI_PSN.ToString("N"));
                    setCelda(oFila, 8, (nTotSA_PSN==0)?" ":nTotSA_PSN.ToString("N"));
                    setCelda(oFila, 9, (nTotDO_PSN==0)?" ":nTotDO_PSN.ToString("N"));
                    setCelda(oFila, 10, (nTotET_PSN==0)?" ":nTotET_PSN.ToString("N"));
                    setCelda(oFila, 13, (nTotEA_PSN==0)?" ":nTotEA_PSN.ToString("N"));
                    setCelda(oFila, 11, (dFF_PSN=="")? " ":dFF_PSN);                    
                }   
                
                nTotLU_Tot += parseFloat(dfn(getCelda(oFila, 3)));
                nTotMA_Tot += parseFloat(dfn(getCelda(oFila, 4)));
                nTotMI_Tot += parseFloat(dfn(getCelda(oFila, 5)));
                nTotJU_Tot += parseFloat(dfn(getCelda(oFila, 6)));
                nTotVI_Tot += parseFloat(dfn(getCelda(oFila, 7)));
                nTotSA_Tot += parseFloat(dfn(getCelda(oFila, 8)));
                nTotDO_Tot += parseFloat(dfn(getCelda(oFila, 9)));
                
                nTotLU_PSN = 0;
                nTotMA_PSN = 0;
                nTotMI_PSN = 0;
                nTotJU_PSN = 0;
                nTotVI_PSN = 0;
                nTotSA_PSN = 0;
                nTotDO_PSN = 0;
                nTotET_PSN = 0;
                nTotEA_PSN = 0;
                dFF_PSN = "";   
                dFF_PT = ""; 
                dFF_F = ""; 				
                dFF_A = "";                                                                 
                continue;
            }		

			if (oFila.getAttribute("tipo") == "PT")
			{				
                if (oFila.getAttribute("PT") == nProyTec){               
                    setCelda(oFila, 3, (nTotLU_PT==0)?" ":nTotLU_PT.ToString("N"));
                    setCelda(oFila, 4, (nTotMA_PT==0)?" ":nTotMA_PT.ToString("N"));
                    setCelda(oFila, 5, (nTotMI_PT==0)?" ":nTotMI_PT.ToString("N"));
                    setCelda(oFila, 6, (nTotJU_PT==0)?" ":nTotJU_PT.ToString("N"));
                    setCelda(oFila, 7, (nTotVI_PT==0)?" ":nTotVI_PT.ToString("N"));
                    setCelda(oFila, 8, (nTotSA_PT==0)?" ":nTotSA_PT.ToString("N"));
                    setCelda(oFila, 9, (nTotDO_PT==0)?" ":nTotDO_PT.ToString("N"));
                    setCelda(oFila, 10, (nTotET_PT==0)?" ":nTotET_PT.ToString("N"));
                    setCelda(oFila, 13, (nTotEA_PT==0)?" ":nTotEA_PT.ToString("N"));
                    setCelda(oFila, 11, (dFF_PT=="")? " ":dFF_PT);	

				    nTotLU_PSN += nTotLU_PT;
				    nTotMA_PSN += nTotMA_PT;
				    nTotMI_PSN += nTotMI_PT;
				    nTotJU_PSN += nTotJU_PT;
				    nTotVI_PSN += nTotVI_PT;
				    nTotSA_PSN += nTotSA_PT;
				    nTotDO_PSN += nTotDO_PT;
                    nTotET_PSN += nTotET_PT;
                    nTotEA_PSN += nTotEA_PT;                
                }else{
					nTotLU_PSN += parseFloat(dfn(getCelda(oFila, 3)));
					nTotMA_PSN += parseFloat(dfn(getCelda(oFila, 4)));
					nTotMI_PSN += parseFloat(dfn(getCelda(oFila, 5)));
					nTotJU_PSN += parseFloat(dfn(getCelda(oFila, 6)));
					nTotVI_PSN += parseFloat(dfn(getCelda(oFila, 7)));
					nTotSA_PSN += parseFloat(dfn(getCelda(oFila, 8)));
					nTotDO_PSN += parseFloat(dfn(getCelda(oFila, 9)));
                    nTotET_PSN += parseFloat(dfn(getCelda(oFila, 10)));
                    nTotEA_PSN += parseFloat(dfn(getCelda(oFila, 13)));	
                    dFF_PT = getCelda(oFila, 11);		             
                }
				
				if (DiffDiasFechas(dFF_PSN, dFF_PT) > 0) dFF_PSN = dFF_PT;

                nTotLU_PT = 0;
                nTotMA_PT = 0;
                nTotMI_PT = 0;
                nTotJU_PT = 0;
                nTotVI_PT = 0;
                nTotSA_PT = 0;
                nTotDO_PT = 0;
                nTotET_PT = 0;
                nTotEA_PT = 0;
                dFF_PT = ""; 
                dFF_F = ""; 				
                dFF_A = ""; 				
				continue
            }				
			if (oFila.getAttribute("tipo") == "F"){
                if (oFila.getAttribute("F") == nFase){
                    setCelda(oFila, 3, (nTotLU_F==0)?" ":nTotLU_F.ToString("N"));
                    setCelda(oFila, 4, (nTotMA_F==0)?" ":nTotMA_F.ToString("N"));
                    setCelda(oFila, 5, (nTotMI_F==0)?" ":nTotMI_F.ToString("N"));
                    setCelda(oFila, 6, (nTotJU_F==0)?" ":nTotJU_F.ToString("N"));
                    setCelda(oFila, 7, (nTotVI_F==0)?" ":nTotVI_F.ToString("N"));
                    setCelda(oFila, 8, (nTotSA_F==0)?" ":nTotSA_F.ToString("N"));
                    setCelda(oFila, 9, (nTotDO_F==0)?" ":nTotDO_F.ToString("N"));
                    setCelda(oFila, 10, (nTotET_F==0)?" ":nTotET_F.ToString("N"));
                    setCelda(oFila, 13, (nTotEA_F==0)?" ":nTotEA_F.ToString("N"));
                    setCelda(oFila, 11, (dFF_F=="")? " ":dFF_F); 

				    nTotLU_PT += nTotLU_F;
				    nTotMA_PT += nTotMA_F;
				    nTotMI_PT += nTotMI_F;
				    nTotJU_PT += nTotJU_F;
				    nTotVI_PT += nTotVI_F;
				    nTotSA_PT += nTotSA_F;
				    nTotDO_PT += nTotDO_F;
                    nTotET_PT += nTotET_F;
                    nTotEA_PT += nTotEA_F;                                                   	                                                                    				
                }else{
					nTotLU_PT += parseFloat(dfn(getCelda(oFila, 3)));
					nTotMA_PT += parseFloat(dfn(getCelda(oFila, 4)));
					nTotMI_PT += parseFloat(dfn(getCelda(oFila, 5)));
					nTotJU_PT += parseFloat(dfn(getCelda(oFila, 6)));
					nTotVI_PT += parseFloat(dfn(getCelda(oFila, 7)));
					nTotSA_PT += parseFloat(dfn(getCelda(oFila, 8)));
					nTotDO_PT += parseFloat(dfn(getCelda(oFila, 9)));
                    nTotET_PT += parseFloat(dfn(getCelda(oFila, 10)));
                    nTotEA_PT += parseFloat(dfn(getCelda(oFila, 13)));
                    dFF_F = getCelda(oFila, 11);			             
                }

                if (DiffDiasFechas(dFF_PT, dFF_F) > 0) dFF_PT = dFF_F;
                
                nTotLU_F = 0;
                nTotMA_F = 0;
                nTotMI_F = 0;
                nTotJU_F = 0;
                nTotVI_F = 0;
                nTotSA_F = 0;
                nTotDO_F = 0;
                nTotET_F = 0;
                nTotEA_F = 0;
                dFF_F = ""; 				                                                               
				continue
            }	

			if (oFila.getAttribute("tipo") == "A"){				                				
                if (oFila.getAttribute("A") == nActi){
                    setCelda(oFila, 3, (nTotLU_A==0)?" ":nTotLU_A.ToString("N"));
                    setCelda(oFila, 4, (nTotMA_A==0)?" ":nTotMA_A.ToString("N"));
                    setCelda(oFila, 5, (nTotMI_A==0)?" ":nTotMI_A.ToString("N"));
                    setCelda(oFila, 6, (nTotJU_A==0)?" ":nTotJU_A.ToString("N"));
                    setCelda(oFila, 7, (nTotVI_A==0)?" ":nTotVI_A.ToString("N"));
                    setCelda(oFila, 8, (nTotSA_A==0)?" ":nTotSA_A.ToString("N"));
                    setCelda(oFila, 9, (nTotDO_A==0)?" ":nTotDO_A.ToString("N"));
                    setCelda(oFila, 10, (nTotET_A==0)?" ":nTotET_A.ToString("N"));
                    setCelda(oFila, 13, (nTotEA_A==0)?" ":nTotEA_A.ToString("N"));
                    setCelda(oFila, 11, (dFF_A=="")? " ":dFF_A);	

                    if (oFila.getAttribute("F")!='0'){
                        nTotLU_F += nTotLU_A;
                        nTotMA_F += nTotMA_A;
                        nTotMI_F += nTotMI_A;
                        nTotJU_F += nTotJU_A;
                        nTotVI_F += nTotVI_A;
                        nTotSA_F += nTotSA_A;
                        nTotDO_F += nTotDO_A;
                        nTotET_F += nTotET_A;
                        nTotEA_F += nTotEA_A;
                        if (DiffDiasFechas(dFF_F, dFF_A) > 0) dFF_F = dFF_A;
                    }else{
				        nTotLU_PT += nTotLU_A;
				        nTotMA_PT += nTotMA_A;
				        nTotMI_PT += nTotMI_A;
				        nTotJU_PT += nTotJU_A;
				        nTotVI_PT += nTotVI_A;
				        nTotSA_PT += nTotSA_A;
				        nTotDO_PT += nTotDO_A;
                        nTotET_PT += nTotET_A;
                        nTotEA_PT += nTotEA_A;  
                        if (DiffDiasFechas(dFF_PT, dFF_A) > 0) dFF_PT = dFF_A;                  
                    }
                }else{
                    if (oFila.getAttribute("F")!='0'){
					    nTotLU_F += parseFloat(dfn(getCelda(oFila, 3)));
					    nTotMA_F += parseFloat(dfn(getCelda(oFila, 4)));
					    nTotMI_F += parseFloat(dfn(getCelda(oFila, 5)));
					    nTotJU_F += parseFloat(dfn(getCelda(oFila, 6)));
					    nTotVI_F += parseFloat(dfn(getCelda(oFila, 7)));
					    nTotSA_F += parseFloat(dfn(getCelda(oFila, 8)));
					    nTotDO_F += parseFloat(dfn(getCelda(oFila, 9)));
                        nTotET_F += parseFloat(dfn(getCelda(oFila, 10)));
                        nTotEA_F += parseFloat(dfn(getCelda(oFila, 13)));
                        dFF_A = getCelda(oFila, 11);
                        if (DiffDiasFechas(dFF_F, dFF_A) > 0) dFF_F = dFF_A;	
                    }else{
					    nTotLU_PT += parseFloat(dfn(getCelda(oFila, 3)));
					    nTotMA_PT += parseFloat(dfn(getCelda(oFila, 4)));
					    nTotMI_PT += parseFloat(dfn(getCelda(oFila, 5)));
					    nTotJU_PT += parseFloat(dfn(getCelda(oFila, 6)));
					    nTotVI_PT += parseFloat(dfn(getCelda(oFila, 7)));
					    nTotSA_PT += parseFloat(dfn(getCelda(oFila, 8)));
					    nTotDO_PT += parseFloat(dfn(getCelda(oFila, 9)));
                        nTotET_PT += parseFloat(dfn(getCelda(oFila, 10)));
                        nTotEA_PT += parseFloat(dfn(getCelda(oFila, 13)));
                        dFF_A = getCelda(oFila, 11);
                        if (DiffDiasFechas(dFF_PT, dFF_A) > 0) dFF_PT = dFF_A;                  
                    }                        		             
                }
                                
                nTotLU_A = 0;
                nTotMA_A = 0;
                nTotMI_A = 0;
                nTotJU_A = 0;
                nTotVI_A = 0;
                nTotSA_A = 0;
                nTotDO_A = 0;
                nTotET_A = 0;
                nTotEA_A = 0;
                dFF_A = "";  
                continue; 
            }   					
			
            nLU = parseFloat(dfn(getCelda(oFila, 3)));
            nMA = parseFloat(dfn(getCelda(oFila, 4)));
            nMI = parseFloat(dfn(getCelda(oFila, 5)));
            nJU = parseFloat(dfn(getCelda(oFila, 6)));
            nVI = parseFloat(dfn(getCelda(oFila, 7)));
            nSA = parseFloat(dfn(getCelda(oFila, 8)));
            nDO = parseFloat(dfn(getCelda(oFila, 9)));
            nET = parseFloat(dfn(getCelda(oFila, 10)));
            nEA = parseFloat(dfn(getCelda(oFila, 13)));
            
            dFF = getCelda(oFila, 11);

            if (oFila.getAttribute("A")!='0')
            {         
                nTotLU_A += nLU;
                nTotMA_A += nMA;
                nTotMI_A += nMI;
                nTotJU_A += nJU;
                nTotVI_A += nVI;
                nTotSA_A += nSA;
                nTotDO_A += nDO;
                nTotET_A += nET;
                nTotEA_A += nEA;
                if (DiffDiasFechas(dFF_A, dFF) > 0) dFF_A = dFF;
            }else{
                nTotLU_PT += nLU;
                nTotMA_PT += nMA;
                nTotMI_PT += nMI;
                nTotJU_PT += nJU;
                nTotVI_PT += nVI;
                nTotSA_PT += nSA;
                nTotDO_PT += nDO;
                nTotET_PT += nET;
                nTotEA_PT += nEA;   
                if (DiffDiasFechas(dFF_PT, dFF) > 0) dFF_PT = dFF;         
            }           
            //actualizar EP
            fETE = parseFloat(dfn(getCelda(oFila, 10)));
            fEAT = parseFloat(dfn(getCelda(oFila, 13)));
            setCelda(oFila, 14, (fETE > 0) ? (fETE - fEAT).ToString("N"): " ");
        }
        //Totales
        
		$I("txtTotalL").value = (aFechas[0]!="")? nTotLU_Tot.ToString("N"):"";
		$I("txtTotalM").value = (aFechas[1]!="")? nTotMA_Tot.ToString("N"):"";
		$I("txtTotalX").value = (aFechas[2]!="")? nTotMI_Tot.ToString("N"):"";
		$I("txtTotalJ").value = (aFechas[3]!="")? nTotJU_Tot.ToString("N"):"";
		$I("txtTotalV").value = (aFechas[4]!="")? nTotVI_Tot.ToString("N"):"";
		$I("txtTotalS").value = (aFechas[5]!="")? nTotSA_Tot.ToString("N"):"";
		$I("txtTotalD").value = (aFechas[6]!="")? nTotDO_Tot.ToString("N"):"";				
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales.");
    }
}

function setFin(objChk){
    try{
        if (objChk.checked){
            var oFila = objChk.parentNode.parentNode;

            oFila.cells[10].children[0].value = oFila.cells[13].innerText;
            oFila.cells[11].children[0].value = "";
            oFila.cells[14].innerText = "";
            
	        var strUltFechaSemana = "";  // Identificamos la fecha del último día de la semana con consumo

            for (var i=9; i>=3; i--){
                //if (oFila.cells[i].children[0].value == "" || oFila.cells[i].children[0].value == "0,00") continue;                
                if (getCelda(oFila, i) == "" || getCelda(oFila, i) == "0,00") 
                    continue;                
                strUltFechaSemana = new Date(aFechas[eval(i-3)].substring(6,10),aFechas[eval(i-3)].substring(3,5)-1,aFechas[eval(i-3)].substring(0,2));                    
                break;
            }

	        if (sUltimoDiaTareaRecur=="") sUltimoDiaTareaRecur = strUltFechaSemana;
	        if (strUltFechaSemana!="")
	        {
	            if (strUltFechaSemana>sUltimoDiaTareaRecur) oFila.cells[11].children[0].value = strUltFechaSemana.ToShortDateString();
                else oFila.cells[11].children[0].value= sUltimoDiaTareaRecur.ToShortDateString();
            }                        
        }

	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los datos por finalizar la labor en la tarea.", e.message);
	}
}
function setResolucionPantalla(){
    try{
        mostrarProcesando();
        var js_args = "setResolucion@#@";
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}

function setResolucion1024(){
    try{
        //$I("flsCriterios").style.width = 980;
//        $I("tbl1").style.width = "1000px";
//        $I("tbl2").style.width = "1000px";

        $I("tblTitulo").style.width = "980px";
        oColgroup = $I("tblTitulo").children[0];
        oColgroup.children[0].style.width = "303px"; 
        oColgroup.children[1].style.width = "68px"; 
        oColgroup.children[2].style.width = "62px"; 
        
        //$I("divTablaTitulo").style.width = 980;
        $I("divCatalogo").style.width = "996px"; 
        $I("divCatalogo").style.height = "460px";
        $I("divCatalogo").children[0].style.width = "980px";

        oColgroup = $I("divCatalogo").children[0].children[0].children[0];
        oColgroup.children[0].style.width = "296px";
        oColgroup.children[1].style.width = "66px";
        oColgroup.children[2].style.width = "61px";
        
        $I("tblResultado").style.width = "980px";
        oColgroup2 = $I("tblResultado").children[0];
        oColgroup2.children[0].style.width = "306px"; 
        oColgroup2.children[1].style.width = "71px"; 
        oColgroup2.children[2].style.width = "62px"; 

    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}
function mostrarBitacora(){
    try{
        if (iFila==-1)
            mmoff("Inf","Debes seleccionar una fila",180);
        else{
            var oFila = $I("tblDatos").rows[iFila];
            switch(oFila.getAttribute("tipo")){
                case "PSN":
                    mostrarBitacoraPE(oFila);
                    break;
                case "PT":
                    mostrarBitacoraPT(oFila);
                    break;
                case "T":
                    mostrarBitacoraT(oFila);
                    break;
//                default:
//                    alert("El tipo de elemneto seleccionado no dispone de bitácora");
//                    break;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la bitácora", e.message);
	}
}
function mostrarBitacoraPE(oFila){
    try{
        mostrarProcesando();
        //var oFila = $I("tblDatos").rows[iFila];
        
        var sParam ="?sEstado="+ codpar(oFila.getAttribute("estado"));
        sParam += "&sCodProy="+ codpar(oFila.getAttribute("idPE"));
        sParam += "&sNomProy="+ codpar(Utilidades.unescape(oFila.getAttribute("desPE")));
        sParam += "&sT305IdProy="+ codpar(oFila.getAttribute("id"));
        sParam += "&sOrigen="+codpar("IAP");
        sParam += "&sAccesoBitacoraPE="+ codpar(oFila.getAttribute("sAccesoBitacoraPE"));
        
        var sPantalla= strServer + "Capa_Presentacion/PSP/Proyecto/Bitacora/Default.aspx"+ sParam;
        modalDialog.Show(sPantalla, self, sSize(1016,663));
        window.focus();	            
        ocultarProcesando();
        
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la bitácora de proyecto económico", e.message);
	}
}

function mostrarBitacoraPT(oFila){
    try{
        mostrarProcesando();
        //var oFila = $I("tblDatos").rows[iFila];
        
//        var sParam ="?sEstado="+ codpar(oFila.getAttribute("estado"));
//        sParam += "&sCodProy="+ codpar(oFila.getAttribute("idPE"));
//        sParam += "&sNomProy="+ codpar(oFila.getAttribute("desPE"));
//        sParam += "&sCodPT="+ codpar(oFila.getAttribute("id"));
//        sParam += "&sNomPT="+ codpar(oFila.getAttribute("desPT"));
//        sParam += "&sOrigen="+codpar("IAP");
//        sParam += "&sAccBitacoraPT="+ codpar(oFila.getAttribute("sAccesoBitacoraPT"));
        var sParam ="?e="+ codpar(oFila.getAttribute("estado"));
        sParam += "&p="+ codpar(oFila.getAttribute("idPE"));
        sParam += "&n="+ codpar(oFila.getAttribute("desPE"));
        sParam += "&pt="+ codpar(oFila.getAttribute("id"));
        sParam += "&npt="+ codpar(Utilidades.unescape(oFila.getAttribute("desPT")));
        sParam += "&o="+codpar("IAP");
        sParam += "&b="+ codpar(oFila.getAttribute("sAccesoBitacoraPT"));
        
        var sPantalla= strServer + "Capa_Presentacion/PSP/ProyTec/Bitacora/Default.aspx"+ sParam;
        modalDialog.Show(sPantalla, self, sSize(1020,680));
        window.focus();	                    
        ocultarProcesando();
        
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la bitácora de proyecto técnico", e.message);
	}
}
function mostrarBitacoraT(oFila){
    try{
        mostrarProcesando();
        //var oFila = $I("tblDatos").rows[iFila];
        
//        var sParam ="?sCodT="+ codpar(oFila.getAttribute("id"));
//        sParam += "&sOrigen="+codpar("IAP");
//        sParam += "&sAccBitacoraT="+ codpar(oFila.getAttribute("sAccesoBitacoraT"));
        var sParam ="?t="+ codpar(oFila.getAttribute("id"));
        //sParam += "&sOrigen="+codpar("IAP");
        sParam += "&a="+ codpar(oFila.getAttribute("sAccesoBitacoraT"));
        
        var sPantalla= strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/Default.aspx"+ sParam;
        modalDialog.Show(sPantalla, self, sSize(1020,675));
        window.focus();	                            
        ocultarProcesando();
        
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la bitácora de proyecto técnico", e.message);
	}
}
function eventosPSN(oFila) {
    if (oFila.onclick == null) oFila.onclick = function() { ms(oFila); hbitPE(oFila) };
}
function eventosPT(oFila) {
    if (oFila.onclick == null) oFila.onclick = function() { ms(oFila); hbitPT(oFila) };
}
function eventosEST(oFila) {
    if (oFila.onclick == null) oFila.onclick = function() { ms(oFila); hbt(oFila) };
}