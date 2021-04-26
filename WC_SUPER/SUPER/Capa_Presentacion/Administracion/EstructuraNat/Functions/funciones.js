var IDActivo = "0-0-0-0";

function init(){
    try{
        nNE = nNEAux;
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
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                break;
            case 4:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}
function mdn(oFila){
    try{
        var aDatos = oFila.id.split("-");
        mostrarProcesando();
        var sUrl;
        switch (parseInt(oFila.getAttribute("nivel"), 10)) {
            case 1:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleTipol/Default.aspx?SN4=" + aDatos[0];
                modalDialog.Show(sUrl, self, sSize(690, 330))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) {
                                var aDatos = ret.split("///");
                                if (aDatos[0] == "true") MostrarInactivos();
                                else {
                                    if (aDatos[2] == "false" && $I("chkMostrarInactivos").checked == false) MostrarInactivos();
                                    else {
                                        oFila.getElementsByTagName("LABEL")[0].innerText = aDatos[1];
                                    }
                                }
                            }
                        }
                    });
                break;
            case 2:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleGrupo/Default.aspx?SN4=" + aDatos[0];
                sUrl += "&SN3=" + aDatos[1];
                modalDialog.Show(sUrl, self, sSize(690, 290))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) {
                                var aDatos = ret.split("///");
                                if (aDatos[0] == "true") MostrarInactivos();
                                else {
                                    if (aDatos[2] == "false" && $I("chkMostrarInactivos").checked == false) MostrarInactivos();
                                    else {
                                        oFila.getElementsByTagName("LABEL")[0].innerText = aDatos[1];
                                    }
                                }
                            }
                        }
                    });
                break;
            case 3:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleSubGrupo/Default.aspx?SN4=" + aDatos[0];
                sUrl += "&SN3="+ aDatos[1];
                sUrl += "&SN2=" + aDatos[2];
                modalDialog.Show(sUrl, self, sSize(690, 330))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) {
                                var aDatos = ret.split("///");
                                if (aDatos[0] == "true") MostrarInactivos();
                                else {
                                    if (aDatos[2] == "false" && $I("chkMostrarInactivos").checked == false) MostrarInactivos();
                                    else {
                                        oFila.getElementsByTagName("LABEL")[0].innerText = aDatos[1];
                                    }
                                }
                            }
                        }
                    });
                break;
            case 4:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleNat/Default.aspx?SN4=" + aDatos[0];
                sUrl += "&SN3="+ aDatos[1];
                sUrl += "&SN2="+ aDatos[2];
                sUrl += "&SN1=" + aDatos[3];
                modalDialog.Show(sUrl, self, sSize(690, 390))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) {
                                var aDatos = ret.split("///");
                                if (aDatos[0] == "true") MostrarInactivos();
                                else {
                                    if (aDatos[2] == "false" && $I("chkMostrarInactivos").checked == false) MostrarInactivos();
                                    else {
                                        oFila.getElementsByTagName("LABEL")[0].innerText = aDatos[1];
                                    }
                                }
                            }
                        }
                    });
                break;
        }
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error ", e.message);
    }
}

