<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Entornos de una familia</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
   	<script src="../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
  	<script src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
   	<script src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
        mostrarProcesando();
    </script> 
<br />      
<center>
    <br />
    <label class="texto">Familia</label>
    <asp:TextBox id="txtDenFamilia" class="txtL" style="width:400px; margin-left:5px;" MaxLength="50" runat="server" onkeyup="activarGrabar()"></asp:TextBox>
    <table style="width:630px; height:520px; margin-top:15px;" border="0">
    <colgroup>
        <col style="width:283px;" />
        <col style="width:40px;" />
        <col style="width:303px;" />
    </colgroup>
        <tr style="height:40px;">
            <td style="text-align:left;">
                Denominación<br />
                <asp:TextBox ID="txtConcepto" runat="server" Width="270px" onKeyPress="javascript:if(event.keyCode==13){bMsg = false;buscarConcepto(this.value);event.keyCode=0;;return false;}" />
			</td>
            <td colspan="2" valign="bottom">
	            <asp:RadioButtonList ID="rdbTipo" SkinId="rbl" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda" onclick="buscarConcepto($I('txtConcepto').value);" style="">
		            <asp:ListItem Value="I"><img src='../../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$I('rdbTipo_0').click();"></asp:ListItem>
		            <asp:ListItem Selected="True" Value="C"><img src='../../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$I('rdbTipo_1').click();"></asp:ListItem>
	            </asp:RadioButtonList>
			</td>
        </tr>
        <tr>
            <td style="vertical-align:top;">
                <fieldset style="width:270px; text-align:left; height:445px;">  
                <legend>Entornos</legend>   
                    <table id="tblTitulo" style="height:33px; width:250px;">
                        <tr>
                            <td  style="text-align:right;">
                                <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblEntornos')" />
                                &nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblEntornos')" />
                            </td>
                        </tr>
                        <tr class="TBLINI">
                            <td style="padding-left:5px">
                                Denominación
                                &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblEntornos',1,'divEntornos','imgLupa1')" height="11" 
                                src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                                <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblEntornos',1,'divEntornos','imgLupa1',event)" height="11" 
                                src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">				    				    
                            </td>	
                        </tr>
                    </table>       
                    <div id="divEntornos" style="overflow-x:hidden; overflow-y:auto; width:266px; height:384px;" runat="server"  name="divCatalogo">
                        <div style="background-image:url('../../../../../Images/imgFT16.gif'); background-repeat:repeat; width:250px; height:auto;">
                                <%=strTablaHTMLEnt%>
                        </div>
                    </div>
                    <table id="tblResultado" style="height:17px; width:250px;">
                        <tr class="TBLFIN"><td></td></tr>
                    </table>
                </fieldset>                 
            </td>
            <td style="vertical-align:middle; padding-left:2px;">
                <asp:Image id="imgPapeleraEntornos" style="cursor: pointer" runat="server" 
                        ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" 
                        onmouseover="setTarget(this);" caso="3">
                </asp:Image>
            </td>
            <td  style="vertical-align:top;">
                <fieldset style="width:290px; text-align:left; height:445px;">  
                <legend>Entornos de la familia</legend>   
                    <table id="Table1" style="height:33px; width:270px;">
                        <tr>
                            <td style="text-align:right;">
                                <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblEntFam')" />
                                &nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblEntFam')" />
                            </td>
                        </tr>
                        <tr class="TBLINI">
                            <td style="padding-left:5px">
                                Denominación				    				    
                            </td>	
                        </tr>
                    </table>       
                    <div id="divEntFam" style="overflow-x:hidden; overflow-y:auto; width:286px; height:384px;" runat="server" name="divEntFam" 
                            target="true" onmouseover="setTarget(this);" caso="1" >
                        <div style="background-image:url('../../../../../Images/imgFT16.gif'); background-repeat:repeat; width:270px; height:auto;">
                            <%=strTablaHTMLFamEnt%>
                        </div>
                    </div>
                    <table id="Table2" style="height:17px; width:270px;"><tr class="TBLFIN"><td></td></tr></table>
                </fieldset>                 
            </td>
        </tr>
    </table>
    <table id="tblBotones" style="margin-left:15px; width:260px;">
        <tr>
            <td>
		        <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);">
			        <img src="../../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
		        </button>	
            </td>
            <td>
		        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);">
			        <img src="../../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		        </button>	 
            </td>
        </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnIdFamilia" id="hdnIdFamilia" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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

