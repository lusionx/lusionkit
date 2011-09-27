# coding:utf-8
import sys 
sys.path.insert(0, 'web.zip')

import web, web3
from google.appengine.ext import db

urls = (
  '/', 'index',
  '/note', 'note',
  '/source', 'source',
  '/crash', 'crash'
)



class index:
    def GET(self): 
        return web.render('share/home')

if __name__ == '__main__':
    web.application(urls, globals()).cgirun()