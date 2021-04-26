<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCGusano.ascx.cs" Inherits="Capa_Presentacion_UserControls_UCGusano" %>
<div id="divPestGusano" style="position:absolute; left:-230px; top:125px; width:160px; height:30px;">
    <table style="width:250px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
    <colgroup>
        <col style="width:230px;" />
        <col style="width:20px;" />
    </colgroup>
    <tr style="vertical-align: text-top;">
        <td>
            <table style="width:230px; height:30px;">
                <tr>
                    <td background="<%=Session["GVT_strServer"] %>Images/Tabla/8.gif" height="6">
                    </td>
                    <td background="<%=Session["GVT_strServer"] %>Images/Tabla/9.gif" height="6" width="6">
                    </td>
                </tr>
                <tr>
                    <td background="<%=Session["GVT_strServer"] %>Images/Tabla/5.gif" style="padding: 5px;vertical-align: text-top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblGusano" style="width:215px; table-layout:fixed;">
                        <colgroup>
                            <col style="width:20px;" />
                            <col style="width:35px; text-align:center;" />
                            <col style="width:190px;" />
                        </colgroup>
                        <tr style="height: 30px;">
                            <td colspan="3" style="padding-left:60px; vertical-align:middle"><b><u>Accesos directos</u></b></td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="<%=Session["GVT_strServer"] %>Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
                    <td background="<%=Session["GVT_strServer"] %>Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="<%=Session["GVT_strServer"] %>Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
        <td><img src="<%=Session["GVT_strServer"] %>Images/imgPestGusano.gif" style="margin-top:5px;cursor:pointer;" onclick="mostrarOcultarPestHorizontal()" /></td>
    </tr>
    </table>
</div>
<script language=Javascript>
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
	idMostrarGusano=setInterval("pullengine()",3);
}
function draw(){
	if (window.idMostrarGusano)
	    clearInterval(idMostrarGusano);
	idOcultarGusano=setInterval("drawengine()",3);
}
function pullengine(){
	if (divPestGusano.offsetLeft<nLimiteDerGusano)
		divPestGusano.style.left=divPestGusano.offsetLeft+nIntervaloPXGusano+"px";
	else if (window.idMostrarGusano){
		divPestGusano.style.left=0;
		clearInterval(idMostrarGusano);
	}
}
function drawengine(){
	if (divPestGusano.offsetLeft>nLimiteIzqGusano)
		divPestGusano.style.left=divPestGusano.offsetLeft-nIntervaloPXGusano+"px";
	else if (window.idOcultarGusano){
		divPestGusano.style.left=nLimiteIzqGusano;
		clearInterval(idOcultarGusano);
	}
}

