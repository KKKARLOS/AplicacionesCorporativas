var sAp1Aux = "";
var sAp2Aux = "";
var sNomAux = "";
var sUsuAux = "";
var sBajaAux = "";

function init(){
    try{
        $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function mostrarProfesionales(){
    try{
        if ($I("txtApellido1").value == "" 
                && $I("txtApellido2").value == "" 
                && $I("txtNombre").value == ""
                && $I("txtUsuario").value == ""){
            mmoff("War","Debes introducir algún criterio de búsqueda", 300);
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "profesionales@#@";
        js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtNombre").value) +"@#@"; 
        js_args += dfn($I("txtUsuario").value) +"@#@"; 
        js_args += ($I("chkBajas").checked)? "1":"0";
        
        sAp1Aux = $I("txtApellido1").value;
        sAp2Aux = $I("txtApellido2").value;
        sNomAux = $I("txtNombre").value;
        sUsuAux = $I("txtUsuario").value;
        sBajaAux = ($I("chkBajas").checked)? "1":"0";
        
        //alert(js_args);
        mostrarProcesando();
        bCambios = false;
        RealizarCallBack(js_args, ""); 
    }catch(e){
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales", e.message);
    }
}

function mostrarProfesionalesAux(){
    try{
        var js_args = "profesionales@#@";
        js_args += Utilidades.escape(sAp1Aux) +"@#@"; 
        js_args += Utilidades.escape(sAp2Aux) +"@#@"; 
        js_args += Utilidades.escape(sNomAux) +"@#@"; 
        js_args += dfnTotal(sUsuAux) + "@#@";
        js_args += sBajaAux;
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
    }catch(e){
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales aux", e.message);
    }
}
     

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "profesionales":
	            $I("divCatalogoFICEPI").children[0].innerHTML = aResul[2];
	            $I("divCatalogoSUPERAlta").children[0].innerHTML = aResul[3];
	            $I("divCatalogoSUPERBaja").children[0].innerHTML = aResul[4];
	            
                $I("txtApellido1").value = "" ;
                $I("txtApellido2").value = "" ;
                $I("txtNombre").value = "";
                $I("txtUsuario").value = "";
	            
                scrollTablaFICEPI();
                scrollTablaSUPERALTA();
                scrollTablaSUPERBAJA();
                $I("txtApellido1").focus();
                break;
        }
    }
    ocultarProcesando();
}

