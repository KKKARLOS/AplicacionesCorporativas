<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_psp_informes_ConsTecnico_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var num_proyecto = "<%=Session["NUM_PROYECTO"] %>";
-->
</SCRIPT><br /><br />
<center>

    <table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
      </tr>
      <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
	    <!-- Inicio del contenido propio de la página -->

 <table id='tblCatalogo' class="texto" align='center' border='0' cellspacing='0' cellpadding='18px'>
    <tr>
        <td  width="126px"><img src="../../../../Images/imgImpresora.gif" /></td>
        <td>
            <br />  
            Concepto&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 		    <asp:dropdownlist id="cboConcepto" runat="server" Width="200px" CssClass="combo" onChange="javascript:CargarConcepto(this.value)">
            </asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:label id="lblConceptoEnlace" onclick="javascript:CargarDatos($I('ctl00_CPHC_cboConcepto').value);" runat="server"
CssClass="enlace" Visible="true" Width="150px"></asp:label> <br />   <br /> 
        <FIELDSET style="width:100%;" id="fldConceptos" runat="server">
        <DIV style="OVERFLOW: auto; OVERFLOW-X: hidden; HEIGHT: 175px; width: 450PX" id="divCatalogo">
        <TABLE id="tblConceptos" cellspacing="0" cellpadding="0" width="100%">
        <%=strTablaHtml %>
        </TABLE>
        </DIV>
        </FIELDSET> 
        </td>
    </tr>
    <tr>
        <td colspan="2" style="vertical-align:top; text-align:center;">
        <table class="texto" align='center' border='0' cellspacing='0' cellpadding='0'>
           <tr><td >
        <FIELDSET id="fldTecnicos" class="fld" style="height:80px; width:'300px';text-align:left" runat="server"> 
        <LEGEND class="Tooltip" title="Profesionales">&nbsp;Profesionales&nbsp;</LEGEND>     
            <br />
            <table class="texto" align='center' border='0' cellspacing='0' cellpadding='10'>
            <tr><td >&nbsp;
            <INPUT hideFocus id="chkInternos" class="check" checked="checked" type="checkbox"  runat="server" />&nbsp;&nbsp;Internos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <INPUT hideFocus id="chkExternos" class="check" checked="checked" type="checkbox"  runat="server" />&nbsp;&nbsp;Externos  
            </td></tr> 
            </table>         
        </FIELDSET>	
            </td><td >    		    
&nbsp;&nbsp;&nbsp;&nbsp;
<FIELDSET id="fldDesglose" class="fld" style="height: 80px;width:300px;text-align:left" runat="server"> 
	<LEGEND class="Tooltip" title="Información">&nbsp;Información&nbsp;</LEGEND>
	        <table class='texto' style='margin-top:12px;margin-left:40px' align='center' border='0' cellspacing='0' cellpadding='8'>
            <tr><td align="center">        
	            <asp:radiobuttonlist id="rdlDesglose" runat="server" Width="250px" SkinID="rbl" RepeatLayout="Table" CellSpacing="0" CellPadding="0"  RepeatDirection="horizontal">
		            <asp:ListItem Value="1">&nbsp;&nbsp;Agregada</asp:ListItem>
		            <asp:ListItem Value="0" Selected="True">&nbsp;&nbsp;Desglosada</asp:ListItem>
	            </asp:radiobuttonlist>
	            </td></tr> 
            </table>    
</FIELDSET>	
                </td>
                </tr> 
            </table>   	                         
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            
            <FIELDSET id="fldPeriodo" class="fld" style="height: 50px;width:'320px';" runat="server">
	           <LEGEND class="Tooltip" title="Periodo">&nbsp;Periodo&nbsp;</LEGEND>
				<br />			
&nbsp;&nbsp;Desde&nbsp;&nbsp;
<asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('D');" goma=1 lectura=0 />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="txtFechaFin" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('H');" goma=1 lectura=0 />
				<br />
				<br />
            </FIELDSET>	
            <br /><br />
        </td>
    </tr>
</table>

	    <!-- Fin del contenido propio de la página -->
	    </td>
        <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
      </tr>
      <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
        <td height="6" background="../../../../Images/Tabla/2.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
      </tr>
    </table>
</center>
<asp:textbox id="hdnEmpleado" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="hdnConcepto" runat="server" Width="0px">0</asp:textbox>
<asp:textbox id="hdnCodConcepto" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="hdnNomConcepto" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="hdnDesCR" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="hdnPerfil" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="hdnNombre" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="FORMATO" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="NESTRUCTURA" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="CODIGO" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="FECHADESDE" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="FECHAHASTA" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="TECNICOS" runat="server" Width="0px"></asp:textbox>
<asp:textbox id="DESGLOSADO" runat="server" Width="0px"></asp:textbox>
</asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
		var oReg = /\$/g;
		var oElement = document.getElementById(eventTarget.replace(oReg,"_"));
		if (eventTarget.split("$")[2] == "Botonera"){
		    var strBoton = oElement.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "pdf": //Boton exportar pdf
				{
					bEnviar = false;
					Exportar('PDF');
					break;
				}
				case "excel": //Boton exportar excel
                {				
					bEnviar = false;
					ExportarOpen('EXC');
					break;
				}				
			}
		}

		var theform;
		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
			theform = document.forms[0];
		}
		else {
			theform = document.forms["frmDatos"];
		}
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar){
			theform.submit();
		}
		else{
			//Si se ha "cortado" el submit, se restablece el estado original de la botonera.
			oElement.restablecer();
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
-->
</SCRIPT>
</asp:Content>
