var aPlantillas = new Array();
var aHitos = new Array();
var oPlantActivo,oPlantHitoActivo;

function oPlantilla(idPlant, idElemento, tipo, orden, margen, nombre, fact){
	this.idPlant    = idPlant;
	this.idElemento	= idElemento;
	this.tipo   	= tipo;
	this.nombre		= nombre;
	this.orden		= orden;
	this.margen  	= margen;
	this.fact  	    = fact;
}
function oHito(idPlant, idElemento, orden, nombre){
	
	this.idPlant    = idPlant;
	this.idElemento	= idElemento;
	this.nombre		= nombre;
	this.orden		= orden;
}
function insertarPlantEnArray(idPlant, idElemento, tipo, orden, margen, nombre, fact){
    try{
        oRec = new oPlantilla(idPlant, idElemento, tipo, orden, margen, nombre, fact);
        aPlantillas[aPlantillas.length]= oRec;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una plantilla en el array.", e.message);
    }
}
function insertarPlantHitoEnArray(idPlant, idElemento, orden, nombre){
    try{
        oRec = new oHito(idPlant, idElemento, orden, nombre);
        aHitos[aHitos.length]= oRec;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar los hitos de una plantilla en el array.", e.message);
    }
}
function buscarPlantillaEnArray(idPlant){
    try{
        for (var nIndice=0; nIndice < aPlantillas.length; nIndice++){
            if (aPlantillas[nIndice].idPlant == idPlant){
                oPlantActivo = aPlantillas[nIndice];
                return aPlantillas[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar una plantilla en el array.", e.message);
    }
}
function buscarPlantillaHitoEnArray(idPlant){
    try{
        for (var nIndice=0; nIndice < aHitos.length; nIndice++){
            if (aHitos[nIndice].idPlant == idPlant){
                oPlantHitoActivo = aHitos[nIndice];
                return aHitos[nIndice];
            }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar hitos de una plantilla en el array.", e.message);
    }
}
function buscarIndicePlantillaEnArray(idPlant){
    try{
        for (var nIndice=0; nIndice < aPlantillas.length; nIndice++){
            if (aPlantillas[nIndice].idPlant == idPlant)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de una plantilla en el array.", e.message);
    }
}
function buscarIndicePlantillaHitoEnArray(idPlant){
    try{
        for (var nIndice=0; nIndice < aHitos.length; nIndice++){
            if (aHitos[nIndice].idPlant == idPlant)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar el índice de hitos de una plantilla en el array.", e.message);
    }
}
function insertarPlantilla(sListaElementos){
    var sCad,aElem,aElem2,nIdPlant="-1";
    try{
        //sPlant + "##" + sTarea + "##" + sDesTipo + "##" + sOrden + "##" + sMargen + "##" + sDesc + "///"
        aElem=sListaElementos.split("///");
        for (var i=0; i<aElem.length;i++){ 
            sCad=aElem[i];
            if (sCad != ""){
                aElem2=sCad.split("##");
                nIdPlant=aElem2[0];
                insertarPlantEnArray(nIdPlant, aElem2[1], aElem2[2], aElem2[3], aElem2[4], aElem2[5], aElem2[6]);
            }
        }
        return nIdPlant;
	}catch(e){
		mostrarErrorAplicacion("Error al almacenar la plantilla en el array", e.message);
    }
}
function insertarPlantillaHito(sListaElementos){
    var sCad,aElem,aElem2,nIdPlant="-1";
    try{
        //sPlant + "##" + codigoHito + "##" + sDesTipo + "##" + sOrden + "##" + sMargen + "##" + sDesc + "///"
        aElem=sListaElementos.split("///");
        for (var i=0; i<aElem.length;i++){
            sCad=aElem[i];
            if (sCad != ""){
                aElem2=sCad.split("##");
                nIdPlant=aElem2[0];
                //                                  codHito     Orden       Desc
                insertarPlantHitoEnArray(nIdPlant, aElem2[1], aElem2[3], aElem2[5]);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al almacenar los hitos de la plantilla en el array", e.message);
    }
}
function generarHtmlPlantilla(nIdPlant, sTipoPlant){
    var sRes="";
    try{
        sRes="<table id='tblTareas' style='width:420px;text-align:left'>";
        sRes += "<colgroup><col style='width:20px' /><col style='width:400px' /></colgroup>";
        for (var nIndice=0; nIndice < aPlantillas.length; nIndice++){
            var objRec = aPlantillas[nIndice];
            if (objRec.idPlant == nIdPlant){
                sRes+="<tr id='" + objRec.idElemento + "' tipo='" + objRec.tipo + "' ";

                if ((sTipoPlant == "T")&&(objRec.tipo == "P"))
                {
                    sRes+="style='display:none;height:20px;' ";
                }else{
                    sRes+="style='height:20px;' ";
                }
                //sRes += " sFact='" + objRec.fact + "' margen='" + objRec.margen + "px' onmouseover='TTip(event);'>";
                sRes += " sFact='" + objRec.fact + "' margen='" + objRec.margen + "' onmouseover='TTip(event);'>";
                //Columna 1
                sRes+="<td>";
                switch (objRec.tipo)
                {
                    case "P":
                        sRes+="<img src='../../../Images/imgProyTecOff.gif' border='0' title='P.T.'>";
                        break;
                    case "F":
                        sRes+="<img src='../../../Images/imgFaseOff.gif' border='0' title='Fase'>";
                        break;
                    case "A":
                        sRes+="<img src='../../../Images/imgActividadOff.gif' border='0' title='Actividad'>";
                        break;
                    case "T":
                        sRes+="<img src='../../../Images/imgTareaOff.gif' border='0' title='Tarea'>";
                        break;
                    case "H":
                        sRes+="<img src='../../../Images/imgHitoOff.gif' border='0' title='Hito'>";
                        break;
                }
                //Columna 2
                sRes+="</td><td><nobr class='NBR' style='width:335px;padding-left:" + objRec.margen + "px;'>" + objRec.nombre + "</nobr></td></tr>";
            }//if
        }//for
        sRes+="</table>";
        return sRes;
	}catch(e){
		mostrarErrorAplicacion("Error al generar la estructura de la plantilla", e.message);
    }
}
function generarHtmlPlantillaHito(nIdPlant, sTipoPlant){
    var sRes="",sDesc,sHito;
    var iId=0;
    try{
        sRes = "<table id='tblHitos' style='width:420px;text-align:left'>";
        sRes += "<colgroup><col style='width:35px' /><col style='width:385px' /></colgroup>";
        for (var nIndice=0; nIndice < aHitos.length; nIndice++){
            var objRec = aHitos[nIndice];
            if (objRec.idPlant == nIdPlant){

                iId++;
                sDesc = objRec.nombre; 
                sHito = objRec.idElemento;

                sRes+="<tr id='" + sHito + "'style='height:20px;' onmouseover='TTip(event);'>";
                //Columna 1
                sRes+="<td><img src='../../../Images/imgHitoOff.gif' border='0' title='Hito'></td>";
                //Columna 2
                sRes+="<td><nobr class='NBR' style='width:280px;'>" + sDesc + "</nobr></td></tr>";
            }//if
        }//for
        sRes+="</table>";
        return sRes;
	}catch(e){
		mostrarErrorAplicacion("Error al generar la estructura de hitos de la plantilla", e.message);
    }
}

