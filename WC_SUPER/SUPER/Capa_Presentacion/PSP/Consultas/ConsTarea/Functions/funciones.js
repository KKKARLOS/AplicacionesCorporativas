var oDivTitulo;
var sValorNodo = "";
var oImgGlobal;
var nUltFilaAnt = 0;
var titulosTabla;
function init(){
    try{
        if (bRes1024) setResolucion1024();
        setNE(1);
        if ($I("chkActuAuto").checked)
            setOp($I("btnObtener"), 30);
        
        
        oDivTitulo = $I("divTablaTitulo");
        aFila = FilasDe("tblDatos");
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }
        else sValorNodo = $I("cboCR").value;

        setExcelImg("imgExcel", "divCatalogo");
        if (!bRes1024) $I("imgExcel_exp").style.left = "1207px";
        else $I("imgExcel_exp").style.left = "993px";
        $I("txtCodProy").focus();
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
        switch (aResul[0])
        {
            case "buscarPE":
                if (aResul[2]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##")
                        $I("hdnT305IdProy").value = aDatos[0];
                        setTimeout("recuperarDatosPSN();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "recuperarPSN":
                if (aResul[4]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            //sEstadoProy = aResul[2];
                switch (aResul[2])
                {
                    case "A": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }
                sValorNodo=aResul[6];
	            //$I("hdnIdNodo").value = aResul[6];
                if (es_administrador == "A" || es_administrador == "SA") {
                    //$I("hdnIdNodo").value = aResul[2];
                    $I("txtDesNodo").value = aResul[10];
                }
                else {
                    $I("cboCR").value = aResul[6];
                }  
	            $I("txtCodProy").value = aResul[4];
                $I("hdnT305IdProy").value = aResul[5];               
                $I("divPry").innerHTML = "<input class=txtM id=txtNomProy name=txtNomProy Text='' style='width:400px' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Responsable:</label>" + aResul[7] + "<br><label style='width:70px'>"+$I("lblNodo").value+":</label>"+aResul[10]+"<br><label style='width:70px'>Cliente:</label>" + aResul[9] + "] hideselects=[off]\" />";
	            $I("txtNomProy").value = aResul[3];
                bLimpiarDatos = true;
	            setTimeout("getAE();", 20);
	            buscar1();
                break;
            case "getAE":
		        $I("divAE").innerHTML = aResul[2];
		        $I("divAE").scrollTop = 0;
                break;
            case "getTarea":
		        $I("divDatos").innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
                if (aResul[3] != ""){
                    $I("txtCodProy").value = formatearFloat($I("txtCodProy").value.replace(".",""), 0, true);
                    $I("txtNomProy").value = aResul[3];
                }
                nUltFilaAnt=0;
                scrollTabla();
                titulosTabla = aResul[4].split(",");
                if(titulosTabla.length > 1) reconstruirTabla(titulosTabla);
//		          nNE=1;
//                colorearNE();
                if (!bMostrar) 
                    recolorear();
                //aFila = FilasDe("tblDatos");
                break;
            case "getTareaM":
                if (aResul[4] == "") alert("No hay datos que cumplan los criterios");
                else {
                    if (aResul[2] == "cacheado") {
                        var xls;
                        try {
                            xls = new ActiveXObject("Excel.Application");
                            generaExcelM(aResul[4]);
                        } catch (e) {
                            crearExcelSimpleServerCache(aResul[3]);
                        }
                    }
                    else
                        generaExcelM(aResul[4]);
                }
                if (aResul[5] != ""){
                    $I("txtNumPE").value = formatearFloat($I("txtNumPE").value.replace(".",""), 0, true);
                    $I("txtDesPE").value = aResul[5];
                }
                break;
            case "getProf":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila+1);
                if (!bMostrar) aFila = FilasDe("tblDatos");
                $I("tblDatos").rows[iFila].cells[0].children[0].src = "../../../../images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado","1");
                
                if (bMostrar){
                    setTimeout("MostrarTodo();", 10);
                }
                scrollTabla();
                //if (!bMostrar) 
                    recolorear();
                break;
                
            case "getConsumos":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila+1);
                
                if (!bMostrar) aFila = FilasDe("tblDatos");
                $I("tblDatos").rows[iFila].cells[0].children[0].src = "../../../../images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado", "1");

                if (bMostrar) setTimeout("MostrarTodo();", 10);
                //if (!bMostrar) 
                    recolorear();
                break;
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc","Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("Inf", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                ocultarProcesando();
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 170);
                ocultarProcesando();
                break;
            case "getPreferencia":
                sValorNodo = aResul[2]; 
                if (es_administrador == "A" || es_administrador == "SA") {
                    //$I("hdnIdNodo").value = aResul[2];
                    $I("txtDesNodo").value = aResul[3];
                }
                else {
                    $I("cboCR").value = aResul[2];
                }  
                $I("hdnT305IdProy").value = aResul[4];
                $I("txtCodProy").value = aResul[5];
                $I("txtNomProy").value = aResul[6];
                $I("hdnIdPT").value = aResul[7];
                $I("txtDesPT").value = aResul[8];
                $I("hdnIdFase").value = aResul[9];
                $I("txtFase").value = aResul[10];
                $I("hdnIdActividad").value = aResul[11];
                $I("txtActividad").value = aResul[12];
                $I("txtIdTarea").value = aResul[13];
                $I("txtDesTarea").value = aResul[14];
                $I("hdnCliente").value = aResul[15];
                $I("txtIdCliente").value = aResul[16];
                $I("txtDesCliente").value = aResul[17];
                $I("txtIdPST").value = aResul[18];
                $I("txtCodPST").value = aResul[19];
                $I("txtDesPST").value = aResul[20];
                      
                if (aResul[21] == "S") $I("rdbCodigo_0").checked = true;
                else{
                    if (aResul[21] == "E") $I("rdbCodigo_1").checked = true;
                    else $I("rdbCodigo_2").checked = true;
                }
//                $I("chkEstado_0").checked = (aResul[22] == "1") ? true : false;
//                $I("chkEstado_1").checked = (aResul[23] == "1") ? true : false;
//                $I("chkEstado_2").checked = (aResul[24] == "1") ? true : false;
//                $I("chkEstado_3").checked = (aResul[25] == "1") ? true : false;
//                $I("chkEstado_4").checked = (aResul[26] == "1") ? true : false;
//                $I("chkEstado_5").checked = (aResul[27] == "1") ? true : false;
                //Los posibles estados de la tarea los guardo como una cadena en unico campo de la tabla de preferencias
                var aAux = aResul[22].split("#");
                for (i=0;i<6;i++)
                    $I("chkEstado_"+i).checked = (aAux[i] == "1") ? true : false;
                
                $I("chkActuAuto").checked = (aResul[23] == "1") ? true : false;
                $I("chkCamposLibres").checked = (aResul[24] == "1") ? true : false;
                if ($I("chkActuAuto").checked){
                    setOp($I("btnObtener"), 30);
                    setTimeout("buscar()", 20);
                }else setOp($I("btnObtener"), 100);
                ocultarProcesando();
                break;
            case "setResolucion":
                location.reload(true);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                
        }
        ocultarProcesando();
    }
}
function VerFecha(strM){
    try {
	    if ($I('txtValIni').value.length==10 && $I('txtValFin').value.length==10){
	        var fecha_desde = new Date();
	        fecha_desde.setFullYear($I('txtFechaInicio').value.substr(6,4),$I('txtFechaInicio').value.substr(3,2)-1,$I('txtFechaInicio').value.substr(0,2));
	        var fecha_hasta = new Date();
	        fecha_hasta.setFullYear($I('txtFechaFin').value.substr(6,4),$I('txtFechaFin').value.substr(3,2)-1,$I('txtFechaFin').value.substr(0,2));
            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtFechaInicio').value = $I('txtFechaFin').value;
        }
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}

function buscar1(){
   try{	 
        borrarCatalogo();
        if ($I('chkActuAuto').checked)
            buscar();
        else
            ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al buscar", e.message);
    }        
}
function buscar2(){
   try{	 
        if ($I('rdbFormato_0').checked==true){//a pantalla
            borrarCatalogo();
            buscar();
        }
        else{//Excel masivo
            excelMasivo();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al buscar", e.message);
    }        
}
function buscar(){
   try{	 
//     	if (($I('hdnT305IdProy').value=="")) 
//  	    {
//  	        alert('Debe indicar el proyecto económico.');
//  	        return;
//  	    }
     	if (($I('txtFechaInicio').value=="")||($I('txtFechaFin').value=="")) 
  	    {
  	        ocultarProcesando();
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
        var sAEs="";
        var js_args = "getTarea@#@" + nNE + "@#@";
        js_args += sValorNodo + "@#@"; 
        js_args += $I("hdnT305IdProy").value + "@#@"; 
        js_args += $I("hdnIdPT").value + "@#@";
        js_args += $I("hdnIdFase").value + "@#@";
        js_args += $I("hdnIdActividad").value + "@#@";
        js_args += $I("txtIdTarea").value.replace(".","") + "@#@";
        js_args += Utilidades.escape($I("txtDesTarea").value) + "@#@";
        js_args += $I("txtIdCliente").value + "@#@";
        js_args += $I("txtFechaInicio").value + "@#@";  //fecha desde
        js_args += $I("txtFechaFin").value + "@#@";  //fecha hasta
        
        //Recorro los AE´s. Para cada AE
        //  Si marcado
        //      Si existe VAE marcado -> solo los VAE´s marcados
        //  Sino (AE no marcado) -> solo los VAE´s marcados
        aNombreAE.length = 0;
        var aF=FilasAE("tblVAE");
        var sIdAE="", sAE="";
        var j=0, nIndAEs=0;
        var bPonerAE=false;
        for (var i=0;i<aF.length;i++){
            sIdAE=aF[i].id;
            //sAE=aF[i].cells[1].innerText;
            sAE=aF[i].getAttribute("des");
            bPonerAE=false;
            var aCheck = aF[i].cells[2].getElementsByTagName("INPUT");
            if (aF[i].cells[0].children[0].checked==true){
                bPonerAE=true;
                if (bHayVAEMarcado(aCheck)){
                    for (var x=0; x<aCheck.length;x++){
                        if (aCheck[x].checked==true){
                            if (j>0) js_args += ",";
                            js_args += aCheck[x].value;
                            j++;
                        }
                    }
                }
            }
            else{
                for (var x=0; x<aCheck.length;x++){
                    if (aCheck[x].checked==true){
                        if (j>0) js_args += ",";
                        js_args += aCheck[x].value;
                        j++;
                        bPonerAE=true;
                    }
                }
            }
            if (bPonerAE) aNombreAE[aNombreAE.length] = sAE;
            //Creo una lista con los AEs marcados indicando el id del AE 
            if (aF[i].cells[0].children[0].checked==true){
                if (nIndAEs>0)sAEs += ",";
                nIndAEs++;
                sAEs+=aF[i].id;
            }
        }
        
        borrarCatalogo();
        /*var nWidthTabla = parseInt($I("tblDescDatos").style.width.substring(0, $I("tblDescDatos").style.width.length-2), 10);
        for (var x=0; x<aNombreAE.length;x++){
            nWidthTabla = nWidthTabla + 150;
            var oCelda = $I("tblDescDatos").rows[0].insertCell(-1);
            oCelda.style.width = "150px";
            oCelda.style.textAlign = "left";
            //oCelda.innerText = aNombreAE[x];
            //alert("XXXX"+Utilidades.escape(aNombreAE[x])+"XXXX");

            var oNBR = document.createElement("span");
            oNBR.className = "NBR W140";
            oNBR.title = aNombreAE[x];
            oNBR.appendChild(document.createTextNode(aNombreAE[x]));

            //var oNBR = document.createElement("<span class='NBR W140' title='" + aNombreAE[x] + "'></span>");
            //oNBR.innerText = aNombreAE[x];
            
	        oCelda.appendChild(oNBR);
            
        }
        $I("tblDescDatos").style.width = nWidthTabla + "px";*/
        
        js_args += "@#@"+$I("txtNomProy").value +"@#@";
        
        var strEstado = "";
        for (var i=0; i<6; i++){
            if ($I("chkEstado_"+i).checked){
                if (strEstado == "") strEstado = i;
                else strEstado += ","+ i;
            }
        }
        js_args += strEstado; // Estado
        js_args += "@#@" + sAEs +"@#@";
        js_args += $I("txtIdPST").value +"@#@";
        if ($I("rdbCodigo_0").checked) js_args += "S"; // Código
        else if ($I("rdbCodigo_1").checked) js_args += "E"; // Código
        else js_args += "N";
        if ($I("chkCamposLibres").checked) js_args += "@#@1"; //Mostrar campos libres
        else js_args += "@#@0"; //No mostrar campos libres

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la consulta", e.message);
    }
}

function reconstruirTabla(titulosTabla) {
    var nWidthTabla = parseInt($I("tblDescDatos").style.width.substring(0, $I("tblDescDatos").style.width.length - 2), 10);
    for (var x = 0; x < titulosTabla.length-1; x++) {
        nWidthTabla = nWidthTabla + 120;
        var oCelda = $I("tblDescDatos").rows[0].insertCell(-1);
        oCelda.style.width = "110px";
        oCelda.style.textAlign = "right";
        //oCelda.innerText = aNombreAE[x];
        //alert("XXXX"+Utilidades.escape(aNombreAE[x])+"XXXX");

        var oNBR = document.createElement("span");
        oNBR.className = "NBR W110";
        oNBR.title = titulosTabla[x];
        oNBR.appendChild(document.createTextNode(titulosTabla[x]));

        //var oNBR = document.createElement("<span class='NBR W140' title='" + aNombreAE[x] + "'></span>");
        //oNBR.innerText = aNombreAE[x];

        oCelda.appendChild(oNBR);        
    }
    $I("tblDescDatos").style.width = nWidthTabla + "px";
}


function excelMasivo(){
   try{	 
//     	if (($I('hdnT305IdProy').value=="")) 
//  	    {
//  	        alert('Debe indicar el proyecto económico.');
//  	        return;
//  	    }
     	if (($I('txtFechaInicio').value=="")||($I('txtFechaFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
        var sAEs="";
        var js_args = "getTareaM@#@";
        js_args += sValorNodo + "@#@"; 
        js_args += $I("hdnT305IdProy").value + "@#@"; 
        js_args += $I("hdnIdPT").value + "@#@";
        js_args += $I("hdnIdFase").value + "@#@";
        js_args += $I("hdnIdActividad").value + "@#@";
        js_args += $I("txtIdTarea").value.replace(".","") + "@#@";
        js_args += Utilidades.escape($I("txtDesTarea").value) + "@#@";
        js_args += $I("txtIdCliente").value + "@#@";
        js_args += $I("txtFechaInicio").value + "@#@";  //fecha desde
        js_args += $I("txtFechaFin").value + "@#@";  //fecha hasta
        if ($I("rdbCodigo_0").checked) js_args += "S@#@"; // Código
        else if ($I("rdbCodigo_1").checked) js_args += "E@#@";
        else js_args += "N@#@"; 
        
        //Recorro los AE´s. Para cada AE
        //  Si marcado
        //      Si existe VAE marcado -> solo los VAE´s marcados
        //  Sino (AE no marcado) -> solo los VAE´s marcados
        aNombreAE.length = 0;
        var aF=FilasAE("tblVAE");
        var sIdAE="", sAE="";
        var j=0, nIndAEs=0;
        var bPonerAE=false;
        for (var i=0;i<aF.length;i++){
            sIdAE=aF[i].id;
            //sAE=aF[i].cells[1].innerText;
            sAE = aF[i].getAttribute("des");
            bPonerAE=false;
            var aCheck = aF[i].cells[2].getElementsByTagName("INPUT");
            if (aF[i].cells[0].children[0].checked==true){
                bPonerAE=true;
                if (bHayVAEMarcado(aCheck)){
                    for (var x=0; x<aCheck.length;x++){
                        if (aCheck[x].checked==true){
                            if (j>0) js_args += ",";
                            js_args += aCheck[x].value;
                            j++;
                        }
                    }
                }
            }
            else{
                for (var x=0; x<aCheck.length;x++){
                    if (aCheck[x].checked==true){
                        if (j>0) js_args += ",";
                        js_args += aCheck[x].value;
                        j++;
                        bPonerAE=true;
                    }
                }
            }
            if (bPonerAE) aNombreAE[aNombreAE.length] = sAE;
            //Creo una lista con los AEs marcados indicando el id del AE 
            if (aF[i].cells[0].children[0].checked==true){
                if (nIndAEs>0)sAEs += ",";
                nIndAEs++;
                sAEs+=aF[i].id;
            }
        }
        
        var nWidthTabla = parseInt($I("tblDescDatos").style.width.substring(0, $I("tblDescDatos").style.width.length-2), 10);
        /*for (var x=0; x<aNombreAE.length;x++){
            nWidthTabla = nWidthTabla + 150;
            var oCelda = $I("tblDescDatos").rows[0].insertCell(-1);
            oCelda.style.width = "150px";
            oCelda.style.textAlign = "left";           
            var oNBR = document.createElement("<nobr class='NBR W140' title='"+aNombreAE[x]+"'></nobr>");
            oNBR.innerText = aNombreAE[x];
	        oCelda.appendChild(oNBR);
            
        }*/
        $I("tblDescDatos").style.width = nWidthTabla;
        var nWidthTabla = 960;
        var sColumnasAE = "";
        for (var x=0; x<aNombreAE.length;x++){
            nWidthTabla = nWidthTabla + 110;
            sColumnasAE += "<td>" + aNombreAE[x] + "</td>";
        }

        js_args += "@#@"+$I("txtNomProy").value +"@#@";
        
        var strEstado = "";
        for (var i=0; i<6; i++){
            if ($I("chkEstado_"+i).checked){
                if (strEstado == "") strEstado = i;
                else strEstado += ","+ i;
            }
        }
        js_args += strEstado; // Estado
        js_args += "@#@" + sAEs +"@#@";
        js_args += $I("txtIdPST").value + "@#@";
        js_args += sColumnasAE;
        if ($I("chkCamposLibres").checked) js_args += "@#@1"; //Mostrar campos libres
        else js_args += "@#@0"; //No mostrar campos libres
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la consulta", e.message);
    }
}

function FilasAE(oTabla){
//Obtien una matriz solo con los elementos <tr> correspondientes a Atributos Estadísticos
    try{
        var aFilas =$I(oTabla).getElementsByTagName("TR");
        var aFilas = $I(oTabla).getElementsByTagName("TR");
        var aRes = new Array();
        var j=0;
        for (var i=0;i<aFilas.length;i++){
            if (aFilas[i].getAttribute("ti")==1){
                aRes[j]=aFilas[i];
                j++;
            }
        }
        return aRes;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las filas de '"+ oTabla +"'", e.message);
    }
}
function mVAEs(oFila){
   try{	 
        var aCheck=oFila.cells[2].getElementsByTagName("INPUT");
        for (var i=0; i<aCheck.length;i++){
            aCheck[i].checked=true;
        }
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar valores de atributos estadísticos", e.message);
    }
}
function dVAEs(oFila){
   try{	 
        var aCheck=oFila.cells[2].getElementsByTagName("INPUT");
        for (var i=0; i<aCheck.length;i++){
            aCheck[i].checked=false;
        }
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al desmarcar valores de atributos estadísticos", e.message);
    }
}
function bHayVAEMarcado(aCheck){
   try{	 
        var bRes=false;
        for (var i=0; i<aCheck.length;i++){
            if (aCheck[i].checked==true){
                bRes=true;
                break;
            }
        }
        return bRes;
	}catch(e){
		mostrarErrorAplicacion("Error al verificar valores de atributos estadísticos marcados", e.message);
    }
}
function mostrar(oImg){
    try{
        mostrarProcesando();
        oImgGlobal= oImg;
        setTimeout("mostrarR()", 50);
	}catch(e){
		mostrarErrorAplicacion("Error al verificar valores de atributos estadísticos marcados", e.message);
    }
}
function mostrarR(){
    try{
        //mostrarProcesando();
        
        var oFila = oImgGlobal.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        if (oImgGlobal.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        
        if (nDesplegado == "0"){
            switch (nNivel){
                case "1": //Tarea
                    var js_args = "getProf@#@";
                    js_args += oFila.getAttribute("T") + "@#@"; 
                    js_args += $I("txtFechaInicio").value +"@#@";
                    js_args += $I("txtFechaFin").value +"@#@";
                    if ($I("rdbCodigo_0").checked) js_args += "S@#@"; // Código
                    else if ($I("rdbCodigo_1").checked) js_args += "E@#@"; // Código
                    else js_args += "N@#@"; 
                    js_args += titulosTabla.length - 1; //aNombreAE.length;
                    break;
                case "2": //profesionales
                    var js_args = "getConsumos@#@";
                    js_args += oFila.getAttribute("T") + "@#@";
                    js_args += oFila.getAttribute("R") + "@#@"; 
                    js_args += $I("txtFechaInicio").value +"@#@";
                    js_args += $I("txtFechaFin").value +"@#@";
                    js_args += titulosTabla.length - 1; //aNombreAE.length;
                    break;
            }
            iFila=nIndexFila;
            
            RealizarCallBack(js_args, ""); 
            return;
        }
        
        if (opcion == "O"){ 
            oImgGlobal.src = "../../../../images/plus.gif";
            //scrollTabla2(nIndexFila + 1);
        }
        else 
            oImgGlobal.src = "../../../../images/minus.gif";

        var tblDatos = $I("tblDatos");          
        for (var i=nIndexFila+1; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O")
                {
                    if (tblDatos.rows[i].getAttribute("nivel") == 2) {
                        if (tblDatos.rows[i].cells[0].innerHTML == ""){
                            tblDatos.rows[i].cells[0].appendChild(oImgP5.cloneNode(true), null);
                            if (tblDatos.rows[i].getAttribute("sexo") == "V") {
                                switch (tblDatos.rows[i].getAttribute("tipo")) {
                                    case "E": tblDatos.rows[i].cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                                    case "I": tblDatos.rows[i].cells[1].appendChild(oImgIV.cloneNode(true), null); break;
                                }
                            }else{
                                switch (tblDatos.rows[i].getAttribute("tipo")){
                                    case "E": tblDatos.rows[i].cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                                    case "I": tblDatos.rows[i].cells[1].appendChild(oImgIM.cloneNode(true), null); break;
                                }
                            }
                            if (tblDatos.rows[i].getAttribute("baja") == "1")
                                setOp(tblDatos.rows[i].cells[1].children[0], 20);
                        }
                        else
                            tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    }
                    tblDatos.rows[i].style.display = "none";
                    bScroll = true;
                }
                else if (tblDatos.rows[i].getAttribute("nivel") - 1 == nNivel) {
                    tblDatos.rows[i].style.display = "table-row";
                    bScroll = true;
                }
            }else{
                break;
            }
        }

        if (bMostrar) MostrarTodo(); 
        if (opcion == "O"){ 
            scrollTabla2(nIndexFila + 1);
        }
        
//        else{
//            if (bScroll){
//                scrollTabla();
//            }
//        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar){
    try{
        if ($I("divDatos").innerHTML == "") return;
        var tblDatos = $I("tblDatos"); 
        if (tblDatos.rows.length == 0) return;
        var j = 0;
        if (nMostrar == 0){//Contraer
            for (var i=0; i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].getAttribute("nivel") < 3) 
                    tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                if (tblDatos.rows[i].getAttribute("nivel") > 1) 
                    tblDatos.rows[i].style.display = "none";
//                else 
//                {
//                    if (j % 2 == 0){
//                        tblDatos.rows[i].className = "FA";
//                    }else{
//                        tblDatos.rows[i].className = "FB";
//                    }
//					j++;
//                }                             
            }
            scrollTabla();
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
var bScroll = false;
function MostrarTodo(){
    try {
        var tblDatos = $I("tblDatos"); 
        
        if (tblDatos==null){
            ocultarProcesando();
            return;
        }

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i=nIndiceAux; i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].getAttribute("nivel") < nNE) { 
                if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                    bMostrar=true;
                    nIndiceTodo = i;
                    mostrar(tblDatos.rows[i].cells[0].children[0]);
                    return;
                }
            }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        if (bScroll) scrollTabla();
        bScroll = false;
        ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}
function excel(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var aFila = FilasDe("tblDatos");
        if (aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("<td style='width:400px;'>Tarea / Profesional / Fecha consumo / Comentario</TD>");
        sb.Append("<td>Usuario</td>");

        for (var i=1; i<$I("tblDescDatos").rows[0].cells.length;i++)//para las columnas de los AE.
            sb.Append($I("tblDescDatos").rows[0].cells[i].outerHTML);

		sb.Append("	</TR>");
		//sb.Append("</TABLE>");
        //sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
	    for (var i=0;i < aFila.length; i++){
		    if (aFila[i].style.display == "none") continue;
		    sb.Append("<tr style='vertical-align:top;'>");
            for (var x=2; x<aFila[i].cells.length;x++){
                sb.Append("<td>");
                if (x==2){
                    if (aFila[i].getAttribute("nivel") == "1") {
                        //sb.Append("");
                        sb.Append(aFila[i].cells[x-1].innerText);
                        sb.Append("&nbsp;");
                        sb.Append(aFila[i].cells[x].innerText);
                        sb.Append("<td></td>");
                    } else if (aFila[i].getAttribute("nivel") == "2") {
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        var sDes = "";
                        var sCod = "";
                        var nPos = aFila[i].cells[x].innerText.lastIndexOf("("); //last por si la descripción ya contuviera.
                        if (nPos != -1){ //hay cod. empleado
                            sb.Append(aFila[i].cells[x].innerText.substring(0, nPos) +"</td>");
                            sb.Append("<td>"+ aFila[i].cells[x].innerText.substring(nPos+1, aFila[i].cells[x].innerText.length-1));
                        }else{ //no hay cod. empleado
                            sb.Append(aFila[i].cells[x].innerText);
                            sb.Append("<td></td>");
                        }
                    } else if (aFila[i].getAttribute("nivel") == "3") {
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        sb.Append(aFila[i].cells[x].innerText);
                        sb.Append("<td></td>");
                    }
                }else
                    sb.Append(aFila[i].cells[x].innerText);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function obtenerProyectos(){
    try{
	    var aOpciones;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
	    if (sValorNodo != ""){
	        strEnlace+="&nNodo=" + sValorNodo + "&sNodo=";
	        if (es_administrador == "A" || es_administrador == "SA")
	            strEnlace+= $I("txtDesNodo").value;
	        else
	            strEnlace+=$I("cboCR").options[$I("cboCR").selectedIndex].innerText;
	    }
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                borrarCatalogo();
	                var aDatos = ret.split("///");
	                $I("hdnIdPT").value = "";
	                $I("txtDesPT").value = "";
	                $I("txtIdTarea").value = "";
	                $I("txtDesTarea").value = "";
	                $I("hdnT305IdProy").value = aDatos[0];
	                recuperarDatosPSN();
	            }
	        });
	    window.focus();	    	            
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}

function obtenerPTs(){
    try{
	    var aOpciones, idPE, sPE,idPT, nPSN;
	    idPE    = $I("txtCodProy").value;
	    sPE     = $I("txtNomProy").value;
	    nPSN     = $I("hdnT305IdProy").value;

	    if (nPSN==""){
	        mmoff("Inf", "Para seleccionar un proyecto técnico debe seleccionar\npreviamente un proyecto económico", 350);
	        return;
	    }
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE) + "&nPSN=" + codpar(nPSN);
	    modalDialog.Show(strEnlace, self, sSize(500, 550))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idPT = aOpciones[0];
	                if ($I("hdnIdPT").value != idPT) {
	                    $I("hdnIdPT").value = idPT;
	                    $I("txtIdTarea").value = "";
	                    $I("txtDesTarea").value = "";
	                    borrarCatalogo();
	                }
	                $I("txtDesPT").value = aOpciones[1];
	                buscar1();
	            }
	        });
	    window.focus();	    	            
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}
function obtenerFases(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase;

	    idPT=$I("hdnIdPT").value;
	    idPE=$I("txtCodProy").value;
	    sPE=$I("txtNomProy").value;
	    sPT=$I("txtDesPT").value;

	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto económico", 310);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto técnico", 310);
	        return;
	    }
	    
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Fase/obtenerFase.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT;
	    modalDialog.Show(strEnlace, self, sSize(500, 540))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idFase = aOpciones[0];
	                if ($I("hdnIdFase").value != idFase) {
	                    $I("hdnIdFase").value = idFase;
	                    $I("hdnIdActividad").value = "";
	                    $I("txtActividad").value = "";
	                }
	                $I("txtFase").value = aOpciones[1];
	                buscar1();
	            }
	        });
	    window.focus();	    	            	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las fases", e.message);
    }
}

