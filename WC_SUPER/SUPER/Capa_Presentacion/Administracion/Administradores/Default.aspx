<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administradores" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
</script>

<table cellpadding="4" style="width:990px;">
    <colgroup><col style="width:470px;"/><col style="width:50px;"/><col style="width:470px"/></colgroup>
	<tr>
		<td style="vertical-align:top;">
		    <table style="width: 450px;" >
                <colgroup><col style="width:130px"/><col style="width:130px"/><col style="width:130px"/><col style="width:60px"/></colgroup>						
			    <tr>
			        <td>&nbsp;Apellido1</td>
			        <td>&nbsp;Apellido2</td>
			        <td>&nbsp;Nombre</td>
			        <td></td>
			    </tr>
			    <tr>
			        <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
			        <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
			        <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
			        <td style="text-align:right;">
			            <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />
			            &nbsp;
			            <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
			        </td>
			    </tr>
		    </table>
		</td>
		<td></td>
		<td style="padding-right:20px; vertical-align:bottom; text-align:right;">
	        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />
	        &nbsp;
	        <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />  									        
		</td>				
	</tr>	
    <tr>
        <td><!-- Relación de Items --> 
            <table id="tblCatIni" style="width: 450px;height: 17px">
                <tr class="TBLINI">
                    <td style="padding-left:5px">Profesional
	                    <img style="display: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
	                    <img style="display: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
				    </td>                    
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; width: 466px; height:440px" runat="server" onscroll="scrollTabla()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 450px;">
                <table id="tblDatos" style="width: 450px;" class="MAM">
                    <colgroup><col style="width:20px" /><col style="width:430px" /></colgroup>
                </table>
                </div>
            </div>
            <table style="width: 450px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>              
        </td>
        <td style="vertical-align:middle; text-align:left;" >
            <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:4px;" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" caso="3" onmouseover="setTarget(this)"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <table id="tblAsignados" style="width: 450px; height: 17px">
                <colgroup>
                    <col style="width:325px;" />
                    <col style="width: 37px;" />
                    <col style="width: 43px;" />
                    <col style="width: 45px;" />
                </colgroup>
                <tr class="TBLINI">
                    <td style="padding-left:5px">Profesionales seleccionados
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',2,'divCatalogo2','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',2,'divCatalogo2','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
					</td>
					<td title="Administrador">ADM</td>
					<td title="SuperAdministrador">SADM</td>						
					<td title="Administrador de personal">ADP</td>						
                </tr>
            </table>
            <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden;  width: 466px; height:440px" target="true" caso="2" onmouseover="setTarget(this);">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 450px;">
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
<table style="width: 450px;margin-top:5px">
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
        </td>
    </tr>                    
</table>      
<div class="clsDragWindow" id="DW" noWrap></div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />

</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        setTimeout("grabar()", 100);
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }
</script>
</asp:Content>

