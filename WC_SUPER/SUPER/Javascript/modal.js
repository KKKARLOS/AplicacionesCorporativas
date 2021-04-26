// inicio

    if ($I("ctl00$hdnRefreshPostback") == null) {
        var oRefreshPostback = document.createElement("INPUT");
        oRefreshPostback.setAttribute("type", "text");
        oRefreshPostback.setAttribute("style", "visibility:hidden");
        oRefreshPostback.setAttribute("id", "ctl00$hdnRefreshPostback");
        oRefreshPostback.setAttribute("name", "ctl00$hdnRefreshPostback");
        oRefreshPostback.setAttribute("value", "N");
    }
    //var opener = window.dialogArguments;
/*
    var opener;

    if (window.dialogArguments) // Internet Explorer supports window.dialogArguments
    {
        opener = window.dialogArguments;
        oRefreshPostback.setAttribute("value", "S");
        //alert('correcto1')
    }
    else // Firefox, Safari, Google Chrome and Opera supports window.opener
    {
        if (window.opener) {
            opener = window.opener;
            oRefreshPostback.setAttribute("value", "S");
            //alert('correcto2');
        }
        //alert('correcto3');
    }

    if (opener == undefined && document.getElementByID("ctl00$hdnRefreshPostback").value != "S") {
        var sUrl = location.pathname;

        var iPos = sUrl.indexOf("Capa_Presentacion");
        sUrl = sUrl.substring(0, iPos);
        location.href = sUrl + "AccesoIncorrecto.aspx";
    }    
*/
// 
           
	this.parent.document.getElementById("ui-dialog-title-"+ this.name).innerText = this.document.title;
	var opener = this.parent;

	var modalDialog = null;
	if (typeof this.parent.modalDialog !== "undefined" && this.parent.modalDialog != null)
		modalDialog = this.parent.modalDialog;
	else
		alert("Error: no se ha inicializado la clase para mostrar ventanas modales.");

	var bLectura = false;

	window.document.onkeypress = function(){
        if (nName == 'ie' || nName == 'chrome'){ 
            if(event.keyCode==27){//escape
                if (typeof(salir) == "function") salir();
                else if (typeof(cerrarVentana) == "function") cerrarVentana();
                else{
                     var returnValue = null;
                     modalDialog.Close(window, returnValue);
                }
            }
        }
        else if (nName == 'mozilla' || nName == 'safari' || nName == 'firefox' ){
            if(event.which==27){//escape
                if (typeof(salir) == "function") salir();
                else if (typeof(cerrarVentana) == "function") cerrarVentana();
                else{
                     var returnValue = null;
                     modalDialog.Close(window, returnValue);
                } 
            }       
        }
	}
	
	//Devuelve el opener del diálogo, sea página maestra o modalDialog
	function fOpener() {
		if (modalDialog.array_dialogos.length > 1)//Si hay más de un diálogo, el opener es otro diálogo
			return opener.$I(modalDialog.array_dialogos[modalDialog.array_dialogos.length - 2]).contentWindow;
		else//Sino el opener es la página maestra
			return opener;
	} 

var bLectura = false;
/*
function setNewSessionModal(){
    try{
        window.frames["iFrmSessionModal"].clearTimeout(window.frames["iFrmSessionModal"].nIDTimeMin);
        window.frames["iFrmSessionModal"].clearTimeout(window.frames["iFrmSessionModal"].nIDTimeSeg);
        window.frames["iFrmSessionModal"].nSession = intSession+1;
        window.frames["iFrmSessionModal"].restaSessionModal();

//        window.frames[0].clearTimeout(window.frames[0].nIDTimeMin);
//        window.frames[0].clearTimeout(window.frames[0].nIDTimeSeg);
//        window.frames[0].nSession = opener.intSession+1;
//        window.frames[0].restaSessionModal();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la sesión en ventana modal", e.message);
	}
}

function sesionCaducadaModal()
{
    try
    {
        bCambios=false;
        window.frames["iFrmSessionModal"].nSeg = 0;
        window.frames["iFrmSessionModal"].restaSegundosModal();
	}
	catch(e)
	{
		mostrarErrorAplicacion("Error al actualizar la caducidad de sesión", e.message);
	}
}
*/
document.onkeyup = function (e){
    if (!e) e = event; 
    if (e==null) return;
    if (nName == 'ie'){ 
        if(e.keyCode==27){//escape
            if (typeof(salir) == "function") salir();
            else if (typeof(cerrarVentana) == "function") cerrarVentana();
            else{
	            var returnValue = null;
	            modalDialog.Close(window, returnValue);	
            }
        }
    }
    else if (nName == 'mozilla' || nName == 'safari' || nName == 'firefox'  || nName == 'chrome'){
        if(e.which==27){//escape
            if (typeof(salir) == "function") salir();
            else if (typeof(cerrarVentana) == "function") cerrarVentana();
            else{
	            var returnValue = null;
	            modalDialog.Close(window, returnValue);	
            } 
        }       
    }
}
/*
document.onkeydown = function (e) {
    if (!e) e = event; 
    if (e==null) return;
    if (nName != "safari") return true;
    // permito el backspace y el suprimir
    if (e.keyCode != 8 && e.keyCode != 46) return false;
}
*/
var bExisteMeta = false;
var aMeta = document.getElementsByTagName("HEAD")[0].getElementsByTagName("META");
if (aMeta.length > 0){
    for (var i=0;i<aMeta.length;i++){
//        if (aMeta[i].getAttribute("http-equiv") == "X-UA-Compatible"
//            && aMeta[i].getAttribute("content") == "IE=8"){
//                bExisteMeta = true;
//                break;
//            }
        if  (
                (aMeta[i].outerHTML.indexOf("IE=5") != -1) ||
                (aMeta[i].outerHTML.indexOf("IE=8") != -1) ||
                (aMeta[i].outerHTML.indexOf("IE=10") != -1)
            )
        {
            bExisteMeta = true;
            break;
        }
    }
}
if (typeof(bSubiendo) != "undefined") bExisteMeta = true;
if (!bExisteMeta){
    alert("¡ Atención !\n\nFalta indicar en la página la vista de compatibilidad.");
}
