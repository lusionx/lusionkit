# -*- coding: utf-8 -*-
#!/usr/bin/python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import settings

url_map = {}

class lst(webapp.RequestHandler):
    def get(self):
        html = u'文章管理'
        self.response.out.write(html)
    def post(self):
        pass
url_map['/admin/post/lst'] = lst

def main():
    application = webapp.WSGIApplication([(k,v) for (k, v) in url_map.items()], debug=settings.debug)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
