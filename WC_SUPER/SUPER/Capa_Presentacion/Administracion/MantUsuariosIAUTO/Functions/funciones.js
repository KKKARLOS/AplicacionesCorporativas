document.onkeydown = KeyDown;
function KeyDown(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && ((node.type == "text") || (node.type == "checkbox"))) {
        try {
            var oNodo = getNextElement(node);
            setTimeout(function() { try { oNodo.focus() } catch (e) {} }, 10);
        } catch (e) { };
        return false;
    }
}

function getNextElement(field) {
    var form = field.form;
    for (var e = 0; e < form.elements.length; e++) {
        if (field == form.elements[e]) break;
    }
    e++;
    while (form.elements[e % form.elements.length].type == "hidden") {
        e++;
    }
    return form.elements[e % form.elements.length];
}

var oTarea = { "nPSN": 0,
    "nPT": 0,
    "nT": 0,
    "desT": "",
    "ImpFes": false,
    "NOJC": false,
    "Obligaest": false,
    "ete": 0,
    "ffe": "",
    "Comentario": ""
};

var oInput = document.createElement("input");
oInput.type = "text";
oInput.style.width = "26px";
oInput.style.cursor = "pointer";
oInput.className = "txtNumL";
oInput.setAttribute("maxLength", "5")

function init() {
    if ($I("hdnErrores").value == "") getProfesionales();
}

