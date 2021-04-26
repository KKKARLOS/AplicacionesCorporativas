<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    <%=sTipologias %>
</script>
    
<center>
<br /><br />
<table id="nombreProyecto" style="width:950px;text-align:left;margin-top:30px;">
    <tr>
        <td>     
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" cellspacing="7" style="width:920px">
                        <colgroup><col style="width:80px" /><col style="width:840px" /></colgroup>
                        <tr> 
                            <td><label id="lblProy" runat="server" title="Proyecto económico" style="width:55px;height:17px" class="enlace" onclick="getPE()">Proyecto</label></td>
                            <td>
                                <asp:TextBox id="txtNumPE" runat="server" SkinID="Numero" MaxLength="6" Width="50px" onkeypress="javascript:if(event.keyCode==13&&this.value!=''){buscar();event.keyCode=0;}else{control();}"></asp:TextBox>
                                &nbsp;&nbsp;<asp:TextBox ID="txtDesPE" style="width:570px;" runat="server" readonly="true" />&nbsp;&nbsp;
                                <asp:Image ID="imgEstado" runat="server" style="width:16px; height:16px; vertical-align:middle;" ImageUrl="~/images/imgSeparador.gif" />&nbsp;&nbsp;
                                <asp:Image ID="imgCat" runat="server" style="width:16px; height:16px; vertical-align:middle;" ImageUrl="~/images/imgSeparador.gif" />
                            </td>
                        </tr>                     
                        <tr>
                            <td><br /></td>
                            <td><br /><br /></td>
                        </tr>
	                    <tr>	
		                    <td colspan="2">
		                        <table id="Table1" cellspacing="0" style="width:920px">
                                <colgroup><col style="width:455px" /><col style="width:10px" /><col style="width:455px" /></colgroup>
                                <tr>
                                <td>
                                    <fieldset style="width: 430px; height:120px;">
            	                    <legend>Datos actuales</legend>
                                    <table style='margin-top:5px; width:430px' cellpadding="5px">
                                        <colgroup><col style="width:430px"/></colgroup>
                                        <tr>
                                            <td><label style="width:60px;">Tipología</label>&nbsp;&nbsp;<asp:TextBox ID="txtTipologia" runat="server" Width="340px" readonly="true"></asp:TextBox>
						                    </td>
                                        </tr> 
                                        <tr>
                                            <td><label style="width:60px;">Naturaleza</label>&nbsp;&nbsp;<asp:TextBox ID="txtNaturaleza" runat="server" Width="340px" readonly="true"></asp:TextBox>
                                            <br />
						                    </td>
                                        </tr>                                                                                 
                                        <tr>
                                            <td><label style="width:60px;">Contrato</label>&nbsp;&nbsp;<asp:TextBox ID="txtIdContrato" style="width:50px;" Text="" SkinID="Numero" readonly="true" runat="server"/>&nbsp;&nbsp;<asp:TextBox ID="txtContrato" runat="server" Width="280px" readonly="true"></asp:TextBox>
                                            <br />
						                    </td>
                                        </tr> 
                                        <tr>
                                            <td><label style="width:60px;">Cliente</label>&nbsp;
                                            <asp:TextBox ID="txtCliente" style="width:340px;" runat="server" readonly="true" />
						                    </td>
                                        </tr>                                                                                 
                                    </table>   				
			                        </fieldset>	
			                    </td>
			                    <td><br /></td>
			                    <td>
                                    <fieldset style="width: 430px; height:120px;">
            	                    <legend>Datos a modificar</legend>
                                    <table style='margin-top:5px;width:430px' cellpadding="5px">
                                        <colgroup><col style="width:430px"/></colgroup>
                                        <tr>
                                            <td><label style="width:60px;">Tipología</label>&nbsp;
                                                <asp:DropDownList ID="cboTipologiaNew" disabled runat="server" Width="344px" onchange="setTipologia();" AppendDataBoundItems=true>
                                                </asp:DropDownList><br />
						                    </td>
                                        </tr> 
                                        <tr>
                                            <td><label id="lblNaturaleza" style="width:60px;" title="Muestra naturalezas con el mismo tipo de coste que la original">Naturaleza</label>&nbsp;&nbsp;<asp:TextBox ID="txtNaturalezaNew" runat="server" Width="340px" readonly="true"></asp:TextBox>
                                            <br />
						                    </td>
                                        </tr>                                                                                 
                                        <tr>
                                            <td><label id="lblContrato" style="width:60px;">Contrato</label>&nbsp;&nbsp;<asp:TextBox ID="txtIdContratoNew" style="width:50px;" Text="" readonly="true" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13&&this.value!=''){event.keyCode=0;NumContrato(this);}else{vtn2(event);$I('txtContratoNew').value='';$I('txtClienteNew').value='';$I('hdnIdCliente').value='';} if ($I('lblContrato').className=='enlace') aG();" />&nbsp;&nbsp;<asp:TextBox ID="txtContratoNew" runat="server" Width="280px" readonly="true"></asp:TextBox>
                                            <br />
						                    </td>
                                        </tr> 
                                        <tr>
                                            <td><label id="lblCliente" style="width:60px;">Cliente</label>&nbsp;
                                            <asp:TextBox ID="txtClienteNew" style="width:340px;" runat="server" readonly="true" />
						                    </td>
                                        </tr>                                              
                                    </table>   				
			                        </fieldset>				                    	
			                    </td>
			                    </tr>
			                    </table>			
                            </td>
                        </tr>  
                 
                    </table>
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
</table>                       
<table style="width:950px;text-align:left;">
    <colgroup>
        <col style="width:90px" />
        <col style="width:90px" />
        <col style="width:630px" />
    </colgroup>
      <tr> 
        <td><img class="ICO" src="../../../../Images/imgProducto.gif" />&nbsp;Producto</td>
        <td><img class="ICO" src="../../../../Images/imgServicio.gif" />&nbsp;Servicio</td>
        <td><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />&nbsp;Proyecto</td>
      </tr>
</table>                            
</center>	

<asp:TextBox ID="hdnIdNaturaleza" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdCliente" runat="server" style="visibility:hidden" Text="" />
    <input type="hidden" name="hdnCosteNaturalezaOrigen" id="hdnCosteNaturalezaOrigen" value="" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        grabar();
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

