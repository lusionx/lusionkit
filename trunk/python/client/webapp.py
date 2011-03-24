# -*- coding: utf-8 -*-
#!/usr/bin/python

import web, os, sys
from models import *
import json
constr = 'sqlite:///'+os.path.dirname(__file__)+'/info.sqlite3'
#这里配置的url映射是大小写敏感的 ;所以最好 类名,路径全是小写
urls = []
urls.extend(['/favicon.ico','fav'])#图标
class fav:
    def GET(self):
        root = os.path.dirname(__file__)
        path = os.path.join(root,'static/favicon.ico')
        return open(path,'rb').read()

urls.extend(['/s/(\w+)/(.+)','static'])#静态文件 图片,css,js,html
class static:
    def GET(self,dire,fname):
        root = os.path.dirname(__file__)
        path = os.path.join(root,'static',dire,fname)
        ext = os.path.splitext(path)[1].lower()
        exts = ['.jpg','.png','.gif','.ico']
        if ext in exts:
            f = open(path,'rb')
        else:
            f = open(path,'r')
        return f.read()
            
#以上是静态文件的处理,以下开始有逻辑页面
urls.extend(['/', 'index'])#首页
class index:
    def GET(self):
        return 'Hello, world!张三'

urls.extend(['/m','models'])#对象输出
class models:
    def GET(self):
        ss = Context(constr).session
        l = ss.query(User)
        a = dict(
                count=l.count(),
                entities = [a.tdict() for a in l],
                )
        ss.close()
        return json.dumps(a)
        
app = web.application(urls, locals())

if __name__ == "__main__":
    #web.config.debug = False
    app.run()
    #print app.request('/m').data
