# -*- coding: utf-8 -*-
#!/usr/bin/python

import ctypes, os, sys
import Image, ImageDraw
#from lxml import etree

types = ['.jpg', '.png']

def getWallpapers(dirpath):
    try:
        directory = os.listdir(dirpath)
    except WindowsError:
        print "Directory does not exist"
        exit(0)
    wallpapers = []
    for file in directory:
        ext = os.path.splitext(file)[1]
        if ext in types:
            wallpapers.append(os.path.join(dirpath,file))
    return wallpapers
    
def set(path):
    try:#这里可能有未知的图片格式错误,如果出错就跳过
        im = Image.open(path)
        #draw = ImageDraw.Draw(im)
        #draw.text((20,im.size[1]-20),path) 不能写中文?
        path = os.path.join(os.path.dirname(__file__),'wallpaper.bmp')
        width, height = im.size
        if width < height:
            im = im.transpose(Image.ROTATE_90)
        im.save(path)
        #del draw
        ctypes.windll.user32.SystemParametersInfoA(20, 0, path , 0)
        return True
    except:
        return False

import random
def main2(argv):#通过xml 获取目录
    cfg = os.path.join(os.path.dirname(__file__),'doc/Wallpapers.xml')
    doc = etree.parse(cfg)
    pics = []
    for a in doc.getroot()[0]:
        pics.extend(getWallpapers(a.text))
    #try:
    #    i = pics.index(doc.getroot()[1].text) + 1
    #    i = i % len(pics)
    #except ValueError:
    #    i = 0
    path = random.choice(pics)
    set(path)
    print path
    #doc.getroot()[1].text = path.encode('utf-8')
    #doc.write(cfg,encoding = "utf-8")
    
def main(root=u"D:\\lusionx\\H和邪社"):#就用和谐社的图片
    pics = []
    for a in os.listdir(root):
        path = os.path.join(root,a)
        #print path
        if os.path.isdir(path) :
            pics.extend(getWallpapers(path))
    path = random.choice(pics)
    if set(path):
        print path#.encode('utf-8')
        #open(os.path.join(os.path.dirname(__file__),'wallpaper.txt'),'a').write(path.encode('utf-8')+'\n')
    else:
        print 'error:' + path
        open(os.path.join(os.path.dirname(__file__),'wallpaper.txt'),'a').write('error:'+path.encode('utf-8')+'\n')

if __name__ == "__main__":
    #main(sys.argv)
    main()
