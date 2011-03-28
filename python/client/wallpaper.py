# -*- coding: utf-8 -*-
#!/usr/bin/python

import ctypes, os, sys
import Image, ImageDraw
from lxml import etree


def getWallpapers(dirpath):
    try:
        directory = os.listdir(dirpath)
    except WindowsError:
        print "Directory does not exist"
        exit(0)
    wallpapers = []
    types = ['.jpg', '.png']
    for file in directory:
        ext = os.path.splitext(file)[1]
        if ext in types:
            wallpapers.append(os.path.join(dirpath,file))
    return wallpapers
    
def set(path):
    try:#这里可能有未知的图片格式错误,如果出错就跳过
        im = Image.open(path)
        draw = ImageDraw.Draw(im)
        draw.text((20,im.size[1]-20),path)
        path = os.path.join(os.path.dirname(__file__),'wallpaper.bmp')
        im.save(path)
        del draw
        ctypes.windll.user32.SystemParametersInfoA(20, 0, path , 0)
    except:
        pass


def main(argv):
    cfg = os.path.join(os.path.dirname(__file__),'doc/Wallpapers.xml')
    doc = etree.parse(cfg)
    pics = []
    for a in doc.getroot()[0]:
        pics.extend(getWallpapers(a.text))
    try:
        i = pics.index(doc.getroot()[1].text) + 1
        i = i % len(pics)
    except ValueError:
        i = 0
    path = pics[i]
    set(path)
    doc.getroot()[1].text = path
    doc.write(cfg,encoding = "utf-8")


if __name__ == "__main__":
    #main(sys.argv)
    main('')
