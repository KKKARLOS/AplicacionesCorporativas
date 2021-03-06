<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title> ::: SUPER ::: - Relaci?n de proyectos y figuras de un profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script> 
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
<style type="text/css">
#tsPestanas table { table-layout:auto; }
#tblTareas TD{border-right: solid 1px #569BBD; padding-left:2px; padding-right:2px;}
</style>
</head>
<body onload="init()" >
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
        mostrarProcesando();
    -->
    </script>    
<table style="width:520px; text-align:left; margin-left:20px; margin-top:10px;" cellpadding="3px">
    <colgroup><col style='width:70px;' /><col style='width:450px;' /></colgroup>
    <tr>
        <td><label id="lblProfesional" runat="server" class="texto">Profesional</label></td>
		<td>
		    <asp:TextBox ID="txtProfesional" style="width:430px;" Text="" readonly="true" runat="server" />
		</td>
    </tr>
    <!--
    <tr>
        <td><label id="lblCR" runat="server" class="texto">C.R.</label></td>
		<td>
		    <asp:TextBox ID="txtDenCR" style="width:430px;" Text="" readonly="true" runat="server" />
		</td>
    </tr>
    -->
</table>

<table style="width:800px; height:17px; text-align:left; margin-left:25px;" border="0">
    <colgroup><col style='width:150px;' /><col style='width:220px;' /><col style='width:150px;' /><col style='width:280px;' /></colgroup>
    <tr>
		<td style="padding-top:3px;" id="cldTec" runat="server">
		    <img id="imgTec" runat="server" src="../../../Images/imgTecnicoV.gif" /> T?cnico especialista
		</td>
        <td style="padding-top:3px;" id="cldCRP" runat="server">
            <img id="imgCRP" runat="server" src="../../../Images/imgCRPV.gif" /> 
            <label id="lblCRP" class="texto" runat="server"></label> 
        </td>
		<td style="padding-top:3px;" id="cldAdm" runat="server">
		    <img id="imgAdm" runat="server" src="../../../Images/imgAdministradorV.gif" /> 
		    <label id="lblAdm" class="texto" runat="server"></label> 
	    </td>
		<td>
            <fieldset style="width:270px; vertical-align:top;">
                <legend>Filtro por estado</legend>   
                <table style="width:260px; margin-left:5px; margin-top:5px;" cellpadding="1px">
                    <tr>
                        <td>
                            <input id="chkPresupuestado" runat="server" onclick="buscar()" class="check" type="checkbox" checked />
                            <img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />
                            <input id="chkAbierto" runat="server" onclick="buscar()" class="check" type="checkbox" checked style="margin-left:25px;" />
                            <img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />
                            <input id="chkCerrado" runat="server" onclick="buscar()" class="check" type="checkbox" style="margin-left:25px;" />
                            <img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />
                            <input id="chkHistorico" runat="server" onclick="buscar()" class="check" type="checkbox" style="margin-left:25px;" />
                            <img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto hist?rico' />
                        </td>
                    </tr>
                </table>
            </fieldset>	
		</td>
    </tr>
