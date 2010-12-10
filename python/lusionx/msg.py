

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext.webapp import template
import kit

acts = []

class list(webapp.RequestHandler):
    def get(self):
        vals = {
            'url': 'url',
            'url_linktext': 'url_linktext'}
        str = kit.renderview(vals,'form','msg')
        self.response.out.write(str)

acts.append((r'/msg/list',list))


class new(webapp.RequestHandler):
    def get(self):
        vals = {
            'title':'new msg',
            'body':''}
        str = kit.renderview(vals,'form','msg')
        self.response.out.write(str)

acts.append((r'/msg/new',new))

def main():
    application = webapp.WSGIApplication(acts, debug=True)
    util.run_wsgi_app(application)


if __name__ == '__main__':
    main()
