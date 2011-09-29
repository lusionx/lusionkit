$(function () {
    $('#form').dialog({
        width: 600,
        buttons: {
            "保存": function () {
                var data = $(this).gform();
                $.post(window.location.href, data, function(obj){
                    alert(obj);
                });
                $(this).dialog("close");
            }
        }
    });
});