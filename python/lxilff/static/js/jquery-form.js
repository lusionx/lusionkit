
//收集 设置 表单
(function ($) {
    $.fn.extend({
        gform: function () {
            var vals = [];
            $(':text[name],:hidden[name],:password[name],select[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                vals.push({ key: this.name, val: this.value });
            });
            $(':checked[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                vals.push({ key: this.name, val: this.value });
            });

            $('textarea[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                vals.push({ key: this.name, val: this.value });
            });


            var vals2 = {};
            $.each(vals, function (i, e) {
                if (vals2[e.key] == undefined) {
                    vals2[e.key] = [];
                }
                vals2[e.key].push(e.val);
            });
            for (var n in vals2) {
                if (vals2[n].length == 1) {
                    vals2[n] = vals2[n][0];
                }
            }
            return vals2;
        },
        sform: function (data) {
            $(':text[name],:hidden[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                if (data[this.name] != undefined) {
                    $(this).val(data[this.name]);
                }
            });
            $(':radio[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                if (data[this.name] != undefined && $(this).val() == data[this.name]) {
                    $(this).attr('checked', 'checked');
                }
            });

            $('textarea[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                if (data[this.name] != undefined) {
                    $(this).val(data[this.name]);
                }
            });
        }
    });
})(jQuery);
