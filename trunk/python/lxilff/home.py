# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import web3

url = {}

url['/'] = 'MainHandler'
class MainHandler(webapp.RequestHandler):
    def get(self):
        self.response.out.write(web3.render('home.html'))

def main():
    application = webapp.WSGIApplication([(a, eval(b)) for a, b in url.items()], debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
