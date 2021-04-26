<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_mantCertificado_Default" ValidateRequest="false"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title> ::: SUPER ::: - Certificado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery.autocomplete.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
 	<script src="Functions/funciones.js?21062018" type="text/Javascript"></script>
 	<script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	
    <style type="text/css">
        .label{ width: 200px; font-size: 16px; }
        .autocomplete-w1 { position:absolute; top:0px; left:0px; margin:6px 0 0 6px; /* IE6 fix: */ _background:none; _margin:1px 0 0 0; }
        .autocomplete { font-size:11px; border:1px solid #999; background:#FFF; cursor:default; text-align:left; max-height:350px; overflow:auto; margin:-6px 6px 6px -6px; /* IE6 specific: */ _height:350px;  _margin:0; _overflow-x:hidden; }
        .autocomplete .selected { background:#F0F0F0; }
        .autocomplete div { padding:2px 5px; white-space:nowrap; overflow:hidden; }
        .autocomplete strong { font-weight:normal; color:#3399FF; }
    </style>
</head>
<body style="OVERFLOW: hidden" leftmargin="10" onload="init()" onbeforeunload="UnloadValor();">

<form id="formExamenCert" name="formExamenCert" runat="server" action="Default.aspx" method="POST" enctype="multipart/form-data">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    //Para grabar documento cuando el certificado no está grabado
    var sIDDocuAux = "<% =sIDDocuAux %>";    
</script>
<asp:Image ID="imgEstado" runat="server" ImageUrl="~/Images/imgSeparador.gif" style="visibility:visible; position:absolute; top:3px; right:40px;" />
<asp:Image ID="imgInfoEstado" runat="server" ImageUrl="~/Images/info.gif" style="visibility:hidden; position:absolute; top:16px; right:45px;" />
<fieldset id="fstDatosCertificado" style="width:470px; height:130px; margin-left:10px; margin-top:18px;">
    <legend>Certificado
        <asp:Label id="lblBuscar" style="color:Red; margin-left:37px; text-decoration:underline; cursor:pointer;" Visible="false" onclick="buscarCertificado();" 
                   runat="server">No lo encuentro</asp:Label> 
     </legend>    
     <table id="Table1" style="width:470px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
    <colgroup>
        <col style="width:100px;"/>
        <col style="width:100px;" />
        <col style="width:100px;" />
        <col style="width:170px;" />
    </colgroup>
    <tr>
        <td>
            <label class="label" style="display:inline;">Denominación</label><label style="color:Red;margin-left:3px">*</label>
        </td>
        <td colspan="3">
            <label title="" onmouseover="this.title=$I('txtDenom').value;TTip(event);">
                <input name="txtDenom" onkeypress="bCambios=true;" id="txtDenom" class="txtM" runat="server" style="width:330px;" maxlength="200" Text="" watermarktext="Ej: Certificación interna en ITIL"/>
            </label>
            <img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá certificaciones existentes" />
        </td>
    </tr>
    <tr>
        <td>
            <label class="label" style="display:inline;">Abreviatura</label>
        </td>
        <td colspan="3">
            <asp:TextBox id="txtAbrev" runat="server" style="width:100px; background-color:#EEEEEE;" ReadOnly="true" tabindex="-5000"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <label class="label" style="display:inline;" title="Fecha de obtención del último examen que ha generado el logro del certificado">F. Obtención</label>
        </td>
        <td>
            <asp:TextBox ID="txtFechaO" runat="server" style="width:60px;background-color:#EEEEEE;" ReadOnly="true" tabindex="-5001" ></asp:TextBox>
        </td>
        <td>
            <label class="label" style="display:inline; margin-left:25px">F. Caducidad</label>
        </td>
        <td>
            <asp:TextBox ID="txtFechaC" runat="server" style="width:60px; background-color:#EEEEEE;" ReadOnly="true" tabindex="-5002" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <label class="label" style="display:inline;">Ent. certificadora</label>
        </td>
        <td colspan="3">
            <%--
            <asp:DropDownList ID="cboEntCert" onChange="cambiarParamBusq();limpiarCertificado();llenarHdnEntCert();bCambios=true;" AppendDataBoundItems="true" runat="server" style="width:354px;" class="combo">
            <asp:ListItem Selected="True"></asp:ListItem>
            </asp:DropDownList>
            --%>
            <asp:TextBox ID="txtEntCert" runat="server" style="width:350px; background-color:#EEEEEE;" ReadOnly="true" tabindex="-5003" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <label class="label" style="display:inline;">Ent. tecnológico</label>
        </td>
        <td colspan="3">
            <%--
            <asp:DropDownList ID="cboEntorno" onChange="llenarHdnEntorno();bCambios=true;" AppendDataBoundItems="true" runat="server" style="width:354px;" class="combo">
            <asp:ListItem Value=""></asp:ListItem>
            </asp:DropDownList>
            --%>
            <asp:TextBox ID="txtEntorno" runat="server" style="width:350px; background-color:#EEEEEE;" ReadOnly="true" tabindex="-5004" ></asp:TextBox>
        </td>
    </tr>
    </table>
    </fieldset>
<fieldset id="fstAdjunto" style="width:470px; height:40px; margin-left:10px; margin-top:10px;">
<legend>Archivo adjunto</legend>
    <table id="Table2" style="width:460px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
        <colgroup>
            <col style="width:100px;"/>
            <col style="width:360px;" />
        </colgroup>
        <tr>
            <td>
                <label title="Documentación acreditativa del certificado">Documento</label>
            </td>
            <td>
                <asp:TextBox ID="txtNombreDocumento" runat="server" style="width:280px;" ReadOnly="true" Text="" />
                <img id="imgUploadDoc" runat="server" src="../../../../Images/imgUpload.png" title="Seleccionar documento" onclick="viaCompletada1();" style="cursor:pointer; vertical-align:middle;"/>
                <img id="imgDownloadDoc" runat="server" src="../../../../Images/imgDownload.png" title="Descargar documento" onclick="verDOC();" style="cursor:pointer; vertical-align:middle; display:none;"/>
                <img id="imgBorrarDoc" runat="server" src="../../../../Images/imgBorrar.gif" title="Borrar documento" onclick="deleteDocumento('doc');" style="cursor:pointer; vertical-align:middle; display:none;"/>
            </td>
        </tr>
    </table>
</fieldset>	
<fieldset id="fstExamenAsociados" style="width:470px; height:200px; margin-left:10px; margin-top:10px;">
<legend>
    Exámenes asociados
    <img style="cursor:pointer; margin-left:10px; vertical-align:middle;" src="../../../../images/imgCatalogo.png" onclick="getVias();" title="Visualiza las vías del certificado" />
</legend>    
    <table id="tblTitulo" style="height:17px; margin-top:8px; margin-left:5px;" width="450px" >
        <colgroup>
            <col style='width:20px;' />
            <col style='width:145px;' />
            <col style='width:65px;' />
            <col style='width:65px;' />
            <col style='width:20px;' />
            <col style='width:135px;' />
        </colgroup>
        <tr class="TBLINI">
            <td></td>
	        <td>Denominación</td>
            <td title="Fecha de obtención">Obtención</td>
            <td title="Fecha de caducidad">Caducidad</td>
            <td></td>
	        <td>Doc. Acreditativo</td>
        </tr>
    </table>       
    <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; WIDTH: 466px; height:108px; margin-left:5px;" runat="server"  name="divCatalogo">
        <div style="background-image:url('../../../../Images/imgFT18.gif'); background-repeat:repeat; width:450px; height:auto;">
                <%=strTablaHTML%>
        </div>
    </div>
    <table id="tblResultado" style="height:17px; margin-left:5px; width:450px;">
        <tr class="TBLFIN">
            <td>
            </td>
        </tr>
    </table>
    <center>
    <table id="Table3" style="width:430px; margin-top:5px; margin-left:8px; display:inline-table;" border="0" runat="server">
        <tr>
            <td align="center">
	            <button id="btnNuevo" type="button" onclick="nuevoExamen(true);" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		             onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;">
		            <img src="../../../../Images/Botones/imgAnadir.gif" /><span title="Añadir nuevo examen">Añadir</span>
	            </button>
                <button id="btnBorrarExamen" type="button" onclick="borrarExamen();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                    onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline-block; margin-left:10px;" title="Elimina el examen seleccionado">
                    <img src="../../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                </button>	  
            </td>
		</tr>
	</table>
	</center>	        
</fieldset>
<center>
    <table id="tblBotones" style="width:430px; margin-top:10px; display:inline-table;" border="0" runat="server">
        <tr>
            <td>
		        <center>
		        <button id="btnAparcar" type="button" onclick="aparcar();" class="btnH25W140" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/imgBorrador.png" /><span title='Guarda la información como borrador'>Guardar borrador</span>
		        </button>
		        <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/botones/imgGrabar.gif" /><span>Grabar</span><%--../../../../Images/imgAceptar.gif--%>
		        </button>
		        <button id="btnEnviar" type="button" onclick="viaCompletada2();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/botones/imgGrabar.gif" /><span>Grabar</span><%--Images/imgEnviarValidar.png--%>
		        </button>	
		        <button id="btnCumplimentar" type="button" onclick="cumplimentar();" class="btnH25W170" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/imgEnviarCumplimentar.png" /><span title='Devolver los datos al profesional para que los modifique/complete'>Enviar a cumplimentar</span>
		        </button>
		        <button id="btnCancelar" type="button" onclick="Cancelar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px;display:inline-block; margin-top:5px;">
			        <img src="../../../../Images/botones/imgCancelar.gif" /><span title="Cerrar la pantalla actual">Cancelar</span>
		        </button>
		        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../Images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
		        </button>
		        </center>	
            </td>
        </tr>
    </table>
</center>
<div id="divFondoMotivo" style="z-index:10; position:absolute; left:0px; top:0px; width:600px; height:600px; background-image: url(../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; visibility:hidden;" runat="server">
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
<div class="ocultarcapa">
<input  name="hdnErrores" id="hdnErrores" runat="server" />
<input  name="hdnCVTCert" id="hdnCVTCert" runat="server" value="-1" />
<input  name="hdnIdCertificadoInicial" id="hdnIdCertificadoInicial" runat="server" value="-1" />
<input  name="hdnEsEncargado" id="hdnEsEncargado" runat="server" value="" />
<input  name="hdnIdFicepi" id="hdnIdFicepi" runat="server" />
<input  name="hdnIdFicAct" id="hdnIdFicAct" runat="server" />
<input  name="hdnNomProf" id="hdnNomProf" runat="server" />
<input  name="hdnCaso" id="hdnCaso" runat="server" />
<input name="hdnMotivo" id="hdnMotivo" runat="server" />
<input name="hdnExamenes" id="hdnExamenes" runat="server" />
<input  name="hdnEstado" id="hdnEstado" runat="server" value="B" />
<input  name="hdnOP" id="hdnOP"  runat="server" />
<input  name="hdnCambioDoc" id="hdnCambioDoc"  runat="server" value="false" />
<input  name="hdnCambioCert" id="hdnCambioCert"  runat="server" value="false" />
<input  name="hdnCambioExam" id="hdnCambioExam"  runat="server" value="false" />
<input  name="hdnIdEntCert" id="hdnIdEntCert"  runat="server" value="" />
<input  name="hdnIdEntorno" id="hdnIdEntorno"  runat="server" value="" />
<input  name="hdnAccBorr" id="hdnAccBorr" runat="server" value="" />
<input  name="hdnValido" id="hdnValido" runat="server" value="N" />
<input  name="hdnIdFicepiCert" id="hdnIdFicepiCert" runat="server" value="-1" />
<input type="hidden" name="hdnDocRechazado" id="hdnDocRechazado" value="N" runat="server"/>
<input type="hidden" name="hdnNombreDocInicial" id="hdnNombreDocInicial" value="" runat="server"/>
<input type="hidden" name="hdnContentServer" id="hdnContentServer" value="" runat="server"/>
<input type="hidden" runat="server" name="hdnEsMiCV" id="hdnEsMiCV" value="N" />
<input type="hidden" name="hdnUsuTick" id="hdnUsuTick" value="" runat="server" />
<input type="hidden" name="hdnCompletado" id="hdnCompletado" value="N" runat="server" />
<input type="hidden" runat="server" name="hdnMsgCumplimentar" id="hdnMsgCumplimentar" value="" />
</div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
		var theform;
		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
			theform = document.forms[0];
		}
		else {
			theform = document.forms["formExamenCert"];
		}
			
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar){
			theform.submit();
		}
		else{
			$I("Botonera").restablecer();
		}
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

