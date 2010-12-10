from google.appengine.ext import webapp
from google.appengine.ext.webapp import util

acts = []

class main(webapp.RequestHandler):
    def get(self):
        self.response.out.write('首页')

acts.append(('/',MainHandler))


def main():
    application = webapp.WSGIApplication(acts, debug=True)
    util.run_wsgi_app(application)


if __name__ == '__main__':
    main()
