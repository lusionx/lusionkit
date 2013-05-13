
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
        #req.session.user = '111111'
        #console.dir req.session# = '1234'
        res.render 'reg', { title: '注册' }

    app.post '/reg', (req, res) ->
        user =
            email: req.body.EMAIL
            pwd: req.body.PWD
            valid: false
        user.pwd = require('crypto/md5').hex_md5 user.pwd
        req.db (err, db) ->
            if err
                res.render 'error500'
            else
                db.collection 'app_user', (err, coll) ->
                    coll.insert user, (err, doc) ->
                        if not err
                            res.render 'comSucc', {msg:'注册成功', next:'/'}

    #把其他的路由包含进来
    require('./user') app





