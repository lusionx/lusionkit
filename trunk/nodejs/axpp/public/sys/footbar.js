Ext.sys.footBar = {};
Ext.sys.footBar.create = function () {
    var bar = Ext.create('Ext.Panel', {
        region: 'south',
        title: 'South Panel',
        collapsible: true,
        html: 'Information goes here',
        split: true,
        height: 100,
        minHeight: 100,
        margins: '0 1 1 1'
    });
    return bar;
};