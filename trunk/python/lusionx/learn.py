#!/usr/bin/env python
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util

class hello(webapp.RequestHandler):
    def get(self):
        self.response.out.write('Hello gea learn')

class test(webapp.RequestHandler):
    def get(self):
        self.response.out.write('ctr=learn action=test')

class par(webapp.RequestHandler):
    def get(self,p1=1,p2=2):
        self.response.out.write('ctr=learn action=par p1='+p1+' p2='+p2)

class default(webapp.RequestHandler):
    def get(self):
        self.response.out.write('没有匹配的类(act)')

def main():
    application = webapp.WSGIApplication([
        (r'/learn/hello', hello),
        (r'/learn/test', test),
        (r'/learn/par/(.*)/(.*)', par),
        (r'/learn.*', default)], debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
