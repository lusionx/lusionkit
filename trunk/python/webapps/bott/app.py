# coding: utf-8

from bottle import route, run, template

@route('/')
def index():
    return 'home page'

@route('/hello/:name')
def hello(name='World'):
    return template('<b>Hello {{name}}</b>!', name=name)

run(host='localhost', port=8080)