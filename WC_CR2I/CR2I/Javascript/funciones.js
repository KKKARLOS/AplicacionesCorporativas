var oBotonera = null;
function $I() {
    var pfj = "ctl00_CPHC_";
    var element = arguments[0];
    if (typeof element == 'string') {
        if (document.getElementById(pfj + element) != null)
            element = document.getElementById(pfj + element);  //Controles Web
        else if (document.getElementById(element) != null)
            element = document.getElementById(element); //Controles HTML
        else if (document.getElementById("ctl00$" + element) != null)
            element = document.getElementById("ctl00$" + element); //hdnErrores
        else if (document.getElementById("ctl00_" + element) != null)
            element = document.getElementById("ctl00_" + element); //hdnErrores
        else
            element = document.getElementById(element); //Controles HTML
    }
    return element;
}
var ie = "";
var nName = "";
var nVer = "";

function navegador() {
    /*  Nombre a utilizar en la aplicación del navegador correspondiente

    opera   -- Opera 
    ie      -- Internet Explorer 
    safari  -- Safari 
    firefox -- FireFox 
    mozilla -- Mozilla 
    chrome  -- Chome

*/
    if (nName != "") return;
    var ua = navigator.userAgent.toLowerCase();
    //alert("ua=" + ua);
    if (ua.indexOf("opera") != -1) {
        nName = "opera";
    }
    else if (ua.indexOf("msie") != -1) {
        nName = "ie";
    }
    else if (ua.indexOf("chrome") != -1) {
        nName = "chrome";
    }
    else if (ua.indexOf("safari") != -1) {
        nName = "safari";
    }
    else if (ua.indexOf("mozilla") != -1) {
        if (ua.indexOf("firefox") != -1) {
            nName = "firefox";
        } else {
            nName = "mozilla";
        }
    }
    nVer = navigator.appVersion;
    ie = (document.all) ? true : false;
}
navegador();

function mostrarProcesando() {
    try {
        $I("procesandoSuperior").style.visibility = "visible";
    }
    catch(e){}
    $I("procesando").style.visibility = "visible";
}

function ocultarProcesando(){
    try {$I("procesandoSuperior").style.visibility = "hidden";}catch(e){}
    $I("procesando").style.visibility = "hidden";
}

function bProcesando(){
    if ($I("procesando").style.visibility == "visible") return true;
    else return false;
}

var message="Función deshabilitada";
//function click(e) 
//{
//    if (event.button == 2) 
//    {
//            alert(message);
//            return false;
//    }
//}
//document.onmousedown=click;
function click(e) {
    //    if (event.button == 2) 
    //    {
    //            alert(message);
    //            return false;
    //    }else 

    if (!e) e = event;
    if (navigator.appName == 'Microsoft Internet Explorer' || navigator.appName == 'Chrome') {
        if (e.button == 1) {
            if (e.srcElement.tagName == "BUTTON") {
                if (e.srcElement.disabled) return;
                oBoton = e.srcElement;
                oBoton.style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonIz2.gif)";
                oBoton.children[0].style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonDr2.gif)";
            }
        }
    }
    else if (navigator.appName == 'Netscape' || navigator.appName == 'Mozilla Firefox') {
        if (e.which == 1) {
            if (e.target.tagName == "BUTTON") {
                if (e.target.disabled) return;
                oBoton = e.target;
                oBoton.style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonIz2.gif)";
                oBoton.children[0].style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonDr2.gif)";
            }
        }
    }
}
//document.oncontextmenu = function() { return false };
document.onmousedown = click;
document.onmouseup = clickup;

