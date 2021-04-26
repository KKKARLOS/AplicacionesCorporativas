<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>   
		<script type="text/javascript">
		    function navegador() {
		        /*  Nombre a utilizar en la aplicación del navegador correspondiente
		        opera   -- Opera 
		        ie      -- Internet Explorer 
		        safari  -- Safari 
		        firefox -- FireFox 
		        mozilla -- Mozilla 
		        chrome  -- Chome
		        */
		        var ua = navigator.userAgent.toLowerCase();
		        if (ua.indexOf("opera") != -1) nName = "opera";
		        else if (ua.indexOf("msie") != -1) nName = "ie";
		        else if (ua.indexOf("chrome") != -1) nName = "chrome";
		        else if (ua.indexOf("safari") != -1) nName = "safari";
		        else if (ua.indexOf("mozilla") != -1) {
		            if (ua.indexOf("firefox") != -1) nName = "firefox";
		            else nName = "mozilla";
		        }
		        nVer = navigator.appVersion;
		        ie = (document.all) ? true : false;
		    }
		    navegador();
		    function sSize(iWidth, iHeight) {
		        try {
		            var sUnidad = (nName != "chrome") ? "px" : "";
		            var sReturn = "dialogWidth: " + iWidth + sUnidad + "; ";
		            sReturn += "dialogHeight: " + iHeight + sUnidad + "; ";
		            sReturn += "dialogLeft: " + parseInt(((window.screen.width - iWidth) / 2), 10) + sUnidad + "; ";
		            sReturn += "dialogTop: " + parseInt(((window.screen.height - iHeight) / 2), 10) + sUnidad + "; ";
		            sReturn += "center:yes; status:NO; help:NO;";
		            return sReturn;
		        } catch (e) {
		            alert("Error al establecer el tamaño de la ventana " + e.message);
		        }
		    }
		    function cerrarVentana() {
		        //alert("Default.aspx -> cerrarVentana()");
		        if (!bMultiVentana) {
		            var ventana = window.self;
		            if (ventana.history.length == 0) {
		                ventana.opener = window.self;
		                ventana.open("", "_parent", "");
		                ventana.close();
		            }
		        } 
		        else {
		            switch (nName) {
		                case "ie":
		                    var ventana = window.self;
		                    ventana.opener = window.self;
		                    ventana.open("", "_parent", "");
		                    ventana.close();
		                    break;
		                case "chrome":
		                    //open("", '_self').close();
		                    break;
		                default:
		                    open("", '_self').close();
		                    break;
		            }
		        }
		    }

		    function init() {
		        var strMsg=document.getElementById("hdnError").value;
		        if (strMsg != "") {
		            var reg = /\|n/g;
		            alert(strMsg.split("@#@")[0].replace(reg, "\n"));
		        }
		        var sNombreVentana = "SUPER";
		        if (bMultiVentana) sNombreVentana = "";
		        var sMostrarIAP=document.getElementById("hdnMostrarIAP30").value;
		        
		        if (bEntrar) {
		            window.open(strEnlace, sNombreVentana, "resizable=no,status=no,scrollbars=yes,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
		        }
		        cerrarVentana();
		        
		    }
        </script>
	</head>
	<body style="OVERFLOW:hidden" onload="init()">
    <form id="form1" runat="server" method="post">
    <script type="text/javascript">
        var bEntrar = <% =((bool)bEntrar)? "true":"false" %>;
        var strEnlace = "<% =strEnlace %>";
        var bMultiVentana = <% =(Session["MULTIVENTANA"] != null && (bool)Session["MULTIVENTANA"])? "true":"false" %>;
	    
        // los valores de servidor que se vayan a recoger en cliente, deben estar dentro del formulario,
        // ya que en caso contrario se produce un error, 
        // "The Controls collection cannot be modified because the control contains code blocks"
    </script>
    <br /><br />
    <center>
        <asp:Label ID="Label1" runat="server" Text="Iniciando aplicación. Espere por favor..." Font-Names="Arial" Font-Size="12px"></asp:Label><!-- onclick="simularacceso();" -->
     </center>
     <input type="hidden" runat="server" name="hdnIdFicepi" id="hdnIdFicepi" value="" />
     <input type="hidden" runat="server" id="hdnError" value="" />
     <input type="hidden" runat="server" id="hdnMostrarIAP30" value="N" />

    </form>
</body>
</html>
