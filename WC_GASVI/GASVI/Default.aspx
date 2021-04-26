<%@ Page language="c#" Inherits="GASVI.Default" CodeFile="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
		<title>GASVI</title>
		<meta http-equiv='X-UA-Compatible' content='IE=edge' />
		<script language="javascript" type="text/javascript">
		<!--
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
			
		    function get_browser(){
		        var ua=navigator.userAgent,tem,M=ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || []; 
		        if(/trident/i.test(M[1])){
		            tem=/\brv[ :]+(\d+)/g.exec(ua) || []; 
		            return {name:'IE',version:(tem[1]||'')};
		        }   
		        if(M[1]==='Chrome'){
		            tem=ua.match(/\bOPR\/(\d+)/)
		            if(tem!=null)   {return {name:'Opera', version:tem[1]};}
		        }   
		        M=M[2]? [M[1], M[2]]: [navigator.appName, navigator.appVersion, '-?'];
		        if((tem=ua.match(/version\/(\d+)/i))!=null) {M.splice(1,1,tem[1]);}
		        return {
		            name: M[0],
		            version: M[1]
		        };
		    }

			function cerrarVentana() {
		        //alert("Default.aspx -> cerrarVentana()");
	            var ventana = window.self;
				switch (nName) {
					case "ie":
						if (ventana.history.length == 0) {
							var ventana = window.self;
							ventana.opener = window.self;
							ventana.open("", "_parent", "");
							ventana.close();
						}
						break;
					case "chrome":
						if (ventana.history.length == 1) 
							open("", '_self').close();
						break;
					default: //safari y firefox
						if (ventana.history.length == 1) 
							open("", '_self').close();
						break;
				}
		    }
		    
			function init() {
			    if (strMsg != "") {
			        var reg = /\|n/g;
			        alert(strMsg.replace(reg, "\n"));
			    }
			    if (bEntrar) {

			        var browser=get_browser();
			        
			        if (browser.version < 11){
			            alert("Tu navegador no está actualizado. Es posible que no puedas utilizar todas las funcionalidades de la aplicación.");
			        }
			            

			           

//			        var strMsgTramit = "Tramitación de las notas de gastos de viaje en el cierre del ejercicio 2011\n\n";
//			        strMsgTramit += "De cara a la correcta imputación de los gastos de viaje en el cierre del ejercicio, se deberán cumplir los siguientes plazos:\n\n";
//			        strMsgTramit += "Los interesados y solicitantes podrán tramitar sus respectivas liquidaciones hasta el día 4 de enero. A partir de esta fecha no pueden quedar solicitudes de viajes de 2011 en estado de aparcada, recuperada o denegada. Es necesario que se envíen los justificantes a los respectivos \"Centros liquidadores\" con la mayor celeridad.\n\n";
//			        strMsgTramit += "A partir del día 5 de enero el sistema no permitirá tramitar liquidaciones correspondientes a viajes realizados en 2011, ni tan siquiera las aparcadas con anterioridad a dicha fecha.\n\n";
//			        strMsgTramit += " Los responsables tienen de plazo para aprobarlas hasta el 5 de enero.";
//			        alert(strMsgTramit);
				    window.open(strEnlace, "GASVINET", "resizable=no,status=no,scrollbars=1,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
				}

//				if (strMsg != ""){
//					alert(strMsg.split("@#@")[0]);
//				}else{
//				    window.open(strEnlace, "GASVINET", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
//				}
				cerrarVentana();
			}
		-->
		</script>
	</head>
	<body style="overflow:hidden" bgcolor="Silver" onload="init()"><!-- background="images/imgLogin.gif" -->
		<form id="Form1" method="post" runat="server">
            <script language="javascript" type="text/javascript">
            <!--
                var strMsg = "<% =strMsg %>";
	            var strEnlace = "<% =strEnlace %>";
                //los valores de servidor que se vayan a recoger en cliente, deben estar dentro del formulario,
                //ya que en caso contrario se produce un error, 
                //"The Controls collection cannot be modified because the control contains code blocks"
                var bEntrar = <% =((bool)bEntrar)? "true":"false" %>;
           -->
            </script>
            <table style="width:100%">
	            <tr style="height:10px"><td colspan="2"></td></tr>
	            <tr style="text-align:center">
		            <td rowspan="2" style="text-align:center"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/imgnet.gif" /></td>
		            <td>
		                <font style="font-family: MS Sans Serif; font-size:12;"><br />
                        &nbsp;&nbsp;Iniciando aplicación. Espere por favor...</font></td>
                </tr>
	            <tr><td colspan="2"></td></tr>
            </table>		
         </form>
	</body>
	
	
</html>
