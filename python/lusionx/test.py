
import os
import kit
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util

acts = []

class t1(webapp.RequestHandler):
    def get(self):
        #str = os.path.join(os.path.dirname(__file__), 'view/home.html')
        #str = os.path.dirname(__file__)
        #str = kit.viewpath
        vals={'a':'dadsds'}
        str = kit.renderview(vals,'form','msg')
        str = kit.getbody(str)
        self.response.out.write(str)

acts.append(('/test/t1.*',t1))


def main():
    application = webapp.WSGIApplication(acts, debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
