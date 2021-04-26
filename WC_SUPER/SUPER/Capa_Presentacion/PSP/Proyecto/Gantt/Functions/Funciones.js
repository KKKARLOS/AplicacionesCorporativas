var oDivTitulo;
var bSalir=false;
var bHayCambios=false;
//Vbles para acceder al detalle de una línea cuando la pantalla está pdte de grabar
var bDetalle=false;
var iFDet=-1;
var sAccesibilidadDetalle="N";
//Para columnas móviles
var oDivBodyFijo = null;
var oDivTituloMovil = null;
var oDivBodyMovil = null;
var oDivPieMovil = null;
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x  

function init(){
    try{
	    if (screen.width < 1280 && !bRes1024){
	        var objBODY = document.getElementsByTagName("body")[0];
		    objBODY.style.overflow = "scroll";
		    objBODY = null;
	    }
        setOp($I("btnGrabar"),30);
        setOp($I("btnGrabarSalir"),30);

        if (!mostrarErrores()) return; 
        
        if (bRes1024) setResolucion1024();
        //Para columnas móviles
        oDivTituloMovil = $I("divTituloMovil");
        oDivBodyMovil = $I("divBodyMovil");
        oDivBodyFijo = $I("divCatalogo");
        oDivPieMovil = $I("divPieMovil");
        $I("tblPieMovil").style.width = $I("tblBodyMovil").style.width;
        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divCatalogo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divCatalogo").addEventListener(mousewheelevt, setScrollFijo, false) 
        
        
        nNE = 1;colorearNE();
        
        $I("divPry").innerHTML = "<INPUT class=txtM id=txtNomProy name=txtNomProy Text='' style='WIDTH:580px' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Responsable:</label>" + $I("hdnResp").value + "<br><label style='width:70px'>"+$I("hdnNodo").value+":</label>"+$I("hdnDenNodo").value+"<br><label style='width:70px'>Cliente:</label>" + $I("hdnCliente").value + "] hideselects=[off]\" />";
        $I("txtNomProy").value = $I("hdnDenProy").value;
        
        //La siguiente línea es necesaria para la exportación a Excel.
        $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;
        $I("divCatalogo").style.visibility = "visible";
        oDivTitulo = $I("divTituloMovil");
        aFila = FilasDe("tblDatos");
        
        if (strHCM != ""){
            var aHitos = strHCM.split("##");
            for (var i=0;i<aHitos.length;i++){
                var aAux = aHitos[i].split("//");
                oHCM_I(aAux[0], aAux[1], aAux[2]);
            }
        }
        
        setExcelImg("imgExcel", "divCatalogo");
        $I("imgExcel_exp").style.left = "1235px";
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload(){
    if (!bSalir) salir();
}
function activarGrabar(){
    try{
        if ($I("hdnAcceso").value!="R"){
            setOp($I("btnGrabar"),100);
            setOp($I("btnGrabarSalir"),100);
            bCambios = true;
            bHayCambios=true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function desActivarGrabar(){
    try{
        setOp($I("btnGrabar"),30);
        setOp($I("btnGrabarSalir"),30);
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
	}
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function aceptar(){
    var strRetorno;
    bSalir = false;
    if ($I("hdnAcceso").value=="R"){
        strRetorno ="F@#@";
    }
    else{
        if (bCambios || bHayCambios)strRetorno ="T@#@";
        else strRetorno ="F@#@";
    }
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function salir() {
    bSalir = false;
    bSaliendo = true;

    if ($I("hdnAcceso").value != "R") {
        if (bCambios && intSession > 0) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    grabar();
                }
                bCambios = false;
                salirCerrarVentana();
            });
        } else salirCerrarVentana();
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
    var strRetorno = "F";
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T";
    }

    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function RespuestaCallBack(strResultado, context){ 
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
            mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                var strImg = aFila[0].cells[0].children[1].src;
                var nPos = strImg.lastIndexOf("/");
                strImgT = strImg.substring(0, nPos+1) + "imgTareaOff.gif";
                strImgH = strImg.substring(0, nPos+1) + "imgHitoOff.gif";
                
                for (var i=0; i<aFila.length; i++){
                    if (aFila[i].getAttribute("modif") == "1"){
                        aFila[i].setAttribute("modif","0");
                        if (aFila[i].getAttribute("sTipo") == "T") aFila[i].cells[0].children[1].src = strImgT;
                        else aFila[i].cells[0].children[1].src = strImgH;
                    }
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bVista){
                    bVista = false;
                    setTimeout("modificarVista($I('cboVista').value);", 20);
                }
                if (bCerradas){
                    bCerradas = false;
                    setTimeout("mostrarCerradas();", 20);
                }
                if (bDetalle){
                    bDetalle=false;
                    setTimeout("mostrarDetalle('"+sAccesibilidadDetalle+"',"+iFDet+")",20);
                    sAccesibilidadDetalle="N";
                }
                if (bSalir) salir();
                break;
            case "getPE":
                $I("divTituloMovil").innerHTML = aResul[6];
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divBodyMovil").innerHTML = aResul[3];
                sIniProy = aResul[4];
                sFinProy = aResul[5];

                oDivPieMovil.scrollLeft = 0;
                $I("divTituloFijo").scrollLeft = 0;
                $I("divTituloMovil").scrollLeft = 0;
                $I("divCatalogo").children[0].scrollLeft = 0;
                $I("divBodyMovil").scrollLeft = 0;
                //alert(sIniProy +"  --  "+ sFinProy);
                aFila = FilasDe("tblDatos");
                $I("tblPieMovil").style.width = $I("tblBodyMovil").style.width;
                break;
                
            case "getPT":
            case "getFase":
            case "getActiv":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila + 1);
                insertarFilasEnTablaDOM("tblBodyMovil", aResul[3], iFila + 1);
                sIniProy = aResul[4];
                sFinProy = aResul[5];
                //alert(sIniProy +"  --  "+ sFinProy);
                $I("tblDatos").rows[iFila].cells[0].getElementsByTagName("IMG")[0].src = strServer + "images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado", "1");
                if (bMostrar) setTimeout("MostrarTodo();", 10);
                else {
                    aFila = FilasDe("tblDatos");
                    recolorearTabla("tblDatos");
                    ocultarProcesando();
                }
                break;
        }
        ocultarProcesando();
    }
}

function grabar(){
    var js_args="";
    try{
        if ($I("hdnAcceso").value=="R")return;
        if (getOp($I("btnGrabar")) != 100) return;

        var js_args = "grabar@#@";
        var sDatos = "";
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("sTipo") == "T" && aFila[i].getAttribute("modif") == "1") {
                sDatos += "T##";
                sDatos += aFila[i].getAttribute("T") +"##";
                sDatos += aFila[i].cells[1].children[0].value +"##";
                sDatos += aFila[i].cells[2].children[0].value +"##"; 
                sDatos += aFila[i].cells[3].children[0].value +"##"; 
                sDatos += aFila[i].cells[4].children[0].value +"///"; 
                sw = 1;
            //}else if (aFila[i].sTipo == "HT" && aFila[i].modif == "1"){
            } else if (aFila[i].getAttribute("sTipo") == "HF" && aFila[i].getAttribute("modif") == "1") {
                sDatos += "H##";
                sDatos += aFila[i].getAttribute("H") +"##";
                sDatos += aFila[i].cells[4].children[0].value +"##";
                sDatos += Utilidades.escape(aFila[i].cells[0].innerText) +"##";
                sDatos += aFila[i].getAttribute("orden") + "##"; 
                sDatos += "///"; 
                sw = 1;
            }
        }
        if (sw == 1) sDatos = sDatos.substring(0, sDatos.length-3);

        js_args+=sDatos;

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

var bVista = false;
function modificarVista(sValue){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados.<br />¿Deseas grabar los datos antes de modificar la vista?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    bVista = true;
                    grabar();
                    return;
                }
                bCambios = false;
                bVista = false;
                LLamarModificarVista(sValue);
            });
        }
        else LLamarModificarVista(sValue);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al solicitar la modificación de la vista-1.", e.message);
    }
}
function LLamarModificarVista(sValue){
    var js_args="";
    try{
        nNE = 1;
        colorearNE();
        js_args = "getPE@#@";
        //js_args+= $I("txtCodProy").value + "@#@";
        js_args += $I("hdnT305IdProy").value +"@#@";
        js_args+= $I("txtEstado").value + "@#@";
        if ($I("chkCerradas").checked) js_args += "1@#@";
        else js_args += "0@#@";
        js_args += sValue +"@#@";
        js_args += sIniProy +"@#@"; 
        js_args += sFinProy; 
        
        mostrarProcesando();
        $I("cboVista").blur();
        //$I("txtEstado").focus();//para quitar el foco del combo y no realizar otra petición "sin querer"
        window.focus();
        RealizarCallBack(js_args, "");
	}
	catch(e){
		mostrarErrorAplicacion("Error al solicitar la modificación de la vista-2.", e.message);
    }
}

var bCerradas = false;
function mostrarCerradas(){
    try{
        if (bCambios) {
            jqConfirm("", "Para modificar esta opción, los datos deben estar grabados. ¿Deseas grabarlos?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bCerradas = true;
                    grabar();
                    return;
                }else bCerradas = false;
                bCambios = false;
                LLamarMostrarCerradas();
            });
        }
        else LLamarMostrarCerradas();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al modificar el control de mostrar tareas cerrada-1.", e.message);
    }
}
function LLamarMostrarCerradas(){
    try{
        js_args = "getPE@#@";
        //js_args+= $I("txtCodProy").value + "@#@";
        js_args += $I("hdnT305IdProy").value +"@#@";
        js_args+= $I("txtEstado").value + "@#@";
        if ($I("chkCerradas").checked) js_args += "1@#@";
        else js_args += "0@#@";
        js_args += $I("cboVista").value +"@#@";
        js_args += sIniProy +"@#@"; 
        js_args += sFinProy; 

        mostrarProcesando();
        RealizarCallBack(js_args, "");  
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el control de mostrar tareas cerradas-2.", e.message);
    }
}

