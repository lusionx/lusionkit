
/*
 * GET home page.
 */

exports.home = function(req, res){
  res.render('index', { title: 'Express' })
};

exports.json = function(req, res){
  res.send({ str: 'json str', aaa:123 })
};