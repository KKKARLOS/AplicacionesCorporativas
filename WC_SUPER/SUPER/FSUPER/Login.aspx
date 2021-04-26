<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge"/>   
<base target="_self" />
    <title></title>

<script type="text/javascript" src="../Javascript/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="../scripts/IB.js"></script>
<script type="text/javascript" src="../scripts/dal.js"></script>
<script type="text/javascript" src="../Javascript/bowser.min.js"></script>
<script language="JavaScript" src="FuncionesLogin.js" type="text/Javascript"></script>   
<style type="text/css">
#divLogin
{
    background-color: #fff;
    -pie-background: linear-gradient(top, #fff, #eee);
    height: auto;
    width: 400px;
    margin: -150px 0 0 -230px;
    padding: 30px;
    position: absolute;
    top: 15%;
    left: 45%;
    z-index: 0;
    -moz-border-radius: 3px;
    -webkit-border-radius: 3px;
    border-radius: 3px;
    -webkit-box-shadow: 0 0 2px rgba(0, 0, 0, 0.2), 0 1px 1px rgba(0, 0, 0, .2), 0 3px 0 #fff, 0 4px 0 rgba(0, 0, 0, .2), 0 6px 0 #fff, 0 7px 0 rgba(0, 0, 0, .2);
    -moz-box-shadow: 0 0 2px rgba(0, 0, 0, 0.2), 1px 1px 0 rgba(0, 0, 0, .1), 3px 3px 0 rgba(255, 255, 255, 1), 4px 4px 0 rgba(0, 0, 0, .1), 6px 6px 0 rgba(255, 255, 255, 1), 7px 7px 0 rgba(0, 0, 0, .1);
    background-image: linear-gradient(top, #fff, #eee);/*ie 6-9 via PIE*/
    /*Permite usar algunos atributos CSS3 en versiones de IE*/
    behavior: url(../PIE.htc);
}

/*Marco punteado*/
#divLogin:before
{
    content: '';
    position: absolute;
    z-index: -1;
    border: 1px dashed #ccc;
    top: 5px;
    bottom: 5px;
    left: 5px;
    right: 5px;
    -moz-box-shadow: 0 0 0 1px #fff;
    -webkit-box-shadow: 0 0 0 1px #fff;
    box-shadow: 0 0 0 1px #fff;
   
}

fieldset
{
    border: 0;
    padding: 0;
    margin: 0;
}

h1
{
    text-shadow: 0 1px 0 rgba(255, 255, 255, .7), 0px 2px 0 rgba(0, 0, 0, .5);
    text-transform: uppercase;
    text-align: center;    
    margin: 0 0 30px 0;
    letter-spacing: 4px;
    font: normal 20px Verdana, Helvetica;
    position: relative;
}

/*Imágenes de los inputs url(../Images/cambio-pass.png)*/
input[type="text"],input[type="password"]   {        
    background: #f1f1f1 url(../Forastero/Images/cambio-pass.png) no-repeat;  
    padding: 10px 10px 10px 30px;
    margin: 0 0 10px 0;
    width: 250px; /* 353 + 2 + 45 = 400 */
    border: 1px solid #ccc;
    -moz-border-radius: 5px;
    -webkit-border-radius: 5px;
    border-radius: 5px;
    -moz-box-shadow: 0 1px 1px #ccc inset, 0 1px 0 #fff;
    -webkit-box-shadow: 0 1px 1px #ccc inset, 0 1px 0 #fff;
    box-shadow: 0 1px 1px #ccc inset, 0 1px 0 #fff;
    font: normal 12px Verdana, Helvetica;      
}

#UserName{background-position: 5px -8px !important;}
#Password{background-position: 5px -58px !important;}

/*FIN Imágenes de los inputs*/

#Login1_UserNameLabel,#Login1_PasswordLabel
{
	color:#666;		
	display:block;
	margin-bottom:3px;	
	font-weight:bold;
}

/*Estilo botones*/
input[type="button"]
{		
    background-color: #ffb94b;
    background-image: -webkit-gradient(linear, left top, left bottom, from(#fddb6f), to(#ffb94b));
    background-image: -webkit-linear-gradient(top, #fddb6f, #ffb94b);
    background-image: -moz-linear-gradient(top, #fddb6f, #ffb94b);
    background-image: -ms-linear-gradient(top, #fddb6f, #ffb94b);
    background-image: -o-linear-gradient(top, #fddb6f, #ffb94b);
    background-image: linear-gradient(top, #fddb6f, #ffb94b);
    
    -moz-border-radius: 3px;
    -webkit-border-radius: 3px;
    border-radius: 3px;
    
    text-shadow: 0 1px 0 rgba(255,255,255,0.5);
    
     -moz-box-shadow: 0 0 1px rgba(0, 0, 0, 0.3), 0 1px 0 rgba(255, 255, 255, 0.3) inset;
     -webkit-box-shadow: 0 0 1px rgba(0, 0, 0, 0.3), 0 1px 0 rgba(255, 255, 255, 0.3) inset;
     box-shadow: 0 0 1px rgba(0, 0, 0, 0.3), 0 1px 0 rgba(255, 255, 255, 0.3) inset;    
    
    border-width: 1px;
    border-style: solid;
    border-color: #d69e31 #e3a037 #d5982d #e3a037;
    float: left;
    height: 30px;
    padding: 0;
    width: 150px;
    cursor: pointer;
    font: bold 15px Arial, Helvetica;
    color: #8f5a0a;
	margin-top:5px;	
	text-align:center;   	
	
}

input[type="text"]:focus,input[type="password"]:focus
{
    border-width: 1px;
    border-style: solid;
    border-color: #d69e31 #e3a037 #d5982d #e3a037;
}

input[type="button"]:hover,input[type="button"]:focus
{		
    background-color: #fddb6f;
    background-image: -webkit-gradient(linear, left top, left bottom, from(#ffb94b), to(#fddb6f));
    background-image: -webkit-linear-gradient(top, #ffb94b, #fddb6f);
    background-image: -moz-linear-gradient(top, #ffb94b, #fddb6f);
    background-image: -ms-linear-gradient(top, #ffb94b, #fddb6f);
    background-image: -o-linear-gradient(top, #ffb94b, #fddb6f);
    background-image: linear-gradient(top, #ffb94b, #fddb6f);      
}	

input[type="button"]:active,input[type="text"]:active,input[type="password"]:active
{		
    outline: none;
   
     -moz-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.5) inset;
     -webkit-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.5) inset;
     box-shadow: 0 1px 4px rgba(0, 0, 0, 0.5) inset;	
     
    
}

input[type="button"]::-moz-focus-inner
{
  border: none;  
}

#divNavegador {
    font-size: 12px;
}

/*FIN estilo botones*/


.links{color:#666;text-decoration:none;font: normal 12px Verdana, Helvetica; cursor:hand; cursor:pointer}

.links:hover {text-decoration:underline;}
.titulo
{
	color:#666;			
	margin-bottom:3px;	
	font-weight:bold;
}
</style>        

  
</head>

<body onload="init()">
<form id="form1" runat="server">

      <div style="text-align:center; display:none" id="divNavegador" runat="server">
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

                            <a target="_blank" href="https://www.google.com.mx/chrome/browser/desktop/?brand=CHBD&gclid=EAIaIQobChMI8dzM6Iux1gIVi4eyCh07nA-rEAAYASAAEgL7VvD_BwE"><img title="Navegador Chrome" alt="Navegador Chrome" src="../Images/imgChrome.png" style="width: 64px" /></a>
                            <a target="_blank" href="https://www.mozilla.org/es-ES/firefox/new/"><img title="Navegador Firefox" alt="Navegador Firefox" src="../Images/imgFirefox.png" style="width: 64px" /></a>
                            <a target="_blank" href="https://support.apple.com/es_ES/downloads/safari"><img title="Navegador Safari de macOS" alt="Navegador Safari de macOS" src="../Images/imgSafari.png" style="width: 64px" /></a>
                            <a target="_blank" href="https://www.microsoft.com/es-es/download/internet-explorer-11-for-windows-7-details.aspx"><img title="Navegador Internet Explorer" alt="Navegador Internet Explorer" src="../Images/imgExplorer.png" style="width: 64px" /></a>
                        </div>
                        
                    </div>
                </div>
            </div>


    <div id="divLogin" style="width:500px; margin-top:30px;" runat="server">
         <div style="color:White;background-color:#507CD1; font-weight:bold; font-size:large;">
            <h1>Iniciar sesión en SUPER</h1>
         </div>
         <br /> <br /> <br /> <br /> <br />
           <fieldset id ="inputs" style="border:none">     
                    <label class="titulo">Usuario</label>
                        <input type="text" id="UserName" runat="server" maxlength="20" style="margin-left:80px"  onkeypress="if(event.keyCode==13){ComprobarUsuario();event.keyCode=0;}" onchange="activarCambios()" />
                    <br />
                    <label id="lblIntroNIF" class="titulo" style="margin-left:125px;color:#3f7abc">Introduce tu NIF y pulsa &lt;Intro&gt (Sólo números y letras)</label>
                    <label class="titulo" id="lblPassword" style="visibility:hidden">Contraseña</label>
                        <input type="password" id="Password" runat="server" style="margin-left:58px;visibility:hidden" onchange="activarCambios()"  onkeypress="if(event.keyCode==13){IniciarSesion();event.keyCode=0;}" maxlength="20" />  <br />                            
                        <label id="LabelPass" class="titulo" style="visibility:hidden;color:#3f7abc; margin-left:125px;">Introduce tu contraseña y pulsa &lt;Intro&gt</label><br /><br /><br />
                        <input type="button" ID="LoginButton" runat="server" value="Inicio de Sesión" onclick="IniciarSesion()" style="display:none;margin-left:270px;" />
            </fieldset>                           
           <br /><br /><br />
           <div id="btnEnlaces" style="display:none">
               <label ID="LinkButton1" class="links" onclick="btn_recordar()"  runat="server">He olvidado mi contraseña</label>
               <br /><br />
               <label ID="LinkButton2" runat="server" class="links"  onclick="btn_cambiarPass()">Cambio de contraseña</label>
               <br /><br />
               <label ID="LinkButton3" runat="server" class="links"   onclick="btn_cambiarPregunta()">Establecimiento de pregunta/respuesta</label><br /><br />
           </div>                                  
    </div>
           <div id="divMsg"  style="width:300px; color:Red; position:relative; top:225px; left:40%; text-decoration:none;font:normal 14px Verdana, Helvetica; text-align:left;" runat="server" ></div>    
</form>
</body>
</html>
