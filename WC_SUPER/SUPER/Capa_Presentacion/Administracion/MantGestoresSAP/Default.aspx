<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GestoresSAP" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	<!--
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";   
	-->
	</script>
<style type="text/css">
    #tblDatos tr { height:20px; }
    #tblDatos td {
	    border-collapse: separate;
        border-spacing: 0px;
	    border: 1px solid #A6C3D2;
	    padding: 0px 2px 0px 2px;
    }
</style>
<center>
    <table style="width:490px;text-align:left">
	    <tr>
	        <td>
                <table id="tblAsignados" style="width: 480px; height: 17px; margin-top:25px">
                    <colgroup>
                        <col style='width:50px' />
                        <col style='width:430px' />
                    </colgroup>    
                    <tr class="TBLINI">
                        <td align="center" title="Obra en curso y Facturación anticipada">OC y FA</td>
                        <td style="padding-left:3px;">Profesional
                            <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
                            <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20" />
			            </td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow-y: auto; overflow-x:hidden; width: 496px; height:480px">
                    <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif); width:480px">
                     <%=strTablaGestoresSAP%>
                    </div>
                </div>
                <table style="width: 480px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
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

