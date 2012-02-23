/*
 * GET home page.
 */

var home = exports.home = function(req, res){
    res.render('index', { title: 'Express' })
};

var json = exports.json = function(req, res){
    res.json({ str: 'json str', b:11, aaa:123 })
};


exports.init = function(app){
    app.get('/', home);
    app.get('/json', json);
};