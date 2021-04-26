<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCGusano.ascx.cs" Inherits="Capa_Presentacion_UserControls_UCGusano" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<div id="divPestGusano" style="position:absolute; left:-230px; top:125px; width:160px; height:30px; z-index:1;">
    <table style="width:250px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
    <colgroup>
        <col style="width:230px;" />
        <col style="width:20px;" />
    </colgroup>
    <tr style="vertical-align:top;">
        <td>
            <table class="texto" style="width:230px; height:30px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td background="<%=Session["strServer"] %>Images/Tabla/8.gif" height="6">
                    </td>
                    <td background="<%=Session["strServer"] %>Images/Tabla/9.gif" height="6" width="6">
                    </td>
                </tr>
                <tr>
                    <td background="<%=Session["strServer"] %>Images/Tabla/5.gif" style="padding:5px; vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblGusano" class="texto" style="width:215px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width:20px;" />
                            <col style="width:35px;" />
                            <col style="width:190px;" />
                        </colgroup>
                        <tr style="height: 30px;">
                            <td colspan="3" class="texto" style="padding-left:60px; vertical-align:middle;"><b><u>Accesos directos</u></b></td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="<%=Session["strServer"] %>Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
                    <td background="<%=Session["strServer"] %>Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="<%=Session["strServer"] %>Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
        <td><img src="<%=Session["strServer"] %>Images/imgPestGusano.gif" style="margin-top:5px;cursor:pointer;" onclick="mostrarOcultarPestHorizontal()" /></td>
    </tr>
    </table>
</div>
<script type="text/javascript">
var bPGEAccesible = <%= (Utilidades.EsModuloAccesible("PGE"))? "true":"false" %>;
var bPSTAccesible = <%= (Utilidades.EsModuloAccesible("PST"))? "true":"false" %>;
var bIAPAccesible = <%= (Utilidades.EsModuloAccesible("IAP"))? "true":"false" %>;

/* Valores necesarios para la pestaña retractil */
var oPestGusano_width = "250px";
var oPestGusano_visible = "20px";
var nIntervaloPXGusano = 20;
var nLimiteDerGusano = 0;
var nLimiteIzqGusano=(parseInt(oPestGusano_width, 10)-parseInt(oPestGusano_visible, 10))*-1;
var idMostrarGusano = null;
var idOcultarGusano = null;
/* Fin de Valores necesarios para la pestaña retractil */

var bPestRetrMostrada = false;
function mostrarOcultarPestHorizontal(){
    if (!bPestRetrMostrada) pull();
    else draw();
    bPestRetrMostrada = !bPestRetrMostrada;
}

function pull(){
	if (window.idOcultarGusano)
	    clearInterval(idOcultarGusano);
	idMostrarGusano=setInterval("pullengine()",1);
}
function draw(){
	if (window.idMostrarGusano)
	    clearInterval(idMostrarGusano);
	idOcultarGusano=setInterval("drawengine()",1);
}
function pullengine(){
	if (divPestGusano.offsetLeft<nLimiteDerGusano)
		divPestGusano.style.left=divPestGusano.offsetLeft+nIntervaloPXGusano+"px";
	else if (window.idMostrarGusano){
		divPestGusano.style.left=0+"px";
		clearInterval(idMostrarGusano);
	}
}
function drawengine(){
	if (divPestGusano.offsetLeft>nLimiteIzqGusano)
		divPestGusano.style.left=divPestGusano.offsetLeft-nIntervaloPXGusano+"px";
	else if (window.idOcultarGusano){
		divPestGusano.style.left=nLimiteIzqGusano+"px";
		clearInterval(idOcultarGusano);
	}
}

