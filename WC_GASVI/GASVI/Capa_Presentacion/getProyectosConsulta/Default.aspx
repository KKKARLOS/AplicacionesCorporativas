<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="GASVI.BLL" %>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title> ::: GASVI 2.0 ::: - Selección de proyecto</title>
        <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	    <script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/funcionesPestVertical.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
    </head>
    <body onload="init()" onunload="unload()">
        <form id="Form1" name="frmPrincipal" runat="server">
            <div id="procesando" style="z-index:103; visibility:visible; width:152px; position:absolute; top:209px; left:408px; height:33px">
                <asp:Image ID="imgProcesando" runat="server" Height="33" Width="152" ImageUrl="~/images/imgProcesando.gif" />
                <div id="reloj" style="z-index:104; width:32px; position: absolute; top:1px; left:118px; height:32px">
                    <asp:Image ID="Image1" runat="server" Height="32" Width="32" ImageUrl="~/images/imgRelojAnim.gif" />
                </div>
            </div>
            <script type="text/javascript" language="JavaScript">
                var strServer = "<% =Session["GVT_strServer"].ToString() %>";
                var intSession = <%=Session.Timeout%>;
                var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
            </script>    
            <img id="imgPestHorizontalAux" src="../../Images/imgPestHorizontal.gif" style="z-index:0; position:absolute; left:40px; top:0px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
            <div id="divPestRetr" style="position:absolute; left:20px; top:0px; width:965px; height:140px; clip:rect(auto auto 0px auto)">
                <table style="width:960px; height:120px;" cellpadding="0">
                    <tr>
                        <td style="background-image:url(../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
                        <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align: text-top;">
                        <table style="width:950px; margin-top:5px;" cellpadding="5" cellspacing="1">
                            <colgroup>
                                <col style="width:60px;" />
                                <col style="width:373px;" />
                                <col style="width:140px;" />
                                <col style="width:180px;" />
                                <col style="width:90px;" />
                            </colgroup>
                            <tr>
                                <td>Proyecto</td>
                                <td>
                                    <asp:TextBox ID="txtNumPE" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscar();}else{vtn2(event);setNumPE();}" />
                                    <asp:TextBox ID="txtDesPE" style="width:272px;" Text="" MaxLength="70" runat="server" onkeypress="if(event.keyCode==13){buscar();event.keyCode=0;}else{setDesPE();}" />
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbTipoBusqueda" CssClass="texto" runat="server" Height="20px" RepeatColumns="2" style="position:absolute; top:15px; left:440px;">
                                        <asp:ListItem Value="I"><img src="../../Images/imgIniciaCon.gif" border="0" title="Inicia con" style="cursor:pointer" hidefocus="hidefocus"></asp:ListItem>
                                        <asp:ListItem Selected="True" Value="C"><img src="../../Images/imgContieneA.gif" border="0" title="Contiene" style="cursor:pointer" hidefocus="hidefocus"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <img src="../../Images/imgObtenerAuto.gif" border="0" title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom; margin-left:80px;">
                                    <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                                </td>
                                <td>
                                    <button id="btnObtener" type="button" onclick="buscar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../Images/imgObtener.gif" /><span title="Obtener">Obtener</span></button>	
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblNodo" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesNodo" style="width:335px;" Text="" runat="server" ReadOnly="true" />
                                    <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
                                    <img id="gomaNodo" src="../../Images/Botones/imgBorrar.gif" border="0" onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle;" runat="server">
                                </td>
                                <td colspan="3" style="vertical-align:middle;">
                                    Estado&nbsp;
                                    <asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
                                        <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                                        <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                                        <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;Categoría&nbsp;
                                    <asp:DropDownList id="cboCategoria" runat="server" Width="80px" onChange="setCombo()" CssClass="combo">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                        <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesCliente" style="width:335px;" Text="" ReadOnly="True" runat="server" />
                                    <asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" SkinID="Numero" ReadOnly="True" runat="server" />
                                    <img src="../../Images/Botones/imgBorrar.gif" border="0" title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;">
                                </td>
                                <td colspan="3">
                                    <label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)" style="width:80px;">Responsable</label>
                                    <asp:TextBox ID="txtResponsable" style="width:335px;" Text="" ReadOnly="True" runat="server" />
                                    <asp:TextBox ID="hdnIdResponsable" style="width:1px;visibility:hidden" Text="" SkinID="Numero" ReadOnly="True" runat="server" />
                                    <img src="../../Images/Botones/imgBorrar.gif" border="0" title="Borra el responsable" onclick="borrarResponsable()" style="cursor:pointer; vertical-align:middle;">
                                </td>
                            </tr>
                        </table>
                        </td>
                        <td style="background-image:url(../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
                    </tr>
                    <tr>
		                <td style="background-image:url(../../Images/Tabla/1.gif); width:6px; height:6px;"></td>
                        <td style="background-image:url(../../Images/Tabla/2.gif); width:6px; height:6px;"></td>
                        <td style="background-image:url(../../Images/Tabla/3.gif); width:6px; height:6px;"></td>
                    </tr>
                </table>
            </div>
            <center>
            <table id="Table1" style="width:965px; margin-left:5px; margin-top:25px; text-align:left" cellpadding="5">
	            <tr>
		            <td>
			            <table id="tblTitulo" style="height:17px; width:940px" cellpadding="0" border="0">
			                <colgroup>
			                    <col style="width:20px" />
			                    <col style="width:20px" />
			                    <col style="width:80px" />
			                    <col style="width:340px" />
			                    <col style="width:220px" />
			                    <col style="width:260px" />
			                </colgroup>
				            <tr class="TBLINI">
				                <td></td>
				                <td></td>
					            <td style="text-align:right; padding-right:3px;">
					                <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa1',event)"
							            height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
							        <img id="imgLupa1" style="cursor:pointer; display:none;" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa1')"
							            height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
							        <img style="cursor:pointer; height:11px;" src="../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					                <map name="img1">
					                    <area onclick="ot('tblDatos', 2, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblDatos', 2, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				                    </map>Nº
					            </td>
					            <td>
					                <img style="cursor:pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					                <map name="img2">
					                    <area onclick="ot('tblDatos', 3, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblDatos', 3, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				                    </MAP>&nbsp;Proyecto&nbsp;
				                    <img id="imgLupa2" style="cursor:pointer; display:none;" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa2')"
						                height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						            <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa2',event)"
						                height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
					            </td>
					            <td>
					                <img style="cursor:pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
					                <map name="img3">
					                    <area onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				                    </map>&nbsp;Cliente&nbsp;
				                    <img id="imgLupa3" style="cursor:pointer; display:none;" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa3')"
						                height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						            <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa3',event)"
						                height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					            </td>
					            <td>
					                <img style="cursor:pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
					                <map name="img4">
					                    <area onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				                    </map>&nbsp;<label id="lblNodo2" runat="server">Nodo</label>&nbsp;
				                    <img id="imgLupa4" style="cursor:pointer; display:none;" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa4')"
						                height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						            <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa4',event)"
						                height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					            </td>
				            </tr>
			            </table>
			            <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width:956px; height:540px;" onscroll="scrollTablaProy();">
                            <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif');WIDTH: 940px;">
                                <%=strTablaHTML%>
                            </div>
                        </div>
                        <table id="tblResultado" style="height:17px; width:940px;text-align:left">
				            <tr class="TBLFIN">
					            <td>&nbsp;</td>
				            </tr>
			            </table>
		            </td>
                </tr>
            </table>
            </center>
            <br />
            <br />
            <table width="940px" style="text-align:center">
                <colgroup>
                    <col style="width:100px" />
                    <col style="width:90px" />
                    <col style="width:210px" />
                    <col style="width:540px" />
                </colgroup>
	              <tr> 
	                <td><img class="ICO" src="../../Images/imgProducto.gif" />Producto</td>
                    <td><img class="ICO" src="../../Images/imgServicio.gif" />Servicio</td>
                    <td></td>
		            <td rowspan="3" style="vertical-align: text-top;">
		                <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../Images/Botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
		            </td>
	              </tr>
	              <tr>
	                <td style="vertical-align: text-top;"><img class="ICO" src="../../Images/imgIconoProyAbierto.gif" title="Proyecto abierto" />Abierto</td>
                    <td><img class="ICO" src="../../Images/imgIconoProyCerrado.gif" title="Proyecto cerrado" />Cerrado</td>
                    <td>
                        <img class="ICO" src="../../Images/imgIconoProyHistorico.gif" title="Proyecto histórico" />Histórico
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../Images/imgIconoProyPresup.gif" title="Proyecto presupuestado" />Presupuestado
                    </td>
                  </tr>
            </table>
            <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
            <uc_mmoff:mmoff ID="mmoff1" runat="server" />
        </form>
        <script type="text/javascript" language="javascript">
	        <!--
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
    </body>
</html>

