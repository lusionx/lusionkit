# -*- coding: utf-8 -*-
#!/usr/bin/python

from sqlalchemy import create_engine
from sqlalchemy.schema import Table, MetaData, Column, ForeignKey
from sqlalchemy.orm import mapper, Session, clear_mappers, relationship, scoped_session, sessionmaker
from sqlalchemy.types import Integer, String

class User(object):
    pass
class Email(object):
    pass
    
def init(create = False):
    db = create_engine('sqlite:///info.sqlite3')
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
    return scoped_session(sessionmaker(bind=db))

def getname():
    ss = init()
    a = ss.query(User).first()
    return a.name

import re
if __name__ == '__main__':
    print getname()
    a = 'dasd ? dsada:dd?aa.'
    
    
