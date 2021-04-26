var bRegresar = false;

document.onkeydown = KeyDown;

	function KeyDown(evt)  
	{    
		var evt = (evt) ? evt : ((event) ? event : null);    
		var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);    
		if ((evt.keyCode == 13) && ((node.type=="text") || (node.type=="radio")))     
		{        
			getNextElement(node).focus();         
			return false;        
		}  
	}    

	function getNextElement(field)  
	{     
		var form = field.form;     
		for ( var e = 0; e < form.elements.length; e++) 
		{         
			if (field == form.elements[e]) break;         
		}     
		e++;     
		while (form.elements[e % form.elements.length].type == "hidden")      
		{         
			e++;     
		}     
		return form.elements[e % form.elements.length]; 
	}

function init() {
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Importes convenio";
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                var aFila = FilasDe("tblImportesConvenio");
                var aEI = aResul[2].split("#sFin#"); //Elementos insertados
                if (aResul[2] != "") {
                    for (var i = 0, nCount = aEI.length; i < nCount; i++) {
                        var aElem = aEI[i].split("#sCad#");
                        for (var j = 0, nCount2 = aFila.length; j < nCount2; j++) {
                            if (aFila[j].id == aElem[0]) {
                                aFila[j].id = aElem[1];
                                break;
                            }
                        }
                    }
                }
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") tblImportesConvenio.deleteRow(i);
                    else mfa(aFila[i], "N");
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta.", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } else {
                    ot('tblImportesConvenio', 1, 0, '');
                    actualizarLupas("tblTitulo", "tblImportesConvenio");
                }
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function comprobarDatos(){
    var aFila = FilasDe("tblImportesConvenio");
    for (var i=0; i<aFila.length; i++){
        if (aFila[i].children[1].children[0].value == "") {
            mmoff("War", "La denominación del importe del convenio no debe estar vacio.", 370);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].children[1].children[0].focus();
            return false;
        }
        if (parseFloat(aFila[i].children[2].children[0].value) < 0) {
            mmoff("War", "No se admiten valores negativo.", 230);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].children[2].children[0].focus();
            return false;
        }
        if (parseFloat(aFila[i].children[3].children[0].value) < 0) {
            mmoff("War", "No se admiten valores negativo.", 230);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].children[3].children[0].focus();
            return false;
        }
        if (parseFloat(aFila[i].children[4].children[0].value) < 0) {
            mmoff("War", "No se admiten valores negativo.", 230);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].children[4].children[0].focus();
            return false;
        }
        if (parseFloat(aFila[i].children[5].children[0].value) < 0) {
            mmoff("War", "No se admiten valores negativo.", 230);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].children[5].children[0].focus();
            return false;
        }
        if (parseFloat(aFila[i].children[6].children[0].value) < 0) {
            mmoff("War", "No se admiten valores negativo.", 230);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].children[6].children[0].focus();
            return false;
        }
    }
    return true;    
}

