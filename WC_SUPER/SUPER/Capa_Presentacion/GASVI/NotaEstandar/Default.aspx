<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Default" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Calculadora/Calculadora.ascx" TagName="Calculadora" TagPrefix="uccalc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Gastos de Viaje</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<style type="text/css">  
    #tsPestanas table { table-layout:auto; }
</style>
<script type="text/javascript" language="javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
    var bCambios = false;
    var strEstructuraNodoCorta = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var sMsgRecuperada = "<%=sMsgRecuperada %>";
    var bLectura = <%=(bLectura)? "true":"false" %>;
    var sOrigen = "<%=sOrigen %>";
    var sDiaLimiteContAnoAnterior = <%=sDiaLimiteContAnoAnterior %>;
    var sDiaLimiteContMesAnterior = <%=sDiaLimiteContMesAnterior %>;
    var sNodoUsuario = "<%=sNodoUsuario %>";
    var nMinimoKmsECO = <%=Constantes.nNumeroMinimoKmsECO %>;
</script>
<style type="text/css">
#tblGastos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblTotalesGastos TD{border-right: solid 1px #A6C3D2; padding-right:2px;}
#tblTerritorio TD{border-right: solid 1px #A6C3D2; padding-right:2px;}
#tblAnticipos TD{border-right: solid 1px #A6C3D2; padding-right:2px;}
#tblPagado TD{border-right: solid 1px #A6C3D2; padding-right:2px;}

