var aFila;

function init(){
    try{
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
  		    //alert("No se puede eliminar el elemento '"+aResul[3]+"',\n ya que existen elementos con los que está relacionado.");
		    alert("No se puede eliminar el medio,\n ya que existen elementos con los que está relacionado.");
		}
		else alert(sError.replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                var sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D") {
                        tblDatos.deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].id = aEI[nIndiceEI]; 
                        nIndiceEI++;
                    }
                    mfa(aFila[i],"N");
                }
//                for (var i=0;i<aFila.length;i++){
//                    aFila[i].orden = i;
//                }
                
                nFilaDesde = -1;
                nFilaHasta = -1;
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();

                break;
        }
        ocultarProcesando();
    }
}


function nuevo(){
    try{
        if (iFila != -1) modoControles(tblDatos.rows[iFila], false);

        //oNF --> objeto nueva fila
        oNF = tblDatos.insertRow();
        oNF.id = oNF.rowIndex;
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        //oNF.orden = oNF.rowIndex;
        oNF.attachEvent('onclick', mm);

//      oNF.insertCell().appendChild(document.createElement("<img src='../../../../images/imgFI.gif'>"));
//	    oNF.insertCell().appendChild(document.createElement("<input type='text' class='txtL' style='width:480px' value='' maxlength='30' onKeyUp='fm(event)'>"));

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);
        var oCtrl1 = document.createElement("input");

        oCtrl1.type = "text";
        oCtrl1.style.width = "480px";
        oCtrl1.className = "txtL";
        oCtrl1.value = "";
        oCtrl1.maxLength = "30";
        oCtrl1.onkeyup = function() { fm_mn(this) };

        oNC2.appendChild(oCtrl1);

        seleccionar(oNF);
	    oNF.cells[1].children[0].focus();
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}

function eliminar(){
    try{
        if (iFila != -1) modoControles(tblDatos.rows[iFila], false);

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

function grabar(){
    try{
        if (iFila != -1) modoControles(tblDatos.rows[iFila], false);

        aFila = FilasDe("tblDatos");
        if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Medio
                sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"); //Descripcion
                sb.Append("///"); // Fila
                sw = 1;
            }
        }
        if (sw == 0){
            desActivarGrabar();                  
            alert("No se han modificado los datos.");
            return;
        }
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos(){
    try{
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].cells[1].children[0].value == ""){
                //msse(aFila[i]);
                ms(aFila[i]);
                alert("Debe indicar la denominación del Medio");
                aFila[i].cells[1].children[0].focus();
                return false;
            }
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}



