<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">var bLectura = <%=sLectura%>;</script>
<center>
<table cellspacing="5" cellpadding="5" style="width:925px;text-align:left">
    <colgroup><col style="width:360px;"/><col style="width:55px;"/><col style="width:530px;"/></colgroup>
    <tr>
        <td>
            <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionAmbito(this.id)">
            <asp:ListItem Selected="True" Value="A" Text="Nombre&nbsp;&nbsp;&nbsp;" />
            <asp:ListItem Value="C" Text="C.R.&nbsp;&nbsp;" />
            <asp:ListItem Value="G" Text="Grupo funcional&nbsp;&nbsp;&nbsp;" />
            <asp:ListItem Value="P" Text="Proyecto" />
            </asp:RadioButtonList>
        </td>
        <td colspan="2">
            <asp:Label ID="lblCliente" runat="server" Text="Cliente" SkinID="enlace" style="width:45px; margin-left:10px;" onclick="mostrarClientes();"></asp:Label>
            <asp:TextBox ID="txtDesCliente" readonly="true" style="width:490px;" runat="server" />
        </td>
    </tr>
    <tr>
	    <td>
	        <span id="ambAp" style="display:block; height:40px;" class="texto">
                <table style="width:330px;">
                    <colgroup><col style="width:110px" /><col style="width:110px" /><col style="width:110px" /></colgroup>
                    <tr>
                        <td>&nbsp;Apellido1</td>
                        <td>&nbsp;Apellido2</td>
                        <td>&nbsp;Nombre</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="txtApellido1" runat="server" style="width:95px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtApellido2" runat="server" style="width:95px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtNombre" runat="server"    style="width:95px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                    </tr>
                </table>
            </span>
            <span id="ambCR" style="display:none; height:40px;" class="texto">
                <label id="lblCR" class="enlace" style="width:330px;height:17px" runat="server" onclick="obtenerCR()">C.R.</label> 
                <asp:TextBox ID="txtCR" runat="server" style="width:327px;" readonly="true" />
            </span>
            <span id="ambGF" style="display:none; height:40px;" class="texto">
                <label id="lblGF" class="enlace" style="width:330px;height:17px" onclick="obtenerGF()">Grupo funcional</label> 
                <asp:TextBox ID="txtGF" runat="server" Width="327px" readonly="true" />
            </span>
            <span id="ambPE" style="display:none; height:40px;" class="texto">
                <label id="lblPE" class="enlace" style="width:330px" onclick="obtenerPE()">Proyecto Económico</label>
                <asp:TextBox ID="txtCodPE" runat="server" Text="" style="text-align:right; width:40px; margin-top:2px;" readonly="true" />
                <asp:TextBox ID="txtPE" runat="server" style="width:280px;" readonly="true"/>
            </span>
	    </td>
	    <td></td>
	    <td>
            <label id="lblMostrarBajas" title="Muestra u oculta los profesionales de baja en la empresa o en SUPER">Mostrar bajas</label>
            <input type="checkbox" id="chkVerBajas" class="check" onclick="mIntegrantes();" />
	    </td>
    </tr>
    <tr>
	    <td>
		    <table id="tblTitulo" style="height:17px;width:330px">
		        <colgroup><col style="width:25px" /><col style="width:305px" /></colgroup>
			    <tr class="TBLINI">
				    <td></td>
				    <td>Profesionales&nbsp;
						<img id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa5')"
						    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				        <img id="imgLupa6" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa5',event)"
						    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				    </td>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 346px; height: 400px;" align="left" onscroll="scrollTablaProf()">
			     <div style='background-image:url(../../../../../Images/imgFT20.gif); width:330px;'>
			        <table class="texto MAM" id="tblOpciones" style="width: 330px"></table>
			     </div>
		    </div>
		    <table id="tblResultado" style="height:17px;width:330px">
			    <tr class="TBLFIN">
				    <td>
				        <img height="1" src="../../../../../Images/imgSeparador.gif" border="0">
					</td>
			    </tr>
		    </table>
	    </td>
	    <td>
		    <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
		</td>
	    <td>
		    <table id="tblTitulo2" style="height:17px;width:495px">
		        <colgroup><col style="width:40px" /><col style="width:308px" /><col style="width:147px" /></colgroup>
			    <tr class="TBLINI">
				    <td></td>
				    <td>Profesional&nbsp;
				        <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',2,'divCatalogo2','imgLupa1')"
						    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				        <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',2,'divCatalogo2','imgLupa1',event)"
						    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				    <td>Código externo&nbsp;
				        <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',3,'divCatalogo2','imgLupa3')"
						    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				        <img id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',3,'divCatalogo2','imgLupa3',event)"
						    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
			    </tr>
		    </table>
		    <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 511px; height: 400px;" align="left" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
			    <div style='background-image:url(../../../../../Images/imgFT20.gif); width:495px;'>
			    <%=strTablaHTMLIntegrantes%>
			    </div>
            </div>
            <table id="tblResultado2" style="height:17px;width:495px">
			    <tr class="TBLFIN">
				    <td>
				        <img height="1" src="../../../../../Images/imgSeparador.gif" border="0">
				    </td>
			    </tr>
		    </table>
	    </td>
    </tr>
    <tr>
        <td colspan="3" style="margin-top:5px">
            <img border="0" src="../../../../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" src="../../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>&nbsp;&nbsp;&nbsp;
            <label style="color:Red">Baja en SUPER o en la empresa</label>
        </td>
    </tr>
</table>
</center>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <div class="clsDragWindow" id="DW" noWrap></div>
    <asp:TextBox ID="txtIDCliente" Width="1px" runat="server" style="visibility:hidden"/>
    <asp:TextBox ID="t305_idproyectosubnodo" runat="server" Text="" Width="1px" style="visibility:hidden;"/>
    <input type="hidden" name="hdnCRActual" id="hdnCRActual" value="" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        switch (strBoton) {
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("CodigoExterno.pdf");
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

</script>
</asp:Content>

