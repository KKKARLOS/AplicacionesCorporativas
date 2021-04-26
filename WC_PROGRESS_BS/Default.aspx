<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register  Src="~/uc/SoloCabecera.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/CabeceraEntrada.ascx" TagPrefix="cb1" TagName="CabeceraEntrada" %>


<!DOCTYPE html>


<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">    
    <title>Progress</title>
    <meta charset="utf-8">    
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv='Expires' content='0' />
    <meta http-equiv='Pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache' />
    <link href="css/bootstrap.css" rel="stylesheet">
    <script type="text/javascript" src="js/bowser.min.js"></script>
     <script type="text/javascript">       
         var strServer = "<%=Session["strServer"]%>";    
    </script>

 <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
   
</head>

<body class="skin-blue">
    
    <cb1:CabeceraEntrada runat="server" ID="CabeceraEntrada" />
    
    <div id="divIniciando" style="display: none;position:fixed;top:20%;" class="col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4">
        <img id="imgIniciando" src="imagenes/imgIniciando.gif"  />
    </div>
    

      <div class="hide" style="text-align:center" id="divNavegador" runat="server">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button id="btnCerrarNavegador" type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Información sobre el navegador</h4>
                        </div>
                        <div class="modal-body">
                            <p>l
                                Tu navegador, o la versión del mismo, no es compatible con esta aplicación.
                            </p>
                            <p>Te sugerimos que utilices uno de los que se muestra a continuación.
                                <br />
                                Si no dispones de ellos, ponte en contacto con el CAU.</p>

                            <img src="imagenes/imgChrome.png" style="width: 64px" />
                            <img src="imagenes/imgFirefox.png" style="width: 64px" />
                            <img src="imagenes/imgSafari.png" style="width: 64px" />
                            <img src="imagenes/imgExplorer.png" style="width: 64px" />                            
                        </div>
                        
                    </div>
                </div>
            </div>


    <div class="hide" id="divUsuNoAut" runat="server">
        Usuario no autorizado.
    </div>
    <div class="hide" id="divMantenimiento" runat="server">
        <span id="txtMantenimiento" runat="server"></span>
    </div>



   

</body>

</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="js/procesando.js"></script>
 
<script>

    $(document).ready(function () {
        
        if (msgerr.toString().length > 0) {
            alertNew("danger", msgerr);
            return;
        }

        var sb = false;
        if ('querySelector' in document && 'localStorage' in window && 'addEventListener' in window) {
            if (bowser.msie && bowser.version < 11) { } else sb = true;
        }
        if (!sb) $("#divNavegador").addClass("show");
        else loginUser();

        $("#btnCerrarNavegador").on("click", function () {
            window.open('', '_self', '');
            window.close();
        })

    })


    $("#divIniciando").css("display", "block");

    function loginUser() {
        $.ajax({
            url: "Default.aspx/loginUser",
            //"data": JSON.stringify(),
            type: "POST",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 20000,
            success: function (result) {

                $("#imgIniciando").css("display", "none");


                if (result.d[0] == "0") {
                    
                    if (getParameterByName("ME") == "true") {
                        location.href = strServer + "Capa_Presentacion/MiEquipo/Confirmarlo/Default.aspx";
                        return;
                    } 
                    if (getParameterByName("IT") == "true") {
                        location.href = strServer + "Capa_Presentacion/MiEquipo/GestIncorporaciones/Default.aspx";
                        return;
                    } 
                    if (getParameterByName("FEVADO") == "true")
                    {
                        location.href = strServer + "Capa_Presentacion/Evaluacion/MisEvaluaciones/Default.aspx?FEVADO=1";
                        return;
                    } 
                    if (getParameterByName("APROL") == "true") {
                        location.href = strServer +"Capa_Presentacion/MiEquipo/GestionarSolicitudCambioRol/Default.aspx";
                        return;
                    } 
                    if (getParameterByName("GESENT") == "true") {
                        location.href = strServer + "Capa_Presentacion/MiEquipo/GestIncorporaciones/Default.aspx";
                        return;
                    } 
                    if (getParameterByName("GESMED") == "true") {
                        location.href = strServer + "Capa_Presentacion/Administracion/Mantenimientos/GestionCambioResponsable/Default.aspxx";
                        return;
                    } 
                    if (getParameterByName("GESSAL") == "true") {                        
                        location.href = strServer + "Capa_Presentacion/MiEquipo/TramitarSalidas/Default.aspx";
                        return;
                    } 
                    
                    location.href = strServer + "capa_presentacion/home/Home.aspx";
                                        
                }
                if (result.d[0] == "1") {
                    //Aplicación no accesible
                    $("#divMantenimiento").removeClass("hide").addClass("bsAlert col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-info cajaTexto pad10");
                    $("#txtMantenimiento").text(result.d[1]);
                }

                if (result.d[0] == "2") {
                    //Usuario no autorizado
                    $("#divUsuNoAut").removeClass("hide").addClass("bsAlert col-xs-10 col-xs-offset-1 col-md-4 col-md-offset-4 alert alert-info cajaTexto pad10");                    
                }

            },
            error: function (ex, status) {
                alertNew("danger", "Error al loguear al usuario");
            }
        });
    }

</script>


<script type="text/javascript">
    function getParameterByName(name) 
    {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    }
   
</script>             

