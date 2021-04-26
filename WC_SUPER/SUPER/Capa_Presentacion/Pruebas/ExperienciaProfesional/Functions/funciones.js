function init(){
    try{
        //$I("txtApellido1").focus();
        bLectura = false;
//        if (nName == 'safari')
//            $I("cboConceptoEje").className = "comboSafari";
//        else
//            $I("cboConceptoEje").className = "comboNormal";
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
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                break;
        }
        ocultarProcesando();
    }
}

function getExp() {
    try {
        //if ($I("txtPSN").value == "") return;
        mostrarProcesando();
        //Acceso a la experiencia profesional desde proyecto SUPER
        var sParam = "?o=P";
        //Acceso a la experiencia profesional sin proyecto SUPER para experiencias fuera de Ibermatica
        //var sParam = "?o=F";

        sParam += "&pr=" + codpar($I("txtPSN").value);
        if (bLectura)
            sParam += "&m=" + codpar("R"); //Acceso a la experiencia profesional en modo escritura
        else
            sParam += "&m=" + codpar("W"); //Acceso a la experiencia profesional en modo lectura

        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Default.aspx" + sParam;
        var sSize = ((nName != "chrome")) ? "dialogwidth:980px; dialogheight:710px;" : "dialogwidth:980; dialogheight:710;";
        var sTamanio = sSize + "center:yes; status:NO; help:NO;";

        modalDialog.Show(sPantalla, self, sTamanio);
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la experiencia profesional", e.message);
    }
}

function subnodo() {
    try {
        mostrarProcesando();
        //Acceso a detalle de subnodo
        var sPantalla = "/SUPER/Capa_Presentacion/Administracion/DetalleSubnodo/Default.aspx?SN4=" + codpar(1) + "&SN3=" + codpar(2)+ "&SN2=" + codpar(17) + "&SN1=" + codpar(142) + "&Nodo=" + codpar(163)+ "&ID=" + codpar(906);
        modalDialog.Show(sPantalla, self, sSize(990, 550));
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el subnodo", e.message);
    }
}
function nodo() {
    try {
        mostrarProcesando();
        //Acceso a detalle de subnodo
        var sPantalla = "/SUPER/Capa_Presentacion/Administracion/DetalleNodo/Default.aspx?SN4=1&SN3=2&SN2=17&SN1=109&ID=163 ";
        modalDialog.Show(sPantalla, self, sSize(990, 610));
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el nodo", e.message);
    }
}
function snn1() {
    try {
        mostrarProcesando();
        //Acceso a detalle de subnodo
        var sPantalla = "/SUPER/Capa_Presentacion/Administracion/DetalleSN/Default.aspx?nNivel=4&SN4=1&SN3=2&SN2=17&SN1=142&ID=43";
        modalDialog.Show(sPantalla, self, sSize(990, 550));
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el SNN1", e.message);
    }
}
function snn2() {
    try {
        mostrarProcesando();
        //Acceso a detalle de subnodo
        var sPantalla = "/SUPER/Capa_Presentacion/Administracion/DetalleSN/Default.aspx?nNivel=3&SN4=1&SN3=2&SN2=17&SN1=142&ID=9";
        modalDialog.Show(sPantalla, self, sSize(990, 550));
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el SNN2", e.message);
    }
}
function snn3() {
    try {
        mostrarProcesando();
        //Acceso a detalle de subnodo
        var sPantalla = "/SUPER/Capa_Presentacion/Administracion/DetalleSN/Default.aspx?nNivel= " + codpar(2) + "&SN4=" + codpar(1);
        sPantalla += "&SN3=" + codpar(2) + "&SN2=" + codpar(17) + "&SN1=" + codpar(142) + "&ID=" + codpar(1);
        modalDialog.Show(sPantalla, self, sSize(990, 550));
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el SNN3", e.message);
    }
}
function snn4() {
    try {
        mostrarProcesando();
        //Acceso a detalle de subnodo
        var sPantalla = "/SUPER/Capa_Presentacion/Administracion/DetalleSN/Default.aspx?nNivel= " + codpar(1) + "&SN4=" + codpar(1);
        sPantalla += "&SN3=" + codpar(2) + "&SN2=" + codpar(17) + "&SN1=" + codpar(142) + "&ID=" + codpar(1);
        modalDialog.Show(sPantalla, self, sSize(990, 550));
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el SNN4", e.message);
    }
}

function buscar() {
    try {
            document.forms["aspnetForm"].submit();
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }
}
