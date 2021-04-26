<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getPeriodo.aspx.cs" Inherits="getPeriodo2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Selección de periodo</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try {
        
            if (!mostrarErrores()) return;
            //alert(sAdministrador +"\n"+ opener.sUltCierreEcoNodo +"\n"+ nPSN);
            mostrarProcesando();
            var js_args = "getUMCNP@#@"+nPSN;
            RealizarCallBack(js_args, ""); 
            
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
                mmoff("Inf","El rango temporal no puede debe estar comprendido entre los años 1990 y 2078.",400);
                return;
            }
            var nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
            var nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
            //alert("nDesde: "+ nDesde +"\nnHasta: "+ nHasta);
            if (sAdministrador != "SA"){
                if (nDesde < nAnomesMinimo){
                    switch (sOpValidacion){
                        case 1:
                            mmoff("Inf", "La fecha de inicio no puede ser anterior a " + AnnomesAFecha(nAnomesMinimo).ToShortDateString(),360);
                            break;
                    }
                    return;
                } else cierraVentana(nDesde,nHasta);
            }else if (nDesde <= parseInt(opener.sUltCierreEcoNodo, 10)){
                jqConfirm("", "El mes de inicio seleccionado es anterior o igual al último mes económico cerrado del "+strEstructuraNodoLarga +" ("+ AnoMesToMesAnoDescLong(opener.sUltCierreEcoNodo) +").<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
                    if (answer) cierraVentana(nDesde,nHasta);
                });
            }else cierraVentana(nDesde,nHasta);

        }catch(e){
            mostrarErrorAplicacion("Error al aceptar", e.message);
        }
    }
    function cierraVentana(nDesde,nHasta){
        try{           
            var returnValue = nDesde + "@#@" + nHasta;
            modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana.", e.message);
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
                case "getUMCNP":
//                    alert(opener.sUltCierreEcoNodo +"\n"+ aResul[2]);
                    if (opener.sUltCierreEcoNodo != aResul[2]){
                        opener.sUltCierreEcoNodo = aResul[2];
                        if (parseInt(aResul[2], 10) <= nAnomesMinimo) nAnomesMinimo = parseInt(aResul[2], 10);
                    }
                    break;

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
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"].ToString()%>";
	    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
        var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
        var nAnomesMinimo = <%=sAnomesMinimo%>; 
        var sOpValidacion = <%=sOpValidacion%>; 
	    var sAdministrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
	    var nPSN = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    </script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table style="width:520px; margin-left:15px; margin-top:8px;">
        <colgroup>
            <col style="width:250px;" />
            <col style="width:20px;" />
            <col style="width:250px;" />
        </colgroup>
        <tr style="text-align:center; font-weight: bold;">
            <td style="padding-bottom:5px;">Inicio</td>
            <td>&nbsp;</td>
            <td style="padding-bottom:5px;">Fin</td>
        </tr>
        <tr style="height:132px;">
            <td style="background-image: url(../../images/imgFondoCalendario.gif); background-repeat: no-repeat; vertical-align:top;">&nbsp;
                <select id="cboDesde" class=combo style="width:80px; position: absolute; top: 34px; left: 55px;" onchange="setMes('D')" runat="server">
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
                <img src="../../Images/imgFlAnt.gif" onclick="setAno('D','A')" style="cursor: pointer; position: absolute; top: 38px; left: 165px;" border=0 />
                <asp:TextBox ID="txtDesde" style="width:30px; position: absolute; top: 36px; left: 180px;" readonly="true" runat="server" Text="" />
                <img src="../../Images/imgFlSig.gif" onclick="setAno('D','S')" style="cursor: pointer; position: absolute; top: 38px; left: 218px;" border=0 />
            </td>
            <td>&nbsp;</td>
            <td style="background-image: url(../../images/imgFondoCalendario.gif); background-repeat: no-repeat; vertical-align:top;">
                <select id="cboHasta" class=combo style="width:80px; position: absolute; top: 34px; left: 325px;" onchange="setMes('D')" runat="server">
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
                <img src="../../Images/imgFlAnt.gif" onclick="setAno('H','A')" style="cursor: pointer; position: absolute; top: 38px; left: 435px;" border=0 />
                <asp:TextBox ID="txtHasta" style="width:30px; position: absolute; top: 36px; left: 450px;" readonly="true" runat="server" Text="" />
                <img src="../../Images/imgFlSig.gif" onclick="setAno('H','S')" style="cursor: pointer; position: absolute; top: 38px; left: 488px;" border=0 />
            </td>
        </tr>
    </table>
    <center>
    <table style="margin-top:25px; width:220px;">
		<tr>
			<td>
                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
			</td>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    </form>
</body>
</html>
