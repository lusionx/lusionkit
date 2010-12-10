

import os
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext.webapp import template


class home(webapp.RequestHandler):
    def get(self):
        template_values = {             
            'url': 'url',
            'url_linktext': 'url_linktext',
            }
        path = os.path.join(os.path.dirname(__file__), 'view/home.html')
        self.response.out.write(template.render(path, template_values))


def main():
    application = webapp.WSGIApplication([('/', home)],
                                         debug=True)
    util.run_wsgi_app(application)


if __name__ == '__main__':
    main()
