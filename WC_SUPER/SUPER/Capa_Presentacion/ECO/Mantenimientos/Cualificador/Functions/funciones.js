var aFila;
//var bMostrarInactivos = false;
var strEstructuraNodo = "";
var iIndice=1;
function init(){
    try{
        $I("chkObligatorio").disabled=true;  
        $I('txtDenominacion').readOnly=true;  

        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblEstructura").className = "enlace";
            $I("lblEstructura").onclick = function(){getEstructura()};
            sValorEstructura = $I("hdnIdEstructura").value;
        }else sValorEstructura = $I("cboEstructura").value;
        strEstructuraNodo = $I("lblEstructura").innerText;
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function getEstructura(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getEstructura.aspx?nNivel=" + sNivel;

        //var ret = window.showModalDialog(strEnlace, self, sSize(450, 480));
        modalDialog.Show(strEnlace, self, sSize(450, 480))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
			        $I("hdnIdEstructura").value = aDatos[0];
			        sValorEstructura = $I("hdnIdEstructura").value;
			        $I("txtDesEstructura").value = aDatos[1];
			        $I("txtDenominacion").value = aDatos[2];
			        $I("chkObligatorio").checked = (aDatos[3]=="1")? true:false;

			        setOp($I("btnEliminar"), 100);
			        setOp($I("btnAnadir"), 100);
                    
                    $I('txtDenominacion').readOnly=false;
                    $I('txtDenominacion').onkeypress=function(){activarGrabar();};
                    $I("chkObligatorio").disabled=false;            
                    $I("chkObligatorio").onclick=function(){activarGrabar();};  	    
        			
			        MostrarCualificadores(sValorEstructura);
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los elementos de estructura", e.message);
    }
}
function MostrarCualificadores(sIdEstructura){
    try{
            var js_args = "getCualificadores@#@";
            js_args += sIdEstructura +"@#@";
            js_args += ($I("chkMostrarInactivos").checked)? "1":"0";
            RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error en MostrarCualificadores", e.message);
    }
}
function controlGrabar()
{
    if (es_administrador!="A")
        if ($I("cboEstructura")[$I("cboEstructura").selectedIndex].EDI=="N") return;
    
    activarGrabar();
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getCualificadores":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                tbody = document.getElementById('tbodyCualificadores'); 
                tbody.onmousedown = startDragIMG;
                tbody.ondragstart = controlGrabar;
                break;
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                        continue;
                    }
                    mfa(aFila[i],"N");
                }
                for (var i=0;i<aFila.length;i++){
                    aFila[i].setAttribute("orden", i);
                }
                
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
             
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function actuCombo()
{
    $I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_OBL=$I("chkObligatorio").checked;
    $I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_DEN=$I('txtDenominacion').value;
    controlGrabar();
}
function setEstructura(){
    try{
      
        if ($I("cboEstructura").value=="") 
        {
            borrarCatalogo();
            return;
        }

	    if (bCambios) {
	        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
	            if (answer){
	                grabar();
	                return;
	            }else{
                    $I("cboEstructura")[iIndice].Q_DEN=$I("cboEstructura")[iIndice].Q_DEN_OLD;
	                $I("cboEstructura")[iIndice].Q_OBL=($I("cboEstructura")[iIndice].Q_OBL_OLD)?true:false;            
	                desActivarGrabar();
                }
	            LLamadaSetEstructura();
	        });
	    }
	    else LLamadaSetEstructura();
    }catch(e){
        mostrarErrorAplicacion("Error-1 al seleccionar el "+ strEstructuraNodo +".", e.message);
    }
}
function LLamadaSetEstructura() {
    try {
        iIndice=$I("cboEstructura").selectedIndex;

        $I("txtDenominacion").value = Utilidades.unescape($I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_DEN);
        $I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_DEN_OLD=$I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_DEN;
        $I("chkObligatorio").checked = ($I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_OBL)?true:false;
        $I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_OBL_OLD=($I("cboEstructura")[$I("cboEstructura").selectedIndex].Q_OBL)?true:false;
        
 	    if ($I("cboEstructura")[$I("cboEstructura").selectedIndex].EDI=="S")
 	    {
 	        setOp($I("btnEliminar"), 100);
 	        setOp($I("btnAnadir"), 100);
            
            $I('txtDenominacion').readOnly=false;
            $I('txtDenominacion').onkeypress=function(){actuCombo()};
            $I("chkObligatorio").disabled=false;            
            $I("chkObligatorio").onclick=function(){actuCombo()};  	    
 	    }
 	    else
 	    {
 	        setOp($I("btnEliminar"), 30);
 	        //$I("btnEliminar").style.cursor = "not-allowed";
 	        setOp($I("btnAnadir"), 30);
 	        //$I("btnAnadir").style.cursor = "not-allowed";	
            $I('txtDenominacion').readOnly=true;
            $I('txtDenominacion').onkeypress=null;
            $I("chkObligatorio").disabled=true;
            $I("chkObligatorio").onclick=null;   	            
        }
 	    
	    sValorEstructura=$I("cboEstructura").value;
        MostrarCualificadores(sValorEstructura);
        
	}catch(e){
		mostrarErrorAplicacion("Error-2 al seleccionar el "+ strEstructuraNodo +".", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("txtDenominacion").value="";
        sValorEstructura="";
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function eliminar(){
    try{
        if (getOp($I("btnEliminar")) == 30) return;  
          
        if ($I("tblDatos")==null) return;
        
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("bd") == "I"){
                    $I("tblDatos").deleteRow(i);
                }else{
                    mfa(aFila[i], "D");
                }
            }
        }
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}

function grabar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        if (!comprobarDatos()){
            return;
        }
        
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        sb.Append(sValorEstructura +"@#@");
        sb.Append(Utilidades.escape($I('txtDenominacion').value) +"@#@");
        sb.Append(($I("chkObligatorio").checked)? "1":"0");
        sb.Append("@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") +"##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Estructura
                sb.Append(aFila[i].getAttribute("orden") +"///"); //Orden
                sw = 1;
            }
        }
