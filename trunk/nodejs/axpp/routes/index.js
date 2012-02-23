
/*
 * GET home page.
 */

exports.home = function(req, res){
  res.render('index', { title: 'Express' })
};

exports.json = function(req, res){
  res.json({ str: 'json str', b:11, aaa:123 })
};