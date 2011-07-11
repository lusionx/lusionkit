var fs = require('fs');

var T = {
    each: function (obj, callback) {
        for (var i in obj) {
            if (callback(i, obj[i]) === false) {
                break;
            }
        }
    },
    trim: function (text) {
        return (text || "").replace(/^\s+|\s+$/g, "");
    },
    filter: {

    },
    putLiteral: function (str) {
        return str;
    },
    putVariable: function (name) {
        return name;
    }
};

//内置函数 each
var each = function (arr, callback) {
    var loop = function (i, len) {
        this.index = i;
        this.length = len;
        this.first = i == 0;
        this.last = i == len - 1;
    };
    for (var i = 0; i < arr.length; i++) {
        var lp = new loop(i,arr.length);
        callback(lp,arr[i]);
    }
};

exports.render = function (tpath, ctx, callback) {
    fs.readFile(tpath, 'utf8', function (err, data) {
        var gencode = [];
        gencode.push('(function(){');
        gencode.push('var writer="";');
        T.each(ctx, function (i, e) {
            var jstr = JSON.stringify(e);
            if(jstr.indexOf('{')>-1){
                gencode.push('var ' + i + " = eval('('+'" + jstr + "'+')');");
            }else{
                gencode.push('var ' + i + ' = ' + jstr + ';');
            }
        });
        T.each(data.split('\n'), function (i, line) {
            line = T.trim(line);
            var ati = line.indexOf('@');
            if (ati === -1) { //no output
                gencode.push("writer += T.putLiteral(" + JSON.stringify(line) + ");");
            } else {
                if (line.charAt(ati + 1) == '=') { //The line output variable
                    //output literal first
                    gencode.push("writer += T.putLiteral(" + JSON.stringify(line.substr(0, ati)) + ");");
                    //then output variable
                    gencode.push("writer += T.putVariable(" + line.substr(ati + 2) + ");");
                } else {//has logic
                    //output literal first
                    gencode.push("writer += T.putLiteral(" + JSON.stringify(line.substr(0, ati)) + ");");
                    //then logic
                    gencode.push(line.substr(ati + 1));
                }
            }
            gencode.push('writer += "\\n";');
        });
        gencode.push('return writer;})();');
        //console.log(gencode.join('\n'));
        var result = eval(gencode.join('\n'));
        callback(result);
    });
};