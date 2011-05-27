var att = require('./template.js');

//console.dir(att);
var ctx = {
    name: 'lxing',
    age: 133,
    arrc: [{
        f1: 'f1',
        f2: 2
    }, {
        f1: 'fa',
        f2: 4
    }],
    arrs:['q','w','e','r','a','s','d']
};

att.render('./putsvar.txt', ctx, function (result) {
    var sys = require('sys');
    var colors = require('colors');
    sys.puts('-------------------------------'.green);
    console.log(result);
});