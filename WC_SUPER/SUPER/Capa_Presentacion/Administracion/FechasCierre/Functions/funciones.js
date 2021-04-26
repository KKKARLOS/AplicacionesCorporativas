var sCambiarAno="";
var sAux="";
var nAnnoActual = new Date().getFullYear();
var aFila = null;

var oFec = document.createElement("input");
    oFec.setAttribute("type", "text");
    oFec.className = "txtM";
    oFec.setAttribute("style", "width:60px; vertical-align:middle; cursor:pointer;");
    oFec.setAttribute("goma", "1");
    oFec.value = "";
    //oFec.setAttribute("value_original", "");
    oFec.setAttribute("Calendar", "oCal");
var oCombo = document.createElement("select");
    oCombo.className = "combo";
    oCombo.setAttribute("style", "width:60px; vertical-align:middle; margin-left:3px;");

function init() {
    try {
        //A petición de Yolanda el 07/01/2015, que se pueda modificar el año anterior.
        //setOp($I("btnAnnoAnt"), 30);
        
        for (var i = 1; i < 24; i++) {
            oCombo.options[oCombo.options.length] = new Option(i.ToString() + ":00", i.ToString() + ":00");
            oCombo.options[oCombo.options.length] = new Option(i.ToString() + ":30", i.ToString() + ":30");
            if (i == 18) oCombo.setAttribute("selected", "true");
        }
        
        getMesesCierre();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }     	       
}

function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "U") {
                        mfa(aFila[i], "N", false);
                    }
                }
                desActivarGrabar();
                bCambios = false;
                mmoff("Suc", "Grabación correcta", 160);
                if (sCambiarAno != "") {
                    sAux = sCambiarAno;
                    setTimeout("setAnno('" + sAux + "')", 20);
                    sCambiarAno = "";
                }
                break;
            case "obtenermeses":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                setControlesTabla();
                mmoff("hide");
                break;                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
    ocultarProcesando();
}

