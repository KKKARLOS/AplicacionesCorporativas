<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">      
    #ctl00_CPHC_rdbEstado tr { height: 30px; }
</style>
    
<center>
<br /><br />
    <table id="nombreProyecto" style="width:750px;text-align:left">
    <tr>
        <td>
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" cellpadding="10px" style="width:720px">
                        <colgroup><col style="width:720px" /></colgroup>
                        <tr>
                            <td>
                                <p>
                                    Este <label style='font-weight: bold;'>proceso</label> permite una vez seleccionado un proyecto económico cerrar todas aquellas tareas que cumplan los criterios especificados. 
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                Los <label style='font-weight: bold;'>criterios</label> especificados pueden ser:<br /><br />
                            <div style="margin-left:10px">
                                . <label style='font-weight: bold;'>Estado de la tarea.</label> El proceso cerrará aquellas tareas cuyo estado coincida con alguno de los marcados dentro del apartado.<br /><br />
                                . <label style='font-weight: bold;'>Vigencia de la tarea.</label><br /><br />
                             </div>
                            <div style="margin-left:30px">
	                            1.- No especificamos rango de fechas, con lo que no se aplica filtro.<br /> <br />  
	                            2.- Especificamos fecha de inicio de vigencia y fecha de fin de vigencia.  El proceso cerrará  aquellas tareas cuya fecha de inicio y fecha fin de 
	                                vigencia estén dentro del rango especificado.<br /><br />   
	                            3.- Sólo especificamos fecha de fin de vigencia.  El proceso cerrará  todas las tareas cuya fecha fin de vigencia
	                                fuera anterior o igual a la fecha de fin especificada.<br /><br />   
	                            4.- Sólo especificamos fecha de inicio de vigencia.  El proceso cerrará  todas las tareas cuya fecha inicio de vigencia
	                                fuera posterior o igual a la fecha de inicio especificada.<br />  
                                </p>
                            </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
</table>
<br /><br />
<table id="nombreProyecto" style="width:750px;text-align:left">
    <tr>
        <td>
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" cellpadding="10px" style="width:720px">
                        <colgroup><col style="width:80px" /><col style="width:640px" /></colgroup>
                        <tr>
                            <td><label id="lblProy" runat="server" title="Proyecto económico" style="width:55px;height:17px;margin-left:10px" class="enlace" onclick="getPE()">Proyecto</label></td>
                            <td>
                                <asp:TextBox id="txtNumPE" runat="server" ReadOnly MaxLength="6" Width="50px"></asp:TextBox>
                                &nbsp;&nbsp;<asp:TextBox ID="txtDesPE" style="width:540px;" runat="server" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td><label style="margin-left:10px">Cliente</label></td>
                            <td><asp:TextBox ID="txtCliente" style="width:600px;" runat="server" readonly="true" /><br /></td>
                        </tr>
                    </table>

                    <table align="center" cellpadding="10px" style="width:720px">
                        <colgroup><col style="width:450px" /><col style="width:270px" /></colgroup>
                    <tr>
                        <td align="left">
                            <fieldset style="width: 410px; margin-left:2px;height:40px;">
                                <legend>Estado de la tarea</legend>
                                <table class="texto" style="margin-top:5px; margin-left:17px; width:400px;" cellpadding="3">
                                    <colgroup>
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
					                    <col style="width:100px;" />
                                    </colgroup>
                                    <tr>
                                        <td style="padding-left:5px;">
                                            <label id="lblParalizada" title="Tareas en estado 'Paralizada'">Paralizada</label>
                                            <input type="checkbox" id="chkParalizada" class="check" runat="server" />
                                        </td>
                                        <td>
                                            <label id="lblActiva" title="Tareas en estado 'Activa'" runat="server">Activa</label>
                                            <input type="checkbox" id="chkActiva" class="check" runat="server" />
                                        </td>
                                        <td>
                                            <label id="lblPendiente" title="Tareas en estado 'Pendiente'" runat="server">Pendiente</label>
                                            <input type="checkbox" id="chkPendiente" class="check" runat="server" />
                                        </td>
                                        <td>
                                            <label id="lblFinalizada" title="Tareas en estado 'Finalizada'" runat="server">Finalizada</label>
                                            <input type="checkbox" id="chkFinalizada" class="check" runat="server" />
                                        </td>
								
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
	                    <td>
			                <fieldset style="width:215px; margin-left:4px;height:40px;">
				                <legend>Vigencia de la tarea</legend>
                                <table style="width:100%;margin-top:5px" cellpadding="4">
                                    <tr>
                                        <td>
                                            &nbsp;Inicio
                                            <asp:TextBox ID="txtValIni" runat="server" style="width:60px;cursor:pointer; margin-right:23px;" Calendar="oCal"></asp:TextBox>
                                            Fin
                                            <asp:TextBox ID="txtValFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" cRef="txtValIni"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>  
                        </td>
                    </tr>  
                    </table>
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
</table>
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />  
<input type="hidden" runat="server" name="hdnRtpt" id="hdnRtpt" value="0" />                                              
</center>	
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    procesar();
					break;
				}
			}
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
</asp:Content>

