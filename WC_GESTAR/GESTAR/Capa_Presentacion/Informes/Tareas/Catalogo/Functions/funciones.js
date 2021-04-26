function Excel(){
    try{
    	if ($I('tblCatalogo').rows.length==0){
            //ocultarProcesando();
            return;
        }

        aFila = $I("tblCatalogo").getElementsByTagName("tr");
  
        var sb = new StringBuilder;

 
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        //13/06/2016 Por petición de Víctor quitamos el check
		//sb.Append("     <td style='width:auto;'>Check</TD>");
        sb.Append("     <td style='width:auto;'>Tarea</TD>");
        sb.Append("     <td style='width:auto;'>Orden</TD>");
        sb.Append("     <td style='width:auto;'>Área</TD>");
        sb.Append("     <td style='width:auto;'>Avance</TD>");   
        sb.Append("     <td style='width:auto;'>Resultado</TD>");
        sb.Append("     <td style='width:auto;'>" + $I("lblfecha").title + "</TD>");
		sb.Append("	</TR>");
		sb.Append("</TABLE>");
		       
        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
        for (var i=0;i < aFila.length; i++){
		    if (aFila[i].style.display == "none") continue;
            //sb.Append(aFila[i].outerHTML);
		    sb.Append("<tr>");
		    for (var j = 1; j < 7; j++) {
		        sb.Append(aFila[i].cells[j].outerHTML);
		    }
		    sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function D(){
    try
    {
        aObjInput = $I("tblCatalogo").getElementsByTagName("INPUT");
        var strCadena = "";
        for (i=0; i<aObjInput.length; i++){ 
            if (aObjInput[i].checked==true)
            {   
                strCadena+=aObjInput[i].id+",";
            }
        } 
        if (strCadena=="") $I('rdlInforme_0').checked=true;
        else $I('rdlInforme_1').checked=true;
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función D", e.message);	
	} 
}   
function Exportar(){
    try
    {
        if ($I('tblCatalogo').rows.length==0){
            mmoff("War", "No hay datos para imprimir", 210);
		    return
	    }
	    var strUrlPag;
        if ($I('rdlInforme_1').checked==true)
        {
            aObjInput = $I("tblCatalogo").getElementsByTagName("INPUT");
            var strCadena = "";
            for (i=0; i<aObjInput.length; i++){ 
	            if (aObjInput[i].checked==true)
	            {   
	                strCadena+=aObjInput[i].id+",";
                }
            }
            if (strCadena==""&&$I('rdlInforme_1').checked==true)
            {
                mmoff("War", "Debe marcar alguna orden", 210);
                return;
            }
            strCadena=strCadena.substring(0,strCadena.length-1);
            //if ($I('hdnOpcion').value=='PANT_AVANZADO')
            //{

            //}
            strUrlPag = "../../Tareas/Avanzado/Informes/Detalle/default.aspx";   
            strUrlPag += "?ID="+strCadena;
	        //alert(strCadena);
        }  
        else
        {
            //if ($I('hdnOpcion').value=='PANT_AVANZADO')
            //{
                strUrlPag = "../../Tareas/Avanzado/Informes/Relacion/default.aspx";   
            //}    
        } 

	    if (screen.width == 800)
		    window.open(strUrlPag,"", "resizable=yes,status=no,scrollbars=yes,menubar=no,top=0,left=0,width="+eval(screen.availwidth-15)+",height="+eval(screen.availheight-37));	
	    else
		    window.open(strUrlPag,"", "resizable=yes,status=no,scrollbars=no,menubar=no,top=0,left=0,width="+eval(screen.availwidth-15)+",height="+eval(screen.availheight-37));							
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Exportar", e.message);	
	} 
}

function Regresar(){
}

function unload(){
}

function init(){
    if ($I("lblfecha").innerText=='F.F.R.') $I("lblfecha").title="Fecha fin real";
    mTabla(1);
	//document.all("procesando").style.visibility = "visible";
	//setTimeout("ObtenerDatos()",20);
}

bMover = false;

function moverTablaUp(){
    try
    {
	    if (bMover){
		    $I("divCatalogo").doScroll("up");
		    setTimeout("moverTablaUp()",100);
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaUp", e.message);	
	} 	
}	

function mTabla(sOp)
{
    try
    {
	    var aInput = document.getElementsByTagName("input");
	    for (i=0;i<aInput.length;i++){
	        if (aInput[i].type != "checkbox") continue;
            if (sOp==1)
            {   
                aInput[i].checked=true;
            }
            else
            {
                aInput[i].checked=false;
            }
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función mTabla", e.message);	
	} 	
}	
function D_Tarea(oFila){
    try
    {
        ID = oFila.id.split("//");    
//	    var winSettings = 'center:yes;resizable:no;help:no;status:no;dialogWidth:940px;dialogHeight:620px';
//        var ret = window.showModalDialog('../../../Tarea/default.aspx?ID=&bNueva=false'+'&IDAREA='+ ID[0]+'&AREA='+ escape(ID[1])+'&IDDEFICIENCIA='+  ID[2] +'&DEFICIENCIA='+ escape(ID[3]) +'&IDTAREA=' + ID[4] + '&ES_COORDINADOR='+ ID[5] +'&ADMIN=N&ESTADO='+ ID[6]+ '&CORREOCOORDINADOR='+escape(ID[7]) + '&COORDINADOR='+escape(ID[8]), self, winSettings);
        var strEnlace = strServer + 'Capa_Presentacion/Tarea/default.aspx?ID=&bNueva=false'+'&IDAREA='+ ID[0]+'&AREA='+ escape(ID[1])+'&IDDEFICIENCIA='+  ID[2] +'&DEFICIENCIA='+ escape(ID[3]) +'&IDTAREA=' + ID[4] + '&ES_COORDINADOR='+ ID[5] +'&ADMIN=N&ESTADO='+ ID[6]+ '&CORREOCOORDINADOR='+escape(ID[7]) + '&COORDINADOR='+escape(ID[8]);
        modalDialog.Show(strEnlace, self, sSize(940,620))
        .then(function(ret) {
            if (ret != null) {
            }    
        }); 
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función D_Tarea", e.message);	
	}    	    	    	    	    
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    try
    {
        var aResul = strResultado.split("@@");
        if (aResul[1] != "OK")
        {
            ocultarProcesando();
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        }
        else
        {    
	        switch (aResul[0]) 
	        {	
		        case "leer": 		
		            //$I("tblCatalogo").outerHTML = aResul[2];
		            $I("divCatalogo").children[0].innerHTML = aResul[2];
		            ocultarProcesando();	
	                break;	   	        
		        case "volcar": 		
		            //VolcarTareasExcel(aResul[2]);
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

		            ocultarProcesando();
	                break;	                                  
                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);	        
		    }
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función RespuestaCallBack", e.message);	
	}		
}
/*
function VolcarTareasExcel(strTabla)
{ 
    try
    {
        crearExcel(strTabla);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
*/
function Volcar()
{ 
	if ($I('tblCatalogo').rows.length==0) return;
   	    var js_args = "volcar";
        RealizarCallBack(js_args,"");  //con argumentos
}
function Cargar(iColum,iOrden)
{ 
	if ($I('tblCatalogo').rows.length==0) return;
    var js_args = "leer"+"@@"+iColum+"@@"+iOrden;
    RealizarCallBack(js_args,"");  //con argumentos
}
