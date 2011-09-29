# coding:utf-8
import web3, web
from google.appengine.ext import db

urls = (
  '/', 'index',
  '/anime', 'anime',
)

class index:
    def GET(self): 
        return web.ext.render('share/home')

class anime:
    def GET(self):
        return 'anime'

if __name__ == '__main__':
    web.ext.run(urls, globals())