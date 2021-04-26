<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Solicitud de creación de certificado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>    
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funciones.js?21062018" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/documentos.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
        var sNumEmpleado = "<% =Session["IDFICEPI_CVT_ACTUAL"].ToString() %>";
        var es_administrador = "<%=Session["ADMINISTRADOR_CVT_ACTUAL"].ToString() %>";
        var bNueva = <%= (nIdSolicitud == 0)? "true":"false" %>;
        var sFechaHoy = "<% =DateTime.Today.ToShortDateString() %>";
        var sIDDocuAux = "<% =sIDDocuAux %>";
    </script>
    <div style="width:665px; margin-left:20px; margin-top:10px;">
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="655px">
            <tr>
                <td background="../../../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../../../Images/Tabla/5.gif" style="padding: 5px 15px 15px 15px; text-align:justify; line-height:150%;">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    Para incorporar un nuevo certificado, es necesario indicar tanto su denominación como unas observaciones 
                    en las que se detalle información relativa a la entidad certificadora, el entorno tecnológico, los exámenes realizados 
                    para su obtención así como un documento acreditativo de certificado o examen.
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
    <table style="width:660px;text-align:left;margin-left:20px;" class="texto">
	<tr>
		<td>
		    <fieldset style="width:640px; margin-top:10px;">
		        <legend>Nuevo certificado</legend>
                <table id="tabla" style="width:630px; text-align:left; margin-top:10px;" cellpadding="5px">
                    <colgroup>
                        <col style="width:100px;" />
                        <col style="width:530px;" />
                    </colgroup>
                    <tr>
                        <td style="text-align:left;">
                            <label id="lblProy" class="texto" >Denominación</label>
                            <label style="color:Red;margin-left:3px">*</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDen" style="width:510px;" MaxLength="200" Text="" runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top;">
                            <label id="Label1" class="texto">Observaciones</label>
                            <label style="color:Red;margin-left:3px">*</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtObs" SkinID="multi" runat="server" TextMode="MultiLine" Rows="9" Width="510px" 
                                watermarktext="Indique entidad certificadora, entorno tecnológico y los exámenes realizados"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
	<tr>
		<td>
            <fieldset style="width:640px; margin-top:15px;">
            <legend>Documentos acreditativos<label style="color:Red;margin-left:3px">*</label></legend>
                <table style="width: 620px; height:17px; margin-top:10px;">
                    <colgroup>
                        <col style="width:310px;" />
                        <col style="width:310px;" />
                    </colgroup>
                    <tr class="TBLINI">
	                    <td style="padding-left:3px;">Descripción</td>
	                    <td>Archivo</td>
                    </tr>
                </table>
                <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 636px; height:100px" runat="server">
                    <div style='background-image:url(../../../../../Images/imgFT20.gif); width:620px'>
                    </div>
                </div>
                <table id="Table3" style="width: 620px; height:17px;">
                    <tr class="TBLFIN"><td></td></tr>
                </table>
                <table style="width:260px;margin-top:10px; margin-left:175px;">
                    <colgroup>
                        <col style="width:130px;" />
                        <col style="width:130px;" />
                    </colgroup>
                    <tr>
                        <td align="center">
					        <button id="btnAddDoc" type="button" onclick="addDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
						         onmouseover="se(this, 25);mostrarCursor(this);">
						        <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
					        </button>													
                        </td>
                        <td align="center">
					        <button id="btnDelDoc" type="button" onclick="delDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
						         onmouseover="se(this, 25);mostrarCursor(this);">
						        <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar documento">Eliminar</span>
					        </button>													
                        </td>
                    </tr>
                </table>
                <iframe id="iFrmSubida" frameborder="0" name="iFrmSubida" width="10px" height="5px" style="visibility:hidden" ></iframe>
            </fieldset>
        </td>
    </tr>
</table>
<center>
    <table id="tblBotonera" border="0" style="width:260px; margin-top:10px" class="texto">
        <colgroup>
            <col style="width:130px;" />
            <col style="width:130px;" />
        </colgroup>
        <tr>
            <td>
			    <button id="btnTramitar" type="button" onclick="Tramitar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/imgAceptar.gif" /><span title="">Enviar</span>
			    </button>			
     
            </td>
            <td>
                <button id="btnSalir" type="button" class="btnH25W90" runat="server" hidefocus="hidefocus"
                    onclick="salir();" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
            </td>
          </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdSolicitud" id="hdnIdSolicitud" value="-1" />
<input type="hidden" name="hdnIdFicepicert" id="hdnIdFicepicert" runat="server" value="-1" />
<input type="hidden" name="hdnTipo" id="hdnTipo" value="C" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />    
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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
