//Para el Drag&Drop
//var tbodyPerf;
//var tbodyEnt;
//Para evitar que se disparen pestañas pulsadas mientras está reiniciando
bReiniciandoPestanas = false;

function init() {
    try {
        if (!mostrarErrores()) return;

        iniciarPestanas();
        if ($I("hdnIdTipo").value=="E")
            tsPestanasGen.setSelectedIndex(1);
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function salir() {
    var strRetorno="F";
    //bSalir = false;bSaliendo = true;
    if (bCambios) strRetorno = "T";
    bCambios = false;
    //setTimeout("window.close();", 250); //para que de tiempo a grabar y actualizar "bCambios";
    modalDialog.Close(window, strRetorno);
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        if (aResul[1] == "DENEGADO") {
            mmoff("warper", aResul[2].replace(reg, "\n"), 400);
        }
        else
            mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                if (aPestGral[0].bModif == true) {
                    var tbl = $I("tblFamPerPri");
                    for (var i = tbl.rows.length - 1; i >= 0; i--) {
                        if (tbl.rows[i].getAttribute("bd") == "D") {
                            tbl.deleteRow(i);
                        }
//                        else if (tbl.rows[i].getAttribute("bd") != "") {
//                            mfa(tbl.rows[i], "N");
//                        }
                    }
                    var tbl2 = $I("tblFamPerPub");
                    for (var i = tbl2.rows.length - 1; i >= 0; i--) {
                        if (tbl2.rows[i].getAttribute("bd") == "D") {
                            tbl2.deleteRow(i);
                        }
//                        else if (tbl2.rows[i].getAttribute("bd") != "") {
//                            mfa(tbl2.rows[i], "N");
//                        }
                    }
                }
                if (aPestGral[1].bModif == true) {
                    var tbl = $I("tblFamEntPri");
                    for (var i = tbl.rows.length - 1; i >= 0; i--) {
                        if (tbl.rows[i].getAttribute("bd") == "D") {
                            tbl.deleteRow(i);
                        }
//                        else if (tbl.rows[i].getAttribute("bd") != "") {
//                            mfa(tbl.rows[i], "N");
//                        }
                    }
                    var tbl2 = $I("tblFamEntPub");
                    for (var i = tbl2.rows.length - 1; i >= 0; i--) {
                        if (tbl2.rows[i].getAttribute("bd") == "D") {
                            tbl2.deleteRow(i);
                        }
//                        else if (tbl2.rows[i].getAttribute("bd") != "") {
//                            mfa(tbl2.rows[i], "N");
//                        }
                    }
                }
                DesmarcarTodo('tblFamPerPub');
                DesmarcarTodo('tblFamPerAje');

                DesmarcarTodo('tblFamEntPub');
                DesmarcarTodo('tblFamEntAje');

                $I("divPerFam").children[0].innerHTML = "";
                $I("divEntFam").children[0].innerHTML = "";

                mmoff("Suc", "Grabación correcta.", 170);
                sOp = "nuevo"; //para el mensaje de validación de pestaña pulsada
                //bCambios = false;
                break;

            case "perfiles":
                $I("divPerFam").children[0].innerHTML = aResul[2];
                break;
            case "publicarFamilia":
                ponerFilaPerfil(aResul[2], Utilidades.unescape(aResul[3]), gsNombreProfesional, 2);
                delFilaId('tblFamPerPri', aResul[2])
                break;
            case "importarFamilia":
                ponerFilaPerfil(aResul[2], Utilidades.unescape(aResul[3]),"", 1);
                break;
            case "getDatosPestana": 
                bOcultarProcesando = false;
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                break;
            case "entornos":
                $I("divEntFam").children[0].innerHTML = aResul[2];
                break;
            case "publicarFamiliaEntorno":
                //ponerFilaEntorno(aResul[2], Utilidades.unescape(aResul[3]), Utilidades.unescape(aResul[4]), 2);
                ponerFilaEntorno(aResul[2], Utilidades.unescape(aResul[3]), "", 2);
                delFilaId('tblFamEntPri', aResul[2])
                break;
            case "importarFamiliaEntorno":
                //ponerFilaEntorno(aResul[2], Utilidades.unescape(aResul[3]), Utilidades.unescape(aResul[4]), 1);
                ponerFilaEntorno(aResul[2], Utilidades.unescape(aResul[3]), "", 1);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
        if (bOcultarProcesando) 
            ocultarProcesando();
    }
}
function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var bOcultar = true;
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0":
                break;
            case "1": //Entornos
                $I("divFamEntPri").children[0].innerHTML = aResul[0];
                $I("divFamEntPub").children[0].innerHTML = aResul[1];
                $I("divFamEntAje").children[0].innerHTML = aResul[2];
                nfs = 0;
                break;
        }
        if (bOcultar) ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function modFilaPerf(oFila) {
    mfa(oFila, 'U', true);
    aG(0);
}
function mostrarPerf(oFila,tipo) {
    ms(oFila);
    ponerCabecera('P', getCelda(oFila, 0), tipo);
    perfiles(oFila.id);
}
function ponerCabecera(familia, sDen, tipo) {
    var sLit = "";
    if (familia == "P")
        sLit = "Perfiles de la familia ";
    else
        sLit = "Entornos de la familia ";
    switch (tipo) {
        case 1:
            sLit += "privada ";
            //sLit += oFila.cells[0].children[0].value;
            break;
        case 2:
            sLit += "publica ";
            //sLit += oFila.cells[0].children[0].value;
            break;
        case 3:
            sLit += "ajena ";
            //sLit += oFila.cells[0].children[0].innerHTML;
            break;
    }
    sLit += sDen;
    if (sLit.length > 50)
        sLit = sLit.substring(0, 50) + "...";
    if (familia == "P")
        $I("lblPerfiles").innerHTML = sLit;
    else
        $I("lblEntornos").innerHTML = sLit;
}
function perfiles(idPerf) {
    try {
        if (idPerf==-1) {
            ocultarProcesando();
            return false;
        }
        mostrarProcesando();
        var js_args = "perfiles@#@" + idPerf;
        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los perfiles de una familia", e.message);
        return false;
    }
}
function grabar() {
    try {
        if (!comprobarDatos()) {
            ocultarProcesando();
            return false;
        }

        mostrarProcesando();
        var js_args = "grabar@#@";
        js_args += grabarPerfPri(); //Familias Perfiles
        js_args += "@#@";
        js_args += grabarEntPri(); //Familias Entornos 

        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los datos a grabar", e.message);
        return false;
    }
}
function comprobarDatos() {
    try {

        return true;
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabarPerfPri() {
    var sb = new StringBuilder;
    try {
        if (aPestGral[0].bModif) {
            var tbl = $I("tblFamPerPri");
            for (var i = 0; i < tbl.rows.length; i++) {
                if (tbl.rows[i].getAttribute("bd") != "") {
                    sb.Append(tbl.rows[i].getAttribute("bd") + "##"); //0
                    sb.Append(tbl.rows[i].id + "##"); //1
                    //sb.Append(Utilidades.escape(tbl.rows[i].cells[1].children[0].value)); //2
                    sb.Append("##0///");
                }
            }
            var tbl2 = $I("tblFamPerPub");
            for (var i = 0; i < tbl2.rows.length; i++) {
                if (tbl2.rows[i].getAttribute("bd") != "") {
                    sb.Append(tbl2.rows[i].getAttribute("bd") + "##"); //0
                    sb.Append(tbl2.rows[i].id + "##"); //1
                    //sb.Append(Utilidades.escape(tbl2.rows[i].cells[1].children[0].value)); //2
                    sb.Append("##1///");
                }
            }
        }
        return sb.ToString();
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al recojer los perfiles a grabar", e.message);
        return false;
    }
}
function grabarEntPri() {
    var sb = new StringBuilder;

    if (aPestGral[1].bModif) {
        var tbl = $I("tblFamEntPub");
        for (var i = 0; i < tbl.rows.length; i++) {
            if (tbl.rows[i].getAttribute("bd") != "") {
                sb.Append(tbl.rows[i].getAttribute("bd") + "##"); //0
                sb.Append(tbl.rows[i].id + "##"); //1
                //sb.Append(Utilidades.escape(tbl.rows[i].cells[0].children[0].value)); //2
                sb.Append("##1///");
            }
        }
        var tbl2 = $I("tblFamEntPri");
        for (var i = 0; i < tbl2.rows.length; i++) {
            if (tbl2.rows[i].getAttribute("bd") != "") {
                sb.Append(tbl2.rows[i].getAttribute("bd") + "##"); //0
                sb.Append(tbl2.rows[i].id + "##"); //1
                //sb.Append(Utilidades.escape(tbl2.rows[i].cells[0].children[0].value)); //2
                sb.Append("##0///");
            }
        }
    }
    return sb.ToString();
}
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
var aPestGral = new Array();
var tsPestanasGen = null;
function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanasGen.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);

    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        bReiniciandoPestanas = true;
        for (var i = 1; i < tsPestanasGen.bbd.bba.getItemCount(); i++)
            aPestGral[i].bLeido = true;

        tsPestanasGen.setSelectedIndex(0);

        for (var i = 1; i < tsPestanasGen.bbd.bba.getItemCount(); i++) {
            aPestGral[i].bLeido = false;
            aPestGral[i].bModif = false;
        }
        bReiniciandoPestanas = false;
    }
    catch (e) {
        bReiniciandoPestanas = false;
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function CrearPestanas() {
    try {
        //tsPestanasGen = EO1021.r._o_ctl00_CPHC_tsPestanasGen;
        tsPestanasGen = EO1021.r._o_tsPestanasGen;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las pestañas de primer nivel.", e.message);
    }
}
var bValidacionPestanas = true;
//validar pestana pulsada
function vpp(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;
        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        if (sSistemaPestanas == "tsPestanasGen" || sSistemaPestanas == "ctl00_CPHC_tsPestanasGen") {
            if (nPestanaPulsada > 0) {
                //Evaluar lo que proceda, y si no se cumple la validación
//                if ($I("txtNumPE").value == "") {
//                    mmoff("Inf", "El acceso a la pestaña seleccionada, requiere grabar el proyecto.", 430);
//                    eventInfo.cancel();
//                    return false;
//                }
            }
        }

        return true;
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al validar la pestaña pulsada.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;
        if (bReiniciandoPestanas) return;
        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        switch (sSistemaPestanas) {  //ID
            case "ctl00_CPHC_tsPestanasGen":
            case "tsPestanasGen":
                if (!aPestGral[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                } 
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function getDatos(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana + "@#@";
//        switch (parseInt(iPestana, 10)) {
//            case 1:
//                js_args += $I("hdnIdProyectoSubNodo").value;
//                break;
//            default:
//                //js_args += parseInt(dfn($I("txtNumPE").value));
//                break;
//        }
        RealizarCallBack(js_args, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}
function aG(iPestana) {//controla en qué pestañas se han realizado modificaciones.
    try {
        aPestGral[iPestana].bModif = true;
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en pestaña " + iPestana, e.message);
    }
}
////////////// FIN CONTROL DE PESTAÑAS  /////////////////////////////////////////////

//function addFila(tabla) {
//    var oNF = $I(tabla).insertRow(-1);
//    oNF.id = idNewFila--;
//    oNF.setAttribute("bd", "I");
//    oNF.setAttribute("style", "height:20px");
//    //oNF.attachEvent('onclick', mm);
//    oNF.onclick = function() { ms(this); };
//    
//    oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
//    
//    var oCtrl1 = document.createElement("input");
//    oCtrl1.setAttribute("type", "text");
//    oCtrl1.className = "txtL";
//    oCtrl1.setAttribute("style", "width:230px");
//    oCtrl1.setAttribute("maxLength", "50");

//    oCtrl1.onfocus = function() { this.className = 'txtM'; this.select(); };
//    oCtrl1.onblur = function() { this.className = 'txtL';};
//    //oCtrl1.onkeyup = function() { actualizarDatos(this) };
//    oNF.insertCell(-1).appendChild(oCtrl1);
//    
//    ms(oNF);

//    oNF.cells[1].children[0].focus();
//}
var iIndice = 0;
var oTabla;
function delFila(tabla) {
    var aFila = FilasDe(tabla);
    if (aFila == null) return;
    if (aFila.length == 0) return;
    var bBorrarFam = false;
    for (var i = aFila.length - 1; i >= 0; i--) {
        if (aFila[i].className == "FS") {
            if (aFila[i].getAttribute("bd") == "I") {
                $I(tabla).deleteRow(i);
                BorrarFilasDe('tblPerFam'); //Tabla con los perfiles de una familia
                $I("lblPerfiles").innerHTML = "Perfiles de la familia";
            }
            else {
                iIndice = i;
                bBorrarFam = true;
                oTabla = tabla;
                break;
            }
        }
    }
    if (bBorrarFam == true) borrarFamilia();
}
function borrarFamilia() {
    ocultarProcesando();
    jqConfirm("", "¿Deseas borrar la familia?", "", "", "war", 250).then(function (answer) {
        if (answer) {
            mostrarProcesando();
            var aFila = FilasDe(oTabla);
            aFila[iIndice].setAttribute("bd", "D");
            BorrarFilasDe('tblPerFam'); //Tabla con los perfiles de una familia
            setTimeout("grabar()", 20);
            $I("lblPerfiles").innerHTML = "Perfiles de la familia";
        }
        else {
            ocultarProcesando();
        }
    });
}
//Borra fila por Id
function delFilaId(tabla, clave) {
    var aFila = FilasDe(tabla);
    if (aFila == null) return;
    if (aFila.length == 0) return;
    for (var i = aFila.length - 1; i >= 0; i--) {
        if (aFila[i].id == clave) {
            $I(tabla).deleteRow(i);
        }
    }
}
//function actualizarDatos(oNombre) {
//    try {
//        fm_mn(oNombre.parentNode.parentNode);
//    }
//    catch (e) {
//        mostrarErrorAplicacion("Error al establecer la denominación.", e.message);
//    }
//}
function verificarDenominacion(tablaPri, tablaPub, denFam) {
    var sRes = denFam;
    var bExiste = false;
    var bPublica = false;
    try {
        for (var i = $I(tablaPri).rows.length - 1; i >= 0; i--) {
            if (denFam == $I(tablaPri).rows[i].cells[0].children[0].value) {
                bExiste = true;
                break;
            }
        }
        if (!bExiste) {
            for (var i = $I(tablaPub).rows.length - 1; i >= 0; i--) {
                if (gsIdFicepi = $I(tablaPub).rows[i].getAttribute("f")) {
                    if (denFam == $I(tablaPub).rows[i].cells[0].children[0].innerHTML) {
                        bExiste = true;
                        bPublica = true;
                        break;
                    }
                }
            }
        }
        if (bExiste) {
            var sAux = "El nombre de la familia ";
            if (bPublica)
                sAux += "publica ";
            else
                sAux += "privada ";
            sAux += denFam + " ya existe. Indique el nombre con el que desea importar la familia";

            sRes = prompt(sAux, '');
            
            if (sRes == null)
                sRes = "";
            else {
                if (sRes != "") {
                    sRes = sRes.toUpperCase();
                    return verificarDenominacion(tablaPri, tablaPub, sRes);
                }
            }
        }
        return sRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al verificar denominaciones ", e.message);
    }
}
//PERFILES///
function addFamPerfil(tipo) {
    try {
        mDetPerf(-1, "", tipo);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir familia de perfil ", e.message);
    }
}
function delFamPerfil(tipo) {
    try {
        aG(0);
        if (tipo==1)
            delFila('tblFamPerPri');
        else
            delFila('tblFamPerPub');
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar familia de perfil ", e.message);
    }
}
function publicarFamPerfPri() {
    var idFam = -2;
    var denFam = "";
    try {
        mostrarProcesando();
        var tbl = $I("tblFamPerPri");
        for (var i = tbl.rows.length - 1; i >= 0; i--) {
            if (tbl.rows[i].className == "FS") {
                idFam = tbl.rows[i].id;
                denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].value);
                break;
            }
        }
        if (idFam > 0) {
            ocultarProcesando();
            jqConfirm("", "¿Desea publicar la familia?", "", "", "war", 250).then(function (answer) {
                if (answer) {
                    var js_args = "publicarFamilia@#@" + idFam + "@#@" + denFam;
                    RealizarCallBack(js_args, "");
                }
                else {
                    ocultarProcesando();
                }
            });
        }
        else {
            ocultarProcesando();
            if (idFam == -2)
                mmoff("Inf", "Debes seleccionar una familia a publicar", 300);
            else
                mmoff("Inf", "Debe grabar la familia antes de publicarla", 320);
        }
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al publicar perfil " + iPestana, e.message);
    }
}
function mostrarDetPerf(oFila, tipo) {
    //mDetPerf(oFila.id, oFila.cells[0].children[0].value, tipo);
    var sDen = getCelda(oFila, 0);
    mDetPerf(oFila.id, sDen, tipo);
}
function mDetPerf(idPerf, denPerf, tipo) {
    var sPantalla = strServer + "Capa_Presentacion/CVT/Mantenimientos/Familias/Perfiles/Default.aspx";
    sPantalla += "?idF=" + idPerf;
    sPantalla += "&denF=" + denPerf;
    mostrarProcesando();
    modalDialog.Show(sPantalla, self, sSize(655, 640))
        .then(function(ret) {
            if (ret != null) {
                aG(0);
                var aDatos = ret.split("///");
                if (idPerf == -1) {
                    ponerFilaPerfil(aDatos[0], aDatos[1], gsNombreProfesional, tipo);
                }
                else
                    cambiarDenominacion("P", tipo, aDatos[0], aDatos[1]);
                setTimeout("perfiles(" + aDatos[0] + ");", 20);
            }
        });
    window.focus();
    ocultarProcesando();

}
function cambiarDenominacion(tipoFamilia, tipo, idElem, denElem){
    var sTabla = "";
    if (tipoFamilia == "P") {//Perfil
        if (tipo == 1) {//Privada
            sTabla = "tblFamPerPri";
        }
        else {//Publica
            sTabla = "tblFamPerPub";
        }
    }
    else{//Entorno
        if (tipo == 1) {//Privado
            sTabla = "tblFamEntPri";
        }
        else {//Publico
            sTabla = "tblFamEntPub";
        }
    }
    if (sTabla != "") {
        var tbl = $I(sTabla);
        for (var i = tbl.rows.length - 1; i >= 0; i--) {
            if (tbl.rows[i].id == idElem) {
                //denFam = Utilidades.escape(getCelda(tbl.rows[i], 0));
                setCelda(tbl.rows[i], 0, denElem);
                break;
            }
        }
    }

}
function ponerFilaPerfil(idPerf, denPerf, denProf, tipo) {
    var oNF;
    if (tipo == 1)
        oNF = $I("tblFamPerPri").insertRow(-1);
    else
        oNF = $I("tblFamPerPub").insertRow(-1);
    oNF.id = idPerf;
    oNF.setAttribute("bd", "");
    oNF.setAttribute("style", "height:20px");
    oNF.onclick = function() { mostrarPerf(this, tipo); };
    //oNF.onkeyup = function() { modFilaPerf(this); };
    oNF.ondblclick = function() { mostrarDetPerf(this,1); };
    
    //oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));

    var oCtrl1 = document.createElement("input");
    oCtrl1.setAttribute("type", "text");
    oCtrl1.className = "txtL";
    if (tipo == 1)
        oCtrl1.setAttribute("style", "width:440px; cursor:url(../../../../images/imgManoAzul2.cur),pointer;");
    else
        oCtrl1.setAttribute("style", "width:340px; cursor:url(../../../../images/imgManoAzul2.cur),pointer;");
    oCtrl1.setAttribute("maxLength", "50");
    oCtrl1.setAttribute("value", denPerf);
    oCtrl1.setAttribute("title", denPerf);
    //oCtrl1.onfocus = function() { this.className = 'txtM'; this.select(); };
    //oCtrl1.onblur = function() { this.className = 'txtL'; };
    //oCtrl1.onclick = function() { perfiles(idPerf); };
    oNF.insertCell(-1).appendChild(oCtrl1);
    
    if (tipo == 2) {
        var oCtrl2 = document.createElement("nobr");
        oCtrl2.className = "NBR W100";
        oCtrl2.setAttribute("style", 'margin-left:0px;');
        oCtrl2.attachEvent('onmouseover', TTip);
        //oCtrl2.setAttribute("value", denProf);
        oCtrl2.setAttribute("title", denProf);
        oNF.insertCell(-1).appendChild(oCtrl2);
        oNF.cells[1].children[0].innerHTML = denProf;
    }
    ms(oNF);
    //$I("divCapaPer").scrollTop = $I("divCapaPer").scrollHeight;
    //$I("divCapaPer").scrollTop = $I("divCapaPer").scrollTop + 16;
    $I("divFamPerPri").scrollTop = $I("tblFamPerPri").rows[$I("tblFamPerPri").rows.length - 1].offsetTop - 16;
    ponerCabecera('P', denPerf, tipo);
    bCambios = true;
    //oNF.cells[1].children[0].focus();
}
//function ponerFilaPerfilPublica(idPerf, denPerf) {
//    var oNF = $I("tblFamPerPub").insertRow(-1);
//    oNF.id = idPerf;
//    oNF.setAttribute("style", "height:20px");
//    oNF.onclick = function() { ms(this); perfiles(idPerf); };

//    var oCtrl1 = document.createElement("nobr");
//    oCtrl1.className = "NBR W400";
//    oCtrl1.setAttribute("style", 'margin-left:3px;');
//    oCtrl1.attachEvent('onmouseover', TTip);
//    oNF.insertCell(-1).appendChild(oCtrl1);
//    oNF.cells[0].children[0].innerHTML = Utilidades.unescape(denPerf);
//    //oCtrl1.onclick = function() { perfiles(idPerf); };
//    ms(oNF);
//}
function importarFamPerfAje() {
    importarFamPerf("tblFamPerAje");
}
function importarFamPerfPub() {
    importarFamPerf("tblFamPerPub");
}
//Pasa una familia ajena o publica a privada
function importarFamPerf(tabla) {
    var idFam = -1;
    var denFam = "";
    //var denProf = "";
    try {
        mostrarProcesando();
        var tbl = $I(tabla);
        for (var i = tbl.rows.length - 1; i >= 0; i--) {
            if (tbl.rows[i].className == "FS") {
                idFam = tbl.rows[i].id;
                //denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].value);
                denFam = Utilidades.escape(getCelda(tbl.rows[i],0));

                //denProf = Utilidades.escape(tbl.rows[i].cells[1].children[0].innerHTML);
//                if (tabla == "tblFamPerAje")
//                    denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].innerHTML);
//                else
//                    denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].value);
                break;
            }
        }
        if (idFam > 0) {
            denFam = verificarDenominacion("tblFamPerPri", "tblFamPerPub", denFam);
            if (denFam !== "") {
                var js_args = "importarFamilia@#@" + idFam + "@#@" + denFam;// + "@#@" + denProf;
                RealizarCallBack(js_args, "");
            }
            else {
                ocultarProcesando();
               mmoff("Inf", "Debes introducir el nombre con el que desea importar la familia", 400);
            }
        }
        else {
            ocultarProcesando();
            mmoff("Inf", "Debes seleccionar una familia a importar", 300);
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al importar la familia ", e.message);
    }
}
/////////////
//ENTORNOS///
function addFamEntorno(tipo) {
    try {
        mDetEnt(-1, "", tipo);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir familia de entorno ", e.message);
    }
}
function delFamEntorno(tipo) {
    try {
        aG(1);
        if (tipo == 1)
            delFila('tblFamEntPri');
        else
            delFila('tblFamEntPub');
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar familia de entorno ", e.message);
    }
}
function publicarFamEntPri() {
    var idFam = -2;
    var denFam = "";
    try {
        mostrarProcesando();
        var tbl = $I("tblFamEntPri");
        for (var i = tbl.rows.length - 1; i >= 0; i--) {
            if (tbl.rows[i].className == "FS") {
                idFam = tbl.rows[i].id;
                denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].value);
                break;
            }
        }
        if (idFam > 0) {
            ocultarProcesando();
            jqConfirm("", "¿Desea publicar la familia?", "", "", "war", 250).then(function (answer) {
                if (answer) {
                    var js_args = "publicarFamiliaEntorno@#@" + idFam + "@#@" + denFam;
                    RealizarCallBack(js_args, "");
                }
                else {
                    ocultarProcesando();
                }
            });
        }
        else {
            ocultarProcesando();
            if (idFam == -2)
                mmoff("Inf", "Debes seleccionar una familia a publicar", 300);
            else
                mmoff("Inf", "Debe grabar la familia antes de publicarla", 320);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al publicar entorno " + iPestana, e.message);
    }
}
function mostrarDetEnt(oFila, tipo) {
    //mDetEnt(oFila.id, oFila.cells[0].children[0].value, tipo);
    var sDen = getCelda(oFila, 0);
    mDetEnt(oFila.id, sDen, tipo);
}
function mDetEnt(idEnt, denEnt, tipo) {
    var sPantalla = strServer + "Capa_Presentacion/CVT/Mantenimientos/Familias/Entornos/Default.aspx";
    sPantalla += "?idF=" + idEnt;
    sPantalla += "&denF=" + denEnt;
    mostrarProcesando();
    modalDialog.Show(sPantalla, self, sSize(655, 640))
        .then(function(ret) {
            if (ret != null) {
                aG(1);
                var aDatos = ret.split("///");
                if (idEnt == -1) {
                    ponerFilaEntorno(aDatos[0], aDatos[1], gsNombreProfesional, tipo);
                }
                else
                    cambiarDenominacion("E", tipo, aDatos[0], aDatos[1])
                setTimeout("entornos(" + aDatos[0] + ");", 20);
            }
        });
    window.focus();
    ocultarProcesando();

}
function ponerFilaEntorno(idEnt, denEnt, denProf, tipo) {
    var oNF;
    if (tipo == 1)
        oNF = $I("tblFamEntPri").insertRow(-1);
    else
        oNF = $I("tblFamEntPub").insertRow(-1);
    oNF.id = idEnt;
    oNF.setAttribute("bd", "");
    oNF.setAttribute("style", "height:20px");
    oNF.onclick = function() { mostrarEnt(this, tipo); };
    //oNF.onkeyup = function() { modFilaEnt(this); };
    oNF.ondblclick = function() { mostrarDetEnt(this, 1); };

    //oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));

    var oCtrl1 = document.createElement("input");
    oCtrl1.setAttribute("type", "text");
    oCtrl1.className = "txtL";
    if (tipo == 1)
        oCtrl1.setAttribute("style", "width:440px; cursor:url(../../../../images/imgManoAzul2.cur),pointer;");
    else
        oCtrl1.setAttribute("style", "width:340px; cursor:url(../../../../images/imgManoAzul2.cur),pointer;");
    //oCtrl1.setAttribute("maxLength", "50");
    oCtrl1.setAttribute("value", denEnt);
    oCtrl1.setAttribute("title", denEnt);
    //oCtrl1.onfocus = function() { this.className = 'txtM'; this.select(); };
    //oCtrl1.onblur = function() { this.className = 'txtL'; };
    //oCtrl1.onclick = function() { entornos(idEnt); };
    oNF.insertCell(-1).appendChild(oCtrl1);

    //oNF.cells[1].children[0].focus();
    if (tipo == 2) {
        var oCtrl2 = document.createElement("nobr");
        oCtrl2.className = "NBR W100";
        oCtrl2.setAttribute("style", 'margin-left:0px;');
        oCtrl2.attachEvent('onmouseover', TTip);
        //oCtrl2.setAttribute("value", denProf);
        oCtrl2.setAttribute("title", denProf);
        oNF.insertCell(-1).appendChild(oCtrl2);
        oNF.cells[1].children[0].innerHTML = denProf;
    }
    $I("divFamEntPri").scrollTop = $I("tblFamEntPri").rows[$I("tblFamEntPri").rows.length - 1].offsetTop - 16;
    ms(oNF);
    ponerCabecera('E', denEnt, tipo);
    bCambios = true;
}
function importarFamEntAje() {
    importarFamEnt("tblFamEntAje");
}
function importarFamEntPub() {
    importarFamEnt("tblFamEntPub");
}
//Pasa una familia privada o publica a privada
function importarFamEnt(tabla) {
    var idFam = -1;
    var denFam = "";
    //var denProf = "";
    try {
        mostrarProcesando();
        var tbl = $I(tabla);
        for (var i = tbl.rows.length - 1; i >= 0; i--) {
            if (tbl.rows[i].className == "FS") {
                idFam = tbl.rows[i].id;
                //denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].innerHTML);
                denFam = Utilidades.escape(getCelda(tbl.rows[i], 0));
                //denProf = Utilidades.escape(tbl.rows[i].cells[1].children[0].innerHTML);
                //                if (tabla == "tblFamEntAje")
//                    denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].innerHTML);
//                else
//                    denFam = Utilidades.escape(tbl.rows[i].cells[0].children[0].value);
                break;
            }
        }
        if (idFam > 0) {
            denFam = verificarDenominacion("tblFamEntPri", "tblFamEntPub", denFam);
            if (denFam != "") {
                var js_args = "importarFamiliaEntorno@#@" + idFam + "@#@" + denFam;// + "@#@" + denProf;
                RealizarCallBack(js_args, "");
            }
        }
        else {
            ocultarProcesando();
            mmoff("Inf", "Debes seleccionar una familia a importar", 300);
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al importar la familia ", e.message);
    }
}
function modFilaEnt(oFila) {
    mfa(oFila, 'U', true);
    aG(1);
}
function mostrarEnt(oFila, tipo) {
    ms(oFila);
    ponerCabecera('E', getCelda(oFila, 0), tipo);
    entornos(oFila.id);
}
function entornos(idEnt) {
    try {
        if (idEnt == -1) {
            ocultarProcesando();
            return false;
        }
        mostrarProcesando();
        var js_args = "entornos@#@" + idEnt;
        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los entornos de una familia", e.message);
        return false;
    }
}

/////////////