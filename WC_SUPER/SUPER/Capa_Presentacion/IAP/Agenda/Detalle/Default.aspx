<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_Agenda_Detalle_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

	var bLectura = <%=sLectura%>;
	var bNuevo = "<%=bNuevo%>";
	var strMsg = "<%=strMsg%>";
	var strFechaIniInicio = "<%=this.txtFechaIni.Text%>";
	var strFechaHoy = "<%=System.DateTime.Today.ToShortDateString()%>";
	var strHoraIni = "";
	var strHoraFin = "";
	var nRecurso = "<%=nRecurso%>";
    var sPerfil = "<%=Session["perfil_iap"].ToString()%>";
    var nUsuarioConectado = "<%=Session["IDFICEPI_ENTRADA"].ToString()%>";
	var nProfesional = "<%=nProfesional%>";
	var nPromotor = "<%=nPromotor%>";
	var sCodRedProfesional = "<%=sCodRedProfesional%>";
    var sCodRedPromotor = "<%=sCodRedPromotor%>";
    var bSalir = false;
</script>
<table class="texto" style="width:1000px;">
    <colgroup><col style="width:460px;"/><col style="width:350px;"/><col style="width:180px;"/></colgroup>
    <tr style="vertical-align:top; height:200px;">
        <td>
            <fieldset style="height:180px; width:445px;">
	            <legend>&nbsp;Datos generales&nbsp;</legend>
	            <table class="texto" cellspacing="2px" cellpadding="3px" style="width:100%;">
	            <colgroup><col style="width:17%" /><col style="width:83%" /></colgroup>
		            <tr>
			            <td>Profesional</td>
			            <td><asp:TextBox id="txtInteresado" runat="server" Width="355px"></asp:TextBox></td>
		            </tr>
		            <tr>
			            <td>Promotor</td>
			            <td><asp:TextBox id="txtPromotor" runat="server" Width="355px"></asp:TextBox></td>
		            </tr>
		            <tr>
			            <td title="Información de visualización pública">Asunto</td>
			            <td><asp:TextBox id="txtAsunto" runat="server" Width="355px" maxlength="50" onkeypress="aG()"></asp:TextBox></td>
		            </tr>
		            <tr id="filaMotivo" runat="server">
			            <td style="vertical-align:top;" title="Información restringida al interesado  y convocados">Motivo</td>
			            <td><asp:TextBox id="txtMotivo" runat="server" Width="355px" SkinID="Multi" Rows="3" TextMode="MultiLine" onkeypress="aG()"></asp:TextBox></td>
		            </tr>
		            <tr>
			            <td><label id="lblTarea" class="enlace" onclick="getTarea()" onmouseover="mostrarCursor(this)">Tarea</label></td>
			            <td>
			                <asp:TextBox id="txtIDTarea" runat="server" SkinID="Numero" Width="50px"  onkeyup="$('txtDesTarea').value='';aG();if (event.keyCode==13){validarTarea(this.value);}"></asp:TextBox>
			                <asp:TextBox id="txtDesTarea" runat="server" Width="302px" onkeypress="aG()"></asp:TextBox>
			            </td>
		            </tr>
	            </table>
            </fieldset>
        </td>
        <td>
	        <fieldset id="fstTemporal" style="PADDING-RIGHT: 10px; PADDING-LEFT: 10px; height: 180px; width:325px;">
	            <legend>Rango temporal</legend>
                    <fieldset style="PADDING-RIGHT: 1px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px; width:320px;">
                        <legend>Inicio</legend>
                        <center>
                        <asp:Label id="Label1" runat="server" Width="40px">Fecha</asp:Label>
                        <asp:textbox id="txtFechaIni" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="validarFecha(this);" runat="server"
				                        ontextchanged="txtFechaIni_TextChanged"></asp:textbox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Label id="Label3" runat="server" Width="30px">Hora</asp:Label>
                        <asp:dropdownlist id="cboHoraIni" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG()">
				        <asp:ListItem Value="0:00">0:00</asp:ListItem>
				        <asp:ListItem Value="0:30">0:30</asp:ListItem>
				        <asp:ListItem Value="1:00">1:00</asp:ListItem>
				        <asp:ListItem Value="1:30">1:30</asp:ListItem>
				        <asp:ListItem Value="2:00">2:00</asp:ListItem>
				        <asp:ListItem Value="2:30">2:30</asp:ListItem>
				        <asp:ListItem Value="3:00">3:00</asp:ListItem>
				        <asp:ListItem Value="3:30">3:30</asp:ListItem>
				        <asp:ListItem Value="4:00">4:00</asp:ListItem>
				        <asp:ListItem Value="4:30">4:30</asp:ListItem>
				        <asp:ListItem Value="5:00">5:00</asp:ListItem>
				        <asp:ListItem Value="5:30">5:30</asp:ListItem>
				        <asp:ListItem Value="6:00">6:00</asp:ListItem>
				        <asp:ListItem Value="6:30">6:30</asp:ListItem>
				        <asp:ListItem Value="7:00">7:00</asp:ListItem>
				        <asp:ListItem Value="7:30">7:30</asp:ListItem>
				        <asp:ListItem Value="8:00">8:00</asp:ListItem>
				        <asp:ListItem Value="8:30">8:30</asp:ListItem>
				        <asp:ListItem Value="9:00">9:00</asp:ListItem>
				        <asp:ListItem Value="9:30">9:30</asp:ListItem>
				        <asp:ListItem Value="10:00">10:00</asp:ListItem>
				        <asp:ListItem Value="10:30">10:30</asp:ListItem>
				        <asp:ListItem Value="11:00">11:00</asp:ListItem>
				        <asp:ListItem Value="11:30">11:30</asp:ListItem>
				        <asp:ListItem Value="12:00">12:00</asp:ListItem>
				        <asp:ListItem Value="12:30">12:30</asp:ListItem>
				        <asp:ListItem Value="13:00">13:00</asp:ListItem>
				        <asp:ListItem Value="13:30">13:30</asp:ListItem>
				        <asp:ListItem Value="14:00">14:00</asp:ListItem>
				        <asp:ListItem Value="14:30">14:30</asp:ListItem>
				        <asp:ListItem Value="15:00">15:00</asp:ListItem>
				        <asp:ListItem Value="15:30">15:30</asp:ListItem>
				        <asp:ListItem Value="16:00">16:00</asp:ListItem>
				        <asp:ListItem Value="16:30">16:30</asp:ListItem>
				        <asp:ListItem Value="17:00">17:00</asp:ListItem>
				        <asp:ListItem Value="17:30">17:30</asp:ListItem>
				        <asp:ListItem Value="18:00">18:00</asp:ListItem>
				        <asp:ListItem Value="18:30">18:30</asp:ListItem>
				        <asp:ListItem Value="19:00">19:00</asp:ListItem>
				        <asp:ListItem Value="19:30">19:30</asp:ListItem>
				        <asp:ListItem Value="20:00">20:00</asp:ListItem>
				        <asp:ListItem Value="20:30">20:30</asp:ListItem>
				        <asp:ListItem Value="21:00">21:00</asp:ListItem>
				        <asp:ListItem Value="21:30">21:30</asp:ListItem>
				        <asp:ListItem Value="22:00">22:00</asp:ListItem>
				        <asp:ListItem Value="22:30">22:30</asp:ListItem>
				        <asp:ListItem Value="23:00">23:00</asp:ListItem>
				        <asp:ListItem Value="23:30">23:30</asp:ListItem>
			        </asp:dropdownlist>
			        </center>
			        </fieldset>
			        <br/>
			        <br/>
                    <fieldset style="PADDING-RIGHT: 1px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px; width:320px;">
                    <legend>Fin</legend>
                    <center>
                    <asp:Label id="Label2" runat="server" Width="40px">&nbsp;Fecha</asp:Label>
                    <asp:textbox id="txtFechaFin" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="validarFecha(this);" runat="server"
				                    ontextchanged="txtFechaFin_TextChanged"></asp:textbox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label id="Label4" runat="server" Width="30px">Hora</asp:Label>
                    <asp:dropdownlist id="cboHoraFin" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG()">
				        <asp:ListItem Value="0:30">0:30</asp:ListItem>
				        <asp:ListItem Value="1:00">1:00</asp:ListItem>
				        <asp:ListItem Value="1:30">1:30</asp:ListItem>
				        <asp:ListItem Value="2:00">2:00</asp:ListItem>
				        <asp:ListItem Value="2:30">2:30</asp:ListItem>
				        <asp:ListItem Value="3:00">3:00</asp:ListItem>
				        <asp:ListItem Value="3:30">3:30</asp:ListItem>
				        <asp:ListItem Value="4:00">4:00</asp:ListItem>
				        <asp:ListItem Value="4:30">4:30</asp:ListItem>
				        <asp:ListItem Value="5:00">5:00</asp:ListItem>
				        <asp:ListItem Value="5:30">5:30</asp:ListItem>
				        <asp:ListItem Value="6:00">6:00</asp:ListItem>
				        <asp:ListItem Value="6:30">6:30</asp:ListItem>
	                    <asp:ListItem Value="7:00">7:00</asp:ListItem>
	                    <asp:ListItem Value="7:30">7:30</asp:ListItem>
	                    <asp:ListItem Value="8:00">8:00</asp:ListItem>
	                    <asp:ListItem Value="8:30">8:30</asp:ListItem>
	                    <asp:ListItem Value="9:00">9:00</asp:ListItem>
	                    <asp:ListItem Value="9:30">9:30</asp:ListItem>
	                    <asp:ListItem Value="10:00">10:00</asp:ListItem>
	                    <asp:ListItem Value="10:30">10:30</asp:ListItem>
	                    <asp:ListItem Value="11:00">11:00</asp:ListItem>
	                    <asp:ListItem Value="11:30">11:30</asp:ListItem>
	                    <asp:ListItem Value="12:00">12:00</asp:ListItem>
	                    <asp:ListItem Value="12:30">12:30</asp:ListItem>
	                    <asp:ListItem Value="13:00">13:00</asp:ListItem>
	                    <asp:ListItem Value="13:30">13:30</asp:ListItem>
	                    <asp:ListItem Value="14:00">14:00</asp:ListItem>
	                    <asp:ListItem Value="14:30">14:30</asp:ListItem>
	                    <asp:ListItem Value="15:00">15:00</asp:ListItem>
	                    <asp:ListItem Value="15:30">15:30</asp:ListItem>
	                    <asp:ListItem Value="16:00">16:00</asp:ListItem>
	                    <asp:ListItem Value="16:30">16:30</asp:ListItem>
	                    <asp:ListItem Value="17:00">17:00</asp:ListItem>
	                    <asp:ListItem Value="17:30">17:30</asp:ListItem>
	                    <asp:ListItem Value="18:00">18:00</asp:ListItem>
	                    <asp:ListItem Value="18:30">18:30</asp:ListItem>
	                    <asp:ListItem Value="19:00">19:00</asp:ListItem>
	                    <asp:ListItem Value="19:30">19:30</asp:ListItem>
	                    <asp:ListItem Value="20:00">20:00</asp:ListItem>
	                    <asp:ListItem Value="20:30">20:30</asp:ListItem>
	                    <asp:ListItem Value="21:00">21:00</asp:ListItem>
			            <asp:ListItem Value="21:30">21:30</asp:ListItem>
			            <asp:ListItem Value="22:00">22:00</asp:ListItem>
			            <asp:ListItem Value="22:30">22:30</asp:ListItem>
			            <asp:ListItem Value="23:00">23:00</asp:ListItem>
			            <asp:ListItem Value="23:30">23:30</asp:ListItem>
			            <asp:ListItem Value="0:00">24:00</asp:ListItem>
	                    </asp:dropdownlist>
	                    </center>
			         </fieldset>
	                <br>
	                <br>
                    <fieldset id="lgdDias" style="PADDING-RIGHT: 1px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px; width:320px;" runat="server">
			            <legend title="Se realizará la reserva en los días de la semana marcados, comprendidos entre las fechas de inicio y fin">Días de repetición</legend>
			            <center>
			            <asp:CheckBoxList id="chkDias" runat="server" Width="310px" CssClass="texto" RepeatDirection="Horizontal">
				            <asp:ListItem Value="Lun" onclick="aG()">Lun</asp:ListItem>
				            <asp:ListItem Value="Mar" onclick="aG()">Mar</asp:ListItem>
				            <asp:ListItem Value="Mi&#233;" onclick="aG()">Mi&#233;</asp:ListItem>
				            <asp:ListItem Value="Jue" onclick="aG()">Jue</asp:ListItem>
				            <asp:ListItem Value="Vie" onclick="aG()">Vie</asp:ListItem>
				            <asp:ListItem Value="S&#225;b" onclick="aG()">S&#225;b</asp:ListItem>
				            <asp:ListItem Value="Dom" onclick="aG()">Dom</asp:ListItem>
			            </asp:CheckBoxList>
			            </center>
		            </fieldset> 
	        </fieldset>
        </td>
        <td rowspan="3" style="padding-top:5px;">
	        <fieldset id="fldDisponibilidad" style="PADDING-RIGHT:5px; WIDTH:170px; PADDING-TOP:0px; height:537px;" runat="server">
		        <legend>&nbsp;Ocupación&nbsp;</legend>
		        <table style="width:170px; padding:0px; margin:0px;">
		            <tr>
		                <td>
		                    <table class="title2" style="width:111px; margin-left:41px;">
		                    <tr><td id="cldReferencia"><%=this.txtFechaIni.Text%></td></tr>
		                    </table>
    		             </td>
                    </tr>    		 
		            <tr>
		                <td>    		 
		                    <div id="divContenido" style="OVERFLOW:auto; margin-left:5px; WIDTH:162px; HEIGHT:484px;">
		                        <div id="ZZ" style="width:140px;">
	 		                        <asp:Table id="tblCal" runat="server" EnableViewState="False" ></asp:Table>
			                    </div>
	                        </div>
    		             </td>
                    </tr>    		 
		            <tr>
		                <td> 	        
		                    <table class="title2" style="width:111px; margin-left:41px;">
		                    <tr><td id="cldTot" runat="server">0</td></tr>
		                    </table>
    		             </td>
                    </tr>    		        
                </table>
	        </fieldset>
        </td>
    </tr>
    <tr style="height:145px;">
        <td>
	        <fieldset id="fldObservaciones" style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:0px; PADDING-TOP:0px; height:140px; width:445px;" runat="server">
		        <legend>Observaciones</legend>
		        <asp:TextBox id="txtObservaciones" runat="server" Width="430px" SkinID="Multi" Rows="7" TextMode="MultiLine" style="margin:5px;" onkeypress="aG()"></asp:TextBox>
	        </fieldset>
        </td>
        <td>
	        <fieldset id="fldPrivado" style="PADDING-RIGHT: 5px; PADDING-LEFT:5px; PADDING-BOTTOM:0px; PADDING-TOP:0px; height:140px; width:335px;" runat="server">
		        <legend title="Información privada del interesado">Privado</legend>
		        <asp:TextBox id="txtPrivado" runat="server" Width="320px" SkinID="Multi" Rows="7" TextMode="MultiLine" style="margin:5px;" onkeypress="aG()"></asp:TextBox>
	        </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <fieldset id="fldSeleccion" style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:0px; PADDING-TOP:2px; height:198px; width:800px;" runat="server">
                <legend>Otros profesionales</legend>
                    <table class="texto" id="Table1" cellspacing="1px" cellpadding="1px" style="width:790px; margin-top:5px;" >
                    <colgroup><col style="width:370px;" /><col style="width:50px;" /><col style="width:370px;" /></colgroup>
			        <TBODY>
				        <tr>
					        <td>
                                <table id="tblApellidos" class="texto" style="width:330px; margin-bottom:5px;">
                                    <colgroup>
                                    <col style="width:110px"/>
                                    <col  style="width:110px"/>
                                    <col  style="width:110px"/>
                                </colgroup>
                                    <tr>
                                        <td>&nbsp;Apellido1</td>
                                        <td>&nbsp;Apellido2</td>
                                        <td>&nbsp;Nombre</td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" CssClass="textareatexto" /></td>
                                        <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" CssClass="textareatexto" /></td>
                                        <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" CssClass="textareatexto" /></td>
                                    </tr>
                                </table>
					        </td>
					        <td></td>
					        <td>&nbsp;</td>
				        </tr>
				        <tr>
					        <td>
						        <table id="tblTitulo" style="width:340px">
							        <tr class="TBLINI">
								        <td>&nbsp;Profesional&nbsp;
								            <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" tipolupa="2" width="20px">
								            <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../../Images/imgLupa.gif" width="20px" tipolupa="1">
							            </td>
							        </tr>
						        </table>
						        <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width: 356px; height: 90px;"
							        align="left">
                                     <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width: 340px">
							            <table class="texto" id="tblDatos" style="width:340px">
							            </table>
							        </div>
						        </div>
						        <table id="TBLFIN" style="width:340px; height:17px;">
							        <tr class="TBLFIN">
								        <td colspan="2">&nbsp;</td>
							        </tr>
						        </table>
					        </td>
					        <td>
                                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"/>
					        </td>
					        <td>
						        <table id="tblTitulo2" style="width:340px; height:17px;">
							        <tr class="TBLINI">
								        <td>&nbsp;Profesionales a asignar ocupación&nbsp;
									        <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo2','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" tipolupa="2" width="20">
								            <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo2','imgLupa2',event)" height="11" src="../../../../Images/imgLupa.gif" tipolupa="1" width="20">
								        </td>
							        </tr>
					            </table>
					            <div id="divCatalogo2" style="OVERFLOW:auto; overflow-x:hidden; WIDTH:356px; HEIGHT:90px;" target="true" onmouseover="setTarget(this);" caso="2">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width: 340px;">
                                    <table id="tblDatos2" class="texto MM" style="width:340px">
                                    </table>
                                    </div>
                                </div>
                                  <table id="TBLFIN2" style="width:340px; height:17px;">
							            <tr class="TBLFIN">
								            <td colspan="2">&nbsp;</td>
							            </tr>
					            </table>
					        </td>
				        </tr>
				        </TBODY>
                    </table>
            </fieldset>
        </td>
    </tr>
</table>
<div class="clsDragWindow" id="DW" nowrap></div>
<asp:TextBox id="hdnIDReserva" style="visibility: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnAsistentes" style="visibility: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnAnulacion" style="visibility: hidden" runat="server" value=""></asp:TextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "grabarreg": 
				{
				    bEnviar = false;
				    setTimeout("grabar()", 20);
					break;
				}
				case "eliminar": 
				{
				    bEnviar = false;
				    setTimeout("eliminar()", 20);
					break;
				}
				case "regresar":
				{
                    if (bCambios){
		                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war",330).then(function (answer) {
		                    if (answer) {
		                        bEnviar = false;
		                        bSalir=true;
		                        setTimeout("grabar()", 20);
		                    }else {
		                        bEnviar = true;
		                        bCambios = false;                               
		                        fSubmit(bEnviar,eventTarget,eventArgument);
		                    }
		                });
                    } else fSubmit(bEnviar, eventTarget, eventArgument);

                    break;
				}
				case "guia": 
				{
				    bEnviar = false;
				    setTimeout("mostrarGuia('Agenda.pdf');", 20);
				    break;
                }
            }
			if (strBoton != "regresar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
	function fSubmit(bEnviar, eventTarget, eventArgument)
    {
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

