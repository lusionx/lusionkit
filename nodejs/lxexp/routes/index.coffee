
###
 * GET home page.
###

module.exports = (app)->
    app.get '/', (req,res)->
        res.render 'index', { title: '主页' }

    app.get '/reg', (req,res)->
        res.render 'index', { title: '注册' }

    #把其他的路由包含进来
    (require './user') app
    

