Ext.sys.midCenter = {};
Ext.sys.midCenter.create = function () {
    var bar = Ext.create('Ext.tab.Panel', {
        region: 'center',
        // TabPanel itself has no title
        activeTab: 0,
        // First tab active by default
        plain: true,
        items: [{
            title: 'Default Tab',
            //closable: true,
            html: 'The first tab\'s content. Others may be added dynamically'
        }],
        id: 's-tabs'
    });
    return bar;
};

Ext.sys.midCenter.get = function () {
    var tabs = Ext.getCmp('s-tabs');
    //tabs
    return tabs;
};

Ext.sys.midCenter.getActiveTab = function () {
    var tabs = Ext.getCmp('s-tabs');
    return tabs.getActiveTab();
};