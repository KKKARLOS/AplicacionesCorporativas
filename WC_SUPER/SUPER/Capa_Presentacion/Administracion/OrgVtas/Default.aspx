<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administradores" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	<!--
	-->
	</script>
<center>
<table style="width:1050px;text-align:left" cellpadding="4">
		<TR>
			<TD width="46%" style="vertical-align:top;">
				<table style="WIDTH: 100%;" >
                <tr style="height:25px">
                    <td width="47%" style="padding-right:18px; vertical-align:bottom; text-align:right;">
                        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                    </td>				
				</tr>					
				</table>
			</TD>
			<td width="4%" align="center"></td>
			<td width="46%">
				<table style="WIDTH: 100%;" >
                <tr style="height:25px">
					<td width="46%" style="padding-right:18px; vertical-align:bottom; text-align:right;">
				        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />  									        
					</td>				
				</tr>				
				</table>				        				        				
			</td>
			<td width="4%"></td>
		</TR>	
        <tr>
            <td width="47%"><!-- Relación de Items -->
                <TABLE id="tblCatIni" style="width: 450px; height: 17px">
                    <colgroup>
                        <col style='width:70px;' />
                        <col style='width:380px;' />
                    </colgroup>
                    <tr class="TBLINI">
                        <td title="Código">
                            &nbsp;C.
                            <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
    					    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
                        </td>
                        <td>
                            Organizaciones de venta en SAP
		                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11px" src="../../../Images/imgLupaMas.gif" width="20px" tipolupa="2" />                      
  		                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11px" src="../../../Images/imgLupa.gif" width="20px" />
					    </td>                    
                    </tr>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; width: 466px; height:460px" runat="server">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px;">
                     <%=strTablaOrgVtasSAP%>
                    </div>
                </DIV>
                <TABLE style="width: 450px; height: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>              
            </td>
            <td width="2%" align="left">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
            </td>
            <td width="46%"><!-- Items asignados -->
                <TABLE id="tblAsignados" style="width: 450px; height: 17px">
                    <colgroup>
                        <col style='width:15px;' />
                        <col style='width:70px;' />
                        <col style='width:365px;' />
                    </colgroup>
                    <tr class="TBLINI">
                        <td></td>
                        <td title="Código">
                            &nbsp;C.
                            <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo2','imgLupa3')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
    					    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo2','imgLupa3',event)" height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
                        </td>
                        <td>
                            Organizaciones de venta visibles en SUPER
		                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa4" onclick="buscarSiguiente('tblDatos2',2,'divCatalogo2','imgLupa4')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
  		                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',2,'divCatalogo2','imgLupa4',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
						</td>			
                    </tr>
                </TABLE>
                <DIV id="divCatalogo2" style="overflow: auto; overflow-x:hidden; width: 466px; height:460px" target="true" onmouseover="setTarget(this);" caso="2">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px">
                     <%=strTablaOrgVtasSuper%>
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
		if (bEnviar) theform.submit();
	}
-->
</script>
</asp:Content>

