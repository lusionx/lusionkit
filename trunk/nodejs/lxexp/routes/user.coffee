

###
###

module.exports = (app)->
    app.get '/user', (req,res)->
        res.render 'index', { title: '用户' }