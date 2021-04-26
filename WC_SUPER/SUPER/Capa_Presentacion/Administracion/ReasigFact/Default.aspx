<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
    <table style="width: 980px;text-align:left;">
    <tr>
        <td align="center">
        <fieldset style="width:360px;text-align:left;">
            <legend>Factura&nbsp;&nbsp;&nbsp;&nbsp;<img id="gomaCli" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarSyN()" style="cursor:pointer; vertical-align:middle;" title="Limpia serie y número"></legend>
            <table class="texto" style="width: 350px;">
                <tr>
                    <td style="width:130px">
                        &nbsp;&nbsp;Serie&nbsp;&nbsp;
                        <asp:TextBox ID="txtSerie" style="width:60px;" Text="" MaxLength="5" runat="server" onkeypress="if(event.keyCode==13){buscar1();event.keyCode=0;}"/>
                    </td>
                    <td style="width:120px">
                        Número&nbsp;&nbsp;
                        <asp:TextBox ID="txtNum" style="width:60px;" Text="" MaxLength="6" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){buscar1();event.keyCode=0;}else{vtn2(event);}" />
                    </td>
                    <td style="width:100px">
						<button id="btnObtener" type="button" onclick="buscar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
							<img src="../../../images/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
						</button>	
                    </td>
                </tr>
            </table>
        </fieldset>
        </td>
    </tr>
    <tr>
        <td>
            <fieldset style="width:970px">
            <legend>Líneas</legend>
		    <table  style="width: 950px; height: 17px; margin-top:5px;">
                <colgroup>
                    <col style='width:285px;' /><col style='width:80px;' />
                    <col style='width:80px;' /><col style='width:80px;' /><col style='width:80px;' />
                    <col style='width:30px;' /><col style='width:25px;' /><col style='width:25px;' />
                    <col style='width:95px;' /><col style='width:100px;' /><col style='width:70px;' />
                </colgroup>                    
			    <tr class="TBLINI">
				    <td style="padding-left:15px;">Proyecto</td>
				    <td style="text-align:right;">Importe(€)</td>
				    <td style="text-align:right;" title="Importe en la moneda del proyecto">Importe(MP)</td>
				    <td style="text-align:right;">Cobro(€)</TD>
				    <td style="text-align:right;" title="Cobro en la moneda del proyecto">Cobro(MP)</td>
				    <td title="Moneda del proyecto">&nbsp;MP</td>
				    <td>Año</td>
				    <td>Mes</td>
				    <td title="Cliente proyecto" align="left">&nbsp;Cliente Proy.</td>
				    <td>&nbsp;Cliente factura</td>
				    <td>Motivo</td>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow: auto; width: 966px; height:340px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:950px">
		        </div>
		    </div>
		    <table id="tblTotal"  style="width: 950px; height: 17px;">
			    <tr class="TBLFIN">
				    <td width="265px" style="font-weight:bold;">&nbsp;&nbsp;Total actual</TD>
				    <td width="100px" style="text-align:right;"></TD>
				    <td width="50px"></TD>
				    <td width="100px" style="text-align:right;"></TD>
				    <td></td>
			    </tr>
		    </table>
		    <table id="tblTotIni" style="width: 950px; height: 17px;">
			    <tr class="TBLFIN">
				    <td width="265px" style="font-weight:bold;">&nbsp;&nbsp;Total original</TD>
				    <td width="100px" style="text-align:right;"></td>
				    <td width="50px"></td>
				    <td width="100px" style="text-align:right;"></td>
				    <td></td>
			    </tr>
		    </table>
            <center>
                <table id="tblBotones" align="center" style="margin-top:5px;" width="420px">
                <tr>
                    <td>
                        <button id="btnNuevo" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                         onmouseover="se(this, 25);mostrarCursor(this);">
	                        <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                        </button>	
                    </td>
                    <td>
                        <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                         onmouseover="se(this, 25);mostrarCursor(this);">
	                        <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                        </button>	
                    </td>
			        <td id="cldAuditoria" runat="server" align="center">
				        <button id="btnAuditoria" type="button" onclick="getAuditoriaAux()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
					         onmouseover="se(this, 25);mostrarCursor(this);">
					        <img src="../../../images/botones/imgAuditoria.gif" /><span title="Auditoría de datos modificados">Auditoría</span>
				        </button>	
			        </td>                    
                </tr>
                </table>
            </center 
            <br />
            </fieldset>
        </td>
    </tr>
    </table>
    </center>
    <table width="940px" border="0" class="texto" align="center">
        <colgroup>
            <col style="width:100px" />
            <col style="width:90px" />
            <col style="width:750px" />
        </colgroup>
	      <tr> 
	        <td><img class="ICO" src="../../../Images/imgProducto.gif" />Producto</td>
            <td colspan=2><img class="ICO" src="../../../Images/imgServicio.gif" />Servicio</td>
	      </tr>
	      <tr><td><img class="ICO" src="../../../Images/imgIconoContratante.gif" />Contratante</td>
                <td><img class="ICO" src="../../../Images/imgIconoRepJor.gif" />Replicado</td>
                <td><img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
          </tr>
	      <tr><td style="vertical-align:top;"><img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
                <td><img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
                <td><img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado</td>
          </tr>
    </table>
   <input type="hidden" runat="server" name="hdnAnoMes" id="hdnAnoMes" value="" />
   <input type="hidden" runat="server" name="hdnClaseEco" id="hdnClaseEco" value="" />
   <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("grabar();", 20);
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

