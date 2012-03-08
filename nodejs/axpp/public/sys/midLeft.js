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
                //Ext.MessageBox.alert('完成', btn.text + '点击');
                var tabs = Ext.sys.midCenter.get();
                var tab = tabs.add({
                    title: btn.text,
                    html : btn.text,
                    closable: true
                });
                tabs.setActiveTab(tab);
            }
        }));
    }
    
    var btns2 = [];
    for (var i = 0; i < 10; i++) {
        btns2.push(Ext.create('Ext.button.Button', {
            text: Ext.String.format('Click2 Me {0}', i),
            enableToggle: true,
            //width:100,
            margin: '4 15 0 15',
            toggleGroup: 'btnG1',
            handler: function (btn, e) {
                Ext.MessageBox.alert('完成', btn.text + '点击');
            }
        }));
    }
    
    var dataTree = Ext.create('Ext.data.TreeStore', {
        root: {
            expanded: true
        },
        proxy: {
            type: 'ajax',
            url: '/sys/dataTree.json'
        }
    });
    
    var treePanel = Ext.create('Ext.tree.Panel', {
        //id: 'tree-panel',
        title: 'accordion0',
        split: true,
        height: 360,
        minSize: 150,
        rootVisible: false,
        autoScroll: true,
        store: dataTree
    });
    
    var bar = Ext.create('Ext.panel.Panel', {
        region: 'west',
        layout: 'accordion',
        //特殊说明, 表示一个可展开的东西
        //collapsible: true,
        //title: '导航 west',
        //titleCollapse : true,
        split: true,
        width: 160,
        margins: '0 0 0 1',
        items: [treePanel, {
            title: 'accordion1',
            items: btns
        }, {
            title: 'accordion2',
            items: btns2
        }]
    });
    return bar;
};

Ext.sys.midLeft.get = function () {

};