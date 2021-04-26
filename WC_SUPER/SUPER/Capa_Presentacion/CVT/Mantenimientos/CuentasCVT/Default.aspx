<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_CuentasCVT_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strCombo = "<%=strHTMLCombo %>";
</script>
<center>
<fieldset style="width:545px; padding:5px; text-align:left;">
<legend>Filtros</legend>
    <table style="width:535px; margin-left:10px;">
        <colgroup><col style="width:75px;"/><col style="width:310px;"/><col style="width:150px;"/></colgroup>
        <tr>
            <td><label style="width:70px;">Denominación</label></td>
            <td>
                <input name="txtDenominacion" id="txtDenominacion" class="txtM" runat="server" style="width:300px; " 
                        maxlength="100" onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}"/>
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
            <td><label style="width:70px;">Estado</label></td>
            <td>
                <asp:DropDownList id="cboEstado" runat="server" style="width:110px;" AppendDataBoundItems="true" CssClass="combo" onchange="buscar();">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Selected="True" Text="No validado" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Validado" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
    </table>
</fieldset>
<div id="principal" style="width:720px; margin-top:15px; text-align:left;">
    <table id="tblTitulo" style="height:17px; margin-top:20px;" width="700px" >
        <colgroup>
            <col style='width:20px;' />
            <col style='width:300px;' />
            <col style='width:330px;' />
            <col style='width:50px;' />
        </colgroup>
        <tr class="TBLINI">
            <td>
            </td>
	        <td style="padding-left:5px" title=" Nombre del cliente que no pertenece al grupo Ibermática.">
		        Denominación
                &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblCatalogo',1,'divCatalogo','imgLupa1')" height="11" 
                src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblCatalogo',1,'divCatalogo','imgLupa1',event)" height="11" 
                src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">			        
            </td>
            <td style="padding-left:5px">
		        Sector/Segmento
                &nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblCatalogo',2,'divCatalogo','imgLupa2')" height="11" 
                src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblCatalogo',2,'divCatalogo','imgLupa2',event)" height="11" 
                src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">			        
            </td>
            <td style="text-align:center;">
		        Estado
            </td>
        </tr>
    </table>       
    <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; WIDTH: 716px; height:396px;" runat="server"  name="divCatalogo" onscroll="scrollTabla()">
        <div style="background-image:url('../../../../Images/imgFT22.gif'); background-repeat:repeat; width:700px; height:auto;">
                <%=strTablaHTML%>
        </div>
    </div>
    <table id="tblResultado" style="width:700px; height:17px;">
        <tr class="TBLFIN">
            <td>
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
	                <img src="../../../../Images/botones/imgReasignar.gif" /><span title="Permite reasignar los profesionales asociados a un cliente no Ibermática a otro cliente">Reasignar</span>
                </button>
            </td>
        </tr>
    </table>
    </center>
</div>
</center>
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
                        grabar();
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
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
</script>
</asp:Content>

