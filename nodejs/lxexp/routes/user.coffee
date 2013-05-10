

###
###

module.exports = (app) ->
    app.get '/user', (req,res) ->
        res.render 'user', { title: '用户' }
