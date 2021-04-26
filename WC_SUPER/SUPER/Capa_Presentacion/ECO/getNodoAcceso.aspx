<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getNodoAcceso.aspx.cs" Inherits="getNodoAcceso" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Selección de <%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %> </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            actualizarLupas("tblTitulo", "tblDatos");
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila, iTipo){
        try {
            var tblDatos = $I("tblDatos");
            if (bProcesando()) return;
            var returnValue = null;
            switch(iTipo){
                case 1:
                    returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("GSB") + "@#@" + tblDatos.rows[indexFila].getAttribute("UMC") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("MC") + "@#@" + tblDatos.rows[indexFila].getAttribute("MT") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("CNP") + "@#@" + tblDatos.rows[indexFila].getAttribute("OBLCNP") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("CSN1P") + "@#@" + tblDatos.rows[indexFila].getAttribute("OBLCSN1P") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("CSN2P") + "@#@" + tblDatos.rows[indexFila].getAttribute("OBLCSN2P") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("CSN3P") + "@#@" + tblDatos.rows[indexFila].getAttribute("OBLCSN3P") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("CSN4P") + "@#@" + tblDatos.rows[indexFila].getAttribute("OBLCSN4P") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("tipolinterna") + "@#@" + tblDatos.rows[indexFila].getAttribute("tipolespecial") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("tipolproductivaSC") + "@#@" + tblDatos.rows[indexFila].getAttribute("idmoneda") +
	                                    "@#@" + tblDatos.rows[indexFila].getAttribute("denominacion_moneda") +
                                        "@#@" + tblDatos.rows[indexFila].getAttribute("prcg");
                    break;
	            case 2:
	            case 3:
	                returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText;
	                break;
	        }
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
	-->
    </script>
    <style type="text/css">      
	    #tblDatos td { padding-left: 3px; }
	    #tblDatos tr { height: 18px; }
    </style>    
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
	-->
	</script>
	<center>
    <table style="width:370px;text-align:left" cellpadding="5" >		
        <tr>
            <td>
                <table id="tblTitulo" style="width: 350px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px;">Denominación&nbsp;<IMG id="imgLupa" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
				                    height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa',event)"
				                    height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
				        </td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 366px; height:352px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:350px;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width: 350px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="300px" align="center" style="margin-top:5px;">
	    <tr>
		    <td align="center">
		        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
		    </td>
	    </tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
