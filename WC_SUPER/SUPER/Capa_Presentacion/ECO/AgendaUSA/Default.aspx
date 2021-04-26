<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<ucgus:UCGusano ID="UCGusano1" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var sAdministrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";	 
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
</script>
<table style="margin-left:20px;">
	<tr>
	    <td>
            <table style="width:940px; height:17px; margin-top:5px;">	
                <tr>
                    <td style="padding-left:5px;">
                        <label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label>&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" runat="server" 
                            onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);setNumPE();}"/>
                        <asp:TextBox ID="txtDesPE" readonly="true" style="width:482px;" Text="" runat="server" />
                    </td>
                </tr>
            </table>
		    <table style="width:940px; height:17px; margin-top:5px;">
			    <colgroup>					
				    <col style="width:140px;" />
				    <col style="width:200px;" />
				    <col style="width:200px;" />
				    <col style="width:200px;" />
				    <col style="width:200px;" />
			    </colgroup>
			    <tr class="TBLINI">				    
				    <td style="padding-left:20px;">Mes</td>
				    <td>Consumos</td>
				    <td>Producción</td>
				    <td>Facturación</td>
				    <td>Otros</td>
			    </tr>
		    </table>
	    </td>
	</tr>
	<tr>
	    <td>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 956px; height:460px" runat="server">
			    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:940px;">
			     <%=strTablaHTML%>
			    </div>
		    </div>
	    </td>
	</tr>
	<tr>
	<td>
		<table style="width:940px; height:17px">
			<tr class="TBLFIN">
				<td >&nbsp;</td>
			</tr>
		</table>
	</td>
	</tr>		
</table>	
<center>
    <table style="margin-top:13px; width:260px;">
	    <tr>
		    <td>
                <button id="btnNueva" type="button" onclick="nueva()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                </button>    
                <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" style="margin-left:60px; display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../Images/Botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                </button>    
		    </td>		
	    </tr>
    </table>
</center>
<asp:TextBox ID="hdnIDPE" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnMesDesde" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnMesHasta" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnMes" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnUSA" runat="server" style="visibility:hidden" Text="N" />
<asp:TextBox ID="hdnProyExternalizable" runat="server" style="visibility:hidden" Text="N" />
<asp:TextBox ID="hdnProyUSA" runat="server" style="visibility:hidden" Text="N" />

<asp:TextBox ID="FORMATO" runat="server" style="visibility:hidden" Text="PDF" />
<asp:TextBox ID="hdnIDS" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnoMes" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="txtMesVisible" runat="server" style="visibility:hidden" Text="" />

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
                    setTimeout("grabar();", 20);
					break;
				}					
				case "regresar": //Boton regresar
				    {
				        bEnviar = false;
				        if (bCambios){
				            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
				                if (answer) {
				                    bEnviar = false;
				                    bRegresar = true;
				                    setTimeout("grabar()", 20);
				                } 
				                else {
				                    bEnviar = true;
				                    bCambios = false;
				                    fSubmit(bEnviar);
				                }
				            });	
				        }
				        else{
				            bEnviar = true;
				            fSubmit(bEnviar);
				        }
					    break;
				    }
				case "pdf": //Boton exportar pdf
				{
					bEnviar = false;
					//Exportar();
					if (bCambios){
					    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
					        if (answer) {
					            bPdf = true;
					            setTimeout("grabar()", 20);
					        } 
					        else {
					            //bCambios = false;
					            Exportar();
					        }
					    });	
					}
					else{
					    Exportar();
					}
					break;
				}
				case "excel":
				{
					bEnviar = false;
					//ExportarExcel();
					if (bCambios){
					    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
					        if (answer) {
					            bExcel = true;
					            setTimeout("grabar()", 20);
					        } 
					        else {
					            //bCambios = false;
					            ExportarExcel();
					        }
					    });	
					}
					else{
					    ExportarExcel();
					}
					break;
				}
			}
		}

		function fSubmit(bEnviar)
		{
		    var theform = document.forms[0];
		    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		    theform.__EVENTARGUMENT.value = eventArgument;
		    if (bEnviar) theform.submit();
		}
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
