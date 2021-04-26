<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="tracelog_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="font-family:Verdana; font-size:10px">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>


    <h2 id="titulo"></h2>
    <ul id="lista"></ul>

    <script src="jquery-2.2.0.min.js"></script>

    <script>

        //**** CONFIG *******
        var app = "";
        var hostName = "";
        var proxy;

        var testInterval = 30000;
        var numRowsToSave = 50;
        //**** CONFIG *******

        var arrlog = []
        var i;
        var numTest = 0;
        var $lista;

        $(document).ready(function(){
            console.log("document.ready");

            app = getParameterByName("app");
            hostName = getParameterByName("hostName");
            proxy = getParameterByName("proxy");

            if (!app || app.length == 0 || !hostName || hostName.length == 0 || proxy == null || proxy.length == 0) {
                alert("parametros incorrectos: ?app=xxx&hostName=xxxx&proxy=xxx");
                return;
            }

            $lista = $("#lista");
            $("#titulo").html("Aplicación: " + app + "; hostName: " + hostName + "; Proxy: " + proxy);
            i = setInterval(ajaxlog, testInterval)
            ajaxlog();
        });

        function ajaxlog() {

            $.ajax({
                url: "default.aspx/ejecuta",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 60000,
                start_time: new Date().getTime(),
                complete: function (data) {
                    var o = JSON.parse(data.responseJSON.d);
                    o.totalTime = new Date().getTime() - this.start_time;
                    o.netTime = o.totalTime - o.openConnection - o.executeSP;

                    arrlog.push(o);
                    var s = JSON.stringify(o)
                    $lista.append("<li>" + s + "</li>");
                    console.log(s);

                    numTest++;
                    if (numTest >= numRowsToSave) { grabalog(); }

                },
                error: function (ex, status) {
                    if (status == "timeout") {
                        msg = "Error: timeout";
                    }
                    else {
                        var msg = "Error: ";
                        if (typeof ex.responseText !== "undefined") {
                            try {
                                responseText = $.parseJSON(ex.responseText)

                                if (typeof responseText.Message !== "undefined") {
                                    msg += responseText.Message;
                                    if (typeof responseText.ExceptionMessage !== "undefined")
                                        msg += ". " + responseText.ExceptionMessage;
                                    if (typeof responseText.MessageDetail !== "undefined")
                                        msg += ". " + responseText.MessageDetail
                                }
                            }
                            catch (err) { }
                        }
                        var o = {};
                        o.openConnection = 0;
                        o.executeSP = 0;
                        o.totalTime = 0;
                        o.netTime = 0;
                        o.errorText = msg;

                        console.log(msg);
                    }
                    numTest++;
                    if (numTest >= numRowsToSave) { grabalog(); }
                }
            });
        }

        function grabalog() {

            if (i) clearInterval(i);

            var payload = JSON.stringify({
                app: app,
                hostName: hostName,
                proxy: proxy,
                lst: arrlog
            });

            $.ajax({
                url: "default.aspx/graba",
                data: payload,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 10000,

                complete: function (data) {
                    $lista.html("");
                    arrlog = [];
                    numTest = 0;
                    i = setInterval(ajaxlog, testInterval)
                    console.log("grabación OK");

                },
                error: function (ex, status) {
                    numTest = 0;
                    i = setInterval(ajaxlog, testInterval)
                    console.log("grabación ERROR");
                }
            });

        }

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }
    </script>
</body>
</html>
