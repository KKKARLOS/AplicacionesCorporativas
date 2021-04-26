<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_Seguimiento_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var num_empleado = <%=Session["UsuarioActual"] %>;
    var nAnoMesActual = <%=nAnoMes %>;
    var nIndiceProy = -1;
    var nIndiceFoto = -1;
    var aFila;
    var aFilaF;
    var aProy = new Array();
    var aFoto = new Array();
    var bLectura = false;
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var bRes1024 = <%=((bool)Session["FOTOPST1024"]) ? "true":"false" %>;
    var sMONEDA_VDP = "<%=(Session["MONEDA_VDP"]==null)?"":Session["MONEDA_VDP"].ToString() %>";
</script>
<center>
<div id="div1024" style="z-index: 105; width: 32px; height: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" style="height:32px; width:32px;" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<div id="divGral" style="overflow:auto; width:1240px; height:790px;" onscroll="setPosImgExcel();">
    <table id="tblCab" class="texto" style="height:20px;width:1200px;" cellspacing="5px">
        <colgroup>
            <col style="width:60px;" />
            <col style="width:20px;" />
            <col style="width:80px;" />
            <col style="width:540px;" />
            <col style="width:500px;" />
        </colgroup>
        <tr>
            <td style="text-align:left;">
                <label id="lblProy" runat="server" title="Proyecto económico" style="width:60px; text-align:left; margin-top:2px" class="enlace" onclick="obtenerProyectos()">Proyecto</label>
            </td>
            <td>
                <asp:Image ID="imgEstProy" runat="server" style="height:16px; width:16px;" ImageUrl="~/images/imgSeparador.gif" />
            </td>
            <td>
                <asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:60px;" SkinID="Numero" 
                    onkeypress="javascript:if(event.keyCode==13){buscarPE();event.keyCode=0;}else{vtn2(event);setNumPE();}" />
            </td>
            <td>
                <div id="divPry" style="width:100%;">
                    <asp:TextBox ID="txtNomProy" runat="server" Text="" Width="500px" readonly="true" />
                </div>
            </td>
            <td>
                <div id="divMonedaImportes" runat="server" style="visibility:hidden">
                    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server">Dólares americanos</label>
                </div>
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" class="texto" style="width: 1320px;">
        <tr>
            <td>
	            <table style="width:1300px; height:17px;">
                <colgroup>
                    <col style='width:70px;' />
                    <col style='width:115px;' />
                    <col style='width:70px;' />
                    <col style='width:60px;' />
                    <col style='width:60px;' />
                    <col style='width:100px;' />

                    <col style='width:60px;' />
                    <col style='width:70px;' />
                    <col style='width:65px;' />
                    <col style='width:70px;' />
                    <col style='width:60px;' />

                    <col style='width:70px;' />
                    <col style='width:70px;' />
                    <col style='width:60px;' />
                    <col style='width:40px;' />

                    <col style='width:40px;' />
                    <col style='width:100px;' />

                    <col style='width:40px;' />
                    <col style='width:40px;' />
                    <col style='width:40px;' />
                </colgroup>
	                <tr class="texto" align="center">
                        <td style="text-align:center;">
                            <img id="imgNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="setNE(1,1);"><img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2,1);">	
                        </td>
                        <td style="text-align:center;">
                            <asp:TextBox ID="txtMesCierre" style="width:90px; margin-bottom:5px; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td colspan="4" class="colTabla">Planificado</td>
                        <td colspan="5" class="colTabla">IAP</td>
                        <td colspan="4" class="colTabla">Previsto</td>
                        <td colspan="2" class="colTabla1">Avance</td>
                        <td colspan="3" class="colTabla1">Indicadores</td>
	                </tr>
	                <tr id="tblTitulo" class="TBLINI" align="center">
                        <td colspan='2'>Denominación</td>
                        <!-- Planificado -->
                        <td style="text-align:right;">Total</td>
                        <td>Inicio</td>
                        <td>Fin</td>
                        <td><label id="lblPresupuesto" title="Importe presupuestado" style="width:100px">Imp. Presup.</label></td>
                        <!-- IAP -->
                        <td style="text-align:right;">Mes</td>
                        <td style="text-align:right;"><label id="lblAcum" title="Acumulado">Acumul.</label></td>
                        <td style="text-align:right;"><label id="lblPendEst" title="Pendiente estimado">Pend. Est.</label></td>
                        <td style="text-align:right;"><label id="lblTotEst" title="Total estimado">Total Est.</label></td>
                        <td><label id="lblFinEst" title="Fin estimado">Fin Est.</label></td>
                        <!-- Previsto -->
                        <td style="text-align:right;">Total</td>
                        <td style="text-align:right;">Pdte.</td>
                        <td>Fin</td>
                        <td>%</td>		
                        <!-- Avance -->
                        <td>%</td>		
                        <td><label id="lblProducido" title="Importe producido" style="width:100px">Imp. Produc.</label></td>
                        <!-- Indicadores -->
                        <td style="text-align:right;"><label id="Label1" title="% Consumido: relación entres los esfuerzos consumidos y los planificados">% CO.</label></td>
                        <td style="text-align:right;"><label id="Label2" title="% Desviación de esfuerzos: relación entre los esfuerzos previstos y planificados">% DE.</label></td>
                        <td style="text-align:right;"><label id="Label12" title="% Desviación de plazos: relación entre los plazos previstos y planificados">% DP.</label></td>
	                </tr>
                </table>
	            <div id="divCatalogo" style="overflow:auto; width: 1317px; height:280px">
	                <div style='background-image:url(../../../../Images/imgFT20.gif); width:1301px;'>
	                    <table id='tblDatos' class='texto' style="width:1300px;"></table>
	                </div>
                </div>
                <table id="tblResultado" class="texto" style='width:1300px; text-align:right;'>
                <colgroup>
                    <col style='width:70px;' />
                    <col style='width:115px;' />
                    <col style='width:70px;' />
                    <col style='width:60px;' />
                    <col style='width:60px;' />
                    <col style='width:100px;' />

                    <col style='width:60px;' />
                    <col style='width:70px;' />
                    <col style='width:65px;' />
                    <col style='width:70px;' />
                    <col style='width:60px;' />

                    <col style='width:70px;' />
                    <col style='width:70px;' />
                    <col style='width:60px;' />
                    <col style='width:40px;' />

                    <col style='width:40px;' />
                    <col style='width:100px;' />

                    <col style='width:40px;' />
                    <col style='width:40px;' />
                    <col style='width:40px;' />
                </colgroup>
                    <tr class="TBLFIN" style="height:17px;">
                        <td colspan="2">Total proyecto económico</td>
                        
                        <td align="right"><input id='txtPL' name='txtPL' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtInicioPL' name='txtInicioPL' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly="readonly" /></td>
                        <td><input id='txtFinPL' name='txtFinPL' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly="readonly" /></td>
                        <td align="right"><input id='txtPrePL' name='txtPrePL' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        
                        <td align="right"><input id='txtMes' name='txtMes' type='text' class='txtLabelNumero' style='width:58px;' value='0,00' readonly="readonly" /></td>
                        <td align="right"><input id='txtAcu' name='txtAcu' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td align="right"><input id='txtPen' name='txtPen' type='text' class='txtLabelNumero' style='width:63px;' value='0,00' readonly="readonly" /></td>
                        <td align="right"><input id='txtEst' name='txtEst' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtFinEst' name='txtFinEst' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly></td>
                        
                        <td align="right"><input id='txtTotPR' name='txtTotPR' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td align="right"><input id='txtPdtePR' name='txtPdtePR' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtFinPR' name='txtFinPR' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly="readonly" /></td>
                        <td align="right"><input id='txtAV' name='txtAV' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>		
                        
                        <td align="right"><input id='txtAVPR' name='txtAVPR' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>
                        <td align="right"><input id='txtPro' name='txtPro' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>		
                        
                        <td align="right"><input id='txtIndiCon' name='txtIndiCon' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>		
                        <td align="right"><input id='txtIndiDes' name='txtIndiDes' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>		
                        <td align="right"><input id='txtIndiDesPlazo' name='txtIndiDesPlazo' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>		
		            </tr>
	            </table>
            </td>
        </tr>
    </table>
    <br />
    <table class="texto" style="width:1200px;">
        <colgroup>
            <col style="width:200px;"/><col style="width:35px;"/><col style="width:35px;"/><col style="width:35px;"/>
            <col style="width:120px;"/><col style="width:775px;"/>
        </colgroup>
        <tr>
            <td style="text-align:left;">
                <label id="Label11" class="enlace" onclick="obtenerFotos()" style="height:18px;">Instantánea</label>
                <asp:TextBox id="txtFechaFoto" runat="server" style="margin-bottom:5px; width:90px; margin-left:10px;" readonly="true" />
            </td>
            <td>
                <table id="btnPriRegOff2" style="width:30px;" onmouseover="mostrarCursor(this)">
                      <tr onclick="mostrarFoto(1)">
                        <td>
                            <img src="../../../../images/btnPriRegOff.gif" border="0" title="Ir a la primera instantánea">
                        </td>
                     </tr>
                </table>
            </td>
            <td>
                <table id="btnAntRegOff2" style="width:30px;" onmouseover="mostrarCursor(this)">
                  <tr onclick="mostrarFoto(2)"><td><img src="../../../../images/btnAntRegOff.gif" border="0" align="absmiddle" title="Ir a la anterior instantánea"></td></tr>
                </table>
            </td>
            <td>
                <table id="btnSigRegOff2" style="width:30px;" onmouseover="mostrarCursor(this)">
                  <tr onclick="mostrarFoto(3)"><td><img src="../../../../images/btnSigRegOff.gif" border="0" align="absmiddle" title="Ir a la siguiente instantánea"></td></tr>
                </table>
            </td>
            <td>
                <table id="btnUltRegOff2" style="width:30px;" onmouseover="mostrarCursor(this)">
                  <tr onclick="mostrarFoto(4)"><td><img src="../../../../images/btnUltRegOff.gif" border="0" align="absmiddle" title="Ir a la última instantánea"></td></tr>
                </table>
            </td>
            <td style="text-align:left;">
                <label id="lblConsumos"></label>
            </td>
        </tr>
    </table>
    <table class="texto" style="width:1258px;">
        <tr>
            <td>
	            <table style="width: 1300px; height: 17px;">
                    <colgroup>
                        <col style='width:70px;' />
                        <col style='width:115px;' />
                        <col style='width:70px;' />
                        <col style='width:60px;' />
                        <col style='width:60px;' />
                        <col style='width:100px;' />

                        <col style='width:60px;' />
                        <col style='width:70px;' />
                        <col style='width:65px;' />
                        <col style='width:70px;' />
                        <col style='width:60px;' />

                        <col style='width:70px;' />
                        <col style='width:70px;' />
                        <col style='width:60px;' />
                        <col style='width:40px;' />

                        <col style='width:40px;' />
                        <col style='width:100px;' />

                        <col style='width:40px;' />
                        <col style='width:40px;' />
                        <col style='width:40px;' />
                    </colgroup>
	                <tr class="texto" style="text-align:center; height:25px;">
                        <td style="text-align:center;">
                            <img id="imgFotoNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="setNE(1,2);"><img id="imgFotoNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2,2);">	
                        </td>
                        <td></td>
                        <td colspan="4" class="colTabla">Planificado</td>
                        <td colspan="5" class="colTabla">IAP</td>
                        <td colspan="4" class="colTabla">Previsto</td>
                        <td colspan="2" class="colTabla1">Avance</td>
                        <td colspan="3" class="colTabla1">Indicadores</td>
	                </tr>
	                <tr id="tblTitulo2" class="TBLINI" style="text-align:center;">
                        <td colspan="2" style="text-align:center;">Denominación</td>
                        <td>Total</td>
                        <td>Inicio</td>
                        <td>Fin</td>
                        <td><label id="Label3" title="Importe presupuestado" style="width:100px">Imp. Presup.</label></td>
                        
                        <td>Mes</td>
                        <td><label id="Label4" title="Acumulado">Acumul.</label></td>
                        <td><label id="Label5" title="Pendiente estimado">Pend. Est.</label></td>
                        <td><label id="Label6" title="Total estimado">Total Est.</label></td>
                        <td><label id="Label7" title="Fin estimado">Fin Est.</label></td>
                        
                        <td>Total</td>
                        <td>Pdte.</td>
                        <td>Fin</td>
                        <td>%</td>		
                        
                        <td>%</td>		
                        <td><label id="Label8" title="Importe producido" style="width:100px">Imp. Produc.</label></td>
                        
                        <td><label id="Label9" title="% Consumido: relación entres los esfuerzos consumidos y los planificados">% CO.</label></td>
                        <td><label id="Label10" title="% Desviación: relación entre los esfuerzos previstos y planificados">% DE.</label></td>
                        <td><label id="Label13" title="% Desviación de plazos: relación entre los plazos previstos y planificados">% DP.</label></td>
	                </tr>
                </table>
	            <div id="divCatalogo2" style="overflow:auto; width:1317px; height:300px;">
	                <div style='background-image:url(../../../../Images/imgFT20.gif); width:1301px;'>
	                    <table id='tblDatos2' class='texto' style='width: 1300px;'></table>
	                </div>
                </div>
                <table id="tblResultado2" class="texto" style='width: 1300px;text-align:right;'>
                    <colgroup>
                        <col style='width:70px;' />
                        <col style='width:115px;' />
                        <col style='width:70px;' />
                        <col style='width:60px;' />
                        <col style='width:60px;' />
                        <col style='width:100px;' />

                        <col style='width:60px;' />
                        <col style='width:70px;' />
                        <col style='width:65px;' />
                        <col style='width:70px;' />
                        <col style='width:60px;' />

                        <col style='width:70px;' />
                        <col style='width:70px;' />
                        <col style='width:60px;' />
                        <col style='width:40px;' />

                        <col style='width:40px;' />
                        <col style='width:100px;' />

                        <col style='width:40px;' />
                        <col style='width:40px;' />
                        <col style='width:40px;' />
                    </colgroup>
                    <tr class="TBLFIN" style="height:17px;">
                        <td colspan="2">&nbsp;&nbsp;Total proyecto económico</td>
                        <td><input id='txtPL2' name='txtPL2' type='text' class='txtLabelNumero' style='width:60px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtInicioPL2' name='txtInicioPL2' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly="readonly" /></td>
                        <td><input id='txtFinPL2' name='txtFinPL2' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly="readonly" /></td>
                        <td><input id='txtPrePL2' name='txtPrePL2' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        
                        <td><input id='txtMes2' name='txtMes2' type='text' class='txtLabelNumero' style='width:58px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtAcu2' name='txtAcu2' type='text' class='txtLabelNumero' style='width:58px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtPen2' name='txtPen2' type='text' class='txtLabelNumero' style='width:63px;' value='0,00' readonly=readonly" /></td>
                        <td><input id='txtEst2' name='txtEst2' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtFinEst2' name='txtFinEst2' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly="readonly" /></td>
                        
                        <td><input id='txtTotPR2' name='txtTotPR2' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtPdtePR2' name='txtPdtePR2' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>
                        <td><input id='txtFinPR2' name='txtFinPR2' type='text' class='txtLabelNumero' style='width:58px;' value='' readonly="readonly" /></td>
                        <td><input id='txtAV2' name='txtAV2' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>		
                        
                        <td><input id='txtAVPR2' name='txtAVPR2' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>
                        <td><input id='txtPro2' name='txtPro2' type='text' class='txtLabelNumero' style='width:68px;' value='0,00' readonly="readonly" /></td>		
                        
                        <td><input id='txtIndiCon2' name='txtIndiCon2' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>		
                        <td><input id='txtIndiDes2' name='txtIndiDes2' type='text' class='txtLabelNumero' style='width:38px;' value='0' readonly="readonly" /></td>		
                        <td><input id="txtIndiDesplazo2" type="text" class="txtLabelNumero" style="width:38px;" value="0" readonly="readonly" /></td>		
		            </tr>
	            </table>
            </td>
        </tr>
    </table>
