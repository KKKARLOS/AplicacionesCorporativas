<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_mantIdioma_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title> ::: SUPER ::: - Detalle de Idioma</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
	<link href="../../../../PopCalendar/CSS/Classic.css"type="text/css" rel="stylesheet" />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../../../PopCalendar/PopCalendar.js"type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
    .txtLMA  /* Caja de texto transparente con cursor mano azul 2*/
    {
        border: 0px;
        padding: 2px 0px 0px 2px;
        margin: 0px;
        font-size: 11px;
        background-color: Transparent;
        font-family: Arial, Helvetica, sans-serif;
        height: 14px;
        cursor: url('../../../../../../images/imgManoAzul2.cur'),url('../../../../../images/imgManoAzul2.cur'),url('../../../../images/imgManoAzul2.cur'),url('../../../images/imgManoAzul2.cur'),url('../../images/imgManoAzul2.cur'),url('../images/imgManoAzul2.cur'),pointer;
    }
    #tblDatos tr { height: 20px; }
    </style>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()" onbeforeunload="UnloadValor();">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" name="form1" runat="server" action="Default.aspx" method="POST" enctype="multipart/form-data">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    var sIDDocuAux = "<% =sIDDocuAux %>";
</script>  
<table style="text-align:left; margin-top:10px; margin-left:20px; width:510px;" cellspacing="2" cellpadding="2" border="0">
    <tr>
        <td>
            <label id="lblIdioma" style=" margin-left:148px;">Idioma</label><asp:Label ID="Label1" runat="server" ForeColor="Red" style="width:20px;text-align:left; margin-left:3px;">*</asp:Label>
            <asp:DropDownList id="cboIdioma" runat="server" style="width:150px;" onchange="setIdioma();" AppendDataBoundItems="true" CssClass="combo">
                <asp:ListItem Selected="True"></asp:ListItem>
            </asp:DropDownList> 
            <input type="text" class="txtM" id="txtIdioma" readonly="readonly" runat="server" style="width:150px;display:none; margin-left:3px;" />
        </td> 
    </tr>    
    <tr>
        <td>
            <fieldset id="fstNivel" style="width:210px;margin-left:147px">
            <legend style="padding-left:5px;">Nivel <asp:Label ID="Label4" runat="server" ForeColor="Red">*</asp:Label></legend>
                <table id="tblNivel" style="width:200px; margin-top:5px; margin-left:5px;" border="0">
                <colgroup>
                    <col style="width:50px;" />
                    <col style="width:50px;" />
                    <col style="width:50px;" />
                    <col style="width:50px;" />
                </colgroup>
                <tr>
                    <td></td>
                    <td><img src="../../../../Images/imgTextoAlto.png" /></td>
                    <td><img src="../../../../Images/imgTextoMedio.png" /></td>
                    <td><img src="../../../../Images/imgTextoBajo.png" /></td>
                </tr>
                <tr>
                    <td><img src="../../../../Images/imgLecturaHuman.png" title="Nivel de lectura" /></td>
                    <td colspan="3" align="left">
                        <input id="rdbLectura_0" type="radio" name="rdbLectura" runat="server" value="1" style="margin-left:15px; cursor:pointer;" onclick="aG();" />
                        <input id="rdbLectura_1" type="radio" name="rdbLectura" runat="server" value="2" style="margin-left:25px; cursor:pointer;" onclick="aG();" />
                        <input id="rdbLectura_2" type="radio" name="rdbLectura" runat="server" value="3" style="margin-left:28px; cursor:pointer;" onclick="aG();" />
                    </td>
                </tr>
                <tr>
                    <td><img src="../../../../Images/imgEscrituraHuman.png" title="Nivel de escritura" /></td>
                    <td colspan="3" align="left">
                        <input id="rdbEscritura_0" type="radio" name="rdbEscritura" runat="server" value="1" style="margin-left:15px; cursor:pointer;" onclick="aG();" />
                        <input id="rdbEscritura_1" type="radio" name="rdbEscritura" runat="server" value="2" style="margin-left:25px; cursor:pointer;" onclick="aG();" />
                        <input id="rdbEscritura_2" type="radio" name="rdbEscritura" runat="server" value="3" style="margin-left:28px; cursor:pointer;" onclick="aG();" />
                    </td>
                </tr>
                <tr>
                    <td><img src="../../../../Images/imgConversacionHuman.png" title="Nivel de conversación" /></td>
                    <td colspan="3" align="left">
                        <input id="rdbConversacion_0" type="radio" name="rdbConversacion" runat="server" value="1" style="margin-left:15px; cursor:pointer;" onclick="aG();" />
                        <input id="rdbConversacion_1" type="radio" name="rdbConversacion" runat="server" value="2" style="margin-left:25px; cursor:pointer;" onclick="aG();" />
                        <input id="rdbConversacion_2" type="radio" name="rdbConversacion" runat="server" value="3" style="margin-left:28px; cursor:pointer;" onclick="aG();" />
                    </td>
                </tr>
                </table>                    
            </fieldset>
        </td>
    </tr>
