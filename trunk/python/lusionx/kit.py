# -*- coding: utf-8 -*-


from google.appengine.ext.webapp import template
import os, settings


def render(dic, *filepath):
    root = settings.app_folder
    path = os.path.join(root, settings.templates, *filepath)
    #return path
    return template.render(path, dic,settings.debug)

