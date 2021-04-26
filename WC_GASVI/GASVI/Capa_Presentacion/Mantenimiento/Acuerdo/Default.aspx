<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_Acuerdo_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <script type="text/javascript" language="javascript">
        var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    </script>
    <img id="imgExcel_exp" src="../../../images/imgExcelAnim.gif" title="Exporta a Excel el contenido de la tabla" style="position: absolute; top: 193px; left: 473px; height: 16px; width: 16px; border-width: 0px; z-index: 0; visibility: visible;" class="MANO" onclick="mostrarProcesando();setTimeout('excelAcuerdo()',500);" >
    <img id="imgExcel2_exp" src="../../../images/imgExcelAnim.gif" title="Exporta a Excel el contenido de la tabla" style="position: absolute; top: 175px; left: 923px; height: 16px; width: 16px; border-width: 0px; z-index: 0; visibility: hidden;" class="MANO" onclick="mostrarProcesando();setTimeout('excelProfesionales()',500);">
    <img id="imgExcel3_exp" src="../../../images/imgExcelAnim.gif" title="Exporta a Excel el contenido de la tabla" style="position: absolute; top: 442px; left: 473px; height: 16px; width: 16px; border-width: 0px; z-index: 0; visibility: hidden;" class="MANO" onclick="mostrarProcesando();setTimeout('excelProyectos()',500);">
    <img id="imgExcel4_exp" src="../../../images/imgExcelAnim.gif" title="Exporta a Excel el contenido de la tabla" style="position: absolute; top: 442px; left: 923px; height: 16px; width: 16px; border-width: 0px; z-index: 0; visibility: hidden;" class="MANO" onclick="mostrarProcesando();setTimeout('excelNodos()',500);">
    <div id="divGeneral" class="W900">
        <div id="divAcuerdoSup">
            <div class="contenedor">
                <div class="fieldsetAcuerdo">Acuerdos</div>
                <div class="divAcuerdo">
                    <div class="caja3 W435"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5"> 
                        <div id="divCheck" style="margin-bottom:5px">
	                        <asp:CheckBox ID="chkTodos" runat="server" style="cursor:pointer; text-align:right;" Text="Mostrar anteriores" Width="150px" ToolTip="Mostrar acuerdos anteriores." onclick="mostrarProcesando();setTimeout('mostrarTodos();',20);" />
                        </div>
	                    <table id="tblTitulo" class="tblTituloW398">
                            <colgroup>
                                 <col style="width:75px;" />
                                 <col style="width:193px; padding-left:10px;" />
                                 <col style="width:65px;" />
                                 <col style="width:65px;" />
                            </colgroup>
	                        <tr class="TBLINI">
		                        <td style="text-align:right">
					                <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblAcuerdos',1,'divCatAcuerdos','imgLupa1',event)"
							            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
							        <img id="imgLupa1" style="cursor:pointer; display:none;" onclick="buscarSiguiente('tblAcuerdos',1,'divCatAcuerdos','imgLupa1')"
							            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
							        <img style="cursor:pointer;margin: 0px 5px 0px 5px; height:11px;" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					                <map name="img1">
					                    <area onclick="ot('tblAcuerdos', 1, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblAcuerdos', 1, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
				                    </map>&nbsp;Nº
					            </td>
		                        <td>
		                            <img style="cursor:pointer;margin-left: 10px;" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img2" border="0" />
					                <map id="img2" name="img2">
					                    <area onclick="ot('tblAcuerdos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					                    <area onclick="ot('tblAcuerdos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				                    </map>&nbsp;Denominación
		                            <img alt="" class="ICO" id="imgLupa2" style="cursor:pointer; display:none;" height="11px" width="20px" 
		                            onclick="buscarSiguiente('tblAcuerdos',2,'divCatAcuerdos','imgLupa2')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                                    <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" 
                                    onclick="buscarDescripcion('tblAcuerdos',2,'divCatAcuerdos','imgLupa2',event)" src="../../../Images/imgLupa.gif" tipolupa="1" /> 
                                </td>
		                        <td>
		                            <img style="cursor:pointer;" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img3" border="0" />
					                <map id="img3" name="img3">
					                    <area onclick="ot('tblAcuerdos', 3, 0, 'fec', '')" shape="RECT" coords="0,0,6,5" />
					                    <area onclick="ot('tblAcuerdos', 3, 1, 'fec', '')" shape="RECT" coords="0,6,6,11" />
				                    </map>&nbsp;Inicio
				                </td>
		                        <td>
		                            <img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img4" border="0" />
					                <map id="img4" name="img4">
					                    <area onclick="ot('tblAcuerdos', 4, 0, 'fec', '')" shape="RECT" coords="0,0,6,5" />
					                    <area onclick="ot('tblAcuerdos', 4, 1, 'fec', '')" shape="RECT" coords="0,6,6,11" />
				                    </map>&nbsp;Fin
				                </td>
	                        </tr>
                        </table>
                        <div id="divCatAcuerdos" class="resultadoGeneral H125 W414">
	                         <div class="pijama20 W398">
		                        <%=strTablaHTMLAcuerdos%>
	                        </div>
                        </div>
                        <table id="tblResultado" class="tblTituloW398">
	                        <tr class="TBLFIN"><td></td></tr>
                        </table>  
                        <center>
                            <table style="width:250px; margin-top:5px; margin-left:15px;text-align:center">
                            <tr>
                                <td style="text-align:center">
                                    <button id="btnAnadir" type="button" onclick="nuevoAcuerdo();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span></button>	
                                </td>
                                <td style="text-align:center">
                                    <button id="btnEliminar" type="button" onclick="eliminarAcuerdo();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>	
                                </td>
                            </tr>
                        </table>
                        </center>
                    </div></div></div></div></div></div>
                </div>
            </div>
            <div id="divContenedorProf" class="contenedor hidden">
                <div class="fieldsetAcuerdo">Profesionales</div>
                <div class="divAcuerdo">
                    <div class="caja3 W435"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5">
                        <table id="tblTitulo2" class="tblTituloW398">
                            <tr class="TBLINI">
                                <td class="W33">&nbsp;</td>
                                <td class="tdTitulo tdTitulo1Elementos">Profesionales</td>
                            </tr>
                        </table>
                        <div id="divCatProf" class="resultadoGeneral H145 W414" onscroll="scrollTablaProf()">
                             <div class="pijama20 W398">
                            </div>
                        </div>
                        <table id="tblResultado2" class="tblTituloW398">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>  
                        <center>
                            <table style="width:250px; margin-top:5px; margin-left:15px;text-align:center">
                            <tr>
                                <td style="text-align:center">
                                    <button id="btnAnadirProf" type="button" onclick="catProfesionales();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span></button>	
                                </td>
                                <td style="text-align:center">
                                    <button id="btnEliminarProf" type="button" onclick="eliminarProf();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>	
                                </td>
                            </tr>
                        </table>
                        </center>
                    </div></div></div></div></div></div>
                </div>
            </div>
            <div class="divisor"></div>
        </div>       
        <div id="divAcuerdoInf" style="visibility:hidden; height:245px;" >
            <div class="contenedor">
                <div class="fieldsetAcuerdo">Proyectos</div>
                <div class="divAcuerdo">
                    <div class="caja3 W435"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5"> 
	                    <table id="tblTitulo3" class="tblTituloW398">
	                        <tr class="TBLINI">
		                        <td class="W33">&nbsp;</td>
		                        <td class="tdTitulo tdTitulo1Elementos">&nbsp;Denominación</td>
	                        </tr>
                        </table>
                        <div id="divCatProy" class="resultadoGeneral H125 W414" onscroll="scrollTablaProy()">
	                        <div class="pijama20 W398">
	                        </div>
                        </div>
                        <table id="tblResultado3" class="tblTituloW398">
	                        <tr class="TBLFIN"><td></td></tr>
                        </table> 
                        <center>
                            <table style="width:250px; margin-top:5px; margin-left:15px;text-align:center">
                            <tr>
                                <td style="text-align:center">
                                    <button id="btnAnadirProy" type="button" onclick="catProyectos();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span></button>	
                                </td>
                                <td style="text-align:center">
                                    <button id="btnEliminarProy" type="button" onclick="eliminarProy();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>	
                                </td>
                            </tr>
                        </table>
                        </center> 
                    </div></div></div></div></div></div>
                </div>
            </div>
            <div class="contenedor">
                <div class="fieldsetAcuerdoCR"><%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></div>
                <div class="divAcuerdo">
                    <div class="caja3 W435"><div class="c1"><div class="c2"><div class="c3"><div class="c4"><div class="c5">
                        <table id="tblTitulo4" class="tblTituloW398">
                            <tr class="TBLINI">
                                <td class="W13">&nbsp;</td>
                                <td class="tdTitulo tdTitulo1Elementos">Denominación</td>
                            </tr>
                        </table>
                        <div id="divCatCR" class="resultadoGeneral H125 W414">
                             <div class="pijama20 W398">
                            </div>
                        </div>
                        <table id="tblResultado4" class="tblTituloW398">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>  
                        <center>
                            <table style="width:250px; margin-top:5px; margin-left:15px;text-align:center">
                            <tr>
                                <td style="text-align:center">
                                    <button id="btnAnadirCR" type="button" onclick="catNodos();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span></button>	
                                </td>
                                <td style="text-align:center">
                                    <button id="btnEliminarCR" type="button" onclick="eliminarCR();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>	
                                </td>
                            </tr>
                        </table>
                        </center>
                    </div></div></div></div></div></div>
                </div>
            </div>
        </div>
        <div class="divLeyenda" style="text-align:left; margin-top: 10px">
            <img alt="" src="../../../Images/imgIconoProyAbierto.gif" class="ICO" title="Proyecto abierto" />Abierto&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgIconoProyCerrado.gif" class="ICO" title="Proyecto cerrado" />Cerrado&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgIconoProyHistorico.gif" class="ICO" title="Proyecto histórico" />Histórico&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../Images/imgIconoProyPresup.gif" class="ICO" title="Proyecto presupuestado" />Presupuestado&nbsp;&nbsp;&nbsp;
        </div>
    </div>        
    <div class="clsDragWindow" id="DW" noWrap></div>
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
				    //case "regresar":
				    //{
				    //    if (bCambios && intSession > 0) {
				    //        if (confirm("Datos modificados. ¿Desea grabarlos?")) {
				    //            bEnviar = false;
				    //            bRegresar = true;
				    //            grabar();
				    //        } 
				    //        else
				    //            bCambios = false;
				    //    }
				    //    break;
				    //}
			        case "regresar":
			            {
			                if (bCambios && intSession > 0) {
			                    bEnviar = false;
			                    jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
			                        if (answer) {
			                            bRegresar = true;
			                            grabar();
			                        }
			                        fSubmit(bEnviar, eventTarget, eventArgument);
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

