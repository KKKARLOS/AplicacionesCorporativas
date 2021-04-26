function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
        aFilas = FilasDe("tblDatos");
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function Detalle(oFila)
{
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Empresas/Detalle/Default.aspx?ID=" + oFila.id + "&bNueva=false"; ;
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(700, 375))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    if (aDatos[0] != "") {
                        oFila.cells[0].innerText = aDatos[0];
                        //Si los datos grabados implican modificación (o elimanción) de la fila, se obtienen de nuevo los datos
                        if (((aDatos[1] == "false") && (oFila.cells[0].style.color != "gray")) ||
                            ((aDatos[1] == "true") && (oFila.cells[0].style.color == "gray"))) {
                            ObtenerDatos();
                        }
                    }
                    else
                        ObtenerDatos();
                }
            });
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}
function ObtenerDatos(){
    try{
        mostrarProcesando();        
        var js_args = "getDatos@#@";
        js_args += ($I("chkActivas").checked) ? "1@#@" : "0@#@";

        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function Nuevo()
{
	try
	{
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/Administracion/Empresas/Detalle/default.aspx?bNueva=true";
	    //window.focus();
	    modalDialog.Show(strEnlace, self, sSize(700, 375));
	    
		ObtenerDatos(); 
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función Nuevo", e.message);	        
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

        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
            if (answer){
                mostrarProcesando();
                var js_args = "eliminar@#@" + intID;
                js_args = js_args.substring(0, js_args.length - 3);
                RealizarCallBack(js_args, "");  //con argumentos
            }
        });

	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función Eliminar", e.message);	        
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
		    mostrarError("No se puede eliminar el item seleccionado,\nya que existen elementos con los que está relacionado.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getDatos":
                //$I("tblDatos").outerHTML = aResul[2];
                $I("divCatalogo").children[0].innerHTML = aResul[2];
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
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}


