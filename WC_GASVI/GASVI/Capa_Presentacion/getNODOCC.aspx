<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getNODOCC.aspx.cs" Inherits="Capa_Presentacion_getNODOCC" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="GASVI.BLL" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de <%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript">   
    <!--        
        function init(){
            try{
                if (!mostrarErrores()) return;
                ocultarProcesando();
	        }catch(e){
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
    
        function aceptarClick(oControl){
            try{
                if (bProcesando()) return;
                
                var oFila;
                while (oControl != document.body){
                    if (oControl.tagName.toUpperCase() == "TR"){
                        oFila = oControl;
                        break;
                    }
                    oControl = oControl.parentNode;
                }

//                window.returnValue = oFila.id + "@#@"
//                                    + oFila.getAttribute("des_cencos") + "@#@"
//                                    + oFila.getAttribute("idnodo") + "@#@"
//                                    + oFila.cells[0].innerText + "@#@"
//                                    + oFila.getAttribute("sMotivosEx");
//                window.close();
                var returnValue =   oFila.id + "@#@"
                                    + oFila.getAttribute("des_cencos") + "@#@"
                                    + oFila.getAttribute("idnodo") + "@#@"
                                    + oFila.cells[0].innerText + "@#@"
                                    + oFila.getAttribute("sMotivosEx");
                modalDialog.Close(window, returnValue);

            }catch(e){
                mostrarErrorAplicacion("Error seleccionar la fila", e.message);
            }
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
<body onLoad="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando1" runat="server" />
<script language="JavaScript">
<!--
    var strServer = "<% =Session["GVT_strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
-->
</script>
<br />
<table style="width:420px; margin-left:10px;">
	<tr>
		<td>
			<table id="tblTitulo" class="MA" style="height:17px; width:400px;">
			    <colgroup>
                <col style='width:400px; padding-left:3px;' />
			    </colgroup>
				<tr class="TBLINI">
					<td><%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></td>
				</tr>
			</table>
			<div id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 416px; HEIGHT: 100px;">
                <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); WIDTH: 400px;">
                <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:400px;">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table style="width:200px; margin-top:10px;text-align:center">
    <colgroup>
        <col style="width:200px" />
    </colgroup>
    <tr> 
        <td style="text-align:center">
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
        </td>
    </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