</table>
<fieldset style="height:280px; width:510px; margin-top:10px; margin-left:20px; margin-right:20px;" id="fldFiltros" runat="server">
<legend id="lgnTitulos">Títulos</legend>
    <table id="tblTitulo" style="width:485px;height:17px; margin-top:10px; margin-left:10px;" border="0">
    <colgroup>
        <col style='width:25px;' />
        <col style='width:40px;' />
        <col style='width:230px;' />
        <col style='width:120px;' />
        <col style='width:70px;' />
    </colgroup>
    <tr class="TBLINI">
        <td></td>
        <td></td>
        <td>Denominación</td>
        <td>Centro</td>
        <td>Fecha</td>
    </tr>
    </table>       
    <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; WIDTH: 501px; height:170px; margin-left:10px;" runat="server"  name="divCatalogo">
    <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:485px; height:auto;">
        <%=strTablaHTML%>
    </div>
    </div>
    <table id="tblResultado" style="height:17px; margin-left:10px;width:485px">
        <tr class="TBLFIN">
            <td>
            </td>
        </tr>
    </table>
     <center>
        <table width="250px" style="margin-top:5px;">
            <tr>
                <td align="center"> 
                    <button id="btnAnadir" type="button" onclick="addTitulo();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../../../Images/Botones/imgAnadir.gif" /><span title="Añadir título">Añadir</span>
                    </button>
                </td>
                <td align="center">
                    <button id="btnEliminar" type="button" onclick="EliminarTitulo();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../../../Images/Botones/imgEliminar.gif" /><span title="Eliminar título">Eliminar</span>
                    </button>
                </td>
            </tr>
        </table>
    </center>
    <div style="margin-left:5px;">
        <img src="../../../../Images/imgTitulo.png" class="ICO" />Documento acreditativo 
    </div>
</fieldset>
<center>
    <table width="375px" style="margin-top:7px;">
        <tr>
            <td align="center"> 
                <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../Images/Botones/imgGrabar.gif" /><span title="Grabar">Grabar</span>
                </button>	  
            </td>
            <td align="center">
                <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../Images/Botones/imgSalir.gif" /><span>Salir</span>
                </button>   
            </td>
        </tr>
    </table>
</center>
        
<input type="hidden" name="hdnErrores" id="hdnErrores" runat="server" value="" />
<input type="hidden" id="hdnOP"  runat="server" value="0"/>
<input type="hidden" id="hdnIdCodIdioma"  runat="server" value=""/>
<input type="hidden" id="hdnIdCodIdiomaEntrada"  runat="server" value=""/>
<input type="hidden" id="hdnIdFicepi"  runat="server" value=""/>
<input type="hidden" id="hdnReturnValue"  runat="server" value=""/>
<input type="hidden" name="hdnEsEncargado" id="hdnEsEncargado" runat="server" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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
</body>
</html>