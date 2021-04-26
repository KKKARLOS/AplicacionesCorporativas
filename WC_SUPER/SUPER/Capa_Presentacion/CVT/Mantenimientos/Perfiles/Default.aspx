<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Mantenimientos_Perfiles_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strCombo = "<%=strHTMLCombo %>";
</script>
<div id="principal" style="margin-top:40px;">
    <table id="tblTitulo" style="height:17px; margin-top:20px; margin-left:140px;" width="700px" >
        <colgroup>
            <col style='width:20px;' />
            <col style='width:430px;' />
            <col style='width:50px;' />
            <col style='width:100px;' />
            <col style='width:100px;' />
        </colgroup>
        <tr class="TBLINI">
            <td>
            </td>
	        <td style="padding-left:5px">
		        Descripción
                &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblCatalogo',1,'divCatalogo','imgLupa1')" height="11" 
                src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblCatalogo',1,'divCatalogo','imgLupa1',event)" height="11" 
                src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">				    				    
		    </td>	
            <td style="text-align:center;">
		        RH
            </td>
            <td style="padding-left:5px">
		        Abreviatura
            </td>
            <td style="padding-left:5px">
		        Nivel
            </td>
        </tr>
    </table>       
    <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; WIDTH: 716px; height:396px; margin-left:140px;" runat="server"  name="divCatalogo" onscroll="scrollTabla()">
        <div style="background-image:url('../../../../Images/imgFT22.gif'); background-repeat:repeat; width:700px; height:auto;">
                <%=strTablaHTML%>
        </div>
    </div>
    <table id="tblResultado" style="height:17px; margin-left:140px;"  width="700px">
        <tr class="TBLFIN">
            <td>
            </td>
        </tr>
    </table>
    <%--<center>--%>
    <table style="margin-left:280px;width:450px; margin-top:10px;">
        <tr>
            <td align="center">
                <button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                 onmouseover="se(this, 25);mostrarCursor(this);" style="margin-left:10px; margin-top:5px;">
	                <img src="../../../../Images/Botones/imgNuevo.gif" /><span title='Nuevp perfil'>Nuevo</span>
                </button>
            </td>
            <td align="center">
                <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
	                <img src="../../../../Images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                </button>
            </td>
        </tr>
    </table>
    <%--</center>--%>
</div>
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