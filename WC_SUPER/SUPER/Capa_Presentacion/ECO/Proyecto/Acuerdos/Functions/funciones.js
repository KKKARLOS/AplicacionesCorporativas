var aAc = new Array(); // Array de espacios de acuerdo
function init(){
    try{
        if (!mostrarErrores()) return;
        ms($I("tblDatos").rows[0]);
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana(){
    try{
        //if (bProcesando()) return;
        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function mostrarAcuerdo(nIdAcuerdo){
    try{
        if (nIdAcuerdo == null) return;
        var bEncontrado = false;
        for (var i=0; i<aAc.length; i++){
            if (aAc[i][0] == nIdAcuerdo){
                bEncontrado = true;
                //$I("txtAcuerdoCal").value = aAc[i][7];
                $I("chkSopFactIap").checked = (aAc[i][1]=="1")? true:false;
                $I("chkSopFactResp").checked = (aAc[i][2]=="1")? true:false;
                $I("chkSopFactCli").checked = (aAc[i][3]=="1")? true:false;
                $I("chkSopFactFijo").checked = (aAc[i][4]=="1")? true:false;
                $I("chkSopFactOtro").checked = (aAc[i][5]=="1")? true:false;
                $I("txtFactOtros").value = Utilidades.unescape(aAc[i][6]);
                $I("chkSopFactConcilia").checked = (aAc[i][9]=="1")? true:false;
                //Radiobutton Antes / Despues o null
                $I("rdbAcuerdo_0").checked = false;
                $I("rdbAcuerdo_1").checked = false;
                if (aAc[i][10]=="A")
                    $I("rdbAcuerdo_0").checked = true;
                else if (aAc[i][10]=="D")
                    $I("rdbAcuerdo_1").checked = true;
                
                $I("txtContacto").value = Utilidades.unescape(aAc[i][11]);
                $I("txtPeriodocidadFactura").value = Utilidades.unescape(aAc[i][7]);
                $I("txtFacturaInformacion").value = Utilidades.unescape(aAc[i][8]);
                //$I("txtAceptFecha").value = aAc[i][12];
                //$I("txtAceptNombre").value = aAc[i][14];
                $I("chkFactSA").checked = (aAc[i][13]=="1")? true:false;
                break;
            }
            else 
                if (bEncontrado || parseInt(aAc[i][0]) < parseInt(nIdAcuerdo)) 
                    break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la información para facturación.", e.message);
	}
}
