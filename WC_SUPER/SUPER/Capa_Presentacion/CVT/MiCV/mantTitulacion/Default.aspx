<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_mantTitulacion_Default" EnableEventValidation="false" EnableViewState="true" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title> ::: SUPER ::: - Detalle de Titulación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="css/estilos.css" type="text/css" rel="stylesheet"/>
	<link href="../../../../PopCalendar/CSS/Classic.css" type="text/css" rel="stylesheet"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/jquery.autocomplete.js" type="text/Javascript"></script>
 	<script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	
</head>
<body style="OVERFLOW: hidden;" onload="init();" onbeforeunload="UnloadValor();">
<form id="formTituloFicepi" name="formTituloFicepi" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
        //Para mensajes emergentes
        var sTareasPendientes = "<% =sTareasPendientes %>";  
        var sMOSTRAR_SOLODIS = "<%=ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"].ToString() %>";
        var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;       	    
        //Para grabar los documento cuando el titulo no está grabado
        var sIDDocuAuxTit = "<% =sIDDocuAuxTit %>";    
        var sIDDocuAuxExpte = "<% =sIDDocuAuxExpte %>";    
	</script>
<asp:Image ID="imgEstado" runat="server" ImageUrl="~/Images/imgSeparador.gif" style="visibility:visible; position:absolute; top:3px; right:40px;" />
<asp:Image ID="imgInfoEstado" runat="server" ImageUrl="~/Images/info.gif" style="visibility:hidden; position:absolute; top:16px; right:45px;" />
<asp:Image ID="imgHistorial" runat="server" ImageUrl="~/Images/imgHistorial.gif" style="visibility:hidden; position:absolute; top:14px; right:15px; cursor:pointer;" onclick="getHistorial()" title="Historial de estados" />
<fieldset id="fstAcademicos" style="width:430px; height:125px; margin-left:10px; margin-top:18px;">
    <legend>Datos académicos</legend>
    <table id="Table2" style="width:430px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
        <colgroup>
            <col style="width:90px;"/>
            <col style="width:340px;" />
        </colgroup>
        <tr>
            <td>
                <label>Título </label><asp:Label ID="Label2" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
            </td>
            <td>
                <input name="txtTitulo" id="txtTitulo" class="txtM" runat="server" style="width:300px;" maxlength="100" watermarktext="Ej: Licenciatura en Informática" value="" /><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá titulaciones existentes" />
            </td>
        </tr>
        <tr>
            <td>
                <label>Tipo </label><asp:Label ID="Label21" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
            </td>
            <td>
                <asp:DropDownList id="cboTipo" runat="server" style="width:150px;" AppendDataBoundItems="true" CssClass="combo">
                    <asp:ListItem Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <label id="lblModalidad">Modalidad </label>
            </td>
            <td>
                <asp:DropDownList id="cboModalidad" runat="server" style="width:150px;" AppendDataBoundItems="true" CssClass="combo">
                    <asp:ListItem Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <label>TIC's </label>
            </td>
            <td>
                <input type="checkbox" id="chkTIC" class="check" checked="checked" runat="server" style="vertical-align:middle;" />
                
                <label style="margin-left:62px; margin-right:6px; vertical-align:middle;display:inline;">Finalizado</label>
                <input type="checkbox" id="chkFinalizado" onclick="setFinalizado()" class="check" checked="checked" runat="server" style="vertical-align:middle;" />
            </td>
        </tr>
        <tr>
            <td>
                <label id="lblEspecialidad" class="label" >Especialidad </label><asp:Label ID="Label6" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
            </td>
            <td>
                <asp:TextBox name="txtEspecialidad" id="txtEspecialidad" CssClass="txtM" runat="server" style="width:300px;" MaxLength="100" watermarktext="Ej: Análisis de aplicaciones" /><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá especialidades existentes" />
            </td>
        </tr>
    </table>
</fieldset>	
<fieldset id="Fieldset1" style="width:430px; height:65px; margin-left:10px; margin-top:10px;">
    <legend>Obtención del título</legend>
    <table id="Table3" style="width:430px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
        <colgroup>
            <col style="width:90px;"/>
            <col style="width:340px;" />
        </colgroup>
        <tr>
            <td>
                <label id="lblCentro" class="label" >Centro </label><asp:Label ID="Label7" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
            </td>
            <td>
               <asp:TextBox name="txtCentro" id="txtCentro" CssClass="txtM" runat="server" style="width:300px;" MaxLength="100" watermarktext="Ej: Universidad del País Vasco (UPV) /Euskal Herriko Unibertsitatea (EHU)" /><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá centros existentes" />
            </td>
        </tr>
        <tr style="vertical-align:middle;">
            <td>
                <label>Periodo </label>
            </td>
            <td>
                <label style="vertical-align:middle;">Inicio</label><asp:Label ID="Label8" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
                <asp:DropDownList id="cboInicio" runat="server" style="width:60px; margin-left:10px; margin-right:60px;" AppendDataBoundItems="true" CssClass="combo" OnChange="cambiarFin();">
                    <asp:ListItem Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <label style="width:30px;vertical-align:middle;">Fin</label>
                <asp:DropDownList id="cboFin" runat="server" style="width:60px;" AppendDataBoundItems="true" CssClass="combo">
                    <asp:ListItem Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</fieldset>	
