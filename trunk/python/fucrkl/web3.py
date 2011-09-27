# -*- coding: utf-8 -*-
# /user/python


import os, web
from google.appengine.ext.webapp import template

TEMPLATEDIR = 'templates'
TEMPLATEEXT = '.html'
DEBUG = True
APPPATH = ''


def _render(path,vals=None):
    if vals == None:
        vals ={'AppPath':APPPATH}
    else:
        vals['AppPath'] = APPPATH
    path = os.path.join(os.path.dirname(__file__), TEMPLATEDIR, path + TEMPLATEEXT)
    return template.render(path, vals)
    
web.render = _render
