
//No usamos el evento ready del documento ya que en él todavia no están registrados los web components
//y no podemos capturar sus elementos del DOM en la vista. En su lugar usamos el evento WebComponentsReady de la ventana
//window.addEventListener('WebComponentsReady', function (e) {
//Volvemos a "$(document).ready(function () { " ya que vamos a prescindir de los webcomponent

var oUltImputac;

$(document).ready(function () {

    if (typeof IB.vars.error !== "undefined") {
        IB.bserror.mostrarErrorAplicacion("Error de aplicación", "Se ha producido un error en la carga de la pantalla<br/><br/>" + IB.vars.error);
        return;
    }

    //alert("Reconectar: " + IB.vars.nReconectar);

    var oUMC = new Date(IB.vars.UMC_IAP.substring(0, 4), eval(IB.vars.UMC_IAP.substring(4, 6) - 1), 1) ; //.add("mo", 1).add("d", -1);
    var aUltimoDia;
    oUltImputac = new Date(1900,0,1);   
 
    if (IB.vars.FechaUltimaImputacion != "") {
        aUltimoDia = IB.vars.FechaUltimaImputacion.split("/");
        oUltImputac = new Date(aUltimoDia[2], eval(aUltimoDia[1] - 1), aUltimoDia[0]);
    }

    if (oUltImputac < oUMC) oUltImputac = oUMC;


    var Dal = calendario.Dal;
    var View = calendario.View(document);

    //Se inicializa el control calendario (monthly.js)
    $('#cal').monthly({
       
    });    
    
    //Se inicializa el control calendario anual (bic_calendar.js)
    var monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
    var dayNames = ["L", "M", "X", "J", "V", "S", "D"];
    var events = [
        {
            date: "",
            title: '',
            link: '',
            color: '',
            content: '',
            class: '',
        }
    ];

    $('#calendari_lateral1').bic_calendar({
        //list of events in array
        events: events,
        //enable select
        enableSelect: true,
        //enable multi-select
        multiSelect: true,
        //set day names
        dayNames: dayNames,
        //set month names
        monthNames: monthNames,
        //show dayNames
        showDays: true,
    });
   
    //Se inicializa el control BootSideMenu (BootSideMenu.js)
    $('#slideImp').BootSideMenu({ side: "right" });
    if ($('#indicator').is(':visible')) {
        $("#L").html('L');
        $("#M").html('M');
        $("#X").html('X');
        $("#J").html('J');
        $("#V").html('V');
        $("#S").html('S');
        $("#D").html('D');
        $('#slideImp').css('display', 'block');
        $('.infoTareasMes').css('display', 'none');

    } else {
        $("#L").html('Lunes');
        $("#M").html('Martes');
        $("#X").html('Miércoles');
        $("#J").html('Jueves');
        $("#V").html('Viernes');
        $("#S").html('Sábado');
        $("#D").html('Domingo');
        $('#slideImp').css('display', 'none');
        $('.infoTareasMes').css('display', 'block');
    }
    
});



