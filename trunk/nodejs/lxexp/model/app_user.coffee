
#ose = require 'mongoose'

init = (ose) ->
    schema = ose.Schema {email: String, pwd: String, valid: Boolean}

    schema.methods.show = () ->
        console.log "I'm " + @email

    ose.model 'app_user', schema, 'app_user'

module.exports = init


