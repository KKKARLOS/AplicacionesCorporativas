
function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

function Detalle(oFila)
{
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Pruebas/MtoCtasCur/Detalle/Default.aspx?ID=" + oFila.id;
        modalDialog.Show(strEnlace, self, sSize(1010, 545));
        window.focus();
        ObtenerDatos();        
	}catch(e){
		mostrarErrorAplicacion("Error en la funci�n Detalle", e.message);
    }
}
function ObtenerDatos(){
    try {
        mostrarProcesando();
        var js_args = "getDatos@#@";
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function Nuevo()
{
	try {
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/Pruebas/MtoCtasCur/Detalle/default.aspx?bNueva=true";
	    modalDialog.Show(strEnlace, self, sSize(1010, 545));
	    window.focus();
		ObtenerDatos(); 
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la funci�n Nuevo", e.message);	        
    }	
}
function eliminar()
{
    try
    {
	    if ($I("tblDatos")==null) return;
	    if ($I("tblDatos").rows.length==0) return;

        aFilas = $I("tblDatos").getElementsByTagName("TR");
        intFila = -1;
        var intID="";
        for (i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].className == "FS"){
                intFila = aFilas[i].rowIndex;
                intID += aFilas[i].id + "///";                
            }
        }
    	
	    if (intID=="")
	    {
	        mmoff("Inf", "No hay ninguna fila seleccionada.", 230);
	        return;
	    }

        jqConfirm("", "�Est�s conforme?", "", "", "war", 200).then(function (answer) {
            if (answer) 
            {
	            mostrarProcesando();
                var js_args = "eliminar@#@"+intID;
                js_args=js_args.substring(0,js_args.length-3);	 
                RealizarCallBack(js_args,"");  //con argumentos
            }
            else return;
        });
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la funci�n Eliminar", e.message);	        
    }	
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar el item seleccionado,\nya que existen elementos con los que est� relacionado.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getDatos":
                $I("tblDatos").outerHTML = aResul[2];
                break;
            case "eliminar":               
                aFilas = $I("tblDatos").getElementsByTagName("TR");
                for (i=aFilas.length-1;i>=0;i--)
                {
                    if (aFilas[i].className == "FS")
                    {
                        $I("tblDatos").deleteRow(i); 
                    }
                }     
                break;        
            default:
                ocultarProcesando();
                mmoff("Err", "Opci�n de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
