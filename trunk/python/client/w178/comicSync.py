# coding:utf-8
"""
同步漫画
"""

import os,shutil

source = u'D:\\lusionx\\Pictures\\漫画'

root = os.path.abspath(os.getcwdu())


def joinp(a,b):
    return os.path.join(a,b)

def main():
    #u盘上的文件夹
    uDirs = [a for a in os.listdir(root) if os.path.isdir(joinp(root, a))]
    for dir in uDirs:
        if os.path.isdir(joinp(source, dir)):#源上 有对应的 文件夹
            #在 u 上 找 最后的 *.zip
            uLast = [a for a in os.listdir(joinp(root, dir)) if os.path.splitext(a)[1] == u'.zip']
            szips = [a for a in os.listdir(joinp(source, dir)) if os.path.splitext(a)[1] == u'.zip']
            if len(uLast) == 0:#文件夹是空的，全部复制
                copys = szips[:]
            else:#找寻在 源上 uLast 之后的 文件
                copys = szips[szips.index(uLast[-1]) + 1:]
            for a in copys:
                f1 = os.path.join(source,dir,a)
                f2 = os.path.join(root,dir,a)
                print 'copy %s to %s = %s' % (f1,f2,shutil.copyfile(f1,f2))

if __name__ == '__main__':
    main()