# -*- coding: utf-8 -*-
#!/usr/bin/python

import web
import orminfo as info
import os
#这里配置的url映射是大小写敏感的
urls = []
urls.extend(['/favicon.ico','fav'])#图标
class fav:
    def GET(self):
        raise web.seeother('/s/favicon.ico')
        
urls.extend(['/s/(\w+)/(.+)','static'])#静态文件
class static:
    def GET(self,dire,fname):
        dir = os.path.dirname(__file__)
        path = os.path.join(dir,'static',dire,fname)
        ext = os.path.splitext(path)[1].lower()
        exts = ['.jpg','.png','.gif','.ico']
        try:
            f = open(path,'rb') if ext in exts else open(path,'r')
            return f.read()
        except:
            pass
        finally:
            f.close()
            
#以上是静态文件的处理,现在开始有逻辑页面
urls.extend(['/index.html', 'index'])#首页
urls.extend(['/', 'index'])#首页
class index:
    def GET(self):
        return 'Hello, world!张三'

urls.extend(['/m','Models'])#对象输出
class Models:
    def GET(self):
        ss = info.init()
        a = ss.query(info.User).first()
        return a.name
        
app = web.application(urls, locals())
if __name__ == "__main__":
    web.config.debug = True
    #app.run()
    print app.request('/m').data
