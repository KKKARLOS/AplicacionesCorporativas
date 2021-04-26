<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var nAnoMesActual = <%=DateTime.Now.Year * 100 + DateTime.Now.Month %>;
</script>
<center> 
<table style="width:1000px;text-align:left;margin-top:100px">
    <tr>    
        <td align="center">
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:5px">
					<table style="width: 940px;margin-top:10px; margin-left:20px;text-align:left; ">
						<colgroup>
							<col style="width:450px;" />
							<col style="width:40px;" />
							<col style="width:450px;" />
						</colgroup>
						<tr height="45px" colspan="3">
							<td colspan="3">Ficheros correspondientes&nbsp;&nbsp;
								<img id="imgAM" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer; vertical-align:middle;" border=0 />
								<asp:TextBox ID="txtMesBase" style="width:90px; vertical-align:middle; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
								<img id="imgSM" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer; vertical-align:middle;" border=0 />
							</td>
						</tr>
						<tr>
							<td><!-- Relación de Items -->
								<table id="tblCatIni" style="width: 430px; height: 17px">
									<tr class="TBLINI">
										<td style="padding-left:3px">
											<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>
											<img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
											<img style="display: none; cursor: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
										</td>                    
									</tr>
								</table>
								<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 446px; height:400px">
									<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:430px">
                                        <%=strTablaHTML%>
									</div>
								</div>
								<table style="width: 430px; height: 17px">
									<tr class="TBLFIN">
										<td></td>
									</tr>
								</table>
							</td>
							<td style="vertical-align:middle;">
								<asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
							</td>
							<td ><!-- Items asignados -->
								<table id="tblAsignados" style="width: 430px; height: 17px">
									<tr class="TBLINI">
										<td style="padding-left:3px">
											Elementos seleccionados
											<img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
											<img style="display: none; cursor: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
										</td>
									</tr>
								</table>
								<div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 446px; height:400px" target="true" onmouseover="setTarget(this)" caso="2">
									<div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:430px">
                                        <%=strTablaHTML2%>
									</div>
								</div>
								<table style="width: 430px; height: 17px">
									<tr class="TBLFIN">
										<td></td>
									</tr>
								</table>
							</td>
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
            <br />
        </td>
    </tr>
  </table>
</center>
<div class="clsDragWindow" id="DW" noWrap></div>
<input type="hidden" id="hdnIdsNodos" value="" runat="server"/>
<asp:TextBox ID="txtAnioMes" runat="server" Text="" style="width:2px;visibility:hidden" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        switch (strBoton) {
				case "procesar": 
				{
                    bEnviar = false;
                    procesar();
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

