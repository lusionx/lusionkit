#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util


class one(webapp.RequestHandler):
    def get(self):         
        self.response.out.write('not_found')


def main():
    application = webapp.WSGIApplication([('/.*', one)], debug=True)
    util.run_wsgi_app(application)


if __name__ == '__main__':
    main()
