<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_JsonNet_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <label title="cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>Pepe<br><label style='width:70px'>NODO:</label>dennodo] hideselects=[off]" >Botón de prueba: </label>
    <label id="lblTitle">Hola</label>
    <button id="btnWriteFile" runat="server">Test grabar fichero</button>
    <br /><br />
    <button id="btnTestConfirm" runat="server">Test jConfirm anidado</button>
    <br /><br />
    <button id="btnTestAj" runat="server">Test Ajax</button>
    <br /><br />
    <button id="btnTestIberdok" title="Simula la llamada desde IBERDOK al servicio SUPER que genera un correo para avisar al usuario de que su pedido está listo" runat="server">Test IBERDOK (expl)</button>
    <br /><br />
    <button id="btnTestIberdok2" title="Simula la llamada desde IBERDOK al servicio SUPER que genera un correo para avisar al usuario de que su pedido está listo" runat="server">Test IBERDOK (local)</button>
    <br /><br />
    <button id="btnCarpetaIberdok" title="Prueba de acceso a la carpeta compartida de IBERDOK" runat="server">Acceso carpeta IBERDOK</button>
    <br /><br />
    <button id="btnQTCNDrupalGET" title="QTCNObtenerPaises. Servicio REST(GET) de QTCNDrupal" runat="server">QTCNDrupal Obtener países</button>
    <br /><br />
    <button id="btnQTCNDrupalGET2" title="QTCNObtenerPaises. Servicio REST(GET) de QTCNDrupal en eplotación" runat="server">QTCNDrupal Obtener países (explotación)</button>
    <br /><br />
    <button id="btnQTCNDrupal" title="QTCNRegistrarInscripcionOferta. Servicio REST(POST) de QTCNDrupal" runat="server">QTCNDrupal Apuntarse a oferta</button>
    <br /><br />
    <button id="btnQTCNDrupalPOST" title="Prueba servicio REST(POST) de QTCNDrupal" runat="server">QTCNDrupal Prueba</button>
    <br /><br />
    <button id="btnLlamarModal" title="Llamada a ventana modal" runat="server">Nodo</button>
    <br /><br />
    <table>
        <tr>
            <td>
                <button id="btnSAP" title="Prueba llamada a servicio SAP para obtener saldos vivos" runat="server">SAP</button>
            </td>
            <td>
                <button id="btnSAP2" title="Prueba 2 llamada a servicio SAP para obtener saldos vivos" runat="server">SAP2</button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

