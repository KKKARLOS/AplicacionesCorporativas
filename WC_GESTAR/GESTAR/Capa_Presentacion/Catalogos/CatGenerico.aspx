<%@ Page Language="c#" CodeFile="CatGenerico.aspx.cs" Inherits="GESTAR.Capa_Presentacion.Catalogo.CatGenerico" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Selección de&nbsp;<%=strTitulo%></title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<link href="../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
		function init(){
	        try
	        {    
                if (!mostrarErrores()) return;

//                if ($I("hdnEsSolicitante").value == "true") 
	            //			    {
                //if ($I("hdnTipoPant").value=="M"){
                    //$I("btnEntrada").style.visibility = "visible";
                    //$I("btnEntrada").style.marginLeft = "60px";
                   // $I("tblBotones").style.marginLeft = "100px";
                //}
                //$I("btnAceptar").style.marginLeft = "70px";
                //$I("btnCancelar").style.marginLeft = "80px";	                    			    				    
//			    }
			    buscar();
			    ocultarProcesando();
			}
	        catch (e)
	        {
                mostrarErrorAplicacion("Error en la función init", e.message);	
	        }					    
		}
		
        function buscar(){
            try{            
                var js_args = $I("hdnOpcion").value;
                //alert(js_args);
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return;                      

	        }catch(e){
		        mostrarErrorAplicacion("Error en la función buscar", e.message);
            }
        }
        function RespuestaCallBack(strResultado, context){
        try{  
                actualizarSession();
                var aResul = strResultado.split("@@");
                if (aResul[1] != "OK"){
                    var reg = /\\n/g;
                    mostrarError(aResul[2].replace(reg, "\n"));
                }else{
                    switch (aResul[0]){
                        case "Area":
                        case "Tipo":
                        case "Entrada":
                        case "Alcance":
                        case "Proceso":
                        case "Producto":
                        case "Requisito":
                        case "Causa":
                        case "Origen":
                        case "CR":
                        case "CR_TEXTO":
                        case "Cliente":
                        case "Proveedor":
                        case "Solicitantes":
                        case "Coordinadores":
                        case "Especialistas":
                        case "Deficiencias":
                            $I("divCatalogo").children[0].innerHTML = aResul[2];	                
                            break;
                        default:
                            ocultarProcesando();
                            mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);	                               
                    }
                }
                ocultarProcesando();
	        }
	        catch (e)
	        {
                mostrarErrorAplicacion("Error en la función RespuestaCallBack", e.message);	
	        }		
            
        }
    </script>
