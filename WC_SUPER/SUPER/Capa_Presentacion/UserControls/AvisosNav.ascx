<%@ Control Language="c#" Inherits="SUPER.Capa_Presentacion.UserControls.Avisos" CodeFile="AvisosNav.ascx.cs" %>
<script type="text/javascript">
var aFilas;
var nIndiceProy = -1;
var iNumFilas=0, iAvisoAct=0;
var oAviso;
function init(){
    try{    
        aFilas = FilasDe("tblDatos");
        if (aFilas != null) iNumFilas = aFilas.length;
        if (iNumFilas > 0) setAviso('S');

        ocultarProcesando();
        mostrarAvisos();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

var nVision = 0;
function mostrarAvisos() {
    nVision = nVision + 10;
    document.getElementById("divFormPadre").style.clip = "rect(auto, auto, " + nVision + "px, auto)";
    if (nVision < 360) setTimeout("mostrarAvisos()", 20);
}

function setAviso(sDire){
    try{
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

        setOp($I("ctl01_btnSiguiente"), 100);
        setOp($I("ctl01_btnAnterior"), 100);

        if (iAvisoAct == 1) setOp($I("ctl01_btnAnterior"), 30);
        if (iAvisoAct == iNumFilas) setOp($I("ctl01_btnSiguiente"), 30);
        //alert(document.getElementById('ctl01_lblNumAviso'));
        oAviso = (document.getElementById('ctl01_lblNumAviso') == null) ? document.getElementById('ctl02_lblNumAviso') : document.getElementById('ctl01_lblNumAviso');
        //oAviso = (document.getElementById('ctl00_CPHC_AvisosNav_lblNumAviso') == null) ? document.getElementById('ctl00_CPHC_AvisosNav_lblNumAviso') : document.getElementById('ctl00_CPHC_AvisosNav_lblNumAviso');

        oAviso.innerText = (iAvisoAct) + " / " + iNumFilas;
        
        var nPos = location.href.indexOf("Capa_Presentacion");
        var strUrlImg = location.href.substring(0, nPos)+ "images";

        if (aFilas[nIndiceProy].getAttribute("afect").indexOf("PGE") != -1) document.getElementById("imgPGE").src = strUrlImg + "/imgPGEon.gif";
        else document.getElementById("imgPGE").src = strUrlImg+"/imgPGEoff.gif";
        if (aFilas[nIndiceProy].getAttribute("afect").indexOf("PST") != -1) document.getElementById("imgPST").src = strUrlImg + "/imgPSTon.gif";
        else document.getElementById("imgPST").src = strUrlImg+"/imgPSToff.gif";
        if (aFilas[nIndiceProy].getAttribute("afect").indexOf("IAP") != -1) document.getElementById("imgIAP").src = strUrlImg + "/imgIAPon.gif";
        else document.getElementById("imgIAP").src = strUrlImg+"/imgIAPoff.gif";
        
        var oTitulo = (document.getElementById('ctl01_lblTitulo')==null)? document.getElementById('ctl02_lblTitulo'):document.getElementById('ctl01_lblTitulo');
        //var oTitulo = document.getElementById('ctl00_CPHC_AvisosNav_lblTitulo');
        oTitulo.innerText = aFilas[nIndiceProy].getAttribute("titulo");
        document.getElementById('txtComentario').value = aFilas[nIndiceProy].getAttribute("texto");
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos del aviso", e.message);
    }
}
function mostrarCursor(objBoton){
    try{
        if (getOp(objBoton) == 100){        
		        objBoton.style.cursor = "pointer";
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al establecer la visibilidad del botón de navegación de avisos.", e.message);
    }
}
function eliminarAviso(){
    try{
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
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "eliminar":
                document.getElementById("tblDatos").deleteRow(nIndiceProy);
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
<!--<div id="divTotal" style="z-index:20; position:absolute; left:0px; top:0px; width:1100px; height:900px; background-image: url(../../Images/imgFondoPixelado.gif); background-repeat:no-repeat;background-color:Aqua">!-->
<div id="divFormPadre" style="z-index:15;position:absolute; left:160px; top:180px;width:710px; height: 310px; padding-top: 20px; clip:rect(auto auto 0px auto);">
<div id="divForm" style="z-index:11;position:absolute; left:0px; top:10px;">
    <div align="center" class="texto" style="background-image: url('../../../Images/imgFondoCal200.gif');background-repeat:no-repeat;
        width: 200px; height: 23px; position: absolute; top: -10px; left: 60px; padding-top: 5px;">
        Comunicado de D.E.F.&nbsp;&nbsp;(<label id="lblNumAviso" runat="server" style="text-align:right;"></label>)</div>
    <table id="tblAvisos" style="width:700px;text-align:left">
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" align="center">
                  <tr>
                    <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                    <td height="6" background="../../../Images/Tabla/8.gif"></td>
                    <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
                  </tr>
                  <tr>
                    <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px">
                        <table id="tblDatos2" cellspacing="7" cellpadding="0" width="600px">
                            <colgroup><col style="width:105px"/><col style="width:130px"/><col style="width:130px"/><col style="width:130px"/><col style="width:105px"/></colgroup>
                            <tr style="height:40px;">
                                <td colspan=5>&nbsp;
                                 <FIELDSET style="position:absolute; left:25px; top:5px; width: 120px; padding: 2px; margin-left:430px">
			                        <LEGEND>Módulos afectados</LEGEND>  
			                        <table style="width:120px; table-layout:fixed; margin-top:2px;">
			                        <tr>
			                            <td style="width:40px;" align=center><img id="imgPGE" src="../../../Images/imgPGEoff.gif" style="width:32px; height:32px; vertical-align:middle;" /></td>
			                            <td style="width:40px;" align=center><img id="imgPST" src="../../../Images/imgPSToff.gif" style="width:32px; height:32px; vertical-align:middle;" /></td>
			                            <td style="width:40px;" align=center><img id="imgIAP" src="../../../Images/imgIAPoff.gif" style="width:32px; height:32px; vertical-align:middle;" /></td>
			                        </tr>
			                        </table>
                                 </FIELDSET> 
                                </td>
                            </tr>
                              <tr>
                                <td colspan=5>
                                    <nobr class="NBR" id="lblTitulo" runat="server" style="width:550px; font-weight:bold; font-size: 11pt;"></nobr>
                                    
                                </td>
                              </tr>
                              <tr> 
                                <td colspan="5">
                                    <textarea id="txtComentario" cols="110" rows="15" class="txtMultiM" readonly ></textarea>
                                </td>
                              </tr>
                              <tr>
                              <td></td>
                              <td  style="padding-right:15px; padding-top:5px;" align="center">
									<button id="btnAnterior" type="button" onclick="setAviso('A');" class="btnH25W100" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/imgFlechaIzOff.gif" /><span title="">&nbsp;&nbsp;Anterior</span>
									</button>								  
                              </td>
                              <td  style="padding-right:15px; padding-top:5px;" align="center">
									<button id="btnSiguiente" type="button" onclick="setAviso('S');" class="btnH25W100" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/imgFlechaDrOff.gif" /><span title="">&nbsp;&nbsp;Siguiente</span>
									</button>							  
                              </td>                              
                              <td style="padding-left:15px; padding-top:5px;" align="center">
									<button id="btnDestruir" type="button" onclick="eliminarAviso()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/botones/imgDestruir.gif" /><span title="">&nbsp;&nbsp;Destruir</span>
									</button>							  
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
<!--</div>!-->
</center>
<div style="display:none">
    <%=strTablaHTML%>
</div>
