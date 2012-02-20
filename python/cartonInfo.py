# coding: utf-8

from bottle import route, get, post, run
from bottle import request, response, template, redirect
import os, datetime
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import create_engine, Column, Integer, String, DateTime

Base = declarative_base()
engine = create_engine('sqlite:///D:\\app\\db.sqlite')

def Session():
    from sqlalchemy.orm import sessionmaker
    Session = sessionmaker(bind=engine)
    return Session()

class Carton(Base):
    __tablename__ = 'cartons'
    id = Column(Integer, primary_key = True)
    name = Column(String, nullable = False)
    name2 = Column(String)
    updatetime = Column(DateTime, nullable = False)
    lastest = Column(Integer, nullable= False)
    link = Column(String, default='')
    def __init__(self):
        pass

@route('/favicon.ico')
def favicon():
    redirect("http://bottlepy.org/docs/dev/_static/favicon.ico")

@route('/')
def r_index():
    htm = u'<p><a href="/add">添加</a></p>'
    ss = Session()
    for a in ss.query(Carton).order_by(Carton.updatetime):
        htm += u'<p>'
        htm += u'<a href="%s" target="_blank">%s</a>' % (a.link,a.name)
        htm += u'<i>%s, %s</i>' % (a.updatetime, a.lastest)
        htm += u'</p>'
    return htm

@get('/add')
def r_add():
    return u'''
<form method="POST">
    <p>name<input name="name" type="text" /></p>
    <p>name2<input name="name2" type="text" /></p>
    <p>updatetime<input name="updatetime" type="text" /></p>
    <p>lastest<input name="lastest" type="text" /></p>
    <p>link<input name="link" type="text" /></p>
    <p><input type="submit" /></p>
</form>'''

@post('/add')
def r_add_p():
    o = Carton()
    o.name = request.forms.name
    o.name2 = request.forms.name2
    print request.forms.updatetime
    o.updatetime = datetime.datetime.strptime(request.forms.updatetime.strip(),'%Y-%m-%d')
    o.lastest = request.forms.lastest
    ss = Session()
    ss.add(o)
    ss.commit()
    return redirect('/')

if __name__ == '__main__':
    #Base.metadata.create_all(engine)
    run(host='localhost', port=8080)


