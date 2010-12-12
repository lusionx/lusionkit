# -*- coding: utf-8 -*-

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext.webapp import template
import kit
from models import Proxy

def getform(entity,request):
    entity.ip = request.get('ip')
    entity.port = int(request.get('port'))
    entity.enbale = request.get('enbale')
    entity.area = request.get('area')
    entity.source = request.get('source')

view_data = {
                'title':'',
                'entities':None,
                'entity':None,
                'atc':''
            }

class list(webapp.RequestHandler):
    def get(self):
        pass
kit.add_act(r'/proxy/list',list)

class edit(webapp.RequestHandler):
    def get(self,id=0):
        pass
    def post(self,id=0):
        pass
kit.add_act(r'/proxy/edit/(\d+)',edit)

class new(webapp.RequestHandler):
    def get(self):
        pass
    def post(self):
        pass
kit.add_act(r'/proxy/edit/(\d+)',edit)

kit.add_act(r'/proxy.*',list)

def main():
    application = kit.get_wsgi_app(webapp)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()

