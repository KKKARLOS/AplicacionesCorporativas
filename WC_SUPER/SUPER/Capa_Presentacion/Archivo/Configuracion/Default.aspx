<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reconexion" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
    <script language="javascript" type="text/javascript">
        $I("procesando").style.top = 120;
    </script>
    <style>
        #tblDatos tr { height: 22px; }
        #tblDatos td { padding: 5px 5px 5px 5px; }
    </style>
    <center>	
    <br /><br /><br />
        <div align="left" style="width: 800px">
            <div align="center" style="background-image: url('../../../Images/imgFondo185.gif');background-repeat:no-repeat;
                width: 185px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
                &nbsp;Configuración personal
            </div>
            <table border="0" cellpadding="0" cellspacing="0" class="texto" width="800px">
                <tr>
                    <td style="background-image:url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                    <td style="background-image:url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                    <td style="background-image:url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                </tr>
                <tr>
                    <td style="background-image:url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                    <td style="background-image:url('../../../Images/Tabla/5.gif'); padding:5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblDatos" style="width:760px" border="0" cellpadding="5px" cellspacing="0">
                            <colgroup>
                                <col style="width:180px;" />
                                <col style="width:180px;" />
                                <col style="width:400px;" />
                            </colgroup>
                            <tr>
                                <td style="padding-top:15px;" title="Activa durante el tiempo indicado o desactiva el mensaje de bienvenida para futuras sesiones.">Mensaje de bienvenida</td>
                                <td style="padding-top:15px;">
                                    <asp:DropDownList ID="cboMensaje" runat="server" Width="120px" onchange="setMensaje(this.value);">
	                                    <asp:ListItem Value="0" Text="0 segundos"></asp:ListItem>
	                                    <asp:ListItem Value="1" Text="1 segundo"></asp:ListItem>
	                                    <asp:ListItem Value="2" Text="2 segundos"></asp:ListItem>
	                                    <asp:ListItem Value="3" Text="3 segundos"></asp:ListItem>
	                                    <asp:ListItem Value="4" Text="4 segundos" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="5" Text="5 segundos"></asp:ListItem>
	                                    <asp:ListItem Value="6" Text="6 segundos"></asp:ListItem>
	                                    <asp:ListItem Value="7" Text="7 segundos"></asp:ListItem>
	                                    <asp:ListItem Value="8" Text="8 segundos"></asp:ListItem>
	                                    <asp:ListItem Value="9" Text="9 segundos"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="16" style="vertical-align:top;">
                                    <fieldset style="width:380px; padding:5px;">
                                    <legend>Resolución de pantalla</legend>
                                        <table id="Table1" border="0" cellpadding="3px" cellspacing="0" class="texto" style="width:370px; margin-top:6px; margin-left:3px;">
                                            <tr align="center">
                                                <td>
                                                    <button id="btn1024" type="button" onclick="setResPantalla('TODAS',1);" class="btnH25W100" runat="server" hidefocus="hidefocus" 
                                                        onmouseover="se(this, 25);mostrarCursor(this);" title="Establece la resolución de 1024 x 768 para todas las pantallas multiresolución.">
                                                        <img src="../../../Images/imgMonitor1024.gif" /><span>1024 x 768</span>
                                                    </button>	  
                                                </td>
                                                <td>
                                                    <button id="btn1280" type="button" onclick="setResPantalla('TODAS',2);" class="btnH25W100" runat="server" hidefocus="hidefocus" 
                                                        onmouseover="se(this, 25);mostrarCursor(this);" title="Establece la resolución de 1280 x 1024 para todas las pantallas multiresolución.">
                                                        <img src="../../../Images/imgMonitor1280.gif" /><span>1280 x 1024</span>
                                                    </button>	  
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tblResolucion" border="0" cellpadding="3px" cellspacing="0" class="texto" style="width:370px; margin-top:3px; margin-left:3px;">
                                            <colgroup>
                                                <col style="width:250px;" />
                                                <col style="width:120px;" />
                                            </colgroup>
                                            <tr>
                                                <td title="">Detalle económico (Carrusel) (PGE)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboCARRUSEL1024" runat="server" Width="100px" onchange="setResPantalla('CARRUSEL1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Avance económico de proyecto (PGE)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboAVANCE1024" runat="server" Width="100px" onchange="setResPantalla('AVANCE1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Consulta de resumen económico (PGE)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboRESUMEN1024" runat="server" Width="100px" onchange="setResPantalla('RESUMEN1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Consulta de datos resumidos (PGE)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboDATOSRES1024" runat="server" Width="100px" onchange="setResPantalla('DATOSRES1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Consulta de ficha económica (PGE)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboFICHAECO1024" runat="server" Width="100px" onchange="setResPantalla('FICHAECO1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Consulta de seguimiento de rentabilidad (PGE)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboSEGRENTA1024" runat="server" Width="100px" onchange="setResPantalla('SEGRENTA1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Avance técnico de proyecto (PST)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboAVANTEC1024" runat="server" Width="100px" onchange="setResPantalla('AVANTEC1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Estructura técnica de proyecto (PST)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboESTRUCT1024" runat="server" Width="100px" onchange="setResPantalla('ESTRUCT1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Histórico de instantáneas (PST)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboFOTOPST1024" runat="server" Width="100px" onchange="setResPantalla('FOTOPST1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Estructura técnica de plantilla (PST)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboPLANT1024" runat="server" Width="100px" onchange="setResPantalla('PLANT1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Consumos por tarea (PST)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboCONST1024" runat="server" Width="100px" onchange="setResPantalla('CONST1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Consulta de facturabilidad (IAP)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboIAPFACT1024" runat="server" Width="100px" onchange="setResPantalla('IAPFACT1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Imputación diaria (IAP)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboIAPDIARIO1024" runat="server" Width="100px" onchange="setResPantalla('IAPDIARIO1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td title="">Cuadro de mando (PGE)</td>
                                                <td>
                                                    <asp:DropDownList ID="cboCUADROMANDO1024" runat="server" Width="100px" onchange="setResPantalla('CUADROMANDO1024',this.value);">
	                                                    <asp:ListItem Value="1" Text="1024 x 768"></asp:ListItem>
	                                                    <asp:ListItem Value="2" Text="1280 x 1024"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>                                
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td title="Establece el defecto por el que se importan las imputaciones de GASVI, una vez aprobadas y contabilizadas, a los proyectos.">Imputación GASVI</td>
                                <td>
                                    <asp:DropDownList ID="cboGASVI" runat="server" Width="120px" onchange="setGASVI(this.value);">
	                                    <asp:ListItem Value="1" Text="Automática" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="Manual"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Activa o desactiva la recepción de los correos informativos generados por SUPER.">Correos informativos</td>
                                <td>
                                    <asp:DropDownList ID="cboCorreos" runat="server" Width="120px" onchange="setCorreos(this.value);">
	                                    <asp:ListItem Value="1" Text="Activados" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="Desactivados"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Activa o desactiva la periodificación automática en la creación de los proyectos en función del importe del contrato.">Periodificación de proyectos</td>
                                <td>
                                    <asp:DropDownList ID="cboPeriodificacion" runat="server" Width="120px" onchange="setPeriodificacion(this.value);">
	                                    <asp:ListItem Value="1" Text="Activada" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="Desactivada"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Activa o desactiva opción de poder abrir más de una ventana de la aplicación SUPER.">Múltiples ventanas de SUPER</td>
                                <td>
                                    <asp:DropDownList ID="cboMultiVentana" runat="server" Width="120px" onchange="setMultiVentana(this.value);">
	                                    <asp:ListItem Value="1" Text="Permitido" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="No permitido"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Indica el botón del ratón que se utilizará para mostrar un calendario de ayuda en los campos de fecha editables.">Botón para mostrar calendario</td>
                                <td>
                                    <asp:DropDownList ID="cboBotCalendario" runat="server" Width="120px" onchange="setBotCalendario(this.value);">
	                                    <asp:ListItem Value="I" Text="Izquierdo" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="D" Text="Derecho"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Establece el defecto del origen de datos para la base de cálculo pantallas de consulta. Online: mayor exactitud y mayor tiempo de respuesta. 7 AM: menor tiempo de respuesta pero posibilidad de datos no actualizados.">Base de cálculo</td>
                                <td>
                                    <asp:DropDownList ID="cboBaseCalculo" runat="server" Width="120px" onchange="setResultadoOnline(this.value);">
	                                    <asp:ListItem Value="1" Text="Online" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="7 AM"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Establece el defecto para el modo de obtener la estructura técnica de un proyecto en PST.">Obtención de estructura técnica</td>
                                <td>
                                    <asp:DropDownList ID="cboObtenerEstructura" runat="server" Width="120px" onchange="setObtenerEstructura(this.value);">
	                                    <asp:ListItem Value="1" Text="En bloque"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="A demanda" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Establece la moneda por defecto en la que ser mostrarán los datos de un proyecto.">Moneda datos de proyecto</td>
                                <td>
                                    <asp:DropDownList ID="cboMonedaVDP" runat="server" style="width:150px;" onchange="setMonedaVDP(this.value);">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Establece la moneda por defecto en la que ser mostrarán los datos consolidados de proyectos.">Moneda de datos consolidados</td>
                                <td>
                                    <asp:DropDownList ID="cboMonedaVDC" runat="server" style="width:150px;" onchange="setMonedaVDC(this.value);">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <!-- Nueva configuración para permitir al usuario a parar/animar la imagen del diamante-->
                            <tr>
                                <td title="Permite iniciar/parar la animación del Logo">Logo</td>
                                <td>
                                    <asp:DropDownList ID="cboDiamante" runat="server" style="width:150px;" onchange="window.frames['iFrmSession'].setDiamante()">
                                        <asp:ListItem Value="1" Text="Animado"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="Estático"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <label id="Label2" class="enlace" onclick="setPassw()" runat="server" title="Contraseña para el acceso a SUPER mediante Servicios Web.">Establecimiento de contraseña</label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td style="background-image:url('../../../Images/Tabla/6.gif'); width:6px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image:url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                    <td style="background-image:url('../../../Images/Tabla/2.gif'); height:6px; "></td>
                    <td style="background-image:url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                </tr>
            </table>
        </div>
    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera"></asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
    <script language="javascript" type="text/javascript">
    <!--
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			    switch (strBoton){
			    }
		    }

		    var theform = document.forms[0];
		    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		    theform.__EVENTARGUMENT.value = eventArgument;
		    if (bEnviar) theform.submit();

	    }
    -->
    </script>
</asp:Content>

