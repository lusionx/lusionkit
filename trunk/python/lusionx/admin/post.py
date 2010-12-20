# -*- coding: utf-8 -*-
#!/usr/bin/python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import settings, sys
from admin import context
import models, kit

url_map = {}

class lst(webapp.RequestHandler):
    def get(self):
        html = u'文章管理'
        self.response.out.write(sys.path)
url_map['/admin/post/lst'] = lst

class edit(webapp.RequestHandler):
    def get(self,pid):
        pid = int(pid)
        ctx = context.default
        ctx['post'] = models.Post()
        html = kit.render(ctx,'admin','postform.html')
        self.response.out.write(html)
url_map['/admin/post/edit/(\d+)'] = edit

def main():
    application = webapp.WSGIApplication([(k,v) for (k, v) in url_map.items()], debug=settings.debug)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
