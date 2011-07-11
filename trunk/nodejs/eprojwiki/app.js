var express = require('express');
var model = require('./model');

var app = express.createServer();
app.configure(function(){
    app.use(express.bodyParser());
    app.use(express.methodOverride());
    app.use(express.cookieParser());
    app.use(app.router);
    app.use(express.static(__dirname + '/public'));
    app.use(express.errorHandler({ dumpExceptions: true, showStack: true }));
});



//列出所有项目
app.get('/', function(req, res){
    //res.send('所有的项目');
    model.Project.find({}, function(err,lst){
        lst.forEach(function(a){
            res.send(a.name+','+a._id+',');
        })
    });
    //res.send(ss);
});

//列出所有模块
app.get('/:proj',function(req, res){
    res.send('所有模块 of '+req.params.proj);
});
app.get('/add/proj/:name', function(req,res){
    var proj = new model.Project();
    proj.name = req.params.name;
    proj.save();
    res.send('新建项目:'+req.params.name);
});

//浏览某个模块的所有信息
app.get('/:proj/:module',function(req, res){
    res.send('this is project '+req.params.proj+'module ' +req.params.module);
});

//节点编辑
app.get('/:proj/:module/:node/edit', function(req, res){
    res.send('edit project:'+req.params.proj+' module:' +req.params.module+' node:'+req.params.node);
});
app.get('/:proj/:module/:node/edit', function(req, res){
    res.send('edit project:'+req.params.proj+' module:' +req.params.module+' node:'+req.params.node);
});

var port = 8202;
app.listen(port);
console.log('localhost:%d',port);
