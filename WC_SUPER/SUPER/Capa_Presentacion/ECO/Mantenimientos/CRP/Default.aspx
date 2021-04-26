<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
</script>
<center>
<TABLE class="texto" id="Table1" style="width:880px; text-align:left;">
    <colgroup><col style="width:410px;"/><col style="width:60px;"/><col style="width:410px;"/></colgroup>
	<TBODY>
		<TR>
			<TD colspan="2">
                <table class="texto" style="WIDTH: 390px;">
                    <colgroup><col style="width:130px;"/><col style="width:130px;"/><col style="width:130px;"/></colgroup>
                    <tr>
                        <td>&nbsp;Apellido1</td>
                        <td>&nbsp;Apellido2</td>
                        <td>&nbsp;Nombre</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                    </tr>
                </table>
			</TD>
            <td>
                <label id="lblNodo" runat="server" class="texto"></label>
                <asp:DropDownList id="cboCR" runat="server" Width="350px" onChange="sValorNodo=this.value;setNodo();" AppendDataBoundItems=true>
                <asp:ListItem Value="" Text=""></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDesNodo" style="width:345px;" Text="" readonly="true" runat="server" />
                <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
            </td>
		</TR>
		<TR>
			<TD>
				<TABLE id="tblTitulo" style="height:17px; width:390px;">
					<TR class="TBLINI">
						<TD>&nbsp;Profesionales&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1', event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</TD>
					</TR>
				</TABLE>
				<DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 406px; HEIGHT: 480px;" align="left" onscroll="scrollTablaProf()">
					 <div style='background-image:url(../../../../Images/imgFT20.gif); width:390px; height:auto'>
					     <TABLE class="texto" id="tblOpciones" style="WIDTH: 390px; text-align:left;" ></TABLE>
					 </DIV>
				</DIV>
				<TABLE id="tblResultado" style="height:17px; width:390px;">
					<TR class="TBLFIN"><TD></TD></TR>
				</TABLE>
			</TD>
			<TD>
			    <asp:Image id="imgPapelera" style="cursor:pointer; margin-left:10px;" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
			</TD>
			<TD>
				<TABLE id="tblTitulo2" style="height:17px; width:390px;">
					<TR class="TBLINI"><TD>&nbsp;Candidatos a responsable de proyecto</TD></TR>
				</TABLE>
				<DIV id="divCatalogo2" style="OVERFLOW:auto; OVERFLOW-X:hidden; WIDTH:406px; HEIGHT:480px;" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
				    <div style='background-image:url(../../../../Images/imgFT20.gif); width:390px; height:auto'>
					</DIV>
                </DIV>
                <TABLE id="tblResultado2" style="height:17px; width:390px;">
					<TR class="TBLFIN"><TD></TD></TR>
				</TABLE>
			</TD>
	    </TR>
    </TABLE>
</center>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <div class="clsDragWindow" id="DW" noWrap></div>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
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

