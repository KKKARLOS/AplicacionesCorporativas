<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_ImpDiaria_Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: IAP ::: - Detalle de tarea</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../App_Themes/Corporativo/Corporativo.css" type="text/css">
    <link rel="stylesheet" href="../../../PopCalendar/CSS/Classic.css" type="text/css">
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/documentos.js" type="text/Javascript"></script>
    <script src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
    <script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var nObligaest = "<% =nObligaest %>";
        var nPT = "<% =nPT %>";
        var sDesTarea = "<% =sDesTarea.Replace((char)34, (char)39) %>";
        var sNotas = "<% =sNotas %>";
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        var bEstadoLectura = <% =bEstadoLectura.ToString().ToLower() %>;
        var sETEOriginal = "";
        var sFFEOriginal = "";
        var sComentarioOriginal = "";
        //mostrarProcesando();
        //Variables a devolver a la imputación diaria.
        var bFinalizadaAntes = "";
        var bFinalizada = "";
        var sIndiComen = "";
        var nPendiente = "";
    </script> 
    <table id="tabla" width="850px" align="center" style="margin-top:20px;">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
							MultiPageID="mpContenido" 
							ClientSideOnLoad="CrearPestanas" 
							>
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Documentación" ToolTip="" Width="110"></eo:TabItem>
							<eo:TabItem Text-Html="Notas" ToolTip="" Width="100"></eo:TabItem>
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
			    <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="535px">
				   <eo:PageView CssClass="PageView" runat="server">				

				    <!-- Pestaña 1 -->
				    <br />
                    <table width="800px" align="center" cellpadding="5">
                        <tr>
	                        <td>
							    <fieldset style="width:800px; margin-left:4px;">
								    <legend>Estructura</legend>
                                    <table align="center" style="width:790px" cellpadding="5">
                                        <colgroup>
                                            <col style="width:120px"/>
                                            <col style="width:350px"/>
                                            <col style="width:60px"/>
                                            <col style="width:260px"/>
                                        </colgroup>
                                        <tr>
                                            <td>Proyecto económico</td>
                                            <td><asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" /><asp:TextBox ID="txtPE" runat="server" style="width:281px" ReadOnly /></td>
                                            <td>Fase</td>
                                            <td><asp:TextBox ID="txtFase" runat="server" style="width:250px" readonly="true" /></td>
                                        </tr>
                                        <tr>
                                            <td>Proyecto técnico</td>
                                            <td><asp:TextBox ID="txtPT" runat="server" style="width:335px" readonly="true" /></td>
                                            <td>Actividad</td>
                                            <td><asp:TextBox ID="txtActividad" runat="server" style="width:250px" readonly="true" /></td>
                                        </tr>
                                    </table>
                                </fieldset>
	                        </td>
                        </tr>
                        <tr>
                            <td>
			                    <fieldset style="width:800px; margin-left:4px;">
				                    <legend>Identificación de tarea</legend>
                                    <table align="center" width="100%" cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td><div>
                                                Número&nbsp;
                                                <asp:TextBox ID="txtIdTarea" runat="server" SkinID="Numero" style="width:60px" readonly="true"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;Denominación&nbsp;
                                                </div>
                                                <asp:TextBox ID="txtDesTarea" runat="server" SkinID="multi" TextMode="MultiLine" Rows="2" Style="width: 580px;height:35px;float:right;margin-top:-22px" readonly="true"></asp:TextBox>                                               
                                            </td>
                                        </tr>
                                    </table>
                            </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Descripción<br />
                            <asp:TextBox ID="txtDescripcion" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="4" style="width:810px; margin-left:4px;" onKeyPress="Count(this,100);" readonly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
			                    <fieldset style="width:800px; margin-left:4px;">
				                    <legend>Datos IAP referentes al técnico</legend>
                                    <table align="center" width="100%" cellpadding="5">
                                        <tr>
                                            <td width="12%">Primer consumo</td>
                                            <td width="21%"><asp:TextBox ID="txtPriCon" style="width:60px" runat="server" readonly="true" /></td>
                                            <td width="15%">Comsumido (horas)</td>
                                            <td width="18%"><asp:TextBox ID="txtConHor" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                                            <td width="15%">Pte. estimado (horas)</td>
                                            <td width="18%"><asp:TextBox ID="txtPteEst" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                                        </tr>
                                        <tr>
                                            <td>Ultimo consumo</td>
                                            <td><asp:TextBox ID="txtUltCon" style="width:60px" runat="server" readonly="true" /></td>
                                            <td>Consumido (jornadas)</td>
                                            <td><asp:TextBox ID="txtConJor" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                                            <td>Avance teórico</td>
                                            <td><asp:TextBox ID="txtAvanEst" style="width:40px" runat="server" SkinID="Numero" readonly="true" />%</td>
                                        </tr>
                                    </table>
                            </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="800px" align="center" cellpadding="5">
                                    <tr>
	                                    <td style="width:400px;">                                     
		                                    <fieldset style="width:385px; height:160px;">
			                                    <legend>Indicaciones del responsable</legend>
                                                <table align="center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                                                    <tr>
                                                        <td width="50%">Total previsto (horas)&nbsp;&nbsp;<asp:TextBox ID="txtTotPre" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                                                        <td width="50%">Fecha fin prevista&nbsp;&nbsp;<asp:TextBox ID="txtFinPre" style="width:60px" runat="server" readonly="true" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">&nbsp;Particulares<br />
                                                        <asp:TextBox ID="txtIndicaciones" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="2" Width="365px" readonly="true" Height="35px"></asp:TextBox>
                                                        <br />&nbsp;Colectivas<br />
                                                        <asp:TextBox ID="txtColectivas" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="2" Width="365px" readonly="true" Height="35px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                        <td style="width:400px;">
			                                <fieldset style="width:390px;height:160px;">
				                                <legend>Comentarios del técnico</legend>
                                                <table align="center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                                                    <tr>
                                                        <td width="43%">Total estimado (horas)</td>
                                                        <td width="57%"><asp:TextBox ID="txtTotEst" SkinID="Numero" style="width:60px" runat="server" onfocus="fn(this)" onchange="controlHorasPtes();" onKeyUp="javascript:$I('chkFinalizada').checked=false;" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Fecha de finalización estimada</td>
                                                        <td><asp:TextBox ID="txtFinEst" style="width:60px;cursor:pointer;" onchange="activarGrabar();" runat="server" Calendar="oCal" />
                                                        <asp:CheckBox ID="chkFinalizada" runat="server" style="cursor:pointer" Text="Tarea finalizada" Width="105px" onclick="controlTareaFin();" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"><asp:TextBox ID="txtComentario" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="4" Width="365px" Height="70px" onkeyup="activarGrabar();" ></asp:TextBox></td>
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

                <eo:PageView CssClass="PageView" runat="server">	
                <!-- Pestaña 2 -->
                <br />
                <table style="width:816px;" align="center">
                    <tr>
	                    <td>
		                <TABLE id="Table2" style="WIDTH: 800px; HEIGHT: 17px">
		                    <colgroup>
		                    <col style="width:290px"/>
		                    <col style="width:235px"/>
		                    <col style="width:225px"/>
		                    <col style="width:50px"/>
		                    </colgroup>
			                <TR class="TBLINI">
				                <td>&nbsp;Descripción</TD>
				                <td>Archivo</TD>
				                <td>Link</TD>
				                <td>Autor</TD>
			                </TR>
		                </TABLE>
		                <DIV id="divCatalogoDoc" style="OVERFLOW: auto; WIDTH: 816px; height:410px" runat="server">
		                    <div id="div1" style="background-image:url(../../../Images/imgFT20.gif);width: 800px;" runat="server">
		                    </DIV>
                        </DIV>
		                <TABLE id="Table1" style="WIDTH: 800px; HEIGHT: 17px">
			                <TR class="TBLFIN">
				                <TD></TD>
			                </TR>
		                </TABLE>
		                <br />
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <center>
                            <table style="width:250px">
                            <tr>
                                <td width="45%">
			                        <button id="btnNuevo" type="button" onclick="javascript:if (bEstadoLectura) return;nuevoDoc('T', $I('hdnIdTarea').value, 'IAP');" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                         onmouseover="se(this, 25);mostrarCursor(this);">
				                        <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			                        </button>	
                                </td>
                                <td width="10%"></td>
                                <td width="45%">
			                        <button id="btnEliminar" type="button" onclick="javascript:if (bEstadoLectura) return;eliminarDoc()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                         onmouseover="se(this, 25);mostrarCursor(this);">
				                        <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			                        </button>	
		                        </td>
                            </tr>
                            </table>
                        </center>                    
                    </td>
                    </tr>
                </table>
                <br />

                <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
                <eo:PageView CssClass="PageView" runat="server">	                

                <!-- Pestaña 3 Notas-->
                <br>
                <table class="texto" width="100%" align="center" border="0">
                    <tr>
	                    <td>
	                    &nbsp;&nbsp;Investigación / Detección
                            <asp:TextBox ID="txtNotas1" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="6" Width="800px" onKeyUp="activarGrabar();" />
                            <br /><br />
	                    &nbsp;&nbsp;Acciones / Modificaciones
                            <asp:TextBox ID="txtNotas2" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="6" Width="800px" onKeyUp="activarGrabar();" />
                            <br /><br />
	                    &nbsp;&nbsp;Pruebas
                            <asp:TextBox ID="txtNotas3" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="6" Width="800px" onKeyUp="activarGrabar();" />
                            <br /><br />
	                    &nbsp;&nbsp;Pasos a producción
                            <asp:TextBox ID="txtNotas4" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="6" Width="800px" onKeyUp="activarGrabar();" />
	                    </td>
                    </tr>
                </table>
                </eo:PageView>
           </eo:MultiPage>
        </td>
        </tr>
        </table><br /><br />
        <center>
        <table style="width:250px">
	        <tr> 
		        <td width="45%">
				    <button id="btnGrabarSalir" disabled type="button" onclick="grabarsalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
				    </button>		
		        </td>
		        <td width="10%"></td>
		        <td width="45%">
				    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
				    </button>	
		        </td>
	        </tr>
        </table>
        </center>	
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnIdTarea" id="hdnIdTarea" value="<%=nIdTarea %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
		        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
		
				//alert("strBoton: "+ strBoton);
//				switch (strBoton){
//					case "regresar": 
//					{
//					    comprobarGrabarOtrosDatos();
//						bEnviar = true;
//						break;
//					}
//				}
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
