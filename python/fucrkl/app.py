# coding:utf-8
import web3, web
import models

urls = (
  '/', 'index',
  '/anime', 'anime',
  '/anime/(\d+)', 'anime',
  '/anime/update', 'animeUpdate',
  '/anime/update/(\d+)', 'animeUpdate'
)

class index:
    def GET(self): 
        return web.ext.render('share/home')

class anime:
    def GET(self, skip=0):#页面
        vals = {}
        vals['basePath'] = '/anime'
        return web.ext.render('anime/list', vals)
        
    def POST(self, skip=0):#列表 数据
        data = web.data()
        return type(data)
        
class animeUpdate:
    def GET2(self, id=0): #得到单条
        pass
    def GET(self, id=0): #更新
        return '1'

        


if __name__ == '__main__':
    web.ext.run(urls, globals())
