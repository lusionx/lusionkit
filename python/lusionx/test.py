
import kit
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util


class t1(webapp.RequestHandler):
    def get(self):
        self.response.out.write('test t1')

kit.add_act('/test/t1.*',t1)

kit.add_act('/test.*',t1)

def main():
    app = kit.get_wsgi_app(webapp)
    util.run_wsgi_app(app)

if __name__ == '__main__':
    main()