function setAppNom(){
    try{
        borrarTablas(); 
        $I("txtUsuario").value = "";
    }catch(e){
        mostrarErrorAplicacion("Error al establecer apellidos y/o nombre", e.message);
    }
}
function preSetUsuario(e) {
    try {
        setUsuario();
        if (e.keyCode == 13) {
            mostrarProfesionales();
            if (nName == 'ie' || nName == 'safari')
                e.keyCode = 0;
            else
                e.preventDefault();
        }
        else {
            vtn2(e);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer usuario", e.message);
    }
}
function setUsuario() {
    try {
        $I("txtApellido1").value = "";
        $I("txtApellido2").value = "";
        $I("txtNombre").value = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer usuario", e.message);
    }
}
function borrarTablas() {
    try{
        $I("divCatalogoFICEPI").children[0].innerHTML = "";
        $I("divCatalogoSUPERAlta").children[0].innerHTML = "";
        $I("divCatalogoSUPERBaja").children[0].innerHTML = "";
    }catch(e){
        mostrarErrorAplicacion("Error al borrar las tablas.", e.message);
    }
}

var nTopScrollFICEPI = -1;
var nIDTimeFICEPI = 0;
function scrollTablaFICEPI() {
    try {
        if ($I("divCatalogoFICEPI").scrollTop != nTopScrollFICEPI) {
            nTopScrollFICEPI = $I("divCatalogoFICEPI").scrollTop;
            clearTimeout(nIDTimeFICEPI);
            nIDTimeFICEPI = setTimeout("scrollTablaFICEPI()", 50);
            return;
        }
        clearTimeout(nIDTimeFICEPI);
        var nFilaVisible = Math.floor(nTopScrollFICEPI / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoFICEPI").offsetHeight / 20 + 1, $I("tblDatosFicepi").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatosFicepi").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatosFicepi").rows[i];
                oFila.setAttribute("sw", 1);
//                if (i == nFilaVisible)
//                    oFila.setAttribute("tipo", "F");
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
    }
}

var nTopScrollSUPERALTA = -1;
var nIDTimeSUPERALTA = 0;
function scrollTablaSUPERALTA() {
    try {
        if ($I("divCatalogoSUPERAlta").scrollTop != nTopScrollSUPERALTA) {
            nTopScrollSUPERALTA = $I("divCatalogoSUPERAlta").scrollTop;
            clearTimeout(nIDTimeSUPERALTA);
            nIDTimeSUPERALTA = setTimeout("scrollTablaSUPERALTA()", 50);
            return;
        }
        clearTimeout(nIDTimeSUPERALTA);
        var nFilaVisible = Math.floor(nTopScrollSUPERALTA / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoSUPERAlta").offsetHeight / 20 + 1, $I("tblDatosSuperAlta").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < $I("tblDatosSuperAlta").rows.length; i++){
            if (!$I("tblDatosSuperAlta").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatosSuperAlta").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); }
                oFila.ondblclick = function() { MostrarUsuario(this) };
                oFila.cells[1].children[0].ondblclick = function() { MostrarUsuario(this.parentNode.parentNode) };

                //oFila.ondblclick = function(){msse(this);MostrarUsuario(this);}
                //oFila.cells[1].children[0].ondblclick = function(){msse(this.parentNode.parentNode);MostrarUsuario(this.parentNode.parentNode);}

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de SUPER Alta.", e.message);
    }
}

var nTopScrollSUPERBAJA = -1;
var nIDTimeSUPERBAJA = 0;
function scrollTablaSUPERBAJA() {
    try {
        if ($I("divCatalogoSUPERBaja").scrollTop != nTopScrollSUPERBAJA) {
            nTopScrollSUPERBAJA = $I("divCatalogoSUPERBaja").scrollTop;
            clearTimeout(nIDTimeSUPERBAJA);
            nIDTimeSUPERBAJA = setTimeout("scrollTablaSUPERBAJA()", 50);
            return;
        }
        clearTimeout(nIDTimeSUPERBAJA);
        var nFilaVisible = Math.floor(nTopScrollSUPERBAJA / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoSUPERBaja").offsetHeight / 20 + 1, $I("tblDatosSuperBaja").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < $I("tblDatosSuperBaja").rows.length; i++){
            if (!$I("tblDatosSuperBaja").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatosSuperBaja").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); }
                oFila.ondblclick = function() { MostrarUsuario(this) };
                oFila.cells[1].children[0].ondblclick = function() { MostrarUsuario(this.parentNode.parentNode) };

                //oFila.ondblclick = function(){msse(this);MostrarUsuario(this);}
                //oFila.cells[1].children[0].ondblclick = function(){msse(this.parentNode.parentNode);MostrarUsuario(this.parentNode.parentNode);}

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de SUPER Baja.", e.message);
    }
}

function fnRelease(e) {
    //alert('entra fnRelease');
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    var bHayAlta = false;
    var oFilaAct;

    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgColectivo":
		            if (nOpcionDD == 1){
		                //alert(oRow.id);
		                //AltaUsuario(oRow);
		                bHayAlta = true;
		                oFilaAct = oRow;
                    }
			        break;
			}
		}
	}
	killTimer();
	CancelDrag();
	obj.style.display	= "none";
	oEl					= null;
	aEl.length = 0;
	oTarget				= null;
	beginDrag			= false;
	TimerID				= 0;
	oRow                = null;
    FromTable           = null;
    ToTable = null;
    if (bHayAlta) {
        bHayAlta = false;
        AltaUsuario(oFilaAct);
    }
}

function AltaUsuario(oFila) {
    try {
        //alert("Id Ficepi: "+ oFila.id);
        var sMsg = "";
        if (oFila.getAttribute("baja") == "1") {
            //jqConfirm("", "El profesional seleccionado se encuentra de baja en FICEPI.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            //    if (!answer) return;
            //});
            sMsg = "El profesional seleccionado se encuentra de baja en FICEPI.<br />";
        }
        else if (oFila.getAttribute("baja") == "E") {
            //jqConfirm("", "El profesional seleccionado es colaborador externo. Estos se dan de alta en SUPER de forma automática.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            //    if (!answer) return;
            //});
            sMsg += "El profesional seleccionado es colaborador externo. Estos se dan de alta en SUPER de forma automática.<br />";
        }

        var sw = 0;
        for (var i = 0; i < $I("tblDatosSuperAlta").rows.length; i++) {
            if (oFila.id == $I("tblDatosSuperAlta").rows[i].getAttribute("idficepi")) {
                sw = 1;
                break;
            }
        }
        if (sw == 1) {
            //jqConfirm("", "El profesional seleccionado ya se encuentra de alta en SUPER.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            //    if (!answer) return;
            //    else AltaUsuarioContinuar(oFila);
            //});
            sMsg += "El profesional seleccionado ya se encuentra de alta en SUPER.<br />";
        }
        //else AltaUsuarioContinuar(oFila);
        if (sMsg != "") {
            jqConfirm("", sMsg + "<br />¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
                if (answer)
                    AltaUsuarioContinuar(oFila);
            });
        }
        else
            AltaUsuarioContinuar(oFila);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a dar de alta al usuario.", e.message);
    }
}
function AltaUsuarioContinuar(oFila){
    try{
        var sTam = "";
        if (oFila.getAttribute("tipo") == "P") {
            //alert("Alta de empleado interno");
            var strEnlace = strServer + "Capa_Presentacion/Administracion/MantUsuarios/DetalleInterno/Default.aspx?nIdUsuario=0&nIdFicepi=" + oFila.id;
            var sTam = sSize(840, 450);
        }
        else {
            if (oFila.getAttribute("tipo") == "F") {
                var strEnlace = strServer + "Capa_Presentacion/Administracion/Foraneos/Consultas/Detalle/Default.aspx?idF=" + codpar(oFila.id);
                var sTam = sSize(900, 420);
            }
            else {
                //alert("Alta de colaborador externo");
                var strEnlace = strServer + "Capa_Presentacion/Administracion/MantUsuarios/DetalleExterno/Default.aspx?nIdUsuario=0&nIdFicepi=" + oFila.id;
                var sTam = sSize(840, 300);
            }
        }
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strEnlace, self, sTam)
            .then(function(ret) {
                if (ret != null) {
                    if (bAlgunCambioGrabadoEnDetalle) {
                        borrarTablas();
                        mostrarProfesionalesAux();
                    }
                    //ocultarProcesando();
                }
        });
        ocultarProcesando();
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir a dar de alta al usuario.", e.message);
    }
}

