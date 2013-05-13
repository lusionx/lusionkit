del model\*.js
node D:\app\nodejs\node_modules\coffee-script\bin\coffee -c model

del routes\*.js
node D:\app\nodejs\node_modules\coffee-script\bin\coffee -c routes

del *.js
node D:\app\nodejs\node_modules\coffee-script\bin\coffee -c app.coffee
node D:\app\nodejs\node_modules\coffee-script\bin\coffee -c connect-mongoose.coffee
node D:\app\nodejs\node_modules\coffee-script\bin\coffee -c connect-mongodb.coffee
node D:\app\nodejs\node_modules\coffee-script\bin\coffee -c settings.coffee
