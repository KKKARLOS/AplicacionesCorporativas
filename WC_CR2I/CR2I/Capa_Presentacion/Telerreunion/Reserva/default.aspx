<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Telerreunion.Reserva.Default" %>
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
</script>
<div id="AreaTrabajo" style="height:100%;width:100%; padding: 0px;">
<table width="100%" align="center" border="0">
	<tr>
		<td>
			<table class="texto" id="tblFiltros" cellpadding="5px" style="/*width:835px;*/" border="0">
				<tr>
					<td style="width:105px;"></td>
					<td style="vertical-align:top; width:440px;">
						<fieldset style="height: 150px; width: 430px;padding-left:10px;">
							<legend>&nbsp;Datos generales&nbsp;</legend>
							<table class="texto" id="tblDatos" style="width:420px; table-layout:fixed; margin-top:10px;" cellspacing="2px" cellpadding="5px" border="0">
							<colgroup><col style="width:70px;" /><col style="width:350px;" /></colgroup>
								<tr>
									<td>Sala</td>
									<td>
										<asp:dropdownlist id="cboLicencia" AutoPostBack="True" runat="server" Width="231px" CssClass="combo" DataValueField="CODIGO"
											DataTextField="DESCRIPCION" onselectedindexchanged="cboLicencia_SelectedIndexChanged"></asp:dropdownlist>
									    <label id="lblVozIP" class="texto" style="margin-left:15px;">Voz IP</label>
									    <asp:CheckBox id="chkVozIP" runat="server" CssClass="texto"></asp:CheckBox>
								    </td>
								</tr>
								<tr>
									<td>
										<% if ((Session["CR2I_PERFIL"].ToString() == "A") || (Session["RESWEBEX"].ToString() == "E")){ %>
										    <asp:Label SkinID="enlace" id="lblInter" onmouseover="mostrarCursor(this)" onclick="obtenerInteresado()" runat="server">Interesado</asp:Label>
										<% }else{ %>
										Interesado
										<% } %>
									</td>
									<td>
										<asp:TextBox id="txtInteresado" runat="server" style="width:295px;" class="textareaTexto"></asp:TextBox>
										<label id="lblQEQ" onclick="mostrarQEQ()" runat="server" class="enlace" Visible="True">QEQ</label>
									</td>
								</tr>
								<tr>
									<td style="vertical-align:top;">
										<label id="lblAsunto" runat="server" class="tooltip" title="Información de visualización pública">Asunto</label></td>
									<td>
										<asp:TextBox id="txtAsunto" runat="server" style="width:325px;" SkinID="Multi" maxlength="50"></asp:TextBox>
									</td>
								</tr>
								<tr id="filaMotivo" runat="server">
									<td style="vertical-align:top;">
										<label id="lblMotivo" runat="server" class="tooltip" title="Información restringida al interesado y convocados">Motivo</label></td>
									<td>
										<asp:TextBox id="txtMotivo" runat="server" style="width:325px; height:40px;" SkinID="Multi" TextMode="MultiLine" />
								    </td>
								</tr>
							</table>
						</fieldset>
					</td>
					<td style="width:5px;"></td>
					<td style="vertical-align:top; width:300px;">
						<fieldset id="fstTemporal" style="padding-right: 10px; padding-left:10px; width:330px; height:150px">
						<legend>&nbsp;Rango temporal&nbsp;</legend>
                            <fieldset style="padding: 2px; height:35px; width:250px; margin-top:2px; padding-left:10px;">
                            <legend>&nbsp;Inicio&nbsp;</legend>
                                <div style="margin-top:1px;">
                                <asp:Label id="Label1" runat="server" style="width:40px;">&nbsp;Fecha</asp:Label>
                                <asp:textbox id="txtFechaIni" runat="server" style="width:60px;cursor:pointer; margin-left:19px;" Calendar="oCal" goma="0"
							        onchange="validarFecha(this);" ontextchanged="txtFechaIni_TextChanged"></asp:textbox>
                                <asp:Label id="Label3" runat="server" style="width:25px; margin-left:20px;">Hora</asp:Label>
                                <asp:dropdownlist id="cboHoraIni" runat="server" style="width:60px;">
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
								                                    </asp:dropdownlist>
					            </div>
					        </fieldset>
					        <br />
                            <fieldset style="padding: 2px; height:35px; width:250px; margin-top:2px; padding-left:10px; ">
                            <legend>&nbsp;Fin&nbsp;</legend>
                                <div style="margin-top:1px;">
                                <asp:Label id="Label2" runat="server" style="width:40px;">&nbsp;Fecha</asp:Label>
                                <asp:textbox id="txtFechaFin" runat="server" style="width:60px;cursor:pointer; margin-left:19px;" Calendar="oCal" goma="0"
                                    onchange="validarFecha(this);" ontextchanged="txtFechaFin_TextChanged">
                                </asp:textbox>
                                <asp:Label id="Label4" runat="server" style="width:25px; margin-left:20px;">Hora</asp:Label>
                                <asp:dropdownlist id="cboHoraFin" runat="server" style="vertical-align:middle; width:60px;" CssClass="combo">
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
						        </div>
							</fieldset> 
                            <br />
                            <fieldset id="lgdDias" style="margin-top:4px; padding-right:2px; padding-left:10px; padding-bottom:2px; padding-top:2px; width:315px; height:40px;" runat="server">
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
	<tr>
		<td class="texto" style="padding-top:0px;">
			<fieldset style="height: 75px; width:944px; margin-left:17px;" runat="server">
			    <legend>&nbsp;Procedimiento&nbsp;</legend>
			    <div style="height:50px;">
                    <div style="margin-top:5px;">- Webex cuenta con audio integrado por VoIP.</div>
                    <div style="margin-top:5px;">- Necesitarás auriculares para asistir a una conferencia Webex; auriculares con micrófono si además vas a participar en la conversación. Si necesitas alguno de estos elementos y no lo tienes puedes solicitarlos a través del Gestor de Peticiones de Infraestructura de la Intranet (Servicios/CAU)</div>
                    <div style="margin-top:5px;">- Si quieres información más detallada puedes consultar este manual de <span class="enlace" onclick="mostrarGuia()">webex</span></div>
                </div>
		    </fieldset> 
		</td>
	</tr>
	<tr>
		<td>
			<table class="texto" cellspacing="1px" cellpadding="1px" style="text-align:left; margin-left:17px; width:100%;" border="0">
				<tr>
					<td style="vertical-align:top;" width="75%">
						<fieldset id="fldSeleccion" style=" padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 0px; height: 205px; width:740px;" runat="server">
					    <legend>&nbsp;Selección de convocados&nbsp;</legend>
                            <table class="texto" id="Table1" cellspacing="1px" cellpadding="1px" style="text-align:left; width:100%;" border="0">
								<tr>
									<td width="48%">
                                        <table border="0" class="texto" style=" width: 320px;" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                            <td>&nbsp;&nbsp;Apellido1</td>
                                            <td>&nbsp;&nbsp;Apellido2</td>
                                            <td>&nbsp;&nbsp;Nombre</td>
                                            </tr>
                                            <tr>
                                            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionalFICEPI();event.keyCode=0;}" MaxLength="50" CssClass="textareaTexto" /></td>
                                            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionalFICEPI();event.keyCode=0;}" MaxLength="50" CssClass="textareaTexto" /></td>
                                            <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionalFICEPI();event.keyCode=0;}" MaxLength="50" CssClass="textareaTexto" /></td>
                                            </tr>
                                        </table>
									</td>
									<td width="5%"></td>
									<td width="47%">&nbsp;</td>
								</tr>
								<tr>
									<td width="48%">
										<table id="tblTitulo" cellspacing="0px" cellpadding="0px" style="width:320px;" border="0" name="tblTitulo">
											<tr class="TBLINI">
												<td>
												    <img style="CURSOR: hand" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
														height="11" src="../../../Images/imgLupa.gif" width="20" /> 
													<img id="imgLupa1" style=" display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
														height="11" src="../../../Images/imgLupaMas.gif" width="20" />
													&nbsp;&nbsp;Personas
												</td>
											</tr>
										</table>
										<div id="divCatalogo" style=" overflow: hidden; overflow: auto; width: 336px; height: 90px; text-align:left;" name="divCatalogo">
											<table class="texto" id="tblOpciones" style=" width: 320px; text-align:left;" cellspacing="0px" border="0">
											</table>
										</div>
										<table id="tblResultado" cellspacing="0px" cellpadding="0px" style="text-align:left; width:320px;" border="0" >
											<tr class="TBLINI">
												<td>&nbsp;</td>
											</tr>
										</table>
									</td>
									<td width="5%">
										<asp:Image id="Image2" style=" cursor: pointer" onclick="anadirConvocados()" runat="server" ImageUrl="../../../Images/imgNextpg.gif"></asp:Image><br />
										<br />
										<asp:Image id="Image3" style=" cursor: pointer" onclick="quitarConvocados()" runat="server" ImageUrl="../../../Images/imgPrevpg.gif"></asp:Image>
								    </td>
									<td width="47%">
										<table id="tblTitulo2" cellspacing="0px" cellpadding="0px" style="width:320px;" border="0" name="tblTitulo">
											<tr class="TBLINI">
												<td>
												    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',0,'divCatalogo2','imgLupa2',event)"
														height="11" src="../../../Images/imgLupa.gif" width="20"> 
													<img id="imgLupa2" style=" display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones2',0,'divCatalogo2','imgLupa2')"
														height="11" src="../../../Images/imgLupaMas.gif" width="20">
													&nbsp;&nbsp;Convocados
											    </td>
											</tr>
										</table>
										<div id="divCatalogo2" style=" overflow: hidden; overflow: auto; width: 336px; height: 90px; text-align:left;" name="divCatalogo2">
											<asp:repeater id="tblCatalogo" runat="server">
												<ItemTemplate>
													<tr class="FA" style="cursor: pointer" id='<%# Eval("CODIGO") %>' onClick="marcarEstaFila(this,false)" onDblClick="desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');">
														<td width="320px" style="padding-left:5px"><%# Eval("DESCRIPCION") %></td>
													</tr>
												</ItemTemplate>
												<AlternatingItemTemplate>
													<tr class="FB" style="cursor: pointer" id='<%# Eval("CODIGO") %>' onClick="marcarEstaFila(this,false)" onDblClick="desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');">
														<td width="320px" style="padding-left:5px"><%# Eval("DESCRIPCION") %></td>
													</tr>
												</AlternatingItemTemplate>
												<HeaderTemplate>
													<table id="tblOpciones2" class="texto" style=" width: 320px; border-collapse: collapse; " cellspacing="0" border="0">
												</HeaderTemplate>
												<FooterTemplate>
						                            </table>
						
                                                </FooterTemplate>
                                            </asp:repeater>
                                         </div>
                                        <table id="tblResultado2"style='text-align:left; width:320px; height:17px;'>
							                <tr class="TBLFIN">
							                    <td>&nbsp;</td>
							                </tr>
						                </table>
						            </td>
			                    </tr>
				                <tr>
					                <td colspan="3">&nbsp;
						                <asp:Label id="lblConvocados" runat="server" Cssclass="tooltip" ToolTip="Indica las direcciones de correo de los convocados externos, separados por punto y coma">&nbsp;E-mails externos (separados por ;)</asp:Label><br />
						                <asp:TextBox id="txtCorreoExt" runat="server" SkinID="Multi" TextMode="MultiLine" style="width:728px; height:25px;"></asp:TextBox>
							        </td>
				                </tr>
			                </table>
			            </fieldset>
			            <br />
			            <table style="width:750px; margin-top:5px;" border="0">
			                <tr>
			                    <td style="width:375px;">
			                        <fieldset id="fldObservaciones" style="width: 355px; height: 100px" runat="server">
				                        <legend>&nbsp;Observaciones</legend>
				                        <asp:TextBox id="txtObservaciones" runat="server" style="width:340px; margin-top:10px; margin-left:4px; height:65px;" SkinID="Multi" TextMode="MultiLine"></asp:TextBox>
			                        </fieldset>
			                    </td>
			                    <td style="width:375px;">
			                        <fieldset id="fldPrivado" style="width: 365px; height: 100px" runat="server">
				                        <legend class="tooltip" title="Información privada del interesado">&nbsp;Confidencial&nbsp;</legend>
				                        <asp:TextBox id="txtPrivado" runat="server" style="width:350px; margin-top:10px; margin-left:4px; height:65px;" SkinID="Multi" TextMode="MultiLine"></asp:TextBox>
			                        </fieldset>
			                    </td>
			                </tr>
			            </table>
		            </td>
		            <td width="2%"></td>
		            <td width="23%">
			            <fieldset id="fldDisponibilidad" style=" padding-right: 5px; padding-left: 5px; padding-bottom: 5px; width: 174px; padding-top: 0px; height: 315px" runat="server">
				            <legend>&nbsp;Disponibilidad de jornada&nbsp;</legend>
				            <div id="divContenido" style=" overflow: auto; width: 155px; height: 292px; margin-top:5px; margin-left:10px;" runat="server">
					            <asp:Table id="tblCal" style=" left: 0px; clip: rect(0px 154px 509px 0px); position: relative; top: 0px" runat="server" EnableViewState="False"></asp:Table>
					        </div>
			            </fieldset>
		            </td>
	            </tr>
            </table>
        </td>
    </tr>
