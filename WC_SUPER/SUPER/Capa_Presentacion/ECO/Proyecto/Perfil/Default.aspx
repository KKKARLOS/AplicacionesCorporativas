<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1"  runat="server">
    <title> ::: SUPER ::: - Asignación de perfiles de usuarios en tareas</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <style type="text/css">
        #tsPestanas table { table-layout:auto; }    
        #tblTareas TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
        .textoG
            {
                font-weight: normal;
                font-size: 13px;
                color: #000000;
                font-family: Arial, Helvetica, sans-serif;
                text-decoration: none;
            }
    </style>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var sNodo = "<%=sNodo %>";
</script>
<br />
<center>
<label class="textoG">
    <b>SUPER ofrece tres opciones de asignación masiva de perfiles a profesionales. Seleccione la más adecuada a sus necesidades.</b>
</label>
<br /><br />
<table class="texto" style="width:980px;">
    <tr>
        <td>
            <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="970px" 
                            MultiPageID="mpContenido" 
                            ClientSideOnLoad="CrearPestanas" 
                            ClientSideOnItemClick="getPestana">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
		                    <eo:TabItem Text-Html="Opción 1" Width="100"></eo:TabItem>
		                    <eo:TabItem Text-Html="Opción 2" Width="100"></eo:TabItem>
		                    <eo:TabItem Text-Html="Opción 3" Width="100"></eo:TabItem>
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
            <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="970px" Height="580px">
                <eo:PageView id="PageView1" CssClass="PageView" runat="server">
				<!-- Pestaña 1 -->
			    <br />
			    <fieldset style="width:820px; margin-left:70px;">
			        <table style="width:810px; text-align:left;" class="textoG">
			            <colgroup>
			                <col style="width:300px;" /><col style="width:185px;" />
			                <col style="width:105px;" /><col style="width:130px;" />
			                <col style="width:90px;" />
			            </colgroup>
			            <tr>
			                <td colspan="2">Aplicar el perfil asignado por defecto a cada profesional, en las tareas en las que</td>
			                <td>
                                <asp:RadioButtonList ID="rdbTengan" CssClass="textoG rbl" runat="server" style="display:inline;">
                                    <asp:ListItem Value="1" Text="tengan"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="no tengan" Selected="True""></asp:ListItem>
                                    <asp:ListItem Value="3" Text="tengan o no"></asp:ListItem>
                                </asp:RadioButtonList>
			                </td>
			                <td>perfil asignado</td>
			                <td rowspan="2">
								<button id="tblGrabar" type="button" onclick="grabarAux()" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
									<img src="../../../../images/botones/imgProcesar.gif" /><span title="Ejecutar">Ejecutar</span>
								</button>  
			                </td>
			            </tr>
			            <tr>
			                <td>y se encuentren en uno de los siguientes estados</td>
			                <td colspan="4">
                                <table class="textoG" style="width:190px; display:inline;" cellpadding="3">
                                    <colgroup>
                                        <col style="width:90px;" />
                                        <col style="width:100px;" />
                                    </colgroup>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="chkAct1" class="check" onclick="verifEst(1);"/>
                                            <label id="Label1">Activa</label>
                                        </td>
                                        <td>
                                            <input type="checkbox" id="chkPar1" class="check" onclick="verifEst(1);"/>
                                            <label id="Label2">Paralizada</label>
                                        </td>
                                     </tr>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="chkCer1" class="check" onclick="verifEst(1);"/>
                                            <label id="Label3">Cerrada</label>
                                        </td>
                                        <td>
                                            <input type="checkbox" id="chkFin1" class="check" onclick="verifEst(1);"/>
                                            <label id="Label4">Finalizada</label>
                                        </td>
                                    </tr>
                                </table>
			                </td>
			            </tr>
			        </table>
			    </fieldset>
			    <table style="width:720px; text-align:left; margin-left:130px;" class="texto">
			        <tr>
			            <td>
                            <table id="tblTitulo" style="WIDTH:700px; HEIGHT:17px; margin-top:5px;">
                                <colgroup><col style='width:460px;' /><col style='width:240px;' /></colgroup>
	                            <tr class="TBLINI">
			                        <td style='padding-left:10px;'>
			                            <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCod" border="0"> 
			                            <MAP name="imgCod">
				                            <AREA onclick="ot('tblDatos', 1, 0, '', 'scrollTablaAE()')" shape="RECT" coords="0,0,6,5">
				                            <AREA onclick="ot('tblDatos', 1, 1, '', 'scrollTablaAE()')" shape="RECT" coords="0,6,6,11">
			                            </MAP>					
			                            &nbsp;Profesional</td>
			                        <td><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgBono" border="0"> 
			                            <MAP name="imgBono">
				                            <AREA onclick="ot('tblDatos', 2, 0, '', 'scrollTablaAE()')" shape="RECT" coords="0,0,6,5">
				                            <AREA onclick="ot('tblDatos', 2, 1, '', 'scrollTablaAE()')" shape="RECT" coords="0,6,6,11">
			                            </MAP>					
			                            &nbsp;Perfil por defecto</td>
	                            </tr>
                            </table>
                            <div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 716px; height:360px" onscroll="scrollTablaAE();">
                                <div style='background-image:url(../../../../Images/imgFT20.gif); width:700px; height:auto'>
                                    <%=strTablaHtml %>
                                </div>
                            </DIV>
                            <table id="Table3" style="WIDTH: 700px; HEIGHT: 17px">
	                            <tr class="TBLFIN"><td></td></tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top:5px;">
                            <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                            <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
                            <label id="lblForaneo" runat="server">Foráneo</label>
                        </td> 
                    </tr>
                </table>
			    </eo:PageView>
			    <eo:PageView id="PageView2" CssClass="PageView" runat="server">
			    <!-- Pestaña 2 -->
			        <table class="textoG" style="width: 960px; text-align:left;">
			            <colgroup><col style="width:400px;" /><col style="width:360px;" /><col style="width:200px;" /></colgroup>
			            <tr>
			                <td colspan="3" style="padding-top:10px;">
			                <fieldset style="width:930px;">
                                <asp:RadioButtonList ID="rdbAplicar1" CssClass="textoG rbl" runat="server" onclick="setOpcPerfil(1)">
                                    <asp:ListItem Value="1" style="cursor:pointer;" Text="Aplicar al profesional seleccionado, en las tareas marcadas, el perfil por defecto que tenga asociado (opción válida cuando exista perfil por defecto)"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RadioButtonList ID="rdbAplicar2" CssClass="textoG rbl" runat="server" style="display:inline;" onclick="setOpcPerfil(2)">
                                    <asp:ListItem Value="2" style="vertical-align:middle;cursor:pointer" Selected="True" Text="Aplicar el perfil"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:DropDownList id="cboPerfil1" runat="server" style="margin-left:5px; width:140px;" onchange="setOpcPerfil(2)">
                                    <asp:ListItem Value="-1" Text="" Selected="True"></asp:ListItem>
                                </asp:DropDownList>    
                                <span style="height:18px;display:inline;" class="textoG">al profesional seleccionado en las tareas marcadas</span>
							    <button id="btnGrabar2" type="button" onclick="grabarAux()" class="btnH25W85" style="display:inline; margin-left:280px;" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
								    <img src="../../../../images/botones/imgProcesar.gif" /><span title="Ejecutar">Ejecutar</span>
							    </button>  
                                 <br />
                                 <asp:RadioButtonList ID="rdbAplicar3" CssClass="textoG rbl" runat="server" style="display:inline;" onclick="setOpcPerfil(3)">
                                    <asp:ListItem Value="3" style="cursor:pointer;" Text="Aplicar el perfil"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:DropDownList id="cboPerfil2" runat="server" style="margin-left:5px; width:140px;" onchange="setOpcPerfil(3)">
                                    <asp:ListItem Value="-1" Text="" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <span style="height:18px;" class="textoG">    
                                    al profesional seleccionado en las tareas marcadas en las que tuviera el perfil
                                </span>
                                <asp:DropDownList id="cboPerfil3" runat="server" style="margin-left:5px; width:140px;" onchange="setOpcPerfil(3)">
                                    <asp:ListItem Value="-1" Text="" Selected="True"></asp:ListItem>
                                </asp:DropDownList>   
                                <br /><br /> 
			                </fieldset>
			            </td>
			            </tr>
			            <tr>
			                <td style="padding-top:15px; padding-right:40px; text-align:justify">
			                Seleccione un profesional de la lista de abajo, para que se muestren las tareas en las que está asignado.
			            </td>
			                <td style="padding-top:15px; padding-right:30px; text-align:justify;">
		                    <label>
		                    Mostrar únicamente tareas que se encuentren en alguno de los estados marcados
		                    </label>
			            </td>
			                <td style="padding-top:15px;">
		                    <input type="checkbox" id="chkAct2" class="check" onclick="getEstructura3();" />
                            <label id="Label5">Activa</label>
		                    <input type="checkbox" id="chkPar2" class="check" style="margin-left:20px;" onclick="getEstructura3();"/>
                            <label id="Label6">Paralizada</label>
                            <br />
		                    <input type="checkbox" id="chkCer2" class="check" onclick="getEstructura3();"/>
                            <label id="Label7">Cerrada</label>
		                    <input type="checkbox" id="chkFin2" class="check" style="margin-left:10px;" onclick="getEstructura3();"/>
                            <label id="Label8">Finalizada</label>
			            </td>
			            </tr>
			            <tr>
			                <td>
                            <table id="tblCabProf2" style="WIDTH: 370px; HEIGHT: 17px; margin-top:5px;">
                                <colgroup><col style='width:230px;' /><col style='width:140px;' /></colgroup>
	                            <tr class="TBLINI">
			                        <td style="padding-left:20px;">
			                            <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCod2" border="0"> 
			                            <MAP name="imgCod2">
				                            <AREA onclick="ot('tblProf2', 1, 0, '', 'scrollProf2()')" shape="RECT" coords="0,0,6,5">
				                            <AREA onclick="ot('tblProf2', 1, 1, '', 'scrollProf2()')" shape="RECT" coords="0,6,6,11">
			                            </MAP>					
			                            &nbsp;Profesional</td>
			                        <td>
			                            <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgPerf2" border="0"> 
			                            <MAP name="imgPerf2">
				                            <AREA onclick="ot('tblProf2', 2, 0, '', 'scrollProf2()')" shape="RECT" coords="0,0,6,5">
				                            <AREA onclick="ot('tblProf2', 2, 1, '', 'scrollProf2()')" shape="RECT" coords="0,6,6,11">
			                            </MAP>					
			                            &nbsp;Perfil por defecto</td>
	                            </tr>
                            </table>
                            <div id="divProf2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 386px; height:340px" onscroll="scrollProf2();">
                                <div style='background-image:url(../../../../Images/imgFT20.gif); width:370px; height:auto'>
                                </div>
                            </div>
                            <table id="Table2" style="WIDTH: 370px; HEIGHT: 17px">
	                            <tr class="TBLFIN"><td></td></tr>
                            </table>
			            </td>
			                <td colspan="2">
                            <table id="tblCabTareas" style="WIDTH: 540px; HEIGHT: 17px; margin-top:5px;">
                                <colgroup><col style='width:390px;' /><col style='width:36px;' /><col style='width:114px;' /></colgroup>
	                            <tr class="TBLINI">
			                        <td>					
			                            &nbsp;Denominación&nbsp;
				                        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblTareas',0,'divTareas','imgLupa2');"
									                    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
	                                    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblTareas',0,'divTareas','imgLupa2', event);"
									                    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
			                        </td>
			                        <td title="Marcar / desmarcar todas las tareas para su procesamiento">
                                        <img id="imgMarcar" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="setChecks(1)" />
                                        <img id="imgDesmarcar" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="setChecks(0)" />
			                        </td>
			                        <td>					
			                            &nbsp;Perfil
		                            </td>
	                            </tr>
                            </table>
                            <div id="divTareas" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 556px; height:340px" onscroll="scrollTareas();">
                                <div style='background-image:url(../../../../Images/imgFT20.gif); width:540px; height:auto'>
                                </div>
                            </div>
                            <table id="Table4" style="WIDTH: 540px; HEIGHT: 17px;">
	                            <tr class="TBLFIN"><td></td></tr>
                            </table>
			            </td>
			            </tr>
			            <tr>
                            <td style="padding-top:5px;" class="texto">
                            <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                            <img id="imgForaneo2" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
                            <label id="lblForaneo2" runat="server">Foráneo</label>
                        </td>
                        <td colspan=2> 
			            </td>
			        </tr>
			        </table>
			    </eo:PageView>
			    <eo:PageView id="PageView3" CssClass="PageView" runat="server">
			    <!-- Pestaña 3 -->
			    <br />
			    <fieldset style="width:815px; margin-left:60px;">
			        <table style="width: 815px;" class="textoG">
			            <colgroup><col style="width:715px;" /><col style="width:100px;" /></colgroup>
			            <tr>
			                <td>
			                    Aplicar el perfil 
                                <asp:DropDownList id="cboPerfil4" runat="server" style="margin-left:5px; width:140px;">
                                </asp:DropDownList>   
			                    &nbsp;a los profesionales marcados en todas las tareas en las que se encuentren  
		                    </td>
		                    <td rowspan="2">
							    <button id="btnGrabar3" type="button" onclick="grabarAux()" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
								    <img src="../../../../images/botones/imgProcesar.gif" /><span title="Ejecutar">Ejecutar</span>
							    </button>  
		                    </td>
	                    </tr>
			            <tr>
			            <td>
			                asignados y que estén en uno de los siguientes estados:
			                <input type="checkbox" id="chkAct3" class="check" style="margin-left:12px;" onclick="verifEst(3);"/>
                            <label id="Label9">Activa</label>
			                <input type="checkbox" id="chkPar3" class="check" style="margin-left:15px;"onclick="verifEst(3);"/>
                            <label id="Label10">Paralizada</label>
			                <input type="checkbox" id="chkCer3" class="check" style="margin-left:15px;"onclick="verifEst(3);"/>
                            <label id="Label11">Cerrada</label>
			                <input type="checkbox" id="chkFin3" class="check" style="margin-left:15px;"onclick="verifEst(3);"/>
                            <label id="Label12">Finalizada</label>
		                </td>
	                </tr>
	                </table>
	            </fieldset>
	            <table style="width: 460px; margin-top:10px; margin-left:230px; text-align:left;" class="texto">
			        <tr>
			            <td>
                            <table id="tblCabProf3" style="WIDTH: 440px; HEIGHT: 17px; margin-top:10px;">
                                <colgroup><col style='width:40px;' /><col style='width:400px;' /></colgroup>
	                            <tr class="TBLINI">
			                        <td title="Marcar / desmarcar todas los profesionales para su procesamiento">
                                        <img id="img1" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="setChecksProf(1)" />
                                        <img id="img2" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="setChecksProf(0)" />
			                        </td>
			                        <td>
			                            <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCod3" border="0"> 
			                            <MAP name="imgCod3">
				                            <AREA onclick="ot('tblProf3', 2, 0, '', 'scrollProf3()')" shape="RECT" coords="0,0,6,5">
				                            <AREA onclick="ot('tblProf3', 2, 1, '', 'scrollProf3()')" shape="RECT" coords="0,6,6,11">
			                            </MAP>					
			                            &nbsp;Profesional</td>
	                            </tr>
                            </table>
                            <div id="divProf3" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 456px; height:400px" onscroll="scrollProf3();">
                                <div style='background-image:url(../../../../Images/imgFT20.gif); width:440px; height:auto'>
                                </div>
                            </div>
                            <table id="Table5" style="WIDTH: 440px; HEIGHT: 17px;">
	                            <tr class="TBLFIN"><td></td></tr>
                            </table>
			            </td>
			        </tr>
			        <tr>
                        <td style="padding-top:5px;">
                        <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                        <img id="imgForaneo3" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
                        <label id="lblForaneo3" runat="server">Foráneo</label>
                    </td>
			        </tr>
			    </table>
                </eo:PageView>
            </eo:MultiPage>
        </td>
    </tr>
</table>
<br />
<table class="texto" style="width:100px;">
	<tr> 
		<td>
			<button id="tblSalir" type="button" onclick="salir()" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
			</button>  
		</td>
	  </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnPSN" id="hdnPSN" value="" />
<input type="hidden" runat="server" name="hdnCualidad" id="hdnCualidad" value="" />
<input type="hidden" runat="server" name="hdnEstadoPSN" id="hdnEstadoPSN" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
		function __doPostBack(eventTarget, eventArgument) {
			var bEnviar = true;
//			if (eventTarget == "Botonera"){
//				var strBoton = $I("Botonera").botonID(eventArgument).toLowerCase();
//				//alert("strBoton: "+ strBoton);
//				switch (strBoton){
//					case "regresar": //Boton Anadir
//					{
//					    comprobarGrabarOtrosDatos();
//						bEnviar = true;
//						break;
//					}
//				}
//			}

			var theform;
			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
				theform = document.forms[0];
			}
			else {
				theform = document.forms["frmPrincipal"];
			}
			
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.__EVENTARGUMENT.value = eventArgument;
			if (bEnviar){
				theform.submit();
			}
			else{
				$I("Botonera").restablecer();
			}
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

