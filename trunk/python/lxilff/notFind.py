# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import web3

url = {}

url['.*'] = 'MainHandler'
class MainHandler(webapp.RequestHandler):
    def get(self):
        self.response.out.write('没有找到对应的请求')


if __name__ == '__main__':
    web3.run(url,globals())