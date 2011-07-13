

Array.prototype.filter = function(fn){
    var self = this;
    var arr = [];
    for(var i=0; i<self.length; i++){
        if (fn(self[i])){
            arr.push(self[i]);
        }
    }
    return arr;
};

module.exports.isFunction = function( obj ) {
    return toString.call(obj) === "[object Function]";
};

module.exports.isArray = function( obj ) {
    return toString.call(obj) === "[object Array]";
};


//Object.prototype.
//console.log(Array

