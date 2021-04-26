<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <script type="text/javascript">
        var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
        var js_cri = new Array();
        var sNodos = "<%=sNodos %>";
    </script>
    <br /> 
    <img id="imgPestHorizontalAux" src="../../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
    <div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:850px; height:280px; clip:rect(auto auto 0px auto)">
        <table style="width:850px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table class="texto" style="width:830px; height:280px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                    <tr>
		                <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                        <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                            <!-- Inicio del contenido propio de la página -->
                            <table class="texto" style="width:820px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                            <colgroup>
                                <col style="width:310px;" />
                                <col style="width:155px;" />
                                <col style="width:155px;" />
                                <col style="width:155px;" />
                                <col style="width:30px;" />
                            </colgroup>
                            <tr style="height:50px;">
                                <td>
                                    Denominación experiencia profesional<br />
                                    <asp:TextBox ID="txtDesExp" style="width:290px;" Text="" MaxLength="70" runat="server" />   
                                </td>
                                <td style="text-align:right;">
                                    <img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                    <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:15px;"
                                        title="Repliegue automático de la pestaña de criterios al obtener información">
                                    <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                                </td>
                                <td>
                                    <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom; margin-left:20px;">
                                    <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                                </td>
                                <td>
                                    <button id="btnObtener" type="button" onclick="buscar();" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" style="margin-left:60px;">
                                        <img src="../../../../images/imgObtener.gif" /><span>Obtener</span>
                                    </button>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                        <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                        <div id="divAmbito" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); background-repeat:repeat; width:260px; height:auto;">
                                             <table id="tblAmbito" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0 >
                                             <%//=strHTMLAmbito%>
                                             </table>
                                            </div>
                                        </div>
                                    </FIELDSET>
                                </td>
                                <td colspan="2">
                                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                        <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                        <div id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                             <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                             <%//=strHTMLResponsable%>
                                             </table>
                                            </div>
                                        </div>
                                    </FIELDSET>
                                </td>
                                <td colspan="2">
                                    <FIELDSET style="width: 135px; height:58px; padding:5px; margin-top:2px;">
                                        <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                                        <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:15px; margin-left:20px;" onclick="setOperadorLogico(true)">
                                            <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </FIELDSET>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                        <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                        <div id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                             <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                             <%//=strHTMLCliente%>
                                             </table>
                                            </div>
                                        </div>
                                    </FIELDSET>
                                </td>
                                <td colspan="2">
                                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                        <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                        <div id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                             <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                             <%//=strHTMLProyecto%>
                                             </table>
                                            </div>
                                        </div>
                                    </FIELDSET>
                                </td>
                                <td colspan="2">
                                    Estado:&nbsp;
                                    <asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()" CssClass="combo">
                                        <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
                                        <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                                        <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                                        <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br /><br />
                                    Categoría:&nbsp;<asp:DropDownList id="cboCategoria" runat="server" Width="90px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                    <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>                            
                            </tr>
                            </table>
                            <!-- Fin del contenido propio de la página -->
                        </td>
                        <td background="../../../../../Images/Tabla/6.gif" width="6">
                            &nbsp;</td>
                    </tr>
                    <tr>
				        <td background="../../../../../Images/Tabla/1.gif" height="6" width="6">
				        </td>
                        <td background="../../../../../Images/Tabla/2.gif" height="6">
                        </td>
                        <td background="../../../../../Images/Tabla/3.gif" height="6" width="6">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </div>
    <table class="texto" id="tblGeneral" style="width:1260px; margin-left:5px; margin-top:5px;" cellspacing="0" cellpadding="5" border="0">
	    <tr>
		    <td>
			    <table id="tblTitulo" style="width:1170px; height:17px;" cellspacing="0" cellpadding="0" border="0">
			        <colgroup>
			            <col style="width:270px" />
			            <col style="width:80px" />
			            <col style="width:20px" />
			            <col style="width:200px" />
			            <col style="width:200px" />
			            <col style="width:200px" />
			            <col style="width:200px" />
			        </colgroup>
				    <tr class="TBLINI">
				        <td style="width:285px">
                            <img style="margin-left:3px; CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img0" border="0">
						        <map name="img0">
						            <area onclick="ot('tblDatos', 0, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						            <area onclick="ot('tblDatos', 0, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					            </map>&nbsp;<label id="Label11" runat="server">Experiencia profesional</label>&nbsp;
					            <img id="imgLupa0" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa0')"
							        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa0', event)"
							        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				        </td>
					    <td style="width:65px; text-align:right;">
						    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					        <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						    <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					        <map name="img1">
					            <area onclick="ot('tblDatos', 1, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <area onclick="ot('tblDatos', 1, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </map>&nbsp;Nº&nbsp;&nbsp;
					    </td>
				        <td style="width:20px"></td>
					    <td style="width:200px">
                            <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						        <map name="img2">
						            <area onclick="ot('tblDatos', 3, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						            <area onclick="ot('tblDatos', 3, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					            </map>&nbsp;Proyecto&nbsp;
					            <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa2')"
							        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa2', event)"
							        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </td>
					    <td style="width:200px">
                            <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
						        <map name="img3">
						            <area onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						            <area onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					            </map>&nbsp;Cliente&nbsp;
					            <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa3')"
							        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa3', event)"
							        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </td>
					    <td style="width:200px">
                            <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						        <map name="img4">
						            <area onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						            <area onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					            </map>&nbsp;<label id="lblNodo2" runat="server">Nodo</label>&nbsp;
					            <img id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa4')"
							        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa4', event)"
							        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </td>
					    <td style="width:200px">
                            <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
						        <map name="img5">
						            <area onclick="ot('tblDatos', 6, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						            <area onclick="ot('tblDatos', 6, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					            </map>&nbsp;<label id="Label1" runat="server">Responsable proyecto</label>&nbsp;
					            <img id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa5')"
							        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa5', event)"
							        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </td>
				    </tr>
			    </table>
			    <div id="divCatalogo" style="OVERFLOW:auto; OVERFLOW-X:hidden; width:1186px; HEIGHT: 740px;" onscroll="scrollTablaProy();">
                    <div style="background-image: url('../../../../../Images/imgFT20.gif'); background-repeat:repeat; width:1170px; height:auto;">
                    <%=strTablaHTML%>
                    </div>
                </div>
                <table id="tblResultado" style="height:17px; width:1170px;" cellSpacing="0" cellPadding="0" border="0">
				    <tr class="TBLFIN">
					    <td>&nbsp;</td>
				    </tr>
			    </table>
			    <table style="width:400px; margin-top:5px;">
                    <colgroup>
                        <col style="width:100px" />
                        <col style="width:90px" />
                        <col style="width:210px" />
                    </colgroup>
	                  <tr>
	                    <td><img class="ICO" src="../../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
                            <td><img class="ICO" src="../../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
                            <td><img class="ICO" src="../../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
                        </td>
                      </tr>
                </table>
		    </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript">

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