var oBoton = null;
function clickup(e) {
    if (!e) e = event;
    if (navigator.appName == 'Microsoft Internet Explorer' || navigator.appName == 'Chrome') {
        if (e.button == 1) {
            if (oBoton != null) {
                oBoton.style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonIz.gif)";
                oBoton.children[0].style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonDr.gif)";
                oBoton = null;
            }
            return false;
        }
    }
    else if (navigator.appName == 'Netscape' || navigator.appName == 'Mozilla Firefox') {
        if (e.which == 1) {
            if (oBoton != null) {
                oBoton.style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonIz.gif)";
                oBoton.children[0].style.backgroundImage = "url(" + strServer + "Images/Botones/imgBackButtonDr.gif)";
                oBoton = null;
            }
            return false;
        }
    }
}

//function mostrarErrores(){

//	if (screen.width == 800){
//		var objBODY = document.getElementsByTagName("body")[0];
//		objBODY.scroll = "yes";
//		objBODY = null;
//	}

//	if ($I("hdnErrores").value != ""){
//		var reg = /\\n/g;
//		var strMsg = $I("hdnErrores").value;
//		strMsg = strMsg.replace(reg,"\n");
//		alert(strMsg);
//	}
//}

function sendHttp(v_sUrl) {
	var oXH;
	var sR='';

	var peticion = false;  
	try {  
		peticion = new XMLHttpRequest();  
		oXH = new XMLHttpRequest();  
	}
	catch (trymicrosoft) {
		try {
			peticion = new ActiveXObject("Msxml2.XMLHTTP");
			oXH = new ActiveXObject("Msxml2.XMLHTTP");
			}
			catch (othermicrosoft) {
				try {
					peticion = new ActiveXObject("Microsoft.XMLHTTP");
					oXH = new ActiveXObject("Microsoft.XMLHTTP");
					}
					catch (failed) {
						peticion = false;
					} 
			}
	}
	//alert(peticion);
	if (!peticion){
		alert("Se ha producido un error en la aplicación.\n\nDescripción: No se ha podido inicializar el objeto XMLHTTP\n\nVuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU.\n\nDisculpa las molestias.");
		try {
			ocultarProcesando();
		}catch(e){}
		return;
	}
	
	//oXH = new ActiveXObject( "Microsoft.XMLHTTP" );
	oXH.open( "POST", v_sUrl, false );
	oXH.onreadystatechange = function () {
		var sM='';
		switch (oXH.readyState) {
			case 1, 2, 3:
					sM+="."
					window.status="Obteniendo datos : " + sM;
					break;
			case 4:
					switch (oXH.status) {
						case 200:
							sR=oXH.responseText;
							break;
						case 404:
							sR="no encontrado";
							break;
					}
					break;
		}
	}	
	try {
		oXH.send();
	}
	catch (e) {
		sR="Error al conectarse al servidor.";
	}
	return (sR);
}	

function mostrarErrorAplicacion(strTitulo, strError){
	alert("Se ha producido un error en la aplicación.\n\nTítulo: "+ strTitulo +"\n\nDescripción: "+ strError +"\n\nVuelva a intentarlo y, si persiste el problema, notifica la incidencia al CAU.\n\nDisculpa las molestias.");
}

//Estas funciones init y unload por defecto se sobreescriben con las
//funciones propias de cada opción, en caso de existir.
function init(){

}

function unload() {
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                bEnviar = grabar();
            }
            else bCambios = false;
        });
    }
}

