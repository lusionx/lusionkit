# coding: utf-8

from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import create_engine
from sqlalchemy import Column, Integer, String, DateTime

Base = declarative_base()
engine = create_engine('sqlite:///D:\\app\\db.sqlite')

class Log(Base):
    __tablename__ = 'logs'
    id = Column(Integer, primary_key=True)
    app = Column(String, nullable = False)
    when = Column(DateTime, nullable = False)

def dbInit():
    Base.metadata.create_all(engine) 

def init():
    pass


def main():
    dbInit()

if __name__ == '__main__':
    main()