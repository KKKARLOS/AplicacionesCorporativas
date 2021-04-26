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
<table style="width:1000px;text-align:left;" cellpadding="4">
        <colgroup>
            <col style="width:475px;" />
            <col style="width:50px;" />
            <col style="width:475px;" />
        </colgroup>
		<tr>
			<td style="vertical-align:top;">
				<table style="width:475px;" border="0">
                    <colgroup><col style="width:415px;" /><col style="width:60px;" /></colgroup>
				    <tr style="height:45px">
					    <td colspan="2">
						    <fieldset id="fldEstructura" style="height: 35px; width:450px" runat="server">
							    <legend class="Tooltip" title="Estado de la estructura">&nbsp;Estado de la estructura&nbsp;</legend>
                                <label title='Proyecto económico'>Proyecto&nbsp;</label>
                                <asp:DropDownList id="cboProyecto" runat="server" Width="100px" onChange="">
                                <asp:ListItem Value="" Text="" selected="True"></asp:ListItem>
                                <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
                                <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                                <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                                <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;Proyecto técnico&nbsp;
                                <asp:DropDownList ID="cboProyTec" runat="server" style="width:70px;" onchange="">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                </asp:DropDownList>    
                                &nbsp;&nbsp;Tarea&nbsp;
                                <asp:DropDownList ID="cboTarea" runat="server" onchange="" style="width:80px">
								    <asp:ListItem Value="" Text=""></asp:ListItem>
								    <asp:ListItem Value="0" Text="Paralizada"></asp:ListItem>
								    <asp:ListItem Value="1" Text="Activa"></asp:ListItem>
								    <asp:ListItem Value="2" Text="Pendiente"></asp:ListItem>
								    <asp:ListItem Value="3" Text="Finalizada"></asp:ListItem>
								    <asp:ListItem Value="4" Text="Cerrada"></asp:ListItem>
								    <asp:ListItem Value="5" Text="Anulada"></asp:ListItem>
                                </asp:DropDownList>                                                  
						    </fieldset>	
					    </td>				
				    </tr>				
				    <tr style="height:35px">
					    <td colspan="2">
						    <fieldset id="fldAmbito" style="height: 35px; width:450px" runat="server">
							    <legend title="Ámbito de selección de profesionales">&nbsp;Selección de profesionales&nbsp;</legend>
							    <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinID="rbl" style="width:450px;" onclick="seleccionAmbito(this.id)">
							        <asp:ListItem Value="A" Selected="True">Nombre</asp:ListItem>
							        <asp:ListItem Value="G">Grupo funcional</asp:ListItem>
							        <asp:ListItem Value="V">Ambito de visión</asp:ListItem>
							        <asp:ListItem Value="P">Proyecto</asp:ListItem>
							    </asp:RadioButtonList> 
						    </fieldset>	
						    <br />  				
					    </td>				
				    </tr>
				    <tr style="height:35px">
					    <td colspan="2">
						    <span id="ambAp" style="display:block" class="texto">
						        <table  style="width: 450px;" >
                                    <colgroup><col style="width:120px;" /><col style="width:120px;" /><col style="width:210px;" /></colgroup>   	
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
						    <span id="ambGF" style="display:none" class="texto">
						        <label id="lblGF" class="enlace" style="width:94px;height:17px" onclick="obtenerGF()">Grupo funcional</label> 
						        <asp:TextBox ID="txtGF" runat="server" style="width:320px;" readonly="true" />&nbsp;&nbsp;
						        <img id="gomaGFun" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarGF()" runat="server" style="cursor:pointer;vertical-align:top;">
						    </span>
						    <span id="ambVI" style="display:none;vertical-align:middle;" class="texto">
						        <table style="width:280px; margin-left:190px;">
						            <colgroup><col style="width:90px;"/><col style="width:190px;"/></colgroup>
						            <tr>
						                <td>
						                    <label id="lblVI" style="width:90px" title="Ambito de visión del profesional">Ambito de visión:</label>
						                </td>
						                <td>
							                <asp:radiobuttonlist id="rdlEstado" onclick="javascript:AmbitoVision();" runat="server" SkinID="rbl" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" >
								                <asp:ListItem Value="A" Selected="True">&nbsp;Activos&nbsp;</asp:ListItem>
								                <asp:ListItem Value="B">&nbsp;Baja&nbsp;</asp:ListItem>
								                <asp:ListItem Value="T">&nbsp;Todos&nbsp;</asp:ListItem>
							                </asp:radiobuttonlist>
						                </td>
						            </tr>
						        </table>
						    </span>	
					        <span id="ambProy" style="display:none" class="texto">
						        <label id="lblProyecto" runat="server" class="enlace" onclick="getPSN()" style="width:50px;">Proyecto</label>
                                <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
						        <asp:TextBox ID="txtNumPE" style="width:60px;" Text="" MaxLength="6" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscar();}else{vtn2(event);}" />
						        <asp:TextBox ID="txtDesProy" style="width:300px;" Text="" readonly="true" runat="server" />
					        </span>				
					    </td>				
				    </tr>
                    <tr style="height:20px">
                        <td style="vertical-align:bottom;">
                            <div id="divBajas" style="width:417px;float:left;">
                                <input id="chkBajas" hidefocus class="check" type="checkbox" runat="server" onclick="javascript:mostrarRelacion();">&nbsp;Mostrar bajas
                            </div>
                        </td>
                        <td>
                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />
                            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                        </td>				
				    </tr>					
				</table>
			</td>
			<td></td>
			<td>
				<table style="width: 475px;" >
				<tr style="height:100px">
					<td>	
                        <fieldset id="fldRtdo" style="height: 100px; width:290px;text-align:left" runat="server">
	                        <legend title="Resultado">&nbsp;Resultado&nbsp;</legend>
                                <table cellspacing='3' cellpadding='0'>
                                    <tr>
	                                    <td style="width:150px">
	                                        <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
	                                    </td>
	                                    <td style="width:130px; vertical-align:top; text-align:center;">
                                            <fieldset id="FIELDSET2" style="height:30px;width:50px; margin-left:10px;" runat="server"> 
                                            <legend class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</legend>
				                            <img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer; margin-left:5px;" title="Excel" />
                                            </fieldset><br /><br />   							
                                            <button id="btnObtener" type="button" onclick="obtenerDatosExcel();"  class="btnH25W95" hidefocus=hidefocus onmouseover="mostrarCursor(this)" runat="server">
                                                <span><img src="../../../../Images/imgObtener.gif" />&nbsp;Obtener</span>
                                            </button>                
	                                    </td>
	                                </tr> 
                                </table> 						
                        </fieldset>	    									
					</td>	
					<td style="vertical-align:top; text-align:right;">
                        <br /><br /><br /><br /><br /><br /><br />
				        <br /><br /><br /><br /><br /><br />
				        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    									        
					</td>				
				</tr>				
				</table>				        				        				
			</td>
		</tr>	
        <tr>
            <td><!-- Relación de Items -->
                <table id="tblCatIni" style="width: 450px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                        Profesional
  		                <img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../../Images/imgLupa.gif" width="20" />
		                <img style="display: none; cursor: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
					    </td>                    
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 466px; height:360px" runat="server" onscroll="scrollTabla()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:450px">
                    <table id="tblDatos" style="width: 450px;">
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
                <asp:Image id="imgPapelera" style="cursor: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
            </td>
            <td><!-- Items asignados -->
                <table id="tblAsignados" style="width: 450px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            Profesionales seleccionados
  		                    <img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo','imgLupa2',event)" height="11" src="../../../../Images/imgLupa.gif" width="20" />
		                    <img style="display: none; cursor: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
						</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 466px; height:360px" target="true" onmouseover="setTarget(this);" caso="2" onscroll="scrollProfAsig()">
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
    
    <table  class="texto" style="WIDTH: 450px;" cellSpacing="0" border="0">
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
            </td>
        </tr>                    
    </table>    
      
    <input type="hidden" runat="server" name="hdnPSN" id="hdnPSN" value="" />
    <input type="hidden" runat="server" name="hdnEstadoProy" id="hdnEstadoProy" value="" />
    <input type="hidden" runat="server" name="hdnEsSoloRtpt" id="hdnEsSoloRtpt" value="N" />

<div class="clsDragWindow" id="DW" noWrap></div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){			
			}
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) theform.submit();

	}
</script>
</asp:Content>

