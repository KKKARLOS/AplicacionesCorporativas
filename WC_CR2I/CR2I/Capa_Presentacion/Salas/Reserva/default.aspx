<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Salas.Reserva.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
	var bLectura = <%=sLectura%>;
	var bNuevo = "<%=bNuevo%>";
	var strMsg = "<%=strMsg%>";
	var strLocation = "<%=strLocation%>";
	var strServer = "<%=Session["strServer"]%>";
	var strFechaIniInicio = "<%=this.txtFechaIni.Text%>";
	var strFechaHoy = "<%=System.DateTime.Today.ToShortDateString()%>";
	var strHoraIni = "";
	var strHoraFin = "";
	var nRequisitos = "<%=nRequisitos%>";
</script>
<label id="lblRequisitos" runat="server" class="enlace" style="position:absolute;top:160px;left:30px; color:Red; display:none;text-decoration:none;">Requisitos</label>
<div id="AreaTrabajo" style="height:100%;width:100%; padding: 0px;">
    <table style="width:100%;">
		<tr>
			<td colspan="2" style='text-align:center;'>
				<table class="texto" id="tblFiltros" style="width:100%; text-align:left;" border="0">
					<tr>
						<td width="58%">
							<fieldset style="padding-left:10px;height: 220px; width:430px; margin-left:120px;">
								<legend>&nbsp;Datos generales&nbsp;</legend>
								<table class="texto" id="tblDatos" cellpadding="5px" width="100%" border="0" align="left" style="margin-top:5px;">
									<tr>
										<td style="width:20%; text-align:left;">Oficina</td>
										<td width="80%">
											<asp:dropdownlist id="cboOficina" AutoPostBack="True" runat="server" Width="325px"
												DataValueField="CODIGO" DataTextField="DESCRIPCION" onselectedindexchanged="cboOficina_SelectedIndexChanged">
										    </asp:dropdownlist>
										</td>
									</tr>
									<tr>
										<td style="width:20%; text-align:left;">Sala</td>
										<td width="80%">
											<asp:dropdownlist id="cboSala" AutoPostBack="True" runat="server" Width="325px" CssClass="combo" DataValueField="CODIGO"
												DataTextField="DESCRIPCION" onselectedindexchanged="cboSala_SelectedIndexChanged"></asp:dropdownlist></td>
									</tr>
									<tr>
										<td style="width:20%; text-align:left;">Características</td>
										<td width="80%">
											<asp:TextBox id="txtCarac" runat="server" style="width:325px; " SkinID="Multi"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td style="width:20%; text-align:left;">
										    <% if ((Session["CR2I_PERFIL"].ToString() == "A") || (Session["CR2I_RESSALA"].ToString() == "E")){ %>
										        <asp:Label SkinID="enlace" id="lblInter" onmouseover="mostrarCursor(this)" onclick="obtenerInteresado()" runat="server">Interesado</asp:label>
											<% }else{ %>
											Interesado
											<% } %>
										</td>
										<td width="80%">
											<asp:TextBox id="txtInteresado" runat="server" Width="295px" class="textareaTexto"></asp:TextBox>
											<label id="lblQEQ" onclick="mostrarQEQ()" class="enlace" >QEQ</label>
									    </td>
									</tr>
									<tr>
										<td style="width:20%; text-align:left;vertical-align:top;">
											<label id="lblAsunto" runat="server" class="tooltip" title="Información de visualización pública">Asunto</label>
										</td>
										<td width="80%">
											<asp:TextBox id="txtAsunto" runat="server" style="width:325px;" SkinID="Multi" maxlength="50"></asp:TextBox>
										</td>
									</tr>
									<tr id="filaMotivo" runat="server">
										<td style="width:20%; text-align:left; vertical-align:top;">
											<label id="lblMotivo" runat="server" class="tooltip" title="Información restringida al interesado  y convocados">Motivo</label>
									    </td>
										<td width="80%">
											<asp:TextBox id="txtMotivo" runat="server" style="width:325px; height:45px;" SkinID="Multi" TextMode="MultiLine"></asp:TextBox>
										</td>
									</tr>
								</table>
							</fieldset>
						</td>
						<td style="vertical-align:top;" width="42%">
							<fieldset id="fstTemporal" style=" padding-right: 10px; padding-left: 10px; height: 220px; width: 325px;">
							<legend>&nbsp;Rango temporal&nbsp;</legend>
                                <div style="margin-top:20px; text-align:right; margin-right:75px;">
									<asp:CheckBox id="chkTodoDia" onclick="mostrarTodoElDia()" runat="server"  Text="Todo el día" style="width:80px;"></asp:CheckBox>
							    </div>
							    <br class="clear" />
                                <fieldset style="padding-right: 2px; padding-left: 10px; padding-bottom: 2px; padding-top: 2px; width:240px; height:30px;">
                                <legend>&nbsp;Inicio&nbsp; </legend>
                                    <asp:Label id="Label1" runat="server" style="width:40px; margin-left:10px;">&nbsp;Fecha</asp:Label>
                                    <asp:textbox id="txtFechaIni" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" goma="0"
                                        ontextchanged="txtFechaIni_TextChanged" onchange="validarFecha(this);">
                                    </asp:textbox>
                                    <asp:Label id="Label3" runat="server" style="width:30px; margin-left:25px;">Hora</asp:Label>
                                    <asp:dropdownlist id="cboHoraIni" runat="server" Width="60px" CssClass="combo" onChange="setChkTodoDia();">
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
								    </asp:dropdownlist></fieldset>
								<br class="clear" /><br class="clear" /><br class="clear" />
                                <fieldset style=" padding-right: 2px; padding-left: 10px; padding-bottom: 2px; padding-top: 2px; width:240px; height:30px;">
                                <legend>&nbsp;Fin&nbsp;</legend>
                                    <asp:Label id="Label2" runat="server" style="width:40px; margin-left:10px;">&nbsp;Fecha</asp:Label>
                                    <asp:textbox id="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" goma="0"
                                        ontextchanged="txtFechaFin_TextChanged" onchange="validarFecha(this);">
                                    </asp:textbox>
                                    <asp:Label id="Label4" runat="server" style="width:30px; margin-left:25px;">Hora</asp:Label>
                                    <asp:dropdownlist id="cboHoraFin" runat="server" Width="60px" CssClass="combo" onChange="setChkTodoDia();">
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
							        </asp:dropdownlist>
							    </fieldset>
							    <br class="clear" /><br class="clear" /><br class="clear" />
                                <fieldset id="lgdDias" style=" padding-right: 2px; padding-left: 10px; padding-bottom: 2px; padding-top: 2px; width:315px; height:40px;" runat="server">
							    <legend class="tooltip" title="Se realizará la reserva en los días de la semana marcados, comprendidos entre las fechas de inicio y fin">&nbsp;Días&nbsp;de repetición&nbsp;</legend>
							        <asp:CheckBoxList id="chkDias" runat="server" style="width:310px; margin-top:10px; margin-left:5px;" CssClass="texto" RepeatDirection="Horizontal">
								        <asp:ListItem Value="Lun">Lun</asp:ListItem>
								        <asp:ListItem Value="Mar">Mar</asp:ListItem>
								        <asp:ListItem Value="Mi&#233;">Mi&#233;</asp:ListItem>
								        <asp:ListItem Value="Jue">Jue</asp:ListItem>
								        <asp:ListItem Value="Vie">Vie</asp:ListItem>
								        <asp:ListItem Value="S&#225;b">S&#225;b</asp:ListItem>
								        <asp:ListItem Value="Dom">Dom</asp:ListItem>
							        </asp:CheckBoxList>
						        </fieldset> 
							</fieldset>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br />
    <table style="width:95%; margin-left:25px;">
		<tr>
			<td style="vertical-align:top; width:78%; margin-top:10px; ">
				<fieldset id="fldSeleccion" style="width:99%; padding-right:5px; padding-left:5px; padding-bottom:5px; padding-top:0px; height:220px" runat="server">
					<legend>&nbsp;Selección de convocados&nbsp;</legend>
                    <table class="texto" id="Table1" width="100%" style='text-align:left;' border="0">
						<tr>
							<td style="width:48%;">
                                <table border="0" class="texto"style=" width: 320px;" cellpadding="0" cellspacing="0">
                                    <tr>
                                    <td>&nbsp;&nbsp;Apellido1</td>
                                    <td>&nbsp;&nbsp;Apellido2</td>
                                    <td>&nbsp;&nbsp;Nombre</td>
                                    </tr>
                                    <tr>
                                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" CssClass="textareaTexto" /></td>
                                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" CssClass="textareaTexto" /></td>
                                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" CssClass="textareaTexto" /></td>
                                    </tr>
                                </table>
							</td>
							<td style="width:5%;"></td>
							<td style="vertical-align:top; text-align:right; width:47%;">
								<asp:CheckBox id="chkCorreo" runat="server" Text="Suscribir notificaciones por correo" Checked="True" style="width:250px;"></asp:CheckBox>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						    </td>
						</tr>
						<tr>
							<td>
								<table id="tblTitulo" style="width:320px;">
									<tr class="TBLINI">
										<td width="80%"><img style=" cursor: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
												height="11" src="../../../Images/imgLupa.gif" width="20"> <img id="imgLupa1" style=" display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
												height="11" src="../../../Images/imgLupaMas.gif" width="20">&nbsp;&nbsp;Personas</td>
										<td width="20%">
										    <img alt="Desplazar hacia arriba" onmouseover="javascript:bMover=true;scrollUp('divCatalogo',16);" style=" cursor: pointer" onmouseout="javascript:bMover=false;"
												height="8px" src="../../../Images/imgFleUp.gif" width="11px" />
										</td>
									</tr>
								</table>
								<div id="divCatalogo" style="OVERFLOW-X: hidden; overflow: auto; width: 336px; height: 90px; text-align:left;">
									<table class="texto" id="tblOpciones" style="WIDTH: 320px; text-align:left;">
									</table>
								</div>
								<table id="tblResultado" style="width:320px;">
									<tr class="TBLFIN">
										<td>
										    <img height="1" src="../../../Images/imgSeparador.gif" width="80%" border="0" />
											<img onmouseover="javascript:bMover=true;scrollDown('divCatalogo',16);" style="cursor: pointer" onmouseout="javascript:bMover=false;"
												height="8px" src="../../../Images/imgFleDown.gif" width="11px" />
										</td>
									</tr>
								</table>
							</td>
							<td>
								<asp:Image id="Image2" style=" cursor: pointer" onclick="anadirConvocados()" runat="server" ImageUrl="../../../Images/imgNextpg.gif"></asp:Image>
								<br /><br />
								<asp:Image id="Image3" style="CURSOR: pointer" onclick="quitarConvocados()" runat="server" ImageUrl="../../../Images/imgPrevpg.gif"></asp:Image></td>
							<td style="vertical-align:top; padding-left:5px;">
								<table id="tblTitulo2" style="width:320px;">
									<tr class="TBLINI">
										<td width="80%">
										    <img style=" cursor: pointer" onclick="buscarDescripcion('tblOpciones2',0,'divCatalogo2','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20"> 
										    <img id="imgLupa2" style=" display: none; cursor: pointer; height:11px;" onclick="buscarSiguiente('tblOpciones2',0,'divCatalogo2','imgLupa2')" src="../../../Images/imgLupaMas.gif" width="20">&nbsp;&nbsp;Convocados
										</td>
										<td width="20%">
										    <img onmouseover="javascript:bMover=true;scrollUp('divCatalogo2',16);" style=" cursor: pointer; height:8px; width:11px;" onmouseout="javascript:bMover=false;" src="../../../Images/imgFleUp.gif">
										</td>
									</tr>
								</table>
								<div id="divCatalogo2" style="OVERFLOW-X: hidden; overflow: auto; width: 336px; height: 90px; text-align:left;">
									<asp:repeater id="tblCatalogo" runat="server">
										<ItemTemplate>
											<tr class="FA" style=" cursor: pointer" id='<%# Eval("CODIGO") %>' onClick="marcarEstaFila(this,false)" onDblClick="desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');">
												<td style="width:320px; padding-left:5px"><%# Eval("DESCRIPCION") %></td>
											</tr>
										</ItemTemplate>
										<AlternatingItemTemplate>
											<tr class="FB" style=" cursor: pointer" id='<%# Eval("CODIGO") %>' onClick="marcarEstaFila(this,false)" onDblClick="desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');">
												<td style="width:320px; padding-left:5px"><%# Eval("DESCRIPCION") %></td>
											</tr>
										</AlternatingItemTemplate>
										<HeaderTemplate>
											<table id="tblOpciones2" class="texto" style=" width: 320px; border-collapse: collapse;">
										</HeaderTemplate>
										<FooterTemplate>
										    </table>
                                        </FooterTemplate>
                                    </asp:repeater>
                                 </div>
                                <table id="tblResultado2" style="width:320px;">
		                            <tr class="TBLFIN">
			                            <td width="80%">
			                                <img height="1" src="../../../Images/imgSeparador.gif" border="0" />
				                        </td>
				                        <td width="20%">
				                            <img onmouseover="javascript:bMover=true;scrollDown('divCatalogo2',16);" style=" cursor: pointer; height:8px; width:11px;" onmouseout="javascript:bMover=false;" src="../../../Images/imgFleDown.gif" />
			                            </td>
		                            </tr>
	                          </table>
				            </td>
	                    </tr>
	                    <tr>
		                    <td colspan="3" style="padding-top:10px;">
			                    <asp:Label id="lblConvocados" runat="server" Cssclass="tooltip" ToolTip="Indica las direcciones de correo de los convocados externos, separados por punto y coma">&nbsp;E-mails externos (separados por ;)</asp:Label>
			                    <br />
			                    <asp:TextBox id="txtCorreoExt" runat="server" style="width:706px; height:30px;" SkinID="Multi" TextMode="MultiLine"></asp:TextBox>
				            </td>
	                    </tr>
	                </table>
	            </fieldset>
	            <br class="clear" /><br class="clear" />
	            <table style="width:740px;" border="0">
	                <tr>
	                    <td style="width:370px;">
                            <fieldset id="fldCentralita" style="width:355px; padding-bottom: 5px; padding-top: 0px; height: 90px;" runat="server">
                                <legend class="tooltip" title="Información enviada por correo a la centralita de la oficina a la que pertenece la sala reservada">&nbsp;Mensaje 
                                    a centralita&nbsp;&nbsp;(<label style="color:Red;">Recomendado para convocar personal externo</label>)
                                </legend>
                                <asp:TextBox id="txtCentralita" runat="server" SkinID="Multi" TextMode="MultiLine" style="margin-top:10px; margin-left:5px; width:340px; height:60px;"></asp:TextBox>
                            </fieldset>
                        </td>
                        <td style="width:370px;">
                            <fieldset id="fldPrivado" style="width:344px; margin-left:5px; padding-right:10px; padding-left:5px; padding-bottom:5px; padding-top:0px; height:90px;" runat="server">
                                <legend class="tooltip" title="Información privada del interesado">&nbsp;Confidencial&nbsp;</legend>
                                <asp:TextBox id="txtPrivado" runat="server" SkinID="Multi" TextMode="MultiLine" style="margin-top:10px; margin-left:10px; width:320px; height:60px;"></asp:TextBox>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:22%;">
	            <fieldset id="fldDisponibilidad" style="margin-left:20px; padding-right:5px; padding-left:5px; padding-bottom:5px; width:174px; height:330px; padding-top: 0px;" runat="server">
		            <legend>&nbsp;Disponibilidad de sala&nbsp;</legend>
		            <div id="divContenido" style=" overflow: auto; width: 155px; height: 307px;  margin-top:5px; margin-left:10px;" runat="server">
			            <asp:Table id="tblCal" style="left: 0px; clip: rect(0px 154px 509px 0px); position:relative; top:0px" runat="server" EnableViewState="False"></asp:Table>
				    </div>
	            </fieldset>
            </td>
	    </tr>
    </table>
