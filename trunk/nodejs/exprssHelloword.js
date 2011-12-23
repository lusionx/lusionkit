
/**
 * Module dependencies.
 */

var express = require('express');

var app = express.createServer();

var cfg = [];

app.get('/', function(req, res){
  res.send('Hello World');
});

app.listen(8202);

console.log('http://localhost:8202')
