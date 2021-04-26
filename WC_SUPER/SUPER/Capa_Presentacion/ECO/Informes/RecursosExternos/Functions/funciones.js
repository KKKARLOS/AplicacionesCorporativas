var strAction = "";
var strTarget = "";
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();

function init(){
    try{
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target;       
        $I("lblProveedor").className = "enlace";
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
           case "generarExcel":
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
           default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function borrarProveedores(){
    $I("tblConceptos").innerHTML="<TABLE id='tblConceptos' style='border-top:10px; text-align:left; width:100%;'><tr id='*' class='FA'><td>&lt; Todos &gt;</td></tr></TABLE>";      
}
function CargarDatos(strOpcion){
    try{      
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;
        ("tblConceptos").innerHTML = "<TABLE id='tblConceptos' style='border-top:10px; text-align:left; width:100%;'><tr id='*' class='FA'><td>&lt; Todos &gt;</td></tr></TABLE>";
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=17";
        mostrarProcesando();
        //var ret = window.showModalDialog(strEnlace, self, sSize(850, 420));
        modalDialog.Show(strEnlace, self, sSize(850, 420))
	        .then(function(ret) {
                if (ret != null && ret != "")
                {
                    var aElem= ret.split("|||");
                    var aOpciones = aElem[0].split("///");
                    BorrarFilasDe("tblConceptos");
                    for (var i=0;i<aOpciones.length;i++)
                    {
                        var aDatos = aOpciones[i].split("@#@");
                        insertarTabla(aDatos[0], Utilidades.unescape(aDatos[1]));
                    }
                }
                ocultarProcesando();   
	        });                   
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los datos", e.message);
    }
}
function insertarTabla(id, nombre){
    try{        
        strNuevaFila = $I("tblConceptos").insertRow(-1);
        var iFila=strNuevaFila.rowIndex;
        if (iFila % 2 == 0) strNuevaFila.className = "FA";
        else strNuevaFila.className = "FB";

        strNuevaFila.id = id;
        strNuevaCelda1 = strNuevaFila.insertCell(-1);
        //strNuevaCelda1.innerText = nombre;
        strNuevaCelda1.innerHTML = "<label style='padding-left:5px;width:350px;cursor:pointer;'>"+nombre+"</label>";  
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una fila en la tabla", e.message);
    }
}

function Exportar(strFormato){
    try{     
    
  	    objTabla = $I("tblConceptos");
      
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
	        if (objTabla.rows[i].id=="*") 
	        {
	            strCadena+=",";
	            break;
	        }
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    strCadena=strCadena.substring(0,strCadena.length-1);
	    
	    //alert(strCadena);	    
	   // if (strCadena=="") return;

	    $I("FORMATO").value = strFormato; 

        if ($I('chkActivo').checked==true && $I('chkBaja').checked==false) $I("ESTADO").value="A"
        else if ($I('chkActivo').checked==false && $I('chkBaja').checked==true) $I("ESTADO").value="B"
        else $I("ESTADO").value="T"

		$I("CODIGO").value = strCadena;
		
		var sScroll = "no";
		if (screen.width == 800) sScroll = "yes";

        //*SSRS

		var params = {
		    reportName: "/SUPER/sup_recursosexternos",
		    tipo: "PDF",
		    ESTADO: $I("ESTADO").value,
		    CODIGO: $I("CODIGO").value,
		    t422_idmoneda: t422_idmoneda,
		    ImportesEn: ImportesEn
		}

		PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR
        document.forms["aspnetForm"].action="Exportar/default.aspx";
		document.forms["aspnetForm"].target="_blank";
		document.forms["aspnetForm"].submit();
        //CR*/
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}               

function generarExcel(){
    try{
        document.forms["aspnetForm"].action=strAction;
        document.forms["aspnetForm"].target=strTarget;

  	    objTabla = $I("tblConceptos");
      
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
	        if (objTabla.rows[i].id=="*") 
	        {
	            strCadena+=",";
	            break;
	        }	    
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    
	    strCadena=strCadena.substring(0,strCadena.length-1);

        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("generarExcel@#@");
        
        sb.Append(strCadena+"@#@");

        if ($I('chkActivo').checked==true && $I('chkBaja').checked==false) sb.Append("A@#@");
        else if ($I('chkActivo').checked==false && $I('chkBaja').checked==true) sb.Append("B@#@");
        else sb.Append("T@#@");
        
        sb.Append(location.href.substring(0,location.href.length-13),+"@#@");
        RealizarCallBack(sb.ToString(), "");        
	}catch(e){
		mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}
function Control(obj,op)
{
    if ($I('chkActivo').checked==false&&$I('chkBaja').checked==false) 
    {
        if (op==1) $I('chkBaja').checked=true;
        else $I('chkActivo').checked=true;
    }
}
function Obtener(){
    try{
        $I('imgImpresora').src='../../../../Images/imgImpresora.gif';
        setTimeout("$I('imgImpresora').src='../../../../Images/imgImpresorastop.gif';", 10000); 
    
        if ($I('rdbFormato_0').checked==true) Exportar('PDF');
        else if ($I('rdbFormato_1').checked==true) generarExcel();		
	}catch(e){
		mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}
function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDC";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("lblMonedaImportes").innerText = aDatos[1];
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}