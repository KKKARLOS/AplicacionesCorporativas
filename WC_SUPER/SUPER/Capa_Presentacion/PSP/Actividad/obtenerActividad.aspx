<%@ Page Language="c#" CodeFile="obtenerActividad.aspx.cs" Inherits="obtenerActividad" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title> ::: SUPER ::: - Selección de actividad</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
			if (strErrores != ""){
				mostrarError(strErrores);
			}
			buscarActividades("");
			ocultarProcesando();
		}
		
        function buscarActividades(strCli){
            try{
                var js_args = "A@#@";
                //var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
                var sAccion="I";
                js_args += $I("txtNumPT").value + "@#@";
                js_args += $I("txtNumFase").value + "@#@";
                js_args += sAccion + "@#@";
                js_args += strCli;

                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return false;
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener las actividades", e.message);
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
                    case "A":
		                $I("divCatalogo").children[0].innerHTML = aResul[2];
                        //$I("txtActividad").value = "";
                        break;
                }
                ocultarProcesando();
            }
        }
		
	-->
    </script>
    <style type="text/css">      
        #tblDatos td { padding-left: 5px; }
        #tblDatos tr { height: 18px; }
    </style>     
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	    <script type="text/javascript">
	    <!--
	        var strErrores = "<%=strErrores%>";
            var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	        var strServer = "<%=Session["strServer"]%>";
	    -->
	    </script>
        <center>
	    <table style="width:500px;margin-top:30px;text-align:left;">		
        <tr>
	        <td>
		        <fieldset style="width:410px; margin-left:22px;"><legend>Estructura</legend>
		        <table style="width:400px" align="center">
				    <tr>
					    <td>
						    <label style="width:85px;">Proy. económico</label>
						    <asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" />
						    <asp:TextBox ID="txtPE" runat="server" style="width:240px" readonly="true" />
					    </td>
				    </tr>
				    <tr>
					    <td>
						    <label style="width:85px;">Proy. técnico</label>
						    <asp:TextBox ID="txtPT" runat="server" style="width:300px" readonly="true" />
					    </td>
				    </tr>
				    <tr>
					    <td>
						    <label style="width:85px;">Fase</label>
						    <asp:TextBox ID="txtFase" runat="server" style="width:300px" readonly="true" />
					    </td>
				    </tr>
		        </table>
		        </fieldset>
                <table style="margin-top:10px;margin-left:22px;width:440px">
                <tr>
	                <td>			    
	                    <table id="tblTitulo" style="width: 410px; height: 17px">
		                    <tr class="TBLINI">
			                    <td>&nbsp;Denominación&nbsp;
				                    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" width="20">
				                    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" width="20"> 
			                    </td>
		                    </tr>
	                    </table>
                    </td>
                </tr>
                <tr>
	                <td>
		                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 426px; height:340px">
			                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:410px;">
				                <table id="tblDatos" style="width: 410px;">
				                </table>
			                </div>
		                </div>					
		                <table style="width: 410px; height: 17px">
			                <tr class="TBLFIN">
				                <td></td>
			                </tr>
		                </table>
	                </td>
                </tr>
                </table>
            </td>
        </tr>            
	    </table>
	    <br />
	    <table width="300px" align="center" style="margin-top:5px;">
		    <tr>
			    <td align="center">
				    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
			    </td>
		    </tr>
	    </table>	
        </center>	
        
    <asp:TextBox ID="txtNumPT" runat="server" SkinID="Numero" style="width:5px;visibility:hidden;"/>
	<asp:TextBox ID="txtNumFase" runat="server" SkinID="Numero" style="width:5px;visibility:hidden;"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />  
    </form>
    <script type="text/javascript">
    <!--
    	
	    function aceptarClick(indexFila){
	        var tblDatos = $I("tblDatos");
	        strDatos = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText + "@#@" +
	                    tblDatos.rows[indexFila].getAttribute("codFase") + "@#@" + tblDatos.rows[indexFila].getAttribute("desFase");

		    var returnValue = strDatos;
		    modalDialog.Close(window, returnValue);
	    }
    	
	    function cerrarVentana(){
		    var returnValue = null;
		    modalDialog.Close(window, returnValue);
	    }
    -->
        </script>
</body>
</HTML>
