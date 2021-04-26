<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Parametrizacion" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
    <script language="javascript" type="text/javascript">
        $I("procesando").style.top = '420px';
    </script>
    <center>	
    <table style="width:750px;text-align:left" border="0">
        <tr>
            <td style="vertical-align:top;">
                <div style="width:315px;">
                    <div align="center" style="background-image: url('../../../Images/imgFondo100.gif');background-repeat:no-repeat; 
                        width: 100px; height: 23px; position: relative; top: 20px; left: 20px; padding-top: 7px;" class="texto">
                        &nbsp;Paso a histórico
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" class="texto">
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                            <td style="background-image:url('../../../Images/Tabla/5.gif'); padding:10px 5px 5px 5px;">
                                <!-- Inicio del contenido propio de la página -->
                                <table border="0" cellpadding="3px" width="210px">
                                    <colgroup>
                                        <col style="width:150px;" />
                                        <col style="width:60px;" />
                                    </colgroup>
                                    <tr style="height:22px;">
                                        <td title="Tolearancia definida por el administrador para el paso de proyectos a estado Histórico.">Tolerancia paso histórico</td>
                                        <td>
                                            <input id="txtTolerancia" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="activarGrabar();" onfocus="fn(this,4,2);" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:22px;">
                                        <td title="Número de meses sin datos para el paso a histórico.">Meses paso histórico</td>
                                        <td>
                                            <input id="txtHistorico" type="text" style="width:50px;" maxlength="2" class="txtNumM" onkeyup="activarGrabar();" onfocus="fn(this,2,0);" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- Fin del contenido propio de la página -->
                            </td>
                            <td style="background-image:url('../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/2.gif'); height:6px; "></td>
                            <td style="background-image:url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                        </tr>
                    </table>
                </div>
                <div id="divGeneral" runat="server" style="width:315px;margin-top:10px">
                    <div align="center" style="background-image: url('../../../Images/imgFondo100.gif');background-repeat:no-repeat; 
                        width: 100px; height: 23px; position: relative; top: 20px; left: 20px; padding-top: 7px;" class="texto">
                        &nbsp;General
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" class="texto">
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                            <td style="background-image:url('../../../Images/Tabla/5.gif'); padding:10px 5px 5px 5px;">
                                <!-- Inicio del contenido propio de la página -->
                                <table border="0" cellpadding="3px" width="260px">
                                    <colgroup>
                                        <col style="width:170px;" />
                                        <col style="width:90px;" />
                                    </colgroup>
                                    <tr style="height:22px; display:none;">
                                        <td>Producción para CVT superior a</td>
                                        <td>
                                            <input id="txtProduccionCVT" type="text" style="width:65px;" maxlength="10" class="txtNumM" onkeyup="activarGrabar();" onfocus="fn(this,7,2);" runat="server" />&nbsp;€
                                        </td>
                                    </tr>
                                    <tr style="height:22px;">
                                        <td>Alertas de proyectos activas</td>
                                        <td>
                                            <input id="chkAlertas" type="checkbox" class="check" style="cursor:pointer;" onclick="activarGrabar();" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:22px;">
                                        <td>Correos CIA</td>
                                        <td>
                                            <input id="chkMailCIA" type="checkbox" class="check" style="cursor:pointer;" onclick="activarGrabar();" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- Fin del contenido propio de la página -->
                            </td>
                            <td style="background-image:url('../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/2.gif'); height:6px; "></td>
                            <td style="background-image:url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                        </tr>
                    </table>
                </div>                
                <div id="divForaneo" runat="server" style="width:315px;margin-top:20px">
                    <div align="center" style="background-image: url('../../../Images/imgFondo100.gif');background-repeat:no-repeat; 
                        width: 100px; height: 23px; position: relative; top: 20px; left: 20px; padding-top: 7px;" class="texto">
                        &nbsp;Foráneos
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" class="texto">
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                            <td style="background-image:url('../../../Images/Tabla/5.gif'); padding:10px 5px 5px 5px;">
                                <!-- Inicio del contenido propio de la página -->
                                <table border="0" cellpadding="3px" width="260px">
                                    <colgroup>
                                        <col style="width:170px;" />
                                        <col style="width:90px;" />
                                    </colgroup>
                                    <tr style="height:22px;">
                                        <td>Se admiten foráneos</td>
                                        <td>
                                            <input id="chkForaneo" type="checkbox" class="check" style="cursor:pointer;" onclick="activarGrabar();" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:22px;">
                                        <td colspan="2">
                                            <label class="enlace" onclick="setFiguras();">Figuras asignables a proyectos</label>
                                        </td>
                                    </tr>
                                </table>
                                <!-- Fin del contenido propio de la página -->
                            </td>
                            <td style="background-image:url('../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/2.gif'); height:6px; "></td>
                            <td style="background-image:url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                        </tr>
                    </table>
                </div>                
                <div style="width:315px;">
                    <div align="center" style="background-image: url('../../../Images/imgFondo100.gif');background-repeat:no-repeat; 
                        width: 100px; height: 23px; position: relative; top: 20px; left: 20px; padding-top: 7px;" class="texto">
                        &nbsp;Datos generales
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" class="texto">
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                            <td style="background-image:url('../../../Images/Tabla/5.gif'); padding:10px 5px 5px 5px;">
                                <!-- Inicio del contenido propio de la página -->
                                <table border="0" cellpadding="3px" width="210px">
                                    <colgroup>
                                        <col style="width:150px;" />
                                        <col style="width:60px;" />
                                    </colgroup>
                                    <tr style="height:22px;">
                                        <td title="Número de días a partir del cual se comunica a los comerciales el vencimiento de facturas.">Días para aviso vencimiento</td>
                                        <td>
                                            <input id="txtDiasAvisoVto" type="text" style="width:50px;" maxlength="5" class="txtNumM" onkeyup="activarGrabar();" onfocus="fn(this,4,0);" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- Fin del contenido propio de la página -->
                            </td>
                            <td style="background-image:url('../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/2.gif'); height:6px; "></td>
                            <td style="background-image:url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                        </tr>
                    </table>
                </div>            
            </td>
            <td>
                <div style="width:435px">
                    <div align="center" style="background-image: url('../../../Images/imgFondo100.gif');background-repeat:no-repeat; 
                        width:100px; height:23px; position:relative; top:20px; left:20px; padding-top:7px;" class="texto"> 
                        &nbsp;Auditorías
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" class="texto">
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                            <td style="background-image:url('../../../Images/Tabla/5.gif'); padding:5px">
                                <!-- Inicio del contenido propio de la página -->
                                <table id="Table1" border="0" cellpadding="3px" cellspacing="0" class="texto" width="410px">
                                    <colgroup>
                                        <col style="width:80px;" />
                                        <col style="width:30px;" />
                                        <col style="width:290px;" />
                                    </colgroup>
                                    <tr>
                                        <td style="vertical-align:middle;"  title="Muestra o no los botones y accesos a las pantallas de auditorías."><label style="margin-top:15px">Ocultar acceso</label></td>
                                        <td style="vertical-align:middle;">
                                            <input id="chkAccesoAu" type="checkbox" name="chkAccesoAu" style="margin-top:15px" runat="server" onclick="activarGrabar();" />
                                        </td>
                                        <td style="cursor:pointer;">
                                            <fieldset style="vertical-align:top;width:265px">
                                                <legend>Estado general</legend>
                                                <asp:RadioButtonList ID="rdbEstado" style="cursor:pointer; width:170px; margin-left:10px; vertical-align:top;" onclick="activarGrabar();" SkinID="rbl" runat="server" RepeatColumns="2">
                                                    <asp:ListItem onclick="$I('rdbEstado_0').click();" Value="1" Selected="True">Activado</asp:ListItem>
                                                    <asp:ListItem onclick="$I('rdbEstado_1').click();" Value="0">Desactivado</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <!--<tr>
                                        <td title="Establece si se activan o desactivan las auditorías de SUPER.">General</td>
                                        <td>
                                            <input id="chkGeneral" type="checkbox" name="chkGeneral" style="vertical-align:middle;" runat="server" onclick="activarGrabar();" />
                                        </td>
                                        <td></td>
                                    </tr> -->
                                    <tr>
                                        <td colspan="3">
                                            <table id="tblTitulo" style="width:390px; height:17px; margin-top:5px; table-layout:fixed; border-collapse:collapse;" cellspacing="0" cellpadding="0" border="0">
                                                <colgroup>
                                                    <col style="width:20px;" />
                                                    <col style="width:320px;" />
                                                    <col style="width:50px;" />
                                                </colgroup>
                                                <tr class="TBLINI">
                                                    <td></td>
                                                    <td>
                                                        &nbsp;Tablas&nbsp;
	                                                    <img id="imgLupa2" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
				                                            height="11px" src="../../../../Images/imgLupaMas.gif" width="20px" tipolupa="2">
	                                                    <img id="imgLupa1" style="display:none; cursor:pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2', event);" 
	                                                        height="11px" src="../../../../Images/imgLupa.gif" width="20px" tipolupa="1">
		                                            </td>
		                                            <td>Estado</td>
                                                </tr>
                                            </table>
                                            <div id="divCatalogo" style="overflow-y:auto; overflow-x:hidden; width:406px; height:358px">
                                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:390px">
                                                    <%=strTablaHtmlTablas%>
                                                </div>
                                            </div>
                                            <table id="Table3" style="width:390px; height:17px">
                                                <tr class="TBLFIN">
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <!-- Fin del contenido propio de la página -->
                            </td>
                            <td style="background-image:url('../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../Images/Tabla/2.gif'); height:6px; "></td>
                            <td style="background-image:url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
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
                //alert("strBoton: "+ strBoton);
                switch (strBoton) {
                    case "grabar":
                        {
                            bEnviar = false;
                            grabar();
                            break;
                        }
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

