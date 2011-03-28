# -*- coding: utf-8 -*-
#!/usr/bin/python

import web, os, sys
from models import *
import json
constr = 'sqlite:///'+os.path.dirname(__file__)+'/info.sqlite3'
#这里配置的url映射是大小写敏感的 ;所以最好 类名,路径全是小写
urls = {}
urls['/favicon.ico'] ='fav'#图标
class fav:
    def GET(self):
        root = os.path.dirname(__file__)
        path = os.path.join(root,'static/favicon.ico')
        return open(path,'rb').read()

urls['/s/(\w+)/(.+)'] = 'static'#静态文件 图片,css,js,html
class static:
    def GET(self,dire,fname):
        root = os.path.dirname(__file__)
        path = os.path.join(root,'static',dire,fname)
        ext = os.path.splitext(path)[1].lower()
        exts = ['.jpg','.png','.gif','.ico']
        htm = ['.html','.htm']
        if ext in htm:
            web.header('Content-Type', 'text/html')
        if ext in exts:
            f = open(path,'rb')
        else:
            f = open(path,'r')
        return f.read()
            
#以上是静态文件的处理,以下开始有逻辑页面
urls['/'] = 'index'#首页
class index:
    def GET(self):
        raise web.seeother('/s/v/index.html')

urls['/select/(.+)'] = 'select'#对象输出
class select:
    def GET(self,modelname):
        ss = Context(constr).session
        l = ss.query(User)
        a = dict(
                count=l.count(),
                entities = [a.tdict() for a in l],
                )
        ss.close()
        return json.dumps(a)


import dataSvc
#urls['/blog'] = select.app #子程序
        

        
dic = urls
urls = []
for k,v in dic.items():
     urls.extend([k,v])
app = web.application(urls, locals())

if __name__ == "__main__":
    #web.config.debug = False
    #print app.request('/m').data
    app.run()
