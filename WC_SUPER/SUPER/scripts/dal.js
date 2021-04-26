var IB = IB || {}

IB.DAL = (function() {

    var post = function (page, webmethod, payload, mapper, callback, callbackerror, timeout) {

        var $defer = $.Deferred();
        var _timeout = 10000;

        if (typeof timeout !== "undefined" && timeout != null) {
            _timeout = timeout;
        }        

        if (page == null || page == "") page = "Default.aspx";
        if (payload != null) payload = JSON.stringify(payload);

        $.ajax({
            url: page + "/" + webmethod,
            data: payload,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: _timeout,
            success: function (result) {
                var datamapped = result.d;
                if (mapper != null)
                    datamapped = result.d.map(mapper);
                if (callback != null)
                    callback(datamapped);

                $defer.resolve(datamapped);
                
            },
            error: function (ex, status) {
                if (callbackerror != null)
                    callbackerror(ex, status);
                else
                    IB.bserror.error$ajax(ex, status);

                $defer.reject();
            }
        });

        return $defer.promise();
    }

    return {
        post: post
    }

})();