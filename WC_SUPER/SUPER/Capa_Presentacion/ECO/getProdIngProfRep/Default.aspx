<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <div id="divMonedaImportes" runat="server" style="position:absolute; top:5px; left:700px; width:300px; visibility:hidden;">
        <table style="width:300px; margin-top:15px; text-align:left" cellpadding="3">
        <tr>
            <td>Moneda proyecto: <label id="lblMonedaProyecto" runat="server"></label></td>
        </tr>
        <tr>
            <td><label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server"></label></td>
        </tr>
        </table>
	    <script type="text/javascript">
	        var sMonedaProyecto = "<%=sMonedaProyecto%>";
	        var sMonedaImportes = "<%=sMonedaImportes%>";
        </script>
    </div>
    <form id="form1" runat="server">
	<script type="text/javascript">

        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var bLectura = <%=sLectura%>;
	    var bLecturaInsMes = <%=sLecturaInsMes%>;
	    var sModoCoste = "<%=sModoCoste%>";
    	var sUltCierreEcoNodo = opener.sUltCierreEcoNodo;
	    var sEstado = "<%=Request.QueryString["sEstadoProy"]%>";
	    var sCualidad = "<%=Request.QueryString["sCualidad"]%>";

    </script>
	<center>
        <table cellpadding="3" style="width: 980px; margin-top:15px; text-align:left">
            <colgroup>
                <col style="width:450px" />
                <col style="width:460px" />
                <col style="width:70px" />
            </colgroup>
            <tr>
                <td>
                    Origen de profesionales <asp:DropDownList ID="cboRecursos" runat="server" CssClass="combo" style="width:250px;" onchange="getProfesionales(this.value)">
                    </asp:DropDownList>
                </td>
                <td style="vertical-align:bottom;">
                    <img id="imgPM" title="Primermes" src="../../../Images/btnPriRegOff.gif" onclick="cambiarMes('P')" style="cursor: pointer;vertical-align:bottom" border="0" />
                    <img id="imgAM" title="Mes anterior" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer;vertical-align:bottom" border="0" />
                    <asp:TextBox ID="txtMesBase" style="width:90px; text-align:center;vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
                    <img id="imgSM" title="Siguiente mes" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer;vertical-align:bottom" border="0" />
                    <img id="imgUM" title="?ltimo mes" src="../../../Images/btnUltRegOff.gif" onclick="cambiarMes('U')" style="cursor: pointer;vertical-align:bottom" border="0" />
                </td>
                <td><img id="imgNoCoste" src="../../../Images/imgNoCoste.gif" border="0" title="Proyecto asociado a naturaleza sin coste." style="visibility:hidden;" /></td>
            </tr>
        </table>
    <table style="width: 980px; text-align:left; margin-top:15px;">
    <tr>
        <td>
            <table style="width: 960px; height: 17px; margin-top:15px;">
            <colgroup>
                <col style="width:30px;"/>
                <col style="width:50px;"/>
                <col style="width:520px;"/>
                <col style="width:120px;"/>
                <col style="width:120px;"/>
                <col style="width:120px;"/>
            </colgroup>  
            <tr class='TBLINI'>                       
                <td style='padding-left:15px;'>&nbsp;</td>
                <td>N? Prof.</td>
                <td>Profesional</td>
                <td style='text-align:right;'><label id="lblModoCoste"></label></td>
                <td style='text-align:right;'><label id="lblUnidades"></label></td>
                <td style='text-align:right; padding-right:2px;'>Importe</td>
                </tr>
            </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 976px; height:360px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:960px">
		            <%=strTablaHTML%>
		        </div>
		    </div>
		    <table id="tblParcial" style="width: 960px; height: 17px;">
		    <colgroup>
                <col style="width:720px;"/>
                <col style="width:120px;"/>
                <col style="width:120px;"/>
            </colgroup>				    
			    <tr class="TBLFIN">
				    <td style="padding-left:5px;">Parciales por origen</td>
				    <td align="right" style="padding-right:2px;"><label id="lblParcialUnidades" class="texto">0,00</label></td>
				    <td align="right" style="padding-right:2px;"><label id="lblParcialImporte" class="texto">0,00</label></td>
			    </tr>
		    </table>
		    <table id="tblTotal" style="width: 960px; height: 17px;">
		    <colgroup>
                <col style="width:720px;"/>
                <col style="width:120px;"/>
                <col style="width:120px;"/>
            </colgroup>			    
			    <tr class="TBLFIN">
				    <td style="padding-left:5px;">Totales</td>
				    <td align="right" style="padding-right:2px;"><label id="lblTotalUnidades" class="texto">0,00</label></td>
				    <td align="right" style="padding-right:2px;"><label id="lblTotalImporte" class="texto">0,00</label></td>
			    </tr>
		    </table>
            <br />
            &nbsp;<img border="0" src="../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
            <nobr id="nbrExterno" style="visibility:hidden"><img border="0" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo</nobr>&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" src="../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">For?neo</label>
            <center>
                <table width="250px" align="center" style="margin-top:10px;" border="0" cellpadding=0 cellspacing=0>
		            <tr>
		                <td align="center">
			                <button id="btnExcel" type="button" onclick="mostrarProcesando();setTimeout('excel()',20)" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				                 onmouseover="se(this, 25); mostrarCursor(this);">
				                <img src="../../../images/botones/imgExcel.gif" /><span title="Exportar a excel">Exportar</span>
			                </button>			
		                </td>
                        <td align="center">
		                    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			                     onmouseover="se(this, 25);mostrarCursor(this);">
			                    <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		                    </button>	 
                        </td>
		            </tr>
                </table>
            </center>
        </td>
    </tr>
    </table>
    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
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