function res(){
    try{
	    if (screen.width == 800){
		    var objBODY = document.getElementsByTagName("BODY")[0];
		    objBODY.scroll = "yes";
		    objBODY = null;
	    }
    	
	    setRes(nResolucion);
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la resolución.", e.message);
    }
}
function setRes(nRes) {
    try {
        clearTimeout(nIDTimeSetRes);
        var nWidth = 0;
        var nHeight = 0;
        var swScroll = 0;
        var nX = 0;
        var nY = 0;

        if (nRes == 1280) {//Pantalla grande
            nWidth = 1280;
            nHeight = 994;
            if (screen.width < 1280) {
                nWidth = screen.width;
                swScroll = 1;
            }
            if (screen.height < 1024) {
                nHeight = screen.height - 30;
                swScroll = 1;
            }
        } else {//Pantalla pequeña
            nWidth = 1024;
            nName
            if (nName == 'mozilla ' || nName == 'firefox' || nName == 'chrome')
                nHeight = 768;
            else nHeight = 738;

            if (screen.width < 1024) {
                nWidth = screen.width;
                swScroll = 1;
            }
            if (screen.height < 768) {
                nHeight = screen.height - 30;
                swScroll = 1;
            }
            if (screen.width > 1024) {
                nX = screen.width / 2 - nWidth / 2;
            }
            if (screen.height > 768) {
                nY = screen.height / 2 - nHeight / 2 - 30;
            }
        }

        window.moveTo(nX, nY);
        window.resizeTo(nWidth, nHeight);

        //$I("procesando").style.top = (document.body.clientHeight/2) -50;
        //$I("procesando").style.left = (document.body.clientWidth/2) -76;

        try {
            if (document.all) {
                $I("popupWin").style.top = ((document.documentElement.clientHeight / 2) - 50) + "px";
                $I("popupWin").style.left = ((document.documentElement.clientWidth / 2) - 115) + "px";
            }
            else {
                $I("popupWin").style.top = ((window.innerHeight / 2) - 50) + "px";
                $I("popupWin").style.left = ((window.innerHeight / 2) - 115) + "px";
            }
        } catch (e) { }

        try {
            if (document.all) {
                $I("popupWin_Session").style.top = ((document.documentElement.clientHeight / 2) - 80) + "px";
                $I("popupWin_Session").style.left = ((document.documentElement.clientWidth / 2) - 140) + "px";
            }
            else {
                $I("popupWin_Session").style.top = ((window.innerHeight / 2) - 80) + "px";
                $I("popupWin_Session").style.left = ((window.innerHeight / 2) - 140) + "px";
            }
        } catch (e) { }

        if (swScroll == 1) {
            var objBODY = document.getElementsByTagName("BODY")[0];
            objBODY.scroll = "yes";
            objBODY = null;
        }
    } catch (e) {
        if (nIntento < 3) {
            nIntento++;
            window.focus();
            nIDTimeSetRes = setTimeout("setRes(" + nRes + ")", 50);
            return;
        } else if (nIntento < 10) {
            nIntento++;
            window.focus();
            nIDTimeSetRes = setTimeout("setRes(" + nRes + ")", 1000);
            return;
        }
        mostrarErrorAplicacion("Error al redimensionar la ventana", e.message);
    }
}
function mostrarErrores(){
    try{
	    if ($I("hdnErrores").value != ""){
	        ocultarProcesando();
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n").split("@#@")[0];
		    alert(strMsg);
		    return false;
	    }
	    return true;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los errores.", e.message);
    }
}
/***********************************************
Función: AccionBotonera
Inputs: strIDBoton --> ID del botón a tratar;
sOp --> Acción a realizar: "H" -> Habilitar, 
"D" -> Deshabilitar; 
"P" -> Simula el pulsado del botón
"E" -> Pregunta si existe el botón y devuelve un booleano
************************************************/
function AccionBotonera(strIDBoton, sOp) {
    try {
        if (oBotonera != null) {//para evitar errores en pantallas modales que no tienen botonera
            switch (sOp) {
                case "H": Botonera.habBotonID(strIDBoton.toLowerCase()); break;
                case "D": Botonera.desBotonID(strIDBoton.toLowerCase()); break;
                case "P": Botonera.pulsarBotonID(strIDBoton.toLowerCase()); break;
                case "E": return Botonera.existeBoton(strIDBoton.toLowerCase()); break;
            }
        }
        return false;
    } catch (e) {
        var strTitulo = "Error";
        if (sOp == "H") strTitulo = "Error al habilitar el botón '" + strIDBoton + "'";
        else if (sOp == "D") strTitulo = "Error al deshabilitar el botón '" + strIDBoton + "'";
        else if (sOp == "P") strTitulo = "Error al simular pulsar el botón '" + strIDBoton + "'";
        mostrarErrorAplicacion(strTitulo, e.message);
    }
}
var nIntento = 0;
var nIDTimeSetRes;
//function setRes(nRes) {
//    try {
//        clearTimeout(nIDTimeSetRes);
//        var nWidth = 0;
//        var nHeight = 0;
//        var swScroll = 0;
//        var nX = 0;
//        var nY = 0;

