<%@ Page Language="C#" AutoEventWireup="true" Theme="Corporativo" CodeFile="Default.aspx.cs" Inherits="SUPER.Contacto_Mensaje" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title> ::: SUPER ::: - Mensaje al administrador</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/funcionestablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
	<body onload="init()">
	<form id="frmUpload" runat="server" enctype="multipart/form-data" method="POST" name="frmUpload">
<ucproc:Procesando ID="Procesando" runat="server" />
	<script language="javascript" type="text/javascript">
	<!--
	    var EsPostBack = <%=EsPostBack %>;
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    function uploadpop()
	    {
	        //Tiene que ir dentro del form por contener <%//= %>
		    //window.open('../../../Documentos/PorcentajeSubida.aspx?guid=<%= Request.QueryString["guid"] %>','',"resizable=no,status=no,scrollbars=no,menubar=no,toolbar=0,height=140,width=250,top="+ eval(screen.availHeight/2-40)+",left="+ eval(screen.availWidth/2-125));
	    }
	-->
    </script>
<center>
    <div style="width:600px; margin-top:10px;" class="texto" align="left">
            <table border="0" cellpadding="0" cellspacing="0" class="texto" style="width:585px;">
                <tr>
                    <td style="background-image:url('../../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                    <td style="background-image:url('../../../../Images/Tabla/8.gif'); height:6px;"></td>
                    <td style="background-image:url('../../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                </tr>
                <tr>
                    <td style="background-image:url('../../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                    <td style="background-image:url('../../../../Images/Tabla/5.gif'); padding:10px 5px 5px 5px;">
                        Desde esta opción, Ud. puede enviar un mensaje al administrador de SUPER.<br /> 
                        En caso de tratarse de un error de aplicación, le rogamos anexe un pantallazo del error producido.
                    </td>
                    <td style="background-image:url('../../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image:url('../../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                    <td style="background-image:url('../../../../Images/Tabla/2.gif'); height:6px; "></td>
                    <td style="background-image:url('../../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                </tr>
            </table>
    </div>
    <table style="width:600px; text-align:left; margin-top:20px;" border="0">
        <tr>
            <td>Mensaje</td>
        </tr>
        <tr>
            <td>
                <textarea id="txtComentario" class="txtMultiM" style="width:580px; height:300px;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset style="width:570px;">
                <legend>Archivo adjunto</legend>
                    <br />
                    <input type="file" id="txtArchivo" runat="server" class="txtIF"  style="width:565px"/>
                    <br /><br />
                </fieldset>
            </td>
        </tr>
    </table>
    <table style="margin-top:15px;width:230px; text-align:left;">
		<colgroup>
			<col style="width:115px;"/>
			<col style="width:115px;"/>
		</colgroup>
        <tr>
	        <td >
			    <button id="btnEnviar" type="button" onclick="enviar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgCorreo.gif" /><span>Enviar</span>
			    </button>	
	        </td>
			<td>
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
			    </button>	
	        </td>
        </tr>
    </table>
</center>
    <input type="hidden" runat="server" name="hdnResul" id="hdnResul" value="" />
<script type="text/javascript">
	<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
