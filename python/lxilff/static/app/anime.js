$(function () {
    $('a.op-add').click(function(){
        $('#form').dialog("option", "title", $('#form legend').text() );
        $('#form form')[0].reset();
        $('#form').dialog('open');
        $('#lastModify').click(function(){
            WdatePicker();
        });
    });
    $('#form').dialog({
        width: 600,
        autoOpen: false,
        buttons: {
            "保存": function () {
                var data = $(this).gform();
                data.id = 0;
                $.post(basePath+'/add', data, function(obj){
                    alert(obj);
                });
                $(this).dialog("close");
            }
        }
    });
});
