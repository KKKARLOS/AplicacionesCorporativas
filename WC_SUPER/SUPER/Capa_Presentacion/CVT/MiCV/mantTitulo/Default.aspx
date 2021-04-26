<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_mantTitulo_Default" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title> ::: SUPER ::: - Detalle del título</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/>
	<link href="../../../../PopCalendar/CSS/Classic.css"type="text/css" rel="stylesheet"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/jquery.autocomplete.js" type="text/Javascript"></script>
 	<script src="../../../../PopCalendar/PopCalendar.js"type="text/Javascript"></script>
 	<script src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	     
    <style type="text/css">
        .autocomplete-w1 { position:absolute; top:0px; left:0px; margin:6px 0 0 6px; /* IE6 fix: */ _background:none; _margin:1px 0 0 0; }
        .autocomplete { font-size:11px; border:1px solid #999; background:#FFF; cursor:default; text-align:left; max-height:350px; overflow:auto; margin:-6px 6px 6px -6px; /* IE6 specific: */ _height:350px;  _margin:0; _overflow-x:hidden; }
        .autocomplete .selected { background:#F0F0F0; }
        .autocomplete div { padding:2px 5px; white-space:nowrap; overflow:hidden; }
        .autocomplete strong { font-weight:normal; color:#3399FF; }
        
        #principal{margin-top:15px;margin-bottom:10px; margin-left:17px; margin-right:3px; width:420px;}
        .sFieldset{width:70px;vertical-align:middle;display:inline;}
        #sButtontable{margin-left:5px; margin-right:5px; width:420px;}
        #tblGeneral td { padding: 4px 4px 4px 4px; }
    </style>
</head>
<body style="OVERFLOW: hidden" onload="init()" onbeforeunload="UnloadValor();" >
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="formTituloIdioma" name="formTituloIdioma" runat="server" action="Default.aspx" method="POST" enctype="multipart/form-data">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    //Para mensajes emergentes
    var sTareasPendientes = "<% =sTareasPendientes %>";  
    var sMOSTRAR_SOLODIS = "<%=ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"].ToString() %>";
    var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;       
    //Para grabar documento cuando el titulo no está grabado
    var sIDDocuAux = "<% =sIDDocuAux %>";    
</script>
<asp:Image ID="imgEstado" runat="server" ImageUrl="~/Images/imgSeparador.gif" style="visibility:visible; position:absolute; top:3px; right:40px;" />
<asp:Image ID="imgInfoEstado" runat="server" ImageUrl="~/Images/info.gif" style="visibility:hidden; position:absolute; top:16px; right:45px;" />
<asp:Image ID="imgHistorial" runat="server" ImageUrl="~/Images/imgHistorial.gif" style="visibility:hidden; position:absolute; top:14px; right:15px; cursor:pointer;" onclick="getHistorial()" title="Historial de estados" />
<fieldset id="fstDatosTitulo" style="width:430px; height:120px; margin-left:10px; margin-top:18px;">
    <legend>Datos Titulo Idioma</legend>    
     <table id="Table1" style="width:430px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
    <colgroup>
        <col style="width:90px;"/>
        <col style="width:340px;" />
    </colgroup>

    <tr>
        <td>
            <label id="lblIdioma" class="label">Idioma</label>
        </td>
        <td>
            <label id="txtIdioma" class="label" runat="server"></label>
            <%--<input type="text" class="txtM" id="txtIdioma" readonly="readonly" runat="server" style="width:150px;" />--%>
        </td>
    </tr>
    <tr>
        <td>
            <label id="lblTitulo" class="label">Denominación</label>
            <asp:Label ID="Label1" runat="server" ForeColor="Red" style=" margin-left:3px;">*</asp:Label>
        </td>
        <td>
            <input name="txtTitulo" id="txtTitulo" class="txtM" runat="server" style="width:310px;" maxlength="100" watermarktext="Ej: First Certificate" value="" /><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá titulos existentes" />
        </td>
    </tr>
    <tr>
        <td>
            <label class="label">F. Obtención </label><asp:Label ID="Label2" runat="server" ForeColor="Red" style="margin-left:3px;">*</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFecha" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" ></asp:TextBox>
            <%--<input style="width:60px;margin-right:5px;" id="txtFecha" Calendar="oCal" onclick="mc(this);" class="texto" readonly="readonly" runat="server"/>--%>
        </td>
    </tr>
    <tr>
        <td>
            <label id="lblCentro" class="label">Centro</label>
            <asp:Label ID="Label4" runat="server" ForeColor="Red" style=" margin-left:3px;">*</asp:Label>
        </td>
        <td>
            <input name="q" id="query" class="txtM" runat="server" style="width:310px;" maxlength="100" watermarktext="Ej: Cambridge Schools Centre" value="" /><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá centros existentes" />
        </td>
    </tr>
    </table>
</fieldset>	
    <fieldset id="fstAdjuntos" style="width:430px; height:45px; margin-left:10px; margin-top:10px;">
    <legend>Archivos adjuntos</legend>
    <table id="Table3" style="width:430px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
    <colgroup>
        <col style="width:90px;"/>
        <col style="width:340px;" />
    </colgroup>
    <tr>
        <td>
            <label title="Documentación acreditativa de titulo">Documento</label><asp:Label ID="Label9" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNombreDocumento" runat="server" style="width:270px;" ReadOnly="true" Text="" />
            <img id="imgUploadDoc" runat="server" src="../../../../Images/imgUpload.png" title="Seleccionar documento" onclick="uploadDocumento('doc');" style="cursor:pointer; vertical-align:middle;"/>
            <img id="imgDownloadDoc" runat="server" src="../../../../Images/imgDownload.png" title="Descargar documento" onclick="verDOC();" style="cursor:pointer; vertical-align:middle; display:none;"/>
            <img id="imgBorrarDoc" runat="server" src="../../../../Images/imgBorrar.gif" title="Borrar documento" onclick="deleteDocumento('doc');" style="cursor:pointer; vertical-align:middle; display:none;"/>
        </td>
    </tr>
    </table>
</fieldset>	
<table id="Table2" style="width:430px; margin-top:5px; margin-left:10px;" cellspacing="3" cellpadding="3" border="0">
    <colgroup>
        <col style="width:430px;" />
    </colgroup>
    <tr>
        <td>
            <label id="lblObservaciones" class="label" >Observaciones </label><br />
            <asp:TextBox Wrap="true" TextMode="MultiLine" id="txtObservaciones" style="width:430px; height:75px; resize:none;"  runat="server" ></asp:TextBox>
        </td>
    </tr>
</table>    
    
<center>
    <table id="tblBotones" style="width:430px; margin-top:5px; display:inline-table;" border="0" runat="server">
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
		        <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
			        <img src="../../../../Images/botones/imgCancelar.gif" /><span title="Cerrar la pantalla actual">Cancelar</span>
		        </button>
		        </center>	
            </td>
        </tr>
    </table>
</center>
<div id="divFondoMotivo" style="z-index:10; position:absolute; left:0px; top:-150px; width:600px; height:600px; background-image: url(../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; visibility:hidden;" runat="server">
    <div id="divMotivo" style="position:absolute; top:225px; left:20px;">
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
            <table id="Table5" class="texto" style="width:400px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td colspan="2">
                        <label id="lblMotivo" style="display:inline;">Establece las indicaciones oportunas para que el profesional complete correctamente la información</label><asp:Label ID="Label3" runat="server" ForeColor="Red" style="display:inline;">*</asp:Label><br />
                        <asp:TextBox TextMode="MultiLine" id="txtMotivoRT" style="width:390px; height:100px; margin-top:2px;resize:none;" runat="server" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <center>
            <table id="Table6" class="texto" style="width:240px; margin-top:10px;" cellspacing="0" cellpadding="5" border="0">
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
<input type="hidden" id="hdnOP"  runat="server" value="0"/>
<input type="hidden" id="hdnIdCodIdioma"  runat="server" value=""/>
<input type="hidden" id="usuticks"  runat="server" value=""/>
<input type="hidden" id="hdnndoc"  runat="server" value=""/>
<input type="hidden" id="hdnIdTitulo"  runat="server" value="-1"/>
<input type="hidden" id="hdnCambioDoc" runat="server" value="false" />
<input type="hidden" name="hdnEstadoInicial" id="hdnEstadoInicial" runat="server" value="B" />
<input type="hidden" name="hdnEstadoNuevo" id="hdnEstadoNuevo" runat="server" value="" />
<input type="hidden" id="hdnPantalla"  runat="server" value=""/>
<input type="hidden" name="hdnEsEncargado" id="hdnEsEncargado" runat="server" value="" />
<input type="hidden" name="hdnIdFicepi" id="hdnIdFicepi" runat="server" value="" />
<input type="hidden" name="hdnNombreDocInicial" id="hdnNombreDocInicial" value="" runat="server"/>
<input type="hidden" name="hdnContentServer" id="hdnContentServer" value="" runat="server"/>
<input type="hidden" runat="server" name="hdnEsMiCV" id="hdnEsMiCV" value="N" />
<input type="hidden" runat="server" name="hdnUserAct" id="hdnUserAct" value="" />
<input type="hidden" runat="server" name="hdnProf" id="hdnProf" value="-1" />  
<input type="hidden" name="hdnUsuTick" id="hdnUsuTick" value="" runat="server" />
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
    <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
</body>
</html>

