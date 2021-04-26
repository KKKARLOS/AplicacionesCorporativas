<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Wifi.Reserva.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
//	var objPanel = $I("AreaTrabajo");
//	objPanel.style.backgroundImage = "url(../../../Images/imgWifiAlpha.gif)";
//	objPanel.style.backgroundRepeat= "no-repeat";
//	objPanel.style.backgroundPosition= "top left";
//	objPanel = null;

	var bNuevo = "<%=bNuevo%>";
	var strMsg = "<%=strMsg%>";
	var strLocation = "<%=strLocation%>";
	var strServer = "<%=Session["strServer"]%>";
	var strFechaHoy = "<%=System.DateTime.Today.ToShortDateString()%>";
	var strHoraIni = "";
	var strHoraFin = "";
	var sResultadoGrabacion = "<%=sResultadoGrabacion%>";
	var bLectura = <%=sLectura%>;
	var bEsInsert = <%=sEsInsert%>;
	var bEsAnular = <%=bEsAnular%>;
-->
	</script>
<div id="AreaTrabajo" style="height:100%;width:100%; padding: 0px;" runat="server">
<br /><br /><br /><br />
<table border="0" class="texto" style="width:770px;" cellpadding="0px" cellspacing="0px" align="center">
    <tr>
        <td background="../../../Images/Tabla/7.gif" style="height:6px; width:6px;">
        </td>
        <td background="../../../Images/Tabla/8.gif" style="height:6px;">
        </td>
        <td background="../../../Images/Tabla/9.gif" style="height:6px; width:6px;">
        </td>
    </tr>
    <tr>
        <td background="../../../Images/Tabla/4.gif" style="width:6px;">
            &nbsp;</td>
        <td background="../../../Images/Tabla/5.gif" style="padding:5px">
            <!-- Inicio del contenido propio de la página -->
            <table class="texto" id="tblFiltros" cellspacing="2" cellpadding="3"  border="0" runat="server">
	            <tr>
		            <td style="width:54%;">
			            <fieldset style="height: 228px; width:100%;">
				            <legend>&nbsp;Datos generales&nbsp;</legend>
				            <table class="texto" id="tblDatos" cellspacing="2px" cellpadding="5px" style="width:100%; margin-top:10px;" border="0">
					            <tr>
						            <td style="width:20%;">Solicitante</td>
						            <td style="width:80%;">
							            <asp:TextBox id="txtSolicitante" runat="server" style="width:315px;" CssClass="textareaTexto"></asp:TextBox>
							        </td>
					            </tr>
					            <tr>
						            <td style="width:20%;">Interesado<label style="color:Red; margin-left:5px;">*</label></td>
						            <td style="width:80%;">
							            <asp:TextBox id="txtInteresado" runat="server" style="width:315px;" CssClass="textareaTexto" onkeyup="dm()"></asp:TextBox>
					                </td>
					            </tr>
					            <tr>
						            <td style="width:20%;">Empresa<label style="color:Red; margin-left:5px;">*</label></td>
						            <td style="width:80%;">
							            <asp:TextBox id="txtEmpresa" runat="server" style="width:315px;" CssClass="textareaTexto" onkeyup="dm()"></asp:TextBox>
					                </td>
					            </tr>
					            <tr>
						            <td style="width:20%;" style="vertical-align:top;">Observaciones</td>
						            <td style="width:80%;">
							            <asp:TextBox id="txtObservaciones" runat="server" style="width:315px; height:110px;" SkinID="Multi" TextMode="MultiLine" onkeyup="dm()"></asp:TextBox>
						            </td>
					            </tr>
				            </table>
			            </fieldset>
		            </td>
		            <td style="width:2%;"></td>
		            <td style="vertical-align:top; width:44%;">
			            <fieldset id="fstTemporal" style=" padding-right: 10px; padding-left: 10px; height: 125px; width:270px;">
			            <legend>&nbsp;Vigencia de la conexión&nbsp;</legend>
                            <fieldset style="width:255px; height:33px; padding-right: 2px; padding-left: 2px; padding-bottom: 2px; padding-top: 2px; margin-top:10px; margin-left:5px;">
                            <legend>&nbsp;Inicio&nbsp;</legend>
                                <asp:Label id="Label1" runat="server" style="width:40px">&nbsp;Fecha</asp:Label>
                                <asp:textbox id="txtFechaIni" runat="server" style="width:60px;cursor:pointer; margin-left:19px;" 
                                        Calendar="oCal" goma="0" onchange="validarFecha(this);dm();">
                                </asp:textbox>
                                <asp:Label id="Label3" runat="server" style="width:30px; margin-left:20px;">Hora</asp:Label>
                                <asp:dropdownlist id="cboHoraIni" runat="server" Width="60px" CssClass="combo" onchange="validarFecha($I('txtFechaIni').value);dm();">
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
                                </asp:dropdownlist>
				            </fieldset>
                            <fieldset style="width:255px; height:33px; padding-right: 2px; padding-left: 2px; padding-bottom: 2px; padding-top: 5px; margin-top:8px; margin-left:5px;">
                            <legend>&nbsp;Fin&nbsp;</legend>
                                <asp:Label id="Label2" runat="server" style="width:40px">&nbsp;Fecha</asp:Label>
                                <asp:textbox id="txtFechaFin" runat="server" style="width:60px;cursor:pointer; margin-left:19px;" 
                                    Calendar="oCal" goma="0" onchange="validarFecha(this);dm();">
                                </asp:textbox>
                                <asp:Label id="Label4" runat="server" style="width:30px; margin-left:20px;">Hora</asp:Label>
                                <asp:dropdownlist id="cboHoraFin" runat="server" Width="60px" CssClass="combo" onchange="validarFecha($I('txtFechaFin').value);dm();">
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
                                </asp:dropdownlist>
                            </fieldset>
			            </fieldset>
			            <fieldset style="height:79px; margin-top:17px; width:285px;" id="fstConexion" runat="server">
				            <legend>&nbsp;Conexión WIFI&nbsp;&nbsp;(a rellenar por el sistema)</legend>
				            <div id="divPestRetr" style=" position:absolute; top:308px; left:585px; height:62px; width:291px; clip:rect(0px 270px 62px auto);">
				            <img src="../../../Images/imgFondoPixelado.gif" />
				            </div>
				            <table class="texto" id="TABLE1" style="margin-left:50px; margin-top:10px;" cellspacing="2px" cellpadding="3px" border="0" >
					            <tr>
						            <td style="width:20%;">Usuario</td>
						            <td style="width:80%"><asp:TextBox id="txtUsuario" runat="server" style="font-size:12px;text-align:center; width:100px;"></asp:TextBox></td>
					            </tr>
					            <tr>
						            <td style="width:20%">Contraseña</td>
						            <td style="width:80%"><asp:TextBox id="txtPwd" runat="server" style="font-size:12px;text-align:center;width:100px;"></asp:TextBox></td>
					            </tr>
				            </table>
			            </fieldset>
		            </td>
	            </tr>
            </table>
            <br />
            <label style="color:Red; margin-left:5px;">* Datos obligatorios</label>
            <!-- Fin del contenido propio de la página -->
        </td>
        <td background="../../../Images/Tabla/6.gif" style="width:6px;">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td background="../../../Images/Tabla/1.gif" style="width:6px; height:6px;">
        </td>
        <td background="../../../Images/Tabla/2.gif" style="height:6px;">
        </td>
        <td background="../../../Images/Tabla/3.gif" style="width:6px; height:6px;">
        </td>
    </tr>
</table>
</div>
<input id="hdnErrores" type="hidden" value="<%=sErrores%>" /> 
<asp:TextBox id="txtCIP" style=" visibility: hidden" runat="server" Width="1px"></asp:TextBox>
<asp:TextBox id="hdnIDReserva" style="visibility: hidden;" runat="server" Text=""></asp:TextBox>
<asp:TextBox id="hdnEstado" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
<asp:TextBox id="hdnOrigen" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
<asp:TextBox id="hdnAnulacion" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
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
	                case "procesar":
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
	                        break;
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
	                case "imprimir":
	                    {
	                        bEnviar = false;
	                        Exportar();
	                        //window.showModalDialog("../Reserva/Exportar/default.aspx", "", "dialogwidth:600px; dialogheight:500px; dialogtop:" + eval(screen.height / 2 - 75) + "px; dialogleft:" + eval(screen.width / 2 - 180) + "px; status:NO; help:NO;");
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
