# -*- coding: utf-8 -*-
#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import web3
from google.appengine.ext.db import djangoforms
from model import anime

class ItemForm(djangoforms.ModelForm):
    class Meta:
        model = anime.Main

url = {}
url['/dev'] = 'MainHandler'
class MainHandler(webapp.RequestHandler):
    def get(self):
        f = ItemForm()
        #self.response.out.write(dir(f))
        self.response.out.write(f.as_p().replace('<p>','<div>').replace('</p>','</div>'))


if __name__ == '__main__':
    web3.run(url,globals())
