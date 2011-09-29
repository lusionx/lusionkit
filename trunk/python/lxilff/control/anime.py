# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import web3
from model import Anime

url = {}

url['/anime'] = 'App'
url['/anime/(\d+)'] = 'App'
class App(webapp.RequestHandler):
    def get(self, skip=0):
        vals = {'basePath':'/anime'}
        htm = web3.render('anime.list',vals)
        self.response.out.write(htm)
        
url['/anime/add'] = 'Add'
url['/anime/add/(\d+)'] = 'Add'
class Add(webapp.RequestHandler):
    def get(self, id=0):
        self.response.out.write(1)
    def post(self, id=0):
        a = Anime()
        a.fill(self.request)
        self.response.out.write(a.name)


if __name__ == '__main__':
    web3.run(url, globals())
