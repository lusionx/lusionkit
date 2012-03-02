Ext.sys.midCenter = {};
Ext.sys.midCenter.create = function () {
    var bar = Ext.createWidget('tabpanel', {
        region: 'center',
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
    });
    return bar;
};