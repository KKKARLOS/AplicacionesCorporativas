var bSubiendo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        $I("FileDocumento").style.visibility = "visible";
        $I("FileDocumento_ctl04").setAttribute("style", " height: 13px; border-top: solid 1px #848284;  border-right: solid 1px #EEEEEE; width:343px; border-bottom: solid 1px #EEEEEE; border-left: solid 1px #848284; padding: 0px 0px 2px 2px; margin: 0px; font-size: 11px; font-family: Arial, Helvetica, sans-serif;");
        $I("FileDocumento_ctl01").setAttribute("style", "background:url('../../../../../Images/imgExaminarFile.png') no-repeat 100% 1px; height:24px; outline: none; margin-top:0px; text-align:right;min-width:450px;width:450px !important;");
        $I("FileDocumento_ctl01").setAttribute("hidefocus", "hidefocus");
        ocultarProcesando(); 
          
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function CancelarDoc() {
    try {
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar el documento.", e.message);
    }
}

function aceptar() {
    try {
        if ($I("txtDescrip").value == "")
        {
            mmoff("Inf", "Debes indicar el nombre del archivo", 290);
            return;
        }
        if ($I("FileDocumento_ctl04").value == "") {
            mmoff("Inf", "Debe especificar el archivo", 290);
            return;        
        }
        mostrarProcesando();
        RealizarCallBack("grabar@#@" + Utilidades.escape($I("txtDescrip").value), "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar el documento.", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));

    } else {
        switch (aResul[0]) {
            case "grabar":
                ocultarProcesando();
                var returnValue = "OK";
                modalDialog.Close(window, returnValue);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
        ocultarProcesando();
    }
}

function uploadError(sender, args) {
    document.getElementById('lblStatus').innerText = args.get_fileName(), "<span style='color:red;'>" + args.get_errorMessage() + "</span>";
}

function StartUpload(sender, args) {
    bSubiendo = true;
    setOp($I("btnAceptar"), 30);
    document.getElementById('lblStatus').innerText = 'Subiendo el archivo al servidor.';
}

//var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml|msg|xlsx|docx";

function UploadComplete(sender, args) 
{
    var fileExtension = args.get_fileName();
    if (!comprobarExt(fileExtension)) {
        $I("FileDocumento_ctl04").value = "";
        return
    }
    
/*    if (fileExtension.indexOf('.doc') != -1) {
        $I("FileDocumento_ctl04").value = "";
        mmoff("Inf", "Extensión de fichero no soportada", 290);
        return
    }
*/
    if (parseInt(args.get_length()) == 0) {
        $I("FileDocumento_ctl04").value = "";
        mmoff("InfPer", "El archivo no puede estar vacío", 290);
        return
    }
    if (parseInt(args.get_length()) > 26214400) {
        $I("FileDocumento_ctl04").value = "";
        mmoff("InfPer", "El archivo debería tener un tamaño menor a 25 MB", 290);
        return    
    }
    bSubiendo = false;
    setOp($I("btnAceptar"), 100);
//    var filename = args.get_fileName();
//    var contentType = args.get_contentType();
//    var text = "El tamaño del archivo '" + filename + "' es " + args.get_length() + " de bytes";
//    if (contentType.length > 0) {
//        text += " y el contenido es de tipo '" + contentType + "'.";
//    }

      document.getElementById('lblStatus').innerText = "Archivo subido correctamente";
}
//Antes teníamos una lista de extensiones permitidas. Por petición de Víctor en Agosto-2016 cambiamos a lista de prohibidas
//function comprobarExt(value) {
//    if (value == "") return true;
//    var re = new RegExp("^.+\.(" + exts + ")$", "i");
//    if (!re.test(value)) {
//        mmoff("InfPer", "Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones válidas son: " + exts.replace(/\|/g, ',') + " \n\n", 340);
//        //frmUpload.txtDescripcion.value="";
//        $I("txtArchivo").value = "";
//        setOp($I("btnAceptar"), 100);
//        return false;
//    }
//    return true;
//}
function comprobarExt(value) {
    if (value == "") return true;
    var re = new RegExp("^.+\.(" + exts + ")$", "i");
    if (re.test(value)) {
        mmoff("InfPer", "Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones prohibidas son: " + extsTexto + " \n\n", 550);//exts.replace(/\|/g, ',')
        $I("txtArchivo").value = "";
        setOp($I("btnAceptar"), 100);
        return false;
    }
    return true;
}

