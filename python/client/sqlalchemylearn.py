# -*- coding: utf-8 -*-
#!/usr/bin/python

"""sqlalchemy 使用示例代码
"""
from sqlalchemy import *

def run(stmt):
    rs = stmt.execute()
    for row in rs:
        print row

def sqlitedb():
    db = create_engine('sqlite:///info.sqlite3')
    db.echo = False
    users = Table('btktpx', MetaData(db), autoload=True)
    s = users.select(users.c.btid == 182720)
    run(s)
    
def mysqldb():
    db = create_engine('mysql://root:444444@127.0.0.1:3306/alx',encoding='utf8')
    db.echo = False
    proxy = Table('proxy', MetaData(db), autoload=True)
    s = proxy.select()
    run(s)
    
def main():
    #sqlitedb()
    mysqldb()

if __name__ == '__main__':
    main()

