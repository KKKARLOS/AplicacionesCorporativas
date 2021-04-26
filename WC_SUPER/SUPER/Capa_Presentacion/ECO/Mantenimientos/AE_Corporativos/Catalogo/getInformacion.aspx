<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getInformacion.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Catalogo de CEEC por&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %> </title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
        #tblDatos tr { height: 16px; }
    </style>
	<script type="text/javascript">

    function init(){
        try{
            if (!mostrarErrores()) return;
            if ($I("hdnCaso").value == "1" || $I("hdnCaso").value == "2")
            {
                $I("divCaso1_2").style.display = "block";
                $I("divCaso3").style.display = "none";
            }
            if ($I("hdnCaso").value == "3")
            {
                $I("divCaso1_2").style.display = "none";
                $I("divCaso3").style.display = "block";
            } 
            actualizarLupas("tblTitulo", "tblDatos");
            setExcelImg("imgExcel", "divCatalogo");
            
            if ($I("hdnCaso").value == "3") $I("imgExcel_exp").style.top =  "38px";
            else $I("imgExcel_exp").style.top =  "30px";

            $I("imgExcel_exp").style.left = "821px";
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
    function seleccionValor(strList){
        try{
            var js_args = "getDatos@#@";
            js_args += getRadioButtonSelectedValue(strList, true);

            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }catch(e){
            mostrarErrorAplicacion("Error en la seleccionAmbito", e.message);
        }
    }
    function getDatos()
    {
        try{
            var js_args = "getDatos@#@";
            js_args += ($I("chkConValorAsignado").checked)? "1" : "0";; //Valor asignaddo

            mostrarProcesando();
            RealizarCallBack(js_args, "");
            
        }catch(e){
            mostrarErrorAplicacion("Error al obtener los datos", e.message);
        }
    }

    function RespuestaCallBack(strResultado, context){
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            mostrarErrorSQL(aResul[3], aResul[2]);
        }else{
            switch (aResul[0]){
                case "getDatos":
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    break;
            }
            ocultarProcesando();
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
    function excel(){
        try {
            var tblDatos = $I("tblDatos");
            if (tblDatos==null){
                ocultarProcesando();
                mmoff("War","No hay información en pantalla para exportar.", 300);
                return;
            }

            var sb = new StringBuilder;
            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
            sb.Append("        <td style='width:auto'>"+sNodo+"</TD>");
            sb.Append("        <td style='width:auto'>Criterio económico</TD>");
            sb.Append("        <td style='width:auto'>Obligatorio</TD>");
            sb.Append("        <td style='width:auto'>Valor</TD>");
            sb.Append("	</TR>");		
            for (var i=0;i < tblDatos.rows.length; i++){
                sb.Append(tblDatos.rows[i].outerHTML);
            }
            sb.Append("</table>");
	    
            crearExcel(sb.ToString());
            var sb = null;
        }catch(e){
            mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
        }
    }
    </script>
</head>
<body style="overflow: hidden; margin-left:10px; margin-top:10px" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var sNodo = "<%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>"; 
	</script>

    <center>
    <span id="divCaso1_2" runat="server" style="display:block;text-align:right;margin-right:30px" class="texto">
	    Con valor asignado&nbsp;&nbsp;<input type="checkbox" id="chkConValorAsignado" onclick="getDatos()" class="check" runat="server" />								
    </span>

    <span id="divCaso3" runat="server"  style="display:none;text-align:left;margin-left:5px" class="texto">
	    <asp:RadioButtonList ID="rdbCaso3" runat="server" RepeatDirection="horizontal" SkinId="rbl" onclick="seleccionValor(this.id)">
		    <asp:ListItem style="cursor:pointer;" onclick="$I('rdbCaso3_0').click();" Selected="True" Value="1" Text="Centros de Responsabilidad en los que el valor está asignado" />
		    <asp:ListItem style="cursor:pointer;" onclick="$I('rdbCaso3_1').click();" Value="0" Text="Centros de Responsabilidad en los que el criterio está asignado pero sin valor" />
	    </asp:RadioButtonList>
    </span>	

    <table style="width:830px;text-align:left" cellpadding="5" >
        <tr>
            <td>
                <table id="tblTitulo" style="width:800px; height:17px">
                    <colgroup><col style="width:280px"/><col style="width:280px"/><col style="width:100px"/><col style="width:140px"/></colgroup>
                    <tr class="TBLINI">
                        <td align="left" width="60px;" style="padding-right:10px;">
							<img style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
						    <map name="img1">
						        <area onclick="ot('tblDatos', 0, 0, '')" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblDatos', 0, 1, '')" shape="RECT" coords="0,6,6,11">
					        </map>&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>
                            <img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
							height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"/> 
                            <img style="cursor: pointer;display: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"/> 
							
                        </td>
                        <td><img style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <map name="img2">
						        <area onclick="ot('tblDatos', 1, 0, '')" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblDatos', 1, 1, '')" shape="RECT" coords="0,6,6,11">
					        </map>&nbsp;CEEC&nbsp;
							<img id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"/> 
							<img style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"/>	
				        </td>
                        <td align="center">Obligatorio</td>
                        <td>Valor</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 816px; height:384px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:800px;">
                        <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width:800px; height:17px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table style="margin-top:5px; width:100px;" >
		<tr>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90"  runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../../images/botones/imgSalir.gif" /><span title="Cancelar">Salir</span>
                </button>    
			</td>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnCaso" id="hdnCaso" value="1" runat="server"/>
    <input type="hidden" name="hdnCriterio" id="hdnCriterio" value="" runat="server"/>
    <input type="hidden" name="hdnValor" id="hdnValor" value="" runat="server"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
