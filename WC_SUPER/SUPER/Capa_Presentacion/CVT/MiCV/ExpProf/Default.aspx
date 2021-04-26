<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Experiencia profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?v=20180326" type="text/Javascript"></script>    
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
</head>
<body onload="init(); " onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    //Para el comportamiento de los calendarios
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
    var bHayExperiencias = <%=sHayExperiencias %>;
    var bECV = <%=(User.IsInRole("ECV"))? "true":"false" %>;
</script>
<br />
<table cellspacing="0" class="texto" style="width: 950px; table-layout:fixed; margin-left:10px;" border="0">
    <colgroup>
        <col style="width:50%" />
        <col style="width:50%" />
    </colgroup>
    <tr>
        <td>
            <label style="width:70px; position:absolute; top:30px; left:15px;">Denominación</label><label style="color:Red; position:absolute; top:30px; left:82px;">*</label>
            <asp:TextBox ID="txtDen" maxlength='70' style="width:380px; position:absolute; top:30px; left:90px;" Text="" runat="server" />
            <asp:Image ID="imgInfoExpProy" runat="server" ImageUrl="~/Images/imgInfoExpProy.png" style="position:absolute; top:80px; left:35px; cursor:pointer;" onclick="getProyExp()" Visible="false" ToolTip="Muestra la relación de proyectos asociados a la experiencia profesional." />
        </td>
        <td rowspan="3">
            <fieldset style="position:relative; margin-left:10px;">
            <legend><label style="color:Red;">*</label>Áreas de conocimiento tecnológico</legend>
                <table id="tblCab1" style="width: 420px; height: 17px;margin-top:5px; margin-left:10px;" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td>
                            &nbsp;&nbsp;Áreas
                        </td>
                    </tr>
                </table>
	            <div id="divConTec" style="overflow: auto; width: 436px; height:64px; margin-left:10px;">
	                <div style='background-image:url(../../../../Images/imgFT16.gif); width:420px;'>
	                    <%=strTablaHtmlConTec %>
	                </div>
                </div>
	            <table id="Table1" style="width:420px; height:17px; margin-left:10px;" cellspacing="0" border="0">
		            <tr class="TBLFIN"><td></td></tr>
	            </table>
	            <table style="width:300px; margin-top:5px; margin-left:100px;">
	            <tr>
		            <td width="50%">
			            <button id="btnNuevo" type="button" onclick="nuevoACT()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			            </button>	
		            </td>
		            <td width="50%">
			            <button id="btnCancelar" type="button" onclick="EliminarACT()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			            </button>	
		            </td>
	            </tr>
	            </table>    
            </fieldset>
            <fieldset style="position:relative; margin-left:10px;">
            <legend><label style="color:Red;">*</label>Áreas de conocimiento sectorial</legend>
                <table id="Table2" style="width: 420px; height: 17px; margin-top:5px; margin-left:10px;" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td>
                            &nbsp;&nbsp;Áreas
                        </td>
                    </tr>
                </table>
	            <div id="divConSec" style="overflow: auto; width: 436px; height:64px; margin-left:10px;">
	                <div style='background-image:url(../../../../Images/imgFT16.gif); width:420px;'>
	                    <%=strTablaHtmlConSec %>
	                </div>
                </div>
	            <table id="Table4" style="width:420px; height:17px; margin-left:10px;" cellspacing="0" border="0">
		            <tr class="TBLFIN"><td></td></tr>
	            </table>
	            <table style="width:300px; margin-top:5px; margin-left:100px;">
	            <tr>
		            <td width="50%">
			            <button id="Button1" type="button" onclick="nuevoACS()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			            </button>	
		            </td>
		            <td width="50%">
			            <button id="Button2" type="button" onclick="EliminarACS()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			            </button>	
		            </td>
	            </tr>
	            </table>    
            </fieldset>
        </td>
    </tr>
    <tr>
        <td style="height:130px;">
            <label id="lblDes" style="width:70px; position:absolute; top:60px; left:15px;" runat="server">Descripción</label><label style="color:Red;position:absolute; top:60px; left:75px;">*</label>
            <asp:TextBox ID="txtDes" SkinID="Multi" TextMode="MultiLine" 
                style="width:380px; height:225px; position:absolute; top:60px; left:90px;" Text="" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <label id="lblProyRef" runat="server" style="width:70px; position:absolute; top:300px; left:15px;" title="Asocia la experiencia actual con otra anterior del mismo cliente">Asociar</label>
            <asp:TextBox ID="txtProyRef" style="width:380px; position:absolute; top:300px; left:90px;" Text="" runat="server" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <fieldset>
            <legend>Profesionales</legend>
	        <table id="tblTitulo" style="width: 915px; table-layout:fixed; height: 17px;margin-top:5px;" cellspacing="0" border="0">
                <colgroup>
                 <col style='width:15px;' />
                 <col style='width:20px;' />
                 <col style='width:300px;' />
                 <col style='width:70px;' />
                 <col style='width:70px;' />
                 <col style='width:70px;' />
                 <col style='width:80px;' />
                 <col style='width:80px;' />
                 <col style='width:80px;' />
                 <col style='width:130px;' />
                 <%--<col style='width:120px;' />--%>
                 <col style='width:0px;' />
                </colgroup>
		        <tr class="TBLINI">
		            <td></td>
		            <td></td>		            
					<td>
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblProf',3,'divCatalogo','imgLupa1')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20px" tipolupa="2">
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblProf',3,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20px" tipolupa="1"> 
						<IMG style="CURSOR: pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblProf', 2, 0, '', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblProf', 2, 1, '', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Profesional&nbsp;&nbsp;
					</td>		            
				    <td title="Primer mes en el que hay imputaciones IAP">Pri. Impu.</td>
				    <td title="Último mes en el que hay imputaciones IAP">Ult. Impu.</td>
				    <td title="Jornadas IAP imputadas" style="text-align:right; padding-right:5px;">Jor. Impu.</td>
				    <td title="Fecha de alta del profesional en la experiencia">FAPE</td>
				    <td title="Fecha de baja del profesional en la experiencia">FBPE</td>
				    <td title="Asignación de profesionales a la experiencia profesional (ctrl o mayús/shift para selección múltiple)"><label class="enlace" onclick="getCV()">CV</label></td>
				    <td title="Asignación de una plantilla a profesionales (ctrl o mayús/shift para selección múltiple)"><label class="enlace" onclick="getPlantilla()">Perfil/Plantilla</label></td>
