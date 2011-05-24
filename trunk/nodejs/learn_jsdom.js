// Run some jQuery on a html fragment
var jsdom = require('jsdom');

jsdom.env('<p><a class="the-link" href="http://jsdom.org>JSDOM\'s Homepage</a></p>', [
    './jquery.js'
], function(errors, window) {
    var $ = window.jquery;
    console.log("contents of a.the-link:", $("a.the-link").text());
});