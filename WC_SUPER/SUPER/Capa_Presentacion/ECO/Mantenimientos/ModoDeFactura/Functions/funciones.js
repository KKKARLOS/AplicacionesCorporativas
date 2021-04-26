var aFila, aFilaModoFac;
var bNuevo = false;
var idSN2="", idModoFacnew=-1;
function init(){
    try{
        refrescarModoFac("*");
        eval($I("sModoFac").value);
        aFila = FilasDe("tblDatos");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var idNuevoSN2, idNuevoModoFac;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                idModoFacnew=-1;
                aFilaModoFac = FilasDe("tblDatosModoFac");
                if (aFilaModoFac != null){
                    for (var i=aFilaModoFac.length-1; i>=0; i--){
                        if (aFilaModoFac[i].getAttribute("bd") == "D"){                            
                            tblDatosModoFac.deleteRow(i);
                            continue;
                        }
                        mfa(aFilaModoFac[i],"N");
                    }
                }
                
                //Actualizo el array de los modos de facturación
                
                for (var i= aModoFac.length - 1; i>=0; i--){
                    if (aModoFac[i].bd == "D")
                        aModoFac.splice(i, 1);
                    else
                        aModoFac[i].bd= "";
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;
        }
        ocultarProcesando();
    }
}
function comprobarDatos(){
    try{
        var sNom="";
        var idSN2ant="";
        for (var nIndice=0; nIndice < aModoFac.length; nIndice++){
            if (aModoFac[nIndice].idSN2 != idSN2ant){
                idSN2ant = aModoFac[nIndice].idSN2;
                //nOrden=0;
            }
            if (aModoFac[nIndice].bd != "D"){
                if (aModoFac[nIndice].nombre == ""){
                    for (var i=0; i<aFila.length; i++){
                        if (aFila[i].id == aModoFac[nIndice].idSN2){
                            sNom = aFila[i].cells[0].innerText;
                            break;
                        }
                    }
                    mmoff("War","Debes indicar las descripciones de los modos de facturación en " + sNom,400);
                    ms(aFila[i]);
                    return false;                            
                }
            }
            //nOrden++;               
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function grabar(){
    try{       
        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");

        for (var nIndice=0; nIndice < aModoFac.length; nIndice++){
            if (aModoFac[nIndice].bd != "")
            {
                sb.Append(aModoFac[nIndice].bd +"##"); 
                sb.Append(aModoFac[nIndice].idSN2 +"##"); //nº SN2
                sb.Append(aModoFac[nIndice].idModoFac +"##"); //nº ModoFac
                sb.Append(Utilidades.escape(aModoFac[nIndice].nombre) +"##"); //Valor
                sb.Append(aModoFac[nIndice].estado +"///"); //Activo
            }
        }
        //sb.Append("@#@");
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
function nuevoModoFac(){
    try{
        if (idSN2 == ""){
            mmoff("War", "Para insertar valores, debe seleccionar el supernodo2",360);
            return;
        }
        oNF = $I("tblDatosModoFac").insertRow(-1);
        var iFila=oNF.rowIndex;

        oNF.id = idModoFacnew;
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";

        oNF.onclick = function() { ms(this); }

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true), null);
        
        //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='txtModoFac' class='txtL' 
        //                              onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" 
        //                              style='width:310px' value='' onKeyUp=\"actualizarDatos('U','nombre',this);\" MaxLength='20'>"));
        oNC2 = oNF.insertCell(-1);
        var oCtrl1 = document.createElement("input");
        oCtrl1.setAttribute("style", "width:310px; margin-left:5px;");
        oCtrl1.type = "text";
        //oCtrl1.style.width = "310px";
        //oCtrl1.style.padding = "0px 0px 0px 5px";
        oCtrl1.className = "txtL";
        oCtrl1.maxLength = "20";
        oCtrl1.onfocus = function() { this.className = 'txtM'; this.select(); };
        oCtrl1.onblur = function() { this.className = 'txtL' };
        oCtrl1.onkeyup = function(e) { actualizarDatos(e,'U', 'nombre', this); };
        oNC2.appendChild(oCtrl1);

        //oNF.insertCell(-1).appendChild(document.createElement("<input type='checkbox' class='checkTabla' checked
        //                               onClick=\"actualizarDatos('U','estado',this);\">"));
        oNC3 = oNF.insertCell(-1);
        var oCtrl3 = document.createElement("input");
        oCtrl3.setAttribute("type", "checkbox");
        if (ie)
            oCtrl3.setAttribute("style", "margin-left:1px;");
        else
            oCtrl3.setAttribute("style", "margin-left:4px;");
        oCtrl3.setAttribute("className", "checkTabla");
        oCtrl3.setAttribute("checked", "true");
        //oCtrl3.setAttribute("class", "check");
        oCtrl3.onclick = function(e) { actualizarDatos(e,'U', 'estado', this); };
        oNC3.appendChild(oCtrl3);

        ms(oNF);

        oNF.cells[1].children[0].focus();
        insertarModoFacEnArray("I", idSN2, idModoFacnew--, "", 1);
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo modo de facturación", e.message);
    }
}
function eliminarModoFac(){
    try{
        var sw = 0;
        aFilaModoFac = FilasDe("tblDatosModoFac");
        if (aFilaModoFac == null) return;
        if (aFilaModoFac.length == 0) return;
        for (var i=aFilaModoFac.length-1; i>=0; i--){
            if (aFilaModoFac[i].className == "FS"){
                sw = 1;
                
                oModoFacActivo = buscarModoFacEnArray(aFilaModoFac[i].id);
                if (oModoFacActivo.bd != "I") oModoFacActivo.bd="D";
                else borrarModoFacDeArray(aFilaModoFac[i].id);

                if (aFilaModoFac[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    $I("tblDatosModoFac").deleteRow(i);
                }    
                else{
                    //Se oculta y marca para borrar de BD
                    aFilaModoFac[i].setAttribute("bd", "D");
                    mfa(aFilaModoFac[i], "D");
                }
            }
        }
        if (sw == 0)
            mmoff("Inf", "Selecciona la fila a eliminar", 220);
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function actualizarDatos(e, accion, clave, obj){
    try{
        var sw = 0;
        if (!e) e = event;
        var tecla = (e.keyCode) ? e.keyCode : e.which;
        switch (tecla) {	    
            case 16://shift
            case 17://ctrl
            case 37://flecha izda
            case 38://flecha arriba
            case 39://flecha dcha
            case 40://flecha abajo
                break; 
            default:
	            sw=1;
	    }
	    if (sw==1){
            var oFila = obj.parentNode.parentNode;
            fm_mn(oFila);
            oModoFacActivo = buscarModoFacEnArray(oFila.id);
            oModoFacActualizar(accion, clave, obj)
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los datos", e.message);
    }
}
function refrescarModoFac(id){
    try{
        
        if (id == "*"){
            $I("divValores").children[0].innerHTML = "<table id='tblDatosModoFac' class='texto MANO'></table>";
            return;
        }
        idSN2=id;
        var sb = new StringBuilder;
        sb.Append("<table id='tblDatosModoFac' class='texto MANO' style='WIDTH: 400px;' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:10px;' /><col style='width:330px;' /><col style='width:60px' /></colgroup>");
        sb.Append("<tbody id='tbodyDatosModoFac'>");
        for (var nIndice=0; nIndice < aModoFac.length; nIndice++){
            if (aModoFac[nIndice].idSN2==id){
                var sImagen="imgFN.gif";
                sb.Append("<tr id='" + aModoFac[nIndice].idModoFac + "' style='height:20px' bd='"+aModoFac[nIndice].bd+ "' onclick='ms(this)'>");
                switch(aModoFac[nIndice].bd){
                    case "I":
                        sImagen="imgFI.gif";
                        break;
                    case "U":
                        sImagen="imgFU.gif";
                        break;
                    case "D":
                        sImagen="imgFD.gif";
                        break;
                    default:
                        sImagen="imgFN.gif";
                        break;
                }
                sb.Append("<td><img src='../../../../images/"+sImagen+"'></td>");
                sb.Append("<td><input type='text' id='txtModoFac' class='txtL MANO' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL MANO'\" style='width:320px; margin-left:5px;' value='" + Utilidades.unescape(aModoFac[nIndice].nombre) + "' onKeyUp=\"actualizarDatos(event,'U','nombre',this);\" MaxLength='20'></td>");

                if (aModoFac[nIndice].estado=="1")
                    sb.Append("<td style='padding-left:5px;'><input type='checkbox' class='checkTabla' id='chkEstado' checked onClick=\"actualizarDatos(event,'U','estado',this);\"></td>");
                else
                    sb.Append("<td style='padding-left:5px;'><input type='checkbox' class='checkTabla' id='chkEstado' onClick=\"actualizarDatos(event,'U','estado',this);\"></td>");
                sb.Append("</tr>");                
            }
        }
        sb.Append("</tbody>");
        sb.Append("</table>");
        $I("divValores").children[0].innerHTML = sb.ToString();
	}catch(e){
		mostrarErrorAplicacion("Error al refrescar los valores", e.message);
    }
}
function restaurarFila2(){
    try
    {
        oModoFacActivo = buscarModoFacEnArray(oFilaARestaurar.id);
        if (oModoFacActivo != null) oModoFacActivo.bd="U";        
    }catch(e){
	    mostrarErrorAplicacion("Error al restaurar la fila",e.message);
    }
}
