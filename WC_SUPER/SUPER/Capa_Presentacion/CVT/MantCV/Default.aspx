<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_MantCV_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<table id="tblGeneral" style="width:980px;" cellpadding="0" cellspacing="0" border="0">
<colgroup>
    <col style="width: 490px;" />
    <col style="width: 490px;" />
</colgroup>
<tr>
    <td>
        <div id="div1" style="position:absolute; left:10px; top:128px; width:490px; z-index:0;">
            <table style="width:100%; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr style="vertical-align:top;">
                    <td>
                        <table class="texto" style="width:100%; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                            <tr>
                                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
                              </tr>
                            <tr>
		                        <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                                <td background="../../../Images/Tabla/5.gif" >
                                <div align="center" style="background-image: url('../../../Images/imgFondo185.gif');background-repeat:no-repeat;
                                    width: 185px; height: 23px; position: relative; top: -15px; left: 10px; padding-top: 5px;">
                                    Búsqueda de profesionales
                                </div>
                                    <!-- Inicio del contenido propio de la página -->
                                    <table id="tblApellidos" style="WIDTH:360px; float:left;" border=0>
                                    <colgroup>
                                        <col style="width:120px"/>
                                        <col style="width:120px"/>
                                        <col style="width:120px"/>
                                    </colgroup>
                                        <tr>
                                        <td>&nbsp;Apellido1</td>
                                        <td>&nbsp;Apellido2</td>
                                        <td>&nbsp;Nombre</td>
                                        </tr>
                                        <tr>
                                        <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                        <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                        <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                        </tr>
                                    </table>
                                    <div style="float:left;margin-top:17px;margin-bottom:10px; visibility:hidden;">
                                        <input type="checkbox" id="chkSoloActivos" class="check" style="cursor:pointer;margin-right:4px;" checked="checked" title="Sólo activos"/>
                                        <label id="lblSoloActivos" style="cursor:pointer; vertical-align:bottom;" onclick="this.previousSibling.click()">Sólo activos</label> 
                                    </div>
	                                <table id="tblTitulo" style="width:450px; HEIGHT: 17px; margin-top:5px;" cellpadding="0" cellspacing="0" border="0">
		                                <tr class="TBLINI">
			                                <td style="padding-left:20px;">Profesional</td>
		                                </tr>
	                                </table>
	                                <div id="divCatalogo" style="overflow-x:hidden; overflow: auto; width: 466px; height:420px;" runat="server" onscroll="scrollTablaProf()">
                                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif);width: 450px;">  
		                                </div>
                                    </div>
                                    <table id="Table2" style="WIDTH: 450px; HEIGHT: 17px;margin-bottom:15px;" cellpadding="0" cellspacing="0" border="0">
                                        <tr class="TBLFIN">
	                                        <td></td>
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
                    </td>
                </tr>
            </table>
        </div>
    </td>
    <td>
        <div id="div2" style="position:absolute; left:510px; top:128px; width:490px; z-index:0;">
            <table style="width:100%; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr style="vertical-align:top;">
                    <td>
                        <table class="texto" style="width:100%; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                            <tr>
                                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
                              </tr>
                            <tr>
		                        <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                                <td background="../../../Images/Tabla/5.gif" >
                                <div align="center" style="background-image: url('../../../Images/imgFondo290.gif');background-repeat:no-repeat;
                                    width: 290px; height: 23px; position: relative; top: -15px; left: 10px; padding-top: 5px;">
                                    Profesionales pendientes de dar de alta en CVT
                                </div>
                                <div style="float:left;margin-top:20px;margin-bottom:7px; margin-left:1px;">
                                    <input type="checkbox" id="chkNoExternos" class="check" style="cursor:pointer;margin-right:4px; vertical-align:middle;" onclick="getProfPendientes()" checked="checked" title="Excluir profesionales externos"/><label id="Label1" style="cursor:pointer; vertical-align:middle;" onclick="this.previousSibling.click()">Excluir externos</label> 
                                </div>
                                <div style="float:right;margin-top:20px;margin-bottom:7px; margin-right:25px;">
                                    <input type="checkbox" id="chkNoCV" class="check" style="cursor:pointer;margin-right:4px; vertical-align:middle;" onclick="getProfPendientes()" title="Incluir aquellos profesionales marcados para que no les de el alta del CV"/><label id="Label2" style="cursor:pointer; vertical-align:middle;" onclick="this.previousSibling.click()">Incluir "No CV"</label> 
                                </div>
                                <!-- Inicio del contenido propio de la página -->
                                <table id="Table1" style="width:450px; HEIGHT: 17px; margin-top:39px;" cellpadding="0" cellspacing="0" border="0">
                                    <colgroup>
                                        <col style="width:275px;" />
                                        <col style="width:65px;" />
                                        <col style="width:65px;" />
                                        <col style="width:45px;" />
                                    </colgroup>
                                    <tr class="TBLINI">
	                                    <td style="padding-left:20px;">Profesional</td>
	                                    <td>Fecha alta</td>
	                                    <td>Fecha baja</td>
	                                    <td>&nbsp;No CV</td>
                                    </tr>
                                </table>	
                                <div id="divCatalogoSC" style="overflow-x:hidden; overflow: auto; width: 466px; height:420px;" runat="server" onscroll="scrollTablaProfSC()">
                                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif);width: 450px;">    
                                        <%=strTablaHTML%>
                                    </div>
                                </div>
                                <table id="Table4" style="WIDTH: 450px; HEIGHT: 17px;margin-bottom:15px;" cellpadding="0" cellspacing="0" border="0">
                                    <tr class="TBLFIN">
	                                    <td></td>
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
                </td>
            </tr>
        </table>
    </div>
</tr>
<tr>
    <td colspan="2" style="position:absolute;bottom:5px;" >
        &nbsp;<img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
        <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
    </td>
</tr>
</table>
<div id="divTotal" style="z-index:10; position:absolute; left:0px; top:0px; width:1100px; height:800px; background-image: url(../../../Images/imgFondoPixelado2.gif); background-repeat:repeat; display:none;" runat="server">
    <div id="divSeguimiento" style="position:absolute; top:200px; left:300px;">
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
            <table id="tblSeguimiento" class="texto" style="width:400px; height:200px;" cellspacing="2" cellpadding="0" border="0">
                <tr>
                    <td>
                        <label id="lblTextoSeguimiento">Para ACTIVAR un seguimiento, es preciso indicar el motivo del mismo.</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Motivo<br />
                        <asp:TextBox id="txtSeguimiento" SkinID="Multi" TextMode="multiLine" runat="server" 
                            style="width:390px; height:100px; margin-top:5px; resize:none; overflow:hidden;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button id="btnActivarDesactivar" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" 
                            style="float:left; margin-left:100px" onmouseover="se(this, 25);">
                            <img id="imgBotonActivar" src="../../../images/imgSegAdd.png" /><span id="lblBoton">Activar</span>
                        </button>
                        <button id="btnCancelarSeg" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" style="float:left; margin-left:20px"
                            onclick="CancelarSeguimiento();" onmouseover="se(this, 25);">
                            <img src="../../../images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
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

<input type="hidden" id="hdnProfesional"  runat="server" value=""/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();            
            switch (strBoton) {
                
                case "grabar":
                    {
                        bEnviar = false;
                        //setTimeout("grabar();", 20);
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
-->
</script>
</asp:Content>

