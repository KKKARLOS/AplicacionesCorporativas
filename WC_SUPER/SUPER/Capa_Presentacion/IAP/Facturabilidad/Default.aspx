<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_Consulta_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var sPerfil = "<%=Session["perfil_iap"].ToString()%>";
    var bRes1024 = <%=((bool)Session["IAPFACT1024"]) ? "true":"false" %>;
</script>
<div id="div1024" style="Z-INDEX: 105; width: 32px; HEIGHT: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<style type="text/css">
#tblDatos td{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblDatos2 td{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<center>
<table style="width:700px; text-align:left">
    <tr>
        <td id="tdInteresado" colspan="2" style="background-image:url('../../../Images/imgFondoCal4G.gif'); background-repeat:no-repeat; width: 650px; height: 28px;">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <span id="lblReconectar" class="texto" style="width:70px;display:inline-block;">Profesional:</span>
            <label id="lblUsuario" class="texto"><%=Session["DES_EMPLEADO_IAP"]%></label>
        </td>
    </tr>
</table>
</center>
<table id="tbl1" style="width: 1240px; margin-top:8px;">
    <tr>
    <td colspan="2">
        <table id="tblCriterios" style="width:1220px;">
            <colgroup>
                <col style="width:295px" />
                <col style="width:325px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
            </colgroup>
            <tr class="texto" style="height:24px; text-align:center;">
                <td colspan="2" style="text-align:left;">
                    &nbsp;Desde&nbsp;&nbsp;
                    <asp:TextBox ID="txtDesde" runat="server" style="width:60px;cursor:pointer" Calendar="oCal" onchange="VerFecha('D');" goma=0 />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta&nbsp;&nbsp;
                    <asp:TextBox ID="txtHasta" runat="server" style="width:60px;cursor:pointer" Calendar="oCal" onchange="VerFecha('H');" goma=0 />
                 </td>
                <td colspan="2" class="colTabla">Planificación</td>
                <td colspan="4" class="colTabla">Periodo</td>
                <td colspan="4" class="colTabla1">Inicio proyecto -> Fin periodo</td>
            </tr>
		    <tr id="tblTitulo" class="TBLINI" align="center" style="height:17px; text-align:center;">
                <td style="text-align:left;">&nbsp;Proyecto económico
                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
								    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
	                <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event);"
									height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				</td>
                <td style="text-align:left;">&nbsp;Tarea
                    <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa4');"
									height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
	                <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa4',event);"
									height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
                </td>
                <td title="Horas planificadas para la tarea">H. Pl.</td>
                <td title="Horas previstas para el profesional en la tarea">H. Pr. P.</td>
                <td title="Horas planificadas en la agenda por el profesional para la tarea dentro del periodo">H. Agen.</td>
                <td title="Horas imputadas por el profesional a la tarea dentro del periodo">H. Prof.</td>
                <td title="Horas imputadas por otros profesionales a la tarea dentro del periodo">H. Otros</td>
                <td title="Total de horas imputadas a la tarea dentro del periodo">Total</td>
                <td title="Horas planificadas en la agenda por el profesional para la tarea desde el inicio del proyecto hasta el fin del periodo">H. Agen.</td>
                <td title="Horas imputadas por el profesional a la tarea desde el inicio del proyecto hasta el fin del periodo">H. Prof.</td>
                <td title="Horas imputadas por otros profesionales a la tarea desde el inicio del proyecto hasta el fin del periodo">H. Otros</td>
                <td title="Total de horas imputadas a la tarea desde el inicio del proyecto hasta el fin del periodo">Total&nbsp;</td>
		    </tr>
        </table>
		<div id="divCatalogo" style="overflow-y: auto; overflow-x: hidden; width: 1237px; height:420px">
            <div id="divCatalogoCI"style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 1221px;">
            <%=strTablaHTML%>
            </div>
        </div>
        <table id="tblResultado" style="width:1220px;">
            <colgroup>
                <col style="width:620px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
                <col style="width:60px" />
            </colgroup>
	        <tr class="TBLFIN" style="text-align:center; height:17px;">
                <td style="text-align:left;">&nbsp;<b>Totales</b></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
                <td style="text-align:right;"></td>
			</tr>
		</table>
    </td>
    </tr>
    <tr>
        <td style="width:750px;" rowspan="2">
            <table style="width:700px;margin-top:5px">
                <colgroup>
                    <col style="width:200px" />
                    <col style="width:300px"/>
                    <col style="width:100px"/>
                    <col style="width:100px"/>
                </colgroup>
		        <tr id="TR1" class="texto" style="height:24px">
                    <td style="text-align:left" colspan="2">
                        <img border="0" src="../../../Images/imgIcoMonedas.gif" class="ICO" />Tarea facturable&nbsp;&nbsp;&nbsp;
                        <img border="0" src="../../../Images/imgIcoMonedasOff.gif" class="ICO" />Tarea no facturable
                    </td>
                    <td class="colTabla1" align="center" colspan="2">Imputaciones registradas</td>
		        </tr>
		        <tr id="TR2" class="TBLINI">
                    <td>&nbsp;Tipología de proyecto</td>
                    <td>Naturaleza de producción</td>
                    <td style="text-align:center"><img id="moneda2" src='../../../images/imgIcoMonedas2.gif' width='16px' height='12' class='ICO'></td>
                    <td style="text-align:center"><img id="moneda2off" src='../../../images/imgIcoMonedasOff2.gif' width='16px' height='12' class='ICO'></td>
		        </tr>
            </table>
		    <div id="divCatalogo2" style="overflow:auto; width:716px; height:200px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 700px; ">
                <%=strTablaHTML2%>
                </div>
            </div>
            <table id="tblIndicadores" style="width:700px; text-align:right;">
                <colgroup>
                    <col style="width:200px" />
                    <col style="width:300px"/>
                    <col style="width:100px"/>
                    <col style="width:100px"/>
                </colgroup>
	            <tr class="TBLFIN" style="height:17px;">
                    <td></td>
                    <td><b>Total horas</b></td>
                    <td id="cldTotFact"></td>
                    <td id="cldTotNoFact" style="padding-right:2px;"></td>
			    </tr>
	            <tr class="TBLFIN" style="visibility:hidden; height:17px;"><!-- solo para la exportación a excel -->
                    <td></td>
                    <td><b>%</b></td>
                    <td id="cldPorFact"></td>
                    <td id="cldPorNoFact" style="padding-right:2px;"></td>
			    </tr>
		    </table> 
        </td>
        <td>
            <table id="tblGrafico" cellpadding="0" cellspacing="0" class="texto" style="width:250px; height:200px; margin-left:220px; margin-top:5px" visibility:hidden;">
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
                    <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <div id="chartdiv1">
                        <asp:CHART id="Chart1" runat="server" Palette="BrightPastel"  
                                        BackColor="243, 223, 193" Width="235px" Height="195px" BorderDashStyle="Solid" 
                                        BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" 
                                        BorderlineWidth="2" ImageStorageMode="UseImageLocation" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)">		                        <legends>
			                        <asp:Legend LegendStyle="Row" Enabled="False" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center"></asp:Legend>
		                        </legends>
		                        <borderskin skinstyle="Emboss"></borderskin>
		                        <series>
			                        <asp:Series XValueType="Double" Name="Default" ChartType="Doughnut" BorderColor="180, 26, 59, 105" ShadowOffset="5" Font="Trebuchet MS, 8.25pt, style=Bold" YValueType="Double"></asp:Series>
		                        </series>
		                        <chartareas>
			                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="" BorderWidth="0">
				                        <area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
				                        <axisy linecolor="64, 64, 64, 64">
					                        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
					                        <majorgrid linecolor="64, 64, 64, 64" />
				                        </axisy>
				                        <axisx linecolor="64, 64, 64, 64">
					                        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
					                        <majorgrid linecolor="64, 64, 64, 64" />
				                        </axisx>
			                        </asp:ChartArea>
		                        </chartareas>
	                        </asp:chart>
                        </div>
                        <!-- Fin del contenido propio de la página -->
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
                    <td colspan="3">
                        <center>
                        <br />
                        <table>                        
                        <tr>
                        <td>
                            <img border="0" src="../../../Images/imgIcoMonedas.gif" class="ICO" />Tarea facturable&nbsp;&nbsp;&nbsp;
                            <img border="0" src="../../../Images/imgIcoMonedasOff.gif" class="ICO" />Tarea no facturable
                        </td>
                        </tr>
                        </table> 
                        </center>                   
                    </td>
                <tr>
                </tr>
            </table>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "guia": 
				{
				    bEnviar = false;
				    setTimeout("mostrarGuia('ConsultaFacturabilidad.pdf');", 20);
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
-->
</script>
</asp:Content>

