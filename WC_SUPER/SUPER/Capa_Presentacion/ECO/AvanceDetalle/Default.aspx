<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="System.Configuration" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Detalle de avance</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link href="../../../../App_Themes/Corporativo/jquery-ui-1.8.17.custom.css" type="text/css" rel="stylesheet" />
    <link href="../../../PopCalendar/CSS/Classic.css" type="text/css" rel="stylesheet"/>
	<link href="Avance.css" type="text/css" rel="stylesheet"/>
	<script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.js" type="text/Javascript"></script>
	<script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
    <script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js?20180116" type="text/Javascript"></script>
    <script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" onload="init()" onunload="salir()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
        var nAnoMesActual = <%=nAnoMes %>;
        nModoLectura = <%=(bModoLectura)?"1":"0" %>;
        var nTareasAcumPrev = <%=nTareasAcumPrev %>;
        var origen = "<%=sOrigen %>";
        var sEsRtptEnAlgunPT = "<%=sEsRtptEnAlgunPT %>";
        var strHoy = "<%=DateTime.Now.ToShortDateString() %>";
        //Si sEsRtptEnAlgunPT=0, es que tiene alguna figura superior a RTPT, por lo que puede modificar todos los PTs.
        var bRes1024Eco = <%=((bool)Session["AVANCE1024"]) ? "true":"false" %>;
        var bRes1024Tec = <%=((bool)Session["AVANTEC1024"]) ? "true":"false" %>;
        var bRes1024Car = <%=((bool)Session["CARRUSEL1024"]) ? "true":"false" %>;
        var bRes1024Des = <%=((bool)Session["ESTRUCT1024"]) ? "true":"false" %>;
        var sEstadoProy = "<%=sEstado %>";
        var sEstadoMes = "<%=sEstadoMes %>";
        var bPermitirPasoProduccion = <%=(bPermitirPasoProduccion)?"true":"false" %>;
        //var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;
        //var sMOSTRAR_SOLODIS = "<%=ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"].ToString() %>";
        var sMONEDA_VDP = "<%=sMONEDA_VDP %>";
	    var sMonedaProyecto = "<%=sMonedaProyecto%>";
	    var sLabelMonedaProyecto = "<%=sLabelMonedaProyecto%>";
	    var sMonedaImportes = "<%=sMonedaImportes%>";
    </script>
	<br />
	<center>
    <table id="tblSuperior" style="width: 1230px; height:60px; margin-left:20px; text-align:left" cellpadding="0">
        <colgroup>
            <col style="width:75px;" />
            <col style="width:150px;" />
            <col style="width:360px;" />
            <col style="width:90px;" />
            <col style="width:555px;" />
        </colgroup>
        <tr style="height:29px;">
            <td>Proyecto&nbsp;&nbsp;<asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" /></td>
            <td colspan="2">
                <div id="divPry" style="width:500px; height:20px;">
                    <asp:TextBox ID="txtProyecto" style="width:480px;" Text="" readonly="true" runat="server" />
                </div>
            </td>
            <td rowspan="2" style="text-align:right;">
                <img id="imgCaution" src="../../../Images/imgCaution.gif" width="53" height="60" border=0 style="display:none;" runat="server" />
            </td>
            <td rowspan="2" style="vertical-align:bottom;">
                <asp:Image ID="imgFlecha" runat="server" Height="33" Width="350" ImageUrl="~/images/imgFactAni.gif" style="cursor:pointer; display:block;" onclick="copiarImportes()" ToolTip="Copia los valores acumulados IAP al total previsto para todas las tareas, cuando los consumos IAP superan las previsiones (esfuerzo total previsto, fecha de fin prevista)" />
            </td>
        </tr>
        <tr style="height:31px;">
            <td style="vertical-align:middle;" align="left" colspan="2">
                <label id="lblMostrarCerradas" for="chkCerradas" runat="server" class="texto" style="cursor:pointer; vertical-align:middle;">Mostrar tareas cerradas o anuladas</label><input type="checkbox" id="chkCerradas" runat="server" onclick="mostrarCerradas();" style="vertical-align:middle" />
            </td>
            <td>
                <div id="divMonedaImportes" runat="server" style="visibility:hidden">
                <label id="lblLinkMonedaImportes" class="enlace" style="vertical-align:bottom" onclick="getMonedaImportes()">Importes</label>&nbsp;&nbsp;<label style="vertical-align:top"> en </label>&nbsp;&nbsp;<label id="lblMonedaImportes" style="vertical-align:top" runat="server"></label>
                </div>Moneda proyecto: <label id="lblMonedaProyecto" runat="server"></label>
            </td>
        </tr>
    </table>
	<br />
    <table id="tblProyecto" style="width:1500px; margin-top:5px;  margin-left:20px; text-align:left" cellpadding="0">
	    <colgroup>
		    <col style="width:365px"/>
		    <col style="width:1135px"/>
	    </colgroup>	
		    <tr>
			    <td style="vertical-align:bottom;" align="left">
				    <div id="divTituloFijo" style="width: 365px;" runat="server">
				    <table id='tblTituloFijo' style="width: 365px; height: 34px; z-index:5;">
					    <colgroup>
						    <col style="width:40px;" />
						    <col style="width:265px;" />
						    <col style="width:60px;" />					
					    </colgroup>
    					
					    <tr style="height:17px">
						    <td><img id="imgNE1" src='../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"><img id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);"></td>
						    <td align="right" colspan="2">
							    <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
							    <asp:TextBox ID="txtMesVisible" style="width:90px; margin-bottom:5px; text-align:center;vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
							    <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
							    &nbsp;&nbsp;&nbsp;
						    </td>				
					    </tr>
					    <tr id="tblTitulo" class="TBLINI">
						    <td>&nbsp;</td>
						    <td align="left">Denominación&nbsp;
							    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',4,'divTituloFijo','imgLupa2','setBuscarDescriFija()')"
								    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"/> 
							    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',4,'divTituloFijo','imgLupa2', event,'setBuscarDescriFija()')"
								    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"/>
						    </td>
						    <td align="center">Estado</td>				
					    </tr>
				    </table>
				    </div>
			    </td>
			    <td style="vertical-align:bottom;" align="left">
				    <div id="divTituloMovil" style="overflow:hidden; width: 846px;" runat="server">
				    <table id="tblTituloMovil" style="width: 1135px; height: 34px; z-index:5;">
					    <colgroup>           		
						    <col style="width:70px"/>
						    <col style="width:60px"/>                    
						    <col style="width:60px"/>
						    <col style="width:100px"/>
    						
						    <col style="width:60px"/>
						    <col style="width:65px"/>
						    <col style="width:65px"/>
						    <col style="width:65px"/>
						    <col style="width:60px"/>
    						
						    <col style="width:70px"/>
						    <col style="width:70px"/>
						    <col style="width:60px"/>
						    <col style="width:40px"/>
    						
						    <col style="width:40px"/>
						    <col style="width:100px"/>
    						
						    <col style="width:50px"/>
						    <col style="width:50px"/>
						    <col style="width:50px"/>					
					    </colgroup>
    					
					    <tr class="texto" align="center">
						    <td colspan="4" class="colTabla">Planificado</td>
						    <td colspan="5" class="colTabla">IAP</td>
						    <td colspan="4" class="colTabla">Previsto</td>
						    <td colspan="2" class="colTabla">Avance</td>
						    <td colspan="3" class="colTabla1">Indicadores</td>		
					    </tr>		
					    <tr id="tblTitulo2" class="TBLINI" align="center">                   
						    <td>Total</td>
						    <td>Inicio</td>
						    <td>Fin</td>
						    <td><label id="lblPresupuesto" title="Importe presupuestado" style="width:100px">Imp. Presup.</label></td>
    						
						    <td>Mes</td>
						    <td><label id="lblAcum" title="Acumulado">Acumul.</label></td>
						    <td><label id="lblPendEst" title="Pendiente estimado">Pend. Est.</label></td>
						    <td><label id="lblTotEst" title="Total estimado">Total Est.</label></td>
						    <td><label id="lblFinEst" title="Fin estimado">Fin Est.</label></td>
    						
						    <td>Total</td>
						    <td>Pendiente</td>
						    <td>Fin</td>
						    <td>%</td>		
    						
						    <td>%</td>		
						    <td><label id="lblProducido" title="Importe producido" style="width:100px">Imp. Produc.</label></td>
    					
						    <td><label id="Label1" title="% Consumido: relación entres los esfuerzos consumidos y los planificados">% Con.</label></td>
						    <td><label id="Label2" title="% Desviación de esfuerzos: relación entre los esfuerzos previstos y planificados">% DE.</label></td>
						    <td><label id="Label4" title="% Desviación de plazos: relación entre los plazos previstos y planificados">% DP.</label></td>
					    </tr>
				    </table>
				    </div>
			    </td>
		    </tr>		
		    <tr>
			    <td style="vertical-align:top;">
				    <div id="divBodyFijo" style="width:365px; height:720px; overflow:hidden;" runat="server">
					    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:365px">
                        <%=strBodyFijoHTML%>
					    </div>
				    </div>				    
			    </td>
			    <td style="vertical-align:top;">
				    <div id="divBodyMovil" style="width:860px; height:720px; overflow-x:hidden;overflow-y:scroll;" runat="server" onscroll="setScroll();">
					    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:1135px">
					    <%=strBodyMovilHTML%>
					    </div>
				    </div>
			    </td>
		    </tr>	
		    <tr>
                <td style="vertical-align:top;">
                    <div id="divPieFijo" style="overflow:hidden; width: 365px;" runat="server">
                        <table id="tblPieFijo" style="width: 365px;">
                            <colgroup>
	                            <col style="width:365px"/>
                            </colgroup>
                            <tr class="TBLFIN" style="height:20px">
	                            <td style='text-align:left;'>&nbsp;&nbsp;Total proyecto</td>	
                            </tr>					
                        </table>
                    </div>                
			    </td>
			    <td style="vertical-align:top;">
					    <div id="divPieMovil" style="overflow-x:scroll;  overflow-y:hidden; width: 846px;" runat="server"  onscroll="setScrollPie();">
					        <table id="tblPieMovil" style="width: 1135px;">
					        <colgroup>
						        <col style="width:70px" />
						        <col style="width:60px" />
						        <col style="width:60px" />
						        <col style="width:100px" />
						        <col style="width:60px" />
						        <col style="width:65px" />
						        <col style="width:65px" />
						        <col style="width:65px" />
						        <col style="width:60px" />
						        <col style="width:70px" />
						        <col style="width:70px" />
						        <col style="width:60px" />
						        <col style="width:40px" />                  
						        <col style="width:40px" />
						        <col style="width:100px" />
						        <col style="width:50px" />                  
						        <col style="width:50px" /> 
						        <col style="width:50px" />                   
					        </colgroup>	            
					        <tr class="TBLFIN" style="height:20px">
						        <td align="right"><input id='txtPL' type='text' class="txtNumL" style='width:65px;' value='0,00' readonly runat="server"/></td>
						        <td><input id='txtInicioPL' type='text' class="txtNumL" style='width:55px;' value='' readonly runat="server"/></td>
						        <td><input id='txtFinPL' type='text' class="txtNumL" style='width:55px;' value='' readonly runat="server"/></td>
						        <td align="right"><input id='txtPrePL' type='text' class="txtNumL" style='width:65px;' value='0,00' readonly runat="server"/></td>
						        <td align="right"><input id='txtMes' type='text' class="txtNumL" style='width:55px;' value='0,00' readonly runat="server"/></td>
						        <td align="right"><input id='txtAcu' type='text' class="txtNumL" style='width:60px;' value='0,00' readonly runat="server"/></td>
						        <td align="right"><input id='txtPen' type='text' class="txtNumL" style='width:60px;' value='0,00' readonly runat="server"/></td>
						        <td align="right"><input id='txtEst' type='text' class="txtNumL" style='width:60px;' value='0,00' readonly runat="server"/></td>
						        <td><input id='txtFinEst' type='text' class="txtNumL" style='width:56px;' value='' readonly runat="server"/></td>
						        <td align="right"><input id='txtTotPR' type='text' class="txtNumL" style='width:65px;' value='0,00' readonly runat="server"/></td>
						        <td align="right"><input id='txtTotPenPR' type='text' class="txtNumL" style='width:65px;' value='0,00' readonly runat="server"/></td>
						        <td><input id='txtFinPR' name='txtFinPR' type='text' class="txtNumL" style='width:55px;' value='' readonly runat="server"/></td>
						        <td align="right"><input id='txtAV' type='text' class="txtNumL" style='width:35px;' value='0' readonly runat="server"/></td>		
						        <td align="right"><input id='txtAVPR' type='text' class="txtNumL" style='width:35px;' value='0' readonly runat="server"/></td>
						        <td align="right"><input id='txtPro' type='text' class="txtNumL" style='width:65px;' value='0,00' readonly runat="server"/></td>		
						        <td align="right"><input id='txtIndiCon' type='text' class="txtNumL" style='width:45px;' value='0' readonly runat="server"/></td>		
						        <td align="right"><input id='txtIndiDes' type='text' class="txtNumL" style='width:45px;' value='0' readonly runat="server"/></td>		
						        <td align="right"><input id='txtIndiDesPlazo' type='text' class="txtNumL" style='width:45px;' value='0' readonly runat="server"/></td>		
					        </tr>
				        </table>
				    </div>					    			    
			    </td>
		    </tr>		
    </table>
    </center>
    <center>   
	<table id="tblBotones" style="width:1120px; margin-bottom:5px; margin-top:10px;">
        <colgroup>
            <col style="width:140px" />
            <col style="width:120px" />
            <col style="width:120px" />
            <col style="width:130px" />
            <col style="width:120px" />
            <col style="width:130px" />
            <col style="width:120px" />
            <col style="width:120px" />
            <col style="width:120px" />
        </colgroup>				
		<tr>
			<td>
				<button id="btnAvanTec" type="button" onclick="$I('divMasivo').style.display='none';setAvanTec()" class="btnH25W120" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgTraspaso.gif" />
					<span title="Realiza el traspaso del avance técnico de la producción">Avan.Técnico</span>
				</button>	
			</td>						
			<td>
				<button id="btnPasoProd" type="button" onclick="$I('divMasivo').style.display='none';pasoAProduccion()" class="btnH25W105" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgTraspaso.gif" />
					<span title="Realiza el traspaso de la producción al módulo económico">Producción</span>
				</button>	
			</td>						
			<td>
				<button id="btnTraspaso" type="button" onclick="$I('divMasivo').style.display='none';realizarTraspaso()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgTraspaso.gif" />
					<span title="Copia el esfuerzo total estimado y la fecha de fin estimada en IAP al esfuerzo total previsto y la fecha de fin prevista">&nbsp;&nbsp;Mover</span>
				</button>	
			</td>						
			<td>
				<button id="btnIndicaciones" type="button" onclick="$I('divMasivo').style.display='none';Indicaciones()" class="btnH25W110" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgIndicaciones.gif" />
					<span title="Permite generar unas indicaciones a un profesional en una tarea">Indicación</span>
				</button>	
			</td>						
			<td>
				<button id="btnMasivo" type="button" onclick="$I('divMasivo').style.display='block';" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgMasivo.gif" />
					<span title="Genera un informe Excel con la información de la pantalla">Masivo</span>
				</button>	
			</td>						
			<td>
				<button id="btnInstantanea" type="button" onclick="$I('divMasivo').style.display='none';generarFoto()" class="btnH25W110" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgInstantanea.gif" />
					<span title="Genera una instantánea con la situación actual del proyecto">Instantánea</span>
				</button>	
			</td>						
			<td>
				<button id="btnGrabar" type="button" onclick="$I('divMasivo').style.display='none';grabar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
				</button>	
			</td>
			<td>
				<button id="btnGrabarSalir" type="button" onclick="$I('divMasivo').style.display='none';grabarSalir()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
				</button>	
			</td>						
			<td>
				<button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
				</button>	 
			</td>
		</tr>
	</table>