//Funciones para contraer/expandir
function mostrar(oImg){
    //Contrae o expande un elemento
    try{
        var opcion, nMargen, nMargenAct, sEstado, sTipo;

        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        if (oImg.src.indexOf("plus.gif") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar
        sTipoAct = oFila.getAttribute("sTipo");
        //alert("nIndexFila: "+ nIndexFila +"\nnDesplegado: "+ nDesplegado +"\nnNivel: "+ nNivel +"\nopcion: "+ opcion +"\nsTipoAct: "+ sTipoAct);
        
        if (nDesplegado == "0"){
            switch (nNivel){
                case "1": //PT
                    var js_args = "getPT@#@";
                    js_args += $I("hdnT305IdProy").value +"@#@";
                    js_args += oFila.getAttribute("PT") + "@#@"; 
                    js_args += $I("txtEstado").value +"@#@"; 
                    js_args += $I("hdnEsRtpt").value +"@#@"; 
                    if ($I("chkCerradas").checked) js_args += "1@#@";
                    else js_args += "0@#@";
                    js_args += $I("cboVista").value +"@#@";
                    js_args += sIniProy +"@#@"; 
                    js_args += sFinProy; 
                    break;
                    
                case "2": //Fase o Actividad
                    if (sTipoAct == "F") var js_args = "getFase@#@";
                    else var js_args = "getActiv@#@";
                    js_args += $I("hdnT305IdProy").value +"@#@";
                    js_args += oFila.getAttribute("PT") + "@#@";
                    if (sTipoAct == "F") js_args += oFila.getAttribute("F") + "@#@";
                    else js_args += oFila.getAttribute("A") + "@#@";
                    js_args += $I("txtEstado").value +"@#@"; 
                    js_args += $I("hdnEsRtpt").value +"@#@"; 
                    if ($I("chkCerradas").checked) js_args += "1@#@";
                    else js_args += "0@#@";
                    js_args += $I("cboVista").value +"@#@";
                    js_args += sIniProy +"@#@"; 
                    js_args += sFinProy; 
                    break;
            }
            iFila=nIndexFila;
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
            return;
        }
    
        var iF = oImg.parentNode.parentNode.rowIndex;
        var sMargen = oFila.getAttribute("margen");
       	//Si pulso sobre la imagen en un elemento que no sea P, F o A no hago nada
       	if ((sTipoAct!="P")&&(sTipoAct!="F")&&(sTipoAct!="A")){
            ocultarProcesando();
            aFila = null;
       	    return;
       	}

       	nMargenAct = oFila.getAttribute("margen");

       	var tblDatos = $I("tblDatos");
       	var tblBodyMovil = $I("tblBodyMovil");
       	for (var i = iF + 1; i < tblDatos.rows.length; i++) {
            sTipo = tblDatos.rows[i].getAttribute("sTipo");
            nMargen = tblDatos.rows[i].getAttribute("margen");
            if (nMargenAct >= nMargen)break;
            else{
                if (opcion == "O")
                {//Al ocultar contraemos todos los hijos independientemente de su nivel
                    if ((sTipo=="P")||(sTipo=="F")||(sTipo=="A")){
                        if (tblDatos.rows[i].cells[0].children[0].tagName == "IMG")
                            if (sTipo != "HT" && sTipo != "HF" && sTipo != "HM")//particular para esta pantalla
                                tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    }
                    tblDatos.rows[i].style.display = "none";
                    tblBodyMovil.rows[i].style.display = "none";
                }
                else{//Al desplegar, para P,F y A solo desplegamos los del siguiente nivel al actual 
                    if ((sTipoAct=="P")||(sTipoAct=="F")){
                        if (nMargenAct == nMargen - 20 || ((sTipo=="HT" && nMargenAct == nMargen - 40))){//Actúo solo sobre el siguiente nivel o sobre los hitos del siguiente nivel.
                            tblDatos.rows[i].style.display = "table-row";
                            tblBodyMovil.rows[i].style.display = "table-row";
                        }
                    }
                    else{
                        if ((sTipoAct=="A")||(sTipoAct=="HT"||sTipoAct=="HF"||sTipoAct=="HM")){
                            tblDatos.rows[i].style.display = "table-row";
                            tblBodyMovil.rows[i].style.display = "table-row";
                        }
                    }
                }
            }
        }
        if (oFila.getAttribute("sTipo") != "HT" && oFila.getAttribute("sTipo") != "HF" && oFila.getAttribute("sTipo") != "HM") {
            if (opcion == "O") {
                oImg.src = "../../../../images/plus.gif";
            }
            else oImg.src = "../../../../images/minus.gif"; 
        }
        if (!bMostrar) recolorearTabla("tblDatos");

        if (bMostrar) {
            MostrarTodo();
        }
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar){
    try{
        if (aFila == null) return;
        if (aFila.length == 0) return;
        var tblBodyMovil = $I("tblBodyMovil");
        if (nMostrar == 0){//Contraer
            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].getAttribute("nivel") > 1)
                {
                    var sTipo = aFila[i].getAttribute("sTipo");
                    if ((sTipo == "F" || sTipo == "A") && (sTipo != "HT" && sTipo != "HF" && sTipo != "HM"))//particular para esta pantalla
                        aFila[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    aFila[i].style.display = "none";
                    tblBodyMovil.rows[i].style.display = "none";
                }
                else 
                {
                    if (aFila[i].getAttribute("sTipo") != "HT" && aFila[i].getAttribute("sTipo") != "HF" && 
                        aFila[i].getAttribute("sTipo") != "HM")//particular para esta pantalla
                        aFila[i].cells[0].children[0].src = "../../../../images/plus.gif";
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

function moverScroll(e){
    try{
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;

        var nPos = oElement.scrollLeft;
        oDivTitulo.scrollLeft = nPos;
	}catch(e){
		mostrarErrorAplicacion("Error al mover el scroll del título", e.message);
    }
}

var bMostrar=false;
var nIndiceTodo = -1;
function MostrarTodo(){
    try
    {
        if (aFila == null) return;
        bMostrar = false;
        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        var tblDatos = $I("tblDatos");
        for (var i=nIndiceAux; i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                bMostrar=true;
                nIndiceTodo = i;
                mostrar(tblDatos.rows[i].cells[0].children[0]);
                return;
            }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        aFila = FilasDe("tblDatos");
        recolorearTabla("tblDatos");//recolorea();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

var oFechaGantt;
var oFilaGantt;
function actualizarFechas(oFecha){
    try
    {
        var oFila = oFecha.parentNode.parentNode;
        if (oFecha.value != oFecha.getAttribute("valAnt")){
            oFechaGantt = oFecha;
            oFilaGantt = oFila;
            mostrarProcesando();
            setTimeout("actualizarFechas2();", 20);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la fecha", e.message);
    }
}
function actualizarFechas2(){
    try
    {
        var oFecha = oFechaGantt;
        var oFila = oFilaGantt;
        var sValorMax="";//Comparo FFPL y FFPR
        sValorMax = fechaMasAlta(oFila.cells[2].children[0].value, oFila.cells[4].children[0].value);

        if (oFecha.value != ""){
            var nPosAux = oFecha.name.indexOf("txtETPR");
            if (nPosAux == -1){ //no se trata de ETPR
                nPosAux = oFecha.name.indexOf("txtFI");
                if (nPosAux != -1) comprobarFechaInicioFin("D", oFecha.value);
                else{
//                    sValorMax = fechaMasAlta(oFila.cells[2].children[0].value, oFila.cells[4].children[0].value);
                    comprobarFechaInicioFin("H", sValorMax);
                }
            }    
        }
        //alert("sTipo: "+ oFila.sTipo +"   Tarea: "+ oFila.T +"  Fecha: "+ sValorMax);
        if (oFila.getAttribute("sTipo") == "T" && aHCM.length > 0) //Si hay hitos de cumplimiento múltiple hay que comprobar si contemplan la tarea modificada.
            comprobarHCM(oFila.getAttribute("T"), sValorMax);
        
        oFila.setAttribute("modif","1");
        var strImg = oFila.cells[0].children[1].src;
        var nPos = strImg.lastIndexOf("/");
        if (oFila.getAttribute("sTipo") != "HT" && oFila.getAttribute("sTipo") != "HF" && oFila.getAttribute("sTipo") != "HM")
            strImg = strImg.substring(0, nPos+1) + "imgTarea.gif";
        else
            strImg = strImg.substring(0, nPos+1) + "imgHito.gif";
        oFila.cells[0].children[1].src = strImg;
        
        //alert(oFila.cells[0].children[1].src);
        activarGrabar();
        setTimeout("calcularTotales();", 10);
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la fecha", e.message);
    }
}
function actualizarFechasFila(oFila){
    try
    {
        var sDesde = oFila.cells[1].children[0].value;
        var sValorMax = fechaMasAlta(oFila.cells[2].children[0].value, oFila.cells[4].children[0].value);
        
        comprobarFechaInicioFin("D", sDesde);
        comprobarFechaInicioFin("H", sValorMax);
        
        calcularTotales();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la fecha", e.message);
    }
}
function comprobarHCM(idTarea, sFecha){
    try
    {
        for (var i=0;i<aHCM.length;i++){
            //alert("idHito: "+aHCM[i].idHito+"\nidTarea: "+ aHCM[i].idTarea+"\nffpr: "+ aHCM[i].ffpr);
            if (aHCM[i].idTarea == idTarea){ //Para cada tarea de HCM que se encuentre en el array.
                //1º Actualizar la fecha de la tarea en el hito
                aHCM[i].ffpr = sFecha;
                
                //2º Obtener del gantt la fila y la fecha del hito cuya tarea se ha modificado.
                for (var x=aFila.length-1; x>=0; x--){//De abajo arriba ya que los HCM se muestran siempre al final.
                    if (aHCM[i].idHito == aFila[x].getAttribute("H")){
                        var aFecHCM = aHCM[i].ffpr.split("/");
                        var aFecAux = aFila[x].cells[4].children[0].value.split("/");
                        var oFecHCM = new Date(aFecHCM[2], eval(aFecHCM[1]-1), aFecHCM[0]);
                        var oFecAux = new Date(aFecAux[2], eval(aFecAux[1]-1), aFecAux[0]);
                        if (oFecHCM > oFecAux){//Si la fecha modificada es superior a la que muestra el HCM en el gantt, se actualiza.
                            aFila[x].cells[4].children[0].value = aHCM[i].ffpr;
                        }else if (oFecHCM < oFecAux){//Si es menor, hay que buscar la fecha mayor de las tareas del hito (porque podía ser la tarea modificada), y actualizar dicha fecha.
                            var oFecMax = new Date(1900, 0, 1);
                            //var bEncontrado = false; //No hago control de encontrado, ya que la información se puede regenerar en función de lo que se haga en la pantalla de detalle de hito.
                            for (var z=0;z<aHCM.length;z++){
                                if (aHCM[i].idHito == aHCM[z].idHito){
                                    var aFecMax = aHCM[z].ffpr.split("/");
                                    var oFecMaxAux = new Date(aFecMax[2], eval(aFecMax[1]-1), aFecMax[0]);
                                    if (oFecMaxAux > oFecMax){
                                        oFecMax = oFecMaxAux;
                                        aFila[x].cells[4].children[0].value = aHCM[z].ffpr;
                                    }
                                    //bEncontrado = true;
                                }//else if (bEncontrado) break;
                            }
                        }
                        break;
                    }
                }
            }
        }

	}catch(e){
		mostrarErrorAplicacion("Error al comprobar las tareas de los HCM", e.message);
    }
}

function mostrarDetalle(sAccesibilidad, nIndiceFila){
    mostrarProcesando();
    setTimeout("mDetalle('"+sAccesibilidad+"',"+nIndiceFila+")",100);
}

function mDetalle(sAccesibilidad, nIndiceFila){
    try{
        if (sAccesibilidad=="N"){
            ocultarProcesando();
            msjNoAccesible();	        
            return;
        }
        iFila = nIndiceFila;
        //	    sCR = $I("txtUne").value;
        //	    sPE = $I("txtCodProy").value;
        //No dejamos acceso a hitos de tarea (para ello comprobamos si el hito tiene asociado código de tarea)
        //        if (sTipo=="HT" || sTipo=="HF" || sTipo=="HM"){
        //            if (aFila[iFila].T != "0"){
        //                ocultarProcesando();
        //                return;
        //            }
        //        }
        //Si la estructura no está grabada solicito grabacion
        if (bCambios) {
            ocultarProcesando();
            jqConfirm("", "Datos modificados.<br />Para acceder al detalle es preciso grabarlos. <br><br>¿Deseas hacerlo?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bDetalle = true;
                    iFDet = nIndiceFila;
                    sAccesibilidadDetalle = sAccesibilidad;
                    grabar();
                }
            });
        } else LLamarMDetalle(sAccesibilidad, nIndiceFila);
    }
    catch (e)
    {
            ocultarProcesando();
            mostrarErrorAplicacion("Error al mostrar el detalle del elemento-1.", e.message);
    }
}
function LLamarMDetalle(sAccesibilidad, nIndiceFila) {
    var bRecalcular = false;
    var sCodigo, sPantalla = "", sPE, sHayCambios;
    var sTipo, sDescAnt, sDescAct, sFIPLAnt, sFIPLAct, sFFPLAnt, sFFPLAct, sETPRAnt, sETPRAct, sFFPRAnt, sFFPRAct;
    var sAVANRAnt, sAVANRAct, sAvanAutoAnt, sAvanAutoAct;

    try {
        var aFila = FilasDe("tblDatos");
        iFila = nIndiceFila;
        sPE = $I("hdnT305IdProy").value;
        sTipo = aFila[iFila].getAttribute("sTipo");

        sPantalla = strServer + "Capa_Presentacion/PSP/";

        switch(sTipo){
            case "T":
                sPantalla += "Tarea/Default.aspx?t="; //nIdTarea
                sCodigo = codpar(aFila[iFila].getAttribute("T"));
                break;
            case "HT":
            case "HF":
            case "HM":
                sPantalla += "Hito/Default.aspx?pe=" + codpar(sPE) + "&th=" + codpar(sTipo) + "&h="; //nPE sTipoHito nIDHito
                sCodigo = codpar(aFila[iFila].getAttribute("H"));
                break;
        }

        sDescAnt = aFila[iFila].cells[0].innerText;
        sFIPLAnt = aFila[iFila].cells[1].children[0].value;
        sFFPLAnt = aFila[iFila].cells[2].children[0].value;
        sETPRAnt = aFila[iFila].cells[3].children[0].value;
        sFFPRAnt = aFila[iFila].cells[4].children[0].value;
        sAVANRAnt= aFila[iFila].getAttribute("avanr");
        sAvanAutoAnt = aFila[iFila].getAttribute("avanceauto");
        
        if (sPantalla!=""){
            sPantalla += sCodigo + "&pm=" + codpar(sAccesibilidad) + "&cr=" + codpar($I("txtUne").value) + "&o=" + codpar("gantt");
            mostrarProcesando();

            modalDialog.Show(sPantalla, self, sSize(940, 650))
            .then(function(ret) {
                    if (ret != null) {
                        //Devuelve una cadena del tipo 
                        //  0          1       2          3        4      5         6               7           8            9               10        11         12    13     14          15
                        //HayCambio@#@tipo@#@descripcion@#@fInicio@#@fFin@#@Duracion@#@Presupuesto@#@Situacion@#@FechaInicioVigor@#@FechaFinVigor@#@Facturable@#@bRecargar@#@ETPR@#@FFPR@#@AVANR@#@calculoAutomatico
                        //Recojo los valores y si hay alguna diferencia actualizo el gantt
                        //Si no es modificable se supone que no ha podido cambiar nada en la pantalla detalle
                        if (sAccesibilidad != "W") {
                            //aFila = null;
                            ocultarProcesando();
                            return;
                        }

                        var aNuevos = ret.split("@#@");

                        sHayCambios = aNuevos[0];
                        if (sHayCambios == "F") {//no ha habido cambios en la pantalla detalle
                            //aFila = null;
                            ocultarProcesando();
                            return;
                        }
                        bHayCambios = true;
                        sTipo = aNuevos[1];
                        sDescAct = aNuevos[2];
                        if (sDescAnt != sDescAct) {
                            aFila[iFila].cells[0].children[2].innerText = sDescAct;
                        }

                        if (sTipo == "T") {
                            sFIPLAct = aNuevos[3];
                            if (sFIPLAnt != sFIPLAct) {
                                bRecalcular = true;
                                aFila[iFila].cells[1].children[0].value = sFIPLAct;
                            }

                            sFFPLAct = aNuevos[4];
                            if (sFFPLAnt != sFFPLAct) {
                                bRecalcular = true;
                                aFila[iFila].cells[2].children[0].value = sFFPLAct;
                            }

                            sETPRAct = aNuevos[12];
                            if (sETPRAnt != sETPRAct) {
                                bRecalcular = true;
                                aFila[iFila].cells[3].children[0].value = sETPRAct;
                            }

                            sFFPRAct = aNuevos[13];
                            if (sFFPRAnt != sFFPRAct) {
                                bRecalcular = true;
                                aFila[iFila].cells[4].children[0].value = sFFPRAct;
                            }

                            sAVANRAct = aNuevos[14];
                            if (sAVANRAnt != sAVANRAct) {
                                bRecalcular = true;
                                aFila[iFila].setAttribute("avanr", dfn(sAVANRAct));
                            }

                            sAvanAutoAct = aNuevos[15];
                            if (sAvanAutoAnt != sAvanAutoAct) {
                                bRecalcular = true;
                                aFila[iFila].setAttribute("avanceauto", sAvanAutoAct);
                            }

                            //alert("sTipo: "+ oFila.sTipo +"   Tarea: "+ oFila.T +"  Fecha: "+ sValorMax);
                            if (aFila[iFila].getAttribute("sTipo") == "T" && aHCM.length > 0) { //Si hay hitos de cumplimiento múltiple hay que comprobar si contemplan la tarea modificada.
                                var sValorMax = fechaMasAlta(aFila[iFila].cells[2].children[0].value, aFila[iFila].cells[4].children[0].value);
                                comprobarHCM(aFila[iFila].getAttribute("T"), sValorMax);
                            }

                        } else { //hito
                            if (sTipo != "HM") {
                                sFIPLAct = aNuevos[3];
                                if (sFIPLAnt != sFIPLAct) {
                                    bRecalcular = true;
                                    aFila[iFila].cells[4].children[0].value = sFIPLAct;
                                }
                            } else { //Se ha modificado un HCM
                                //alert(aNuevos[3]+"\n"+aNuevos[4]);
                                //1º borrar los datos relativos al HCM aNuevos[3] del array.
                                bRecalcular = true;
                                for (var i = aHCM.length - 1; i >= 0; i--) {
                                    //alert("idHito: "+aHCM[i].idHito+"\nidTarea: "+ aHCM[i].idTarea+"\nffpr: "+ aHCM[i].ffpr);
                                    if (aHCM[i].idHito == aNuevos[3]) aHCM.splice(i, 1);
                                }
                                //2º insertar los datos nuevos.
                                var oFecMax = new Date(1900, 0, 1);
                                var sFecMax = "";
                                var aTareas = aNuevos[4].split("##");
                                for (var i = 0; i < aTareas.length; i++) {
                                    if (aTareas[i] == "") continue;
                                    var aDatos = aTareas[i].split("//");
                                    oHCM_I(aNuevos[3], aDatos[0], aDatos[1]);
                                    var aFecMax = aDatos[1].split("/");
                                    var oFecMaxAux = new Date(aFecMax[2], eval(aFecMax[1] - 1), aFecMax[0]);
                                    if (oFecMaxAux > oFecMax) {
                                        oFecMax = oFecMaxAux;
                                        sFecMax = aDatos[1];
                                    }
                                }
                                aFila[iFila].cells[4].children[0].value = sFecMax;
                            }
                        }
                        if (bRecalcular) actualizarFechasFila(aFila[iFila]); // calcularTotales();
                    } //if (ret != null)
                });
            window.focus();
	    } //if (sPantalla!="")
        ocultarProcesando();
        //aFila = null;
    }//try
    catch(e){
	    ocultarProcesando();
		mostrarErrorAplicacion("Error al mostrar el detalle del elemento-2", e.message);
    }
}
function msjNoAccesible(){
    mmoff("Inf","Acceso no permitido.\n\nEl elemento seleccionado queda fuera de tu responsabilidad.",400);
    ocultarProcesando();
}
//Función a la que se llama cuando hay que añadir fechas (años o meses)
//en el gráfico.
function comprobarFechaInicioFin(sOrigen, sValor) {
    try {   //alert(sOrigen+" - "+ sValor);
        var nDif;
        var nAnno;
        var nWidthCol;
        var nColSpan;
        var sVista = $I("cboVista").value;

        switch (sVista) {
            case "M": nWidthCol = 336; break;
            case "S": nWidthCol = 40; break;
            case "D": nWidthCol = 56; break;
        }

        nColSpan = parseInt($I("tdCol").colSpan, 10);
        var aDatos = sValor.split("/");
        if (sOrigen == "D") {
            var aInicio = sIniProy.split("/");
            //alert(aDatos[2] +" / "+ aInicio[2]);
            //if (aDatos[2] < aInicio[2]){
            var dInicio = new Date(aInicio[2], eval(aInicio[1] - 1), aInicio[0]);
            var dDatos = new Date(aDatos[2], eval(aDatos[1] - 1), aDatos[0]);
            if (dDatos < dInicio) {
                switch (sVista) {
                    case "M":
                        var nWidthTabla = parseInt($I("tblBodyMovil").style.width.substring(0, $I("tblBodyMovil").style.width.length - 2), 10);
                        var nWidthColumna = parseInt($I("tblBodyMovil").children[0].children[1].style.width.substring(0, $I("tblBodyMovil").children[0].children[1].style.width.length - 2), 10);
                        while (dInicio > dDatos) {
                            dInicio = dInicio.add("y", -1);
                            $I("tblTituloMovil").style.width = (parseInt($I("tblTituloMovil").style.width.substring(0, $I("tblTituloMovil").style.width.length - 2), 10) + nWidthCol) + "px";
                            var oCelda = $I("tblTituloMovil").rows[0].insertCell(1);
                            oCelda.style.width = nWidthCol + "px";
                            oCelda.align = "middle";
                            oCelda.innerText = dInicio.getFullYear(); //nAnno;
                            nWidthTabla += nWidthCol;
                            nWidthColumna += nWidthCol;
                            nColSpan++;
                        }
                        $I("tblBodyMovil").style.width = nWidthTabla + "px";
                        $I("tblBodyMovil").children[0].children[1].style.width = nWidthColumna + "px";
                        $I("tdCol").colSpan = nColSpan;
                        sIniProy = "01/01/" + aDatos[2];
                        break;
                    case "S":
                        var nWidthTabla = (parseInt($I("tblBodyMovil").style.width.substring(0, $I("tblBodyMovil").style.width.length - 2), 10));
                        var nWidthColumna = (parseInt($I("tblBodyMovil").children[0].children[1].style.width.substring(0, $I("tblBodyMovil").children[0].children[1].style.width.length - 2), 10));
                        var sPriSem = $I("tblTituloMovil").rows[0].cells[0].innerText;
                        var nSemana = sPriSem.split("/")[0];
                        var nAnno = sPriSem.split("/")[1];
                        while (dInicio > dDatos) {
                            //dInicio = dInicio.add("mo", -1);
                            dInicio = dInicio.add("d", -7);
                            $I("tblTituloMovil").style.width = (parseInt($I("tblTituloMovil").style.width.substring(0, $I("tblTitulo").style.width.length - 2), 10) + nWidthCol) + "px";
                            var oCelda = $I("tblTituloMovil").rows[0].insertCell(1);
                            oCelda.style.width = nWidthCol + "px";
                            oCelda.align = "middle";
                            oCelda.style.fontSize = "8pt";
                            if (nSemana == 1) {
                                nSemana = 52;
                                nAnno = parseInt(nAnno, 10) - 1;
                                if (nAnno < 10) nAnno = "0" + nAnno;
                            }
                            //oCelda.innerText = obtenerTextoMes(dInicio);
                            oCelda.innerText = nSemana + "/" + nAnno;
                            nSemana--;
                            nWidthTabla += nWidthCol;
                            nWidthColumna += nWidthCol;
                            nColSpan++;
                        }
                        $I("tblBodyMovil").style.width = nWidthTabla + "px";
                        $I("tblBodyMovil").children[0].children[1].style.width = nWidthColumna + "px";
                        $I("tdCol").colSpan = nColSpan;
                        sIniProy = "01/" + aDatos[1] + "/" + aDatos[2];
                        break;
                    case "D":
                        var nWidthTabla = parseInt($I("tblBodyMovil").style.width.substring(0, $I("tblDatos").style.width.length - 2), 10);
                        var nWidthColumna = parseInt($I("tblBodyMovil").children[0].children[1].style.width.substring(0, $I("tblBodyMovil").children[0].children[1].style.width.length - 2), 10);
                        while (dInicio > dDatos) {
                            //dInicio = dInicio.add("d", -1);
                            dInicio = dInicio.add("d", -7);
                            $I("tblTituloMovil").style.width = (parseInt($I("tblTituloMovil").style.width.substring(0, $I("tblTituloMovil").style.width.length - 2), 10) + nWidthCol) + "px";
                            var oCelda = $I("tblTituloMovil").rows[0].insertCell(1);
                            oCelda.style.width = nWidthCol + "px";
                            oCelda.align = "middle";
                            oCelda.style.fontSize = "7pt";
                            oCelda.innerText = obtenerTextoDia(dInicio);
                            nWidthTabla += nWidthCol;
                            nWidthColumna += nWidthCol;
                            nColSpan++;
                        }
                        $I("tblBodyMovil").style.width = nWidthTabla + "px";
                        $I("tblBodyMovil").children[0].children[1].style.width = nWidthColumna + "px";
                        $I("tdCol").colSpan = nColSpan;
                        var sDia = dInicio.getDate();
                        if (sDia.length == 1) sDia = "0" + sDia;
                        var sMes = dInicio.getMonth() + 1;
                        if (sMes.length == 1) sMes = "0" + sMes;
                        var sAnno = dInicio.getFullYear();
                        sIniProy = sDia + "/" + sMes + "/" + sAnno;
                        //sIniProy = sValor;
                        break;
                }
                //sIniProy = "01/01/"+aDatos[2];
            }
        } else {
            var aFin = sFinProy.split("/");
            //alert(aDatos[2] +" / "+ aFin[2]);
            //if (aDatos[2] > aFin[2]){
            var dFin = new Date(aFin[2], eval(aFin[1] - 1), aFin[0]);
            var dDatos = new Date(aDatos[2], eval(aDatos[1] - 1), aDatos[0]);
            if (dDatos > dFin) {
                switch (sVista) {
                    case "M":
                        var nWidthTabla = parseInt($I("tblBodyMovil").style.width.substring(0, $I("tblBodyMovil").style.width.length - 2), 10);
                        var nWidthColumna = parseInt($I("tblBodyMovil").children[0].children[1].style.width.substring(0, $I("tblBodyMovil").children[0].children[1].style.width.length - 2), 10);
                        while (dFin < dDatos) {
                            dFin = dFin.add("y", 1);
                            $I("tblTituloMovil").style.width = (parseInt($I("tblTituloMovil").style.width.substring(0, $I("tblTituloMovil").style.width.length - 2), 10) + nWidthCol) + "px";
                            var oCelda = $I("tblTituloMovil").rows[0].insertCell(-1);
                            oCelda.style.width = nWidthCol + "px";
                            oCelda.align = "middle";
                            oCelda.innerText = dFin.getFullYear();
                            nWidthTabla += nWidthCol;
                            nWidthColumna += nWidthCol;
                            nColSpan++;
                        }
                        $I("tblDatos").style.width = nWidthTabla + "px";
                        $I("tblDatos").children[0].children[6].style.width = nWidthColumna + "px";
                        $I("tdCol").colSpan = nColSpan;
                        sFinProy = "31/12/" + aDatos[2];
                        break;
                    case "S":
                        var nWidthTabla = parseInt($I("tblBodyMovil").style.width.substring(0, $I("tblBodyMovil").style.width.length - 2), 10);
                        var nWidthColumna = parseInt($I("tblBodyMovil").children[0].children[1].style.width.substring(0, $I("tblBodyMovil").children[0].children[1].style.width.length - 2), 10);
                        var sUltSem = $I("tblTituloMovil").rows[0].cells[$I("tblTituloMovil").rows[0].cells.length - 1].innerText;
                        var nSemana = sUltSem.split("/")[0];
                        var nAnno = sUltSem.split("/")[1];

                        while (dFin < dDatos) {
                            //dFin = dFin.add("mo", 1);
                            dFin = dFin.add("d", 7);
                            $I("tblTituloMovil").style.width = (parseInt($I("tblTituloMovil").style.width.substring(0, $I("tblTituloMovil").style.width.length - 2), 10) + nWidthCol) + "px";
                            var oCelda = $I("tblTituloMovil").rows[0].insertCell(-1);
                            oCelda.style.width = nWidthCol + "px";
                            oCelda.align = "middle";
                            oCelda.style.fontSize = "8pt";
                            //oCelda.innerText = obtenerTextoMes(dFin);
                            if (nSemana == 52) {
                                nSemana = 1;
                                nAnno = parseInt(nAnno, 10) + 1;
                                if (nAnno < 10) nAnno = "0" + nAnno;
                            }
                            oCelda.innerText = nSemana + "/" + nAnno;
                            nSemana--;

                            nWidthTabla += nWidthCol;
                            nWidthColumna += nWidthCol;
                            nColSpan++;
                        }
                        $I("tblBodyMovil").style.width = nWidthTabla + "px";
                        $I("tblBodyMovil").children[0].children[1].style.width = nWidthColumna + "px";
                        $I("tdCol").colSpan = nColSpan;
                        sFinProy = "01/" + aDatos[1] + "/" + aDatos[2];
                        break;
                    case "D":
                        var nWidthTabla = parseInt($I("tblBodyMovil").style.width.substring(0, $I("tblBodyMovil").style.width.length - 2), 10);
                        var nWidthColumna = parseInt($I("tblBodyMovil").children[0].children[1].style.width.substring(0, $I("tblBodyMovil").children[0].children[1].style.width.length - 2), 10);
                        while (dFin < dDatos) {
                            //dFin = dFin.add("d", 1);
                            dFin = dFin.add("d", 7);
                            $I("tblTituloMovil").style.width = (parseInt($I("tblTituloMovil").style.width.substring(0, $I("tblTituloMovil").style.width.length - 2), 10) + nWidthCol) + "px";
                            var oCelda = $I("tblTituloMovil").rows[0].insertCell(-1);
                            oCelda.style.width = nWidthCol + "px";
                            oCelda.align = "middle";
                            oCelda.style.fontSize = "7pt";
                            //oCelda.innerText = obtenerTextoDia(dFin);
                            oCelda.innerHTML = obtenerTextoDia(dFin);
                            nWidthTabla += nWidthCol;
                            nWidthColumna += nWidthCol;
                            nColSpan++;
                        }
                        $I("tblBodyMovil").style.width = nWidthTabla + "px";
                        $I("tblBodyMovil").children[0].children[1].style.width = nWidthColumna + "px";
                        $I("tdCol").colSpan = nColSpan;
                        //sFinProy = sValor;
                        var sDia = dFin.getDate();
                        if (sDia.length == 1) sDia = "0" + sDia;
                        var sMes = dFin.getMonth() + 1;
                        if (sMes.length == 1) sMes = "0" + sMes;
                        var sAnno = dFin.getFullYear();
                        sFinProy = sDia + "/" + sMes + "/" + sAnno;
                        break;
                }
                //sFinProy = "31/12/"+aDatos[2];
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las fechas del proyecto.", e.message);
    }
}

function obtenerTextoMes(oFecha){
    try
    {
        var sMes, sAnno;
        sAnno = oFecha.getFullYear().toString().substring(2,4);
        switch (oFecha.getMonth()){
            case 0: sMes = "Ene"; break;
            case 1: sMes = "Feb"; break;
            case 2: sMes = "Mar"; break;
            case 3: sMes = "Abr"; break;
            case 4: sMes = "May"; break;
            case 5: sMes = "Jun"; break;
            case 6: sMes = "Jul"; break;
            case 7: sMes = "Ago"; break;
            case 8: sMes = "Sep"; break;
            case 9: sMes = "Oct"; break;
            case 10: sMes = "Nov"; break;
            case 11: sMes = "Dic"; break;
        }
        return sMes + " - " + sAnno;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nombre del mes - año", e.message);
    }
}

function obtenerTextoDia(oFecha){
    try
    {
        var sDia, sMes, sAnno;
        sDia = oFecha.getDate().toString();
        if (sDia.length == 1) sDia = "0"+ sDia;
        var nMonth = oFecha.getMonth()+1;
        sMes = nMonth.toString();
        if (sMes.length == 1) sMes = "0"+ sMes;
        //sAnno = oFecha.getFullYear().toString().substring(2,4);
        sAnno = oFecha.getFullYear().toString();
        return sDia + "/" + sMes + "/" + sAnno;
        
        var strSpan = "<span title='"+ sDia + "/" + sMes + "/" + sAnno +"'>"+ sDia + "/" + sMes +"</span>";
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nombre del dia/mes/año", e.message);
    }
}

function fechaMasAlta(sFecha1, sFecha2){
    try
    {   //04/10/2007 Victor: Manda FFPR (sFecha2), si no existe FFPL (sFecha1)
        var sValorMax = "";
        /*
        var sFec1 = sFecha1;
        var sFec2 = sFecha2;
        var aFec1, aFec2; 
        if (sFec1 != "" && sFec2 != ""){
            aFec1 = sFec1.split("/");
            aFec2 = sFec2.split("/");
            var dFec1 = new Date(aFec1[2], eval(aFec1[1]-1), aFec1[0]);
            var dFec2 = new Date(aFec2[2], eval(aFec2[1]-1), aFec2[0]);
            sValorMax = (dFec1 > dFec2) ? sFec1 : sFec2;
        }else if (sFec1 != ""){
            sValorMax = sFec1;
        }else if (sFec2 != ""){
            sValorMax = sFec2;
        }
        */
        if (sFecha2 != "") sValorMax = sFecha2;
        else sValorMax = sFecha1;
        return sValorMax;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la fecha más alta", e.message);
    }
}
function ocultarImagenesMovil(sTipo, indF) {
    try {
        var tblBodyMovil = $I("tblBodyMovil");
        switch (sTipo) {
            case "T":
                tblBodyMovil.rows[indF].cells[0].children[0].style.visibility = "hidden";
                var aImg = tblBodyMovil.rows[indF].cells[0].children[1].getElementsByTagName("IMG");
                var imgRojo = aImg[0];
                var imgVerde = aImg[1];
                var imgAzul = aImg[3]; //2 -->imgSeparador

                imgRojo.style.visibility = "hidden";
                imgVerde.style.visibility = "hidden";
                imgAzul.style.visibility = "hidden";
                break;
            case "HT":
            case "HF":
            case "HM":
                tblBodyMovil.rows[indF].cells[0].children[0].style.visibility = "hidden";
                tblBodyMovil.rows[indF].cells[0].children[1].style.visibility = "hidden";
                break;
            default:
                tblBodyMovil.rows[indF].cells[0].children[0].style.visibility = "hidden";
                tblBodyMovil.rows[indF].cells[0].children[1].style.visibility = "hidden";
                tblBodyMovil.rows[indF].cells[0].children[2].style.visibility = "hidden";
                tblBodyMovil.rows[indF].cells[0].children[3].style.visibility = "hidden";
                break;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar las imágenes", e.message);
    }
}

function mostrarImagenesMovil(sTipo, iInd, sDesde, sHasta) {
    try {
        var oFilaMovil = $I("tblBodyMovil").rows[iInd];
        var oFila = $I("tblDatos").rows[iInd];
        var nDiasInicio = DiffDiasFechas(sIniProy, sDesde);
        var nDiasDuracion = DiffDiasFechas(sDesde, sHasta);
        var nIni, nDur, nAvanR, nETPR = 0;

        var nAnchoDia = 0
        switch ($I("cboVista").value) {
            case "M":
                nAnchoDia = 0.9205;  //   336/365
                break;
            case "S":
                nAnchoDia = (40 * 52) / (52 * 7);
                break;
            case "D":
                nAnchoDia = 8;
                break;
        }

        nDur = parseInt(nDiasDuracion * nAnchoDia, 10);
        nIni = parseInt(nDiasInicio * nAnchoDia, 10);

        //alert(nIni +" / "+nDur);
        var sDesdeAnt;
        if (sTipo != "HT" && sTipo != "HF" && sTipo != "HM")
            sDesdeAnt = oFila.cells[1].children[0].getAttribute("valAnt");
        else
            sDesdeAnt = oFila.cells[4].children[0].getAttribute("valAnt");

        if (sTipo != "HT" && sTipo != "HF" && sTipo != "HM") {
            //Se obtiene de nuevo el sHasta, ya que a la función se ha pasado el mayor de FFPL y FFPR
            //para el cálculo de Inicio y Duración, pero ahora hay que actualizar el Tooltip.
            sHasta = oFila.cells[2].children[0].value;
            var sHastaAnt = oFila.cells[2].children[0].getAttribute("valAnt");

            var sHastaAntPR = oFila.cells[4].children[0].getAttribute("valAnt");
            var sHastaPR = oFila.cells[4].children[0].value;

            var sAvanTAnt = oFila.getAttribute("avant");
            var sAvanRAnt = oFila.getAttribute("avanr");
            var sETPRAnt = oFila.getAttribute("etpr");

            if (oFila.cells[3].children[0].value != "") nETPR = parseFloat(dfn(oFila.cells[3].children[0].value));
            oFila.setAttribute("etpr", nETPR);
            if (nETPR != 0)
                oFila.setAttribute("avant", parseFloat(oFila.getAttribute("consumo").replace(",", ".")) / nETPR * 100);
            else
                oFila.setAttribute("avant", 0);

            if (oFila.getAttribute("avanceauto") == "1") oFila.setAttribute("avanr", oFila.getAttribute("avant"));
        }

        var strTitle = "";
        if (sTipo != "HT" && sTipo != "HF" && sTipo != "HM") {
            strTitle += "Fecha inicio planificada:  " + sDesde;
            strTitle += "<br>Fecha fin planificada:      " + sHasta;
            strTitle += "<br>Estimación planificada:    " + oFila.getAttribute("etpl").ToString("N") + " horas";
            strTitle += "<br>Fecha fin prevista:          " + sHastaPR;
            strTitle += "<br>Estimación prevista:        " + oFila.getAttribute("etpr").ToString("N") + " horas";
            //strTitle += "<br>Consumido:                      " + formata(oFila.getAttribute("consumo").replace(",",".")) + " horas";
            strTitle += "<br>Consumido:                      " + oFila.getAttribute("consumo").ToString("N") + " horas";
            strTitle += "<br>Avance teórico:               " + oFila.getAttribute("avant").ToString("N") + " %";
            if (oFila.getAttribute("T") != "0")
                strTitle += "<br>Avance real:                    " + oFila.getAttribute("avanr").ToString("N") + " %";
        } else {
            strTitle += "Fecha de cumplimiento de hito:  " + sDesde;
        }
        switch (sTipo) {
            case "T":
                var aImg = oFilaMovil.cells[0].children[1].getElementsByTagName("IMG");
                var imgVerde = aImg[0];
                var imgRojo = aImg[1];
                var imgSeparadorAzul = aImg[2];
                var imgAzul = aImg[3];
                
                imgRojo.style.visibility = "hidden";
                imgVerde.style.visibility = "hidden";
                imgAzul.style.visibility = "hidden";

                var bAvanMax = false;
                if (oFila.getAttribute("avant") > 100) {
                    var nAvanT = nDur;
                    bAvanMax = true;
                } else
                    var nAvanT = parseInt(nDur * oFila.getAttribute("avant") / 100, 10);

                var nResto = parseInt(nDur - nAvanT, 10);

                if (oFila.getAttribute("avanr") > 100)
                    var nAvanR = nDur;
                else
                    var nAvanR = parseInt(nDur * oFila.getAttribute("avanr") / 100, 10);

                var strImg = imgVerde.src;
                var nPos = strImg.lastIndexOf("/");
                if (bAvanMax) imgVerde.src = strImg.substring(0, nPos + 1) + "imgGanttAvanMax.gif";
                else imgVerde.src = strImg.substring(0, nPos + 1) + "imgGanttV.gif";


                oFilaMovil.cells[0].children[0].style.width = nIni + "px";
                imgSeparadorAzul.style.width = nIni + "px";
                imgVerde.style.width = nAvanT + "px";
                imgRojo.style.width = nResto + "px";
                imgAzul.style.width = nAvanR + "px";

                oFilaMovil.cells[0].children[0].style.visibility = "visible";
                if (nAvanT > 0)
                    imgVerde.style.visibility = "visible";
                if (nResto > 0)
                    imgRojo.style.visibility = "visible";
                if (nAvanR > 0)
                    imgAzul.style.visibility = "visible";

                var sTitle = oFilaMovil.cells[0].children[1].title;
                if (sTitle != "") {
                    var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + strTitle + "] hideselects=[off]\"";
                    oFilaMovil.cells[0].children[1].title = sTootTip; //span
                } else {
                    oFilaMovil.cells[0].children[1].boBDY = strTitle; //span
                }
                break;
            case "HT":
            case "HF":
            case "HM":
                oFilaMovil.cells[0].children[0].style.width = (nIni + nDur - 5) + "px";
                oFilaMovil.cells[0].children[0].style.visibility = "visible";
                if (sDesde != "")
                    oFilaMovil.cells[0].children[1].style.visibility = "visible";
                else
                    oFilaMovil.cells[0].children[1].style.visibility = "hidden";
                var sTitle = oFilaMovil.cells[0].children[1].title;
                if (sTitle != "") {
                    var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + strTitle + "] hideselects=[off]\"";
                    oFilaMovil.cells[0].children[1].title = sTootTip;
                } else {
                    oFilaMovil.cells[0].children[1].boBDY = strTitle;
                }
                break;
            default:
                nIni = nIni - 3;
                nDur = nDur - 3;
                if (nIni < 1) nIni = 1;
                if (nDur < 1) nDur = 1;
                oFilaMovil.cells[0].children[0].style.width = nIni + "px";
                oFilaMovil.cells[0].children[2].style.width = nDur + "px";
                oFilaMovil.cells[0].children[0].style.visibility = "visible";
                oFilaMovil.cells[0].children[1].style.visibility = "visible";
                oFilaMovil.cells[0].children[2].style.visibility = "visible";
                oFilaMovil.cells[0].children[3].style.visibility = "visible";

                var nWidthColumna = parseInt($I("tblBodyMovil").children[0].children[0].style.width.substring(0, $I("tblBodyMovil").children[0].children[0].style.width.length - 2), 10);
                var nTotalImgs = nIni + nDur + 14; //7*2
                if (nTotalImgs > nWidthColumna) {
                    $I("tblBodyMovil").children[0].children[1].style.width = nTotalImgs + "px";
                }

                var sTitle = oFilaMovil.cells[0].children[2].title;
                if (sTitle != "") {
                    var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + strTitle + "] hideselects=[off]\"";
                    oFilaMovil.cells[0].children[2].title = sTootTip; //span
                } else {
                    oFilaMovil.cells[0].children[1].boBDY = strTitle; //span
                }
                break;
        }


        if (sTipo != "HT" && sTipo != "HF" && sTipo != "HM") {
            oFila.cells[1].children[0].setAttribute("valAnt", sDesde);
            oFila.cells[2].children[0].setAttribute("valAnt", sHasta);
            oFila.cells[3].children[0].setAttribute("valAnt", oFila.cells[3].children[0].value);
            oFila.cells[4].children[0].setAttribute("valAnt", sHastaPR);
        } else
            oFila.cells[4].children[0].setAttribute("valAnt", sDesde);
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las imágenes", e.message);
    }
}

function calcularTotales(){
/*
    Calcula los totales y redimensiona las imágenes
*/
    var sCad="";
    var sFecAux=""; 
    var sFIPL_PT="", sFIPL_F="", sFIPL_A=""; 
    var sFFPL_PT="", sFFPL_F="", sFFPL_A="";  
    var sFFPR_PT="", sFFPL_R="", sFFPR_A="";  
    var fETPR_PT=0, fETPR_F=0, fETPR_A=0;
   
    try{
        if (aFila==null)
            aFila = FilasDe("tblDatos");

        for (var i=aFila.length-1; i>=0; i--){
            sTipo = aFila[i].getAttribute("sTipo");
            sFase = aFila[i].getAttribute("F");
            sActividad = aFila[i].getAttribute("A");

            switch(sTipo){
                case "T":
                    //fecha inicio planificada
                    sFecAux=aFila[i].cells[1].children[0].value;
                    if (sFecAux!=""){
                        if (sFIPL_PT=="") sFIPL_PT = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFIPL_PT, sFecAux);
                            if (dif < 0) sFIPL_PT = sFecAux;
                        }
                        if (sFase != "0") {
                            if (sFIPL_F=="") sFIPL_F = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_F, sFecAux);
                                if (dif < 0) sFIPL_F = sFecAux;
                            }
                        }
                        if (sActividad != "0"){
                            if (sFIPL_A=="") sFIPL_A = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_A, sFecAux);
                                if (dif < 0) sFIPL_A = sFecAux;
                            }
                        }
                    }
                    //fecha fin planificada
                    sFecAux=aFila[i].cells[2].children[0].value;
                    if (sFecAux!=""){
                        if (sFFPL_PT=="") sFFPL_PT = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFFPL_PT, sFecAux);
                            if (dif > 0) sFFPL_PT = sFecAux;
                        }
                        if (sFase != "0") {
                            if (sFFPL_F=="") sFFPL_F = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_F, sFecAux);
                                if (dif > 0) sFFPL_F = sFecAux;
                            }
                        }
                        if (sActividad != "0"){
                            if (sFFPL_A=="") sFFPL_A = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_A, sFecAux);
                                if (dif > 0) sFFPL_A = sFecAux;
                            }
                        }
                    }
                    //fecha fin prevista
                    sFecAux=aFila[i].cells[4].children[0].value;
                    if (sFecAux!=""){
                        if (sFFPR_PT=="") sFFPR_PT = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFFPR_PT, sFecAux);
                            if (dif > 0) sFFPR_PT = sFecAux;
                        }
                        if (sFase != "0") {
                            if (sFFPR_F=="") sFFPR_F = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPR_F, sFecAux);
                                if (dif > 0) sFFPR_F = sFecAux;
                            }
                        }
                        if (sActividad != "0"){
                            if (sFFPR_A=="") sFFPR_A = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPR_A, sFecAux);
                                if (dif > 0) sFFPR_A = sFecAux;
                            }
                        }
                    }
                    
                    sCad=aFila[i].cells[3].children[0].value;
                    if (sCad=="") fAux=0;
                    else fAux=parseFloat(dfn(sCad));
                    if (fAux != 0){
                        fETPR_PT += fAux;
                        if (sFase != "0") fETPR_F += fAux;
                        if (sActividad != "0") fETPR_A += fAux;
                    }
                    break;
                    
                case "A":
                    if (aFila[i].getAttribute("desplegado") == "0"){
                        //fecha inicio planificada
                        sFecAux=aFila[i].cells[1].children[0].value;
                        if (sFecAux!=""){
                            if (sFIPL_PT=="") sFIPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_PT, sFecAux);
                                if (dif < 0) sFIPL_PT = sFecAux;
                            }
                            if (sFase != "0"){
                                if (sFIPL_F=="") sFIPL_F = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFIPL_F, sFecAux);
                                    if (dif < 0) sFIPL_F = sFecAux;
                                }
                            }
                        }
                        //fecha fin planificada
                        sFecAux=aFila[i].cells[2].children[0].value;
                        if (sFecAux!=""){
                            if (sFFPL_PT=="") sFFPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_PT, sFecAux);
                                if (dif > 0) sFFPL_PT = sFecAux;
                            }
                            if (sFase != "0"){
                                if (sFFPL_F=="") sFFPL_F = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFPL_F, sFecAux);
                                    if (dif > 0) sFFPL_F = sFecAux;
                                }
                            }
                        }
                        //fecha fin prevista
                        sFecAux=aFila[i].cells[4].children[0].value;
                        if (sFecAux!=""){
                            if (sFFPR_PT=="") sFFPR_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPR_PT, sFecAux);
                                if (dif > 0) sFFPR_PT = sFecAux;
                            }
                            if (sFase != "0"){
                                if (sFFPR_F=="") sFFPR_F = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFPR_F, sFecAux);
                                    if (dif > 0) sFFPR_F = sFecAux;
                                }
                            }
                        }
                        
                        sCad=aFila[i].cells[3].children[0].value;
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fETPR_PT += fAux;
                            if (sFase != "0") fETPR_F += fAux;
                        }

                        sFIPL_A = "";
                        sFFPL_A = "";
                        sFFPR_A = "";
                        fETPR_A = 0;

                        var sDesde = aFila[i].cells[1].children[0].value;
                        //var sHasta = aFila[i].cells[2].children[0].value;
                        var sHasta = fechaMasAlta(aFila[i].cells[2].children[0].value, aFila[i].cells[4].children[0].value);
                        //alert(sDesde +" - "+sHasta);
                        if (sDesde == "" || sHasta == ""){
                            //var sResul = ocultarImagenes(aFila[i]);
                            var sResul = ocultarImagenesMovil(aFila[i].getAttribute("sTipo"), i);
                        }else{
                            //var sResul = mostrarImagenes(aFila[i], sDesde, sHasta);
                            var sResul = mostrarImagenesMovil(aFila[i].getAttribute("sTipo"), i, sDesde, sHasta);
                        }
                        continue;
                    }
                
                    aFila[i].cells[1].children[0].value = sFIPL_A;
                    aFila[i].cells[2].children[0].value = sFFPL_A;
                    if (fETPR_A != 0) aFila[i].cells[3].children[0].value = fETPR_A.ToString("N");
                    else aFila[i].cells[3].children[0].value = "";
                    aFila[i].cells[4].children[0].value = sFFPR_A;

                    sFIPL_A = "";
                    sFFPL_A = "";
                    sFFPR_A = "";
                    fETPR_A = 0;
                    break;
                    
                case "F":
                    if (aFila[i].getAttribute("desplegado") == "0"){
                        //fecha inicio planificada
                        sFecAux=aFila[i].cells[1].children[0].value;
                        if (sFecAux!=""){
                            if (sFIPL_PT=="") sFIPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_PT, sFecAux);
                                if (dif < 0) sFIPL_PT = sFecAux;
                            }
                        }
                        //fecha fin planificada
                        sFecAux=aFila[i].cells[2].children[0].value;
                        if (sFecAux!=""){
                            if (sFFPL_PT=="") sFFPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_PT, sFecAux);
                                if (dif > 0) sFFPL_PT = sFecAux;
                            }
                        }
                        //fecha fin prevista
                        sFecAux=aFila[i].cells[4].children[0].value;
                        if (sFecAux!=""){
                            if (sFFPR_PT=="") sFFPR_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPR_PT, sFecAux);
                                if (dif > 0) sFFPR_PT = sFecAux;
                            }
                        }

                        sCad=aFila[i].cells[3].children[0].value;
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fETPR_PT += fAux;
                        }

                        sFIPL_F = "";
                        sFFPL_F = "";
                        sFFPR_F = "";
                        fETPR_F = 0;
                        sFIPL_A = "";
                        sFFPL_A = "";
                        sFFPR_A = "";
                        fETPR_A = 0;

                        var sDesde = aFila[i].cells[1].children[0].value;
                        //var sHasta = aFila[i].cells[2].children[0].value;
                        var sHasta = fechaMasAlta(aFila[i].cells[2].children[0].value, aFila[i].cells[4].children[0].value);
                        //alert(sDesde +" - "+sHasta);
                        if (sDesde == "" || sHasta == ""){
                            //var sResul = ocultarImagenes(aFila[i]);
                            var sResul = ocultarImagenesMovil(aFila[i].getAttribute("sTipo"), i);
                        }else{
                            //var sResul = mostrarImagenes(aFila[i], sDesde, sHasta);
                            var sResul = mostrarImagenesMovil(aFila[i].getAttribute("sTipo"), i, sDesde, sHasta);
                        }
                        continue;
                    }
                    
                    aFila[i].cells[1].children[0].value = sFIPL_F;
                    aFila[i].cells[2].children[0].value = sFFPL_F;
                    if (fETPR_F != 0) aFila[i].cells[3].children[0].value = fETPR_F.ToString("N");
                    else aFila[i].cells[3].children[0].value = "";
                    aFila[i].cells[4].children[0].value = sFFPR_F;
                    
                    sFIPL_F = "";
                    sFFPL_F = "";
                    sFFPR_F = "";
                    fETPR_F = 0;
                    sFIPL_A = "";
                    sFFPL_A = "";
                    sFFPR_A = "";
                    fETPR_A = 0;
                    break;

                case "P":
                        if (aFila[i].getAttribute("desplegado") == "0"){
                        sFIPL_PT = "";
                        sFFPL_PT = "";
                        sFFPR_PT = "";
                        fETPR_PT = 0;
                        sFIPL_F = "";
                        sFFPL_F = "";
                        sFFPR_F = "";
                        fETPR_F = 0;
                        sFIPL_A = "";
                        sFFPL_A = "";
                        sFFPR_A = "";
                        fETPR_A = 0;
                        
                        var sDesde = aFila[i].cells[1].children[0].value;
                        //var sHasta = aFila[i].cells[2].children[0].value;
                        var sHasta = fechaMasAlta(aFila[i].cells[2].children[0].value, aFila[i].cells[4].children[0].value);
                        //alert(sDesde +" - "+sHasta);
                        if (sDesde == "" || sHasta == ""){
                            //var sResul = ocultarImagenes(aFila[i]);
                            var sResul = ocultarImagenesMovil(aFila[i].getAttribute("sTipo"), i);
                        }else{
                            //var sResul = mostrarImagenes(aFila[i], sDesde, sHasta);
                            var sResul = mostrarImagenesMovil(aFila[i].getAttribute("sTipo"), i, sDesde, sHasta);
                        }
                        continue;
                    }

                    aFila[i].cells[1].children[0].value = sFIPL_PT;
                    aFila[i].cells[2].children[0].value = sFFPL_PT;
                    if (fETPR_PT != 0) aFila[i].cells[3].children[0].value = fETPR_PT.ToString("N");
                    else aFila[i].cells[3].children[0].value = "";
                    aFila[i].cells[4].children[0].value = sFFPR_PT;
                    
                    sFIPL_PT = "";
                    sFFPL_PT = "";
                    sFFPR_PT = "";
                    fETPR_PT = 0;
                    sFIPL_F = "";
                    sFFPL_F = "";
                    sFFPR_F = "";
                    fETPR_F = 0;
                    sFIPL_A = "";
                    sFFPL_A = "";
                    sFFPR_A = "";
                    fETPR_A = 0;
                    break;
                    
                case "HT":
                case "HF":
                case "HM":
                    if (aFila[i].PT != "0"){
                        //var sDesde = aFila[i-1].cells[1].children[0].value; //Hito de cumplimiento simple
                        aFila[i].cells[4].children[0].value = aFila[i-1].cells[4].children[0].value;
                        var sDesde = fechaMasAlta(aFila[i-1].cells[2].children[0].value, aFila[i-1].cells[4].children[0].value);
                    }else
                        var sDesde = aFila[i].cells[4].children[0].value; //Hito de fecha
