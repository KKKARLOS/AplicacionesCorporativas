<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Video.Reserva.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

//	var objPanel = $I("AreaTrabajo");
//	objPanel.style.backgroundImage = "url(../../../Images/imgVideoconfAlpha.gif)";
//	objPanel.style.backgroundRepeat= "no-repeat";
//	objPanel.style.backgroundPosition= "top left";
//	objPanel = null;

	//var bLectura	= <%//=sLectura%>;
	var bLectura	= <%=sLectura%>;
	var bNuevo		= "<%=bNuevo%>";
	var strMsg		= "<%=strMsg%>";
	var strLocation = "<%=strLocation%>";
	var strServer	= "<%=Session["strServer"]%>";
	var strSalas	= "<%=strSalas%>";
	var aSalas		= new Array(<%=aCodSalas%>);
	var aSalasSelec	= new Array(<%=aCodSalasSelec%>);
	var strFechaIniInicio = "<%=this.txtFechaIni.Text%>";
	var strFechaHoy = "<%=System.DateTime.Today.ToShortDateString()%>";
	var strHoraIni	= "";
	var strHoraFin	= "";
	var strPostBack	= <%=strPostBack%>;

</script>
<div id="AreaTrabajo" style="height:100%;width:100%; padding: 0px;">
    <table class="texto" id="tblFiltros" style="width:990px;" border="0" name="tblFiltros" cellspacing="5px" cellpadding="5px">
		<tr>
			<td style="width:265px;">
				<fieldset style="height: 150px; width:95%;">
					<legend>&nbsp;Salas de videoconferencia&nbsp;</legend>
					<div id="divSalas" style="OVERFLOW-X: hidden; overflow: auto; width: 95%; height: 125px" name="divSalas">
						<asp:CheckBoxList id="chkLstSalas" runat="server" CssClass="texto"></asp:CheckBoxList>
					</div>
				</fieldset>
			</td>
			<td style="width:410px;">
				<fieldset style=" height: 150px; width:97%;">
					<legend>&nbsp;Datos generales&nbsp;</legend>
					<table class="texto" id="tblDatos" style="width:100%; margin-top:10px;" cellspacing="5px" cellpadding="5px" border="0">
						<tr>
							<td width="18%"><% if ((Session["CR2I_PERFIL"].ToString() == "A") || (Session["CR2I_RESSALA"].ToString() == "E")){ %>
							    <label class="enlace" id="lblInter" onmouseover="mostrarCursor(this)" onclick="obtenerInteresado()" name="lblInter">Interesado</label>
								<% }else{ %>
								Interesado
								<% } %>
							</td>
							<td width="82%">
								<asp:TextBox id="txtInteresado" runat="server" class="textareaTexto" style="width:270px;"></asp:TextBox>
								<label id="lblQEQ" onclick="mostrarQEQ()" runat="server" class="enlace">QEQ</label>
						    </td>
						</tr>
						<tr>
							<td style="vertical-align:top;" width="20%">
								<asp:Label id="lblAsunto" runat="server" class="tooltip" ToolTip="Información de visualización pública">Asunto</asp:Label>
							</td>
							<td width="80%">
								<asp:TextBox id="txtAsunto" runat="server" SkinID="Multi" style="width:300px;" maxlength="50"></asp:TextBox>
							</td>
						</tr>
						<tr id="filaMotivo" runat="server">
							<td style="vertical-align:top;" width="20%">
								<asp:Label id="lblMotivo" runat="server" Cssclass="tooltip" ToolTip="Información restringida al interesado  y convocados">Motivo</asp:Label></td>
							<td width="80%">
								<asp:TextBox id="txtMotivo" runat="server" SkinID="Multi" style="width:300px; height:60px;" TextMode="MultiLine"></asp:TextBox>
						    </td>
						</tr>
					</table>
				</fieldset>
			</td>
			<td style="width:315px;">
				<fieldset style="padding-left: 10px; width:87%; height: 150px">
				<legend>&nbsp;Rango temporal&nbsp;</legend>
                    <div style="width:100%; margin-left:180px;">
						<asp:CheckBox id="chkTodoDia" onclick="mostrarTodoElDia()" runat="server" CssClass="texto" Text="Todo el día"></asp:CheckBox>
					</div>
					<br />
                    <fieldset style="width:90%; padding-right: 2px; padding-left: 2px; padding-bottom: 2px; padding-top: 2px">
                    <legend>&nbsp;Inicio&nbsp;</legend>
                    <asp:Label id="Label1" runat="server" Width="35px">&nbsp;Fecha</asp:Label>
                    <asp:textbox id="txtFechaIni"  runat="server" style="width:60px;cursor:pointer; margin-left:19px;" Calendar="oCal" goma="0"
                        onchange="validarFecha(this);" ontextchanged="txtFechaIni_TextChanged">
                    </asp:textbox>
                    <asp:Label id="Label3" runat="server" Width="25px">Hora</asp:Label>
                    <asp:dropdownlist id="cboHoraIni" runat="server" CssClass="combo" Width="60px" onChange="setChkTodoDia();">
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
				  </fieldset> 
				    <br /><br />
                    <fieldset style="width:90%; padding-right: 2px; padding-left: 2px; padding-bottom: 2px; padding-top: 2px">
                    <legend>&nbsp;Fin&nbsp;</legend>
                        <asp:Label id="Label2" runat="server" Width="35px">&nbsp;Fecha</asp:Label>
                        <asp:textbox id="txtFechaFin"  runat="server" style="width:60px;cursor:pointer; margin-left:19px;" Calendar="oCal" goma="0"
                            onclick="validarFecha(this);" ontextchanged="txtFechaFin_TextChanged">
                        </asp:textbox>
                        <asp:Label id="Label4" runat="server" Width="25px">Hora</asp:Label>
                        <asp:dropdownlist id="cboHoraFin" runat="server" CssClass="combo" Width="60px" onChange="setChkTodoDia();">
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
				</fieldset>
			</td>
		</tr>
		<tr>
			<td colspan="3">
				<fieldset id="fldDisponibilidad" style=" padding-right: 5px; padding-left: 5px; padding-bottom: 5px; width: 973px; padding-top: 0px; height: 180px" runat="server">
					<legend>&nbsp;Disponibilidad de jornada</legend>
					<div id="divContenido" style="overflow: auto; width:970px; height:160px; margin-top:10px;" runat="server">
						<asp:Table id="tblCal" runat="server" EnableViewState="False"></asp:Table>
					</div>
				</fieldset>
			</td>
		</tr>
	</table>
	<table style="width:990px;">
		<tr>
			<td style="width:75%;">
				<fieldset id="fldSeleccion" style="width:100%; height:220px" runat="server">
					<legend>&nbsp;Selección de convocados&nbsp;</legend>
                    <table class="texto" id="Table1" cellspacing="3px" cellpadding="3px" width="100%" border="0">
						<tr>
							<td width="48%">
                                <table border="0" class="texto" style="width:320px;" cellpadding="0" cellspacing="0">
                                    <tr>
                                    <td>&nbsp;&nbsp;Apellido1</td>
                                    <td>&nbsp;&nbsp;Apellido2</td>
                                    <td>&nbsp;&nbsp;Nombre</td>
                                    </tr>
                                    <tr>
                                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" class="textareaTexto" /></td>
                                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" class="textareaTexto" /></td>
                                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" class="textareaTexto" /></td>
                                    </tr>
                                </table>
							</td>
							<td width="5%"></td>
							<td style="vertical-align:top; text-align:right;" width="47%">
								<asp:CheckBox id="chkCorreo" runat="server" style="height:6px; width:100%; margin-right:20px;" Text="Suscribir notificaciones por correo" Checked="True"></asp:CheckBox>
							</td>
						</tr>
						<tr>
							<td width="48%">
								<table id="tblTitulo" style="width:320px;" cellspacing="0" cellpadding="0" border="0" name="tblTitulo">
									<tr class="TBLINI">
										<td width="80%"><IMG style="cursor:pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
												height="11" src="../../../Images/imgLupa.gif" width="20"> <IMG id="imgLupa1" style=" display: none; cursor:pointer;" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
												height="11" src="../../../Images/imgLupaMas.gif" width="20">&nbsp;&nbsp;Personas</td>
										<td width="20%"><IMG onmouseover="javascript:bMover=true;scrollUp('divCatalogo',16);" style=" cursor:pointer;" onmouseout="javascript:bMover=false;"
												height="8" src="../../../Images/imgFleUp.gif" width="11"></td>
									</tr>
								</table>
								<div id="divCatalogo" style="OVERFLOW-X: hidden; overflow: auto; width: 336px; height: 80px; text-align:left;" name="divCatalogo">
									<table class="texto" id="tblOpciones" style="width: 320px; text-align:left;" cellspacing="0" border="0">
									</table>
								</div>
								<table id="tblResultado" cellspacing="0" cellpadding="0" style="width:320px; text-align:left;" border="0" name="tblResultado">
									<tr class="TBLFIN">
										<td colspan="2"><IMG height="1" src="../../../Images/imgSeparador.gif" width="80%" border="0">
											<img onmouseover="javascript:bMover=true;scrollDown('divCatalogo',16);" style=" cursor:pointer" onmouseout="javascript:bMover=false;" height="8" src="../../../Images/imgFleDown.gif" width="11" />
										</td>
									</tr>
								</table>
							</td>
							<td width="5%">
								<asp:Image id="Image2" style=" cursor: pointer;" onclick="anadirConvocados()" runat="server" ImageUrl="../../../Images/imgNextpg.gif"></asp:Image>
								<br /><br />
								<asp:Image id="Image3" style=" cursor: pointer;" onclick="quitarConvocados()" runat="server" ImageUrl="../../../Images/imgPrevpg.gif"></asp:Image>
						    </td>
							<td width="47%">
								<table id="tblTitulo2" cellspacing="0" cellpadding="0" style="width:320px" border="0" name="tblTitulo">
									<tr class="TBLINI">
										<td width="80%"><IMG style=" cursor:pointer;" onclick="buscarDescripcion('tblOpciones2',0,'divCatalogo2','imgLupa2',event)"
												height="11" src="../../../Images/imgLupa.gif" width="20"> <IMG id="imgLupa2" style=" display: none; cursor:pointer;" onclick="buscarSiguiente('tblOpciones2',0,'divCatalogo2','imgLupa2')"
												height="11" src="../../../Images/imgLupaMas.gif" width="20">&nbsp;&nbsp;Convocados</td>
										<td width="20%"><IMG onmouseover="javascript:bMover=true;scrollUp('divCatalogo2',16);" style=" cursor:pointer;" onmouseout="javascript:bMover=false;"
												height="8" src="../../../Images/imgFleUp.gif" width="11"></td>
									</tr>
								</table>
								<div id="divCatalogo2" style="OVERFLOW-X: hidden; overflow: auto; width: 336px; height: 80px; text-align:left;" name="divCatalogo2">
									<asp:repeater id="tblCatalogo" runat="server">
										<ItemTemplate>
											<tr class="FA" style=" cursor:pointer;" id='<%# Eval("CODIGO") %>' onClick="marcarEstaFila(this,false)" onDblClick="desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');">
												<td width="320px" style="padding-left:5px"><%# Eval("DESCRIPCION") %></td>
											</tr>
										</ItemTemplate>
										<AlternatingItemTemplate>
											<tr class="FB" style=" cursor:pointer;" id='<%# Eval("CODIGO") %>' onClick="marcarEstaFila(this,false)" onDblClick="desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');">
												<td width="320px" style="padding-left:5px"><%# Eval("DESCRIPCION") %></td>
											</tr>
										</AlternatingItemTemplate>
										<HeaderTemplate>
											<table id="tblOpciones2" class="texto" style="width: 320px; border-collapse: collapse; " cellspacing="0" border="0">
										</HeaderTemplate>
										<FooterTemplate>
				                            </table>
						                </FooterTemplate>
			                        </asp:repeater>
			                    </div>
                                <table id="tblResultado2"cellspacing="0" cellpadding="0" style="width:320px;" border="0" name="tblResultado">
					                <tr class="TBLFIN">
						                <td colspan="2"><IMG height="1" src="../../../Images/imgSeparador.gif" width="80%" border="0">
							                <IMG onmouseover="javascript:bMover=true;scrollDown('divCatalogo2',16);" style=" cursor:pointer;" onmouseout="javascript:bMover=false;" height="8" src="../../../Images/imgFleDown.gif" width="11">
						                </td>
					                </tr>
				                </table>
				            </td>
	                    </tr>
	                    <tr>
		                    <td colspan="3">&nbsp;
			                    <label id="lblConvocados" runat="server" class="tooltip" title="Indica las direcciones de correo de los convocados externos, separados por punto y coma">&nbsp;E-mails externos (separados por ;)</label>
			                    <br />
			                    <asp:TextBox id="txtCorreoExt" runat="server" SkinID="Multi" style="width:710px; height:25px;" TextMode="MultiLine"></asp:TextBox>
				            </td>
	                    </tr>
                    </table>
                </fieldset>
            </td>
            <td style="width:2%;"></td>
            <td style="width:23%;">
                <fieldset id="fldCentralita" style="width:98%; height:100px;" runat="server">
	                <legend class="tooltip" title="Información enviada por correo a las centralitas de las oficinas a las que pertenecen las salas de video reservadas">&nbsp;Mensaje a centralitas&nbsp;</legend>
	                <asp:TextBox id="txtCentralita" runat="server" SkinID="Multi" style="width:205px; margin-left:5px; margin-top:10px; height:65px;" TextMode="MultiLine"></asp:TextBox>
                </fieldset>
                <br /><br />	
                <fieldset id="fldPrivado" style="width:98%; height:100px;" runat="server">
	                <legend class="tooltip" title="Información privada del interesado">&nbsp;Confidencial&nbsp;</legend>
	                <asp:TextBox id="txtPrivado" runat="server" SkinID="Multi" style="width:205px; margin-left:5px; margin-top:10px; height:65px;" TextMode="MultiLine"></asp:TextBox>
                </fieldset>
            </td>
        </tr>
	</table>
</div>
<input id="hdnErrores" type="hidden" value="<%=sErrores%>" /> 
<asp:TextBox id="txtCIP" style=" visibility: hidden; width:1px;" runat="server"></asp:TextBox>
<asp:TextBox id="hdnIDReserva" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnAsistentes" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnOrigen" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
<asp:TextBox id="hdnAnulacion" style="VISIBILITY: hidden" runat="server" value=""></asp:TextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
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
	</script>
</asp:Content>
