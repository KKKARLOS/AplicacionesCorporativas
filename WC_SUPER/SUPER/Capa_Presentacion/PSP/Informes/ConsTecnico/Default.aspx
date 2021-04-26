<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_psp_informes_ConsTecnico_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

    var num_proyecto = "<%=Session["NUM_PROYECTO"] %>";

    //SSRS
    var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
    if ("True" == "<%=SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()%>") usuario = 0;
    else usuario = <%=Session["UsuarioActual"]%>;
    //SSRS

</script>
<br /><br /><br />
<center>
<table border="0" cellspacing="0" cellpadding="0">
    <tr>
    <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
    <td height="6" background="../../../../Images/Tabla/8.gif"></td>
    <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
    </tr>
    <tr>
    <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
        <!-- Inicio del contenido propio de la p�gina -->

            <table style="width:665px" cellpadding='10px'>
            <colgroup>
                <col style="width:300px"/>
                <col style="width:365px"/>
            </colgroup>	            
            <tr>
                <td colspan=2 align="center">
                    <br />  
                        Concepto&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:dropdownlist id="cboConcepto" runat="server" Width="200px" CssClass="combo" onChange="javascript:CargarConcepto(this.value)">
                        </asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:label id="lblConceptoEnlace" onclick="javascript:CargarDatos($I('ctl00_CPHC_cboConcepto').value);" runat="server" CssClass="enlace" Visible="true" Width="150px"></asp:label> <br />   <br /> 

                        <fieldset style="width:470px;text-align:left;" id="fldConceptos" runat="server">
                            <div style="overflow: auto; overflow-x: hidden; height: 170px; width: 470px" id="divCatalogo">
                                <table id="tblConceptos">
                                    <%=strTablaHtml %>
                                </table>
                            </div>
                        </fieldset> 
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset id="fldTecnicos" class="fld" style="height: 80px;width: 250px;" runat="server"> 
                        <legend title="Profesionales">&nbsp;Profesionales&nbsp;</legend>     
                        <table align='center' cellpadding='10'>
                            <tr>
                                <td ><br /><br />&nbsp;
                                    <input hideFocus id="chkInternos" class="check" checked type=checkbox  runat="server" />&nbsp;&nbsp;Internos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input hideFocus id="chkExternos" class="check" checked type=checkbox  runat="server" />&nbsp;&nbsp;Externos  
                                </td>
                            </tr> 
                        </table>         
                    </fieldset>	
                </td>
                <td>
                <fieldset id="fldDesglose" class="fld" style="height: 80px;width: 315px;" runat="server"> 
                    <legend title="Informaci�n">&nbsp;Informaci�n&nbsp;</legend>
                        <table style='margin-top:17px; margin-left:40px'>
                            <tr>
                                <td>        
                                    <asp:radiobuttonlist id="rdlDesglose" runat="server" SkinId="rbl" RepeatColumns="2" RepeatDirection="Horizontal" style="width:240px;display:inline-table">
                                    <asp:ListItem Value="1">Agregada&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">Desglosada&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    </asp:radiobuttonlist>
                                </td>
                            </tr> 
                        </table>    
                </fieldset>	
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset id="fldPeriodo" class="fld" style="height: 110px;width: 250px;" runat="server">
                       <legend title="Periodo">&nbsp;Periodo&nbsp;</legend>
                        <br /><br /><br />&nbsp;&nbsp;Desde&nbsp;&nbsp;
                        <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('D');" goma="0" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('H');" goma="0" />
                    </fieldset>	
                </td>
                <td >
                    <fieldset id="FIELDSET1" class="fld" style="height: 110px;width: 315px;" runat="server">
                    <legend title="Resultado">&nbsp;Resultado&nbsp;</legend>
                        <table class='texto' border='0' cellspacing='3' cellpadding='0'>
                            <tr>
                                <td style="width:150px">
                                    <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
                                </td>
                                <td style="width:130px; vertical-align:top; text-align:center;">
                                    <fieldset id="FIELDSET2" class="fld" style="height: 30px; width:130px; text-align:center; padding-left:5px;" runat="server"> 
                                    <legend title="Formato">&nbsp;Formato&nbsp;</legend>
                                        <asp:radiobuttonlist id="rdbFormato" runat="server" Width="125px" SkinId="rbli" RepeatDirection="horizontal" onclick="setInformacion()">
                                            <asp:ListItem Value="1" Selected="True"><img src="../../../../Images/botones/imgPDF.gif" style="cursor:pointer" onclick="$I('rdbFormato_0').checked=true" title="PDF"></asp:ListItem>
                                            <asp:ListItem Value="0"><img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer" onclick="$I('rdbFormato_1').checked=true" title="Excel"></asp:ListItem>
                                            <asp:ListItem Value="2"><img src="../../../../Images/botones/imgMasivo.gif" style="cursor:pointer" onclick="$I('rdbFormato_2').checked=true" title="Masivo"></asp:ListItem>
                                        </asp:radiobuttonlist>
                                    </fieldset><br /><br />   							
					                <button id="btnObtener" type="button" onclick="Obtener();" class="btnH25W95" style="margin-left:25px;" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
						                <img src="../../../../images/botones/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
					                </button>             
                                </td>
                            </tr> 
                        </table> 						
                    </fieldset>	                
                </td>
            </tr> 
        </table>
        <!-- Fin del contenido propio de la p�gina -->
        </td>
        <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
    </tr>
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
        <td height="6" background="../../../../Images/Tabla/2.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
    </tr>
</table>
</center>
<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnConcepto" runat="server" style="visibility:hidden">0</asp:textbox>
<asp:textbox id="hdnCodConcepto" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnNomConcepto" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnDesCR" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnPerfil" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnNombre" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="FORMATO" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="NESTRUCTURA" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="CODIGO" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="FECHADESDE" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="FECHAHASTA" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="TECNICOS" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="DESGLOSADO" runat="server" style="visibility:hidden"></asp:textbox>
</asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
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
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</asp:Content>
