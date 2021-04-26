<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="USAs" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	<!--
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
	-->
	</script>
<center>
<table align="center" width="100%" style="text-align:left" cellpadding="4" >
	<TR>
		<TD width="47%" style="vertical-align:top;">
			<table style="WIDTH: 100%;" >
			<tr style="height:35px">
				<td>
					<table style="width:390px;" >
                        <colgroup><col style="width:130px" /><col style="width:130px" /><col style="width:130px" /></colgroup>
						<tr>
						<td>&nbsp;Apellido1</td>
						<td>&nbsp;Apellido2</td>
						<td>&nbsp;Nombre</td>
						</tr>
						<tr>
						<td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
						<td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
						<td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" />							
						</td>
						</tr>
					</table>
				</td>				
			</tr>
            <tr style="height:20px">
                <td width="47%" style="padding-right:19px; vertical-align:bottom; text-align:right;">
                    <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                </td>				
			</tr>					
			</table>
		</TD>
		<td width="5%"></td>
		<td width="48%">
			<table style="WIDTH: 100%;" >
            <tr height="55px">
				<td width="46%" style="padding-right:30px; vertical-align:bottom; text-align:right;">
			        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />  									        
				</td>				
			</tr>				
			</table>				        				        				
		</td>
	</TR>	
    <tr>
        <td width="47%"><!-- Relación de Items -->
            <TABLE id="tblCatIni" style="width: 450px; height: 17px">
                <TR class="TBLINI">
                    <td style="padding-left:3px">
                    Profesional
	                <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
	                <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
				    </TD>                    
                </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW: auto; width: 466px; height:440px" runat="server" onscroll="scrollTabla()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:450px;">
                <TABLE id="tblDatos" style="WIDTH: 450px;" class="texto MAM">
                    <colgroup><col style="width:20px;" /><col style="width:430px;" /></colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="width: 450px; height: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>              
        </td>
        <td width="5%" style="vertical-align:middle; text-align:left;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:5px;" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
        </td>
        <td width="48%"><!-- Items asignados -->
            <TABLE id="tblAsignados" style="width: 450px; height: 17px">
                <TR class="TBLINI">
                    <td style="padding-left:3px;">
                        Profesionales seleccionados
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',2,'divCatalogo2','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',2,'divCatalogo2','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
					</TD>
                </TR>
            </TABLE>
            <DIV id="divCatalogo2" style="OVERFLOW: auto; width: 466px; height:440px" target="true" onmouseover="setTarget(this);" caso="2">
                <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif); width:450px">
                 <%=strTablaAdmin%>
                </div>
            </DIV>
            <TABLE style="width: 450px; height: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
    </tr>
</table>
</center>
<TABLE  style="WIDTH: 450px; margin-top:5px;">
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;<img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
        </td>
    </tr>                    
</TABLE>      
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />

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
-->
</script>
</asp:Content>

