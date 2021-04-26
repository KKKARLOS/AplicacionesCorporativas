<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SeleccionDias_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Reconexion.ascx" TagName="reconexion" TagPrefix="uc1" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="JavaScript">

var nCal   = <%=nCal%>;
var num_empleado    = <%=Session["NUM_EMPLEADO_IAP"]%>;
var idFicepi    = <%=Session["IDFICEPI_IAP"]%>;
var controlhuecos = <%=((bool)Session["CONTROLHUECOS"])?"true":"false"%>;
var nReconectar = "<%=Session["reconectar_iap"].ToString()%>";
var sPerfil = "<%=Session["perfil_iap"].ToString()%>";
var intMes;
var intAnno;
var strFestivos = "";
var strUltimoMesCerrado;
var strUMC = "<%=Session["UMC_IAP"]%>";
var strAuxUltimoDia = "<%=Session["FEC_ULT_IMPUTACION"]%>";
var aUltimoDia;
var oUltImputac = new Date(1900,0,1);   
var oDiaActual = new Date(<%=DateTime.Now.Year%>,<%=DateTime.Now.Month-1%>,<%=DateTime.Now.Day%>);  
if (strAuxUltimoDia != ""){
    aUltimoDia = strAuxUltimoDia.split("/");
    oUltImputac = new Date(aUltimoDia[2],eval(aUltimoDia[1]-1),aUltimoDia[0]);  
}
var aSemLab	= "<%=Session["aSemLab"] %>".split(",");
//alert(aSemLab);
var reconectar_msg = <%=Session["reconectar_msg_iap"]%>;
var aDatos;
var aSemanas = new Array();
aSemanas[0] = new Array();
aSemanas[1] = new Array();
aSemanas[2] = new Array();
aSemanas[3] = new Array();
aSemanas[4] = new Array();
aSemanas[5] = new Array();

var aFestivos = new Array();
var aFestivosG = new Array();
<%=aFestivosG%>
    var aDiaFes = new Array();

<%--var aVacaciones= new Array();
<%=aVacaciones%>
var aDiaVacaciones = new Array();--%>



var intMesCalendario;
var intAnnoCalendario;

