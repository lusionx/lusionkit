# -*- coding: utf-8 -*-
#!/usr/bin/python

"""
使用python 下载 npmjs.org的模块
"""

import httplib2, json, os, tarfile, uuid, sys

cfg = {}
cfg['download'] = 'D:/DevelopTool/nodejs/tmp/'
cfg['target'] = 'D:/DevelopTool/nodejs/lib/node/'

def cleanDir(Dir):
    if not os.path.isdir(Dir):
        print 'is not a dir'
        return
    for path in os.listdir(Dir):
        filePath = os.path.join(Dir, path)
        if os.path.isfile(filePath):
            os.remove(filePath)
        elif os.path.isdir(filePath):
            os.path.isdir(filePath)
            cleanDir(filePath)
            os.rmdir(filePath)
        else:
            print '%s is not file ,dir ' % filePath
    #os.removedirs(Dir)


def install(nname):
    url = 'http://registry.npmjs.org/'+nname;
    h = httplib2.Http(".cache")
    
    #分析json 得到最新版的地址,版本号
    print 'Get package info from %s' % url
    resp, content = h.request(url, "GET")
    info = json.loads(content)
    
    #可能有找不到的情况
    if info.has_key('error'):
        print "Can't find %s" % nname
        return
    last = info['dist-tags']['latest']
    
    print 'The last verson is %s' % last
    
    lastverson = info['versions'][last]
    url = lastverson['dist']['tarball']
    dependencies = {}
    if lastverson.has_key('dependencies'):
        dependencies = lastverson['dependencies']
    #处理依赖
    analysisDependencies(dependencies)

    #下载 tgz文件
    print 'Download from %s' % url
    resp, content = h.request(url,'GET')
    filename = nname + '-' + last + '.tgz'
    f = open(cfg['download'] + filename,'wb')
    f.write(content)
    f.close()
    
    #解压 tgz文件
    print 'Extracting'
    tar = tarfile.open(cfg['download'] + filename)
    uidf = cfg['download'] + nname + '-' + str(uuid.uuid4())
    tar.extractall(uidf)
    tar.close()
    
    #先删除目标目录
    if os.path.isdir(cfg['target'] + nname):
        cleanDir(cfg['target'] + nname)
        os.removedirs(cfg['target'] + nname)
    #复制文件夹到目标地址并改名
    os.rename(uidf + '/package' , cfg['target'] + nname)
    os.removedirs(uidf)

    print 'install ' + nname + ' v' + last + ' finished!'

def analysisDependencies(dpd):
    for k,v in dpd.items():
        localv = 'none'
        path = cfg['target'] + k + '/package.json'
        if os.path.isfile(path):
            f = open(path)
            localv = json.loads(f.read())['version']
            f.close()
        print 'require %s : local %s %s, ' % (k, localv, v)


def remove(nname):
    dirpath = cfg['target'] + nname
    if os.path.isdir(dirpath):
        cleanDir(dirpath)
        os.removedirs(dirpath)
        print 'Remove %s from lib(%s)' % (nname, cfg['target'])
        return True
    else:
        print "Can't find %s from lib(%s)" % (nname, cfg['target'])
        return False

def show(name):
    def showone(nname):
        path = cfg['target'] + nname + '/package.json'
        if os.path.isfile(path):
            f = open(path)
            info = json.loads(f.read())
            f.close()
            keys = ['name','version','description']
            for k in keys:
                print '%s : %s' % (k,info[k])
            if info.has_key('dependencies'):
                print '%s : %s' % ('dependencies',info['dependencies'])
            print ''
    if name == 'all':
        i = 0
        for path in os.listdir(cfg['target']):
            if os.path.isdir(os.path.join(cfg['target'],path)) and path[0] != '.':
                showone(path)
                i+=1
        print 'There are %d packages.' % i
    else:
        showone(name)
        

if __name__ == '__main__':
    act = {}
    act['install'] = install
    act['remove'] = remove
    act['show'] = show
    if act.has_key(sys.argv[1]) and len(sys.argv) == 3 and len(sys.argv[2]) > 0:
        act[sys.argv[1]](sys.argv[2])
    else:
        print """e.g.
python npm.py install <packagename>
python npm.py remove <packagename>
python npm.py show <all | packagename>
"""

