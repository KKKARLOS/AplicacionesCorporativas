<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table style="width:990px;text-align:left" align="center">
    <colgroup>
        <col style="width:480px;" />
        <col style="width:30px;" />
        <col style="width:480px;" />
    </colgroup>
	<tbody>
	<tr>
	    <td>
            <div align="center" class="texto" style="background-image: url('../../../../Images/imgFondoCal3.gif');
                width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px; background-repeat:no-repeat;">
                &nbsp;Profesionales</div>
            <table class="texto" style="width:470px; height:540px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">

                <table id="tblApellidos" border="0" class="texto" style="WIDTH: 430px;margin-bottom:5px; margin-top:2px;" cellpadding="0" cellspacing="0">
                <colgroup>
                    <col style="width:110px;" />
                    <col style="width:110px;" />
                    <col style="width:110px;" />
                    <col style="width:100px;" />
                </colgroup>
                    <tr>
                    <td>&nbsp;Apellido1</td>
                    <td>&nbsp;Apellido2</td>
                    <td>&nbsp;Nombre</td>
                    <td style="padding-left:10px;"></td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:setAppNom();if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:setAppNom();if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:setAppNom();if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td>Mostrar bajas <input type="checkbox" id="chkBajas" onclick="setAppNom();mostrarProfesionales()" class="check" runat="server" /></td>
                    </tr>
                </table>
				<table id="tblTitulo" style="WIDTH: 430px; height:17px; margin-top:2px;" >
					<tr class="TBLINI">
						<td>&nbsp;Profesional&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
					</tr>
				</table>
				<div id="divCatalogoFICEPI" style="OVERFLOW: auto; width: 446px; height: 440px;" onscroll="scrollTablaFICEPI()">
                    <div style='background-image:url(../../../../Images/imgFT20.gif); width:430px'>
                    <table style='WIDTH: 430px;'>
					</table>
					</div>
                </div>
                <table style="WIDTH: 430px; HEIGHT: 17px" cellSpacing="0" border="0">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </table>

                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
	    </td>
	    <td></td>
	    <td>
            <div align="center" class="texto" style="background-image: url('../../../../Images/imgFondoCal3.gif');
                width: 90px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;background-repeat:no-repeat;">
                &nbsp;Colectivo SUPER</div>
            <table class="texto" style="width:470px; height:540px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">

                <table class="texto" style="WIDTH: 450px; table-layout:fixed; margin-top:7px;" cellpadding="0" cellSpacing="0" border="0">
                    <colgroup>
                        <col style="width:60px;" />
                        <col style="width:370px;" />
                    </colgroup>
                    <tr style="vertical-align:top;">
                        <td>&nbsp;Usuario<br />
                            <asp:TextBox ID="txtUsuario" runat="server" style="width:50px; text-align:right; padding-right:2px;" 
                                onkeypress="preSetUsuario(event);"  MaxLength="6" />
                        </td>
                        <td align="center"><img id="imgColectivo" src="../../../../Images/imgColectivoSUPER1.gif" style="width:250px; height: 128px; border:0px;" title="Para dar de alta un nuevo usuario, seleccionar profesional y arrastrarlo hasta esta imagen." target="true" onmouseover="setTarget(this);" caso="1" /> </td>
                    </tr>
                    <tr>
                        <td colspan="2">
				        <table id="Table1" style="WIDTH: 430px; height:17px;" cellSpacing="0" cellPadding="0" border="0">
                            <colgroup>
                                <col style='width:325px;' />
                                <col style='width:40px;' />
                                <col style='width:65px;' />
                            </colgroup>
					        <tr class="TBLINI">
						        <td style='padding-left:3px;'>&nbsp;Profesionales en estado activo&nbsp;
							        <IMG id="img1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						            <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
								        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						        </td>
						        <td style='text-align:right;'>Usuario</td>
						        <td style='text-align:center;'>Alta</td>
					        </tr>
				        </table>
				        <div id="divCatalogoSUPERAlta" style="overflow: auto; overflow-x: hidden;  width: 446px; height: 150px;" onscroll="scrollTablaSUPERALTA()">
                            <div style='background-image:url(../../../../Images/imgFT20.gif); width:430px'>
                            <table style='width:430px;'>
					        </table>
					        </div>
                        </div>
                        <table style="width: 430px; height: 17px">
                            <TR class="TBLFIN">
                                <TD></TD>
                            </TR>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
				        <table id="table3" style="width: 430px; height:17px; margin-top:7px;">
                            <colgroup>
                                <col style='width:325px;' />
                                <col style='width:40px;' />
                                <col style='width:65px;' />
                            </colgroup>
					        <tr class="TBLINI">
						        <td style='padding-left:3px;'>&nbsp;Profesionales de baja&nbsp;
							        <IMG id="img2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						            <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
								        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						        </td>
						        <td style='text-align:right;'>Usuario</td>
						        <td style='text-align:center;'>Baja</td>
					        </tr>
				        </table>
				        <div id="divCatalogoSUPERBaja" style="OVERFLOW: auto; OVERFLOW-X: hidden; width: 446px; height: 150px;" onscroll="scrollTablaSUPERBAJA()">
                            <div style='background-image:url(../../../../Images/imgFT20.gif); width:430px'>
                            <table style='width: 430px;'>
					        </table>
					        </div>
                        </div>
                        <table style="width: 430px; height: 17px">
                            <TR class="TBLFIN">
                                <TD></TD>
                            </TR>
                        </table>
                        </td>
                    </tr>
                </table>


                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
	    </td>
	</tr>
	<tr>
	    <td style="padding-top: 5px;">
            &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
	    </td>
	    <td></td>
	    <td></td>
	</tr>
    </tbody>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<div class="clsDragWindow" id="DW" noWrap></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
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
</asp:Content>

