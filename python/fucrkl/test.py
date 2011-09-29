# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import models

url = {}

url['/test'] = 'MainHandler'
class MainHandler(webapp.RequestHandler):
    def get(self):
        a = models.User(name = 'dsadsa')
        self.response.out.write(1)

def main():
    maps = [(a, eval(b,globals())) for a, b in url.items()]
    application = webapp.WSGIApplication(maps, debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
