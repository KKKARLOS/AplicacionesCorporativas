var bProcesoNoCorrecto = false;
var bMostrar = false;
var sSubnodos = "";
var js_subnodos = new Array();
var sMesOriginal = "";

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 120;
var nTopPestana = 125;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

function init(){
    try{
        ToolTipBotonera("procesar","Procesa el borrado de datos");
        AccionBotonera("procesar", "H");
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
            case "procesar":
                //alert(aResul[2]);
                mmoff("Suc","Datos borrados correctamente", 210);
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
                break;
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("tblAmbito").rows.length == 0
            && $I("tblResponsable").rows.length == 0
            && $I("tblProyecto").rows.length == 0){
            mmoff("War","Debes restringir el borrado de datos seleccionando ámbito, responsable o proyecto.", 500, 3000);
            return false;
            }
            
        var sw=0;
        for (var i=0; i<$I("tblDatos").rows.length; i++){
            if ($I("tblDatos").rows[i].getAttribute("tipo") != "CL") continue;
            if ($I("tblDatos").rows[i].cells[0].children[0].checked){
                sw = 1;
                break;
            }
        }
        
        if (!$I("chkConsProf").checked
            && !$I("chkConsNivel").checked
            && !$I("chkProdProf").checked
            && !$I("chkProdPerfil").checked
            && !$I("chkAvance").checked
            && !$I("chkPeriodCons").checked
            && !$I("chkPeriodProd").checked
            && !$I("chkCirculante").checked
            && sw == 0){
            mmoff("War", "Debes seleccionar algún dato a borrar.", 350, 3000);
            return false;
            }

	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de procesar", e.message);
        return false;
    }
    return true;
}

function procesar(){
    try{
        if (!comprobarDatos()){
            return;
        }
    
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("procesar@#@");
        sb.Append($I("hdnDesde").value +"@#@");
        sb.Append($I("hdnHasta").value +"@#@");
        sb.Append(getDatosTabla(2)+ "@#@"); //Responsable
        sb.Append(js_subnodos.join(",")+ "@#@"); //ids estructura ambito
        sb.Append(getDatosTabla(16)+ "@#@"); //ProyectoSubnodos
        
        for (var i=0; i< $I("tblDatos").rows.length; i++){
            if ($I("tblDatos").rows[i].getAttribute("tipo") != "CL") continue;
            if ($I("tblDatos").rows[i].cells[0].children[0].checked) 
                sb.Append(tblDatos.rows[i].id+",");
        }
        sb.Append("@#@"); //Clases a borrar
      
        sb.Append(($I("chkConsProf").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkConsNivel").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkProdProf").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkProdPerfil").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkAvance").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkPeriodCons").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkPeriodProd").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkCirculante").checked)? "1@#@":"0@#@");
        sb.Append(($I("chkCerrados").checked)? "1@#@":"0@#@");

        RealizarCallBack(sb.ToString(), "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos a procesar.", e.message);
    }
}
function getCriterios(nTipo){
    try{
        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(850, 420);
        
        var strEnlace = "";
        switch (nTipo){
            case 1:               
                //strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sSnds=" + codpar(sSubnodos);
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx";
                sTamano = sSize(950, 450);
                break;         
            case 16:
                strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pge";
                sTamano = sSize(1020, 700)
                break;
            default:
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo="+nTipo;
                break;
        }   
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo){
            case 2: oTabla = $I("tblResponsable"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }
        if (nTipo != 1){
            slValores=fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }       
	    //window.focus();
	    modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
	            if (ret != null) {
	                var aElementos = ret.split("///");
	                switch (nTipo) {
	                    case 1:
	                        BorrarFilasDe("tblAmbito");
	                        for (var i = 1; i < aElementos.length; i++) {
	                            if (aElementos[i] == "") continue;
	                            var aDatos = aElementos[i].split("@#@");
	                            var oNF = $I("tblAmbito").insertRow(-1);
	                            oNF.style.height = "20px";
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
	                            oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
	                        }
	                        divAmbito.scrollTop = 0;
	                        break;
	                    case 2: insertarTabla(aElementos, "tblResponsable"); break;
	                    case 16:
	                        BorrarFilasDe("tblProyecto");
	                        for (var i = 0; i < aElementos.length; i++) {
	                            if (aElementos[i] == "") continue;
	                            var aDatos = aElementos[i].split("@#@");
	                            var oNF = $I("tblProyecto").insertRow(-1);
	                            oNF.id = aDatos[0];
	                            oNF.style.height = "20px";
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

	                            var oCtrl1 = document.createElement("nobr");
	                            oCtrl1.className = "NBR W190";
	                            oCtrl1.setAttribute("style", 'margin-left:0px;');
	                            oCtrl1.attachEvent('onmouseover', TTip);

	                            oNF.cells[0].appendChild(oCtrl1);
	                            oNF.cells[0].children[3].innerHTML = Utilidades.unescape(aDatos[1]);
	                        }
	                        divProyecto.scrollTop = 0;
	                        break;
	                }
	            }
	        });
	    
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}
function insertarTabla(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "16px";

            var oCtrl1 = document.createElement("div");
            oCtrl1.className = "NBR W255";
            oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));

            oNF.insertCell(-1).appendChild(oCtrl1);

            //oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}
function delCriterios(nTipo) {
    try{
        //alert(nTipo);
        mostrarProcesando();
        switch (nTipo)
        {
            case 1: 
                BorrarFilasDe("tblAmbito"); 
                js_subnodos.length = 0;
                js_ValSubnodos.length = 0;
                break;
            case 2: BorrarFilasDe("tblResponsable"); break;
            case 16: BorrarFilasDe("tblProyecto"); break;
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
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
                case 2: mmoff("Inf","Has seleccionado un número excesivo de responsables de proyecto.",500); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyecto.",350); break;
            }
            return;   
		}
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}

function getPeriodo(){
    try{
        mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD="+ codpar($I("hdnDesde").value) +"&sH="+ codpar($I("hdnHasta").value);
	    //window.focus();
	    modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
	                $I("hdnDesde").value = aDatos[0];
	                $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
	                $I("hdnHasta").value = aDatos[1];
	            }
	        });
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}
