
function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function Detalle(oFila)
{
    try{    
	    var strEnlace = "../Detalle/Default.aspx?ID="+ oFila.id + "&bNueva=false"; ;         
	    //var ret = window.showModalDialog(strEnlace, self, "dialogwidth:630px; dialogheight:200px; center:yes; status:NO; help:NO;");
	    modalDialog.Show(strEnlace, self, sSize(630, 200))
	        .then(function(ret) {
	                if (ret != null) ObtenerDatos();
	        });                
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}
function ObtenerDatos(){
    try{
        mostrarProcesando();        
        var js_args = "getDatos@#@";
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function nuevo()
{
	try
	{
		var strEnlace ="../Detalle/default.aspx?bNueva=true"; 
//	    var ret = window.showModalDialog(strEnlace, self, "dialogwidth:630px; dialogheight:275px; center:yes; status:NO; help:NO;");
//		if (ret!=null) ObtenerDatos();
		modalDialog.Show(strEnlace, self, sSize(630, 200))
	        .then(function(ret) {
	            if (ret != null) ObtenerDatos();
	        });  
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función nuevo", e.message);	        
    }	
}
function eliminar(){
    try{
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("bd") == "I") {
                    tblDatos.deleteRow(i);
                }else{
                    mfa(aFila[i], "D");
                }
            }
        }
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
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
		    alert("No se puede eliminar el item seleccionado,\nya que existen elementos con los que está relacionado.");
		}
		else alert(sError.replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "getDatos":
                $I("tblDatos").outerHTML = aResul[2];
                break;
            case "grabar":
                var sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D") {
                        tblDatos.deleteRow(i);
                        continue;
                    }
//                    else if (aFila[i].bd == "I"){
//                        aFila[i].id = aEI[nIndiceEI]; 
//                        nIndiceEI++;
//                    }
                    mfa(aFila[i],"N");
                }

                nFilaDesde = -1;
                nFilaHasta = -1;
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;      
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
        }
        ocultarProcesando();
    }
}
function grabar(){
    try{
        aFila = FilasDe("tblDatos");
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "D"
                sb.Append(aFila[i].id +"##"); //ID Medio
                sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"); //Descripcion
                sb.Append("///"); // Fila
                sw = 1;
            }
        }
        if (sw == 0){
            desActivarGrabar();                  
            mmoff("Inf","No se han modificado los datos.",260);
            return;
        }
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
