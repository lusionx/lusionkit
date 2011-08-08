# -*- coding: utf-8 -*-
# /user/python


import os
from google.appengine.ext.webapp import template
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from notFind import MainHandler

TEMPLATEDIR = 'template'
TEMPLATEEXT = '.html'
DEBUG = True
APPPATH = ''


def render(path,vals=None):
    if vals == None:
        vals ={'AppPath':APPPATH}
    else:
        vals['AppPath'] = APPPATH
    path = os.path.join(os.path.dirname(__file__), TEMPLATEDIR, path + TEMPLATEEXT)
    return template.render(path, vals)
    
def run(maps):
    maps.append(('.*',MainHandler))
    application = webapp.WSGIApplication(maps, debug=DEBUG)
    util.run_wsgi_app(application)
    