function setControlesTabla() {
    try {
        var aFila = FilasDe("tblDatos");
        if (aFila != null) {
            for (var i = 0; i < aFila.length; i++) {
                aFila[i].cells[0].style.borderRight = '';
                aFila[i].cells[1].style.borderLeft = '';
                /* Fecha límite para crear órdenes de facturación  */
                if (aFila[i].cells[2].children.length == 0) {
                    var oFecLimFac = oFec.cloneNode(true);
                    oFecLimFac.setAttribute("goma", "0");
                    oFecLimFac.onchange = function() { fm_mn(this); };
                    oFecLimFac.id = "txtLOF_" + aFila[i].id;

                    if (btnCal == "I") {
                        oFecLimFac.onclick = function() { mc(this); };
                        oFecLimFac.setAttribute("readonly", "readonly");
                    }
                    else {
                        oFecLimFac.onmousedown = function() { mc1(this) };
                        oFecLimFac.attachEvent("onfocus", focoFecha);
                    }
                    aFila[i].cells[2].appendChild(oFecLimFac);

                    var oComboLimFac = oCombo.cloneNode(true);
                    oComboLimFac.onchange = function() { fm_mn(this); };
                    aFila[i].cells[2].appendChild(oComboLimFac);
                }
                aFila[i].cells[2].children[0].value = (aFila[i].getAttribute("flimiteof") != "") ? aFila[i].getAttribute("flimiteof") : "";
                aFila[i].cells[2].children[1].value = (aFila[i].getAttribute("hlimiteof") != "") ? aFila[i].getAttribute("hlimiteof") : "18:00";

                /* Fecha límite de respuesta para las alertas */
                if (aFila[i].cells[3].children.length == 0) {
                    var oFecLimAler = oFec.cloneNode(true);
                    oFecLimAler.onchange = function() { fm_mn(this); };
                    oFecLimAler.id = "txtLAL_" + aFila[i].id;

                    if (btnCal == "I") {
                        oFecLimAler.onclick = function() { mc(this); };
                        oFecLimAler.setAttribute("readonly", "readonly");
                    }
                    else {
                        oFecLimAler.onmousedown = function() { mc1(this) };
                        oFecLimAler.attachEvent("onfocus", focoFecha);
                    }
                    aFila[i].cells[3].appendChild(oFecLimAler);
                }
                aFila[i].cells[3].children[0].value = (aFila[i].getAttribute("flimiterespalertas") != "") ? aFila[i].getAttribute("flimiterespalertas") : "";
                
                /* Previsión cierre económico de empresa */
                if (aFila[i].cells[4].children.length == 0) {
                    var oFecCierreECO = oFec.cloneNode(true);
                    oFecCierreECO.onchange = function() { fm_mn(this); };
                    oFecCierreECO.id = "txtLECO_" + aFila[i].id;

                    if (btnCal == "I") {
                        oFecCierreECO.onclick = function() { mc(this); };
                        oFecCierreECO.setAttribute("readonly", "readonly");
                    }
                    else {
                        oFecCierreECO.onmousedown = function() { mc1(this) };
                        oFecCierreECO.attachEvent("onfocus", focoFecha);
                    }
                    aFila[i].cells[4].appendChild(oFecCierreECO);
                }
                aFila[i].cells[4].children[0].value = (aFila[i].getAttribute("fprevcierreeco") != "") ? aFila[i].getAttribute("fprevcierreeco") : "";
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function grabar(){
    try{
        //if (!comprobarDatos()) return;

        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        aFila = FilasDe("tblDatos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") == "U") {
                sb.Append(aFila[i].id + "{sep}");
                sb.Append(((aFila[i].cells[2].children[0].value != "") ? aFila[i].cells[2].children[0].value + ' ' + aFila[i].cells[2].children[1].options[aFila[i].cells[2].children[1].selectedIndex].innerText : "") + "{sep}");
                sb.Append(aFila[i].cells[3].children[0].value + "{sep}");
                sb.Append(aFila[i].cells[4].children[0].value + "{sepreg}");
            }
        }
        
        RealizarCallBack(sb.ToString(), ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos.", e.message);
    }
}

function setAnno(sOpcion){
    try {
        if (sOpcion == "A" && getOp($I("btnAnnoAnt")) != 100) return;
        if (sOpcion == "S" && getOp($I("btnAnnoSig")) != 100) return;
    
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    sCambiarAno = sOpcion;
                    grabar();
                }
                else {
                    bCambios = false;
                    setAnnoContinuar(sOpcion);
                }
            });
        }
        else setAnnoContinuar(sOpcion);

       
    }catch(e){
        mostrarErrorAplicacion("Error en la función setAnno", e.message);
    }
}
function setAnnoContinuar(sOpcion) {
    try {
        if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
        else if (sOpcion == "S") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;

        setOp($I("btnAnnoAnt"), 100);
        setOp($I("btnAnnoSig"), 100);
        //A petición de Yolanda el 07/01/2015, que se pueda modificar el año anterior.
        //if (parseInt($I("txtAnno").value, 10) <= nAnnoActual) setOp($I("btnAnnoAnt"), 30);
        if (parseInt($I("txtAnno").value, 10) < nAnnoActual) setOp($I("btnAnnoAnt"), 30);
        if (parseInt($I("txtAnno").value, 10) > nAnnoActual + 18) setOp($I("btnAnnoSig"), 30);


        getMesesCierre();
    } catch (e) {
        mostrarErrorAplicacion("Error en setAnnoContinuar", e.message);
    }
}
function getMesesCierre(){
    try{
        mmoff("infper", "Obteniendo los meses de cierre...", 250);
        var js_args = "obtenermeses@#@" + $I("txtAnno").value;       
        RealizarCallBack(js_args, ""); 
    }catch(e){
        mostrarErrorAplicacion("Error al ir a obtener los meses de cierre.", e.message);
    }
}    