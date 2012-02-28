var extjs = exports.extjs = function (req, res) {
        var path = 'ext-4.0.7-gpl/' + req.params[0] + '.' + req.params[1];
        var zippath = require('path').join(__dirname, '..', 'public/ext-4.0.7-gpl.zip');
        console.log(zippath);
        console.log(path);
        var file = new require('adm-zip').Zip(zippath);
        var data = file.readAsText(path);
        //console.log(data);
        res.json({ str: 'json str', b: 11, aaa: 123 });
    };

var home = exports.home = function (req, res) {
        res.render('index', {
            title: 'Express'
        })
    };

var json = exports.json = function (req, res) {
        res.json({
            str: 'json str',
            b: 11,
            aaa: 123
        })
    };


exports.init = function (app) {
    //app.get('/extjs/*.*', extjs);
    app.get('/', home);
    app.get('/json', json);
};