function obtenerActividades(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase, sFase, idActividad;
	    
	    idPT=$I("hdnIdPT").value;
	    idPE=$I("txtCodProy").value;
	    sPE=$I("txtNomProy").value;
	    sPT=$I("txtDesPT").value;
	    idFase=$I("hdnIdFase").value;
	    sFase=$I("txtFase").value;

	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto económico", 320);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto técnico", 320);
	        return;
	    }
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Actividad/obtenerActividad.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase;
	    modalDialog.Show(strEnlace, self, sSize(500, 560))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idActividad = aOpciones[0];
	                $I("hdnIdActividad").value = idActividad;
	                $I("txtActividad").value = aOpciones[1];
	                buscar1();
	            }
	        });
	    window.focus();	    	            
	    ocultarProcesando();
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las actividades", e.message);
    }
}

function obtenerTareas(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idTarea, sTarea, strEnlace, idFase, sFase, idActividad, sActividad;
	    idPE    = $I("txtCodProy").value;
	    sPE     = $I("txtNomProy").value;
	    idPT    = $I("hdnIdPT").value;
	    sPT     = $I("txtDesPT").value;
	    sTarea  = $I("txtDesTarea").value;
	    idFase  = $I("hdnIdFase").value;
	    sFase   = $I("txtFase").value;
	    idActividad= $I("hdnIdActividad").value;
	    sActividad= $I("txtActividad").value;
	    
	    if (idPE==""){
	        strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea2.aspx?nIdPE=" + $I("hdnT305IdProy").value + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
	    }else{
	    strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea.aspx?nIdPE=" + $I("hdnT305IdProy").value + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
	    }
	    mostrarProcesando();	    
	    modalDialog.Show(strEnlace, self, sSize(500, 580))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idTarea = aOpciones[0];
	                $I("txtIdTarea").value = idTarea;
	                $I("txtDesTarea").value = Utilidades.unescape(aOpciones[1]);
	                $I("txtCodProy").value = aOpciones[2].ToString("N", 9, 0);
	                $I("txtNomProy").value = aOpciones[3];
	                $I("hdnIdPT").value = aOpciones[4];
	                $I("txtDesPT").value = aOpciones[5];
	                if (aOpciones[6] == "0") aOpciones[6] = "";
	                $I("hdnIdFase").value = aOpciones[6];
	                $I("txtFase").value = aOpciones[7];
	                if (aOpciones[8] == "0") aOpciones[8] = "";
	                $I("hdnIdActividad").value = aOpciones[8];
	                $I("txtActividad").value = aOpciones[9];
	                $I("hdnT305IdProy").value = aOpciones[10];
	                buscar1();
	            }
	        });
	    window.focus();	    	            
	    
	    ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al acceder a tareas", e.message);
    }
}
function obtenerCliente(){
    try{
	    var aOpciones;

	    mostrarProcesando();
	    var sPantalla = strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0";
	    modalDialog.Show(sPantalla, self, sSize(600, 480))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                $I("txtIdCliente").value = aOpciones[0];
	                $I("txtDesCliente").value = aOpciones[1];
	                buscar1();
	            }
	        });
	    window.focus();	    	            

        ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function borrarCliente(){
    try{
        $I("txtIdCliente").value = "";
        $I("txtDesCliente").value = "";
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function borrarPE(){
    try{
        $I("hdnT305IdProy").value = "";
        $I("txtCodProy").value = "";
        $I("txtNomProy").value = "";
        $I("txtDesPT").value = "";
        $I("hdnIdPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto económico", e.message);
    }
}

function borrarPT(){
    try{
        $I("txtDesPT").value = "";
        $I("hdnIdPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto técnico", e.message);
    }
}

function borrarFase(){
    try{
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}

function borrarActividad(){
    try{
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}

function borrarTarea(){
    try{
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}

function recolorear(){
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos.rows.length == 0) return;
        
        //var nIndice1 = 0; No recoloreo las filas de tarea pues su color ya me lo da el pijama de la capa 
        var nIndice2 = 0;
        var nIndice3 = 0;
       
        for (var i=0; i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].style.display == "none")  continue;

            switch (tblDatos.rows[i].getAttribute("nivel")) {
                case "1":
//                    if (nIndice1 % 2 == 0){
//                        //tblDatos.rows[i].className = "FA";
//                        tblDatos.rows[i].style.backgroundColor ="#E6EEF2";
//                    }
//                    else{
//                        //tblDatos.rows[i].className = "FB";
//                        tblDatos.rows[i].style.backgroundColor ="#FFFFFF";
//                    }
//                    nIndice1++;
                    nIndice2 = 0;
                    nIndice3 = 0;
                    break;
                case "2":
                    if (nIndice2 % 2 == 0){
                        //tblDatos.rows[i].className = "FAM1";
                        tblDatos.rows[i].style.backgroundColor ="#FFFF84";
                    }
                    else{
                        //tblDatos.rows[i].className = "FAM2";
                        tblDatos.rows[i].style.backgroundColor ="#FFFFC6";
                    }
                    nIndice2++;
                    nIndice3 = 0;
                    break;
                case "3":
                    if (nIndice3 % 2 == 0){
                        //tblDatos.rows[i].className = "FAM3";
                        tblDatos.rows[i].style.backgroundColor ="#E0FCE2";
                    }
                    else{
                        //tblDatos.rows[i].className = "FAM4";
                        tblDatos.rows[i].style.backgroundColor ="#F2FBF3";
                    }
                    nIndice3++;
                    break;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al recolorear las filas de la tabla", e.message);
    }
}

function mostrarOTC(){
    try{
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerPST.aspx?sIdCli=" + $I("txtIdCliente").value + "&nCR=" + sValorNodo;
	    modalDialog.Show(strEnlace, self, sSize(940, 500))
            .then(function(ret) {
	            if (ret != null) {
	                var aOpciones = ret.split("@#@");
	                $I('txtIdPST').value = aOpciones[0];
	                $I('txtCodPST').value = aOpciones[1];
	                $I('txtDesPST').value = aOpciones[2];
	                $I('txtDesPST').title = aOpciones[2];
	                buscar1();
	            }
	        });
	    window.focus();	    	            

	    ocultarProcesando();
	}catch(e){
		ocultarprocesando();
		mostrarErrorAplicacion("Error al mostrar las órdenes de trabajo codificadas", e.message);
	}
}

function limpiarPST(){
    try{
        $I('txtCodPST').value="";
        $I('txtDesPST').value="";
        $I('txtDesPST').title="";
        $I('txtIdPST').value="";	
        buscar1();
    }catch(e){
		mostrarErrorAplicacion("Error al limpiar los valores de la PST", e.message);
	}
}
function getAE(){
    try{
        var js_args = "getAE@#@" + sValorNodo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos de los atributos estadísticos.", e.message);
    }
}
function buscarPE(){
    try{
        $I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtCodProy").value);
        setNumPE();
        //alert(js_args);     
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
            $I("imgEstProy").title = "";
            $I("divPry").innerHTML = "<input type='text' class='txtM' id='txtNomProy' value='' style='width:400px;' readonly='true' />";
            $I("hdnIdPT").value = "";
            $I("txtDesPT").value = "";
            $I("txtIdTarea").value = "";
            $I("txtDesTarea").value = "";
            borrarCatalogo();
	            
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}
function getPEByNum(){
    try{    
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&nPE=" + dfn($I("txtCodProy").value);
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    recuperarDatosPSN();
                }
            });
        window.focus();

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}
function recuperarDatosPSN(){
    try{
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        js_args += $I("hdnT305IdProy").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
var nNE = 1;
function setNE(nValor){
    try{
        nNE = nValor;
        colorearNE();
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }
//        mostrarProcesando();
//        nNE = nValor;
//        colorearNE();
//        MostrarOcultar(0);
//        if (nNE > 1) MostrarOcultar(1);
//        //recolorear();
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
                $I("imgNE3").src = "../../../../images/imgNE3off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../../images/imgNE3off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../../images/imgNE3on.gif";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

var oImgEM = document.createElement("img");
oImgEM.setAttribute("src", "../../../../images/imgUsuEM.gif");
oImgEM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgIM = document.createElement("img");
oImgIM.setAttribute("src", "../../../../images/imgUsuIM.gif");
oImgIM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgEV = document.createElement("img");
oImgEV.setAttribute("src", "../../../../images/imgUsuEV.gif");
oImgEV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgIV = document.createElement("img");
oImgIV.setAttribute("src", "../../../../images/imgUsuIV.gif");
oImgIV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgP = document.createElement("img");
oImgP.setAttribute("src", "../../../../images/plus.gif");
oImgP.setAttribute("style", "width:9px; height:9px;");
oImgP.onclick = function() { mostrar(this) };
oImgP.className = "ICO";

var oImgM = document.createElement("img");
oImgM.setAttribute("src", "../../../../images/minus.gif");
oImgM.setAttribute("style", "width:9px; height:9px;");
oImgM.onclick = function() { mostrar(this) };
oImgM.className = "ICO";

var oImgP5 = document.createElement("img");
oImgP5.setAttribute("src", "../../../../images/plus.gif");
oImgP5.setAttribute("style", "width:9px; height:9px;margin-left:10px;");
oImgP5.onclick = function() { mostrar(this) };
oImgP5.className = "ICO";

var oImgM5 = document.createElement("img");
oImgM5.setAttribute("src", "../../../../images/minus.gif");
oImgM5.setAttribute("style", "width:9px; height:9px;margin-left:10px;");
oImgM5.onclick = function() { mostrar(this) };
oImgM5.className = "ICO";

var nTopScroll = -1;
var nIDTime = 0;
function borrarCatalogo(){
    try{
        oDivTitulo.scrollLeft = 0;
        $I("divCatalogo").scrollLeft = 0;
        var nCeldas = $I("tblDescDatos").rows[0].cells.length;
        for (var i=nCeldas-1; i>8; i--) 
            $I("tblDescDatos").rows[0].deleteCell(i);
        if (bRes1024) 
            $I("tblDescDatos").style.width = "980px";
        else
            $I("tblDescDatos").style.width = "1160px";
        $I("divDatos").innerHTML = "<table id='tblDatos'></table>";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function scrollTabla(e){
    try{
        if ($I("divCatalogo").scrollTop != nTopScroll){
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
//        if (event != null){
//            if (oDivTitulo.scrollLeft != event.srcElement.scrollLeft){
//                oDivTitulo.scrollLeft = event.srcElement.scrollLeft;
//                return;
//            }
        //        }
        //if (!e) e = event; 
        if (e != null) {
            if (oDivTitulo.scrollLeft != $I("divCatalogo").scrollLeft) {
                oDivTitulo.scrollLeft = $I("divCatalogo").scrollLeft;
                return;
            }
        }
//        var nFilaVisible = Math.floor(nTopScroll/18);
        var nFilaVisible = nUltFilaAnt
//        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/18 +1, tblDatos.rows.length);
        var oFila;
        var j=0, i=0;
        //for (var i = nFilaVisible; i < nUltFila; i++){
        var tblDatos = $I("tblDatos");
        
        for (i = nFilaVisible; i < tblDatos.rows.length; i++){
            //if (!tblDatos.rows[i].sw && tblDatos.rows[i].style.display != "none"){
            if (tblDatos.rows[i].style.display != "none") {//&& (tblDatos.rows[i].getAttribute("nivel") !=3)
                j++;
                if (j > 30)
                    break;
                oFila = tblDatos.rows[i];
                if (oFila.getAttribute("sw") == 0) {
                    nUltFilaAnt=i;
                    oFila.setAttribute("sw", 1);
                    
                    switch (nNE)
                    {
                        case 1:
                            if (oFila.getAttribute("nivel") == 1) {
                                var oImg = oImgP.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);                                
                            }
                            else {
                                if (oFila.getAttribute("nivel") == 2) {
                                    var oImg = oImgP5.cloneNode(true);
                                    oImg.onclick = function() { mostrar(this) };
                                    oFila.cells[0].appendChild(oImg, null);
                                }
                            }
                            break;
                        case 2:
                            if (oFila.getAttribute("nivel") == 1) {
                                var oImg = oImgM.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);
                            }
                            else {
                                var oImg = oImgP5.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);
                            }
                            break;
                        case 3:
                            if (oFila.getAttribute("nivel") == 1) {
                                var oImg = oImgM.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);
                            } 
                            else {
                                if (oFila.getAttribute("nivel") == 2) {
                                    var oImg = oImgM5.cloneNode(true);
                                    oImg.onclick = function() { mostrar(this) };
                                    oFila.cells[0].appendChild(oImg, null);
                                }
                            }
                            break;
                    }

                    if (oFila.getAttribute("nivel") == 2) {//profesionales
                        if (oFila.getAttribute("sexo")=="V"){
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                                case "I": oFila.cells[1].appendChild(oImgIV.cloneNode(true), null); break;
                            }
                        }else{
                        switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                                case "I": oFila.cells[1].appendChild(oImgIM.cloneNode(true), null); break;
                            }
                        }
                        if (oFila.getAttribute("baja") == "1")
                            setOp(oFila.cells[1].children[0], 20);
                    }
                }       
                else{
                    if (oFila.getAttribute("nivel") == 2) {//profesionales
                        if (oFila.getAttribute("baja") == "1")
                            setOp(oFila.cells[1].children[0], 20);
                    }
                }       
            }
        }
        
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
function scrollTabla2(nFilaVisible){
    try{
        if (event != null){
            if (oDivTitulo.scrollLeft != event.srcElement.scrollLeft){
                oDivTitulo.scrollLeft = event.srcElement.scrollLeft;
                return;
            }
        }
        var oFila;
        var j = 0;
        var tblDatos = $I("tblDatos");
        for (var i = nFilaVisible; i < tblDatos.rows.length; i++){
            if (tblDatos.rows[i].style.display != "none"){// && tblDatos.rows[i].nivel != 3
                j++;
                if (j > 30)
                    break;
                if (tblDatos.rows[i].getAttribute("sw") == 0) {
                    oFila = tblDatos.rows[i];
                    oFila.setAttribute("sw", 1);
                    switch (nNE) {
                        case 1:
                            if (oFila.getAttribute("nivel") == 1) {
                                var oImg = oImgP.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);
                            }
                            else {
                                if (oFila.getAttribute("nivel") == 2) {
                                    var oImg = oImgP5.cloneNode(true);
                                    oImg.onclick = function() { mostrar(this) };
                                    oFila.cells[0].appendChild(oImg, null);
                                }
                            }
                            break;
                        case 2:
                            if (oFila.getAttribute("nivel") == 1) {
                                var oImg = oImgM.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);
                            }
                            else {
                                var oImg = oImgP5.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);
                            }
                            break;
                        case 3:
                            if (oFila.getAttribute("nivel") == 1) {
                                var oImg = oImgM.cloneNode(true);
                                oImg.onclick = function() { mostrar(this) };
                                oFila.cells[0].appendChild(oImg, null);
                            }
                            else {
                                if (oFila.getAttribute("nivel") == 2) {
                                    var oImg = oImgM5.cloneNode(true);
                                    oImg.onclick = function() { mostrar(this) };
                                    oFila.cells[0].appendChild(oImg, null);
                                }
                            }
                            break;
                    }   
                    /*                 
                    switch (nNE)
                    {
                        case 1:
                            if (oFila.getAttribute("nivel") == 1) {
                                oFila.cells[0].appendChild(oImgP.cloneNode(true), null);
                                oImgP.onclick = function() { mostrar(this) };
                            } else {
                                if (oFila.getAttribute("nivel") == 2) {
                                    oFila.cells[0].appendChild(oImgP5.cloneNode(true), null);
                                    oImgP5.onclick = function() { mostrar(this) };
                                }
                            }
                            break;
                        case 2:
                            if (oFila.getAttribute("nivel") == 1) 
                            {
                                oFila.cells[0].appendChild(oImgM.cloneNode(true), null);
                                oImgM.onclick = function() { mostrar(this) };
                            }
                            else {
                                oFila.cells[0].appendChild(oImgP5.cloneNode(true), null);
                                oImgP5.onclick = function() { mostrar(this) };
                            }
                            break;
                        case 3:
                            if (oFila.getAttribute("nivel") == 1) {
                                oFila.cells[0].appendChild(oImgM.cloneNode(true), null);
                                oImgM.onclick = function() { mostrar(this) };
                            }
                            else {
                                if (oFila.getAttribute("nivel") == 2) {
                                    oFila.cells[0].appendChild(oImgM5.cloneNode(true), null);
                                    oImgM5.onclick = function() { mostrar(this) };
                                }
                            }
                            break;
                    }
                    */
                    if (oFila.getAttribute("nivel") == 2) {//profesionales
                        if (oFila.getAttribute("sexo") == "V") {
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                                case "I": oFila.cells[1].appendChild(oImgIV.cloneNode(true), null); break;
                            }
                        }else{
                        switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                                case "I": oFila.cells[1].appendChild(oImgIM.cloneNode(true), null); break;
                            }
                        }
                        if (oFila.getAttribute("baja") == "1")
                            setOp(oFila.cells[1].children[0], 20);
                    }
                }        
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll2 de la tabla.", e.message);
    }
}

function setCombo(){ 
    try{
        $I("hdnT305IdProy").value = "";
        $I("txtCodProy").value = "";
        $I("txtNomProy").value = "";
        $I("hdnIdPT").value = "";
        $I("txtDesPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        //Si solo tengo un nodo accesible ya se habrá cargado al cargar la pantalla, por lo que no merece la pena recargarlo
        if (sValorNodo != "" && $I("hdnNumNodos").value != 1) getAE();//sValorNodo = $I("cboCR").value;
        setTimeout("buscar1()", 50);
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}
function getNodo(){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    setTimeout("getAE();", 20);
                    buscar1();
                }
            });
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener el "+ strEstructuraNodo, e.message);
    }
}