</div>
    <input id="hdnErrores" type="hidden" value="<%=sErrores%>" /> 
    <asp:TextBox id="txtCIP" style="visibility: hidden; width:1px;" runat="server"></asp:TextBox>
    <asp:TextBox id="hdnIDReserva" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
    <asp:TextBox id="hdnAsistentes" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
    <asp:TextBox id="hdnOrigen" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
    <asp:TextBox id="hdnAnulacion" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
    <input id="hdnFecha" type="hidden" value="" runat="server" /> 

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
function __doPostBack(eventTarget, eventArgument) {
    var bEnviar = true;
    if (eventTarget.split("$")[2] == "Botonera") {
        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
        switch (strBoton) {
            case "tramitar":
                {
                    mostrarProcesando();
                    AccionBotonera("Tramitar", "D");
                    bEnviar = comprobarDatos();
                    if (!bEnviar) {
                        AccionBotonera("Tramitar", "H");
                        ocultarProcesando();
                    }
                    break;
                }
            case "anular":
                {
                    bEnviar = false;
                    /*
                    var ret = window.showModalDialog("../../Anulacion.aspx", self, "dialogwidth:350px; dialogheight:150px; center:yes; status:NO; help:NO;");
                    if ((ret != null) && (ret != "")) {
                        var aRes = ret.split("@#@");
                        if (aRes[0] == "A") {
                            if (aRes[1] != "") {
                                $I("hdnAnulacion").value = aRes[1];
                                bEnviar = true;
                            }
                            else
                                alert("Para anular una reserva es necesario indicar el motivo");
                        }
                    }
                    */
                    var strEnlace = strServer + "Capa_Presentacion/Anulacion.aspx";
                    modalDialog.Show(strEnlace, self, sSize(350, 150))
                    .then(function(ret) 
                    {
                        if ((ret != null) && (ret != "")) {
                            var aRes = ret.split("@#@");
                            if (aRes[0] == "A") {
                                if (aRes[1] != "") {
                                    $I("hdnAnulacion").value = aRes[1];
                                    bEnviar = true;
                                }
                                else
                                    alert("Para anular una reserva es necesario indicar el motivo");
                            }
                        }
                        var theform = document.forms[0];
                        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
                        theform.__EVENTARGUMENT.value = eventArgument;
                        if (bEnviar) theform.submit();                        
                    });	                    
                    break;
                }
	    } //switch
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
