<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Mantenimientos_Titulaciones_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strComboTipo = "<%=strHTMLComboTipo %>";
    var strComboModalidad = "<%=strHTMLComboModalidad %>";
</script>
<style type="text/css">
    #tblCatalogo tr { height: 22px; cursor: pointer;}
    #tblCatalogo td { text-align:left; }
</style>
<center>
<fieldset style="width:545px; padding:5px; text-align:left;">
    <legend>Filtros</legend>
    <table style="width:535px; margin-left:10px;">
        <colgroup><col style="width:75px;"/><col style="width:310px;"/><col style="width:150px;"/></colgroup>
        <tr>
            <td><label style="width:55px;">Título</label></td>
            <td>
                <input name="txtTitulo" id="txtTitulo" class="txtM" runat="server" style="width:300px;" maxlength="100" 
                        onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}"/>
            </td>
            <td>
                <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda">
                    <asp:ListItem style="cursor:pointer;" Value="I" onclick="buscar();">
                        <img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus="hidefocus">
                    </asp:ListItem>
                    <asp:ListItem style="cursor:pointer;" Selected="True" Value="C" onclick="buscar();">
                        <img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus="hidefocus">
                    </asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td><label style="width:52px;">Estado</label></td>
            <td>
                <asp:DropDownList id="cboEstado" runat="server" style="width:110px; margin-top:5px;" AppendDataBoundItems="true" CssClass="combo" onchange="buscar();">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Selected="True" Text="No validado" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Validado" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
    </table>
</fieldset>
<div id="principal" style="width:930px; margin-top:15px; text-align:left;">
    <!--Header Table-->
    <table id="tblTitulo" style="width: 910px; BORDER-COLLAPSE: collapse; HEIGHT: 17px; text-align:left; margin-top:15px;" cellspacing="0" cellpadding="0" border="0">
	    <tr class="TBLINI">
	        <td width="20px"></td>
		    <td width="440px">Denominación
                &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblCatalogo',1,'divCatalogo','imgLupa1')" height="11" 
                src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblCatalogo',1,'divCatalogo','imgLupa1',event)" height="11" 
                src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
		    </td>
		    <td width="120px">Tipo</td>
		    <td width="120px">Modalidad</td>
		    <td width="70px"  style="text-align:center;">Tíc</td>
		    <td width="70px"  style="text-align:center;">Estado</td>
		    <td width="70px"  style="text-align:center;">RH</td>
	    </tr>
    </table>	
    	    		    		    
    <!--Content-->		    
    <div id="divCatalogo" style="overflow-x:hidden;overflow: auto; width: 926px; height:396px" runat="server" onscroll="scrollTabla()">
    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:910px;">    
        <%=strTablaHTML%>
    </div>
    </div>
    
    <!--Footer Table-->		    
    <table id="Table4" style="WIDTH: 910px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellspacing="0" border="0">
	    <tr class="TBLFIN">
		    <td style="padding-left:20px; text-align:left;">
		    </td>
	    </tr>
    </table>
    <center>
    <table style="width:350px; margin-top:10px;">
        <tr>
            <td align="center">
                <button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                 onmouseover="se(this, 25);mostrarCursor(this);">
	                <img src="../../../../Images/Botones/imgNuevo.gif" /><span title='Nueva Titulación'>Nuevo</span>
                </button>
            </td>
            <td align="center">
                <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                 onmouseover="se(this, 25);mostrarCursor(this);">
	                <img src="../../../../Images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                </button>
            </td>
            <td align="center">
                <button id="btnReasignar" type="button" onclick="reasignar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
	                 onmouseover="se(this, 25);mostrarCursor(this);">
	                <img src="../../../../Images/botones/imgReasignar.gif" /><span title="Permite reasignar los profesionales asociados a una titulación a otra titulación">Reasignar</span>
                </button>
            </td>
        </tr>
    </table>
    </center>
</div>
</center>
<!-- User Controls-->
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {

                case "grabar":
                    {
                        bEnviar = false;
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
-->
</script>
</asp:Content>

