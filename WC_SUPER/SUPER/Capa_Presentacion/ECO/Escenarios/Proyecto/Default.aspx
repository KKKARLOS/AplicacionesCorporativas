<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Escenarios_Proyecto_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<div id="divCC">
    <ul id="contextMenu">
        <!--<li><a id="cm_cut" href="#">Cortar</a></li>
        <li><a id="cm_copy" href="#">Copiar</a></li>
        <li><a id="cm_paste" href="#">Pegar</a></li>
        <div id="cm_linea" align="center" style=" padding-left:10px; margin:5px; width:80%; height: 2px; background-image:url('../../../../Images/imgSepMC.gif'); background-repeat:repeat;"></div>-->
        <li><a id="cm_addcom" href="#">Insertar comentario</a></li>
        <li><a id="cm_updcom" href="#">Editar comentario</a></li>
        <li><a id="cm_delcom" href="#">Eliminar comentario</a></li>
    </ul>
</div>
<table id="tblSuperior" style="width:990px;" border="1px">
<colgroup>
    <col style="width:450px;" />
    <col style="width:450px;" />
</colgroup>
    <tr>
        <td colspan="2"><label style="width:70px; vertical-align: middle;" title="Denominación del escenario">Denominación</label>
            <asp:TextBox ID="txtDenominacion" style="width:500px;" Text="" readonly="true" runat="server" MaxLength="50" />
        </td>
    </tr>
</table>
<br /><br />

<table id="tblGeneral" style="width:1010px; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="0" runat="server">
    <tr>
        <td style="width:590px;">
        <div id="divTituloFijo" class="divTitulo" style="width:590px;" runat="server">
        <table id='tblTituloFijo' style='width:590px;' cellpadding='0' cellspacing='0' border='0'>
            <colgroup>
               <col style='width:150px;' />
               <col style='width:125px;' />
               <col style='width:125px;' />
               <col style='width:50px;' />
               <col style='width:50px;' />
               <col style='width:90px;' />
            </colgroup>
            <tr style='height:17px; vertical-align:middle;'>
               <td rowspan="2">Partida económica</td>
               <td colspan="2">Descripción</td>
               <td rowspan="2">Coste<br />Tarifa</td>
               <td rowspan="2">Total<br />H / J</td>
               <td rowspan="2">Total<br />Presup.</td>
            </tr>
            <tr style='height:17px; vertical-align:top;'>
                <td>Motivo</td>
                <td>CR / Proveedor</td>
            </tr>
        </table>
        </div>
        </td>
        <td style="width:420px;">
        <div id="divTituloMovil" class="divTitulo" style="width:394px; overflow:hidden;" runat="server">
        <table id='tblTituloMovil' style='font-size:8pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
        </table>
        </div>
        </td>
    </tr>
    <tr>
        <td style="width:590px; vertical-align:top;">
            <div id="divBodyFijo" style="width:590px; height:340px; overflow:hidden;" runat="server"> <!--  onmousewheel="alert('sdfasdf');" -->
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:590px; height:auto;">
                <table id='tblBodyFijo' style='font-size:9pt; width:590px;' cellpadding='0' cellspacing='0' border='0'>
                </table>
                </div>
            </div>
        </td>
        <td style="width:420px; vertical-align:top;">
            <div id="divBodyMovil" style="width:410px; height:340px; overflow-x:hidden; overflow-y:auto;" runat="server" onscroll="setScrollY()">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:1300px; height:auto;">
                <table id='tblBodyMovil' style='font-size:9pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
                </table>
                </div>
            </div>
        </td>
    </tr>
    <tr style="vertical-align:top;">
        <td style="width:590px;">
            <div id="divPieFijo" style="width:590px;" runat="server">
                <table id='tblPieFijo' style='font-size:9pt; width:590px;' cellpadding='0' cellspacing='0' border='0'>
                    <colgroup>
                       <col style='width:250px;' />
                       <col style='width:150px;' />
                       <col style='width:50px;' />
                       <col style='width:50px;' />
                       <col style='width:90px;' />
                    </colgroup>
                    <tr class="TBLFIN" style="height:17px;">
                       <td colspan="4">Margen de contribución</td>
                       <td style=" text-align:center;">0,00</td>
                    </tr>
                    <tr class="TBLFIN" style="height:17px;">
                       <td colspan="4">Rentabilidad</td>
                       <td style=" text-align:center;">0,00</td>
                    </tr>
                    <tr class="TBLFIN" style="height:17px;">
                       <td colspan="4">Ingresos netos</td>
                       <td style=" text-align:center;">0,00</td>
                    </tr>
                    <tr class="TBLFIN" style="height:17px;">
                       <td colspan="4">Obra en curso / Facturación anticipada</td>
                       <td style=" text-align:center;">0,00</td>
                    </tr>
                    <tr class="TBLFIN" style="height:17px;">
                       <td colspan="4">Saldo de clientes</td>
                       <td style=" text-align:center;">0,00</td>
                    </tr>
                    <tr class="TBLFIN" style="height:17px;">
                       <td colspan="4">Grado de avance de la producción</td>
                       <td style=" text-align:center;">0,00</td>
                    </tr>
                </table>
            </div>
        </td>
        <td style="width:420px;">
            <div id="divPieMovil" style="width:394px; height:118px; overflow-x:scroll; overflow-y:hidden;" runat="server" onscroll="setScrollX()">
                <table id='tblPieMovil' style='font-size:9pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divTotalComentario" style="z-index:10; position:absolute; left:0px; top:0px; width:100%; height:100%; background-image: url('../../../../Images/imgFondoPixeladoOscuro.gif'); background-repeat:repeat; display:none;" runat="server">
    <div id="divSeguimiento" style="position:absolute; top:200px; left:300px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px;">
            <!-- Inicio del contenido propio de la página -->
            <table id="tblSeguimiento" class="texto" style="width:400px; height:200px; table-layout:fixed;" cellSpacing="2" cellPadding="0" border="0">
                <tr>
                    <td colspan="2">&nbsp;Comentario<br /><asp:TextBox TextMode="MultiLine" ID="txtComentario" SkinID="Multi" runat="server" style="width:390px; height:140px; margin-top:5px;" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <button id="btnAceptar" type="button" onclick="AceptarComentario();" class="btnH25W90" style="margin-right:20px;" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                        </button>
                    </td>
                    <td align="left">
                        <button id="btnCancelar" type="button" onclick="CancelarComentario();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>

<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdEscenario" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnMesesBorrados" runat="server" style="visibility:hidden" Text="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {

        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        $I("txtDenominacion").focus();
                        mostrarProcesando();
                        grabar();
                        break;
                    }
                case "insertarmes":
                    {
                        bEnviar = false;
                        insertarmes();
                        break;
                    }
                case "borrarmes":
                    {
                        bEnviar = false;
                        borrarmes();
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

