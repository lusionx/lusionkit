module.exports.index = function(){
};

module.exports.act = function(p1){
   return JSON.stringify(this.query) + JSON.stringify(this.form);
}

module.exports.err = function(p1){
   throw 'sss';
}