
function init() {
    //alert("EsPostBack=" + EsPostBack);
    //setOp($I("btnImportar"), 30);
    window.focus(); 
    $I("txtComentario").focus();
    ocultarProcesando();
    if ($I("hdnResul").value != "" && $I("hdnResul").value != "OK") {
        mmoff("War", $I("hdnResul").value, 400);
        $I("hdnResul").value = "OK";
    }
    else {
        if (EsPostBack) {
            $I("txtComentario").value = "";
            mmoff("Inf", "Mensaje enviado correctamente", 300);
            setTimeout("salir()", 2000);
        }
    }
}
function enviar() {
    frmUpload.submit();
}
function salir() {
    modalDialog.Close(window, null);
}
function limpiar() {
    $I("txtComentario").value = "";
    resetFileInput("txtArchivo");
    $I("txtComentario").focus();
}
function resetFileInput(id) {
    var fld = document.getElementById(id);
    fld.form.reset();
    fld.focus();
}
