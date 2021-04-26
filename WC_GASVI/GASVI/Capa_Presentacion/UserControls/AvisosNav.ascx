<%@ Control Language="c#" Inherits="GASVI.Capa_Presentacion.UserControls.Avisos" CodeFile="AvisosNav.ascx.cs" %>
<script language="javascript">
var aFilas;
var nIndiceProy = -1;
var iNumFilas=0, iAvisoAct=0;
var oAviso;
function init(){
    try{    
        aFilas = FilasDe("tblDatos3");
        if (aFilas != null) iNumFilas = aFilas.length;
        if (iNumFilas > 0) setAviso('S');

        ocultarProcesando();
        mostrarAvisos();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

var nVision=0;
function mostrarAvisos() {
    nVision = nVision + 10;
    document.getElementById("divFormPadre").style.clip = "rect(auto, auto, " + nVision + "px, auto)";
    if (nVision < 360) setTimeout("mostrarAvisos()", 20);
}

function setAviso(sDire){
    try {
        if (aFilas.length == 0) 
        {
            document.getElementById('divTotal').removeNode(true);
            return;
        }
        if  (sDire=="S")
        {
            iAvisoAct++;
            nIndiceProy++;
        }
        else
        {        
            iAvisoAct--;
            nIndiceProy--;
        }
        if (iAvisoAct > iNumFilas) iAvisoAct=iNumFilas;
        if (iAvisoAct < 1) 
        {
            iAvisoAct=1;
            nIndiceProy=0;
        }
        if (nIndiceProy >= aFilas.length-1) nIndiceProy = aFilas.length-1;
        
        setOp($I("btnSiguiente"),100);
        setOp($I("btnAnterior"),100);
        
        if (iAvisoAct==1) setOp($I("btnAnterior"),30);
        if (iAvisoAct == iNumFilas) setOp($I("btnSiguiente"), 30);

        oAviso = (document.getElementById('ctl01_lblNumAviso')==null)? document.getElementById('ctl02_lblNumAviso'):document.getElementById('ctl01_lblNumAviso');
        oAviso.innerText = (iAvisoAct) + " / " + iNumFilas;
        
        var oTitulo = (document.getElementById('ctl01_lblTitulo')==null)? document.getElementById('ctl02_lblTitulo'):document.getElementById('ctl01_lblTitulo');
        oTitulo.innerText = aFilas[nIndiceProy].getAttribute("titulo");
        document.getElementById('txtComentario').value = aFilas[nIndiceProy].getAttribute("texto");

        if (aFilas[nIndiceProy].getAttribute("borrable") == '0') {
            setOp($I("btnDestruir"), 30);
        }
        else {
            setOp($I("btnDestruir"), 100);
        }        

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos del aviso", e.message);
    }
}
function mostrarCursor(objBoton){
    try{
        if (objBoton.filters.alpha.opacity == 100){
		        objBoton.style.cursor = "pointer";
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al establecer la visibilidad del botón de navegación de avisos.", e.message);
    }
}
function eliminarAviso(){
    try {

        if (aFilas == null) return;
        if (aFilas.length == 0) return;
        if (nIndiceProy < 0) return;
        var js_args = "eliminar@#@" + aFilas[nIndiceProy].id;
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");        
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el aviso", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "eliminar":
                $I("tblDatos3").deleteRow(nIndiceProy);
                iNumFilas = aFilas.length;
                if (aFilas.length == 0){
                    document.getElementById('divTotal').removeNode(true);
                    ocultarProcesando();
                    return;
                }
                setAviso('D');
                break;
        }
        ocultarProcesando();
    }
}
</script>
<center>
<div id="divTotal">
<div id="divFormPadre" style="z-index:15;position:absolute; left:160px; top:180px;width:710px; height: 310px; padding-top: 20px; clip:rect(auto auto 0px auto);">
<div id="divForm" style="z-index:11;position:absolute; left:0px; top:10px;">
        <div class="texto" style="text-align:center; background-image: url('../../../Images/imgFondoCal200.gif');
            width: 200px; height: 23px; position: absolute; top: -10px; left: 60px; padding-top: 5px;">
            Comunicado de GASVI&nbsp;&nbsp;(<label id="lblNumAviso" runat="server" style="text-align:right;"></label>)</div>
    <table id="tblAvisos" style="text-align:center; width:700px;" border="0" class="texto">
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" style="text-align:center">
                  <tr>
                    <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                    <td height="6" background="../../../Images/Tabla/8.gif"></td>
                    <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
                  </tr>
                  <tr>
                    <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px">
                        <table id="tblDatos2" class="texto" border="0" cellspacing="7" cellpadding="0" width="600px">
                            <colgroup><col width=120px/><col width=130px/><col width=130px/><col width=130px/><col width=120px/></colgroup>
                              <tr>
                                <td colspan=5>
                                    <nobr class="NBR" id="lblTitulo" runat="server" style="width:550px; font-weight:bold; font-size: 11pt;"></nobr>
                                    
                                </td>
                              </tr>
                              <tr> 
                                <td colspan=5>
                                    <textarea id="txtComentario" cols="140" rows="15" class="txtMultiM" readonly ></textarea>
                                </td>
                              </tr>
                              <tr>
                              <td></td>
                              <td  style="padding-right:5px; padding-top:5px;text-align:center">
                                    <button id="btnAnterior" type="button" onclick="setAviso('A');" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgFlechaIzOff.gif" /><span title="Anterior">Anterior</span></button>
                              </td>
                              <td  style="padding-right:5px; padding-top:5px;text-align:center">
                                  <button id="btnSiguiente" type="button" onclick="setAviso('S');" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgFlechaDrOff.gif" /><span title="Siguiente">Siguiente</span></button>
                              </td>                              
                              <td style="padding-left:5px; padding-top:5px;text-align:center">
                                  <button id="btnDestruir" type="button" onclick="eliminarAviso();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgDestruir.gif" /><span title="Destruir">Destruir</span></button>
                              </td> 
                              <td></td>                       
                            </tr>
                        </table>
                    </td>
                    <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
                  </tr>
                  <tr>
                    <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
                    <td height="6" background="../../../Images/Tabla/2.gif"></td>
                    <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
                  </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</div>
</center>
<div style="display:none">
    <%=strTablaHTML%>
</div>
