$(function () {
    $('a.op-add').click(function(){
        $('#form').dialog("option", "title", $('#form legend').text() );
        $('#form').dialog('open');
    });
    $('#lastModify').datepicker();
    $('#form').dialog({
        width: 600,
        autoOpen: false,
        buttons: {
            "保存": function () {
                var data = $(this).gform();
                data.id = 0;
                $.post(basePath+'/update', data, function(obj){
                    alert(obj);
                });
                $(this).dialog("close");
            }
        }
    });
});
