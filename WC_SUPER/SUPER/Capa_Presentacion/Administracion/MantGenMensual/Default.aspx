<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Parametrizacion" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
    <script language="javascript" type="text/javascript">
        var nAnoMesGenDialogos = <%=nAnoMesGenDialogos %>;
        var nAnoMesActual = <%=nAnoMesActual %>;    
    </script>
    <center>	
    <table style="width:850px;text-align:left;margin-top:70px" cellpadding="0" cellspacing="0" border="0" >
		<tr>		
			<td style="width:300px;vertical-align:top;">
				<div align="center" class="texto" style="background-image: url('../../../Images/imgFondoCal100.gif');background-repeat:no-repeat;
					width: 100px; height: 23px; position: relative; top: 20px; left: 50px; padding-top: 7px;" class="texto">
					&nbsp;Activación</div>
				<table class="texto" border="0" cellspacing="0" cellpadding="0" style="margin-left:30px;">
				  <tr>
					<td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
					<td height="6" background="../../../Images/Tabla/8.gif"></td>
					<td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
				  </tr>
				  <tr>
					<td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
					<td background="../../../Images/Tabla/5.gif" style="padding:5px">
						<table id="tblDatosAsig" class="texto" border="0" cellspacing="7" cellpadding="0" style="width:200px">
							<tr>
								<td align="center"><br />
									<img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer; vertical-align:bottom;" border=0  />
									<asp:TextBox ID="txtAnnoNes" style="width:100px; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
									<img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer; vertical-align:bottom;" border=0  />
								</td>
							</tr>
							<tr>
								<td align="center" style="padding-top:10px;">
									<button id="btnAsignar" type="button" onclick="asignar();" class="btnH25W95" runat="server" hidefocus="hidefocus"  title="Activa la ejecución diferida (nocturna) del proceso de generación para el mes seleccionado."
										 onmouseover="se(this, 25);mostrarCursor(this);">
										<img src="../../../images/imgClockAdd.png" /><span>&nbsp;Activar</span>
									</button>	
								</td>				
							</tr>
						</table>
					</td>
					<td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
				  </tr>
				  <tr>
					<td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
					<td height="6" background="../../../Images/Tabla/2.gif"></td>
					<td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
				  </tr>
				</table>
			</td> 	
			<td rowspan="2" style="width:50px;"></td>			
			<td rowspan="2" style="width:500px;">
			    <label>Ejecuciones de generación mensual más recientes</label>
			    <table id='tblTitulo' style='width:480px; margin-top:3px;' cellpadding='0' cellspacing='0' border='0' mantenimiento='1'>
                    <colgroup>
                        <col style='width:90px;' />
		                <col style='width:300px;' />
		                <col style='width:90px;' />
                    </colgroup>
				    <tr class="TBLINI" style="height:17px;">
				        <td style="padding-left:3px;">Mes</td>
				        <td>Profesional</td>
					    <td>F. Ejecución</td>
				    </tr>
			    </table>
			    <div id="divCatalogo" style="overflow: auto; width: 496px; height: 300px;">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:480px">
                    </div>
                </div>
                <table id="tblResultado" style="width:480px">
				    <tr class="TBLFIN"  style="height:17px;">
					    <td>&nbsp;</td>
				    </tr>
			    </table>
			</td>			
		</tr>		
        <tr>
			<td style="width:290px;vertical-align:top;"><br /><br />
                <div align="center" style="background-image: url('../../../Images/imgFondoCal100.gif');background-repeat:no-repeat; 
                    width: 100px; height: 23px; position: relative; top: 20px; left: 20px; padding-top: 7px;" class="texto">
                    &nbsp;Mes activado
                </div>
                <table border="0" cellpadding="0" cellspacing="0" class="texto">
                    <tr>
                        <td style="background-image:url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                        <td style="background-image:url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                        <td style="background-image:url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                    </tr>
                    <tr>
                        <td style="background-image:url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                        <td style="background-image:url('../../../Images/Tabla/5.gif'); padding:10px 5px 5px 5px;">
                            <!-- Inicio del contenido propio de la página -->
                            <table style="width:260px;" border="0" cellpadding="3px">
                                <colgroup>
                                    <col style="width:130px;" />
                                    <col style="width:130px;" />
                                </colgroup>
                                <tr style="height:22px;">
                                    <td align="center" colspan="2"><br />
										<asp:TextBox id="txtMesGen" style="width:100px; margin-bottom:5px; text-align:center;vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>											
                                    </td>
                                </tr>
							    <tr>
								    <td align="center">
									    <button id="btnBorrar" type="button" onclick="borrarAsig();" class="btnH25W95" runat="server" hidefocus="hidefocus"  title="Desactiva la ejecución diferida (nocturna) del proceso de generación."
										     onmouseover="se(this, 25);mostrarCursor(this);">
										    <img src="../../../images/imgClockDel.png" /><span>Desactivar</span>
									    </button>	 
								    </td>
								    <td align="center">
									    <button id="btnEjecutar" type="button" onclick="ejecutar();" class="btnH25W95" runat="server" hidefocus="hidefocus"  title="Ejecuta inmediatamente el proceso de generación que se encuentre activado."
										     onmouseover="se(this, 25);mostrarCursor(this);">
										    <img src="../../../images/botones/imgEjecutar.gif" /><span>Ejecutar</span>
									    </button>	 
								    </td>
							    </tr>
                            </table>
                            <!-- Fin del contenido propio de la página -->
                        </td>
                        <td style="background-image:url('../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="background-image:url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                        <td style="background-image:url('../../../Images/Tabla/2.gif'); height:6px; "></td>
                        <td style="background-image:url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                    </tr>
                </table>            
			</td>
		</tr>
	</table>			
	</center>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera"></asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
    <script language="javascript" type="text/javascript">
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	            //alert("strBoton: "+ strBoton);
	            switch (strBoton) {
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
    </script>
</asp:Content>

