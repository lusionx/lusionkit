Ext.sys.midLeft = {};
Ext.sys.midLeft.create = function () {
    var bar = Ext.create('Ext.Panel', {
        region: 'west',
        layout: 'accordion',//特殊说明, 表示一个可展开的东西
        //collapsible: true,
        //title: '导航 west',
        split: true,
        width: 150,
        margins: '0 0 0 1',
        items: [{
            title: 'accordion1',
            html: '<a>11111</a>'
        }, {
            title: 'accordion2',
            html: '<a>2222</a>'
        }]
    });
    return bar;
};