<%@ Page language="c#" Inherits="GESTAR.Capa_Presentacion.ASPX.MtoEntradas" EnableEventValidation="false" CodeFile="default.aspx.cs" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head id="cabecera" runat="server">
		<title>Detalle de la entrada</title>
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
		<link rel="stylesheet" href="../../../../../PopCalendar/CSS/Classic.css" type="text/css" />
		<link rel="stylesheet" href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css"/>

        <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
        <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
		<script language="JavaScript" src="../../../../../JavaScript/funciones.js" type="text/Javascript"></script>
		<script language="JavaScript" src="../../../../../JavaScript/funcionesTablas.js" type="text/Javascript"></script>
        <script language="JavaScript" src="../../../../../JavaScript/documentos.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>        
		<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
        <script language="JavaScript" src="../../../../../JavaScript/modal.js" type="text/Javascript"></script>
		<script type="text/javascript">
		</script>
	</head>
	<body class="FondoBody" onload="init()">
        <ucproc:Procesando ID="Procesando" runat="server" />
		<form id="frmDatos" method="post" runat="server">
		    <script type="text/javascript">

		        var strServer = "<%=Session["strServer"].ToString()%>";
	            var intSession = <%=Session.Timeout%>; 
                var bNueva = <%=Request.QueryString["bNueva"] %>;
		    </script>
        <center>
               <table style="width:96%;text-align:left" cellpadding="3px">
				    <tr>
					    <td><img style="height: 8px" src="../../../../../Images/imgSeparador.gif" align="left">
					    </td>
				    </tr>
				    <tr>					
				    <td>
					    <fieldset class="fld" style="width:99%" >
						    <table width="100%">
							    <tr>	
							    <td width="8%" valign="middle"><label class="texto" style="width:76px;">Denominación </label></td>
							    <td width="42%"><img style="width: 1px;height: 1px" src="../../../../../Images/imgSeparador.gif" align="left">
								                <asp:TextBox id="txtDenominacion" onKeyUp="javascript:ActivarGrabar();" runat="server" width="330px" MaxLength="100"
									                CssClass="textareatexto"></asp:TextBox>
								</td>
							    <td width="8%">Creador&nbsp;</td>
							    <td width="42%">							    
							    <asp:textbox id="txtCreador" onKeyUp="javascript:ActivarGrabar();" runat="server" width="330px" CssClass="textareatexto"
									    MaxLength="70" ReadOnly=true></asp:textbox>									 
							    </tr>	
							    <tr>	
							    <td width="8%" valign="middle"><asp:label id="lblOrigen" onclick="javascript:CargarDatos('Origen');" runat="server" SkinID="enlace" Visible="true">Origen</asp:label>&nbsp;</td>
							    <td width="42%"><img style="width: 1px;height: 1px" src="../../../../../Images/imgSeparador.gif" align="left">
								    <asp:textbox id="txtOrigen" runat="server" width="330px" CssClass="textareatexto"
									    MaxLength="70" ReadOnly=true></asp:textbox>&nbsp;&nbsp;<asp:image id="btnOrigen" style="cursor: pointer" onclick="javascript:$I('txtOrigen').value='';$I('hdnOrigen').value='';ActivarGrabar();" runat="server" ImageUrl="../../../../../images/imgGoma.gif"></asp:image>	
								</td>
							    <td width="8%">Comunicante&nbsp;</td>
							    <td width="42%">							    
							    <asp:textbox id="txtComunicante" onKeyUp="javascript:ActivarGrabar();" runat="server" width="330px" CssClass="textareatexto"
									    MaxLength="70"></asp:textbox>									 
							    </tr>	
							    <tr>	
							    <td width="8%">Medio</td>
							    <td width="42%">
							        <img style="width: 1px;height: 1px" src="../../../../../Images/imgSeparador.gif" align="left">
								    <asp:textbox id="txtMedio" onKeyUp="javascript:ActivarGrabar();" runat="server" width="330px" CssClass="textareatexto"
									    MaxLength="70" ></asp:textbox></td>
							    <td width="8%">Organización</td>
							    <td width="42%">
								    <asp:textbox id="txtOrganizacion" onKeyUp="javascript:ActivarGrabar();" runat="server" width="330px" CssClass="textareatexto"
									    MaxLength="70"></asp:textbox></td>
							    </tr>											
						    </table>
					    </fieldset>
				    </td>	
				    </tr>
	                <tr>
		                <td><img style="height: 8px" src="../../../../../Images/imgSeparador.gif" align="left">
		                </td>
	                </tr>
				    <tr>					
				    <td>
					    <fieldset class="fld" style="width:99%">
						    <legend title="Figuras">&nbsp;Análisis&nbsp;</legend>
						    <table width="100%" align="center">
							    <tr>	
							    <td width="8%" valign="middle"><asp:label id="lblAnalista" onclick="javascript:CargarDatos('Analista');" runat="server" SkinID="enlace" Visible="true">Profesional</asp:label>&nbsp;
							    </td>
							    <td width="42%"><asp:textbox id="txtAnalista" runat="server" width="340px" CssClass="textareatexto"
									    MaxLength="70" readonly="true"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnAnalista" style="cursor: pointer" onclick="javascript:$I('txtAnalista').value='';$I('hdnAnalista').value='';ActivarGrabar();" runat="server" ImageUrl="../../../../../images/imgGoma.gif"></asp:image></td>
							    <td width="50%" valign="middle">Fecha&nbsp;
                                    <asp:textbox id="txtFechaAnalisis" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="ActivarGrabar()" runat="server" goma="0" lectura="0"></asp:textbox>
                                    &nbsp;&nbsp;&nbsp;Orden&nbsp;<asp:textbox id="txtOrden" className="txtL" style="width:70px; vertical-align:middle; cursor:pointer;" onchange="ActivarGrabar()" runat="server" MaxLength="4"></asp:textbox>
								 </td>
							    </tr>			
						    </table>
					    </fieldset>
				    </td>	
				    </tr>
	                <tr>
		                <td><img style="height: 8px" src="../../../../../Images/imgSeparador.gif" align="left">
		                </td>
	                </tr>				    	                								   
				    <tr>
						<td>&nbsp;&nbsp;Descripción&nbsp;<br />
