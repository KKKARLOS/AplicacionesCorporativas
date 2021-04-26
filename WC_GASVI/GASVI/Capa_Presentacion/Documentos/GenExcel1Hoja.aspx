<%@ Page Language="C#" AutoEventWireup="true"  ValidateRequest="false" CodeFile="GenExcel1Hoja.aspx.cs" Inherits="Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Location="None" VaryByParam="None" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Descargar archivo</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
		<script type="text/javascript">
		<!--

            var sBrowser="";
            var sVersion="";
            //alert(navigator.userAgent.toLowerCase());
            function setBrowserType(){
                var aBrowFull = new Array("opera", "msie", "netscape");
                var aBrowAbrv = new Array("op",    "ie",   "ns"      );
                var sInfo = navigator.userAgent.toLowerCase();

                sBrowser = "";
                for (var i = 0; i < aBrowFull.length; i++){
                 if ((sBrowser == "") && (sInfo.indexOf(aBrowFull[i]) != -1)){
                  sBrowser = aBrowAbrv[i];
                  sVersion = String(parseFloat(sInfo.substr(sInfo.indexOf(aBrowFull[i]) + aBrowFull[i].length + 1)));
                 }
                }
             }
             
            setBrowserType();
             //alert("Navegador: "+ sBrowser +"\nVersión: "+ sVersion);

            function cerrarVentana(){
                if (!bMultiVentana){
	                var ventana = window.self;
	                if (ventana.history.length == 0){
		                ventana.opener = window.self;
		                ventana.open("","_parent","");
		                ventana.close();
	                }
	            }else{
	                var ventana = window.self;
	                ventana.opener = window.self;
	                ventana.open("","_parent","");
	                ventana.close();
	            }
            }


			function init(){
				cerrarVentana();
			}			
		-->
        </script>
</head>
<body onload="init()">
    <form id="form1" runat="server">
        <script type="text/javascript">
        <!--
            var bMultiVentana = <% =(Session["MULTIVENTANA"] != null && (bool)Session["MULTIVENTANA"])? "true":"false" %>;
       -->
        </script>        
    </form>
</body>
</html>
