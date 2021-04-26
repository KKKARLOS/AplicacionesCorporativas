<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailAceptador.aspx.cs" Inherits="Capa_Presentacion_EmailAceptador" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Correo al solicitante/beneficiario</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript">
    <!--        
        function init(){
            try{
                if (!mostrarErrores()) return;
                ocultarProcesando();
                $I("txtTextoCorreo").focus();
                
            } catch (e) {
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }

        function RespuestaCallBack(strResultado, context) {
            actualizarSession();
            var aResul = strResultado.split("@#@");
            if (aResul[1] != "OK") {
                mostrarErrorSQL(aResul[3], aResul[2]);
            } else {
                switch (aResul[0]) {
                    case "enviar":
                        setTimeout("aceptar();", 20);
                        break;

                    default:
                        ocultarProcesando();
                        alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                        break;
                }
            }
        }        

	    function aceptar(){
	        //opener.$I("tblGastos").rows[opener.iFila].comentario = Utilidades.escape($I("Comentario").value);

	        //		    window.returnValue = "OK";
	        //		    window.close();
	        var returnValue = "OK";
	        modalDialog.Close(window, returnValue);	
		}
		
		function enviar() {
		    //alert("función eliminar");
		    if (!$I("chkSolicitante").checked && !$I("chkBeneficiario").checked) {
		        mmoff("War", "Debe seleccionar al menos un destinatario.", 300);
		        return;
		    }
		    
		    if ($I("txtTextoCorreo").value == "") {
		        mmoff("War", "Debe indicar el texto del correo.",230);
		        return;
		    }

		    mostrarProcesando();
		    var sb = new StringBuilder;

		    sb.Append("enviar@#@");
		    sb.Append(opener.$I("hdnReferencia").value + "@#@");

		    var sDestinatarios = "";
		    if ($I("chkSolicitante").checked)
		        sDestinatarios += $I("hdnCodRedSolicitante").value;

		    if ($I("chkBeneficiario").checked) {
		        if (sDestinatarios != "") sDestinatarios += "/";
		        sDestinatarios += $I("hdnCodRedBeneficiario").value;
		    }
		    sb.Append(sDestinatarios + "@#@");
		    sb.Append(Utilidades.escape($I("txtTextoCorreo").value));
		    
		    RealizarCallBack(sb.ToString(), "");
		}
    	
	    function cerrarVentana(){
	        //        window.returnValue = null;
	        //        window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
    -->
    </script>    
</head>
<body class="FondoBody" onLoad="init()">
<form id="Form1" name="frmDatos" runat=server>
<script language=javascript>
    var strServer = '<%=Session["GVT_strServer"].ToString()%>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<ucproc:Procesando ID="Procesando1" runat="server" />
<table style="width:430px; margin-left:10px;">
    <tr>
        <td>
        <div style="text-align:center;background-image: url('../Images/imgFondo100.gif'); background-repeat:no-repeat;
            width: 100px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
        font:bold 12px Arial;
        color:#5894ae;">Destinatarios</div>
        <table style="width:420px;"  cellpadding="0">
            <tr>
                <td background="../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../Images/Tabla/5.gif" style=" vertical-align:top; ">
                    <!-- Inicio del contenido propio de la página -->
                    <asp:CheckBox ID="chkSolicitante" CssClass="check" runat="server" /><asp:Label ID="lblSolicitante" style="width:350px" runat="server"></asp:Label><asp:TextBox ID="hdnCodRedSolicitante" runat="server" style="width:1px; visibility:hidden;" /><br />
                    <asp:CheckBox ID="chkBeneficiario" CssClass="check" runat="server" /><asp:Label ID="lblBeneficiario" style="width:350px" runat="server"></asp:Label><asp:TextBox ID="hdnCodRedBeneficiario" runat="server" style="width:1px; visibility:hidden;" />
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../Images/Tabla/6.gif" width="6">&nbsp;</td>
            </tr>
            <tr>
                <td background="../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
        </td>
    </tr>
    <tr> 
    <td>
        <div style="text-align:center;background-image: url('../Images/imgFondo100.gif'); background-repeat:no-repeat;
            width: 100px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
        font:bold 12px Arial;
        color:#5894ae;">Mensaje</div>
        <table style="width:420px;"  cellpadding="0">
            <tr>
                <td background="../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../Images/Tabla/5.gif" style="vertical-align:top; padding: 10px 5px 10px 5px">
                    <!-- Inicio del contenido propio de la página -->
                     <asp:TextBox ID="txtTextoCorreo" runat="server" SkinID=multi Text="" TextMode=multiLine  style="width:395px;" Rows="10" MaxLength="500"></asp:TextBox>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../Images/Tabla/6.gif" width="6">&nbsp;</td>
            </tr>
            <tr>
                <td background="../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td><img src="../images/imgSeparador.gif" width="1" height="10"></td>
  </tr>
</table>
<center>
    <table style="width:200px;text-align:center">
	    <tr style="text-align:center"> 
		    <td>
                <button id="btnAceptar" type="button" onclick="enviar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../Images/imgEnviarMail.png" /><span title="Enviar">Enviar</span></button>	            
		      </td>
		    <td>
		        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>
		    </td>
	      </tr>
    </table>    
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