<fieldset id="Fieldset2" style="width:430px; height:65px; margin-left:10px; margin-top:10px;">
    <legend>Archivos adjuntos</legend>
    <table id="Table4" style="width:430px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
        <colgroup>
            <col style="width:90px;"/>
            <col style="width:340px;" />
        </colgroup>
        <tr>
            <td>
                <label title="Documentación acreditativa del título">Documento</label><asp:Label ID="Label9" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
            </td>
            <td><asp:TextBox ID="txtNombreDocumento" runat="server" style="width:260px;" ReadOnly="true" Text="" />
                <img id="imgUploadDoc" runat="server" src="../../../../Images/imgUpload.png" title="Seleccionar documento" onclick="uploadDocumento('doc');" style="cursor:pointer; vertical-align:middle;"/>
                <img id="imgDownloadDoc" runat="server" src="../../../../Images/imgDownload.png" title="Descargar documento" onclick="verDOC('doc');" style="cursor:pointer; vertical-align:middle; display:none;"/>
                <img id="imgBorrarDoc" runat="server" src="../../../../Images/imgBorrar.gif" title="Borrar documento" onclick="deleteDocumento('doc');" style="cursor:pointer; vertical-align:middle; display:none;"/>
            </td>
        </tr>
        <tr>
            <td>
                <label title="Documentación del expediente académico">Expediente</label>
            </td>
            <td><asp:TextBox ID="txtNombreExpediente" runat="server" style="width:260px;" ReadOnly="true" Text="" />
                <img id="imgUploadExp" runat="server" src="../../../../Images/imgUpload.png" title="Seleccionar expediente" onclick="uploadDocumento('exp');" style="cursor:pointer; vertical-align:middle;"/>
                <img id="imgDownloadExp" runat="server" src="../../../../Images/imgDownload.png" title="Descargar expediente" onclick="verDOC('expte');" style="cursor:pointer; vertical-align:middle; display:none;"/>
                <img id="imgBorrarExp" runat="server" src="../../../../Images/imgBorrar.gif" title="Borrar expediente" onclick="deleteDocumento('exp');" style="cursor:pointer; vertical-align:middle; display:none;"/>
            </td>
        </tr> 
    </table>
</fieldset>	
<table id="principal" style="width:430px; margin-left:8px; margin-top:5px;" cellspacing="0" cellpadding="0" border="0">
<colgroup>
    <col style="width:90px;"/>
    <col style="width:340px;" />
</colgroup>
<tr>
    <td>
        <label id="lblObservaciones" class="label">Observaciones </label><br />
        <asp:TextBox Wrap="true" TextMode="MultiLine" id="txtObservaciones" style="width:425px; height:50px; resize:none;"  runat="server" ></asp:TextBox>
    </td>
</tr>
</table>
<center>
    <table id="tblBotones" style="width:430px; margin-top:0px; display:inline-table;" border="0" runat="server">
        <tr>
            <td>
		        <center>
		        <button id="btnAparcar" type="button" onclick="aparcar();" class="btnH25W140" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/imgBorrador.png" /><span title='Guarda la información como borrador'>Guardar borrador</span>
		        </button>
		        <button id="btnValidar" type="button" onclick="validar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;"><%--Images/imgValidar.png--%>
			        <img src="../../../../Images/Botones/imgGrabar.gif" /><span>Grabar</span><%--Images/imgAceptar.gif--%>
		        </button>
		        <button id="btnPseudovalidar" type="button" onclick="pseudovalidar();" class="btnH25W160" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none; margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/imgPseudovalidado.png" /><span title="Aceptar/dar por buena la información, a falta de documento acreditativo">Pendiente de anexar</span>
		        </button>
		        <button id="btnEnviar" type="button" onclick="enviar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/botones/imgGrabar.gif" /><span>Grabar</span><%--Images/imgEnviarValidar.png--%>
		        </button>	
		        <button id="btnCumplimentar" type="button" onclick="cumplimentar();" class="btnH25W170" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/imgEnviarCumplimentar.png" /><span title='Devolver los datos al profesional para que los modifique/complete'>Enviar a cumplimentar</span>
		        </button>
		        <button id="btnRechazar" type="button" onclick="rechazar();" class="btnH25W130" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/imgRechazar.png" /><span title='Este dato será visible únicamente por el profesional'>Info. privada</span>
		        </button>
		        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
			        <img src="../../../../Images/botones/imgCancelar.gif" /><span title="Cerrar la pantalla actual">Cancelar</span>
		        </button>
		        </center>	
            </td>
        </tr>
    </table>
