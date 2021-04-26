<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Copiar estructura</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js?v=7" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
    var bCambios = false;
    var bSalir = false;
    var bLectura = false;
    /* Se guardan los datos del proyecto destino para restaurarlos después de seleccionar el proyecto origen */
    var nID_PROYECTOSUBNODO = "<% =Session["ID_PROYECTOSUBNODO"].ToString() %>";//int
    var bMODOLECTURA_PROYECTOSUBNODO = "<% = (((bool)Session["MODOLECTURA_PROYECTOSUBNODO"])? "1":"0") %>";//bool
    var bRTPT_PROYECTOSUBNODO = "<% = (((bool)Session["RTPT_PROYECTOSUBNODO"])? "1":"0") %>";//bool
    var sMONEDA_PROYECTOSUBNODO = "<% =Session["MONEDA_PROYECTOSUBNODO"].ToString() %>";//string
    var intSession = <%=Session.Timeout%>;  
</script>  
<div style="width:660px; margin-left:27px; margin-top:10px;">
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
    </tr>
    <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
           <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                    <!-- Inicio del contenido propio de la página -->
                    <br />
                    Respecto al tratamiento de los documentos asociados al proyecto origen, dispone de dos posibilidades:
                    <br /><br />
                    <b>1.- No copiar: </b>&nbsp;No se copian los documentos del proyecto origen al destino
                    <br /><br />
                    <b>2.- Generar copia: </b>&nbsp;Se copian los documentos del proyecto origen al destino. Es decir, podrá modificar
                                                documentos en un proyecto sin afectar a los documentos del otro proyecto.
                                                Esta opción está en construcción por lo que temporalmente no está disponible.
                    <br /><br />
                    <!-- Fin del contenido propio de la página 
                    <b>3.- Compartir documento: </b>&nbsp;Ambos proyectos (origen y destino) comparten los documentos. Es decir, si modifica
                                                documentos en un proyecto también estará modificando los documentos del otro proyecto.
                        -->
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
<br /><br />
<center>
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
    </tr>
    <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
           <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
        <!-- Inicio del contenido propio de la página -->
                <center>
	            <table style="width:640px;text-align:left;" cellpadding="5">
	                <colgroup><col style="width:320px;"/><col style="width:320px;"/></colgroup>
	                <tr>
	                    <td colspan="2">	
                            <fieldset style="width: 615px; height:40px; margin-left:8px">
                            <legend><label id="lblOrigen"  runat="server"> Origen</label></legend>
                                <table align='left' cellpadding='7'>
                                    <tr>
                                        <td>
                                            <label class="enlace" onclick="obtenerProyectos();" style="width:70px;height:17px">Proyecto</label>
                                            <asp:TextBox ID="txtNumPE_Origen" style="width:60px;"  Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;getPEByNum();}else{vtn2(event);}"/>&nbsp;&nbsp;
                                            <asp:TextBox ID="txtDesPE_Origen" readonly="true" style="width:450px;" Text="" runat="server" />
					                    </td>
                                    </tr> 
                                </table>   				
		                    </fieldset>	
	                    </td>
	                </tr>	
	                <tr>
	                    <td colspan="2">
                            <fieldset id="fldOtrosDatos" style="width: 615px; height:75px;margin-top:10px; margin-left:8px">
                            <legend>&nbsp;&nbsp;Estructura técnica
                            &nbsp;&nbsp;
        			                <img src="../../../../images/botones/imgmarcar.gif" onclick="mTab('fldOtrosDatos',1)" title="Marca todas" />
		                    &nbsp;&nbsp;
		                            <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mTab('fldOtrosDatos',2);" title="Desmarca todas"/>                                
                            </legend>
                            <table style="width: 615px;" cellpadding='5'>
                            <colgroup>
                                <col style="width: 530px;"/>
                                <col style="width: 85px;"/>
                                <col />
                            </colgroup>
                                <tr>
                                    <td>
                                        <fieldset style="width: 520px; height:35px; margin-top:3px;margin-left:5px">
                                        <legend>&nbsp;&nbsp;Estado de las tareas</legend>
                                        <table cellpadding='5' width="100%">
                                            <tr>
                                                <td>	
						                            <input hideFocus id="chkParalizada_TA" class="check" onclick="" type="checkbox" runat="server" />&nbsp;&nbsp;Paralizada
					                            </td>	
                                                <td>	
						                            <input hideFocus id="chkActiva_TA" class="check" onclick="" type="checkbox"  checked runat="server" />&nbsp;&nbsp;Activa
					                            </td>
                                                <td>	
						                            <input hideFocus id="chkPendiente_TA" class="check" onclick="" type="checkbox"  checked runat="server" />&nbsp;&nbsp;Pendiente
					                            </td>
                                                <td>	
						                            <input hideFocus id="chkFinalizada_TA" class="check" onclick="" type="checkbox"  runat="server" />&nbsp;&nbsp;Finalizada
					                            </td>							
                                                <td>	
						                            <input hideFocus id="chkCerrada_TA" class="check" onclick="" type="checkbox"  runat="server" />&nbsp;&nbsp;Cerrada
					                            </td>	
                                                <td>	
						                            <input hideFocus id="chkAnulada_TA" class="check" onclick="" type="checkbox"  runat="server" />&nbsp;&nbsp;Anulada
					                            </td>							            												
                                            </tr>  
			                            </table> 
			                            </fieldset>
                                    </td>
					                <td>
					                   <table style='width: 50px; margin-top:7px;margin-left:35px'>
                                            <tr>
                                                <td>	
		                                            <input hideFocus id="chkHitos" class="check" onclick="" type="checkbox" checked runat="server" />&nbsp;&nbsp;Hitos
		            		                    </td>	            												
                                            </tr>  
			                            </table> 
					                </td>                
				                </tr>                                        
                            </table> 
                            </fieldset>  	
	                    </td>
	                </tr>
                    <tr>
                        <td style="text-align:top;">
                            <fieldset style="width: 150px; height:60px; margin-left:8px">
                            <legend>&nbsp;&nbsp;Bitácora</legend>
                            <table cellpadding="5px" style="width:100%;">
                                <tr>
                                <!--<td><INPUT hideFocus id="chkBitacora_PE" class="check" onclick="" type="checkbox" runat="server" />&nbsp;&nbsp;Proyecto económico</td>-->
                                    <td>	
			                            <input hidefocus id="chkBitacora_PT" class="check" onclick="" type="checkbox" checked runat="server" />&nbsp;&nbsp;Proyecto técnico
		                            </td>
		                        </tr>
		                        <tr>
                                    <td>	
			                            <input hidefocus id="chkBitacora_TA" class="check" onclick="" type="checkbox"  checked runat="server" />&nbsp;&nbsp;Tarea
		                            </td>	            												
                                </tr> 
                            </table> 
                            </fieldset>
                         </td>
                         <td> 
                              <fieldset style="width:220px;">
                                <legend id="lyd1" runat="server">&nbsp;Acción sobre documentos asociados&nbsp;</legend>
                                <asp:RadioButtonList ID="rdbDoc" SkinID="rbl" runat="server" style="width:200px;height:70px; margin-left:50px;">
                                            <asp:ListItem Value="N" Text="No copiar" />                            
                                            <asp:ListItem Value="G" Text="Generar copia" />
                                </asp:RadioButtonList>
                                            <!--<asp:ListItem Value="M" Text="Compartir documento" />-->
                              </fieldset>
                            
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

</center>
<center>
    <table style="margin-top:15px;" width="240px">
        <tr>
	        <td align="center">
			    <button id="btnProcesar" type="button" onclick="procesar();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgProcesar.gif" /><span title="Procesar">Procesar</span>
			    </button>	
	        </td>		
	        <td align="center">
			    <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgCancelar.gif" /><span title="Salir">Cancelar</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
    
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" runat="server" id="hdnT305IdProy_Origen" value="" />
    <input type="hidden" runat="server" id="hdnNumPE_Destino" value="" />
    <input type="hidden" runat="server" id="hdnT305IdProy_Destino" value="" />
    <input type="hidden" runat="server" id="CRorigen" value="" />
    <input type="hidden" runat="server" id="CRdestino" value="" />

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
</body>
</html>


