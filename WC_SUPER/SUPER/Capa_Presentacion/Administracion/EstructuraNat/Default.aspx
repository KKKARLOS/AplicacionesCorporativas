<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_EstructuraNat_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var nNEAux = <%= nNE.ToString() %>;
</script>
<center>
    <table style="width:520px; text-align:left; height: 17px;">
    <colgroup>
    <col style="width:130px;" />
    <col style="width:40px;" />
    <col style="width:40px;" />
    <col style="width:40px;" />
    <col style="width:120px;" />
    <col style="width:150px;" />
    </colgroup>
    <tr style="height:35px;">
        <td>
            <img id="imgNE1" src='../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"><img id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);"><img id="imgNE4" src='../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);">
        </td>
        <td>
			<button id="btnTipologia" type="button" onclick="insertarItem(1);" class="btnH25W35" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/imgTipologia.gif" /><span title=""></span>
			</button>		
		</td> 
        <td>
			<button id="btnGrupo" type="button" onclick="insertarItem(2);" class="btnH25W35" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/imgGrupo.gif" /><span title=""></span>
			</button>			
		</td>   
        <td>
			<button id="btnSubgrupo" type="button" onclick="insertarItem(3);" class="btnH25W35" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/imgSubgrupo.gif" /><span title=""></span>
			</button>			
		</td>   		
        <td>
			<button id="btnNaturaleza" type="button" onclick="insertarItem(4);" class="btnH25W35" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/imgNaturaleza.gif" /><span title=""></span>
			</button>			
		</td>   
        <td style="text-align:right; padding-right: 20px">
            <asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar inactivos" /> <input type=checkbox id="chkMostrarInactivos" class="check" onclick="MostrarInactivos();" />
        </td>
    </tr>
    <tr><td colspan="6">
        <TABLE id="tblTitulo" style="WIDTH: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px;" cellspacing="0" cellpadding="0" border="0">
            <TR class="TBLINI">
                <td style="padding-left:35px;">Denominación&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY:none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
            </TR>
        </TABLE>
        <DIV id="divCatalogo" style="OVERFLOW: auto; WIDTH: 516px; height:460px;" runat="server">
        </DIV>
        <TABLE id="tblResultado" style="WIDTH: 500px; HEIGHT: 17px;">
            <TR class="TBLFIN">
                <TD>&nbsp;</TD>
            </TR>
        </TABLE>
    </td></tr>
    </table>
    <br />
    <img border="0" src="../../../Images/imgTipologia.gif" />&nbsp;Tipología proyecto&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgGrupo.gif" />&nbsp;Grupo naturaleza&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSubgrupo.gif" />&nbsp;Subgrupo naturaleza&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgNaturaleza.gif" />&nbsp;Naturaleza
   </center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "eliminar":
                    {
                        bEnviar = false;
                        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
                            if (answer) {
                                eliminar();
                            }
                            fSubmit(bEnviar, eventTarget, eventArgument);
                            return;
                        });
                        break;
                    }
            }
            if (strBoton != "eliminar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
    function fSubmit(bEnviar, eventTarget, eventArgument)
    {
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

