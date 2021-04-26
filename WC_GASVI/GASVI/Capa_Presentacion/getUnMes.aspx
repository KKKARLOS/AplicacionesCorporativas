<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getUnMes.aspx.cs" Inherits="getUnMes" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Selección de mes</title>
        <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	    <script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	    <script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
	    <script language="Javascript" type="text/javascript">
	    <!--
        function init(){
            try{
                if (!mostrarErrores()) return;
                window.focus();
           }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
    	
        function aceptar(){
	        try{
//	            window.returnValue = parseInt($I("txtAnno").value, 10) * 100 + parseInt($I("cboMes").value, 10);
//	            window.close();
	            var returnValue = parseInt($I("txtAnno").value, 10) * 100 + parseInt($I("cboMes").value, 10);
	            modalDialog.Close(window, returnValue);     	            
            }catch(e){
                mostrarErrorAplicacion("Error al aceptar", e.message);
            }
        }
    	
        function setAnno(sOpcion){
	        try{
                if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
                else  $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;
            }catch(e){
                mostrarErrorAplicacion("Error al seleccionar el año", e.message);
            }
        }
    	
        function cerrarVentana(){
	        try{

	            //        window.returnValue = null;
	            //        window.close();
	            var returnValue = null;
	            modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
            }
        }
	    -->
        </script>
    </head>
    <body style="overflow:hidden; margin-left:10px;" onload="init()">
        <form id="form1" runat="server">
	    <script language="Javascript">
	    <!--
            var intSession = <%=Session.Timeout%>; 
	        var strServer = "<%=Session["GVT_strServer"]%>";
	    -->
	    </script>
	    <img src="<%=Session["GVT_strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
        <table width="250px" style="margin-left:5px; margin-top:8px">
            <colgroup>
                <col style="width:250px;" />
            </colgroup>
            <tr style="height:135px;">
                <td style="background-image: url('../Images/imgFondoCalendario.gif'); background-repeat: no-repeat;vertical-align: text-top;">&nbsp;
                    <select id="cboMes" class="combo" style="width:80px; position:absolute; top:15px; left:50px;" runat="server" onchange="window.focus();">
                        <option value="1">Enero</option>
                        <option value="2">Febrero</option>
                        <option value="3">Marzo</option>
                        <option value="4">Abril</option>
                        <option value="5">Mayo</option>
                        <option value="6">Junio</option>
                        <option value="7">Julio</option>
                        <option value="8">Agosto</option>
                        <option value="9">Septiembre</option>
                        <option value="10">Octubre</option>
                        <option value="11">Noviembre</option>
                        <option value="12">Diciembre</option>
                    </select>&nbsp;&nbsp;&nbsp;&nbsp;<img src="../Images/imgFlAnt.gif" onclick="setAnno('A')" style="cursor:pointer; position:absolute; top:20px; left:150px;" border="0" />
                    <asp:TextBox ID="txtAnno" style="width:30px; position:absolute; top:17px; left:165px;" ReadOnly="true" runat="server" Text="" />
                    <img src="../Images/imgFlSig.gif" onclick="setAnno('S')" style="cursor:pointer; position:absolute; top:20px; left:200px;" border="0" />
                </td>
            </tr>
        </table>
        <table width="250px" style="text-align:center;margin-top:15px; margin-left:20px">
	        <tr>
		        <td style="text-align:center">
		            <button id="btnExcel" type="button" onclick="aceptar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../Images/Botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>	
		        </td>
		        <td style="text-align:center">
		            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
		        </td>
	        </tr>
        </table>
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
        </form>
    </body>
</html>
