<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimiento_CentrosCoste_Default" Title="Página sin título" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
  <%@ Import Namespace="GASVI.BLL" %>
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <script type="text/javascript" language="JavaScript">
        var strServer = "<%=Session["GVT_strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
    </script>
    
	<table style="width: 530px;margin-left:20px" cellpadding="0" cellspacing="0" border="0">
        <colgroup>
        <col style="width:110px;" />
        <col style="width:420px;" />
        </colgroup>
        <tr style="height:35px;">
            <td>
                <img alt="" id="imgNE1" src='../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);"/><img alt="" id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"/><img alt="" id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);"/><img alt="" id="imgNE4" src='../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);"/><img alt="" id="imgNE5" src='../../../images/imgNE5off.gif' class="ne" onclick="setNE(5);"/><img alt="" id="imgNE6" src='../../../images/imgNE6off.gif' class="ne" onclick="setNE(6);"/>
            </td>
            <td style=" text-align:right;padding-right:2px"><asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar elementos de la estructura inactivos" /> <input type="checkbox" id="chkInact" class="check" onclick="CargarEstr();" /></td>
        </tr>
    </table>
    <div style="height:547px; width:1000px; overflow-y:auto">
        <div style="width:600px; float:left; margin-left:20px;">
            <table id="tblTituloE" style="width:530px" class="H17">
                <tr class="TBLINI">
                    <td style="padding-left:23px;">Estructura organizativa
                        <img id="imgLupa1" style="cursor:pointer; display:none;" onclick="buscarSiguiente()"
                            height="11px" src="../../../Images/imgLupaMas.gif" width="20px">
                        <img style="cursor:pointer; " onclick="buscarDescripcion()"
                            height="11px" src="../../../Images/imgLupa.gif" width="20px" >

                    </td>
                    <td style="padding-right:5px; text-align:right">Estado Gasvi</td>
                </tr>
            </table>
            <div id="divCatalogoE" style="overflow-x:hidden; overflow-y:auto; height:500px; width:546px;">
                <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); width:530px;">
                    <table id="tblEstructura" class="texto" cellpadding="0" cellspacing="0" border="0" style="width:530px;">
                        <colgroup>
                            <col width="500px"/>
                            <col width="30px"/>
                        </colgroup>
                        <thead>
                          <tr style="height:0px"> <th></th> <th></th></tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <table id="tblResultadoE" style="width:530px" class="H17">
                <tr class="TBLFIN"><td></td></tr>
            </table>
        </div>
        <div style="float:right; margin-right:20px">
            <table id="tblTituloC" style="width:320px" class="H17">
                <tr class="TBLINI">
                    <td colspan="2" style="padding-left:23px;">Centros de coste sin asignar</td>
                </tr>
            </table>
            <div id="divCatalogoCC" style="overflow-x:hidden; overflow-y:auto; height:500px; width:336px;">
                <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); width:320px;">
                    <table id="tblCentroCoste"  class="texto"  style="width:320px;" >
                        <colgroup> 
                            <col width="320px"/>
                        </colgroup>
                        <thead>
                          <tr id="tblVacia" style="height:0px"> <th></th></tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <table id="tblResultadoCC" style="width:320px" class="H17">
                <tr class="TBLFIN"><td></td></tr>
            </table>
        </div>
    </div>
    <img style="margin-left:20px" border="0" src="../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>

</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript" language="javascript">
        function __doPostBack(eventTarget, eventArgument) {
            var bEnviar = true;
            if (eventTarget.split("$")[2] == "Botonera") {
                var strBoton = Botonera.botonID(eventArgument).toLowerCase();
                switch (strBoton) {
                    //case "grabar":
                    //    {
                    //        bEnviar = false;
                    //        grabar();
                    //        break;
                    //    }
                    case "regresar":
                        {
                            if (bCambios && intSession > 0) {
                                bEnviar = false;
                                jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                                    if (answer) {
                                        bRegresar = true;
                                        grabar();
                                    }
                                    else {
                                        bCambios = false;
                                        fSubmit(true, eventTarget, eventArgument);
                                    }
                                });
                                break;
                            }
                            else
                                fSubmit(bEnviar, eventTarget, eventArgument);
                            break;
                        }
                }
                if (strBoton != "regresar")
                    fSubmit(bEnviar, eventTarget, eventArgument);
            }
        }
    function fSubmit(bEnviar, eventTarget, eventArgument) {
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

