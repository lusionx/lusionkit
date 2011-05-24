
/**
 * Module dependencies.
 */

var express = require('express');

var app = express.createServer();

app.get('/', function(req, res){
  res.send('Hello World');
});

app.listen(8202);

console.log('http://localhsot:8202')
