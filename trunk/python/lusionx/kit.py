#!/usr/bin/python
# -*- coding: utf-8 -*-

from google.appengine.ext.webapp import template
from google.appengine.ext import webapp
import os, settings

def render(dic, *filepath):
    root = settings.app_folder
    path = os.path.join(root, settings.templates, *filepath)
    #return path
    return template.render(path, dic,settings.debug)

class error(webapp.RequestHandler):
    def get(self):
        self.error(404)
    def post(self):
        self.error(404)

