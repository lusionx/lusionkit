var mongoose = require('mongoose');

var constr = 'mongodb://localhost/test';
var conn = mongoose.connect(constr);

var Log = (function(ose){
    var obj = new ose.Schema({
        title: String,
        body: String,
        date: Date
    });
    return ose.model('logs', obj)
})(mongoose);



exports.logs = {
    add: function (ss, next) {
        var m = new Log();
        ss = '13';
        m.body = ss;
        m.title = ss;
        m.save(function(err){
            if(!err){
                next();
            }
            mongoose.disconnect();
        });
    },
    del: function () {
        return 0;
    }
};
