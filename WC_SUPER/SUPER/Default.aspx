<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>   
    
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <style type="text/css">
        #divNavegador {
            font-size:12px;
        }
    </style>

    <script type="text/javascript" src="Javascript/jquery-1.7.1/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="scripts/IB.js"></script>
    <script type="text/javascript" src="scripts/dal.js"></script>
    <script type="text/javascript" src="Javascript/bowser.min.js"></script>
        
		<script type="text/javascript">		   
		    
		    function cerrarVentana() {		        
		        if (!bMultiVentana) {
		            var ventana = window.self;
		            if (ventana.history.length == 0) {
		                ventana.opener = window.self;
		                ventana.open("", "_parent", "");
		                ventana.close();
		            }
		        } 
		        else {
		            switch (bowser.msie) {
		                case true:
		                    var ventana = window.self;
		                    ventana.opener = window.self;
		                    ventana.open("", "_parent", "");
		                    ventana.close();
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

		        var allowBrowser = false;
		        if ('querySelector' in document && 'localStorage' in window && 'addEventListener' in window) { 		            
		            if (bowser.msie && parseInt(bowser.version) < 11) 
		            {		            		            
		                document.getElementById("Label1").className="hide";
		                logoff();
		            } 
		            else {
		                allowBrowser = true;                        
		            }
		        }

		        if (!allowBrowser) {
		            document.getElementById("divNavegador").className="show";
		        }
		        else {
		            var sNombreVentana = "SUPER";
		            if (bMultiVentana) sNombreVentana = "";
		            //var sMostrarIAP=document.getElementById("hdnMostrarIAP30").value;
		        
		            if (bEntrar) {
		                window.open(strEnlace, sNombreVentana, "resizable=no,status=no,scrollbars=yes,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
		            }
		            cerrarVentana();
		        }		      		        
		    }

		    function logoff () {
		        IB.DAL.post(null, "logoff", null, null, null)		            		        
		    }
        </script>
	</head>
	<body style="OVERFLOW:hidden" onload="init()">
    
    <div class="hide" style="text-align:center" id="divNavegador" runat="server">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">                            
                            <h4 class="modal-title">Información sobre el navegador</h4>
                        </div>
                        <div class="modal-body">
                            <p>
                                Tu navegador, o la versión del mismo, no es compatible con esta aplicación.
                            </p>
                            <br/>
                            <p>Te sugerimos que utilices uno de los que se muestra a continuación.
                                <br /><br /><br /><br />
                                Si necesitas ayuda, ponte en contacto con el CAU.</p>

                            <br />

                            <a target="_blank" href="https://www.google.com.mx/chrome/browser/desktop/?brand=CHBD&gclid=EAIaIQobChMI8dzM6Iux1gIVi4eyCh07nA-rEAAYASAAEgL7VvD_BwE"><img title="Navegador Chrome" alt="Navegador Chrome" src="Images/imgChrome.png" style="width: 64px" /></a>
                            <a target="_blank" href="https://www.mozilla.org/es-ES/firefox/new/"><img title="Navegador Firefox" alt="Navegador Firefox" src="Images/imgFirefox.png" style="width: 64px" /></a>
                            <a target="_blank" href="https://support.apple.com/es_ES/downloads/safari"><img title="Navegador Safari de macOS" alt="Navegador Safari de macOS" src="Images/imgSafari.png" style="width: 64px" /></a>
                            <a target="_blank" href="https://www.microsoft.com/es-es/download/internet-explorer-11-for-windows-7-details.aspx"><img title="Navegador Internet Explorer" alt="Navegador Internet Explorer" src="Images/imgExplorer.png" style="width: 64px" /></a>
                        </div>
                        
                    </div>
                </div>
            </div>

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
     <!--<input type="hidden" runat="server" id="hdnMostrarIAP30" value="N" />-->

    </form>
</body>
</html>


