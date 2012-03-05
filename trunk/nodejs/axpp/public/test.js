Ext.require(['*']);
Ext.sys = {};

Ext.onReady(function () {
    var top = Ext.create('Ext.form.Panel', {
        frame:true,
        title: 'Multi Column, Nested Layouts and Anchoring',
        bodyStyle:'padding:5px 5px 0',
        width: 600,
        fieldDefaults: {
            labelAlign: 'top',
            msgTarget: 'side'
        },

        items: [{
            xtype: 'container',
            anchor: '100%',
            layout:'column',
            items:[{
                xtype: 'container',
                columnWidth:.5,
                layout: 'anchor',
                items: [{
                    xtype:'textfield',
                    fieldLabel: 'First Name',
                    name: 'first',
                    anchor:'96%'
                }, {
                    xtype:'textfield',
                    fieldLabel: 'Company',
                    name: 'company',
                    anchor:'96%'
                }]
            },{
                xtype: 'container',
                columnWidth:.5,
                layout: 'anchor',
                items: [{
                    xtype:'textfield',
                    fieldLabel: 'Last Name',
                    name: 'last',
                    anchor:'100%'
                },{
                    xtype:'textfield',
                    fieldLabel: 'Email',
                    name: 'email',
                    vtype:'email',
                    anchor:'100%'
                }]
            }]
        }, {
            xtype: 'htmleditor',
            name: 'bio',
            fieldLabel: 'Biography',
            height: 200,
            anchor: '100%'
        }],

        buttons: [{
            text: 'Save'
        },{
            text: 'Cancel'
        }],
        renderTo: Ext.getBody()
    });

    
});