
/**
 * Module dependencies.
 */

var express = require('express')
  , routes = require('./routes');

var app = module.exports = express.createServer();

// Configuration

app.configure(function(){
    app.set('views', __dirname + '/views');
    
    //app.set('view engine', 'jade');//使用 jqtpl, 而不是jade
    app.set('view engine', 'html');
    app.register(".html", require("jqtpl").express);
    
    //layout 通过{{layout 'xxx'}}在view中单独控制, 而不在全局
    app.set('view options', { layout: false });
    
    app.use(express.bodyParser());
    app.use(express.methodOverride());
    app.use(app.router);
    app.use(express.static(__dirname + '/public'));
});

app.configure('development', function(){
    app.use(express.errorHandler({ dumpExceptions: true, showStack: true }));
});

app.configure('production', function(){
    app.use(express.errorHandler());
});

// Routes

app.get('/', routes.home);
app.get('/json', routes.json);

app.listen(8100);
console.log('http://localhost:%s',app.address().port);
console.log("Express server listening on port %d in %s mode", app.address().port, app.settings.env);
