$(document).ready(function () {
    try {
        comprobarerrores(msgerr);

        //Inicializamos tooltip
        $("a[data-toggle=tooltip]").tooltip();
        $('[data-toggle="popover"]').popover({
            trigger: 'hover',
            html: true,
        });

        //Selección de roles
        $('body').on('click', '#divRoles .list-group .list-group-item', function (e) {
            lista = $(this).parent().children();
            if (e.shiftKey && lista.filter('.active').length > 0) {
                first = lista.filter('.active:first').index();//Primer seleccionado
                last = lista.filter('.active:last').index();//Último seleccionado
                $('#divRoles .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                if ($(this).index() > first)
                    lista.slice(first, $(this).index() + 1).addClass('active');
                else
                    lista.slice($(this).index(), last + 1).addClass('active');
            }
            else if (e.ctrlKey) {
                $(this).toggleClass('active');
            } else {
                //$('#divRoles .list-group .list-group-item').removeClass('active');
                //$(this).addClass('active');                 
                $(this).toggleClass('active');
            }
            
        });

        //Click en los botones
        $('.list-arrows button').on('click', function () {
            var $button = $(this), actives = '';
            if ($button.hasClass('move-left')) {
                actives = $('.list-right ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('.list-left ul');
                    ordenar($('.list-left ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún rol");
                }
            } else if ($button.hasClass('move-right')) {
                actives = $('.list-left ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('.list-right ul');
                    ordenar($('.list-right ul li'));
                    
                } else {
                    alertNew("warning", "Tienes que seleccionar algún rol");
                }
            }
            actives.remove();
            $('.list-group-item').removeClass('active');
            $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
            grabar();
        });

        //Seleccionar todos o ninguno
        $('.dual-list .selector').on('click', function () {
            var $checkBox = $(this);
            if (!$checkBox.hasClass('selected')) {
                $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
                $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
            } else {
                $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
                $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
            }
        });

        //Buscar
        $('[name="SearchDualList"]').on('keyup', function (e) {
            var code = e.keyCode || e.which;
            if (code == '9') return;
            if (code == '27') $(this).val(null);
            var $rows = $(this).closest('.dual-list').find('.list-group li');
            var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
            $rows.show().filter(function () {
                var text = normalize($(this).text().replace(/\s+/g, ' ').toLowerCase());
                return !~text.indexOf(normalize(val));
            }).hide();
        });
    } catch (e) {
        alertNew("danger", "Ocurrió un error al iniciar la página.");
    }
});

function grabar() {
    var lisAprobadores = [];
    //Seleccionamos todas las roles la la lista de aprobadores
    $('.list-right ul li').each(function () {
        lisAprobadores.push($(this).attr("value"));
    });

    actualizarSession();
    $.ajax({
        url: "Default.aspx/updatearRoles",   // Current Page, Method
        data: JSON.stringify({ listaRoles: lisAprobadores }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        error: function (ex, status) {
            alertNew("danger", "Error al intentar actualizar los roles.");
        }

    });
}
