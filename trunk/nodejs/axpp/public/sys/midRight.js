Ext.sys.midRight = {};
Ext.sys.midRight.create = function () {
    var bar = Ext.create('Ext.panel.Panel', {
        region: 'east',
        title: 'East Panel',
        collapsible: true,
        split: true,
        width: 150,
        hidden: false,
        margins: '0 1 0 0'
    });
    return bar;
};