<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_BonoTransporte_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodoCorta = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var nAnoMesActual = <%=DateTime.Now.Year * 100 + DateTime.Now.Month %>;
    var sMsgRecuperada = "<%=sMsgRecuperada %>";
    var bLectura = <%=(bLectura)? "true":"false" %>;
    var sOrigen = "<%=sOrigen %>";
    var sDiaLimiteContAnoAnterior = <%=sDiaLimiteContAnoAnterior %>;
    var sDiaLimiteContMesAnterior = <%=sDiaLimiteContMesAnterior %>;
    var bSeleccionBeneficiario = <%=((bool)Session["GVT_MULTIUSUARIO"] || User.IsInRole("T") || User.IsInRole("S") || User.IsInRole("A"))? "true":"false" %>;
    var bAdministrador = <%=(User.IsInRole("A"))? "true":"false" %>;
    var sNodoUsuario = "<%=sNodoUsuario %>";
</script>
<asp:Image ID="imgManoVisador" ImageUrl="~/Images/imgManoVisador.gif" style="position:absolute; top:323px; left:770px; cursor:pointer; width:50px; height:50px; visibility:hidden;" onclick="getVisador();" runat="server" ToolTip="" />
<table style="width:1000px">
    <tr>
	    <td><br />
            <table style="width:980px;"  cellpadding="0">
                <tr>
                    <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px; padding: 0px"></td>
                    <td style="background-image:url(../../Images/Tabla/8.gif);"></td>
                    <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px; padding: 0px"></td>
                </tr>
                <tr>
                    <td style="background-image:url(../../Images/Tabla/4.gif);">&nbsp;</td>
                    <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                    <div id="divTituloDatosGenerales" style="text-align:center;background-image: url(../../Images/imgFondo200.gif); background-repeat:no-repeat;
                        width: 200px; height: 23px; position: relative; top: -20px; left: 10px; padding-top:5px; text-align:center;
                        font:bold 12px Arial; color:#5894ae;">Datos generales</div>
                        <table style="width:940px; margin-top:-15px;" cellpadding="4">
                            <colgroup>
                                <col style="width:90px;" />
                                <col style="width:330px;" />
                                <col style="width:70px;" />
                                <col style="width:250px;" />
                                <col style="width:200px;" />
                            </colgroup>
                            <tr>
                                <td><label id="lblBeneficiario" class="texto" runat="server">Beneficiario</label></td>
                                <td><asp:TextBox ID="txtInteresado" style="width:300px;" Text="" runat="server" ReadOnly="true" /></td>
                                <td>Referencia</td>
                                <td><asp:TextBox ID="txtReferencia" style="width:60px;" SkinID="numero" Text="" runat="server" ReadOnly="true" /></td>
                                <td rowspan="3">
                                    <asp:Image ID="imgEstado" ImageUrl="~/Images/imgEstado2.gif" style="width:160px; height:80px;" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Mes</td>
                                <td>
                                    <img id="imgAM" src="../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor:pointer; vertical-align:middle;" border="0" runat="server" />
                                    <asp:TextBox ID="txtFecha" style="width:90px; vertical-align:middle; text-align:center;" ReadOnly="true" runat="server" Text=""></asp:TextBox>
                                    <img id="imgSM" src="../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor:pointer; vertical-align:middle;" border="0" runat="server" />
                                </td>
                                <td>Empresa</td>
                                <td><asp:TextBox ID="txtEmpresa" style="width:200px;" Text="" runat="server" ReadOnly="true" />
                                    <asp:DropDownList ID="cboEmpresa" runat="server" Width="200px" onchange="setEmpresa();aG();" AppendDataBoundItems="true"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td><label id="lblBono" class="enlace" onclick="getBono();" runat="server">Bono</label> <span style="color:Red; visibility:visible;">*</span></td>
                                <td><asp:TextBox ID="txtBono" style="width:300px;" Text="" runat="server" ReadOnly="true" /></td>
                                <td>Importe</td>
                                <td><asp:TextBox ID="txtImporte" style="width:60px;" Text="" runat="server" class="txtNumL" SkinID="Numero" onchange="aG();" ReadOnly="true" /> <label id="lblMoneda" runat="server"></label></td>                            
                            </tr>
                            <tr>
                                <td>Proyecto</td>
                                <td><asp:TextBox ID="txtProyecto" style="width:300px; margin-top:3px;" Text="" runat="server" ReadOnly="true" /></td>
                            </tr>
                            <tr style='vertical-align: text-top;'>
                                <td style="vertical-align: top;">Observaciones</td>
                                <td colspan="3"><asp:TextBox TextMode="multiLine" ID="txtObservacionesBono" style="width:600px; height:70px;" Rows="3" Text="" runat="server" onKeyUp="if(!bLectura) aG();" TabIndex="1" />
                                <asp:Image ID="imgMail" ImageUrl="~/Images/imgEmailEdit32.png" style="cursor:pointer; display:none; vertical-align:top;" onclick="mdMail();" runat="server" /></td>
                                <td style="vertical-align: top;"><div id="divAnotaciones" style="cursor:pointer;" onclick="getAnotaciones();" runat="server"><asp:Image ID="imgAnotaciones" ImageUrl="~/Images/imgAnotacionesPer.gif" style="vertical-align:middle; margin-top:3px;" runat="server" /> Anotaciones personales</div></td>
                            </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td style="background-image:url(../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/2.gif); height:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td><br />
            <table style="width:990px">
                <colgroup>
                    <col style="width: 330px;" />
                    <col style="width: 660px;" />
                </colgroup>
                <tr style='vertical-align: text-top;'>
                    <td>
                    <div id="divTituloOtro" style="text-align:center;background-image: url(../../Images/imgFondo200.gif); background-repeat:no-repeat;
                        width: 200px; height: 23px; position: relative; top: 12px; left: 20px; padding-top:5px; text-align:center;
                        font:bold 12px Arial; color:#5894ae;">Otros Bonos de</div>
                    <table style="width:320px;"  cellpadding="0">
                        <tr>
                            <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px; "></td>
                            <td style="background-image:url(../../Images/Tabla/8.gif); "></td>
                            <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px;"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url(../../Images/Tabla/4.gif);">&nbsp;</td>
                            <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                                <!-- Inicio del contenido propio de la página -->
                                <table id="Table1" style="width:280px; height:17px; margin-top:10px;">
                                    <colgroup>					
                                        <col style="width:170px;" />
                                        <col style="width:50px;" />
                                        <col style="width:60px; " />
                                    </colgroup>
                                    <tr class="TBLINI">				    
                                        <td style="padding-left:3px;">Concepto</td>
                                        <td>Moneda</td>
                                        <td style="text-align:right; padding-right:2px;">Importe</td>
                                    </tr>
                                </table>
                                <div id="divCatalogoOtros" style="overflow-x:hidden; overflow:auto; width:296px; height:97px" runat="server">
                                    <div class="pijama20 W280">
                                    </div>
                                </div>
                                <%--<table id="tblPieOtros" style="width:280px;height:17px;">
                                    <tr class="TBLFIN">
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>--%>
                                <!-- Fin del contenido propio de la página -->
                            </td>
                            <td style="background-image:url(../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                            <td style="background-image:url(../../Images/Tabla/2.gif); height:6px"></td>
                            <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
                        </tr>
                    </table>
                    </td>
                    <td rowspan="2">
                        <div style="text-align:center;background-image: url('../../Images/imgFondo100.gif'); background-repeat:no-repeat;
                            width:100px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
                            font:bold 12px Arial;color:#5894ae;">Historial</div>
                        <table style="width:647px;"  cellpadding="0">
                            <tr>
                                <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px;"></td>
                                <td style="background-image:url(../../Images/Tabla/8.gif); "></td>
                                <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px; "></td>
                            </tr>
                            <tr>
                                <td style="background-image:url(../../Images/Tabla/4.gif); ">&nbsp;</td>
                                <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                                    <!-- Inicio del contenido propio de la página -->
                                    <table id="tblTituloHistorial" style="width:610px;height:17px; margin-top:10px;">
                                        <colgroup>					
                                            <col style="width:105px; padding-left:3px;" />
                                            <col style="width:105px;" />
                                            <col style="width:400px;" />
                                        </colgroup>
                                        <tr class="TBLINI">				    
                                            <td>Estado</td>
                                            <td>Fecha</td>
                                            <td>Profesional / Causa</td>
                                        </tr>
                                    </table>
                                    <div id="divCatalogoHistorial" style="overflow-x:hidden; overflow:auto; width:626px; height:197px" runat="server">
                                        <div style="width:610px; height:0%"></div>
                                    </div>
                                   <%-- <table id="tblPieHistorial" style="width:610px;height:17px;">
                                        <tr class="TBLFIN">
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>--%>
                                    <!-- Fin del contenido propio de la página -->
                                </td>
                                <td style="background-image:url(../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                                <td style="background-image:url(../../Images/Tabla/2.gif); height:6px"></td>
                                <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style='vertical-align: text-bottom;'>
                        <div id="divContabilizacion" style="text-align:center;background-image: url('../../Images/imgFondo185.gif'); background-repeat:no-repeat;
                            width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
                            font:bold 12px Arial; visibility:hidden; color:#5894ae;">Datos para contabilización</div>
                        <table id="tblContabilizacion" style="width:320px; visibility:hidden;" runat="server"  cellpadding="0">                        
                            <tr>
                                <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px"></td>
                                <td style="background-image:url(../../Images/Tabla/8.gif); height:6px"></td>
                                <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px"></td>
                            </tr>
                            <tr>
                                <td style="background-image:url(../../Images/Tabla/4.gif); ">&nbsp;</td>
                                <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                                    <!-- Inicio del contenido propio de la página -->
                                    <table id="Table2" style="width: 300px; margin-top:5px; padding:5px;">
                                        <colgroup>
                                            <col style="padding-left:3px;" />
                                        </colgroup>
                                        <tr>
                                            <td style="padding-left:5px;">Fecha <asp:TextBox ID="txtFecContabilizacion" style="width:60px; margin-left:5px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(this);" onchange="aG();" ReadOnly="true" runat="server" goma="0" /></td>
                                        </tr>
			                            <tr>
			                                <td>
			                                    <fieldset id="flsTipoCambio" style="width:292px; padding:5px; visibility:hidden;">
			                                        <legend>Tipo de cambio</legend>
			                                        <label>1 &euro; equivale a</label><br />
			                                        <asp:TextBox ID="txtTipoCambio" style="width:60px; margin-top:3px;" SkinID="numero" Text="" runat="server" onfocus="fn(this, 3, 4)" />
			                                        <label id="lblLiteralMoneda"></label>
			                                    </fieldset>
			                                </td>
			                            </tr>
                                    </table>
                                    <!-- Fin del contenido propio de la página -->
                                </td>
                                <td style="background-image:url(../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                                <td style="background-image:url(../../Images/Tabla/2.gif); height:6px"></td>
                                <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:TextBox ID="hdnReferencia" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnotacionesPersonales" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnInteresado" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdBono" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnImporte" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDTerritorio" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstado" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstadoAnterior" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDEmpresa" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnFecha" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnMoneda" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnOficinaLiquidadora" runat="server" style="visibility:hidden" Text="" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">
    function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
		    switch (strBoton){
			    case "tramitar": 
			    {
                    bEnviar = false;
                    tramitar();
				    break;
			    }
			    case "cancelar": 
			    {
                    bEnviar = false;
                    bCambios = false;
                    location.href = "../Inicio/Default.aspx";
				    break;
			    }
			    case "aprobar": 
			    {
                    bEnviar = false;
                    aprobar();
				    break;
			    }
			    case "noaprobar": 
			    {
                    bEnviar = false;
                    noaprobar();
				    break;
			    }
			    case "aceptarnota": 
			    {
                    bEnviar = false;
                    aceptar();
				    break;
			    }
			    case "noaceptar": 
			    {
                    bEnviar = false;
                    noaceptar();
				    break;
			    }
			    case "anular": 
			    {
                    bEnviar = false;
                    anular();
				    break;
			    }
    			case "pdf": //Boton exportar pdf
			    {
			        bEnviar = false;
			        Exportar();
			        break;
			    }
		    }
	    }

	    var theform;
	    theform = document.forms[0];
	    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	    theform.__EVENTARGUMENT.value = eventArgument;
	    if (bEnviar){
		    theform.submit();
	    }
	    /*else{
		    //Si se ha "cortado" el submit, se restablece el estado original de la botonera.
		    $I("Botonera").restablecer();
	    }*/
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

