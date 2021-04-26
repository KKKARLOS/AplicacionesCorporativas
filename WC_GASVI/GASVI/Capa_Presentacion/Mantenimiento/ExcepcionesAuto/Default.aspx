<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <div id="divGeneral"> 
        <center>    
            <div id="divContenidoT" style="width:950px" >                   
            <div style="background-image:url(../../../Images/imgFondo185.gif); background-repeat:no-repeat;
                width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
                font:bold 12px Arial; color:#5894ae;">Selección de profesionales</div>
            <table class="W950 H200;"  cellpadding="0">
                <tr>
                    <td style="background-image:url(../../../Images/Tabla/7.gif); background-repeat:no-repeat;padding:0px; height:6px; width:6px"></td>
                    <td style="background-image:url(../../../Images/Tabla/8.gif); padding:0px; background-repeat: repeat-x; height:6px;"></td>
                    <td style="background-image:url(../../../Images/Tabla/9.gif); background-repeat:no-repeat;padding:0px;height:6px; width:6px"></td>
                </tr>
                <tr>
                    <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
                    <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <div id="divIzquierdaInferior">
                            <br />
                            <div id="divBuscador" class="buscadorWidth410">
                                <div class="contenidoBuscador">
                                    <div>Apellido1</div>
                                    <div><asp:TextBox ID="txtApellido1" runat="server" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></div>
                                </div>
                                <div class="contenidoBuscador">
                                    <div>Apellido2</div>
                                    <div><asp:TextBox ID="txtApellido2" runat="server" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></div>
                                </div>
                                <div class="contenidoBuscador">
                                    <div>Nombre</div>
                                    <div><asp:TextBox ID="txtNombre" runat="server" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></div>
                                </div>  
                                <div class="divisor"></div>                              
                            </div>
                            <div id="divTablaBuscador" style="margin-top:5px;">
                                <table id="tblTitulo" class="W398 H17">
                                    <tr class="TBLINI">
                                        <td class="W20"></td>
                                        <td class="tdTitulo" style="padding-left:0px">
                                            Profesionales
                                            <img alt="" class="ICO" id="imgLupa1" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblPersonas',1,'divPersonas','imgLupa1')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                            <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblPersonas',1,'divPersonas','imgLupa1',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
	                                    </td>
                                    </tr>
                                </table>
                                <div id="divPersonas" class="resultadoGeneral W414 H170" onscroll="scrollTablaProf()">
                                     <div class="pijama20 W398"></div>
                                </div>
                                <table id="tblResultado" class="W398 H17">
                                    <tr class="TBLFIN"><td></td></tr>
                                </table>  
                            </div>  
                        </div>
                        <div id="divPapelera" class="papelera">
                            <asp:Image id="imgPapelera" style="margin:50px 17px 0px 10px;" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                        </div>
                        <br />
                        <div id="divDerechaInferior">
                            <table id="tblTitulo2" class="W398 H17">
                                <tr class="TBLINI">
                                    <td class="W36"></td>
                                    <td class="tdTitulo" style="padding-left:0px">
                                        Profesionales con autorización excepcional
                                    </td>            								    
                                </tr>
                            </table>
                            <div id="divIntegrantes" class="resultadoGeneral W414 H170" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaIn()">
                                <div class="pijama20 W398">
                                    <%=strTablaHTMLExcep%>
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
                    <td style="background-image:url(../../../Images/Tabla/1.gif); background-repeat:no-repeat;padding:0px; height:6px; width:6px"></td>
                    <td style="background-image:url(../../../Images/Tabla/2.gif); padding:0px; height:6px"></td>
                    <td style="background-image:url(../../../Images/Tabla/3.gif); background-repeat:no-repeat;padding:0px; height:6px; width:6px"></td>
                </tr>
            </table> 
        </div>
            <div id="divMotivoAuto"  class="hidden" >
            <div style="background-image:url(../../../Images/imgFondo185.gif); background-repeat:no-repeat;
                width:185px; height:23px; position:relative; top:12px; left:-135px; padding-top:5px; text-align:center;
                font:bold 12px Arial; color:#5894ae;">Autorización excepcional</div>
            <table class="W500 H200;" cellpadding="0" style="text-align:left">
                <tr>
                    <td style="background-image:url(../../../Images/Tabla/7.gif);background-repeat:no-repeat;padding:0px; height:6px; width:6px"></td>
                    <td style="background-image:url(../../../Images/Tabla/8.gif);background-repeat: repeat-x;padding:0px; height:6px;"></td>
                    <td style="background-image:url(../../../Images/Tabla/9.gif);background-repeat:no-repeat;padding:0px; height:6px; width:6px"></td>
                </tr>
                <tr>
                    <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
                    <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblTitulo3" class="W450 H17" style="margin-top:10px;">
                            <tr class="TBLINI">
                                <td style="padding-left:2px;" class="W15">&nbsp;</td>
                                <td class="tdTitulo" style="width:172px; padding-left:0px">
                                    Motivo
                                    <img alt="" class="ICO" id="imgLupa2" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblMotivosExcepcionAuto',1,'divMotivos','imgLupa2')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                    <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblMotivosExcepcionAuto',1,'divMotivos','imgLupa2',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
                                </td>
                                <td class="tdTitulo" style="width:254px; padding-left:0px;" >Aprobador</td>
                            </tr>
                        </table>
                        <div id="divMotivos" class="resultadoGeneral W466 H125">
                            <div class="pijama20 W450">
                                <%=strTablaHTMLMotivo%>
                            </div>
                        </div>
                        <table id="tblResultado3" class="W450 H17">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td style="background-image:url(../../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image:url(../../../Images/Tabla/1.gif);background-repeat:no-repeat;padding:0px; height:6px; width:6px"></td>
                    <td style="background-image:url(../../../Images/Tabla/2.gif); padding:0px; height:6px"></td>
                    <td style="background-image:url(../../../Images/Tabla/3.gif);background-repeat:no-repeat;padding:0px; height:6px; width:6px"></td>
                </tr>
            </table>             
        </div>
            <div id="divLeyenda">
            <img alt="" src="../../../Images/imgUsuIVM.gif" class="ICO" />Interno&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuEVM.gif" class="ICO" />Externo&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuNVM.gif" class="ICO" />Becario&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuTVM.gif" class="ICO" />ETT&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuGVM.gif" class="ICO" />Genérico
        </div>
        </center>
    </div>
    
    <div class="clsDragWindow" id="DW" noWrap></div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <asp:TextBox ID="hdnEstado" SkinID="Hidden" ReadOnly="true" runat="server" />
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
