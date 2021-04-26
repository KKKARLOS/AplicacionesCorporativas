var tbody;
function init(){
    try{
        setEstadistica();
        tbody = document.getElementById('tbodyDatos'); 
        tbody.onmousedown = startDragIMG; 
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
           case "getConsultas":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                tbody = document.getElementById('tbodyDatos'); 
                tbody.onmousedown = startDragIMG; 
                bTodasMostradas = true;
                break;
           /*
           case "ejecutar":
                var xls;
                try {
                    xls = new ActiveXObject("Excel.Application");
                    crearExcel(aResul[4]);
                } catch (e) {
                    crearExcelServerCache("N_HOJAS",aResul[3]);
                }
                break;
             */
           case "grabar":
                setEstadistica();
                break;
               
           default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function ejecutar(oFila){
    try{
        mostrarProcesando();
        var sb = new StringBuilder;
        //sb.Append(encodeURI(oFila.cells[1].children[0].innerHTML) + "@#@");//Denominación de la consula personalizada
        sb.Append(oFila.cells[1].children[0].innerHTML + "@#@");//Denominación de la consula personalizada
        sb.Append(oFila.getAttribute("procalm") + "@#@");

        if (oFila.getAttribute("num_parametros") == "0") {
            //RealizarCallBack(sb.ToString(), "");
            excelDataSet(sb.ToString());
        }
        else {
            var strEnlace = strServer + "Capa_Presentacion/Archivo/MisConsultas/getParametros.aspx?nIdConsulta=" + oFila.id;
	        modalDialog.Show(strEnlace, self, sSize(750, 360))
                .then(function(ret) {
                    if (ret != null) {
                        sb.Append(ret);
                        //RealizarCallBack(sb.ToString(), "");
                        excelDataSet(sb.ToString());
                    }
                    else
                        ocultarProcesando();
	            });
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir a ejecutar la consulta.", e.message);
    }
}
function excelDataSet(sParam) {
    try {
        //if (bProcesando()) return;
        //if ($("#divCatalogo div").first().html() == "") {
        //    ocultarProcesando();
        //    return;
        //}

        if ($I("iFrmDescarga") == null) {
            var oIFrmDescarga = document.createElement("iframe");
            oIFrmDescarga.setAttribute("id", 'iFrmDescarga');
            oIFrmDescarga.setAttribute("name", 'iFrmDescarga');
            oIFrmDescarga.setAttribute('width', '10px');
            oIFrmDescarga.setAttribute('height', '10px');
            oIFrmDescarga.setAttribute("style", "visibility:hidden;");
            document.forms[0].appendChild(oIFrmDescarga);
        }

        //Consulta personalizada
        token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/Documentos/getDocOffice.aspx?";
        strEnlace += "descargaToken=" + token;
        strEnlace += "&sOp=" + codpar("ConsultaPersonalizada");
        strEnlace += "&par=" + codpar(sParam);

        mostrarProcesando();
        initDownload();
        $I("iFrmDescarga").src = strEnlace;
    } catch (e) {
        mostrarErrorAplicacion("Error al exportar la información.", e.message);
    }
}
var bTodasMostradas = false;
function getTodasConsultas(){
    try{
        if (bTodasMostradas) return;
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("getConsultas@#@0");
        
        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las consultas.", e.message);
    }
}

function setActivo(oControl){
    try{
        if (oControl.checked) oControl.setAttribute("checked", "true");
        else oControl.setAttribute("checked", "false");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las consultas.", e.message);
    }
}
function activarGrabar(){
    try{
        //se utiliza la función activarGrabar() para grabar, porque no queremos
        //mostrar la botonera
        //alert("grabar");
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        
        var aFila = FilasDe("tblDatos");
        for (var i=0; i<aFila.length; i++){
            sb.Append(aFila[i].id +"##"); //idConsulta
            //sb.Append(i +"##"); //orden
            sb.Append((aFila[i].getAttribute("activa") == "1") ? "1///" : "0///"); //estado
        }
        
        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}

function setEstadistica(){
    try{
        var nCountActivas = 0;
        var aFila = FilasDe("tblDatos");
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("activa") == "1") nCountActivas++;
        }
        $I("cldActivas").innerText = nCountActivas.ToString("N", 9, 0);
        $I("cldInactivas").innerText = (nConsultas - nCountActivas).ToString("N", 9, 0);
	}catch(e){
		mostrarErrorAplicacion("Error al establecer las estadísticas.", e.message);
	}
}

function ActivarDesactivar(){
    try{
        var nCountActivas = 0;
        var aFila = FilasDe("tblDatos");
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("activa") == "1") {
                    aFila[i].setAttribute("activa", "0");
                    aFila[i].cells[1].style.color = "#CCCCCC";
                } else 
                {
                    aFila[i].setAttribute("activa", "1");
                    aFila[i].cells[1].style.color = "black";
                }
                aFila[i].className = "";
            }
        }
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al establecer las estadísticas.", e.message);
	}
}

