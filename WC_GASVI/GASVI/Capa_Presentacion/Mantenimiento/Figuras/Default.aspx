<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_Figuras_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <script type="text/javascript" language="javascript">
        var strEstructuraNODO = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
        var strEstructuraSUPERNODO1 = "<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) %>";
        var strEstructuraSUPERNODO2 = "<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) %>";
        var strEstructuraSUPERNODO3 = "<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) %>";
        var strEstructuraSUPERNODO4 = "<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) %>";
    </script>
    <center>
        <div id="divGeneral">
            <div id="divCombo">
                Figuras
                <asp:DropDownList ID="cboFiguras" runat="server" onchange="cargarFiguras();" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="margin-top:-15px;">
                <div id="divTituloSuperior" class="fieldsetFiguras">Selección de profesionales</div>
                <div id="divFiguras" style="height:240px">
                    <div class="caja3"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5" style="height:200px"> 
                        <div id="divIzquierdaSuperior" style="margin-left:15px;">
                            <div id="divBuscador" class="buscadorWidth410">
                                <div class="contenidoBuscador">
                                    <div>Apellido1</div>
                                    <div><asp:TextBox ID="txtApellido1" runat="server" style="width:115px" Enabled="false" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional(1);event.keyCode=0;}" MaxLength="50" /></div>
                                </div>
                                <div class="contenidoBuscador">
                                    <div>Apellido2</div>
                                    <div><asp:TextBox ID="txtApellido2" runat="server" style="width:115px" Enabled="false" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional(1);event.keyCode=0;}" MaxLength="50" /></div>
                                </div>
                                <div class="contenidoBuscador">
                                    <div>Nombre</div>
                                    <div><asp:TextBox ID="txtNombre" runat="server" style="width:115px" Enabled="false" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional(1);event.keyCode=0;}" MaxLength="50" /></div>
                                </div>
                                <div class="divisor"></div>
                            </div>
                            <div id="divTablaBuscador"  style="margin-top:5px;">
                                <table id="tblTitulo" class="tblTituloW398">
                                    <tr class="TBLINI">
                                        <td style="width:16px;"></td>
                                        <td class="tdTitulo tdTitulo2Elemento">
                                            Profesionales
                                            <img alt="" class="ICO" id="imgLupa1" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblPersonas',1,'divPersonas','imgLupa1')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                            <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblPersonas',1,'divPersonas','imgLupa1',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
	                                    </td>
                                    </tr>
                                </table>
                                <div id="divPersonas" class="resultadoGeneral H110 W414" onscroll="scrollTablaProf()">
                                     <div class="pijama20 W398"></div>
                                </div>
                                <table id="tblResultado" class="tblTituloW398">
                                    <tr class="TBLFIN"><td></td></tr>
                                </table>  
                            </div>  
                        </div>
                        <div id="divPapeleraSuperior" class="papelera">
                            <asp:Image id="imgPapelera" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                        </div>
                        <div id="divDerechaSuperior" >
                            <table id="tblTitulo2" class="tblTituloW390">
                                <tr class="TBLINI">
                                    <td style="width:33px;"></td>
                                    <td id="tdTituloIntegrantes" class="tdTitulo tdTitulo2Elemento">
                                        Integrantes
                                    </td>            								    
                                </tr>
                            </table>
                            <div id="divIntegrantes" class="resultadoGeneral H110 W406" target="true" onmouseover="setTarget(this)" caso="1" onscroll="scrollTablaProfAsig()">
                                <div class="pijama20 W390">
                                    <%=strTablaHTMLIntegrantes%>
                                </div>
                            </div>                                        
                            <table id="tblResultado2" class="tblTituloW390">
                                <tr class="TBLFIN"><td></td></tr>
                            </table>        
                        </div>
                        <span class="divisor"></span>
                        <br />
                    </div></div></div></div></div></div>
                </div>
            </div>
            <div style="margin-top:-15px">
                <div id="divTituloInferior" class="fieldsetFiguras hidden">Selección de integrantes</div>
                <div id="divSubFiguras" class="hidden"  >
                <div class="caja3"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5" style="height:210px"> 
                    <div id="divIzquierdaInferior"  style="margin-top:5px;margin-left:15px;">
                        <div id="divBuscadorTramitados" class="buscadorWidth410 hidden">
                            <div class="contenidoBuscador">
                                <div>Apellido1</div>
                                <div><asp:TextBox ID="txtSubApellido1" runat="server" Enabled="false" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional(2);event.keyCode=0;}" MaxLength="50" /></div>
                            </div>
                            <div class="contenidoBuscador">
                                <div>Apellido2</div>
                                <div><asp:TextBox ID="txtSubApellido2" runat="server" Enabled="false" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional(2);event.keyCode=0;}" MaxLength="50" /></div>
                            </div>
                            <div class="contenidoBuscador">
                                <div>Nombre</div>
                                <div><asp:TextBox ID="txtSubNombre" runat="server" Enabled="false" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional(2);event.keyCode=0;}" MaxLength="50" /></div>
                            </div>
                            <div class="divisor"></div>
                        </div>
                        <div id="divTablaBuscador2">
                            <table id="tblTitulo3" class="tblTituloW398">
                                <tr class="TBLINI">
                                    <td id="tdTituloBuscador2" class="tdTitulo tdTitulo1Elemento">
                                        Relación de ...
                                        <img alt="" class="ICO" id="imgLupa2" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblPersonas2',1,'divPersonas2','imgLupa2')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                        <img alt="" class="ICO" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblPersonas2',1,'divPersonas2','imgLupa2',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
	                                </td>
                                </tr>
                            </table>
                            <div id="divPersonas2" class="resultadoGeneral H110 W414" onscroll="scrollTablaProf2()">
                                 <div class="pijama20 W398"></div>
                            </div>
                            <table id="tblResultado3" class="tblTituloW398">
                                <tr class="TBLFIN"><td></td></tr>
                            </table>
                        </div>
                    </div>
                    <div id="divPapeleraInferior" class="papelera">
                        <asp:Image id="imgPapelera2" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                    </div>
                    <div id="divDerechaInferior">
                        <table id="tblTitulo4" class="tblTituloW390">
                            <tr class="TBLINI">
                                <td style="width:13px;"></td>
                                <td id="tdTitulo" class="tdTitulo tdTitulo2Elemento" runat="server"></td>            								    
                            </tr>
                        </table>
                        <div id="divTramNodo" class="resultadoGeneral H110 W406" target="true" onmouseover="setTarget(this)" caso="1" onscroll="scrollTablaProfAsig2()">
                            <div class="pijama20 W390"></div>
                        </div>                                        
                        <table id="tblResultado4" class="tblTituloW390">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>        
                    </div>
                    <span class="divisor"></span>
                    <br />
                </div></div></div></div></div></div>
            </div>
            </div>
            <div id="divLeyenda" style="margin: 5px 0px 0px 5px;width:942px">
            <img alt="" src="../../../Images/imgUsuIVM.gif" class="ICO" />Interno&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuEVM.gif" class="ICO" />Externo&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuNVM.gif" class="ICO" />Becario&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuTVM.gif" class="ICO" />ETT&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgUsuGVM.gif" class="ICO" />Genérico
        </div>
        </div>     
    </center>   
    <div class="clsDragWindow" id="DW" noWrap></div>
    <asp:TextBox ID="hdnFiguraAnt" runat="server" style="visibility:hidden" Text="" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript" language="javascript">
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