function flImportesConvenio() {
    /*Recorre la tabla tblImportesConvenio para obtener una cadena que se pasará como parámetro
    al procedimiento de grabación
    */
    var sRes = "";
    try{
        aFila = FilasDe("tblImportesConvenio");
        for (var i = 0, nCount = aFila.length; i < nCount; i++) {
            switch (aFila[i].getAttribute("bd")) {
                case "U":
                case "I":
                    sRes += aFila[i].getAttribute("bd") + "#sCad#" + aFila[i].id;
                    sRes += "#sCad#" + aFila[i].children[1].children[0].value;
                    sRes += "#sCad#" + ((aFila[i].children[2].children[0].value == "") ? "0" : aFila[i].children[2].children[0].value); //Completa
                    sRes += "#sCad#" + ((aFila[i].children[3].children[0].value == "") ? "0" : aFila[i].children[3].children[0].value); //Media
                    sRes += "#sCad#" + ((aFila[i].children[4].children[0].value == "") ? "0" : aFila[i].children[4].children[0].value); //Alojamiento
                    sRes += "#sCad#" + ((aFila[i].children[5].children[0].value == "") ? "0" : aFila[i].children[5].children[0].value); //Especial
                    sRes += "#sCad#" + ((aFila[i].children[6].children[0].value == "") ? "0" : aFila[i].children[6].children[0].value); //Km
                    sRes += "#sCad#" + ((aFila[i].children[7].children[0].checked) ? "1" : "0"); //Activo
                    sRes += "#sFin#";
                    break;
                case "D":
                    sRes += aFila[i].getAttribute("bd") + "#sCad#" + aFila[i].id;
                    sRes += "#sFin#";
                    break;
            }            
        }//for
        if(sRes != "") sRes = sRes.substring(0, sRes.length-6);  
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}

function grabar(){
    try{
        if (iFila >= 0) modoControles(tblImportesConvenio.rows[iFila], false);
        if (!comprobarDatos()) return;
       
        js_args = "grabar@#@";
        js_args += flImportesConvenio();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

var nNuevaFila = 30000;
function nuevo(){
    try {
        aFila = FilasDe("tblImportesConvenio");
        if (iFila >= 0) modoControles(tblImportesConvenio.rows[iFila], false);
        oNF = $I("tblImportesConvenio").insertRow(-1);
        //iFila = oNF.rowIndex;
        nNuevaFila++;

        oNF.id = nNuevaFila; 
        oNF.setAttribute("height", "20px");
        oNF.setAttribute("bd", "I");
        oNF.onclick = function() { ms(this, 'FG'); };
        oNF.onkeyup = function() { fm_mn(this) };
        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.cells[0].style.borderRight = "0px";

        var oDen = document.createElement("input");
        oDen.id = "txtDenomi" + oNF.id;
        oDen.className = "txtL";
        oDen.setAttribute("type", "text");
        oDen.setAttribute("style", "width:245px;");
        //oDen.setAttribute("readOnly", "true");
        oDen.setAttribute("maxlength", "50");
        oDen.value =  "";
        oDen.onfocus = gf; //function(e){gf(this,e);};
        oDen.onKeyUp = function(){fm(this)};
        oDen.onchange = function(){activarGrabar();};
        oNF.insertCell(-1).appendChild(oDen);
        
        
        var oComp = document.createElement("input");
        oComp.id = "txtCompleta" + oNF.id;
        oComp.className = "txtNumL";
        oComp.setAttribute("type", "text");
        oComp.setAttribute("style", "width:77px;");
        //oComp.setAttribute("readOnly", "true");
        oComp.setAttribute("maxlength", "25");
        oComp.value =  "";
        oComp.onfocus = function(){fn(this);gf;};
        oComp.onchange = function(){activarGrabar();};
        oNF.insertCell(-1).appendChild(oComp);
        
        var oMed = document.createElement("input");
        oMed.id = "txtMedia" + oNF.id;
        oMed.className = "txtNumL";
        oMed.setAttribute("type", "text");
        oMed.setAttribute("style", "width:77px;");
        //oMed.setAttribute("readOnly", "true");
        oMed.setAttribute("maxlength", "25");
        oMed.value =  "";
        oMed.onfocus = function(){fn(this);gf;};
        oMed.onchange = function(){activarGrabar();};
        oNF.insertCell(-1).appendChild(oMed);
        
        
        var oAloj = document.createElement("input");
        oAloj.id = "txtAlojamiento" + oNF.id;
        oAloj.className = "txtNumL";
        oAloj.setAttribute("type", "text");
        oAloj.setAttribute("style", "width:77px;");
        //oAloj.setAttribute("readOnly", "true");
        oAloj.setAttribute("maxlength", "25");
        oAloj.value =  "";
        oAloj.onfocus = function(){fn(this);gf;};
        oAloj.onchange = function(){activarGrabar();};
        oNF.insertCell(-1).appendChild(oAloj);
        
        var oEsp = document.createElement("input");
        oEsp.id = "txtEspecial" + oNF.id;
        oEsp.className = "txtNumL";
        oEsp.setAttribute("type", "text");
        oEsp.setAttribute("style", "width:77px;");
        //oEsp.setAttribute("readOnly", "true");
        oEsp.setAttribute("maxlength", "25");
        oEsp.value =  "";
        oEsp.onfocus = function(){fn(this);gf;};
        oEsp.onchange = function(){activarGrabar();};
        oNF.insertCell(-1).appendChild(oEsp);
        
        
         var oKM = document.createElement("input");
        oKM.id = "txtKm" + oNF.id;
        oKM.className = "txtNumL";
        oKM.type= "text";
        oKM.setAttribute("style", "width:45px;");
        //oKM.setAttribute("readOnly", "true");
        oKM.setAttribute("maxlength", "4");
        oKM.value =  "";
        oKM.onfocus =function(){fn(this);gf;};
        oKM.onchange = function(){activarGrabar();};
        oNF.insertCell(-1).appendChild(oKM);
        
        
	    var oArch = document.createElement("input");
        oArch.id = "chkActivo" + oNF.id;
        oArch.className = "checkTabla";
        oArch.setAttribute("name", "chkActivo" + oNF.id);
        oArch.setAttribute("type", "checkbox");
        oArch.setAttribute("checked", "checked");
        oArch.setAttribute("style", "width:15px; cursor:pointer;margin-left:12px");
        oArch.onclick = function(){activarGrabar(); fm(event);};
        oNF.insertCell(-1).appendChild(oArch);
        
        
	    oNF.cells[1].children[0].focus();
	    if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
	    activarGrabar();
	 
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}


function eliminar(){
    try{
        var sw = 0;
        var sw2 = 0;
        var aFila = FilasDe("tblImportesConvenio");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FG") {
                sw2 = 1;
                if (aFila[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    $I("tblImportesConvenio").deleteRow(i);
                }    
                else{
                    mfa(aFila[i],"D");
                    sw = 1;
                }
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("War", "Debe seleccionar la fila a eliminar.",250);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor.", e.message);
    }
}
/* gf: get foco */
//function gf(oControl, event) {
function gf(e) {
    try {
        if (bLectura) return;
        if (!e) e = event; 
        var oControl = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
        if (e.keyCode == 13
            || e.keyCode == 16
            || e.keyCode == 17 
            || e.keyCode == 9) return; //enter, shift, ctrl o tabulador
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                if (oControl.className != "FG")
                    if (ie) oControl.click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        oControl.dispatchEvent(clickEvent);
                    }
                break;
            }
            oControl = oControl.parentNode;
        }
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar modificación de datos", e.message);
    }
}
