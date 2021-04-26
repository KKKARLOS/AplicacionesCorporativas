<%@ Page Language="C#" AutoEventWireup="true" Theme="Corporativo" CodeFile="Subir.aspx.cs" Inherits="GESTAR.Subir" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Subir archivos</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../App_Themes/Corporativo/Corporativo.css" type="text/css">
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
    <base target="_self">
    <script type="text/javascript">
    <!--
        var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml|msg|xlsx|docx";
        //var exts = ".*"; //Acepta todas las extensiones.

        function init() {
            if ($I("hdnError").value != "") {
                //mmoff("Err", $I("hdnError").value, 400);
                mmoff("Err", $I("hdnError").value, 400);
            }             
            if (EsPostBack){
                aceptar();
            }
            if ($I("hdnsAccion").value == "U"){
                $I("rowArchivo").style.display = "block";
                $I("tdNuevo").innerText = "Nuevo (Máx: 50Mb)";
                $I("rowAutorModif").style.display = "block";
            }
            
            ocultarProcesando();
            if ($I('hdnModoLectura').value == 1) setop($I("tblAceptar"), 30);
            else {
                try { $I('txtDescripcion').focus(); } catch (e) { };
            }
        }
        
        function comprobarExt(value)
        {
            if(value=="")return true;
            var re = new RegExp("^.+\.("+exts+")$","i");
            if(!re.test(value))
            {
                mmoff("Inf","Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones válidas son: "+exts.replace(/\|/g,',')+" \n\n",350);
                frmUpload.txtDescripcion.value="";
                return false;
            }
            return true;
        }
	    
	    function enviar(){
	        if (getop($I("btnAceptar")) == 30) return;
	        setop($I("btnAceptar"),30);
	        
	        if (frmUpload.txtDescripcion.value==""){
	            mmoff("War", "Debe indicar la descripción del archivo", 260);
                setop($I("btnAceptar"), 100);
	            return;
	        }
	        
	        if ($I("hdnsAccion").value == "I"){
	            if (frmUpload.txtArchivo.value=="" && frmUpload.txtLink.value==""){
	                mmoff("War", "Seleccione un fichero o introduzca un link", 280);
	                setop($I("btnAceptar"), 100);
	                return;
	            }
	            if(!comprobarExt(frmUpload.txtArchivo.value))return;
	        }else{ //U
	            if (frmUpload.txtArchivoOld.value=="" && frmUpload.txtArchivo.value=="" && frmUpload.txtLink.value==""){
	                mmoff("War", "Seleccione un fichero o introduzca un link", 280);
	                setop($I("btnAceptar"), 100);
	                return;
	            }
	        }    
	        mostrarProcesando();
	        //Si no estamos en ejecutando en local o extranet (y se va a subir un archivo), que muestre la barra de progreso.
	        var strURL = location.href.toLowerCase();
	        if (strURL.indexOf("localhost") == -1 && strURL.indexOf("https") == -1 && frmUpload.txtArchivo.value != "") uploadpop();
	        else if (frmUpload.txtArchivo.value != ""){
                try{
					var fso = new ActiveXObject("Scripting.FileSystemObject");
					var nLength = fso.GetFile(frmUpload.txtArchivo.value).Size;
					//alert(nLength);
					if (nLength > 26214400){//25Mb, en bytes.
                        ocultarProcesando();
                        setop($I("btnAceptar"), 100);
                        mmoff("War", "¡Denegado! Se ha seleccionado un archivo de mayor tamaño del máximo establecido en 25Mb.", 420,2500);
					    return;
					}
                }catch(e){
                    //Para el caso en que el usuario indique No a la ventana del sistema
                    //que solicita permiso para ejecutar ActiveX
                    ocultarProcesando();
                    setop($I("btnAceptar"), 100);
                    mmoff("War", "Para poder exponer ficheros, su navegador en las políticas de seguridad debe permitir \n\"Inicializar y activar la secuencia de comandos de los\ncontroles de ActiveX no marcados como seguros\".", 420,2500);
                    return;
                }
	        }
	        frmUpload.submit();
	    }
	    
        function aceptar(){
		    var returnValue = "OK";
		    modalDialog.Close(window, returnValue);		 		    
	    }
	    
	    function cerrarVentana(){
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);		 
	    }
	    
	    function obtenerAutor(){
	        var aDatos; 
//	        var ret = window.showModalDialog("../obtenerProfesional.aspx", self, "dialogWidth:450px; dialogHeight:420px; center:yes; status:NO; help:NO;");
//	        if (ret != null){
//		        aDatos = ret.split("@@");
//                $I("txtNumEmpleado").value = aDatos[0];
//                $I("txtAutor").value = aDatos[2];
//            }

	        modalDialog.Show(strServer + "Capa_Presentacion/obtenerProfesional.aspx", self, sSize(450, 420))
            .then(function(ret) {
                if (ret != null) {
	                aDatos = ret.split("@@");
                    $I("txtNumEmpleado").value = aDatos[0];
                    $I("txtAutor").value = aDatos[2];
                }
            });	        
	        
	    }
    -->
    </script>
