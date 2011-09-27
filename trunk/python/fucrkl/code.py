import sys 
sys.path.insert(0, 'web.zip')

import web
from google.appengine.ext import db


urls = (
  '/', 'index',
  '/note', 'note',
  '/source', 'source',
  '/crash', 'crash'
) 

class index:
    def GET(self): 
        return 'home page'

if __name__ == '__main__':
    web.application(urls, globals()).cgirun()