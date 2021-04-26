<%@ Page Language="c#" CodeFile="getProfCons.aspx.cs" Inherits="getProfCons" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!doctype html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
	    <title>Selección de profesional</title>
	    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	    <link href="../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
	    <script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	    <script type="text/javascript">
		    function init(){
                try{
                    if (!mostrarErrores()) return;
			        $I("txtApellido1").focus();
    			    ocultarProcesando();
                }catch(e){
                    mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
                }
		    }
            function RespuestaCallBack(strResultado, context){
                try {
                    actualizarSession();
                    var aResul = strResultado.split("@#@");
                    if (aResul[1] != "OK"){
                        mostrarErrorSQL(aResul[3], aResul[2]);
                    }else{
                        switch (aResul[0]){
                            case "profesionales":
                                $I("divCatalogo").children[0].innerHTML = aResul[2];
                                actualizarLupas("tblTitulo", "tblDatos");
                                break;
                        }
                        ocultarProcesando();
                    }
                } catch (e) {
                    mostrarErrorAplicacion("Error en la función RespuestaCallBack", e.message);
                }
            }
            function msgNoVision(){
                mmoff("War", "El profesional no está bajo su ámbito de visión", 300);
            }
            function mostrarProfesionales(){
                try{
                    if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
                        mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
                        $I("txtApellido1").focus();
                        return;
                    }
                    var js_args = "profesionales@#@";
                    js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
                    js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
                    js_args += Utilidades.escape($I("txtNombre").value); 
                    
                    mostrarProcesando();
                    RealizarCallBack(js_args, ""); 
                    return;
                    
	            }catch(e){
		            mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
                }
            }
            
            function aceptarClick(indexFila){
                try{
                    if (bProcesando()) return;
                    var sTexto = $I("tblDatos").rows[indexFila].cells[0].innerText;
                    var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + sTexto;
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
    <body style="OVERFLOW: hidden;margin-left:15px;" onload="init()">
        <ucproc:Procesando ID="Procesando" runat="server" />
	    <form id="Form1" method="post" runat="server">
	    <script type="text/javascript">
	        var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	        var strServer   = "<%=Session["strServer"]%>";
        </script>
        <center>
		    <table width="412px" align="center" style="margin-top:10px;">
			    <tbody>
			        <tr>
			            <td>
                            <table id="tblApellidos" style="width:390px; margin-bottom:5px; text-align:left;">
                            <colgroup><col style="width:130px"/><col style="width:130px"/><col style="width:130px"/></colgroup>
                                <tr>
                                    <td>&nbsp;Apellido1</td>
                                    <td>&nbsp;Apellido2</td>
                                    <td>&nbsp;Nombre</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                </tr>
                            </table>
			            </td>
			        </tr>
				    <tr>
					    <td>
						    <table id="tblTitulo" height="17" cellSpacing="0" cellPadding="0" width="396" align="left"
							    border="0" name="tblTitulo">
							    <tr class="TBLINI">
								    <td>&nbsp;Profesional&nbsp;
									    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								        <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
										    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								    </td>
							    </tr>
						    </table>
						    &nbsp;
						    <div id="divCatalogo" style="OVERFLOW: auto; WIDTH: 412px; HEIGHT: 350px" align="left">
                                <div style='background-image:url(../../../Images/imgFT18.gif);WIDTH: 396px;'>
                                    <table id='tblDatos' style='WIDTH: 396px;'>
							        </table>
							    </div>
		                    </div>
                            <table style="WIDTH: 396px; HEIGHT: 17px">
                                <tr class="TBLFIN">
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">En gris profesionales que no están bajo tu ámbito de visión</td>
                                </tr>
                            </table>
                            <br />
                            <table width="396px" align="center">
		                        <tr>
			                        <td align="center">
				                        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					                         onmouseover="se(this, 25);mostrarCursor(this);">
					                        <img src="../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
				                        </button>	
			                        </td>
		                        </tr>
                            </table>
		    </td></tr></tbody></table>
	    </center>
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	    </form>
    </body>
</html>
