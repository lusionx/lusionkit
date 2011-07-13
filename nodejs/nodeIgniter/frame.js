var http = require('http');
var cfg = require('./config.js');
var ext = require('./ext.js');
var fs = require('fs');
var util = require('util');
var log = function (obj) {
    console.log(JSON.stringify(obj));
};

//根据requset 初始化 上下文
var Context = function (req) {
    this.query = (function () {
        if (req.url.split('?').length == 2) {
            return require('querystring').parse(req.url.split('?')[1]);
        } else {
            return {};
        }
    })();
    this.form = (function () {})();
    this.data = (function () {})();
};

var router = function (url) { //通过url分析control, action
    var ctrl, act, ar = url.split('/').filter(function (a) {
        return a != '';
    });
    if (ar.length == 2) { //标准, /contrl/act??
        ctrl = ar[0];
        act = ar[1];
        ar = [];
    } else if (ar.length == 1) { //只有control,默认atc
        ctrl = ar[0];
        act = cfg.act;
        ar = [];
    } else if (ar.length == 0) { //首页的情况
        ctrl = cfg.ctrl;
        act = cfg.act;
        ar = [];
    } else { //需要传递参数的情况 /ctrl/act/p1/p2
        ctrl = ar[0];
        act = ar[1];
        ar = ar.slice(2); //取得之后的 参数
    }
    return {
        ctrl: ctrl,
        act: act,
        ar: ar
    };
};

var staticFile = function (url, response) { //路径是否直接指向静态文件
    try {
        if (fs.statSync('.' + url).isFile()) {
            var mime = require('mime');
            response.writeHead(200, {
                'Content-Type': mime.lookup(url)
            });
            response.end(fs.readFileSync('.' + url));
            return true;
        } else {
            throw new Error('no file');
        }
    } catch (ex) {
        //console.log('no file: .'url+','+ex.toString());
        return false;
    }
};

var execAct = function (rout, request, response) {
    try {
        fs.statSync('./control/' + rout.ctrl + '.js'); //测试是否能找到 control
        var act = require('./control/' + rout.ctrl + '.js')[rout.act]; // 是否有act
        if (!ext.isFunction(act)) {
            throw new Error('no act 400');
        }
        try {
            var context = new Context(request);
            var result = act.apply(context, rout.ar);
            response.writeHead(200, {
                'Content-Type': 'text/html'
            });
            response.end(result);
        } catch (ex) {
            response.writeHead(500);
            fs.writeFileSync(cfg.log, util.inspect(ex));
            response.end();
        }
    } catch (ex) {
        response.writeHead(400);
        response.end();
    }
};

module.exports.run = function (port, a, b) {
    http.createServer(function (request, response) {
        log('-------------------------------------------');
        var url = request.url.split('?')[0];
        //分析是否指向实际存在的文件
        if (!staticFile(url, response)) {
            //分析路由
            var rout = router(url);
            execAct(rout, request, response);
        }
    }).listen(port, a, b);
};