.TooltipGasto{color:#f2ec50;}

.TBLINIGASTO
{
    FONT-WEIGHT: bold;
    FONT-SIZE: 12px;
    background-color: #77b2c8;/* #6ca5ba;*/
    COLOR: #ffffff;
    FONT-FAMILY: Arial, Helvetica, sans-serif;
    height:17px;
}
table
{
    table-layout:fixed;
    border-collapse:collapse;
    empty-cells: show;
    cursor: pointer;
    /* Por defecto, el texto de las tablas que tengan el estilo de la clase .texto */    
    FONT-WEIGHT: normal;
    FONT-SIZE: 11px;
    COLOR: #000000;
    FONT-FAMILY: Arial, Helvetica, sans-serif;
    TEXT-DECORATION: none;
    /*  */
}

</style>
<asp:Image ID="imgCalculadora" ImageUrl="~/Capa_Presentacion/UserControls/Calculadora/Images/imgCalculadora50.gif" style="position:absolute; top:170px; left:950px; cursor:pointer; width:33px; height:50px;" onclick="getCalculadora(845, 0);" runat="server" />
<asp:Image ID="imgJustificantes" ImageUrl="~/Images/imgSeparador.gif" style="position:absolute; top:140px; left:340px; width:40px; height:40px;" runat="server" ToolTip="�Existen justificantes?" />
<br />
<table style="width:990px;text-align:left; margin-left:8px" cellpadding="4" >
<colgroup>
    <col style="width:70px;" />
    <col style="width:340px;" />
    <col style="width:100px;" />
    <col style="width:300px;" />
    <col style="width:180px;" />
</colgroup>
    <tr>
        <td><label id="lblBeneficiario" class="texto" runat="server">Beneficiario</label></td>
        <td><asp:TextBox ID="txtInteresado" style="width:300px;" Text="" runat="server" readonly="true" /></td>
        <td>Referencia</td>
        <td><asp:TextBox ID="txtReferencia" style="width:60px;" SkinID="numero" Text="" runat="server" readonly="true" /></td>
        <td rowspan="3">
            <asp:Image ID="imgEstado" ImageUrl="~/Images/imgEstado.gif" style="width:160px; height:80px;" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Concepto <span style="color:Red">*</span></td>
        <td><asp:TextBox ID="txtConcepto" style="width:300px;" Text="" runat="server" MaxLength="50" /></td>
        <td>Empresa</td>
        <td><asp:TextBox ID="txtEmpresa" style="width:200px;" Text="" runat="server" readonly="true" />
        <asp:DropDownList ID="cboEmpresa" runat="server" Width="200px" onchange="setEmpresa();aG(0);" AppendDataBoundItems="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Motivo</td>
        <td><asp:DropDownList ID="cboMotivo" runat="server" Width="140px" onchange="aG(0);setOblProy();" AppendDataBoundItems="true">
                                </asp:DropDownList></td>
        <td>Oficina liquidadora</td>
        <td><asp:TextBox ID="txtOficinaLiq" style="width:200px;" Text="" runat="server" /></td>
    </tr>
    <tr>
        <td><label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label> <span id="spanOblProy" style="color:Red; visibility:visible;">*</span></td>
        <td><asp:TextBox ID="txtProyecto" style="width:300px;" Text="" runat="server" readonly="true" /></td>
        <td>Moneda</td>
        <td colspan="2"><asp:DropDownList ID="cboMoneda" runat="server" Width="200px" onchange="aG(0); setMoneda();" AppendDataBoundItems="true">
                                </asp:DropDownList>
            </td>
    </tr>
    <tr style="vertical-align:top;">
        <td colspan="2" style="vertical-align:top;">
            <table style="width:440px">
                <col style="width:180px;" />   
                <col style="width:100px;" />
                <col style="width:160px;" /> 
            <tr>
                <td>
                    <fieldset style="width: 160px; height:55px;">
                        <legend>A cobrar</legend>
                        <table style="width:160px; margin-left: 7px; margin-top:5px;">
                        <colgroup>
                            <col style="width:80px;" />
                            <col style="width:80px;" />
                        </colgroup>
                        <tr>
                            <td>Sin retenci�n<br /><asp:TextBox ID="txtACobrarDevolver" SkinID="numero" style="width:65px;" Text="" runat="server" readonly="true" /></td><!-- onclick="getCalendarioRango()" -->
                            <td>En n�mina<br /><asp:TextBox ID="txtNomina" SkinID="numero" style="width:65px;" Text="0,00" runat="server" readonly="true" /></td><!-- onfocus="fn(this);ic(this.id);" -->
                        </tr>
                        </table>
                     </fieldset>                
                </td>
                <td>            
                    <fieldset style="width: 80px; height:55px;">
                        <legend>Total viaje</legend>   
                        <table style="width:70px; margin-left:3px; margin-top:16px;">
                        <tr>
                            <td><asp:TextBox ID="txtTotalViaje" SkinID="numero" style="width:65px;" Text="" runat="server" readonly="true" /></td>
                        </tr>
                        </table>
                    </fieldset>
                </td>
                <td> 
                    <fieldset style="width: 85px; height:55px;">
                        <legend>Justificantes <span style="color:Red">*</span></legend>   
                        <asp:RadioButtonList ID="rdbJustificantes" SkinId="rbl" style="margin-left:3px;" runat="server" RepeatColumns="1" onclick="if(!$I('rdbJustificantes_0').disabled){setImagenJustificantes();aG(0);}">
                            <asp:ListItem Value="1">S�&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </td>
            </tr>
            </table>
        </td>
        <td>Observaciones</td>
        <td><asp:TextBox TextMode="multiLine" ID="txtObservacionesNota" style="width:290px; height:70px;" Rows="3" Text="" runat="server" /></td>
        <td>
        <div id="divDisposiciones" runat="server"><asp:Image ID="imgDisposiciones" ImageUrl="~/Images/imgDispGen.gif" style="vertical-align:middle;" runat="server" /> Disposiciones generales<br /></div>
        <div id="divAnotaciones" style="cursor:pointer; margin-top:3px; vertical-align:bottom;" onclick="getAnotaciones();" runat="server"><asp:Image ID="imgAnotaciones" ImageUrl="~/Images/imgAnotacionesPer.gif" style="vertical-align:middle;" runat="server" /> <nobr id="lblAnotaciones">Anotaciones personales</nobr></div>
         <br />
         <asp:Image ID="imgLote" ImageUrl="~/Images/imgLote.gif" style="cursor:pointer; visibility:hidden;" onclick="mdlote();" runat="server" />
        </td>
    </tr>
</table>
<table style="width:1000px; margin-left:8px" align="center">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
							MultiPageID="mpContenido" 
							ClientSideOnLoad="CrearPestanas" 
							ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="Gastos" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Anticipos" ToolTip="Anticipos y pagado por la empresa" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Otros datos" ToolTip="" Width="100"></eo:TabItem>
					</Items>
				 </TopGroup>
			<LookItems>
					<eo:TabItem ItemID="_Default" 
					 LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
					 LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
					 LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
					 Image-Url="~/Images/Pestanas/normal_bg.gif"
					 Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
					 Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
						RightIcon-Url="~/Images/Pestanas/normal_right.gif"
						RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
						RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
						NormalStyle-CssClass="TabItemNormal"
						HoverStyle-CssClass="TabItemHover"
						SelectedStyle-CssClass="TabItemSelected"
						DisabledStyle-CssClass="TabItemDisabled"
						Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
					</eo:TabItem>
			</LookItems>
			</eo:TabStrip>
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="340px">
			    <eo:PageView CssClass="PageView" runat="server">
				<!-- Pesta�a 1 Gastos-->
				<table id="tblTituloGastos" style="width:970px; margin-left:-6px; border:solid 1px #e9e9e9;">
		        <colgroup>
			        <col style="width:130px" />
			        <col style="width:165px" />
			        <col style="width:20px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:65px" />
			        <col style="width:40px" />
			        <col style="width:65px" />
			        <col style="width:30px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:75px" />
		        </colgroup>
                <tr style="vertical-align:top; text-align:center;" class="TBLINIGASTO"> 
                  <td rowspan="2" style="border-right:solid 1px #e9e9e9;">FECHA</td>
                  <td rowspan="2"></td>
                  <td rowspan="2" style="border-right:solid 1px #e9e9e9;"></td>
                  <td colspan="8" style="border-right:solid 1px #e9e9e9;">SIN JUSTIFICANTE</td>
                  <td rowspan="2" colspan="4" style="border-right:solid 1px #e9e9e9;">CON JUSTIFICANTE</td>
                  <td rowspan="2"></td>
                </tr>
                <tr class="TBLINIGASTO" style="height:18px; vertical-align:bottom;"> 
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" colspan="5" align="center">Dietas</td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" colspan="3"><img id="imgKMSEstandares" src="../../../Images/imgSeparador.gif" style="width:16px; height:16px;" border="0" /><span style="width:auto; text-align:center; vertical-align: top; margin-left:7px">Veh�culo propio</span></td>
                </tr>
                <tr class="TBLINIGASTO" align="center">
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;">Inicio&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fin&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                  <td style="border-top:solid 1px #e9e9e9;">Destino</td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" title="Comentario al gasto"><img src="../../../Images/imgComGastoOn.gif" border="0" /></td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" id="cldDC"><nobr style="noWrap:true;cursor:pointer;" class="TooltipGasto" title="cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />��N� de dietas completas] body=[La dieta incluye todos los gastos que se produzcan en el d�a, ocasionados por el viaje, a excepci�n de desplazamientos, hoteles y aparcamiento.<br><br>La cuant�a diaria que satisfar� la Empresa y que no requerir� la entrega de ning�n justificante es de 38,17 �.<br><br>Se podr� aplicar en los siguientes tipos de desplazamiento:<br>Con pernocta<br>Sin pernocta y lejos (distancias superiores a 50 kil�metros), en desplazamientos espor�dicos (duraci�n inferior a dos semanas)<br>(Como alternativa, en aquellas circunstancias por las que se pueda justificar razonablemente, la Empresa abonar� los gastos correspondientes, con un l�mite de 18,03 � por comida)<br><br>Art.12 del Convenio.] hideselects=[off]">C</nobr></td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" id="cldDM"><nobr style="noWrap:true;cursor:pointer;" class="TooltipGasto" title="cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />��N� de medias dietas] body=[La media dieta incluye los mismos conceptos que la dieta, pero se aplica cuando no se realiza la comida o la cena.<br><br>La cuant�a diaria que satisfar� la Empresa y que no requerir� la entrega de ning�n justificante es de 21,47 �.<br><br>Se podr� aplicar en los siguientes tipos de desplazamiento:<br>Con pernocta<br>Sin pernocta y lejos (distancias superiores a 50 kil�metros), en desplazamientos espor�dicos (duraci�n inferior a dos semanas)<br>(Como alternativa, en aquellas circunstancias por las que se pueda justificar razonablemente, la Empresa abonar� los gastos correspondientes, con un l�mite de 18,03 � por comida)<br><br>Art.12 del Convenio.] hideselects=[off]">M</nobr></td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" id="cldDA"><nobr style="noWrap:true;cursor:pointer;" class="TooltipGasto" title="cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />��N� de dietas especiales] body=[La cuant�a que satisfar� la Empresa, y que no requerir� la entrega de ning�n justificante, es de 10,00 �.<br><br>Se podr� aplicar en los siguientes tipos de desplazamiento:<br>Sin pernocta y lejos (distancias superiores a 50 kil�metros) en desplazamientos continuados (duraci�n superior a dos semanas)<br>Sin pernocta y cerca.<br>(Como alternativa, la empresa abonar� los gastos pagados correspondientes al men� del d�a)<br><br>Art.12 del Convenio.] hideselects=[off]">E</nobr></td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;" id="cldDE"><nobr style="noWrap:true;cursor:pointer;" class="TooltipGasto" title="cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />��N� de dietas de alojamiento] body=[En el caso de que el empleado decidiese alojarse por su cuenta, IBERM�TICA le abonar� por ese concepto un importe equivalente al 75% del precio de la dieta por cada d�a que permanezca en dicha situaci�n.<br><br>Art.12 del Convenio.] hideselects=[off]">A</nobr></td>
		          <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;">Importe</td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;">Kms.</td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;">Importe</td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;">ECO</td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;"><label id="lblPeajes" style="cursor:pointer;" class="TooltipGasto">Peajes</label></td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;"><label id="lblComidas" style="cursor:pointer;" class="TooltipGasto">Comidas</label></td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;"><label id="lblTransporte" style="cursor:pointer;" class="TooltipGasto">Transp.</label></td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;">Hoteles</td>
                  <td style="border-top:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;">TOTAL</td>
                </tr>
                </table>
                <div id="divCatalogoGastos" style="margin-left:-5px; overflow-x:hidden; overflow-y:auto; width: 986px; height:240px;">
                <div id="divFondoCatalogoGastos" runat="server" style="background-image:url('../../../Images/imgFT20.gif'); width:970px">
 				<table id="tblGastos" class="MANO" style="width:970px;" mantenimiento="1">
		        <colgroup>
			        <col style="width:130px" />
			        <col style="width:165px" />
			        <col style="width:20px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:65px" />
			        <col style="width:40px" />
			        <col style="width:65px" />
			        <col style="width:30px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:75px" />
		        </colgroup>  
                </table>
                </div>
                </div>
				<table id="tblTotalesGastos" style="width:970px; margin-left:-5px;">
		        <colgroup>
			        <col style="width:130px" />
			        <col style="width:165px" />
			        <col style="width:20px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:30px" />
			        <col style="width:65px" />
			        <col style="width:40px" />
			        <col style="width:65px" />
			        <col style="width:30px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:65px" />
			        <col style="width:75px" />
		        </colgroup>
		        <tr class="TBLFIN" align="right"> 
                  <td colspan="3" align="center"> 
                    <div class="texto">TOTAL</div>
                  </td>
                  <td><label id="txtGSTDC">0</label></td>
                  <td><label id="txtGSTMD">0</label></td>
                  <td><label id="txtGSTDE">0</label></td>
                  <td><label id="txtGSTAL">0</label></td>
                  <td><label id="txtGSTIDI">0,00</label></td>
                  <td><label id="txtGSTKM">0</label></td>
                  <td><label id="txtGSTIKM">0,00</label></td>
                  <td></td>
                  <td><label id="txtGSTPE">0,00</label></td>
                  <td><label id="txtGSTCO">0,00</label></td>
                  <td><label id="txtGSTTR">0,00</label></td>
                  <td><label id="txtGSTHO">0,00</label></td>
                  <td><label id="txtGSTOTAL">0,00</label></td>
                </tr>
              </table>
                <div align="right" style="margin-right:50px; margin-top:2px;">
                <img src="../../../Images/imgNuevoGasto.gif" class="ICO MANO" onclick="addGasto(true)" title="A�ade nueva fila de gasto" />
                <img src="../../../Images/imgEliminarGasto.gif" class="ICO MANO" onclick="delGasto()" title="Elimina la fila de gasto seleccionada" />
                <img src="../../../Images/imgDuplicarGasto.gif" class="ICO MANO" onclick="dupGasto()" title="Duplica fila de gasto seleccionada" />
                </div>
                </eo:PageView>
                
				<eo:PageView CssClass="PageView" runat="server">
				<!-- Pesta�a 2 Anticipos y Empresa-->
				<table id="" style="width: 970px; margin-top:10px;">
				    <tr>
				        <td>
                        <fieldset id="Fieldset1" style="width: 960px; height:140px;">
				            <legend>LIQUIDACI�N DE ANTICIPOS (S�lo si hay anticipos a liquidar)</legend>
				            <table id="tblAnticipos" style="width: 940px; margin:5px; margin-top:10px;" border="0" cellpadding="5">
				            <colgroup>
				                <col style="width:50px;" />
				                <col style="width:220px;" />
				                <col style="width:50px;" />
				                <col style="width:230px;" />
				                <col style="width:390px;" />
				            </colgroup>
				            <tr class="TBLINIGASTO" style="height:24px; text-align:center;">
				                <td colspan="2" style=" border: solid 1px #e9e9e9; padding-left:3px;">Anticipado</td>
				                <td colspan="2" style=" border: solid 1px #e9e9e9;">Devuelto</td>
				                <td style=" border: solid 1px #e9e9e9;">Aclaraciones</td>
				            </tr>
				            <tr class="FA">
				                <td style='padding-left:3px;'>Fecha</td>
				                <td><asp:TextBox ID="txtFecAnticipo" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="aG(0);" runat="server" goma="0" /></td>
				                <td>Fecha</td>
				                <td><asp:TextBox ID="txtFecDevolucion" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="aG(0);" runat="server" goma="0" /></td>
				                <td rowspan="3">
                                    <asp:TextBox TextMode="multiLine" ID="txtAclaracionesAnticipos" style="width:380px; height:70px;" Rows="3" Text="" runat="server" />
				                </td>
				            </tr>
				            <tr class="FB">
				                <td style="padding-left:3px;">Importe</td>
				                <td><asp:TextBox ID="txtImpAnticipo" style="width:60px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setTotales();aG(1);" /></td>
				                <td>Importe</td>
				                <td><asp:TextBox ID="txtImpDevolucion" style="width:60px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setTotales();aG(1);" /></td>
				            </tr>
				            <tr class="FA">
				                <td style="padding-left:3px;">Oficina</td>
				                <td><asp:TextBox ID="txtOficinaAnticipo" style="width:200px;" MaxLength="50" Text="" runat="server" /></td>
				                <td>Oficina</td>
				                <td><asp:TextBox ID="txtOficinaDevolucion" style="width:200px;" MaxLength="50" Text="" runat="server" /></td>
				            </tr>
				            </table>
				        </fieldset><br /><br />
				    	</td>
				    </tr>
				    <tr>
				        <td>
                        <fieldset id="Fieldset2" style="width: 960px; height:140px;">
				            <legend>GASTOS PAGADOS POR LA EMPRESA (S�lo si se va a facturar al cliente. Importes sin IVA) </legend>
				            <table id="tblPagado" style="width: 940px; margin:5px; margin-top:10px;" border="0" cellpadding="5">
				            <colgroup>
				                <col style="width:150px;" />
				                <col style="width:100px;" />
				                <col style="width:100px;" />
				                <col style="width:100px;" />
				                <col style="width:100px;" />
				                <col style="width:390px;" />
				            </colgroup>
				            <tr class="TBLINIGASTO" style="height:24px;">
				                <td rowspan="2" style="border: solid 1px #e9e9e9; text-align:center;">Billetes de agencia</td>
				                <td style=" border: solid 1px #e9e9e9; text-align:center;">Transporte</td>
				                <td style=" border: solid 1px #e9e9e9; text-align:center;">Hotel</td>
				                <td style=" border: solid 1px #e9e9e9; text-align:center;">Otros</td>
				                <td style=" border: solid 1px #e9e9e9; text-align:center;">Total &euro;</td>
				                <td style=" border: solid 1px #e9e9e9; text-align:center;">Aclaraciones</td>
				            </tr>
				            <tr class="FA" style=" vertical-align:top;">
				                <td style="text-align:right; padding-right:2px;"><asp:TextBox ID="txtPagadoTransporte" style="width:90px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setPagadoEmpresa();aG(1);" /></td>
				                <td style="text-align:right; padding-right:2px;"><asp:TextBox ID="txtPagadoHotel" style="width:90px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setPagadoEmpresa();aG(1);" /></td>
				                <td style="text-align:right; padding-right:2px;"><asp:TextBox ID="txtPagadoOtros" style="width:90px;" SkinID="numero" Text="" runat="server" onfocus="fn(this)" onchange="setPagadoEmpresa();aG(1);" /></td>
				                <td style="text-align:right; padding-right:2px;"><asp:TextBox ID="txtPagadoTotal" style="width:90px;" SkinID="numero" Text="" runat="server" readonly="true" /></td>
				                <td><asp:TextBox TextMode="multiLine" ID="txtAclaracionesPagado" style="width:380px; height:70px;" Rows="3" Text="" runat="server" /></td>
				            </tr>
				            </table>
                                    
				        </fieldset>
				        </td>
				    </tr>
				</table>
                </eo:PageView>
                
				<eo:PageView CssClass="PageView" runat="server">
				<!-- Pesta�a 3 Otros datos-->
				<table style="width:990px">
				<colgroup>
				    <col style="width: 330px;" />
				    <col style="width: 660px;" />
				</colgroup>
				<tr style="vertical-align:top;">
				    <td>
                    <div align="center" style="background-image: url('../../../Images/imgFondo240.gif');background-repeat:no-repeat;
                        width: 240px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
                    font:bold 12px Arial;
                    color:#5894ae;">Territorio fiscal: <asp:Label ID="lblTerritorio" runat="server" style="font:bold 12px Arial; color:#5894ae;"></asp:Label></div>
                    <table style="width:320px;" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                            </td>
                            <td background="../../../Images/Tabla/8.gif" height="6">
                            </td>
                            <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                            </td>
                        </tr>
                        <tr>
                            <td background="../../../Images/Tabla/4.gif" width="6">
                                &nbsp;</td>
                            <td background="../../../Images/Tabla/5.gif" style="padding: 5px; vertical-align:top;">
                                <!-- Inicio del contenido propio de la p�gina -->
				                <table id="tblTerritorio" style="width: 300px; margin-top:5px;">
				                <colgroup>
				                    <col style="width:100px;" />
				                    <col style="width:80px;" />
				                    <col style="width:120px;" />
				                </colgroup>
				                <tr class="TBLINIGASTO">
				                    <td rowspan="2" style="border-left:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;border-top:solid 1px #e9e9e9; text-align:center;">Conceptos</td>
				                    <td colspan="2" style="border-bottom:solid 1px #e9e9e9;border-right:solid 1px #e9e9e9;border-top:solid 1px #e9e9e9;">Importes &euro;</td>
				                </tr>
				                <tr class="TBLINIGASTO">
				                    <td style="border-right:solid 1px #e9e9e9;">Convenio</td>
				                    <td style="border-right:solid 1px #e9e9e9;">Exento de retenci�n</td>
				                </tr>
				                <tr class="FA">
				                    <td style="padding-left:3px;">Kilometraje</td>
				                    <td id="cldKMCO" runat="server" style="text-align:center;">0,00</td>
				                    <td id="cldKMEX" runat="server" style="border-right:0px; text-align:center;"></td>
				                </tr>
				                <tr class="FB">
				                    <td style="padding-left:3px;">Dieta completa</td>
				                    <td id="cldDCCO" runat="server" style="text-align:center;">0,00</td>
				                    <td id="cldDCEX" runat="server" style="border-right:0px; text-align:center;">0,00</td>
				                </tr>
				                <tr class="FA">
				                    <td style="padding-left:3px;">Media dieta</td>
				                    <td id="cldMDCO" runat="server" style="text-align:center;">0,00</td>
				                    <td id="cldMDEX" runat="server" style="border-right:0px; text-align:center;">0,00</td>
				                </tr>
				                <tr class="FB">
				                    <td style="padding-left:3px;">Dieta especial</td>
				                    <td id="cldDECO" runat="server" style="text-align:center;">0,00</td>
				                    <td id="cldDEEX" runat="server" style="border-right:0px; text-align:center;">0,00</td>
				                </tr>
				                <tr class="FA">
				                    <td style="padding-left:3px;">Dieta alojamiento</td>
				                    <td id="cldDACO" runat="server" style="text-align:center;">0,00</td>
				                    <td id="cldDAEX" runat="server" style="border-right:0px; text-align:center;">0,00</td>
				                </tr>
				                <tr class="TBLFIN">
				                    <td colspan="3" style="border-right:0px;">&nbsp;</td>
				                </tr>
				                </table>
                                <!-- Fin del contenido propio de la p�gina -->
                            </td>
                            <td background="../../../Images/Tabla/6.gif" width="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                            </td>
                            <td background="../../../Images/Tabla/2.gif" height="6">
                            </td>
                            <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                            </td>
                        </tr>
                    </table>
				    </td>
				    <td rowspan="2">
                    <div align="center" style="background-image: url('../../../Images/imgFondo100.gif');background-repeat:no-repeat;
                        width: 100px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
                    font:bold 12px Arial;
                    color:#5894ae;">Historial</div>
                    <table style="width:647px;" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                            </td>
                            <td background="../../../Images/Tabla/8.gif" height="6">
                            </td>
                            <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                            </td>
                        </tr>
                        <tr>
                            <td background="../../../Images/Tabla/4.gif" width="6">
                                &nbsp;</td>
                            <td background="../../../Images/Tabla/5.gif" style="padding: 5px; vertical-align:top;">
                                <!-- Inicio del contenido propio de la p�gina -->
                                <table id="tblTituloHistorial" style="width:610px;height:17px; margin-top:10px;">
	                                <colgroup>					
		                                <col style="width:105px;" />
		                                <col style="width:105px;" />
		                                <col style="width:400px;" />
	                                </colgroup>
	                                <tr class="TBLINI">				    
		                                <td style="padding-left:3px;">Estado</td>
		                                <td>Fecha</td>
		                                <td>Profesional / Motivo</td>
	                                </tr>
                                </table>
                                <div id="divCatalogoHistorial" style="overflow: auto; overflow-x: hidden; width: 626px; height:225px" runat="server">
	                                <div style="width:610px">
	                                </div>
                                </div>
                                <table id="tblPieHistorial" style="width:610px;height:17px;">
	                                <tr class="TBLFIN">
		                                <td>&nbsp;</td>
	                                </tr>
                                </table>
                                <!-- Fin del contenido propio de la p�gina -->
                            </td>
                            <td background="../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                            </td>
                            <td background="../../../Images/Tabla/2.gif" height="6">
                            </td>
                            <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                            </td>
                        </tr>
                    </table>
				    </td>
				</tr>
				<tr>
				    <td style="vertical-align:bottom;">
                    <div id="divContabilizacion" align="center" style="background-image: url('../../../Images/imgFondo185.gif');background-repeat:no-repeat;
                        width: 185px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
                    font:bold 12px Arial; visibility:hidden;
                    color:#5894ae;">Datos para contabilizaci�n</div>
                    <table id="tblContabilizacion" style="width:320px; visibility:hidden;">
                        
                        <tr>
                            <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                            </td>
                            <td background="../../../Images/Tabla/8.gif" height="6">
                            </td>
                            <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                            </td>
                        </tr>
                        <tr>
                            <td background="../../../Images/Tabla/4.gif" width="6">
                                &nbsp;</td>
                            <td background="../../../Images/Tabla/5.gif" style="padding: 5px; vertical-align:top;">
                                <!-- Inicio del contenido propio de la p�gina -->
				                <table id="Table1" style="width: 300px; margin-top:5px; padding:5px;">
				                <tr>
				                    <td style="padding-left:5px;">Fecha <asp:TextBox ID="txtFecContabilizacion" style="width:60px; margin-left:5px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(event);" onchange="aG(0);" readonly="true" runat="server" goma="0" /></td>
				                </tr>
				                <tr>
				                    <td>
				                        <fieldset id="flsTipoCambio" style="width:292px; padding:5px; visibility:hidden;">
				                        <legend>Tipo de cambio</legend>
				                        <label>1 &euro; equivale a</label><br />
				                        <asp:TextBox ID="txtTipoCambio" style="width:60px; margin-top:3px;" SkinID="numero" Text="" runat="server" onfocus="fn(this, 3, 4)" /> <label id="lblLiteralMoneda"></label>
				                        </fieldset>
				                    </td>
				                </tr>

				                </table>
                                <!-- Fin del contenido propio de la p�gina -->
                            </td>
                            <td background="../../../Images/Tabla/6.gif" width="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                            </td>
                            <td background="../../../Images/Tabla/2.gif" height="6">
                            </td>
                            <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                            </td>
                        </tr>
                    </table>
				    </td>
				</tr>
				</table>
                </eo:PageView>

           </eo:MultiPage>
        </td>
    </tr>
</table>
<br />
<center>
    <table style="width:300px;" align="center">
    <tr>
        <td>
		    <button id="btnTramitar" type="button" onclick="tramitar()" class="btnH25W105" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/botones/imgTramitar.gif" /><span title="">&nbsp;&nbsp;Tramitar</span>
		    </button>		  
        </td>
        <td>
		    <button id="btnCancelar" type="button" onclick="bCambios=false; salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/botones/imgCancelar.gif" /><span title="">&nbsp;&nbsp;Cancelar</span> 
		    </button>		
        </td>
    </tr>
    </table>
</center>
<div id="divExportar" runat="server" 
    style="position: absolute;
            top:250px;
            left:300px;
            width:350px;
            z-index:10;
            display:none"
    class="texto">
	<table style="border:1px solid #5894ae;width:350px; height:30px; background-color:#bcd4df;" align="center">
		<tr>
			<td style="padding-left:5px;"><B><FONT size="3">Modelo de exportaci�n</FONT></B></td>
		</tr>
	</table>

	<table style="border:1px solid #5894ae;width:350px;background-color:#D8E5EB;" align="center">
	    <tr><td><br /></td></tr>
	    <tr>
	        <td align="center">
	            <table align='center'>
	                <tr>
	                    <td>
	                    	<table align='center' style="width:150px;">
	                        <tr>
	                            <td>
			                    <asp:radiobuttonlist id="rdbMasivo" runat="server" SkinId="rbl" width="200px" RepeatDirection="vertical">
				                    <asp:ListItem Selected=true Value="1">Solicitud individual<br /></asp:ListItem>
				                    <asp:ListItem Value="2">Lote completo</asp:ListItem>
			                    </asp:radiobuttonlist> 
			                    </td>
			                </tr>
			                </table>
	                    </td>
	                </tr>
	                <tr>
						<td><br /></td>
					</tr>
                    <tr>
                        <td>	
							 <table align="center" style="margin-top:5px; width:300px">
									<tr>
										<td align="center">
										<button id="btnAceptar" type="button" onclick="aceptarExportacion()" style="width:85px" hidefocus=hidefocus>
											<span><img src="../../../images/imgAceptar.gif" />&nbsp;Aceptar</span>
										</button>    
										</td>
										<td align="center">
										<button id="btnSalir" type="button" onclick="$I('divExportar').style.display='none'" style="width:85px" hidefocus=hidefocus>
											<span><img src="../../../images/imgCancelar.gif" />&nbsp;Cancelar</span>
										</button>    
										</td>
									</tr>
								</table> 
                        </td>
                   </tr>
                   <tr><td><br /></td></tr>
                </table>
             </td> 
        </tr>			
    </table>
</div> 	
    <asp:TextBox ID="hdnReferencia" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnInteresado" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnEstado" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnEstadoAnterior" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnOficinaLiquidadora" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnOficinaBase" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnAnotacionesPersonales" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIDEmpresa" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIDTerritorio" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnAutorresponsable" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIDLote" runat="server" style="visibility:hidden" Text="" />
    <input type="hidden" name="hdnFechaIAP" id="hdnFechaIAP" value="<%=sFechaIAP %>" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uccalc:Calculadora ID="Calculadora" runat="server" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript" language="javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

		    switch (strBoton){
			    case "tramitar": 
			    {
                    bEnviar = false;
                    //alert("tramitar");
                    tramitar();
				    break;
			    }
		    }
	    }
	    var theform = document.forms[0];
	    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	    theform.__EVENTARGUMENT.value = eventArgument;
	    if (bEnviar) theform.submit();
    }
	
    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
</script>
</body>
</html>

