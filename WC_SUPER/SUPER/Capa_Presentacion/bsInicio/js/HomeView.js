var IB = IB || {};
IB.SUPER = IB.SUPER || {};

//Botones de acceso rápido HOME
IB.SUPER.HomeView = (function () {

    var dom = {
        accesoHome: $("#Menu_ulPrincipal li[accesoHome]"),
        breadcrumbs: $("#Menu_SiteMap1")
    }

    var sels = {
        btnAccesoHome: "btnAccesoHome",
        attrAccesoHome : "accesoHome"
    }

    var render = function () {
        dom.accesoHome.each(function (index, element) {
            $("#" + sels.btnAccesoHome + element.getAttribute(sels.attrAccesoHome)).css("opacity", "1").css("pointer-events", "auto");
            $("#" + sels.btnAccesoHome + element.getAttribute(sels.attrAccesoHome) + " a").attr("href", element.firstChild.href);
            $("#" + sels.btnAccesoHome + element.getAttribute(sels.attrAccesoHome) + " span").css("cursor", "pointer");
        })

    }

    //Ocultamos las migas porque muestra la entrada de selección de usuario
    dom.breadcrumbs.css("display", "none");

    return {
        render: render        
    }
})();