<asp:textbox onKeyUp="javascript:ActivarGrabar();"
									id="txtDescripcion" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="11">
									</asp:textbox>								
						</td>	
					</tr>
	                <tr>
		                <td><img style="height: 8px" src="../../../../../Images/imgSeparador.gif" align="left">
		                </td>
	                </tr>						
				    <tr>
						<td>&nbsp;&nbsp;Notas&nbsp;<br />
									<asp:textbox onKeyUp="javascript:ActivarGrabar();"
									id="txtNotas" runat="server" SkinID="Multi" width="100%" TextMode="MultiLine" Rows="11">

									</asp:textbox>									
						</td>	
					</tr>														
			    </table>	
			<br/>
            <table id="tblBotones" style="width:500px; margin-top:10px;" class="texto">
	            <tr> 
		            <td> 
			            <button id="btnGrabar" type="button" onclick="grabar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../images/botones/imgGrabar.gif" /><span title="Graba la información">Grabar</span>
			            </button>	
		            </td>
		            <td> 
			            <button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../images/botones/imgGrabarSalir.gif" /><span title="Graba la información y regresa a la pantalla">Grabar</span>
			            </button>	
		            </td>
		            <td>
			            <button id="btnSalir" type="button" onclick="salir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../images/botones/imgSalir.gif" /><span title="Regresa a la pantalla anterior">Salir</span>
			            </button>	
		            </td>
	            </tr>
            </table>
        </center>				            	                
        <asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnIDEntrada" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnOrigen" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnAnalista" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:textbox id="hdnErrores" runat="server" style="visibility:hidden" ></asp:textbox>
		<asp:TextBox ID="hdnModoLectura" runat="server" style="visibility:hidden" Text="" />			
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
            <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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
