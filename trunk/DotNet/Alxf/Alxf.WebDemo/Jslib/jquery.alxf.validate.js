/// <reference path="jquery-1.3.2.min.js" />
/*
配合代码生成工具使用的验证插件alxf.validate = alxv
*/

//(function ($) {
//    $.fn.extend({
//        test: function () {
//            var mytestfun = function () {
//                alert('test');
//            };
//            return this.each(mytestfun);
//        }
//    });
//})(jQuery);

(function ($) {
    $.fn.extend({
        alxv2: function (rule) {

            var options = {
                success: 'alxv-success',
                failed: 'alxv-failed',
                msg: 'alxv-msg',
                json: 'alxjs'
            };
            var validate = function () {               
                var jd = $('#' + rule.vid);
                jd.next('.' + options.msg).remove();
                if (rule.isRequired || jd.val() != '') {
                    if (rule.rule(jd)) {//验证通过
                        jd.removeClass(options.failed).addClass(options.success);
                    }
                    else {
                        jd.removeClass(options.success).addClass(options.failed);
                        var msg = $('<span/>').addClass(options.msg).text(rule.msg);
                        jd.after(msg);
                    }
                } else {
                    $(this).val(rule.Default);
                }
            };
            return this.blur(validate);
        }
    });
})(jQuery);

//供调用的验证规则没必要news此类,
var alxvRule = function () {
    this.defaultVal = '';
    this.rule = function () { return false };
    this.errorMsg = '';
    this.vid = '';
    this.isReq = true;
};

(function () {
//初始化验证规则集合
    if (!window.alxVr) {
        window.alxVr = [];
    }
})();  //自定义客户端验证的集合

$(function () {

    //把简单验证绑定
    $('input[alxjs]').alxv();
    //绑定多条件验证    
    $.each(alxVr, function () {
        $('#' + this.id).alxv2(this);
    });

    $('form').submit(function () {

        $('input[alxjs]').blur(); //触发一遍 简单验证       
        $.each(alxVr, function () {//触发一遍多条件验证
            $('#' + this.id).blur();
        });
        if ($('.alxv-failed').size() == 0) {
            return true;
        } else {
            return false;
        }
    });
});