function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("txtDesNodo").value = "";
        }
        else {
            $I("cboCR").value = "";
        }  
        //$I("hdnIdNodo").value = "";      
        sValorNodo = "";
        
        //$I("divCatalogo").innerHTML = "";
        $I("divAE").innerHTML = "<table id='tblVAE'></table>";
        $I("hdnT305IdProy").value = "";
        $I("txtCodProy").value = "";
        $I("txtNomProy").value = "";
        $I("txtDesPT").value = "";
        $I("hdnIdPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        buscar1();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el "+ strEstructuraNodo, e.message);
    }
}
//function setActuAuto(){
//    try{
//        if ($I("chkActuAuto").checked){
//            setOp($I("btnObtener"), 30);
//            buscar();
//        }else{
//            setOp($I("btnObtener"), 100);
//        }

//	}catch(e){
//		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
//	}
//}

////PREFERENCIAS///////
function setPreferencia(){
    try{
        mostrarProcesando();
        var js_args = "setPreferencia@#@";
        js_args += sValorNodo+"@#@";
        js_args += $I("hdnT305IdProy").value +"@#@";
        js_args += $I("hdnIdPT").value +"@#@";
        js_args += $I("hdnIdFase").value +"@#@";
        js_args += $I("hdnIdActividad").value +"@#@";
        js_args += $I("txtIdTarea").value.replace(".","") +"@#@";
        js_args += $I("txtIdCliente").value.replace(".","") +"@#@";
        js_args += $I("txtIdPST").value +"@#@";
        js_args += getRadioButtonSelectedValue("rdbCodigo", true) +"@#@";
        for (i=0;i<6;i++){
            js_args += ($I("chkEstado_"+i).checked)? "1#":"0#";   
        }
        js_args += ($I("chkActuAuto").checked) ? "@#@1" : "@#@0";
        js_args += ($I("chkCamposLibres").checked) ? "@#@1" : "@#@0";
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function getCatalogoPreferencias(){
    try{
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia);
        modalDialog.Show(sPantalla, self, sSize(450, 470))
            .then(function(ret) {
                if (ret != null) {
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
                }
            });
        window.focus();
        
	    ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}
///////////////////////
function generaExcelM(sHtml){
    try{
        if (sHtml==""){
            ocultarProcesando();
            return;
        }
        var sb = new StringBuilder;
        /*sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("<td style='width:300px;'>Proyecto Económico</TD>");
        sb.Append("<td style='width:300px;'>Proyecto Técnico</TD>");
        sb.Append("<td style='width:300px;'>Tarea</TD>");
        sb.Append("<td style='width:300px;'>Profesional</TD>");
        sb.Append("<td style='width:70px;'>F. consumo</TD>");
        sb.Append("<td style='width:300px;'>Comentario</TD>");
        sb.Append("<td style='width:200px;'>Código OTC</TD>");
        sb.Append("<td style='width:400px;'>Denominación OTC</TD>");
        sb.Append("<td style='width:200px;'>OTL</TD>");
        sb.Append("<td style='width:60px;'>Estado</td>");
        sb.Append("<td style='width:75px;'>ETPL</td>");
        sb.Append("<td style='width:75px;'>ETPR</td>");
        sb.Append("<td style='width:60px;'>FFPR</td>");
        sb.Append("<td style='width:75px;'>Horas</td>");
        sb.Append("<td style='width:75px;'>Jornadas</td>");
        for (var i=0;i<aNombreAE.length;i++)//para las columnas de los AE.
            sb.Append("<td>" + aNombreAE[i] + "</td>");
		sb.Append("</TR>");	*/
		//Inserto todas las filas que ha devuelto la consulta
		sb.Append(sHtml);
		sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
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
        $I("col4").style.width = "390px";
        $I("tblCriterios").style.width = "980px";
        $I("flsCriterios").style.width = "980px";
        $I("tblGral").style.width = "990px";

        $I("divAE").style.width = "386px";
        $I("tblCabAE").style.width = "370px";
        $I("tblCabAE").children[0].children[0].style.width = "210px";
        $I("tblCabAE").rows[0].cells[0].style.width = "210px";  
        $I("tblPieAE").style.width = "370px";
        $I("tblPieAE").children[0].children[0].style.width = "370px";
        
        $I("divTablaTitulo").style.width = "980px";
        $I("tblDescDatos").style.width = "980px";
        $I("tblDescDatos").children[0].children[0].style.width = "370px";
        $I("tblDescDatos").rows[0].cells[0].style.width = "370px";
        $I("tblDescDatos").children[0].children[1].style.width = "85px";
        $I("tblDescDatos").rows[0].cells[1].style.width = "85px";
        $I("tblDescDatos").children[0].children[2].style.width = "85px";
        $I("tblDescDatos").rows[0].cells[2].style.width = "85px";
        $I("tblDescDatos").children[0].children[3].style.width = "65px";
        $I("tblDescDatos").rows[0].cells[3].style.width = "65px";        

        $I("divCatalogo").style.height = "240px";
        $I("divCatalogo").style.width = "996px";
        $I("tblResultado").style.width = "980px";
        
    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}
