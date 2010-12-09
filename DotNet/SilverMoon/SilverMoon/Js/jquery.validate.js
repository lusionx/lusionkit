/// <reference name="MicrosoftAjax.js"/>
/// <reference path="jquery-1.2.6.min.js" />
(function($) {
    var dicmsg = {
        "必填项目": "Required Field",
        "要求数字": "Required Number"
    };

    $.fn.validate = function(options) {

        var check = function() {
            var target = $(this);
            target.next('.jv-msg').remove();
            var thisReg = new RegExp(target.attr('reg'));
            if (thisReg.test(target.val())) {
                target.after('<span class="jv-msg"><img accpet="y" src="/img/jv-accept.png" /></span>');
                return true;
            }
            else {
                var errormsg = '';
                if (target.attr('errmsg') != undefined) {
                    errormsg = target.attr('errmsg');
                }
                target.after('<span class="jv-msg"><img src="/img/jv-reject.png" /><span class="jv-errmsg">' + errormsg + '</span></span>');
                return false;
            }
        };

        return this.each(function() {
            $(this).blur(check);
        });

    };

    //绑定验证
    $(function() {
        var nval = $('input[reg]')
        nval.validate();
        $('form').submit(function() {
            nval.blur();
            return nval.size() == $('img[accpet]').size();
        })
        nval.eq(0).focus();
    });
})(jQuery);