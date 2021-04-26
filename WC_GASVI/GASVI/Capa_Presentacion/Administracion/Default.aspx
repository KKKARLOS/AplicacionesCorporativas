<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <script language="javascript" type="text/javascript">
        var nConsultas = <%=nConsultas.ToString() %>;
    </script>
    <table>
        <colgroup>
            <col style="width:45%; padding-left:30px;" />
            <col style="width:55%;" />
        </colgroup>
        <tr>
            <td>
                <br />
                <table id="tblTitulo" class="W390 H17">
                    <tr class="TBLINI">
                        <td style="padding-left:5px;">Mantenimientos</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; height:240px; width:423px;">
                    <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT16.gif'); width:390px;">
                        <table id="tblMantenimiento" class="MANO W390">
                            <tr id="Mant_1" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Acuerdo/Default.aspx'">Acuerdos</td></tr>
                            <tr id="Mant_2" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/AnnoGasto/Default.aspx'">Año gasto</td></tr>
                            <tr id="Mant_3" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Autorresponsables/Default.aspx'">Autorresponsable</td></tr>
                            <tr id="Mant_4" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/BonoTransporte/Default.aspx'">Bono transporte</td></tr>
                            <tr id="Mant_5" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/CentrosCoste/Default.aspx'">Centros de coste</td></tr>
                            <tr id="Mant_6" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/ExcepcionesAuto/Default.aspx'">Excepciones autorización</td></tr>
                            <tr id="Mant_7" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Figuras/Default.aspx'">Figuras</td></tr>
                            <tr id="Mant_8" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/ImportesConvenio/Default.aspx'">Importes convenio</td></tr>
                            <tr id="Mant_9" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Monedas/Default.aspx'">Monedas</td></tr>
                            <tr id="Mant_10" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Motivos/Default.aspx'">Motivos</td></tr>
                            <tr id="Mant_11" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/MotivoEx/Default.aspx'">Motivos por excepción</td></tr>
                            <tr id="Mant_12" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/OficinaLiquidacion/Default.aspx'">Oficinas de liquidación</td></tr>
                            <tr id="Mant_13" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Parametrizacion/Default.aspx'">Parametrización GASVI</td></tr>
                            <tr id="Mant_14" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Tooltips/Default.aspx?t=0'">Textos de ayuda</td></tr>
                            <tr id="Mant_15" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Mantenimiento/Avisos/Catalogo/Default.aspx?t=0'">Comunicados de Administración</td></tr>
                        </table>
                    </div>
                </div>                                        
               <%-- <table id="tblResultado" class="W390 H17">
                    <tr class="TBLFIN"><td></td></tr>
                </table>--%>
                <br />
                <table id="tblTitulo2" class="W390 H17">
                    <tr class="TBLINI">
                        <td style="padding-left:5px;">Procesos</td>
                    </tr>
                </table>
                <div id="divProcesos" style="overflow-x:hidden; overflow-y:auto; height:160px; width:423px;">
                    <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT16.gif'); width:390px;">
                        <table id="Table2" class="MANO W390">
                            <tr id="Proc_1" onclick="ms(this)" style="height:16px"><td style="padding-left:5px;" onclick="location.href='../Procesos/CambioEstado/Default.aspx'">Cambio de estado de solicitud</td></tr>
                            <tr style="height:16px"><td style="padding-left:5px;">Anulación de notas al cierre de ejercicio (pendiente)</td></tr>
                            <!--<tr><td>proceso3</td></tr>
                            <tr><td>proceso4</td></tr>
                            <tr><td>proceso5</td></tr>
                            <tr><td>proceso6</td></tr>
                            <tr><td>proceso7</td></tr>
                            <tr><td>proceso8</td></tr>
                            <tr><td>proceso9</td></tr>
                            <tr><td>proceso10</td></tr>
                            <tr><td>proceso11</td></tr>
                            <tr><td>proceso12</td></tr>
                            <tr><td>proceso13</td></tr>-->
                        </table>
                    </div>
                </div>                                        
               <%-- <table id="tblResultado2" class="W390 H17">
                    <tr class="TBLFIN"><td></td></tr>
                </table>--%>
            </td> 
            <td>
                <table style="width:520px;">
                    <tr>
                        <td>
                            <table style="width:500px;">
                                <colgroup>
                                    <col style="width:80px; padding-left:3px;" />
                                    <col style="width:420px; padding-left:3px;" />
                                </colgroup>
                                <tr style="height:75px;">
                                    <td colspan="2">                                    
                                        <div  style="text-align:left;width:400px;">
                                            <div  style="background-image: url('<%=Session["GVT_strServer"] %>Images/imgFondo185.gif'); background-repeat:no-repeat;
                                                width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:6px;text-align:center;">
                                                &nbsp;Consultas personalizadas
                                            </div>
                                            <table  cellpadding="0">
                                                <tr>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/7.gif'); width:6px; height:6px"></td>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/8.gif');"></td>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/9.gif');width:6px; height:6px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/4.gif');">&nbsp;</td>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/5.gif'); padding:5px; vertical-align:top;">
                                                        <!-- Inicio del contenido propio de la página -->                                            	
                                                        <table id="tblEstadisticas" style="width:370px;">
                                                            <colgroup>
                                                                <col style="width:65px;" />
                                                                <col style="width:25px; text-align:right;" />
                                                                <col style="width:55px;" />
                                                                <col style="width:50px;" />
                                                                <col style="width:25px; text-align:right;" />
                                                                <col style="width:67px;" />
                                                                <col style="width:55px;" />
                                                                <col style="width:25px; text-align:right;" />
                                                            </colgroup>
                                                            <tr>
                                                                <td>Disponibles:</td>
                                                                <td id="cldTotal"><%=nConsultas.ToString() %></td>
                                                                <td></td>
                                                                <td>Activas:</td>
                                                                <td id="cldActivas">0</td>
                                                                <td></td>
                                                                <td>Inactivas:</td>
                                                                <td id="cldInactivas">0</td>
                                                            </tr>
                                                        </table>
                                                        <!-- Fin del contenido propio de la página -->
                                                    </td>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/6.gif');  width:6px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/1.gif'); width:6px; height:6px"></td>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/2.gif'); height:6px; padding:0px;"></td>
                                                    <td style="background-image: url('<%=Session["GVT_strServer"] %>Images/Tabla/3.gif'); width:6px; height:6px"></td>
                                                </tr>
                                            </table>                                        
                                        </div>   
                                        <div id="divCheck" style="position:relative; top:-30px; left:385px;">
	                                        <asp:CheckBox ID="chkTodos" runat="server" style="cursor:pointer; text-align:right;" Text="Mostrar inactivas" Width="120px" ToolTip="Mostrar todas las consultas." onclick="getTodasConsultas();" />
                                        </div>                                 
                                    </td>
                                </tr>
                                <tr id="Tr1" class="TBLINI">
                                    <td style="padding-left:23px;">Número</td>
                                    <td>Denominación</td>
                                </tr>
                            </table>
                            <div id="divConsultas" style="overflow-x:hidden; overflow-y:auto; width:516px; height:405px;">
                                <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); width:500px;">
                                    <%=strTablaHTML%>
                                </div>
                            </div>
                          <%--  <table id="tblTotales" style="width:500px;">
                                <tr class="TBLFIN">
                                    <td>&nbsp;</td>
                                </tr>
                            </table>--%>
                        </td>
                    </tr>
                </table>
            </td>   
            </tr>
        </table>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<iframe id="iFrmDescarga" frameborder="0" name="iFrmDescarga" width="10px" height="10px" style="visibility:hidden" ></iframe>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript" language="javascript">
    <!--
        function __doPostBack(eventTarget, eventArgument) {
            var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
		        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
                switch (strBoton) {
                    case "buscar":
                        {
                            bEnviar = false;
                            location.href = "../Consultas/Administrador/Default.aspx?so=in";
                            break;
                        }
                }
            }

            var theform;
            theform = document.forms[0];
            theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
            theform.__EVENTARGUMENT.value = eventArgument;
            if (bEnviar) {
                theform.submit();
            }
//            else {
//                //Si se ha "cortado" el submit, se restablece el estado original de la botonera.
//                $I("Botonera").restablecer();
//            }
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
</asp:Content>