</head>
<body style="overflow: hidden" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	    var intSession  = <%=Session.Timeout%>;
	    var strServer   = "<%=Session["strServer"]%>";
    </script>
    <center>
		<table style="margin-top:10px;text-align:left;width:98%">
				<tr>
					<td align="center">
                        <table style="text-align:left">
                            <tr>
                                <td width="10%">&nbsp;</td>
                                <td width="90%" align="left">					    
						            <table id="tblTitulo" height="17" cellSpacing="0" cellPadding="0" width="396px" align="left"
							            border="0" >
							            <tr class="tituloColumnaTabla">
								            <td width="360px"><img style="cursor: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
										            height="11" src="../../Images/imgLupa.gif" width="20"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										            height="11" src="../../Images/imgLupaMas.gif" width="20">&nbsp;Denominación</td>
								            <td width="36px"><img onmouseover="javascript:bMover=true;moverTablaUp()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
										            height="8" src="../../Images/imgFleUp.gif" width="11"></td>
							            </tr>
						            </table>
						        </td>
						    </tr>
                            <tr>
                                <td width="10%">&nbsp;
                                </td>
                                <td width="90%" align="left">	
						            <div id="divCatalogo" align="left" style="overflow-x: hidden; overflow-y: auto; width: 412px; height: 305px">						                                     							
                       					<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:396px">
        					                <table class="texto" id="tblDatos" style="width: 396px">                                           		
							                </table>        									 
        					            </div>
		                            </div>
    		                        <table id="tblResultado" style="width:396px; text-align:left; height:17px">
			                            <tr class="textoResultadoTabla">
				                            <td colSpan="2"><img height="1" src="../../Images/imgSeparador.gif" width="356px" border="0">
					                            <img onmouseover="javascript:bMover=true;moverTablaDown()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
						                            height="8" src="../../Images/imgFleDown.gif" width="11"></td>
			                            </tr>
		                            </table>		                                      
                                </td>
                            </tr>
                        </table>		                
		            </td>
	            </tr>
	            <tr>
                <td>
			        <table id="tblBotones" style="margin-top:15px; margin-left:100px; width:300px;" border="0">
                        <colgroup><col style="width:150px;" /><col style="width:150px;" /></colgroup>
		                <tr>	
			                <td>
				                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
			                </td>
			                <td>
				                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
				                    <img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
				                </button>				
			                </td>
		                </tr>
	                </table>	
			  </td>
	          </tr>	
		</table>
    </center>		
    <asp:textbox id="hdnOpcion" runat="server" style="visibility:hidden"></asp:textbox>
    <asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden"></asp:textbox>
    <asp:textbox id="hdnErrores" runat="server" style="visibility:hidden"></asp:textbox>
	<asp:textbox id="hdnEsSolicitante" runat="server" style="visibility:hidden">false</asp:textbox>

    <input type="hidden" name="hdnTipoPant" id="hdnTipoPant" value="B" runat="server"/>

    <uc_mmoff:mmoff ID="mmoff1" runat="server" />		
	</form>

	<script type="text/javascript">
        function aceptar(){
            try{  
	            var strOpciones = "";
	            var sw = 0;
            	
	            var aFila = FilasDe("tblDatos");
	            for (var i=0; i<aFila.length;i++){
	                if (aFila[i].className == "FS"){
		                strOpciones = aFila[i].id + "@@" + aFila[i].cells[0].innerText  + "@@";
		                sw = 1;
		                break;
		            }
	            }
	            if (sw == 0){
	                mmoff("Inf", "Debe seleccionar algún elemento del catálogo", 330);
	                return;
	            }
            	
	            strOpciones = strOpciones.substring(0,strOpciones.length-2);

	            var returnValue = strOpciones;
	            modalDialog.Close(window, returnValue);		            
	        }
	        catch (e)
	        {
                mostrarErrorAplicacion("Error en la función aceptar", e.message);	
	        }			        
        }

        function aceptarClick(obj){
            try{  
                strOpciones = "";
                var tblDatos = $I("tblDatos");
                strOpciones = tblDatos.rows[obj.rowIndex].id + "@@" + tblDatos.rows[obj.rowIndex].cells[0].innerText + "@@";
	            strOpciones = strOpciones.substring(0,strOpciones.length-2);

	            var returnValue = strOpciones;
	            modalDialog.Close(window, returnValue);		            	            
	        }
	        catch (e)
	        {
                mostrarErrorAplicacion("Error en la función aceptarClick", e.message);	
	        }			        
        }

        function cerrarVentana(){
            try {
                var returnValue = null;
                modalDialog.Close(window, returnValue);	                     
	        }
	        catch (e)
	        {
                mostrarErrorAplicacion("Error en la función cerrarVentana", e.message);	
	        }		            
        }
        function entrada(){
            try
            {
                if ($I("hdnIDArea").value==""||$I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }                
                var strURL = strServer + "Capa_Presentacion/Areas/Detalle/CatEntradas/default.aspx?IDAREA="+$I("hdnIDArea").value+"&SOLICITANTE="+$I("hdnEsSolicitante").value;

                modalDialog.Show(strURL, self, sSize(940, 580))
                .then(function(ret) {
                    ocultarProcesando();

                    var js_args = $I("hdnOpcion").value;
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");	                        
                });                                  
            }
	        catch (e)
	        {
                mostrarErrorAplicacion("Error en la función entrada", e.message);	
	        } 
        }	         
    </script>	
</body>
</html>
