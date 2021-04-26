<%@ Page Language="c#" CodeFile="getUsuario.aspx.cs" Inherits="getUsuario" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de usuario SUPER</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
		function init(){
            try{
                if (!mostrarErrores()) return;
			    ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}

        function aceptarClick(indexFila){
            try{
                if (bProcesando()) return;
                var tblDatos = $I("tblDatos");
                var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].getAttribute("idnodo") + "@#@" + tblDatos.rows[indexFila].getAttribute("profesional");
                modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error seleccionar la fila", e.message);
            }
        }

        function cerrarVentana(){
            try{
                if (bProcesando()) return;
            
                var returnValue = null;
                modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
            }
        }
    </script>
</head>
<body style="overflow: hidden" leftmargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	    var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer   = "<%=Session["strServer"]%>";
    </script>
	<table style="width:820px; margin-left:10px; margin-top:10px; text-align:left">
		<tr>
			<td style="padding-top:10px">
			    Profesional: <asp:Label ID="lblProfesional" runat="server" Text="" /><br /><br />
				<table id="tblTitulo" style="width:800px; height:17px;">
				    <colgroup>
				        <col style="width:60px;" />
				        <col style="width:220px;" />
				        <col style="width:220px;" />
				        <col style="width:60px;" />
				        <col style="width:60px;" />
				    </colgroup>
					<tr class="TBLINI">
						<td style="text-align:right; padding-right:10px;">Usuario</td>
						<td><%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></td>
						<td>Empresa</td>
						<td>F. Alta</td>
						<td>F. Baja</td>
					</tr>
				</table>
                <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width: 816px; height:120px;">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:800px">
                    <%=strTablaHTML%>
                    </div>
                </div>
                <table style="width:800px; height:17px;">
                    <tr class="TBLFIN">
                        <td>&nbsp;</TD>
                    </tr>
                </table>
                <table width="300px" align="center" style="margin-top:8px;">
                    <tr>
	                    <td align="center">
						    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							     onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
						    </button>	 
	                    </td>
                    </tr>
                </table>
            </td>
        </tr>
	</table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
