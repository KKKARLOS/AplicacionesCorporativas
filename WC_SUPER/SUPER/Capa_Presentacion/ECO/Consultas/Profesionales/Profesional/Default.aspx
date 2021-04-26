<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ConsultaprofesionalPGE" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sNodo = "<%=sNodo %>";
	</script>
<table class="texto" style="margin-left:10px; width:980px;">
    <colgroup><col style="width:470px;"/><col style="width:40px;"/><col style="width:470px;"/></colgroup>
	<tr>
		<td style="vertical-align:top;">
			<table class="texto" style="WIDTH:450px;" >
			    <colgroup><col style="width:225px;"/><col style="width:225px;"/></colgroup>
			    <tr style="height:65px;">
				    <td>
					    <FIELDSET id="fldAmbito" class="fld" style="height:55px; width:200px;" runat="server">
						    <LEGEND class="Tooltip" title="Ámbito de selección de profesionales">&nbsp;Selección de profesionales&nbsp;</LEGEND>
						        <br />
						        <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinId="rbl" onclick="seleccionAmbito(this.id)">
						        </asp:RadioButtonList> 
					    </FIELDSET>	
				    </td>
				    <td style="vertical-align:top;">	
                        <FIELDSET style="width: 140px; height:57px; vertical-align:top; margin-left:3px;">
                            <LEGEND><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></LEGEND>
                            <table style="width:135px;" cellpadding="3px" >
                                <colgroup><col style="width:35px;"/><col style="width:100px;"/></colgroup>
                                <tr>
                                    <td>Inicio</td>
                                    <td>
                                        <asp:TextBox ID="txtDesde" style="width:90px;" Text="" readonly="true" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fin</td>
                                    <td>
                                        <asp:TextBox ID="txtHasta" style="width:90px;" Text="" readonly="true" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </FIELDSET>
				    </td>				
			    </tr>
			    <tr style="height:35px;">
				    <td colspan="2">
					    <span id="ambAp" style="display:block" class="texto">
					        <table class="texto" style="width:390px;">
					            <colgroup><col style='width:130px;'/><col style='width:130px;' /><col style='width:130px;' /></colgroup>
						        <tr>
						            <td>&nbsp;Apellido1</td>
						            <td>&nbsp;Apellido2</td>
						            <td>&nbsp;Nombre</td>
						        </tr>
						        <tr>
						            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:115px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
						            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
						            <td>
						                <asp:TextBox ID="txtNombre" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" />						                
						            </td>
						        </tr>
					        </table>
					    </span>
					    <span id="ambGF" style="display:none" class="texto">
                            <label id="lblNodo" runat="server" class="texto" style="width:50px;">Nodo</label>
                            <asp:TextBox ID="hdnIdNodo" style="width:1px;visibility:hidden;" Text="" runat="server" />
                            <asp:DropDownList id="cboCR" runat="server" Width="380px" onChange="sValorNodo=this.value;mostrarRelacionTecnicosCR()" AppendDataBoundItems=true>
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtDesNodo" style="width:382px;" Text="" readonly="true" runat="server" />
					    </span>
					    <span id="ambVI" style="display:none" class="texto">
						    <label id="lblProyecto" runat="server" class="enlace" onclick="getPSN()" style="width:50px;">Proyecto</label>&nbsp;&nbsp;
						    <asp:TextBox id="txtNumPE" runat="server" SkinID="Numero" MaxLength="6" Width="50px" readonly="true"></asp:TextBox>
						    <asp:TextBox ID="txtDesProy" style="width:327px;" Text="" readonly="true" runat="server" />
					    </span>				
				    </td>				
			    </tr>
                <tr style="height:30px;">
                    <td>
                        <INPUT id="chkBajas" hidefocus class="check" type="checkbox" runat="server" onclick="javascript:mostrarRelacion();">&nbsp;Mostrar bajas
                    </td>
                    <td style="vertical-align:bottom; text-align:right;">
                        <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                    </td>				
			    </tr>					
			</table>
		</td>
		<td></td>
		<td>
			<table class="texto" style="WIDTH:450px;" >
			    <tr style="height:100px;">
				    <td style="padding-right:18px; text-align:right;">	
                        <FIELDSET id="fldRtdo" class="fld" style="height:100px; width:290px; text-align:left" runat="server">
                            <LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
                                <table class='texto' border='0' cellspacing='3' cellpadding='0'>
                                    <tr>
                                        <td style="width:150px">
                                            <img id="imgImpresora" src="../../../../../Images/imgImpresorastop.gif" />
                                        </td>
                                        <td style="width:130px; vertical-align:top; text-align:center;">
                                            <FIELDSET id="FIELDSET2" class="fld" style="height:30px; width:50px; text-align:left; margin-left:20px;" runat="server"> 
                                            <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
			                                    <img src="../../../../../Images/botones/imgExcel.gif" style="cursor:pointer; margin-left:20px;" title="Excel" />
                                            </FIELDSET>
                                            <br />   							
                                            <button id="btnObtener" type="button" onclick="obtenerDatosExcel()" class="btnH25W90" style="margin-top:5px; margin-left:10px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                <img src="../../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                                            </button>    
                                        </td>
                                    </tr> 
                                </table> 						
                        </FIELDSET>	    									
				    </td>
		        </tr>
                <tr style="height:20px;">
                    <td style="text-align:right; vertical-align:bottom;">
			            <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />  									        
				    </td>				
			    </tr>				
			</table>				        
		</td>
	</tr>	
    <tr>
        <td>
            <TABLE id="tblCatIni" style="WIDTH: 450px; HEIGHT: 17px">
                <TR class="TBLINI">
                    <td style="padding-left:3px">
                        Profesional
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
				    </td>                    
                </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 466px; height:380px" runat="server" onscroll="scrollTabla()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:450px;">
                <TABLE id="tblDatos" style="WIDTH: 450px;" class="texto">
                    <colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 450px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>              
        </td>
        <td style="vertical-align:middle; text-align:center;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" caso="4" onmouseover="setTarget(this);"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <TABLE id="tblAsignados" style="WIDTH:450px; HEIGHT:17px;" >
                <TR class="TBLINI">
                    <td style="padding-left:3px">
                        Profesionales seleccionados
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo','imgLupa2')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo','imgLupa2', event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
					</TD>
                </TR>
            </TABLE>
            <DIV id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 466px; height:380px" target="true" caso="2" onmouseover="setTarget(this);" onscroll="scrollProfAsig()">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px;">
                <TABLE id="tblDatos2" style="WIDTH: 450px;" class="texto MM">
                    <colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 450px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
    </tr>
</table>
<table class="texto" style="width:450px; margin-left:10px; margin-top:5px;">
    <tr>
        <td>
            <img class="ICO" src="../../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
            <img id="imgForaneo" class="ICO" src="../../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>                    
</table>      
<div class="clsDragWindow" id="DW" noWrap></div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" runat="server" name="hdnPSN" id="hdnPSN" value="" />
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
			switch (strBoton){			
//				case "excel": 
//				{
//					bEnviar = false;
//					obtenerDatosExcel();
//					break;
//				}
			}
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) theform.submit();

	}
-->
</script>
</asp:Content>

