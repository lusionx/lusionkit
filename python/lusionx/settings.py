#!/usr/bin/python
# -*- coding: utf-8 -*-
# Filename: proxy.py

"""
全局系统配置
"""

version = '0.10'
theme = 'default'
debug = True
templates = 'templates'

import os, sys

app_folder = os.path.dirname(__file__)

theme_home = __import__('theme.'+theme+'.home',fromlist=['theme.'+theme+'.home'])

theme_post = __import__('theme.'+theme+'.post',fromlist=['theme.'+theme+'.post'])

theme_template = os.path.join(app_folder, 'theme', theme)