//                    var sHasta = fechaMasAlta(aFila[i].cells[2].children[0].value, aFila[i].cells[4].children[0].value);
//                    //alert(sDesde +" - "+sHasta);
//                    if (sDesde == "" || sHasta == ""){
//                        var sResul = ocultarImagenes(aFila[i]);
//                    }else{
//                        var sResul = mostrarImagenes(aFila[i], sDesde, sHasta);
//                    }
                    //var sResul = mostrarImagenes(aFila[i], sDesde, sDesde);
                    var sResul = mostrarImagenesMovil(aFila[i].getAttribute("sTipo"), i, sDesde, sHasta);
                    continue;
                    break;
                    
            }//switch

            var sDesde = aFila[i].cells[1].children[0].value;
            //var sHasta = aFila[i].cells[2].children[0].value;
            var sHasta = fechaMasAlta(aFila[i].cells[2].children[0].value, aFila[i].cells[4].children[0].value);
            //alert(sDesde +" - "+sHasta);
            if (sDesde == "" || sHasta == ""){
                //var sResul = ocultarImagenes(aFila[i]);
                var sResul = ocultarImagenesMovil(aFila[i].getAttribute("sTipo"), i);
            }else{
                //var sResul = mostrarImagenes(aFila[i], sDesde, sHasta);
                var sResul = mostrarImagenesMovil(aFila[i].getAttribute("sTipo"), i, sDesde, sHasta);
            }
        }//for
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales", e.message);
    }
}
function excel() {
    try {
        aFila = FilasDe("tblDatos");
        var aFilaMovil = FilasDe("tblBodyMovil");

        if ($I("tblDatos") == null || aFila == null || aFila.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td></td>");
        sb.Append("        <td>Denominación</td>");
        sb.Append("        <td>FIPL</td>");
        sb.Append("        <td>FFPL</td>");
        sb.Append("        <td>ETPL</td>");
        sb.Append("        <td>FFPR</td>");

        var aCells = $I("tblTituloMovil").rows[0].getElementsByTagName("td");

        var sVista = $I("cboVista").value;
        var sEspacio = "";
        var nColspan = 0;

        for (var i = 0; i < aCells.length; i++) {
            switch (sVista) {
                case "M":
                    sb.Append("<td style='width:336px;' align=center>");
                    sb.Append(aCells[i].innerText);
                    sb.Append("<br>&nbsp;&nbsp;");
                    sb.Append("E&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("F&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("M&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("M&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("J&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("J&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("S&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("O&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("N&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append("D");
                    break;
                case "S":
                    sb.Append("<td style='width:40px;' align=center>");
                    sb.Append(aCells[i].innerText + " ");
                    break;
                case "D":
                    sb.Append("<td style='font-family:Arial;font-size:7pt;width:56px;' align=center>");
                    sb.Append(aCells[i].innerText.substring(0, 6) + aCells[i].innerText.substring(8, 10) + " ");
                    break;
            }

            sb.Append(sEspacio + "</td>");
            nColspan++;
        }
        sb.Append("</tr>");

        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].style.display == "none") continue;
            sb.Append("<tr style='height:20px;'>");

            sb.Append("<td>" + aFila[i].getAttribute("sTipo") + "</td>");
            sb.Append("<td style='color:" + aFila[i].cells[0].children[2].style.color + ";'>");
            switch (aFila[i].getAttribute("margen")) {
                case "0":
                    sb.Append("");
                    break;
                case "20":
                    sb.Append("     ");
                    break;
                case "40":
                    sb.Append("          ");
                    break;
                case "60":
                    sb.Append("               ");
                    break;
                case "80":
                    sb.Append("                    ");
                    break;
                default:
                    sb.Append("");
                    break;
            }
            sb.Append(aFila[i].cells[0].innerText + "</td>");
            sb.Append("<td>" + aFila[i].cells[1].children[0].value + "</td>");
            sb.Append("<td>" + aFila[i].cells[2].children[0].value + "</td>");
            sb.Append("<td>" + aFila[i].cells[3].children[0].value + "</td>");
            sb.Append("<td>" + aFila[i].cells[4].children[0].value + "</td>");
            sb.Append("<td colspan=" + nColspan + ">");
            switch (aFila[i].getAttribute("sTipo")) {
                case "T":
                    sb.Append("<img src='" + aFilaMovil[i].cells[0].children[0].src + "' width=" + aFilaMovil[i].cells[0].children[0].width + " height=14 margin='0'/>");

                    if (aFilaMovil[i].cells[0].children[1].children[0].style.visibility != "hidden")
                        sb.Append("<img src='" + aFilaMovil[i].cells[0].children[1].children[0].src + "' width=" + aFilaMovil[i].cells[0].children[1].children[0].width + " height=14 margin='0'/>");
                    if (aFilaMovil[i].cells[0].children[1].children[1].style.visibility != "hidden")
                        sb.Append("<img src='" + aFilaMovil[i].cells[0].children[1].children[1].src + "' width=" + aFilaMovil[i].cells[0].children[1].children[1].width + " height=14 margin='0'/>");
                    if (aFilaMovil[i].cells[0].children[1].children[4].style.visibility != "hidden") {
                        sb.Append("<BR><img src='" + aFilaMovil[i].cells[0].children[0].src + "' width=" + aFilaMovil[i].cells[0].children[0].width + " height=14 margin='0'/>");
                        sb.Append("<img src='" + aFilaMovil[i].cells[0].children[1].children[4].src + "' width=" + aFilaMovil[i].cells[0].children[1].children[4].width + " height=14 margin='0'/>");
                    }
                    break;
                case "HT":
                case "HF":
                case "HM":
                    if (aFilaMovil[i].cells[0].children[1].style.visibility != "hidden") {
                        sb.Append("<img src='" + aFilaMovil[i].cells[0].children[0].src + "' width=" + aFilaMovil[i].cells[0].children[0].width + " height=14 margin='0'/>");
                        sb.Append("<img src='" + aFilaMovil[i].cells[0].children[1].src + "' width=" + aFilaMovil[i].cells[0].children[1].width + " height=14 margin='0'/>");
                    }
                    break;
                default:
                    if (aFilaMovil[i].cells[0].children[1].style.visibility != "hidden") {
                        var oImgSep = aFilaMovil[i].cells[0].children[0];
                        var oImgAcu = aFilaMovil[i].cells[0].children[2];
                        oImgSep.width = oImgSep.width + 3;
                        oImgAcu.width = oImgAcu.width + 3;
                        sb.Append("<img src='" + oImgSep.src + "' width=" + oImgSep.width + " height=14 margin='0'/>");
                        sb.Append("<img src='" + oImgAcu.src + "' width=" + oImgAcu.width + " height=14 margin='0'/>");
                        oImgSep.width = oImgSep.width - 3;
                        oImgAcu.width = oImgAcu.width - 3;
                    }
                    break;
            }
            sb.Append("</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");


        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

/* Función para establecer el nivel de expansión */
var nNE = 1;
function setNE(nValor){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        nNE = nValor;
        colorearNE();
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
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function setResolucion1024(){
    try{

        $I("tblCab").style.width = 990 + "px";
        oColgroup = $I("tblCab").children[0];
        oColgroup.children[4].style.width = 230 + "px";
        
        //$I("txtNomProy").style.width = 450;

        $I("tblProyecto").style.width = 990 + "px";
        oColgroup2 = $I("tblProyecto").children[0];
        oColgroup2.children[1].style.width = 400 + "px";
        $I("divTituloMovil").style.width = 400 + "px";
        $I("divBodyMovil").style.width = 400 + "px";
        $I("divCatalogo").style.height = 510 + "px";
        $I("divBodyMovil").style.height = 510 + "px";
        $I("divPieMovil").style.width = 400 + "px";
    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}
//Para columnas moviles
function setScrollX() {
    try {
        oDivTituloMovil.scrollLeft = oDivPieMovil.scrollLeft;
        oDivBodyMovil.scrollLeft = oDivPieMovil.scrollLeft;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

function setScrollY(e) {
    try {
        if (oDivBodyMovil != null)
            oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll vertical", e.message);
    }
}

function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}
function ms_local(oFila) {
    try {
        var objTabla = oFila.parentNode.parentNode.id;
        var idFila = oFila.id;

        var nMantenimiento = 0;
        var sTipo = oFila.getAttribute("sTipo");
        if (sTipo == "HF")
            nMantenimiento = 1;
        else {//Si la fila es de acumulado de tareas cerradas, no habilito los campos
            if (sTipo == "T") {
                var sCodTarea = oFila.getAttribute("T");
                if (sCodTarea != "0")
                    nMantenimiento = 1;
            }
        }
        var aFila = FilasDe(objTabla);
        var j = 0;

        for (var i = 0, nCountLoop = aFila.length; i < nCountLoop; i++) {
            if (aFila[i].style.display == "none") continue;

            if (aFila[i].id == idFila) {
                aFila[i].className = "FS";
                if (nMantenimiento == 1) modoControles(aFila[i], true);
                iFila = i;
            }
            else {
                if (nMantenimiento == 1) modoControles(aFila[i], false);
                if (aFila[i].className == "FS") aFila[i].className = "";
            }
            j++;
        }
        aFila = null;
    } catch (e) {
        mostrarErrorAplicacion("Error en la selección simple", e.message);
    }
}
