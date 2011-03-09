# -*- coding: utf-8 -*-
#!/usr/bin/python

'''
生成所有页面的地址,和对应的功能
'''

import os
import BeautifulSoup as bsp


filelist=[]

def sanc_dir(dirpath, dirname):
    isdir = dirname in ('.svn','obj','HTML_Test','test')
    return not isdir

def include_file(dirpath, filename):
    f,ext=os.path.splitext(filename)
    return ext == '.aspx'


def scan(dirpath):
    for path in os.listdir(dirpath):
        fullpath = os.path.join(dirpath, path)
        if os.path.isdir(fullpath) and sanc_dir(dirpath, path):
            scan(fullpath)
        if os.path.isfile(fullpath) and include_file(dirpath, path):
            filelist.append(fullpath)


import dbtables as tbs
import extPy

def inserUrl():
    dir =os.path.todir(u'D:\91huayi\健康档案\Web')
    scan(dir)
    #生成了所有的页面地址
    filelist2 = map(lambda a: '~'+a[len(dir):].replace('\\','/'),filelist)
    for a in filelist2:
        i = tbs.hr_url.insert()
        i.execute(url = a)

def inserMod():
    #读取数据库导出的所有的模块的xml
    f = open(u'D:/admin/Desktop/doc/sys_module.xml')
    soup = bsp.BeautifulSoup(f.read())
    modules = map(lambda a:dict(
        mod_cat_key=a.mod_cat_key.string,
        parent_key=a.parent_key.string,
        module_name=a.module_name.string,
        url=a.url.string,
        sort_order=a.sort_order.string),soup.findAll('record'))
    for a in modules:
        i = tbs.hr_mod.insert()
        i.execute(a)
    f.close()
    
def main():
    """合并2个表的数据"""
    


if __name__ == '__main__':
    main()
