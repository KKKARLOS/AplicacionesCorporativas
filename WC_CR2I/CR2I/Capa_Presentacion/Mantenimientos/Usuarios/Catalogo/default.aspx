<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Mantenimientos.Usuarios.Catalogo.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
	    var strInicialSeleccionada = "";
	    var strServer = "<%=Session["strServer"]%>";
	</script>
    <center>
	<table class="texto" id="Table1" cellspacing="1px" cellpadding="1px" style='width:820px; text-align:left;' border="0">
		<tr>
			<td width="470px">&nbsp;<%
							string strEstilo;
							Response.Write("<div id='abc' class='texto'>");
							for (int intIndice=65;intIndice<=78;intIndice++){
								if (strInicial == ((char)intIndice).ToString()){
									strEstilo = "linkInicialesSelec";
								}else{
									strEstilo = "linkIniciales";
								}
						%><A class="<%=strEstilo%>" style="CURSOR: pointer" 
                            onclick='javascript:mostrarProfesional("<%=(char)intIndice%>")'><%=(char)intIndice%></A>&nbsp;&nbsp;&nbsp;<%
							}
							if(strInicial == "Ñ"){
								strEstilo = "linkInicialesSelec";
							}else{
								strEstilo = "linkIniciales";
							}
						%><A class="<%=strEstilo%>" style="CURSOR: pointer" 
                            onclick='javascript:mostrarProfesional("Ñ")'>Ñ</A>&nbsp;&nbsp;&nbsp;<%
							for (int intIndice=79;intIndice<=90;intIndice++){
								if (strInicial == ((char)intIndice).ToString()){
									strEstilo = "linkInicialesSelec";
								}else{
									strEstilo = "linkIniciales";
								}
						%><A class="<%=strEstilo%>" style="CURSOR: pointer" 
                            onclick='javascript:mostrarProfesional("<%=(char)intIndice%>")'><%=(char)intIndice%></A>&nbsp;&nbsp;&nbsp;<%  
							}
							Response.Write("</div>");
						%>
			</td>
			<td style="vertical-align:bottom;" width="350px">
                Apellido1
				<asp:TextBox class="textareaTexto" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional(this.value);event.keyCode=0;}"
					id="txtApellido" runat="server" Width="180">
				</asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id="tblTitulo" height="17px" cellspacing="0" cellpadding="0" width="800px" border="0" name="tblTitulo">
                    <colgroup>
                        <col style="width:350px;"/>
                        <col style="width:90px;"/>
                        <col style="width:90px;"/>
                        <col style="width:90px;"/>
                        <col style="width:90px;"/>
                        <col style="width:90px;"/>
                    </colgroup>
					<tr class="tituloColumnaTabla">
						<td><IMG style=" cursor: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20"> <IMG id="imgLupa1" style=" display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20">&nbsp;&nbsp;Usuarios</td>
						<td>CR2I</td>
						<td>Reunión</td>
						<td>Video</td>
						<td>Telerreunión</td>
						<td>Wifi</td>
					</tr>
				</table>
				<div id="divCatalogo" style="OVERFLOW-X: hidden; overflow: auto; width: 816px; height: 512px; text-align:left;" name="divCatalogo">
					<table class="texto MA" id="tblOpciones" style=" width: 800px; text-align:left;" cellspacing="0" border="0">
						<tr>
							<td>&nbsp;</td>
						</tr>
					</table>
				</div>
				<table id="tblResultado" height="17px" cellspacing="0" cellpadding="0" width="800px" style='text-align:left;'
					border="0" name="tblResultado">
					<tr class="textoResultadoTabla">
						<td colSpan="2"><IMG height="1" src="../../../../Images/imgSeparador.gif" width="1" border="0">
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
    </center>
	<input id="hdnErrores" type="hidden" value="<%=sErrores%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	            switch (strBoton) {
	                case "inicio":
	                case "regresar":
	                    {
	                        bEnviar = false;
	                        window.open("../../../Default.aspx", "CR2I", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 365) + "px,left=" + eval(screen.availWidth / 2 - 510) + "px,width=1010px,height=705px");
	                        break;
	                    }
	            }
	        }
	        var theform = document.forms[0];
	        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	        theform.__EVENTARGUMENT.value = eventArgument;
	        if (bEnviar) theform.submit();
	    }
	</script>
</asp:Content>
