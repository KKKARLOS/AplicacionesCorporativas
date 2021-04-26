<%@ Page Language="C#" AutoEventWireup="true" Theme="Corporativo" CodeFile="Default.aspx.cs" Inherits="SUPER.Validacion_OpenProj" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title> ::: SUPER ::: - Validar estructura técnica del archivo XML</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/funcionestablas.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
</head>
	<body onload="init()">
	<form id="frmUpload" runat="server" enctype="multipart/form-data" method="POST" name="frmUpload">
<ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
	    var EsPostBack = <%=EsPostBack %>;
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var hayConsumos = <%=hayConsumos %>;
	    function uploadpop()
	    {
	        //Tiene que ir dentro del form por contener <%//= %>
		    //window.open('../../../Documentos/PorcentajeSubida.aspx?guid=<%= Request.QueryString["guid"] %>','',"resizable=no,status=no,scrollbars=no,menubar=no,toolbar=0,height=140,width=250,top="+ eval(screen.availHeight/2-40)+",left="+ eval(screen.availWidth/2-125));
	    }
    </script>
    <center>
    <br />
    <fieldset id="fldImport" style="width:800px; margin-left:16px;">
    <legend>Selección del fichero que contiene la estructura técnica del proyecto</legend>
        <br />
        <input type="file" id="txtArchivo" runat="server" onchange="comprobarExt(this.value)" class="txtIF"  style="width:760px"/>
        <br /><br />
    </fieldset>
	<br /><br />
	<table id="nombreProyecto"  style="width:1000px; margin-left:16px;text-align:left;">
    <tr>
        <td>
            <table id="Table2" style="width: 800px; height: 17px;">
            <colgroup>
                <col style="width:20px" />
                <col style="width:410px" />
                <col style="width:80px" />
                <col style="width:70px" />
                <col style="width:80px" />
                <col style="width:70px" />
            </colgroup>
	            <tr class="TBLINI" align="center">
		            <td>&nbsp;</td>
		            <td align="left">Denominación</td>
		            <td title='Esfuerzo total planificado' style="text-align:right;">ETPL</td>
		            <td title='Fecha inicio planificada'>FIPL</td>
		            <td title='Fecha fin planificada'>FFPL</td>
		            <td title='Esfuerzo total previsto' style="text-align:right;">ETPR</td>
		            <td title='Fecha fin prevista'>FFPR</td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 816px; height:360px;">
                <div style='background-image:url(../../../../Images/imgFT20.gif); width:800px'>
                    <%=strTablaHTMLTarea%>
                </div>
            </div>
            <table id="tblResultado" style="width: 800px; height: 17px;">
	            <tr class="TBLFIN">
		            <td></td>
	            </tr>
            </table>
        </td>
    </tr>
    </table>
    <br />
    <table style="width:600px;text-align:left;">
        <tr>
            <td>
	            <table id="Table4" style="width: 550px; height: 17px">
		            <tr class="TBLINI">
			            <td width="25px">&nbsp;</td>
			            <td width="450px">Denominación del Hito</td>
			            <td width="75px">Fecha</td>
		            </tr>
	            </table>
	            <div id="divHitos" style="overflow: auto; overflow-x: hidden; WIDTH: 566px; height:60px;">
                    <div style='background-image:url(../../../../Images/imgFT20.gif); width:550px'>
    	                <%=strTablaHTMLHito%>
    	            </div>
                </div>
	            <table id="Table5" style="width: 550px; height: 17px">
		            <tr class="TBLFIN">
			            <td></td>
		            </tr>
	            </table>
            </td>
        </tr>
    </table>
    <table style="margin-top:15px;width:590px; text-align:left;">
		<colgroup>
			<col style="width:205px;"/>
			<col style="width:115px;"/>
			<col style="width:115px;"/>
			<col style="width:145px;"/>
		</colgroup>
        <tr>
            <td>
                <asp:CheckBox ID="chkEstr" runat="server" Text="Borrar estructura antes de importar " Width="200px" TextAlign=left CssClass="check texto" Checked=true />   
            </td> 
	        <td >
			    <button id="btnImportar" type="button" onclick="importar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgEstructura.gif" /><span title="Genera la estructura del proyecto económico a partir del contenido del fichero">Importar</span>
			    </button>	
	        </td>
	        <td>
			    <button id="btnAyuda" type="button" onclick="mostrarGuia('ImportardeOpenProj.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGuia.gif" /><span title="Guía">Guía</span>
			    </button>	
	        </td>			        
			<td>
			    <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
			    </button>	
	        </td>
		
        </tr>
    </table>
</center>
    <input type="hidden" runat="server" name="hdnPSN" id="hdnPSN" value="" />
    <input type="hidden" runat="server" name="hdnResul" id="hdnResul" value="" />
    <input type="hidden" runat="server" name="hdnAccion" id="hdnAccion" value="V" />
    <input type="hidden" runat="server" name="hdnRecursos" id="hdnRecursos" value="" />
    <input type="hidden" runat="server" name="hdnTareas" id="hdnTareas" value="" />
    <input type="hidden" runat="server" name="hdnError" id="hdnError" value="" />
    <input type="hidden" runat="server" name="hdnConsumos" id="hdnConsumos" value="N" />
    <asp:CheckBox ID="chkRecursos" runat="server" Text="Incluir recursos en la exportación " Width="200px" TextAlign="Left" CssClass="check texto" style="visibility:hidden" />
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }

    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
</script>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm1" />
</form>
</body>
</html>