</center>
    
<div id="divFondoMotivo" style="z-index:10; position:absolute; left:0px; top:0px; width:600px; height:600px; background-image: url(../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; visibility:hidden;" runat="server">
    <div id="divMotivo" style="position:absolute; top:180px; left:20px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table id="Table1" class="texto" style="width:400px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td colspan="2">
                        <label id="lblMotivo" style="display:inline;">Establece las indicaciones oportunas para que el profesional complete correctamente la información</label><asp:Label ID="Label3" runat="server" ForeColor="Red" style="display:inline;">*</asp:Label><br />
                        <asp:TextBox TextMode="MultiLine" id="txtMotivoRT" style="width:390px; height:100px; margin-top:2px; resize:none;" runat="server" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <center>
            <table id="Table1" class="texto" style="width:240px; margin-top:10px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td>
                        <button id="btnAceptarMotivo" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="AceptarMotivo();" onmouseover="se(this, 25);">
                            <img src="../../../../Images/imgAceptar.gif" /><span>Aceptar</span>
                        </button>
                    </td>
                    <td>
                        <button id="btnCancelarMotivo" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="CancelarMotivo();" onmouseover="se(this, 25);">
                            <img src="../../../../Images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
            </center>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>

    <input type="hidden" name="hdnErrores" id="hdnErrores" runat="server" value="" />
    <input type="hidden" name="hdnOP" id="hdnOP"  runat="server" value="0"/>
    <input type="hidden" name="hdnIdficepi" id="hdnIdficepi" runat="server" value="" />
    <input type="hidden" name="hdnIdTituloficepi" id="hdnIdTituloficepi" runat="server" value="" />
    <input type="hidden" name="hdnIdtitulacion" id="hdnIdtitulacion" runat="server" value="" />
    <input type="hidden" name="hdnEsEncargado" id="hdnEsEncargado" runat="server" value="" />
    <input type="hidden" name="hdnCambioDoc" id="hdnCambioDoc" runat="server" value="false" />
    <input type="hidden" name="hdnCambioExp" id="hdnCambioExp" runat="server" value="false" />
    <input type="hidden" name="hdnEstadoInicial" id="hdnEstadoInicial" runat="server" value="B" />
    <input type="hidden" name="hdnEstadoNuevo" id="hdnEstadoNuevo" runat="server" value="" />
    <input type="hidden" name="hdnTituloEstadoIni" id="hdnTituloEstadoIni" runat="server" value="" />
    <input type="hidden" name="hdnIdtitulacionIni" id="hdnIdtitulacionIni" runat="server" value="-1" />
    <input type="hidden" name="hdnOPTit" id="hdnOPTit"  runat="server" value=""/>
    <input type="hidden" name="hdnPermiteEnviarValidar" id="hdnPermiteEnviarValidar"  runat="server" value="N"/>
    <input type="hidden" name="hdnNombreDocInicial" id="hdnNombreDocInicial" value="" runat="server"/>
    <input type="hidden" name="hdnContentServer" id="hdnContentServer" value="" runat="server"/>
    <input type="hidden" name="hdnNombreDocInicialExpte" id="hdnNombreDocInicialExpte" value="" runat="server"/>
    <input type="hidden" name="hdnContentServerExpte" id="hdnContentServerExpte" value="" runat="server"/>
    <input type="hidden" runat="server" name="hdnEsMiCV" id="hdnEsMiCV" value="N" />
    <input type="hidden" runat="server" name="hdnProfAct" id="hdnProfAct" value="-1" /> 
    <iframe id="iFrmSubida" name="iFrmSubida" frameborder="no" width="10px" height="10px" style="visibility:hidden;" ></iframe>
    <input type="hidden" name="hdnUsuTickTit" id="hdnUsuTickTit" value="" runat="server" />
    <input type="hidden" name="hdnUsuTickExpte" id="hdnUsuTickExpte" value="" runat="server" />
    <input type="hidden" runat="server" name="hdnMsgCumplimentar" id="hdnMsgCumplimentar" value="" />

    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    </form>
<script type="text/javascript">
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
</script>
    <iframe id="Iframe1" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
</body>
</html>