var IDActivo = "0-0-0-0-0-0";

function init(){
    try{
        nNE = $I("hdnNE").value;
        colorearNE();
        setExcelImg("imgExcel", "divCatalogo");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
		mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}


function mdn(oFila){
    try{
        //alert("Nivel: " + oFila.nivel +", ID completo: " + oFila.id +", ID nodo: " + oFila.id.split("-")[oFila.nivel-1]);
        var aDatos = oFila.id.split("-");
        mostrarProcesando();

        var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/DetalleSubNodo/Default.aspx?SN4=" + codpar(aDatos[0]);
        strEnlaceSubNodo += "&SN3="+ codpar(aDatos[1]);
        strEnlaceSubNodo += "&SN2="+ codpar(aDatos[2]);
        strEnlaceSubNodo += "&SN1="+ codpar(aDatos[3]);
        strEnlaceSubNodo += "&Nodo="+ codpar(aDatos[4]);
        strEnlaceSubNodo += "&ID="+ codpar(aDatos[5]) + "&origen=" + codpar("MantNodo");
        //var ret = window.showModalDialog(strEnlaceSubNodo , self, sSize(990, 550));
        modalDialog.Show(strEnlaceSubNodo, self, sSize(990, 550))
	        .then(function(ret) {
	            if (ret != null){
	                if (ret){
	                    var aDatos = ret.split("///");
	                    if (aDatos[0] == "true") ObtenerEstructura();
	                    else{
	                        oFila.getElementsByTagName("label")[0].innerText = aDatos[1];
	                        oFila.getElementsByTagName("label")[0].style.color = (aDatos[2]=="true")? "black":"gray";
	                        ocultarProcesando();
	                    }
	                }
	                else ocultarProcesando();
                }else ocultarProcesando();
	        }); 
	    //alert(ret);        
	}catch(e){
		mostrarErrorAplicacion("Error ", e.message);
    }
}

function mostrar(oImg){
    try{
        if (IDActivo != "0-0-0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
        }
        
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        //var nDesplegado = oFila.desplegado;
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);

        var tblDatos = $I("tblDatos");
        //alert("nIndexFila: "+ nIndexFila);
        for (var i=nIndexFila+1; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O")
                {
                    tblDatos.rows[i].style.display = "none";
                    if (tblDatos.rows[i].getAttribute("nivel") < 2)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
                else if (tblDatos.rows[i].getAttribute("nivel") - 1 == nNivel) tblDatos.rows[i].style.display = "table-row";
            }else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../../images/plus.gif";
        else oImg.src = "../../../../images/minus.gif"; 

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

        if (IDActivo != "0-0-0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
        }
        
        var tblDatos = $I("tblDatos");
        if (nMostrar == 0){//Contraer
            for (var i=0; i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].getAttribute("nivel") > 1)
                {
                    if (tblDatos.rows[i].getAttribute("nivel") < 2)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    tblDatos.rows[i].style.display = "none";
                }
                else 
                {
                    tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
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
function MostrarTodo(){
    try
    {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
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
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
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
        
        if (IDActivo != "0-0-0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
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

function ObtenerEstructura(){
    try{
        mostrarProcesando();
        
        if (IDActivo != "0-0-0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
        }
        
        var js_args = "getEstructura@#@";
        js_args += ($I("chkMostrarInactivos").checked) ? "1@#@":"0@#@";
        js_args += (nNE=="0")? "1":nNE;
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
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
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar el item seleccionado,\nya que existen elementos con los que está relacionado.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getEstructura":
                $I("divCatalogo").innerHTML = aResul[2];
                IDActivo = "0-0-0-0-0-0";
                AccionBotonera("eliminar", "D");
                break;
            case "eliminar":
                setTimeout("ObtenerEstructura();", 50);
                break;        
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function insertarItem(){
    try{
        //alert("insertar elemento nivel "+ nNivelItem);
        
        var aIDActivo = IDActivo.split("-");
        if (aIDActivo[4] == "0" || aIDActivo[5] != "0"){
            mmoff("Inf", "Debes seleccionar el " + strEstructuraNodo + " en el que insertar el nuevo elemento.",400);
            return;
        }

        mostrarProcesando();

        var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/DetalleSubNodo/Default.aspx?SN4=" + codpar(aIDActivo[0]);
        strEnlaceSubNodo += "&SN3="+ codpar(aIDActivo[1]);
        strEnlaceSubNodo += "&SN2="+ codpar(aIDActivo[2]);
        strEnlaceSubNodo += "&SN1="+ codpar(aIDActivo[3]);
        strEnlaceSubNodo += "&Nodo="+ codpar(aIDActivo[4]);
        strEnlaceSubNodo += "&ID=" + codpar(0)+ "&origen=" + codpar("MantNodo");
        //var ret = window.showModalDialog(strEnlaceSubNodo, self, sSize(990, 570));
        modalDialog.Show(strEnlaceSubNodo, self, sSize(990, 570))
	        .then(function(ret) {
	            //alert(ret);
	            if (ret != null){
	                if (ret) ObtenerEstructura();
	                else ocultarProcesando();
                }else ocultarProcesando();
	        }); 
//ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
    }
}

function marcarLabel(oLabel){
    try{
        if (IDActivo != "0-0-0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
        }
        oLabel.style.backgroundColor = "#83afc3";
        IDActivo = oLabel.parentNode.parentNode.id;
        AccionBotonera("eliminar", "H");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a marcar el elemento", e.message);
    }
}

function eliminar(){
    try{
        if (IDActivo == "0-0-0-0-0-0"){
            AccionBotonera("eliminar", "D");
            return;
            
        }
        
        mostrarProcesando();
        var aIDActivo = IDActivo.split("-");

        var js_args = "eliminar@#@";
        js_args += aIDActivo[5];

        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el elemento de estructura.", e.message);
    }
}

function MostrarInactivos(){
    try{
        mostrarProcesando();
        
        if (IDActivo != "0-0-0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
        }
        
        var js_args = "getEstructura@#@";
        js_args += ($I("chkMostrarInactivos").checked) ? "1@#@":"0@#@";
        js_args += nNE;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
    }
}

function excel(){
    try {
        var tblDatos = $I("tblDatos");
    
        if (tblDatos==null){
            ocultarProcesando();
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
        sb.Append("        <td >Denominación</TD>");
		sb.Append("	</TR>");
		sb.Append("</TABLE>");
        
        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
        var aDatos;
	    for (var i=0;i < tblDatos.rows.length; i++){
		    if (tblDatos.rows[i].style.display == "none") continue;
		    sb.Append("<TR><TD>");
		    aDatos = tblDatos.rows[i].id.split("-");
		    switch (tblDatos.rows[i].getAttribute("nivel")) {
		        case "1": sb.Append(aDatos[4] + " - " + tblDatos.rows[i].innerText); break;
		        case "2": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[5] + " - " + tblDatos.rows[i].innerText); break;
		    }
		    sb.Append("</TD></TR>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}