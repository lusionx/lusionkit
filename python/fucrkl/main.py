#!/usr/bin/python
# -*- coding: utf-8 -*-
# Filename: main.py
#
import sys, os, logging

# Uninstall Django 0.96.
for k in [k for k in sys.modules if k.startswith('django')]:
    del sys.modules[k]

# Add Django 1.0 archive to the path.
django_path = os.path.join(os.path.dirname(__file__),'django.zip')
#django_path = 'django.zip'
sys.path.insert(0, django_path)


# Must set this env var before importing any part of Django
os.environ['DJANGO_SETTINGS_MODULE'] = 'settings'
# Force Django to reload its settings.
from django.conf import settings
settings._target = None


def log_exception(*args, **kwds):
    logging.exception('Exception in request:')


from django.core.handlers import wsgi
from google.appengine.ext.webapp import util

def main():
  # Create a Django application for WSGI.
    application = wsgi.WSGIHandler()

  # Run the WSGI CGI handler with that application.
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
