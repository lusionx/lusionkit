Ext.require(['*']);

Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [{
            region: 'north',
            html: '<h1 class="x-panel-header">Page Title</h1>',
            autoHeight: true,
            border: true,
            margins: '1 1 4 1'
        }, {
            region: 'west',
            layout: 'accordion',
            collapsible: true,
            title: '导航 west',
            split: true,
            width: 150,
            margins: '0 0 0 1',
            items: [{
                title: 'accordion1'
            }, {
                title: 'accordion2'
            }]
            // could use a TreePanel or AccordionLayout for navigational items
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
            items: {
                title: 'Default Tab',
                html: 'The first tab\'s content. Others may be added dynamically'
            }
        }]
    });
});