<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblResultado TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var nIndiceProy = -1;
    //var aFila;
    var aProy = new Array();
    var bLectura = false;
    var bRTPT = false;
    var nAnoMesActual = <%=DateTime.Now.Year * 100 + DateTime.Now.Month %>;
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var modolectura_proyectosubnodo_actual = <%=((bool)Session["MODOLECTURA_PROYECTOSUBNODO"])? "true":"false" %>;
    var bRTPT_proyectosubnodo_actual = <%=((bool)Session["RTPT_PROYECTOSUBNODO"])? "true":"false" %>;
    var bMostrarMsg = true;
    var strHoy = "<%=DateTime.Now.ToShortDateString() %>";
    var str1DiaMes = "<%=DateTime.Now.AddDays(DateTime.Now.Day * -1 + 1).ToShortDateString() %>";
    var bRes1024 = <%=((bool)Session["ESTRUCT1024"]) ? "true":"false" %>;
    //Para el comportamiento de los calendarios
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
    var bEstrCompleta = <%=((bool)Session["CARGAESTRUCTURA"]) ? "true":"false" %>;
</script>
<ucgus:UCGusano ID="UCGusano1" runat="server" />
<div id="div1024" style="Z-INDEX: 105; WIDTH: 32px; HEIGHT: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" Width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<table id="tblCab" style="width:1190px;text-align:left" cellspacing="2">
    <colgroup>
        <col style="width:60px;" />
        <col style="width:20px;" />
        <col style="width:80px;" />
        <col style="width:540px;" />
        <col style="width:100px;" />
        <col style="width:305px;" />
        <col style="width:85px;" />
    </colgroup>
    <tr>
        <td>
            <label id="lblProy" runat="server" title="Proyecto económico" style="width:50px; padding-left:5px;" class="enlace" onclick="obtenerProyectos()">Proyecto</label>
        </td>
        <td>
            <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
        </td>
        <td align="center">
            <asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:60px;" SkinID="Numero" onkeypress="javascript:if(event.keyCode==13){buscarPE();event.keyCode=0;}else{vtn2(event);setNumPE();}" />
        </td>
        <td>
            <asp:TextBox ID="txtNomProy" runat="server" Text="" style="width:510px;" readonly="true" />
        </td>
        <td>
            <fieldset id="fstObtencion" style="width: 80px; text-align:left; height:45px;" 
                title="'En bloque' trae de BBDD toda la información de la estructura técnica del proyecto.&#13;'A demanda' va trayendo de BBDD la parte inmediatamente inferior que depende del elemento seleccionado.&#13;&#13;En proyectos con una gran estructura técnica es recomendable utilizar la opción 'A demanda'.">
                <legend>Obtener</legend>   
                <asp:RadioButtonList ID="rdbObtener" SkinID="rbl" runat="server" RepeatColumns="1" RepeatDirection="vertical" onclick="ObtenerEstructura();">
                    <asp:ListItem Value="B" Text="En bloque"></asp:ListItem>
                    <asp:ListItem Value="D" Text="A demanda"></asp:ListItem>
                </asp:RadioButtonList>
             </fieldset>
        </td>
        <td>
            <table style="width:290px">
            <colgroup><col style="width:115px;" /><col style="width:175px;" /></colgroup>
            <tr>
                <td style="text-align:center;">
                    <div id="divCualidadPSN" style="width:110px; height:40px;">
                        <asp:Image ID="imgCualidadPSN" runat="server" Height="40"  ImageUrl="~/images/imgSeparador.gif" />
                    </div>
                </td>
                <td>
                    <img id="btnBitacora" src="../../../../images/imgBTPEN.gif" border="0" class="MANO" style="width:32px;" title="Sin acceso a la bitácora de proyecto económico" />                    
                    &nbsp;&nbsp;<img id="btnVacatas" src="../../../../images/imgVelero.gif" border="0" class="MANO" style="width:32px" onclick="verVacaciones()" title="Vacaciones de los profesionales internos asociados al proyecto" />                    
                    &nbsp;&nbsp;<img id="btnOpenProjExp" src="../../../../images/imgOpenProjOut.png" runat="server" border="0" class="MANO" style="width:32px; visibility:hidden;" onclick="openProjExp()" title="Exportar a OpenProj vía fichero XML" />                    
                    &nbsp;&nbsp;<img id="btnOpenProjImp" src="../../../../images/imgOpenProjIn.png" runat="server" border="0" class="MANO" style="width:32px; visibility:hidden;" onclick="openProjImp()" title="Importar desde OpenProj vía fichero XML" />                    
                </td>            
            </tr>
            </table>            
        </td>
       <td align="right" title="Mostrar tareas cerradas o anuladas, con la estructura de la que cuelgan" style="padding-right:20px;">
        <asp:CheckBox ID="chkCerradas" runat="server" Text="Mostrar TC" Width="80px" TextAlign=left CssClass="check texto" onclick="mostrarCerradas();" />     
       </td>        
    </tr>
