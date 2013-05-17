

###
###

module.exports = (app) ->
    app.get '/user', (req, res) ->
        db = req.ose
        db.open()

        doc = new db.app_user({email:'lrand' + Math.random(), pwd:'asdf'})
        #doc.show()
        #doc.save (err, doc) ->
        db.app_user.find {}, null, {take: 10}, (err, docs) ->
            #docs = (e.toObject() for e in docs)
            #msg = (e.get('name') for e in docs)
            msg = (e.name for e in docs)
            #res.send(msg[0].name)
            res.render 'user', { title: '用户', msg: msg.length + ':' + msg.join('>,<') }
            db.close()

    app.get '/user/:id', (req, res) ->
        res.json {id:req.params.id}


