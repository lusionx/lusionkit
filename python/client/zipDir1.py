# coding: utf-8
"""
把指定文件夹下的直接文件压缩成一个zip
保存在与文件夹同级的位置
"""
import os
import zipfile

def main():
    zipDir('D:\\lusionx\\Pictures\\H和邪社\\雪女')
    
def zipDir(dirpath):
    filelist = []
    fullpath = ''
    for path in os.listdir(dirpath):
        fullpath = os.path.join(dirpath, path)
        if os.path.isfile(fullpath):
            filelist.append(fullpath)
        else:
            print "except:" + fullpath
    myzip = zipfile.ZipFile(os.path.basename(dirpath) + '.zip', 'w')
    for path in filelist:
        myzip.write(path,os.path.basename(path))
    myzip.close()

if __name__ == '__main__':
    main()