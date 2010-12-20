#!/usr/bin/python
# -*- coding: utf-8 -*-
# File: home.py
# User: Administrator
# Date: 2010-12-15
# Time: 20:41:15 

"""what?"""
import models
def get():
    a = '<a href="/admin" >管理</a>'
    return '我从default主题来' + a

def main():
    return get()
  
