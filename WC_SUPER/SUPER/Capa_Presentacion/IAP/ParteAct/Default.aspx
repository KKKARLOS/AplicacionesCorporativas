<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_ParteAct_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var sOrigen = "<%=sOrigen%>";
    //SSRS
    var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
    //SSRS
</script>
<style>
#tblDatos2 TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<table style=" margin-left:15px;" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
    <td height="6" background="../../../Images/Tabla/8.gif"></td>
    <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
  </tr>
  <tr>
    <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
    <td background="../../../Images/Tabla/5.gif" style="padding:5px;">
    <!-- Inicio del contenido propio de la página -->
        <table style="width:930px;">
        <colgroup>
            <col style="width:310px;" />
            <col style="width:310px;" />
            <col style="width:155px;" />
            <col style="width:155px;" />
        </colgroup>
        <tr>
			<td>	
                <FIELDSET style="width: 290px; height:55px; padding:5px;">
                    <LEGEND><label id="lblProfesional" class="texto" runat="server">Profesional</label></LEGEND>
                    <DIV id="divProfesional" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                         <table id="tblProfesional" style="width:260px;">
                         <%=strHTMLProfesionales%>
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>			    
			</td>
            <td>
                <FIELDSET id="fldTareas" class="fld" style="height: 55px;width:290px;text-align:left" runat="server"> 
                        <LEGEND class="Tooltip" title="Tareas">&nbsp;Tareas&nbsp;</LEGEND>
                        <br />   
                        <table class='texto' style='margin-top:5px;margin-left:3px;' align='center' border='0' cellspacing='0' cellpadding='0'>
			                <tr><td align="center">  
                            <INPUT hideFocus id="chkFacturable" class="check" checked type=checkbox runat="server" onclick="if (!this.checked)$I('chkNoFacturable').checked=true;Borrar();" />&nbsp;&nbsp;Facturables
                            <INPUT hideFocus id="chkNoFacturable" class="check" checked type=checkbox runat="server" onclick="if (!this.checked)$I('chkFacturable').checked=true;Borrar();" style="margin-left:30px;" />&nbsp;&nbsp;No facturables                           
			                </td></tr> 
	                    </table> 
	           </FIELDSET>	 
			</td>
            <td>
        	     <FIELDSET id="fldPeriodo" class="fld" style="height: 55px; width:140px; padding-left:15px;" runat="server">
		         <LEGEND class="Tooltip" title="Periodo temporal">&nbsp;Periodo&nbsp;</LEGEND>
                    <table style="margin-left:5px; width:110px;" cellpadding="2px">
                        <colgroup><col style="width:40px;"/><col style="width:70px;"/></colgroup>
                        <tr>
                            <td>Desde</td>
                            <td>
                                <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('D');" goma=0 />
                            </td>
                        </tr>
                        <tr>
                            <td>Hasta</td>
                            <td>
                                <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('H');" goma=0 />
                            </td>
                        </tr>
                    </table>
		        </FIELDSET>	                
            </td>
            <td align="center">
				<button id="btnObtener" type="button" onclick="buscar();" class="btnH25W85" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/imgObtener.gif" /><span>Obtener</span>
				</button>						                 
            </td>
        </tr>
        <tr>
            <td>
                <FIELDSET style="width: 290px; height:55px; padding:5px;">
                    <LEGEND><label id="Label10" class="enlace" onclick="getProyectos()" runat="server">Proyecto</label><img id="Img15" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="BorrarFilasDe('tblProyecto');setFilaTodos('tblProyecto', true, true);" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                         <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td>
                <FIELDSET style="width: 290px; height:55px; padding:5px;">
                    <LEGEND><label id="Label7" class="enlace" onclick="getClientes()" runat="server">Cliente</label><img id="Img5" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="BorrarFilasDe('tblCliente');setFilaTodos('tblCliente', true, true);" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                         <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td colspan="2">
                <FIELDSET id="fldOrden" runat="server" style="width: 290px; height:55px; padding:5px;">
				<LEGEND class="Tooltip" title="Ordenación">&nbsp;Modelo de parte&nbsp;</LEGEND>
				<table class='texto' style="width:275px; table-layout:fixed;margin-top:7px;margin-left:3px;" border='0' cellspacing='0' cellpadding='0'>
				<tr>
				    <td style="width:170px" align="center">        					    			   
					<asp:radiobuttonlist id="rdbModelo" runat="server" style="width:150px" SkinID="rbl" RepeatLayout="Table" CellSpacing="0" CellPadding="0"  RepeatDirection="horizontal">
						<asp:ListItem Value="1" Selected="True"><img src="../../../Images/imgModeloDia.gif" style="cursor:pointer; margin-right:25px;" title="Un impreso por cliente, profesional y fecha" onclick="seleccionar($I('rdbModelo_0'))" /></asp:ListItem>
						<asp:ListItem Value="2"><img src="../../../Images/imgModeloPeriodo.gif" style="cursor:pointer" title="Un impreso por cliente y profesional" onclick="seleccionar($I('rdbModelo_1'))" /></asp:ListItem>
					</asp:radiobuttonlist>
					</td>
                    <td align="center">
						<button id="btnPDF" type="button" onclick="Exportar();" class="btnH25W85" runat="server" hidefocus="hidefocus" 
							 onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../images/botones/imgPDF.gif" /><span>Exportar</span>
						</button>			         
                    </td>
				</tr> 
				</table> 						
				</FIELDSET>	   
            </td>
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
<img src="../../../images/botones/imgmarcar.gif" onclick="mTabla()" title="Marca todas las imputaciones" style="cursor:pointer; margin-left:11px; margin-top:3px;" />
<img src="../../../images/botones/imgdesmarcar.gif" onclick="dTabla()" title="Desmarca todas las imputaciones" style="cursor:pointer;margin-top:3px;" />   
<TABLE class="texto" id="Table1" style="width:960px; margin-left:5px;" cellSpacing="0" cellPadding="5" border="0">
	<TR>
		<TD>
			<TABLE id="tblTitulo" style="height:17px;width:960px;">
			    <colgroup>
                    <col style='width:20px;' />
                    <col style='width:270px;' />
                    <col style='width:240px;' />
                    <col style='width:65px;' />
                    <col style='width:35px;' />
                    <col style='width:200px;' />
                    <col style='width:30px;' />
                    <col style='width:100px;' />
			    </colgroup>
				<tr class="TBLINI">
				    <td style='padding-left:2px;'></td>
					    <td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img2" border="0">
						        <MAP name="img2">
						            <AREA onclick="ot('tblDatos', 1, 0, '', '')" shape="RECT" coords="0,0,6,5">
						            <AREA onclick="ot('tblDatos', 1, 1, '', '')" shape="RECT" coords="0,6,6,11">
					            </MAP>&nbsp;Tarea&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							    height="11px" src="../../../Images/imgLupaMas.gif" width="20px" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							    height="11px" src="../../../Images/imgLupa.gif" width="20px" tipolupa="1">
					</td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img3" border="0">
						    <MAP name="img3">
						        <AREA onclick="ot('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Profesional&nbsp;<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa3')"
							height="11px" src="../../../Images/imgLupaMas.gif" width="20px" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa3',event)"
							height="11px" src="../../../Images/imgLupa.gif" width="20px" tipolupa="1">
					</td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img4" border="0">
						    <MAP name="img4">
						        <AREA onclick="ot('tblDatos', 3, 0, 'fec', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 3, 1, 'fec', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Fecha
					</td>
					<td style='text-align:right;'>Horas</td>
					<td style='padding-left:5px;'><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img6" border="0">
						    <MAP name="img6">
						        <AREA onclick="ot('tblDatos', 5, 0, '', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 5, 1, '', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Cliente&nbsp;<IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa4')"
							height="11px" src="../../../Images/imgLupaMas.gif" width="20px" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa4',event)"
							height="11px" src="../../../Images/imgLupa.gif" width="20px" tipolupa="1"> 
					</td>
					<td title="Facturable" style="text-align:center;">F.</td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img7" border="0">
						    <MAP id="img7">
						        <AREA onclick="ot('tblDatos', 7, 0, '', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 7, 1, '', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Modo fact.&nbsp;<IMG id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa5')"
							height="11px" src="../../../Images/imgLupaMas.gif" width="20px" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa5',event)"
							height="11px" src="../../../Images/imgLupa.gif" width="20px" tipolupa="1">
					</td>
				</tr>
			</TABLE>
			<div id="divCatalogo" style="OVERFLOW: auto; width: 976px; HEIGHT: 380px;">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 960px;">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <TABLE id="tblResultado" style="height:17px;width:960px" align="left">
				<TR class="TBLFIN">
					<TD>&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
    </TR>
</TABLE>
<asp:textbox id="FORMATO" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnConsumos" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnOrigen" runat="server" style="visibility:hidden"></asp:textbox>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "guia": 
				{
				    bEnviar = false;
				    setTimeout("mostrarGuia('ConsultaFacturabilidad.pdf');", 20);
					break;
				}
			}
		}

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
-->
</script>
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</asp:Content>

