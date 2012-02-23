# -*- coding: utf-8 -*-
#!/usr/bin/python

"""
使用python 下载 npmjs.org的模块
"""

import httplib2, json, os, tarfile, uuid, sys

cfg = {}
cfg['base'] = 'D:\\app\\nodejs'
cfg['download'] = os.path.join(cfg['base'], 'tmp')
cfg['target'] = os.path.join(cfg['base'], 'node_modules')
cfg['childPkg'] = []

def cleanDir(Dir):
    if not os.path.isdir(Dir):
        print 'is not a dir'
        return
    for path in os.listdir(Dir):
        filePath = os.path.join(Dir, path)
        if os.path.isfile(filePath):
            os.remove(filePath)
        elif os.path.isdir(filePath):
            cleanDir(filePath)
            os.rmdir(filePath)
        else:
            print '%s is not file ,dir ' % filePath
    #os.removedirs(Dir)


def install(nname):
    url = 'http://registry.npmjs.org/' + nname;
    #h = httplib2.Http(".cache")
    
    #分析json 得到最新版的地址,版本号
    print 'Get package info from %s' % url
    h = httplib2.Http()
    resp, content = h.request(url, "GET")
    #content = urllib2.urlopen(url).read()
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
    #content = urllib2.urlopen(url).read()
    filename = nname + '-' + last + '.tgz'
    f = open(os.path.join(cfg['download'], filename),'wb')
    f.write(content)
    f.close()
    
    #解压 tgz文件
    print 'Extracting'
    tar = tarfile.open(os.path.join(cfg['download'], filename))
    uidf = os.path.join(cfg['download'], nname + '-' + str(uuid.uuid4()))
    tar.extractall(uidf)
    tar.close()
    
    #先删除目标目录
    if os.path.isdir(os.path.join(cfg['target'], nname)):
        cleanDir(os.path.join(cfg['target'], nname))
        os.removedirs(os.path.join(cfg['target'], nname))
    #复制文件夹到目标地址并改名
    os.rename(uidf + '/package' , os.path.join(cfg['target'], nname))
    os.removedirs(uidf)

    print 'install ' + nname + ' v' + last + ' finished!'
    if len(cfg['childPkg']) > 0:
        a = cfg['childPkg'][0]
        del cfg['childPkg'][0]
        install(a)
        

def analysisDependencies(dpd):
    for k,v in dpd.items():
        localv = 'none'
        path = os.path.join(cfg['target'], k, 'package.json')
        if os.path.isfile(path):
            f = open(path)
            localv = json.loads(f.read())['version']
            f.close()
        print 'require %s %s : local %s, ' % (k, v, localv)
        if localv == 'none':
            cfg['childPkg'].append(k)


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
        path = os.path.join(cfg['target'], nname, 'package.json')
        if os.path.isfile(path):
            f = open(path)
            info = json.loads(f.read())
            f.close()
            keys = ['name','version']
            for k in keys:
                print '%s : %s' % (k,info[k])
            if info.has_key('description'):
                k = 'description'
                print '%s : %s' % (k,info[k])
            if info.has_key('dependencies') and len(info['dependencies']):
                print 'dependencies:'
                for k,v in info['dependencies'].items():
                    print '    %s : %s' % (k, v)
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
        

import argparse

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    group = parser.add_mutually_exclusive_group(required=True)
    group.add_argument('-r', action="store_true", help='remove pkg')
    group.add_argument('-i', action="store_true", help='install pkg')
    group.add_argument('-s', action="store_true", help='show pkg')
    parser.add_argument('PKG', nargs=1, help='pkg name')
    results = parser.parse_args()
    if results.r:
        remove(results.PKG[0])
    if results.i:
        install(results.PKG[0])
    if results.s:
        show(results.PKG[0])

