

###
###

module.exports = (app) ->
    app.get '/user', (req, res) ->
        db = new req.ose()

        doc = new db.app_user({email:'lrand' + Math.random(), pwd:'asdf'})
        doc.show()
        #doc.save (err, doc) ->
        db.app_user.find {}, null, {take: 10}, (err, docs) ->
            msg = (e.name for e in docs)

            res.render 'user', { title: '用户', msg: msg.length + ':' + msg.join('>,<') }
            db.close()


