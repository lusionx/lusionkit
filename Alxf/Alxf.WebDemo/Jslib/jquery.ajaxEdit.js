/// <reference name="MicrosoftAjax.js"/>
(function($) {

    $.fn.ajaxEdit = function(updatefunc) {

        var setting = {
            btnUpdate: "更新",
            btnCancel: "取消"
        };
        var cssClass = {
            error: 'ajaxEdit-error',
            input: 'ajaxEdit-input',
            update: 'ajaxEdit-update',
            cancel: 'ajaxEdit-cancel'

        };
        var dom = {
            input: '<input type="text" />',
            button: '<input type="button" />',
            hidden: '<input type="hidden" />'
        };
        var cancel = function() {
            var e = $(this);
            e.parent().text(e.next().val());
        };

        var validate = function(reg, val) {
            var r = new RegExp(reg);
            return r.test(val);
        };

        var update = function() {
            var e = $(this);
            var input = e.prev();
            var span = e.parent();
            if (validate(span.attr('reg'), input.val())) {
                updatefunc(span, input.val());
            } else {
                input.addClass(cssClass.error);
            }
        };

        var edit = function() {
            $('.' + cssClass.cancel).click();
            var orgv = $(this).text();
            var input = $(dom.input).addClass(cssClass.input).val(orgv).dblclick(function() {
                return false;
            });
            var btnupdate = $(dom.button).addClass(cssClass.update).val(setting.btnUpdate).click(update);
            var btncancel = $(dom.button).addClass(cssClass.cancel).val(setting.btnCancel).click(cancel);
            var hidden = $(dom.hidden).val(orgv);
            $(this).empty();
            $(this).append(input, btnupdate, btncancel, hidden);
        };

        return this.each(function() {
            $(this).dblclick(edit);
        });

    }

})(jQuery);
