<!--
this.parent.document.getElementById("ui-dialog-title-"+ this.id_dialog_body).innerText = this.document.title;

var opener = this.parent;


var modalDialog = null;
if (typeof this.parent.modalDialog !== "undefined" && this.parent.modalDialog != null)
    modalDialog = this.parent.modalDialog;
else
	alert("Error: no se ha inicializado la clase para mostrar ventanas modales.");
	
/*
var opener = window.dialogArguments;
if (opener == undefined){
    var sUrl=location.pathname;
    var iPos=sUrl.indexOf("Capa_Presentacion");
    sUrl=sUrl.substring(0, iPos);
    location.href=sUrl + "AccesoIncorrecto.aspx";
}
*/
var bLectura = false;
/*
function setNewSessionModal(){
    try{
        window.frames.iFrmSessionModal.clearTimeout(window.frames.iFrmSessionModal.nIDTimeMin);
        window.frames.iFrmSessionModal.clearTimeout(window.frames.iFrmSessionModal.nIDTimeSeg);
        window.frames.iFrmSessionModal.nSession = intSession+1;
        window.frames.iFrmSessionModal.restaSessionModal();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la sesión en ventana modal", e.message);
	}
}
*/

window.document.onkeypress = function(){
    if(event.keyCode==27){//escape
        if (typeof(salir) == "function") salir();
        else if (typeof(cerrarVentana) == "function") cerrarVentana();
        else{
//            window.returnValue = null;
//            window.close();
            var returnValue = null;
            modalDialog.Close(window, returnValue);          
        }
    }
}
/*
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
*/
-->