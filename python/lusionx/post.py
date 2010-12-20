#!/usr/bin/python
# -*- coding: utf-8 -*-
# File: index.py
# User: Administrator
# Date: 2010-12-15
# Time: 17:38:57 

"""what?"""
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import settings

url_map = {}

class act1(webapp.RequestHandler):
    def get(self):
        html = settings.theme_post()
        self.response.out.write(html.main())
    def post(self):
        pass
url_map['/p/(\d+)'] = act1

def main():
    application = webapp.WSGIApplication([(k,v) for (k, v) in url_map.items()], debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
  
