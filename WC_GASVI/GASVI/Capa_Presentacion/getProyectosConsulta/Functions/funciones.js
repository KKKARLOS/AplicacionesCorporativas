var bLectura=false;
var sValorNodo = "";

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 120;
var nTopPestana = 0;
/* Fin de Valores necesarios para la pestaña retractil */

function init(){
    try{
        if (!mostrarErrores()) return;

        mostrarOcultarPestVertical();        
        sValorNodo = $I("hdnIdNodo").value;

        window.focus();
        $I("txtNumPE").focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}


function aceptarClick(oControl){
    try{
        if (bProcesando()) return;
        var strRetorno = "";
        var oFila;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

//        window.returnValue = oFila.id + "@#@" + oFila.cells[2].innerText + " - " + oFila.cells[3].innerText;  //+ "@#@" + oFila.estado;
//        window.close();

        var returnValue = oFila.id + "@#@" + oFila.cells[2].innerText + " - " + oFila.cells[3].innerText;  //+ "@#@" + oFila.estado;
        modalDialog.Close(window, returnValue);
             
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir(){
    //        window.returnValue = null;
    //        window.close();
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
                if (tblDatos.rows.length == 0)
                    mmoff("War", "Para los filtros establecidos, no se encuentran proyectos en solicitudes GASVI.", 500, 3000);
                window.focus();
                break;
            case "setPSN":
                aceptarSalir();
                break;           
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}

function getCliente(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getCliente.aspx";
        modalDialog.Show(strEnlace, self, sSize(610, 485))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                }
                ocultarProcesando();
            }); 
            
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function borrarCliente(){
    try{
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        borrarCatalogo();
	    if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar(){
    try{
        var js_args = "buscar@#@";
        js_args += sValorNodo +"@#@";
        js_args += $I("cboEstado").value +"@#@";
        js_args += $I("cboCategoria").value +"@#@";
        js_args += $I("hdnIdCliente").value +"@#@";
        js_args += $I("hdnIdResponsable").value +"@#@";
        js_args += dfn($I("txtNumPE").value) +"@#@";
        js_args += Utilidades.escape($I("txtDesPE").value) + "@#@";
        js_args += getRadioButtonSelectedValue("rdbTipoBusqueda", true);
      
        //alert(js_args);     
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        
        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

function getNodo(){
    try{
        mostrarProcesando();
//	    var ret = window.showModalDialog(strServer +  "Capa_Presentacion/getNodos/Default.aspx", self, sSize(410, 600));
//	    window.focus();

        var strEnlace = strServer + "Capa_Presentacion/getNodos/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(410, 600))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];

                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                }
                ocultarProcesando();
            }); 

	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener el "+ strEstructuraNodo, e.message);
    }
}

function borrarNodo(){
    try{
        mostrarProcesando();
        $I("hdnIdNodo").value = "";
        $I("txtDesNodo").value = "";
        sValorNodo = "";
        
        $I("divCatalogo").children[0].innerHTML = "";
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el "+ strEstructuraNodo, e.message);
    }
}

function getResponsable(){
    try{
        mostrarProcesando();
//        var url = strServer +  "Capa_Presentacion/getResponsable/Default.aspx";
//        var ret = window.showModalDialog(url, self, sSize(550, 540));
//        window.focus();
        var strEnlace = strServer + "Capa_Presentacion/getResponsable/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 540))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                }
                ocultarProcesando();
            }); 

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function borrarResponsable(){
    try{
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        borrarCatalogo();
	    if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}

function setNumPE(){
    try{
        $I("hdnIdNodo").value = "";
        $I("txtDesNodo").value = "";
        sValorNodo = "";
        
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        $I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("txtDesPE").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function setDesPE(){
    try{
        $I("hdnIdNodo").value = "";
        $I("txtDesNodo").value = "";
        sValorNodo = "";

        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        $I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("txtNumPE").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir la denominación de proyecto", e.message);
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
        
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                oFila.ondblclick = function(){aceptarClick(this)};

                if (oFila.getAttribute("categoria")=="P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                
                oFila.cells[3].children[0].ondblclick = function(){aceptarClick(this)};
                oFila.cells[4].children[0].ondblclick = function(){aceptarClick(this)};
                oFila.cells[5].children[0].ondblclick = function(){aceptarClick(this)};
                
                switch (oFila.getAttribute("estado")){
                    case "A": oFila.cells[1].appendChild(oImgAb.cloneNode(true), null); break;
                    case "C": oFila.cells[1].appendChild(oImgCe.cloneNode(true), null); break;
                    case "H": oFila.cells[1].appendChild(oImgHi.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgPr.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}

function setActuAuto(){
    try{
        if ($I("chkActuAuto").checked){
            setOp($I("btnObtener"), 30);
            buscar();
        }else{
            setOp($I("btnObtener"), 100);
        }

	}catch(e){
		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
	}
}

function setCombo(){
    try{
        borrarCatalogo();
        if ($I("chkActuAuto").checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}

function setNodo(oNodo){
    try{
        sValorNodo=oNodo.value;
        borrarCatalogo();
        if ($I("chkActuAuto").checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el "+ strEstructuraNodo +".", e.message);
    }
}