

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util


acts = []


class hello(webapp.RequestHandler):
    def get(self):
        self.response.out.write('Hello gea learn')

acts.append((r'/learn/hello', hello))


class test(webapp.RequestHandler):
    def get(self):
        self.response.out.write('ctr=learn action=test')

acts.append((r'/learn/test', test))


class par(webapp.RequestHandler):
    def get(self,p1=1,p2=2):
        self.response.out.write('ctr=learn action=par p1='+p1+' p2='+p2)

acts.append((r'/learn/par/(.*)/(.*)', par))


class default(webapp.RequestHandler):
    def get(self):
        self.response.out.write('没有匹配的类(act)')

acts.append((r'/learn.*', default))


def main():
    application = webapp.WSGIApplication(acts, debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
