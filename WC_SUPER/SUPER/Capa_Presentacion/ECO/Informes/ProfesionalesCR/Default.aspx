<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_eco_informes_ProfCR_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">

<script type="text/javascript">

    var num_proyecto = "<%=Session["NUM_PROYECTO"] %>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var sSubnodos = "<%=sSubnodos %>";
    <%=sCriterios %> 
    
    //SSRS
    var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
    if ("True" == "<%=SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()%>") usuario = 0;
    else usuario = <%=Session["UsuarioActual"]%>;
    t422_idmoneda = "<%=Session["MONEDA_VDC"].ToString()%>";
    ImportesEn = "* Importes en " + "<%=Session["DENOMINACION_VDC"].ToString()%>";
    //SSRS

</script>    
   
    <br /><br /><br /><br /><br />
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
    <!-- Inicio del contenido propio de la página -->
<table id='tblCatalogo' class="texto" cellspacing='0' cellpadding='18px'>
    <tr>
        <td style="width:126px; vertical-align:top;">
		    <FIELDSET id="FIELDSET1" class="fld" style="height:110px; width:290px; text-align:left" runat="server">
		    <LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
	            <table class='texto' border='0' cellspacing='5px' cellpadding='0'>
                    <tr>
                        <td colspan="2">
                            <div id="divMonedaImportes" runat="server" style="visibility:hidden">
                                <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" style="width:200px;" runat="server">Dólares americanos</label>
                            </div>
                        </td>
                    </tr>				            
	                <tr>
		                <td style="width:150px">
		                    <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
		                </td>
		                <td style="width:130px; vertical-align:top; text-align:center;" >
	                        <FIELDSET id="FIELDSET2" class="fld" style="height:30px; width:100px; text-align:center;" runat="server"> 
	                        <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
				                <asp:radiobuttonlist id="rdbFormato" runat="server" Width="100px" SkinId="rbli" RepeatDirection="horizontal">
					                <asp:ListItem Value="1" Selected="True"><img src="../../../../Images/botones/imgPDF.gif" style="cursor:pointer" onclick="$I('rdbFormato_0').checked=true" title="PDF"></asp:ListItem>
					                <asp:ListItem Value="0"><img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer" onclick="$I('rdbFormato_1').checked=true" title="Excel"></asp:ListItem>
				                </asp:radiobuttonlist>
                            </FIELDSET>  							
                            <button id="btnObtener" type="button" onclick="Obtener()" class="btnH25W90" style="margin-top:10px; margin-left:10px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                <img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                            </button>    
		                </td>
		            </tr> 
	            </table> 						
		    </FIELDSET>	
		    <br /><br />
		    <FIELDSET id="fldColaboradores" class="fld" style="height: 43px;width:140px; margin-left:60px;" runat="server"> 
                    <LEGEND class="Tooltip" title="Colaborador">&nbsp;Colaborador&nbsp;</LEGEND>   
                    <table class='texto' style='margin-top:5px;margin-left:3px;' align='center' border='0' cellspacing='0' cellpadding='0'>
		                <tr>
		                    <td>  
                                <INPUT hideFocus id="chkActivo" class="check" style="margin-left:10px;" onclick="Control(this,1)" checked type="checkbox"  runat="server" />&nbsp;&nbsp;Activo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <INPUT hideFocus id="chkBaja" class="check" onclick="Control(this,2)" type="checkbox"  runat="server" />&nbsp;&nbsp;Baja                           
		                    </td>
		                </tr> 
                    </table> 
           </FIELDSET>	    								           
        </td>
        <td>
			<table class="texto" style="text-align:left;">
			<tr>
				<td>	
					<FIELDSET style="width:100%;" id="fldConceptos" runat="server">
					    <LEGEND class="Tooltip" title="Estructura organizativa">&nbsp;<asp:label id="lblCR" onclick="javascript:CargarDatos();" runat="server" CssClass="enlace" Visible="true" Width="70px">Estructura</asp:label><img id="imgGoma" src='../../../../Images/Botones/imgBorrar.gif' title="Borra el catálogo de la estructura organizativa" onclick="borrarProfesionales()" style="cursor:pointer; vertical-align:middle; border:0px;"></LEGEND>
					    <DIV style="OVERFLOW: auto; OVERFLOW-X: hidden; HEIGHT: 180px; width: 450px" id="divCatalogo">
					    <TABLE id="tblConceptos" style="border-top:10px" width="100%">
						    <%=strTablaHtml %>
					    </TABLE>
					    </DIV>
					</FIELDSET> 
					<br />  		
				</td>
			</tr>
			</table>					
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
</center>
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnConcepto" runat="server" style="visibility:hidden;">0</asp:textbox>
<asp:textbox id="hdnCodConcepto" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNomConcepto" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnDesCR" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnPerfil" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNombre" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="FORMATO" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="ESTADO" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="CODIGO" runat="server" style="visibility:hidden;"></asp:textbox>
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