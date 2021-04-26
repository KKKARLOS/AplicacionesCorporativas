var aFila;
var sValorNodo="";
function init(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
            sValorNodo = $I("hdnIdNodo").value;
        }
        else{
            sValorNodo = $I("cboCR").value;   	    
            $I("lblNodo").className = "texto";
        }
        aFila = FilasDe("tblDatos");
        iFila = aFila.length - 1;
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
		    mostrarError("No se puede eliminar el origen '" + aResul[3] + "',\n ya que existen tareas que lo tienen asignado.\n\nSi deseas que un origen no se pueda asignar a más tareas debes desactivarlo.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();
                break;
            case "getNodo":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                window.focus();
                break;
        }
        ocultarProcesando();
    }
}
function comprobarDatos(){
    try{
        if (sValorNodo=="" || sValorNodo=="-1") return false;
        var aFila = FilasDe("tblDatos");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != "D"){
                if (aFila[i].cells[1].children[0].value == ""){
                    mmoff("Inf","Debes indicar la denominación del origen", 250);
                    ms(aFila[i]);
                    aFila[i].cells[1].children[0].focus();
                    return false;
                }
                if (aFila[i].cells[2].children[0].checked && aFila[i].cells[3].children[0].value == ""){
                    mmoff("Inf","Si el origen es notificable, debes indicar la dirección de correo a la que se tenga que notificar.",500);
                    ms(aFila[i]);
                    aFila[i].cells[3].children[0].focus();
                    return false;
                }
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function grabar(){
    try{
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@" + sValorNodo + "@#@";

        var sw = 0;
        var aFila = FilasDe("tblDatos");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != ""){
                sw = 1;
                var nNotificable = 0;
                if (aFila[i].cells[2].children[0].checked) nNotificable = 1; 
                var nEstado = 0;
                if (aFila[i].cells[4].children[0].checked) nEstado = 1;
                js_args += aFila[i].getAttribute("bd") + "##"; //Opcion BD. "I", "U", "D"
                js_args += aFila[i].id +"##"; //ID Origen
                js_args += Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"; //Descripcion
                js_args += nNotificable +"##"; //Notificable
                js_args += Utilidades.escape(aFila[i].cells[3].children[0].value) +"##"; //E-Mail
                js_args += nEstado +"///"; //Notificable
            }
        }
        if (sw == 1) js_args = js_args.substring(0, js_args.length-3);
        //if (js_args != "grabar@#@"){       // siempre es distinto pues se pone también el sValorNodo
        
        if (sw == 1){
            //alert(js_args);
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        else desActivarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

function nuevo(){
    try{
        if (sValorNodo=="" || sValorNodo=="-1")
            mmoff("Inf", "Debes seleccionar un " + $I("lblNodo").innerText, 330);
        else{
            if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
            
            oNF = $I("tblDatos").insertRow(-1);
            var iFila=oNF.rowIndex;

            oNF.id = iFila;
            oNF.setAttribute("bd","I");
            oNF.attachEvent('onclick', mm);

            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

            var oCtrl1 = document.createElement("input");
            oCtrl1.type = "text";
            oCtrl1.id = "txtFun" + iFila;
            oCtrl1.className = "txtL";
            oCtrl1.setAttribute("class", "txtL");
            oCtrl1.setAttribute("style", "width:200px");
            oCtrl1.setAttribute("maxLength", "25");
            oCtrl1.onkeyup = function() { fm_mn(this) };
            oNF.insertCell(-1).appendChild(oCtrl1);            

            //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='txtFun" + iFila + "' class='txtL' style='width:215px' value='' maxlength='25' onKeyUp='fm(this)'>"));

            var oCtrl2 = document.createElement("input");
            oCtrl2.type = "checkbox";
            oCtrl2.className = "checkTabla";
            oCtrl2.id = "chkNot" + iFila;
            oCtrl2.name = "chkNot" + iFila;            
            oCtrl2.setAttribute("style", "width:15px");
            oCtrl2.onclick = function() { fm_mn(this) };
            oNF.insertCell(-1).appendChild(oCtrl2);

            //oNF.insertCell(-1).appendChild(document.createElement("<input type='checkbox' style='width:15' name='chkNot" + iFila + "' id='chkNot" + iFila + "' class='checkTabla' onclick='fm(this)'>"));

            var oCtrl3 = document.createElement("input");
            oCtrl3.type = "text";
            oCtrl3.id = "txtMail" + iFila;
            oCtrl3.className = "txtL";
            oCtrl3.setAttribute("class", "txtL");
            oCtrl3.setAttribute("style", "width:540px");
            oCtrl3.setAttribute("maxLength", "250");
            oCtrl3.onkeyup = function() { fm_mn(this) };
            oNF.insertCell(-1).appendChild(oCtrl3);   
            
            //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='txtMail" + iFila + "' class='txtL' style='width:540px' value='' maxlength='250' onKeyUp='fm(this)'>"));

            var oCtrl4 = document.createElement("input");
            oCtrl4.type = "checkbox";
            oCtrl4.checked = true;
            oCtrl4.className = "checkTabla";
            oCtrl4.id = "chkEst" + iFila;
            oCtrl4.name = "chkEst" + iFila;
            oCtrl4.setAttribute("style", "width:15px");
            oCtrl4.onclick = function() { fm_mn(this) };
            oNF.insertCell(-1).appendChild(oCtrl4);
            	        
	        //oNF.insertCell(-1).appendChild(document.createElement("<input type='checkbox' checked='true' style='width:15' name='chkEst" + iFila + "' id='chkEst" + iFila + "' class='checkTabla' onclick='fm(this)'>"));

            ms(oNF);
	        oNF.cells[1].children[0].focus();
            activarGrabar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo registro", e.message);
    }
}

function eliminar(){
    try{
        var sw = 0;
        var sw2 = 0;
        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw2=1;
                if (aFila[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    $I("tblDatos").deleteRow(i);
                    iFila=-1;
                }    
                else{
                    mfa(aFila[i],"D");
                    sw = 1;
                }
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("Inf", "Debes seleccionar la fila a eliminar", 250);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function buscar(){
    try{
        var js_args = "getNodo@#@"+ sValorNodo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener orígenes de tarea del nodo", e.message);
    }
}
function setCombo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener orígenes de tarea del nodo", e.message);
    }
}

function getNodo(){
    try{
        if ($I("lblNodo").className == "texto") return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 460))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    setCombo();
                }
            });
        window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo ", e.message);
    }
}


