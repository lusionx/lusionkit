
import os
from google.appengine.ext.webapp import template
from xml.dom.minidom import parseString


webapp_debug = True

_templateExt = '.html'
_templateFold = 'view'
_sharedFold = 'shared'

def _getviewpath(name, folder):
    root = os.path.dirname(__file__)
    if os.path.exists(os.path.join(root,_templateFold,folder,name+_templateExt)):
        return os.path.join(root,_templateFold,folder,name+_templateExt)
    elif os.path.exists(os.path.join(root,_templateFold,_sharedFold,name+_templateExt)):
        return os.path.join(root,_templateFold,_sharedFold,name+_templateExt)
    else:
        return "I can't find view"

def render_view(dict, name, folder=''):
    path = _getviewpath(name,folder)
    return template.render(path, dict,True)

def getbody(htmlstr):
    """useless
    """
    xml = parseString(htmlstr)
    body = xml.getElementsByTagName('body')[0]
    nodes = body.childNodes
    str = ''
    for node in nodes:
        str+=node.toxml()
    return str


_acts = []

def add_act(url,act_class):
    _acts.append((url,act_class))

def get_wsgi_app(webapp):
    return webapp.WSGIApplication(_acts, webapp_debug)
