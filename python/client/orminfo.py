# -*- coding: utf-8 -*-
#!/usr/bin/python

from sqlalchemy import create_engine
from sqlalchemy.schema import Table, MetaData, Column, ForeignKey
from sqlalchemy.orm import mapper, Session, clear_mappers, relationship
from sqlalchemy.types import *

class User(object):
    pass
class Email(object):
    pass
    
def init():
    db = create_engine('sqlite:///info.sqlite3')
    #db.echo = True
    metadata = MetaData(db)
    
    users = Table('users', metadata,
        Column('id', Integer, primary_key=True),
        Column('name', String(40), nullable=False),
        Column('age', Integer, default=0),
        Column('password', String(40), default=''),
    )
    
    emails = Table('emails', metadata,
        Column('id', Integer, primary_key=True),
        Column('address', String(40), nullable=False),
        Column('user_id', Integer, ForeignKey('users.id'), nullable=False),#
    )
    
    clear_mappers()
    
    mapper(Email, emails, properties={
        'user': relationship(User)
    })
    mapper(User, users, properties={
        'emails': relationship(Email),
    })

    
def curr():
    init()
    return Session()
    
    
    
if __name__ == '__main__':

    ss = curr()
    man = ss.query(User).filter(User.name == 'John').first()
    print man.name
    lxs = man.emails
    print ','.join([a.address for a in lxs])
    mail = ss.query(Email).filter(Email.id == 1).first()
    print mail.address
    print mail.user.name
