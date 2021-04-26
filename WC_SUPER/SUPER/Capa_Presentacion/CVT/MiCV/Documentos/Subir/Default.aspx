<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title>Documento a subir</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>    	
    <link rel="stylesheet" href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css"/>        
</head>
<body onload="init()" onunload="unload()">
    <form id="form1" runat="server">
    <ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
   -->
    </script>  
    <br /><br /><br />
        <label style="position:absolute; top:12px; left:30px;" class="texto" runat="server">Nombre</label>
        <asp:TextBox ID="txtDescrip" style="position:absolute; top:12px; left:150px; width:345px;" Text="" runat="server"  />    
        <label id="lblArchivo" class="texto" style="position:absolute; top:52px; left:30px;">Archivo (Max 25 Mb)</label>
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
        
        <div style="position:absolute; top:50px; left:150px">				
            <ajaxToolkit:AsyncFileUpload ID="FileDocumento" Width="450px" runat="server" ToolTip="Selección y subida del fichero"
            OnClientUploadError="uploadError" 
            OnClientUploadStarted="StartUpload"
            OnClientUploadComplete="UploadComplete"
            AllowedFileTypes="zip,rar,jpg,gif,doc,rtf,xls,pps,ppt,txt,pdf,xml,msg,xlsx,docx"             
            CompleteBackColor="Lime" 
            UploaderStyle="Modern" 
            ErrorBackColor="Red" 
            ThrobberID="Throbber" 
            onuploadedcomplete="FileDocumento_UploadedComplete" 
            onuploadedfileerror="FileDocumento_UploadedFileError"
            UploadingBackColor="#66CCFF" />		
			 
            <asp:Label ID="Throbber" runat="server" Style="display: none">
	            <img src="../../../../../images/indicator.gif" align="absmiddle" alt="loading" />
            </asp:Label>
            <br />
            <br />			
            <asp:Label ID="lblStatus" runat="server" Style="font-family: Arial; font-size: small;"></asp:Label>
        </div>	
        <center>	
        <br /><br />
        <br /><br />   
        <br /><br />
        <br /><br /> 
        <br /><br />
        <br /><br />
        <table style="width:300px; margin-top:10px;">
		    <tr>
			    <td align="center">
                    <button id="btnAceptar" type="button" disabled onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                    </button>
			    </td>
			    <td align="center">
                    <button id="btnCancelar" type="button" onclick="CancelarDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                    </button>
			    </td>		    
		    </tr>
        </table>
        </center>
        <input type="hidden" id="hdnErrores" value="<%=sErrores %>" />  
        <asp:textbox id="hdnID" runat="server" style="visibility:hidden"></asp:textbox>
        <asp:textbox id="hdnAccion" runat="server" style="visibility:hidden"></asp:textbox>                  
        <input type="hidden" id="hdnContentServer" value="" runat="server" />  
        <input type="hidden" id="hdnNomArchivo" value="" runat="server" />  
        <input type="hidden" name="hdnTitle" id="hdnTitle" value="" runat="server" />
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />        
    </form>
    
    <script type="text/javascript">
	    <!--
        if ($I("ctl00$hdnRefreshPostback") == null) {
            var oRefreshPostback = document.createElement("INPUT");
            oRefreshPostback.setAttribute("type", "text");
            oRefreshPostback.setAttribute("style", "visibility:hidden");
            oRefreshPostback.setAttribute("id", "ctl00$hdnRefreshPostback");
            oRefreshPostback.setAttribute("name", "ctl00$hdnRefreshPostback");
            oRefreshPostback.setAttribute("value", "N");
        }
        if (!document.getElementById("hdnTitle"))
            alert('Error: no se ha declarado el campo hdnTitle');
        else if ($I("hdnTitle").value == "") {
            //$I("hdnTitle").value = this.id_dialog_body;
            //this.parent.document.getElementById("ui-dialog-title-" + this.id_dialog_body).innerText = this.document.title;
            $I("hdnTitle").value = this.name;
            this.parent.document.getElementById("ui-dialog-title-" + this.name).innerText = this.document.title;
        }

        var opener = this.parent;

        var modalDialog = null;
        if (typeof this.parent.modalDialog !== "undefined" && this.parent.modalDialog != null)
            modalDialog = this.parent.modalDialog;
        else
            alert("Error: no se ha inicializado la clase para mostrar ventanas modales.");

        //Devuelve el opener del diálogo, sea página maestra o modalDialog
        function fOpener() {
            if (modalDialog.array_dialogos.length > 1)//Si hay más de un diálogo, el opener es otro diálogo
                return opener.$I(modalDialog.array_dialogos[modalDialog.array_dialogos.length - 2]).contentWindow;
            else//Sino el opener es la página maestra
                return opener;
        }
        var bLectura = false;	    

    
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
        -->
    </script>
</body>
</html>
