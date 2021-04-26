<%@ Control Language="C#" %>
<div id="divFondoMotivoPreferencia" style="z-index:10; position:absolute; left:0px; top:0px; width:100%; height:100%; visibility:hidden;">
    <div id="divInnerPreferencia" style="position:absolute; top:280px; left:200px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:320px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table id="Table1" class="texto" style="width:300px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td colspan="2">
                        Preferencia almacenada con referencia: <label id="lblNPreferencia" style="display:inline;"></label><br /><br />
                        Pulse &lt;Editar&gt; para asignarle otra denominación o cambiar la seleccionada como defecto.
                    </td>
                </tr>
            </table>
            <center>
            <table id="Table1" class="texto" style="width:240px; margin-top:10px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td align="center">
                        <button id="btnEditarPreferencia" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="goEditarPreferencia();" onmouseover="se(this, 25);">
                            <img src="../../../Images/imgEdicion.gif" /><span>Editar</span>
                        </button>
                    </td>
                    <td align="center">
                        <button id="btnCancelarPreferencia" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="hideCP();" onmouseover="se(this, 25);">
                            <img src="../../../Images/Botones/imgCerrarVentana.gif" /><span>Cerrar</span>
                        </button>
                    </td>
                </tr>
            </table>
            </center>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>
<script type="text/javascript" language="javascript">
<!--
    function centrarCapaPreferencia() {
        try {
            $I("divFondoMotivoPreferencia").style.width = "100%";
            $I("divFondoMotivoPreferencia").style.height = "100%";
            if (!bEsVentanaModal()) {
                if (document.all) {
                    $I("divInnerPreferencia").style.top = ((document.documentElement.clientHeight / 2) - 50) + "px";
                    $I("divInnerPreferencia").style.left = ((document.documentElement.clientWidth / 2) - 160) + "px";
                }
                else {
                    $I("divInnerPreferencia").style.top = ((window.innerHeight / 2) - 50) + "px";
                    $I("divInnerPreferencia").style.left = ((window.innerWidth / 2) - 160) + "px";
                }
            } else {
                if (document.all) {
                    $I("divInnerPreferencia").style.top = ((parseInt(window.dialogHeight, 10) / 2) - 50) + "px";
                    $I("divInnerPreferencia").style.left = ((parseInt(window.dialogWidth, 10) / 2) - 160) + "px";
                }
                else {
                    $I("divInnerPreferencia").style.top = ((window.innerHeight / 2) - 50) + "px";
                    $I("divInnerPreferencia").style.left = ((window.innerWidth / 2) - 160) + "px";
                }
            }
        } catch (e) {
            mostrarErrorAplicacion("Error al centrar la capa de edición de preferencia.", e.message);
        }
    }
    centrarCapaPreferencia();

    function showCP() {
        try {
            $I("divFondoMotivoPreferencia").style.visibility = "visible";
        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar la capa de edición de preferencia.", e.message);
        }
    }
    function hideCP() {
        try {
            $I("divFondoMotivoPreferencia").style.visibility = "hidden";
        } catch (e) {
            mostrarErrorAplicacion("Error al ocultar la capa de edición de preferencia.", e.message);
        }
    }
    function goEditarPreferencia() {
        try {
            if (isNaN(nPantallaPreferencia)) {
                mmoff("War", "No se ha podido determinar la pantalla de preferencia", 400);
                return;
            }
            if (isNaN($I("lblNPreferencia").innerText)) {
                mmoff("War", "No se ha podido determinar el número de preferencia", 400);
                return;
            }
            alert("Pantalla pref: " + nPantallaPreferencia + ". Nº pref: " + $I("lblNPreferencia").innerText);
            var strEnlace = strServer + "Capa_Presentacion/getPreferenciaMant.aspx";
            var nWidth = 450;
            if (bPantallaCM) {
                strEnlace = strServer + "Capa_Presentacion/getPreferenciaMantCM.aspx";
                nWidth = 600;
            }
            strEnlace += "?nP=" + codpar(nPantallaPreferencia) + "&nPref=" + codpar(dfn($I("lblNPreferencia").innerText));

            modalDialog.Show(strEnlace, self, sSize(nWidth, 470))
                .then(function(ret) {
                    if (ret != null){
                        if (ret.resultado == "OK") {
                            //alert(ret);
                            $I("lblDenPreferencia").innerHTML = "<b>Preferencia</b>: " + Utilidades.unescape(ret.denominacion);
                            hideCP();
                        }
                    }
                });
            window.focus();

        } catch (e) {
            mostrarErrorAplicacion("Error al acceder a la edición de preferencias.", e.message);
        }
    }
-->
</script>