</center>    
<center>
    <div id="divMasivo" runat="server" style="position: absolute; top:420px; left:400px; display:none"  class="texto">
    <center>
    <table style="width:350px; border:1px solid #5894ae; background-color:#bcd4df; text-align:left;" class="texto">
	    <tr>
		    <td><b><font size="3">Exportación masiva</font></b></td>
	    </tr>
	    <tr>
	        <td>
                <table style="width:100%; margin-top:15px; text-align:left; background-color:#D8E5EB;" class="texto">
                    <tr>
                        <td>
                            <asp:radiobuttonlist id="rdbMasivo" runat="server" SkinId="rbl" style="width:200px;margin-left:65px; margin-top:15px;" RepeatDirection="vertical" RepeatLayout="Table">
                                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbMasivo_0').click();" Selected="True" Value="1">Sólo profesionales con consumos</asp:ListItem>
                                <asp:ListItem style="cursor:pointer;" onclick="$I('rdbMasivo_1').click();" Value="2">Todos los profesionales asignados</asp:ListItem>
                            </asp:radiobuttonlist> 
                         </td> 
                    </tr>			
	                <tr>
	                    <td>
                            <table style="width:220px; margin-top:25px; margin-left:70px;">
                                <tr>
	                                <td>
		                                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
		                                <img src="../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
	                                </td>
	                                <td>
		                                <button id="btnSalir" type="button" onclick="$I('divMasivo').style.display='none'" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
		                                <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
	                                </td>
                                </tr>
                            </table>
	                    </td>
	                </tr>
                </table>
	        </td>
	    </tr>
    </table>
    </center>
    </div> 	
    <div id="divSetAvan" title="Confirmación de paso a producción del avance técnico" style="display:none; width:560px; height:400px; font-size:larger;">
        Se van a trasladar <label id="lblAvanNew"></label> a Producción por avance técnico PST del mes actual.
        <br /><br />
        <span id ="spnAvanOld">Se reemplazarán los <label id="lblAvanOld"></label> actualmente registrados.</span>
        <br />
    </div>

</center>
    <asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnNivelPresupuesto" runat="server" style="visibility:hidden" Text="" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" id="hdnAvanTecOld" value="0" />
    <input type="hidden" id="hdnAvanTecNew" value="0" />
    
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
</body>
</html>
