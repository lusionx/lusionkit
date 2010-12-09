/// <reference path="jquery-1.3.2-vsdoc2.js" />
$(function() {
    $('fieldset.query tbody tr:even').addClass('alrow');

});
$(function() {
    var main = $('#main');
    var chid = main.children();
    var m = $.max(chid.eq(0).height(), chid.eq(1).height());
    main.height(m);
    if (chid.eq(0).height() > chid.eq(1).height()) {
        chid.eq(1).height(chid.eq(0).height())
    }
});
$(function() {
    $('.data tbody tr:odd').addClass('alrow');
    $('.data tbody tr').hover(function() {
        $(this).addClass("hover");
    }, function() {
        $(this).removeClass("hover");
    });
});
$(function() {
    var bl = function() {
        this.blur();
    };
    $('a').focus(bl);
    $('a[href=""]').attr('href', 'javascript:');
});
$(function() {
    $('.sblock tbody tr:even').addClass('alrow');
});
$(function(){
    $('.ddlyear').change(function(){
        $.get(AppPath+"Handler/Year.ashx",{p:$(this).val()});
    }).change();
});