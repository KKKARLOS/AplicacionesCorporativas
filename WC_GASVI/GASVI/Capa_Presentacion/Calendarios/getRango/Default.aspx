<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Calendario_getRango" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Selección de rango de fechas</title>
	<meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<link href="Calendario.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/fechas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftMargin="20" topmargin="15px" onload="init()">
<form id="Form1" method="post" runat="server">
<ucproc:Procesando ID="Procesando1" runat="server" />
	<script language="Javascript">
	<!--
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["GVT_strServer"]%>";
	    var sFechaDesde = "<%=sFechaDesde%>";
        var sFechaHasta = "<%=sFechaHasta%>";
	-->
    </script>
    <div id="divMesesInicio">
        <div class="TituloMeses">Inicio</div>
        <div id="divLiteralFechaInicio" class="LiteralFecha" onclick="setMesSeleccionado('I');"></div>
            <span class="spanBotones">
                <img name="btnAntRegInicio" onclick="setAntSig('I','A')" style="cursor:pointer;" border="0" src="../../../images/btnAntRegOn.gif" width="24" height="20" />&nbsp;
                <img name="btnSigRegInicio" onclick="setAntSig('I','S')" style="cursor:pointer;" border="0" src="../../../images/btnSigRegOn.gif" width="24" height="20" />
            </span>
            <table style="width:405px; height:210px;"  cellpadding="0">
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
                        <br />
                        <table style="width:100%">
                        <tr>
                            <td>
                            <label id="lblMesIni1" class="texto LiteralMes1"></label>
                            <table id="tblMesIni1" class="TablaMes1" cellpadding="3">
                                <colgroup>
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                </colgroup>
                                <tr class="TBLINI">
                                    <td title="Lunes">L</td>
                                    <td title="Martes">M</td>
                                    <td title="Miércoles">X</td>
                                    <td title="Jueves">J</td>
                                    <td title="Viernes">V</td>
                                    <td title="Sábado">S</td>
                                    <td title="Domingo">D</td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            </td>
                            <td>
                            <label id="lblMesIni2"  class="texto LiteralMes2"></label>
                            <table id="tblMesIni2" class="TablaMes2" cellpadding="3">
                                <colgroup>
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                </colgroup>
                                <tr class="TBLINI">
                                    <td title="Lunes">L</td>
                                    <td title="Martes">M</td>
                                    <td title="Miércoles">X</td>
                                    <td title="Jueves">J</td>
                                    <td title="Viernes">V</td>
                                    <td title="Sábado">S</td>
                                    <td title="Domingo">D</td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                        </table>
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
        </table>
    </div>
        
    <div id="divMesesFin">
        <div class="TituloMeses">Fin</div>
        <div id="divLiteralFechaFin" class="LiteralFecha" onclick="setMesSeleccionado('F');"></div>
            <span class="spanBotones">
                <img name="btnAntRegFin" onclick="setAntSig('F','A')" style="cursor:pointer;" border="0" src="../../../images/btnAntRegOn.gif" width="24" height="20" />&nbsp;
                <img name="btnSigRegFin" onclick="setAntSig('F','S')" style="cursor:pointer;" border="0" src="../../../images/btnSigRegOn.gif" width="24" height="20" />
            </span>
            <table style="width:405px; height:210px;" cellpadding="0">
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
                        <br />
                        <table style="width:100%">
                        <tr>
                            <td>
                            <label id="lblMesFin1" class="texto LiteralMes1"></label>
                            <table id="tblMesFin1" class="TablaMes1" cellpadding="3">
                                <colgroup>
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                </colgroup>
                                <tr class="TBLINI">
                                    <td title="Lunes">L</td>
                                    <td title="Martes">M</td>
                                    <td title="Miércoles">X</td>
                                    <td title="Jueves">J</td>
                                    <td title="Viernes">V</td>
                                    <td title="Sábado">S</td>
                                    <td title="Domingo">D</td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            </td>
                            <td>
                            <label id="lblMesFin2" class="texto LiteralMes2"></label>
                            <table id="tblMesFin2" class="TablaMes2" cellpadding="3">
                                <colgroup>
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                    <col style="text-align:center; width:22px;" />
                                </colgroup>
                                <tr class="TBLINI">
                                    <td title="Lunes">L</td>
                                    <td title="Martes">M</td>
                                    <td title="Miércoles">X</td>
                                    <td title="Jueves">J</td>
                                    <td title="Viernes">V</td>
                                    <td title="Sábado">S</td>
                                    <td title="Domingo">D</td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="height:22px">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                        </table>
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
        </table>
    </div>
        
<table id="tblBotonera" width="200px" style="text-align:center">
    <colgroup>
        <col style="width:100px" />
        <col style="width:100px" />
    </colgroup>
	  <tr> 
        <td>
            <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>	
        </td>
        <td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>			        

		</td>
	  </tr>
</table>

    <input type="hidden" id="hdnErrores" value="<%=strErrores %>" />
    </form>
</body>
</html>
