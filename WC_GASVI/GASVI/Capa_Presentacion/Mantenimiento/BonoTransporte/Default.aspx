<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_BonoTransporte_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <div id="divGeneral">
        <div id="divBonoSup">
            <div class="contenedor">
                <div class="fieldsetBono">Bonos</div>
                <div id="divBono">
                    <div class="caja3 W435"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5"> 
                        <div id="divRadio">
                            <asp:RadioButtonList ID="rdbEstado" CssClass="texto" runat="server" RepeatColumns="2" ToolTip="Estado del bono" onclick="cambiarCatalogo();">
                                <asp:ListItem Selected="True" Value="A">Activo</asp:ListItem>
                                <asp:ListItem Value="B">Bloqueado</asp:ListItem>
                            </asp:RadioButtonList>    
                        </div>
	                    <table id="tblTitulo" class="tblTituloW398" cellpadding='0'>
	                        <tr class="TBLINI">
		                        <td class="W13">&nbsp;</td>
		                        <td class="tdTitulo tdTitulo1Elementos">Denominación</td>
		                        <td class="tdTitulo tdTitulo3Elementos">Estado</td>
	                        </tr>
                        </table>
                        <div id="divCatalogo" class="resultadoGeneral H125 W414">
	                         <div class="pijama20 W398">
		                        <%=strTablaHTMLBonos%>
	                        </div>
                        </div>
                        <table id="tblResultado" class="tblTituloW398">
	                        <tr class="TBLFIN"><td></td></tr>
                        </table>
                        <center>
                            <table style="width:220px; margin-top:5px;">
                            <tr>
                                <td style="text-align:center">
                                    <button id="btnAnadir" type="button" onclick="nuevoBono();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span></button>	
                                </td>
                                <td style="text-align:center">
                                    <button id="btnEliminar" type="button" onclick="eliminarBono();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>	
                                </td>
                            </tr>
                        </table>
                        </center>
                    </div></div></div></div></div></div>
                </div>
            </div>
            <div id="divContenedorImporte" class="contenedor hidden">
                <div class="fieldsetBono">Importes</div>
                <div id="divImportes">
                    <div class="caja3 W435"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5">
                        <table id="tblTitulo2" class="tblTituloW398" cellpadding="0" cellspacing="0" border="0" style="margin-top:5px;">
                            <tr class="TBLINI">
                                <td style="width:15px;">&nbsp;</td>
                                <td style="width:90px; padding-right:2px; text-align:right;">Cuantía</td>
                                <td style="width:90px; padding-left:5px;">Moneda</td>
	                            <td style="width:93px; padding-left:2px;">Desde</td>
	                            <td style="width:93px; padding-left:2px;">Hasta</td>
                            </tr>
                        </table>
                        <div id="divCatImportes" class="resultadoGeneral W414" style="height:142px;">
                             <div class="pijama20 W398">
                            </div>
                        </div>
                        <table id="tblResultado2" class="tblTituloW398">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>  
                        <center>
                            <table style="width:220px; margin-top:5px; ">
                            <tr>
                                <td style="text-align:center">
                                        <button id="Button3" type="button" onclick="nuevoImporte();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span></button>	
                                    </td>
                                <td style="text-align:center">
                                    <button id="Button4" type="button" onclick="eliminarImporte();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>	
                                </td>
                            </tr>
                        </table>
                        </center>
                    </div></div></div></div></div></div>
                </div>
            </div>
            <div class="divisor"></div>
        </div>       
        <div id="divBonoInf" class="hidden">
            <div class="fieldsetOficinas">Oficinas</div>
            <div id="divOficinas">
                <div class="caja3"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5" style="height:235px">
                    <div class="contenedorOficinasL">
                        <table id="tblTitulo3" class="tblTituloW390">
                            <tr class="TBLINI">
                                <td class="tdTitulo tdTitulo1Elemento">Catálogo</td>
                                <td class="tdTitulo tdTitulo4Elementos">Nºde bonos</td>
                            </tr>
                        </table>
                        <div id="divCatOficinas" class="resultadoGeneral H180 W406">
                            <div class="pijama20 W390">
                                <%=strTablaHTMLOficinas%>
                            </div>
                        </div>
                        <table id="tblResultado3" class="tblTituloW390">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>
                    </div>
                    <div id="divPapelera" class="papelera">
                        <asp:Image id="imgPapelera" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                    </div>               
                    <div class="contenedorOficinasR">
                        <table id="tblTitulo4" class="tblTituloW406">
                            <tr class="TBLINI">
                                <td class="W13">&nbsp;</td>
                                <td class="tdTitulo tdTitulo2Elementos">Oficinas asociadas</td>
                                <td class="tdTitulo">Desde</td>
                                <td class="tdTitulo">Hasta</td>
                            </tr>
                        </table>
                        <div id="divCatBonosOficinas" class="resultadoGeneral H180 W422" target="true" onmouseover="setTarget(this);" caso = "1">
                             <div class="pijama20 W406">
                            </div>
                        </div>
                        <table id="tblResultado4" class="tblTituloW406">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>
                    </div>
                    <span class="divisor" />
                    <br />
                </div></div></div></div></div></div>
            </div>
        </div>
    </div>        
    <div class="clsDragWindow" id="DW" noWrap></div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript" language="javascript">
        function __doPostBack(eventTarget, eventArgument) {
            var bEnviar = true;
            if (eventTarget.split("$")[2] == "Botonera") {
                var strBoton = Botonera.botonID(eventArgument).toLowerCase();
                switch (strBoton) {
                    case "grabar":
                        {
                            bEnviar = false;
                            grabar();
                            break;
                        }
                    case "regresar":
                        {
                            if (bCambios && intSession > 0) {
                                bEnviar = false;
                                jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                                    if (answer) {
                                        bRegresar = true;
                                        grabar();
                                    }
                                    else {
                                        bCambios = false;
                                        fSubmit(true, eventTarget, eventArgument);
                                    }
                                });
                                break;
                            }
                            else
                                fSubmit(bEnviar, eventTarget, eventArgument);
                            break;
                        }
                }
                if (strBoton != "grabar" && strBoton != "regresar")
                    fSubmit(bEnviar, eventTarget, eventArgument);
            }
        }
        function fSubmit(bEnviar, eventTarget, eventArgument) {
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

