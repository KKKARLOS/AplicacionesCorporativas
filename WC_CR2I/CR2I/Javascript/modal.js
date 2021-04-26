<!--
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
                    //window.returnValue = null;
                    //window.close();
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
                    //window.returnValue = null;
                    //window.close();
                } 
            }       
        }
}

-->