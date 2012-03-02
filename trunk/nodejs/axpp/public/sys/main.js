Ext.require(['*']);
Ext.sys = {};
Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();
    Ext.create('Ext.container.Viewport', {
        layout: 'border',
        items: [
        Ext.sys.topBar.create(), Ext.sys.footBar.create(), Ext.sys.midLeft.create(), Ext.sys.midCenter.create(), Ext.sys.midRight.create(), ]
    });
});