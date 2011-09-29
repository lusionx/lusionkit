# -*- coding: utf-8 -*-
#!/usr/bin/python
# Filename: models.py

#property基类可用参数
#class Property(
#   verbose_name=None,
#   name=None,
#   default=None,
#   required=False,
#   validator=None,
#   choices=None)
#具体类型参考
#http://code.google.com/intl/en/appengine/docs/python/datastore/typesandpropertyclasses.html
#
#blog相关类
"""所有实体类的定义"""
from google.appengine.ext import db
from datetime import datetime

def _getdict(req):
    s = []
    for argname in req.arguments():
        s.append(argname +'="'+ req.get(argname)+'"')
    return eval('dict('+','.join(s)+')')

class Anime(db.Model):
    """主体"""
    name = db.StringProperty(verbose_name=u'名称', default='')
    yearMM = db.StringProperty(verbose_name=u'档期', default='')
    episode = db.IntegerProperty(verbose_name=u'已经播放', default=1)
    lastModify = db.DateTimeProperty(verbose_name=u'上次更新时间', default=datetime(2010,1,1))
    isEnd = db.BooleanProperty(verbose_name=u'完结', default=False)
    castMode = db.StringProperty(verbose_name=u'播出方式', default='TV')
     
    def fill(self, req):
        dic = _getdict(req)
        self.name = dic['name']
        self.yearMM = dic['yearMM']
        self.episode = int(dic['episode'])
        self.lastModify = datetime.strptime(dic['lastModify'],'%Y-%m-%d')
        self.isEnd = dic['isEnd'] == '1'
        self.castMode = dic['castMode']
        


class Tag(db.Model):
    """标签"""
    name = db.StringProperty(verbose_name='名称', default='')
    ower = db.ReferenceProperty(Anime)

