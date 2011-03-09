# -*- coding: utf-8 -*-
#!/usr/bin/python


from sqlalchemy import *

db = create_engine('sqlite:///info.sqlite3')

db.echo = True

metadata = MetaData(db)

hr_mod = Table('hr_module', metadata,
    Column('mod_cat_key', Integer, primary_key=True),
    Column('parent_key', Integer),
    Column('module_name', String(64)),
    Column('url', String(128)),
    Column('role', String(16)),
    Column('remark', String(16)),
)
#hr_mod.create()

hr_url = Table('hr_url',metadata,
    Column('url',String(128)),
)
#hr_url.create()
