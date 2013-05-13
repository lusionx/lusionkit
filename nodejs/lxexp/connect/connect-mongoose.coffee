###

###

ose = require 'mongoose'

class myose

    constructor: ->
        ose.connect require('../settings').db.build()



    app_user: require('../model/app_user')(ose)

    close: ->
        ose.disconnect()



#test = () ->




module.exports = (opt) ->
    return (req, res, next) ->
        req.ose = myose
        next()