var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProf() {
    try {
        if ($I("divProfAsig").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divProfAsig").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divProfAsig").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
               
                if (oFila.getAttribute("bd") != "I") oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgFI.cloneNode(true), null);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                    }
                } else {
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                    }
                }
                oFila.cells[0].children[0].style.position = "relative";
                oFila.cells[0].children[0].style.left = "-3px";

                oFila.cells[1].children[0].style.position = "relative";
                oFila.cells[1].children[0].style.left = "-5px";

                sAux = oFila.cells[2].innerText;
                oFila.cells[2].innerText = "";  
                
                var oCtrl = document.createElement("nobr");
                oCtrl.className = "NBR W310";
                oCtrl.appendChild(document.createTextNode(sAux));
                oFila.cells[2].appendChild(oCtrl);

                if (oFila.getAttribute("tooltipProf") != "") {
                    oFila.cells[2].onmouseover = function() { showTTE(this.parentNode.getAttribute("tooltipProf")); }
                    oFila.cells[2].onmouseout = function() { hideTTE(); }
                }    
                            
                if (oFila.cells[3].innerText == "") {
                    mfa(oFila, "I");
                    oFila.cells[3].style.backgroundImage = "url('../../../images/imgRequerido.gif')";
                    oFila.cells[3].style.backgroundRepeat = "no-repeat";
                }
                else {
                    sAux = oFila.cells[3].innerText;
                    oFila.cells[3].innerText = "";                              
                    var oCtrl1 = document.createElement("nobr");
                    oCtrl1.className = "NBR W370";
                    oCtrl1.appendChild(document.createTextNode(sAux));
                    oFila.cells[3].appendChild(oCtrl1);

                    if (oFila.getAttribute("tooltipTarea") != "") {
                        oFila.cells[3].onmouseover = function() { showTTE(this.parentNode.getAttribute("tooltipTarea")); }
                        oFila.cells[3].onmouseout = function() { hideTTE(); }
                    }                                         
                }   
                                                    
                sAux = oFila.cells[4].innerText;
                oFila.cells[4].innerText = "";
                oInput.value = sAux;
                oFila.cells[4].appendChild(oInput.cloneNode(true), null);
                oFila.cells[4].children[0].onfocus = function() { seleccionar(this.parentNode.parentNode); fn(this, 2, 2);};
                oFila.cells[4].children[0].attachEvent("onchange", aGr);
                
                sAux = oFila.cells[5].innerText;
                oFila.cells[5].innerText = "";
                oInput.value = sAux;
                oFila.cells[5].appendChild(oInput.cloneNode(true), null);
                oFila.cells[5].children[0].onfocus = function() { fn(this,2,2); };
                oFila.cells[5].children[0].attachEvent("onchange", aGr);
                
                sAux = oFila.cells[6].innerText;
                oFila.cells[6].innerText = "";
                oInput.value = sAux;
                oFila.cells[6].appendChild(oInput.cloneNode(true), null);
                oFila.cells[6].children[0].onfocus = function() { fn(this, 2, 2); };
                oFila.cells[6].children[0].attachEvent("onchange", aGr);
                                
                sAux = oFila.cells[7].innerText;
                oFila.cells[7].innerText = "";
                oInput.value = sAux;
                oFila.cells[7].appendChild(oInput.cloneNode(true), null);
                oFila.cells[7].children[0].onfocus = function() { fn(this, 2, 2); };
                oFila.cells[7].children[0].attachEvent("onchange", aGr);                
                
                sAux = oFila.cells[8].innerText;
                oFila.cells[8].innerText = "";
                oInput.value = sAux;
                oFila.cells[8].appendChild(oInput.cloneNode(true), null);
                oFila.cells[8].children[0].onfocus = function() { fn(this, 2, 2); };
                oFila.cells[8].children[0].attachEvent("onchange", aGr);     
                
                sAux = oFila.cells[9].innerText;
                oFila.cells[9].innerText = "";
                oInput.value = sAux;
                oFila.cells[9].appendChild(oInput.cloneNode(true), null);
                oFila.cells[9].children[0].onfocus = function() { fn(this, 2, 2); };
                oFila.cells[9].children[0].attachEvent("onchange", aGr);   
                
                sAux = oFila.cells[10].innerText;
                oFila.cells[10].innerText = "";
                oInput.value = sAux;
                oFila.cells[10].appendChild(oInput.cloneNode(true), null);
                oFila.cells[10].children[0].onfocus = function() { fn(this, 2, 2); };
                oFila.cells[10].children[0].attachEvent("onchange", aGr);   
                
                sAux = oFila.cells[11].innerText;
                oFila.cells[11].innerText = "";
                var oCtrl = document.createElement("input");
                oCtrl.type = "checkbox";
                oCtrl.setAttribute("style", "width:15px;margin-left:10px;");
                oCtrl.className = "checkTabla";
                if (sAux == "1") oCtrl.checked = true; else oCtrl.checked = false;
                oCtrl.onclick = function() { aG(this) };
                oFila.cells[11].appendChild(oCtrl);                
            }
        }
        actualizarLupas("tblAsignados", "tblDatos");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function aG(ob) {
    mfa(ob.parentNode.parentNode, "U");
    activarGrabar();
}
function aGr(e) {
    try{
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;
        aG(oElement);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }           
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
    switch (aResul[0]) {
            case "getProfesionales":
                $I("divProfAsig").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                mmoff("hide");
                break;
            case "grabar":
                bCambios = false;
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;

                var bProfBorrados = false;
                var tblDatos = $I("tblDatos");

                for (var i = tblDatos.rows.length - 1; i >= 0; i--) {
                    if (tblDatos.rows[i].getAttribute("bd") == "D") {
                        bProfBorrados = true;
                        tblDatos.deleteRow(i);
                        continue;
                    } else if (tblDatos.rows[i].getAttribute("bd") == "I") {
                        tblDatos.rows[i].id = aEI[nIndiceEI];
                        nIndiceEI++;
                    }                        
                    mfa(tblDatos.rows[i], "N");
                }

                if (bProfBorrados) scrollTablaProf();

                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;
            case "getTarea":
                var sToolTip = "";
                sToolTip += "<label style='width:70px;'>P.Económico:</label>" + aResul[18] + "<br>";
                sToolTip += "<label style='width:70px;'>P.Técnico  :</label>" + aResul[19] + "<br>";
                if (aResul[20] != "") sToolTip += "<label style='width:70px;'>Fase       :</label>" + aResul[20] + "<br>";
                if (aResul[21] != "") sToolTip += "<label style='width:70px;'>Actividad  :</label>" + aResul[21] + "<br>";
                sToolTip += "<label style='width:70px;'>Tarea      :</label>" + oTarea.nT.toString("N", 7, 0) + " - " + oTarea.desT;

                oCeldaTarea.onmouseover = function() { showTTE(sToolTip); }
                oCeldaTarea.onmouseout = function() { hideTTE(); }
               
                break;                 
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
    ocultarProcesando();
}

function getProfAsig() {
    try {
        if (bLectura) return;
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=27&TipoRecurso=I";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(850, 420))
            .then(function(ret) {
                if (ret != null) {
                    var sb = new StringBuilder;
                    var aProf = ret.split("///");
                    var sw = 0;
                    var aDatos;
                    var sTipo = "";
                    var bHayInsert = false;
                    for (var i = 0; i < aProf.length; i++) {
                        aDatos = aProf[i].split("@#@");
                        sw = 0;
                        var tblDatos = $I("tblDatos");
                        for (var x = 0; x < tblDatos.rows.length; x++) {
                            if (aDatos[0] == tblDatos.rows[x].id) {
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 0) {
                            bHayInsert = true;
                            sb.Append("<tr id='" + aDatos[0] + "' ");
                            sb.Append("bd='I' ");
                            sb.Append("tipo='P' ");
                            sb.Append("idTarea='' ");
                            sb.Append("sexo='" + aDatos[2] + "' ");

                            var sToolTip = "<label style='width:70px;'>Profesional:</label>" + Utilidades.unescape(aDatos[1]);
                            sToolTip += "<br><label style='width:70px;'>Usuario:</label>" + aDatos[0].ToString("N", 7, 0);
                            sToolTip += "<br><label style='width:70px;'>" + strEstructuraNodo + ":</label>" + Utilidades.unescape(aDatos[3]);
                            sToolTip += "<br><label style='width:70px;'>Empresa:</label>" + Utilidades.unescape(aDatos[4]);
                            sb.Append("tooltipProf=" + "\"" + sToolTip + "\" ");

                            sb.Append(" style='height:20px;'>");

                            //Cells 0
                            sb.Append("<td></td>");
                            //Cells 1
                            sb.Append("<td style='text-align:center'></td>");
                            //Cells 2 //Usuario
                            //sb.Append("<td style='text-align:right; padding-right: 5px;'>" + aDatos[0].ToString("N", 9, 0) + "</td>");
                            //Cells 2 //Profesional

                            sb.Append("<td onmouseover=showTTE(this.parentNode.getAttribute('tooltipProf')) onMouseout=\"hideTTE()\">" + Utilidades.unescape(aDatos[1]) + "</td>");

                            //Cells 3 //Tarea
                            sb.Append("<td class='MAM' ondblclick='getTarea(this)'></td>");
                            //Cells 4 //Lunes
                            sb.Append("<td></td>");
                            //Cells 5 //Martes
                            sb.Append("<td></td>");
                            //Cells 6 //Miércoles
                            sb.Append("<td></td>");
                            //Cells 7 //Jueves
                            sb.Append("<td></td>");
                            //Cells 8 //Viernes
                            sb.Append("<td></td>");
                            //Cells 9 //Sábado
                            sb.Append("<td></td>");
                            //Cells 10 //Domingo
                            sb.Append("<td></td>");
                            //Cells 11 //Aviso
                            sb.Append("<td>0</td>");
                            sb.Append("</tr>");
                        }
                    }

                    if (bHayInsert) {
                        insertarFilasEnTablaDOM("tblDatos", sb.ToString(), $I("tblDatos").rows.length, true);
                        $I("divProfAsig").scrollTop = tblDatos.rows[tblDatos.rows.length - 1].offsetTop - 20;
                        scrollTablaProf();
                        activarGrabar();
                    }
                }
            });
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al seleccionar los profesionales", e.message);
    }
}
var oCeldaTarea;

function getTarea(o) {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/IAP/ImpMasiva/getTareaMasiva/Default.aspx?idUsuario=" + o.parentNode.getAttribute("id"), self, sSize(650, 670))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    o.parentNode.setAttribute("idTarea", aDatos[2]);
                    o.style.backgroundImage = "";
                    o.innerHTML = "";
                    var oCtrl1 = document.createElement("nobr");
                    oCtrl1.attachEvent("onmouseover", TTip)
                    oCtrl1.className = "NBR W370";
                    oCtrl1.appendChild(document.createTextNode(aDatos[3]));
                    o.appendChild(oCtrl1);
                    oCeldaTarea = o;

                    with (oTarea) {
                        nPSN = aDatos[0];
                        nPT = aDatos[1];
                        nT = aDatos[2];
                        desT = aDatos[3];
                        ImpFes = (aDatos[4] == "1") ? true : false;
                        NOJC = (aDatos[5] == "1") ? true : false;
                        Obligaest = (aDatos[6] == "1") ? true : false;
                    }

                    mfa(o.parentNode, "U");
                    activarGrabar();

                    var js_args = "getTarea@#@" + aDatos[2] + "@#@" + o.parentNode.getAttribute("id");
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
            });

        //window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las tareas", e.message);
    }
}