function setOpcionGusano(sOpciones){
    try{
        var js_ar = new Array();
        js_ar[0] = {"modulo":"PGE","pantalla":"Detalle de proyecto","imagen":"imgIconoProyAzul.gif","url":"Capa_Presentacion/ECO/Proyecto/default.aspx?sOp=datos"};
        js_ar[1] = {"modulo":"PGE","pantalla":"Detalle económico (Carrusel)","imagen":"botones/imgCarrusel.gif","url":"Capa_Presentacion/ECO/SegEco/default.aspx"};
        js_ar[2] = {"modulo":"PGE","pantalla":"Resumen económico","imagen":"botones/imgResumenEco.gif","url":"Capa_Presentacion/ECO/ResumenEcoProy/default.aspx"};
        js_ar[3] = {"modulo":"PST","pantalla":"Estructura completa","imagen":"imgEstructuraCompleta.gif","url":"Capa_Presentacion/PSP/Proyecto/Desglose/default.aspx"};
        js_ar[4] = {"modulo":"IAP","pantalla":"Reporte calendario","imagen":"imgReporteCalendario.gif","url":"Capa_Presentacion/IAP/Calendario/default.aspx?or=bWVudQ=="};
        js_ar[5] = {"modulo":"PGE","pantalla":"Cierre global","imagen":"imgCierreGlobal.gif","url":"Capa_Presentacion/ECO/Replica/Default.aspx?origen=menucierre"};
        js_ar[6] = {"modulo":"PST","pantalla":"Avance técnico","imagen":"imgProduccionAvance.gif","url":""};
        js_ar[7] = {"modulo":"PGE","pantalla":"Órdenes de facturación","imagen":"imgOrdenT.gif","url":"Capa_Presentacion/ECO/OrdenFacturacion/Catalogo/Default.aspx"};

        var aOp = sOpciones.split(",");
        for (var x=0; x<aOp.length; x++){
            for (var i=0; i<js_ar.length; i++){
                if (parseInt(aOp[x],10)==i){
                    var oNF = tblGusano.insertRow(-1);
                    oNF.style.height = "20px";
//                    oNF.insertCell(-1).appendChild(document.createElement("<img src='"+ strServer + "Images/"+ js_ar[i].imagen +"' />")); 
                    var oImg = document.createElement("img");
                    oImg.setAttribute("src", strServer + "Images/"+ js_ar[i].imagen);
                    oNF.insertCell(-1).appendChild(oImg.cloneNode(true), null);
                    oNF.insertCell(-1).innerText = "("+ js_ar[i].modulo +")"; 
                    //bAccesoModulo(js_ar[i].modulo);
                    if (bAccesoModulo(js_ar[i].modulo)){
                        switch (i){
                            case 2: oNF.insertCell(-1).appendChild(document.createElement("<a href='#' onclick=\"if (typeof(goToResumenEco)=='function'){goToResumenEco()}else{location.href='"+ strServer + js_ar[i].url +"'}\" class='texto' onmouseover=\"this.className='enlace'\" onmouseout=\"this.className='texto'\"></a>")); break;
                            case 6: oNF.insertCell(-1).appendChild(document.createElement("<a href='#' onclick=\"if (typeof(mdat)=='function'){mostrarOcultarPestHorizontal();mdat()}else{alert('Opción no implementada');}\" class='texto' onmouseover=\"this.className='enlace'\" onmouseout=\"this.className='texto'\"></a>")); break;
                            default: oNF.insertCell(-1).appendChild(document.createElement("<a href='"+ strServer + js_ar[i].url +"' class='texto' onmouseover=\"this.className='enlace'\" onmouseout=\"this.className='texto'\"></a>")); break;
                        }
//                        if (i==2){//Resumen económico
//                            oNF.insertCell(-1).appendChild(document.createElement("<a href='#' onclick=\"if (typeof(goToResumenEco)=='function'){goToResumenEco()}else{location.href='"+ strServer + js_ar[i].url +"'}\" class='texto' onmouseover=\"this.className='enlace'\" onmouseout=\"this.className='texto'\"></a>")); 
//                        }else{
//                            oNF.insertCell(-1).appendChild(document.createElement("<a href='"+ strServer + js_ar[i].url +"' class='texto' onmouseover=\"this.className='enlace'\" onmouseout=\"this.className='texto'\"></a>")); 
//                        }
                    }else oNF.insertCell(-1).appendChild(document.createElement("<label class='texto' onclick=\"if (typeof(mmoff)=='function'){mmoff('Acceso no permitido',200)}else{alert('Acceso no permitido')}\"></label>")); 
                    oNF.cells[2].children[0].innerText = js_ar[i].pantalla;
                    break;
                }
            }
        }
    }catch(e){
        mostrarErrorAplicacion("Error al crear el acceso rápido", e.message);
    }
}

var sRolesUsuario = "<%=sRoles %>";
var aRolesUsuario = sRolesUsuario.split(",");
function bAccesoModulo(sModulo){
    try{
        //alert(sModulo + " / " +sRolesUsuario);
        switch (sModulo){
            case "PGE":
                //Roles indicados en PGE -> Proyectos del web.sitemap
                var aRolesPermitidos = new Array("A","RSN4","DSN4","CSN4","ISN4","GSN4","RSN3","DSN3","CSN3","ISN3","GSN3","RSN2","DSN2","CSN2","ISN2","GSN2","RSN1","DSN1","CSN1","ISN1","GSN1","RN","DN","CN","IN","GN","RSB","DSB","ISB","GSB","RC","DC","IC","RL","DL","IL","RH","DH","IH","RQN","DQN","IQN","RQ1","DQ1","IQ1","RQ2","DQ2","IQ2","RQ3","DQ3","IQ3","RQ4","DQ4","IQ4","RP","DP","CP","IP");
                break;
            case "PST":
                //Roles indicados en PST -> Gestión del web.sitemap
                var aRolesPermitidos = new Array("A","OT","RSN4","DSN4","CSN4","ISN4","RSN3","DSN3","CSN3","ISN3","RSN2","DSN2","CSN2","ISN2","RSN1","DSN1","CSN1","ISN1","RN","DN","CN","IN","RSB","DSB","ISB","RC","DC","IC","RL","DL","IL","RH","DH","IH","RQN","DQN","IQN","RQ1","DQ1","IQ1","RQ2","DQ2","IQ2","RQ3","DQ3","IQ3","RQ4","DQ4","IQ4","RP","DP","CP","IP","JP","MP","K","BP");
                break;
            case "IAP":
                return true;
                break;
        }
        for (var z=0; z<aRolesUsuario.length; z++){
            for (var n=0; n<aRolesPermitidos.length; n++){
                if (aRolesUsuario[z] == aRolesPermitidos[n]){
                    return true;
                }
            }
        }
        return false;
    }catch(e){
        mostrarErrorAplicacion("Error al obtener el acceso al módulo del acceso rápido", e.message);
    }
}

</script>