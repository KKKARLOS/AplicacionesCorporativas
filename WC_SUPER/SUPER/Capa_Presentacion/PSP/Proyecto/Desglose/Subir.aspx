<%@ Page Language="C#" AutoEventWireup="true" Theme="Corporativo" CodeFile="Subir.aspx.cs" Inherits="SUPER.Subir" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<HTML>
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Subir archivos</title>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <script language=javascript>
    <!--
        //var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml";
        var exts = "xml";
        //var exts = ".*"; //Acepta todas las extensiones.
        
        function init(){
            if (EsPostBack) aceptar();
            if (hayConsumos){
                //$I("fldImport").style.visibility = "visible";
                $I("chkEstr").checked=false;
                $I("chkEstr").disabled = true;
            }
            window.focus();
            ocultarProcesando();
        }
        
        function comprobarExt(value)
        {
            if(value=="")return true;
            var re = new RegExp("^.+\.("+exts+")$","i");
            if(re.test(value))
            {
                mmoff("InfPer", "Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones prohibidas son: " + extsTexto + " \n\n", 550);
                //frmUpload.txtDescripcion.value="";
                frmUpload.txtArchivo.value = "";
                setOp($I("tblAceptar"),100);
                return false;
            }
            return true;
        }
	    
	    function importar(){
	        if (getOp($I("btnImportar")) == 30) return;
	        setOp($I("btnImportar"),30);
	        
            if (frmUpload.txtArchivo.value==""){
                mmoff("inf", "Seleccione un fichero", 190);
    	        setOp($I("btnImportar"),100);
                return;
            }
            if(!comprobarExt(frmUpload.txtArchivo.value))return;
            Accion=1;
            $I("hdnAccion").value=1;
	        mostrarProcesando();
	        //Si no estamos en ejecutando en local o extranet (y se va a subir un archivo), que muestre la barra de progreso.
	        var strURL = location.href.toLowerCase();
	        if (strURL.indexOf("localhost") == -1 && strURL.indexOf("https") == -1 && frmUpload.txtArchivo.value != "") uploadpop();
	        else if (frmUpload.txtArchivo.value != ""){
	            comprobarTamano("txtArchivo","btnImportar");
	        }
	        frmUpload.submit();
	    }
	    function exportar(){
	        if (getOp($I("btnExportar")) == 30) return;
	        setOp($I("btnExportar"),30);
            Accion=2;
            $I("hdnAccion").value=2;
	        mostrarProcesando();
	        frmUpload.submit();
	    }
        function aceptar(){
            if ($I("hdnAccion").value == 2){
                //alert("sPathCompleto="+sPathCompleto);
                //alert("hdnArchivo="+$I("hdnArchivo").value);
                $I("iFrmSubida").src = "../../../Documentos/Descargar.aspx?sTipo=xml&nIDDOC=0&sPath="+ codpar(sPathCompleto);
            }
            else{
		        var returnValue = $I("hdnResul").value;
		        modalDialog.Close(window, returnValue);
            }
	    }
	    
	    function cerrarVentana(){
		    var returnValue = $I("hdnResul").value;//null;
		    modalDialog.Close(window, returnValue);
	    }
	
    -->
    </script>
</head>
	<body onload="init()" leftmargin=15 topmargin=15>
		<form id="frmUpload" runat="server" enctype="multipart/form-data" method="POST" name="frmUpload">
<ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
	<!--
	    var EsPostBack = <%=EsPostBack %>;
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var hayConsumos = <%=hayConsumos %>;
	    var Accion = <%=Accion %>;
        var sPathCompleto = "<%= sPathCompleto %>";
	    function uploadpop()
	    {
	        //Tiene que ir dentro del form por contener <%//= %>
		    window.open('../../../Documentos/PorcentajeSubida.aspx?guid=<%= Request.QueryString["guid"] %>','',"resizable=no,status=no,scrollbars=no,menubar=no,toolbar=0,height=140,width=250,top="+ eval(screen.availHeight/2-40)+",left="+ eval(screen.availWidth/2-125));
	    }
	-->
    </script>
	<table class="texto" cellspacing="5" cellpadding="0" style="width:630px; table-layout:fixed;" border="0">
	<tr>
	    <td>
	        <fieldset id="fldExport" style="width:370px">
	        <legend>Exportación a Open Project</legend>
	            <br />
	            <table class="texto" style="width:360px; table-layout:fixed;" border="0">
	                <tr>
	                    <td style="width:150px">
                            <button id="btnExportar" type="button" onclick="javascript:exportar()" style="width:110px" title="Genera un fichero XML con la estructura del proyecto económico">
                                <span>
                                    <img src="../../../../images/botones/imgEstructura.gif" />
                                    <label id="lblPE">&nbsp;Exportar P.E.</label>
                                 </span>
                            </button>  
                        </td>
                        <td style="width:210px">
                            <asp:CheckBox ID="chkRecursos" runat="server" Text="Incluir recursos en la exportación " Width="200px" TextAlign="Left" CssClass="check texto" style="visibility:hidden" />
                        </td>
                    </tr>
                </table>
                <br />  
	        </fieldset>
	    </td>
	</tr>
	<tr>
	    <td>
	    </td>
	</tr>
	<tr>
	    <td>
	        <fieldset id="fldImport" style="width:620px;">
	        <legend>Importación desde Open Project</legend>
	            <br />
	            <table class="texto" cellspacing="5" cellpadding="0" style="table-layout:fixed; width:610px;" border="0" >
	                <tr>
	                    <td colspan=2 id="tdNuevo">Archivo (Máx: 25Mb)</td>
	                </tr>
	                <tr>
	                    <td colspan=2>
	                        <input type="file" class="txtIF" id="txtArchivo" runat="server" onchange="comprobarExt(this.value)" style="width:605px">
	                    </td>
	                </tr>
	                <tr>
	                    <td style="width:300px">
                            <button id="btnImportar" type="button" onclick="javascript:importar()" style="width:110px" title="Genera la estructura del proyecto económico a partir del contenido del fichero">
                                <span>
                                    <img src="../../../../images/botones/imgEstructura.gif" />
                                    <label id="Label1">&nbsp;Importar P.E.</label>
                                 </span>
                            </button>    
	                    </td>
                        <td style="width:310px">
                            <asp:CheckBox ID="chkEstr" runat="server" Text="Borrar estructura SUPER actual " Width="200px" TextAlign="Left" CssClass="check texto" Checked=true />
                        </td>
	                </tr>
	            </table>
	            <br />
	        </fieldset>
	    </td>
	</tr>
	</table>
	<br /><br />
	<table cellspacing="0" cellpadding="0" width="630px" class="texto" align="center" border="0">
        <tr>
            <td>
                <table style="height:20px; text-align:center;" cellspacing="0" cellpadding="0" >
	                <tr style="CURSOR: pointer" onclick="javascript:cerrarVentana()">
		                <td width="7"><IMG src="../../../../images/imgBtnIzda.gif" width="7"></td>
		                <td style="vertical-align:middle; text-align:center; width:20px; background:'../../../../images/bckBoton.gif'"><A hideFocus href="#"><IMG src="../../../../images/imgCancelar.gif" align="absMiddle" border="0"></A></td>
		                <td class="txtBot" width="40" background="../../../../images/bckBoton.gif"><A class="txtBot" hideFocus href="#">&nbsp;&nbsp;Cancelar</A></td>
		                <td width="7"><IMG src="../../../../images/imgBtnDer.gif" width="7"></td>
	                </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" runat="server" name="hdnPSN" id="hdnPSN" value="" />
    <input type="hidden" runat="server" name="hdnResul" id="hdnResul" value="" />
    <asp:TextBox ID="hdnAccion" runat="server" style="visibility:hidden" Text="" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
    <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
	</body>
</html>
