var aFila;
var sElementosInsertados = "";

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
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].id = aEI[nIndiceEI]; 
                        nIndiceEI++;
                    }
                    mfa(aFila[i],"N");
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;
        }
        ocultarProcesando();
    }
}

function nuevo(){
    try{
        //if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        //oNF --> objeto nueva fila
        oNF = $I("tblDatos").insertRow(-1);
        //oNF.id = oNF.rowIndex;
        oNF.id = -1;
        
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("orden", oNF.rowIndex);
        oNF.setAttribute("style", "height:20px");

        //(typeof document.detachEvent != 'undefined') ? oNF.attachEvent('onclick', mm) : oNF.addEventListener('click', mm, false);
        oNF.attachEvent('onclick', mm);

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);
        
        var oCtrl1 = document.createElement("input");
        //oCtrl1.setAttribute("type", "text");
        //oCtrl1.setAttribute("style", "width:270px; padding-left:5px;");
        //oCtrl1.setAttribute("maxLength", "25");  
        //oCtrl1.setAttribute("onkeyup", "fm_mn(this)");
        //oCtrl1.setAttribute("keyup", "fm_mn(this)");
             
        oCtrl1.type="text";        
        oCtrl1.style.width = "270px";
        oCtrl1.style.padding = "0px 0px 0px 5px";
        oCtrl1.className="txtL";
        oCtrl1.maxLength="25";
        
        oCtrl1.onkeyup = function() { fm_mn(this) };
        oNC2.appendChild(oCtrl1);

        oNC3 = oNF.insertCell(-1);

        var oCtrl2 = document.createElement("input");
        //oCtrl2.setAttribute("type", "text");
        //oCtrl2.setAttribute("style", "width:95px");
        //oCtrl2.setAttribute("value", "0,00");
        //oCtrl2.setAttribute("onkeyup", "fm_mn(this)");
        //oCtrl2.setAttribute("keyup", "fm_mn(this)");
        //oCtrl2.setAttribute("onfocus", "fm_mn(this)");
        //oCtrl2.setAttribute("focus", "fn(this)");
      
        oCtrl2.type = "text";
        oCtrl2.style.width = "95px";
        oCtrl2.className="txtNumL";
        oCtrl2.value="0,00";
      
        oCtrl2.onkeyup = function() { fm_mn(this) };
        oCtrl2.onfocus = function() { fn(this,4,2) };

        oNC3.appendChild(oCtrl2);

        oNC4 = oNF.insertCell(-1);

        var oCtrl3 = oCtrl2.cloneNode(false);
        
        oCtrl3.onkeyup = function() { fm_mn(this) };
        oCtrl3.onfocus = function() { fn(this, 4, 2) };

        oNC4.appendChild(oCtrl3);
        
        ms(oNF);
        oNF.cells[1].children[0].focus();
	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}

function eliminar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("bd") == "I") {
                    $I("tblDatos").deleteRow(i);
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
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        if (!comprobarDatos()){
            return;
        }

        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Categoría
                sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"); //Descripcion
                sb.Append((aFila[i].cells[2].children[0].value!="")? aFila[i].cells[2].children[0].value +"##" : "0##"); //hora
                sb.Append((aFila[i].cells[3].children[0].value!="")? aFila[i].cells[3].children[0].value +"///" : "0///"); //jornada
                sw = 1;
            }
        }
        if (sw == 0){
            desActivarGrabar();            
            mmoff("Inf","No se han modificado los datos.",200);
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
                mmoff("War","Debes indicar la denominación de la categoría",310);
                aFila[i].cells[1].children[0].focus();
                return false;
            }
            if (aFila[i].cells[2].children[0].value=="") aFila[i].cells[2].children[0].value="0,00";
            if (aFila[i].cells[3].children[0].value=="") aFila[i].cells[3].children[0].value="0,00";
            if (parseFloat(dfn(aFila[i].cells[2].children[0].value))>parseFloat(dfn(aFila[i].cells[3].children[0].value)))
            {
                mmoff("War","El importe hora no puede ser mayor al importe jornada",340);
                aFila[i].cells[2].children[0].focus();
                return false;
            }
        }
        return true;
       
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
}

