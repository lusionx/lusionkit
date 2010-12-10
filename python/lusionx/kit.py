
import os
from google.appengine.ext.webapp import template
from xml.dom.minidom import parseString

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

def renderview(dict, name, folder=''):
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
