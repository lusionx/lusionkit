#!/usr/bin/python
# -*- coding: utf-8 -*-

"""post"""
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import settings
import models

url_map = {}

class act1(webapp.RequestHandler):
    def get(self,id):
        id = int(id)
        #html = settings.theme_post()
        html = dir(models.Post.title)
        self.response.out.write(html)
    def post(self):
        pass
url_map['/p/(\d+)'] = act1

def main():
    application = webapp.WSGIApplication([(k,v) for (k, v) in url_map.items()], debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
  
