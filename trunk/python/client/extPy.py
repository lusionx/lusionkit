# -*- coding: utf-8 -*-
#!/usr/bin/python

"""
为内置类,扩展一些常用的方法
"""

import os

def _todir(path):
    """将目录用'/'分割,并保证以'/'结尾"""
    path=path.replace('\\','/')
    if path.rfind('/') != len(path):
        path += '/'
    return path
os.path.todir = _todir



def _fix(path):
    """处理路径,替换'\\'->'/'"""
    return path.replace('\\','/')
os.path.fix = _fix

class adDict():
    def __init__(self,data):
        self.d = data
    def __getattr__(self,name):
        return adDict(self.d[name])
    def __getitem__(self,i):
        return adDict(self.d[i])
    def __getslice__(self,i=None,j=None):
        return adDict(self.d[i:j])
    def __str__(self):
        return str(self.d)
    def text(self):
        return self.d
