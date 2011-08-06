# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import web3

url = {}

url['/anime.*'] = 'MainHandler'
class MainHandler(webapp.RequestHandler):
    def get(self):
        self.response.out.write('control.anime')

def main():
    web3.run([(a, eval(b)) for a, b in url.items()])

if __name__ == '__main__':
    main()
