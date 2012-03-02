
Ext.sys.topBar.get = function(){
    var bar = Ext.create('Ext.Panel.',{
        region: 'north',
        html: '<h1 class="x-panel-header">Page Title</h1>',
        autoHeight: true,
        border: true,
        margins: '1 1 4 1'
    });
    return bar;
};