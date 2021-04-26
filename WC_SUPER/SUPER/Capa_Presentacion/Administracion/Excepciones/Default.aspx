<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Excepciones" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var strNodoDefCorta = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
        var strNodoDefLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";	    
	</script>
<center>
<table align="center" width="100%" style="text-align:left" cellpadding="4" >
	<tr>
		<td width="47%" style="vertical-align:top;">
			<table style="width:450px" >	
			<colgroup><col style="width:410px" /><col style="width:40px" /></colgroup>								
			<tr style="height:70px">
			    <td>
					<fieldset style="height: 35px; width:300px" runat="server">
						<legend title="Tipo de elemento">&nbsp;Tipo de elemento&nbsp;</legend>
                            <asp:RadioButtonList width="280px" ID="rdbAmb" runat="server" SkinId="rbl" RepeatColumns="3" onclick="seleccionTipo(this.id)" style="margin-top:3px;">
                            <asp:ListItem Value="P" Text="Profesional" onclick="$I('rdbAmb_0').click();" Selected/>                            
                            <asp:ListItem Value="N" Text="C.R.&nbsp;&nbsp;&nbsp;"  onclick="$I('rdbAmb_1').click();"  />
                            <asp:ListItem Value="C" Text="Cliente" onclick="$I('rdbAmb_2').click();" />
                            </asp:RadioButtonList>
					</fieldset>				
			        <br />	
			        <span id="ambAp" style="display:block; height:35px;" class="texto">	
					    <table style="width:410px;" >
                            <colgroup><col style="width:130px" /><col style="width:130px" /><col style="width:130px" /></colgroup>
						    <tr>
						    <td>&nbsp;Apellido1</td>
						    <td>&nbsp;Apellido2</td>
						    <td>&nbsp;Nombre</td>
						    </tr>
						    <tr>
						    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
						    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
						    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
						    </tr>
					    </table>
					</span>
					<span id="ambCR" style="display:none; width:410px; height:35px;" class="texto">
					</span>						
                    <span id="ambCliente" style="display:none; width:410px; height:35px;" class="texto">	
                        <br />	
	                    <table class="texto" style="width:390px">
		                    <tr>
			                    <td style="width:240px;" valign="bottom">Cadena de búsqueda
				                    <asp:TextBox ID="txtCliente" runat="server" style="width:120px;" onkeypress="javascript:if(event.keyCode==13){bMsg = false;buscarClientes(this.value);event.keyCode=0;return false;}" />
			                    </td>
			                    <td style="width:150px;" valign="bottom">
				                    <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" style="width:149px;" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda" onclick="buscarClientes($I('txtCliente').value);">
					                    <asp:ListItem Value="I"><img src='../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus=hidefocus onclick="$I('rdbTipo_0').click();"></asp:ListItem>
					                    <asp:ListItem Selected="True" Value="C"><img src='../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus=hidefocus onclick="$I('rdbTipo_1').click();"></asp:ListItem>
				                    </asp:RadioButtonList>
			                    </td>
		                    </tr>
	                    </table>
                    </span>					
				</td>
	    	    <td style="vertical-align:bottom;" align="right"><img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" /></td>
			</tr>			
			</table>
		</td>
		<td width="5%"></td>
		<td width="48%" style="vertical-align:bottom;">
			<table style="width: 100%;" >
            <tr style="height:70px">
				<td width="46%" style="padding-right:20px; vertical-align:bottom; text-align:right;">
			        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />  									        
				</td>				
			</tr>				
			</table>				        				        				
		</td>
	</tr>	
    <tr>
        <td width="47%"><!-- Relación de Items -->
            <table id="tblCatIni" style="width: 450px; height: 17px">
                <tr class="TBLINI">
                    <td style="padding-left:3px"><label id="titulo">
                    Profesional
                    </label>
	                <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos', nColumnaA, 'divCatalogo', 'imgLupa1');" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
	                <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1bis" onclick="buscarDescripcion('tblDatos', nColumnaA, 'divCatalogo', 'imgLupa1', event);" height="11" src="../../../Images/imgLupa.gif" width="20" />
				    </td>                    
                </tr>
            </table>
            <div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 466px; height:400px" runat="server" onscroll="scrollTabla()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:450px;">
                <table id="tblDatos" style="width: 450px;" class="texto MAM">
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
        <td width="5%" style="vertical-align:middle; text-align:left;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:5px;" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
        </td>
        <td width="48%"><!-- Items asignados -->
            <table id="tblAsignados" style="width: 450px; height: 17px">
                <tr class="TBLINI">
                    <td style="padding-left:3px;"><label id="titulo2">
                        Profesionales excluidos
                        </label>
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',nColumnaB,'divCatalogo2','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2bis" onclick="buscarDescripcion('tblDatos2',nColumnaB,'divCatalogo2','imgLupa2', event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
					</td>
                </tr>
            </table>
            <div id="divCatalogo2" style="overflow-y: auto; overflow-x:hidden; width: 466px; height:400px" target="true" onmouseover="setTarget(this);" caso="2">
                <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif); width:450px">
                 <%=strTablaAdmin%>
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
<table id="pieProfe" style="width: 450px; margin-top:5px; visibility:visible">
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>                    
</table>      
<div class="clsDragWindow" id="DW" noWrap></div>
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />

</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
			switch (strBoton){			
				case "grabar": 
				{
					bEnviar = false;
					grabar();
					break;
				}
			}
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) {
		    theform.submit();
		}
	}
</script>
</asp:Content>

