/* Valores necesarios para la pesta�a retractil */
var nIntervaloPX = 5;
var nAlturaPestana = 200;
var nTopPestana = 100;
/* Fin de Valores necesarios para la pesta�a retractil */

function init(){
    $I("ctl00_SiteMapPath1").innerText = " > Gesti�n > Facturaci�n > Comunicaci�n de consumos";
    mostrarProcesando();
//    var ret = window.showModalDialog("../MailConsumos/default.aspx", self, "dialogwidth:320px; dialogheight:215px; center:yes; status:NO; help:NO;");
    modalDialog.Show("../MailConsumos/default.aspx", self, sSize(420, 250))
	        .then(function(ret) {
	        });  
    
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "eliminar":
                break;
                
            default:
                ocultarProcesando();
                alert("Opci�n de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}