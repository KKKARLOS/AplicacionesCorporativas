<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">  
    #tblDatos td{
        padding-left: 5px;
        text-align:left;
    }
</style>
<center>
<table style="width:570px;" cellpadding="0" cellspacing="0" border="0">
    <tr>
     <td>
        <fieldset style="width:532px;">
            <legend>Búsqueda por denominación o código SAP</legend>
            <table style="width:526px">
                <colgroup>
                    <col style="width:380px"/>
                    <col style="width:146px"/>
                </colgroup>
	            <tr>
	                <td>&nbsp;Cadena de búsqueda 
                        <asp:TextBox ID="txtCliente" runat="server" Width="260px" onKeyPress="javascript:if(event.keyCode==13){buscarClientes(this.value);event.keyCode=0;}"></asp:TextBox>
                    </td>
                    <td onclick="buscarClientes($I('txtCliente').value);">
                        <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda">
                            <asp:ListItem style="cursor:pointer;" Value="I"><img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$('rdbTipo_0').checked=true;buscarClientes($I('txtCliente').value);" hidefocus=hidefocus></asp:ListItem>
                            <asp:ListItem style="cursor:pointer;" Selected="True" Value="C"><img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$('rdbTipo_1').checked=true;buscarClientes($I('txtCliente').value);" hidefocus=hidefocus></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </fieldset>
     </td>
    </tr>	
    <tr>
         <td>	
             <table style="width:570px;" cellpadding="0" cellspacing="0" border="0">
	            <tbody>
		            <tr>
			            <td>
				            <table id="tblTitulo" style="margin-top:10px; width:550px; height:17px;">
					            <tr class="TBLINI">
						            <td style="text-align:left; width:460px;">&nbsp;Denominación&nbsp;
						                <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							            <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
								            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
						            </td>
						            <td title="Código externo">Cód. Externo</td>
					            </tr>
				            </table>
		                </td>
		            </tr>
                    <tr>
                        <td>
                            <div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; WIDTH: 566px; HEIGHT: 470px;">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif');WIDTH: 550px;">
	                            </div>
                            </div>
                            <table id="tblResultado" style="height:17px; width:550px;">
	                            <tr class="TBLFIN">
		                            <td>&nbsp;</td>
	                            </tr>
                                <tr>
                                    <td style="padding-top:5px; text-align:left;">
                                        <img class="ICO" src="../../../../Images/imgM.gif" style="vertical-align:middle;" />Matriz&nbsp;&nbsp;&nbsp;
                                        <img class="ICO" src="../../../../Images/imgF.gif" style="vertical-align:middle;" />Filial
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
            </tbody>
            </table>
        </td>
    </tr>	
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            //alert("strBoton: "+ strBoton);
            //			switch (strBoton){
            //				case "guia": 
            //				{
            //                    bEnviar = false;
            //                    mostrarGuia("TarifasCliente.pdf");
            //					break;
            //				}
            //			}
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

