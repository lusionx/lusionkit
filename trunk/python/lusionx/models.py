#!/usr/bin/python
# -*- coding: utf-8 -*-
# Filename: models.py

from google.appengine.ext import db

#property基类可用参数
#class Property(verbose_name=None, name=None,
#   default=None, required=False, validator=None, choices=None)
#具体类型参考
#http://code.google.com/intl/en/appengine/docs/python/datastore/typesandpropertyclasses.html

class Person(db.Model):
    name = db.StringProperty(default = '')
    age = db.IntegerProperty(default = 1)
    msg = db.TextProperty(default = '')

class Proxy(db.Model):
    ip = db.StringProperty(default = '')
    port = db.IntegerProperty(default = 80)
    enable = db.BooleanProperty(default = True)
    #上次验证时间
    verify = db.DateTimeProperty(auto_now = True)
    #此ip所属地区
    area = db.StringProperty(default = '')
    #此代理从哪个网页得来的
    source = db.LinkProperty()
