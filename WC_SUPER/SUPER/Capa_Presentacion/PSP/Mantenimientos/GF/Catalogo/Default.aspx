<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";  
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
    </script>
    
    <style type="text/css">  
    #tblDatos td{
        padding-left: 5px;
        text-align:left;
    }
    </style>    
<center>
<table style="width:902px;text-align:left">
    <colgroup><col style="width:456px"/><col style="width:416px"/></colgroup>
    <tr>
        <td>
            <label id="lblNodo" style="width:430px" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:DropDownList id="cboCR" runat="server" Width="430px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems="true">
                <asp:ListItem Value="" Text=""></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtDesNodo" style="width:427px;" Text="" readonly="true" runat="server" />
            
        </td>
    </tr>
    <tr>
        <td>
            <table id="tblTitulo" style="width: 430px; height: 17px; margin-top:5px;">
                <colgroup>
                    <col style="width:430px"/>
                </colgroup>
                <TR class="TBLINI">
                    <td>&nbsp;
                        <IMG style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgPlant" border="0"> 
                        <MAP name="imgPlant">
		                    <AREA onclick="ordenarTabla(1,0)" shape="RECT" coords="0,0,6,5">
		                    <AREA onclick="ordenarTabla(1,1)" shape="RECT" coords="0,6,6,11">
	                    </MAP>&nbsp;Denominación&nbsp;
	                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
				            height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
	                    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event)"
	                        height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            </TD>
                </TR>
            </table>
            <DIV id="divCatalogo" style="overflow: auto; width: 446px; height:460px">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:430px">
                <%=strTablaHtmlGF%>
                </DIV>
            </DIV>
            <table style="width: 430px; height: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </table>
        </td>
        <td>
		    <table id="tblTitulo2" style="width: 430px; height: 17px;margin-top:5px;">
                <colgroup>
                    <col style="width:90px"/><col style="width:340px"/>
                </colgroup>		    
			    <TR class="TBLINI">
				    <TD align=right>Integrantes</TD>
				    <TD align=right title="Responsable de grupo funcional">Resp.&nbsp;</TD>
			    </TR>
		    </table>
		    <div id="divCatalogo2" style="overflow: auto; width: 446px; height: 460px;" align="left" onscroll="scrollTablaProfAsig()">
			    <div style='background-image:url(../../../../../Images/imgFT20.gif); width:430px; height:auto'>
			    <%=strTablaHTMLIntegrantes%>
			    </div>
            </div>
            <table id="tblResultado2" style="width: 430px; height: 17px">
			    <TR class="TBLFIN">
			        <TD>
			        </TD>
			    </TR>
		    </table>
        </td>
    </tr>
    <tr>
        <td></td>
        <td style="padding-top:5px;">
            <img border="0" src="../../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> actual&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
</center>	
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<input type="hidden" name="hdnIdGrupo" id="hdnIdGrupo" value="" runat="server"/>
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" />
<input type="hidden" name="hdnEsSoloRGF" id="hdnEsSoloRGF" value="S" runat="server"/>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {

	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "nuevo": 
				{
                    bEnviar = false;
                    nuevoGF();
					break;
				}
				case "eliminar": 
				case "borrar": 
				{
                    bEnviar = false;
					//if (confirm("¿Estás conforme?")){
                        eliminarGF();
                    //}
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("GruposFuncionales.pdf");
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

