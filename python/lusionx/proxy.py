#!/usr/bin/python
# -*- coding: utf-8 -*-
# Filename: proxy.py

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext.webapp import template
import kit
from models import Proxy
from google.appengine.api import urlfetch

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
        q = Proxy.all()
        view_data['title'] = '代理 列表'
        view_data['entities'] = q
        html = kit.render_view(view_data,'list','proxy')
        self.response.out.write(html)
kit.add_act(r'/proxy/list',list)

class edit(webapp.RequestHandler):
    def get(self,id=0):
        pass
    def post(self,id=0):
        pass
kit.add_act(r'/proxy/edit/(\d+)',edit)

import urllib2
class fetch(webapp.RequestHandler):
    def get(self,id=0):
        one = Proxy.get_by_id(int(id))
        url = 'http://www.geoiptool.com/'
        add = 'http://wsdwss'+one.ip+':'+str(one.port)
        proxy_support = urllib2.ProxyHandler({
            'http': add
            })
        opener = urllib2.build_opener(proxy_support, urllib2.HTTPHandler)
        urllib2.install_opener(opener)
        msg = ''
        try:
            result = urllib2.urlopen(url)
            msg = result.read()
        except urllib2.URLError, e:
            msg = '抓取出错'
        #我的ip 125.64.124.215
        self.response.out.write(msg+add)
kit.add_act(r'/proxy/fetch/(\d+)',fetch)

class new(webapp.RequestHandler):
    def get(self):
        one = Proxy()
        view_data['title'] = '添加一个代理'
        view_data['act'] = '/proxy/new'
        view_data['entity'] = one
        html = kit.render_view(view_data,'form','proxy')
        self.response.out.write(html)
    def post(self):
        one = Proxy()
        getform(one,self.request)
        one.put()
        self.redirect('/proxy/list')

kit.add_act(r'/proxy/new',new)

kit.add_act(r'/proxy.*',list)

def main():
    application = kit.get_wsgi_app(webapp)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()

