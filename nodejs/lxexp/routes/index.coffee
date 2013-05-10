
###
 * GET home page.
###

module.exports = (app) ->
    app.get '/', (req, res) ->
        req.db (error, db) ->
            if error
                console.dir error
                res.render 'error500'
            else
            #db.collectionNames (err,items)->
                db.collection 'bis_app', (err, collection) ->
                    collection.find({}).toArray (err, docs) ->
                        res.render 'index', { items: docs }
                        db.close()


    app.get '/reg', (req, res) ->
        req.session.user = '111111'

        console.dir req.session# = '1234'
        res.render 'reg', { title: '注册' }

    app.post '/reg', (req, res) ->
        console.log req.session.user
        res.render 'comSucc', {msg:'注册成功', next:'/'}

    #把其他的路由包含进来
    require('./user') app





