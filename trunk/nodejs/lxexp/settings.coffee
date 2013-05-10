
###
mongodb://[username:password@]host1[:port1][,host2[:port2],...[,hostN[:portN]]][/[database][?options]]
###

options = 
  db: 
    host:'127.0.0.1'
    port:'27017'
    name:'test'
    user:'' # works when mongod auth=Y
    pwd:'pwd2'
    build:()->
      auth = if @user then @user + ':' + @pwd + '@' else ''
      #auth = @user+':'+@pwd+'@' if @user?
      'mongodb://' + auth + @host + ':' + @port + '/' + @name
  port: 3000

  locals:#  provided to templates
    title: 'lxing express app'

#console.log options.db.build()

module.exports = options

        

