#!/usr/bin/node
var paths = require.paths;
var mongo = require('mongodb')

var util = require('util')
util.log(util.inspect(paths));
