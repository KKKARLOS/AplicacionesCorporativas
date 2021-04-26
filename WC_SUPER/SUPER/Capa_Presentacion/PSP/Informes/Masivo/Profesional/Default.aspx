<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ConsultaTecnicoMasiva" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
	</script>
	<center>
    <table style="width:1000px;text-align:left;" cellpadding="3px">
        <colgroup>
            <col style="width:475px;" />
            <col style="width:50px;" />
            <col style="width:475px;" />
        </colgroup>
		<tr>
			<td style="vertical-align:top;">
				<table style="width: 100%;" >
				    <tr style="height:45px;">
					    <td>
						    <fieldset id="fldAmbito" class="fld" style="height: 45px; width:435px" runat="server">
							    <legend class="Tooltip" title="Ámbito de selección de profesionales">&nbsp;Selección de profesionales&nbsp;</legend>
							    <asp:RadioButtonList ID="rdbAmbito" style="margin-top:5px; width:420px;" runat="server" RepeatColumns="5" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionAmbito(this.id)">
							        <asp:ListItem Value="A" Selected="True">Nombre</asp:ListItem>
							        <asp:ListItem Value="G" title="Grupo funcional">Grupo F.</asp:ListItem>
							        <asp:ListItem Value="V" title="Ámbito de visión">Á.Visión&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="P">Proveedor</asp:ListItem>
                                    <asp:ListItem Value="C" title="Centro de responsabilidad">C.R.</asp:ListItem>
							    </asp:RadioButtonList> 
						    </fieldset>	
						    <br />  <br />  				
					    </td>				
				    </tr>
				    <tr style="height:35px;">
					    <td>
						    <span id="ambAp" style="display:block" class="texto">
						    <table style="width: 450px;" >
                                <colgroup>
                                    <col style="width:120px;" />
                                    <col style="width:120px;" />
                                    <col style="width:210px;" />
                                </colgroup>   						
							    <tr>
							    <td>&nbsp;Apellido1</td>
							    <td>&nbsp;Apellido2</td>
							    <td>&nbsp;Nombre</td>
							    </tr>
							    <tr>
							    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
							    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
							    <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" />
							    </td>
							    </tr>
						    </table>
						    </span>
						    <span id="ambGF" style="display:none" class="texto"><label id="lblGF" class="enlace" style="width:94px;height:17px" onclick="obtenerGF()">Grupo funcional</label> <asp:TextBox ID="txtGF" runat="server" Width="240px" readonly="true" />&nbsp;&nbsp;<img id="gomaGFun" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarGF()" runat="server" style="cursor:pointer;vertical-align:bottom;"></span>
						    <span id="ambVI" style="display:none;vertical-align:middle;" class="texto"><label id="lblVI" style="width:100px" title="Ambito de visión del profesional">Ambito de visión</label> 
							    <asp:radiobuttonlist id="rdlEstado"  onclick="javascript:AmbitoVision();"  runat="server" Width="180px" SkinID="rbl" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" RepeatLayout="Flow">
								    <asp:ListItem Value="A">&nbsp;Activos&nbsp;</asp:ListItem>
								    <asp:ListItem Value="B">&nbsp;Baja&nbsp;</asp:ListItem>
								    <asp:ListItem Value="T">&nbsp;Todos&nbsp;</asp:ListItem>
							    </asp:radiobuttonlist>
						    </span>	
                            <span id="ambCR" style="display:none" class="texto"><label id="lblCR" class="enlace" style="width:94px;height:17px" onclick="obtenerCR()">C.R.</label> <asp:TextBox ID="txtCR" runat="server" Width="240px" readonly="true" />&nbsp;&nbsp;<img id="Img1" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarCR()" runat="server" style="cursor:pointer;vertical-align:bottom;"></span>			
                            <span id="ambProv" style="display:none" class="texto"><label id="lblProv" class="enlace" style="width:94px;height:17px" onclick="obtenerProv()">Proveedor</label><asp:TextBox ID="txtProv" runat="server" Width="240px" readonly="true" />&nbsp;&nbsp;<img id="Img2" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarProv()" runat="server" style="cursor:pointer;vertical-align:bottom;"></span>			
					    </td>				
				    </tr>
				</table>
			</td>
			<td></td>
			<td>
				<table style="width:475px;">
                <colgroup>
                    <col style="width:280px;" />
                    <col style="width:195px;" />
                </colgroup>   					
				<tr style="height:110px">
					<td style="vertical-align:top;">	
 	                        <fieldset id="fldRtdo" style="height: 110px; width:265px; text-align:left" runat="server">
		                        <legend title="Resultado">&nbsp;Resultado&nbsp;</legend>
	                                <table style="width:260px">
	                                    <colgroup>
                                            <col style="width:140px" />
                                            <col style="width:120px;" />
                                        </colgroup>   			
	                                    <tr>
		                                    <td>
		                                        <img id="imgImpresora" src="../../../../../Images/imgImpresorastop.gif" />
		                                    </td>
		                                    <td style="vertical-align:top; text-align:center;">
	                                            <fieldset id="FIELDSET2" style="height:30px; width:50px;text-align:left; margin-left:10px;" runat="server"> 
	                                            <legend title="Formato">&nbsp;Formato&nbsp;</legend>
					                            <img src="../../../../../Images/botones/imgExcel.gif" style="cursor:pointer; margin-left:20px; margin-top:2px;" title="Excel" />
                                                </fieldset><br /><br />   							
                                                <button id="btnObtener" type="button" onclick="obtenerDatosExcel();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
                                                    <span><img src="../../../../../images/imgObtener.gif" />&nbsp;Obtener</span>
                                                </button>                
		                                    </td>
		                                </tr> 
	                                </table> 						
	                        </fieldset>	    									
					</td>	
					<td style="vertical-align:top; text-align:center;">
 				        <fieldset id="fldPeriodo" style="height:50px; width:125px; margin-left:10px;" runat="server">
				            <legend title="Periodo temporal">&nbsp;Periodo&nbsp;</legend>
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
				        </fieldset>	
					</td>				
				</tr>				
				</table>				        
			</td>
		</tr>
		<tr>
            <td style="padding-right:22px; vertical-align:bottom;">
            <div id="divBajas" style="width:417px;float:left;">
                <input id="chkBajas" hidefocus class="check" type="checkbox" runat="server" onclick="javascript:mostrarRelacion();">&nbsp;Mostrar bajas
            </div>
                <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>				
		    <td></td>
            <td style="padding-right:22px; vertical-align:bottom; text-align:right;">
		        <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
		    </td>
		</tr>	
        <tr>
            <td><!-- Relación de Items -->
                <table id="tblCatIni" style="width: 450px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                        Profesional
  		                <img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
		                <img style="display: none; cursor: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
					    </td>                    
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 466px; height:380px" runat="server" onscroll="scrollTabla()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:450px">
                    <table id="tblDatos" style="width:450px;">
                        <colgroup><col style="width:20px;" /><col style="width:430px;" /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 450px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>              
            </td>
            <td style="vertical-align:middle; text-align:center;">
                <asp:Image id="imgPapelera" style="cursor: pointer" runat="server" ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
            </td>
            <td><!-- Items asignados -->
                <table id="tblAsignados" style="width: 450px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            Profesionales seleccionados
  		                    <img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo','imgLupa2',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
		                    <img style="display: none; cursor: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo','imgLupa2')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
						</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 466px; height:380px" target="true" onmouseover="setTarget(this);" caso="2" onscroll="scrollProfAsig()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px;">
                    <table id="tblDatos2" style="width: 450px;" class="texto MM" >
                        <colgroup><col style="width:20px;" /><col style="width:430px;" /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 450px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </center>
    
    <table  class="texto" style="width: 450px;" >
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
                <img id="imgForaneo" class="ICO" src="../../../../../Images/imgUsuFVM.gif" runat="server" />
                <label id="lblForaneo" runat="server">Foráneo</label>
            </td>
        </tr>                    
    </table>      


<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

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
</script>
</asp:Content>

