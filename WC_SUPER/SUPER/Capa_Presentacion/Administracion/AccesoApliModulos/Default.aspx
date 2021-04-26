<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var bEs_superadministrador = <%=(Session["ADMINISTRADOR_PC_ACTUAL"].ToString()=="SA")? "true":"false" %>;//A->Administrador; SA->SuperAdministrador
-->
</script>
<br /><br /><br /><br />  
<center>
<table style="width:710px" border="0" celspacing="0" cellpadding="0">
    <tr>
    <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
    <td height="6" background="../../../Images/Tabla/8.gif"></td>
    <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
    </tr>
    <tr>
    <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
    <td background="../../../Images/Tabla/5.gif" style="padding:5px; vertical-align:top;" >
        <div align="center" class="texto" style="background-image: url('../../../Images/imgFondoCal150.gif');background-repeat:no-repeat;
            width: 150px; height: 50px; position: relative; top: -35px; left: 20px;">
            <img id="imgSUPER" style="width: 132px; height: 45px; margin-top:2px;" src="../../../images/imgLogoTrans.gif" /> 
        </div>
        <!-- Inicio del contenido propio de la página -->
            <table id='tblCatalogo' style="text-align:left" align='center' width="100%" cellpadding='15px'>
                <tr>
                    <td style="vertical-align:top; text-align:center;">                        
                        <fieldset style="width: 100%; padding: 7px;">
			            <legend>Aplicación</legend>  
			            <br />                                                 
                        <table id='tblAplicacion' width="100%" align='center' cellpadding='10px'>   
                            <tr>
                                <td width="10%"></td>
                                <td  width="90%">
                                    <label id="lblAudit" class="texto" style="visibility:hidden;">Mostrar auditorías</label>
                                    <input type="checkbox" id="chkAudit" class="check" style="visibility:hidden; margin-left:5px;" onclick="activarGrabar()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:middle; text-align:center;">
									<img id="candSUPER" runat="server" class="MANO" src="../../../Images/icoCerradoG.gif" onclick="abrirCerrar('SUPER');"/> 
								</td>
                                <td style="vertical-align:top; text-align:center;">
									<fieldset style="width: 100%">
			            			<legend>Motivo</legend> 
										<asp:TextBox ID="txtMotivo" TextMode="MultiLine"  SkinID="Multi" Rows="3" MaxLength="220" runat="server" onkeyup="activarGrabar()" Text="" Width="550px"/>
									</fieldset>
                                </td>
                            </tr>	
                        </table>
                       </fieldset>
				    </td>				
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 100%; padding: 7px;">
			            <legend>Módulos</legend> 
			            <br /> 
                        <table id='tblModulos' width="100%" class="texto" align='center' border='0' cellspacing='0' cellpadding='10px'>   
                            <tr>
                                <td style="vertical-align:middle; text-align:center;">
					                <img id='candIAP' runat="server" class="MANO" src="../../../Images/icoCerradoG.gif" onclick="abrirCerrar('IAP');"/> 
					                &nbsp;&nbsp;<img id="imgIAP" runat="server" style="margin-bottom:2px;" src="../../../Images/imgIAPon.gif" />
                                </td>
                                <td style="vertical-align:bottom; text-align:center;">
					                <img id='candPST' runat="server" class="MANO" src="../../../Images/icoCerradoG.gif" onclick="abrirCerrar('PST');"/> 
					                &nbsp;&nbsp;<img id='imgPST' runat="server" style="margin-bottom:2px;" src="../../../Images/imgPSTon.gif" />
                                </td>
                                <td style="vertical-align:bottom; text-align:center;">
					                <img id='candPGE' runat="server" class="MANO" src="../../../Images/icoCerradoG.gif" onclick="abrirCerrar('PGE');"/> 
					                &nbsp;&nbsp;<img id='imgPGE' runat="server" style="margin-bottom:2px;" src="../../../Images/imgPGEon.gif" />
                                </td>
                                <td style="vertical-align:bottom; text-align:center;">
					                <img id='candADP' runat="server" class="MANO" src="../../../Images/icoCerradoG.gif" onclick="abrirCerrar('ADP');"/> 
					                &nbsp;&nbsp;<img id='imgADP' runat="server" style="margin-bottom:2px;" src="../../../Images/imgADPon.gif" />
                                </td>
                            </tr>	
                        </table>
                        </fieldset>		
                    </td>
                </tr>
            </table>
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
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
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
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("Proveedores.pdf");
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
-->
</script>
</asp:Content>

