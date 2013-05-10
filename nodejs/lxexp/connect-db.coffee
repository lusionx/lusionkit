

mongodb = require 'mongodb'
settings = require './settings'

db = (callback) ->
    mongodb.MongoClient.connect settings.db.build(), callback

module.exports = (opt) ->
    return (req, res, next) -> 
        req.db = db
        next()