#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.http import Http404, HttpResponse
import models
import datetime

def getnow(request):
    now = datetime.datetime.now()
    html = "<html><body>It is now %s.</body></html>" % now
    return HttpResponse(html)

def hello(request):
    return HttpResponse("你好 django")

def hours_ahead(request, offset):
    try:
        offset = int(offset)
    except ValueError:
        raise Http404()
    dt = datetime.datetime.now() + datetime.timedelta(hours=offset)
    #assert False
    html = "<html><body>In %s hour(s), it will be %s.</body></html>" % (offset, dt)
    return HttpResponse(html)

def model(requset):
    return HttpResponse(models.__doc__)
