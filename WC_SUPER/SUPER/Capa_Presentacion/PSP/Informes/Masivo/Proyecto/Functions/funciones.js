var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var bCargandoCriterios=false;
var js_subnodos = new Array();

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 440;
var nTopPestana = 98;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

function init(){
    try{  
        setOperadorLogico(false);
     
        if (bHayPreferencia){
            //if (!!tblDatos) 
            if ($I("tblDatos") != null)
                scrollTablaProy();
        }else mostrarOcultarPestVertical();

        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != ""){
            slValores=fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
                window.focus();
                swBuscarProy = "1";
                break;
            case "getExcel":
                //Excel(aResul[2]);
                //bOcultarProcesando = false;

                if (aResul[2] == "cacheado") {
                    var xls;
                    try {
                        xls = new ActiveXObject("Excel.Application");
                        crearExcel(aResul[4]);
                    } catch (e) {
                        crearExcelSimpleServerCache(aResul[3]);
                    }
                }
                else
                    crearExcel(aResul[2]);
                break;                
           case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc","Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("Inf", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 170);
                break;
            case "getPreferencia":
                $I("cboEstado").value = aResul[8];  
                $I("cboCategoria").value = aResul[3];  //2  +1
                $I("cboCualidad").value = aResul[4];
                $I("chkCerrarAuto").checked = (aResul[5]=="1")? true:false;
                $I("chkActuAuto").checked = (aResul[6]=="1")? true:false;
                nUtilidadPeriodo = parseInt(aResul[8], 10);
                $I("hdnDesde").value = aResul[10];
                $I("txtFechaInicio").value = aResul[11];
                $I("hdnHasta").value = aResul[12];
                $I("txtFechaFin").value = aResul[13];
                //aResul[14] //la opción se determinará al buscar
                js_subnodos.length = 0;
                js_subnodos = aResul[14].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[15], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[17], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[19], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[21], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblHorizontal");
                insertarFilasEnTablaDOM("tblHorizontal", aResul[23], 0);
                $I("divHorizontal").scrollTop = 0;

                BorrarFilasDe("tblSector");
                insertarFilasEnTablaDOM("tblSector", aResul[25], 0);
                $I("divSector").scrollTop = 0;

                BorrarFilasDe("tblSegmento");
                insertarFilasEnTablaDOM("tblSegmento", aResul[27], 0);
                $I("divSegmento").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[29], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[31], 0);
                $I("divContrato").scrollTop = 0;

                BorrarFilasDe("tblQn");
                insertarFilasEnTablaDOM("tblQn", aResul[33], 0);
                $I("divQn").scrollTop = 0;

                BorrarFilasDe("tblQ1");
                insertarFilasEnTablaDOM("tblQ1", aResul[35], 0);
                $I("divQ1").scrollTop = 0;

                BorrarFilasDe("tblQ2");
                insertarFilasEnTablaDOM("tblQ2", aResul[37], 0);
                $I("divQ2").scrollTop = 0;

                BorrarFilasDe("tblQ3");
                insertarFilasEnTablaDOM("tblQ3", aResul[39], 0);
                $I("divQ3").scrollTop = 0;

                BorrarFilasDe("tblQ4");
                insertarFilasEnTablaDOM("tblQ4", aResul[41], 0);
                $I("divQ4").scrollTop = 0;
                
                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[43], 0);
                $I("divProyecto").scrollTop = 0;
                
                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[7]=="1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;
                setTodos();
                
                if ($I("chkActuAuto").checked)
                    setTimeout("buscar();", 20);
                break;            
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
function obtenerDatosExcel()
{
    try
    {   
        var strCadena = "";
        var fecha_desde = new Date();
        
        fecha_desde.setFullYear($I('txtFechaInicio').value.substr(6,4),$I('txtFechaInicio').value.substr(3,2)-1,$I('txtFechaInicio').value.substr(0,2));
        var fecha_hasta = new Date();
        fecha_hasta.setFullYear($I('txtFechaFin').value.substr(6,4),$I('txtFechaFin').value.substr(3,2)-1,$I('txtFechaFin').value.substr(0,2));
        if (fecha_desde > fecha_hasta || $I('txtFechaInicio').value == "")
        {
            mmoff("Inf", "Rango de fechas incorrecto o debes indicar las fechas.", 370);
	        return false;
        }

        objTabla = $I('tblDatos2');
        var sb = new StringBuilder;
        
        for (i=0;i<objTabla.rows.length;i++)	    
        {
            sb.Append(objTabla.rows[i].getAttribute("id") + ",");
        }
        
        var strCadena = sb.ToString();
	    if (strCadena=="")
	    {
	        mmoff("Inf", "Debes seleccionar algún proyecto.", 230);
	        return;
        }       
	    else
	        strCadena = strCadena.substring(0, strCadena.length - 1);

        var js_args = "getExcel@#@";
        js_args += $I('txtFechaInicio').value+ "@#@";
        js_args += $I('txtFechaFin').value+ "@#@";
        js_args += strCadena;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al preparar los datos para Excel", e.message);
    }	    
}

