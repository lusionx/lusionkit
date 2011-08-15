# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import web3

url = {}

url['/'] = 'MainHandler'
class MainHandler(webapp.RequestHandler):
    def get(self):
        self.response.out.write(web3.render('share/home'))

def main():
    web3.run(url,globals())

if __name__ == '__main__':
    main()
