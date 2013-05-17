###
oseObj =
    open: ->
        ose.connect require('../settings').db.build()

    close: ->
        ose.disconnect()

    app_user: require('../model/app_user')(ose)
###


ose = require 'mongoose'

class myose

    isOpen: null

    cnstr: null

    constructor: ->
        @isOpen = false
        @cnstr = ''

    open: (conn=null) ->
        if not @isOpen
            if conn is null and @cnstr is '' then @cnstr = require('../settings').db.build()
            ose.connect @cnstr
            @isOpen = true

    app_user: () ->
        @open()
        require('../model/app_user')(ose)

    close: ->
        ose.disconnect()


module.exports = (opt) ->
    (req, res, next) ->
        req.ose = new myose()
        next()