function Excel(sHTML) {
    try {
        //alert(sHTML);
        crearExcel(sHTML);
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function Excel_New() {
    try {
        //aValorGanado
        token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/Documentos/getDocOffice.aspx?";
        strEnlace += "descargaToken=" + token;
        strEnlace += "&sOp=" + codpar("CONSPROYMASIVO");

        mostrarProcesando();
        initDownload();
        $I("iFrmDescarga").src = strEnlace;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function setCombo() {
    try{
        borrarCatalogo();
        if ($I("chkActuAuto").checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la función setCombo ", e.message);
    }
}

function buscar(){
    try{
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("hdnDesde").value +"@#@");
        sb.Append($I("hdnHasta").value +"@#@");
        sb.Append("7@#@");  //nNivelEstructura //subnodos
        sb.Append($I("cboEstado").value +"@#@");
        sb.Append($I("cboCategoria").value +"@#@");
        sb.Append($I("cboCualidad").value +"@#@");
        sb.Append(getDatosTabla(16)+ "@#@"); //Proyectos		
        sb.Append(getDatosTabla(8)+ "@#@"); //Clientes
        sb.Append(getDatosTabla(2)+ "@#@"); //Responsable
        sb.Append(getDatosTabla(3)+ "@#@"); //Naturaleza
        sb.Append(getDatosTabla(5)+ "@#@"); //Horizontal
        sb.Append(getDatosTabla(4)+ "@#@"); //ModeloCon
        sb.Append(getDatosTabla(9)+ "@#@"); //Contrato
        sb.Append(js_subnodos.join(",")+ "@#@"); //ids estructura ambito
        sb.Append(getDatosTabla(6)+ "@#@"); //Sector
        sb.Append(getDatosTabla(7)+ "@#@"); //Segmento
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false)+ "@#@"); //Operador lógico
        sb.Append(getDatosTabla(10)+ "@#@"); //CNP
        sb.Append(getDatosTabla(11)+ "@#@"); //CSN1P
        sb.Append(getDatosTabla(12)+ "@#@"); //CSN2P
        sb.Append(getDatosTabla(13)+ "@#@"); //CSN3P
        sb.Append(getDatosTabla(14)+ "@#@"); //CSN4P
       
        if ($I("chkCerrarAuto").checked){
            bPestRetrMostrada = true;
            mostrarOcultarPestVertical();
        }
       
        RealizarCallBack(sb.ToString(), "");
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function getDatosTabla(nTipo){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo)
        {
            case 1: oTabla = $I("tblAmbito"); break;
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 5: oTabla = $I("tblHorizontal"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 10: oTabla = $I("tblQn"); break;
            case 11: oTabla = $I("tblQ1"); break;
            case 12: oTabla = $I("tblQ2"); break;
            case 13: oTabla = $I("tblQ3"); break;
            case 14: oTabla = $I("tblQ4"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }
        
        for (var i=0; i<oTabla.rows.length;i++){
            if (oTabla.rows[i].id == "-999") continue;
            if (i>0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }
        
        if (sb.ToString().length > 8000)
        {
            ocultarProcesando();
            switch (nTipo)
            {
                //case 1: break;
                case 2: mmoff("Inf", "Has seleccionado un número excesivo de responsables de proyecto.", 500); break;
                case 3: mmoff("Inf", "Has seleccionado un número excesivo de naturalezas.", 450); break;
                case 4: mmoff("Inf", "Has seleccionado un número excesivo de modelos de contratación.", 500); break;
                case 5: mmoff("Inf", "Has seleccionado un número excesivo de horizontales.", 450); break;
                case 6: mmoff("Inf", "Has seleccionado un número excesivo de sectores.", 450); break;
                case 7: mmoff("Inf", "Has seleccionado un número excesivo de segmentos.", 450); break;
                case 8: mmoff("Inf", "Has seleccionado un número excesivo de clientes.", 450); break;
                case 9: mmoff("Inf", "Has seleccionado un número excesivo de contratos.", 450); break;
                case 10: mmoff("Inf", "Has seleccionado un número excesivo de Qn.", 400); break;
                case 11: mmoff("Inf", "Has seleccionado un número excesivo de Q1.", 400); break;
                case 12: mmoff("Inf", "Has seleccionado un número excesivo de Q2.", 400); break;
                case 13: mmoff("Inf", "Has seleccionado un número excesivo de Q3.", 400); break;
                case 14: mmoff("Inf", "Has seleccionado un número excesivo de Q4.", 400); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos.", 450); break;
            }
            return;   
		}
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}

function mdpsn(oNOBR){
    try{
        insertarItem(oNOBR.parentNode.parentNode);
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar proyecto", e.message);
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


        var oTable = $I("tblDatos2");
        var sNuevo = oFila.innerText;
         for (var iFilaNueva=0; iFilaNueva < oTable.rows.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual=oTable.rows[iFilaNueva].innerText;
            if (sActual>sNuevo)break;
        }

        // Se inserta la fila
        
        var oNF = oTable.insertRow(iFilaNueva);

        var oCloneNode	= oFila.cloneNode(true);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        oCloneNode.style.height = "20px";
        oCloneNode.style.cursor = strCurMM;
        oCloneNode.className = "";
        oCloneNode.setAttribute("id", oFila.getAttribute("id"));
		oNF.swapNode(oCloneNode);

		//oCloneNode.cells[0].className = "MM";
						
 		actualizarLupas("tblAsignados", "tblDatos2");
       
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}
/*
var oImgProducto = document.createElement("img");
oImgProducto.setAttribute("src", "../../../../../images/imgProducto.gif");
oImgProducto.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgServicio = document.createElement("img");
oImgServicio.setAttribute("src", "../../../../../images/imgServicio.gif");
oImgServicio.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgContratante = document.createElement("img");
oImgContratante.setAttribute("src", "../../../../../images/imgIconoContratante.gif");
oImgContratante.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgRepJor = document.createElement("img");
oImgRepJor.setAttribute("src", "../../../../../images/imgIconoRepJor.gif");
oImgRepJor.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgRepPrecio = document.createElement("img");
oImgRepPrecio.setAttribute("src", "../../../../../images/imgIconoRepPrecio.gif");
oImgRepPrecio.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgAbierto = document.createElement("img");
oImgAbierto.setAttribute("src", "../../../../../images/imgIconoProyAbierto.gif");
oImgAbierto.setAttribute("title", "Proyecto abierto");
oImgAbierto.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgCerrado = document.createElement("img");
oImgCerrado.setAttribute("src", "../../../../../images/imgIconoProyCerrado.gif");
oImgCerrado.setAttribute("title", "Proyecto cerrado");
oImgCerrado.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgHistorico = document.createElement("img");
oImgHistorico.setAttribute("src", "../../../../../images/imgIconoProyHistorico.gif");
oImgHistorico.setAttribute("title", "Proyecto histórico");
oImgHistorico.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgPresup = document.createElement("img");
oImgPresup.setAttribute("src", "../../../../../images/imgIconoProyPresup.gif");
oImgPresup.setAttribute("title", "Proyecto presupuestado");
oImgPresup.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
*/

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
        clearTimeout(nIDTimeProy);
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                oFila.ondblclick = function() { insertarItem(this) };
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
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
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20 + 1, tblDatos2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos2.rows[i].getAttribute("sw")) {
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
                oFila.style.cursor = strCurMM;
				oFila.className = "";
				//oFila.cells[3].className = "MM";
				//oFila.cells[4].children[0].className = "MM";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos asignados.", e.message);
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
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {

                        var oNF;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;

                        var oCloneNode = oRow.cloneNode(true);
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        oNF.swapNode(oCloneNode);
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
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

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}

function getCriterios(nTipo){
    try{
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == ""){
            nCriterioAVisualizar = nTipo;
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
            return;
        }

        nCriterioAVisualizar = 0;
        mostrarProcesando();
        
        var nCC = 0; //ncount de criterios.
        var bExcede = false;
        for (var i=0; i<js_cri.length; i++)
        {
            if (js_cri[i].t > nTipo) break;
            if (js_cri[i].t < nTipo) continue;
            if (typeof(js_cri[i].excede)!="undefined"){
                bExcede = true;
                break;
            }
        }

        if (es_administrador != "" || bExcede) bCargarCriterios = false;
        else bCargarCriterios = true;

        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(850, 400);
        
        var strEnlace = "";
        switch (nTipo){
            case 1:               
                if (bCargarCriterios){
                    for (var i=0; i<js_cri.length; i++)
                    {
                        if (js_cri[i].t > 1) break;
                        if (i==0) sSubnodos = js_cri[i].c;
                        else sSubnodos += ","+js_cri[i].c;
                    }
                }
                //strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sSnds=" + codpar(sSubnodos) + "&sExcede=" + ((bExcede) ? "T" : "F");
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sOrigen=PST&sExcede=" + ((bExcede) ? "T" : "F");
                sTamano = sSize(950, 450);
                break;         
            case 16:  

                if (bCargarCriterios){
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioProyecto/Default.aspx?nTipo=" + nTipo + "&sMod=pst";
                    sTamano = sSize(1010, 570);  
                }
                else{
                    strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pst";
                    sTamano = sSize(1010, 720);  
                }
                break;
            default:
                if (bCargarCriterios) 
                {
                    sTamano = sSize(850, 460);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterio/Default.aspx?nTipo=" + nTipo;
                }
                else 
                {
                    sTamano = sSize(850, 420);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
                }                
                break;
                break;
        }   
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo){
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 5: oTabla = $I("tblHorizontal"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 10: oTabla = $I("tblQn"); break;
            case 11: oTabla = $I("tblQ1"); break;
            case 12: oTabla = $I("tblQ2"); break;
            case 13: oTabla = $I("tblQ3"); break;
            case 14: oTabla = $I("tblQ4"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }
        if (nTipo != 1){
            slValores=fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
            if (ret != null) {
                var aElementos = ret.split("///");
                switch (nTipo) {
                    case 1:
                        nNivelEstructura = parseInt(aElementos[0], 10);
                        nNivelSeleccionado = parseInt(aElementos[0], 10);
                        BorrarFilasDe("tblAmbito");
                        //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
                        for (var i = 1; i < aElementos.length; i++) {
                            if (aElementos[i] == "") continue;
                            var aDatos = aElementos[i].split("@#@");
                            var oNF = tblAmbito.insertRow(-1);
                            oNF.style.height = "16px";
                            oNF.setAttribute("tipo", aDatos[0]);
                            var aID = aDatos[1].split("-");
                            switch (parseInt(oNF.getAttribute("tipo"), 10)) {
                                case 1:
                                    oNF.insertCell(-1).appendChild(oImgSN4.cloneNode(true), null);
                                    oNF.id = aID[0];
                                    break;
                                case 2:
                                    oNF.insertCell(-1).appendChild(oImgSN3.cloneNode(true), null);
                                    oNF.id = aID[1];
                                    break;
                                case 3:
                                    oNF.insertCell(-1).appendChild(oImgSN2.cloneNode(true), null);
                                    oNF.id = aID[2];
                                    break;
                                case 4:
                                    oNF.insertCell(-1).appendChild(oImgSN1.cloneNode(true), null);
                                    oNF.id = aID[3];
                                    break;
                                case 5:
                                    oNF.insertCell(-1).appendChild(oImgNodo.cloneNode(true), null);
                                    oNF.id = aID[4];
                                    break;
                                case 6:
                                    oNF.insertCell(-1).appendChild(oImgSubNodo.cloneNode(true), null);
                                    oNF.id = aID[5];
                                    break;
                            }
                            var oCtrl1 = document.createElement("div");
                            oCtrl1.className = "NBR W230";
                            oCtrl1.attachEvent('onmouseover', TTip);

                            oNF.cells[0].appendChild(oCtrl1);
                            oNF.cells[0].children[1].innerHTML = Utilidades.unescape(aDatos[2]);
                        }
                        divAmbito.scrollTop = 0;
                        break;
                    case 2: insertarTabla(aElementos, "tblResponsable"); break;
                    case 3: insertarTabla(aElementos, "tblNaturaleza"); break;
                    case 4: insertarTabla(aElementos, "tblModeloCon"); break;
                    case 5: insertarTabla(aElementos, "tblHorizontal"); break;
                    case 6: insertarTabla(aElementos, "tblSector"); break;
                    case 7: insertarTabla(aElementos, "tblSegmento"); break;
                    case 8: insertarTabla(aElementos, "tblCliente"); break;
                    case 9: insertarTabla(aElementos, "tblContrato"); break;
                    case 10: insertarTabla(aElementos, "tblQn"); break;
                    case 11: insertarTabla(aElementos, "tblQ1"); break;
                    case 12: insertarTabla(aElementos, "tblQ2"); break;
                    case 13: insertarTabla(aElementos, "tblQ3"); break;
                    case 14: insertarTabla(aElementos, "tblQ4"); break;
                    case 16:
                        BorrarFilasDe("tblProyecto");
                        for (var i = 0; i < aElementos.length; i++) {
                            if (aElementos[i] == "") continue;
                            var aDatos = aElementos[i].split("@#@");
                            var oNF = $I("tblProyecto").insertRow(-1);
                            oNF.id = aDatos[0];
                            oNF.style.height = "16px";

                            oNF.setAttribute("categoria", aDatos[2]);
                            oNF.setAttribute("cualidad", aDatos[3]);
                            oNF.setAttribute("estado", aDatos[4]);
                            oNF.insertCell(-1);

                            if (aDatos[2] == "P") oNF.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                            else oNF.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                            switch (aDatos[3]) {
                                case "C": oNF.cells[0].appendChild(oImgContratante.cloneNode(true), null); break;
                                case "J": oNF.cells[0].appendChild(oImgRepJor.cloneNode(true), null); break;
                                case "P": oNF.cells[0].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                            }

                            switch (aDatos[4]) {
                                case "A": oNF.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                                case "C": oNF.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                                case "H": oNF.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                                case "P": oNF.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                            }

                            var oCtrl1 = document.createElement("span");
                            oCtrl1.className = "NBR W190";
                            oCtrl1.setAttribute("style", 'margin-left:3px;');
                            oCtrl1.attachEvent('onmouseover', TTip);

                            oNF.cells[0].appendChild(oCtrl1);
                            oNF.cells[0].children[3].innerHTML = Utilidades.unescape(aDatos[1]);
                        }
                        divProyecto.scrollTop = 0;
                        break;
                }
                setTodos();

                if ($I("chkActuAuto").checked) {
                    buscar();
                }
                ocultarProcesando();
            }
        });
        window.focus();

        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}
function insertarTabla(aElementos,strName){
    try{    
		BorrarFilasDe(strName);
		for (var i=0; i<aElementos.length; i++){
			if (aElementos[i]=="") continue;
			var aDatos = aElementos[i].split("@#@");
			var oNF = $I(strName).insertRow(-1);
			oNF.id = aDatos[0];
			oNF.style.height = "16px";

			var oCtrl1 = document.createElement("span");
			oCtrl1.className = "NBR W255";
			oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));

			oNF.insertCell(-1).appendChild(oCtrl1);
						
			//oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
		}
		$I(strName).scrollTop=0;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar las filas en la tabla "+strName, e.message);
    }
}

function delCriterios(nTipo){
    try{
        //alert(nTipo);
        mostrarProcesando();
        switch (nTipo)
        {
            case 1: 
                    nNivelEstructura=0;
                    nNivelSeleccionado = 0;
                    BorrarFilasDe("tblAmbito"); 
                    js_subnodos.length = 0;
                    js_ValSubnodos.length = 0;
                    break;
            case 2: BorrarFilasDe("tblResponsable"); break;
            case 3: BorrarFilasDe("tblNaturaleza"); break;
            case 4: BorrarFilasDe("tblModeloCon"); break;
            case 5: BorrarFilasDe("tblHorizontal"); break;
            case 6: BorrarFilasDe("tblSector"); break;
            case 7: BorrarFilasDe("tblSegmento"); break;
            case 8: BorrarFilasDe("tblCliente"); break;
            case 9: BorrarFilasDe("tblContrato"); break;
            case 10: BorrarFilasDe("tblQn"); break;
            case 11: BorrarFilasDe("tblQ1"); break;
            case 12: BorrarFilasDe("tblQ2"); break;
            case 13: BorrarFilasDe("tblQ3"); break;
            case 14: BorrarFilasDe("tblQ4"); break;
            case 16: BorrarFilasDe("tblProyecto"); break;
        }
	        
        borrarCatalogo();
        setTodos();            
        
        if ($I("chkActuAuto").checked){
            buscar();
        }else ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function setPreferencia(){
    try{
        mostrarProcesando();
        
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("setPreferencia@#@");
        sb.Append($I("cboEstado").value +"@#@");
        sb.Append($I("cboCategoria").value +"@#@");
        sb.Append($I("cboCualidad").value +"@#@");
        sb.Append(($I("chkCerrarAuto").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkActuAuto").checked)? "1@#@":"0@#@");
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) +"@#@");
        sb.Append(getValoresMultiples());
       
        RealizarCallBack(sb.ToString(), "");
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

var sOLAnterior = "";
function setOperadorLogico(bBuscar){
    try{
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        if (sOL == sOLAnterior) return;
        else sOLAnterior = sOL;
        
        setTodos();

        if ($I("chkActuAuto").checked){
            if (bBuscar) buscar();
        }
        
    }catch(e){
		mostrarErrorAplicacion("Error al modificar el operador lógico.", e.message);
	}
}
function setTodos(){
    try{
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        setFilaTodos("cboEstado", (sOL=="1")?true:false, true);
        setFilaTodos("cboCategoria", (sOL=="1")?true:false, false);
        setFilaTodos("cboCualidad", (sOL=="1")?true:false, false);
        setFilaTodos("tblAmbito", (sOL=="1")?true:false, true);
        setFilaTodos("tblSector", (sOL=="1")?true:false, true);
        setFilaTodos("tblResponsable", (sOL=="1")?true:false, true);
        setFilaTodos("tblSegmento", (sOL=="1")?true:false, true);
        setFilaTodos("tblNaturaleza", (sOL=="1")?true:false, false);
        setFilaTodos("tblCliente", (sOL=="1")?true:false, true);
        setFilaTodos("tblModeloCon", (sOL=="1")?true:false, true);
        setFilaTodos("tblContrato", (sOL=="1")?true:false, true);
        setFilaTodos("tblHorizontal", (sOL=="1")?true:false, true);
        setFilaTodos("tblQn", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ1", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ2", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ3", (sOL=="1")?true:false, true);
        setFilaTodos("tblQ4", (sOL=="1")?true:false, true);
        setFilaTodos("tblProyecto", (sOL=="1")?true:false, true);
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
	}
}

function getValoresMultiples(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n=1; n<=16; n++){
            if (n==15) continue;
            switch (n) {
                case 1: oTabla = $I("tblAmbito"); break;
                case 2: oTabla = $I("tblResponsable"); break;
                case 3: oTabla = $I("tblNaturaleza"); break;
                case 4: oTabla = $I("tblModeloCon"); break;
                case 5: oTabla = $I("tblHorizontal"); break;
                case 6: oTabla = $I("tblSector"); break;
                case 7: oTabla = $I("tblSegmento"); break;
                case 8: oTabla = $I("tblCliente"); break;
                case 9: oTabla = $I("tblContrato"); break;
                case 10: oTabla = $I("tblQn"); break;
                case 11: oTabla = $I("tblQ1"); break;
                case 12: oTabla = $I("tblQ2"); break;
                case 13: oTabla = $I("tblQ3"); break;
                case 14: oTabla = $I("tblQ4"); break;
                case 16: oTabla = $I("tblProyecto"); break;                            
            }
        
            for (var i=0; i<oTabla.rows.length;i++){
                if (oTabla.rows[i].id == "-999") continue;
                if (n==1){
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }else if (n==16){
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }else{
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n +"##"+ oTabla.rows[i].id +"##"+ Utilidades.escape(oTabla.rows[i].innerText));
                }
            }
        }
        
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}
function VerFecha(strM){
    try {
		if ($I('txtFechaInicio').value.length==10 && $I('txtFechaFin').value.length==10){
		    aa = $I('txtFechaInicio').value;
		    bb = $I('txtFechaFin').value;
		    if (aa == "") aa = "01/01/1900";
		    if (bb == "") bb = "01/01/1900";
		    fecha_desde = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
		    fecha_hasta = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);

            if (strM=='D' && $I('txtFechaInicio').value == "") return;
            if (strM=='H' && $I('txtFechaFin').value == "") return;
    		
            if ($I('txtFechaInicio').value.length < 10 || $I('txtFechaFin').value.length < 10) return;

            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtFechaInicio').value = $I('txtFechaFin').value;

            $I("hdnDesde").value = aa.substr(6,4)+aa.substr(3,2);
            $I("hdnHasta").value = bb.substr(6,4)+bb.substr(3,2);
            setCombo(); 
        } 
        else{
            $I("hdnDesde").value = "190001";
            $I("hdnHasta").value = "190001";
            borrarCatalogo();   
        }       		
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}
function Limpiar()
{
    nNivelEstructura=0;
    nNivelSeleccionado = 0;
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;

    var aTable= $I('divPestRetr').getElementsByTagName("TABLE");       
    for (var i=0; i<aTable.length; i++){
        if (aTable[i].id.substring(0,3) != "tbl") continue;
        BorrarFilasDe(aTable[i].id);
    }

    $I("rdbOperador_0").checked=true;
	$I("cboEstado").value="";
    $I("cboCategoria").value="0";
    $I("cboCualidad").value="0"; 
    $I("chkCerrarAuto").checked=true;
    $I("chkActuAuto").checked=false;
    
    setTodos();
}