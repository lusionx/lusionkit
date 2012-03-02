Ext.require(['*']);

Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [ Ext.sys.get(), {
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
        }, {
            region: 'south',
            title: 'South Panel',
            collapsible: true,
            html: 'Information goes here',
            split: true,
            height: 100,
            minHeight: 100,
            margins: '0 1 1 1'
        }, {
            region: 'east',
            title: 'East Panel',
            collapsible: true,
            split: true,
            width: 150,
            hidden: false,
            margins: '0 1 0 0'
        }, {
            region: 'center',
            xtype: 'tabpanel',
            // TabPanel itself has no title
            activeTab: 0,
            // First tab active by default
            items: [{
                title: 'Default Tab',
                html: 'The first tab\'s content. Others may be added dynamically'
            }, {
                title: ' Tab 2',
                html: 'The 2 tab\'s content. Others may be added dynamically'
            }]
        }]
    });
});