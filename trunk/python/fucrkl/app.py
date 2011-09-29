# coding:utf-8
import web3, web
from google.appengine.ext import db

urls = (
  '/', 'index',
  '/anime', 'anime',
  '/anime/(\d+)', 'anime',
)

class index:
    def GET(self): 
        return web.ext.render('share/home')

class anime:
    def GET(self, skip=0):
        return web.ext.render('anime/list')
    def POST(self, skip=0):
        data = web.data()
        return dir(data)


if __name__ == '__main__':
    web.ext.run(urls, globals())