function setOpcionGusano(sOpciones) {
    try {
        var js_ar = new Array();
        js_ar[0] = { "modulo": "PGE", "pantalla": "Detalle de proyecto", "imagen": "imgIconoProyAzul.gif", "url": "Capa_Presentacion/ECO/Proyecto/default.aspx?sOp=datos" };
        js_ar[1] = { "modulo": "PGE", "pantalla": "Detalle económico (Carrusel)", "imagen": "botones/imgCarrusel.gif", "url": "Capa_Presentacion/ECO/SegEco/default.aspx" };
        js_ar[2] = { "modulo": "PGE", "pantalla": "Resumen económico", "imagen": "botones/imgResumenEco.gif", "url": "Capa_Presentacion/ECO/ResumenEcoProy/default.aspx" };
        js_ar[3] = { "modulo": "PST", "pantalla": "Estructura completa", "imagen": "imgEstructuraCompleta.gif", "url": "Capa_Presentacion/PSP/Proyecto/Desglose/default.aspx" };
        js_ar[4] = { "modulo": "IAP", "pantalla": "Reporte calendario", "imagen": "imgReporteCalendario.gif", "url": "Capa_Presentacion/IAP30/Reporte/Calendario/default.aspx?or=bWVudQ==" };
        js_ar[5] = { "modulo": "PGE", "pantalla": "Cierre global", "imagen": "imgCierreGlobal.gif", "url": "Capa_Presentacion/ECO/Replica/Default.aspx?origen=menucierre" };
        js_ar[6] = { "modulo": "PST", "pantalla": "Avance técnico", "imagen": "imgProduccionAvance.gif", "url": "" };
        js_ar[7] = { "modulo": "PGE", "pantalla": "Órdenes de facturación", "imagen": "imgOrdenT.gif", "url": "Capa_Presentacion/ECO/OrdenFacturacion/Catalogo/Default.aspx" };
        js_ar[8] = { "modulo": "PGE", "pantalla": "Control de facturación", "imagen": "imgRemesaOF.gif", "url": "Capa_Presentacion/ECO/OrdenFacturacion/ControlOrdenFact/Default.aspx" };
        js_ar[9] = { "modulo": "PGE", "pantalla": "Espacio de comunicación", "imagen": "botones/imgComunicacion.gif", "url": "Capa_Presentacion/ECO/EspacioComunicacion/Default.aspx" };
        js_ar[10] = { "modulo": "PGE", "pantalla": "Agenda USA", "imagen": "botones/imgAgendaUSA.gif", "url": "Capa_Presentacion/ECO/AgendaUSA/Default.aspx" };
        js_ar[11] = { "modulo": "PGE", "pantalla": "Proyectos USA", "imagen": "botones/imgProyUSA.gif", "url": "Capa_Presentacion/ECO/ProyectosUSA/Default.aspx" };
        js_ar[12] = { "modulo": "PGE", "pantalla": "Valor ganado", "imagen": "botones/imgValorGanado.gif", "url": "Capa_Presentacion/ECO/ValorGanado/Default.aspx" };
        js_ar[13] = { "modulo": "PGE", "pantalla": "Proyectos no cerrados", "imagen": "icoAbierto.gif", "url": "Capa_Presentacion/ECO/ProyNoCerrados/Default.aspx" };

        var aOp = sOpciones.split(",");
        for (var x = 0; x < aOp.length; x++) {
            for (var i = 0; i < js_ar.length; i++) {
                if (parseInt(aOp[x], 10) == i) {
                    var oNF = $I("tblGusano").insertRow(-1);
                    oNF.style.height = "20px";
                    oNF.setAttribute("style", "height:20px");
                    var oImg = document.createElement("img");
                    oImg.setAttribute("src", strServer + "Images/" + js_ar[i].imagen);
                    oNF.insertCell(-1).appendChild(oImg);
                    setOp(oImg, 30);

                    //oNF.insertCell(-1).innerText = "(" + js_ar[i].modulo + ")";
                    var oModulo = document.createElement('label');
                    oModulo.innerText = "(" + js_ar[i].modulo + ")";
                    setOp(oModulo, 30);
                    if ((js_ar[i].modulo == "PGE" && bPGEAccesible && !bBloquearPGEByAcciones)
                            || (js_ar[i].modulo == "PST" && bPSTAccesible && !bBloquearPSTByAcciones)
                            || (js_ar[i].modulo == "IAP" && bIAPAccesible && !bBloquearIAPByAcciones)
                            ) {
                        setOp(oImg, 100);
                        setOp(oModulo, 100);
                    }
                    oNF.insertCell(-1).appendChild(oModulo);


                    //bAccesoModulo(js_ar[i].modulo);
                    var newlink;
                    if (bAccesoModulo(js_ar[i].modulo, i)) {
                        switch (i) {
                            case 2:
                                newlink = document.createElement('a');
                                newlink.className = "texto";
                                newlink.setAttribute('href', '#');
                                newlink.setAttribute("destino", strServer + js_ar[i].url);
                                newlink.onclick = function() { if (typeof (goToResumenEco) == 'function') { goToResumenEco() } else { location.href = this.getAttribute("destino"); } };
                                newlink.onmouseover = function() { this.className = 'enlace'; }
                                newlink.onmouseout = function() { this.className = 'texto'; }
                                oNF.insertCell(-1).appendChild(newlink);
                                break;
                            case 6:
                                newlink = document.createElement('a');
                                newlink.className = "texto";
                                newlink.setAttribute('href', '#');
                                newlink.onclick = function() { if (typeof (mdat) == 'function') { mostrarOcultarPestHorizontal(); mdat() } else { mmoff('Opción no implementada',190); } };
                                newlink.onmouseover = function() { this.className = 'enlace'; }
                                newlink.onmouseout = function() { this.className = 'texto'; }

                                oNF.insertCell(-1).appendChild(newlink);
                                break;
                            case 9:
                                newlink = document.createElement('a');
                                newlink.className = "texto";
                                newlink.setAttribute('href', '#');
                                newlink.onclick = function() { if (typeof (accesoEspaComu) == 'function') { mostrarOcultarPestHorizontal(); accesoEspaComu() } else {  mmoff('Opción no implementada',190); } };
                                newlink.onmouseover = function() { this.className = 'enlace'; }
                                newlink.onmouseout = function() { this.className = 'texto'; }

                                oNF.insertCell(-1).appendChild(newlink);
                                break;
                            case 10:
                                newlink = document.createElement('a');
                                newlink.className = "texto";
                                newlink.setAttribute('href', '#');
                                newlink.onclick = function() { if (typeof (accesoAgendaUSA) == 'function') { mostrarOcultarPestHorizontal(); accesoAgendaUSA() } else {  mmoff('Opción no implementada',190); } };
                                newlink.onmouseover = function() { this.className = 'enlace'; }
                                newlink.onmouseout = function() { this.className = 'texto'; }

                                oNF.insertCell(-1).appendChild(newlink);
                                break;
                            case 11:
                                newlink = document.createElement('a');
                                newlink.className = "texto";
                                newlink.setAttribute('href', '#');
                                newlink.onclick = function() { if (typeof (accesoProyectosUSA) == 'function') { mostrarOcultarPestHorizontal(); accesoProyectosUSA() } else {  mmoff('Opción no implementada',190); } };
                                newlink.onmouseover = function() { this.className = 'enlace'; }
                                newlink.onmouseout = function() { this.className = 'texto'; }

                                oNF.insertCell(-1).appendChild(newlink);
                                break;
                            default:
                                newlink = document.createElement('a');
                                newlink.className = "texto";
                                newlink.setAttribute("destino", strServer + js_ar[i].url);
                                newlink.onclick = function() { try{location.href = this.getAttribute("destino");}catch(e){}};
                                newlink.onmouseover = function() { this.className = 'enlace'; }
                                newlink.onmouseout = function() { this.className = 'texto'; }

                                oNF.insertCell(-1).appendChild(newlink);
                                break;
                        }
                    } else {
                        newlink = document.createElement('label');
                        newlink.className = "texto";
                        setOp(newlink, 30);
                        newlink.onclick = function() { if (typeof (mmoff) == 'function') { mmoff("War", "Acceso no permitido", 200); } else { alert('Acceso no permitido'); } };
                        oNF.insertCell(-1).appendChild(newlink);
                    }

                    oNF.cells[2].children[0].innerText = js_ar[i].pantalla;
                    break;
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al crear el acceso rápido", e.message);
    }
}

var sRolesUsuario = "<%=sRoles %>";
var aRolesUsuario = sRolesUsuario.split(",");
function bAccesoModulo(sModulo, nOpcion){
    try{
        var aRolesPermitidos;
        //alert(sModulo + " / " +sRolesUsuario);
        switch (sModulo){
            case "PGE":
                //Roles indicados en PGE -> Proyectos del web.sitemap
                switch (nOpcion){
                    case 9:
                        aRolesPermitidos = new Array("A","RP","DP","CP","USA");
                        break;
                    case 10:
                        aRolesPermitidos = new Array("A","USA");
                        break;
                    case 12:
                        aRolesPermitidos = new Array("A", "OT", "RSN4", "DSN4", "CSN4", "ISN4", "GSN4", "RSN3", "DSN3", "CSN3", "ISN3", "GSN3", "RSN2", "DSN2", "CSN2", "ISN2", "GSN2", "RSN1", "DSN1", "CSN1", "ISN1", "GSN1", "RN", "DN", "CN", "IN", "GN", "RSB", "DSB", "ISB", "GSB", "RC", "DC", "IC", "RL", "DL", "IL", "RH", "DH", "IH", "RQN", "DQN", "IQN", "RQ1", "DQ1", "IQ1", "RQ2", "DQ2", "IQ2", "RQ3", "DQ3", "IQ3", "RQ4", "DQ4", "IQ4", "RP", "DP", "CP", "IP", "JP", "MP", "USA");
                        break;
                    default:
                        aRolesPermitidos = new Array("A","RSN4","DSN4","CSN4","ISN4","GSN4","RSN3","DSN3","CSN3","ISN3","GSN3","RSN2","DSN2","CSN2","ISN2","GSN2","RSN1","DSN1","CSN1","ISN1","GSN1","RN","DN","CN","IN","GN","RSB","DSB","ISB","GSB","RC","DC","IC","RL","DL","IL","RH","DH","IH","RQN","DQN","IQN","RQ1","DQ1","IQ1","RQ2","DQ2","IQ2","RQ3","DQ3","IQ3","RQ4","DQ4","IQ4","RP","DP","CP","IP","USA");
                        break;
                }
                break;
            case "PST":
                //Roles indicados en PST -> Gestión del web.sitemap
                aRolesPermitidos = new Array("A","OT","RSN4","DSN4","CSN4","ISN4","RSN3","DSN3","CSN3","ISN3","RSN2","DSN2","CSN2","ISN2","RSN1","DSN1","CSN1","ISN1","RN","DN","CN","IN","RSB","DSB","ISB","RC","DC","IC","RL","DL","IL","RH","DH","IH","RQN","DQN","IQN","RQ1","DQ1","IQ1","RQ2","DQ2","IQ2","RQ3","DQ3","IQ3","RQ4","DQ4","IQ4","RP","DP","CP","IP","JP","MP","K","BP");
                break;
            case "IAP":
                return true;
                break;
        }
        for (var z=0; z<aRolesUsuario.length; z++){
            for (var n=0; n<aRolesPermitidos.length; n++){
                if (aRolesUsuario[z] == aRolesPermitidos[n]){
                    //return true;
                    if ( (sModulo == "PGE" && bPGEAccesible && !bBloquearPGEByAcciones)
                            || (sModulo == "PST" && bPSTAccesible && !bBloquearPSTByAcciones)
                            || (sModulo == "IAP" && bIAPAccesible && !bBloquearIAPByAcciones)
                            ){
                        return true;
                    }
                }
            }
        }
        return false;
    }catch(e){
        mostrarErrorAplicacion("Error al obtener el acceso al módulo del acceso rápido", e.message);
    }
}

</script>