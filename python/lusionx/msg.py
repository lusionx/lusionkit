

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext.webapp import template
import kit
from models import Person

acts = []

def getform(entity,request):
    entity.name = request.get('name')
    entity.age = int(request.get('age'))
    entity.msg = request.get('msg')

class list(webapp.RequestHandler):
    def get(self):
        q = Person.all()
        vals = {
            'title': '列表',
            'entities': q}
        htm = kit.renderview(vals,'list','msg')
        #one = q.fetch(1)[0]
        self.response.out.write(htm)

acts.append((r'/msg/list',list))


class new(webapp.RequestHandler):
    def get(self):
        one = Person()
        vals = {
            'title':'new msg',
            'entity':one,
            'act':'/msg/new'}
        htm = kit.renderview(vals,'form','msg')
        self.response.out.write(htm)

    def post(self):
        entity = Person()
        getform(entity,self.request)
        entity.put()
        self.redirect('/msg/list')

acts.append((r'/msg/new',new))


class edit(webapp.RequestHandler):
    def get(self,id=0):
        id = int(id)
        one = Person.get_by_id(id)
        vals = {
            'title':'edit one',
            'entity':one,
            'act':'/msg/edit/'+str(id)}
        htm = kit.renderview(vals,'form','msg')
        self.response.out.write(htm)

    def post(self,id=0):
        id = int(id)
        one = Person.get_by_id(id)
        getform(one,self.request)
        one.put()
        self.redirect('/msg/list')

acts.append((r'/msg/edit/(\d+)',edit))


def main():
    application = webapp.WSGIApplication(acts, debug=True)
    util.run_wsgi_app(application)


if __name__ == '__main__':
    main()