//        if (nRes == 1280) {//Pantalla grande
//            nWidth = 1280;
//            nHeight = 994;
//            if (screen.width < 1280) {
//                nWidth = screen.width;
//                swScroll = 1;
//            }
//            if (screen.height < 1024) {
//                nHeight = screen.height - 30;
//                swScroll = 1;
//            }
//        } else {//Pantalla pequeña
//            nWidth = 1024;

//            if (navigator.appName == 'Netscape' || navigator.appName == 'Mozilla Firefox' || navigator.appName == 'Chrome')
//                nHeight = 768;
//            else nHeight = 738;

//            if (screen.width < 1024) {
//                nWidth = screen.width;
//                swScroll = 1;
//            }
//            if (screen.height < 768) {
//                nHeight = screen.height - 30;
//                swScroll = 1;
//            }
//            if (screen.width > 1024) {
//                nX = screen.width / 2 - nWidth / 2;
//            }
//            if (screen.height > 768) {
//                nY = screen.height / 2 - nHeight / 2 - 30;
//            }
//        }

//        window.moveTo(nX, nY);
//        window.resizeTo(nWidth, nHeight);

//        $I("procesando").style.top = (document.body.clientHeight / 2) - 50;
//        $I("procesando").style.left = (document.body.clientWidth / 2) - 76;

//        try {
//            $I("popupWin").style.top = (document.body.clientHeight / 2) - 50;
//            $I("popupWin").style.left = (document.body.clientWidth / 2) - 115;
//        } catch (e) { }

//        try {
//            $I("popupWin_Session").style.top = (document.body.clientHeight / 2) - 80;
//            $I("popupWin_Session").style.left = (document.body.clientWidth / 2) - 140;
//        } catch (e) { }

//        if (swScroll == 1) {
//            var objBODY = document.getElementsByTagName("BODY")[0];
//            objBODY.scroll = "yes";
//            objBODY = null;
//        }
//    } catch (e) {
//        if (nIntento < 3) {
//            nIntento++;
//            window.focus();
//            nIDTimeSetRes = setTimeout("setRes(" + nRes + ")", 50);
//            return;
//        } else if (nIntento < 10) {
//            nIntento++;
//            window.focus();
//            nIDTimeSetRes = setTimeout("setRes(" + nRes + ")", 1000);
//            return;
//        }
//        mostrarErrorAplicacion("Error al redimensionar la ventana", e.message);
//    }
//}
var myclose = false;
function ConfirmClose(e) {
    if (!e) e = event;
    if (document.all) {
        if (e.clientY < 0) {
            //event.returnValue = 'Any message you want';
            setTimeout('myclose=false', 100);
            myclose = true;
        }
    }
    else {
        if (e.pageY < 0) {
            //event.returnValue = 'Any message you want';
            setTimeout('myclose=false', 100);
            myclose = true;
        }
    }
}

