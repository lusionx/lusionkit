# -*- coding: utf-8 -*-
#!/usr/bin/python

import web
import orminfo as DB

urls = (
    "/index.html", "hello",
    "/",'hello',
    '/model/(\w+)/(\d+)/(\d+)','model',
    '/output','output'
    )
app = web.application(urls, locals())

class hello:
    def GET(self):
        return 'Hello, world!'
        
class model:
    def GET(self,table,skip,take):
        return '%s skip %s take %s' % (table,skip,take)


    
class output:
    def GET(self):
        return 'output'
        
if __name__ == "__main__":
    print 'running web'
    app.run()
