<%@ Control Language="C#" ClassName="JQConfirm" %>
<link href="<%=Session["strServer"] %>App_Themes/Corporativo/jquery-ui-1.8.17.custom.css" rel="stylesheet" />
<!--
<script src="<%=Session["strServer"] %>Javascript/jquery-1.7.1/jquery-1.7.1.min.js"></script>
<script src="<%=Session["strServer"] %>Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js"></script>
-->
<script type="text/javascript">

    //titulo: Título de la ventana modal.
    //mensaje: El mensaje que se va  a mostrar en pantalla.
    //botonAceptarTexto: Texto para el boton Aceptar, en caso de que sea vacio o nulo se mostrara el texto "Aceptar"
    //botonCancelarTexto: Texto para el boton Cancelar,  en caso de que sea vacio o nulo  se mostrara el texto "Cancelar"
    //tipoDialogo: Indica la imagen que hay que mostrar; por defecto es la imagen de tipo 'question'. Si no se indica ningun valor
    //o el valor es incorrecto se muestra la imagen de tipo question "ques".
    //ancho: Ancho en pixeles de la ventana modal. El valor por defecto es 400px.
    function jqConfirm(titulo, mensaje, botonAceptarTexto, botonCancelarTexto, tipoDialogo, ancho) {

        //Si no se indica el tipo de dialogo por defecto se asigna el dialogo question
        if (titulo == "" || !titulo) titulo = "Confirmación";
        var isClicked = false;
        //Si no se indica el tipo de dialogo por defecto se asigna el dialogo question
        if (tipoDialogo == "" || !tipoDialogo) tipoDialogo = "ques";
        //Si no se indica el ancho de definen 400px por defecto
        if (ancho == "" || ancho == 0 || !ancho) ancho = 400;
        //Si no se indica texto para los botones, se indica por defecto los valores "Aceptar" y "Cancelar"
        if (botonAceptarTexto == "" || !botonAceptarTexto) botonAceptarTexto = "Aceptar";
        if (botonCancelarTexto == "" || !botonCancelarTexto) botonCancelarTexto = "Cancelar";

        var dfd = new $.Deferred();

        //Se comprueba el tipo de dialogo y busca la imagen asociada al tipo

        switch (tipoDialogo.toLowerCase()) {
            case "ques":
                imagePath = "<%=Session["strServer"] %>Capa_Presentacion/UserControls/JQConfirm/Images/QuestionIcon.png";
                break;
            case "inf":
                imagePath = "<%=Session["strServer"] %>Capa_Presentacion/UserControls/JQConfirm/Images/InformationIcon.png";
                break;
            case "err":
                imagePath = "<%=Session["strServer"] %>Capa_Presentacion/UserControls/JQConfirm/Images/ErrorIcon.png"
                break;
            case "ok":
                imagePath = "<%=Session["strServer"] %>Capa_Presentacion/UserControls/JQConfirm/Images/OkIcon.png"
                break;
            case "war":
                imagePath = "<%=Session["strServer"] %>Capa_Presentacion/UserControls/JQConfirm/Images/WarningIcon.png"
                break;
            default:
                imagePath = "<%=Session["strServer"] %>Capa_Presentacion/UserControls/JQConfirm/Images/QuestionIcon.png";
        }

        //Se obtiene la referencia al div que va a contener el dialogo
        var jqConfirmDialog = $("#jqConfirmDialog");

        //Si no existe creamos el div y se anexa al cuerpo de la página
        if (jqConfirmDialog.length == 0) {

            jqConfirmDialog = $('<div id="jqConfirmDialog"></div>').appendTo('body');
        }

        //Se definen los botones
        var botones = [{
            text: botonAceptarTexto,
            click: function () {
                isClicked = true;
                dfd.resolve(true);          //Se resuelve la promesa como true
                $(this).dialog("close");    //Se cierra el dialogo
            }
        },
        {
            text: botonCancelarTexto,
            click: function () {
                isClicked = true;
                dfd.resolve(false);         //Se resuelve la promesa como false
                $(this).dialog("close");    //Se cierra el dialogo
            }
        }]

        //Se definen las opciones de la ventana modal
        var opt = {
            autoOpen: true,         
            modal: true,            
            resizable: false,       
            title: titulo,        
            width: ancho,           
            buttons: botones,
            close: function () { if (!isClicked) dfd.resolve(null); }
        };

        //Se crea el cuerpo del mensaje con la imagen y el texto que se ha pasado como parametro "mensaje"
        var cuerpo = '<div style="position:absolute;top:0;left:10px;width:35px;height:100%"><span style="display:inline-block;height: 100%;vertical-align: middle"></span><img src=' + imagePath + ' style="vertical-align: middle;"></div><div class="textoConfirm">' + mensaje + '</div>';

        //Se le añade el cuerpo y las opciones para a continuacion mostrarlo en pantalla
        jqConfirmDialog.html(cuerpo).dialog(opt).dialog("open");

        //Se devuelve la promesa
        return dfd.promise();
    }
</script>
