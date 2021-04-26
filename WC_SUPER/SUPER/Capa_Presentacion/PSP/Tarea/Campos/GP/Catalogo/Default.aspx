<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title> ::: SUPER ::: - Mantenimiento de equipos de profesionales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link  href="../../../../../../PopCalendar/CSS/Classic.css"type="text/css" rel="stylesheet" />
    <script src="../../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="../../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../../../../../PopCalendar/PopCalendar.js"type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
    .txtLMA  /* Caja de texto transparente con cursor mano azul 2*/
    {
        border: 0px;
        padding: 2px 0px 0px 2px;
        margin: 0px;
        font-size: 11px;
        background-color: Transparent;
        font-family: Arial, Helvetica, sans-serif;
        height: 14px;
        cursor: url('../../../../../../images/imgManoAzul2.cur'),url('../../../../../images/imgManoAzul2.cur'),url('../../../../images/imgManoAzul2.cur'),url('../../../images/imgManoAzul2.cur'),url('../../images/imgManoAzul2.cur'),url('../images/imgManoAzul2.cur'),pointer;
    }
    #tblDatos tr { height: 20px; }
    #tblOpciones2 tr { height: 20px; }
    </style>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" name="form1" runat="server" action="Default.aspx" method="POST" enctype="multipart/form-data">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
</script>  
<center>
<table style="width:902px;text-align:left; margin-top:15px;">
    <colgroup><col style="width:456px"/><col style="width:416px"/></colgroup>
    <tr>
        <td>
            <table id="tblTitulo" style="width: 430px; height: 17px; margin-top:5px;">
                <colgroup>
                    <col style="width:430px"/>
                </colgroup>
                <TR class="TBLINI">
                    <td>&nbsp;
                        <IMG style="CURSOR: pointer" height="11" src="../../../../../../Images/imgFlechas.gif" width="6" useMap="#imgPlant" border="0"> 
                        <MAP name="imgPlant">
						    <AREA onclick="ot('tblDatos', 1, 0, '', '')" shape="RECT" coords="0,0,6,5">
						    <AREA onclick="ot('tblDatos', 1, 1, '', '')" shape="RECT" coords="0,6,6,11">
	                    </MAP>&nbsp;Denominación&nbsp;
	                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
				            height="11" src="../../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
	                    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event)"
	                        height="11" src="../../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            </TD>
                </TR>
            </table>
            <div id="divCatalogo" style="overflow: auto; width: 446px; height:420px">
                <div style="background-image:url(../../../../../../Images/imgFT20.gif); width:430px">
                <%=strTablaHtmlGP%>
                </div>
            </div>
            <table style="width: 430px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
        <td>
		    <table id="tblTitulo2" style="width: 430px; height: 17px;margin-top:5px;">
                <colgroup>
                    <col style="<col style="width:430px"/>
                </colgroup>		    
			    <TR class="TBLINI">
				    <TD>&nbsp;Integrantes</TD>
			    </TR>
		    </table>
		    <div id="divCatalogo2" style="overflow: auto; width: 446px; height: 420px;" onscroll="scrollTablaProfAsig()">
			    <div style='background-image:url(../../../../../../Images/imgFT20.gif); width:430px; height:auto'>
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
        <td>
            <table style="width:420px; margin-top:10px; margin-left:20px; text-align:left;">
                <colgroup><col style="width:150px;" /><col style="width:150px;" /><col style="width:120px;" /></colgroup>
	            <tr>
		            <td>
			            <button id="btnAnadir" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					            onmouseover="se(this, 25);mostrarCursor(this);">
					            <img src="../../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			            </button>	
		            </td>
		            <td>
			            <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					            onmouseover="se(this, 25);mostrarCursor(this);">
					            <img src="../../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			            </button>	
		            </td>			
		            <td>
			            <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					            onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			            </button>	
		            </td>
	            </tr>
            </table>
        </td>
		<td style="padding-top:15px; word-spacing:3px;" colspan="3">
            &nbsp;<img class="ICO" src="../../../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
		</td>
    </tr>
</table>
</center>
	
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
<input type="hidden" id="hdnIdGrupo" value="" runat="server"/>
<input type="hidden" id="hdnErrores" value="<%=sErrores%>" />
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
