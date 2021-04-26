<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_ImpMasiva_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

var bControlhuecos   = <%=Session["CONTROLHUECOS"].ToString().ToLower()%>;
var num_empleado    = <%=Session["NUM_EMPLEADO_IAP"]%>;
var UsuarioActual    = <%=Session["UsuarioActual"]%>;

var nCal   = <%=nCal%>;
var bObligaest = false;

var aFestivos = new Array();
<%=aFestivos %>

var strAuxUltimoDia = "<%=strF_ult_imputac%>";
if (strAuxUltimoDia == ""){
	var strUltImputac = new Date(2000,0,1); 
	var objDiaSigUDR = new Date(2000,0,2); 
	var objDiaAntUDR = new Date(1999,11,31); 
    intUltimoDia = 0;
	intDiaAnterior = 0;
}else{
	var strUltImputac = cadenaAfecha(strAuxUltimoDia);
	//alert("strUltImputac: "+ strUltImputac);
	var objDiaSigUDR = new Date();
	var objDiaAntUDR = cadenaAfecha(strAuxUltimoDia).add("d", -1);
	
	var sw = 0;
	var sw1 = 0;
	var bFes = false;
    for (var y = 1; y <= 7; y++){
        var objNuevaFecha = cadenaAfecha(strAuxUltimoDia).add("d", y);
		bFes = false;
		for (var indice=0;indice<aFestivos.length;indice++){
			var strNuevaFecha = objNuevaFecha.ToShortDateString();
			//Comparo las fechas, porque si lo hago con los .setTime,
			//inexplicablemente, el 28/03/2005 me da las 01:00:00 en lugar de 00:00:00
			if (strNuevaFecha == aFestivos[indice]){
				bFes = true;
				break;
			}
		}
		//Añadir, y que el día no sea festivo.
        //if ((intDiaSemana == 0) || (intDiaSemana == 6) || bFes){ // el array de festivos incluye no laborables
        if (bFes){
            continue;
        }else{
			if (sw1 == 0){
				var intNuevaFecha2 = objNuevaFecha.getTime();
				sw1 = 1;
			}
            for (var z=0;z<aFestivos.length;z++){
                var strFechaAux = cadenaAfecha(aFestivos[z]);
                if (strFechaAux <= strUltImputac){
                    continue;
                }
                /* Al final del año, como todos los festivos son
                anteriores al último día reportado, nunca se llega hasta
                aquí, por lo que sw queda a 0 */
                if (strUltImputac.getTime() != strFechaAux.getTime()){
                    objDiaSigUDR.setTime(objNuevaFecha);
					sw = 1;
					break;
                }
            }
        }
		if (sw == 1) break;
    }
	//alert(sw);        
	if (sw == 0){
		objNuevaFecha = new Date();
		objNuevaFecha.setTime(intNuevaFecha2);
		objDiaSigUDR.setTime(objNuevaFecha);
	}
	
}
	
//alert("objDiaSigUDR: "+ objDiaSigUDR);
var strDiaSigUDR = objDiaSigUDR.ToShortDateString();

//Ultimo mes cerrado
var objUMC = AnnomesAFecha("<%=Session["UMC_IAP"]%>").add("mo", 1).add("d", -1);

//var strFechas = "strUltImputac: "+ strUltImputac +"\n";
//strFechas += "objDiaSigUDR: "+ objDiaSigUDR +"\n";
//strFechas += "objDiaAntUDR: "+ objDiaAntUDR +"\n";
//strFechas += "objUMC: "+ objUMC +"\n";
//alert(strFechas);
	var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";

