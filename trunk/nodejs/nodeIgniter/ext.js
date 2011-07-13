

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

//Object.prototype.
//console.log(Array

