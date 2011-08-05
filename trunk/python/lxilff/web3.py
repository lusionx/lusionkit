# -*- coding: utf-8 -*-
# /user/python


import os
from google.appengine.ext.webapp import template

TEMPLATEDIR = 'template'

def render(path,vals=None):
    path = os.path.join(os.path.dirname(__file__), TEMPLATEDIR, path)
    return template.render(path, vals)
    