function mostrar(oImg){
    try{
        if (IDActivo != "0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0";
        }
        
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        
        for (var i=nIndexFila+1; i<$I("tblDatos").rows.length; i++){
            if ($I("tblDatos").rows[i].getAttribute("nivel") > nNivel){
                if (opcion == "O")
                {
                    $I("tblDatos").rows[i].style.display = "none";
                    if ($I("tblDatos").rows[i].getAttribute("nivel") < 3)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }
                else if ($I("tblDatos").rows[i].getAttribute("nivel")-1 == nNivel) $I("tblDatos").rows[i].style.display = "table-row";
            }else{
                break;
            }
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

        if (IDActivo != "0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0";
        }

        if (nMostrar == 0){//Contraer
            for (var i=0; i<$I("tblDatos").rows.length;i++){
                if ($I("tblDatos").rows[i].getAttribute("nivel") > 1)
                {
                    if ($I("tblDatos").rows[i].getAttribute("nivel") < 4)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                    $I("tblDatos").rows[i].style.display = "none";
                }
                else 
                {
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
            if ($I("tblDatos").rows[i].getAttribute("nivel") < nNE){ 
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
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }
        
        if (IDActivo != "0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0";
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

function MostrarInactivos(){
    try{
        mostrarProcesando();
        
        if (IDActivo != "0-0-0-0"){
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0";
        }
        
        var js_args = "getEstructura@#@";
        js_args += ($I("chkMostrarInactivos").checked) ? "1@#@":"0@#@";
        js_args += nNE;
        RealizarCallBack(js_args, "");
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
        mostrarErrorSQL(aResul[3], aResul[2], (aResul[4]!=null)? aResul[4]:null);
    }else{
        switch (aResul[0]){
            case "getEstructura":
                $I("divCatalogo").innerHTML = aResul[2];
                IDActivo = "0-0-0-0";
                AccionBotonera("eliminar", "D");
                break;
            case "eliminar":
                setTimeout("MostrarInactivos();", 50);
                break;        
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function insertarItem(nNivelItem){
    try{
        mostrarProcesando();
        var aIDActivo = IDActivo.split("-");
        var sUrl;
        switch (nNivelItem){
            case 1:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleTipol/Default.aspx?SN4=0";
                sUrl += "&SN3=0";
                sUrl += "&SN2=0";
                sUrl += "&SN1=0";
                modalDialog.Show(sUrl, self, sSize(690, 330))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) MostrarInactivos();
                            else ocultarProcesando();
                        }
                    });
                break;
            case 2:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleGrupo/Default.aspx?SN4=" + aIDActivo[0];
                sUrl += "&SN3=0";
                sUrl += "&SN2=0";
                sUrl += "&SN1=0";
                if (aIDActivo[0]=="0")
                    mmoff("War", "Debes seleccionar una tipología de proyecto", 330);
                else
                    modalDialog.Show(sUrl, self, sSize(690, 290))
                        .then(function(ret) {
                            if (ret != null) {
                                if (ret) MostrarInactivos();
                                else ocultarProcesando();
                            }
                        });
                break;
            case 3:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleSubGrupo/Default.aspx?SN4=" + aIDActivo[0];
                sUrl += "&SN3="+ aIDActivo[1];
                sUrl += "&SN2=0";
                sUrl += "&SN1=0";
                if (aIDActivo[1]=="0")
                    mmoff("War", "Debes seleccionar un grupo de naturaleza", 310);
                else
                    modalDialog.Show(sUrl, self, sSize(690, 290))
                        .then(function(ret) {
                            if (ret != null) {
                                if (ret) MostrarInactivos();
                                else ocultarProcesando();
                            }
                        });

                break;
            case 4:
                sUrl = strServer + "Capa_Presentacion/Administracion/EstructuraNat/DetalleNat/Default.aspx?SN4=" + aIDActivo[0];
                sUrl += "&SN3="+ aIDActivo[1];
                sUrl += "&SN2="+ aIDActivo[2];
                sUrl += "&SN1=0";
                if (aIDActivo[2]=="0")
                    mmoff("War", "Debes seleccionar un subgrupo de naturaleza", 310);
                else
                    modalDialog.Show(sUrl, self, sSize(690, 390))
                        .then(function(ret) {
                            if (ret != null) {
                                if (ret) MostrarInactivos();
                                else ocultarProcesando();
                            }
                        });
                    
                break;
        }
        window.focus();
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
    }
}

function marcarLabel(oLabel){
    try{
        if (IDActivo != "0-0-0-0"){
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
        if (IDActivo == "0-0-0-0"){
            AccionBotonera("eliminar", "D");
            return;
            
        }
        var js_args = "eliminar@#@";
        js_args += $I(IDActivo).getAttribute("nivel") +"@#@";
        
        var aIDActivo = IDActivo.split("-");
	    switch($I(IDActivo).getAttribute("nivel")){
	        case "1": js_args += aIDActivo[0]; break;
	        case "2": js_args += aIDActivo[1]; break;
	        case "3": js_args += aIDActivo[2]; break;
	        case "4": js_args += aIDActivo[3]; break;
	    }

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el elemento de estructura.", e.message);
    }
}

function excel(){
    try{
        if ($I("tblDatos")==null){
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
	    for (var i=0;i < $I("tblDatos").rows.length; i++){
		    if ($I("tblDatos").rows[i].style.display == "none") continue;
		    sb.Append("<TR><TD>");
		    aDatos = $I("tblDatos").rows[i].id.split("-");
		    switch($I("tblDatos").rows[i].getAttribute("nivel")){
		        case "1": sb.Append(aDatos[0] + " - " + $I("tblDatos").rows[i].innerText); break;
		        case "2": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[1] + " - " + $I("tblDatos").rows[i].innerText); break;
		        case "3": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[2] + " - " + $I("tblDatos").rows[i].innerText); break;
		        case "4": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[3] + " - " + $I("tblDatos").rows[i].innerText); break;
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