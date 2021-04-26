<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Columnas_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<table id="tblDatos" border=1 style=" width:300px;">
    <colgroup>
        <col style="background-color:Red; width:33%;" />
        <col style="background-color:Navy; width:34%;" />
        <col style="background-color:Red; width:33%;" />
    </colgroup>
    <tr style="height:20px;">
        <td>&nbsp;00000</td>
        <td>&nbsp;11111</td>
        <td>&nbsp;22222</td>
    </tr>
    <tr>
        <td>&nbsp;33333</td>
        <td>&nbsp;44444</td>
        <td>&nbsp;55555</td>
    </tr>
        <tr>
        <td>&nbsp;Flaño</td>
        <td>&nbsp;Muñoz</td>
        <td>&nbsp;Pérez</td>
    </tr>
</table>

<br />
<center>	
	<table id="general" style="width:420px;">
		<tr>
			<td>
				<table style="text-align:left; width:410px;">
					<tr>
					    <td>&nbsp;Apellido1</td>
					    <td>&nbsp;Apellido2</td>
					    <td>&nbsp;Nombre</td>
					</tr>
					<tr>
					    <td><input name="txtApellido1" type="text" maxlength="50" id="txtApellido1" class="txtM" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" style="width:122px" /></td>
					    <td><input name="txtApellido2" type="text" maxlength="50" id="txtApellido2" class="txtM" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" style="width:122px" /></td>
					    <td><input name="txtNombre" type="text" maxlength="50" id="txtNombre" class="txtM" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" style="width:122px" /></td>
					</tr>
				</table>	
			</td>
		</tr>
		<tr>
			<td>
				<table id="tblTitulo" style="width:400px;height:17px; margin-top:5px;">
					<tr class="TBLINI">
						<td align="left">&nbsp;Profesionales
							<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" tipolupa="2" width="20">
							<img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" tipolupa="1" width="20">
					    </td>
					</tr>
				</table>				
	            	            
                <div id="divCatalogo" style="OVERFLOW: auto; overflow-x:hidden; width: 416px; height:460px;" onscroll="scrollTablaProf()">
                    <div style="width:400px;background-image:url('../../../Images/imgFT20.gif');">
                        <table id="Table1" style="width:400px;" class="MA">
                            <colgroup><col style="width:20px;" /><col style="width:380px;" /></colgroup>
                        </table>
                    </div>
                </div>
			</td>
		</tr>
		<tr>
			<td>				
                <table style="width:400px;height:17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
			</td>
		</tr>			
        <tr>
            <td style="padding-top: 5px;">
                &nbsp;<img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
            </td>
        </tr>
	</table>
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
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

