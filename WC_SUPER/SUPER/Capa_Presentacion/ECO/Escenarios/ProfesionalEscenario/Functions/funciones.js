function init(){
    try{
        if (!mostrarErrores()) return;
//        if ($I("hdnCualidad").value=="P")
//            $I("lblCR").className = "texto";
//        else
//            $I("lblCR").className = "enlace";
        $I("txtApellido1").focus();
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "tecnicos":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                //tratarTecnicosDeBaja();
                scrollTablaProf();
                break;
        }
        ocultarProcesando();
    }
}

function aceptarAux(){
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar(){
    try{
        var strDatos = "";
        var tblDatos2=$I("tblDatos2");
        var sb = new StringBuilder; //sin paréntesis
        for (var i=0; i<tblDatos2.rows.length;i++){
            sb.Append(tblDatos2.rows[i].id + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("nodo") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("baja") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("costecon") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("costerep") + "@#@");
            sb.Append(tblDatos2.rows[i].innerText + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("sexo") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("desnodo") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("desempresa") + "@#@");
            sb.Append(fTrim(tblDatos2.rows[i].getAttribute("alta")) + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("idCal") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("descal") + "///");
        }
        
        strDatos = sb.ToString();
        if (strDatos != "")
            strDatos = strDatos.substring(0,strDatos.length-3);
        else{
            ocultarProcesando();            
            mmoff("War", "Debes seleccionar algún profesional.", 230);
            return;   
		}
		var returnValue = strDatos;
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

var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            $I("divCatalogo").children[0].innerHTML = "";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambAp").style.display = "none";
            $I("txtGF").value = "";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    if ($I("hdnNodoActual").value == "") return;
                    $I("txtCR").value = $I("hdnDesNodoActual").value;
                    mostrarRelacionTecnicos("C", $I("hdnNodoActual").value);
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
            }
            sAmb = sOp;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}

function mostrarRelacionTecnicos(sOpcion, sValor){
    try{
        //alert("mostrarRelacionTecnicos("+ sOpcion +","+ sValor +")");
        var sValor1 = "";
        var sValor2 = "";
        var sValor3 = "";
        if (sOpcion == "N"){
            sValor1 = Utilidades.escape($I("txtApellido1").value);
            sValor2 = Utilidades.escape($I("txtApellido2").value);
            sValor3 = Utilidades.escape($I("txtNombre").value);
            if (sValor1=="" && sValor2=="" && sValor3==""){
                mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
                return;
            }
        }else{
            sValor1 = sValor;
        }
        //var js_args = "tecnicos@#@"+sOpcion +"@#@"+ sValor1 +"@#@"+ sValor2 +"@#@"+ sValor3 +"@#@"+ opener.$I("cboTarificacion").value;
        var js_args = "tecnicos@#@";
        js_args += sOpcion +"@#@";
        js_args += sValor1 +"@#@";
        js_args += sValor2 +"@#@";
        js_args += sValor3 +"@#@";
        js_args += opener.getRadioButtonSelectedValue("rdbCoste", false)+"@#@";
        js_args += opener.$I("hdnCualidad").value+"@#@";
        js_args += opener.$I("hdnIdNodo").value+"@#@";
        js_args += opener.$I("hdnIdProyectoSubNodo").value;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}

function obtenerCR(){
    try{
        //En proyectos replicados con gestión solo podemos seleccionar profesionales externos o del nodo del proyecto
        if ($I("hdnCualidad").value=="P") return;
        
        if ($I("hdnEsReplicable").value == "0"){
            mmoff("War", "El proyecto no permite seleccionar profesionales pertenecientes a otro "+ strEstructuraNodo, 500, 2500);
            return;
        }
        
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodo.aspx";
	    //var ret = window.showModalDialog(strEnlace, self, sSize(400, 470));
        modalDialog.Show(strEnlace, self, sSize(400, 470))
	        .then(function(ret) {
	            if (ret != null){
		            var aOpciones = ret.split("@#@");
		            $I("txtCR").value = aOpciones[1];
                    mostrarRelacionTecnicos("C", aOpciones[0]);
	            }else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}
function obtenerGF(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnNodoActual").value;
	    //var ret = window.showModalDialog(strEnlace, self, sSize(450, 450));
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
	        .then(function(ret) {
	            if (ret != null){
		            var aOpciones = ret.split("@#@");
		            $I("txtGF").value = aOpciones[1];
                    mostrarRelacionTecnicos("G", aOpciones[0]);
	            }else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}

var aProfesionales = new Array();
function insertarRecurso(oFila, nOp){
    try{
        var idRecurso = oFila.id;
        var bExiste = false;

        //if (aProfesionales.length == 0){
        if (nOp == 1){
            for (var i = 0; i < $I("tblDatos2").rows.length; i++) {
                aProfesionales[aProfesionales.length] = $I("tblDatos2").rows[i].id;
            }
        }

        for (var i=0; i < aProfesionales.length; i++){
            if (aProfesionales[i] == idRecurso){
                bExiste = true;
                break;
            }
        }

        if (bExiste){
            //alert("El profesional indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

        if (nOp == 1){
            var oTable = $I("tblDatos2");
            var sNuevo = oFila.innerText;
            for (var iFilaNueva=0; iFilaNueva < oTable.rows.length; iFilaNueva++){
                //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                var sActual=oTable.rows[iFilaNueva].innerText;
                if (sActual>sNuevo)break;
            }
        }

        // Se inserta la fila
        var NewRow;
        if (nOp == 1) NewRow = $I("tblDatos2").insertRow(iFilaNueva);
        else NewRow = $I("tblDatos2").insertRow(-1);
        var oCloneNode	= oFila.cloneNode(true);
        oCloneNode.className = "";
        NewRow.swapNode(oCloneNode);
        
//        if (nOp == 1){
//            ot("tblDatos2", 0, 0, "");
//        }
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
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
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", "1");
                
                oFila.cells[1].children[0].ondblclick = function(){insertarRecurso(this.parentNode.parentNode, 1);}
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
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
                
                if (oFila.getAttribute("baja")=="1"){
                    oFila.style.color = "red";
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de Profesionales.", e.message);
    }
}

var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, $I("tblDatos2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos2").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos2").rows[i];
                oFila.setAttribute("sw", "1");
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
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
                
                if (oFila.getAttribute("baja")=="1"){
                    oFila.style.color = "red";
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de Profesionales asignados.", e.message);
    }
}