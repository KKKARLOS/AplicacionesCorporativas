<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PagoConcertado_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
<!--
    var strEstructuraNodoCorta = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var sMsgRecuperada = "<%=sMsgRecuperada %>";
    var bLectura = <%=(bLectura)? "true":"false" %>;
    var sOrigen = "<%=sOrigen %>";
    var sDiaLimiteContAnoAnterior = <%=sDiaLimiteContAnoAnterior %>;
    var sDiaLimiteContMesAnterior = <%=sDiaLimiteContMesAnterior %>;
    var bSeleccionBeneficiario = <%=((bool)Session["GVT_MULTIUSUARIO"] || User.IsInRole("T") || User.IsInRole("S") || User.IsInRole("A"))? "true":"false" %>;
    var bAdministrador = <%=(User.IsInRole("A"))? "true":"false" %>;
    var sNodoUsuario = "<%=sNodoUsuario %>";
-->
</script>
<asp:Image ID="imgManoVisador" ImageUrl="~/Images/imgManoVisador.gif" style="position:absolute; top:330px; left:930px; cursor:pointer; width:50px; height:50px; visibility:hidden;" onclick="getVisador();" runat="server" ToolTip="" />
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
                    <div id="divTituloDatosGenerales" style="text-align:center; background-image: url(../../Images/imgFondo200.gif);background-repeat:no-repeat;
                        width: 200px; height: 23px; position: relative; top: -20px; left: 10px; padding-top:5px; text-align:center;
                    font:bold 12px Arial;
                    color:#5894ae;">Datos generales</div>
                    <table style="width:950px; margin-top:-15px;" cellpadding="4">
                        <colgroup>
                            <col style="width:90px;" />
                            <col style="width:330px;" />
                            <col style="width:70px;" />
                            <col style="width:250px;" />
                            <col style="width:180px;" />
                        </colgroup>
                        <tr>
                            <td><label id="lblBeneficiario" class="texto" runat="server">Beneficiario</label></td>
                            <td><asp:TextBox ID="txtInteresado" style="width:300px;" Text="" runat="server" ReadOnly="true" /></td>
                            <td>Referencia</td>
                            <td><asp:TextBox ID="txtReferencia" style="width:60px;" SkinID="numero" Text="" runat="server" ReadOnly="true" /></td>
                            <td rowspan="4" style="text-align:left">
                                <asp:Image ID="imgEstado" ImageUrl="~/Images/imgEstado2.gif" style="width:160px; height:80px;" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Motivo</td>
                            <td>
                                <asp:DropDownList ID="cboMotivo" runat="server" Width="140px" onchange="aG();setOblProy();getCC();" AppendDataBoundItems="true"></asp:DropDownList>
                            </td>
                            <td>Empresa</td>
                            <td><asp:TextBox ID="txtEmpresa" style="width:200px;" Text="" runat="server" ReadOnly="true" />
                                <asp:DropDownList ID="cboEmpresa" runat="server" Width="200px" onchange="setEmpresa();aG()" AppendDataBoundItems="true"></asp:DropDownList></td>
                        </tr>
                        <tr>
                        <td>
                            <label id="lblProy" class="enlace" onclick="getPE()" runat="server">Proyecto</label>
                            <span id="spanOblProy" style="color:Red;display:none;">*</span>
                            <label id="lblNodo" runat="server" style="display:none;"><%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></label>
                        </td>
                        <td><asp:TextBox ID="txtProyecto" style="width:300px;" Text="" runat="server" ReadOnly="true" />
                            <asp:TextBox ID="txtDesNodo" style="width:300px;display:none;" Text="" runat="server" ReadOnly="true" />
                        </td>
                            <td>Moneda</td>
                            <td>
                                <asp:DropDownList ID="cboMoneda" runat="server" Width="200px" onchange="aG()" AppendDataBoundItems="true"></asp:DropDownList>
                            </td>                            
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="divAnotaciones" style="cursor:pointer; width:140px;" onclick="getAnotaciones();" runat="server"><asp:Image ID="imgAnotaciones" ImageUrl="~/Images/imgAnotacionesPer.gif" style="vertical-align:middle; margin-top:3px;" runat="server" /> Anotaciones personales</div>
                            </td>                                
                            <td>Importe&nbsp;<span id="spanImp" style="color:Red">*</span></td>
                            <td><asp:TextBox ID="txtImporte" style="width:60px;" Text="" runat="server" class="txtNumL" SkinID="Numero" onfocus="fn(this)" onchange="aG();setImporte(this);" /></td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table style="width:100%;">
                                    <colgroup>
                                        <col style="width:190px; vertical-align:top;" />
                                        <col style="width:760px;" />
                                    </colgroup>
                                    <tr>
                                        <td style="vertical-align: top;">Descripción detallada de la solicitud&nbsp;<span id="spanDesc" style="color:Red">*</span></td>
                                        <td><asp:TextBox TextMode="multiLine" ID="txtObservaciones" style="width:695px; height:60px;" Rows="3" Text="" runat="server" onKeyUp="if(!bLectura) aG();" TabIndex="1" />
                                        <asp:Image ID="imgMail" ImageUrl="~/Images/imgEmailEdit32.png" style="cursor:pointer; display:none; vertical-align:top;" onclick="mdMail();" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdlblAcuerdo" runat="server" style="visibility:hidden;"><label id="lblAcuerdo" class="enlace" onclick="getAC();" runat="server">Acuerdo</label>&nbsp;<span id="span1" style="color:Red;">*</span></td>
                            <td id="tdtxtAcuerdo" runat="server" style="visibility:hidden;"><asp:TextBox ID="txtAcuerdo" style="width:300px;" Text="" runat="server" ReadOnly="true" /></td>
                            <td colspan="3"></td>
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
        <td>
	        <table style="width:990px">
	            <colgroup>
	                <col style="width: 330px;" />
	                <col style="width: 660px;" />
	            </colgroup>
	            <tr style="vertical-align: text-top;">
	                <td>
                    <div id="divTituloOtro" style="text-align:center;background-image: url(../../Images/imgFondo200.gif); background-repeat:no-repeat;
                        width: 200px; height: 23px; position: relative; top: 12px; left: 20px; padding-top:5px; text-align:center;
                        font:bold 12px Arial; color:#5894ae;">Otros pagos concertados</div>
                    <table style="width:320px;" cellpadding="0">
                        <tr>
                            <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px; padding: 0px"></td>
                            <td style="background-image:url(../../Images/Tabla/8.gif); "></td>
                            <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px; padding: 0px"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url(../../Images/Tabla/4.gif); ">&nbsp;</td>
                            <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                                <!-- Inicio del contenido propio de la página -->
	                            <table id="Table1" style="width:280px; height:17px; margin-top:10px;">
                                    <colgroup>					
                                        <col style="width:90px;" />
                                        <col style="width:70px;" />
                                        <col style="width:70px;" />
                                        <col style="width:50px; " />
                                    </colgroup>
                                    <tr class="TBLINI">				    
                                        <td style="padding-left:10px">Solicitud</td>
                				        <td title="Fecha de tramitación">F. Tram.</td>
                                        <td>Moneda</td>
                                        <td style="text-align:right; padding-right:2px">Importe</td>
                                    </tr>
                                </table>
	                            <div id="divCatalogoOtros" style="overflow-x:hidden; overflow:auto; width:296px; height:97px;" runat="server">
                                    <div class="pijama20 W280"></div>
                                </div>
                              <%--  <table id="tblPieOtros" style="width:280px; height:17px;">
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
                                <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px; padding: 0px"></td>
                                <td style="background-image:url(../../Images/Tabla/8.gif); "></td>
                                <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px; padding: 0px"></td>
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
                                  <%--  <table id="tblPieHistorial" style="width:610px;height:17px;">
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
	                <td style="vertical-align: text-bottom">
                        <div id="divContabilizacion" style="text-align:center; background-image: url('../../Images/imgFondo185.gif');background-repeat:no-repeat;
                            width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
                            font:bold 12px Arial; visibility:hidden; color:#5894ae;">Datos para contabilización</div>
                        <table id="tblContabilizacion" style="width:320px; visibility:hidden;" runat="server"  cellpadding="0">                        
                            <tr style="height:6px;">
                                <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px; "></td>
                                <td style="background-image:url(../../Images/Tabla/8.gif); "></td>
                                <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px;"></td>
                            </tr>
                            <tr>
                                <td style="background-image:url(../../Images/Tabla/4.gif);">&nbsp;</td>
                                <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                                    <!-- Inicio del contenido propio de la página -->
	                                <table id="Table2" style="width: 300px; margin-top:5px; padding:5px;">
	                                    <tr>
	                                        <td style="padding-left:5px; width:140px">Fecha <asp:TextBox ID="txtFecContabilizacion" style="width:60px; margin-left:5px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(this);" onchange="aG();" ReadOnly="true" runat="server" goma="0" /></td>
			                                <td style="width:160px">
			                                    <fieldset id="flsTipoCambio" style="width:140px; padding:5px; visibility:hidden;">
			                                        <legend>Tipo de cambio</legend>
			                                        <label>1 &euro; equivale a</label>
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
                            <tr style="height:6px;">
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
<asp:TextBox ID="hdnIdAcuerdoGV" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnImporte" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDTerritorio" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstado" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEstadoAnterior" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDEmpresa" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdMonedaAC" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnOficinaLiquidadora" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCentroCoste" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnNodoCentroCoste" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnNodoBeneficiario" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCCIberper" runat="server" style="visibility:hidden" Text="" />

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
                    ocultarProcesando();
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

