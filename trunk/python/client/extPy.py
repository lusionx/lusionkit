# -*- coding: utf-8 -*-
#!/usr/bin/python

"""
为内置类,扩展一些常用的方法
"""

import os

def todir(path):
    """将目录用'/'分割,并保证以'/'结尾"""
    path=path.replace('\\','/')
    if path.rfind('/') != len(path):
        path += '/'
    return path
    
os.path.todir = todir



def fix(path):
    """处理路径,替换'\\'->'/'"""
    return path.replace('\\','/')
    
os.path.fix = fix


