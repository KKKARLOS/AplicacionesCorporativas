<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_Default" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
     <style type="text/css">
     #ctl00_CPHC_tsPestanas table { table-layout:auto; }
     .titulo3{
         color: #28406c;
         font-size: 9pt; 
     }
     .profesional{
         font-size: 11pt; 
         vertical-align:middle;
     }
     .titulo1{
         font-weight: bold; 
         font-size: 13pt; 
         color: #28406c;
     }
     .colorA{
        color:Blue;
     }
     .colorN{
        color:Orange;
     }
     .warning {
        width: 16px;
        height: 16px;
        margin-left: 10px;
        vertical-align: middle;
     }
     #tblDatosTitulacion tr, #tblDatosFormacionRecibida tr,
     #tblDatosExamenCert tr, #tblDatosIdiomas tr, #tblOtros tr { height: 20px; }
     
     #tblTituloAcademica td, #tblDatosTitulacion td,
     #tblTituloAcciones td, #tblDatosFormacionRecibida td,
     #tblTituloCertificados td, #tblDatosExamenCert,
     #tblTituloIdiomas td, #tblDatosIdiomas td { padding-left: 2px; }
     
     #fstPersonales, #fstOrganizativos { padding: 5px 10px 10px 10px; }
     #tblPersonales td, #tblOrganizativos td, #tblOtros td { padding: 3px 3px 3px 3px;}
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var origen = "<%=sOrigen%>";
    var sIDFicepiEntrada =  "<% =sIDFicepiEntrada  %>";
    var sDatosPendientes =  "<% =sDatosPendientes  %>";
    //Para mensajes emergentes    
    var sTareasPendientes = "<% =sTareasPendientes %>";  
    var sMOSTRAR_SOLODIS = "<%=ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"].ToString() %>";
    var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;       
</script>  
	
<table id="Table2" style="width:980px; margin-bottom: 15px; margin-top:10px;" border="0">
<colgroup>
    <col style="width:500px;" />
    <col style="width:480px;" />
</colgroup>
    <tr>
        <td style="background-image:url('../../../Images/imgFondoCal500b.png'); background-repeat:no-repeat; height: 32px;" valign="top">
            <img src="../../../Images/imgBirrete.png" style="vertical-align:middle; margin-left:5px; margin-right:6px;" />
            <label runat="server" id="nombre" class="profesional"></label>
        </td>
        <td style="float:right;">
            <button id="btnFinCv" type="button" onclick="FinalizarCv();" class="btnH30W150" style="padding-left:17px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 30);mostrarCursor(this);">
                <img src="../../../Images/imgCVOK.png" /><span>Dar de alta el CV</span>
            </button>
        </td>
    </tr>
</table>
<table id="MensajeFijo" style="width:350px;text-align:left;height:55px;display:none;position:absolute;top:135px;left:660px;">
    <tr>
        <td>     
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:5px">
				
                    <table id="tblDatos2" cellspacing="5" style="width:320px;text-align:left">
	                    <colgroup><col style="width:200px" /><col style="width:120px" /></colgroup>
	                    <tr> 
		                    <td>
			                    &nbsp;&nbsp;&nbsp;&nbsp;Actualiza tu CV para el <label id="lblFechaLimite" runat="server" title="Fecha límite de actualización" style="width:70px;height:17px;font-weight: bold;text-decoration:underline;" >30/07/2014</label>
		                    </td>
		                    <td rowspan="2" valign="middle">
<%--		                        <div id="lblEnPlazoyFuera" style="background-color:red;width:120px;height:50px"></div>
--%>		                            <img id="imgEnPlazoyFuera" src="../../../Images/imgCVFueraPlazo.png" style="width:120px;height:50px"/>
<%--				                <label id="lblEnPlazoyFuera">imgCVEnPlazo.png Fuera de plazo</label>                            
--%>		                    </td>
	                    </tr>
	                    <tr> 
		                    <td>
			                    <button id="btnCVCompletadoProf" type="button" onclick="setCompletadoProf();" class="btnH25W180" style="padding-left:17px; margin-left:10px; display:block;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Doy por finalizada la revisión y actualización del CV.">
				                    <img src="../../../Images/imgCVOK.png" /><span>CV revisado/actualizado</span>
			                    </button>							
		                    </td>
	                    </tr>						
                    </table>
                </td>
                <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
</table>	
<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
				MultiPageID="mpContenido" 
				ClientSideOnLoad="CrearPestanas" 
				ClientSideOnItemClick="getPestana"
				>
	<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		<Items>
				<eo:TabItem Text-Html="General" ToolTip="" Width="120"></eo:TabItem>
				<eo:TabItem Text-Html="Formación" ToolTip="" Width="120"></eo:TabItem>
				<eo:TabItem Text-Html="Experiencia" ToolTip="" Width="120"></eo:TabItem>
				<eo:TabItem Text-Html="Sinopsis" ToolTip="" Width="120"></eo:TabItem>
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
        Image-Mode="TextBackground" 
        Image-BackgroundRepeat="RepeatX">
    </eo:TabItem>
