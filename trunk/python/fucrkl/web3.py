# -*- coding: utf-8 -*-
# /user/python
"""
定义一些 自己的方法,
附加到web.py 的web.ext.*
"""
import sys 
sys.path.insert(0, 'web.zip')

import os, web, cfg
from google.appengine.ext.webapp import template

web.ext = cfg

def _render(path, vals=None):
    if vals == None:
        vals ={'AppPath':cfg.APPPATH}
    else:
        vals['AppPath'] = cfg.APPPATH
    path = os.path.join(os.path.dirname(__file__), cfg.TEMPLATEDIR, path + cfg.TEMPLATEEXT)
    return template.render(path, vals)
web.ext.render = _render

def _run(a, b):
    web.application(a, b).cgirun()
web.ext.run = _run