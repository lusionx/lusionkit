#!/usr/bin/python
# -*- coding: utf-8 -*-
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

class Main(db.Model):
    """主体"""
    name = db.StringProperty(verbose_name='名称', default='')
    yearMM = db.StringProperty(verbose_name='档期', default='')
    episode = db.IntegerProperty(verbose_name='已经播放', default=1)
    lastModify = db.DateTimeProperty(verbose_name='上次更新时间', required=True)
    isEnd = db.BooleanProperty(verbose_name='完结', default=False)
    castMode = db.StringProperty(verbose_name='播出方式', choices=['TV', '长篇', 'Moive', 'OVA', 'OAD',])

class Tag(db.Model):
    """标签"""
    name = db.StringProperty(verbose_name='名称', default='')
    ower = db.ReferenceProperty(Main)

