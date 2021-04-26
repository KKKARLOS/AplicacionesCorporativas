var oDivBodyFijo = null;
var oDivTituloMovil = null;
var oDivBodyMovil = null;

//var mousewheelevt = (document.attachEvent) ? "mousewheel" : "DOMMouseScroll"  //FF doesn't recognize mousewheel as of FF3.x
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x  

var bSalir = false;
var bAlgunCambio = false;
var tblBodyMovil = null;
var tblTituloMovil = null;
var aMeses = new Array();

function init(){
    try {    
        if (!mostrarErrores()) return;

        if (bLecturaInsMes) {
            setOp($I("btnInsertarMes"), 30);
        }

        if (bLectura) {
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
        }        

        if (sCualidad == "J") setOp($I("btnInsertarMes"), 30);


        var iWidth = parseInt(divTituloMovil.children[0].style.width.substring(0, divTituloMovil.children[0].style.width.length - 2), 10)

        if (iWidth <= 540) {
            divTituloMovil.style.width = divTituloMovil.children[0].style.width;
            divBodyMovil.style.width = (parseInt(divBodyMovil.children[0].children[0].style.width.substring(0, divBodyMovil.children[0].children[0].style.width.length - 2), 10) + 16) + "px"
        }
        
        divBodyMovil.children[0].style.width = divBodyMovil.children[0].children[0].style.width;        
        oDivBodyFijo = $I("divBodyFijo");        
        oDivTituloMovil = $I("divTituloMovil");
        oDivBodyMovil = $I("divBodyMovil");

        if ($I("tblBodyFijo") != null) {
            scrollTablaRecursos();
            tblBodyMovil = $I("tblBodyMovil");
            tblTituloMovil = $I("tblTituloMovil");
            if (tblTituloMovil.rows.length!=0)
            {
                for (var i = 0; i < tblTituloMovil.rows[0].cells.length; i++) {
                    aMeses[aMeses.length] = parseInt(DescToAnoMes(tblTituloMovil.rows[0].cells[i].innerText), 10);
                }
            }
        }
        if ($I("tblBodyFijo").rows.length == 0) 
        {
            setOp($I("btnInsertarMes"), 30);
            setOp($I("btnExcel"), 30);
        } 
        
        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false)
    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function setScroll() {
    try {
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}
function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}
function setFilaFija(oFila) {
    try {
        ms(oFila);
        cObj(oFila);
        ms($I("tblBodyMovil").rows[oFila.rowIndex]);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila fija", e.message);
    }
}
function setFilaMovil(oFila) {
    try {
        ms($I("tblBodyFijo").rows[oFila.rowIndex]);
        cObj(oFila);
        ms(oFila);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila móvil", e.message);
    }
}

function cObj_old(oFila) {
    try {
        if ($I("tblBodyMovil").rows[oFila.rowIndex].cells[0].children[0] == null) {
            for (var x = 0; x < $I("tblBodyMovil").rows[oFila.rowIndex].cells.length; x++) {
                if ($I("tblBodyMovil").rows[oFila.rowIndex].getAttribute("tipo") == "1") {
                    var fbaja = $I("tblBodyMovil").rows[oFila.rowIndex].getAttribute("fbaja");
                    if (DescToAnoMes($I("tblTituloMovil").rows[0].cells[x].innerText) > fbaja && fbaja != '000000') {
                        $I("tblBodyMovil").rows[oFila.rowIndex].cells[x].style.backgroundColor = "DimGray";
                        //$I("idFuera").style.visibility = "visible";
                        continue;
                    }
                    var oCtrl = document.createElement("input");
                    oCtrl.type = "text";
                    oCtrl.style.width = "50px";
                    oCtrl.style.cursor = "pointer";
                    oCtrl.className = "txtNumL";
                    oCtrl.setAttribute("oValue", $I("tblBodyMovil").rows[oFila.rowIndex].cells[x].innerText);
                    oCtrl.value = $I("tblBodyMovil").rows[oFila.rowIndex].cells[x].innerText;
                    oCtrl.onkeyup = function() { fm_mn(this); activarGrabar(); };
                    oCtrl.onfocus = function() { fn(this, 3, 2) };
                    oCtrl.onchange = function() { actDis(this); };
                    $I("tblBodyMovil").rows[oFila.rowIndex].cells[x].innerText = "";
                    $I("tblBodyMovil").rows[oFila.rowIndex].cells[x].appendChild(oCtrl);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al crear los objetos en la fila", e.message);
    }
}

var oInput = document.createElement("input");
oInput.type = "text";
oInput.style.width = "50px";
oInput.style.cursor = "pointer";
oInput.className = "txtNumL";

function cObj(oFilaAux) {
    try {
        var oFila = tblBodyMovil.rows[oFilaAux.rowIndex];
        //if (oFila.cells[0].children[0] == null) {
        if (oFila.getAttribute("swctrl") == null) {
            oFila.setAttribute("swctrl", "1");
            var bContinue = false;
            for (var x = 0; x < oFila.cells.length; x++) {
                if (oFila.getAttribute("tipo") == "1") {
                    var fbaja = parseInt(oFila.getAttribute("fbaja"), 10);
                    var falta = parseInt(oFila.getAttribute("falta"), 10);

                    if (aMeses[x] < falta) {
                        oFila.cells[x].style.backgroundColor = "DimGray";
                        bContinue = true; // continue;
                    }
                    if (aMeses[x] > fbaja && fbaja != 0) {
                        oFila.cells[x].style.backgroundColor = "DimGray";
                        bContinue = true; // continue;
                    }

                    if (tblBodyMovil.rows[oFila.rowIndex + 2].cells[x].getAttribute("jl") == "0") {
                        tblBodyMovil.rows[oFila.rowIndex + 2].cells[x].style.backgroundColor = "#F58D8D";
                    }
                    
                    if (bContinue) continue;
                    
                    var oCtrl = oInput.cloneNode(true);
                    oCtrl.setAttribute("oValue", oFila.cells[x].innerText);
                    oCtrl.value = oFila.cells[x].innerText;
                    oCtrl.onkeyup = function() { fm_mn(this); activarGrabar(); };
                    oCtrl.onfocus = function() { fn(this, 3, 2) };
                    oCtrl.onchange = function() { actDis(this); };
                    oFila.cells[x].innerText = "";
                    oFila.cells[x].appendChild(oCtrl);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al crear los objetos en la fila", e.message);
    }
}

function actDis(obj) 
{
    try {
        var JornadasProy = dfn(obj.value);
        indice = obj.parentNode.parentNode.rowIndex;
        var JornadasOtrosProy = dfn($I("tblBodyMovil").rows[indice + 1].cells[obj.parentNode.cellIndex].innerText);
        var JLCalendario = dfn($I("tblBodyMovil").rows[indice + 2].cells[obj.parentNode.cellIndex].getAttribute("jl"));
        $I("tblBodyMovil").rows[indice + 2].cells[obj.parentNode.cellIndex].innerText = (parseFloat(JLCalendario) - parseFloat(JornadasProy) - parseFloat(JornadasOtrosProy)).ToString("N");
    } catch (e) {
        mostrarErrorAplicacion("Error al crear los objetos en la fila", e.message);
    }
}

function setBuscarDescriFija() {
    if (intFilaSeleccionada != -1) {
        ms($I("tblBodyMovil").rows[intFilaSeleccionada]);
        $I("divBodyMovil").scrollTop = $I("tblBodyMovil").rows[intFilaSeleccionada].offsetTop - 16;
        return;
    };

    var aFilaBus = FilasDe("tblBodyMovil");
    if (aFilaBus.length == 0) return;
    for (var i = 0; i < aFilaBus.length; i++) {
        if (aFilaBus[i].style.display == "none") continue;
        if (aFilaBus[i].className != "") aFilaBus[i].className = "";
    }
    intFilaSeleccionada = -1;
    $I("divBodyMovil").scrollTop = 0;
}

function salir() {
    var returnValue = null;
    if (bAlgunCambio) returnValue = "SI";

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function grabarsalir(){
    bSalir = true;
    grabar();
}

var bInsertarMes = false;
function insertarmes() {
    try {
        if (getOp($I("btnInsertarMes")) != 100) return;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bInsertarMes = true;
                    grabar();
                }
                else {
                      bCambios = false;
                      LLamarInsertarmes();
                }
            });
        } else LLamarInsertarmes();
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-1", e.message);
    }
}

function LLamarInsertarmes() {
    try {
        bInsertarMes = false;

        var nMesACrear = getPMACrear();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getPeriodo.aspx?sDesde=" + nMesACrear + "&sHasta=" + nMesACrear;
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    var js_args = "addMesesProy@#@";
                    js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                    js_args += aDatos[0] + "@#@";
                    js_args += aDatos[1];
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-2", e.message);
    }
}
function obtener() {
    try {
        var js_args = "obtener@#@";
        js_args += $I("hdnIdProyectoSubNodo").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes", e.message);
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
            case "obtener":
                $I("divTituloMovil").innerHTML = aResul[2];
                $I("divBodyMovil").children[0].innerHTML = aResul[3];
                $I("divBodyFijo").children[0].innerHTML = aResul[4];
                var iWidth = parseInt($I("divTituloMovil").children[0].style.width.substring(0, $I("divTituloMovil").children[0].style.width.length - 2), 10)

                if (iWidth <= 540) {
                    $I("divTituloMovil").style.width = $I("divTituloMovil").children[0].style.width;
                    $I("divBodyMovil").style.width = (parseInt($I("divBodyMovil").children[0].children[0].style.width.substring(0, $I("divBodyMovil").children[0].children[0].style.width.length - 2), 10) + 16) + "px"
                }
                else {
                    $I("divTituloMovil").style.width = "540px";
                    $I("divBodyMovil").style.width = "556px";
                }

                $I("divBodyMovil").children[0].style.width = $I("divBodyMovil").children[0].children[0].style.width;                
 
                oDivBodyFijo = $I("divBodyFijo");
                oDivTituloMovil = $I("divTituloMovil");
                oDivBodyMovil = $I("divBodyMovil");


                oDivBodyFijo.scrollTop = 0;
                oDivBodyMovil.scrollTop = 0;
                oDivTituloMovil.scrollLeft = 0;
                oDivBodyMovil.scrollLeft = 0; ;
                //setExcelImg("imgExcel", "divBodyMovil");
                scrollTablaRecursos();
                tblBodyMovil = $I("tblBodyMovil");
                tblTituloMovil = $I("tblTituloMovil");
                aMeses.length = 0;
                if (tblTituloMovil.rows.length > 0) {
                    for (var i = 0; i < tblTituloMovil.rows[0].cells.length; i++) {
                        aMes[aMes.length] = parseInt(DescToAnoMes(tblTituloMovil.rows[0].cells[i].innerText), 10);
                    }
                }                
                break;

            case "addMesesProy":
                bAlgunCambio = true;
                setTimeout("obtener();", 50);
                break;

            case "grabar":
                desActivarGrabar();
                bAlgunCambio = true;
                if (bSalir) setTimeout("salir();", 50);

                var tblBodyFijo = $I("tblBodyFijo");
                for (var x = 0; x < tblBodyFijo.rows.length; x++) {
                    tblBodyFijo.rows[x].setAttribute("bd", "");
                }
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) setTimeout("salir();", 50);
                else {
                    if (bInsertarMes)
                        setTimeout("LLamarInsertarmes();", 50);
                }
                
                break;                
                              
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
                
        }
        ocultarProcesando();
    }
}
function activarGrabar(){
    try {
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function desActivarGrabar() {
    try {
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
    }
}

function grabar(){
    try{
        var sb = new StringBuilder;
        var tblBodyMovil = $I("tblBodyMovil");
        for (var i = 0; i < tblBodyMovil.rows.length; i++) {
            if (tblBodyMovil.rows[i].getAttribute("bd") == "U") {
                for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
                    if (tblBodyMovil.rows[i].cells[x].children.length == 0) continue;
                    if (tblBodyMovil.rows[i].cells[x].children[0].value != tblBodyMovil.rows[i].cells[x].children[0].getAttribute("oValue")) 
                    {
                        sb.Append("I##"); //Opcion BD. "I", "U", "D"
                        sb.Append(DescToAnoMes(tblTituloMovil.rows[0].cells[x].innerText) + "##"); //AnnoMes
                        sb.Append(tblBodyMovil.rows[i].getAttribute("usu") + "##"); //Usuario
                        sb.Append(tblBodyMovil.rows[i].getAttribute("coste") + "##"); //Coste
                        if (tblBodyMovil.rows[i].cells[x].children[0].value == "") sb.Append("0,00##"); //Unidades                      
                        else sb.Append(tblBodyMovil.rows[i].cells[x].children[0].value + "##"); //Unidades
                        sb.Append(tblBodyMovil.rows[i].getAttribute("costerep") + "##"); //CosteRep
                        sb.Append(tblBodyMovil.rows[i].getAttribute("empnodo") + "##"); //idempresa_nodomes
                        sb.Append(tblBodyMovil.rows[i].getAttribute("nodousumes") + "///"); //Nodo                        
                    }
                }
            }
        }


        mostrarProcesando();
        RealizarCallBack("grabar@#@" + sb.ToString() + "@#@", "");  
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
	}
}

function excel() {
    try {
        var tblBodyMovil = $I("tblBodyMovil");

        if (tblBodyMovil == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Profesional</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'></td>");
        var tblTituloMovil = $I("tblTituloMovil");
        
        for (var x = 0; x < tblTituloMovil.rows[0].cells.length; x++) {
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>" + tblTituloMovil.rows[0].cells[x].innerText + "</td>");        
        }

        sb.Append("	</TR>");

//        var tblBodyFijo = $I("tblBodyFijo");
//        var tblBodyMovil = $I("tblBodyMovil");
//        
        for (var i = 0; i < tblBodyFijo.rows.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append(tblBodyFijo.rows[i].cells[1].innerText);
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append(tblBodyFijo.rows[i].cells[2].innerText);
            sb.Append("</td>");

            //genero las columnas
            
            for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
                sb.Append("<td style='text-align:right'>");
               if (tblBodyMovil.rows[i].cells[x].children[0]!=null) 
                    sb.Append(tblBodyMovil.rows[i].cells[x].children[0].value);
                else sb.Append(tblBodyMovil.rows[i].cells[x].innerText);
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaRecursos() {
    try {
        if ($I("divBodyMovil").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divBodyMovil").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaRecursos()", 50);
            return;
        }

        var tblBodyFijo = $I("tblBodyFijo");
        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divBodyMovil").offsetHeight / 20 + 1, tblBodyFijo.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblBodyFijo.rows[i].getAttribute("sw")) {
                oFila = tblBodyFijo.rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("caso")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                    }
                } else {
                     switch (oFila.getAttribute("caso")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                    }
                }      
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function getAuditoriaAux() {
    try {
        getAuditoria(5, $I("hdnIdProyectoSubNodo").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}
/* Obtener el añomes del primer mes que no exista, que sea posterior al último mes económico cerrado del nodo */

function getPMACrear() {
    try {
        var oFecha = new Date();
        var nAnnoMesActual = oFecha.getFullYear() * 100 + oFecha.getMonth() + 1;
        var nMesInsertarAux = FechaAAnnomes(AnnomesAFecha(parseInt(sUltCierreEcoNodo, 10)).add("mo", 1));

        nMesInsertarAux = (nAnnoMesActual > nMesInsertarAux) ? nAnnoMesActual : nMesInsertarAux;
        var tblBodyMovil = $I("tblBodyMovil");

        for (var x = 0; x < tblTituloMovil.rows[0].cells.length; x++) {
            if (nMesInsertarAux < parseInt(DescToAnoMes(tblTituloMovil.rows[0].cells[x].innerText), 10)) {
                break;
            }
            if (nMesInsertarAux > parseInt(DescToAnoMes(tblTituloMovil.rows[0].cells[x].innerText), 10)) {
                continue;
            }
            if (parseInt(DescToAnoMes(tblTituloMovil.rows[0].cells[x].innerText), 10) >= nMesInsertarAux) {
                nMesInsertarAux = FechaAAnnomes(AnnomesAFecha(nMesInsertarAux).add("mo", 1));
            }            
        }
        return nMesInsertarAux;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el mes a insertar.", e.message);
    }
}