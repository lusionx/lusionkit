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

class Post(db.Model):
    """文章"""
    title = db.StringProperty(verbose_name='标题', default='')
    content = db.TextProperty(verbose_name='正文', default='')
    author = db.UserProperty(auto_current_user=True)
    status = db.StringProperty(verbose_name='状态', default=u'草稿')#, choices=[u'草稿',u'发布',u'作废'])
    published = db.DateTimeProperty(auto_now_add=True)
    comment_abled = db.BooleanProperty(verbose_name='允许评论', default=True)
    modified = db.DateTimeProperty(auto_now=True)

class Comment(db.Model):
    """文章的评论"""
    name = db.StringProperty(verbose_name='称呼', default='')
    email = db.EmailProperty(verbose_name='E-mail', default='')
    content = db.StringProperty(verbose_name='内容',default='')
    father = db.SelfReferenceProperty()
    post_id = db.IntegerProperty(default=0)
    post = db.ReferenceProperty(Post)

class Tag(db.Model):
    """标签"""
    name = db.StringProperty(default='')
    times = db.IntegerProperty(default=0)

class Post_Tag(db.Model):
    """文章和标签的关联"""
    post = db.ReferenceProperty(Post)
    tag = db.ReferenceProperty(Tag)

class Category(db.Model):
    """各种 分类(link,post)"""
    name = db.StringProperty(default='')
    father = db.SelfReferenceProperty()
    kindof = db.StringProperty(choices=['Link','Post'])

class Link(db.Model):
    """"""
    text = db.StringProperty(default='')
    href = db.StringProperty(default='')
    title = db.StringProperty(default='')
    category = db.ReferenceProperty(Category)
    def html(self):
        ls = '<a href="%(href)s" title="%(title)s" >%(text)s</a>' % {'href':self.href, 'title':self.title, 'text':self.text }
        return ls

def transForm():
    pass
