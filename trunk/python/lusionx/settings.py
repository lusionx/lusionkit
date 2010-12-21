#!/usr/bin/python
# -*- coding: utf-8 -*-
# Filename: settings.py

"""
全局系统配置
"""

version = '0.10'
theme = 'default'
debug = True
templates = 'templates'
"""全局默认缓存时间(分钟)"""
cache = 60

import os, sys

app_folder = os.path.dirname(__file__)

theme_home = __import__('theme.'+theme+'.home',fromlist=['theme.'+theme+'.home'])

theme_post = __import__('theme.'+theme+'.post',fromlist=['theme.'+theme+'.post'])

theme_template = os.path.join(app_folder, 'theme', theme)
