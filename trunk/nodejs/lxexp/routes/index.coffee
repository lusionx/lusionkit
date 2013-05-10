
###
 * GET home page.
###

module.exports = (app) ->
    app.get '/', (req, res) ->
        res.app.get('mongodb') (error, db) ->
            if error
                console.dir error
                res.render 'error500'
            else
            #db.collectionNames (err,items)->
                db.collection 'bis_app', (err, collection) ->
                    collection.find({}).toArray (err, docs) ->
                        res.render 'index', { items: docs }
                        db.close()


    app.get '/reg', (req,res) ->
        res.render 'reg', { title: '注册' }

    #把其他的路由包含进来
    (require './user') app


    
     

