<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">

    <script type="text/javascript">
	    var sNumEmpleado = "<% =Session["UsuarioActual"].ToString() %>"; 
	</script>
    <style type="text/css">
        #tblDatos tr { height:20px; }
        #tblDatos td {
	        border-collapse: separate;
            border-spacing: 0px;
	        padding: 0px 2px 0px 2px;
        }
    </style>
    <center>
       <table style="width:940px;text-align:left;margin-top:10px">
			<colgroup>
		        <col style="width: 470px" />	
                <col style="width: 470px" />		        			
			</colgroup>
            <tr>                       
                <td align="left" valign="top">
                    <fieldset style="width: 140px; height:60px; padding:5px;text-align:left">
                        <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
                            <table style="width:135px;" cellpadding="3px" >
                                <colgroup><col style="width:35px;"/><col style="width:100px;"/></colgroup>
                                <tr>
                                    <td>Inicio</td>
                                    <td>
                                        <asp:TextBox ID="txtDesde" style="width:90px;" Text="" readonly="true" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fin</td>
                                    <td>
                                        <asp:TextBox ID="txtHasta" style="width:90px;" Text="" readonly="true" runat="server" />
                                    </td>
                                </tr>
                            </table>
                    </fieldset>                        
                    <fieldset style="width: 320px; height:410px; padding:5px; text-align:left; margin-top:20px;">
                        <legend>Totales mensuales</legend>
                        <br />
                        <table style="width: 300px; height: 17px;">
                            <colgroup>
                                    <col style="width: 200px;"/>
					                <col style="width: 100px" />		        
                            </colgroup>
                            <tr class="TBLINI">
                                <td style="padding-left:5px;">Mes/Año</td>
                                <td style="padding-right:5px;text-align:right">Horas</td>
                            </tr>
                        </table>
                        <div id="divMensual" style="overflow: auto; overflow-x: hidden; width: 316px; height:350px;" runat="server">
                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:300px">
					            <%=strTablaMensual%>
                            </div>
                        </div>
                        <table style="width: 300px; height: 17px; margin-bottom: 3px;">
                            <tr class="TBLFIN">
                                <td></td>
                            </tr>
                        </table>
                    </fieldset>
                </td>  
                <td style="padding-left:0px;">
                    <fieldset style="width: 470px; height:500px; padding:5px;text-align:left">
                        <legend>Detalle</legend>
                        <br />
                        <table style="width: 450px; height: 17px;">
                            <colgroup>
                                    <col style="width: 150px;"/>
                                    <col style="width: 200px;" />
					                <col style="width: 100px" />		        
                            </colgroup>
                            <tr class="TBLINI">
                                <td style="padding-left:5px;">Fecha</td>
                                <td>Día de la semana</td>
                                <td style="padding-right:10px;text-align:right">Horas</td>
                            </tr>
                        </table>
                        <div id="divDetalle" style="overflow: auto; overflow-x: hidden; width: 466px; height:440px;" runat="server">
                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px">
					            <%=strTablaDetalle%>
                            </div>
                        </div>
                        <table style="width: 450px; height: 17px; margin-bottom: 3px;">
                            <tr class="TBLFIN">
                                <td></td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
       </table>
</center>  
<asp:TextBox ID="FORMATO" runat="server" style="visibility:hidden" Text="PDF" />
<asp:TextBox ID="hdnIDS" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnoMesDesde" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnoMesHasta" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />

</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">

<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
	        switch (strBoton) {
	            case "pdf": //Boton exportar pdf
	                {
	                    bEnviar = false;
	                    Exportar();
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

