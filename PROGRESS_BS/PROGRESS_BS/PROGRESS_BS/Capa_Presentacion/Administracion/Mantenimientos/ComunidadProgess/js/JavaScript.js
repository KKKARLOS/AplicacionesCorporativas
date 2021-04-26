$(document).ready(function () {
    try {
        comprobarerrores(msgerr);

        //Inicializamos tooltip
        $("a[data-toggle=tooltip]").tooltip();
        $('[data-toggle="popover"]').popover({
            trigger: 'hover',
            html: true,
        });

        //Selección de profesionales
        $('body').on('click', '#divProfesionales .list-group .list-group-item', function (e) {
            lista = $(this).parent().children();
            if (e.shiftKey && lista.filter('.active').length > 0) {
                first = lista.filter('.active:first').index();//Primer seleccionado
                last = lista.filter('.active:last').index();//Último seleccionado
                $('#divProfesionales .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                if ($(this).index() > first)
                    lista.slice(first, $(this).index() + 1).addClass('active');
                else
                    lista.slice($(this).index(), last + 1).addClass('active');
            }
            else if (e.ctrlKey) {
                $(this).toggleClass('active');
            } else {
                //SÓLO DEJA HACER SELECCIÓN MÚLTIPLE CON SHIFT Y CONTROL (VERSIÓN VIEJA)
                //$('#divProfesionales .list-group .list-group-item').removeClass('active');
                //$(this).addClass('active');

                //SELECCIÓN MÚLTIPLE SIEMPRE
                $(this).toggleClass('active');
            }
        });

        //Click en los botones
        $('#divProfesionales .list-arrows button').on('click', function () {
            var $button = $(this), actives = '';
            if ($button.hasClass('move-left')) {
                actives = $('#lisProfesionales2.list-right ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#ulProfesionales1');
                    actives.clone().appendTo('#ulForzados1');
                    ordenar($('#lisProfesionales.list-left ul li'));
                    ordenar($('#lisProfesionalesForzados.list-left ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún profesional");
                }
            } else if ($button.hasClass('move-right')) {
                actives = $('#lisProfesionales.list-left ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#ulProfesionales2');
                    
                    //Borramos los elementos clonados de la lista de profesionales forzados
                    $.each(actives, function (k, v) {
                        $("#ulForzados1 li[value='" + v.value+ "']").remove();
                    })

                    ordenar($('#lisProfesionales2.list-right ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún profesional");
                }
            }
            actives.remove();
            $('.list-group-item').removeClass('active');
            $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
            grabar(1);
        });

        //Seleccionar todos o ninguno
        $('#divProfesionales .dual-list .selector').on('click', function () {
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
        $('#divProfesionales [name="SearchDualList"]').on('keyup', function (e) {
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

        /*FIN PROFESIONALES*/


        /*CR's*/
        $('body').on('click', '#divCR .list-group .list-group-item', function (e) {
            lista = $(this).parent().children();
            if (e.shiftKey && lista.filter('.active').length > 0) {
                first = lista.filter('.active:first').index();//Primer seleccionado
                last = lista.filter('.active:last').index();//Último seleccionado
                $('#divCR .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                if ($(this).index() > first)
                    lista.slice(first, $(this).index() + 1).addClass('active');
                else
                    lista.slice($(this).index(), last + 1).addClass('active');
            }
            else if (e.ctrlKey) {
                $(this).toggleClass('active');
            } else {
                //$('#divCR .list-group .list-group-item').removeClass('active');
                //$(this).addClass('active');
                $(this).toggleClass('active');
            }
        });


        //Click en los botones
        $('#divCR .list-arrows button').on('click', function () {
            var $button = $(this), actives = '';
            if ($button.hasClass('move-left')) {
                actives = $('#lisCR2.list-right ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#ulCR1');
                    ordenar($('#lisCR.list-left ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún CR");
                }
            } else if ($button.hasClass('move-right')) {
                actives = $('#lisCR.list-left ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#ulCR2');
                    ordenar($('#lisCR2.list-right ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún CR");
                }
            }
            actives.remove();
            $('.list-group-item').removeClass('active');
            $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
            grabar(2);
        });

        //Seleccionar todos o ninguno
        $('#divCR .dual-list .selector').on('click', function () {
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
        $('#divCR [name="SearchDualList"]').on('keyup', function (e) {
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
        /*FIN CR'S*/


        /*EMRPESAS*/
        $('body').on('click', '#divEmpresas .list-group .list-group-item', function (e) {
            lista = $(this).parent().children();
            if (e.shiftKey && lista.filter('.active').length > 0) {
                first = lista.filter('.active:first').index();//Primer seleccionado
                last = lista.filter('.active:last').index();//Último seleccionado
                $('#divEmpresas .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                if ($(this).index() > first)
                    lista.slice(first, $(this).index() + 1).addClass('active');
                else
                    lista.slice($(this).index(), last + 1).addClass('active');
            }
            else if (e.ctrlKey) {
                $(this).toggleClass('active');
            } else {
                //$('#divEmpresas .list-group .list-group-item').removeClass('active');
                //$(this).addClass('active');
                $(this).toggleClass('active');
            }
        });


        //Click en los botones
        $('#divEmpresas .list-arrows button').on('click', function () {
            var $button = $(this), actives = '';
            if ($button.hasClass('move-left')) {
                actives = $('#lisEmpresas2.list-right ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#UlEmpresas1');
                    ordenar($('#lisEmpresas.list-left ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar alguna empresa");
                }
            } else if ($button.hasClass('move-right')) {
                actives = $('#lisEmpresas.list-left ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#UlEmpresas2');
                    ordenar($('#lisEmpresas2.list-right ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar alguna empresa");
                }
            }
            actives.remove();
            $('.list-group-item').removeClass('active');
            $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
            grabar(3);
        });

        //Seleccionar todos o ninguno
        $('#divEmpresas .dual-list .selector').on('click', function () {
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
        $('#divEmpresas [name="SearchDualList"]').on('keyup', function (e) {
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



        //Selección de profesionales forzados
        $('body').on('click', '#divProfesionalesForzados .list-group .list-group-item', function (e) {
            lista = $(this).parent().children();
            if (e.shiftKey && lista.filter('.active').length > 0) {
                first = lista.filter('.active:first').index();//Primer seleccionado
                last = lista.filter('.active:last').index();//Último seleccionado
                $('#divProfesionalesForzados .list-group .list-group-item').removeClass('active');//Borrar de las dos listas
                if ($(this).index() > first)
                    lista.slice(first, $(this).index() + 1).addClass('active');
                else
                    lista.slice($(this).index(), last + 1).addClass('active');
            }
            else if (e.ctrlKey) {
                $(this).toggleClass('active');
            } else {
                //SÓLO DEJA HACER SELECCIÓN MÚLTIPLE CON SHIFT Y CONTROL (VERSIÓN VIEJA)
                //$('#divProfesionales .list-group .list-group-item').removeClass('active');
                //$(this).addClass('active');

                //SELECCIÓN MÚLTIPLE SIEMPRE
                $(this).toggleClass('active');
            }
        });

        //Click en los botones
        $('#divProfesionalesForzados .list-arrows button').on('click', function () {
            var $button = $(this), actives = '';
            if ($button.hasClass('move-left')) {
                actives = $('#lisProfesionalesForzados2.list-right ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#ulForzados1');
                    actives.clone().appendTo('#ulProfesionales1');
                    ordenar($('#lisProfesionalesForzados.list-left ul li'));
                    ordenar($('#lisProfesionales.list-left ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún profesional");
                }
            } else if ($button.hasClass('move-right')) {
                actives = $('#lisProfesionalesForzados.list-left ul li.active');
                if (actives.length > 0) {
                    actives.clone().appendTo('#ulForzados2');

                    //Borramos los elementos clonados de la lista de profesionales
                    $.each(actives, function (k, v) {
                        $("#ulProfesionales1 li[value='" + v.value + "']").remove();
                    })

                    ordenar($('#lisProfesionalesForzados2.list-right ul li'));
                } else {
                    alertNew("warning", "Tienes que seleccionar algún profesional");
                }
            }
            actives.remove();
            $('.list-group-item').removeClass('active');
            $('.dual-list .selector').children('i').addClass('glyphicon-unchecked');
            grabar(4);
        });

        //Seleccionar todos o ninguno
        $('#divProfesionalesForzados .dual-list .selector').on('click', function () {
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
        $('#divProfesionalesForzados [name="SearchDualList"]').on('keyup', function (e) {
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


function grabar(contenedor) {
    var lista = [];
    
    if (contenedor == 1) {
        $('#lisProfesionales2.list-right ul li').each(function () {
            lista.push($(this).attr("value"));
        });
    }
   
    else if (contenedor == 2) {
        $('#lisCR2.list-right ul li').each(function () {
            lista.push($(this).attr("value"));
        });
    }

    else if (contenedor == 3) {
        $('#lisEmpresas2.list-right ul li').each(function () {
            lista.push($(this).attr("value"));
        });
    }

    else if (contenedor == 4) {
        $('#lisProfesionalesForzados2.list-right ul li').each(function () {
            lista.push($(this).attr("value"));
        });
    }

    actualizarSession();
    $.ajax({
        url: "Default.aspx/update",   // Current Page, Method
        data: JSON.stringify({ contenedor: contenedor, lista: lista }),  // parameter map as JSON
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 20000,
        error: function (ex, status) {
            alertNew("danger", "Error al intentar actualizar.");
        }

    });
}