</head>
	<body onload="init()" leftmargin=15 topmargin=15>
		<form id="frmUpload" runat="server" enctype="multipart/form-data" method="post" name="frmUpload">
    <ucproc:Procesando ID="Procesando" runat="server" />
		<script language=javascript>
		<!--
        var strServer = '<% =Session["strServer"].ToString() %>';
        var intSession = <%=Session.Timeout%>;
		var EsPostBack = <%=EsPostBack %>;
	    function uploadpop()
	    {
	        //Tiene que ir dentro del form por contener <%//= %>
		    window.open('PorcentajeSubida.aspx?guid=<%= Request.QueryString["guid"] %>','',"resizable=no,status=no,scrollbars=no,menubar=no,toolbar=0,height=140,width=250,top="+ eval(screen.availHeight/2-40)+",left="+ eval(screen.availwidth/2-125));
	    }
		-->
        </script>
            <center>
			    <table cellPadding="5" style="margin-top:30px;margin-left:10px;text-align:left;width:650px">
			    <colgroup><col style='width:150px;' /><col width='500px' /></colgroup>
			    <tr>
			        <td>Descripción</td>
			        <td><asp:TextBox ID="txtDescripcion" runat="server" Style="width: 370px" MaxLength="50" /></td>
			    </tr>
			    <tr>
			        <td>Link</td>
			        <td><asp:TextBox ID="txtLink" runat="server" Style="width: 370px" MaxLength="250" /></td>
			    </tr>
			    <tr id="rowArchivo" style="display:none;">
			        <td>Archivo</td>
			        <td><asp:TextBox ID="txtArchivoOld" runat="server" Style="width: 370px" MaxLength="250" />
			        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="$I('txtArchivoOld').value='';txtArchivoOld" style="cursor:pointer; vertical-align:baseline;" /></td>
			    </tr>
			    <tr>
			        <td id="tdNuevo">Archivo (Máx: 25Mb)</td>
			        <td><input type="file" id="txtArchivo" runat="server" onChange="comprobarExt(this.value)" class="textareaTexto"  style="width:450px"></td>
			    </tr>
			    <tr>
			        <td>Características</td>
			        <td>
			        <asp:CheckBox ID="chkPrivado" runat="server" style="cursor:pointer" Text="Privado" width="100px" ToolTip="Indica si el archivo está disponible para el resto de usuarios" />
			        <asp:CheckBox ID="chkLectura" runat="server" style="cursor:pointer" Text="Sólo lectura" width="100px" ToolTip="Indica si el resto de usuarios puede actualizar el archivo" />
			        </td>
			    </tr>
			    <tr>
			        <td><%=strAutor %></td>
			        <td><asp:TextBox ID="txtAutor" runat="server" Style="width: 370px" SkinID="Label" ReadOnly /><asp:TextBox ID="txtNumEmpleado" runat="server" Style="width: 20px; visibility:hidden" /></td>
			    </tr>
			    <tr id="rowAutorModif" style="display:none;">
			        <td>Modificado:</td>
			        <td><asp:TextBox ID="txtAutorModif" runat="server" Style="width: 370px" SkinID="Label" ReadOnly /></td>
			    </tr>
			    </table>
            </center>   
            <center>
			    <table style="width:650px;margin-left:50px;margin-top:30px">
 	                <tr id="Pie" style="visibility:visible">
					    <td width="10%">&nbsp;</td>
					    <td width="10%">&nbsp;
					    </td>
					    <td width="30%">
						    <button id="btnAceptar" type="button" onclick="enviar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../images/botones/imgAceptar.gif" /><span title="Aceptamos el elemento seleccionado">Aceptar</span>
						    </button>						
					    </td>
					    <td width="30%">
						    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../images/botones/imgCancelar.gif" /><span title="Salir sin selección de ningun elemento"">Cancelar</span>
						    </button>						
					    </td>		
					    <td width="10%">&nbsp;</td>
					    <td width="10%">&nbsp;</td>
				    </tr>
                </table>
            </center>                      
<asp:TextBox ID="hdnsTipo" name="hdnsTipo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnnItem" name="hdnnItem" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnsAccion" name="hdnsAccion" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnnIDDOC" name="hdnnIDDOC" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" name="hdnContentServer" id="hdnContentServer" value="" runat="server" />
<input type="hidden" name="hdnError" id="hdnError" value="" runat="server" />
<asp:TextBox ID="hdnModoLectura" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" name="hdnTitle" id="hdnTitle" value="" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script language="JavaScript" src="../../Javascript/modalSubmit.js" type="text/Javascript"></script>
<script type="text/javascript">
<!--
/*
    if ($I("hdnTitle").value == "") $I("hdnTitle").value = this.id_dialog_body;
    else this.id_dialog_body = $I("hdnTitle").value;

    this.parent.document.getElementById("ui-dialog-title-" + this.id_dialog_body).innerText = this.document.title;
    var opener = this.parent;

    var modalDialog = null;
    if (typeof this.parent.modalDialog !== "undefined" && this.parent.modalDialog != null)
        modalDialog = this.parent.modalDialog;
    else
        alert("Error: no se ha inicializado la clase para mostrar ventanas modales.");
        */
-->
</script>
</body>
</html>
