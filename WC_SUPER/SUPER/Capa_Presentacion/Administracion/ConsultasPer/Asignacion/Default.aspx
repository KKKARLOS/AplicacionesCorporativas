<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">      
    #tblDatos td { padding-left: 5px; }
    #tblDatos tr { height: 16px; }
</style>
    
<center>
<table style="width: 516px;text-align:left;">
    <tr>
        <td>
	        <table style="width: 500px; height: 17px">
		        <tr class="TBLINI">
			        <td width="500px">Denominación</td>
		        </tr>
	        </table>
	        <div id="divCons" style="overflow: auto; width: 516px; height:206px">
	            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:500px%">
	                <%=strTablaHTML%>
                </div>
            </div>
	        <table style="width: 500px; height: 17px">
		        <tr class="TBLFIN">
			        <td> </td>
		        </tr>
	        </table>
        </td>
    </tr>
</table>
<br />
<table cellspacing="1" cellpadding="1" style="width:850px;">
	<tr>
		<td style="vertical-align:top;" width="75%">
			<fieldset id="fldSeleccion" style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 0px; HEIGHT: 300px; width:850px;" runat="server">
			<legend>Selección de profesionales asignados</legend>
            <table cellspacing="1" cellpadding="1" ="" style="width:850px; text-align:left">
		    <colgroup><col style="width:380px"/><col style="width:40px"/><col style="width:430px"/></colgroup>
	                <tr>
		                <td colspan="3">
                            <table style="width: 350px;">
                                <tr>
                                    <td>&nbsp;Apellido1</td>
                                    <td>&nbsp;Apellido2</td>
                                    <td>&nbsp;Nombre</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px; cursor:not-allowed;"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" readonly="true" ToolTip="Debe seleccionar una consulta para poder asociar profesionales a la misma" /></td>
                                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px; cursor:not-allowed;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" readonly="true" ToolTip="Debe seleccionar una consulta para poder asociar profesionales a la misma" /></td>
                                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px; cursor:not-allowed;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" readonly="true" ToolTip="Debe seleccionar una consulta para poder asociar profesionales a la misma" /></td>
                                </tr>
                            </table>
		                </td>
	                </tr>
				    <tr>
					    <td>
						    <table id="tblTitulo" style="height:17px;width:350px">
							    <TR class="TBLINI">
								    <td>&nbsp;Profesionales&nbsp;
									    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
										    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								        <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1',event)"
										    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								    </td>
							    </tr>
						    </table>
						    <div id="divCatalogo" style="OVERFLOW: auto; width: 366px; height: 180px;" align="left" onscroll="scrollTablaProf()">
							     <div style='background-image:url(../../../../Images/imgFT20.gif); width:350px'>
							     <table class="texto" id="tblOpciones" style="WIDTH: 350px;" align="left"></table>
							     </div>
						    </div>
						    <table id="tblResultado" style="height:17px;width:350px">
							    <tr class="TBLFIN"><td></td></tr>
						    </table>
					    </td>
					    <td>
						    <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
						</td>
					    <td>
						    <table id="tblTitulo2" style="height:17px;width:400px">
							    <tr class="TBLINI">
								    <td width="75%">&nbsp;Asociados&nbsp;
									    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',2,'divCatalogo2','imgLupa2')"
										    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								        <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',2,'divCatalogo2','imgLupa2',event)"
										    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								    </td>
								    <td width="25%" align="right">Activo&nbsp;</td>
							    </tr>
						    </table>
						    <div id="divCatalogo2" style="OVERFLOW: auto; width: 416px; height: 180px;" align="left" target="true" onmouseover="setTarget(this);" caso="1"  onscroll="scrollTablaProfAsig()">
							    <div style='background-image:url(../../../../Images/imgFT20.gif); width:400px'>
							    <%=strTablaHTMLIntegrantes%>
							    </div>
                            </div>
                            <table id="tblResultado2" style="height:17px;width:400px">
							    <tr class="TBLFIN"><td></td></tr>
						    </table>
					    </td>
			        </tr>
			        <tr>
                        <td colspan="3"style="padding-top:4px;">
                            <img border="0" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
                            <img border="0" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                            <img id="imgForaneo" src="../../../../Images/imgUsuFVM.gif" runat="server" />
                            <label id="lblForaneo" runat="server">Foráneo</label>
                        </td>
			        </tr>
	        </table>
		    </fieldset>
	    </td>
    </tr>
</table>
</center>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <div class="clsDragWindow" id="DW" noWrap></div>
    <asp:TextBox ID="hdnIdCons" runat="server" style="visibility: hidden"></asp:TextBox>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
			    case "regresar":
			        {
			            if (bCambios && intSession > 0) {
			                bEnviar = false;
			                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
			                    if (answer) {
			                        bEnviar = false;
			                        bRegresar = true;
			                        setTimeout("grabar()", 20);
			                    } else {
			                        bEnviar = true;
			                        bCambios = false;
			                        fSubmit(bEnviar, eventTarget, eventArgument);
			                    }
			                });
			                break;
			            }
			            else
			                fSubmit(bEnviar, eventTarget, eventArgument);
			            break;
			        }
			}
			if (strBoton != "grabar" && strBoton != "regresar")
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

