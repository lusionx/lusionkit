var options = {
  host: 'www.google.com',
  port: 80,
  path: '/index.html'
};

var http = require('http');

http.get(options, function(res) {
  console.log("Got response: " + res.statusCode);
}).on('error', function(e) {
  console.log("Got error: " + e.message);
});