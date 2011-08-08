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


def main():
    web3.run([(a, eval(b)) for a, b in url.items()])

if __name__ == '__main__':
    main()
