/// <reference name="MicrosoftAjax.js"/>
/// <reference path="jquery-1.2.6.min.js" />
(function ($) {
    $.extend({
        postJSON: function (url, data, success) {
            return jQuery.ajax({
                type: "POST",
                url: url,
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: success
            });
        },
        queryString: function (query) {
            var search = window.location.search + '';
            if (search.charAt(0) != '?') {
                return null;
            }
            else {
                search = search.replace('?', '').split('&');
                for (var i = 0; i < search.length; i++) {
                    if (search[i].split('=')[0] == query) {
                        return decodeURI(search[i].split('=')[1]);
                    }
                }
                return null;
            }
        },
        toJson: function (obj) {
            try {
                //json2.js
                return JSON.stringify(obj);
            } catch (e) {
                return '';
            }
        },
        toObj: function (jsontext) {
            return JSON.parse(jsontext);
        },
        format: function (format) {
            var args = new Array();
            for (var i = 1; i < arguments.length; i++) {
                args.push(arguments[i]);
            }
            return format.replace(/\{(\d+)\}/g, function (m, i) {
                return args[i];
            });
        }
    });
})(jQuery);
