# -*- coding: utf-8 -*-
#!/usr/bin/python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import kit, settings
from admin import context

url_map = {}


class act(webapp.RequestHandler):
    def get(self):
        html = kit.render(context.default,'admin','home.html')
        self.response.out.write(html)
    def post(self):
        pass
url_map['/admin'] = act

def main():
    application = webapp.WSGIApplication([(k,v) for (k, v) in url_map.items()], debug=settings.debug)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
