if ($I("ctl00$hdnRefreshPostback") == null) {
    var oRefreshPostback = document.createElement("INPUT");
    oRefreshPostback.setAttribute("type", "text");
    oRefreshPostback.setAttribute("style", "visibility:hidden");
    oRefreshPostback.setAttribute("id", "ctl00$hdnRefreshPostback");
    oRefreshPostback.setAttribute("name", "ctl00$hdnRefreshPostback");
    oRefreshPostback.setAttribute("value", "N");
}
this.parent.document.getElementById("ui-dialog-title-"+ this.id_dialog_body).innerText = this.document.title;
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
