<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getPeriodo.aspx.cs" Inherits="getPeriodo2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de periodo</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            ocultarProcesando();           
            window.focus();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function setAno(sControl, sOpcion){
        try{
            var nDesde, nHasta;
            if (sControl == "D"){
                if (sOpcion == "A"){
                    $I("txtDesde").value = parseInt($I("txtDesde").value, 10) - 1;
                }else{
                    $I("txtDesde").value = parseInt($I("txtDesde").value, 10) + 1;
                }
                nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
                nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
                if (nDesde > nHasta){
                    $I("cboHasta").value = parseInt(nDesde.toString().substring(4, 6), 10);
                    $I("txtHasta").value = nDesde.toString().substring(0, 4);
	            }
            }else{
                if (sOpcion == "A"){
                    $I("txtHasta").value = parseInt($I("txtHasta").value, 10) - 1;
                }else{
                    $I("txtHasta").value = parseInt($I("txtHasta").value, 10) + 1;
                }
                nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
                nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
                if (nDesde > nHasta){
                    $I("cboDesde").value = parseInt(nHasta.toString().substring(4, 6), 10);
                    $I("txtDesde").value = nHasta.toString().substring(0, 4);
	            }
            }
        }catch(e){
            mostrarErrorAplicacion("Error al seleccionar el año", e.message);
        }
    }
    function setMes(sControl){
        try{
            var nDesde, nHasta;
            nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
            nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
            if (sControl == "D"){
                if (nDesde > nHasta){
                    $I("cboHasta").value = $I("cboDesde").value;
	            }
            }else{
                if (nDesde > nHasta){
                    $I("cboDesde").value = $I("cboHasta").value;
	            }
            }
        }catch(e){
            mostrarErrorAplicacion("Error al seleccionar el mes", e.message);
        }
    }
	
    function aceptar(){
	    try{
            if (parseInt($I("txtDesde").value, 10) < 1990 || parseInt($I("txtHasta").value, 10) > 2078){
                mmoff("War","El rango temporal debe estar comprendido entre los años 1990 y 2078.",450);
                return;
            }
            var nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
            var nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
            //alert("nDesde: "+ nDesde +"\nnHasta: "+ nHasta);

	        var returnValue = nDesde + "@#@" + nHasta;
	        modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al aceptar", e.message);
        }
    }

    function cerrarVentana(){
	    try{
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
    
    /*
    El resultado se envía en el siguiente formato:
    "opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
    */
    function RespuestaCallBack(strResultado, context){
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            ocultarProcesando();
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        }else{
            switch (aResul[0]){
//                case "getUMCNP":
////                    alert(opener.sUltCierreEcoNodo +"\n"+ aResul[2]);
//                    if (opener.sUltCierreEcoNodo != aResul[2]){
//                        opener.sUltCierreEcoNodo = aResul[2];
//                        if (parseInt(aResul[2], 10) <= nAnomesMinimo) nAnomesMinimo = parseInt(aResul[2], 10);
//                    }
//                    break;

                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                    break;
            }
            ocultarProcesando();
        }
    }
    
	-->
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"].ToString()%>";
	    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
        var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
	    var sAdministrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
	    var nPSN = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
	-->
    </script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table class="texto" style="margin-left:15px; width:520px;" >
        <colgroup><col style="width:250px;" /><col style="width:20px;" /><col style="width:250px;" /></colgroup>
        <tr style="text-align:center; font-weight: bold;">
            <td style="padding-bottom:5px;">Inicio</td>
            <td>&nbsp;</td>
            <td style="padding-bottom:5px;">Fin</td>
        </tr>
        <tr style="height:132px;">
            <td style="background-image: url(../../../images/imgFondoCalendario.gif); background-repeat: no-repeat;">&nbsp;
                <select id="cboDesde" class="combo" style="width:80px; position:absolute; top:26px; left:55px;" onchange="setMes('D')" runat="server">
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
                </select>&nbsp;&nbsp;&nbsp;&nbsp;
                <img src="../../../Images/imgFlAnt.gif" onclick="setAno('D','A')" style="cursor: pointer; position: absolute; top: 28px; left: 165px;" border=0 />
                <asp:TextBox ID="txtDesde" style="width:30px; position:absolute; top:26px; left: 180px;" readonly="true" runat="server" Text="" />
                <img src="../../../Images/imgFlSig.gif" onclick="setAno('D','S')" style="cursor:pointer; position:absolute; top:28px; left:215px;" border=0 />
            </td>
            <td>&nbsp;</td>
            <td style="background-image: url(../../../images/imgFondoCalendario.gif); background-repeat: no-repeat;">
                <select id="cboHasta" class="combo" style="width:80px; position:absolute; top: 26px; left:325px;" onchange="setMes('D')" runat="server">
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
                </select>&nbsp;&nbsp;&nbsp;&nbsp;
                <img src="../../../Images/imgFlAnt.gif" onclick="setAno('H','A')" style="cursor:pointer; position: absolute; top:28px; left:435px;" border=0 />
                <asp:TextBox ID="txtHasta" style="width:30px; position: absolute; top: 26px; left: 450px;" readonly="true" runat="server" Text="" />
                <img src="../../../Images/imgFlSig.gif" onclick="setAno('H','S')" style="cursor:pointer; position: absolute; top:28px; left:485px;" border=0 />
            </td>
        </tr>
    </table>
    <center>
    <table style="margin-top:25px; width:220px;">
		<tr>
			<td>
                <button id="btnAceptar" type="button" onclick="aceptar()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgAceptar.gif" /><span title="Añadir">Aceptar</span>
                </button>    
                <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" style="display:inline; margin-left:30px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
                </button>    
			</td>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
