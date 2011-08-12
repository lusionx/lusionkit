# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import web3
from model import anime

url = {}

url['/anime/(\d+)'] = 'MainHandler'
url['/anime'] = 'MainHandler'
class MainHandler(webapp.RequestHandler):
    def get(self, skip=0):
        htm = web3.render('anime/list')
        self.response.out.write(htm)
        
url['/anime/add'] = 'Add'
class Add(webapp.RequestHandler):
    def get(self):
        htm = web3.render('anime/one')
        self.response.out.write(htm)


def main():
    web3.run(url, globals())

if __name__ == '__main__':
    main()
