window.addEventListener('WebComponentsReady', function (e) {
    var Dal = accionPT.Dal;
    var View = accionPT.View(document);
    $('#tablaTareas').stacktable({ headIndex: 1 });
    $('#tablaDocu').stacktable();
});