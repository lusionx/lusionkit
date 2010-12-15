#!/usr/bin/python
# -*- coding: utf-8 -*-
# Filename: proxy.py

_version = '0.10'
theme = 'default'

import os

def get_home():
    return __import__('theme.'+theme+'.home',fromlist=['theme.'+theme+'.home'])

def get_post():
    return __import__('theme.'+theme+'.post',fromlist=['theme.'+theme+'.post'])

def get_app_folder():
    return os.path.dirname(__file__)