var bAlgunCambioGrabadoEnDetalle = false;
function MostrarUsuario(oFila){
    try {
        var sTam = "";
        if (oFila.getAttribute("tipo") == "P") {
            var strEnlace = strServer + "Capa_Presentacion/Administracion/MantUsuarios/DetalleInterno/Default.aspx?nIdUsuario=" + oFila.id + "&nIdFicepi=" + oFila.getAttribute("idficepi");
            sTam = sSize(840, 450);
        }
        else {
            if (oFila.getAttribute("tipo") == "F") {
                var strEnlace = strServer + "Capa_Presentacion/Administracion/Foraneos/Consultas/Detalle/Default.aspx?idF=" + codpar(oFila.getAttribute("idficepi"));
                sTam = sSize(900, 420);
            }
            else {
                var strEnlace = strServer + "Capa_Presentacion/Administracion/MantUsuarios/DetalleExterno/Default.aspx?nIdUsuario=" + oFila.id + "&nIdFicepi=" + oFila.getAttribute("idficepi");
                sTam = sSize(840, 300);
            }
        }
	    mostrarProcesando();
        //window.focus();
	    modalDialog.Show(strEnlace, self, sTam)
            .then(function (ret) {
                if (ret != null) {
                    if (bAlgunCambioGrabadoEnDetalle) {
                        bAlgunCambioGrabadoEnDetalle = false;
                        borrarTablas();
                        mostrarProfesionalesAux();
                    }
                }
                ocultarProcesando();
            });
    }
    catch(e){
	    mostrarErrorAplicacion("Error al mostrar el detalle de usuario.", e.message);
    }
}