function HandleOnClose() {
    if (myclose) {
        //alert("Window is closed");
    }
}
function mostrarFichaQEQ(sNif) {
    try {
//        var strEnlace = "http://web.intranet.ibermatica/paginasamarillas/profesionales/datos_simple.asp?cod=" + sNif + "&origen=MAPACON";
//        var ret = window.showModalDialog(strEnlace, "QEQ", "center:yes; dialogwidth:640px; dialogheight:400px; status:NO; help:NO;");
//        modalDialog.Show(strEnlace, self, sSize(640, 400))
//        .then(function(ret) {
//        }); 
        var strEnlace = "http://web.intranet.ibermatica/paginasamarillas/profesionales/datos_simple.asp?cod=" + sNif + "&origen=MAPACON";
        window.open(strEnlace, "QEQ", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 365) + "px,left=" + eval(screen.availWidth / 2 - 510) + "px,width=640px,height=400px");    
    }
    catch (e) {
        //alert("No se ha podido acceder a la ficha.");
    }
}
if (typeof HTMLElement != "undefined" && !HTMLElement.prototype.insertAdjacentElement) {
    HTMLElement.prototype.insertAdjacentElement = function(where, parsedNode) {
        switch (where) {
            case 'beforeBegin':
                this.parentNode.insertBefore(parsedNode, this)
                break;
            case 'afterBegin':
                this.insertBefore(parsedNode, this.children[0]);
                break;
            case 'beforeEnd':
                this.appendChild(parsedNode);
                break;
            case 'afterEnd':
                if (this.nextSibling)
                    this.parentNode.insertBefore(parsedNode, this.nextSibling);
                else this.parentNode.appendChild(parsedNode);
                break;
        }
    }

    HTMLElement.prototype.insertAdjacentHTML = function(where, htmlStr) {
        var r = this.ownerDocument.createRange();
        r.setStartBefore(this);
        var parsedHTML = r.createContextualFragment(htmlStr);
        this.insertAdjacentElement(where, parsedHTML)
    }


    HTMLElement.prototype.insertAdjacentText = function(where, txtStr) {
        var parsedText = document.createTextNode(txtStr)
        this.insertAdjacentElement(where, parsedText)
    }

    HTMLElement.prototype.__defineGetter__("innerText", function() {
        return (this.textContent);
    });
    HTMLElement.prototype.__defineSetter__("innerText", function(txt) {
        this.textContent = txt;
    });
    
}

var Base64 = {
    // private property
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

    // public method for encoding
    encode: function(input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        input = Base64._utf8_encode(input);

        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
			this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
			this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

        }

        return output;
    },

    // public method for decoding
    decode: function(input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {

            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }

        }

        output = Base64._utf8_decode(output);

        return output;

    },

    // private method for UTF-8 encoding
    _utf8_encode: function(string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function(utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }

        }

        return string;
    }
}
//Codificar parámetro
function codpar(sCadena) {
    try {
        return Base64.encode(Utilidades.escape(sCadena));
    } catch (e) {
        mostrarErrorAplicacion("Error al codificar el parámetro", e.message);
    }
}

function Utilidades() { }
Utilidades.escape = function(sTexto) {
    try {
        return encodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"escape\" de un texto. " + e.message);
    }
}
Utilidades.unescape = function(sTexto) {
    try {
        return decodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"unescape\" de un texto. " + e.message);
    }
}

function goInicio() {
    try {
        window.top.location.href = strServer + "Capa_Presentacion/Default.aspx";
    } catch (e) {
        //mostrarErrorAplicacion("Error al ir a abandonar la sesión JS.", e.message);
    }
    return;
}
function sSize(iWidth, iHeight) {
    try {
        //return ((nName != "chrome") ? "dialogwidth:"+iWidth+"px; dialogheight:"+iHeight+"px;" : "dialogwidth:"+iWidth+"; dialogheight:"+iHeight+";") +" center:yes; status:NO; help:NO;";                

        var sUnidad = (nName != "chrome") ? "px" : "";
        var sReturn = "dialogWidth: " + iWidth + sUnidad + "; ";
        sReturn += "dialogHeight: " + iHeight + sUnidad + "; ";
        sReturn += "dialogLeft: " + parseInt(((window.screen.width - iWidth) / 2), 10) + sUnidad + "; ";
        sReturn += "dialogTop: " + parseInt(((window.screen.height - iHeight) / 2), 10) + sUnidad + "; ";
        sReturn += "center:yes; status:NO; help:NO;";
        return sReturn;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el tamaño de la ventana", e.message);
    }
}