var sys = require('sys');
var colors = require('colors');

sys.puts('hello'.green); // outputs green text
sys.puts('i like cake and pies'.underline.red) // outputs red underlined text
sys.puts('inverse the color'.inverse); // inverses the color
sys.puts('OMG Rainbows!'.rainbow); // rainbow (ignores spaces)