</table>

<table id="nombreProyecto" style="width:1200px; text-align:left;margin-left:5px">
    <tr>
        <td>
			<fieldset id="flsCriterios" style="width: 1178px;">
				<legend>&nbsp;Mantenimiento de estructura&nbsp;</legend>
                <table id="btnPT" align="left">
	                <tr>
                        <td><img id="imgNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);">	&nbsp;
                            <img id="imgExplosion" src='../../../../images/imgExplosion.png' title="Expande completamente el elemento seleccionado" onclick="expandirNodoAux();">&nbsp;&nbsp;                       
	                        <img src='../../../../Images/imgFlechaIzOff.gif' id="imgBotIzda" border='0' title="Desplaza hacia la izquierda la fila seleccionada" onclick="desplazarFilasMarcadas('I')" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgFlechaDrOff.gif' id="imgBotDcha" border='0' title="Desplaza hacia la derecha la fila seleccionada" onclick="desplazarFilasMarcadas('D')" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgFlechaUp.gif' id="imgBotArriba" border='0' title="Desplaza hacia arriba la fila seleccionada" onclick="subirFilasMarcadas()" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgFlechaDown.gif' id="imgBotAbajo" border='0' title="Desplaza hacia abajo la fila seleccionada" onclick="bajarFilasMarcadas()" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/Botones/imgBorrar.gif' id="imgBotBorrarT" border='0' title="Borra la fila seleccionada" onclick="eliminarTarea('tblDatos')" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgProyTec2.gif' id="imgBotPT" border='0' title="Proyecto técnico" onclick="Tarea('tblDatos','P')" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgFase2.gif' id="imgBotFase" border='0' title="Fase" onclick="Tarea('tblDatos','F')" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgActividad2.gif' id="imgBotActividad" border='0' title="Actividad" onclick="Tarea('tblDatos','A')" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgTarea2.gif' id="imgBotTarea" border='0' title="Tarea" onclick="Tarea('tblDatos','T')" style="cursor:pointer">&nbsp;&nbsp;
	                        <img src='../../../../Images/imgHito2.gif' id="imgBotHito" border='0' title="Hito" onclick="Tarea('tblDatos','HT')" style="cursor:pointer">
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbAccion" SkinID="rbl" runat="server" Height="20px" RepeatColumns="3" ToolTip="Acción a realizar" onclick="estadoImportar();">
                                <asp:ListItem Selected="True" Value="A">Añadir</asp:ListItem>
                                <asp:ListItem Value="I">Insertar</asp:ListItem>
                                <asp:ListItem Value="M">Modificar</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>&nbsp;</td>
		                <td> 
                            <button id="tblPE" type="button" onclick="flCargarPlantillaPE()" class="btnH25W110" title="Cargar plantilla de proyecto económico">
                                <img src="../../../../images/botones/imgEstructura.gif" />
                                <label id="lblPE">&nbsp;Plantilla P.E.</label>
                            </button>    
		                </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <button id="tblPT" type="button" onclick="flCargarPlantillaPT()" class="btnH25W110" title="Cargar plantilla de proyecto técnico">
                                <img src="../../../../images/botones/imgEstructura.gif" />
                                <label id="lblPT">&nbsp;Plantilla P.T.</label>
                            </button>    
		                </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <button id="tblPOOL" type="button" onclick="flMostrarPool()" class="btnH25W95" title="Mostrar Pool de Responsables de Proyectos Técnicos">
                                <img src="../../../../images/botones/imgDestinatarios.gif" />
                                <label id="lblPool">&nbsp;Pool RTP</label>
                            </button>    
		                </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <button id="tblImport" type="button" onclick="flImportar()" class="btnH25W95" title="Importar tareas desde fichero de texto">
                                <img src="../../../../images/botones/imgImporTareas.gif" />
                                <label id="lblImport">&nbsp;Importar</label>
                            </button>    
		                </td>
                    </tr>
                </table>
			</fieldset>
        </td>
    </tr>
    <tr>
        <td>
            <table id="Table2" style="width: 1190px; text-align:center; margin-top:3px; height: 17px;">
                <colgroup>
                    <col style="width:518px;" />
                    <col style="width:80px;" />
                    <col style="width:65px;" />
                    <col style="width:65px;" />
                    <col style="width:80px;" />
                    <col style="width:70px;" />
                    <col style="width:70px;" />
                    <col style="width:65px;" />
                    <col style="width:65px;" />
                    <col style="width:60px;" />
                    <col style="width:30px;" /> 
                    <col style="width:22px;" />                   
                </colgroup>
	            <tr class="TBLINI">
		            <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Denominación&nbsp;
					    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
		                <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event);"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
		            <td title='Esfuerzo total planificado'>ETPL</td>
		            <td title='Fecha inicio planificada'>FIPL</td>
		            <td title='Fecha fin planificada'>FFPL</td>
		            <td title='Presupuesto'>Presup.(&euro;)</td>
		            <td title='Consumo en horas'>Cons.(H)</td>
		            <td title='Consumo en jornadas'>Cons.(J)</td>
		            <td title='Fecha inicio vigencia'>FIV</td>
		            <td title='Fecha fin vigencia'>FFV</td>
		            <td>Estado</td>
		            <td title='Facturable'>Fact.</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width:1209px; height:460px; visibility:hidden;" onscroll="scrollTareas()">
                <div style='background-image:url(../../../../Images/imgFT20.gif); width:1190px'>
                <%=strTablaHTMLTarea%>
                </div>
            </div>
            <table id="tblResultado" style="width: 1190px; height: 17px;">
                <colgroup>
                    <col style="width:518px;" />
                    <col style="width:80px;" />
                    <col style="width:65px;" />
                    <col style="width:65px;" />
                    <col style="width:80px;" />
                    <col style="width:70px;" />
                    <col style="width:70px;" />
                    <col style="width:65px;" />
                    <col style="width:65px;" />
                    <col style="width:60px;" />
                    <col style="width:30px;" /> 
                    <col style="width:22px;" />                   
                </colgroup>            
	            <tr class="TBLFIN">
		            <td>&nbsp;Total proyecto</td>
		            <td style="text-align:right;"><label id='lblETPL' style="width:80;" class='txtNumL' title='Esfuerzo total planificado'></label></td>
		            <td style="text-align:right;"><label id='lblFIPL' class='txtL' style="width:60px;" title='Fecha inicio planificada'></label></td>
		            <td style="text-align:right;"><label id='lblFFPL' class='txtL' style="width:60px;" title='Fecha fin planificada'></label></td>
		            <td style="text-align:right;"><label id='lblPRES' class='txtNumL' style="width:80px" title='Presupuesto'></label></td>
		            <td style="text-align:right;"><label id='lblCONH' class='txtNumL' style="width:70px;" title='Consumo en horas'></label></td>
		            <td style="text-align:right;"><label id='lblCONJ' class='txtNumL' style="width:70px" title='Consumo en jornadas'></label></td>
		            <td style="text-align:center;"><label id='lblFIV' class='txtL' style="width:60px;" title='Fecha inicio vigencia'></label></td>
		            <td style="text-align:center;"><label id='lblFFV' class='txtL' style="width:60px;" title='Fecha fin vigencia'></label></td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td style="border-right:'';">&nbsp;</td>
	            </tr>
            </table>
        </td>
    </tr>
    <tr style="vertical-align:top;">
        <td>
            <table style="width:970px;">
                <colgroup><col style="width:670px;"/><col style="width:145px;"/><col style="width:155px;"/></colgroup>
                <tr>
                    <td>
			            <fieldset style="width:638px;">
				            <legend>&nbsp;Mantenimiento de hitos temporales y de cumplimiento múltiple&nbsp;</legend>
                            <table id="Table1" style="margin:0px; width:100%">
	                            <tr>
	                                <td>
	                                    <img src='../../../../Images/imgFlechaUp.gif' id="imgBotUp" border='0' title="Desplaza hacia arriba la fila marcada" onclick="subirHitosMarcados()" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;
	                                    <img src='../../../../Images/imgFlechaDown.gif' id="imgBotDown" border='0' title="Desplaza hacia abajo la fila marcada" onclick="bajarHitosMarcados()" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;
	                                    <img src='../../../../Images/Botones/imgBorrar.gif' id="imgBotBorrarH" border='0' title="Borra la fila marcada" onclick="eliminarHito('tblDatos2')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                                    <img src='../../../../Images/imgHito2.gif' id="imgBotHito2" border='0' title="Hito" onclick="Hito('tblDatos2','HF')" style="cursor:pointer;">&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>    
                                        <asp:RadioButtonList ID="rdbAccion2" SkinID="rbl" runat="server" Height="20px" RepeatColumns="3" ToolTip="Acción a realizar" Width="190px">
                                            <asp:ListItem Selected="True" Value="A">Añadir</asp:ListItem>
                                            <asp:ListItem Value="I">Insertar</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
			            </fieldset>
	                    <table id="Table4" style="width:650px; height:17px; margin-top:3px">
	                        <colgroup>
	                            <col style="width:25px"/>
	                            <col style="width:460px"/>
	                            <col style="width:65px"/>
	                            <col style="width:50px"/>
	                            <col style="width:25px"/>
	                            <col style="width:25px"/>
	                        </colgroup>
		                    <tr class="TBLINI">
			                    <td>&nbsp;</td>
			                    <td>Denominación</td>
			                    <td>Fecha</td>
			                    <td>Estado</td>
			                    <td title='Hito de proyecto económico'>PE</td>
			                    <td>&nbsp;</td>
		                    </tr>
	                    </table>
	                    <div id="divHitos" style="overflow:auto; overflow-x:hidden; width:666px; height:80px; visibility:hidden">
                            <div style='background-image:url(../../../../Images/imgFT20.gif); width:650px'>
    	                        <%=strTablaHTMLHito%>
    	                    </div>
                        </div>
	                    <table id="Table5" style="width: 650px; height: 17px">
		                    <tr class="TBLFIN">
			                    <td></td>
		                    </tr>
	                    </table>
	                </td>
	                <td style="vertical-align:top;">
                        <fieldset id="fstPresupuestacion" style="width:140px; visibility:hidden;">
	                        <legend><label id="lblPresupuestacion">&nbsp;Presupuestación&nbsp;</label></legend>
	                        <label id="lblNivelPresupuestacion" style="margin-top:7px; margin-bottom:3px; margin-left:2px;">	                            
	                        </label>
	                    </fieldset>
                    </td>
                    <td style="vertical-align:top;">
	                    <fieldset id="fstCualificacion" style="width:150px; visibility:hidden; margin-left:15px;">
	                        <legend>&nbsp;CVT&nbsp;</legend>
	                        <label id="lblCualificacion" class="enlace" onclick="getExp();" 
	                            title="Acceso a la cualificación del proyecto para la gestión de currículums"
	                            style="margin-top:7px; margin-bottom:3px; margin-left:2px;">
	                            Cualificación del proyecto
	                        </label>
	                    </fieldset>
	                </td>
	            </tr>
	        </table>
        </td>
    </tr>
