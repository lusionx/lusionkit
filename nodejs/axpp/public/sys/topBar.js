Ext.sys.topBar = {};
Ext.sys.topBar.create = function(){
    var bar = Ext.create('Ext.panel.Panel',{
        xtype: 'box',
        id: 'header',
        region: 'north',
        html: '<h1>topBar log...</h1>',
        height: 30,
        margins: '1 1 4 1'
    });
    return bar;
};