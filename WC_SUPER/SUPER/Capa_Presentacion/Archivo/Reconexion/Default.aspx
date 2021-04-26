<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reconexion" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
<br />
<center>	
	<table id="general" style="width:420px;text-align:left">
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
				<table id="tblTitulo" style="width:400px; height:17px; margin-top:15px;" >
					<tr class="TBLINI">
						<td align="left">&nbsp;Profesionales
							<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" tipolupa="2" width="20">
							<img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" tipolupa="1" width="20">
					    </td>
					</tr>
				</table>				
	            	            
                <div id="divCatalogo" style="overflow: auto; overflow-x:hidden; width: 416px; height:460px;" onscroll="scrollTablaProf()">
                    <div style="width:400px;background-image:url('../../../Images/imgFT20.gif');">
                        <table id="tblDatos" style="width:400px;" class="MA">
                            <colgroup><col style="width:20px;" /><col style="width:380px;" /></colgroup>
                        </table>
                    </div>
                </div>
			</td>
		</tr>
		<tr>
			<td>				
                <table style="width:400px;height:17px" class="TBLFIN">
                    <tr>
                        <td></td>
                    </tr>
                </table>
			</td>
		</tr>			
        <tr>
            <td style="padding-top: 5px;">
                &nbsp;<img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                <label id="lblForaneo" runat="server">Foráneo</label>
            </td>
        </tr>
	</table>
</center>
<asp:TextBox ID="hdnCodRed" runat="server" Text="" style="visibility:hidden"></asp:TextBox>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript" language="javascript">
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
</script>
</asp:Content>

