var bGrabando=false;
function init()
{	
    if ($I("hdnLectura").value == "N"){
        //Los administradores pueden modificar las dos figuras por lo que pueden marcar ambos checks
        //Los SAT únicamente pueden modificar los SAA por lo que solo pueden tocar ese check
        //Los SAA no pueden reasignar por lo que la pantalla queda en modo lectura
        //El resto de perfiles no tienen acceso (por rol) a esta pantalla
        if (es_administrador!=""){
            $I("chkSAT").checked=true;  
            $I("chkSAT").disabled=false; 
        } 
        else{
            $I("chkSAT").checked=false;  
            $I("chkSAT").disabled=true; 
        } 
        $I("lblOrigen").className = "enlace";
        $I("lblOrigen").title="Permite seleccionar un profesional entre aquellos que son USA en proyectos abiertos";
        $I("lblOrigen").onclick = function (){getProfesional(1);};
        $I("lblOrigen").onmouseover = function (){mostrarCursor(this);};
        
        $I("chkSAA").checked=true;  
        $I("chkSAA").disabled=false;  
        $I("lblDestino").className = "enlace";
        $I("lblDestino").title="Permite seleccionar un profesional entre aquellos que forman parte del pool de USAs";
        $I("lblDestino").onclick = function (){getProfesional(2);};
        $I("lblDestino").onmouseover = function (){mostrarCursor(this);};
        //setOp($I("btnProyectos"), 100);
    }
    else{
        $I("chkSAT").checked=false;  
        $I("chkSAT").disabled=true;  
        $I("lblOrigen").className = "texto";
        $I("lblOrigen").onclick = null;
        $I("lblOrigen").onmouseover = null;
        
        $I("chkSAA").checked=false;  
        $I("chkSAA").disabled=true; 
        $I("lblDestino").className = "texto";
        $I("lblDestino").onclick = null;
        $I("lblDestino").onmouseover = null;
        //setOp($I("btnProyectos"), 30);
        mmoff("Inf", "Solo Administradores y SATs pueden realizar reasignación.",400);
    }
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                //desActivarGrabar();
                bCambios = false;
                mmoff("Suc", "Grabación correcta", 200, null, null, null, 220);
                //bGrabando=true;
                setTimeout("getProyectos()", 50); 
                break;
            case "proyectos":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        actualizarLupas("tblTitulo", "tblDatos");
		        window.focus();
                scrollTablaProy();
//                if (!bGrabando)
//                    activarGrabar();
//                bGrabando=false;
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
    ocultarProcesando();
}
function getProyectosAux(){
    if ($I("hdnIdUserOrigen").value != ""){
        getProyectos();
    }
}
function getProyectos(){
    try{
        if ($I("hdnIdUserOrigen").value == ""){
            mmoff("Inf", "Debes indicar el profesional.", 210);
            return;
        }
        if (!$I("chkSAT").checked && !$I("chkSAA").checked){
            mmoff("War", "Debes indicar al menos un tipo de USA.", 220);
            return;
        }
        var js_args = "proyectos@#@";
        js_args += ($I("chkSAT").checked==true)? "S@#@" : "N@#@";
        js_args += ($I("chkSAA").checked==true)? "S@#@" : "N@#@";
        js_args += $I("hdnIdUserOrigen").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener proyectos.", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("hdnIdUserOrigen").value == ""){
            mmoff("War","Debe seleccionar profesional origen",220);
            return false;
        }
        if ($I("hdnIdUserDestino").value == ""){
            mmoff("War","Debe seleccionar profesional destino",220);
            return false;
        }
        if ($I("hdnIdUserDestino").value == $I("hdnIdUserOrigen").value){
            mmoff("War", "El profesional de origen y de destino no pueden ser iguales.",380);
            return false;
        }
        var bHayFilasMarcadas = false;
        
        for (var i=0; i< $I("tblDatos").rows.length;i++){
            if ($I("tblDatos").rows[i].cells[0].children[0].checked){
                bHayFilasMarcadas=true;
                break;
            }
        }
        if (!bHayFilasMarcadas){
            mmoff("War", "Debes seleccionar algún proyecto al que modificar el USA",350);
            return false;
        }
        
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar(){
    try{
        if (!comprobarDatos()){
            ocultarProcesando();
            return false;
        }
        var js_args = "grabar@#@" + $I("hdnIdUserOrigen").value + "@#@" + $I("hdnIdUserDestino").value + "@#@";
        var sb = new StringBuilder;

        var tblDatos = $I("tblDatos");
        for (var i=0; i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].cells[0].children[0].checked){
                if (tblDatos.rows[i].getAttribute("sat") == $I("hdnIdUserOrigen").value)
                    sb.Append(tblDatos.rows[i].getAttribute("idProy") + "##T##");

                if (tblDatos.rows[i].getAttribute("saa") == $I("hdnIdUserOrigen").value)
                    sb.Append(tblDatos.rows[i].getAttribute("idProy") + "##A##");
                
                sb.Append(tblDatos.rows[i].cells[4].children[0].innerText +"///");
            }
        }
        js_args += sb.ToString();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar la reasignacion de USA.", e.message);
    }
}

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
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                oFila.attachEvent('onclick', mm);
                
                if (oFila.getAttribute("categoria") == "P") oFila.cells[1].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[1].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[2].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[2].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[3].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[3].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[3].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[3].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

function marcar(nOpcion){
    try{
        if ($I("hdnLectura").value == "S") return;
        var tblDatos = $I("tblDatos");
        for (var i=0; i<tblDatos.rows.length; i++){
            tblDatos.rows[i].cells[0].children[0].checked = (nOpcion==1)?true:false;
        }
        //activarGrabar();
        //bCambios=true;
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}
function getProfesional(sTipo){
    try{
        mostrarProcesando(); 
        //var sPantalla="../../getProfesional.aspx";
        var sPantalla = strServer + "Capa_Presentacion/ECO/getSoporteAdm.aspx?Tipo=" + sTipo;
        //var ret = window.showModalDialog(sPantalla, self, sSize(450, 500));
        modalDialog.Show(sPantalla, self, sSize(450, 500))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            if (sTipo == "1"){
                        BorrarFilasDe("tblDatos");
                        $I("hdnIdUserOrigen").value = aDatos[0];
                        $I("txtOrigen").value = aDatos[1];
                        getProyectos();
                    }
                    else{
                        $I("hdnIdUserDestino").value = aDatos[0];
                        $I("txtDestino").value = aDatos[1];
                        //activarGrabar();
                    }
	            }
	            ocultarProcesando();
	        });      	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el soporte administrativo", e.message);
    }
}
