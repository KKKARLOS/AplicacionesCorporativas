<%@ Page language="c#" Inherits="SUPER.Mensaje" CodeFile="Mensaje.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
		<title> ::: SUPER ::: Mensaje al usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
        <meta http-equiv="X-UA-Compatible" content="IE=8"/> 
    	<script language="JavaScript" src="Javascript/funciones.js" type="text/Javascript"></script>
        <script language="JavaScript" src="Javascript/modal.js" type="text/Javascript"></script>
        <script type="text/javascript">
            function Devolver(blnDevolver) {
                try {
                    var returnValue = blnDevolver;
                    modalDialog.Close(window, returnValue);
                } catch (e) {
                    mostrarErrorAplicacion("Error al devolver el valor", e.message);
                }
            }

        </script>
	</head>
	<body class="FondoBody" style="overflow:hidden">
		<form id="Form1" method="post" runat="server">
	        <script type="text/javascript">
	            var strServer = "<%=Session["strServer"]%>";
            </script>
			<center>
                <div id="popupWin_Confirmar"
	                 style=" 
			                PADDING-RIGHT: 2px; 
			                DISPLAY: block; 
			                PADDING-LEFT: 2px; 
			                Z-INDEX: 9999; 
			                
			                PADDING-BOTTOM: 2px; 
			                PADDING-TOP: 2px; 
			                POSITION: absolute; 
			                WIDTH: 340px; 
			                HEIGHT: 190px">

	                <div id="popupWin_content_Confirmar"
		                style="
		                PADDING-RIGHT: 2px; 
		                DISPLAY: block; 
		                PADDING-LEFT: 2px; 		                
		                LEFT: 2px; 
		                PADDING-BOTTOM: 2px; 
		                OVERFLOW: hidden; 
		                PADDING-TOP: 2px; 
		                POSITION: absolute; 
		                TOP: 0px; 
		                WIDTH: 320px; 
		                HEIGHT: 180px; 
		                TEXT-ALIGN: left">
		
		                 <span style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif">
			                <p style="FONT-SIZE: 11px; PADDING-TOP: 0px"><br />
				                <table border="0" cellpadding="1" cellspacing="0" width="100%">
					                <tr><td Width="4%"></td>
						                <td width="20%" valign="middle"><img Border=0 src="<%=strUrl %>imgInfo.gif" width="48px" height="48px"></td>
						                <td width="4%"></td>
						                <td width="72%" valign="middle" class="msn">
						                <div id="mensaje" runat="server" style="height:130px;">
						                </div>
						                <br />
                                            <table id="tblMensaje" border="0" cellspacing="0" cellpadding="0" align="center" width="100%">
                                                <tr>
                                                    <td>
                                                        <table id="tblAceptar" border="0" cellspacing="0" cellpadding="0" align="center">
                                                            <tr onclick="Devolver(true);" style="cursor:pointer"> 
                                                                <td width="7px"><img src="<%=strUrl %>imgBtnIzda.gif" width="7px"></td>
								                                <td align="center" width="20" background="<%=strUrl %>bckBoton.gif"><IMG src="<%=strUrl %>imgAceptar.gif" border="0"></td>
                                                                <td width="15px" background="<%=strUrl %>bckBoton.gif" align="center" class="txtBot"><a href="#" hidefocus>&nbsp;Aceptar</a></td>
                                                                <td width="7px"><img src="<%=strUrl %>imgBtnDer.gif" width="7px"></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table id="tblCancelar" border="0" cellspacing="0" cellpadding="0" align="center">
                                                            <tr onclick="Devolver(false);" style="cursor:pointer"> 
                                                                <td width="7px"><img src="<%=strUrl %>imgBtnIzda.gif" width="7px"></td>
                                                                <td align="center" width="20" background="<%=strUrl %>bckBoton.gif"><IMG src="<%=strUrl %>imgCancelar.gif" border="0"></td>
                                                                <td width="15px" background="<%=strUrl %>bckBoton.gif" align="center" class="txtBot"><a href="#" hidefocus>&nbsp;Cancelar</a></td>
                                                                <td width="7px"><img src="<%=strUrl %>imgBtnDer.gif" width="7px"></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
					                </tr>
				                </table>
			                </p>
		                </span>
	                </div>
                </div> 
			</center>
		</form>
	</body>
</html>