function eliminarProfAsig() {
    if (bLectura) return;
    mostrarProcesando();
    try {
        var sw = 0;
        var tblDatos = $I("tblDatos");
        for (var i = tblDatos.rows.length - 1; i >= 0; i--) {
            if (tblDatos.rows[i].className == "FS") {
                if (tblDatos.rows[i].getAttribute("bd") == 'I') tblDatos.deleteRow(i);
                else mfa(tblDatos.rows[i], "D");
                sw = 1;
            }
        }
        if (sw == 1) {
            activarGrabar();
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar los profesionales a eliminar", e.message);
    }
}
function controlValor(oFila, j) {
    if (oFila.cells[j].children[0].value < '0' && oFila.cells[j].children[0].value!='') {
        mmoff("Inf", "El valor del campo no puede ser negativo.", 300);
        seleccionar(oFila);
        oFila.cells[j].children[0].focus();
        oFila.cells[j].children[0].select();
        return false;    
    }
    if (parseInt(oFila.cells[j].children[0].value) < 0 || parseInt(oFila.cells[j].children[0].value) > 24) {
        mmoff("Inf", "El valor del campo admite un valor dentro del rango que va de 0 a 24.", 420);
        seleccionar(oFila);
        oFila.cells[j].children[0].focus();
        oFila.cells[j].children[0].select();
        return false;
    }
    else return true;
}
function grabar(){
    try{
        var js_args = "grabar@#@";

        var sb = new StringBuilder;
        for (var i=0; i<$I("tblDatos").rows.length;i++){
            if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                if (!tblDatos.rows[i].getAttribute("sw")) {
                    seleccionar($I("tblDatos").rows[i]);
                    $I("divProfAsig").scrollTop = $I("tblDatos").rows[i].offsetTop - 20;
                    mmoff("War", "Debes indicar al menos un valor.", 240);
                    return;
                }
            
                sb.Append($I("tblDatos").rows[i].getAttribute("bd") + "##"); //0
                sb.Append($I("tblDatos").rows[i].id + "##"); //1
                sb.Append($I("tblDatos").rows[i].getAttribute("idTarea") + "##"); //2

                if ($I("tblDatos").rows[i].getAttribute("idTarea") == "") {
                    seleccionar($I("tblDatos").rows[i]);
                    $I("divProfAsig").scrollTop = $I("tblDatos").rows[i].offsetTop - 20;
                    mmoff("War", "Debes indicar el nombre de la tarea", 240);
                    return;
                }
                if  (
                    $I("tblDatos").rows[i].cells[4].children[0].value=="" &&
                    $I("tblDatos").rows[i].cells[5].children[0].value=="" &&
                    $I("tblDatos").rows[i].cells[6].children[0].value=="" &&
                    $I("tblDatos").rows[i].cells[7].children[0].value=="" &&
                    $I("tblDatos").rows[i].cells[8].children[0].value=="" &&
                    $I("tblDatos").rows[i].cells[9].children[0].value=="" &&
                    $I("tblDatos").rows[i].cells[10].children[0].value=="" &&
                    $I("tblDatos").rows[i].getAttribute("bd") != "D"
                    ) 
                {
                    seleccionar($I("tblDatos").rows[i]);
                    mmoff("War", "Debes indicar al menos un valor.", 240);
                    return;
                }                    
                if (controlValor($I("tblDatos").rows[i], 4) == false) return;
                sb.Append($I("tblDatos").rows[i].cells[4].children[0].value + "##"); //3

                if (controlValor($I("tblDatos").rows[i], 5) == false) return;
                sb.Append($I("tblDatos").rows[i].cells[5].children[0].value + "##"); //4

                if (controlValor($I("tblDatos").rows[i], 6) == false) return;
                sb.Append($I("tblDatos").rows[i].cells[6].children[0].value + "##"); //5

                if (controlValor($I("tblDatos").rows[i], 7) == false) return;
                sb.Append($I("tblDatos").rows[i].cells[7].children[0].value + "##"); //6

                if (controlValor($I("tblDatos").rows[i], 8) == false) return;
                sb.Append($I("tblDatos").rows[i].cells[8].children[0].value + "##"); //7

                if (controlValor($I("tblDatos").rows[i], 9) == false) return;
                sb.Append($I("tblDatos").rows[i].cells[9].children[0].value + "##"); //8

                if (controlValor($I("tblDatos").rows[i], 10) == false) return;
                sb.Append($I("tblDatos").rows[i].cells[10].children[0].value + "##"); //9

                sb.Append(($I("tblDatos").rows[i].cells[11].children[0].checked)? "1##" : "0##"); //10            
                
                sb.Append("///");
            }
        }
        js_args += sb.ToString();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del profesional", e.message);
    }
}

function getProfesionales(){
    try {
        mmoff("InfPer", "Obteniendo profesionales.", 220);
        var js_args = "getProfesionales@#@";
        //mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los profesionales con imputaciones automáticas.", e.message);
    }
}        