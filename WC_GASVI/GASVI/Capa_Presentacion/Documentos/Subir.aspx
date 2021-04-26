<%@ Page Language="C#" AutoEventWireup="true" Theme="Corporativo" CodeFile="Subir.aspx.cs" Inherits="GASVI.Subir" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
    <title>Subir archivos</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
   <script language=javascript>
    <!--
        var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml";
        //var exts = ".*"; //Acepta todas las extensiones.

        function init(){
            if (EsPostBack){
                aceptar();
            }
            if ($I("hdnsAccion").value == "U"){
                $I("rowArchivo").style.display = "block";
                $I("tdNuevo").innerText = "Nuevo (Máx: 25Mb)";
                $I("rowAutorModif").style.display = "block";
            }
            ocultarProcesando();
        }
        
        function comprobarExt(value)
        {
            if(value=="")return true;
            var re = new RegExp("^.+\.("+exts+")$","i");
            if(!re.test(value))
            {
                alert("Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones válidas son: "+exts.replace(/\|/g,',')+" \n\n");
                //frmUpload.txtDescripcion.value="";
                frmUpload.txtArchivo.value = "";
                setOp($I("tblAceptar"),100);
                return false;
            }
            return true;
        }
	    
	    function enviar(){
	        if (getOp($I("tblAceptar")) == 30) return;
	        setOp($I("tblAceptar"),30);
	        
	        if (frmUpload.txtDescripcion.value==""){
                alert("Debe indicar la descripción del archivo");
    	        setOp($I("tblAceptar"),100);
	            return;
	        }
	        
	        if ($I("hdnsAccion").value == "I"){
	            if (frmUpload.txtArchivo.value=="" && frmUpload.txtLink.value==""){
	                alert("Seleccione un fichero o introduzca un link");
        	        setOp($I("tblAceptar"),100);
	                return;
	            }
	            if(!comprobarExt(frmUpload.txtArchivo.value))return;
	        }else{ //U
	            if (frmUpload.txtArchivoOld.value=="" && frmUpload.txtArchivo.value=="" && frmUpload.txtLink.value==""){
	                alert("Seleccione un fichero o introduzca un link");
        	        setOp($I("tblAceptar"),100);
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
        	            setOp($I("tblAceptar"),100);
					    alert("¡Denegado! Se ha seleccionado un archivo mayor del máximo establecido en 25Mb.");
					    return;
					}
                }catch(e){
                    //Para el caso en que el usuario indique No a la ventana del sistema
                    //que solicita permiso para ejecutar ActiveX
                    ocultarProcesando();
        	        setOp($I("tblAceptar"),100);
                    alert("Para poder exponer ficheros, su navegador en las políticas de seguridad debe permitir \n\"Inicializar y activar la secuencia de comandos de los\ncontroles de ActiveX no marcados como seguros\".");
                    return;
                }
	        }
	        frmUpload.submit();
	    }
	    
        function aceptar(){
            //		    window.returnValue = "OK";
            //		    window.close();
            var returnValue = "OK";
            modalDialog.Close(window, returnValue);	
	    }
	    
	    function cerrarVentana(){
	        //        window.returnValue = null;
	        //        window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
	    
	    function obtenerAutor(){
	        var aDatos; 
	        mostrarProcesando();
//	        var ret = window.showModalDialog(strServer +  "Capa_Presentacion/getProfesional.aspx", self, sSize(450, 520)); 
//	        window.focus();

	        var strEnlace = strServer + "Capa_Presentacion/getProfesional.aspx";
	        modalDialog.Show(strEnlace, self, sSize(450, 520))
            .then(function(ret) {
	            if (ret != null) {
	                //alert(ret);
	                aDatos = ret.split("@#@");
	                $I("txtNumEmpleado").value = aDatos[0];
	                $I("txtAutor").value = aDatos[1];
	            }
	            ocultarProcesando();
            });   	          
	    }
    -->
    </script>
</head>
	<body onload="init()" leftmargin=15 topmargin=15>
        <ucproc:Procesando ID="Procesando" runat="server" />
		<form id="frmUpload" runat="server" enctype="multipart/form-data" method="POST" name="frmUpload">
	<script language=javascript>
	<!--
	    var EsPostBack = <%=EsPostBack %>;
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["GVT_strServer"]%>";
	    function uploadpop()
	    {
	        //Tiene que ir dentro del form por contener <%//= %>
		    window.open('PorcentajeSubida.aspx?guid=<%= Request.QueryString["guid"] %>','',"resizable=no,status=no,scrollbars=no,menubar=no,toolbar=0,height=140,width=250,top="+ eval(screen.availHeight/2-40)+",left="+ eval(screen.availWidth/2-125));
	    }
	-->
    </script>
	<table cellSpacing="5" style="width:630px;margin:10px;">
	<colgroup><col style="width:110px;" /><col style="width:520px;" /></colgroup>
	<tr>
	    <td>Descripción</td>
	    <td><asp:TextBox ID="txtDescripcion" runat="server" Style="width: 370px" MaxLength="50" /></td>
	</tr>
	<tr id="rowLink" runat="server">
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
	    <td><input type="file" id="txtArchivo" runat="server" onChange="comprobarExt(this.value)" class="txtM"  style="width:450px"></td>
	</tr>
	<tr id="rowCaracteristicas" runat="server">
	    <td>Características</td>
	    <td>
	    <asp:CheckBox ID="chkPrivado" runat="server" style="cursor:pointer" Text="Privado" Width="100px" ToolTip="Indica si el archivo está disponible para el resto de usuarios" />
	    <asp:CheckBox ID="chkLectura" runat="server" style="cursor:pointer" Text="Sólo lectura" Width="100px" ToolTip="Indica si el resto de usuarios puede actualizar el archivo" />
	    <asp:CheckBox ID="chkGestion" runat="server" style="cursor:pointer" Text="Visible desde IAP" Width="150px" ToolTip="Indica si el archivo es visible desde IAP para los profesionales"  />
	    </td>
	</tr>
	<tr>
	    <td><%=strAutor %></td>
	    <td><asp:TextBox ID="txtAutor" runat="server" Style="width: 500px" SkinID="Label" ReadOnly /><asp:TextBox ID="txtNumEmpleado" runat="server" Style="width: 20px; visibility:hidden" /></td>
	</tr>
	<tr id="rowAutorModif" style="display:none;">
	    <td>Modificado:</td>
	    <td><asp:TextBox ID="txtAutorModif" runat="server" Style="width: 500px" SkinID="Label" ReadOnly /></td>
	</tr>
	</table>
	<table style="position:absolute; top:190px;width:650px;text-align:center">
        <tr>
            <td width="25%"></td>
            <td>
                <button id="tblAceptar" type="button" onclick="enviar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>
             </td>
            <td>
                <button id="tblCancelar" type="button" onclick="cerrarVentana();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>
            </td>
            <td width="25%"></td>
        </tr>
    </table>
<asp:TextBox ID="hdnsTipo" name="hdnsTipo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnnItem" name="hdnnItem" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnsAccion" name="hdnsAccion" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnnIDDOC" name="hdnnIDDOC" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" name="hdnTitle" id="hdnTitle" value="" runat="server" />
    </form>
    <script language="JavaScript" src="../../Javascript/modalSubmit.js" type="text/Javascript"></script>
	</body>
</html>