<%--				    <td title="Indica el responsable de la experiencia (ctrl o mayús/shift para selección múltiple)"><label class="enlace" onclick="getValidador()">Responsable CVT</label></td>--%>
                    <td></td>
		        </tr>
	        </table>
	        <div id="divCatalogo" style="overflow: auto; width: 931px; height:154px" onscroll="scrollTablaProf();">
	            <div style='background-image:url(../../../../Images/imgFT22.gif); width:915px;'>
	                <%=strTablaHtml %>
	            </div>
            </div>
	        <table id="Table3" style="width: 915px; border-collapse: collapse; height: 17px" cellspacing="0" border="0">
		        <tr class="TBLFIN"><td></td></tr>
	        </table>
	        <table id="Table5" style="width: 915px; border-collapse: collapse;" cellspacing="0" border="0">
		        <%--<tr>
                    <td style="padding-top:5px;">
                        <label style="width:150px;" class="enlace" onclick="getPlantillas()" id="lblPlantillasAsociadas" runat="server">Plantillas asociadas</label>
                    </td> 
		        </tr>--%>
		        <tr>
                    <td style="padding-top:5px;visibility:hidden">
                        <img id="imgAviso" src="../../../../Images/imgWarning.png" alt=""/>
                        <label id="lgnInfo" runat="server">
                            <%-- Una plantilla supone trasladar un perfil profesional a los CV&#39;s de los profesionales. 
                            Las plantillas no pueden ser modificadas por los profesionales ni requieren la figura del Validador
                            --%>
                            &nbsp;&nbsp;La asignación de un plantilla es incompatible con indicar un Responsable. Una plantilla supone el traslado de información al CV del profesional.
                        </label>
                    </td> 
		        </tr>
	        </table>
	        </fieldset>
        </td>
    </tr>
    <tr>
        <td style="padding-top:5px;">
            <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo (ETT)
        </td> 
        <td style="padding-top:5px;text-align:right;vertical-align:middle" >
            <img class="ICO" src="../../../../Images/imgOrdenF.gif" />&nbsp;Perfil desde plantilla&nbsp;
            <img class="ICO" src="../../../../Images/imgProyTecOff.gif" />&nbsp;Perfil desde el CV&nbsp;
            <img class="ICO" src="../../../../Images/imgOpcional.gif" />&nbsp;Sin perfil
        </td> 

    </tr>
</table>
<table id="tblBotones" style="width:500px; margin-left:230px; margin-top:2px;">
    <tr>
	    <td align="center">
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span>&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td align="center">
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			</button>
	    </td>	
        <td align="center">
		    <button id="btnGuia" type="button" onclick="mostrarGuia('ExperienciaProfesional.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/botones/imgGuia.gif" /><span>&nbsp;&nbsp;Guía</span>
		    </button>	
        </td>				
	    <td align="center">
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span>&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnOri" id="hdnOri" value="" />
<input type="hidden" runat="server" name="hdnPR" id="hdnPR" value="-1" />
<input type="hidden" runat="server" name="hdnEP" id="hdnEP" value="-1" />
<input type="hidden" runat="server" name="hdnModo" id="hdnModo" value="R" />
<input type="hidden" runat="server" name="hdnCtaOri" id="hdnCtaOri" value="-1" />
<input type="hidden" runat="server" name="hdnCtaDes" id="hdnCtaDes" value="-1" />
<input type="hidden" runat="server" name="hdnCliProy" id="hdnCliProy" value="-1" />
<input type="hidden" runat="server" name="hdnEmp" id="hdnEmp" value="-1" />
<input type="hidden" runat="server" name="hdnEnIb" id="hdnEnIb" value="S" />
<input type="hidden" name="nIdProy" id="nIdProy" value="<%=nIdProy %>" />
<input type="hidden" runat="server" name="hdnSelPR" id="hdnSelPR" value="N" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>

<script type="text/javascript">
	<!--
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
    	
    -->
</SCRIPT>
</body>
</html>

