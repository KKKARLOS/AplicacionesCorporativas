<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="GASVI.BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Location="None" VaryByParam="None" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <title> ::: GASVI 2.0 ::: - Detalle de aviso</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css">
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
    <div id="procesando" style="Z-INDEX: 103; VISIBILITY: visible; WIDTH: 152px; POSITION: absolute; TOP: 200px;  LEFT: 380px ;HEIGHT: 33px">
        <asp:Image ID="imgProcesando" runat="server" Height="33" Width="152" ImageUrl="~/images/imgProcesando.gif" />
        <div id="reloj" style="Z-INDEX: 104; WIDTH: 32px; POSITION: absolute; TOP: 1px;  LEFT: 118px; HEIGHT: 32px">
            <asp:Image ID="Image1" runat="server" Height="32" Width="32" ImageUrl="~/images/imgRelojAnim.gif" />
        </div>
    </div>
    <script language="JavaScript">
        var strServer = "<% =Session["GVT_strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bSalir = false;
    </script> 
    <br /><br />
    <center>
        <table border='0' width='98%' cellspacing='0' cellpadding='0' style="margin-top:5px">
        <tr>
        <td width='6' height='6' background='../../../../Images/Tabla/7.gif'></td>
        <td height='6' background='../../../../Images/Tabla/8.gif'></td>
        <td width='6' height='6' background='../../../../Images/Tabla/9.gif'></td>
        </tr>
    <tr>
    <td width='6' background='../../../../Images/Tabla/4.gif'>&nbsp;</td>
    <td background='../../../../Images/Tabla/5.gif' style='padding:5px'>
    <!-- Inicio del contenido propio de la página -->
    <center>    
       
    <TABLE id="tabla" cellSpacing="0" cellPadding="0" width="880px" style="padding:10px;text-align:left">
	<TR>
		<TD>
				<br>
                <table class="texto" width="800px" cellpadding="8" border="0">
                <colgroup><col width="90px" /><col width="480px" /><col width="230px" /></colgroup>
                    <tr>
	                    <td>Denominación</td>
	                    <td>
	                        <asp:TextBox ID="txtDen" runat="server" Style="width: 450px" MaxLength="50" onKeyUp="aG(0);"></asp:TextBox>
	                    </td>
	                    <td>&nbsp;<br /></td>
                    </tr>
                    <tr>
	                    <td>Título</td>
	                    <td colspan=2>
                            <asp:TextBox ID="txtTit" runat="server" Style="width: 450px" MaxLength="50" onKeyUp="aG(0);"></asp:TextBox>
	                    </td>
                    </tr>
                    <tr>
	                    <td colspan=3>
	                        <table>
	                        <tr>
	                            <td>
	                                <FIELDSET style="width:250px;">
								        <LEGEND>Vigencia</LEGEND>
                                        <table style="text-align:center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inicio
                                                    <asp:TextBox ID="txtValIni" runat="server" style="width:60px;cursor:pointer; margin-right:23px;" Calendar="oCal" onchange="aG(0);" ></asp:TextBox>
                                                    Fin
                                                    <asp:TextBox ID="txtValFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG(0);"></asp:TextBox>
                                               </td>
                                            </tr>
                                        </table>
                                    </FIELDSET>
	                            </td>
	                            <td style="vertical-align:bottom;"><asp:CheckBox ID="chkBorrable" runat="server" Text="Borrable" onClick="aG(0);" ToolTip="" />   </td>
	                        </tr>
	                    </table>
                        </td>
                    </tr>
                    <tr>
	                    <td colspan=3>
                            <br />
                            &nbsp;Texto<br />
                        <asp:TextBox ID="txtDescripcion" SkinID=multi runat="server" TextMode="MultiLine" Rows="28" Width="865px" Height="360px" onKeyUp="aG(0);"></asp:TextBox>
	                    </td>
                    </tr>
                </table>
        </TD>
    </TR>
</TABLE>
<table width="50%" border="0" class="texto" style="text-align:center; margin-top:10px">
	<tr> 
		<td> 
		    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W85" hidefocus="hidefocus" disabled onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">Grabar</span></button>	
		  </td>
		<td>
		    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Cancelar">Grabar...</span></button>	 
		  </td>		
		<td>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span></button>	
        </td>
	  </tr>
</table>
</center>
<!--  Fin del contenido propio de la página -->
</td>
<td width='6' background='../../../../Images/Tabla/6.gif'>&nbsp;</td>
</tr>
<tr>
<td width='6' height='6' background='../../../../Images/Tabla/1.gif'></td>
<td height='6' background='../../../../Images/Tabla/2.gif'></td>
<td width='6' height='6' background='../../../../Images/Tabla/3.gif'></td>
</tr>
            </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnIdAviso" id="hdnIdAviso" value="<%=nIdAv %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<SCRIPT language="javascript">
		function __doPostBack(eventTarget, eventArgument) {
			var bEnviar = true;

			var theform;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.forms[0];
			}
			else {
				theform = document.forms["frmPrincipal"];
			}
			
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.__EVENTARGUMENT.value = eventArgument;
			if (bEnviar){
				theform.submit();
			}
//			else{
//				$I("Botonera").restablecer();
//			}
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
</SCRIPT>
</body>
</html>
