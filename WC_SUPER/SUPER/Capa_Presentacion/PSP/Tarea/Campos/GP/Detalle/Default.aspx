<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title> ::: SUPER ::: - Asignación de profesionales a equipos de profesionales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../../PopCalendar/CSS/Classic.css"type="text/css" rel="stylesheet" />
    <script src="../../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
    .txtLMA  /* Caja de texto transparente con cursor mano azul 2*/
    {
        border: 0px;
        padding: 2px 0px 0px 2px;
        margin: 0px;
        font-size: 11px;
        background-color: Transparent;
        font-family: Arial, Helvetica, sans-serif;
        height: 14px;
        cursor: url('../../../../../../images/imgManoAzul2.cur'),url('../../../../../images/imgManoAzul2.cur'),url('../../../../images/imgManoAzul2.cur'),url('../../../images/imgManoAzul2.cur'),url('../../images/imgManoAzul2.cur'),url('../images/imgManoAzul2.cur'),pointer;
    }
    #tblDatos tr { height: 20px; }
    #tblOpciones2 tr { height: 20px; }
    </style>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" name="form1" runat="server" action="Default.aspx" method="POST" enctype="multipart/form-data">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
</script>  
<center>	
<table cellspacing="1" cellpadding="1" style="width:880px;margin-top:30px;text-align:left">
<colgroup><col style="width:880px" "/></colgroup>
    <tr>
        <td align="center">
            <table border="0" cellspacing="0" cellpadding="0" style="width:480px; margin-top:20px">
              <tr>
                <td width="6" height="6" background="../../../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../../../Images/Tabla/5.gif" style="padding:5px;">
	                    <table id="tblDatos2" cellspacing="0" cellpadding="0" style="width:480px;text-align:left;">
	                        <colgroup>
	                            <col style="width:70px" />
	                            <col style="width:410px"/>
	                        </colgroup>
	                        <tr>
	                            <td>Denominación</td>
	                            <td><asp:TextBox id="txtDesGP" runat="server" MaxLength="50" Width="350px" onKeyUp="activarGrabar();"></asp:TextBox></td>
	                        </tr>
	                    </table>
                </td>
                <td width="6" background="../../../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
	<tr>
		<td style="vertical-align:top; width:75%;">
			<fieldset id="fldSeleccion" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 0px; height: 470px; width:890px;"
				runat="server"><legend>Selección de integrantes</legend>
                <table cellspacing="1" cellpadding="1" style="width:890px; text-align:left"> 
            	    <colgroup>
                        <col style="width:366px" />
                        <col style="width:118px"/>
                        <col style="width:406px"/>
                    </colgroup>       
			        <tbody>    				
	                <tr style="height:35px;">
                        <td>										    
                            <table class="texto"style="width: 360px; margin-top:15px">
                                <colgroup><col style="width:120px;" /><col style="width:120px;" /><col style="width:120px;" /></colgroup>
                                <tr>
                                    <td>&nbsp;Apellido1</td>
                                    <td>&nbsp;Apellido2</td>
                                    <td>&nbsp;Nombre</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtApe1" runat="server"  style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtApe2" runat="server"  style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtNom"  runat="server"  style="width:105px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                </tr>
                            </table>	
                        </td>
                        <td>&nbsp;</td>
                        <td>
                        </td>
                    </tr>	
				    <tr>
					    <td>
						    <table id="tblTitulo" style="height:17px;width:350px; margin-top:15px">
							    <tr class="TBLINI">
								    <td>&nbsp;Profesionales&nbsp;
									    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
										    height="11" src="../../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								        <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1',event)"
										    height="11" src="../../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								    </td>
							    </tr>
						    </table>    
						    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 366px; height: 360px;" onscroll="scrollTablaProf()">
							     <div style='background-image:url(../../../../../../Images/imgFT20.gif); width:350px;'>
							        <table style="width: 350px"></table>
							     </div>
						    </div>
						    <table id="tblResultado" style="height:17px;width:350px">
							    <tr class="TBLFIN"><td></td></tr>
						    </table>
					    </td>
					    <td align="center">
						    <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
						</td>
					    <td>
						    <table id="tblTitulo2" style="height:17px;width:390px; margin-top:15px">
							    <tr class="TBLINI">
								    <td>&nbsp;Integrantes&nbsp;
									    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',2,'divCatalogo2','imgLupa2')"
										    height="11" src="../../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								        <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',2,'divCatalogo2','imgLupa2',event)"
										    height="11" src="../../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								    </td>
							    </tr>
						    </table>
						    <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 406px; height: 360px;" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
							    <div style='background-image:url(../../../../../../Images/imgFT20.gif); width:390px;'>
							        <%=strTablaHTMLIntegrantes%>
							    </div>
                            </div>
                            <table id="tblResultado2" style="height:17px;width:390px">
							    <tr class="TBLFIN"><td></td></tr>
						    </table>
					    </td>
			        </tr>
                    <tr>
		                <td style="padding-top:15px; word-spacing:3px;" colspan="3">
                            &nbsp;<img class="ICO" src="../../../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
                            <img id="imgForaneo" class="ICO" src="../../../../../../Images/imgUsuFVM.gif" runat="server" />
                            <label id="lblForaneo" runat="server">Foráneo</label>
		                </td>
                        <td></td>
                        <td></td>
                    </tr>
		        </tbody>
		        </table>
		</fieldset>
		<br />
	</td>
    </tr>
	<tr>
		<td align="center">				        
			<table align="center" style="margin-top:45px;margin-left:10px;width:300px">
            <colgroup>
                <col style="width:150px"/>
                <col style="width:150px"/>
            </colgroup>
				<tr>
					<td align="center">
						<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
								onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
						</button>	
					</td>						
					<td align="center">
						<button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
								onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
						</button>	 
					</td>
				</tr>
			</table>				
		</td>
	</tr>
</table>
</center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <div class="clsDragWindow" id="DW" noWrap></div>
    <asp:TextBox ID="hdnIdGp" runat="server" style="visibility: hidden"></asp:TextBox>
    <input type="hidden" id="hdnErrores" value="<%=sErrores%>" />
</form>
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
</body>
</html>
