var mongoose = require('./db.js')
  , Schema = mongoose.Schema;

var ModuelSchema = new Schema({
    name:{ type: String, required: true, default: 'no name module'},
});

var ProjectSchema = new Schema({
    name:{ type: String, required: true, default: 'no name project'},
    remark:{ type: String, default: ''},
    modules:[ModuelSchema]
});

mongoose.model('Project',ProjectSchema);

var Project = mongoose.model('Project');

module.exports = Project;

/*
var proj = new Project();
mongoose.connect('127.0.0.1', 'test', 27017);


proj.save(function(err){
    //console.log(err);
});

//console.dir(db.Connection);

//所有关于连接释放的好像都有问题
//mongoose.disconnect()
*/

