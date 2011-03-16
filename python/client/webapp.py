# -*- coding: utf-8 -*-
#!/usr/bin/python

import web
import orminfo as DB
import os

urls =  [
    '/favicon.ico','fav',#图标
    '/s/(.+)','static',#静态文件
        ]
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
#以上是静态文件的处理,现在开始有逻辑页面
urls.extend(['/index.html', 'index',])#首页
urls.extend(['/', 'index',])#首页
class index:
    def GET(self):
        return u'Hello, world!刘兴'
        
urls.extend(['/output','output'])
class output:
    def GET(self):
        return 'output'
        
        
app = web.application(urls, locals())
if __name__ == "__main__":
    print 'running webapp'
    app.run()
