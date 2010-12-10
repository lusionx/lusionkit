

import os
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import kit


class home(webapp.RequestHandler):
    def get(self):
        vals = {
            'url': 'url',
            'url_linktext': 'url_linktext'}
        str = kit.render_view(vals,'home')
        #path = os.path.join(os.path.dirname(__file__), 'view/home.html')
        self.response.out.write(str)


def main():
    application = webapp.WSGIApplication([('/', home)], debug=True)
    util.run_wsgi_app(application)


if __name__ == '__main__':
    main()
