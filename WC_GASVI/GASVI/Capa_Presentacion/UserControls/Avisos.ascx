<%@ Control Language="c#" Inherits="GASVI.Capa_Presentacion.UserControls.Avisos" CodeFile="Avisos.ascx.cs" %>
<script language="javascript">
    var aFilas;
    var nIndiceProy = -1;
    var iNumFilas = 0, iAvisoAct = 0;
   // window.onload = initAvisos;

    function initAvisos() {
        try {
            aFilas = FilasDe("tblDatos3");
            if (aFilas != null) iNumFilas = aFilas.length;
            if (iNumFilas > 0) mostrarDatos();
            ocultarProcesando();
            mostrarAvisos();
        } catch (e) {
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }

    var nVision = 0;
    function mostrarAvisos() {
	    nVision = nVision + 10;
	    document.getElementById("divFormPadre").style.clip = "rect(auto, auto, " + nVision + "px, auto)";
	    if (nVision < 360) setTimeout("mostrarAvisos()", 20);
    }

    function mostrarDatos() {
        try {
            if (aFilas.length == 0) return;
            if (iAvisoAct >= iNumFilas) {
                document.getElementById('divTotal').removeNode(true);
                return;
            }
            if (nIndiceProy < aFilas.length - 1) {
                nIndiceProy++;
                iAvisoAct++;
                var oAviso = (document.getElementById('ctl01_lblNumAviso') == null) ? document.getElementById('ctl02_lblNumAviso') : document.getElementById('ctl01_lblNumAviso');
                oAviso.innerText = (iAvisoAct) + " / " + iNumFilas;

                var oTitulo = (document.getElementById('ctl01_lblTitulo') == null) ? document.getElementById('ctl02_lblTitulo') : document.getElementById('ctl01_lblTitulo');
                oTitulo.innerText = aFilas[nIndiceProy].getAttribute("titulo");
                document.getElementById('txtComentario').value = aFilas[nIndiceProy].getAttribute("texto");
                if (aFilas[nIndiceProy].getAttribute("borrable") == '0') {
                    $I("pieSiguiente").style.display = "block";
                    $I("pieBorrable").style.display = "none";
                }
                else {
                    $I("pieSiguiente").style.display = "none";
                    $I("pieBorrable").style.display = "block";
                }
            }
            else {
                document.getElementById('divTotal').removeNode(true);
                return;
            }
        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar los datos del aviso", e.message);
        }
    }
//    function mostrarCursor(objBoton) {
//        try {
//            if (objBoton.filters.alpha.opacity == 100) {
//                objBoton.style.cursor = "pointer";
//            }
//        }
//        catch (e) {
//            mostrarErrorAplicacion("Error al establecer la visibilidad del botón de navegación de avisos.", e.message);
//        }
//    }
    function eliminarAviso() {
        try {
            if (aFilas == null) return;
            if (aFilas.length == 0) return;
            if (nIndiceProy < 0) return;
            var js_args = "eliminar@#@" + aFilas[nIndiceProy].id;
            //alert(js_args);
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        } catch (e) {
            mostrarErrorAplicacion("Error al eliminar el aviso", e.message);
        }
    }
    function RespuestaCallBack(strResultado, context) {       
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK") {
            ocultarProcesando();
            var reg = /\\n/g;
            alert(aResul[2].replace(reg, "\n"));
        } else {
            switch (aResul[0]) {
                case "eliminar":
                    $I("tblDatos3").deleteRow(nIndiceProy);
                    if (aFilas.length == 0) {
                        document.getElementById('divTotal').removeNode(true);
                        ocultarProcesando();
                        return;
                    }
                    nIndiceProy--;
                    mostrarDatos();
                    break;
            }
            ocultarProcesando();
        }
    }
    function setAviso(sDire) {
        try {
            if (aFilas.length == 0 || iAvisoAct == iNumFilas) {
                document.getElementById('divTotal').removeNode(true);
                return;
            }

            if (sDire == "S") {
                iAvisoAct++;
                nIndiceProy++;
            }
            else {
                iAvisoAct--;
                nIndiceProy--;
            }

            if (iAvisoAct > iNumFilas) iAvisoAct = iNumFilas;

            if (iAvisoAct < 1) {
                iAvisoAct = 1;
                nIndiceProy = 0;
            }

            if (nIndiceProy >= aFilas.length - 1) nIndiceProy = aFilas.length - 1;

            oAviso = (document.getElementById('ctl01_lblNumAviso') == null) ? document.getElementById('ctl02_lblNumAviso') : document.getElementById('ctl01_lblNumAviso');
            oAviso.innerText = (iAvisoAct) + " / " + iNumFilas;

            var oTitulo = (document.getElementById('ctl01_lblTitulo') == null) ? document.getElementById('ctl02_lblTitulo') : document.getElementById('ctl01_lblTitulo');
            oTitulo.innerText = aFilas[nIndiceProy].getAttribute("titulo");
            document.getElementById('txtComentario').value = aFilas[nIndiceProy].getAttribute("texto");

            if (aFilas[nIndiceProy].getAttribute("borrable") == '0') {
                $I("pieSiguiente").style.display = "block";
                $I("pieBorrable").style.display = "none";
            }
            else {
                $I("pieSiguiente").style.display = "none";
                $I("pieBorrable").style.display = "block";
            }
        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar los datos del aviso", e.message);
        }
    }    