</table>
</div>
<input id="hdnErrores" type="hidden" value="<%=sErrores%>" /> 
<asp:TextBox id="txtCIP" style="visibility: hidden; width:1px;" runat="server"></asp:TextBox>
<asp:TextBox id="txtIDFICEPI" style="visibility: hidden; width:1px;" runat="server"></asp:TextBox>
<asp:TextBox id="hdnIDReserva" style="visibility: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnAsistentes" style="visibility: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnOrigen" style="visibility: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnAnulacion" style="visibility: hidden" runat="server" value=""></asp:TextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
		<!--
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	            //alert("strBoton: "+ strBoton);
	            switch (strBoton) {
						case "tramitar": 
						{
							bEnviar = comprobarDatos();
							//alert(bEnviar);
							break;
						}
						case "anular": 
						{
						    bEnviar = false;
						    /*
						    var ret = window.showModalDialog("../../Anulacion.aspx", "", "dialogwidth:350px; dialogheight:150px; dialogtop:" + eval(screen.height / 2 - 75) + "px; dialogleft:" + eval(screen.width / 2 - 180) + "px; status:NO; help:NO;");
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
                            .then(function(ret) {
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
					}
				}
				var theform = document.forms[0];
				theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
				theform.__EVENTARGUMENT.value = eventArgument;
				if (bEnviar) theform.submit();
            }
		-->
	</script>
</asp:Content>
