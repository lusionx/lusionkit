Ext.sys.midLeft = {};
Ext.sys.midLeft.create = function () {
    var btns = [];
    for (var i = 0; i < 10; i++) {
        btns.push(Ext.create('Ext.button.Button', {
            text: Ext.String.format('Click Me {0}', i),
            enableToggle: true,
            //width:100,
            margin: '4 15 0 15',
            toggleGroup: 'btnG1',
            handler: function (btn, e) {
                Ext.MessageBox.alert('完成', btn.text + '点击');
            }
        }));
    }

    var bar = Ext.create('Ext.panel.Panel', {
        region: 'west',
        layout: {
            type: 'accordion',
            aline: 'center'
        },
        //特殊说明, 表示一个可展开的东西
        //collapsible: true,
        //title: '导航 west',
        //titleCollapse : true,
        split: true,
        width: 150,
        margins: '0 0 0 1',
        items: [{
            title: 'accordion1',
            items: btns
        }, {
            title: 'accordion2',
            html: '<a>2222</a>'
        }]
    });
    return bar;
};

Ext.sys.midLeft.get = function () {

};