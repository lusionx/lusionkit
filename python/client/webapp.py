# -*- coding: utf-8 -*-
#!/usr/bin/python

import web
import orminfo as DB
import os

urls = (
    '/index.html', 'hello',#首页
    '/','hello',#首页
    '/favicon.ico','fav',#图标
    '/s/(.+)','static',#静态文件
    '/output','output',
    )
app = web.application(urls, locals())

class hello:
    def GET(self):
        return u'Hello, world!刘兴'
class fav:
    def GET(self):
        raise web.seeother('/s/favicon.ico')
class static:
    def GET(self,path):
        dir = os.path.dirname(__file__)
        path = os.path.join(dir,'static',path)
        byts = ['.jpg','.png','.gif','.ico']
        ext = os.path.splitext(path)[1].lower()
        try:
            if ext in byts:
                f = open(path,'rb')
            else:
                f = open(path,'r')
            return f.read()
        except:
            pass
        finally:
            f.close()
    
class output:
    def GET(self):
        return 'output'
        
if __name__ == "__main__":
    print 'running web'
    app.run()