</table>
<center>
<table id="tabla" style="width:830px; margin-left:5px; margin-top:5px; text-align:left;" border="0">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="810px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="Proyectos" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Figuras" Width="100"></eo:TabItem>
					</Items>
				</TopGroup>
				<LookItems>
					<eo:TabItem ItemID="_Default" 
						LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
						LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
						LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
						Image-Url="~/Images/Pestanas/normal_bg.gif"
						Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
						Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
						RightIcon-Url="~/Images/Pestanas/normal_right.gif"
						RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
						RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
						NormalStyle-CssClass="TabItemNormal"
						HoverStyle-CssClass="TabItemHover"
						SelectedStyle-CssClass="TabItemSelected"
						DisabledStyle-CssClass="TabItemDisabled"
						Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
					</eo:TabItem>
				</LookItems>
			</eo:TabStrip>
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="810px" Height="530px">
			   <eo:PageView ID="PageView1" CssClass="PageView" runat="server">	
			        <!-- Pesta?a 1 Proyectos-->
                    <table style="text-align:left; width:780px; margin-left:5px; margin-top:5px;" border="0">
                        <tr>
	                        <td>
                                <table style="width:760px; height:17px;">
                                    <colgroup>
                                        <col style="width:20px"/>
                                        <col style="width:20px"/>
                                        <col style="width:20px"/>
                                        <col style="width:60px"/>
                                        <col style="width:300px"/>
                                        <col style="width:200px"/>
                                        <col style="width:70px"/>
                                        <col style="width:70px"/>
                                    </colgroup>
                                    <tr class="TBLINI">
		                                <td></td>
		                                <td></td>
		                                <td></td>
		                                <td style="text-align:right;">Proyecto</td>
		                                <td></td>
		                                <td id="denCR">Centro de Responsabilidad</td>
		                                <td title='Fecha de alta en el proyecto'>F. Alta</td>
		                                <td title='Fecha de baja en el proyecto'>F. Baja</td>
                                    </tr>
                                </table>
                                <div id="divProy" style=" overflow:auto; overflow-x:hidden; width:776px; height:420px" onscroll="scrollTablaProy();">
                                    <div style='background-image:url(../../../Images/imgFT20.gif); width:760px;'>
                                    <%=strTablaProys %>
                                    </div>
                                </div>
                                <table style="width: 760px; height: 17px;">
                                    <tr class="TBLFIN">
                                        <td></td>
                                    </tr>
                                </table>
	                        </td>
                        </tr>
                    </table>
                    <table style="width:420px; margin-left:15px;">
                        <colgroup>
                            <col style="width:100px" />
                            <col style="width:90px" />
                            <col style="width:230px" />
                        </colgroup>
	                    <tr> 
	                        <td><img class="ICO" src="../../../Images/imgProducto.gif" />Producto</td>
                            <td><img class="ICO" src="../../../Images/imgServicio.gif" />Servicio</td>
                            <td></td>
	                      </tr>
	                    <tr>
	                        <td><img class="ICO" src="../../../Images/imgIconoContratante.gif" />Contratante</td>
                            <td><img class="ICO" src="../../../Images/imgIconoRepJor.gif" />Replicado</td>
                            <td><img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" />Replicado con gesti?n propia</td>
                          </tr>
	                    <tr>
	                        <td style="vertical-align:top;"><img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
                            <td><img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
                            <td>
                                <img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto hist?rico' />Hist?rico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
                            </td>
                          </tr>
                    </table>
                    
               </eo:PageView>
               <eo:PageView ID="PageView5" CssClass="PageView" runat="server" align="center">	
                <!-- Pesta?a 2 Figuras-->
                    <table style="text-align:left; width:805px; margin-left:5px; margin-top:5px;" border="0">
                        <tr>
                            <td>Tipo de ?tem&nbsp;&nbsp;
                                <asp:DropDownList id="cboTipoItem" runat="server" Width="260px" onChange="buscarFig()" AppendDataBoundItems="true">
                                </asp:DropDownList>
	                        </td>  
                        </tr>
                        <tr>
	                        <td>
                                <table style="width:765px; height:17px; margin-top:5px;">
                                    <colgroup><col style='width:30px;' /><col style='width:525px;' /><col style='width:210px;' /></colgroup>
	                                <tr id="tblTitulo" class="TBLINI">
	                                    <td>&nbsp;</td>
					                    <td align="left">Denominaci?n de item&nbsp;
					                        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							                    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						                    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							                    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					                    </td>
                                        <td>Figuras</td>
	                                </tr>
                                </table>
                                <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width: 781px; height:280px;" onscroll="scrollTablaFig();">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:765px">
                                    <%=strTablaFig%>
                                    </div>
                                </div>
                                <table id="tblTotales" style="width: 765px; height: 17px">
	                                <tr class="TBLFIN">
                                        <td>&nbsp;</td>
	                                </tr>
                                </table>
	                        </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width:755px; height:17px; text-align:left;">
                                    <colgroup><col style="width:355px;" /><col style="width:400px;" /></colgroup>
                                    <tr>
                                        <td>
                                            <fieldset style="width:350px; padding:3px; height:155px;">
                                                <legend>Tipos de ?tem</legend>   
                                                <table style="width:340px;" cellpadding="1px">
                                                    <colgroup><col style="width: 170px;" /><col style="width: 170px;" /></colgroup>
                                                      <tr> 
                                                        <td><img class="ICO" src="../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4)%></td>
                                                        <td><img class="ICO" src="../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3)%></td>
                                                      </tr>
                                                      <tr> 
                                                        <td><img class="ICO" src="../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2)%></td>
                                                        <td><img class="ICO" src="../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1)%></td>
                                                      </tr>
                                                      <tr> 
                                                        <td><img class="ICO" src="../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO)%></td>
                                                        <td><img class="ICO" src="../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO)%></td>
                                                      </tr>
                                                      <tr> 
                                                        <td><img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto hist?rico' />&nbsp;Proyecto</td>
                                                        <td><img class="ICO" src="../../../Images/imgContrato.gif" />&nbsp;Contrato</td>
                                                      </tr>
                                                      <tr> 
                                                        <td><img class="ICO" src="../../../Images/imgHorizontal.gif" />&nbsp;Horizontal</td>
                                                        <td><img class="ICO" src="../../../Images/imgClienteICO.gif" />&nbsp;Cliente</td>
                                                      </tr>
                                                      <tr> 
                                                        <td><img class="ICO" src="../../../Images/imgOT.gif" />&nbsp;Oficina T?cnica</td>
                                                        <td><img class="ICO" src="../../../Images/imgGF.gif" />&nbsp;Grupo Funcional</td>
                                                      </tr>
                                                      <tr> 
                                                        <td colspan="2"><img class="ICO" src="../../../Images/imgQn.gif" /><img class="ICO" src="../../../Images/imgQ1.gif" /><img class="ICO" src="../../../Images/imgQ2.gif" /><img class="ICO" src="../../../Images/imgQ3.gif" /><img class="ICO" src="../../../Images/imgQ4.gif" />&nbsp;Cualificadores de proyecto</td>
                                                      </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                        <td>
                                            <fieldset style="width:393px; margin-left:10px; padding:3px; height:155px;">
                                                <legend>Figuras</legend>   
                                                <table style="width:388px;" cellpadding="1" cellspacing="0" class="texto">
                                                    <colgroup><col style="width: 194px;" /><col style="width: 194px;" /></colgroup>
                                                  <tr> 
                                                    <td><img class="ICO" src="../../../Images/imgResponsable.gif" />&nbsp;Responsable</td>
                                                    <td><img class="ICO" src="../../../Images/imgDelegado.gif" />&nbsp;Delegado</td>
                                                  </tr>
                                                  <tr> 
                                                    <td><img class="ICO" src="../../../Images/imgColaborador.gif" />&nbsp;Colaborador</td>
                                                    <td><img class="ICO" src="../../../Images/imgInvitado.gif" />&nbsp;Invitado</td>
                                                  </tr>
                                                  <tr> 
                                                    <td><img class="ICO" src="../../../Images/imgGestor.gif" />&nbsp;Gestor</td>
                                                    <td><img class="ICO" src="../../../Images/imgSecretaria.gif" />&nbsp;Asistente</td>
                                                  </tr>
                                                  <tr> 
                                                    <td title="Destinatario del informe de control de imputaciones en IAP"><img class="ICO" src="../../../Images/imgPerseguidor.gif" title="Receptor de Informes de Actividad" />&nbsp;RIA</td>
                                                    <td><img class="ICO" src="../../../Images/imgBitacorico.gif" />&nbsp;Bitac?rico</td>
                                                  </tr>
                                                  <tr> 
                                                    <td><img class="ICO" src="../../../Images/imgJefeProyecto.gif" />&nbsp;Jefe de proyecto</td>
                                                    <td><img class="ICO" src="../../../Images/imgSubjefeProyecto.gif" title="Responsable t?cnico de proyecto econ?mico" />&nbsp;RTPE</td>
                                                  </tr>
                                                  <tr> 
                                                    <td><img class="ICO" src="../../../Images/imgRTPT.gif" />&nbsp;Responsable de proyecto t?cnico</td>
                                                    <td><img class="ICO" src="../../../Images/imgMiembroOT.gif" />&nbsp;Miembro de Oficina T?cnica</td>
                                                  </tr>
                                                  <tr> 
                                                    <td title="Soporte titular de soporte administrativo"><img class="ICO" src="../../../Images/imgSAT.gif" />&nbsp;SAT</td>
                                                    <td title="Soporte alternativo de soporte administrativo"><img class="ICO" src="../../../Images/imgSAA.gif" />&nbsp;SAA</td>
                                                  </tr>
                                                  <tr> 
                                                    <td title="Consultor de curr?culums"><img class="ICO" src="../../../Images/imgConsultaCV.png" />&nbsp;Consultor CV</td>
                                                    <td></td>
                                                  </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<center>
    <table id="tblBotones" style="margin-top:10px;" width="120px">
        <tr>
	        <td>
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />
<input type="hidden" name="hdnIdFicepi" id="hdnIdFicepi" value="" runat="server" />
<input type="hidden" name="hdnIdUser" id="hdnIdUser" value="" runat="server" />

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
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
</body>
</html>
