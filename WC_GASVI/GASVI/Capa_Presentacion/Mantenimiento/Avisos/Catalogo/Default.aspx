<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="GASVI.BLL"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table id="tbl1" height="20" width="716px" style="text-align:left"  cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td>
	        <TABLE id="tblTitulo" style="WIDTH: 700px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellpadding="0" cellSpacing="0" border="0">
		        <TR class="TBLINI">
			        <td width="495px" style="padding-left:5px">Denominación
				        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
				        <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event);" 
				            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
					</TD>
					<td width="100px" style="text-align:right">Inicio Vigencia</td>
					<td width="100px" style="text-align:right">Fin Vigencia</td>
		        </TR>
	        </TABLE>
	        <DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 716px; height:400px">
	            <%=strTablaHtmlGF%>
            </DIV>
	        <TABLE id="Table3" style="WIDTH: 700px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" border="0">
		        <TR class="TBLFIN">
			        <TD></TD>
		        </TR>
	        </TABLE>
        </td>
    </tr>
    <tr>
        <td><br />
            <asp:TextBox ID="txtTexto" SkinID=multi runat="server" TextMode="MultiLine" Rows="7" Width="700px" ReadOnly/>
        </td>
    </tr>
</table>
</center>	
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_mmoff:mmoff ID="mmoff2" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script language="javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "nuevo": 
                    bEnviar = false;
                    nuevoAviso();
					break;
				//case "eliminar": 
                //    bEnviar = false;
				//	if (confirm("¿Está Ud. conforme?")){
                //        eliminarAviso();
                //    }
				//	break;
			    case "eliminar":
			        bEnviar = false;
			        jqConfirm("", "Deseas borrar los elementos seleccionados?", "", "", "war", 350).then(function (answer) {
			            if (answer) {
			                eliminarAviso();
			            }
			            fSubmit(bEnviar, eventTarget, eventArgument);
			        });
			        break;
		    }
			if (strBoton != "eliminar") fSubmit(bEnviar, eventTarget, eventArgument);
		}
	}
	function fSubmit(bEnviar, eventTarget, eventArgument) {
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

