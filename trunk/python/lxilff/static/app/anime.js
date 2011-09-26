$(function () {
    $('#form').dialog({
        width: 600,
        buttons: {
            "保存": function () {
                $(this).dialog("close");
            }
        }
    });
});