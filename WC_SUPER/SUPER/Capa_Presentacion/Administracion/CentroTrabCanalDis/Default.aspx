<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administradores" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	<!--
	-->
	</script>
<center>
<table align="center" width="516px">
<TR>
	<TD align="left" width="80%">
        <TABLE  id="tblCabecera" style="width: 500px; height: 17px">
            <colgroup>
                <col style="width:280px;"/>
                <col style="width:195px;"/>
            </colgroup>
            <TR class="TBLINI">
                <td style="padding-left:20px">
                    Centro de trabajo
                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
		        </td>
		        <td style="padding-left:8px">
		        Canal de distribución
		        </td>			
            </TR>
        </TABLE>
    </TD>
</TR>  
<TR>
    <TD align="left">          
        <div id="divCatalogo" style="overflow-y: auto; overflow-x:hidden; width: 516px; height:520px">
            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
             <%=strTablaCentrosTrabajo%>
            </div>
        </div>
    </TD>
</TR>  
<TR>
	<TD align="left">            
        <TABLE style="WIDTH: 500px; HEIGHT: 17px" cellSpacing="0" border="0">
            <TR class="TBLFIN">
                <TD></TD>
            </TR>
        </TABLE>
    </TD>
</TR>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
	        switch (strBoton) {
			    case "grabar":
		        {
		            bEnviar = false;
		            mostrarProcesando();
		            setTimeout("grabar();", 20);
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

