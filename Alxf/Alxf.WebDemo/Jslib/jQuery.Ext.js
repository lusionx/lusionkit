(function ($) {
    jQuery.fn.extend({         
        postObj:function () {
            var slter = 'input:text,input:password,input:hidden,textarea,select,input:radio,input:checkbox';
            //var slter = 'input:hidden';
            var fm = this.find(slter);                         
            var obj = new Array();
            var sa = fm.serializeArray();             
            for(var nv in sa){
                obj.push( '"' + sa[nv].name + '":"' + sa[nv].value + '"');
            }
            var jsonstr = '{' + obj.join(',') + '}';
            alert(jsonstr);
            return eval('(' + jsonstr + ')');
        }
    });
})(jQuery);