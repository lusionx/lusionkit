# coding:utf-8


import web3, web
from google.appengine.ext import db

urls = (
  '/', 'index',
  '/note', 'note',
  '/source', 'source',
  '/crash', 'crash'
)

class index:
    def GET(self): 
        return web.ext.render('share/home')

if __name__ == '__main__':
    web.ext.run(urls, globals())