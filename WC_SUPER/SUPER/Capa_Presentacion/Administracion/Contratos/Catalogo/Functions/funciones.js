function init(){
    try{
        if (!mostrarErrores()) return;
        ToolTipBotonera("Duplicar", "Genera contratos en SUPER a partir de oportunidades en HERMES");
        LiteralBotonera("Duplicar", "Generar");

        try{$I("txtCadena").focus();}catch(e){};
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function NumContrato(oContrato){
    try{      
        mostrarProcesando();
        var js_args = "NumContrato@#@"+dfn(oContrato.value);
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al buscar contrato por su número.", e.message);
    }
}
function buscarNum(){
    try{
        buscar($I("txtCadena").value);   
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function buscar(strCadena){
    try{
        if (strCadena == ""){
            mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
            return;
        }
        var js_args = "contrato@#@";
        var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
        js_args += sAccion + "@#@";
        js_args += Utilidades.escape(strCadena) + "@#@";
        //js_args += sInterno;

        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
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
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "NumContrato":                
            case "contrato":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("txtIdContrato").value = "";
                $I("txtCadena").value = "";
                actualizarLupas("tblTitulo", "tblDatos");
                break;
            case "eliminar":               
                aFilas = $I("tblDatos").getElementsByTagName("TR");
                for (i=aFilas.length-1;i>=0;i--)
                {
                    if (aFilas[i].className == "FS") $I("tblDatos").deleteRow(i); 
                }     
                break;                 
        }
        ocultarProcesando();
    }
}

function Detalle(oFila)
{
    try{  
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Contratos/Detalle/Default.aspx?ID=" + codpar(oFila.id);
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 530));

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}
function PoolFVP() 
{
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Contratos/Pool/Default.aspx";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 530));
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la función PoolFVP", e.message);
    }
}
function generarContratos() {
    try {
        //mostrarProcesando();
        location.href = strServer + "Capa_Presentacion/Administracion/ContratosHermes/Default.aspx";
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función generarContratos", e.message);
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
        var strID="";
        for (i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].className == "FS"){
                intFila = aFilas[i].rowIndex;
                strID += aFilas[i].id +"##" + Utilidades.escape(aFilas[i].cells[0].innerHTML) + "///";                
            }
        }
    	
	    if (strID=="")
	    {
	        mmoff("Inf", "No hay ninguna fila seleccionada.", 230);
	        return;
	    }

        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "eliminar@#@" + strID;
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
