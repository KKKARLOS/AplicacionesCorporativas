var aProfesional = new Array();
var aGestor = new Array();

function init() {
    try{
        if (!mostrarErrores()) return;

        getGestores();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getGestores":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("tdGrupo1").innerText = aResul[3];
                $I("txtApellido1").focus();
                break;

            case "addGestor":
                //mmoff("War", "Falta añadir los Profesionales a las tablas.", 300);
//                bOcultarProcesando = false;
//                setTimeout("getGestores()", 20);
                addGestorTabla();
                break;
            case "delGestor": 
                delGestorTabla();
                break;

            case "getProfesionales":
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                $I("txtApellido1").focus();
                $I("divProfesionales").children[0].innerHTML = aResul[2];
                //if (!document.all) $I("divCatalogo").children[0].children[0].style.backgroundImage = "url(../../../Images/imgFT20.gif)";
                $I("divProfesionales").scrollTop = 0;
                scrollTablaProf();
                //actualizarLupas("tblTitulo", "tblDatos");
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function setScroll() {
    try {
        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

var nFilaOldBodyMovil = -1;
function setScrollBuscar(){
    try {
        oDivBodyMovil.scrollTop = oDivBodyFijo.scrollTop;
        //alert(iFila);
        if (iFila != -1) {
            //msse($I("tblBodyMovil").rows[iFila]);
            ms($I("tblBodyMovil").rows[iFila]);
            nFilaOldBodyMovil = iFila;
        } else {
            if (nFilaOldBodyMovil != -1)
                $I("tblBodyMovil").rows[nFilaOldBodyMovil].className = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}

function getGestores() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;

        sb.Append("getGestores@#@");
      
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los proyectos.", e.message);
    }
}

//Marcar Desmarcar Tabla
function mdTabla(nAccion) {
    try {
        var aFilas = FilasDe("tblNegocios");
        if (aFilas != null) {
            for (i = 0; i < aFilas.length; i++) {
                aFilas[i].cells[0].children[0].checked = (nAccion == 1) ? true : false;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar", e.message);
    }
}

function addGestor(nGrupo) {
    try {
        var sb = new StringBuilder;
        sb.Append("addGestor@#@");
    
        //El array de profesional se simula, luego será de la tabla
        aProfesional.length = 0;
        //aProfesional[0] = { id: 1317, nombre: "Josemi", sexo: "V", tipo: "I", tabla: "" };
        //aProfesional[1] = { id: 8459, nombre: "Sandra", sexo: "M", tipo: "I", tabla: "" };
        var tblProfesionales = $I("tblProfesionales");
        if (tblProfesionales != null) {
            for (var i = 0; i < tblProfesionales.rows.length; i++) {
                if (tblProfesionales.rows[i].className == "FS") {
                    aProfesional[aProfesional.length] = { id: tblProfesionales.rows[i].id, nombre: tblProfesionales.rows[i].cells[1].innerText, sexo: tblProfesionales.rows[i].getAttribute("sexo"), tipo: tblProfesionales.rows[i].getAttribute("tipo"), tabla: "" };
                }
            }
        } else {
            mmoff("War", "No se ha seleccionado ningún profesional para la asignación de gestores.", 500);
            return;
        }

        aGestor.length = 0;

        var tblNegocios = $I("tblNegocios");
        var swNeg = 0;
        if (tblNegocios != null) {
            //Recorremos la tabla de negocios
            for (var i = 0; i < tblNegocios.rows.length; i++) {
                //Para cada negocio marcado
                if (tblNegocios.rows[i].cells[0].children[0].checked) {
                    swNeg = 1;
                    //Obtenemos la tabla a tratar
                    var tblGestores = FilasDe("tblGestoresG" + nGrupo + "_" + tblNegocios.rows[i].id);
                    //Para cada profesional seleccionado, miramos si ya existe.
                    //Si no existe, lo insertamos.
                    for (var x = 0; x < aProfesional.length; x++) {
                        var sw = 0;
                        for (var y = 0; y < tblGestores.length; y++) {
                            if (aProfesional[x].id == tblGestores[y].id) {
                                sw = 1; //Profesional ya existe como gestor
                                break;
                            }
                        }
                        if (sw == 0) {
                            //Hay que añadir al profesional como gestor.
                            sb.Append("I" + "{dato}"); // Accion
                            sb.Append(tblNegocios.rows[i].id + "{dato}"); // Negocio
                            //sb.Append(nGrupo + "{dato}");           // Grupo
                            sb.Append(aProfesional[x].id + "{fila}"); //Usuario
                            //aProfesional[x].tabla = "tblGestoresG" + nGrupo + "_" + tblNegocios.rows[i].id; //Para añadirlo a la tabla una vez la grabación ha sido correcta
                            aGestor[aGestor.length] = { id: aProfesional[x].id, nombre: aProfesional[x].nombre, sexo: aProfesional[x].sexo, tipo: aProfesional[x].tipo, tabla: "tblGestoresG" + nGrupo + "_" + tblNegocios.rows[i].id };
                        }
                    }
                }
            }
        }
        //alert(sb.ToString());
        if (sb.buffer.length == 1) {
            if (swNeg == 0)
                mmoff("War", "No se ha marcado ningún negocio para la asignación de gestores.", 420);
            //else
                //mmoff("War", "No se ha seleccionado ningún profesional para la asignación de gestores.", 500);

            return;
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a asignar gestores", e.message);
    }
}

function delGestor() {
    try {
        var sb = new StringBuilder;
        sb.Append("delGestor@#@");

        var tblNegocios = $I("tblNegocios");
        if (tblNegocios != null) {
            //Recorremos la tabla de negocios
            for (var i = 0; i < tblNegocios.rows.length; i++) {
                var tblGestoresG1 = FilasDe("tblGestoresG1_" + tblNegocios.rows[i].id);
                for (var x = 0; x < tblGestoresG1.length; x++) {
                    if (tblGestoresG1[x].className == "FS") {
                        sb.Append("D" + "{dato}"); // Accion
                        sb.Append(tblNegocios.rows[i].id + "{dato}");    // Negocio
                        //sb.Append("1" + "{dato}");                  // Grupo
                        sb.Append(tblGestoresG1[x].id + "{fila}");  //Usuario
                    }
                }
            }
        }
        //alert(sb.ToString());
        if (sb.buffer.length == 1) {
            mmoff("War", "No se ha seleccionado ningún profesional para su desasignación como gestor.", 500);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a desasignar gestores", e.message);
    }
}


function addGestorTabla() {
    try {
        if (aGestor.length > 0) {
            for (var i = 0; i < aGestor.length; i++) {
                var oNF = $I(aGestor[i].tabla).insertRow(-1);
                oNF.id = aGestor[i].id;
                oNF.setAttribute("sexo", aGestor[i].sexo);
                oNF.onclick = function() { mef(this) };
                
                var oNC1 = oNF.insertCell(-1);
                if (aGestor[i].sexo == "V") {
                    switch (aGestor[i].tipo) {
                        case "E": oNC1.appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oNC1.appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oNC1.appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (aGestor[i].tipo) {
                        case "E": oNC1.appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oNC1.appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oNC1.appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                var oNC2 = oNF.insertCell(-1);
                oNF.attachEvent("onmouseover", TTip);
                var oCtrl1 = document.createElement("nobr");
                oCtrl1.className = "NBR W250 MANO";
                oCtrl1.appendChild(document.createTextNode(aGestor[i].nombre));
                oNC2.appendChild(oCtrl1);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a asignar gestores de la tabla.", e.message);
    }
}

function delGestorTabla() {
    try {
        var tblNegocios = $I("tblNegocios");
        if (tblNegocios != null) {
            //Recorremos la tabla de negocios
            for (var i = 0; i < tblNegocios.rows.length; i++) {
                var tblGestoresG1 = $I("tblGestoresG1_" + tblNegocios.rows[i].id);
                for (var x = tblGestoresG1.rows.length - 1; x >= 0; x--) {
                    if (tblGestoresG1.rows[x].className == "FS")
                        tblGestoresG1.deleteRow(x);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a desasignar gestores de la tabla.", e.message);
    }
}

function getProfesionales() {
    try {
        if (fTrim($I("txtApellido1").value) == "" && fTrim($I("txtApellido2").value) == "" && fTrim($I("txtNombre").value) == "") {
            mmoff("War", "Debes indicar algún filtro de búsqueda", 200);
            return;
        }
        mostrarProcesando();
        var js_args = "getProfesionales@#@" + Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar los profesionales", e.message);
    }
}

var nTopScrollFICEPI = -1;
var nIDTimeFICEPI = 0;
function scrollTablaProf() {
    try {
        if ($I("divProfesionales").scrollTop != nTopScrollFICEPI) {
            nTopScrollFICEPI = $I("divProfesionales").scrollTop;
            clearTimeout(nIDTimeFICEPI);
            nIDTimeFICEPI = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollFICEPI / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divProfesionales").offsetHeight / 20 + 1, $I("tblProfesionales").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblProfesionales").rows[i].getAttribute("sw")) {
                oFila = $I("tblProfesionales").rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
    }
}       