//        if (sw == 0){
//            alert("No se han modificado los datos.");
//            desActivarGrabar();
//            ocultarProcesando();
//            return;
//        }
        
        mostrarProcesando();        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos(){
    try{
        var nOrden=0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].cells[1].children[0].value == ""){
                ms(aFila[i]);
                mmoff("Inf", "Debes indicar la denominación del valor", 250);
                aFila[i].cells[1].children[0].focus();
                return false;
            }
            if (aFila[i].getAttribute("orden") != nOrden){
                if (aFila[i].getAttribute("bd") != "I") aFila[i].setAttribute("bd", "U");
                aFila[i].setAttribute("orden", nOrden);
            }
            nOrden++;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function MostrarInactivos(){
    try{
        if (sValorEstructura==""){
            mmoff("Inf", "Es obligatorio seleccionar un " + strEstructuraNodo, 270);
            return;
        }    
	    if (bCambios) {
	        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
	            if (answer) {
	                grabar();
	                return;
	            } else desActivarGrabar();
	            LLamadaMostrarInactivos();
	        });
	    }
	    else LLamadaMostrarInactivos();
    }catch(e){
        mostrarErrorAplicacion("Error al ir a mostrar los inactivos-1.", e.message);
    }
}
function LLamadaMostrarInactivos() {
    try {
        mostrarProcesando();
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);        
        MostrarCualificadores(sValorEstructura);
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar los inactivos-2.", e.message);
    }
}

function mdn(oFila){
    try{
    
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/Administracion/DetalleCualificador/Default.aspx?nCualificador=" + oFila.id;
        strEnlace += "&sNivel="+ sNivel
        strEnlace += "&nIdEstructura="+ sValorEstructura;
        if (getOp($I("btnEliminar")) == 100) strEnlace += "&edicion=S";
        else strEnlace += "&edicion=N";
        
        //var ret = window.showModalDialog(strEnlace , self, sSize(990, 550));
        modalDialog.Show(strEnlace, self, sSize(990, 550))
	        .then(function(ret) {
	            //alert(ret);
	            if (ret != null){
	                if (ret){
                        mostrarProcesando();
                        MostrarCualificadores(sValorEstructura);
	                }
	                else ocultarProcesando();
                }else ocultarProcesando();
	        });     
	}catch(e){
		mostrarErrorAplicacion("Error ", e.message);
    }
}
function nuevo(){
    try{
        if (getOp($I("btnAnadir")) == 30) return;  
        
        if (sValorEstructura==""){
            mmoff("Inf", "Es obligatorio seleccionar un " + strEstructuraNodo, 270);
            return;
        }

        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
    
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/Administracion/DetalleCualificador/Default.aspx?nCualificador=0";
        strEnlace += "&sNivel="+ sNivel
        strEnlace += "&nIdEstructura="+ sValorEstructura;
        if (getOp($I("btnEliminar")) == 100) strEnlace += "&edicion=S";
        else strEnlace += "&edicion=N";
        
        //var ret = window.showModalDialog(strEnlace , self, sSize(990, 550));
        modalDialog.Show(strEnlace, self, sSize(990, 550))
	        .then(function(ret) {
                if (ret != null){
                    if (ret){
                        var aDatos = ret.split("///");
                        if (aDatos[0] == "true") MostrarInactivos();
                        else{
        //	                oFila.cells[2].innerText = aDatos[1];
        //	                oFila.cells[2].style.color = (aDatos[2]=="true")? "black":"gray";
                            ocultarProcesando();
                        }
                    }
                    else ocultarProcesando();
                }else ocultarProcesando();
	        });         
	}catch(e){
		mostrarErrorAplicacion("Error ", e.message);
    }
}
