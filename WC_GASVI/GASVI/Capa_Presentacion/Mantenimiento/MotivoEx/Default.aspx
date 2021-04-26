<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <script type="text/javascript" language="javascript">
        var strEstructura = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %>";
    </script>

    <div id="divGeneral">
        <center>
            <div class="divContenidoD">
	            <div style="float:right; visibility:visible;margin-right:25px">
		            <input type="checkbox" id="chkSoloActivos" class="check" style="cursor:pointer;margin-right:4px;" checked="checked" title="Si está marcado 'Sólo activos', en caso contrario 'Todos'"/ onclick="GestActInactivos()">
		            <label id="lblSoloActivos" style="cursor:pointer; vertical-align:bottom;" onclick="this.previousSibling.click()">Sólo activos</label> 
	            </div>
                <div style="text-align:center;background-image:url(../../../Images/imgFondo200.gif); background-repeat:no-repeat;
                    width:200px; height:23px; position:relative; top:12px; left:-350px; padding-top:5px; text-align:center;
                    font:bold 12px Arial; color:#5894ae;">Selección de <%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %></div>
                <table class="W950 H200"  cellpadding="0">
                    <tr>
                        <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px"></td>
                        <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
                        <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px"></td>
                    </tr>
                    <tr>
                        <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
                        <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                            <!-- Inicio del contenido propio de la página -->
                            <div id="divIzquierdaSMotivoEx">
                                <table id="tblTitulo" class="W398 H17">
                                    <tr class="TBLINI">
                                        <td class="tdTitulo tdTitulo2Elemento">
                                            <%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %>
                                            <img alt="" class="ICO" id="imgLupa1" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblSN2',0,'divSN2','imgLupa1')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                            <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblSN2',0,'divSN2','imgLupa1',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
                                        </td>
                                    </tr>
                                </table>
                                <div id="divSN2" class="resultadoGeneral W414 H180">
                                     <div class="pijama20 W398">
                                        <%=strTablaHTMLSN2%>
                                     </div>
                                </div>
                                <table id="tblResultado" class="W398 H17">
                                    <tr class="TBLFIN"><td></td></tr>
                                </table>  
                            </div>
                            <div id="divPapelera" class="papelera">
                                <asp:Image id="imgPapelera" style="margin:0px 17px 0px 10px;" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="3"></asp:Image>
                            </div>
                            <br />
                            <div id="divDerechaSMotivoEx">

                                <table id="tblTitulo2" class="W398 H17">
                                    <tr class="TBLINI">
                                        <td style="width:13px;"></td>
                                        <td>
                                            <%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %> con motivos por excepción
                                            <img alt="" class="ICO" id="imgLupa2" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblSN2Ex',1,'divSN2Ex','imgLupa2')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                            <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblSN2Ex',1,'divSN2Ex','imgLupa2',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
                                        </td>            								    
                                    </tr>
                                </table>
                                <div id="divSN2Ex" class="resultadoGeneral W414 H180" target="true" onmouseover="setTarget(this);" caso="1">
                                    <div class="pijama20 W398">
                                        <%=strTablaHTMLSN2Ex%>
                                    </div>
                                </div>                                        
                                <table id="tblResultado2" class="W398 H17">
                                    <tr class="TBLFIN"><td></td></tr>
                                </table>        
                            </div>
                            <!-- Fin del contenido propio de la página -->
                        </td>
                        <td style="background-image:url(../../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="background-image:url(../../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                        <td style="background-image:url(../../../Images/Tabla/2.gif); height:6px"></td>
                        <td style="background-image:url(../../../Images/Tabla/3.gif); height:6px; width:6px"></td>
                    </tr>
                </table> 
            </div>
            <br />
            <div id="divContenidoD" class="divContenidoD hidden">                   
                <div style="text-align:center;background-image:url(../../../Images/imgFondo200.gif); background-repeat:no-repeat;
                    width:200px; height:23px; position:relative; top:12px; left:25px; padding-top:5px; text-align:center;
                    font:bold 12px Arial; color:#5894ae;">Selección de Motivos</div>
                <table class="W950 H200"  cellpadding="0">
                    <tr>
                        <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px"></td>
                        <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
                        <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px"></td>
                    </tr>
                    <tr>
                        <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
                        <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                            <!-- Inicio del contenido propio de la página -->
                            <div id="divIzquierdaIMotivoEx">
                                <table id="tblTitulo3" class="W398 H17">
                                    <tr class="TBLINI">
                                        <td class="tdTitulo tdTitulo2Elemento">
                                            Motivos
                                            <img alt="" class="ICO" id="imgLupa3" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblMotivos',0,'divCatMotivos','imgLupa3')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                            <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblMotivos',0,'divCatMotivos','imgLupa3',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
                                        </td>
                                    </tr>
                                </table>
                                <div id="divCatMotivos" class="resultadoGeneral W414 H180">
                                     <div class="pijama20 W398"></div>
                                </div>
                                <table id="tblResultado3" class="W398 H17">
                                    <tr class="TBLFIN"><td></td></tr>
                                </table>  
                            </div>
                            <div id="divPapelera2" class="papelera">
                                <asp:Image id="imgPapelera2" style="margin:00px 17px 0px 10px;" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                            </div>
                            <br />
                            <div id="divDerechaIMotivoEx">
                                <table id="tblTitulo4" class="W398 H17">
                                    <tr class="TBLINI">
                                        <td style="width:10px;"></td>
                                        <td class="tdTitulo">
                                            Motivos por excepción
                                            <img alt="" class="ICO" id="imgLupa4" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblMotivosEx',1,'divMotivosEx','imgLupa4')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                            <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblMotivosEx',1,'divMotivosEx','imgLupa4',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
                                        </td>            								    
                                    </tr>
                                </table>
                                <div id="divMotivosEx" class="resultadoGeneral W414 H180" target="true" onmouseover="setTarget(this);" caso="1">
                                    <div class="pijama20 W398"></div>
                                </div>                                        
                                <table id="tblResultado4" class="W398 H17">
                                    <tr class="TBLFIN"><td></td></tr>
                                </table>        
                            </div>
                            <!-- Fin del contenido propio de la página -->
                        </td>
                        <td style="background-image:url(../../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="background-image:url(../../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                        <td style="background-image:url(../../../Images/Tabla/2.gif); height:6px"></td>
                        <td style="background-image:url(../../../Images/Tabla/3.gif); height:6px; width:6px"></td>
                    </tr>
                </table> 
            </div>
        </center>
    </div>
    <div class="clsDragWindow" id="DW" noWrap></div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <asp:TextBox ID="hdnDefectoOld" SkinID="Hidden" ReadOnly="true" runat="server" />
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
