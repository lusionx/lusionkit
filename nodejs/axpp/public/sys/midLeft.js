Ext.sys.midLeft = {};
Ext.sys.midLeft.create = function () {
    var bar = Ext.create('Ext.Panel', {
        region: 'west',
        layout: 'accordion',//����˵��, ��ʾһ����չ���Ķ���
        //collapsible: true,
        //title: '���� west',
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