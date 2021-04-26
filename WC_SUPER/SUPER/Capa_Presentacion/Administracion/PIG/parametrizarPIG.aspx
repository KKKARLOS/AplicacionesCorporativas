<%@ Page Language="C#" AutoEventWireup="true" CodeFile="parametrizarPIG.aspx.cs" Inherits="getHistoria" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Parametrización del proceso de generación de proyectos improductivos genéricos</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="./Functions/parametrizar.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
	var strServer = "<%=Session["strServer"]%>";
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var aNat = new Array(); // aNN --> Array de Naturalezas
    var aNN = new Array(); // aNN --> Array de Nodos Naturalezas
</script>
<center>
    <table style="width:1120px; text-align:left">
    <colgroup><col style="width:420px;" /><col style="width:700px;" /></colgroup>
    <tr>
        <td>
            <div align="left" style="width:300px;">
                <div align="center" style="background-image: url('../../../Images/imgFondo185.gif');background-repeat:no-repeat;
                    width: 185px; height: 23px; position: absolute; top: 15px; left: 70px; padding-top: 5px;">
                    &nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></div>
                <table border="0" cellspacing="0" cellpadding="0" style="margin-top:25px;">
                  <tr>
                    <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                    <td height="6" background="../../../Images/Tabla/8.gif"></td>
                    <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
                  </tr>
                  <tr>
                    <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:3px; padding-top:10px;">
	                <!-- Inicio del contenido propio de la página -->
            	
                    <table id="tblEstadisticas" style="WIDTH: 250px;" cellpadding="3">
                        <colgroup>
                        <col style="width:220px;" />
                        <col style="width:30px;" />
                        </colgroup>
                        <tr>
                            <td>Mostrados (activos en estructura activa):</td>
                            <td id="cldEstNodo" style="text-align:right;">0</td>
                        </tr>
                        <tr>
                            <td>Seleccionados para cálculo:</td>
                            <td id="cldEstNodoSel" style="text-align:right;">0</td>
                        </tr>
                    </table>

	                <!-- Fin del contenido propio de la página -->
	                </td>
                    <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
                  </tr>
                  <tr>
                    <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
                    <td height="6" background="../../../Images/Tabla/2.gif"></td>
                    <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
                  </tr>
                </table>
            </div>
        </td>
        <td>
            <div align="left" style="width: 370px;">
                <div align="center" style="background-image: url('../../../Images/imgFondo185.gif');;background-repeat:no-repeat;
                    width: 185px; height: 23px; position: absolute; top: 15px; left:500px; padding-top: 5px;">
                    &nbsp;Naturalezas</div>
                <table border="0" cellspacing="0" cellpadding="0" style="margin-top:25px;">
                  <tr>
                    <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                    <td height="6" background="../../../Images/Tabla/8.gif"></td>
                    <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
                  </tr>
                  <tr>
                    <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:3px; padding-top:10px;">
	                <!-- Inicio del contenido propio de la página -->
            	
                    <table style="WIDTH: 290px;" cellpadding="3">
                        <colgroup>
                        <col style="width:260px;" />
                        <col style="width:30px;" />
                        </colgroup>
                        <tr>
                            <td>Naturalezas improductivas: </td>
                            <td id="cldNatImprod" style="text-align:right;">0</td>
                        </tr>
                        <tr>
                            <td>Con vigencia parametrizada superior a 12 meses: </td>
                            <td id="cldVigParam" style="text-align:right;">0</td>
                        </tr>
                    </table>

	                <!-- Fin del contenido propio de la página -->
	                </td>
                    <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
                  </tr>
                  <tr>
                    <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
                    <td height="6" background="../../../Images/Tabla/2.gif"></td>
                    <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
                  </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <table style="width: 380px;">
                <colgroup>
                    <col style="width:50px;" />
                    <col style="width:30px;" />
                    <col style="width:270px;" />
                    <col style="width:30px;" />
                </colgroup>
                <tr align="center">
                    <td style="padding-left:1px;">
                        <img id="imgMarcar" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarCalcular(1)" />
                        <img id="imgDesmarcar" src="../../../Images/Botones/imgDesmarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarCalcular(0)" />
                    </td>
				    <td style="text-align:right;" ></td>
				    <td style="padding-bottom:2px;padding-left:3px;">
				        <table style="width:200px; BORDER-COLLAPSE: collapse; text-align:right; margin-left:90px;" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td>
							    <button id="btnAtnat" type="button" onclick="mostrarProcesando();setTimeout('marcarTodo()',20);" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)" title="Asignar todas las naturalezas a todos los <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>">
								    <span><img src="../../../images/Botones/imgMarcarTodo.gif" />&nbsp;&nbsp;ATNAT<%= Estructura.getDefLarga(Estructura.sTipoElem.NODO).Substring(0,1).ToUpper() %></span>
							    </button>							
                            </td>
                            <td>
							    <button id="btnDtdt" type="button" onclick="mostrarProcesando();setTimeout('desmarcarTodo()',20);" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)" title="Desasignar todas las naturalezas de todos los <%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>">
								    <span><img src="../../../images/Botones/imgDesmarcarTodo.gif" />&nbsp;&nbsp;DTNDT<%= Estructura.getDefLarga(Estructura.sTipoElem.NODO).Substring(0,1).ToUpper() %></span>
							    </button>							
                            </td>
                        </tr>
                        </table>
				    </td>
				    <td style="text-align:right; padding-right: 2px;">
				    </td>
                </tr>
                <tr id="tblTitulo" class="TBLINI">
                    <td>&nbsp;Grabar</td>
				    <td style="text-align:right;">
                        <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',1,'divCatalogo','imgLupa1',event)"
							    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',1,'divCatalogo','imgLupa1')"
							    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgN" border="0">
				        <map name="imgN">
				            <area onclick="ot('tblNodos', 1, 0, 'num')" shape="RECT" coords="0,0,6,5">
				            <area onclick="ot('tblNodos', 1, 1, 'num')" shape="RECT" coords="0,6,6,11">
			            </map>Nº</td>
				    <td>&nbsp;<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgDenominacion" border="0">
				        <map name="imgDenominacion">
				            <area onclick="ot('tblNodos', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				            <area onclick="ot('tblNodos', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			            </map><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',2,'divCatalogo','imgLupa2')"
							    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',2,'divCatalogo','imgLupa2',event)"
							    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
                    <td title="Número de naturalezas marcadas para generar">Nat.</td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow:auto; width:396px; height:680px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:380px">
                <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblTotales" style="width: 380px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:top;">
            <table style="width:680px; margin-top:11px;" border="0">
                <colgroup>
                    <col style="width:440px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                </colgroup>
                <tr>
                    <td></td>
                    <td></td>
                    <td style="text-align:center;">
                        <img id="img4" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(1,3)" />
                        <img id="img5" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(0,3)" />
                    </td>
                    <td style="text-align:center;">
                        <img id="img6" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(1,4)" />
                        <img id="img7" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(0,4)" />
                    </td>
                    <td style="text-align:center;">
                        <img id="img8" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(1,5)" />
                        <img id="img9" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(0,5)" />
                    </td>
                </tr>
                <tr id="Tr1" class="TBLINI">
				    <td style="padding-left:3px;">&nbsp;Naturalezas de producción improductivas</td>
				    <td style="text-align:center;" title="Número de meses de vigencia">Vigencia</td>
				    <td style="text-align:center;" title="Los proyectos PIG creados para estas naturalezas permiten o no ser replicados">Replica</td>
                    <td style="text-align:center;" title="A los proyectos se les asigna por defecto los profesionales del C.R.">Hereda</td>
                    <td style="text-align:center;" title="Permite imputar gastos de viaje contra el proyecto">Imputable</td>
                </tr>
            </table>
            <div id="divNatMant" style="overflow:auto; width: 696px; height:300px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:680px">
                <%=strTablaHTML2%>
                </div>
            </div>
            <table id="TABLE3" style="width:680px;" >
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table style="width:680px; margin-top:10px;" border="0">
                <colgroup><col style="width:150px;"/><col style="width:90px;"/><col style="width:410px;"/><col style="width:30px;"/></colgroup>
                <tr>
                    <td><img class="ICO" src="../../../Images/imgIconoEmpresarial.gif" />&nbsp;Plantilla empresarial</td>
                    <td title="Selección de responsable de proyecto para su asignación.">
                        <label id="lblResp" class="enlace" onClick="getResponsable()">Responsable</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResponsable" runat="server" Text="" Width="400px" readonly="true" />
                        <input type="hidden" id="hdnIdResponsable" value=""/>
                    </td>
                    <td>
                        <img id="imgResponsable" src='../../../Images/imgAutomatico.gif' border='0' title="Asigna a las naturalezas seleccionadas el responsable de los proyectos a crear" 
                            onclick="setResponsable()" style="cursor:pointer; vertical-align:middle; width:28px; height:26px;" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:bottom;">
                        <img id="img1" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarNaturalezas(1)" />
                        <img id="img2" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarNaturalezas(0)" />
                    </td>
                    <td title="Selección de validador GASVI">
                        <label id="lblValid" class="enlace" onClick="getValidador()">Validador</label>
                        <img id="Img3" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delValidador()" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:5px;">
                    </td>
                    <td>
                        <asp:TextBox ID="txtValidador" runat="server" Text="" Width="400px" readonly="true" />
                        <input type="hidden" id="hdnIdValidador" value=""/>
                    </td>
                    <td>
                        <img id="imgValidador" src='../../../Images/imgAutomatico.gif' border='0' title="Asigna a las naturalezas seleccionadas el validador GASVI de los proyectos a crear" 
                            onclick="setValidador()"  style="cursor:pointer; vertical-align:middle; width:28px; height:26px;" />
                    </td>
                </tr>
            </table>
            <table style="width:680px;">
                <colgroup>
                    <col style="width:30px;" />
                    <col style="width:270px;" />
				    <col style="width:60px;" />
				    <col style="width:60px;" />
				    <col style="width:100px;" />
				    <col style="width:60px;" />
				    <col style="width:100px;" />
                </colgroup>
                <tr id="tblTituloNat" class="TBLINI">
                    <td title="Grabar">Gra.</td>
				    <td style="padding-left:3px;">Naturalezas de producción improductivas</td>
				    <td title="Los proyectos PIG creados para estas naturalezas permiten o no ser replicados" style="text-align:center;">Replica</td>
				    <td title="A los proyectos se les asigna por defecto los profesionales del C.R." style="text-align:center;">Hereda</td>
				    <td title="Responsable del proyecto">Responsable</td>
				    <td title="Permite anotar gastos de viaje contra el proyecto" style="text-align:left;">Imputable</td>
				    <td title="Validador GASVI">Validador</td>
                </tr>
            </table>
            <div id="divCatalogoNat" style="overflow:auto; width:696px; height:280px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:680px">
                    <table id='tblNaturalezas' class='texto MANO' style='width:680px;' mantenimiento='1' border="0">
                    <colgroup>
                        <col style='width:30px;' />
                        <col style='width:250px;' />
                        <col style='width:20px;' /><!-- img plantilla -->
				        <col style="width:60px;" />
				        <col style="width:60px;" />
				        <col style="width:100px;" />
				        <col style="width:60px;" />
				        <col style="width:100px;" />
                    </colgroup>
            
                    </table>
                </div>
            </div>
            <table id="tblTotalesNat" style="width: 680px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td></td>
        <td style="padding-top:3px;"></td>
    </tr>
    </table>
    <table width="500px" style="margin-top:15px;">
		<tr>
			<td align="center">
				<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
				</button>	
			</td>
			<td align="center">
				<button id="btnGrabarSalir" type="button" onclick="grabarsalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
				</button>	
			</td>						
			<td align="center">
				<button id="btnBorrar" type="button" onclick="borrarParam()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgEliminar.gif" /><span title="Elimina la parametrización">Eliminar</span>
				</button>	
			</td>
			<td align="center">
				<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
				</button>	 
			</td>
		</tr>
    </table>
</center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnArrayNat" id="hdnArrayNat" value="" runat="server" />
    <input type="hidden" name="hdnArrayNN" id="hdnArrayNN" value="" runat="server" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    </form>
</body>
</html>
