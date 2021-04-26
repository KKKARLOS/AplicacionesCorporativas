<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Clonar mes</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="/../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	    var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var bLectura = <%=sLectura%>;
	    var bLecturaInsMes = <%=sLecturaInsMes%>;
    	var sUltCierreEcoNodo = opener.sUltCierreEcoNodo;
	    var sEstado = "<%=Request.QueryString["sEstadoProy"]%>";
        var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
        var bConsPersonas = true;
        var bConsNivel = true;
        var bProdProfesional = true;
        var bProdPerfil = true;
        var bAvance = true;
        var bPeriodificacionC = true;
        var bPeriodificacionP = true;
        var sClasesClonables = "<%=sClasesClonables %>";
    </script>
    <center>
    <table id="Table2" cellpadding="3px" style="width: 630px; text-align:left">
        <colgroup>
            <col style="width: 400px;" />
            <col style="width: 160px;" />
            <col style="width: 70px;" />
        </colgroup>
        <tr>
            <td style="vertical-align:bottom;">
                <img id="imgPM" title="Primermes" src="../../../Images/btnPriRegOff.gif" onclick="cambiarMes('P')" style="cursor: pointer;vertical-align:bottom" border="0" />
                <img id="imgAM" title="Mes anterior" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer;vertical-align:bottom" border="0" />
                <asp:TextBox ID="txtMesBase" style="width:90px; text-align:center; vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
                <img id="imgSM" title="Siguiente mes" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer;vertical-align:bottom" border="0" />
                <img id="imgUM" title="Último mes" src="../../../Images/btnUltRegOff.gif" onclick="cambiarMes('U')" style="cursor: pointer;vertical-align:bottom" border="0" />
                <img id="imgArbol" src="../../../Images/imgArbolClases.gif" border="0" style="position:absolute; top: 30px; left:330px; cursor:pointer;" title="Selección de datos a clonar" onclick="getClasesClonar()" />
            </td>
            <td>
                <fieldset style="width: 145px; height:60px;">
                    <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
                        <table style="width:135px;" cellpadding="4px" >
                            <colgroup><col style="width:40px;"/><col style="width:95px;"/></colgroup>
                            <tr>
                                <td>Inicio</td>
                                <td>
                                    <asp:TextBox ID="txtDesde" style="width:90px;" Text="" readonly="true" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Fin</td>
                                <td>
                                    <asp:TextBox ID="txtHasta" style="width:90px;" Text="" readonly="true" runat="server" />
                                </td>
                            </tr>
                        </table>
                </fieldset>
            </td>
            <td>
                <img id="imgCaution" src="../../../Images/imgCaution.gif" border="0" style=" margin-left:10px; vertical-align:bottom; display:none;" title="" />         
            </td>
        </tr>
    </table>
    <table style="width: 630px; text-align:left">
    <colgroup>
            <col style="width: 400px;" />
            <col style="width: 230px;" />
    </colgroup>
    <tr>
        <td>
            <table style='width: 350px; height: 17px; margin-top:5px;'>
            <colgroup>
                <col style='width:150px;' />
                <col style='width:100px;' />
                <col style='width:100px;' />
            </colgroup>
            <tr class='TBLINI'>
                <td style='padding-left:5px;'>Mes de referencia</td>
                <td style='text-align:right;'>Consumos</td>
                <td style='text-align:right;padding-right:2px;'>Producción</td>
            </tr>
            </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 366px; height:360px" runat="server">
            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:350px">
		    <%=strTablaHTML%>
		    </div>
		    </div>
		    <table  style="width: 350px;height: 17px;">
			    <tr class="TBLFIN">
				    <td>&nbsp;</td>
			    </tr>
		    </table>
        </td>
        <td>
            <table style='width: 200px; height: 17px; margin-top:5px;'>
            <colgroup>
                <col style='width:40px;' />
                <col style='width:160px;' />
            </colgroup>
            <tr class='TBLINI'>
                <td style='padding-left:5px;'>Clonar</td>
                <td style='padding-left:5px;'>Mes</td>
            </tr>
            </table>

		    <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 216px; height:360px" runat="server">
            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:200px">
                <table id='tblDatos2' style='width: 200px;'>
                <colgroup>
                    <col style='width:40px;' />
                    <col style='width:160px; ' />
                </colgroup>
                </table>
		    </div>
		    </div>
		    <table id="Table1" style="width: 200px; height: 17px;">
			    <tr class="TBLFIN">
				    <td>&nbsp;</td>
			    </tr>
		    </table>
        </td>
    </tr>
    </table>
    <table width="290px" style="margin-top:25px;">
		<tr>
			<td>
                <button id="btnProcesar" type="button" onclick="Procesar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/botones/imgProcesar.gif" /><span>Procesar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/botones/imgSalir.gif" /><span>Salir</span>
                </button>
			</td>
		</tr>
    </table>
    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
    <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
    <div class="clsDragWindow" id="DW" noWrap></div>
    </form>
<script type="text/javascript">
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
</body>
</html>
