try{
var st = require('fs').statSync('./control/homes.js');
console.dir(st);
} catch (e){
console.dir(e);
}
var st = require('fs').lstatSync('./a.txt');
console.dir(st);

var st = require('fs').lstatSync('./control');
console.dir(st);

