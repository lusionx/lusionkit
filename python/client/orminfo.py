# -*- coding: utf-8 -*-
#!/usr/bin/python

from sqlalchemy import create_engine
from sqlalchemy.schema import Table, MetaData, Column, ForeignKey
from sqlalchemy.orm import mapper, Session, clear_mappers, relationship, scoped_session, sessionmaker
from sqlalchemy.types import Integer, String
import os

class BaseModel(object):
    def tdict(self):
        """将实例转换为字典"""
        d = self.__dict__
        k = '_sa_instance_state'
        if d.has_key(k):
            del d[k]
        return d
    def fdict(self,dic):
        """用一个字典初始化本实例"""
        pass

class User(BaseModel):
    def fun1(self):
        return 'fun1'

class Email(BaseModel):
    pass
    
def init(create = False):
    path = os.path.dirname(__file__)
    db = create_engine('sqlite:///'+path+'/info.sqlite3')
    #db.echo = True
    metadata = MetaData(db)
    users = Table('ts_user', metadata,
        Column('id', Integer, primary_key=True),
        Column('name', String(40), nullable=False),
        Column('age', Integer, default=0),
        Column('password', String(40), default=''),
    )
    emails = Table('ts_email', metadata,
        Column('id', Integer, primary_key=True),
        Column('address', String(40), nullable=False),
        Column('user_id', Integer, ForeignKey('ts_user.id'), nullable=False),#
    )
    if create:
        users.create()
        emails.create()
    clear_mappers()
    mapper(Email, emails, properties={
        'user': relationship(User)
    })
    mapper(User, users, properties={
        'emails': relationship(Email),
    })
    #return scoped_session(sessionmaker(bind=db))
    return Session()

if __name__ == '__main__':
    pass
    
    
