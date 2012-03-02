Ext.sys.topBar = {};
Ext.sys.topBar.create = function(){
    var bar = Ext.create('Ext.Panel',{
        region: 'north',
        html: '<h1 class="x-panel-header">Page Title</h1>',
        autoHeight: true,
        border: true,
        margins: '1 1 4 1'
    });
    return bar;
};