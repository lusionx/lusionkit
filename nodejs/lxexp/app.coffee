
###
 * Module dependencies.
 ###

express = require 'express'
path = require 'path'
settings = require './settings'
mongodb = require 'mongodb'


app = express()

# all environments
app.set 'port', settings.port
app.set 'views', __dirname + "/views"
app.set 'view engine', 'jade'


app.locals settings.locals

app.use express.logger('dev')
app.use require('./connect-db')()
app.use express.bodyParser()
app.use express.methodOverride()
app.use express.cookieParser settings.secret
app.use express.session { key: 'sid', cookie: { maxAge: 60000 }}
app.use require('connect-flash')()
app.use app.router
app.use express.static(path.join(__dirname, 'static'))

#development only
if 'development' is app.get 'env'
    app.use express.errorHandler()


require('./routes') app

require('http').createServer(app).listen app.get('port'), () ->
    console.log 'Express server ' + app.get('env') + ' listening on port ' + app.get('port')

