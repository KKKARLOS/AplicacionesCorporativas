$(document).ready(function () { m.init(); });


var m = (function () {

    function init() {

        $("#btn1").on("click", function () {

            $.when(IB.bsconfirm.confirmCambios()).then(function () { alert() });

        })

    }

    return {
        init: init,
    }
})();