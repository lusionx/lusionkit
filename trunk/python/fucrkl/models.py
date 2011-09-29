# coding: utf-8
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

class User(db.Model):
    name = db.StringProperty(verbose_name=u'名称', default='')

class Anime(db.Model):
    """主体"""
    name = db.StringProperty(verbose_name=u'名称', default='')
    yearMM = db.StringProperty(verbose_name=u'档期', default='')
    episode = db.IntegerProperty(verbose_name=u'已经播放', default=1)
    lastModify = db.DateTimeProperty(verbose_name=u'上次更新时间', required=True)
    isEnd = db.BooleanProperty(verbose_name=u'完结', default=False)
    castMode = db.StringProperty(verbose_name=u'播出方式', required=True, choices=[u'TV', u'长篇', u'Moive', u'OVA', u'OAD',])


class AnimeTag(db.Model):
    """标签"""
    name = db.StringProperty(verbose_name='名称', default='')
    ower = db.ReferenceProperty(Anime)