</script>
<table class="texto" style="width:980px;" cellpadding="0" cellspacing="3px">
    <tr>
        <td colspan="2">
			<fieldset>
				<legend>Tipo de imputación</legend>
                <asp:RadioButtonList ID="rdbImpMas" runat="server" SkinID="rbl" Width="100%" onclick="seleccionarOpcion();">
                    <asp:ListItem Value="1" Selected="True">Imputar horas/día estándar a una tarea, desde el día siguiente al último día reportado hasta una fecha determinada.</asp:ListItem>
                    <asp:ListItem Value="3">Imputa r horas/día estándar a una tarea en un intervalo de fechas.</asp:ListItem>
                    <asp:ListItem Value="2">Imputar x horas a una tarea en un intervalo de fechas.</asp:ListItem>
                </asp:RadioButtonList>
             </fieldset>         
        </td>
    </tr>
    <tr>
        <td colspan="2"><br />
			<fieldset>
				<legend>Datos generales</legend>
                <table class="texto" style="width:950px;" cellpadding="5px" cellspacing="0" >
                    <colgroup>
                        <col style="width:90px" />
                        <col style="width:460px" />
                        <col style="width:130px" />
                        <col style="width:100px" />
                        <col style="width:50px" />
                        <col style="width:120px" />
                    </colgroup>
                    <tr>
                        <td colspan="2" rowspan="4">
                        <table class="texto" style="width:100%;" cellpadding="5px" cellspacing="0" >
                        <colgroup>
                            <col style="width:60px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td>Proyecto</td>
                            <td><asp:TextBox ID="txtProyecto" runat="server" readonly="true" Style="width:384px" /></td>
                        </tr>
                        <tr>
                            <td>P. técnico</td>
                            <td><asp:TextBox ID="txtPT" runat="server" readonly="true" Style="width:384px" /></td>
                        </tr>
                        <tr>
                            <td>Fase</td>
                            <td><asp:TextBox ID="txtFase" runat="server" readonly="true" Style="width:384px" /></td>
                        </tr>
                        <tr>
                            <td>Actividad</td>
                            <td><asp:TextBox ID="txtActividad" runat="server" readonly="true" Style="width: 384px" /></td>
                        </tr>
                        </table>
                        </td>
                        <td>Último día reportado</td>
                        <td>
                            <asp:TextBox ID="txtvUDR" runat="server" style="width:60px;" readonly="true" Text=""></asp:TextBox></td>
                        <td>Modo</td>
                        <td>
                            <asp:DropDownList ID="cbovModo" runat="server" style="width:90px" onChange="activarGrabar();">
                            <asp:ListItem Selected="True" Value="1" Text="Sustitución"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Acumulación"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Fecha inicio imputación</td>
                        <td><asp:TextBox ID="txtDesde" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="quitarTarea();   activarGrabar();" goma="0"></asp:TextBox></td>
                        <td><span title="Incluir no laborables y festivos" style="cursor:pointer">Festivos</span></td>
                        <td><asp:CheckBox ID="chkFestivos" runat="server" onClick="activarGrabar();" /></td>
                    </tr>
                    <tr>
                        <td>Fecha fin imputación</td>
                        <td><asp:TextBox ID="txtHasta" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="activarGrabar();" goma=0 /></td>
                        <td rowspan="2" colspan="2" style="vertical-align:top;">
			            <fieldset style="width:130px; height:35px; padding: 5px;">
				            <legend>Facturable <asp:CheckBox ID="chkFacturable" runat="server" style="vertical-align:middle;" Enabled=false /></legend>
				            <label id="lblModo"></label>
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>                        
                    </tr>

                    <tr>
                        <td colspan="6">
                            <table class="texto" style="width:100%;" cellpadding="0px" cellspacing="0" >
                                <colgroup>
                                    <col style="width:60px" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td><label id="lblTarea" class="enlace" onclick="getTarea()" style="margin-left:5px" onmouseover="mostrarCursor(this)">Tarea</label></td>
                                    <td colspan="5">
                                        <asp:TextBox id="txtIDTarea" MaxLength="8" runat="server" SkinID="Numero" style="width:50px;margin-left:5px" onkeyup="limpiar(event);" onkeypress="if(event.keyCode==13){event.keyCode=0;obtenerTarea();}else{vtn2(event);}" />
			                            <asp:TextBox id="txtDesTarea" runat="server" style="width:810px;display:inline-block" readonly="true" />
			                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>          
        </td>
    </tr>
    <tr>
        <td><br />
			<fieldset style="width:475px; height:80px;">
				<legend>Imputación</legend><br />
                 <table class="texto" style="width:450px;" cellpadding="0" cellspacing="3px">
                    <colgroup>
                        <col style="width:40px" />
                        <col style="width:55px" />
                        <col style="width:70px" />
                        <col style="width:295px" />
                    </colgroup>
                    <tr style="vertical-align:top">
                        <td>&nbsp;Horas</td>
                        <td>
                            <asp:TextBox ID="txtvHoras" style="width:32px" maxlength="5" SkinID="Numero" runat="server" onfocus="fn(this)"></asp:TextBox></td>
                        <td>Comentario</td>
                        <td>
                            <asp:TextBox ID="txtvComentario" TextMode=MultiLine SkinID="Multi" Columns="53" Rows="3" runat="server" style="width:300px" onKeyUp="activarGrabar();"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset> 
        </td>
        <td><br />
            <fieldset style="margin-left:4px; height:80px;">
                <legend>Datos IAP referentes al técnico</legend>
                <table align="center" border="0" class="texto" style="width:475px; table-layout:fixed; margin-top:8px;" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:85px;" />
                        <col style="width:75px;" />
                        <col style="width:95px;" />
                        <col style="width:70px;" />
                        <col style="width:85px;" />
                        <col style="width:65px;" />
                    </colgroup>
                    <tr>
                        <td>Primer consumo</td>
                        <td><asp:TextBox ID="txtPriCon" style="width:60px" runat="server" readonly="true" /></td>
                        <td>Consumido (horas)</td>
                        <td><asp:TextBox ID="txtConHor" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                        <td title="Pendiente estimado en horas">Pte. est. (horas)</td>
                        <td><asp:TextBox ID="txtPteEst" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                    </tr>
                    <tr>
                        <td style="padding-top:8px;">Último consumo</td>
                        <td style="padding-top:8px;"><asp:TextBox ID="txtUltCon" style="width:60px" runat="server" readonly="true" /></td>
                        <td style="padding-top:8px;" title="Consumido en jornadas">Consumido (jorn.)</td>
                        <td style="padding-top:8px;"><asp:TextBox ID="txtConJor" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                        <td style="padding-top:8px;">Avance teórico</td>
                        <td style="padding-top:8px;"><asp:TextBox ID="txtAvanEst" style="width:40px" runat="server" SkinID="Numero" readonly="true" />&nbsp;%</td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td><br />
            <fieldset style="width:475px; height:155px;">
            <legend>Indicaciones del responsable</legend>
            <table align="center" border="0" class="texto" style="width:450px; table-layout:fixed;" cellpadding="5" cellspacing="0">
                <tr>
                    <td width="50%">Total previsto (horas)&nbsp;&nbsp;<asp:TextBox ID="txtTotPre" SkinID="Numero" style="width:60px" runat="server" readonly="true" /></td>
                    <td width="50%">Fecha fin prevista&nbsp;&nbsp;<asp:TextBox ID="txtFinPre" style="width:60px" runat="server" readonly="true" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;Particulares<br />
                    <asp:TextBox ID="txtIndicaciones" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="2" Width="430px" readonly="true" Height="35px" style="margin-bottom:4px;"></asp:TextBox>
                    <br />&nbsp;Colectivas<br />
                    <asp:TextBox ID="txtColectivas" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="2" Width="430px" readonly="true" Height="35px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        </td>
        <td><br />
            <fieldset style="height:155px; margin-left:4px;">
                <legend>Comentarios del técnico</legend>
                <table align="center" border="0" class="texto" style="width:100%;" cellpadding="5px" cellspacing="0">
                    <tr>
                        <td width="35%">Total estimado (horas)</td>
                        <td width="65%"><asp:TextBox ID="txtvETE" style="width:50px;" SkinID="Numero" runat="server" onFocus="fn(this);" onKeyUp="activarGrabar();" /></td>
                    </tr>
                    <tr>
                        <td>Fecha de finalización estimada</td>
                        <td>
                            <asp:TextBox ID="txtvFFE" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="activarGrabar();" /><!--  onclick="showCal('CalendarioFFE')" -->
                            <asp:CheckBox ID="chkFinalizada" runat="server" style="cursor:pointer; margin-left:40px; vertical-align:middle;" Text="Trabajo finalizado" />
                        </td> <!-- onclick="controlTareaFin();"-->
                    </tr>
                    <tr>
                        <td colspan="2">Observaciones<br /><asp:TextBox ID="txtvObservaciones" TextMode="MultiLine" SkinID="Multi" Width="450px" Rows="4" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
</table>
<asp:TextBox ID="hdnOpcion" runat="server" Width="10px" Text="1" style="visibility:hidden"></asp:TextBox>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "grabar": 
				{
					bEnviar = false;
					setTimeout("grabar()", 20);
   					break;
				}
				case "guia": 
				{
				    bEnviar = false;
				    setTimeout("mostrarGuia('ImputacionMasiva.pdf');", 20);
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

