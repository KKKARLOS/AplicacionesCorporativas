<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Mantenimientos_Idiomas_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">
    #tblCatalogo tr { height: 22px; cursor: pointer;}
    #tblCatalogo td { text-align:left; padding: 0px 2px 0px 2px;}
</style>
<center>
<br /><br />
<table border="0" cellspacing="0" class="texto" style="width: 520px;border-collapse: collapse;">
    <tr>
        <td>
            <!--Header Table-->
		    <table id="tblTitulo" style="width: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px; margin-top:5px;" cellspacing="0" border="0">
			    <tr class="TBLINI">
				    <td style="padding-left:20px; text-align:left;">Denominación
                        &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblCatalogo',1,'divCatalogo','imgLupa1')" height="11" 
                        src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                        <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblCatalogo',1,'divCatalogo','imgLupa1',event)" height="11" 
                        src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">				    				    
				    </td>				    
			    </tr>
		    </table>	
		    	    		    		    
		    <!--Content-->		    
		    <div id="divCatalogo" style="overflow-x:hidden; overflow: auto; width: 516px; height:418px" runat="server" onscroll="scrollTabla()">
		    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif);">    
                <%=strTablaHTML%>
            </div>
		    </div>
		    
		    <!--Footer Table-->		    
		    <table id="Table4" style="WIDTH: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellspacing="0" border="0">
			    <tr class="TBLFIN">
				    <td style="padding-left:20px; text-align:left;"></td>				    
			    </tr>
		    </table>
        </td>
    </tr>
</table>
<table style="margin-left:70px;width:380px; margin-top:15px;">
        <tr>
            <td>
                <button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                 onmouseover="se(this, 25);mostrarCursor(this);" style="margin-left:30px; margin-top:5px;">
	                <img src="../../../../Images/Botones/imgNuevo.gif" /><span title='Nueva Titulación'>Nuevo</span>
                </button>
            </td>
            <td>
                <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:-25px;display:inline-block; margin-top:5px;">
	                <img src="../../../../Images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                </button>
            </td>
        </tr>
    </table>
</center>

<!--User controls-->
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