</table>

<div style="position:relative; margin-top:1px; margin-left:0px">
<table id="tblImg" width="1200px" border="0" class="texto" >
      <tr>
        <td><img border="0" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' class='ICO' />&nbsp;Abierto
        &nbsp;&nbsp;<img border="0" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' class='ICO' />&nbsp;Cerrado
        &nbsp;&nbsp;<img border="0" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' class='ICO' />&nbsp;Histórico
        &nbsp;&nbsp;<img border="0" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' class='ICO' />&nbsp;Presupuestado
        &nbsp;&nbsp;&nbsp;
        <img border="0" src="../../../../Images/imgAccesoW.gif" class='ICO' />&nbsp;Permiso escritura
        &nbsp;&nbsp;<img border="0" src="../../../../Images/imgAccesoV.gif" class='ICO' />&nbsp;Permiso escritura restringida en detalle
        &nbsp;&nbsp;<img border="0" src="../../../../Images/imgAccesoR.gif" class='ICO' />&nbsp;Acceso en lectura a detalle
        &nbsp;&nbsp;<img border="0" src="../../../../Images/imgAccesoN.gif" class='ICO' />&nbsp;Sin acceso a detalle
        </td>
      </tr>
</table>
</div>
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<input type="hidden" runat="server" name="hdnEsBitacorico" id="hdnEsBitacorico" value="F" />
<input type="hidden" runat="server" name="hdnNivelPresupuesto" id="hdnNivelPresupuesto" value="" />
<input type="hidden" runat="server" name="hdnAccesoPresupuestacion" id="hdnAccesoPresupuestacion" value="" />
<label id="lblNodo" runat="server" class="texto" style="width:2px;visibility:hidden"></label>
<asp:TextBox ID="txtDesCR" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<asp:TextBox ID="txtEstado" runat="server" style="width:2px;visibility:hidden" readonly="true" />
<asp:TextBox ID="txtUne" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<asp:TextBox ID="txtNomResp" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<asp:TextBox ID="txtFacturable" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<asp:TextBox ID="txtNomCliente" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript"> 
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
				case "plantillape": 
				{
                    bEnviar = false;
                    grabarPlantilla();
					break;
				}
				case "plantillapt": 
				{
                    bEnviar = false;
                    grabarPlantillaPT();
					break;
				}
				case "gantt": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("gantt();", 20);
					break;
				}
				case "documentos": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("mostrarDocumentos();", 20);
					break;
				}
				case "cargarestr": 
				{
                    bEnviar = false;
                    duplicar();
					break;
                } 
                case "excel":
                    {
                        bEnviar = false;
                        setTimeout("masivoBitacora();", 20);
                        break;
                    }
			    case "guia":
			        {
			            bEnviar = false;
			            mostrarGuia("EstructuraTecnica.pdf");
			            break;
			        }
			    case "traspasoiap":
			        {
			            bEnviar = false;
			            setTimeout("cierreTecnico();", 20);
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
</asp:Content>