</div>
</center>
    <asp:TextBox ID="t305IdProyectoSubnodo" runat="server" Text="" style="width:2px;visibility:hidden" />
    <asp:TextBox ID="txtEstado" runat="server" style="width:2px;visibility:hidden" />
    <asp:TextBox ID="MonedaPSN" runat="server" style="visibility:hidden" Text="" />
    <label id="lblNodo" runat="server" class="texto" style="width:2px;visibility:hidden"></label>
    <asp:TextBox ID="txtUne" runat="server" Text="" style="width:2px;visibility:hidden" />
    <asp:TextBox ID="txtDesCR" runat="server" Text="" style="width:2px;visibility:hidden" />
    <asp:TextBox ID="txtNomResp" runat="server" Text="" style="width:2px;visibility:hidden" />
    <asp:TextBox ID="txtNomCliente" runat="server" Text="" style="width:2px;visibility:hidden" />
    <asp:TextBox ID="txtNivelPresupuesto" runat="server" Text="" style="width:2px;visibility:hidden" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript"> 
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "excel":
                    {
                        bEnviar = false;
                        mostrarProcesando();
                        setTimeout("excelTotal();", 20);
                        break;
                    }
                case "guia":
                    {
                        bEnviar = false;
                        mostrarGuia("HistoricoInstantaneasTecnicas.pdf");
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }

    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }    	
</script>
</asp:Content>