</LookItems>
</eo:TabStrip>
<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="460px">
    <eo:PageView ID="PageView1" CssClass="PageView" runat="server">				
    <!-- Pestaña 1 -->
    <table id="Table21" style="width:980px; margin-top:0px;" border="0">
    <colgroup>
        <col style="width:500px;" />
        <col style="width:180px;" />
        <col style="width:300px;" />
    </colgroup>
    <tr>
        <td style="padding-top:5px;">
            <label id="lblDatosPersonales" class="titulo1 W200">Datos personales</label>
            <table id="tblPersonales" style="width:440px; margin-top: 10px; margin-left: 20px;">
            <colgroup>
                <col style="width:90px;" />
                <col style="width:350px;" />
            </colgroup>
            <tr>
                <td><label id="lblNIF" class="titulo3">NIF:</label></td>
                <td><label runat="server" id="idNIF"></label></td>
            </tr>
            <tr>
                <td><label id="lblFNacimiento" class="titulo3">F. Nacimiento:</label></td>
                <td><label runat="server" id="idNacimiento"></label></td>
            </tr>
            <tr>
                <td><label id="lblNacionalidad" class="titulo3">Nacionalidad:</label></td>
                <td><label runat="server" id="idNacionalidad"></label></td>
            </tr>
            <tr>
                <td><label  id="lblSexo" class="titulo3">Sexo:</label></td>
                <td><label  runat="server" id="idSexo"></label></td>
            </tr>
            </table>
        </td>
        <td>
            <div align="left"><label onclick='getDocumentos()' class='enlace'>Archivos asociados</label></div>
        </td>
        <td valign="top">
            <div align="right">
                <% if (Session["FOTOUSUARIO"] != null)
                { %>
                <img title="" class="ICO" id="imgFoto" src="~/Capa_Presentacion/Inicio/ObtenerFoto.aspx" style="height:140px; margin-bottom: 10px;" runat="server" />
                <% }
                else
                { %>
                <img title="" class="ICO" id="imgFoto" src="../../../Images/imgTrans9x9.gif" style="height:140px; margin-bottom: 10px;" />
                <% } %>
            </div>
        </td>
    </tr>
    <tr valign="top">
        <td>
            <label id="lblDatosOrganizativos" class="titulo1 W200">Datos organizativos</label>
            <table id="tblOrganizativos" style="width:440px; margin-left: 20px;" border="0">
            <colgroup>
                <col style="width:160px;" />
                <col style="width:280px;" />
            </colgroup>
            <tr>
                <td><label id="lblEmpresa" class="titulo3">Empresa:</label></td>
                <td><label runat="server" id="empresa"></label></td>
            </tr>
            <tr>
                <td><label id="lblUnidad" class="titulo3"><%= Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %>:</label></td>
                <td><label runat="server" id="SN2"></label></td>
            </tr>
            <tr>
                <td><label id="lblCR" class="titulo3"><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>:</label></td>
                <td><label runat="server" id="CR"></label></td>
            </tr>
            <tr>
                <td><label id="lblFechaA"class="titulo3">Fecha antigüedad:</label></td>
                <td><label runat="server" id="fantigu"></label></td>
            </tr>
            <tr>
                <td><label id="lblRolI" class="titulo3" style="display:none;">Rol interno:</label></td>
                <td><label  runat="server" id="rol" style="display:none;"></label></td>
            </tr>
            <tr>
                <td><label id="lblPerfil" class="titulo3" style="display:none;">Perfil mercado:</label></td>
                <td><label runat="server" id="perfil" style="display:none;"></label></td>
            </tr>
            <tr>
                <td><label id="lblOficina" class="titulo3">Oficina:</label></td>
                <td><label runat="server" id="oficina"></label></td>
            </tr>
            <tr>
                <td><label id="lblProvincia" class="titulo3">Provincia:</label></td>
                <td><label runat="server" id="provincia"></label></td>
            </tr>
            <tr>
                <td><label id="lblPais" class="titulo3">País:</label></td>
                <td><label  runat="server" id="pais"></label></td>
            </tr>
        </table>
        </td>
        <td colspan="2">
            <label id="lblOtrosDatos" class="titulo1 W200" >Otros datos</label>
            <table id="tblOtros" style="width:440px; margin-left: 20px;" border="0">
            <colgroup>
                <col style="width:260px;" />
                <col style="width:180px;" />
            </colgroup>
            <tr style="height:16px;">
                <td><label class="titulo3">Interesado en trayectoria internacional:</label></td>
                <td>
                   <asp:radiobuttonlist id="rdlInternacional" filterName="Internacional" RepeatLayout="Flow" runat="server" SkinID="rbl" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" onclick="activarGrabar()" >
                        <asp:ListItem Value="1">Sí&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>   
                    </asp:radiobuttonlist>
                </td>
            </tr>
            <tr>
                <td><label class="titulo3">Disponibilidad para movilidad geográfica:</label></td>
                <td>
                    <asp:DropDownList ID="cboMovilidad" style="width:85px;" onChange="activarGrabar()" runat="server" AppendDataBoundItems="true" CssClass="combo">
                          <asp:ListItem Selected="True" Value="">Ninguna</asp:ListItem>  
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2"><label class="titulo3">Observaciones:</label><br />
                <textarea id="txtObserva" runat="server" style="width:430px;" cols="100" rows="5" class="txtMultiM" onkeyup="activarGrabar()"></textarea></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../Images/Botones/imgGrabar.gif" /><span>Grabar</span>
                </button>	  
                </td>
            </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div id="txtTextoLegal" style="width:935px; height:100px; padding-left:10px;  padding-right:10px; margin-top:5px; overflow-x:hidden; overflow:auto; border:1px solid black;" runat="server">
                <p style="text-align:justify;">
                <b>Ibermática, S.A.</b>, (en adelante Ibermática) mantendrá y utilizará los datos que se encuentran reflejados en el CV para el mantenimiento de su relación con clientes, proveedores 
                en la realización de ofertas, propuestas de servicios en el ámbito profesional así como para la gestión de los datos curriculares que se realizan en esta herramienta. 
                Dicho acceso y utilización de los datos curriculares se mantendrá durante el tiempo de vigencia de la relación contractual laboral de cada uno de los profesionales de Ibermática. 
                Adicionalmente, Ibermática conservará, una vez finalizada la relación contractual laboral los datos curriculares durante un plazo máximo de tres (3) años a los únicos efectos efecto de 
                cumplir con lo exigido en sus auditorías de calidad, todo ello conforme a la normativa vigente. 
                Fuera de lo recogido anteriormente los datos curriculares no serán cedidos a terceros, sin perjuicio de la eventual comunicación a las autoridades competentes en los casos que exista una 
                obligación legal para ello.<br /><br />
                En todo caso el interesado tiene derecho acceder a sus datos personales, así como a solicitar su rectificación, supresión y oposición; limitación del tratamiento; portabilidad de datos; y, 
                a no ser objeto de decisiones individualizadas automatizadas <a href='mailto:arco@ibermatica.com' class='enlace'>(arco@ibermatica.com)</a>.
                </p>
                    <br /><br />
                &nbsp;&nbsp;&nbsp;Responsable del tratamiento: Ibermática; S.A.<br />
                &nbsp;&nbsp;&nbsp;CIF: A20038915<br />
                &nbsp;&nbsp;&nbsp;Dir.: San Sebastián, Paseo Mikeletegi nº 5 (20009) Guipúzcoa<br />
                &nbsp;&nbsp;&nbsp;Email: <a href='mailto:rgpd@ibermatica.com' class='enlace'>rgpd@ibermatica.com</a> 
                <br /><br />
                Puede consultar la Política de Privacidad en el siguiente enlace: <a href='http://ibermatica.com/aviso-legal/#arco' target="_blank">Política de Privacidad</a>
            </div>
        </td>
    </tr>
    </table>
    </eo:PageView>
    <eo:PageView ID="PageView2" CssClass="PageView" runat="server">				
    <!-- Pestaña 2 -->
    <eo:TabStrip runat="server" id="tsPestanasFor" ControlSkinID="None" Width="982" 
				    MultiPageID="mpContenidoFor" 
				    ClientSideOnLoad="CrearPestanasFor" 
				    ClientSideOnItemClick="getPestana"
				    >
	    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		    <Items>
				    <eo:TabItem Text-Html="Formación académica" ToolTip="" Width="170"></eo:TabItem>
				    <eo:TabItem Text-Html="Acciones formativas" ToolTip="" Width="170"></eo:TabItem>
				    <eo:TabItem Text-Html="Cert. / Exam." ToolTip="Certificados y Exámenes" Width="120"></eo:TabItem>
				    <eo:TabItem Text-Html="Idiomas" ToolTip="" Width="100"></eo:TabItem>
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
            Image-Mode="TextBackground" 
            Image-BackgroundRepeat="RepeatX">
        </eo:TabItem>
    </LookItems>
    </eo:TabStrip>
    <eo:MultiPage runat="server" id="mpContenidoFor" CssClass="FMP" Width="982" Height="420px">
        <eo:PageView ID="PageView4" CssClass="PageView" runat="server">
        <!-- Pestaña 1 -->
            <!--<label id='lblAnadirTitulacion' onclick="AnadirTitulacion('','');" style='cursor:pointer;margin-left:3px'>&nbsp;Añadir</label>
            <img src="../../../Images/imgAddItem.png" title="Añadir titulación" onclick="AnadirTitulacion('','');" style="cursor:pointer;margin-left:3px">
            <img src="../../../Images/imgDelItem.png" title="Eliminar la titulación seleccionada" onclick="EliminarTitulacion('','');" style="cursor:pointer;margin-left:10px">-->
            <input type="checkbox" id="chkFor_For" class="check" style="margin-left:630px;" onclick="mostrarPdtes(this)" />
            Mostrar únicamente elementos pendientes de cumplimentar
            <table id="tblTituloAcademica" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                <colgroup>
                    <col style='width:25px;' />
                    <col style='width:20px;' />
                    <col style='width:35px;' />
                    <col style='width:50px;'/>
                    <col style='width:50px;'/>
                    <col style='width:750px;' />
                </colgroup>
                <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>Inicio</td>
                    <td>Fin</td>
                    <td>Título</td>
               </tr>
            </table>
            <div id="divCatalogoAcademica" style="width:946px; height:300px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">
                <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                </div>
            </div>
            <table id="Table1" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div>
                <img src="../../../Images/imgTitulo.png" class="ICO" />Documento acreditativo 
                <img src="../../../Images/imgExpediente.png" class="ICO" style="margin-left:20px;" />Expediente académico 
            </div>
            <center>
            <table style="width: 340px; margin-top: 2px;">
            <tr>
                <td align="center">
                    <button id="btnAddAcademica" type="button" onclick="AnadirTitulacion('','');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Añadir titulación">
                        <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="btnDelAcademica" type="button" onclick="borrarFormacionAcademica();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Eliminar la titulación seleccionada">
                        <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="Button1" type="button" onclick="mostrarGuia('GuiaFormacionAcademica.pdf');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                        <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                    </button>	  
                </td>
            </tr>
            </table>
            </center>
        </eo:PageView>		
        <eo:PageView ID="PageView5" CssClass="PageView" runat="server">
            <eo:TabStrip runat="server" id="tsPestanasAcciones" ControlSkinID="None" Width="970" 
				        MultiPageID="mpContenidoAcciones" 
				        ClientSideOnLoad="CrearPestanasAcciones" 
				        ClientSideOnItemClick="getPestana"
				        >
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
				            <eo:TabItem Text-Html="Recibidas" ToolTip="" Width="140"></eo:TabItem>
				            <eo:TabItem Text-Html="Impartidas" ToolTip="" Width="140"></eo:TabItem>
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
                        Image-Mode="TextBackground" 
                        Image-BackgroundRepeat="RepeatX">
                    </eo:TabItem>
                </LookItems>
            </eo:TabStrip>
            <eo:MultiPage runat="server" id="mpContenidoAcciones" CssClass="FMP" Width="970" Height="380px">
                <eo:PageView ID="PageView8" CssClass="PageView" runat="server">
                <!-- Pestaña 2 Formación.Acciones Formativas.Recibidas-->
                    <input type="checkbox" id="chkFor_Acc_Rec" class="check" style="margin-left:630px;" onclick="mostrarPdtes(this)" />
                    Mostrar únicamente elementos pendientes de cumplimentar
                    <table id="tblTituloAcciones" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                        <colgroup>
                            <col style='width:25px;' />
                            <col style='width:20px;' />
                            <col style='width:320px;'/>
                            <col style='width:50px;' />
                            <col style='width:90px;' />
                            <col style='width:90px;' />
                            <col style='width:215px;' />
                            <col style='width:60px;' />
                            <col style='width:60px;' />
                        </colgroup>
                        <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                            <td></td>
                            <td></td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgAFR1" border="0">
                                <MAP name="imgAFR1">
				                    <AREA onclick="ot('tblDatosFormacionRecibida', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				                    <AREA onclick="ot('tblDatosFormacionRecibida', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			                    </MAP>
                                Acción</td>
                            <td>Horas</td>
                            <td>Provincia</td>
                            <td>País</td>
                            <td>Centro</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgAFR2" border="0">
                                <MAP name="imgAFR2">
				                    <AREA onclick="ot('tblDatosFormacionRecibida', 6, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				                    <AREA onclick="ot('tblDatosFormacionRecibida', 6, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			                    </MAP>
                                Inicio</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgAFR3" border="0">
                                <MAP name="imgAFR3">
				                    <AREA onclick="ot('tblDatosFormacionRecibida', 7, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				                    <AREA onclick="ot('tblDatosFormacionRecibida', 7, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			                    </MAP>
                                Fin</td>
                        </tr>
                    </table>
                    <div id="divCatalogoAcciones" style="width:946px; height:260px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">
                        <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                        </div>
                    </div>
                    <table id="Table3" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                        <tr class="TBLFIN">
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <div>
                        <img src="../../../Images/imgTitulo.png" class="ICO" />Documento acreditativo 
                    </div>
                    <center>
                    <table style="width: 340px; margin-top:4px;margin-bottom:5px;">
                    <tr>
                        <td align="center">
                            <button id="btnAddAcciones" type="button" onclick="AnadirCurso('','');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Añadir acción formativa">
                                <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                            </button>	  
                        </td>
                        <td align="center">
                            <button id="btnDelAcciones" type="button" onclick="borrarFormacionRecibida()" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Eliminar la acción formativa seleccionada">
                                <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                            </button>	  
                        </td>
                        <td align="center">
                            <button id="Button2" type="button" onclick="mostrarGuia('GuiaAccionesFormativas.pdf');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                                <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                            </button>	  
                        </td>
                    </tr>
                    </table>
                    </center>
                </eo:PageView>
                <eo:PageView ID="PageView9" CssClass="PageView" runat="server">
                    <!-- Pestaña 10 Formación.Acciones Formativas.Impartidas-->
                    <input type="checkbox" id="chkFor_Acc_Imp" class="check" style="margin-left:630px;" onclick="mostrarPdtes(this)" />
                    Mostrar únicamente elementos pendientes de cumplimentar
                    <table id="tblTituloAccionesImpartidas" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                        <colgroup>
                            <col style='width:25px;' />
                            <col style='width:20px;' />
                            <col style='width:320px;'/>
                            <col style='width:50px;' />
                            <col style='width:90px;' />
                            <col style='width:90px;' />
                            <col style='width:215px;' />
                            <col style='width:60px;' />
                            <col style='width:60px;' />
                        </colgroup>
                        <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                            <td></td>
                            <td></td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgAFI1" border="0">
                                <MAP name="imgAFI1">
				                    <AREA onclick="ot('tblDatosFormacionImpartida', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				                    <AREA onclick="ot('tblDatosFormacionImpartida', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			                    </MAP>
                                Acción</td>
                            <td>Horas</td>
                            <td>Provincia</td>
                            <td>País</td>
                            <td>Centro</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgAFI2" border="0">
                                <MAP name="imgAFI2">
				                    <AREA onclick="ot('tblDatosFormacionImpartida', 6, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				                    <AREA onclick="ot('tblDatosFormacionImpartida', 6, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			                    </MAP>
                                Inicio</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgAFI3" border="0">
                                <MAP name="imgAFI3">
				                    <AREA onclick="ot('tblDatosFormacionImpartida', 7, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				                    <AREA onclick="ot('tblDatosFormacionImpartida', 7, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			                    </MAP>
                                Fin</td>
                        </tr>
                    </table>
                    <div id="divCatalogoAccionesImpartidas" style="width:946px; height:260px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">
                        <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                        </div>
                    </div>
                    <table id="Table11" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                        <tr class="TBLFIN">
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <div>
                        <img src="../../../Images/imgTitulo.png" class="ICO" />Documento acreditativo 
                    </div>
                    <center>
                    <table style="width: 340px; margin-top:4px;margin-bottom:5px;">
                    <tr>
                        <td align="center">
                            <button id="btnAddAccionesImp" type="button" onclick="AnadirCursoImpartido('','');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Añadir acción formativa">
                                <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                            </button>	  
                        </td>
                        <td align="center">
                            <button id="btnDelAccionesImp" type="button" onclick="borrarFormacionImpartida();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Eliminar la acción formativa seleccionada">
                                <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                            </button>	  
                        </td>
                        <td align="center">
                            <button id="Button3" type="button" onclick="mostrarGuia('GuiaAccionesFormativas.pdf');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                                <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                            </button>	  
                        </td>
                    </tr>
                    </table>
                    </center>
                </eo:PageView>
            </eo:MultiPage>
        </eo:PageView>	
        <eo:PageView ID="PageView6" CssClass="PageView" runat="server">
            <!-- Pestaña 3 Formación.Certificados y Exámenes-->
            <eo:TabStrip runat="server" id="tsPestanaCertExam" ControlSkinID="None" Width="970" 
				        MultiPageID="mpContenidoCertExam" 
				        ClientSideOnLoad="CrearPestanasCertExam" 
				        ClientSideOnItemClick="getPestana"
				        >
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
				            <eo:TabItem Text-Html="Certificados" ToolTip="" Width="140"></eo:TabItem>
				            <eo:TabItem Text-Html="Exámenes" ToolTip="" Width="140"></eo:TabItem>
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
                        Image-Mode="TextBackground" 
                        Image-BackgroundRepeat="RepeatX">
                    </eo:TabItem>
                </LookItems>
            </eo:TabStrip>
            <eo:MultiPage runat="server" id="mpContenidoCertExam" CssClass="FMP" Width="970" Height="380px">
                <eo:PageView ID="PageView13" CssClass="PageView" runat="server">
                    <!--<label id='lblAnadirExamen' onclick="abrirDetalleExamen('','');" style='cursor:pointer;margin-left:3px'>&nbsp;Añadir</label>-->
                    <input type="checkbox" id="chkFor_CertSug" class="check" style="margin-left:420px;" onclick="mostrarCertificadosSugeridos(this);"  />
                    Mostrar certificados sugeridos
                    <input type="checkbox" id="chkFor_Cert" class="check" onclick="mostrarPdtes(this);" style="margin-left:40px;" />
                    Mostrar únicamente elementos pendientes de cumplimentar
                    <table id="tblTituloCertificados" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                        <colgroup>
                            <col style='width:25px;' />
                            <col style='width:20px;' />
                            <col style='width:275px;'/>
                            <col style='width:90px;' />
                            <col style='width:250px;' />
                            <col style='width:240px;' />
                            <col style='width:30px;' />
                        </colgroup>
                        <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                            <td></td>
                            <td></td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCert1" border="0">
                                <MAP name="imgCert1">
			                        <AREA onclick="ot('tblDatosExamenCert', 2, 0, '')" shape="RECT" coords="0,0,6,5">
			                        <AREA onclick="ot('tblDatosExamenCert', 2, 1, '')" shape="RECT" coords="0,6,6,11">
		                        </MAP>
                                Denominacion</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCert2" border="0">
                                <MAP name="imgCert2">
			                        <AREA onclick="ot('tblDatosExamenCert', 3, 0, 'fec')" shape="RECT" coords="0,0,6,5">
			                        <AREA onclick="ot('tblDatosExamenCert', 3, 1, 'fec')" shape="RECT" coords="0,6,6,11">
		                        </MAP>
                                F. Obtención</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCert3" border="0">
                                <MAP name="imgCert3">
			                        <AREA onclick="ot('tblDatosExamenCert', 4, 0, '')" shape="RECT" coords="0,0,6,5">
			                        <AREA onclick="ot('tblDatosExamenCert', 4, 1, '')" shape="RECT" coords="0,6,6,11">
		                        </MAP>
                                Entidad certicadora</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCert4" border="0">
                                <MAP name="imgCert4">
			                        <AREA onclick="ot('tblDatosExamenCert', 5, 0, '')" shape="RECT" coords="0,0,6,5">
			                        <AREA onclick="ot('tblDatosExamenCert', 5, 1, '')" shape="RECT" coords="0,6,6,11">
		                        </MAP>
                                Entorno tecnológico</td>
                            <td>Vías</td>
                        </tr>
                    </table>
                    <div id="divCatalogoCertificados" style="width:946px; height:260px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">
                        <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                        </div>
                    </div>
                    <table id="Table4" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                        <tr class="TBLFIN">
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <div style="position:absolute;top:610px;left:30px;">
                        <%--<img src="../../../Images/imgExpediente.png" class="ICO" />Documento examen --%>
                        <img src="../../../Images/imgCertificado.png" class="ICO" />Documento certificado
                        <img src="../../../Images/imgIncompleto.png" class="ICO" style="margin-left:10px;" title="Certificación que podrías lograr, en caso de superar los exámenes que componen el certificado. No es obligatorio completarlo"/>Incompleto
                        <br />
                        <img src="../../../Images/imgNO.gif" class="ICO" style="margin-left:1px; margin-top:2px;" title="El certificado contiene algún examen para el que has solicitado su borrado"/>
                        <label>Examen pdte. eliminar</label>
                        <img src="../../../Images/imgDocNoValido.png" class="ICO" style="margin-left:1px;" title="Reemplaza el documento acreditativo del certificado"/>
                        <label>Doc. a reemplazar</label> 
                    </div> 
                    <div style="position:absolute;top:610px;left:680px;">            
                        <table style="width:300px; height:30px;">
                            <tbody><tr>
                                <td>
                                    <span style="font-weight:bold;">Nota:</span> El sistema te sugiere otras Certificaciones (en rojo) que podrías conseguir                 
                               a partir de un examen que has incorporado. No es obligatorio completarlo. </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="margin-left:30px"></span>
                                </td>
                            </tr>                               
                        </tbody></table>
                    </div>
                    <table style="width:380px; margin-left:270px; margin-top:15px;">
                        <tr>
                            <td align="center">
                                <button id="btnRenovarCert" type="button" onclick="renovarCert();" class="btnH25W85" 
                                    runat="server" hidefocus="hidefocus" onmouseover="se(this, 25); mostrarCursor(this);" 
                                    title="Renovación de certificado">
                                    <img src="../../../Images/Botones/imgModificar.gif" /><span>Renovar</span>
                                </button>	  
                            </td>
                            <td align="center">
                                <button id="btnAddCertificado" type="button" onclick="solicitarCert();" class="btnH25W85" 
                                    runat="server" hidefocus="hidefocus" onmouseover="se(this, 25); mostrarCursor(this);" 
                                    title="Solicitar creación de certificado">
                                    <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                                </button>	  
                            </td>
                            <td align="center">
                                <button id="btnDelCertificado" type="button" onclick="borrarCertificados();" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Eliminar el certificado seleccionado">
                                    <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                                </button>	  
                            </td>
                            <td align="center">
                                <button id="Button4" type="button" onclick="mostrarGuia('GuiaCertificaciones.pdf');" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                                    <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                                </button>	  
                            </td>
                        </tr>
                    </table>
                    </eo:PageView>
                <eo:PageView ID="PageView14" CssClass="PageView" runat="server">
                    <input type="checkbox" id="chkFor_Exam" class="check" onclick="mostrarPdtes(this);" style="margin-left:625px;" />
                    Mostrar únicamente elementos pendientes de cumplimentar
                    <table id="tblTituloExamenes" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                        <colgroup>
                            <col style='width:30px;' />
                            <col style='width:480px;'/>
                            <col style='width:80px;' />
                            <col style='width:80px;' />
                            <col style='width:230px;' />
                            <col style='width:30px;' />
                        </colgroup>
                        <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                            <td></td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgExam1" border="0">
                                <MAP name="imgExam1">
			                        <AREA onclick="ot('tblDatosExamen', 1, 0, '')" shape="RECT" coords="0,0,6,5">
			                        <AREA onclick="ot('tblDatosExamen', 1, 1, '')" shape="RECT" coords="0,6,6,11">
		                        </MAP>
                                Denominacion</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgExam2" border="0">
                                <MAP name="imgExam2">
			                        <AREA onclick="ot('tblDatosExamen', 2, 0, 'fec')" shape="RECT" coords="0,0,6,5">
			                        <AREA onclick="ot('tblDatosExamen', 2, 1, 'fec')" shape="RECT" coords="0,6,6,11">
		                        </MAP>
                                Obtención</td>
                            <td>
                                <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgExam3" border="0">
                                <MAP name="imgExam3">
			                        <AREA onclick="ot('tblDatosExamen', 3, 0, 'fec')" shape="RECT" coords="0,0,6,5">
			                        <AREA onclick="ot('tblDatosExamen', 3, 1, 'fec')" shape="RECT" coords="0,6,6,11">
		                        </MAP>
                                Caducidad</td>
                            <td>Documento</td>
                            <td title="Certificados de los que forma parte el examen">Cert.</td>
                        </tr>
                    </table>
                    <div id="divCatalogoExamenes" style="width:946px; height:260px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">
                        <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                        </div>
                    </div>
                    <table id="Table10" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                        <tr class="TBLFIN">
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:380px; margin-left:320px ; margin-top:20px;">
                        <tr>
                            <td align="center">
                                <button id="btnRenovarExamen" type="button" onclick="renovarExamen();" class="btnH25W85" 
                                    runat="server" hidefocus="hidefocus" onmouseover="se(this, 25); mostrarCursor(this);" 
                                    title="Renovación de examen">
                                    <img src="../../../Images/Botones/imgModificar.gif" /><span>Renovar</span>
                                </button>	  
                            </td>
                            <td align="center">
                                <button id="btnAddExamen" type="button" onclick="solicitarExamen();" class="btnH25W85" 
                                    runat="server" hidefocus="hidefocus" onmouseover="se(this, 25); mostrarCursor(this);" 
                                    title="Solicitar creación de examen">
                                    <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                                </button>	  
                            </td>
                            <td align="center">
                                <button id="btnDelExamen" type="button" onclick="borrarExamenes();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Eliminar el examen seleccionado">
                                    <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                                </button>	  
                            </td>
                            <td align="center">
                                <button id="btnGuiaExam" type="button" onclick="mostrarGuia('GuiaCertificaciones.pdf');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                                    <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                                </button>	  
                            </td>
                        </tr>
                    </table>
                </eo:PageView>
            </eo:MultiPage>
        </eo:PageView>		
        <eo:PageView ID="PageView7" CssClass="PageView" runat="server">
        <!-- Pestaña 4 -->
            <!--<label id='lblAnadirIdioma' onclick="mantIdioma(-1,-1);" style='cursor:pointer;margin-left:3px'>&nbsp;Añadir</label>-->
            <input type="checkbox" id="chkFor_Idi" class="check" style="margin-left:630px;" onclick="mostrarPdtes(this)" />
            Mostrar únicamente títulos pendientes de cumplimentar
            <table id="tblTituloIdiomas" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                <colgroup>
                    <col style='width:25px;' />
                    <col style='width:295px;' />
                    <col style='width:30px;' />
                    <col style='width:30px;' />
                    <col style='width:30px;' />
                    <col style='width:430px;' />
                    <col style='width:90px;' />
                </colgroup>
                <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                    <td></td>
                    <td>Idioma</td>
                    <td title="Nivel de lectura"><img src="../../../Images/imgGafas.png" /></td>
                    <td title="Nivel de escritura"><img src="../../../Images/imgLapiz.png" /></td>
                    <td title="Nivel de conversación"><img src="../../../Images/imgConversacion.png" /></td>
                    <td>Título</td>
                    <td>Obtención</td>
                </tr>
            </table>
            <div id="divCatalogoIdiomas" style="width:946px; height:300px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">
                <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                </div>
            </div>
            <table id="Table5" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div style="margin-top: 3px">
                <img src="../../../Images/imgNivelAlto.png" class="ICO" />Nivel alto 
                <img src="../../../Images/imgNivelMedio.png" class="ICO" style="margin-left:20px;" />Nivel medio 
                <img src="../../../Images/imgNivelBajo.png" class="ICO" style="margin-left:20px;" />Nivel bajo 
            </div>
            <center>
            <table style="width:340px; margin-top: 2px;">
            <tr>
                <td align="center">
                    <button id="btnAddIdioma" type="button" onclick="mantIdioma(-1,-1);" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Añadir idioma">
                        <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="btnDelIdioma" type="button" onclick="borrarIdiomas();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Eliminar el idioma seleccionado">
                        <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="Button5" type="button" onclick="mostrarGuia('GuiaIdiomas.pdf');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                        <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                    </button>	  
                </td>
            </tr>
            </table>
            </center>
        </eo:PageView>		
    </eo:MultiPage>
    </eo:PageView>
    <eo:PageView ID="PageView3" CssClass="PageView" runat="server">				
    <!-- Pestaña 3 -->
    <eo:TabStrip runat="server" id="tsPestanasExp" ControlSkinID="None" Width="975" 
				    MultiPageID="mpContenidoExp" 
				    ClientSideOnLoad="CrearPestanasExp" 
				    ClientSideOnItemClick="getPestana"
				    >
	    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		    <Items>
				    <eo:TabItem Text-Html="En Ibermática" ToolTip="" Width="150"></eo:TabItem>
				    <eo:TabItem Text-Html="Fuera de Ibermática" ToolTip="" Width="170"></eo:TabItem>
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
            Image-Mode="TextBackground" 
            Image-BackgroundRepeat="RepeatX">
        </eo:TabItem>
    </LookItems>
    </eo:TabStrip>
    <eo:MultiPage runat="server" id="mpContenidoExp" CssClass="FMP" Width="975" Height="420px">
        <eo:PageView ID="PageView10" CssClass="PageView" runat="server">
        <!-- Pestaña 1 -->
            <!--<label id="lblExpIber" runat="server" onclick="cargar('cargarExpIb');">En Ibermática </label>-->
            <input type="checkbox" id="chkExpDentro" class="check" style="margin-left:630px;" onclick="mostrarPdtes(this)" />
            Mostrar únicamente elementos pendientes de cumplimentar
            <table id="Table6" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                <colgroup>
                    <col style='width:20px;' />
                    <col style='width:440px;'/>
                    <col style='width:330px;'/>
                    <col style='width:70px;'/>
                    <col style='width:70px;'/>
                </colgroup>
                <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                    <td></td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgDen" border="0">
                        <MAP name="imgDen">
				            <AREA onclick="ot('tblExpProfIber', 1, 0, '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfIber', 1, 1, '')" shape="RECT" coords="0,6,6,11">
			            </MAP>
			            Denominación</td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCli" border="0">
                        <MAP name="imgCli">
				            <AREA onclick="ot('tblExpProfIber', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfIber', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			            </MAP>
                        Cliente</td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFI" border="0">
                        <MAP name="imgFI">
				            <AREA onclick="ot('tblExpProfIber', 3, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfIber', 3, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			            </MAP>
                        F.Inicio</td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFF" border="0">
                        <MAP name="imgFF">
				            <AREA onclick="ot('tblExpProfIber', 4, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfIber', 4, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			            </MAP>
                        F.Fin</td>
               </tr>
            </table>
            <div id="divExpIber" style="width:946px; height:320px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">  
                <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                </div>
            </div>
            <table id="Table7" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
            <center>
            <table style="width: 340px; margin-top: 2px;">
            <tr>
                <td align="center">
                    <button id="btnAddExpProfIber" type="button" onclick="anadirExpIber();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Añade experiencia profesional">
                        <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="btnDelExpProfIber" type="button" onclick="borrarExpIber();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Elimina la experiencia profesional">
                        <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="Button6" type="button" onclick="mostrarGuia('GuiaExperienciasIB.pdf');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                        <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                    </button>	  
                </td>
            </tr>
            </table>
            </center>
        </eo:PageView>		
        <eo:PageView ID="PageView11" CssClass="PageView" runat="server">
        <!-- Pestaña 2 -->
            <!--<label id="lblExpNoIber" runat="server" class="titulo2" onclick="cargar('cargarExpNoIb');">Fuera de Ibermática </label>-->
            <input type="checkbox" id="chkExpFuera" class="check" style="margin-left:630px;" onclick="mostrarPdtes(this)" />
            Mostrar únicamente elementos pendientes de cumplimentar
            <table id="Table8" style="width:930px; height:19px; margin-top:10px; margin-left:3px;">
                <colgroup>
                    <col style='width:20px;' />
                    <col style='width:440px;'/>
                    <col style='width:330px;'/>
                    <col style='width:70px;'/>
                    <col style='width:70px;'/>
                </colgroup>
                <tr class="TBLINI" style="background-image: url('../../../Images/fondoEncabezamientoListas19.gif'); background-repeat:repeat-x;">
                    <td></td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgDen2" border="0">
                        <MAP name="imgDen2">
				            <AREA onclick="ot('tblExpProfNoIber', 1, 0, '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfNoIber', 1, 1, '')" shape="RECT" coords="0,6,6,11">
			            </MAP>
			            Denominación</td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCli2" border="0">
                        <MAP name="imgCli2">
				            <AREA onclick="ot('tblExpProfNoIber', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfNoIber', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			            </MAP>
                        Empresa Contratante</td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFI2" border="0">
                        <MAP name="imgFI2">
				            <AREA onclick="ot('tblExpProfNoIber', 3, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfNoIber', 3, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			            </MAP>
                        F.Inicio</td>
                    <td>
                        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFF2" border="0">
                        <MAP name="imgFF2">
				            <AREA onclick="ot('tblExpProfNoIber', 4, 0, 'fec')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblExpProfNoIber', 4, 1, 'fec')" shape="RECT" coords="0,6,6,11">
			            </MAP>
                        F.Fin</td>
               </tr>
            </table>
             <div id="divExpNoIber"  style="width:946px; height:320px; overflow-x:hidden; overflow:auto; margin-left:3px;" runat="server">
                <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:930px; height:auto;">
                </div>
            </div>
            <table id="Table9" style="width:930px; height:17px; margin-left:3px; margin-bottom:3px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
            <center>
            <table style="width: 340px; margin-top: 2px;">
            <tr>
                <td align="center">
                    <button id="btnAddExpProfNoIber" type="button" onclick="anadirExpNoIber();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Añade experiencia profesional">
                        <img src="../../../Images/Botones/imgAnadir.gif" /><span>Añadir</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="btnDelExpProfNoIber" type="button" onclick="borrarExpNoIber();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Elimina la experiencia profesional">
                        <img src="../../../Images/Botones/imgEliminar.gif" /><span>Eliminar</span>
                    </button>	  
                </td>
                <td align="center">
                    <button id="Button7" type="button" onclick="mostrarGuia('GuiaExperienciasNoIB.pdf');" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Acceso a documento de ayuda">
                        <img src="../../../Images/Botones/imgGuia.gif" /><span>Guía</span>
                    </button>	  
                </td>
            </tr>
            </table>
            </center>
        </eo:PageView>		
    </eo:MultiPage>
    </eo:PageView>
    <eo:PageView ID="PageView12" CssClass="PageView" runat="server">				
    <!-- Pestaña General 4 -->
        <div>
            <textarea id="txtSinopsis" style="height:390px; width:965px;  margin:5px" class="txtMultiM"  onkeyup="activarGrabarS();"></textarea>
            <center>
                <button id="btnGrabarS" type="button" onclick="grabarSinopsis();" class="btnH25W90" style="margin-top:5px" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../Images/Botones/imgGrabar.gif" /><span>Grabar</span>
                </button> 
            </center>
        </div>
    </eo:PageView>
</eo:MultiPage>
<div style="margin-top: 5px">
    <img src="../../../Images/imgPseudovalidado.png" class="ICO" />Pendiente de anexar 
    <img src="../../../Images/imgPenCumplimentar.png" class="ICO" style="margin-left:20px;" />Pendiente de cumplimentar 
    <img src="../../../Images/imgBorrador.png" class="ICO" style="margin-left:20px;" />Borrador 
    <img src="../../../Images/imgPetBorrado.png" class="ICO" style="margin-left:15px;" />Pdte de eliminar    
    <img src="../../../Images/imgPenValidar.png" class="ICO" style="visibility:hidden;" />
    <img src="../../../Images/imgRechazar.png" class="ICO" style="visibility:hidden;" />
    <label style="margin-left:180px;font-weight:bold;width:140px;" runat="server">Fecha Ult.Actualización:</label><label runat="server" id="lblUltActu"></label>
</div>
<div id="divFondoFinCv" style="z-index:10; position:absolute; left:0px; top:0px; width:100%; height:100%; background-image: url(../../../Images/imgFondoOscurecido.png); background-repeat:repeat; visibility:hidden;" runat="server">
    <div id="divFinCv" style="position:absolute; top:250px; left:300px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table class="texto" style="width:400px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td colspan="2">
                        <label>Comentario</label><asp:Label ID="lblCorreo" runat="server" ForeColor="Red" style="margin-left:3px;">*</asp:Label>
                        <input type="checkbox" id="chkCorreo" class="check" onclick="setObligatoriedadMotivo()" checked="checked" style="margin-left:60px;" />
                        <label style="width:200px;">Enviar correo al profesional</label>
                        <br />
                        <asp:TextBox TextMode="MultiLine" id="txtComentario" style="width:390px; height:100px; margin-top:2px;" runat="server" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <center>
            <table class="texto" style="width:240px; margin-top:10px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td>
                        <button id="btnAceptarFinCv" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="AceptarFinCv();" onmouseover="se(this, 25);">
                            <img src="../../../Images/imgAceptar.gif" /><span>Aceptar</span>
                        </button>
                    </td>
                    <td>
                        <button id="btnCancelarFinCv" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="CancelarFinCv();" onmouseover="se(this, 25);">
                            <img src="../../../Images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
            </center>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>

<input type="hidden" id="hdnIdficepi"  runat="server" value=""/>
<input type="hidden" id="hdnPlantilla"  runat="server" value="1"/><%--Para la exportación indicamos el número de plantilla (1:Plantilla del CV completo)--%>
<input type="hidden" id="rdbFormato"  runat="server" value=""/> <%--Para la exportación--%>
<input type="hidden" id="hdnReturnValue"  runat="server" value=""/>
<input type="hidden" id="hdnEsEncargado" runat="server" value="0" />
<input type="hidden" runat="server" name="hdnEsMiCV" id="hdnEsMiCV" value="N" />
<input type="hidden" runat="server" name="hdnProf" id="hdnProf" value="-1" />
<input type="hidden" id="hdnCriterios" runat="server" value=""/>
<input type="hidden" id="hdnCamposWord" runat="server" value=""/>

<!-- Inicio para exportación de CV -->
<input type="hidden" id="hdnNombreProfesionales"  runat="server" value=""/>
<input type="hidden" id="hdnIdficepis"  runat="server" value=""/>
<!-- Fin para exportación de CV -->
<iframe id="iFrmSubida" name="iFrmSubida" frameborder="no" width="10px" height="10px" style="visibility:hidden;" ></iframe>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script language="javascript" type="text/javascript">
 function __doPostBack(eventTarget, eventArgument) {

        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "exportar":
                    {
                        bEnviar = false;
                        exportar();
                        break;
                    }
                case "guia":
                    {
                        bEnviar = false;
                        mostrarGuia("GuiaMiCV.pdf");
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

