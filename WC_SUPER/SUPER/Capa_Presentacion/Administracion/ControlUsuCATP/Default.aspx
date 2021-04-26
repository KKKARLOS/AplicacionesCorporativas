<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	    
        //SSRS
        var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
	    var nAnoMesActual = <%=nAnoMes %>;
        //SSRS

	</script>
    <style type="text/css">
        #tblDatos tr { height:20px; }
        #tblDatos td {
	        border-collapse: separate;
            border-spacing: 0px;
	        padding: 0px 2px 0px 2px;
        }
        .excede
        {
            WIDTH: 32px;
            BORDER: #315d6b 1px solid;
            PADDING: 0px 1px 0px 0px;
            MARGIN: 0px 1px 0px 2px;
            FONT-SIZE: 11px;
            LEFT: 0px;
            FONT-FAMILY: Arial, Helvetica, sans-serif;
            TOP: 0px;
            HEIGHT: 16px;
            BACKGROUND-COLOR: #F58D8D;
            TEXT-ALIGN: right
        }   
        .colTablaD
        {
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #569BBD;
            border-top-style: solid;
            border-top-width: 1px;
            border-top-color: #569BBD;
            background-color: #E4EFF3;
        }         
    </style>
<img id="imgCaution" runat="server" src="../../../Images/imgCaution.gif" border=0 style="position:absolute; top:135px; left:930px;" title="Existen profesionales que han excedido las horas contratadas." />    
<center>
       <table style="width:846px;text-align:left">
			<colgroup>
					<col style="width: 25px;"/>
					<col style="width: 470px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 45px" />
	                <col style="width: 35px" />
                    <col style="width: 16px" />			                			        					
			</colgroup>
            <tr>
            <td colspan="2" align="left">
                        <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
                        <asp:TextBox ID="txtMesVisible" style="width:90px; text-align:center; vertical-align:super" readonly=true runat="server" Text=""></asp:TextBox>
                        <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
                        <br /><br />
            </td>
            <td colspan="7" class="colTabla" style="font-size:13px;text-align:center">Relación de horas contratadas</td>            
		    <td colspan="2" class="colTablaD" style="padding-right:7px; vertical-align:middle; text-align:right;">
	            <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />
	            &nbsp;
	            <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />  									        
		    </td>
		    <td>
		    </td>
            </tr>
            <tr>
                <td style="padding-left:0px;">
                    <table id="tblAsignados" style="width: 830px; height: 17px;">
                    <colgroup>
                            <col style="width: 25px;"/>
					        <col style="width: 470px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 45px" />
			                <col style="width: 45px" />	
                    </colgroup>
                    <tr class="TBLINI">
                        <td align="center">&nbsp;</td>
                        <td style="padding-left: 2px;"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgEmp" border="0">
					        <map name="imgEmp">
					            <area onclick="ot('tblDatos', 1, 0, '', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
					            <area onclick="ot('tblDatos', 1, 1, '', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
				            </MAP>Profesional
				            <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divProfAsig','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" tipolupa="2" width="20">
							<img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divProfAsig','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" tipolupa="1" width="20">				            
				        </td>
                        <td style="text-align:center"><label title="Lunes">L</label></td>
                        <td style="text-align:center"><label title="Martes">M</label></td>
						<td style="text-align:center"><label title="Miércoles">X</label></td>
						<td style="text-align:center"><label title="Jueves">J</label></td>
						<td style="text-align:center"><label title="Viernes">V</label></td>
						<td style="text-align:center"><label title="Sábado">S</label></td>
						<td style="text-align:center"><label title="Domingo">D</label></td>
						<td style="text-align:center"><label title="Semana">SEM</label></td>
						<td style="text-align:center"><label title="Mes">MES</label></td>
                    </tr>
                    </table>
                    <div id="divProfAsig" style="overflow: auto; overflow-x: hidden; width: 846px; height:460px;" runat="server" onscroll="scrollTablaProf()">
                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:830px">
						<%=strTablaUsuariosCATP%>
                        </div>
                    </div>
                    <table style="width: 830px; height: 17px; margin-bottom: 3px;">
                        <tr class="TBLFIN">
                            <td></td>
                        </tr>
                    </table>
                    <table class="texto" style="width: 846px; height: 17px; margin-top:5px;" border="0">
					<tr>
						<td style="padding-top: 5px;">
							<input type="text" class="excede" readonly="readonly" style="margin-left:0px;" />&nbsp;Imputaciones superiores a las horas contratadas
						</td>
					</tr>
                    </table>
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
<!--
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
-->
</script>
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</asp:Content>

