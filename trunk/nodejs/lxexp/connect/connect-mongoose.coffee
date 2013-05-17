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

    open: () ->
        if not @isOpen
            @cnstr = require('../settings').db.build()
            @isOpen = true
            ose.connect @cnstr

    app_user: require('../model/app_user')(ose)

    close: ->
        ose.disconnect()


module.exports = (opt) ->
    (req, res, next) ->
        req.ose = new myose()
        next()