<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="System.Configuration" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Disponibilidad</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<link href="css/estilos.css" type="text/css" rel="stylesheet"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
    <script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" onload="init()" onunload="salir()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var bLectura = <%=sLectura%>;
	    var bLecturaInsMes = <%=sLecturaInsMes%>;	    
        var nAnoMesActual = <%=nAnoMes %>;
        var strHoy = "<%=DateTime.Now.ToShortDateString() %>";
        var sNodo = "<%=sNodo%>";
        var sCualidad = "<%=sCualidad%>";
	    var sUltCierreEcoNodo= "<%=Request.QueryString["sUltCierreEcoNodo"]%>";
    </script>
	<br />
	<br />	
	<center>
    <table id="tblProyecto" style="width:1200px; margin-top:5px;  margin-left:10px; text-align:left" cellpadding="0">
	    <colgroup>
		    <col style="width:400px"/>
		    <col style="width:800px"/>
	    </colgroup>	
		    <tr>
			    <td style="vertical-align:bottom;" align="left" >
				    <div id="divTituloFijo" style="width: 400px;" runat="server">
				    <table id='tblTituloFijo' style="width: 400px;">
					    <colgroup>
						    <col style="width:400px;" />		
					    </colgroup>

					    <tr id="tblTitulo" class="TBLINI">
						    <td align="left">&nbsp;Profesional&nbsp;
							    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',1,'divTituloFijo','imgLupa2','setBuscarDescriFija()')"
								    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"/> 
							    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',1,'divTituloFijo','imgLupa2', event,'setBuscarDescriFija()')"
								    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"/>
						    </td>			
					    </tr>
				    </table>
				    </div>
			    </td>
			    <td style="vertical-align:bottom;" align="left">
				    <div id="divTituloMovil" style="overflow:hidden; width: 540px;" runat="server">
				    <%=strTituloMovilHTML%>
				    </div>
			    </td>
		    </tr>		
		    <tr>
			    <td style="vertical-align:top;" class='tdbl'>
				    <div id="divBodyFijo" style="width:400px; height:500px; overflow:hidden;border-bottom: solid 1px #A6C3D2; " runat="server">
					    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT60.gif'); width:400px">
                        <%=strBodyFijoHTML%>
					    </div>
				    </div>	
                    <table id="tblResultado" style="width: 400px ;height:16px;">
	                    <tr class="TBLFIN" style="height:16px;">
		                    <td>&nbsp;</td>
	                    </tr>
                    </table>				    			    
			    </td>
			    <td style="vertical-align:top;">
				    <div id="divBodyMovil" style="width:556px; height:516px; overflow-x:auto;overflow-y:scroll;" runat="server" onscroll="setScroll();scrollTablaRecursos();">
					    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT60.gif'); width:540px">
					    <%=strBodyMovilHTML%>
					    </div>
				    </div>
			    </td>
		    </tr>	
    </table>
    </center>
    <br />
    <table style="width:750px; margin-left:50px; text-align:left" cellpadding="0" border="0">
    <colgroup>
	    <col style="width:350px"/>
	    <col style="width:400px"/>
    </colgroup>	        
    <tr>
        <td>
            <img border="0" src="../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo            
        </td>
        <td>
        <input type="text" class="OutVig" readonly="readonly" style="margin-left:60px;" />&nbsp;Fuera de proyecto
        <input type="text" class="OutCal" readonly="readonly" style="margin-left:40px;" />&nbsp;Sin calendario
        </td>
    </tr>
    </table>  
    <table id="tblBotones" style="margin-top:20px; margin-left:95px" width="800px">
            <tr>
	            <td align="center">
		            <button id="btnInsertarMes" type="button" onclick="insertarmes()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25);mostrarCursor(this);">
			            <img src="../../../images/botones/imgInsertarMes.gif" /><span title="Insertar mes">Ins. mes</span>
		            </button>			
	            </td>			
	            <td align="center">
		            <button id="btnExcel" type="button" onclick="mostrarProcesando();setTimeout('excel()',20)" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25); mostrarCursor(this);">
			            <img src="../../../images/botones/imgExcel.gif" /><span title="Exportar a excel">Exportar</span>
		            </button>			
	            </td>		            
                <td align="center">
	                <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
		                 onmouseover="se(this, 25);mostrarCursor(this);">
		                <img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;Grabar</span>
	                </button>	
                </td>
                <td align="center">
	                <button id="btnGrabarSalir" type="button" onclick="grabarsalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
		                 onmouseover="se(this, 25);mostrarCursor(this);">
		                <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
	                </button>	
                </td>			
    		
	            <td id="cldAuditoria" runat="server" align="center">
		            <button id="btnAuditoria" type="button" onclick="getAuditoriaAux()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25);mostrarCursor(this);">
			            <img src="../../../images/botones/imgAuditoria.gif" /><span title="Auditoría de datos modificados">Auditoría</span>
		            </button>	
	            </td>
    		
                <td align="center">
	                <button id="Button1" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
		                 onmouseover="se(this, 25);mostrarCursor(this);">
		                <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
	                </button>	 
                </td>
            </tr>
        </table>
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
</body>
</html>