</script>
<center>
<table style="width:900px" >
<colgroup>
<col style="width:700px;" />
<col style="width:200px;" />
</colgroup>
<tr><td colspan="2" id="tdInteresado" style="text-align:left;visibility:hidden;background-image:url('../../../Images/imgFondoCal4G.gif'); background-repeat:no-repeat; width: 650px; height: 28px;">&nbsp;&nbsp;&nbsp;&nbsp;<span id="lblReconectar" class="enlace" onclick="reconectar();" style="width:70px;display:inline-block;">Profesional:</span><asp:Image ID="imgProfesional" runat="server" Height="16px" Width="16px" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" /><label id="lblUsuario" class="texto"><%=Session["DES_EMPLEADO_IAP"]%></label></td></tr>
<tr><td colspan="2" style="height: 5px;"><img src="../../../Images/imgSeparador.gif" width="1px" /></td></tr>
<tr><td colspan="2" style="text-align:left;background-image:url('../../../Images/imgFondoCal4G.gif'); background-repeat:no-repeat; width: 650px; height: 28px;">&nbsp;&nbsp;&nbsp;&nbsp;<span class="enlace" onclick="mostrarHorario();" style="width:70px;display:inline-block;">Calendario:</span><label id="lblCalendario" class="texto"><%=Session["DESCALENDARIO_IAP"]%></label></td></tr>
<tr> 
    <td style="text-align:left;padding-top:10px;">
        <script type="text/javascript">
	        var intAnnoInicio = <%=nCurrentYear%>;
	        var intMesInicio = <%=nCurrentMonth%>;
	        var cal = new Calendar();
        </script>
    </td>
    <td style="text-align:left;vertical-align:text-top;">
		<table class="texto" border="0" cellspacing="0" cellpadding="0" style='width: 100px;'>
		  <tr> 
			<td style="color:navy">&nbsp;Días</td>
		  </tr>
		</table>
		<table class="texto" border="0" cellspacing="0" cellpadding="3" style="background-image:url('../../../Images/imgFondoCal2.gif'); width: 90px; height: 126px;">
		  <tr> 
			<td class="diaCerrado" style="font-size:12px ">&nbsp;Mes cerrado</td>
		  </tr>
		  <tr> 
			<td class="diaImputado" style="font-size:12px ">&nbsp;Registrados</td>
		  </tr>
		  <tr> 
			<td class="diaNoImputado" style="font-size:12px ">&nbsp;Sin datos</td>
		  </tr>
		  <tr> 
			<td class="diaFuturo" style="font-size:12px ">&nbsp;Futuros</td>
		  </tr>
		  <tr> 
			<td class="diaFestivo" style="font-size:12px ">&nbsp;Festivos</td>
		  </tr>
		  <tr> 
			<td class="diaVacacion" style="font-size:12px ">&nbsp;Vacaciones</td>
		  </tr>
		</table><br><br>
		<table class="texto" border="0" cellspacing="0" cellpadding="0" style='width: 100px'>
		  <tr> 
			<td style="color:navy">Último día reportado</td>
		  </tr>
		</table>
		<table class="texto" border="0" cellspacing="0" cellpadding="0" style="background-image:url('../../../Images/imgFondoCal3.gif'); width: 90px; height: 23px;">
		  <tr> 
			<td>&nbsp;&nbsp;&nbsp;<label id="lblFUI" class="diaCerrado" style="font-size:12px "><%=Session["FEC_ULT_IMPUTACION"]%></label></td>
		  </tr>
		</table><br><br>
		<table class="texto" border="0" cellspacing="0" cellpadding="0" style='width: 100px'>
		  <tr> 
			<td style="color:navy">Último mes cerrado</td>
		  </tr>
		</table>
		<table class="texto" border="0" cellspacing="0" cellpadding="0" style="background-image:url('../../../Images/imgFondoCal100.gif'); width: 101px; height: 23px; text-align:center;">
		  <tr> 
			<td><label id="lblUMC" class="diaCerrado" style="font-size:12px;"><%=Fechas.AnnomesAFechaDescLarga((int)Session["UMC_IAP"])%></label></td>
		  </tr>
		</table><br><br>
		<table class="texto" border="0" cellspacing="0" cellpadding="0" style='width: 100px; '>
		  <tr> 
			<td style="color:navy">&nbsp;Horas</td>
		  </tr>
		</table>
		<table class="texto" border="0" cellspacing="0" cellpadding="3" style="background-image:url('../../../Images/imgFondoCal5.gif'); width: 90px; height: 65px;">
		  <tr> 
			<td style="font-size:12px; color: orange ">&nbsp;Planificadas</td>
		  </tr>
		  <tr> 
			<td class="diaImputado" style="font-size:12px ">&nbsp;Registradas</td>
		  </tr>
		  <tr> 
			<td style="font-size:12px ">&nbsp;Estándar</td>
		  </tr>
		</table><br>
		<img class="ICO" style="margin-bottom:2px;" src="../../../Images/icoAbiertoG.gif" />Entrada a imputar abierta<br />
		<img class="ICO" style="margin-bottom:2px;" src="../../../Images/icoCerradoG.gif" />Entrada a imputar cerrada<br />
		<img class="ICO" style="margin-bottom:2px;" src="../../../Images/imgPlanifON.gif" />Planificación de agenda abierta<br />
		<img class="ICO" src="../../../Images/imgPlanifOFF.gif" />Planificación de agenda cerrada
	</td>
  </tr>
	<tr>
	    <td style="padding-top: 5px;text-align:left;">
            &nbsp;<img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
	    </td>
	    <td></td>
	</tr>
</table>
</center>
    <uc1:reconexion ID="Reconexion" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "reconexion":
                    {
                        bEnviar = false;
                        setTimeout("reconectar()", 20);
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

