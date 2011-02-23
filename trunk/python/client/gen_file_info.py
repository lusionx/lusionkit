# -*- coding: utf-8 -*-
#!/usr/bin/python

'''
将此文件放在某处执行,将生成所在文件夹下所有文件的列表xml,
默认排除隐藏文件
'''

import os

filelist=[]

def ss(dirpath):
    for path in os.listdir(dirpath):
        fullpath = os.path.join(dirpath,path)
        if os.path.isdir(fullpath):
            ss(fullpath)
        elif os.path.isfile(fullpath):
            filelist.append(fullpath)
        else:
            pass


def main():
    #ss(os.path.dirname(__file__))
    ss(u'D:\\admin\\Desktop\\always')
    for path in filelist:
        f = open(path)
        str = f.read()
        if str.find('最具SEO优化性能的管理平台，请点击访问官网') > -1:
            print path

if __name__ == '__main__':
    main()
