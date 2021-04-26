<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var bLectura = <%=sLectura%>;
    var bSalir = false;    
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
</script>
<table style="width:880px; margin-left:50px;">
    <tr>
        <td>
            <center>
            <table border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="6" height="6" background="../../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../../Images/Tabla/5.gif" style="padding:5px;">
	                    <table id="tblDatos2" cellspacing="7" style="width:480px;text-align:left;">
	                        <colgroup>
	                            <col style="width:70px" />
	                            <col style="width:410px"/>
	                        </colgroup>
	                        <tr>
	                            <td>Denominación</td>
	                            <td align="center"><asp:TextBox id="txtDesGF" runat="server" MaxLength="50" Width="350px" onKeyUp="activarGrabar();"></asp:TextBox></td>
	                        </tr>
                            <tr>
                                <td>
                                    <label id="lblNodo" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
                                </td>
                                <td align="center">                                     
                                    <asp:TextBox ID="txtDesNodo" style="width:350px;" Text="" readonly="true" runat="server"/>       
                                    
                                </td>    
                            </tr>                            
	                    </table>
                </td>
                <td width="6" background="../../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            </center>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
	<tr>
		<td style="vertical-align:top; width:75%;">
			<fieldset id="fldSeleccion" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 0px; height: 470px; width:890px;"
				runat="server"><legend>Selección de integrantes</legend>
                <table cellspacing="1" cellpadding="1" style="width:890px; text-align:left"> 
            	    <colgroup>
                        <col style="width:366px" />
                        <col style="width:118px"/>
                        <col style="width:406px"/>
                    </colgroup>       
			        <tbody>
                    <tr style="height:25px;">
                        <td>
                            <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinId="rbl" onclick="seleccionAmbito(this.id)">
                            <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_0').click();" Selected="True" Value="A" Text="Apellido&nbsp;&nbsp;" />
                            <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_1').click();" Value="C" Text="C.R.&nbsp;&nbsp;" />
                            <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_2').click();" Value="P" Text="Proy. económico" />
                            </asp:RadioButtonList>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                        </td>
                    </tr>
    				
	                <tr style="height:35px;">
                        <td>										
                            <span id="ambAp" style="display:block" class="texto">
                                <table class="texto"style="WIDTH: 360px;">
                                    <colgroup><col style="width:120px;" /><col style="width:120px;" /><col style="width:120px;" /></colgroup>
                                    <tr>
                                        <td>&nbsp;Apellido1</td>
                                        <td>&nbsp;Apellido2</td>
                                        <td>&nbsp;Nombre</td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtApe1" runat="server"  style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                        <td><asp:TextBox ID="txtApe2" runat="server"  style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                        <td><asp:TextBox ID="txtNom"  runat="server"  style="width:105px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                    </tr>
                                </table>
                            </span>
                            <span id="ambCR" style="display:none" class="texto">
                                <label id="lblCR" class="enlace" style="width:33px" onclick="obtenerCR()">C.R.</label> 
                                <asp:TextBox ID="txtCR" runat="server" width="310px" />
                            </span>
                            <span id="ambPE" style="display:none" class="texto">
                                <label id="lblPE" class="enlace" style="width:80px" onclick="obtenerPE()">P. Económico</label>
                                <asp:TextBox ID="txtCodPE" runat="server" Text="" Width="60px" style="text-align:right" readonly="true" />
                                <asp:TextBox ID="txtPE" runat="server" Width="195px" readonly="true" />                            
                            </span>				
                        </td>
                        <td>&nbsp;</td>
                        <td>
                        </td>
                    </tr>	
				    <tr>
					    <td>
						    <table id="tblTitulo" style="height:17px;width:350px">
							    <tr class="TBLINI">
								    <td>&nbsp;Profesionales&nbsp;
									    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
										    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								        <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
										    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								    </td>
							    </tr>
						    </table>
						    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 366px; height: 360px;" onscroll="scrollTablaProf()">
							     <div style='background-image:url(../../../../../Images/imgFT20.gif); width:350px;'>
							        <table style="width: 350px"></table>
							     </div>
						    </div>
						    <table id="tblResultado" style="height:17px;width:350px">
							    <tr class="TBLFIN"><td></td></tr>
						    </table>
					    </td>
					    <td align="center">
						    <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
						</td>
					    <td>
						    <table id="tblTitulo2" style="height:17px;width:390px">
							    <tr class="TBLINI">
								    <td width="75%">&nbsp;Integrantes&nbsp;
									    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',2,'divCatalogo2','imgLupa2')"
										    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								        <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',2,'divCatalogo2','imgLupa2',event)"
										    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								    </td>
								    <td width="25%" style="text-align:right;">Responsable&nbsp;</td>
							    </tr>
						    </table>
						    <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 406px; height: 360px;" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
							    <div style='background-image:url(../../../../../Images/imgFT20.gif); width:390px;'>
							        <%=strTablaHTMLIntegrantes%>
							    </div>
                            </div>
                            <table id="tblResultado2" style="height:17px;width:390px">
							    <tr class="TBLFIN"><td></td></tr>
						    </table>
					    </td>
			        </tr>
		        </tbody>
		        </table>
		</fieldset>
	</td>
    </tr>
    <tr>
        <td align="left">
            <img border="0" src="../../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> actual&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" src="../../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <DIV class="clsDragWindow" id="DW" noWrap></DIV>
    <asp:TextBox ID="hdnIdGf" runat="server" style="visibility: hidden"></asp:TextBox>
    <asp:TextBox ID="hdnCR" runat="server" style="visibility: hidden"></asp:TextBox>
    <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" />
    <asp:TextBox ID="hdnMasDeUnGF" runat="server" style="visibility: hidden"></asp:TextBox>
    <input type="hidden" name="hdnEsSoloRGF" id="hdnEsSoloRGF" value="N" runat="server"/>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
			    case "regresar":
			        {
			            if (bCambios) {
			                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
			                    if (answer) {
			                        bEnviar = false;
			                        bSalir = true;
			                        setTimeout("grabar()", 20);
			                    } else
			                    {
			                        bEnviar = true;
			                        bCambios = false;
			                        fSubmit(bEnviar, eventTarget, eventArgument);
			                    }
			                });
			            } else fSubmit(bEnviar, eventTarget, eventArgument);

			            break;

			        }
			}
            if (strBoton != "regresar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
        return;
    }
    function fSubmit(bEnviar, eventTarget, eventArgument) {
        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
        return;
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

