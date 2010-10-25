/// <reference name="MicrosoftAjax.js"/>
/// <reference path="jquery-1.3.2-vsdoc.js"/>

$(function() {
    bindCheck();
    $('form').submit(function() {
        return $('.jev-n').size() == 0;
    });
    $(":text").blur();
})

function bindCheck() {
    $('.jev-reqf').blur(function() {
        var oo = $(this);
        Change(oo, !isEmpty(oo.val()));
    })
    //在此处增加相应函数
}

function isEmpty(str) {
    return str == null || str.length == 0;
}

function isDate(v) {
    if (isEmpty(v)) {
        return true;
    }
}

function Change(oo, validate) {
    if (validate) {
        oo.removeClass('jev-n');
        oo.addClass('jev-y');
    }
    else {
        oo.removeClass('jev-y');
        oo.addClass('jev-n');
    }
}