</script>
<center>
<div id="divTotal" style="z-index:10; position:absolute; left:0px; top:0px; width:1100px; height:900px; background-image: url(../../Images/imgFondoPixelado.gif); background-repeat:no-repeat;">
<div id="divFormPadre" style="z-index:15;position:absolute; left:160px; top:180px;width:710px; height: 310px; padding-top: 20px; clip:rect(auto auto 0px auto);">
<div id="divForm" style="z-index:11;position:absolute; left:0px; top:10px;">

      <div class="texto" style="text-align:center;background-image: url('../../Images/imgFondoCal200.gif'); background-repeat:repeat-x;
            z-index:9; width: 200px; height: 23px; position: absolute; top: -10px; left: 60px; padding-top: 5px;">
            Comunicado de GASVI&nbsp;&nbsp;(<label id="lblNumAviso" runat="server" style="text-align:right;"></label>)</div>
    <table id="tblAvisos" style="width:700px;text-align:center" border="0" class="texto" >
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" style="width:700px;">
                  <tr>
                    <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/8.gif); background-repeat:repeat-x;"></td>
                    <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px"></td>
                  </tr>
                  <tr>
                    <td style="background-image:url(../../Images/Tabla/4.gif); background-repeat:repeat-y;">&nbsp;</td>
                    <td style="background-image:url(../../Images/Tabla/5.gif); background-repeat: repeat; padding:5px; vertical-align:top;">
                        <table id="tblDatos2" class="texto" border="0" cellspacing="7" cellpadding="0" width="680px">
                            <colgroup><col width=340px/><col width=340px/></colgroup>
                              <tr>
                                <td colspan=2>
                                    <nobr class="NBR" id="lblTitulo" runat="server" style="width:630px; font-weight:bold; font-size: 11pt;"></nobr>
                                    
                                </td>
                              </tr>
                              <tr> 
                                <td colspan=2>
                                    <textarea id="txtComentario" style="width:650px" rows="15" class="txtMultiM" readonly ></textarea>
                                </td>
                              </tr>
	                          <tr style="text-align:center">                       
			                          <td colspan=2 style="padding-top:5px;">
			                          <center>
                                            <table id="pieSiguiente" style="display:none; margin:0 auto 0 auto;" class="texto" border="0" cellspacing="0" cellpadding="0" width="90px">
                                            <tr>
                                                <td style="text-align:center">	
                                                    <button id="btnSiguiente" type="button" onclick="setAviso('S');" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgFlechaDrOff.gif" /><span title="Siguiente">Siguiente</span></button>								
                                                </td>
					                        </tr>
					                        </table>					                                
                                            <table id="pieBorrable" style="display:block;" class="texto" border="0" cellspacing="0" cellpadding="0" width="210px">
                                            <colgroup>
                                            <col style="width:105px;" />
                                            <col style="width:105px;"/>
                                            </colgroup>
                                            <tr>
                                                <td>
                                                    <button id="btnConservar" type="button" onclick="setAviso('S');" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgFlechaDrOff.gif" /><span title="Siguiente">Siguiente</span></button>								
                                                </td>
                                                <td>
                                                    <button id="btnEliminar" type="button" onclick="eliminarAviso();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgDestruir.gif" /><span title="Destruir">Destruir</span></button>								
					                            </td>
					                        </tr>
					                        </table>
					                   </center>
			                          </td>
                        			 
	                          </tr>
                        </table>
                    </td>
                    <td style="background-image:url(../../Images/Tabla/6.gif); background-repeat: repeat-y; width:6px">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/2.gif); height:4px"></td>
                    <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
                  </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</div>
</div>

</center>
<div style="display:none">
    <%=strTablaHTML%>
</div>
<script language="javascript">
    initAvisos();
</script>