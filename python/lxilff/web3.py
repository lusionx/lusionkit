# -*- coding: utf-8 -*-
# /user/python


import os
from google.appengine.ext.webapp import template
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util

TEMPLATEDIR = 'template'
DEBUG = True

def render(path,vals=None):
    path = os.path.join(os.path.dirname(__file__), TEMPLATEDIR, path)
    return template.render(path, vals)
    
def run(maps):
    application = webapp.WSGIApplication(maps, debug=DEBUG)
    util.run_wsgi_app(application)
    
