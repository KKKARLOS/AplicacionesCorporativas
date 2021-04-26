<%@ Page Language="C#" AutoEventWireup="true" Theme="Corporativo" CodeFile="Subir.aspx.cs" Inherits="SUPER.Subir" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title> ::: SUPER ::: - Subir archivos</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">
        //Se pone a nivel global
        //var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml|msg|xlsx|docx";
        //var exts = ".*"; //Acepta todas las extensiones.
        function init() {
            try {
                if ($I("hdnError").value != "") {
                    mmoff("Err", $I("hdnError").value, 400);
                }
/*
                if (document.getElementById("ctl00$hdnRefreshPostback") == null) {
                    var oRefreshPostback = document.createElement("INPUT");
                    oRefreshPostback.setAttribute("type", "text");
                    oRefreshPostback.setAttribute("style", "visibility:hidden");
                    oRefreshPostback.setAttribute("id", "ctl00$hdnRefreshPostback");
                    oRefreshPostback.setAttribute("name", "ctl00$hdnRefreshPostback");
                    oRefreshPostback.setAttribute("value", "N");
                }
                //var opener = window.dialogArguments;
                var opener;
                if (window.dialogArguments) // Internet Explorer supports window.dialogArguments
                {
                    opener = window.dialogArguments;
                    oRefreshPostback.setAttribute("value", "S");
                    //alert('correcto1')
                }
                else // Firefox, Safari, Google Chrome and Opera supports window.opener
                {
                    if (window.opener) {
                        opener = window.opener;
                        oRefreshPostback.setAttribute("value", "S");
                        //alert('correcto2');
                    }
                    //alert('correcto3');
                }
                if ($I("ctl00$hdnRefreshPostback")==null) document.forms[0].appendChild(oRefreshPostback);           
                if (opener == undefined && document.getElementById("ctl00$hdnRefreshPostback").value != "S") {
                    var sUrl = location.pathname;
                    var iPos = sUrl.indexOf("Capa_Presentacion");
                    sUrl = sUrl.substring(0, iPos);
                    location.href = sUrl + "AccesoIncorrecto.aspx";
                }
*/
                //alert(EsPostBack)
                if (EsPostBack) {
                    aceptar();
                    return;
                }
                if ($I("hdnsAccion").value == "U") {
                    $I("tdNuevo").innerText = "Nuevo (Máx: 25Mb)";
                    if ($I("hdnsTipo").value == "CERT" || $I("hdnsTipo").value == "EXAM" || $I("hdnsTipo").value == "TIF" ||
                        $I("hdnsTipo").value == "CURSOR" || $I("hdnsTipo").value == "CURSOI" ||
                        $I("hdnsTipo").value == "TAD" || $I("hdnsTipo").value == "TAE") {
                        $I("rowArchivo").style.display = "none";
                        $I("rowAutor").style.display = "none";
                        $I("rowAutorModif").style.display = "none";
                    }
                    else {
                        $I("rowArchivo").style.display = "table-row";
                        $I("rowAutor").style.display = "table-row";
                        $I("rowAutorModif").style.display = "table-row";
                    }
                }
                ocultarProcesando();
            } catch (e) {
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }   
        
        function comprobarExt(value)
        {
            if (value=="")return true;
            var re = new RegExp("^.+\.("+exts+")$","i");
            if(re.test(value))
            {
                mmoff("InfPer","Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones prohibidas son: "+extsTexto+" \n\n",550);//exts.replace(/\|/g,',')
                //frmUpload.txtDescripcion.value="";
                $I("txtArchivo").value = "";
                setOp($I("btnAceptar"), 100);
                return false;
            }
            return true;
        }

        function enviar() {
	        if (getOp($I("btnAceptar")) == 30) return;
	        setOp($I("btnAceptar"), 30);

	        if ($I("txtDescripcion").value == "") {
	            if ($I("hdnsTipo").value != "EXAM" && $I("hdnsTipo").value != "CERT" && $I("hdnsTipo").value != "TIF" &&
	                $I("hdnsTipo").value != "CURSOR" && $I("hdnsTipo").value != "CURSOI" &&
	                $I("hdnsTipo").value != "TAD" && $I("hdnsTipo").value != "TAE") {
	                mmoff("War", "Debes indicar la descripción del archivo", 260);
                    setOp($I("btnAceptar"), 100);
	                return;
	            }
	        }
	        
	        if (!comprobarExt($I("txtArchivo").value)) return;
	        
	        if ($I("hdnsAccion").value == "I") {
	            if ($I("txtArchivo").value=="" && $I("txtLink").value==""){
	                mmoff("War", "Seleccione un fichero o introduzca un link", 280);
	                setOp($I("btnAceptar"), 100);
	                return;
	            }
	        }
	        else{ //U
	            if ($I("txtArchivoOld").value=="" && $I("txtArchivo").value=="" && $I("txtLink").value==""){
	                mmoff("War", "Seleccione un fichero o introduzca un link", 280);
	                setOp($I("btnAceptar"), 100);
	                return;
	            }
	        }
 
	        mostrarProcesando();
	        //Si no estamos en ejecutando en local o extranet (y se va a subir un archivo), que muestre la barra de progreso.
	        var strURL = location.href.toLowerCase();
	        if (strURL.indexOf("localhost") == -1 && strURL.indexOf("https") == -1 && $I("txtArchivo").value != "") 
	            uploadpop();
	        else if ($I("txtArchivo").value != "")
	                 comprobarTamano("txtArchivo","btnAceptar");
	        document.forms[0].submit();
	    }

	    function aceptar() {
	        if ($I("hdnError").value == "") {
	            //window.returnValue = "OK";
	            var returnValue = "OK";
	            //window.close();
	            modalDialog.Close(window, returnValue);
	        }
	    }

	    function cerrarVentana() {
		    //window.returnValue = null;
	        //window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
	    
	    function obtenerAutor(){
	        var aDatos;
	        mostrarProcesando();
	        var sPantalla = strServer + "Capa_Presentacion/ECO/getProfesional.aspx?F=S";
	        modalDialog.Show(sPantalla, self, sSize(460, 535))
            .then(function(ret) {
	            if (ret != null) {
	                aDatos = ret.split("@#@");
	                $I("txtAutor").value = aDatos[1];
	                switch ($I("hdnsTipo").value) {
	                    case "DI": //dialogo de alerta
	                    case "SC": //solicitud de certificado Curvit
	                        $I("txtNumEmpleado").value = aDatos[2];
	                        break;
	                    default:
	                        $I("txtNumEmpleado").value = aDatos[0];
	                        break;
	                }

	            }
	        });
	        window.focus();
	        ocultarProcesando();
	    }
    </script>
</head>
<body onload="init()" style="margin-left:15px;margin-top:15px">
<form id="frmUpload" runat="server" enctype="multipart/form-data" method="POST" name="frmUpload">
<ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
	    var EsPostBack = <%=EsPostBack %>;
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    function uploadpop()
	    {
	        //Tiene que ir dentro del form por contener <%//= %>
		    window.open('PorcentajeSubida.aspx?guid=<%= Request.QueryString["guid"] %>','',"resizable=no,status=no,scrollbars=no,menubar=no,toolbar=0,height=140,width=250,top="+ eval(screen.availHeight/2-40)+",left="+ eval(screen.availWidth/2-125));
	    }
    </script>

	<table style="width:620px;" border="0">
	<colgroup><col style="width:110px;" /><col style="width:480px;" /><col style="width:30px;" /></colgroup>
	<tr id="rowCab" style="display:none; height:30px;" runat="server" >
	    <td></td>
	</tr>
	<tr id ="rowDesc" style="height:25px;" runat="server">
	    <td>Descripción</td>
	    <td colspan="2"><asp:TextBox id="txtDescripcion" name ="txtDescripcion" runat="server" Style="width:465px" MaxLength="50" /></td>
	</tr>
	<tr id="rowLink" runat="server" style="height:25px;">
	    <td>Link</td>
	    <td colspan="2"><asp:TextBox id="txtLink" runat="server" Style="width:465px" MaxLength="250" /></td>
	</tr>
	<tr id="rowArchivo" style="display:none; height:25px;">
	    <td>Archivo</td>
	    <td>
	        <asp:TextBox id="txtArchivoOld" runat="server" style="width:465px;" MaxLength="250" />
	    </td>
	    <td>
	        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="$I('txtArchivoOld').value='';" style="cursor:pointer; vertical-align:baseline;" />
	    </td>
	</tr>
	<tr style="height:25px;">
	    <td id="tdNuevo">Archivo (Máx: 25Mb)</td>
	    <td colspan="2">
	        <input type="file" id="txtArchivo" name="txtArchivo" runat="server" onchange="comprobarExt(this.value); return;" class="txtIF"  style="width:470px"/>
	    </td>
	</tr>
	<tr id="rowCaracteristicas" runat="server" style="height:25px;">
	    <td>Características</td>
	    <td colspan="2">
	        <asp:CheckBox ID="chkPrivado" runat="server" style="cursor:pointer" Text="Privado" Width="100px" ToolTip="Indica si el archivo está disponible para el resto de usuarios" />
	        <asp:CheckBox ID="chkLectura" runat="server" style="cursor:pointer" Text="Sólo lectura" Width="100px" ToolTip="Indica si el resto de usuarios puede actualizar el archivo" />
	        <asp:CheckBox ID="chkGestion" runat="server" style="cursor:pointer" Text="Visible desde IAP" Width="150px" ToolTip="Indica si el archivo es visible desde IAP para los profesionales"  />
	    </td>
	</tr>
	<tr id="rowAutor" style="height:25px;">
	    <td><%=strAutor %></td>
	    <td colspan="2" style="vertical-align:middle">
	        <asp:TextBox ID="txtAutor" runat="server" Style="width:490px" SkinID="Label" readonly="true" />
	        <asp:TextBox ID="txtNumEmpleado" runat="server" Style="width:1px; visibility:hidden" />
	    </td>
	</tr>
	<tr id="rowAutorModif" style="display:none; height:25px;">
	    <td>Modificado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
	    <td colspan="2"><asp:TextBox ID="txtAutorModif" runat="server" Style="width:490px" SkinID="Label" readonly="true" /></td>
	</tr>
	<tr id="rowComodin" style="display:none; height:30px;" runat="server" >
	    <td></td>
	</tr>
	</table>
	<br /><br />
	<center>
	    <table style="width:250px">
            <tr>
                <td width="45%">
				    <button id="btnAceptar" type="button" onclick="enviar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span>
				    </button>	
                </td>
                <td width="10%"></td>
                <td width="45%">
				    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
				    </button>		
                </td>
            </tr>
        </table>	
   </center>
<asp:TextBox ID="hdnsTipo" name="hdnsTipo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnnItem" name="hdnnItem" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnsAccion" name="hdnsAccion" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnnIDDOC" name="hdnnIDDOC" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" name="hdnContentServer" id="hdnContentServer" value="" runat="server" />
<input type="hidden" name="hdnError" id="hdnError" value="" runat="server" />

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
