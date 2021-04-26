<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Foraneos_Consultas_Detalle_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle foráneo</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <link rel="stylesheet" href="css/estilos.css" type="text/css"/>
    <script type="text/Javascript" src="../../../../../Javascript/funciones.js"></script>
    <script type="text/Javascript" src="Functions/funciones.js?17102017"></script>
    <script language="JavaScript" src="../../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init();">
    <ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
    <script type="text/javascript">
    <!--
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var strServer = "<%=Session["strServer"]%>";
    -->
</script>

    <table style="width:900px;" border="0">
        <colgroup><col style="width:450px;" /><col style="width:450px;" /></colgroup>
        <tr>
            <td style="vertical-align:top;">
                <fieldset id="fldPersonales" style="height:160px; margin-top:5px;">
                    <legend>Datos personales</legend>
                    <label class="Titulo">Apellido1</label>
                        <asp:TextBox runat="server" id="txtApe1" MaxLength="25" onkeyup="activarGrabar();" />
                        <br />
                    <label class="Titulo">Apellido2</label>
                        <asp:TextBox runat="server" id="txtApe2" MaxLength="25" onkeyup="activarGrabar();"/>
                        <br />
                    <label class="Titulo">Nombre</label>
                        <asp:TextBox runat="server" id="txtNombre" MaxLength="20" onkeyup="activarGrabar();"/>
                        <br />
                    <label class="Titulo">E-mail</label>
                        <asp:TextBox runat="server" id="txtMail" MaxLength="50" onkeyup="activarGrabar();" style="width:200px;"/>
                    <br />
                    <label class="Titulo">CIP</label>
                        <asp:TextBox runat="server" id="txtCip" ReadOnly="true"/>
                        <br />
                    <label class="Titulo">Teléfono</label>
                        <asp:TextBox runat="server" id="txtTel" MaxLength="15" onkeyup="activarGrabar();" />
                    <fieldset id="fldSexo" >
                        <legend>Sexo</legend>
                            <asp:RadioButtonList ID="rdbSexo" CssClass="texto" runat="server" RepeatColumns="2" onclick="activarGrabar()">
                                <asp:ListItem Value="M">
                                    <img alt="" class="ICO" src="../../../../../Images/imgUsuFM.gif" title="Mujer" onclick="this.parentNode.click();" />
                                </asp:ListItem>
                                <asp:ListItem Value="V">
                                    <img alt="" class="ICO" src="../../../../../Images/imgUsuFV.gif" title="Hombre" onclick="this.parentNode.click()" />
                                </asp:ListItem>
                            </asp:RadioButtonList>
                    </fieldset>
                    
                </fieldset>
            </td>
            <td style="vertical-align:top;">
                <fieldset id="fldCredenciales" style="height:160px;">
                    <legend>Credenciales</legend>
                    <label class="Titulo" title="Fecha de último acceso">Últ. acceso</label>
                        <asp:TextBox runat="server" id="txtfultacc" ReadOnly="true"/>
                    <br />
                    <label class="Titulo">Password</label>
                        <asp:TextBox runat="server" id="txtPass" ReadOnly="true"/>
                        <br />
                    <label class="Titulo">Pregunta</label>
                        <asp:TextBox runat="server" id="txtPreg" ReadOnly="true" />
                        <br />
                    <label class="Titulo">Respuesta</label>
                        <asp:TextBox runat="server" id="txtResp" ReadOnly="true" />
                        <br />
                    <label class="Titulo" title="Fecha de establecimiento de las credenciales">Establecimiento</label>
                        <asp:TextBox runat="server" id="txtFCrea" ReadOnly="true"/>
                    <br /><br />
                    <asp:CheckBox ID="chkBloqueado" runat="server" Text="Bloqueado" style="margin-left:80px;" onClick="activarGrabar();" ToolTip="Bloquear un foráneo impide su acceso a SUPER" />
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <fieldset id="fldEmpresariales" style="width:850px;">
                    <legend>Datos empresariales</legend>
                    <table style="width:840px;" border="0">
                        <colgroup><col style="width:560px;" /><col style="width:280px;" /></colgroup>
                        <tr>
                            <td>
                                <label class="Titulo">Usuario</label>
                                <asp:TextBox ID="txtUsuario" style="width:50px; text-align:right;" Text="" readonly="true" runat="server" />
                                <label style="margin-left:60px;">Alias</label>    
                                <asp:TextBox ID="txtAlias" runat="server" style="width:303px;" MaxLength="30" onkeyup="activarGrabar()" />
                                
                            </td>
                            <td rowspan="3">
                                <fieldset style="width: 250px;">
                                    <legend>Coste</legend>  
                                    <table class="texto" style="width: 240px; height:31px;" border="0" cellpadding="5px" cellspacing="0">
                                        <tr>
                                            <td>
                                                <label style="margin-left:5px;">Moneda</label>
                                                <asp:DropDownList ID="cboMoneda" runat="server" Width="173px" onchange="activarGrabar();" AppendDataBoundItems="true"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img src="../../../../../Images/Botones/imgHorario.gif" class="ICO" style="margin-left:29px;" title="Coste por hora" hidefocus="hidefocus" />
                                                <asp:TextBox ID="txtCosteHora" SkinID="Numero" runat="server" style="width:60px;" onfocus="fn(this, 6, 4)" onkeyup="activarGrabar();if (this.value=='') this.value='0,0000';" />
                                                <img src='../../../../../Images/Botones/imgCalendario.gif' class="ICO" title="Coste por jornada" style="margin-left:20px;" hidefocus=hidefocus /> 
                                                <asp:TextBox ID="txtCosteJornada" SkinID="Numero" runat="server" style="width:60px;" onfocus="fn(this, 6, 4)" onkeyup="activarGrabar();if (this.value=='') this.value='0,0000';" />
                                            </td>
                                        </tr>
                                    </table>
                                 </fieldset> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="Titulo">Promotor</label>
                                <asp:TextBox runat="server" id="txtProm" style="width:348px;" ReadOnly="true" />
                                <label title="Fecha en la que se dió de alta al profesional en SUPER" style="margin-left:10px;">Alta</label>
                                <asp:TextBox runat="server" id="txtAltaForaneo" style="width:60px;" ReadOnly="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="Titulo" title="Fecha de alta del usuario en SUPER">Fecha alta</label>
                                <asp:TextBox runat="server" id="txtFAlta" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="activarGrabar();" />
                                <label title="Fecha de baja" style="margin-left:20px;">Fecha baja</label>
                                <asp:TextBox ID="txtFecBaja" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="activarGrabar();" />
                                <label title="Sistema de cálculo de las jornadas adaptadas" style="margin-left:25px;">Cálculo jornadas adaptadas</label>
                                <asp:DropDownList id="cboCJA" runat="server" Width="80px" onChange="activarGrabar();window.focus();" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0" Text="Diario"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Mensual"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="enlace Titulo " onclick="abrirCalendario();">Calendario</label>
                                <asp:TextBox runat="server" id="txtCal" style="width:315px;" ReadOnly="true" />
                                <label id="lblNJornLab" style="margin-left:10px;">N. Jorn. Lab.</label> <asp:TextBox ID="txtNJornLab" style="width:50px;" Text="" readonly="true" runat="server"/>
                            </td>
                            <td rowspan="2">
                                <fieldset style="width: 250px;">
                                    <legend>IAP</legend>  
                                    <table class="texto" style="width:250px; margin-left:5px;" cellpadding="5px">
                                    <tr>
                                        <td>
                                            <label id="lblUltImp">Última imputación registrada</label> 
                                            <asp:TextBox ID="txtUltImp" runat="server" style="width:60px; margin-left:2px;" readonly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkHuecos" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();activarGrabar();" checked="true" />&nbsp;Control huecos
                                            <asp:CheckBox ID="chkMailIAP" runat="server" Text="" style="cursor:pointer; vertical-align:middle; margin-left:20px;" onclick="this.blur();activarGrabar();" Checked />&nbsp;Correo recordatorio
                                        </td>
                                    </tr>
                                    </table>
                                 </fieldset> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="enlace" onclick="verProyectos();" title="Consulta de proyectos a los que está asociado">Proyectos</label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table style="width:400px; margin-left:200px; margin-top:20px;" border="0">
        <colgroup><col style="width:250px;" /><col style="width:150px;" /></colgroup>
        <tr>
            <td>
                <button type="button" class="btnH25W90" onclick="grabar()" id="btnGrabar" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../../Images/Botones/imgGrabar.gif" />
                    <span title="Salir">Grabar</span>
                </button>
            </td>
            <td>
                <button type="button" class="btnH25W90"  onclick="salir()" id="btnSalir" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../../Images/Botones/imgSalir.gif" />
                    <span title="Salir">Salir</span>
                </button>
            </td>
        </tr>
    </table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnIdFicepi" runat="server" id="hdnIdFicepi" value="-1" />
    <input type="hidden" name="hdnIdUser" runat="server" id="hdnIdUser" value="-1" />
    <input type="hidden" name="hdnIdCalendario" runat="server" id="hdnIdCalendario" value="" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
