# -*- coding: utf-8 -*-
#!/usr/bin/python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.api import memcache
import zipfile, os, settings

url_map = {}

content_type = {
            '.js':'application/javascript',
            '.html':'text/html',
            '.css':'text/css',
            '.png':'image/png',
            '.js':'application/javascript'}

def get_from_zip(path):
    zipname = 'ckeditor_3.5.zip'
    zippath = os.path.join(os.path.dirname(__file__),zipname)
    file = zipfile.ZipFile(zippath, 'r')
    try:
        data = file.read(path)
        return data
    finally:
        file.close()

def get_from_cache(path):
    data = memcache.get(path)
    if data is not None:
        return data
    else:
        data = get_from_zip(path)
        memcache.set(path,data,settings.cache*60)
        return data


class act1(webapp.RequestHandler):
    def get(self,path):
        data = get_from_cache(path)
        self.response.headers["Content-Type"] = content_type[os.path.splitext(path)[1]]
        self.response.out.write(data)
    def post(self):
        pass
url_map['/(.*)'] = act1

def main():
    application = webapp.WSGIApplication([(k,v) for (k, v) in url_map.items()], debug=True)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
