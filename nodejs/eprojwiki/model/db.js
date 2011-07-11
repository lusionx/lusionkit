var mongoose = require('mongoose');
mongoose.connect('127.0.0.1', 'test', 27017);
module.exports = mongoose;