<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Foraneos_Consultas_Catalogo_Default" Title="Página sin título" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table style="width:980px; text-align:left;">
    <tr>
     <td>
        <fieldset style="width:948px;">
            <legend>Criterios de búsqueda</legend>
            <table style="width: 940px;" id="tblFiltros" border="0">
                <colgroup>
                    <col style="width:210px"/><col style="width:200px"/><col style="width:200px"/>
                    <col style="width:200px; "/><col style="width:130px; "/>
                </colgroup>						
			    <tr>
			        <td>
			            <label style="width:70px;">Apellido1</label> 
			            <asp:TextBox ID="txtApellido1" runat="server" style="width:120px"  onkeypress="javascript:if(event.keyCode==13){obtener();event.keyCode=0;} else limpiar();" MaxLength="25" />
			        </td>
			        <td>Apellido2 <asp:TextBox ID="txtApellido2" runat="server" style="width:120px" onkeypress="javascript:if(event.keyCode==13){obtener();event.keyCode=0;} else limpiar();" MaxLength="25" /></td>
			        <td>Nombre <asp:TextBox ID="txtNombre" runat="server" style="width:120px" onkeypress="javascript:if(event.keyCode==13){obtener();event.keyCode=0;} else limpiar();" MaxLength="20" /></td>
			        <td>
			            <asp:CheckBox ID="chkBloqueados" runat="server" onclick="limpiar();" Text="" style="vertical-align:middle;cursor:pointer;"/>
			            <label style="vertical-align:middle;"> Incluir bloqueados</label>
			        </td>
			        <td>
			            <button type="button" onclick="obtener();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
						        onmouseover="se(this, 25);mostrarCursor(this);">
						    <img src="../../../../../Images/Botones/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
						</button>	
			        </td>
			    </tr>
			    <tr>
			        <td colspan="5">
			            <label class="enlace" onclick="limpiar(); abrirPromotor();" style="width:70px;" >Promotor</label>
			            <asp:TextBox ReadOnly="true" ID="txtPromotor" runat="server" idF="" style="width:498px" />
			            <img id="imgGomaPromotor" src='../../../../../Images/Botones/imgBorrar.gif' title="Borra el promotor" onclick="borrarPromotor(); limpiar();" style="cursor:pointer; vertical-align:middle;">
			        </td>
			    </tr>
		    </table>        
		</fieldset>
     </td>
    </tr>	
    <tr>
         <td>	
            <table id="tblTitulo" style="margin-top:10px; width:960px; height:17px; float:left" border="0">
            <colgroup>
                <col style="width:20px;" />
                <col style="width:310px;" />
                <col style="width:290px;" />
                <col style="width:70px;" />
                <col style="width:130px;" />
                <col style="width:70px;" />
                <col style="width:70px;" />
            </colgroup>
	            <tr class="TBLINI">
		            <td></td>
		            <td>Profesional</td>
		            <td>Promotor</td>
		            <td title="Fecha de alta">Alta</td>
		            <td title="Fecha de última conexión a SUPER">Última Conexión</td>
		            <td title="Número de proyectos en los que está de alta" style="text-align:right;" >Nº Proy.</td>
		            <td>&nbsp;Bloqueado</td>
	            </tr>
            </table>
            <img id="imgExcel" src="../../../../../Images/imgExcelAnim.gif" alt="Exporta a Excel el contenido de la tabla" class="MANO" onclick="mostrarProcesando();setTimeout('excel();',500);" style="float:left; padding-top:10px;"/>
            <div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width:976px; height:420px;" onscroll="scrollTabla()">
                <div style="background-image:url(../../../../../Images/imgFT20.gif); width:960px; height:auto;">
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:960px;">
                <tr class="TBLFIN">
                    <td align="left" style="padding-left:10px"></td>
                    <td style="text-align:right; padding-right:5px;"> &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>	
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

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