# coding: utf-8
"""
��ָ���ļ����µ�ֱ���ļ�ѹ����һ��zip
���������ļ���ͬ����λ��
"""
import os
import zipfile

def main():
    zipDir('D:\\lusionx\\Pictures\\H��а��\\ѩŮ')
    
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