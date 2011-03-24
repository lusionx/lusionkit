# -*- coding: utf-8 -*-
#!/usr/bin/python

from sqlalchemy import create_engine
from sqlalchemy.schema import Table, MetaData, Column, ForeignKey
from sqlalchemy.orm import mapper, Session, clear_mappers, relationship
from sqlalchemy.types import Integer, String

class BaseObj(object):
    def tdict(self):
        """将实例转换为字典"""
        d = self.__dict__
        k = '_sa_instance_state'
        #剔除sqlalchemy给对象的状态字段
        if d.has_key(k):
            del d[k]
        return d
    def __init__(self,dic=None):
        """用一个字典初始化本实例,递归到底层
        主要是用来接收 由json串反序列化出来的字典
        """
        if dic == None:
            return
        for k,v in dic.items():
            exec('self.'+k+'=v')
            if type(v) == list:
                for i in xrange(len(v)):
                    if type(v[i]) == dict:
                        v[i] = BaseObj(v[i])

        
class User(BaseObj):
    pass

class Email(BaseObj):
    pass
    
class Apod(BaseObj):
    pass

class Context():
    def __init__(self,constr):
        db = create_engine(constr)
        metadata = MetaData(db)
        table = {}
        clear_mappers()
        table['users'] = Table('ts_user', metadata,
            Column('id', Integer, primary_key=True),
            Column('name', String(40), nullable=False),
            Column('age', Integer, default=0),
            Column('password', String(40), default=''),
        )
        table['emails'] = Table('ts_email', metadata,
            Column('id', Integer, primary_key=True),
            Column('address', String(40), nullable=False),
            Column('user_id', Integer, ForeignKey('ts_user.id'), nullable=False),
        )
        table['apods'] = Table('nasa_apod', metadata,
            Column('url', String, primary_key=True),
            Column('date', String, nullable=False, default=''),
            Column('remark', String, nullable=False, default=''),
            Column('src', String, nullable=False, default=''),
            Column('state', String, nullable=False, default=''),
            Column('local', String, nullable=False, default=''),
        )

        mapper(Email, table['emails'], properties={
            'user': relationship(User)
        })
        mapper(User, table['users'], properties={
            'emails': relationship(Email),
        })
        mapper(Apod, table['apods'])
        
        self.table = table
        self.session = Session()
