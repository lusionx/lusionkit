# -*- coding: utf-8 -*-
#!/usr/bin/python

from sqlalchemy import create_engine
from sqlalchemy.schema import Table, MetaData, Column, ForeignKey
from sqlalchemy.orm import mapper, Session, clear_mappers, relationship
from sqlalchemy.types import Integer, String
from cls import *

class Context():
    def __init__(self,constr):
        db = create_engine(constr)
        db.echo = True
        self.meta = MetaData(db)
        table = {}
        clear_mappers()
        self.table = table
        self.__init_ts()
        self.__init_nasa()
        self.__init_rss()
        self.__init_carton()
        self.session = Session()
        
    def __init_ts(self):
        table = self.table
        table['ts_user'] = Table('ts_user', self.meta,
            Column('id', Integer, primary_key=True),
            Column('name', String(40), nullable=False),
            Column('age', Integer, default=0),
            Column('password', String(40), default=''),
        )
        table['ts_email'] = Table('ts_email', self.meta,
            Column('id', Integer, primary_key=True),
            Column('address', String(40), nullable=False),
            Column('user_id', Integer, ForeignKey('ts_user.id'), nullable=False),
        )
        mapper(User, table['ts_user'], properties={
            'emails': relationship(Email),
        })
        mapper(Email, table['ts_email'], properties={
            'user': relationship(User)
        })
        
    def __init_nasa(self):
        table = self.table
        table['nasa_apod'] = Table('nasa_apod', self.meta,
            Column('url', String, primary_key=True),
            Column('date', String, nullable=False, default=''),
            Column('remark', String, nullable=False, default=''),
            Column('src', String, nullable=False, default=''),
            Column('state', String, nullable=False, default=''),
            Column('local', String, nullable=False, default=''),
        )
        mapper(Apod, table['nasa_apod'])
        
    def __init_rss(self):
        table = self.table
        table['rss_feed'] = Table('rss_channel', self.meta,
            Column('id', Integer, primary_key=True),
            Column('title', String, nullable=False, default=''),
            Column('link', String, nullable=False, default=''),
            Column('description', String, nullable=False, default=''),
            Column('author', String, nullable=False, default=''),
        )
        table['rss_item'] = Table('rss_item', self.meta,
            Column('channel_id', Integer, ForeignKey('rss_channel.id'), nullable=False),
            Column('link', String, primary_key=True),
            Column('title', String, nullable=False, default=''),
            Column('author', String, nullable=False, default=''),
            Column('contentSnippet', String, nullable=False, default=''),
            Column('content', String, nullable=False, default=''),
            Column('categories', String, nullable=False, default=''),
            Column('publishedDate', String, nullable=False, default=''),
        )
        mapper(Channel, table['rss_feed'], properties={
            'items': relationship(Item)
        })
        mapper(Item, table['rss_item'], properties={
            'channel': relationship(Channel),
        })
        
    def __init_carton(self):
        table = self.table
        table['ct_carton'] = Table('ct_carton', self.meta,
            Column('name', String, primary_key=True),
            Column('name_jp', String, nullable=False, default=''),
            Column('name_en', String, nullable=False, default=''),
            Column('week', Integer, nullable=False, default=0),#星期X更新
            Column('remark', String, nullable=False, default=''),#说明
            Column('labels', String, nullable=False, default=''),#分类标签
            Column('episode', Integer, nullable=False, default=0),#已更新集数
            Column('start', String, nullable=False, default=''),#开播时间
        )
        mapper(Carton, table['ct_carton'])
        
import os
if __name__ == '__main__':

    constr = 'sqlite:///'+os.path.dirname(__file__)[:-7]+'/info.sqlite3'
    print constr
    db = Context(constr)
    #db.table['ct_carton'].create()
