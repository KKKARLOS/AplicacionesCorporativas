<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
   <center>
<table id="tblGeneral" style="width:970px; text-align:left">
    <tr>
        <td>
           <fieldset align="left" style="width:300px;float:left">
                <legend>Estados</legend>                 
                <table style="margin-top:5px; height:17px">
                    <colgroup>
                        <col />
                    </colgroup>
                    <tr> 
                        <td>                         
                            Activa&nbsp;&nbsp;
                            <asp:CheckBox id="chkAbierto" checked runat="server"  onclick="Estados(1);buscar();" style="vertical-align:middle;"/>                                  
                            &nbsp;&nbsp;&nbsp;  
                            Bloqueada&nbsp;&nbsp;
                            <asp:CheckBox id="chkBloqueado" checked runat="server"  onclick="Estados(2);buscar();" style="vertical-align:middle;"/>                                  
                            &nbsp;&nbsp;&nbsp;  
                            Inactiva&nbsp;&nbsp;
                            <asp:CheckBox id="chkInactivo" runat="server"  onclick="Estados(3);buscar();" style="vertical-align:middle;"/>                                  
                        </td>                    
                    </tr>
                </table>
           </fieldset>
           <fieldset align="left" style="margin-left:15px;width:334px;float:left">
                <legend>Asignación</legend>                 
                <table style="margin-top:5px; height:17px">
                    <colgroup>
                        <col />
                    </colgroup>
                    <tr> 
                        <td>  
                            &nbsp;&nbsp;&nbsp;
                            Responsable&nbsp;&nbsp;
                            <asp:CheckBox id="chkResponsable" checked runat="server"  onclick="Asignacion(1);buscar();" style="vertical-align:middle;"/>                                  
                            &nbsp;&nbsp;&nbsp;
                            Beneficiario<asp:CheckBox id="chkBeneficiario" checked runat="server"  onclick="Asignacion(2);buscar();" style="vertical-align:middle;"/>                                  
                        </td>                    
                    </tr>
                </table>
            </fieldset>            
	       </td>    
        </tr>    
        <tr>
           <td>               
           <table id="tblTitulo" style="width:954px; margin-top:10px; height:17px">
                <colgroup>
                    <col style="width:20px;" />
                    <col style="width:40px;" />
                    <col style="width:104px;" />
                    <col style="width:80px;" />
                    <col style="width:355px;" />
                    <col style="width:355px;" />
                </colgroup>
                <tr>
                <td colspan="5">
                
                </td>
                </tr>
                <tr class="TBLINI" align="left">	
                    <td colspan="2" style="padding-left:5px;">Cod.País</td>	
                    <td>
                        <img alt="" class="ICO" style="display:none; cursor:hand" height="11px" src="../../../Images/imgFlechas.gif" width="6px" usemap="#img1" />
                        <map id="img1" name="img1">
                            <area alt="" onclick="ot('tblDatos', 1, 0, 'num', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
                            <area alt="" onclick="ot('tblDatos', 1, 1, 'num', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                        </map>NºLínea
                        <img alt="" class="ICO" id="imgLupa1" style="display:none; cursor:hand" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
	                        height="11px" src="../../../Images/imgLupaMas.gif" width="20px" tipolupa="2" /> 
	                    <img alt="" class="ICO" style="cursor:hand; display:none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
	                        height="11px" src="../../../Images/imgLupa.gif" width="20px" tipolupa="1" />
                    </td>
                    <td>
                        <img alt="" class="ICO" style="display:none; cursor:hand" height="11px" src="../../../Images/imgFlechas.gif" width="6px" usemap="#img2" />
                        <map id="img2" name="img2">
                            <area alt="" onclick="ot('tblDatos', 2, 0, 'num', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
                            <area alt="" onclick="ot('tblDatos', 2, 1, 'num', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                        </map>Extension
                    </td>
                    <td>
                        <img alt="" class="ICO" style="display:none; cursor:hand" height="11px" src="../../../Images/imgFlechas.gif" width="6px" usemap="#img3" />
                        <map id="img3" name="img3">
                            <area alt="" onclick="ot('tblDatos', 3, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
                            <area alt="" onclick="ot('tblDatos', 3, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                        </map>Responsable
                    </td>
                    <td>
                        <img alt="" class="ICO" style="display:none; cursor:hand" height="11px" src="../../../Images/imgFlechas.gif" width="6px" usemap="#img4" />
                        <map id="img4" name="img4">
                            <area alt="" onclick="ot('tblDatos', 4, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
                            <area alt="" onclick="ot('tblDatos', 4, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                        </map>Beneficiario / Departamento
                    </td>
                </tr>	            
            </table>
            <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width:970px; height:460px;" onscroll="scrollTablaLineas();" runat="server">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:954px">
                    <%=strTablaHtml%>
                </div>
            </div>
            <table id="TABLE2" style="height:17px" width="954px" align="left">
                <tr class="TBLFIN">
                    <td align="left" style="padding-left:3px;">Nº de líneas:
                        <asp:Label id="lblNumLineas" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>            
    </td>
    </tr>
    <tr>  
        <td style="padding-top:15px;">
            <img alt="" src="../../../Images/imgActiva.gif" class="ICO" />Activa&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgBloqueada.gif" class="ICO" />Bloqueada&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgInactiva.gif" class="ICO" />Inactiva
            <%--<img alt="" src="../../../Images/imgPreinactiva.gif" class="ICO" />Preinactiva&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgPreactiva.gif" class="ICO" />Preactiva&nbsp;&nbsp;&nbsp;--%>
        </td>                 
    </tr>
</table>
    </center>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "nuevo": //Boton Nuevo
				{
				    bEnviar = false;
					Nuevo();
					break;
				}				
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminar();
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

